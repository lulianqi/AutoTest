using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using AutoTest.myTool;
using MyCommonHelper;
using MyCommonHelper.NetHelper;

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
    public partial class myDirectionalTestForDevEpAdd : Form
    {
        public myDirectionalTestForDevEpAdd()
        {
            InitializeComponent();
        }

        public myDirectionalTestForDevEpAdd(IPEndPoint yourIpEp, string yourSn)
        {
            InitializeComponent();
            myIpEp = yourIpEp;
            myGwSn = yourSn;
        }

        public struct myTestState
        {
            public bool isEpAdded;              //是否正在添加
            public bool isEpDeled;              //是否真正删除
            public bool isSended;               //是否已经发出请求

            public int addSuccuseCount;         //成功添加到次数
            public int delSuccuseCount;         //失败添加的次数
            public int addFailCount;
            public int delFailCount;
            public int timeoutCount;            
            public int retryCount;              //重试次数
            public int disConnectCount;
            public int unknowCount;
            public int allCount;

            public int nextStep;
            public int waitTime;

            public myTestState(int yourWaitTime)
            {
                isEpAdded = false;
                isEpDeled = false;
                isSended = false;
                
                
                addSuccuseCount = 0;
                delSuccuseCount = 0;
                addFailCount = 0;
                delFailCount = 0;

                timeoutCount = 0;
                disConnectCount = 0;
                retryCount = 0;

                unknowCount = 0;
                allCount = 0;
                nextStep = 1;
                waitTime = yourWaitTime;
            }
        }

        int setTimeOut = 60;            //预设超时时间
        IPEndPoint myIpEp;
        string myGwSn;
        bool isTest;                    //是否正在进行测试
        int mywaitTime;                 //请求间隔时间
        int myTimeOut;                  //超时时间
        MySocket nowSocket;             
        System.Windows.Forms.Timer myBeatTimer = new System.Windows.Forms.Timer();
        myVaneConfigRequestData nowVaneConfigRequestData ;
        myTestState nowTestState;       //测试结果记录
        

        string myGwKey = "";
        /// <summary>
        /// 网关密码
        /// </summary>
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

        public bool doConfiguration(string yourKey)
        {
            if (nowSocket.isTcpClientConnected)
            {
                byte[] tempDataToSend = nowVaneConfigRequestData.devConfigurationQuery(myGwSn, yourKey);
                nowSocket.sendData(tempDataToSend);
            }
            else
            {

            }
            return true;
        }

        void myBeatTimer_Tick(object sender, EventArgs e)
        {
            byte[] tempDataToSend;
            tempDataToSend = nowVaneConfigRequestData.devHeartBeatRequest();
            nowSocket.sendData(tempDataToSend);
        }

        /// <summary>
        /// the tcp have Losted i will deal here
        /// </summary>
        void nowSocket_OnTcpConnectionLosted()
        {
            addMessage("连接中断");
Found:
            if (nowSocket.connectClient())
            {
                addMessage("重连成功");
                if (isTest)
                {
                    reStartTest();
                    addMessage("继续任务");
                }
            }
            else
            {
                addMessage(nowSocket.myErroerMessage);
                addMessage("重连失败");
                goto Found;
            }
            /*
            DialogResult yourResult = MessageBox.Show("连接中断！是否关闭该窗口", "stop", MessageBoxButtons.AbortRetryIgnore);
            if (yourResult == DialogResult.Retry)
            {
                if (nowSocket.connectClient())
                {
                    addMessage("重连成功");
                    if (isTest)
                    {
                        reStartTest();
                        addMessage("继续任务");
                    }
                }
                else
                {
                    MessageBox.Show(nowSocket.myErroerMessage, "STOP");
                }
            }
            else if (yourResult == DialogResult.Abort)
            {
                this.Close();
            }
            else
            {
                //do nothing
            }
            */
        }

        /// <summary>
        /// here i will receive the back data 
        /// </summary>
        /// <param name="yourData">back data </param>
        void nowSocket_nowReceiveData(byte[] yourData)
        {
            richTextBox_resData.Text += myVaneConfigTool.getHexByBytes(yourData) + "\n";
            vaneConfig nowCig = new vaneConfig("");
            nowCig = myVaneConfigTool.myConfigDataAnalyze(yourData);

            listView_AllDataBack.BeginUpdate();
            listView_AllDataBack.Items.Add(new ListViewItem(new string[] { "", "" }));
            foreach (KeyValuePair<byte[], byte[]> tempKvp in nowCig.Contents)
            {
                //如果是口令返回则更新Token信息
                if (myVaneConfigTool.isBytesSame(tempKvp.Key, new byte[] { 0x00, 0x81 }) && myVaneConfigTool.isBytesSame(tempKvp.Value, new byte[] { 0x00, 0x05 }))
                {
                    nowVaneConfigRequestData.myRunTimeGwID = myVaneConfigTool.pickOutContent(nowCig, 0x86);
                    nowVaneConfigRequestData.myRunTimeGwToken = myVaneConfigTool.rmBytesEnd(myVaneConfigTool.pickOutContent(nowCig, 0x98));
                    if (myVaneConfigTool.isBytesSame(myVaneConfigTool.pickOutContent(nowCig, 0x84), new byte[] { 0x00, 0x00, 0x00, 0x00 }))
                    {
                        addMessage("login seccuse");
                    }
                    else
                    {
                        MessageBox.Show("密码错误，请重新登录");
                        addMessage("密码错误，请重新登录");
                    }
                    
                }
                //获取或处理测试返回
                if (isTest)
                {
                    //添加返回
                    if (myVaneConfigTool.isBytesSame(tempKvp.Key, new byte[] { 0x00, 0x81 })&& myVaneConfigTool.isBytesSame(tempKvp.Value, new byte[] { 0x00, 0x0B }))
                    {
                        if (myVaneConfigTool.isBytesSame(myVaneConfigTool.pickOutContent(nowCig, 0x84), new byte[] { 0x00, 0x00, 0x00, 0x00 }))
                        {
                            nowTestState.nextStep = 2;
                            int tempTime = setTimeOut - myTimeOut;
                            addMessage("添加EP成功,即将删除 TIME:" + tempTime);
                            nowTestState.addSuccuseCount++;
                        }
                        else
                        {
                            nowTestState.nextStep = 1;
                            addMessage("添加EP失败，即将重新添加");
                            nowTestState.addFailCount++;
                        }
                        RefreshResult();
                    }

                    //删除返回
                    if (myVaneConfigTool.isBytesSame(tempKvp.Key, new byte[] { 0x00, 0x81 }) && myVaneConfigTool.isBytesSame(tempKvp.Value, new byte[] { 0x00, 0x0D }))
                    {
                        if (myVaneConfigTool.isBytesSame(myVaneConfigTool.pickOutContent(nowCig, 0x84), new byte[] { 0x00, 0x00, 0x00, 0x00 }))
                        {
                            nowTestState.nextStep = 1;
                            addMessage("删除EP成功，即将重新添加");
                            ////************************
                            //nowTestState.waitTime += 30;
                            //addMessage("进入延时");
                            ////************************
                            nowTestState.delSuccuseCount++;
                        }
                        else
                        {
                            nowTestState.nextStep = 2;
                            addMessage("删除EP失败，即将重新删除");
                            nowTestState.delFailCount++;
                        }
                        RefreshResult();
                    }

                }
                string tempKey, tempValue = "";
                tempKey = myVaneConfigTool.findInterpretation(myShareData.myAssignedNumberDictionary, tempKvp.Key);
                tempValue = myVaneConfigTool.getVaneContentVaule(tempKvp);
                listView_AllDataBack.Items.Add(new ListViewItem(new string[] { tempKey, tempValue }));
            }
            listView_AllDataBack.EnsureVisible(listView_AllDataBack.Items.Count - 1);
            listView_AllDataBack.EndUpdate();

        }

        private void myDirectionalTestForDevEpAdd_Load(object sender, EventArgs e)
        {
            isTest = false;
            mywaitTime = 10;
            myTimeOut = setTimeOut;
            comboBox_waitTime.SelectedIndex = 0;
            //class
            nowVaneConfigRequestData = new myVaneConfigRequestData();
            myBeatTimer.Interval = 20000;
            myBeatTimer.Tick += new EventHandler(myBeatTimer_Tick);

            //set the from name
            this.Text += ("  " + myIpEp.Address.ToString());

            nowSocket = new MySocket(myIpEp, 500);
            if (!nowSocket.isTcpClientConnected)
            {
                if (nowSocket.connectClient())
                {
                    nowSocket.OnReceiveData += new MySocket.delegateReceiveData(nowSocket_nowReceiveData);
                    nowSocket.OnTcpConnectionLosted += new MySocket.ConnectionLosted(nowSocket_OnTcpConnectionLosted);
                    myBeatTimer.Enabled = true;
                    addMessage("coonect success");
                }
                else
                {
                    MessageBox.Show(nowSocket.myErroerMessage);
                    this.Close();
                }
            }
        }

        private void bt_Login_Click(object sender, EventArgs e)
        {
            doConfiguration(tb_GwKey.Text);
            addMessage("start login");
        }

        private void bt_startTest_Click(object sender, EventArgs e)
        {
            if (isTest)
            {
                endTest();
                comboBox_waitTime.Enabled = true;
                bt_add.Enabled = bt_del.Enabled = true;
                bt_startTest.Text = "开始";
            }
            else
            {
                startTest();
                comboBox_waitTime.Enabled = false;
                bt_add.Enabled = bt_del.Enabled = false;
                bt_startTest.Text = "停止";
            }
            
        }

        //主任务
        private void timer_wait_Tick(object sender, EventArgs e)
        {
            if (isTest)
            {
                switch (nowTestState.nextStep)
                {
                    case 1:
                        nowTestState.waitTime--;
                        if (nowTestState.waitTime <1)
                        {
                            nowTestState.waitTime = mywaitTime;
                            nowTestState.nextStep = 0;
                            doAddEp();
                            myTimeOut = setTimeOut;
                            nowTestState.isSended = true;
                        }
                        break;
                    case 2:
                        nowTestState.waitTime--;
                        if (nowTestState.waitTime <1)
                        {
                            nowTestState.waitTime = mywaitTime;
                            nowTestState.nextStep = 0;
                            doDelEp();
                            myTimeOut = setTimeOut;
                            nowTestState.isSended = true;
                        }
                        break;
                    default:
                        nowTestState.nextStep = 0;  //设置为默认请务必放在顶部
                        if (myTimeOut > -1)
                        {
                            myTimeOut--;
                            if (myTimeOut <1)
                            {
                                addMessage("操作超时");
                                addMessage("超时重试");
                                reStartTest();
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 开始一个新任务
        /// </summary>
        private void startTest()
        {
            isTest = true;
            mywaitTime = comboBox_waitTime.SelectedIndex + 2;
            nowTestState = new myTestState(comboBox_waitTime.SelectedIndex+2);
            timer_wait.Enabled = true;
            addMessage("Start Test");
            reStartTest();
        }

        /// <summary>
        /// 结束任务
        /// </summary>
        private void endTest()
        {
            isTest = false;
            timer_wait.Enabled = false;
            addMessage("End Test");
        }

        /// <summary>
        /// 超时或断线重连
        /// </summary>
        private void reStartTest()
        {
            isTest = true;
            timer_wait.Enabled = true;
            nowTestState.nextStep = 1;
            nowTestState.waitTime = 1;
            nowTestState.retryCount++;
            RefreshResult();
            addMessage("re Start");
        }

        /// <summary>
        /// 添加EP
        /// </summary>
        private void doAddEp()
        {
            byte[] tempDataToSend;
            tempDataToSend = nowVaneConfigRequestData.devEpAddRequest(tb_EpId.Text, tb_EpName.Text);
            nowSocket.sendData(tempDataToSend);
            addMessage("开始添加");
        }

        /// <summary>
        /// 删除EP
        /// </summary>
        private void doDelEp()
        {
            byte[] tempDataToSend;
            tempDataToSend = nowVaneConfigRequestData.devEpRemoveRequest(tb_EpId.Text);
            nowSocket.sendData(tempDataToSend);
            addMessage("开始删除");
        }

        private void RefreshResult()
        {
            lb_testInfo_1.Text = nowTestState.addSuccuseCount.ToString();
            lb_testInfo_2.Text = nowTestState.addFailCount.ToString();
            lb_testInfo_3.Text = nowTestState.delSuccuseCount.ToString();
            lb_testInfo_4.Text = nowTestState.delFailCount.ToString();
            lb_testInfo_5.Text = nowTestState.retryCount.ToString();
        }

        private void richTextBox_info_TextChanged(object sender, EventArgs e)
        {
            richTextBox_info.SelectionStart = richTextBox_info.Text.Length;
            Application.DoEvents();
        }

        private void addMessage(string yourMes)
        {
            richTextBox_info.Text += (DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") +"：  "+ yourMes + "\n");
        }

        private void richTextBox_resData_TextChanged(object sender, EventArgs e)
        {
            richTextBox_resData.SelectionStart = richTextBox_resData.Text.Length;
            Application.DoEvents();
        }

        private void bt_add_Click(object sender, EventArgs e)
        {
            byte[] tempDataToSend;
            tempDataToSend = nowVaneConfigRequestData.devEpAddRequest(tb_EpId.Text, tb_EpName.Text);
            nowSocket.sendData(tempDataToSend);
        }

        private void bt_del_Click(object sender, EventArgs e)
        {
            byte[] tempDataToSend;
            tempDataToSend = nowVaneConfigRequestData.devEpRemoveRequest(tb_EpId.Text);
            nowSocket.sendData(tempDataToSend);
        }
    }
}
