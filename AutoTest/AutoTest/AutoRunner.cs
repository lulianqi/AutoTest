using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AutoTest.myTool;
using System.Xml;
using System.Net;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net.NetworkInformation;
using AutoTest.myDialogWindow;
using System.Collections;
using MyCommonTool;
using CaseExecutiveActuator;
using CaseExecutiveActuator.Cell;
using CaseExecutiveActuator.CaseMefHelper;


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
    public partial class AutoRunner : Form
    {
        public AutoRunner()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;                                    //自行控制ui线程安全
        }

        #region basicDefined
        string nowErrorMessage="";                                                              //当前任务的错误消息（请在主窗体任务发生错误时读取,务必不要在其他线程修改该数据）
        //int dataListNum = 0;                                                                    //当前反馈数据集条数
        string casePath = System.Environment.CurrentDirectory + "\\casefile\\case1.xml";        //默认脚本路径

        bool isShowCaseContent = true;                                                          //是否在脚本呈现界面显示case详情

        public string startPath = System.Windows.Forms.Application.StartupPath;                 //应用程序启动目录
        bool isFistLoad = true;                                                                 //是否为首次加载

        public int sleepTime = 100;                                                             //单次睡眠时间
        public int SleepTime
        {
            get { return this.sleepTime; }
            set { this.sleepTime = value; }
        }
       
        public int maxLine = 0;                                                                 //最大显示行
        public int MaxLine
        {
            get { return this.maxLine; }
            set { this.maxLine = value; }
        }
        public int postDataDes = 0;                                                             //post请求中body位置，0/1 ： body/Head
        public int PostDataDes
        {
            get { return this.postDataDes; }
            set { this.postDataDes = value; }
        }

        public string myReportPath = "";                                                        //指定报告输出
        bool isShowMessage = false;                                                             //是否正在显示提示信息
        int ShowMessageTime = 4;                                                                //显示的时间
        #endregion

        #region oriented Defined

        public System.Windows.Forms.Timer myCasetimer = new System.Windows.Forms.Timer();       //运行状态计数器                   ***重构考虑移除
        public CaseActionActuator nowCaseActionActuator;                                        //用于执行及处理基础case单元的执行器
        public CaseFileXmlAnalysis myCase = new CaseFileXmlAnalysis();                          //用例数据处理集,将向外提供
        List<caseProject> myCaseProjects = new List<caseProject>();                             //树中所有工程集合

        public XmlNode showNode;                                                                //正在显示的xml节点,将对外提供      ***重构考虑移除

        Hashtable checkDataHt=new Hashtable();                                                  //脚本检查数据源   
        Dictionary<string, string> runInfoMessage;                                              //运行时错误或提示

        public Dictionary<string, string> RunInfoMessage
        {
            get 
            {
                return this.runInfoMessage; 
            }
        }

        public myRunCount nowProjectRunCount;                                                   //运行时数量统计                       

        public bool pictureBox_dataAddStopTag = true;                                           //图片标记-返回数据底部跟随     

        Action<myExecutionDeviceResult> AddExecutiveResult ;                                    //结果添加委托
        Action<string> AddExecutiveRecord;                                                      //过程添加委托
        #endregion

        #region myfunction

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
            rtb.SelectionColor = fontColor;
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
        /// 添加记录
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="vaule">内容</param>
        /// <param name="myRtb">目标richtextbox</param>
        public void myAddRecord(string name, string vaule, ref RichTextBox myRtb)
        {
            if (myRtb.Lines.Length > maxLine)
            {
                myRtb.Clear();
            }
            else
            {
                myAddRtbStr(ref myRtb, name, Color.Blue, true);
                myAddRtbStr(ref myRtb, vaule, Color.Black, true);
            }
        }

        /// <summary>
        /// 设置Tree节点图标索引
        /// </summary>
        /// <param name="yourNode">脚本节点</param>
        /// <param name="yourImageIndex">索引</param>
        public void setNodeImageIndex(TreeNode yourNode,int yourImageIndex)
        {
            if (yourNode != null)
            {
                yourNode.ImageIndex = yourImageIndex;
                yourNode.SelectedImageIndex = yourImageIndex;
            }
        }

        /// <summary>
        /// 设置脚本图标
        /// </summary>
        /// <param name="yourNode">脚本节点</param>
        public void mySetNodeReadRun(TreeNode yourNode)
        {
            setNodeImageIndex(yourNode,5);
        }

        /// <summary>
        /// 设置脚本图标
        /// </summary>
        /// <param name="yourNode">脚本节点</param>
        public void mySetNodeRunOk(TreeNode yourNode)
        {
            setNodeImageIndex(yourNode, 5);
        }

        #endregion

        #region 预备函数
        //填充脚本检查数据源
        public void fillCheckDataHashtable()
        {
            checkDataHt.Add("repeat", new string[] { "", "StartRepeat", "EndRepeat" });
            checkDataHt.Add("@method", new string[] { "", "is", "not", "like", "endwith", "startwith", "contain", "uncontain", "null", "default" });
            checkDataHt.Add("@HttpMethod", new string[] { "", "OPTIONS", "HEAD", "GET", "POST", "PUT", "DELETE", "TRACE", "CONNECT" });
        }

        //设置初始值
        public void setInitialValue()
        {
            //启动数据呈现
            lb_msg1.Text = "please load case file ";
            lb_msg2.Text = "none tasck";
            lb_msg3.Text = "";
            lb_msg4.Text = "";
            lb_msg5.Text = "";
            //环境变量
            runInfoMessage = new Dictionary<string, string>();
            nowProjectRunCount = new myRunCount();
            //内置委托
            AddExecutiveResult = new Action<myExecutionDeviceResult>(AddExecutiveResultToLvDataAdd);
            AddExecutiveRecord = new Action<string>(AddExecutiveDataToRecord);
            //UI初始位置
            myShareData.sdExpandablePanel_dataAdd_Position = expandablePanel_dataAdd.Location;
            myShareData.sdExpandablePanel_testMode_Position = expandablePanel_testMode.Location;
        }

        //加载配置文件
        public void loadIni()
        {
            myReceiveData.vaneApp_key = myini.IniReadValue("vaneinterface", "app_key", System.Environment.CurrentDirectory + "\\seting\\seting.ini");
            myReceiveData.vaneApp_secret = myini.IniReadValue("vaneinterface", "app_secret", System.Environment.CurrentDirectory + "\\seting\\seting.ini");
            myReceiveData.vaneV = myini.IniReadValue("vaneinterface", "v", System.Environment.CurrentDirectory + "\\seting\\seting.ini");
            myReceiveData.vaneUrl = myini.IniReadValue("vaneinterface", "vaneUrl", System.Environment.CurrentDirectory + "\\seting\\seting.ini");

            tb_caseFilePath.Text = myini.IniReadValue("casepath", "defaultpath", System.Environment.CurrentDirectory + "\\seting\\seting.ini");

            tb_tryTestData.Text = myini.IniReadValue("vane", "trytest", System.Environment.CurrentDirectory + "\\seting\\seting.ini");
            tb_caseFilePath.Text = myini.IniReadValue("vane", "filepath", System.Environment.CurrentDirectory + "\\seting\\seting.ini");

            
            try
            {
                maxLine = int.Parse(myini.IniReadValue("vane", "maxline", System.Environment.CurrentDirectory + "\\seting\\seting.ini"));
                sleepTime = int.Parse(myini.IniReadValue("vane", "sleeptime", System.Environment.CurrentDirectory + "\\seting\\seting.ini"));
                postDataDes = int.Parse(myini.IniReadValue("postsetting", "body", System.Environment.CurrentDirectory + "\\seting\\seting.ini"));
            }
            catch(Exception ex)
            {
                maxLine = 100;
                sleepTime = 100;
                ErrorLog.PutInLog("ID:0240  " + ex.Message);
                ShowMessage(ex.Message);
            }
        }

        /// <summary>
        /// i will show message in trb_addRecord
        /// </summary>
        /// <param name="myMessage">the message</param>
        public void ShowRunStatus(string myMessage)
        {
            trb_addRecord.AddDate(myMessage, Color.LightSlateGray, true);
        }

        /// <summary>
        /// i will show message in Panel
        /// </summary>
        /// <param name="myMessage">the message</param>
        public void ShowMessage(string myMessage)
        {
            runInfoMessage.MyAddEx(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff"), myMessage);
            //richTextBox_showMessage.Visible = true;
            richTextBox_showMessage.Text = myMessage;
            expandablePanel_messageBox.Expanded = true;
            isShowMessage = true;
            ShowMessageTime = 5;
        }

        /// <summary>
        /// i will show message in Panel
        /// </summary>
        /// <param name="myMessage">the message</param>
        /// <param name="myTitle">your Title</param>
        public void ShowMessage(string myMessage,string myTitle)
        {
            myMessage = "【" + myTitle + "】" + "\r\n" + myMessage;
            ShowMessage(myMessage);
        }

        /// <summary>
        /// i will add progressBar with the tempVaule
        /// </summary>
        public void progressBarRefresh()
        {
            if(nowCaseActionActuator!=null)
            {
                progressBar_case.UpdateListMinimal(nowCaseActionActuator.RunProgress);
            }
        }

        /// <summary>
        /// load the case file in the ui
        /// </summary>
        /// <param name="myCasePath">file path</param>
        public void LoadTreeView(string myCasePath)
        {
            if (GetMainTaskRunState()!= CaseActuatorState.Stop)
            {
                MessageBox.Show("The TestCase not stop", "stop");
                return;
            }
            if (!File.Exists(myCasePath))
            {
                MessageBox.Show("用例文件不存在，请重新选择", "stop");
                return;
            }
            if (!myCase.LoadFile(myCasePath))
            {
                MessageBox.Show("该脚本数据格式错误，请修正错误。详情请查看错误日志", "STOP");
                return;
            }

            XmlNodeList myCaseProject = myCase.xml.ChildNodes[1].ChildNodes;

            #region check case data
            foreach (DictionaryEntry de in checkDataHt)
            {
                string tempStr = CaseTool.CheckCase(myCase.xml.ChildNodes[1], (string)de.Key, (string[])de.Value);
                if (tempStr != "")
                {
                    if (MessageBox.Show("加载异常：" + tempStr + "\n是否放弃加载", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        return;
                    }
                }
            }
            #endregion

            tvw_Case.Nodes.Clear();
            myCaseProjects.Clear();
            Dictionary<int, Dictionary<int, TreeNode>> myProjectCaseDictionary = new Dictionary<int, Dictionary<int, TreeNode>>();
            if(nowCaseActionActuator != null)
            {
                nowCaseActionActuator.DisconnectExecutionDevice();
                nowCaseActionActuator.OnGetActionError -= nowCaseActionActuator_OnGetErrorData;
                nowCaseActionActuator.OnGetExecutiveData -= nowCaseActionActuator_OnGetExecutiveData;
                nowCaseActionActuator.OnActuatorStateChanged -= nowCaseActionActuator_OnActuatorStateChanged;
                nowCaseActionActuator.OnExecutiveResult -= nowCaseActionActuator_OnExecutiveResult;
                nowCaseActionActuator.Dispose();
                nowCaseActionActuator = null;
            }
            try 
            {

                #region Project analyze 
                foreach (XmlNode tempNode in myCaseProject)
                {
                    myCaseLaodInfo tempProjectLoadInfo = myCaseScriptAnalysisEngine.getCaseLoadInfo(tempNode);
                    string thisErrorTitle = "Project ID:" + tempProjectLoadInfo.id;
                    if (tempProjectLoadInfo.ErrorMessage != "")
                    {
                        ShowMessage(tempProjectLoadInfo.ErrorMessage, thisErrorTitle);
                    }
                    if (tempProjectLoadInfo.caseType == CaseType.ScriptRunTime)
                    {
                        //deal with ScriptRunTime
                        if (nowCaseActionActuator == null)
                        {
                            nowCaseActionActuator = new CaseActionActuator();
                            nowCaseActionActuator.OnGetActionError += nowCaseActionActuator_OnGetErrorData;
                            nowCaseActionActuator.OnGetExecutiveData += nowCaseActionActuator_OnGetExecutiveData;
                            nowCaseActionActuator.OnActuatorStateChanged += nowCaseActionActuator_OnActuatorStateChanged;
                            nowCaseActionActuator.OnExecutiveResult += nowCaseActionActuator_OnExecutiveResult;
                            nowCaseActionActuator.LoadScriptRunTime(tempNode);
                        }
                        else
                        {
                            thisErrorTitle = "ScriptRunTime";
                            ShowMessage("find another ScriptRunTime ,ScriptRunTime is unique so it will be skip", thisErrorTitle);
                        }
                        continue;
                    }
                    if (tempProjectLoadInfo.caseType != CaseType.Project)
                    {
                        ShowMessage("not legal Project ,it will be skip");
                        continue;
                    }

                    myCaseProjects.Add(new caseProject(tempProjectLoadInfo.name, tempNode));
                    TreeNode tempTree = new TreeNode(myCaseProjects[myCaseProjects.Count - 1].name, 0, 0);
                    tempTree.Tag = new myTreeTagInfo(tempProjectLoadInfo.caseType, tempNode);
                    tvw_Case.Nodes.Add(tempTree);

                    #region deal this Project

                    Dictionary<int, TreeNode> tempCaseDictionary = new Dictionary<int, TreeNode>();
                    if (myProjectCaseDictionary.ContainsKey(tempProjectLoadInfo.id))
                    {
                        ShowMessage("find the same project id in this file ,it will make [Goto] abnormal", thisErrorTitle);
                    }
                    else
                    {
                        myProjectCaseDictionary.Add(tempProjectLoadInfo.id, tempCaseDictionary);
                    }
                    //myTargetCaseList 将包含当前project或repeat集合元素
                    List<KeyValuePair<TreeNode, XmlNode>> myTargetCaseList = new List<KeyValuePair<TreeNode, XmlNode>>();
                    myTargetCaseList.Add(new KeyValuePair<TreeNode, XmlNode>(tempTree, tempNode));
                    while (myTargetCaseList.Count > 0)
                    {
                        //Case analyze
                        foreach (XmlNode tempChildNode in myTargetCaseList[0].Value)
                        {
                            //load Show Info
                            myCaseLaodInfo tempCaseLoadInfo = myCaseScriptAnalysisEngine.getCaseLoadInfo(tempChildNode);
                            thisErrorTitle = "Case ID:" + tempCaseLoadInfo.id;
                            if (tempCaseLoadInfo.ErrorMessage != "")
                            {
                                ShowMessage(tempCaseLoadInfo.ErrorMessage);
                                ShowMessage("this error can not be repair so drop it", thisErrorTitle);
                            }
                            else
                            {
                                TreeNode tempChildTreeNode;
                                if (tempCaseLoadInfo.caseType == CaseType.Case)
                                {
                                    if (isShowCaseContent)
                                    {
                                        tempChildTreeNode = new TreeNode(myDataAnalysis.myStringAdd("ID:" + tempCaseLoadInfo.id, tempCaseLoadInfo.remark, 15) + " ● " + tempCaseLoadInfo.content);
                                    }
                                    else
                                    {
                                        tempChildTreeNode = new TreeNode(myDataAnalysis.myStringAdd("ID:" + tempCaseLoadInfo.id, tempCaseLoadInfo.remark, 15));
                                    }

                                    if (tempCaseLoadInfo.actions.Count > 0)
                                    {
                                        setNodeImageIndex(tempChildTreeNode, 16);
                                    }
                                    else
                                    {
                                        setNodeImageIndex(tempChildTreeNode, 1);
                                    }
                                    //load Run Data
                                    var tempCaseRunData = myCaseScriptAnalysisEngine.getCaseRunData(tempChildNode);
                                    if (tempCaseRunData.errorMessages!=null)
                                    {
                                        foreach(string tempErrorMes in tempCaseRunData.errorMessages)
                                        {
                                            ShowMessage(tempErrorMes, thisErrorTitle);
                                        }
                                        tempChildTreeNode.BackColor = Color.LightYellow;
                                    }
                                    tempChildTreeNode.Tag = new myTreeTagInfo(tempCaseLoadInfo.caseType, tempChildNode, tempCaseRunData);
                                    if (tempCaseDictionary.ContainsKey(tempCaseLoadInfo.id))
                                    {
                                        ShowMessage("find the same case id in this project ,it will make [Goto] abnormal", thisErrorTitle);
                                        tempChildTreeNode.BackColor = Color.LightYellow;
                                    }
                                    else
                                    {
                                        tempCaseDictionary.Add(tempCaseLoadInfo.id, tempChildTreeNode);
                                    }
                                    myTargetCaseList[0].Key.Nodes.Add(tempChildTreeNode);
                                }
                                else if (tempCaseLoadInfo.caseType == CaseType.Repeat)
                                {
                                    tempChildTreeNode = new TreeNode(myDataAnalysis.myStringAdd("Repeat:" + tempCaseLoadInfo.times, tempCaseLoadInfo.remark, 15));
                                    setNodeImageIndex(tempChildTreeNode, 9);
                                    tempChildTreeNode.Tag = new myTreeTagInfo(tempCaseLoadInfo.caseType, tempChildNode);
                                    myTargetCaseList[0].Key.Nodes.Add(tempChildTreeNode);

                                    myTargetCaseList.Add(new KeyValuePair<TreeNode, XmlNode>(tempChildTreeNode, tempChildNode));
                                }
                                else
                                {
                                    //it will cant be project and if it is unknow i will not show it
                                    ShowMessage("find unkown case so drop it", thisErrorTitle);
                                }
                            }

                        }
                            
                        myTargetCaseList.Remove(myTargetCaseList[0]);
                    }
                        
                    #endregion
                       
                }
                #endregion

                if (nowCaseActionActuator == null)
                {
                    tvw_Case.Enabled = false;
                    ShowMessage("ScriptRunTime is not find ");
                    MessageBox.Show("ScriptRunTime is not find ,the case will cannot run", "STOP");
                    lb_msg1.Text = "ScriptRunTime error";
                }
                else
                {
                    tvw_Case.Enabled = true;
                    //V2执行器中SetCaseRunTime使用的结构不一样，保留该函数仅为调试请不要直接使用
                    //nowCaseActionActuator.SetCaseRunTime(myProjectCaseDictionary);
                    //启动数据呈现
                    lb_msg1.Text = "case load sucess";
                }
    
            }
            //严重错误
            catch (Exception ex)
            {
                int tempErrorIndex1 = 0;
                int tempErrorIndex2 = 0;
                if (tvw_Case.Nodes.Count != 0)
                {
                    tempErrorIndex1 = tvw_Case.Nodes.Count;
                    tempErrorIndex2 = tvw_Case.Nodes[tempErrorIndex1 - 1].Nodes.Count;
                }
                MessageBox.Show(string.Format("用例脚本错误，请检查\n详见错误日志\n错误代码：{0}X{1}", tempErrorIndex1, tempErrorIndex2), "STOP");
                tvw_Case.Nodes.Clear();
                ErrorLog.PutInLog("ID:0527  " + ex.Message);
                ErrorLog.PutInLog("脚本错误位置  ：第" + tempErrorIndex1 + "个工程，第" + (tempErrorIndex2) + "个用例");
                //启动数据呈现
                lb_msg1.Text = "case file error";
                ShowMessage(ex.Message);
            }

        }

        public void LoadTreeViewEx(string myCasePath)
        {
            if (GetMainTaskRunState() != CaseActuatorState.Stop)
            {
                MessageBox.Show("The TestCase not stop", "stop");
                return;
            }
            if (!File.Exists(myCasePath))
            {
                MessageBox.Show("用例文件不存在，请重新选择", "stop");
                return;
            }
            if (!myCase.LoadFile(myCasePath))
            {
                MessageBox.Show("该脚本数据格式错误，请修正错误。详情请查看错误日志", "STOP");
                return;
            }

            XmlNodeList myCaseProject = myCase.xml.ChildNodes[1].ChildNodes;

            #region check case data
            foreach (DictionaryEntry de in checkDataHt)
            {
                string tempStr = CaseTool.CheckCase(myCase.xml.ChildNodes[1], (string)de.Key, (string[])de.Value);
                if (tempStr != "")
                {
                    if (MessageBox.Show("加载异常：" + tempStr + "\n是否放弃加载", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        return;
                    }
                }
            }
            #endregion

            Dictionary<int, Dictionary<int, CaseCell>> myProjectCaseDictionary = new Dictionary<int, Dictionary<int, CaseCell>>();

            if (nowCaseActionActuator != null)
            {
                nowCaseActionActuator.DisconnectExecutionDevice();
                nowCaseActionActuator.OnGetActionError -= nowCaseActionActuator_OnGetErrorData;
                nowCaseActionActuator.OnGetExecutiveData -= nowCaseActionActuator_OnGetExecutiveData;
                nowCaseActionActuator.OnActuatorStateChanged -= nowCaseActionActuator_OnActuatorStateChanged;
                nowCaseActionActuator.OnExecutiveResult -= nowCaseActionActuator_OnExecutiveResult;
                nowCaseActionActuator.Dispose();
                nowCaseActionActuator = null;
            }

            ProjctCollection myProjctCollection = new ProjctCollection();
            try
            {

                #region Project analyze

                foreach (XmlNode tempNode in myCaseProject)
                {
                    myCaseLaodInfo tempProjectLoadInfo = myCaseScriptAnalysisEngine.getCaseLoadInfo(tempNode);
                    string thisErrorTitle = "Project ID:" + tempProjectLoadInfo.id;
                    if (tempProjectLoadInfo.ErrorMessage != "")
                    {
                        ShowMessage(tempProjectLoadInfo.ErrorMessage, thisErrorTitle);
                    }
                    if (tempProjectLoadInfo.caseType == CaseType.ScriptRunTime)
                    {
                        //deal with ScriptRunTime
                        if (nowCaseActionActuator == null)
                        {
                            nowCaseActionActuator = new CaseActionActuator();
                            nowCaseActionActuator.OnGetActionError += nowCaseActionActuator_OnGetErrorData;
                            nowCaseActionActuator.OnGetExecutiveData += nowCaseActionActuator_OnGetExecutiveData;
                            nowCaseActionActuator.OnActuatorStateChanged += nowCaseActionActuator_OnActuatorStateChanged;
                            nowCaseActionActuator.OnExecutiveResult += nowCaseActionActuator_OnExecutiveResult;
                            nowCaseActionActuator.LoadScriptRunTime(tempNode);
                        }
                        else
                        {
                            thisErrorTitle = "ScriptRunTime";
                            ShowMessage("find another ScriptRunTime ,ScriptRunTime is unique so it will be skip", thisErrorTitle);
                        }
                        continue;
                    }
                    if (tempProjectLoadInfo.caseType != CaseType.Project)
                    {
                        ShowMessage("not legal Project ,it will be skip");
                        continue;
                    }

                    #region deal this Project

                    CaseCell tempProjctCell = new CaseCell(tempProjectLoadInfo.caseType,tempNode,null);
                    myProjctCollection.Add(tempProjctCell);
                   
                    Dictionary<int, CaseCell> tempCaseDictionary = new Dictionary<int, CaseCell>();
                    if (myProjectCaseDictionary.ContainsKey(tempProjectLoadInfo.id))
                    {
                        ShowMessage("find the same project id in this file ,it will make [Goto] abnormal", thisErrorTitle);
                    }
                    else
                    {
                        myProjectCaseDictionary.Add(tempProjectLoadInfo.id, tempCaseDictionary);
                    }

                    //myTargetCaseList 将包含当前project或repeat集合元素
                    List<KeyValuePair<CaseCell, XmlNode>> myTargetCaseList = new List<KeyValuePair<CaseCell, XmlNode>>();
                    myTargetCaseList.Add(new KeyValuePair<CaseCell, XmlNode>(tempProjctCell, tempNode));
                    while (myTargetCaseList.Count > 0)
                    {
                        //Case analyze
                        foreach (XmlNode tempChildNode in myTargetCaseList[0].Value)
                        {
                            //load Show Info
                            myCaseLaodInfo tempCaseLoadInfo = myCaseScriptAnalysisEngine.getCaseLoadInfo(tempChildNode);
                            thisErrorTitle = "Case ID:" + tempCaseLoadInfo.id;
                            if (tempCaseLoadInfo.ErrorMessage != "")
                            {
                                ShowMessage(tempCaseLoadInfo.ErrorMessage);
                                ShowMessage("this error can not be repair so drop it", thisErrorTitle);
                            }
                            else
                            {
                                if (tempCaseLoadInfo.caseType == CaseType.Case)
                                {
                                    //load Run Data
                                    var tempCaseRunData = myCaseScriptAnalysisEngine.getCaseRunData(tempChildNode);
                                    if (tempCaseRunData.errorMessages != null)
                                    {
                                        foreach (string tempErrorMes in tempCaseRunData.errorMessages)
                                        {
                                            ShowMessage(tempErrorMes, thisErrorTitle);
                                        }
                                    }
                                    CaseCell tempChildCell = new CaseCell(tempCaseLoadInfo.caseType, tempChildNode, tempCaseRunData);
                                    if (tempCaseDictionary.ContainsKey(tempCaseLoadInfo.id))
                                    {
                                        ShowMessage("find the same case id in this project ,it will make [Goto] abnormal", thisErrorTitle);
                                    }
                                    else
                                    {
                                        tempCaseDictionary.Add(tempCaseLoadInfo.id, tempChildCell);
                                    }
                                    myTargetCaseList[0].Key.Add(tempChildCell);
                                }
                                else if (tempCaseLoadInfo.caseType == CaseType.Repeat)
                                {
                                    CaseCell tempChildCell = new CaseCell(tempCaseLoadInfo.caseType, tempChildNode, null);

                                    myTargetCaseList[0].Key.Add(tempChildCell);
                                    myTargetCaseList.Add(new KeyValuePair<CaseCell, XmlNode>(tempChildCell, tempChildNode));
                                }
                                else
                                {
                                    //it will cant be project and if it is unknow i will not show it
                                    ShowMessage("find unkown case so drop it", thisErrorTitle);
                                }
                            }
                        }
                        myTargetCaseList.Remove(myTargetCaseList[0]);
                    }
                    #endregion

                }
                #endregion

                if (nowCaseActionActuator == null)
                {
                    tvw_Case.Enabled = false;
                    ShowMessage("ScriptRunTime is not find ");
                    MessageBox.Show("ScriptRunTime is not find ,the case will cannot run", "STOP");
                    lb_msg1.Text = "ScriptRunTime error";
                }
                else
                {
                    tvw_Case.Enabled = true;
                    nowCaseActionActuator.SetCaseRunTime(myProjectCaseDictionary, myProjctCollection);
                    //启动数据呈现
                    lb_msg1.Text = "case load sucess";
                }
            }
            //严重错误
            catch (Exception ex)
            {
                int tempErrorIndex1 = 0;
                string tempErrorIndex2 = "";
                if (myProjectCaseDictionary.Count != 0)
                {
                    tempErrorIndex1 = myProjectCaseDictionary.Count;
                    if (tempErrorIndex1 > 0)
                    {
                        foreach (Dictionary<int, CaseCell> tempProject in myProjectCaseDictionary.Values)
                        {
                            tempErrorIndex2 += " ->" + tempProject.Count + 1;
                        }
                    }
                }
                MessageBox.Show(string.Format("用例脚本错误，请检查\n详见错误日志\n错误代码：{0}X{1}", tempErrorIndex1, tempErrorIndex2), "STOP");
                tvw_Case.Nodes.Clear();
                ErrorLog.PutInLog("ID:0527  " + ex.Message);
                ErrorLog.PutInLog("脚本错误位置  ：第" + tempErrorIndex1 + "个工程，第" + (tempErrorIndex2) + "个用例");
                //启动数据呈现
                lb_msg1.Text = "case file error";
                ShowMessage(ex.Message);
            }

            CreateTree(myProjctCollection);
        }

        public void CreateTree(ProjctCollection yourProjctCell)
        {
            tvw_Case.Nodes.Clear();
            if(yourProjctCell==null)
            {
                return;
            }
            string thisErrorTitle;
            foreach(var tempProjctCell in yourProjctCell.ProjectCells)
            {
                myCaseLaodInfo tempProjectLoadInfo = myCaseScriptAnalysisEngine.getCaseLoadInfo(tempProjctCell.CaseXmlNode);
                TreeNode tempProjctTree = new TreeNode(tempProjectLoadInfo.name, 0, 0);
                tempProjctTree.Tag = tempProjctCell;
                tempProjctCell.UiTag = tempProjctTree;
                List<KeyValuePair<TreeNode, CaseCell>> unDealCaseList = new List<KeyValuePair<TreeNode, CaseCell>>();
                if (tempProjctCell.IsHasChild)
                {
                    unDealCaseList.Add(new KeyValuePair<TreeNode, CaseCell>(tempProjctTree, tempProjctCell));
                }
                #region Deal unDealCaseList
                while (unDealCaseList.Count > 0)
                {
                    if (unDealCaseList[0].Value.IsHasChild)
                    {
                        foreach (var tempCell in unDealCaseList[0].Value.ChildCells)
                        {
                            myCaseLaodInfo tempCaseLoadInfo = myCaseScriptAnalysisEngine.getCaseLoadInfo(tempCell.CaseXmlNode);
                            thisErrorTitle = "Case ID:" + tempCaseLoadInfo.id;
                            if (tempCaseLoadInfo.ErrorMessage != "")
                            {
                                ShowMessage(tempCaseLoadInfo.ErrorMessage);
                                ShowMessage("this error can not be repair so drop it", thisErrorTitle);
                            }
                            else
                            {
                                TreeNode tempChildTreeNode;
                                if (tempCaseLoadInfo.caseType == CaseType.Case)
                                {
                                    if (isShowCaseContent)
                                    {
                                        tempChildTreeNode = new TreeNode(myDataAnalysis.myStringAdd("ID:" + tempCaseLoadInfo.id, tempCaseLoadInfo.remark, 15) + " ● " + tempCaseLoadInfo.content);
                                    }
                                    else
                                    {
                                        tempChildTreeNode = new TreeNode(myDataAnalysis.myStringAdd("ID:" + tempCaseLoadInfo.id, tempCaseLoadInfo.remark, 15));
                                    }

                                    if (tempCaseLoadInfo.actions.Count > 0)
                                    {
                                        setNodeImageIndex(tempChildTreeNode, 16);
                                    }
                                    else
                                    {
                                        setNodeImageIndex(tempChildTreeNode, 1);
                                    }
                                    tempChildTreeNode.Tag = tempCell;
                                    tempCell.UiTag = tempChildTreeNode;
                                    if (tempCell.CaseRunData.errorMessages != null)
                                    {
                                        tempChildTreeNode.BackColor = Color.LemonChiffon;
                                    }
                                    unDealCaseList[0].Key.Nodes.Add(tempChildTreeNode);
                                }
                                else if (tempCaseLoadInfo.caseType == CaseType.Repeat)
                                {
                                    tempChildTreeNode = new TreeNode(myDataAnalysis.myStringAdd("Repeat:" + tempCaseLoadInfo.times, tempCaseLoadInfo.remark, 15));
                                    setNodeImageIndex(tempChildTreeNode, 9);
                                    tempChildTreeNode.Tag = tempCell;
                                    tempCell.UiTag = tempChildTreeNode;
                                    unDealCaseList.Add(new KeyValuePair<TreeNode, CaseCell>(tempChildTreeNode, tempCell));
                                    unDealCaseList[0].Key.Nodes.Add(tempChildTreeNode);
                                }
                                else
                                {
                                    //it will cant be project and if it is unknow i will not show it
                                    ShowMessage("find unkown case so drop it", thisErrorTitle);
                                }
                            }
                        }
                    }
                    unDealCaseList.Remove(unDealCaseList[0]);
                }
                #endregion
                
                tvw_Case.Nodes.Add(tempProjctTree);
            }
        }


        /// <summary>
        /// 准备执行
        /// </summary>
        /// <returns>是否可以进行</returns>
        public bool readyRun(out TreeNode yourStartTreeNode)
        {
            if ((yourStartTreeNode=tvw_Case.SelectedNode)== null)
            {
                nowErrorMessage = "未选择任何项目" ;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 执行
        /// </summary>
        public void RunCase()
        {
            TreeNode tempStartTreeNode;
            if (!readyRun(out tempStartTreeNode))
            {
                MessageBox.Show(nowErrorMessage, "stop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ShowMessage("未能执行指定项目");
                return;
            }

            if (nowCaseActionActuator.RunCaseScript((CaseCell)tempStartTreeNode.Tag))
            {
                nowProjectRunCount = new myRunCount();

                //启动数据呈现
                lb_msg3.Text = "Pass 0";
                lb_msg4.Text = "Fail 0";
                lb_msg5.Text = "Unknow 0";
                ShowMessage("开始执行选定项目");
            }
            else
            {
                MessageBox.Show(nowCaseActionActuator.ErrorInfo, "stop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ShowMessage("未能执行指定项目");
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void StopCase()
        {
            if (nowCaseActionActuator != null)
            {
                if (!nowCaseActionActuator.StopCaseScript())
                {
                    MessageBox.Show(richTextBox_showMessage.Text, "Stop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("请先加载工程");
            }
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void PauseCase()
        {
            if (nowCaseActionActuator != null)
            {
                if (!nowCaseActionActuator.PauseCaseScript())
                {
                    MessageBox.Show(richTextBox_showMessage.Text, "Stop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("请先加载工程");
            }
        }

        /// <summary>
        /// 单步执行
        /// </summary>
        public void RunNextCase()
        {
            if (nowCaseActionActuator != null)
            {
                if (tvw_Case.SelectedNode != null)
                {
                    nowCaseActionActuator.TryNextCaseScript((CaseCell)(tvw_Case.SelectedNode).Tag);
                }
                else
                {
                    MessageBox.Show("请先选择项目");
                }
            }
            else
            {
                MessageBox.Show("请先加载工程");
            }
        }

        /// <summary>
        /// 单项执行
        /// </summary>
        public void RunTryCase()
        {
            if (nowCaseActionActuator != null)
            {
                if (tvw_Case.SelectedNode != null)
                {
                    if (!nowCaseActionActuator.TryNowCaseScript((CaseCell)(tvw_Case.SelectedNode).Tag))
                    {
                        MessageBox.Show(richTextBox_showMessage.Text, "Stop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("未选择任何项");
                }
            }
            else
            {
                MessageBox.Show("请先加载工程");
            }
        }

        /// <summary>
        /// 获取当前运行状态
        /// </summary>
        /// <returns>运行状态</returns>
        public CaseActuatorState GetMainTaskRunState()
        {
            if (nowCaseActionActuator == null)
            {
                return CaseActuatorState.Stop;
            }
            else
            {
                return nowCaseActionActuator.Runstate;
            }
        }
        #endregion

        #region CustomDelegate
        /// <summary>
        /// 添加返回结果到listView_DataAdd
        /// </summary>
        /// <param name="yourResult">your Result</param>
        private void AddExecutiveResultToLvDataAdd(myExecutionDeviceResult yourResult)
        {
            myCommonTool.SetControlFreeze(listView_DataAdd);
            listView_DataAdd.BeginUpdate();
            listView_DataAdd.Items.Add(new ListViewItem(new string[] { listView_DataAdd.Items.Count.ToString(), yourResult.caseId.ToString(), yourResult.startTime, yourResult.spanTime, yourResult.result.ToString(), yourResult.caseTarget + "->" + yourResult.backContent, yourResult.staticDataResultCollection.MyToString() + yourResult.additionalRemark }));
            if (pictureBox_dataAddStopTag)
            {
                listView_DataAdd.EnsureVisible(listView_DataAdd.Items.Count - 1);
            }
            listView_DataAdd.EndUpdate();
            myCommonTool.SetControlUnfreeze(listView_DataAdd);

            nowProjectRunCount.allCount++;
            if (yourResult.result == CaseResult.Pass)
            {
                nowProjectRunCount.passCount++;
                lb_msg3.Text = "Pass " + nowProjectRunCount.passCount;
            }
            else if (yourResult.result == CaseResult.Fail)
            {
                nowProjectRunCount.failCount++;
                lb_msg4.Text = "Fail " + nowProjectRunCount.failCount;
            }
            else
            {
                nowProjectRunCount.otherCount++;
                lb_msg5.Text = "Unknow " + nowProjectRunCount.otherCount;
            }

        }

        private void AddExecutiveDataToRecord(string yourMes)
        {
            trb_addRecord.AddDate(yourMes, Color.Red, true);
        }

        #endregion

        #region CustomEvent
        /// <summary>
        /// ExecutiveData将由此暴露执行中数据
        /// </summary>
        /// <param name="yourContent">your Content</param>
        void nowCaseActionActuator_OnGetExecutiveData(string sender, string yourContent)
        {
            if (trb_addRecord.InvokeRequired)
            {
                trb_addRecord.BeginInvoke(AddExecutiveRecord, yourContent);
            }
            else
            {
                trb_addRecord.AddDate(yourContent, Color.Red, true);
            }
        }

        /// <summary>
        /// ExecutiveData将由此暴露执行器遇到的非致命性错误或警告
        /// </summary>
        /// <param name="yourContent">your Content</param>
        void nowCaseActionActuator_OnGetErrorData(string sender, string yourContent)
        {
            //ShowMessage(yourContent);
            this.BeginInvoke(new Action<string>((str) => ShowMessage(str)), yourContent);
        }


        /// <summary>
        /// ExecutiveResult将由此暴露执行结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="yourResult"></param>
        void nowCaseActionActuator_OnExecutiveResult(string sender, myExecutionDeviceResult yourResult)
        {
            if (listView_DataAdd.InvokeRequired)
            {
                listView_DataAdd.BeginInvoke(AddExecutiveResult, yourResult);
            }
            else
            {
                AddExecutiveResultToLvDataAdd(yourResult);
            }

        }

        /// <summary>
        /// ActionActuator运行状态发生了改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="yourState"></param>
        void nowCaseActionActuator_OnActuatorStateChanged(string sender, CaseActuatorState yourState)
        {
            //lb_msg2.Text = yourState.ToString();
            this.BeginInvoke(new Action<string>((str) => lb_msg2.Text = str), yourState.ToString());
            progressBarRefresh();
        }

        #endregion

        //form load 
        private void AutoTest_Load(object sender, EventArgs e)
        {
            //载入参数初始值
            setInitialValue();
            //载入配置文件
            loadIni();
            fillCheckDataHashtable();
            isFistLoad = false;
            //文件选取地址
            openFileDialog_caseFile.InitialDirectory = System.Environment.CurrentDirectory + "\\casefile";
            //启动ui计时器
            timer_show.Enabled = true;
            //载入文件列表
            loadCaseFile();
            //载入MEF组件
            loadMefDriver();
            ////自动载入用例
            //LoadTreeView(tb_caseFilePath.Text);
            ////改变工作路径Environment.CurrentDirectory随程序运行可能会发生改变
            //System.Environment.CurrentDirectory = Application.StartupPath;

            //vanelife_config页面加载
            vcLoad();
            //vanelife_serial页面加载

            //AutoTest_CaseRunner页面加载
            AT_CaseRunnerLoad();

            //AutoTest_RemoteRunner页面加载
            AT_RemoteRunnerLoad();
        }

        //close
        private void AutoTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            myini.IniWriteValue("vane", "filepath", tb_caseFilePath.Text, System.Environment.CurrentDirectory + "\\seting\\seting.ini");

            if (GetMainTaskRunState() == CaseActuatorState.Running)
            {
                if (MessageBox.Show("您有任务还在运行，是否强制关闭？", "Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    nowCaseActionActuator.StopCaseScript();
                    Thread.Sleep(100);
                    if ((GetMainTaskRunState() == CaseActuatorState.Running))
                    {
                        nowCaseActionActuator.KillAll();
                    }
                }
            }
        

            //vanelife_config
            vcAutoTest_FormClosing(sender, e);

            //AutoTest_CaseRunne
            AT_CaseRunner_FormClosing(sender, e);

            //AutoTest_RemoteRunner
            AT_RemoteRunner_FormClosing(sender, e);

        }

        //UI计时器
        private void timer_show_Tick(object sender, EventArgs e)
        {
            //列表刷新
            if (GetMainTaskRunState() == CaseActuatorState.Stop || GetMainTaskRunState() == CaseActuatorState.Trying || GetMainTaskRunState() == CaseActuatorState.Stoping)
            {
                //do someting when task is stoped
            }
            else
            {
                progressBarRefresh();
            }
            
            //while (dataListNum < myReceiveData.myData.Count)
            //{
            //    dataListNum++;
            //    listView_DataAdd.Items.Add(new ListViewItem(new string[] {dataListNum.ToString(), myReceiveData.myData[dataListNum - 1].caseId, myReceiveData.myData[dataListNum - 1].startTime, myReceiveData.myData[dataListNum - 1].spanTime, myReceiveData.myData[dataListNum - 1].ret, myReceiveData.myData[dataListNum - 1].result, myReceiveData.myData[dataListNum - 1].remark}));
            //    if (pictureBox_dataAddStopTag)
            //    {
            //        listView_DataAdd.EnsureVisible(listView_DataAdd.Items.Count - 1);
            //    }
            //}

            //提示控制
            if (isShowMessage)
            {
                ShowMessageTime--;
                if(ShowMessageTime==0)
                {
                    isShowMessage = false;
                    expandablePanel_messageBox.Expanded = false;
                }
            }
        }

        //UI尺寸调整
        private void AutoTest_Resize(object sender, EventArgs e)
        {
            if (isFistLoad)
            {
                return;
            }
            //tvw_Case Resize
            tvw_Case.Height = this.Height - 167;
            tvw_Case.Width = this.Width - 5;

            //trb_addRecord Resize
            trb_addRecord.Width = this.Width - 503;

            //show message box
            expandablePanel_messageBox.Expanded = true;
            expandablePanel_messageBox.Location = new Point(this.Width - 204, this.Height - 105 - expandablePanel_messageBox.Height+26);
            expandablePanel_messageBox.Expanded = false;
            expandablePanel_messageBox.Location=new Point(this.Width-204,this.Height-105);
            
            //other Resize
            lb_msg1.Location = new Point(lb_msg1.Location.X, this.Height - 74);
            lb_msg2.Location = new Point(this.Width - 423, this.Height - 74);
            progressBar_case.Location = new Point(progressBar_case.Location.X, this.Height - 77);
            progressBar_case.Width = this.Width - 558;

            lb_msg3.Location = new Point(this.Width - 249, this.Height - 74);
            lb_msg4.Location = new Point(this.Width - 162, this.Height - 74);
            lb_msg5.Location = new Point(this.Width-84, this.Height - 74);

            //vanelife_config
            vcAutoTest_Resize(sender,e);

            //AutoTest_CaseRunner
            AT_CaseRunner_Resize(sender, e);

            //AutoTest_RemoteRunner
            AT_RemoteRunner_Resize(sender, e);
        }

        //open test file
        private void test_Click(object sender, EventArgs e)
        {
            //LoadTreeView(System.Environment.CurrentDirectory + "\\casefile\\DebugCase.xml");
            //Performance test//myGlobalStaticData.startWatch();
            //LoadTreeView(tb_caseFilePath.Text);
            //Performance test//MessageBox.Show(myGlobalStaticData.getWatchTime());
            //Performance test//myGlobalStaticData.startWatch();
            LoadTreeViewEx(tb_caseFilePath.Text);
            //Performance test//MessageBox.Show(myGlobalStaticData.getWatchTime());
            //改变工作路径
            System.Environment.CurrentDirectory = Application.StartupPath;

        }

        //export Report file
        private void pictureBox_exportReport_Click(object sender, EventArgs e)
        {
            if (GetMainTaskRunState() != CaseActuatorState.Stop)
            {
                MessageBox.Show("任务未结束，请先停止执行", "stop");
            }
            else
            {
                if (nowCaseActionActuator == null)
                {
                    MessageBox.Show("未发现可用执行器", "stop");
                    return;
                }
                else if (nowCaseActionActuator.NowExecutionResultList.Count<1)
                {
                    MessageBox.Show("执行器中未发现任何可用数据", "stop");
                    return;
                }
                if (!myTool.myResultOut.createReport(myCase.myFile, nowCaseActionActuator.NowExecutionResultList, ref myReportPath))
                {
                    MessageBox.Show("报告生成失败，祥见错误日志", "stop");
                }
                else
                {
                    if (MessageBox.Show("报告生成成功是否查看", "ok", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        myCommonTool.OpenPress(myReportPath, "");
                    }       
                }
            }
        }

        // open InterfaceTest_Click
        private void pictureBox_openInterfaceTest_Click(object sender, EventArgs e)
        {
            if (!myCommonTool.OpenPress(System.Environment.CurrentDirectory + "\\myTool\\InterfaceTest\\HttpDome.exe", ""))
            {
                MessageBox.Show("外部组件丢失","stop");
            }
        }


        //组件控制
        private void checkBox_dataBack_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxItem_dataAdd.Checked = checkBox_dataBack.Checked;
        }

        private void checkBox_run_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxItem_run.Checked = checkBox_run.Checked;
        }

        //打开公共参数设置
        private void pictureBox_set1_Click(object sender, EventArgs e)
        {
            buttonItem12_Click(null, null);
            /*
            CaseActuatorState tempState = GetMainTaskRunState();
            if (tempState == CaseActuatorState.Running || tempState == CaseActuatorState.Trying || tempState == CaseActuatorState.Stoping)
            {
                MessageBox.Show("测试任务正在执行,请先停止执行", "stop");
                return;
            }
            SetingWindow newSet = new SetingWindow();
            //OptionWindow newSet = new OptionWindow();
            newSet.ShowDialog(this);
            */
        }

        //尝试手动测试
        private void pictureBox_tryTest1_Click(object sender, EventArgs e)
        {
            CaseActuatorState tempState = GetMainTaskRunState();
            if (tempState == CaseActuatorState.Running || tempState == CaseActuatorState.Trying || tempState == CaseActuatorState.Stoping)
            {
                MessageBox.Show("测试任务正在执行,请先停止执行", "stop");
            }
            else
            {
                string myTempStr = MyCommonTool.myWebTool.MyHttp.SendData(tb_tryTestData.Text, "", "GET");
                trb_addRecord.AddDate( myTempStr, Color.Gray, true);
            }
        }

        //open the test file
        private void bt_openFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog_caseFile.ShowDialog() == DialogResult.OK)
            {
                tb_caseFilePath.Text = openFileDialog_caseFile.FileName;
            }
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
                ShowMessage(ex.Message);
            }
        }

        //更改选中项
        private void tvw_Case_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //show with none
            if (tvw_Case.SelectedNode == null)
            {
                showNode = null;
                return;
            }
            //show when select project
            if (tvw_Case.SelectedNode.Parent == null)
            {
                showNode = ((CaseCell)tvw_Case.SelectedNode.Tag).CaseXmlNode;
            }
            //show when select case
            else
            {
                showNode = ((CaseCell)tvw_Case.SelectedNode.Tag).CaseXmlNode;
            }
        }


        #region StartButton

        //枚举指定用例列表
        public void loadCaseFile()
        {
            string[] dirs = System.IO.Directory.GetFiles(System.Environment.CurrentDirectory + "\\casefile");
            foreach (string tempPath in dirs)
            {
                DevComponents.DotNetBar.ButtonItem tempButtomItem = new DevComponents.DotNetBar.ButtonItem(tempPath, myCommonTool.findFileName(tempPath));
                tempButtomItem.Click += new EventHandler(tempButtomItem_Click);
                itemContainer_case.SubItems.Add(tempButtomItem);
            }
        }

        //载入Mef组件
        public void loadMefDriver()
        {
            MefPlugInDriver mi = new MefPlugInDriver();
            mi.Compose();
            MessageBox.Show(mi.ExtendTest.ExtendProtocolName);
        }

        //open xml to edit
        void tempButtomItem_Click(object sender, EventArgs e)
        {

            //if (MessageBox.Show("如果对脚本规则不熟悉，请不要在此处修改相关用例脚本", "Warming", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            //{
            //    return;
            //}
            myini.IniWriteValue("casepath", "pathtoopen", ((DevComponents.DotNetBar.ButtonItem)sender).Name, System.Environment.CurrentDirectory + "\\seting\\seting.ini");
            System.Diagnostics.Process.Start(System.Environment.CurrentDirectory + "\\myTool\\XmlPad.exe");
        }


        //打开错误日志
        private void btitem_openError_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(System.Environment.CurrentDirectory + "\\log");
        }

        //open Result
        private void btitem_testResult_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(System.Environment.CurrentDirectory + "\\testResult");
        }

        //open Export
        private void btitem_openExport_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(System.Environment.CurrentDirectory + "\\testReport");
        }


        //打开使用帮助
        private void btitem_help_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(System.Environment.CurrentDirectory + "\\help\\AutoTest.chm");
        }

        //打开关于窗口
        private void btitem_about_Click(object sender, EventArgs e)
        {
            AboutBox myAboutBox = new AboutBox();
            myAboutBox.StartPosition = FormStartPosition.CenterParent;
            myAboutBox.ShowDialog();
        }

        //打开设置窗口
        private void buttonItem12_Click(object sender, EventArgs e)
        {
            if (GetMainTaskRunState() != CaseActuatorState.Stop)
            {
                MessageBox.Show("请先停止当前任务", "stop");
                return;
            }
            //SetingWindow newSet = new SetingWindow();
            OptionWindow newSet = new OptionWindow();
            newSet.ShowDialog(this);
        }

        //组件控制
        private void checkBoxItem_run_CheckedChanged(object sender, DevComponents.DotNetBar.CheckBoxChangeEventArgs e)
        {
            if (checkBoxItem_run.Checked)
            {
                checkBox_run.Checked = true;
                expandablePanel_testMode.Visible = true;
                expandablePanel_testMode.Location = myShareData.sdExpandablePanel_testMode_Position;
            }
            else
            {
                checkBox_run.Checked = false;
                expandablePanel_testMode.Visible = false;
            }
        }

        private void checkBoxItem_edit_CheckedChanged(object sender, DevComponents.DotNetBar.CheckBoxChangeEventArgs e)
        {

        }

        private void checkBoxItem_dataAdd_CheckedChanged(object sender, DevComponents.DotNetBar.CheckBoxChangeEventArgs e)
        {
            if (checkBoxItem_dataAdd.Checked)
            {
                checkBox_dataBack.Checked = true;
                expandablePanel_dataAdd.Visible = true;
                expandablePanel_dataAdd.Location = myShareData.sdExpandablePanel_dataAdd_Position;
            }
            else
            {
                checkBox_dataBack.Checked = false;
                expandablePanel_dataAdd.Visible = false;
            }
        }

        #endregion

        #region MenuStripEven

        //从此处开始执行
        private void RunHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunCase();
        }

        //暂停项目
        private void PauseRunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PauseCase();
        }

        //单步执行下一个
        private void RunNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunNextCase();
        }

        //仅执行此处
        private void runHereOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {

            RunTryCase();
        }

        //停止执行
        private void stopRunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopCase();
        }

        //修改脚本
        private void ModifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //null project
            if (GetMainTaskRunState() != CaseActuatorState.Stop)
            {
                MessageBox.Show("请先停止任务", "stop");
                return;
            }

            if (tvw_Case.SelectedNode == null)
            {
                MessageBox.Show("未选择任何项目", "can not run");
                return;
            }

            switch(((CaseCell)tvw_Case.SelectedNode.Tag).CaseType)
            {
                case CaseType.Case:
                    {
                        EditCasebody newEdit = new EditCasebody(tvw_Case.SelectedNode);
                        newEdit.Owner = this;
                        newEdit.ShowDialog(this);
                        //update tree
                        break;
                    }
                case CaseType.Project:
                    {
                        EditProjiectHead newEdit = new EditProjiectHead();
                        newEdit.Owner = this;
                        newEdit.ShowDialog(this);
                        //update tree
                        break;
                    }
                case CaseType.Repeat:
                    break;
                case CaseType.ScriptRunTime:
                    break;
                default:
                    break;
            }

        }

        //添加脚本
        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myCase.myFile != "")
            {
                if (MessageBox.Show("功能模块丢失，是否直接打开脚本文件进行编辑", "Warming", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    myini.IniWriteValue("casepath", "pathtoopen", myCase.myFile, System.Environment.CurrentDirectory + "\\seting\\seting.ini");
                    System.Diagnostics.Process.Start(System.Environment.CurrentDirectory + "\\myTool\\XmlPad.exe");
                }
            }
            else
            {
                MessageBox.Show("请先加载用例", "STOP");
            }
        }

        //删除脚本
        private void DelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myCase.myFile != "")
            {
                if (MessageBox.Show("功能模块丢失，是否直接打开脚本文件进行编辑", "Warming", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    myini.IniWriteValue("casepath", "pathtoopen", myCase.myFile, System.Environment.CurrentDirectory + "\\seting\\seting.ini");
                    System.Diagnostics.Process.Start(System.Environment.CurrentDirectory + "\\myTool\\XmlPad.exe");
                }
            }
            else
            {
                MessageBox.Show("请先加载用例", "STOP");
            }
        }

        //组件控制
        private void ToolStripMenuItem_dataAdd_Click(object sender, EventArgs e)
        {
            checkBox_dataBack.Checked = !checkBox_dataBack.Checked;
        }

        private void ToolStripMenuItem_runQuick_Click(object sender, EventArgs e)
        {
            checkBox_run.Checked = !checkBox_run.Checked;
        }

        private void ToolStripMenuItem_editQuick_Click(object sender, EventArgs e)
        {
            checkBoxItem_edit.Checked = !checkBoxItem_edit.Checked;
        }

        //编辑运行时参数
        private void caseParameterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myDialogWindow.myCaseParameter myParameterWindow = new myCaseParameter();
            myParameterWindow.Owner = this;
            myParameterWindow.StartPosition = FormStartPosition.CenterParent;
            myParameterWindow.Show();
        }

        #endregion

        #region DataBackWindow

        private void pictureBox_dataAddClose_Click(object sender, EventArgs e)
        {
            checkBox_dataBack.Checked = false;
        }

        private void pictureBox_dataAddSave_Click(object sender, EventArgs e)
        {
            //no data to save
            if (listView_DataAdd.Items.Count == 0)
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
                    foreach (ListViewItem tempVal in listView_DataAdd.Items)
                    {
                        sw.WriteLine(tempVal.SubItems[0].Text + "#" + tempVal.SubItems[1].Text + "#" + tempVal.SubItems[2].Text + "#" + tempVal.SubItems[3].Text + "#" + tempVal.SubItems[4].Text + "#" + tempVal.SubItems[5].Text + "#" + tempVal.SubItems[6].Text);
                    }
                    sw.Close();
                    fs.Close();
                    MessageBox.Show("保存成功，请到测试结果文件testResult查看", "Sucess");
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (!File.Exists(tempFilePathForBack + ".bak" + i))
                        {
                            Directory.Move(tempFilePathForBack, tempFilePathForBack + ".bak" + i);
                            break;
                        }
                    }

                    FileStream fs = new FileStream(tempFilePathForBack, FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(DateTime.Now.ToString());
                    foreach (ListViewItem tempVal in listView_DataAdd.Items)
                    {
                        sw.WriteLine(tempVal.SubItems[0].Text + "#" + tempVal.SubItems[1].Text + "#" + tempVal.SubItems[2].Text + "#" + tempVal.SubItems[3].Text + "#" + tempVal.SubItems[4].Text + "#" + tempVal.SubItems[5].Text + "#" + tempVal.SubItems[6].Text);
                    }
                    sw.Close();
                    fs.Close();
                    MessageBox.Show("保存成功，请到测试结果文件testResult查看", "Sucess");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存文件错误，详情请查看错误日志", "Stop");
                ErrorLog.PutInLog("ID:1940  " + ex.Message);
                ShowMessage(ex.Message);
            }
        }

        private void pictureBox_dataAddclean_Click(object sender, EventArgs e)
        {
            //this time if the DataReceive is open it will has an erorr
            //myReceiveData.myData.Clear();
            //dataListNum = 0;
            listView_DataAdd.Items.Clear();
            ShowMessage("记录清除完成");
        }

        private void pictureBox_dataAddStop_Click(object sender, EventArgs e)
        {
            if (pictureBox_dataAddStopTag)
            {
                pictureBox_dataAddStopTag = false;
                pictureBox_dataAddStop.Image = imageListForButton.Images[1];
            }
            else
            {
                pictureBox_dataAddStopTag = true;
                pictureBox_dataAddStop.Image = imageListForButton.Images[0];
            }
        }

        //call  CBalloon with test result info
        private void listView_DataAdd_DoubleClick(object sender, EventArgs e)
        {
            if (listView_DataAdd.SelectedItems[0] != null)
            {
                Point myPosition = new Point(listView_DataAdd.SelectedItems[0].Bounds.X + 5 + expandablePanel_dataAdd.Location.X, listView_DataAdd.SelectedItems[0].Bounds.Y + 85 + expandablePanel_dataAdd.Location.Y);
                myDialogWindow.MyCBalloonResultInfo myListViewCBallon = null;
                myListViewCBallon = new myDialogWindow.MyCBalloonResultInfo(listView_DataAdd.SelectedItems[0]);
                //myListViewCBallon.Owner = this;
                myListViewCBallon.HasShadow = true;
                myListViewCBallon.setBalloonPosition(this, myPosition, new Size(1, 1));
                myListViewCBallon.Show();
            }
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
                if (expandablePanel_dataAdd.Location.X > this.Width - 50 || expandablePanel_dataAdd.Location.Y>this.Height-100 )
                {

                    isCanMove = false;
                }
                else
                {
                    isCanMove = true;
                }
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

                if (expandablePanel_dataAdd.Height < 40 || expandablePanel_dataAdd.Width<200)
                {
                    isCanMove = false;
                }
                else
                {
                    isCanMove = true;
                }
            }
        }

        private void pictureBox_changeDataAddSize_MouseUp(object sender, MouseEventArgs e)
        {
            isCanMove = false;
        }

        private void label_moveFlagForDataAdd_DoubleClick(object sender, EventArgs e)
        {
            int defultDataAddHeight = 242;
            int defultDataAddWidth = 605;
            Point defultDataAddLocation = new Point(389, 91);
            if(expandablePanel_dataAdd.Height==tvw_Case.Height&&expandablePanel_dataAdd.Width==tvw_Case.Width)
            {
                expandablePanel_dataAdd.Location = defultDataAddLocation;
                expandablePanel_dataAdd.Height = defultDataAddHeight;
                expandablePanel_dataAdd.Width = defultDataAddWidth;
                label_moveFlagForDataAdd.Width = expandablePanel_dataAdd.Width - 28;
            }
            else
            {
                expandablePanel_dataAdd.Location = tvw_Case.Location;
                expandablePanel_dataAdd.Height = tvw_Case.Height;
                expandablePanel_dataAdd.Width = tvw_Case.Width;
                label_moveFlagForDataAdd.Width = expandablePanel_dataAdd.Width - 28;
            }
        }

        #endregion

        #endregion

        #region RunWindow
        #region RunWindowMove
        private void label_moveFlagForRun_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left))
            {
                isCanMove = true;
                myOriginPoint = Control.MousePosition;
                myOriginLocation = new Point(expandablePanel_testMode.Location.X, expandablePanel_testMode.Location.Y);
            }
        }

        private void label_moveFlagForRun_MouseUp(object sender, MouseEventArgs e)
        {
           isCanMove = false;
        }

        private void label_moveFlagForRun_MouseMove(object sender, MouseEventArgs e)
        {
            if (isCanMove)
            {
                Point tempMousePosition = new Point(Control.MousePosition.X - myOriginPoint.X, Control.MousePosition.Y - myOriginPoint.Y);
                isCanMove = false;
                expandablePanel_testMode.Location = new Point(myOriginLocation.X + tempMousePosition.X, myOriginLocation.Y + tempMousePosition.Y);
                expandablePanel_testMode.Update();
                if (expandablePanel_testMode.Location.X > this.Width - 50 || expandablePanel_testMode.Location.Y > this.Height - 100)
                {
                    isCanMove = false;
                }
                else
                {
                    isCanMove = true;
                }
            }
        }
        #endregion

        private void pictureBox_runClose_Click(object sender, EventArgs e)
        {
            checkBox_run.Checked = false;
        }

        private void pictureBox_RunHere_Click(object sender, EventArgs e)
        {
            RunHereToolStripMenuItem_Click(sender, e);
        }

        private void pictureBox_runHereOnly_Click(object sender, EventArgs e)
        {
            runHereOnlyToolStripMenuItem_Click(sender, e);
        }

        private void pictureBox_stopRun_Click(object sender, EventArgs e)
        {
            stopRunToolStripMenuItem_Click(sender, e);
        }

        private void pictureBox_set_Click(object sender, EventArgs e)
        {
            PauseRunToolStripMenuItem_Click(sender, e);
        }

        //i will reload the case file
        private void pictureBox_reLoadCase_Click(object sender, EventArgs e)
        {
            //expandablePanel_messageBox.SendToBack();
            if (GetMainTaskRunState() != CaseActuatorState.Stop)
            {
                MessageBox.Show("请先停止当前任务", "STOP");
            }
            else
            {
                if (myCase.myFile != "")
                {
                    LoadTreeViewEx(myCase.myFile);
                    ShowMessage("刷新完成，请继续操作");
                }
                else
                {
                    MessageBox.Show("请先加载用例", "STOP");
                }
            }
        }


        private void pictureBox_caseParameter_Click(object sender, EventArgs e)
        {
            caseParameterToolStripMenuItem_Click(sender, e);
        }
       
        #endregion

        #region other Control

        //go in to last line
        private void trb_addRecord_TextChanged(object sender, EventArgs e)
        {
            //老方法会闪屏
            //trb_addRecord.Focus();
            //trb_addRecord.SelectionStart = trb_addRecord.Text.Length;
            //trb_addRecord.ScrollToCaret();

            //若没有其他填充逻辑可以直接创建填以下充函数
            //rtb_info.AppendText(tempStr + "\n");
            //rtb_info.Focus();
            //Application.DoEvents();

            //定位到尾行
            //trb_addRecord.SelectionStart = trb_addRecord.Text.Length;
            //设置trb_addRecord.HideSelection = false;，即可以不用使用.Focus()
            //trb_addRecord.Focus();
            //Application.DoEvents();
            
        }

        //pictureBox change for all
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
           if (sender == pictureBox_MessageInfoList)
            {
                pictureBox_MessageInfoList.BackColor = Color.Transparent;
            }
            else
            { 
                ((PictureBox)sender).BackColor = Color.Honeydew;
            }
        }

        //pictureBox change for all
        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            if (sender == pictureBox_MessageInfoList)
            {
                pictureBox_MessageInfoList.BackColor = Color.Honeydew;
            }
            else
            {
                ((PictureBox)sender).BackColor = Color.Transparent;
            }
        }

        //call CBalloon with case info
        private void tvw_Case_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (tvw_Case.SelectedNode != null)
            {
                if (tvw_Case.SelectedNode.Parent == null)
                {
                }
                else
                {
                    if (((CaseCell)tvw_Case.SelectedNode.Tag).CaseType == CaseType.Case)
                    {
                        Point myPosition = new Point(tvw_Case.SelectedNode.Bounds.X, tvw_Case.SelectedNode.Bounds.Y + 150);
                        myControl.MyCBalloon myListViewCBallon = null;
                        myListViewCBallon = new myControl.MyCBalloon(tvw_Case.SelectedNode);
                        myListViewCBallon.Owner = this;
                        myListViewCBallon.HasShadow = true;
                        myListViewCBallon.setBalloonPosition(this, myPosition, new Size(1, 1));
                        myListViewCBallon.Show();
                    }
                }
            }
        }

        //call window with Message InfoList
        private void pictureBox_MessageInfoList_Click(object sender, EventArgs e)
        {
            myDialogWindow.MyMessageListWindow myMessageWindow = new MyMessageListWindow("MessageInfor",true ,runInfoMessage);
            myMessageWindow.Owner = this;
            myMessageWindow.StartPosition = FormStartPosition.CenterParent;
            myMessageWindow.Show();
        }

        //all quick run box is here
        private void pictureBox_MianRun_Click(object sender, EventArgs e)
        {
            switch (((PictureBox)sender).Name)
            {
                case "pictureBox_RunHere":
                    RunHereToolStripMenuItem_Click(null, null);
                    break;
                case "pictureBox_PauseRun":
                    PauseRunToolStripMenuItem_Click(null, null);
                    break;
                case "pictureBox_RunNext":
                    RunNextToolStripMenuItem_Click(null, null);
                    break;
                case "pictureBox_StopRun":
                    stopRunToolStripMenuItem_Click(null, null);
                    break;
                case "pictureBox_RunHereOnly":
                    runHereOnlyToolStripMenuItem_Click(null, null);
                    break;
                case "pictureBox_reLoadCase":
                    pictureBox_reLoadCase_Click(null, null);
                    break;
                default:
                    MessageBox.Show("功能模块丢失", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    break;
            }
        }

        #endregion 


       


    }
}
