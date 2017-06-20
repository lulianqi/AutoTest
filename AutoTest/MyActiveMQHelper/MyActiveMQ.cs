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

        
        private ConnectionFactory factory;
        //IConnectionFactory factory;
        private IConnection connection;
        private ISession session;
        private List<IMessageConsumer> consumerList;

        private string nowErrorMes;
        private bool isWithEvent;

        public delegate void GetMQStateMessage(string sender, string message);
        public event GetMQStateMessage OnGetMQStateMessage;

        public delegate void GetMQMessage(string sender, string message);
        public event GetMQMessage OnGetMQMessage;

        public MyActiveMQ(string yourBrokerUri, string yourClientId, string yourFactoryUserName, string yourFactoryPassword, List<KeyValuePair<string, bool>> yourQueueList, List<KeyValuePair<string, bool>> yourTopicList ,bool yourIsWithEvent)
        {
            isWithEvent = yourIsWithEvent;
            consumerList = new List<IMessageConsumer>();
            brokerUri = yourBrokerUri;
            clientId = yourClientId;
            factoryUserName = yourFactoryUserName;
            factoryPassword = yourFactoryPassword;
            queuesList = yourQueueList;
            topicList = yourTopicList;
        }

        public string NowErrorMes
        {
            get
            {
                return nowErrorMes;
            }
        }

        #region mq event
        void session_TransactionStartedListener(ISession session)
        {
            ShowStateMessage("session_TransactionStartedListener", session.ToString());
        }

        void session_TransactionRolledBackListener(ISession session)
        {
            ShowStateMessage("session_TransactionRolledBackListener", session.ToString());
        }

        void session_TransactionCommittedListener(ISession session)
        {
            ShowStateMessage("session_TransactionCommittedListener", session.ToString());
        }

        void connection_ConnectionResumedListener()
        {
            ShowStateMessage("connection_ConnectionResumedListener", "connection_ConnectionResumedListener");
        }

        void connection_ConnectionInterruptedListener()
        {
            ShowStateMessage("connection_ConnectionInterruptedListener", "connection_ConnectionInterruptedListener");
        }

        void connection_ExceptionListener(Exception exception)
        {
            ShowStateMessage("connection_ExceptionListener", exception.Message);
        }

        void factory_OnException(Exception exception)
        {
            ShowStateMessage("factory_OnException", exception.Message);
        } 
        #endregion

        #region function helper
        private string GetIMessage(IMessage message ,bool ishasNMSInfo)
        {
            string outMes="";
            if (ishasNMSInfo)
            {
                outMes = (string.Format("NMSDestination : {0} \r\nNMSCorrelationID: {1} \r\nNMSMessageId: {2} \r\nNMSTimestamp: {3} \r\nNMSTimeToLive: {4} \r\nNMSType:{5} \r\nNMSPriority: {6} \r\n", message.NMSDestination.ToString(), message.NMSCorrelationID, message.NMSMessageId, message.NMSTimestamp.ToLocalTime(), message.NMSTimeToLive.ToString(), message.NMSType, message.NMSPriority.ToString()));
            }
            if (message is ITextMessage)
            {
                outMes+=(((ITextMessage)message).Text);
            }
            else if (message is IBytesMessage)
            {
                outMes += BitConverter.ToString(((IBytesMessage)message).Content);
            }
            else if (message is IMapMessage)
            {
                outMes += ("can not show IMapMessage");
            }
            else if (message is IStreamMessage)
            {
                outMes += ("can not show IStreamMessage");
            }
            else
            {
                outMes += ("find nuknow IMessage");
            }
            return outMes;
        }
        
        private string GetIMessage(IMessage message)
        {
            return GetIMessage(message, false);
        }
        #endregion

        private void ShowStateMessage(string sender, string message)
        {
            if(OnGetMQStateMessage!=null)
            {
                OnGetMQStateMessage(sender, message);
            }
        }

        public bool Connect()
        {
            #region create tcp connection
            //factory
            try
            {
                factory = new ConnectionFactory(brokerUri);
                factory.OnException += factory_OnException;
            }
            catch (Exception ex)
            {
                nowErrorMes = ex.Message;
                return false;
            }
            if (factoryUserName != null && factoryPassword != null)
            {
                factory.UserName = factoryUserName;
                factory.Password = factoryPassword;
            }
            if (clientId != null)
            {
                factory.ClientId = clientId;
            }

            //connection (a factory can creart multiple connection)
            try
            {
                connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                nowErrorMes = ex.Message;
                return false;
            }
            if (clientId != null)
            {
                connection.ClientId = clientId;
            }
            connection.Start();
            connection.ExceptionListener += connection_ExceptionListener;
            connection.ConnectionInterruptedListener += connection_ConnectionInterruptedListener;
            connection.ConnectionResumedListener += connection_ConnectionResumedListener; 
            #endregion

            #region create session
            //AcknowledgementMode
            //Session.AUTO_ACKNOWLEDGE。当客户成功的从receive方法返回的时候，
            //或者从MessageListener.onMessage 方法成功返回的时候，会话自动确认
            //客户收到的消息。
            // Session.CLIENT_ACKNOWLEDGE。客户通过消息的acknowledge 方法确认消
            //息。需要注意的是，在这种模式中，确认是在会话层上进行：确认一个被
            //消费的消息将自动确认所有已被会话消费的消息。例如，如果一个消息消
            //费者消费了10 个消息，然后确认第5 个消息，那么所有10 个消息都被确
            //认。
            // Session.DUPS_ACKNOWLEDGE。该选择只是会话迟钝的确认消息的提交。如
            //果JMS provider 失败，那么可能会导致一些重复的消息。如果是重复的
            //消息，那么JMS provider 必须把消息头的JMSRedelivered 字段设置为
            //true。
            session = connection.CreateSession();
            session.TransactionCommittedListener += session_TransactionCommittedListener;
            session.TransactionRolledBackListener += session_TransactionRolledBackListener;
            session.TransactionStartedListener += session_TransactionStartedListener;
            #endregion

            return true;
        }

        public bool ReConnect()
        {
            if (connection != null)
            {
                DisConnect();
            }  
            return Connect();
        }

        public bool SubscribeConsumer(string consumerName, bool isQueues, string durableName)
        {
            IMessageConsumer consumer;
            try
            {
                if (!isQueues && durableName!=null)
                {
                    consumer = session.CreateDurableConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(consumerName), durableName, null, false);
                }
                else
                {
                    consumer = session.CreateConsumer(((isQueues) ? ((IDestination)(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(consumerName))):((IDestination)(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(consumerName))) ), null, false);
                }
            }
            catch (Exception ex)
            {
                nowErrorMes = ex.Message;
                return false;
            }
            consumerList.Add(consumer);
            if (isWithEvent)
            {
                consumer.Listener += consumer_Listener;
            }
            return true;
        }

        void consumer_Listener(IMessage message)
        {
            if(OnGetMQMessage!=null)
            {
                OnGetMQMessage(message.NMSDestination.ToString(), GetIMessage(message));
            }
        }

        public List<string> ReadAllConsumerMessage()
        {
            List<string> outMessageList = new List<string>();
            if(isWithEvent)
            {
                throw (new Exception("all message will show in the OnGetMQMessage"));
            }
            foreach(IMessageConsumer nowConsuner in consumerList)
            {
                IMessage tempMessage;
                tempMessage = nowConsuner.ReceiveNoWait();
                while(tempMessage!=null)
                {
                    outMessageList.Add(GetIMessage(tempMessage));
                    tempMessage = nowConsuner.ReceiveNoWait();
                }
            }
            return outMessageList;
        }

        public bool PublishMessage(string senderName,string message,bool isTopic,MessageType messageType)
        {
            if (session == null || senderName == null || message==null)
            {
                nowErrorMes = "find null data in PublishMessage";
                return false;
            }
            IMessageProducer prod;
            try
            {
                prod = isTopic ? session.CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(senderName)) : session.CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(senderName));
            }
            catch (Exception ex)
            {
                nowErrorMes = ex.Message;
                return false;
            }
            IMessage msg;
            switch(messageType)
            {
                case MessageType.BytesMessage:
                    msg = prod.CreateBytesMessage();
                    string[] hexStrs;
                    hexStrs = message.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
                    byte[] resultBytes = new byte[hexStrs.Length];
                    try
                    {
                        for (int i = 0; i < hexStrs.Length; i++)
                        {
                            resultBytes[i] = Convert.ToByte(hexStrs[i], 16);
                        }
                    }
                    catch(Exception ex)
                    {
                        nowErrorMes ="iit is not hex16 data :" +ex.Message;
                        return false;
                    }
                    ((IBytesMessage)msg).Content = resultBytes;
                    break;
                case MessageType.TextMessage:
                    msg = prod.CreateTextMessage();
                    ((ITextMessage)msg).Text = message;
                    break;
                default:
                    nowErrorMes = "not support this IMessage";
                    return false;
            }
            prod.Send(msg, Apache.NMS.MsgDeliveryMode.NonPersistent, Apache.NMS.MsgPriority.Normal, TimeSpan.MinValue);
            return true;
        }

        public void DisConnect()
        {
            if (connection != null)
            {
                
                foreach (var tempConsumer in consumerList)
                {
                    tempConsumer.Close();
                    tempConsumer.Dispose();
                }

                session.Close();
                session.Dispose();
                session = null;
                connection.Stop();
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
