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

            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                string response = saveRequestDataToDb(db, carOrder);
                return response;

            }
        }

        private string saveRequestDataToDb(DataClassesDataContext db, Dictionary<string, string> carOrder)
        {
            string success = "Success";
            string fail = "Fail";

            carOrder.TryGetValue("originPlace", out string originPlace);
            carOrder.TryGetValue("destinationPlace", out string destinationPlace);
            carOrder.TryGetValue("carType", out string carType);
            carOrder.TryGetValue("pickupTime", out string pickupTimeString);
            carOrder.TryGetValue("phoneNumber", out string phoneNumber);

            string decodedUrl = HttpUtility.UrlDecode(originPlace);

            Car_Order newOrder = new Car_Order();
            newOrder.origin_place = HttpUtility.UrlDecode(originPlace);
            newOrder.destination_place = HttpUtility.UrlDecode(destinationPlace);
            newOrder.car_type = HttpUtility.UrlDecode(carType);
            string pickupTimeDecoded = HttpUtility.UrlDecode(pickupTimeString);
            newOrder.pickup_time = DateTime.ParseExact(pickupTimeDecoded, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            newOrder.contact_number = HttpUtility.UrlDecode(phoneNumber);

            try
            {
                db.Car_Orders.InsertOnSubmit(newOrder);
                db.SubmitChanges();
                return success;
            
            }
            catch (Exception)
            {
                return fail;
            }

        }

        public List<string> GetCarType()
        {
            List<string> carTypeList = new List<string>();
            DataClassesDataContext db = new DataClassesDataContext();
            Table<Lst_LoaiXe> listLoaiXe = db.GetTable<Lst_LoaiXe>();
            var query = from cartype in listLoaiXe select cartype;
            foreach (var cartype in query)
            {
                carTypeList.Add(cartype.ten_loai_xe);
            }

            return carTypeList;
        }
    }


}
