using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestForUi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void advTree1_Click(object sender, EventArgs e)
        {

        }

        public delegate int TestDelCallBack();

        public event TestDelCallBack OnTest;


         /// <summary>
        /// 添加防火墙例外端口
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="port">端口</param>
        /// <param name="protocol">协议(TCP、UDP)</param>
        public static void NetFwAddPorts(string name, int port, string protocol)
        {
            //创建firewall管理类的实例
            INetFwMgr netFwMgr = (INetFwMgr)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwMgr"));

            INetFwOpenPort objPort = (INetFwOpenPort)Activator.CreateInstance(
                Type.GetTypeFromProgID("HNetCfg.FwOpenPort"));

            objPort.Name = name;
            objPort.Port = port;
            if (protocol.ToUpper() == "TCP")
            {
                objPort.Protocol = NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
            }
            else
            {
                objPort.Protocol = NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP;
            }
            objPort.Scope = NET_FW_SCOPE_.NET_FW_SCOPE_ALL;
            objPort.Enabled = true;

            bool exist = false;
            //加入到防火墙的管理策略
            foreach (INetFwOpenPort mPort in netFwMgr.LocalPolicy.CurrentProfile.GloballyOpenPorts)
            {

                if (objPort == mPort)
                {
                    exist = true;
                    break;
                }
            }
            if (exist)
            {
                System.Windows.Forms.MessageBox.Show("exist"); 
            }
            if (!exist)
            {
                netFwMgr.LocalPolicy.CurrentProfile.GloballyOpenPorts.Add(objPort);
                //netFwMgr.LocalPolicy.CurrentProfile.GloballyOpenPorts.Add()
            }
        }
        /// <summary>
        /// 将应用程序添加到防火墙例外
        /// </summary>
        /// <param name="name">应用程序名称</param>
        /// <param name="executablePath">应用程序可执行文件全路径</param>
        public static void NetFwAddApps(string name, string executablePath)
        {
            //创建firewall管理类的实例
            INetFwMgr netFwMgr = (INetFwMgr)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwMgr"));

            INetFwAuthorizedApplication app = (INetFwAuthorizedApplication)Activator.CreateInstance(
                Type.GetTypeFromProgID("HNetCfg.FwAuthorizedApplication"));

            //在例外列表里，程序显示的名称
            app.Name = name;

            //程序的路径及文件名
            app.ProcessImageFileName = executablePath;

            //是否启用该规则
            app.Enabled = true;

            //加入到防火墙的管理策略
            netFwMgr.LocalPolicy.CurrentProfile.AuthorizedApplications.Add(app);

            bool exist = false;
            //加入到防火墙的管理策略
            foreach (INetFwAuthorizedApplication mApp in netFwMgr.LocalPolicy.CurrentProfile.AuthorizedApplications)
            {
                if (app == mApp)
                {
                    exist = true;
                    break;
                }
            }
            if (!exist) netFwMgr.LocalPolicy.CurrentProfile.AuthorizedApplications.Add(app);
        }
        /// <summary>
        /// 删除防火墙例外端口
        /// </summary>
        /// <param name="port">端口</param>
        /// <param name="protocol">协议（TCP、UDP）</param>
        public static void NetFwDelApps(int port, string protocol)
        {
            INetFwMgr netFwMgr = (INetFwMgr)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwMgr"));
            if (protocol == "TCP")
            {
                netFwMgr.LocalPolicy.CurrentProfile.GloballyOpenPorts.Remove(port, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP);
            }
            else
            {
                netFwMgr.LocalPolicy.CurrentProfile.GloballyOpenPorts.Remove(port, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP);
            }
        }
        /// <summary>
        /// 删除防火墙例外中应用程序
        /// </summary>
        /// <param name="executablePath">程序的绝对路径</param>
        public static void NetFwDelApps(string executablePath)
        {
            INetFwMgr netFwMgr = (INetFwMgr)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwMgr"));

            netFwMgr.LocalPolicy.CurrentProfile.AuthorizedApplications.Remove(executablePath);

        }


        private void Form1_Load(object sender, EventArgs e)
        {

            List<string> xxx = "".Split(',').ToList();
            //advTree1.Nodes[1].Nodes[1].Cells.Add(new DevComponents.AdvTree.Cell("cnm"));
            advTree1.Nodes[0].Nodes[0].Cells[0].Text = "sss";
            advTree1.Nodes[0].Nodes[0].Cells[1].Text = "sss";
            advTree1.Nodes[0].Nodes[0].Cells[2].Text = "sss";

            this.OnTest += Form1_OnTest;
            this.OnTest += Form2_OnTest;
        }

        int Form2_OnTest()
        {
            return 1;
        }

        int Form1_OnTest()
        {
            return 2;
        }

        private void advTree1_AfterCheck(object sender, DevComponents.AdvTree.AdvTreeCellEventArgs e)
        {
            MessageBox.Show("advTree1_AfterCheck");
            if(e.Cell.Parent.HasChildNodes)
            {
                foreach(DevComponents.AdvTree.Node x in e.Cell.Parent.Nodes)
                {
                    x.Checked = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Go();
            int x = this.OnTest();

            DevComponents.AdvTree.Node tempNode = new DevComponents.AdvTree.Node();
            tempNode.Cells.Add(new DevComponents.AdvTree.Cell("1"));
            tempNode.Cells.Add(new DevComponents.AdvTree.Cell("2"));
            tempNode.Cells.Add(new DevComponents.AdvTree.Cell("3"));
            tempNode.Cells[0].Text = "0";
            tempNode.Image = imageList1.Images[10];

            advTree1.Nodes[1].Nodes.Add(tempNode);
            pictureBox1.Enabled = false;
        }

        void Go()
        {
            bool x = true;
            li:
            while(x)
            {
                x = false;
                goto li;
                MessageBox.Show("dd");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NetFwAddPorts("lijie", 11111, "TCP");
            NetFwAddApps("lijie", System.Windows.Forms.Application.StartupPath + "\\TestForUi.exe");
        }
    }
}
