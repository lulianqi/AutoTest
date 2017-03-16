using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Windows.Forms;
using System;

using AutoTest.myTool;
using AutoTest.myDialogWindow;
using System.Threading;
using MyCommonHelper;


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


namespace AutoTest
{
    public partial class AutoRunner
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//初始化一个Scoket协议
        IPEndPoint iep = new IPEndPoint(IPAddress.Any, myShareData.sdBroadcastPort);
        EndPoint ep;
        byte[] data = new byte[1024];
        byte[] dataToSend = new byte[1234];

        public string vc_configPath = "";

        private bool pictureBox_GwListMaxTag = false;                                           //GwList窗口是否处于最大化状态(任何时候都请不要单独设置该值)
        
        /// <summary>
        /// get the value that direction is GwList is in max mode 【the value is same as pictureBox_GwListMaxTag】
        /// </summary>
        public bool GwListMaxTag
        {
            get { return pictureBox_GwListMaxTag; }
        }

        System.Windows.Forms.Timer timer_vcBroadcast = new System.Windows.Forms.Timer();

        List<vaneConfigBroadcast> myVaneBroadcastConfigList = new List<vaneConfigBroadcast>();

        void timer_vcBroadcast_Tick(object sender, EventArgs e)
        {
            if (myVaneBroadcastConfigList.Count != 0)
            {
                for (int i = 0;  i < myVaneBroadcastConfigList.Count;i++)
                {
                    myVaneBroadcastConfigList[i].liveClick--;
                    if (myVaneBroadcastConfigList[i].liveClick < 1)
                    {
                        delGW(myVaneBroadcastConfigList, i);
                    }
                }
            }
            if (socket.Available > 0)
            {
                byte[] tempData = new byte[socket.Available];
                socket.ReceiveFrom(tempData, ref ep);
                richTextBox_BroadcastRecord.Text += myVaneConfigTool.getHexByBytes(tempData)+"\n";
                vaneConfig tempvaneConfig = myVaneConfigTool.myConfigDataAnalyze(tempData);
                if (tempvaneConfig.errorMsg == "")
                {
                    if (myVaneBroadcastConfigList.Count > 0)
                    {
                        bool tempIsNew = true;
                        for (int i = 0; i < myVaneBroadcastConfigList.Count; i++)
                        {
                            if (ContrastVaneConfigItem(myVaneBroadcastConfigList[i].myVaneConfig, tempvaneConfig, 0x99))
                            {
                                tempIsNew = false;
                                myVaneBroadcastConfigList[i].liveClick = 25;
                                if (!(ContrastVaneConfigItem(myVaneBroadcastConfigList[i].myVaneConfig, tempvaneConfig, 0x9D) &&
                                    ContrastVaneConfigItem(myVaneBroadcastConfigList[i].myVaneConfig, tempvaneConfig, 0x87) &&
                                    ContrastVaneConfigItem(myVaneBroadcastConfigList[i].myVaneConfig, tempvaneConfig, 0x89)))
                                {
                                    updataGW(tempvaneConfig,i);
                                }
                                break;
                            }
                        }
                        if (tempIsNew)
                        {
                            addGW(myVaneBroadcastConfigList,tempvaneConfig);
                        }
                    }
                    else
                    {
                        addGW(myVaneBroadcastConfigList,tempvaneConfig);
                    }
                }
                else
                {
                    ErrorLog.PutInLogEx(tempvaneConfig.errorMsg);
                }

            }
        }

        #region function

