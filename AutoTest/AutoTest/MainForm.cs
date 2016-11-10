using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AutoTestForVane.myTool;
using System.Xml;
using System.Net;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net.NetworkInformation;


/*******************************************************************************
* Copyright (c) 2013,浙江风向标科技有限公司
* All rights reserved.
* 
* 文件名称: 
* 内容摘要: mycllq@hotmail.com
* 
* 历史记录:
* 日	  期:   201301022          创建人: 测试部 李杰 15158155511
* 描    述: 创建
*******************************************************************************/

namespace AutoTestForVane
{
    public partial class AutoTestForVane : Form
    {
        public AutoTestForVane()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;                                    //自行控制线程安全
        }

        #region basicDefined
        bool isRunCase = false;                                                                 //运行状态（请不要在逻辑中试图单独置否）
        bool isRunTest = false;                                                                 //是否正在进（行单条测试,请不要在逻辑中试图单独置否）
        int dataListNum = 0;                                                                    //当前反馈数据集条数
        int timerCount = 0;                                                                     //终端接收信息确认
        bool isRead93 = false;                                                                  //接收确认
        int timerFlag = 0;                                                                      //状态标识
        bool isRead90 = false;                                                                  //准备接收
        string casePath = System.Environment.CurrentDirectory + "\\casefile\\case1.xml";        //默认脚本路径
        int nowIndex = 0;                                                                       //运行时索引
        int nowCount = 0;                                                                       //运行时工程命令总数
        int endIndex = 0;                                                                       //运行时结束索引

        bool isRunAll = false;                                                                  //是否执行整个文件
        //int rootIndex = 0;                                                                    //执行项目的当前索引
        //int rootNum = 0;                                                                      //执行项目的当前数量

        int repeatTime = 0;                                                                     //循环执行的次数
        int repeatStartIndex = 0;                                                               //循环其实索引
        bool isCatchEnd = false;                                                                //循环终止点
        int showIcoIndex = 0;                                                                   //正在被设置图标的tree节点索引

        bool isChooseTree = false;                                                              //是否选中tree产生
        string[] myApiNameArray;                                                                //api接口名列表

        string myDataWillSend = "";                                                             //将要发送的数据
        string myDataWillRecive = "";                                                           //预期返回值

        int sleepTime = 100;                                                                    //单次睡眠时间
        bool isFistLoad = true;                                                                 //是否为首次加载
        #endregion

        #region oriented Defined
        public System.Windows.Forms.Timer myCasetimer = new System.Windows.Forms.Timer();       //运行状态计数器

        public StringBuilder myBuilder_1 = new StringBuilder();                                 //可变字符串


        public myTestProjectAnalysis myCase = new myTestProjectAnalysis();                      //用例数据处理集,将向外提供
        myDataIn tempData = new myDataIn("");                                                   //终端返回数据
        List<caseProject> mycaseProjects = new List<caseProject>();                             //树中所有工程集合

        XmlLinkedNode startXmlNode;                                                             //运行时开始的xml节点
        TreeNode startTreeNode;                                                                 //运行时开始的tree节点
        XmlNode parentsXml;                                                                     //运行时开始的xml父节点
        TreeNode parentsTree;                                                                   //运行时开始的tree父节点
        public XmlNode showNode;                                                                //正在显示的xml节点,将对外提供
        myDataOut showMdt = new myDataOut();                                                    //正在显示的数据结构
        XmlNode showApiTipNode;                                                                 //正在显示的数据结构提示
        #endregion

        #region myfunction

