using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NavisUberCarOrderHandler
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string CarOrderRequestReceiver(Stream orderStream)
        {
            // convert Stream Data to StreamReader
            StreamReader reader = new StreamReader(orderStream);
            // Read StreamReader data as string
            string carOrderRequest = reader.ReadToEnd();

            CarOrder carOrder = new CarOrder();
            MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(carOrderRequest));
            DataContractJsonSerializer deserializer = new DataContractJsonSerializer(carOrder.GetType());
            carOrder = deserializer.ReadObject(memoryStream) as CarOrder;

            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                string response = saveRequestDataToDb(db, carOrder);
                return response;
            
            }
        }

        private string saveRequestDataToDb(DataClassesDataContext db, CarOrder carOrder)
        {
            string success = "Success";

            Car_Order newOrder = new Car_Order();
            newOrder.origin_place = carOrder.originPlace;
            newOrder.destination_place = carOrder.destinationPlace;
            newOrder.car_type = carOrder.carType;
            newOrder.pickup_time = DateTime.ParseExact(carOrder.pickupTime, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            newOrder.contact_number = carOrder.phoneNumber;

            db.Car_Orders.InsertOnSubmit(newOrder);
            db.SubmitChanges();

            return success;
        }
    }
}
