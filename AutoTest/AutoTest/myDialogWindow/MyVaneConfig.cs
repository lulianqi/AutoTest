using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AutoTest.MyControl;
using System.Threading;
using System.Net;
using AutoTest.MyTool;

using SYDControls;
using MyCommonHelper;
using MyCommonHelper.NetHelper;
using MyCommonHelper.FileHelper;

/*******************************************************************************
* Copyright (c) 2013,浙江风向标科技有限公司
* All rights reserved.
* 
* 文件名称: 
* 内容摘要: mycllq@hotmail.com
* 
* 历史记录:
* 日	  期:   201401122          创建人: 测试部 李杰 15158155511
* 描    述: 创建
*******************************************************************************/

namespace AutoTest.myDialogWindow
{
    //public partial class MyVaneConfig : Form
    public partial class MyVaneConfig : SYDFormEx
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="yourIpEp">TCP 连接用网络节点</param>
        public MyVaneConfig(IPEndPoint yourIpEp ,string yourID)
        {
            InitializeComponent();
            myIpEp = yourIpEp;
            myGwID = yourID;
        }

        IPEndPoint myIpEp;
        string myGwID;
        MyTcpClient nowSocket;
        System.Windows.Forms.Timer myBeatTimer = new System.Windows.Forms.Timer();
        myVaneConfigRequestData nowVaneConfigRequestData ;
        int myMaxLine;
        bool isMyAlive;

        bool isKeyNeed = true;
        public bool _isKeyNeed
        {
            get
            {
                return isKeyNeed;
            }
            set
            {
                isKeyNeed = value;
            }
        }

