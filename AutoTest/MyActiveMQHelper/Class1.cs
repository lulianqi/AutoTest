using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyActiveMQHelper
{
    public class MyActiveMQ
    {
        private string brokerUri;
        private string clientId;
        private string factoryUserName;
        private string factoryPassword;
        private List<KeyValuePair<string, bool>> queuesList;
        private List<KeyValuePair<string, bool>> topicList;

        private List<IMessageConsumer> consumerList;
        private ConnectionFactory factory;
        //IConnectionFactory factory;
        private IConnection connection;
        private ISession session;

        public MyActiveMQ(string yourBrokerUri, string yourClientId, string yourFactoryUserName, string yourFactoryPassword, List<KeyValuePair<string, bool>> yourQueueList, List<KeyValuePair<string, bool>> yourTopicList)
        {
            brokerUri = yourBrokerUri;
            clientId = yourClientId;
            factoryUserName = yourFactoryUserName;
            factoryPassword = yourFactoryPassword;
            queuesList = yourQueueList;
            topicList = yourTopicList;
        }

        public bool Connecte()
        {
            try
            {
factory = new ConnectionFactory(brokerUri);
            }
            
        }

        public void DisConnect()
        {

        }
    }
}
