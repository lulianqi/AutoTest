using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Windows.Forms;

namespace SelWCFServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Uri baseAddress = new Uri("http://localhost:8080/SelService");
        Uri baseAddress2 = new Uri("http://localhost:8080/DuaService");

        private void AddInfo(string info)
        {
            //System.Collections.Specialized.NameValueCollection
            richTextBox_info.AppendText(info + "\r\n");
        }

        ServiceHost host1 = null;
        ServiceHost host2 = null;


        private void OpenSrv()
        {
            //SelService selService=new SelService();
            //selService.ShowMesEvent += (sender, mes) => AddInfo(mes);
            //host1 = new ServiceHost(selService, baseAddress);

            host1 = new ServiceHost(typeof(SelService), baseAddress);

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;

            host1.Description.Behaviors.Add(smb);
            host1.AddServiceEndpoint(typeof(ISelService), new BasicHttpBinding(), "");
            //host1.AddServiceEndpoint(typeof(ISelService), new WSDualHttpBinding(), "");


            host1.Opening += new EventHandler((yourObject, yourEventAgrs) => AddInfo("Opening"));
            host1.Opened += new EventHandler((yourObject, yourEventAgrs) => AddInfo("Opened"));
            host1.Closed += new EventHandler((yourObject, yourEventAgrs) => AddInfo("Closed"));
            host1.Closing += new EventHandler((yourObject, yourEventAgrs) => AddInfo("Closing"));
            host1.Faulted += new EventHandler((yourObject, yourEventAgrs) => AddInfo("Faulted"));

            AddInfo(baseAddress.ToString() + "服务开启");
            host1.Open();
        }

        private void OpenSrvDua()
        {
            //SelService selService=new SelService();
            //selService.ShowMesEvent += (sender, mes) => AddInfo(mes);
            //host1 = new ServiceHost(selService, baseAddress);

            host2 = new ServiceHost(typeof(DuplexService), baseAddress2);

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;

            host2.Description.Behaviors.Add(smb);
            //host2.AddServiceEndpoint(typeof(ISelService), new BasicHttpBinding(), "");
            host2.AddServiceEndpoint(typeof(IDuplexService), new WSDualHttpBinding(), "");


            host2.Opening += new EventHandler((yourObject, yourEventAgrs) => AddInfo("Opening"));
            host2.Opened += new EventHandler((yourObject, yourEventAgrs) => AddInfo("Opened"));
            host2.Closed += new EventHandler((yourObject, yourEventAgrs) => AddInfo("Closed"));
            host2.Closing += new EventHandler((yourObject, yourEventAgrs) => AddInfo("Closing"));
            host2.Faulted += new EventHandler((yourObject, yourEventAgrs) => AddInfo("Faulted"));

            AddInfo(baseAddress2.ToString() + "服务开启");
            host2.Open();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            MessageTransferChannel.MessageCallback += (se, mes) => AddInfo(mes);
        }

        private void bt_startService_Click(object sender, EventArgs e)
        {
            if(host1==null)
            {
                OpenSrv();
            }
            else
            {
                if(host1.State == CommunicationState.Opened)
                {
                    AddInfo(baseAddress.ToString() + "服务已经开启");
                }
                else if(host1.State == CommunicationState.Opening)
                {
                    AddInfo(baseAddress.ToString() + "服务正在开启");
                }
                else
                {
                    OpenSrv();
                }
            }
        }


        private void bt_stopService_Click(object sender, EventArgs e)
        {
            if (host1 == null)
            {
                AddInfo("未发现服务");
            }
            else
            {
                if (host1.State != CommunicationState.Closed)
                {
                    AddInfo(baseAddress.ToString() + "服务关闭");
                    host1.Close();
                }
                else
                {
                    AddInfo(baseAddress.ToString() + "服务已经关闭");
                }
            }
        }

        private void button_duaStart_Click(object sender, EventArgs e)
        {
            if (host2 == null)
            {
                OpenSrvDua();
            }
            else
            {
                if (host2.State == CommunicationState.Opened)
                {
                    AddInfo(baseAddress2.ToString() + "服务已经开启");
                }
                else if (host2.State == CommunicationState.Opening)
                {
                    AddInfo(baseAddress2.ToString() + "服务正在开启");
                }
                else
                {
                    OpenSrvDua();
                }
            }
        }

        private void button_duaStop_Click(object sender, EventArgs e)
        {
            if (host2 == null)
            {
                AddInfo("未发现服务");
            }
            else
            {
                if (host2.State != CommunicationState.Closed)
                {
                    AddInfo(baseAddress2.ToString() + "服务关闭");
                    host2.Close();
                }
                else
                {
                    AddInfo(baseAddress2.ToString() + "服务已经关闭");
                }
            }
        }

    }
}