        /// <summary>
        /// 启动其他程序
        /// </summary>
        /// <param name="FileName">需要启动的外部程序名称</param>
        public static bool OpenPress(string FileName, string Arguments)
        {
            System.Diagnostics.Process pro = new System.Diagnostics.Process();
            if (System.IO.File.Exists(FileName))
            {
                pro.StartInfo.FileName = FileName;
                pro.StartInfo.Arguments = Arguments;
                pro.Start();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 用于检查IP地址或域名是否可以使用TCP/IP协议访问(使用Ping命令),true表示Ping成功,false表示Ping失败 
        /// </summary>
        /// <param name="strIpOrDName">输入参数,表示IP地址或域名</param>
        /// <returns></returns>
        public static bool PingIpOrDomainName(string strIpOrDName)
        {
            try
            {
                Ping objPingSender = new Ping();
                PingOptions objPinOptions = new PingOptions();
                objPinOptions.DontFragment = true;
                string data = "";
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                int intTimeout = 120;
                PingReply objPinReply = objPingSender.Send(strIpOrDName, intTimeout, buffer, objPinOptions);
                string strInfo = objPinReply.Status.ToString();
                if (strInfo == "Success")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// find the file name from filepath
        /// </summary>
        /// <param name="myPath"> file path</param>
        /// <returns>file name</returns>
        public string findFileName(string myPath)
        {
            string tempFileName = myPath.Remove(0, myPath.LastIndexOf('\\') + 1);
            return tempFileName;
        }

        /// <summary>
        /// 添加带颜色内容
        /// </summary>
        /// <param name="rtb">目标richtextbox</param>
        /// <param name="strInput">输入内容</param>
        /// <param name="fontColor">颜色</param>
        /// <param name="isNewLine">是否换行</param>
        public void myAddRtbStr(ref RichTextBox rtb, string strInput, Color fontColor, bool isNewLine)
        {
            int p1 = rtb.TextLength;
            if (isNewLine)
            {
                rtb.AppendText(strInput + "\n");  //保留每行的所有颜色。 //  rtb.Text += strInput + "/n";  //添加时，仅当前行有颜色。    
            }
            else
            {
                rtb.AppendText(strInput);
                //rtb.Text += strInput;
            }
            int p2 = strInput.Length;
            rtb.Select(p1, p2);
            rtb.SelectionColor = fontColor;
        }

        /// <summary>
        /// read and write ini file
        /// </summary>
        class myini
        {
            [DllImport("kernel32")]
            private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
            [DllImport("kernel32")]
            private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

            public static void IniWriteValue(string Section, string Key, string Value, string filepath)//对ini文件进行写操作的函数
            {
                try
                {
                    WritePrivateProfileString(Section, Key, Value, filepath);
                }
                catch (Exception ex)
                {
                    ErrorLog.PutInLog("ID:D314  " + ex.Message);
                }
            }

            public static string IniReadValue(string Section, string Key, string filepath)//对ini文件进行读操作的函数
            {
                StringBuilder temp = new StringBuilder(255);
                try
                {
                    int i = GetPrivateProfileString(Section, Key, "", temp, 255, filepath);
                }
                catch (Exception ex)
                {
                    ErrorLog.PutInLog("ID:D327  " + ex.Message);
                }
                return temp.ToString();
            }


        }

        #endregion

        #region 预备函数

        public void loadIni()
        {
            myReceiveData.vaneApp_key = myini.IniReadValue("vaneinterface", "app_key", System.Environment.CurrentDirectory + "\\seting\\seting.ini");
            myReceiveData.vaneApp_secret = myini.IniReadValue("vaneinterface", "app_secret", System.Environment.CurrentDirectory + "\\seting\\seting.ini");
            myReceiveData.vaneV = myini.IniReadValue("vaneinterface", "v", System.Environment.CurrentDirectory + "\\seting\\seting.ini");
            myReceiveData.vaneUrl = myini.IniReadValue("vaneinterface", "vaneUrl", System.Environment.CurrentDirectory + "\\seting\\seting.ini");
        }

        /// <summary>
        /// i will Send Data
        /// </summary>
        /// <param name="url"> url </param>
        /// <param name="data"> param </param>
        /// <param name="method">GET/POST</param>
        /// <returns>back </returns>
        public static string SendData(string url, string data, string method)
        {
            string re = "";
            try
            {
                WebRequest wr = WebRequest.Create(url);
                wr.Method = method;
                wr.ContentType = "application/x-www-form-urlencoded";
                char[] reserved = { '?', '=', '&' };
                StringBuilder UrlEncoded = new StringBuilder();
                byte[] SomeBytes = null;
                if (data != null && method != "GET")
                {
                    SomeBytes = Encoding.UTF8.GetBytes(data);
                    wr.ContentLength = SomeBytes.Length;
                    Stream newStream = wr.GetRequestStream();
                    newStream.Write(SomeBytes, 0, SomeBytes.Length);
                    newStream.Close();
                }
                else
                {
                    wr.ContentLength = 0;
                }


                WebResponse result = wr.GetResponse();
                Stream ReceiveStream = result.GetResponseStream();

                Byte[] read = new Byte[512];
                int bytes = ReceiveStream.Read(read, 0, 512);

                re = "";
                while (bytes > 0)
                {
                    Encoding encode = System.Text.Encoding.GetEncoding("UTF-8");
                    re += encode.GetString(read, 0, bytes);
                    bytes = ReceiveStream.Read(read, 0, 512);
                }
            }
            catch (Exception ex)
            {
                re = ex.Message;
                ErrorLog.PutInLog("ID:0090 " + ex.InnerException);
            }
            return re;
        }

        /// <summary>
        /// i will Send Data
        /// </summary>
        /// <param name="url"> url </param>
        /// <param name="data"> param </param>
        /// <param name="method">GET/POST</param>
        /// <param name="myAht">the myAutoHttpTest will fill the data</param>
        /// <returns>back </returns>
        public static string SendData(string url, string data, string method ,ref myAutoHttpTest myAht)
        {
            string re = "";
            Stopwatch myWatch = new Stopwatch(); 
            try
            {
                //except POST other data will add the url,if you want adjust the ruleschange here
                if (method.ToUpper() != "POST")
                {
                    url += "?" + data;
                    data = null;
                }
                WebRequest wr = WebRequest.Create(url);
                wr.Method = method;
                wr.ContentType = "application/x-www-form-urlencoded";
                char[] reserved = { '?', '=', '&' };
                StringBuilder UrlEncoded = new StringBuilder();
                byte[] SomeBytes = null;
                myAht.startTime = DateTime.Now.ToString("HH:mm:ss");
                myWatch.Start();
                if (data != null)
                {
                    SomeBytes = Encoding.UTF8.GetBytes(data);
                    wr.ContentLength = SomeBytes.Length;
                    myWatch.Reset();
                    myWatch.Start();
                    Stream newStream = wr.GetRequestStream();
                    newStream.Write(SomeBytes, 0, SomeBytes.Length);
                    myWatch.Stop();
                    newStream.Close();
                }
                else
                {
                    wr.ContentLength = 0;
                }

                if (data == null)
                {
                    myWatch.Start();
                }
                WebResponse result = wr.GetResponse();
                if (data == null)
                {
                    myWatch.Stop();
                }

                Stream ReceiveStream = result.GetResponseStream();

                Byte[] read = new Byte[512];
                int bytes = ReceiveStream.Read(read, 0, 512);

                re = "";
                while (bytes > 0)
                {
                    Encoding encode = System.Text.Encoding.GetEncoding("UTF-8");
                    re += encode.GetString(read, 0, bytes);
                    bytes = ReceiveStream.Read(read, 0, 512);
                }
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        re += "StatusCode:  " + Convert.ToInt32(((HttpWebResponse)wex.Response).StatusCode) + "\r\n";
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            re += reader.ReadToEnd();
                        }
                    }
                }
                else
                {
                    re += "Unknow Error";
                }
            }
            catch (Exception ex)
            {
                re ="Error : "+ ex.Message;
            }

            if (myWatch.IsRunning)
            {
                myWatch.Stop();
            }
            else
            {
                myAht.spanTime = myWatch.ElapsedMilliseconds.ToString();
            }
            myAht.result = re;
            return re;
        }

        /// <summary>
        /// load the case file in the ui
        /// </summary>
        /// <param name="myCasePath">file path</param>
        public void LoadTreeView(string myCasePath)
        {
            if (isRunCase)
            {
                MessageBox.Show("The TestCase is Runing", "stop");
                return;
            }
            if (!myCase.LoadFile(myCasePath))
            {
                MessageBox.Show("该脚本数据格式错误，请修正错误。详情请查看错误日志", "STOP");
                return;
            }
            XmlNodeList myCaseProject = myCase.xml.ChildNodes[1].ChildNodes;
            tvw_Case.Nodes.Clear();
            try
            {
                foreach (XmlNode tempNode in myCaseProject)
                {
                    mycaseProjects.Add(new caseProject(tempNode.Attributes[0].Value, tempNode));
                    TreeNode tempTree = new TreeNode(mycaseProjects[mycaseProjects.Count - 1].name, 0, 0);
                    foreach (XmlNode tempChildNode in mycaseProjects[mycaseProjects.Count - 1].myXmlNode.ChildNodes)
                    {
                        //tempTree.Nodes.Add("TestCase", myDataAnalysis.myStringAddEx(tempChildNode.Attributes[0].Value,tempChildNode.ChildNodes[2].InnerText), 1, 2);
                        TreeNode tempChildTreeNpde = new TreeNode(myDataAnalysis.myStringAdd("ID:"+tempChildNode.Attributes[0].Value, tempChildNode.ChildNodes[2].InnerText,15));
                        if (tempChildNode.ChildNodes[0].InnerText == "StartRepeat")
                        {
                            tempChildTreeNpde.ImageIndex = 9;
                            tempChildTreeNpde.SelectedImageIndex = 9;
                        }
                        else if (tempChildNode.ChildNodes[0].InnerText == "EndRepeat")
                        {
                            tempChildTreeNpde.ImageIndex = 10;
                            tempChildTreeNpde.SelectedImageIndex = 10;
                        }
                        else
                        {
                            tempChildTreeNpde.ImageIndex = 1;
                            tempChildTreeNpde.SelectedImageIndex = 1;
                        }

                        tempChildTreeNpde.Tag = tempChildNode;
                        tempTree.Nodes.Add(tempChildTreeNpde);
                    }
                    tempTree.Tag = tempNode;
                    tvw_Case.Nodes.Add(tempTree);
                }
            }
            catch (Exception ex)
            {
                int tempErrorIndex1 = 0;
                int tempErrorIndex2 = 0;
                if (tvw_Case.Nodes.Count != 0)
                {
                    tempErrorIndex1 = tvw_Case.Nodes.Count;
                    tempErrorIndex2 = tvw_Case.Nodes[tempErrorIndex1 - 1].Nodes.Count;
                }
                MessageBox.Show("用例脚本错误，请检查\n详见错误日志\n错误代码：" + tempErrorIndex1.ToString() + "X" + (tempErrorIndex2).ToString(), "STOP");
                tvw_Case.Nodes.Clear();
                ErrorLog.PutInLog("ID:0527  " + ex.Message);
                ErrorLog.PutInLog("脚本错误位置  ：第" + tempErrorIndex1 + "个工程，第" + (tempErrorIndex2) + "个用例");
            }

            #region 清除侧边栏信息
            //showNode = null;
            //label_caseInfo4.Text = "Data1";
            //label_caseInfo5.Text = "Data2";
            //label_caseInfo6.Text = "Data1";
            //label_caseInfo7.Text = "Data1";
            //label_caseInfo8.Text = "Data1";
            //label_caseInfo9.Text = "Long";
            //textBox_id.Text = "";
            //textBox_name.Text = "";
            //textBox_time.Text = "";
            //textBox_long.Text = "";
            //textBox_data1.Text = "";
            //textBox_data2.Text = "";
            //textBox_data3.Text = "";
            //textBox_data4.Text = "";
            //textBox_data5.Text = "";
            #endregion

        }

        /// <summary>
        /// 准备执行
        /// </summary>
        /// <returns>是否可以进行</returns>
        public bool readyRun()
        {
            //检查网络状态 可能有强制执行点需求 不放在此处检查
            //if (!comm_1.IsOpen)
            //{
            //    MessageBox.Show("未连接到终端", "can not run");
            //    stopRun();
            //    return false;
            //}
            if (isRunCase || isRunTest)
            {
                MessageBox.Show("发现未结束的任务", "can not run");
                stopRun();
                return false;
            }
            if (tvw_Case.SelectedNode == null)
            {
                MessageBox.Show("未选择任何项目", "can not run");
                stopRun();
                return false;
            }
            startTreeNode = tvw_Case.SelectedNode;

            repeatTime = 0;
            repeatStartIndex = 0;
            isCatchEnd = false;

            if (startTreeNode.Parent == null)
            {
                //do something
                startTreeNode.Toggle();
                parentsTree = startTreeNode;
                startTreeNode = parentsTree.Nodes[0];
                startXmlNode = (System.Xml.XmlLinkedNode)(startTreeNode.Tag);
                parentsXml = startXmlNode.ParentNode;
                nowIndex = 0;
                nowCount = startXmlNode.ParentNode.ChildNodes.Count;
                endIndex = nowCount - 1;
                //startTreeNode.ImageIndex = 8;
                //startTreeNode.SelectedImageIndex = 8;
                //timerCount = 0;
                //timerFlag = 0;
                //myCasetimer.Enabled = true;
                return true;
            }
            startXmlNode = (System.Xml.XmlLinkedNode)(tvw_Case.SelectedNode.Tag);
            parentsTree = startTreeNode.Parent;
            parentsXml = startXmlNode.ParentNode;
            nowIndex = startTreeNode.Index;
            nowCount = startXmlNode.ParentNode.ChildNodes.Count;
            endIndex = nowCount - 1;
            //startTreeNode.ImageIndex = 8;
            //startTreeNode.SelectedImageIndex = 8;
            //timerCount = 0;
            //timerFlag = 0;
            //myCasetimer.Enabled = true;
            //isRead90 = true;
            //isRead93 = false;
            return true;
        }

        public void doTest()
        {
            string myCaseToSend = "";
            myAutoHttpTest myReportData = new myAutoHttpTest("null");
            if (!isRunCase)
            {
                MessageBox.Show("项目已停止", "run stop");
                return;
            }
            string myTarget = myReceiveData.vaneUrl + parentsXml.ChildNodes[nowIndex].Attributes[1].Value;
            parentsTree.Nodes[showIcoIndex].ImageIndex = 5;
            parentsTree.Nodes[showIcoIndex].SelectedImageIndex = 5;
            myReportData.caseId = parentsXml.ChildNodes[nowIndex].Attributes[0].Value;
            myCaseToSend = myDataAnalysis.myCreatSendData(parentsXml.ChildNodes[nowIndex].ChildNodes[2].InnerText);
            //check the data
            if (myCaseToSend == "can't find =")
            {
                myReportData.startTime = DateTime.Now.ToString("HH:mm:ss");
                myReportData.result = "skip ; 未发送";
                myReportData.remark = "用例数据解析失败";
                myReportData.ret = "break";
                parentsTree.Nodes[showIcoIndex].ImageIndex = 13;
                parentsTree.Nodes[showIcoIndex].SelectedImageIndex = 13;
                myReceiveData.myData.Add(myReportData);
                return;
            }
            //here i will send the data
            SendData(myTarget, myCaseToSend, parentsXml.ChildNodes[nowIndex].ChildNodes[4].InnerText, ref myReportData);
            if (myReportData.result.StartsWith("Unknow"))
            {
                myReportData.remark = "未知错误";
                myReportData.ret = "unknow";
                parentsTree.Nodes[showIcoIndex].ImageIndex = 12;
                parentsTree.Nodes[showIcoIndex].SelectedImageIndex = 12;
                myReceiveData.myData.Add(myReportData);
                return;
            }
            if (myReportData.result.StartsWith("Error"))
            {
                myReportData.remark = "请求发送模块异常";
                myReportData.ret = "error";
                parentsTree.Nodes[showIcoIndex].ImageIndex = 14;
                parentsTree.Nodes[showIcoIndex].SelectedImageIndex = 14;
                myReceiveData.myData.Add(myReportData);
                return;
            }

            //now i am sure the request is ok
            if (myReportData.result == parentsXml.ChildNodes[nowIndex].ChildNodes[3].InnerText)
            {
                myReportData.ret = "pass";
                myReportData.remark = "测试通过";
                parentsTree.Nodes[showIcoIndex].ImageIndex = 15;
                parentsTree.Nodes[showIcoIndex].SelectedImageIndex = 15;
            }
            else
            {
                myReportData.ret = "fail";
                myReportData.remark = "预期值:"+parentsXml.ChildNodes[nowIndex].ChildNodes[3].InnerText;
                parentsTree.Nodes[showIcoIndex].ImageIndex = 7;
                parentsTree.Nodes[showIcoIndex].SelectedImageIndex = 7;
            }
            //fill the result here
            myReceiveData.myData.Add(myReportData);

        }

        /// <summary>
        /// 终止执行
        /// </summary>
        public void stopRun()
        {
            isRunCase = false;
            isRunTest = false;
            //myCasetimer.Enabled = false;
            isRunAll = false;
            repeatTime = 0;
            repeatStartIndex = 0;
            isCatchEnd = false;
        }

        Thread myThread;
        public void runTest()
        {
            myThread = new Thread(new ThreadStart(newHttpTest));
            myThread.IsBackground = true;
            myThread.Start();
        }

        public void newHttpTest()
        {
            //网络检查
            startTreeNode.ImageIndex = 8;
            startTreeNode.SelectedImageIndex = 8;
            if (!PingIpOrDomainName(myReceiveData.vaneUrl))
            {
                if (MessageBox.Show("您的网络可能无法连接目标主机,是否放弃本次任务", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    startTreeNode.ImageIndex = 1;
                    startTreeNode.SelectedImageIndex = 1;
                    stopRun();
                    return;
                }
            }

            while (isRunCase)
            {
                if (nowIndex > endIndex)
                {
                    if (isRunAll)
                    { 
                        if (parentsTree.NextNode == null)
                        {
                            stopRun();
                            MessageBox.Show("执行完成", "info");
                            return;
                        }
                        tvw_Case.SelectedNode = parentsTree.NextNode.Nodes[0];
                        readyRun();
                        isRunCase = true;
                    }
                    else
                    {
                        stopRun();
                        MessageBox.Show("执行完成", "info");
                        return;
                    }
                }

                else
                {
                    #region 是否需要循环处理
                    if (repeatTime == 0)
                    {
                        if (parentsXml.ChildNodes[nowIndex].ChildNodes[0].InnerText == "StartRepeat")
                        {
                            repeatStartIndex = nowIndex;
                            try
                            {
                                repeatTime = int.Parse(parentsXml.ChildNodes[nowIndex].ChildNodes[1].InnerText);
                                if (repeatTime <= 0)
                                {
                                    repeatStartIndex = 0;
                                    repeatTime = 0;
                                    MessageBox.Show("用例脚本错误，已强制更正，错误明细请查看错误日志", "Tip");
                                }
                            }
                            catch
                            {
                                repeatStartIndex = 0;
                                repeatTime = 0;
                                MessageBox.Show("用例脚本错误，已强制更正，错误明细请查看错误日志", "Tip");
                            }
                        }
                    }
                    else
                    {
                        if (parentsXml.ChildNodes[nowIndex].ChildNodes[0].InnerText == "EndRepeat")
                        {
                            if (repeatTime > 0)
                            {
                                isCatchEnd = true;
                            }
                            else
                            {
                                MessageBox.Show("用例脚本错误，已强制更正，错误明细请查看错误日志", "Tip");
                            }
                            
                        }
                    }
                    #endregion

                    //
                    Thread.Sleep(sleepTime);
                    showIcoIndex = nowIndex;
                    doTest();
                    if (isCatchEnd)
                    {
                        if (repeatTime > 1)
                        {
                            repeatTime--;
                            int tempEnd=nowIndex;
                            nowIndex = repeatStartIndex;
                            for (int i = nowIndex; i <= tempEnd; i++)
                            {
                                parentsTree.Nodes[i].ImageIndex = 1;
                                parentsTree.Nodes[i].SelectedImageIndex = 1;
                            }
                        }
                        else
                        {
                            repeatStartIndex = 0;
                            repeatTime = 0;
                        }
                        isCatchEnd = false;
                    }
                    else
                    {
                        nowIndex++;
                    }
                }

            }
        }

        #endregion

        //form load 
        private void AutoTestForVane_Load(object sender, EventArgs e)
        {
            //启动数据呈现
            lb_msg1.Text = "none case file ";
            lb_msg2.Text = "none tasck";
            //载入配置文件
            loadIni();
            isFistLoad = false;
            //启动ui计时器
            timer_show.Enabled = true;
        }

        //close
        private void AutoTestForVane_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myThread == null)
            {
                return;
            }
            else if (myThread.ThreadState != System.Threading.ThreadState.Stopped)
            {
                if (MessageBox.Show("执行模块正在运行，是否强制关闭？", "Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    //e.Cancel();
                }
                myThread.Abort();
            }
        }

        //UI计时器
        private void timer_show_Tick(object sender, EventArgs e)
        {
            while (dataListNum < myReceiveData.myData.Count)
            {
                dataListNum++;
                listView_DataAdd.Items.Add(new ListViewItem(new string[] { myReceiveData.myData[dataListNum - 1].caseId, myReceiveData.myData[dataListNum - 1].startTime, myReceiveData.myData[dataListNum - 1].spanTime, myReceiveData.myData[dataListNum - 1].ret, myReceiveData.myData[dataListNum - 1].result, myReceiveData.myData[dataListNum - 1].remark}));
            }
        }

        //UI调整
        private void AutoTestForVane_Resize(object sender, EventArgs e)
        {
            if (isFistLoad)
            {
                return;
            }
            //tvw_Case Resize
            tvw_Case.Height = this.Height - 167;
            tvw_Case.Width = this.Width - 5;

            lb_msg1.Location = new Point(lb_msg1.Location.X, this.Height - 74);
            lb_msg2.Location = new Point(lb_msg2.Location.X, this.Height - 74);
            progressBar_case.Location = new Point(progressBar_case.Location.X, this.Height - 77);
        }


        private void test_Click(object sender, EventArgs e)
        {
            LoadTreeView(System.Environment.CurrentDirectory + "\\casefile\\DebugCase.xml");
        }


        //使右键也具有选中功能
        private void tvw_Case_Click(object sender, EventArgs e)
        {
            //右键添加选中功能
            try
            {
                if (((System.Windows.Forms.MouseEventArgs)(e)).Button == MouseButtons.Right)
                {
                    TreeNode tn = tvw_Case.GetNodeAt(((System.Windows.Forms.MouseEventArgs)(e)).X, ((System.Windows.Forms.MouseEventArgs)(e)).Y);
                    if (tn != null)
                    {
                        tvw_Case.SelectedNode = tn;
                        tvw_Case.ContextMenuStrip = contextMenuStrip_CaseTree;
                    }
                    else
                    {
                        tvw_Case.ContextMenuStrip = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ErrorLog.PutInLog("ID:0001  " + ex.Message);
            }
        }



        #region MenuStripEven

        //从此处开始执行
        private void RunHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isRunTest)
            {
                MessageBox.Show("测试还未结束", "stop");
                return;
            }
            if (isRunCase)
            {
                MessageBox.Show("上一个任务还未执行完成", "stop");
                return;
            }
            if (!readyRun())
            {
                return;
            }
            isRunCase = true;
            runTest();
        }

        //执行所有下文
        private void runAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isRunTest)
            {
                MessageBox.Show("测试还未结束", "stop");
                return;
            }
            if (isRunCase)
            {
                MessageBox.Show("上一个任务还未执行完成", "stop");
                return;
            }
            if (!readyRun())
            {
                return;
            }
            isRunAll = true;
            isRunCase = true;
            runTest();
        }

        //停止执行
        private void stopRunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stopRun();
        }

        #endregion

        #region DataBackWindow

        private void pictureBox_dataAddClose_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_dataAddSave_Click(object sender, EventArgs e)
        {
            //no data to save
            if (dataListNum == 0)
            {
                MessageBox.Show("没有发现可用返回数据", "Stop");
                return;
            }

            //save data 
            string tempFilePathForBack = System.Windows.Forms.Application.StartupPath + "\\testResult\\" + DateTime.Now.ToString("yyyy.MM.dd") + ".ini";
            try
            {
                if (!File.Exists(tempFilePathForBack))
                {
                    FileStream fs = new FileStream(tempFilePathForBack, FileMode.Create, FileAccess.Write);//创建写入文件 
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(DateTime.Now.ToString());
                    sw.WriteLine("Start");
                    foreach (myAutoHttpTest tempVal in myReceiveData.myData)
                    {
                        sw.WriteLine(tempVal.caseId + "#" + tempVal.startTime + "#" + tempVal.spanTime + "#" + tempVal.ret + "#" + tempVal.result + "#" + tempVal.remark);
                    }
                    sw.WriteLine("End");
                    sw.WriteLine(" ");
                    sw.Close();
                    fs.Close();
                    MessageBox.Show("保存成功，请到测试结果文件中查看", "Sucess");
                }
                else
                {
                    FileStream fs = new FileStream(tempFilePathForBack, FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                    sw.WriteLine(DateTime.Now.ToString());
                    sw.WriteLine("Start");
                    foreach (myAutoHttpTest tempVal in myReceiveData.myData)
                    {
                        sw.WriteLine(tempVal.caseId + "#" + tempVal.startTime + "#" + tempVal.spanTime + "#" + tempVal.ret + "#" + tempVal.result + "#" + tempVal.remark);
                    }
                    sw.WriteLine("End");
                    sw.WriteLine(" ");
                    sw.Close();
                    fs.Close();
                    MessageBox.Show("追加保存成功，请到测试结果文件中查看", "Sucess");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存文件错误，详情请查看错误日志", "Stop");
                ErrorLog.PutInLog("ID:1940  " + ex.Message);
            }
        }

        private void pictureBox_dataAddclean_Click(object sender, EventArgs e)
        {
            //this time if the DataReceive is open it will has an erorr
            myReceiveData.myData.Clear();
            dataListNum = 0;
            listView_DataAdd.Items.Clear();
        }

        #region expandablePanel_dataAdd move

        bool isCanMove = false;
        Point myOriginPoint = new Point(0, 0);
        Point myOriginLocation = new Point(0, 0);
        int myControHight = 0;
        int myControWidth = 0;

        private void label_moveFlagForDataAdd_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left))
            {
                isCanMove = true;
                myOriginPoint = Control.MousePosition;
                myOriginLocation = new Point(expandablePanel_dataAdd.Location.X, expandablePanel_dataAdd.Location.Y);
            }
        }

        private void label_moveFlagForDataAdd_MouseMove(object sender, MouseEventArgs e)
        {
            if (isCanMove)
            {
                Point tempMousePosition = new Point(Control.MousePosition.X - myOriginPoint.X, Control.MousePosition.Y - myOriginPoint.Y);
                isCanMove = false;
                expandablePanel_dataAdd.Location = new Point(myOriginLocation.X + tempMousePosition.X, myOriginLocation.Y + tempMousePosition.Y);
                expandablePanel_dataAdd.Update();
                isCanMove = true;
            }
        }

        private void label_moveFlagForDataAdd_MouseUp(object sender, MouseEventArgs e)
        {
            isCanMove = false;
        }
        #endregion

        #region changeDataAddSize
        private void pictureBox_changeDataAddSize_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left))
            {
                isCanMove = true;
                myOriginPoint = Control.MousePosition;
                myControHight = expandablePanel_dataAdd.Height;
                myControWidth = expandablePanel_dataAdd.Width;
            }
        }

        private void pictureBox_changeDataAddSize_MouseMove(object sender, MouseEventArgs e)
        {
            if (isCanMove)
            {
                Point tempMousePosition = new Point(Control.MousePosition.X - myOriginPoint.X, Control.MousePosition.Y - myOriginPoint.Y);
                isCanMove = false;
                expandablePanel_dataAdd.Height = myControHight + tempMousePosition.Y;
                expandablePanel_dataAdd.Width = myControWidth + tempMousePosition.X;
                label_moveFlagForDataAdd.Width = expandablePanel_dataAdd.Width - 28;
                expandablePanel_dataAdd.Update();
                isCanMove = true;
            }
        }

        private void pictureBox_changeDataAddSize_MouseUp(object sender, MouseEventArgs e)
        {
            isCanMove = false;
        }

        #endregion


        #endregion
        

    }
}