        string myGwKey = "";
        public string _myGwKey
        {
            get
            {
                return myGwKey;
            }
            set
            {
                myGwKey = value;
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="yourKey">网关密码</param>
        /// <returns>返回结果</returns>
        public bool doConfiguration(string yourKey)
        {
            if (nowSocket.IsTcpClientConnected)
            {
                byte[] tempDataToSend = nowVaneConfigRequestData.devConfigurationQuery(myGwID, yourKey);
                nowSocket.SendData(tempDataToSend);
            }
            else
            {
                return false;
            }
            return true;
        }

        private void MyVaneConfig_Load(object sender, EventArgs e)
        {
            //cantrol and vaule
            splitContainer_orderForm.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            myMaxLine = ((AutoRunner)(this.Parent.Parent.Parent.Parent)).MaxLine;
            cb_verUpgradeRequest_verType.SelectedIndex = 0;
            isMyAlive = true;
            this.Activate();
            //class
            nowVaneConfigRequestData = new myVaneConfigRequestData();
            myBeatTimer.Interval = 20000;
            myBeatTimer.Tick += new EventHandler(myBeatTimer_Tick);
            //test
            Thread myThread = new Thread(new ParameterizedThreadStart(newTestThread));
            myThread.IsBackground = true;
            myThread.Start("");

            //set the from name
            this.Text += ("  "+myIpEp.Address.ToString());
            nowSocket = new MyTcpClient(myIpEp, 500);
            if (!nowSocket.IsTcpClientConnected)  
            {
                if (nowSocket.Connect())
                {
                    nowSocket.OnReceiveData += new MyTcpClient.delegateReceiveData(nowSocket_nowReceiveData);
                    nowSocket.OnTcpConnectionLosted += new MyTcpClient.ConnectionLosted(nowSocket_OnTcpConnectionLosted);
                    myBeatTimer.Enabled = true;
                    MyPutInKey keyWindow = new MyPutInKey();
                    keyWindow.Owner = this;
                    keyWindow.StartPosition = FormStartPosition.CenterParent;
                    keyWindow.ShowDialog();
                    {
                        if (isKeyNeed)
                        {
                            //MessageBox.Show(myGwKey);
                            doConfiguration(myGwKey);
                        }
                        else
                        {
                            MessageBox.Show("NOT FIND THE KEY");
                        }
                    }
                }
                else
                {
                    MessageBox.Show(nowSocket.ErroerMessage);
                    this.Close();
                }
            }
        }

        void myBeatTimer_Tick(object sender, EventArgs e)
        {
            byte[] tempDataToSend;
            tempDataToSend = nowVaneConfigRequestData.devHeartBeatRequest();
            nowSocket.SendData(tempDataToSend);
            if(isMyAlive)
            {
                pictureBox_headHeart.Image = ((AutoRunner)(this.Parent.Parent.Parent.Parent)).imageListForButton.Images[3];
            }
        }

        /// <summary>
        /// the tcp have Losted i will deal here
        /// </summary>
        void nowSocket_OnTcpConnectionLosted()
        {
            DialogResult yourResult = MessageBox.Show("连接中断！是否关闭该窗口", myIpEp.Address.ToString(), MessageBoxButtons.AbortRetryIgnore);
            if (yourResult == DialogResult.Retry)
            {
                if (nowSocket.Connect())
                {
                }
                else
                {
                    pictureBox_headHeart.Image = ((AutoRunner)(this.Parent.Parent.Parent.Parent)).imageListForButton.Images[2];
                    this.Text += " DisConnect";
                    MessageBox.Show(nowSocket.ErroerMessage, "STOP");
                }
            }
            else if (yourResult == DialogResult.Abort)
            {
                //已知风险，重复关闭
                this.Close();
            }
            else
            {
                pictureBox_headHeart.Image = ((AutoRunner)(this.Parent.Parent.Parent.Parent)).imageListForButton.Images[2];
                this.Text += " DisConnect";
            }
        }

        /// <summary>
        /// here i will receive the back data 
        /// </summary>
        /// <param name="yourData">back data </param>
        public void nowSocket_nowReceiveData(byte[] yourData)
        {
            //MessageBox.Show(myVaneConfigTool.getHexByBytes(yourData));
            richTextBox_vaneConfigResponse.Text += "ReceiveData:  " + myVaneConfigTool.getHexByBytes(yourData) + "\n";
            vaneConfig nowCig = new vaneConfig("");
            nowCig = myVaneConfigTool.myConfigDataAnalyze(yourData);

            List<ListViewItem> myTempItems = new List<ListViewItem>();
            bool isHeatBeat = false;
            myTempItems.Add(new ListViewItem(new string[] { "", "" }));
            foreach (KeyValuePair<byte[], byte[]> tempKvp in nowCig.Contents)
            {
                //如果是心跳返回
                if (myVaneConfigTool.isBytesSame(tempKvp.Key, new byte[] { 0x00, 0x81 }) && myVaneConfigTool.isBytesSame(tempKvp.Value, new byte[] { 0x00, 0x02 }))
                {
                    isHeatBeat = true;
                    break;
                }
                //如果是口令返回则更新Token信息
                else if (myVaneConfigTool.isBytesSame(tempKvp.Key, new byte[] { 0x00, 0x81 }) && myVaneConfigTool.isBytesSame(tempKvp.Value, new byte[] { 0x00, 0x05 }))
                {
                    nowVaneConfigRequestData.myRunTimeGwID = myVaneConfigTool.pickOutContent(nowCig, 0x86);
                    nowVaneConfigRequestData.myRunTimeGwToken = myVaneConfigTool.rmBytesEnd(myVaneConfigTool.pickOutContent(nowCig, 0x98));

                    if (!myVaneConfigTool.isBytesSame(myVaneConfigTool.pickOutContent(nowCig, 0x84), new byte[] { 0x00, 0x00, 0x00, 0x00 }))
                    {
                        MessageBox.Show("您输入的密码可能是错误的！");
                    }
                }
                //如果返回EP列表信息则进行进一步解析
                else if (myVaneConfigTool.isBytesSame(tempKvp.Key, new byte[] { 0x00, 0x81 }) && myVaneConfigTool.isBytesSame(tempKvp.Value, new byte[] { 0x00, 0x09 }))
                {
                    try
                    {
                        showEpList(myVaneConfigTool.pickOutContent(nowCig, 0x8F));
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.PutInLog(ex);
                        MessageBox.Show(ex.Message);
                    }

                    richTextBox_epListData.Text = myVaneConfigTool.getHexByBytes(myVaneConfigTool.pickOutContent(nowCig, 0x8F));
                }
                //如果返回的版本检查结果，追加解析
                else if (myVaneConfigTool.isBytesSame(tempKvp.Key, new byte[] { 0x00, 0x81 }) && myVaneConfigTool.isBytesSame(tempKvp.Value, new byte[] { 0x00, 0x16 }))
                {
                    try
                    {
                        showVerList(myVaneConfigTool.pickOutContents(nowCig, 0xA6));
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.PutInLog(ex);
                        MessageBox.Show(ex.Message);
                    }
                }
                //如果是一键添加返回的EP添加信息
                else if (myVaneConfigTool.isBytesSame(tempKvp.Key, new byte[] { 0x00, 0x81 }) && myVaneConfigTool.isBytesSame(tempKvp.Value, new byte[] { 0x00, 0x1B }))
                {
                    listView_addAllEpList.Items.Add(new ListViewItem(new string[] { (listView_addAllEpList.Items.Count + 1).ToString(), myVaneConfigTool.byteToHexStr(myVaneConfigTool.pickOutContent(nowCig, 0x91)) }));
                }
                string tempKey, tempValue = "";
                tempKey = myVaneConfigTool.findInterpretation(myShareData.myAssignedNumberDictionary, tempKvp.Key);
                tempValue = myVaneConfigTool.getVaneContentVaule(tempKvp);
                myTempItems.Add(new ListViewItem(new string[] { tempKey, tempValue }));
            }

            if (isHeatBeat)
            {
                pictureBox_headHeart.Image = ((AutoRunner)(this.Parent.Parent.Parent.Parent)).imageListForButton.Images[4];
            }
            else
            {
                if (myTempItems.Count > 0)
                {
                    listView_AllDataBack.BeginUpdate();
                    foreach (ListViewItem tempListViewItem in myTempItems)
                    {
                        listView_AllDataBack.Items.Add(tempListViewItem);
                    }
                    listView_AllDataBack.EnsureVisible(listView_AllDataBack.Items.Count - 1);
                    listView_AllDataBack.EndUpdate();
                }
            }
        }

        /// <summary>
        /// show ep in listView_epList
        /// </summary>
        /// <param name="yourData">ep data</param>
        public void showEpList(byte[] yourData)
        {
            List<vaneConfigEpInfo> myEpList= myVaneConfigTool.pickOutEpInfo(yourData);
            if (myEpList == null)
            {
            }
            else
            {
                listView_epList.BeginUpdate();
                listView_epList.Items.Clear();
                foreach (vaneConfigEpInfo tempEpInfo in myEpList)
                {
                    listView_epList.Items.Add(new ListViewItem(new string[] { (listView_epList.Items.Count + 1).ToString(), tempEpInfo.epId, tempEpInfo.epName, tempEpInfo.epSignal, tempEpInfo.epBattery, tempEpInfo.epOnline, tempEpInfo.epType, tempEpInfo.epModel, tempEpInfo.epVersion, tempEpInfo.errorMsg }));
                }
                listView_epList.EndUpdate();
            }
        }

        /// <summary>
        /// show new version list
        /// </summary>
        /// <param name="yourData">version list </param>
        public void showVerList(List<byte[]> yourData)
        {
            List<vaneConfigNewVerInfo> myVerList = myVaneConfigTool.pickOutVerInfo(yourData);
            if (myVerList == null)
            {
                listView_newVersionList.Items.Clear();
            }
            else
            {
                listView_newVersionList.BeginUpdate();
                listView_newVersionList.Items.Clear();
                foreach (vaneConfigNewVerInfo tempVerInfo in myVerList)
                {
                    listView_newVersionList.Items.Add(new ListViewItem(new string[] { tempVerInfo.verType, tempVerInfo.verEpId, tempVerInfo.verCurrentVersion, tempVerInfo.verCode, tempVerInfo.verDesc, tempVerInfo.verString, tempVerInfo.errorMsg}));
                }
                listView_newVersionList.EndUpdate();
            }
        }

        public void newTestThread(object xx)
        {
            //while (true)
            //{
            //    Thread.Sleep(2000);
            //    this.tabControl1.TabPages[0].Text = this.tabControl1.TabPages[0].Text + 1;
            //}
            //nowSocket.disConnectClient();
            //MessageBox.Show(nowSocket.myErroerMessage);
        }

        #region MyRequestClick
        private void pictureBox_send_Click(object sender, EventArgs e)
        {
            byte[] tempDataToSend;
            switch (((PictureBox)sender).Name)
            {
                case "pictureBox_ConfigRequest_send":
                    tempDataToSend = nowVaneConfigRequestData.devConfigurationModifyRequest(tb_ConfigRequest_GwName.Text, tb_ConfigRequest_GwKey.Text);                 
                    break;
                case "pictureBox_NewVersionCheck_send":
                    tempDataToSend = nowVaneConfigRequestData.devNewVersionCheckRequest();
                    break;
                case "pictureBox_NewVersionUpgrade_send":
                    byte[] tempSendVer = myVaneConfigTool.strToToHexByte(tb_verUpgradeRequest_verCode.Text);
                    if (tempSendVer == null)
                    {
                        MessageBox.Show("版本输入不符规范" , "Stop");
                        return;
                    }
                    tempDataToSend = nowVaneConfigRequestData.devNewVersionUpgradeRequest(cb_verUpgradeRequest_verType.SelectedIndex, tempSendVer, tb_verUpgradeRequest_EpId.Text);
                    break;
                case "pictureBox_EpAddRequest_send":
                    tempDataToSend = nowVaneConfigRequestData.devEpAddRequest(tb_EpAddRequest_EpId.Text, tb_EpAddRequest_EpName.Text);
                    break;
                case "pictureBox_EpRemoveRequest_send":
                    tempDataToSend = nowVaneConfigRequestData.devEpRemoveRequest(tb_EpRemoveRequest_EpId.Text);
                    break;
                case "pictureBox_EpModifyRequest_send":
                    tempDataToSend = nowVaneConfigRequestData.devEpModifyRequest(tb_EpModifyRequest_EpId.Text, tb_EpModifyRequest_EpName.Text);
                    break;
                case "pictureBox_EpListRequest_send":
                    tempDataToSend = nowVaneConfigRequestData.devEpListRequest();
                    break;
                case "pictureBox_AddAllStart_send":
                    listView_addAllEpList.Items.Clear();
                    tempDataToSend = nowVaneConfigRequestData.devAddAllStartRequest();
                    break;
                case "pictureBox_AddAllStop_send":
                    tempDataToSend = nowVaneConfigRequestData.devAddAllStopRequest();
                    break;
                default: 
                    tempDataToSend = new byte[] { 0xff, 0xff };
                    break;
            }
            richTextBox_vaneConfigRequest.Text = myVaneConfigTool.getHexByBytes(tempDataToSend);
            nowSocket.SendData(tempDataToSend);
        }

        private void pictureBox_doRequest_Click(object sender, EventArgs e)
        {
            byte[] tempDataToSend;
            tempDataToSend = myVaneConfigTool.strToToHexByte(richTextBox_vaneConfigRequest.Text);
            if (tempDataToSend == null)
            {
                MessageBox.Show("find illegal data ", "STOP");
            }
            else
            {
                nowSocket.SendData(tempDataToSend);
            }
        }
        #endregion

        private void richTextBox_vaneConfigResponse_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox_vaneConfigResponse.Lines.Length > myMaxLine )
            {
                richTextBox_vaneConfigResponse.Clear();
            }
            richTextBox_vaneConfigResponse.SelectionStart = richTextBox_vaneConfigResponse.Text.Length;
            Application.DoEvents();
        }

