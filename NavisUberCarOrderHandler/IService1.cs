﻿using System;
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
        // send string
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/CarOrderRequestReceiver",
        BodyStyle = WebMessageBodyStyle.Bare)]
        string CarOrderRequestReceiver(Stream stream);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetCarType",
        BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        List<string> GetCarType();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/HandlePushRequest",
        BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json)]
        void HandlePushRequest(Stream dataStream);
    }


    // not use
    [DataContract]
    public class CarOrder
    {
        [DataMember]
        public string originPlace { get; set; } 

        [DataMember]
        public string destinationPlace { get; set; }

        [DataMember]
        public string carType { get; set; }

        [DataMember]
        public DateTime pickupTime { get; set; }

        [DataMember]
        public string phoneNumber { get; set; } 
     
    }
}
