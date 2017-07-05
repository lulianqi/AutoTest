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
        private List<string> queuesList;
        private List<KeyValuePair<string, string>> topicList;

        
        private ConnectionFactory factory;
        //IConnectionFactory factory;
        private IConnection connection;
        private ISession session;
        private List<IMessageConsumer> consumerList;

        private string nowErrorMes;
        private bool isWithEvent;
        private bool isMQMessageWithSender;

        public delegate void GetMQStateMessage(string sender, string message);
        /// <summary>
        /// 获取ActiveMQ连接的状态信息
        /// </summary>
        public event GetMQStateMessage OnGetMQStateMessage;

        public delegate void GetMQMessage(string sender, string message);
        /// <summary>
        /// 接收ActiveMQ消息推送（当isWithEvent被设置为true时生效）
        /// </summary>
        public event GetMQMessage OnGetMQMessage;

        /// <summary>
        /// 初始化ActiveMQ（该处ActiveMQ仅支持默认openwire协议）
        /// </summary>
        /// <param name="yourBrokerUri">链接地址</param>
        /// <param name="yourClientId">ClientId（当使用null时会使用默认id）</param>
        /// <param name="yourFactoryUserName">如果不需要认证请传null</param>
        /// <param name="yourFactoryPassword">如果不需要认证请传null</param>
        /// <param name="yourQueueList">需要订阅的Queue列表(构造函数里的queue/topic为默认就会订阅的消费者，断开重新连接也会进行重新订阅)</param>
        /// <param name="yourTopicList">需要订阅的Topic列表 如果使用durable topic list的value填唯一id，为null则为普通topic</param>
        /// <param name="yourIsWithEvent">是否使用事件来接收消息</param>
        public MyActiveMQ(string yourBrokerUri, string yourClientId, string yourFactoryUserName, string yourFactoryPassword, List<string> yourQueueList, List<KeyValuePair<string, string>> yourTopicList ,bool yourIsWithEvent)
        {
            isWithEvent = yourIsWithEvent;
            isMQMessageWithSender = false;
            consumerList = new List<IMessageConsumer>();
            brokerUri = yourBrokerUri;
            clientId = yourClientId;
            factoryUserName = yourFactoryUserName;
            factoryPassword = yourFactoryPassword;
            if (yourQueueList != null)
            {
                queuesList = yourQueueList;
            }
            if (yourTopicList != null)
            {
                topicList = yourTopicList;
            }
        }

        /// <summary>
        /// 初始化ActiveMQ（该处ActiveMQ仅支持默认openwire协议）(使用该重载版本将不使用事件模型接收消息)
        /// </summary>
        /// <param name="yourBrokerUri">链接地址</param>
        /// <param name="yourClientId">ClientId（当使用null时会使用默认id）</param>
        /// <param name="yourFactoryUserName">如果不需要认证请传null</param>
        /// <param name="yourFactoryPassword">如果不需要认证请传null</param>
        /// <param name="yourQueueList">需要订阅的Queue列表</param>
        /// <param name="yourTopicList">需要订阅的Topic列表 如果使用durable topic list的value填唯一id，为null则为普通topic</param>
        public MyActiveMQ(string yourBrokerUri, string yourClientId, string yourFactoryUserName, string yourFactoryPassword, List<string> yourQueueList, List<KeyValuePair<string, string>> yourTopicList )
            :this( yourBrokerUri,  yourClientId,  yourFactoryUserName,  yourFactoryPassword,  yourQueueList,  yourTopicList ,false)
        {

        }

        /// <summary>
        /// 获取最新一次的错误信息
        /// </summary>
        public string NowErrorMes
        {
            get
            {
                return nowErrorMes;
            }
        }

        /// <summary>
        /// 获取或设置消息的显示模式
        /// </summary>
        public bool IsMQMessageWithSender
        {
            get
            {
                return isMQMessageWithSender;
            }
            set
            {
                isMQMessageWithSender = value;
            }
        }

        /// <summary>
        /// 获取Consumer列表(key 为 消费者完整名称 value 为ConsumerId)
        /// </summary>
        public List<KeyValuePair<string,string>> ConsumerList
        {
            get
            {
                List<KeyValuePair<string, string>> outList = new List<KeyValuePair<string, string>>();
                foreach (IMessageConsumer tempConsumer in consumerList)
                {
                    outList.Add(new KeyValuePair<string, string>( ((Apache.NMS.ActiveMQ.MessageConsumer)(tempConsumer)).ConsumerInfo.Destination.ToString(), ((Apache.NMS.ActiveMQ.MessageConsumer)(tempConsumer)).ConsumerInfo.ConsumerId.ToString()));
                }
                return outList;
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

        /// <summary>
        /// 连接MQ服务器
        /// </summary>
        /// <returns>是否连接成功（错误信息请使用NowErrorMes获取）</returns>
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

            return SubscribeInnerConsumer();
        }

        /// <summary>
        /// 重连MQ服务器
        /// </summary>
        /// <returns>是否连接成功（错误信息请使用NowErrorMes获取）</returns>
        public bool ReConnect()
        {
            if (connection != null)
            {
                DisConnect();
            }  
            if(Connect())
            {
                return SubscribeInnerConsumer();
            }
            return false;
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="consumerName">消费者名称</param>
        /// <param name="isQueues">是否为Queues</param>
        /// <param name="durableName">durable id（为null 使用普通模式，且仅对topic有效）</param>
        /// <returns>是否成功</returns>
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

        private bool SubscribeInnerConsumer()
        {
            foreach (string tempQueue in queuesList)
            {
                if (!SubscribeConsumer(tempQueue, true, null))
                {
                    return false;
                }
            }
            foreach (KeyValuePair<string,string> tempToipc in topicList)
            {
                if (!SubscribeConsumer(tempToipc.Key, false, tempToipc.Value))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="consumerNameFullName">consumerName （为形如queue://consumerName 格式的数据）</param>
        /// <returns>被删除的消费者的数量（可能会有重名的会被一起删除）</returns>
        public int UnSubscribeConsumer(string consumerNameFullName)
        {
            int unSubscribeNumber = 0;
            for (int i = consumerList.Count - 1; i >= 0;i-- )
            {
                IMessageConsumer tempConsumer = consumerList[i];
                if (((Apache.NMS.ActiveMQ.MessageConsumer)(tempConsumer)).ConsumerInfo.Destination.ToString() == consumerNameFullName)
                {
                    tempConsumer.Close();
                    tempConsumer.Dispose();
                    consumerList.RemoveAt(i);
                    unSubscribeNumber++;
                }
            }
            return unSubscribeNumber;
        }
        private void consumer_Listener(IMessage message)
        {
            if(OnGetMQMessage!=null)
            {
                OnGetMQMessage(message.NMSDestination.ToString(), GetIMessage(message));
            }
        }

        /// <summary>
        /// 获取当前所有消费者订阅的消息（当IsWithEvent为false时可用）
        /// </summary>
        /// <returns>消息列表</returns>
        public List<KeyValuePair<string,string>> ReadConsumerMessage()
        {
            return ReadConsumerMessage(null);
        }

        /// <summary>
        /// 获取当前所有消费者订阅的消息（当IsWithEvent为false时可用）
        /// </summary>
        /// <param name="yourConsumerName">指定Consumer 的名字（完整路径名）(当该值设置为null时即表示获取所有consumer消息)</param>
        /// <returns>获取当前所有消费者订阅的消息（当IsWithEvent为false时可用）（没有消失则count为0，返回不会为null）</returns>
        public List<KeyValuePair<string, string>> ReadConsumerMessage(string yourConsumerName)
        {
            List<KeyValuePair<string, string>> outMessageList = new List<KeyValuePair<string, string>>();
            if (isWithEvent)
            {
                throw (new Exception("all message will show in the OnGetMQMessage"));
            }
            foreach (IMessageConsumer nowConsuner in consumerList)
            {
                if (yourConsumerName != null)
                {
                    //if ((((Apache.NMS.ActiveMQ.MessageConsumer)(nowConsuner)).ConsumerInfo.Destination).PhysicalName != yourConsumerName)
                    if (((Apache.NMS.ActiveMQ.MessageConsumer)(nowConsuner)).ConsumerInfo.Destination.ToString() != yourConsumerName)
                    {
                        continue;
                    }
                }
                IMessage tempMessage;
                tempMessage = nowConsuner.ReceiveNoWait();
                while (tempMessage != null)
                {
                    outMessageList.Add(new KeyValuePair<string, string>(tempMessage.NMSDestination.ToString(), GetIMessage(tempMessage, isMQMessageWithSender)));
                    tempMessage = nowConsuner.ReceiveNoWait();
                }
            }
            return outMessageList;
        }

        /// <summary>
        /// 发布Mq消息
        /// </summary>
        /// <param name="senderName">名称</param>
        /// <param name="message">消息内容 (如果使用字节模式 只能接受形如FF FF FF 这样的字符串)</param>
        /// <param name="isTopic">是否为topic</param>
        /// <param name="messageType">消息类型</param>
        /// <param name="sendNum">发送次数</param>
        /// <returns>是否发送成功</returns>
        public bool PublishMessage(string senderName,string message,bool isTopic,MessageType messageType ,int sendNum)
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
            try
            { 
                for (int i = 0; i < sendNum;i++ )
                {
                    prod.Send(msg, Apache.NMS.MsgDeliveryMode.NonPersistent, Apache.NMS.MsgPriority.Normal, TimeSpan.MinValue);
                }
            }
            catch (Exception ex)
            {
                nowErrorMes = "it is not hex16 data :" + ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 发布Mq消息
        /// </summary>
        /// <param name="senderName">名称</param>
        /// <param name="message">消息内容 (如果使用字节模式 只能接受形如FF FF FF 这样的字符串)</param>
        /// <param name="isTopic">是否为topic</param>
        /// <param name="messageType">消息类型</param>
        /// <returns>是否发送成功</returns>
        public bool PublishMessage(string senderName,string message,bool isTopic,MessageType messageType)
        {
            return PublishMessage(senderName, message, isTopic, messageType, 1);
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConnect()
        {
            if (connection != null)
            {
                
                foreach (var tempConsumer in consumerList)
                {
                    tempConsumer.Close();
                    tempConsumer.Dispose();
                }
                consumerList.Clear();
                if (session != null)
                {
                    session.Close();
                    session.Dispose();
                    session = null;
                    connection.Stop();
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        public override string ToString()
        {
            return string.Format("{0}", brokerUri);
        }
    }
}
