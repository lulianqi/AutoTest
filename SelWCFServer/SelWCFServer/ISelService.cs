using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace SelWCFServer
{
    //class ISelService
    //{
    //}

    [ServiceContract]
    public interface ISelService
    {
        [OperationContract]
        string SayHello(int vaule);

        [OperationContract(IsOneWay=true)]
        void SayBye(int vaule);

        [OperationContract]
        string WhoIs(MyData myData);

    }

    [ServiceContract( SessionMode = SessionMode.Required, CallbackContract = typeof(IServiceCallBack))]
    public interface IDuplexService
    {
        [OperationContract]
        string TryCallBack(string info);

        [OperationContract(IsOneWay=true)]

        void TryOneWayCallBack(string info);
    }

    public interface IServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void ReportTime(string time);

        [OperationContract(IsOneWay = true)]
        void ReportState(string info);
    }

    [DataContract]
    public class MyData
    {
        [DataMember]
        string exInfo;
        bool isMan;
        string name;
        int high;

        public MyData(string yourName)
        {
            name = yourName;
        }

        public MyData(bool yourIsMan)
        {
            isMan = yourIsMan;
        }


        [DataMember]
        public bool IsMan
        {
            get { return isMan; }
            set { isMan = value; }
        }

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public int High
        {
            get { return high; }
            set { high = value; }
        }

        public bool IsAngoodPerson(string yourIdol)
        {
            if(yourIdol == "lijie")
            {
                return true;
            }
            return false;
        }
    }


    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        string myTestStr = "ts";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }

        public string MyTest
        {
            get { return myTestStr; }
        }
    }
}
