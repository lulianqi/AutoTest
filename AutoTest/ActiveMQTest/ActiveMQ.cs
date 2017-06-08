using Apache.NMS;
using Apache.NMS.ActiveMQ;
using MyCommonHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ActiveMQTest
{
    public partial class ActiveMQ : Form
    {
        public ActiveMQ()
        {
            InitializeComponent();
        }

        List<IMessageConsumer> consumerList;
        ConnectionFactory factory;
        //IConnectionFactory factory;
        IConnection connection;
        ISession session;

        private void ActiveMQ_Load(object sender, EventArgs e)
        {
            cb_TopicQueues.SelectedIndex = cb_sendTopicQueues.SelectedIndex = cb_sendTextByte.SelectedIndex = 0;
            consumerList = new List<IMessageConsumer>();
            this.MinimumSize = new Size(this.Width, this.Height);
        }

        private void ShowMessage(string mes)
        {
            Drb_messageReceive.AddDate(mes, Color.Black, true);
        }

        private void ShowState(string mes)
        {
            Drb_messageReceive.AddDate(mes, Color.SlateBlue, true);
        }

        private void ShowLowLevelInfo(string mes)
        {
            Drb_messageReceive.AddDate(mes, Color.LightGray, true);
        }

        private void ShowError(string mes)
        {
            Drb_messageReceive.AddDate(mes, Color.Red, true);
        }

        private void ConnectionFactoryConnect()
        {
            tb_uri.Enabled = tb_clientId.Enabled = tb_userName.Enabled = tb_Password.Enabled = false;
            bt_Connect.Enabled = false;
            ShowMessage("connecting............");
            try
            {
                connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                tb_uri.Enabled = tb_clientId.Enabled = tb_userName.Enabled = tb_Password.Enabled = true;
                bt_Connect.Enabled = true;
                return;
            }
            if(factory.ClientId!=null)
            {
                try
                {
                    connection.ClientId = factory.ClientId;  
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                    ShowError("当前ClientId可能正在被其他用户使用");
                    connection.Close();
                    connection.Dispose();
                    tb_uri.Enabled = tb_clientId.Enabled = tb_userName.Enabled = tb_Password.Enabled = true;
                    bt_Connect.Enabled = true;
                    return;
                }
            }
            connection.Start();
            ShowState(factory.BrokerUri.AbsoluteUri + " connected");     
            connection.ExceptionListener += connection_ExceptionListener;
            connection.ConnectionInterruptedListener += connection_ConnectionInterruptedListener;
            connection.ConnectionResumedListener += connection_ConnectionResumedListener;

            session = connection.CreateSession();
            session.TransactionCommittedListener += session_TransactionCommittedListener;
            session.TransactionRolledBackListener += session_TransactionRolledBackListener;
            session.TransactionStartedListener += session_TransactionStartedListener;
        }

        void session_TransactionStartedListener(ISession session)
        {
            ShowState("session_TransactionStartedListener");
        }

        void session_TransactionRolledBackListener(ISession session)
        {
            ShowState("session_TransactionRolledBackListener");
        }

        void session_TransactionCommittedListener(ISession session)
        {
            ShowState("session_TransactionCommittedListener");
        }

        private void ConnectionFactoryDisconnect()
        {
            if (connection != null)
            {
                ShowState(factory.BrokerUri.AbsoluteUri + " disconnect");
                foreach(var tempConsumer in consumerList)
                {
                    tempConsumer.Close();
                    tempConsumer.Dispose();
                }
                consumerList.Clear();
                RefreshPathList(consumerList);

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
            tb_uri.Enabled = tb_clientId.Enabled = tb_userName.Enabled = tb_Password.Enabled = true;
            bt_Connect.Enabled = true;
        }

        private void SubscribeSicion()
        {
            if (connection==null)
            {
                ShowError("connection is null");
                return;
            }
            if(connection.IsStarted)
            {
                ISession session = connection.CreateSession();
            }
            else
            {
                ShowError("connection is not start");
            }
            
        }

        void connection_ConnectionResumedListener()
        {
            Drb_messageReceive.AddDate("ConnectionResumedListener", Color.Red, true);
        }

        void connection_ConnectionInterruptedListener()
        {
            Drb_messageReceive.AddDate("ConnectionInterruptedListener", Color.Red, true);
        }

        void connection_ExceptionListener(Exception exception)
        {
            Drb_messageReceive.AddDate(exception.Message, Color.Red, true);
            Drb_messageReceive.AddDate(exception.StackTrace, Color.Red, true);
        }

        void factory_OnException(Exception exception)
        {
            Drb_messageReceive.AddDate(exception.Message, Color.Red, true);
            Drb_messageReceive.AddDate(exception.StackTrace, Color.Red, true);
        }

        private void bt_Connect_Click(object sender, EventArgs e)
        {
            if(tb_uri.Text!="")
            {
                try
                {
                    factory = new ConnectionFactory(tb_uri.Text);
                    factory.OnException += factory_OnException;
                }
                catch(Exception ex)
                {
                    ShowError(ex.Message);
                    return;
                }
                if(tb_clientId.Text!="")
                {
                    factory.ClientId = tb_clientId.Text;
                }
                if(tb_userName.Text!="")
                {
                    factory.UserName = tb_userName.Text;
                    factory.Password = tb_Password.Text;
                }
                else
                {
                    tb_Password.Text = "";
                }
                ConnectionFactoryConnect();
            }
            else
            {
                MessageBox.Show("please put in adreess ", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void bt_Discounect_Click(object sender, EventArgs e)
        {
            ConnectionFactoryDisconnect();
        }

        private void bt_Subscribe_Click(object sender, EventArgs e)
        {
            if (tb_ConsumerTopic.Text=="")
            {
                MessageBox.Show("please put in topic or queues ", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if(session!=null)
            {
                IMessageConsumer consumer;
                //IMessageConsumer consumer = session.CreateDurableConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic("testing"), "testing listener", null, false);
                try
                {
                    if (ck_isDurable.Checked)
                    {
                        cb_TopicQueues.SelectedIndex = 0;
                        consumer = session.CreateDurableConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(tb_ConsumerTopic.Text), (tb_ConsumerName.Text == "" ? null : tb_ConsumerName.Text), null, false);
                    }
                    else
                    {
                        consumer = session.CreateConsumer(((cb_TopicQueues.SelectedIndex == 0) ? ((IDestination)(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(tb_ConsumerTopic.Text))) : ((IDestination)(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(tb_ConsumerTopic.Text)))), null, false);
                    }
                }
                catch(Exception ex)
                {
                    ShowError("Subscribe fail");
                    ShowError(ex.Message);
                    return;
                }
                consumer.Listener += consumer_Listener;
                ShowState(tb_ConsumerTopic.Text + "  Subscribed");
                consumerList.Add(consumer);
                RefreshPathList(consumerList);
            }
            else
            {
                ShowError("session is null");
            }
        }

        private void ck_isDurable_CheckedChanged(object sender, EventArgs e)
        {
            if(ck_isDurable.Checked)
            {
                tb_ConsumerName.Visible = true;
                tb_ConsumerName.Text = tb_clientId.Text;
                tb_ConsumerTopic.Location = new Point(365, 69);
                tb_ConsumerTopic.Width = 361;
            }
            else
            {
                tb_ConsumerName.Visible = false;
                tb_ConsumerTopic.Location = new Point(158, 69);
                tb_ConsumerTopic.Width = 568;
            }
        }

        void consumer_Listener(IMessage message)
        {
            if(this.InvokeRequired)
            {
                this.BeginInvoke(new Action<IMessage>(ShowIMessage), message);
            }
            else
            {
                ShowIMessage(message);
            }
        }

        private void ShowIMessage(IMessage message)
        {

            ShowLowLevelInfo(string.Format("NMSDestination : {0} \r\nNMSCorrelationID: {1} \r\nNMSMessageId: {2} \r\nNMSTimestamp: {3} \r\nNMSTimeToLive: {4} \r\nNMSType:{5} \r\nNMSPriority: {6}", message.NMSDestination.ToString(), message.NMSCorrelationID, message.NMSMessageId, message.NMSTimestamp.ToLocalTime(), message.NMSTimeToLive.ToString(), message.NMSType, message.NMSPriority.ToString()));
            if (message is ITextMessage)
            {
                ShowMessage(((ITextMessage)message).Text);
            }
            else if (message is IBytesMessage)
            {
                ShowMessage("[HEX16:]" + MyEncryption.ByteToHexString(((IBytesMessage)message).Content, MyEncryption.HexaDecimal.hex16, MyEncryption.ShowHexMode.space));
                try
                {
                    ShowMessage(Encoding.UTF8.GetString(((IBytesMessage)message).Content));
                }
                catch
                {
                    ShowError("chnage to text by utf8 fail");
                }
            }
            else if (message is IMapMessage)
            {
                ShowError("can not show IMapMessage");
            }
            else if (message is IStreamMessage)
            {
                ShowError("can not show IStreamMessage");
            }
            else
            {
                ShowError("find nuknow IMessage");
            }
        }

        private void RefreshPathList(List<IMessageConsumer> souceData)
        {
            if (souceData != null)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<List<IMessageConsumer>>(RefreshPathList), souceData);
                }
                else
                {
                    MyCommonTool.SetControlFreeze(lv_pathList);
                    lv_pathList.ClearEx();
                    foreach (IMessageConsumer tempConsumer in souceData)
                    {
                        ListViewItem tempLvi = new ListViewItem(new string[] { ((Apache.NMS.ActiveMQ.MessageConsumer)(tempConsumer)).ConsumerInfo.Destination.ToString(), "" });
                        tempLvi.Tag = new Button();
                        ((Button)tempLvi.Tag).Text = "Delete";
                        tempLvi.SubItems[0].Tag = tempConsumer;
                        lv_pathList.AddItemEx(tempLvi);
                    }
                    MyCommonTool.SetControlUnfreeze(lv_pathList);
                }
            }
        }

        private void lv_pathList_ButtonClickEvent(object sender, EventArgs e)
        {
            if (sender is ListViewItem)
            {
                IMessageConsumer tempConsumer = (IMessageConsumer)((ListViewItem)sender).SubItems[0].Tag;
                tempConsumer.Close();
                tempConsumer.Dispose();
                consumerList.Remove(tempConsumer);
                ((ListViewItem)sender).SubItems[0].Tag = null;
                lv_pathList.DelItemEx((ListViewItem)sender);
            }
            else
            {
                ShowError("delete error");
            }
        }


        private void lv_pathList_DoubleClick(object sender, EventArgs e)
        {
            IMessageConsumer tempConsumer = (IMessageConsumer)((ListView)sender).SelectedItems[0].SubItems[0].Tag;
            string tempInfoStr = string.Format("Destination : \r\n  {0} \r\nClientId : \r\n  {1} \r\nConsumerId : \r\n  {2}", ((Apache.NMS.ActiveMQ.MessageConsumer)(tempConsumer)).ConsumerInfo.Destination.ToString(), ((Apache.NMS.ActiveMQ.MessageConsumer)(tempConsumer)).ConsumerInfo.ClientId, ((Apache.NMS.ActiveMQ.MessageConsumer)(tempConsumer)).ConsumerInfo.ConsumerId.ToString());
            MessageBox.Show(tempInfoStr, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bt_publish_Click(object sender, EventArgs e)
        {
            if(session==null)
            {
                ShowError("not conection");
                return;
            }
            if (tb_sendTopic.Text == "")
            {
                MessageBox.Show("please put in topic or queues ", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            IMessageProducer prod;
            try
            {
                prod = cb_sendTopicQueues.SelectedIndex == 0 ? session.CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(tb_sendTopic.Text)) : session.CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(tb_sendTopic.Text));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            finally
            {

            }
            IMessage msg;
            if(cb_sendTextByte.SelectedIndex==0)
            {
                msg = prod.CreateTextMessage();
                ((ITextMessage)msg).Text = rtb_dataToSend.Text;  
            }
            else 
            {
                msg = prod.CreateBytesMessage();
                try
                {
                    ((IBytesMessage)msg).Content = MyEncryption.HexStringToByte(rtb_dataToSend.Text, MyEncryption.HexaDecimal.hex16, MyEncryption.ShowHexMode.space);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            int sendNum =1;
            try
            {
                sendNum = int.Parse(tb_sendCount.Text);
            }
            catch
            {
                tb_sendCount.Text = "1";
            }
            for (int i = 0; i < sendNum;i++ )
            {
                prod.Send(msg, Apache.NMS.MsgDeliveryMode.NonPersistent, Apache.NMS.MsgPriority.Normal, TimeSpan.MinValue);
            }
            prod.Dispose();
            ShowState("published");
            tb_sendTopic.AutoCompleteCustomSource.Add(tb_sendTopic.Text);
        }

        private void ActiveMQ_Resize(object sender, EventArgs e)
        {
            Drb_messageReceive.Width = this.Width - 241;
            rtb_dataToSend.Width = this.Width - 241;
            rtb_dataToSend.Height = this.Height - 462;
            lv_pathList.Height = this.Height - 142;
        }



      
    }
}
