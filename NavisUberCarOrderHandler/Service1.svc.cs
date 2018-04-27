using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace NavisUberCarOrderHandler
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        //handle request
        public string CarOrderRequestReceiver(Stream orderStream)
        {
            StreamReader reader = new StreamReader(orderStream, Encoding.UTF8);
            string carOrderRequest = reader.ReadToEnd();
            var items = carOrderRequest.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Split(new[] { '=' }));

            Dictionary<string, string> carOrder = new Dictionary<string, string>();
            foreach (var item in items)
            {
                carOrder.Add(item[0], item[1]);
            }

            using (DataClasses2DataContext db = new DataClasses2DataContext())
            {
                string response = saveRequestDataToDb(db, carOrder);
                return response;
            }
        }

        //save data to db
        private string saveRequestDataToDb(DataClasses2DataContext db, Dictionary<string, string> carOrder)
        {
            string success = "Success";
            string fail = "Fail";

            carOrder.TryGetValue("originPlace", out string originPlace);
            carOrder.TryGetValue("originLatitude", out string orignLatitude);
            carOrder.TryGetValue("originLongitude", out string originLongitude);
            carOrder.TryGetValue("destinationPlace", out string destinationPlace);
            carOrder.TryGetValue("destinationLat", out string destinationLatitude);
            carOrder.TryGetValue("destinationLong", out string destinationLongitude);
            carOrder.TryGetValue("carType", out string carType);
            carOrder.TryGetValue("orderTime", out string orderTime);
            carOrder.TryGetValue("pickupTime", out string pickupTimeString);
            carOrder.TryGetValue("phoneNumber", out string phoneNumber);
            carOrder.TryGetValue("refreshed_token", out string registrationID);

            //decode
            Lst_DatXe newOrder = new Lst_DatXe();
            newOrder.diem_bat_dau = HttpUtility.UrlDecode(originPlace);
            newOrder.lat_bat_dau = Convert.ToDouble(HttpUtility.UrlDecode(orignLatitude));
            newOrder.long_bat_dau = Convert.ToDouble(HttpUtility.UrlDecode(originLongitude));
            newOrder.diem_ket_thuc = HttpUtility.UrlDecode(destinationPlace);
            newOrder.lat_ket_thuc = Convert.ToDouble(HttpUtility.UrlDecode(destinationLatitude));
            newOrder.long_ket_thuc = Convert.ToDouble(HttpUtility.UrlDecode(destinationLongitude));

            //split: ten xe - so ghe
            string car_type = HttpUtility.UrlDecode(carType);
            string[] seatNumber = car_type.Split('-');
            int soGhe = Convert.ToInt32(seatNumber[1]);
            newOrder.so_ghe = soGhe;

            Table<Lst_LoaiXe> listLoaiXe = db.GetTable<Lst_LoaiXe>();
            newOrder.id_loai_xe = (from idloaixe in listLoaiXe where Convert.ToInt32(idloaixe.so_ghe) == soGhe select idloaixe.id_loai_xe).First();         

            string orderTimeDecoded = HttpUtility.UrlDecode(orderTime); 
            newOrder.thoi_diem_dat_xe = DateTime.ParseExact(orderTimeDecoded, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            string pickupTimeDecoded = HttpUtility.UrlDecode(pickupTimeString);
            newOrder.thoi_diem_khoi_hanh = DateTime.ParseExact(pickupTimeDecoded, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

            newOrder.sdt_nguoi_dat = HttpUtility.UrlDecode(phoneNumber);
            newOrder.status = 0;
            newOrder.registrationID = HttpUtility.UrlDecode(registrationID);

            try
            {
                db.Lst_DatXes.InsertOnSubmit(newOrder);
                db.SubmitChanges();
                return success;
            
            }
            catch (Exception e) 
            {
                return fail;
            }

        }

        //get cartype to send to app
        public List<string> GetCarType()
        {
            List<string> carTypeList = new List<string>();

            DataClasses2DataContext db = new DataClasses2DataContext();

            Table<Lst_LoaiXe> listLoaiXe = db.GetTable<Lst_LoaiXe>();
            var query = from cartype in listLoaiXe select cartype;
            //concate car type - number of seat
            foreach (var cartype in query)
            {
                carTypeList.Add(cartype.ten_loai_xe + " - " + cartype.so_ghe);
            }

            return carTypeList;
        }

        //handle request from trackingmanagement web page
        public void HandlePushRequest(Stream dataStream)
        {
            StreamReader dataReader = new StreamReader(dataStream, Encoding.UTF8);
            string notiData = dataReader.ReadToEnd();

            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
            ID idData = scriptSerializer.Deserialize<ID>(notiData);

            int carID = idData.id_xe;
            int orderID = idData.id_dat_xe;

            GetInfoFromDb(carID, orderID);                  
        }

        //get info to send request to firebase server
        private void GetInfoFromDb(int carID, int orderID)
        {
            DataClasses2DataContext db = new DataClasses2DataContext();
            //get car order from Lst_DatXe
            Table<Lst_DatXe> lst_datxe = db.GetTable<Lst_DatXe>();
            var queryOrder = from o in lst_datxe where o.id_dat_xe == orderID select o;
            var order = queryOrder.FirstOrDefault();

            //get car information from Lst_Xe
            Table<Lst_Xe> lst_xe = db.GetTable<Lst_Xe>();
            var queryCar = from c in lst_xe where c.id_xe == carID select c;
            var car = queryCar.FirstOrDefault();

            //get driver's information from Lst_LaiXe
            Table<Lst_LaiXe> lst_laixe = db.GetTable<Lst_LaiXe>();
            var queryDriver = from d in lst_laixe where d.id_lai_xe == car.id_lai_xe_chinh select d;
            var driver = queryDriver.FirstOrDefault();

            RequestFirebaseSendNoti(order, car, driver);
        }

        //send data to push notification from firebase server
        private void RequestFirebaseSendNoti(Lst_DatXe order, Lst_Xe car, Lst_LaiXe driver)
        {
            string serverKey = "AAAA62U35Pg:APA91bHm0D9udChK9kBnoZP_5yUDHYOPXy62a4pTa_bTbdpEYY2-Em727VMPElPgm0aXRXjDGFwBltn6ZsO9snHZne6rcR9JhsejNnm0JVqpuEAjZzdcymKXy5bHbGMDYGcRJsc_thFT";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add(HttpRequestHeader.Authorization, "key=" + serverKey);

            string notiTitle = "Thông báo xe đã nhận:";
            string notiMessage = "- Từ: " + order.diem_bat_dau.Trim() + "." + Environment.NewLine + "- Đến: " + order.diem_ket_thuc.Trim() + "."
                 + Environment.NewLine + "- Xe: " + car.loai_xe  + Environment.NewLine + "- Biển kiểm soát: " + car.bien_kiem_soat
                 + Environment.NewLine + "- Lái xe: " + driver.ten_lai_xe + Environment.NewLine + "- Số điện thoại: " + driver.so_dien_thoai
                 + Environment.NewLine + "- Khởi hành: " + order.thoi_diem_khoi_hanh;
            var data = new
            {
                to = order.registrationID,
                notification = new
                {
                    title = notiTitle,
                    body = notiMessage
                }
            };

            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(data);
            Byte[] byteArray = Encoding.UTF8.GetBytes(json);
            request.ContentLength = byteArray.Length;

            using (Stream notiStream = request.GetRequestStream())
            {
                notiStream.Write(byteArray, 0, byteArray.Length);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream dataStreamResponse = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(dataStreamResponse))
                        {
                            String responseMessage = reader.ReadToEnd();
                            Console.WriteLine(responseMessage);
                        }
                    }
                }
            }
        }
    }

    public class ID
    {
        public int id_xe { get; set; }
        public int id_dat_xe { get; set; }
    }
}