        private void listView_epList_DoubleClick(object sender, EventArgs e)
        {
            if (listView_epList.SelectedItems != null)
            {
                Clipboard.SetText(listView_epList.SelectedItems[0].SubItems[1].Text);
            }
        }

        //复制元素
        private void listView_AllDataBack_DoubleClick(object sender, EventArgs e)
        {
            if (listView_AllDataBack.SelectedItems != null)
            {
                Clipboard.SetText(listView_AllDataBack.SelectedItems[0].SubItems[1].Text);
            }
        }

        //关闭配置窗体
        private void MyVaneConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (nowSocket != null)
            {
                if (nowSocket.IsTcpClientConnected)
                {
                    nowSocket.DisConnect();
                }
            }
            isMyAlive = false;
        }

        //大小改变
        private void MyVaneConfig_SizeChanged(object sender, EventArgs e)
        {
            splitContainer_allFrom.Height = this.Height - 26;
            splitContainer_allFrom.Width = this.Width - 4;
        }

        #region WndProc
        const int WM_SYSCOMMAND = 0x112;
        const int SC_CLOSE = 0xF060;
        const int SC_MINIMIZE = 0xF020;
        const int SC_MAXIMIZE = 0xF030;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SYSCOMMAND)
            {
                if (m.WParam.ToInt32() == SC_MINIMIZE)
                {
                    //this.Visible = false;
                    return;
                }
            }
            base.WndProc(ref m);
        }
        #endregion

        bool isFormHide = false;
        private void MyVaneConfig_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                int myFormHideIndex = 0;
                foreach (var tempContor in this.Parent.Controls)
                {
                    if (tempContor is MyVaneConfig)
                    {
                        if (((MyVaneConfig)tempContor).isFormHide)
                        {
                            myFormHideIndex++;
                        }
                    }
                }
                Point myLocation =Point.Empty;
                if (this.Location.Y - 22 * myFormHideIndex > -1)
                {
                    myLocation = new Point(this.Location.X, this.Location.Y - 22 * myFormHideIndex);
                }
                else
                {

                }
                this.WindowState = FormWindowState.Normal;
                this.Location = myLocation;
                this.Width = 400;
                this.Height = 25;
               
                isFormHide = true;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                if (isFormHide)
                {
                    this.WindowState = FormWindowState.Normal;
                    this.Location = new Point(0, 0);
                    this.Width = 732;
                    this.Height = 363;
                    isFormHide = false;
                }
            }
        }

    }

}
