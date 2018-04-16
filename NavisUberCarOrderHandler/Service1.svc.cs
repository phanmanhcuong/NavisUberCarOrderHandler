using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

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
    }


}