        public bool ContrastVaneConfigItem(vaneConfig yourVaneConfigA, vaneConfig yourVaneConfigB, byte yourAssignedNumber)
        {
            byte[] tempA = myVaneConfigTool.pickOutContent(yourVaneConfigA, yourAssignedNumber);
            byte[] tempB = myVaneConfigTool.pickOutContent(yourVaneConfigB, yourAssignedNumber);
            if (tempA == null || tempB == null)
            {
                return false;
            }
            if (tempA.Length == tempB.Length)
            {
                for (int i = 0; i < tempA.Length; i++)
                {
                    if (tempA[i] != tempB[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 更新一个网关-UI
        /// </summary>
        /// <param name="yourVaneConfig"></param>
        /// <param name="yourIndex"></param>
        public void updataGW(vaneConfig yourVaneConfig ,int yourIndex)
        {
            myVaneBroadcastConfigList[yourIndex] = new vaneConfigBroadcast(25, yourVaneConfig);
            listViewEx_GWlist.Items[yourIndex] = new ListViewItem(new string[] { (yourIndex+1).ToString(),
                    myVaneConfigTool.getIpByBytes(myVaneConfigTool.pickOutContent(yourVaneConfig, 0x89)),
                    myVaneConfigTool.getUtf8StrByBytes(myVaneConfigTool.pickOutContent(yourVaneConfig, 0x99)),
                    myVaneConfigTool.getUtf8StrByBytes(myVaneConfigTool.pickOutContent(yourVaneConfig, 0x87)),
                    myVaneConfigTool.findInterpretation(myShareData.myNewVersionFlagDictionary,myVaneConfigTool.pickOutContent(yourVaneConfig, 0x9D)), 
                    myVaneConfigTool.findInterpretation(myShareData.myGWAbilityDictionary,myVaneConfigTool.pickOutContent(yourVaneConfig, 0xA1))
            });
        }

        /// <summary>
        /// 添加一个网关-UI
        /// </summary>
        /// <param name="yourBrdList"></param>
        /// <param name="yourVaneConfig"></param>
        public void addGW(List<vaneConfigBroadcast> yourBrdList, vaneConfig yourVaneConfig)
        {
            yourBrdList.Add(new vaneConfigBroadcast(20, yourVaneConfig));
            updataGWlist(yourBrdList);
        }

        /// <summary>
        /// 移出一个网关-UI
        /// </summary>
        /// <param name="yourBrdList"></param>
        /// <param name="yourIndex"></param>
        public void delGW(List<vaneConfigBroadcast> yourBrdList, int yourIndex)
        {
            yourBrdList.Remove(yourBrdList[yourIndex]);
            updataGWlist(yourBrdList);
        }

        /// <summary>
        /// 更新网关列表-UI
        /// </summary>
        /// <param name="yourBroadcastList"></param>
        public void updataGWlist(List<vaneConfigBroadcast> yourBroadcastList)
        {

            listViewEx_GWlist.BeginUpdate();
            listViewEx_GWlist.Items.Clear();
            foreach (var tempConfigBroadcast in yourBroadcastList)
            {
                listViewEx_GWlist.Items.Add(new ListViewItem(new string[] {(listViewEx_GWlist.Items.Count+1).ToString(),
                    myVaneConfigTool.getIpByBytes(myVaneConfigTool.pickOutContent(tempConfigBroadcast.myVaneConfig, 0x89)),
                    myVaneConfigTool.getUtf8StrByBytes(myVaneConfigTool.pickOutContent(tempConfigBroadcast.myVaneConfig, 0x99)),
                    myVaneConfigTool.getUtf8StrByBytes(myVaneConfigTool.pickOutContent(tempConfigBroadcast.myVaneConfig, 0x87)),
                    myVaneConfigTool.findInterpretation(myShareData.myNewVersionFlagDictionary,myVaneConfigTool.pickOutContent(tempConfigBroadcast.myVaneConfig, 0x9D)), 
                    myVaneConfigTool.findInterpretation(myShareData.myGWAbilityDictionary,myVaneConfigTool.pickOutContent(tempConfigBroadcast.myVaneConfig, 0xA1))
                }));
            }
            listViewEx_GWlist.EndUpdate();
        }

        #endregion

        #region action

        public void loadVaneWifiConfig()
        {
            string[] tempModeList = new string[myShareData.myRouterAuthModeDictionary.Count];
            if (tempModeList.Length > 0)
            {
                int i = 0;
                foreach (KeyValuePair<byte, string> tempKvp in myShareData.myRouterAuthModeDictionary)
                {
                    tempModeList[i] = tempKvp.Value;
                    i++;
                }
            }
            else
            {
                tb_wifiCfg_SSID.Enabled = false;
                tb_wifiCfg_Key.Enabled = false;
                cb_wifiCfg_Mode.Text = "未发现配置文件";
                cb_wifiCfg_Mode.Enabled = false;
                return;
            }
            cb_wifiCfg_Mode.Items.AddRange(tempModeList);
            cb_wifiCfg_Mode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cb_wifiCfg_Mode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cb_wifiCfg_Mode.SelectedIndex = cb_wifiCfg_Mode.Items.Count - 1;
        }

        public void loadVaneConfigValue()
        {
            vc_configPath = startPath + "\\data\\VaneConfig.xml";
        }

        public void loadVaneConfigDictionary()
        {
            myTool.myVaneConfigTool.loadVaneConfigFile(vc_configPath, myShareData.myAssignedNumberDictionary, 0);
            myTool.myVaneConfigTool.loadVaneConfigFile(vc_configPath, myShareData.myProtocolTypeDictionary, 1);
            myTool.myVaneConfigTool.loadVaneConfigFile(vc_configPath, myShareData.myStatusDictionary, 2);
            myTool.myVaneConfigTool.loadVaneConfigFile(vc_configPath, myShareData.myContentTypeDictionary, 3);
            myTool.myVaneConfigTool.loadVaneConfigFile(vc_configPath, myShareData.myNewVersionFlagDictionary, 4);
            myTool.myVaneConfigTool.loadVaneConfigFile(vc_configPath, myShareData.myGWAbilityDictionary, 5);
            myTool.myVaneConfigTool.loadVaneConfigFile(vc_configPath, myShareData.myRouterAuthModeDictionary, 6);
            myTool.myVaneConfigTool.loadVaneConfigFile(vc_configPath, myShareData.myWifiAssignedNumberDictionary, 7);
            myTool.myVaneConfigTool.loadVaneConfigFile(vc_configPath, myShareData.myVersionTypeNumberDictionary, 8);
        }

        public void startReceiveBroadcast(int yourPort)
        {
            try
            {
                iep = new IPEndPoint(IPAddress.Any, yourPort);
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);// set it not only
                socket.Bind(iep);
                ep = (EndPoint)iep;
                timer_vcBroadcast.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ErrorLog.PutInLogEx(ex.Message);
            }
        }

        public void stopReceiveBroadcast()
        {
            socket.Close();
            timer_vcBroadcast.Enabled = false;
        }

        /// <summary>
        /// set the GwList size mode
        /// </summary>
        /// <param name="isMax">is max</param>
        public void setGwListMax(bool isMax)
        {
            if(pictureBox_GwListMaxTag!=isMax)
            {
                pictureBox_GwListMaxTag = isMax;
                redrawGwListSize();
            }
        }

        /// <summary>
        /// ajust the GwList Size from GwListMaxTag【pictureBox_GwListMaxTag】
        /// </summary>
        private void redrawGwListSize()
        {
            if (GwListMaxTag)
            {
                listViewEx_GWlist.Size = new System.Drawing.Size(this.Width - 7, this.Height - 84);
                pictureBox_gwListMax.Location = new System.Drawing.Point(listViewEx_GWlist.Width - 20, 5);
                pictureBox_gwListMax.Image = imageListForButton.Images[7];
            }
            else
            {
                listViewEx_GWlist.Size = new System.Drawing.Size(676, 120);
                pictureBox_gwListMax.Location = new System.Drawing.Point(656, 5);
                pictureBox_gwListMax.Image = imageListForButton.Images[6];
            }
        }

        #endregion

        #region event

        public void vcLoad()
        {
            loadVaneConfigValue();
            loadVaneConfigDictionary();
            loadVaneWifiConfig();

            timer_vcBroadcast.Interval = 500;
            timer_vcBroadcast.Tick += new EventHandler(timer_vcBroadcast_Tick);

            startReceiveBroadcast(myShareData.sdBroadcastPort);

            /*
            //Test
            myDialogWindow.AboutBox OptionWindow = new myDialogWindow.AboutBox();
            //myAboutBox.StartPosition = FormStartPosition.CenterParent;
            //this.IsMdiContainer = true;
            OptionWindow.TopLevel = false;
            OptionWindow.Parent = this.ribbonPanel2;
            OptionWindow.Show();

            Form xx = new Form();
            xx.TopLevel = false;
            xx.Parent = this.panel_configMain;
            xx.Show();
            * */ 
        }

        public void vcAutoTest_Resize(object sender, EventArgs e)
        {
            //panel_configMain
            panel_configMain.Height = this.Height - 203;
            panel_configMain.Width = this.Width - 5;

            //richTextBox_BroadcastRecord
            richTextBox_BroadcastRecord.Width = this.Width - 690;

            //expandablePanel_vaneWifiConfig
            expandablePanel_vaneWifiConfig.Location = new System.Drawing.Point(this.Width-408, expandablePanel_vaneWifiConfig.Location.Y);

            //listViewEx_GWlist
            redrawGwListSize();
        }

        private void pictureBox_gwListMax_Click(object sender, EventArgs e)
        {
            setGwListMax(!pictureBox_GwListMaxTag);
        }


        public void vcAutoTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            //close all child window .if it is alive the thread that creat by it may throw error
            foreach (var tempContor in this.panel_configMain.Controls)
            {
                if (tempContor is MyVaneConfig)
                {
                    ((MyVaneConfig)tempContor).Close();
                }
            }
        }

        private void richTextBox_BroadcastRecord_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox_BroadcastRecord.Lines.Length > maxLine)
            {
                richTextBox_BroadcastRecord.Clear();
            }
            //定位到尾行
            richTextBox_BroadcastRecord.SelectionStart = richTextBox_BroadcastRecord.Text.Length;
            //设置trb_addRecord.HideSelection = false;，即可以不用使用.Focus()
            //trb_addRecord.Focus();
            Application.DoEvents();
        }

        private void listViewEx_GWlist_DoubleClick(object sender, EventArgs e)
        {
            //testWindow myTestWindow = new testWindow();
            //myTestWindow.TopLevel = false;
            //myTestWindow.Parent = this.panel_configMain;
            //myTestWindow.Show();
            //return;
            if (listViewEx_GWlist.SelectedItems != null)
            {
                if (listViewEx_GWlist.SelectedItems.Count == 0)
                {
                    return;
                }
                setGwListMax(false);
                int nowSelectIndex = listViewEx_GWlist.SelectedItems[0].Index;
                byte[] tempIp=myVaneConfigTool.pickOutContent(myVaneBroadcastConfigList[nowSelectIndex].myVaneConfig,0x89);
                int tempPort = myVaneConfigTool.getByteLen(myVaneConfigTool.pickOutContent(myVaneBroadcastConfigList[nowSelectIndex].myVaneConfig, 0x8A));
                string tempGW_ID = myVaneConfigTool.getUtf8StrByBytes(myVaneConfigTool.pickOutContent(myVaneBroadcastConfigList[nowSelectIndex].myVaneConfig, 0x86));
                IPEndPoint tempIep = new IPEndPoint(new IPAddress(tempIp), tempPort);
                vaneConfigConnectInfo tempConnectInfo = new vaneConfigConnectInfo(tempGW_ID, tempIep);
                Thread myThread = new Thread(new ParameterizedThreadStart(newVaneCfgWindow));
                myThread.Start(tempConnectInfo);
            }
        }

        private void ToolStripMenuItem_DirectionalTest_1_Click(object sender, EventArgs e)
        {

            if (listViewEx_GWlist.SelectedItems != null)
            {
                if (listViewEx_GWlist.SelectedItems.Count == 0)
                {
                    return;
                }
                int nowSelectIndex = listViewEx_GWlist.SelectedItems[0].Index;
                byte[] tempIp = myVaneConfigTool.pickOutContent(myVaneBroadcastConfigList[nowSelectIndex].myVaneConfig, 0x89);
                int tempPort = myVaneConfigTool.getByteLen(myVaneConfigTool.pickOutContent(myVaneBroadcastConfigList[nowSelectIndex].myVaneConfig, 0x8A));
                string tempGW_SN = myVaneConfigTool.getUtf8StrByBytes(myVaneConfigTool.pickOutContent(myVaneBroadcastConfigList[nowSelectIndex].myVaneConfig, 0x99));
                IPEndPoint tempIep = new IPEndPoint(new IPAddress(tempIp), tempPort);
                vaneConfigConnectInfo tempConnectInfo = new vaneConfigConnectInfo(tempGW_SN, tempIep);
                Thread myThread = new Thread(new ParameterizedThreadStart(newDirectionalTestForDevEpAdd));
                myThread.Start(tempConnectInfo);
            }
        }

        public void newVaneCfgWindow(object ConnectInfo)
        {
            if (this.InvokeRequired)
            {
                //this.BeginInvoke(new Action<string>((str) => ShowMessage(str)), yourContent);
                this.Invoke(new MethodInvoker(delegate { newVaneCfgWindow(ConnectInfo); }));
                return;
            }
            MyVaneConfig vcWindow = new MyVaneConfig(((vaneConfigConnectInfo)ConnectInfo).myIpEp, ((vaneConfigConnectInfo)ConnectInfo).myGW_ID);
            vcWindow.TopLevel = false;
            vcWindow.Parent = this.panel_configMain;
            try
            {
                vcWindow.Show();
            }
            catch (Exception ex)
            {
                ErrorLog.PutInLogEx(ex);
            }
        }

        public void newDirectionalTestForDevEpAdd(object ConnectInfo)
        {
            //if (this.InvokeRequired)
            //{
            //    this.Invoke(new MethodInvoker(delegate { newDirectionalTestForDevEpAdd(ConnectInfo); }));
            //    return;
            //}
            myDirectionalTestForDevEpAdd myDirectionalTestForDevEpAddWindow = new myDirectionalTestForDevEpAdd(((vaneConfigConnectInfo)ConnectInfo).myIpEp, ((vaneConfigConnectInfo)ConnectInfo).myGW_ID);
            try
            {
                myDirectionalTestForDevEpAddWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorLog.PutInLogEx(ex);
            }
        }

        #endregion

        #region Vane Wifi Config

        Socket myWifiCfgSocket ;                    //发送广播
        Socket myWifiCfgBroadcast ;                 //接收广播
        IPEndPoint myWifiCfgEp ;                    //发送EP
        IPEndPoint myWifiCfgReceiveEp;              //接收EP
        EndPoint tempEp;                            //端口
        int wifiCfgBroadcastLifeTime = myShareData.sdWifiCfgBroadcastLifeTime;

        System.Windows.Forms.Timer timer_wifiCfgBroadcast = new System.Windows.Forms.Timer();

        private void startWifiCfgBroadcast()
        {
            if (timer_wifiCfgBroadcast.Enabled == true)
            {
                wifiCfgBroadcastLifeTime = myShareData.sdWifiCfgBroadcastLifeTime;
                progressBarX_WifiConfig.Value = 0;
            }
            else
            {
                wifiCfgBroadcastLifeTime = myShareData.sdWifiCfgBroadcastLifeTime;
                myWifiCfgBroadcast = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                myWifiCfgReceiveEp = new IPEndPoint(IPAddress.Any, myShareData.sdWifiCfgPort);
                myWifiCfgBroadcast.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);// set it not only
                myWifiCfgBroadcast.Bind(myWifiCfgReceiveEp);

                tempEp = (EndPoint)myWifiCfgReceiveEp;
                timer_wifiCfgBroadcast.Interval = 500;
                timer_wifiCfgBroadcast.Tick += new EventHandler(timer_wifiCfgBroadcast_Tick);
                timer_wifiCfgBroadcast.Enabled = true;
                progressBarX_WifiConfig.Value = 0;
            }
        }

