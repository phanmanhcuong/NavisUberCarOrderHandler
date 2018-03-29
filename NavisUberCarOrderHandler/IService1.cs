using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NavisUberCarOrderHandler
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/CarOrderRequestReceiver",
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        string CarOrderRequestReceiver(Stream stream);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    internal class CarOrder
    {
        [DataMember]
        internal string originPlace = null;

        [DataMember]
        internal string destinationPlace = null;

        [DataMember]
        internal string carType = null;

        [DataMember]
        internal string pickupTime = null;

        [DataMember]
        internal string phoneNumber = null;
    }
}
