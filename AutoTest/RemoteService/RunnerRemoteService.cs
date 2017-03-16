using CaseExecutiveActuator;
using RemoteService.MyService;
using RemoteService.MyTool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Windows.Forms;


/*******************************************************************************
* Copyright (c) 2015 lijie
* All rights reserved.
* 
* 文件名称: 
* 内容摘要: mycllq@hotmail.com
* 
* 历史记录:
* 日	  期:   201505016           创建人: 李杰 15158155511
* 描    述: 创建
*******************************************************************************/


namespace RemoteService
{
    public partial class RunnerRemoteService : Form
    {
        public RunnerRemoteService()
        {
            Control.CheckForIllegalCrossThreadCalls = false;                                    //自行控制ui线程安全
            InitializeComponent();
        }

        private List<CaseRunner> caseRunnerList = new List<CaseRunner>();
        System.Windows.Forms.Timer RS_CaseRunner_Timer = new System.Windows.Forms.Timer();
        CaseRunner selctRunner = null;

        ServerHost serverHost = null;

        private void RemoteService_Load(object sender, EventArgs e)
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 2000;
            RS_CaseRunner_Timer.Interval = 1000;
            RS_CaseRunner_Timer.Tick += AT_CaseRunner_Timer_Tick;
            RS_CaseRunner_Timer.Enabled = true;

            ShowIp();

            serverHost = new ServerHost(new Uri("http://localhost:8087/SelService"));
            serverHost.OnBackNowRunnerList += serverHost_OnBackNowRunnerList;
            serverHost.OnServerHostMessage += serverHost_OnServerHostMessage;
        }

        private void ShowIp()
        {
            IPAddress[] ips = MyCommonHelper.NetHelper.MyNetConfig.getNetworkInterfaceAddress();
            if(ips!=null)
            {
                foreach(IPAddress ip in ips)
                {
                    AddInfo(ip.ToString());
                    comboBox_ips.Items.Add(ip.ToString());
                }
            }
            comboBox_ips.Text = "请选择服务地址";
        }

        private void AddInfo(string info)
        {
            richTextBox_info.AppendText(info + "\r\n");
        }

        private void AT_CaseRunner_Timer_Tick(object sender, EventArgs e)
        {
            UpdateRunnerView();
            SendStateInfo();
        }

        private void bt_AddRunner_Click(object sender, EventArgs e)
        {
            MyWindow.AddRunner newSet = new MyWindow.AddRunner();
            newSet.StartPosition = FormStartPosition.CenterParent;
            newSet.ShowDialog(this);
        }

        private void comboBox_ips_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_baseAddress.Text = "http://" + comboBox_ips.Text + ":" + tb_port .Text+ "/SelService";
        }

        private void tb_port_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void tb_port_TextChanged(object sender, EventArgs e)
        {
            tb_baseAddress.Text = "http://" + comboBox_ips.Text + ":" + tb_port.Text + "/SelService";
        }


        #region Runner

        /// <summary>
        /// 判断用户列表下是否有同名用户
        /// </summary>
        /// <param name="yourName">用户名</param>
        /// <returns>是否有同名用户</returns>
        public bool IsContainRunnerName(string yourName)
        {
            foreach (CaseRunner tempRunner in caseRunnerList)
            {
                if (yourName == tempRunner.RunnerName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 添加一个新用户
        /// </summary>
        /// <param name="yourCaseRunner">新用户</param>
        public void AddRunner(CaseRunner yourCaseRunner)
        {
            listView_CaseRunner.AddRunner(yourCaseRunner);
            caseRunnerList.Add(yourCaseRunner);
        }

        /// <summary>
        /// 删除一个用户
        /// </summary>
        /// <param name="yourCaseRunner">被删除用户</param>
        public void DelRunner(CaseRunner yourCaseRunner)
        {
            listView_CaseRunner.DelRunner(yourCaseRunner);
            caseRunnerList.Remove(yourCaseRunner);
        }

        /// <summary>
        /// 更新CaseRunner界面
        /// </summary>
        private void UpdateRunnerView()
        {
            if (caseRunnerList.Count > 0)
            {
              
                foreach (CaseRunner tempRunner in caseRunnerList)
                {
                    if (tempRunner.RunnerState == CaseActuatorState.Running)
                    {
                        tempRunner.UpdateProgressBar();
                    }
                }
            }
        }

        #endregion


        #region Service

        Uri baseAddress2 = new Uri("http://localhost:8087/SelService");

        /// <summary>
        /// 来自serverHost的提示信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        void serverHost_OnServerHostMessage(string sender, string message)
        {
            this.Invoke(new Action<string>(AddInfo), message);
        }


        /// <summary>
        /// serverHost请求当前执行器列表
        /// </summary>
        /// <returns></returns>
        List<CaseRunner> serverHost_OnBackNowRunnerList()
        {
            return caseRunnerList;
        }
     
        private void button_duaStart_Click(object sender, EventArgs e)
        {
            try
            {
                serverHost.BaseAddress = new Uri(tb_baseAddress.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            serverHost.OpenBaseHost();
        }

        private void button_duaStop_Click(object sender, EventArgs e)
        {
            serverHost.CloseBaseHost();
        }

        private void SendStateInfo()
        {
            if (serverHost.BaseHostState == CommunicationState.Opened)
            {
                serverHost.SendStateCallBack(caseRunnerList);
            }
        }

        #endregion




    }
}