        private void stopWifiCfgBroadcast()
        {
            timer_wifiCfgBroadcast.Tick -= new EventHandler(timer_wifiCfgBroadcast_Tick);
            timer_wifiCfgBroadcast.Enabled = false;
            myWifiCfgBroadcast.Close();
        }

        void timer_wifiCfgBroadcast_Tick(object sender, EventArgs e)
        {
            if (wifiCfgBroadcastLifeTime < 0)
            {
                stopWifiCfgBroadcast();
            }
            else
            {
                wifiCfgBroadcastLifeTime--;
                progressBarX_WifiConfig.Value += 1;
                if (myWifiCfgBroadcast.Available > 0)
                {
                    byte[] tempData = new byte[myWifiCfgBroadcast.Available];
                    myWifiCfgBroadcast.ReceiveFrom(tempData, ref tempEp);
                    //MessageBox.Show(myVaneConfigTool.getHexByBytes(tempData));
                    vaneConfig tempvaneConfig = myVaneConfigTool.myConfigDataAnalyze(tempData);

                    listView_WifiConfigDataBack.BeginUpdate();
                    listView_WifiConfigDataBack.Items.Add(new ListViewItem(new string[] { "", "" }));
                    foreach (KeyValuePair<byte[], byte[]> tempKvp in tempvaneConfig.Contents)
                    {
                        //如果接收到wifi配置返回
                        if (myVaneConfigTool.isBytesSame(tempKvp.Key, new byte[] { 0x00, 0x81 }) && myVaneConfigTool.isBytesSame(tempKvp.Value, new byte[] { 0x00, 0xA1 }))
                        {
                            if (myVaneConfigTool.isBytesSame(myVaneConfigTool.pickOutContent(tempvaneConfig, 0x84), new byte[] { 0x00, 0x00, 0x00, 0x00 }))
                            {
                            }
                            else
                            {
                            }
                        }
                        string tempKey = myVaneConfigTool.findInterpretation(myShareData.myWifiAssignedNumberDictionary, tempKvp.Key);
                        string tempValue = myVaneConfigTool.getVaneWifiContentVaule(tempKvp);
                        listView_WifiConfigDataBack.Items.Add(new ListViewItem(new string[] { tempKey, tempValue }));
                    }
                    listView_WifiConfigDataBack.EnsureVisible(listView_WifiConfigDataBack.Items.Count - 1);
                    listView_WifiConfigDataBack.EndUpdate();
                }
            }
        }

        private void pictureBox_startWifiConfig_Click(object sender, EventArgs e)
        {
            startWifiCfgBroadcast();

            myWifiCfgSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            myWifiCfgSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            myVaneConfigRequestData myWifiCfgRequestData = new myVaneConfigRequestData();
            //获取所以网口的广播地址，如果没有则使用限制广播地址
            IPAddress[] myBroadIpList = MyNetConfig.getBroadcasetAddress();
            if (myBroadIpList.Length == 0)
            {
                myBroadIpList = new IPAddress[] { IPAddress.Broadcast };
            }
            foreach (IPAddress tempIp in myBroadIpList)
            {
                myWifiCfgEp = new IPEndPoint(tempIp, myShareData.sdWifiCfgPort);//255.255.255.255  IPAddress.Parse("192.168.11.255")
                myWifiCfgSocket.SendTo(myWifiCfgRequestData.vaneWifiConfigRequest(tb_wifiCfg_SSID.Text, tb_wifiCfg_Key.Text, cb_wifiCfg_Mode.SelectedIndex), myWifiCfgEp);
            }
            myWifiCfgSocket.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            startWifiCfgBroadcast();
        }

        #endregion
    }
}