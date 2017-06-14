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

        private delegate void GetMQStateMessage(string sender, string message);
        public event GetMQStateMessage OnGetMQStateMessage;

        public MyActiveMQ(string yourBrokerUri, string yourClientId, string yourFactoryUserName, string yourFactoryPassword, List<KeyValuePair<string, bool>> yourQueueList, List<KeyValuePair<string, bool>> yourTopicList)
        {
            brokerUri = yourBrokerUri;
            clientId = yourClientId;
            factoryUserName = yourFactoryUserName;
            factoryPassword = yourFactoryPassword;
            queuesList = yourQueueList;
            topicList = yourTopicList;
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

            //connection (a factory can creart 
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
            if(factory==null)
            {
                return Connect();
            }
            return false;
        }

        public void SubscribeConsumer(string consumerName, bool isQueues, bool isDurable)
        {
            IMessageConsumer consumer;
            //IMessageConsumer consumer = session.CreateDurableConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic("testing"), "testing listener", null, false);
            try
            {
                if (isDurable)
                {
                    consumer = session.CreateDurableConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(tb_ConsumerTopic.Text), (tb_ConsumerName.Text == "" ? null : tb_ConsumerName.Text), null, false);
                }
                else
                {
                    consumer = session.CreateConsumer(((!isQueues) ? ((IDestination)(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(consumerName))) : ((IDestination)(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(consumerName)))), null, false);
                }
            }
            catch (Exception ex)
            {
                ShowError("Subscribe fail");
                ShowError(ex.Message);
                return;
            }
            consumer.Listener += consumer_Listener;
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
            else
            {
                ShowError("not connection");
            }
        }
    }
}
