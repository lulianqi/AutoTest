using CaseExecutiveActuator.Cell;
using MyCommonTool;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;


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


namespace CaseExecutiveActuator
{
    /// <summary>
    /// 在这里绑定UI组件，如果想让运行过程事实反馈到UI界面上，请到此订阅（如果使用了自定义的UI组件，请在相应的位置按原有规则添加反馈）
    /// </summary>
   class myActionActuator
    {
        private delegate void delegateTreeNodeChange(TreeNode yourTreeNode);
        private delegate void delegateTreeNodeChangeColor(TreeNode yourTreeNode, Color yourColor);
        private delegate void delegateTreeNodeChangeImage(TreeNode yourTreeNode, int yourImageIndex);
        private delegate void delegateTreeNodeChangeText(TreeNode yourTreeNode, string yourText);
        private delegate void delegateTreeNodeChangeColorAndImage(TreeNode yourTreeNode, Color yourColor, int yourImageIndex);

        private static void CaseTreeNodeChangeExpand(TreeNode yourTreeNode)
        {
            yourTreeNode.TreeView.Invoke(new delegateTreeNodeChange((treeNode)=>treeNode.Toggle()), new object[] { yourTreeNode });
        }

        private static void CaseTreeNodeChangeState(TreeNode yourTreeNode, Color yourColor)
        {
            yourTreeNode.TreeView.Invoke(new delegateTreeNodeChangeColor((treeNode, color) => treeNode.BackColor = color), new object[] { yourTreeNode, yourColor });
        }

        private static void CaseTreeNodeChangeState(TreeNode yourTreeNode, int yourImageIndex)
        {
            yourTreeNode.TreeView.Invoke(new delegateTreeNodeChangeImage((treeNode, imageIndex) => treeNode.SelectedImageIndex = treeNode.ImageIndex = imageIndex), new object[] { yourTreeNode, yourImageIndex });
        }

        private static void CaseTreeNodeChangeState(TreeNode yourTreeNode, Color yourColor, int yourImageIndex)
        {
            yourTreeNode.TreeView.Invoke(new delegateTreeNodeChangeColorAndImage((treeNode, color, imageIndex) => { treeNode.BackColor = color; treeNode.SelectedImageIndex = treeNode.ImageIndex = imageIndex; }), new object[] { yourTreeNode, yourColor, yourImageIndex });
        }

        private static void CaseTreeNodeChangeText(TreeNode yourTreeNode, string yourText)
        {
            yourTreeNode.TreeView.Invoke(new delegateTreeNodeChangeText((treeNode, text) => treeNode.Text = text), new object[] { yourTreeNode, yourText });
        }




        public static void SetCaseNodeRunning(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 19);
            }
        }

        public static void SetCaseNodeSleeping(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 20);
            }
        }

        public static void SetCaseNodePass(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 15);
            }
        }

        public static void SetCaseNodeFial(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 7);
            }
        }

        public static void SetCaseNodeWarning(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 13);
            }
        }

        public static void SetCaseNodeBreak(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 11);
            }
        }

        public static void SetCaseNodePause(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 18);
            }
        }

        public static void SetCaseNodeStop(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 25);
            }
        }

        public static void SetCaseNodeNukown(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 12);
            }
        }

        public static void SetCaseNodeAbnormal(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 21);
            }
        }

        public static void SetCaseNodeNoActuator(CaseCell yourCell)
        {
            if (yourCell.UiTag == null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 24);
            }
        }

        public static void SetCaseNodeConnectInterrupt(CaseCell yourCell)
        {
            if (yourCell.UiTag==null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, 23);
            }
        }

        public static void SetCaseNodeContentError(CaseCell yourCell)
        {
            if (yourCell.UiTag==null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, Color.Pink);
            }
        }

        public static void SetCaseNodeContentWarning(CaseCell yourCell)
        {
            if (yourCell.UiTag==null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                CaseTreeNodeChangeState(yourTreeNode, Color.LightGoldenrodYellow);
            }
        }

        public static void SetCaseNodeContentEdit(CaseCell yourCell)
        {
             if (yourCell.UiTag==null)
            {
                return;
            }
             if (yourCell.UiTag is TreeNode)
             {
                 TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;
                 CaseTreeNodeChangeState(yourTreeNode, Color.PaleGoldenrod);
             }
        }



        /// <summary>
        /// 为TreeNode添加额外运行时消息以表示loop的变化（额外消息原则上为【···】这种格式或者为空"",处理时替换原有【···】，如果没有则直接添加）
        /// </summary>
        /// <param name="yourCell">CaseCell</param>
        /// <param name="yourMessage">Message （请务必保证数据为【···】这种格式，或为空""）</param>
        public static void SetCaseNodeLoopChange(CaseCell yourCell, string yourMessage)
        {
            if (yourCell.UiTag==null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                if (yourTreeNode.Text.StartsWith("【"))
                {
                    int tempEnd = yourTreeNode.Text.IndexOf('】');
                    CaseTreeNodeChangeText(yourTreeNode, yourMessage + yourTreeNode.Text.Remove(0, tempEnd + 1));
                }
                else
                {
                    CaseTreeNodeChangeText(yourTreeNode, yourMessage + yourTreeNode.Text);
                }
            }
        }

        /// <summary>
        /// 进行下一个loop刷新/清除当前loop的节点执行结果（最后的loop请不要调用该方法）
        /// </summary>
        /// <param name="yourCell">CaseCell</param>
        public static void SetCaseNodeLoopRefresh(CaseCell yourCell)
        {
            if (yourCell.UiTag==null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;
                yourTreeNode.TreeView.BeginUpdate();
                if (yourTreeNode.Nodes.Count > 0)
                {
                    foreach (TreeNode tempNode in yourTreeNode.Nodes)
                    {
                        if (((CaseCell)tempNode.Tag).CaseType != CaseType.Case)
                        {
                            continue;
                        }
                        if (((CaseCell)(tempNode.Tag)).CaseRunData.actions != null)
                        {
                            if (((CaseCell)(tempNode.Tag)).CaseRunData.actions.Count > 0)
                            {
                                CaseTreeNodeChangeState(tempNode, 16);
                                continue;
                            }
                        }
                        CaseTreeNodeChangeState(tempNode, 1);
                    }
                }
                yourTreeNode.TreeView.EndUpdate();
            }
        }

        /// <summary>
        /// 展开当前执行节点脚本
        /// </summary>
        /// <param name="yourCell">your CaseCell</param>
        public static void SetCaseNodeExpand(CaseCell yourCell)
        {
            if (yourCell.UiTag==null)
            {
                return;
            }
            if (yourCell.UiTag is TreeNode)
            {
                TreeNode yourTreeNode = (TreeNode)yourCell.UiTag;

                if (yourTreeNode.Nodes.Count > 0)
                {
                    //CaseType.Repeat
                    if (!yourTreeNode.IsExpanded)
                    {
                        CaseTreeNodeChangeExpand(yourTreeNode);
                    }
                }
                else
                {
                    //CaseType.Case
                    TreeNode tempParentNode = yourTreeNode.Parent;
                    while (tempParentNode != null)
                    {
                        if (!tempParentNode.IsExpanded)
                        {
                            CaseTreeNodeChangeExpand(tempParentNode);
                        }
                        tempParentNode = tempParentNode.Parent;
                    }
                }
            }
            //若要支持更多的ui请在后面添加
        }

    }

    /// <summary>
    /// CASE执行器
    /// </summary>
    public class CaseActionActuator:IDisposable,ICloneable
    {
        #region inner Class
        /// <summary>
        /// 描述执行可能所需要的附加信息(可扩展)，可以为null
        /// </summary>
        private class ExecutiveAdditionalInfo
        {
            private bool isRetry;
            private int tryTimes;
            private bool isStoping;
            private bool isTryCase;

            public ExecutiveAdditionalInfo(bool yourIstry,int yourTryTimes)
            {
                isRetry = yourIstry;
                tryTimes = yourTryTimes;
                isStoping = false;
                isTryCase = false;
            }

            public ExecutiveAdditionalInfo(bool yourStoping)
            {
                isRetry = false;
                isTryCase = false;
                tryTimes = -98;
                isStoping = yourStoping;
            }

            public ExecutiveAdditionalInfo(bool yourStoping, bool yourTryCase)
            {
                isRetry = false;
                isTryCase = yourTryCase;
                tryTimes = -98;
                isStoping = yourStoping;
            }

            public bool IsReTry
            {
                get
                {
                    return isRetry;
                }
                set
                {
                    isRetry = value;
                }
            }

            public bool IsTryCase
            {
                get
                {
                    return isTryCase;
                }
            }

            public int TryTimes
            {
                get
                {
                    return tryTimes;
                }
                set
                {
                    tryTimes = value;
                }
            }

            public bool IsStoping
            {
                get
                {
                    return isStoping;
                }
            }
        }

        /// <summary>
        /// 描述单次执行所需的基本数据集  (未使用)
        /// </summary>
        private class ExecutivebasicData
        {
            myRunCaseData<ICaseExecutionContent> runCaseData;
            TreeNode executiveNode;

            public ExecutivebasicData(myRunCaseData<ICaseExecutionContent> yourCaseData,TreeNode yourExecutiveNode)
            {
                runCaseData = yourCaseData;
                executiveNode = yourExecutiveNode;
            }
        }

        public class ActuatorStaticDataCollection : IDisposable, ICloneable
        {
            /// <summary>
            /// RunTimeParameter List
            /// </summary>
            private Dictionary<string, string> runActuatorParameterList;

            /// <summary>
            /// RunTimeStaticData List
            /// </summary>
            private Dictionary<string, IRunTimeStaticData> runActuatorStaticDataList;

            /// <summary>
            /// RunTimeDataSouce List
            /// </summary>
            private Dictionary<string, IRunTimeDataSource> runActuatorStaticDataSouceList;

            public ActuatorStaticDataCollection()
            {
                runActuatorParameterList = new Dictionary<string, string>();
                runActuatorStaticDataList = new Dictionary<string, IRunTimeStaticData>();
                runActuatorStaticDataSouceList = new Dictionary<string, IRunTimeDataSource>();
            }

            public ActuatorStaticDataCollection(Dictionary<string, string> yourActuatorParameterList, Dictionary<string, IRunTimeStaticData> yourActuatorStaticDataList, Dictionary<string, IRunTimeDataSource> yourActuatorStaticDataSouceList)
            {
                runActuatorParameterList = yourActuatorParameterList;
                runActuatorStaticDataList = yourActuatorStaticDataList;
                runActuatorStaticDataSouceList = yourActuatorStaticDataSouceList;
            }

            public Dictionary<string, string> RunActuatorParameterList
            {
                get { return runActuatorParameterList; }
            }

            public Dictionary<string, IRunTimeStaticData> RunActuatorStaticDataList
            {
                get { return runActuatorStaticDataList; }
            }

            public Dictionary<string, IRunTimeDataSource> RunActuatorStaticDataSouceList
            {
                get { return runActuatorStaticDataSouceList; }
            }
            public object Clone()
            {
                return new ActuatorStaticDataCollection(runActuatorParameterList.MyClone<string, string>(), runActuatorStaticDataList.MyClone(), runActuatorStaticDataSouceList.MyClone());
            }

            public void Dispose()
            {
                runActuatorParameterList.Clear();
                runActuatorStaticDataList.Clear();
                runActuatorStaticDataSouceList.Clear();
            }

        }

        #endregion

        /// <summary>
        /// 克隆Actuator的根
        /// </summary>
        private CaseActionActuator rootActuator;
            
        /// <summary>
        /// 执行线程同步器
        /// </summary>
        private ManualResetEvent myManualResetEvent = new ManualResetEvent(true);

        /// <summary>
        /// 执行器名称
        /// </summary>
        private string myName;

        /// <summary>
        /// Actuator State 
        /// </summary>
        private CaseActuatorState runState;


        /// <summary>
        /// case guide diver
        /// </summary>
        private myCaseRunTime caseRunTime;

        /// <summary>
        /// ExecutionDevice List with his name【执行驱动器映射表】
        /// </summary>
        private Dictionary<string, ICaseExecutionDevice> myExecutionDeviceList;

        /// <summary>
        /// RunTimeParameter List
        /// </summary>
        private Dictionary<string, string> runActuatorParameterList;

        /// <summary>
        /// RunTimeStaticData List
        /// </summary>
        private Dictionary<string, IRunTimeStaticData> runActuatorStaticDataList;

        /// <summary>
        /// RunTimeDataSouce List
        /// </summary>
        private Dictionary<string, IRunTimeDataSource> runActuatorStaticDataSouceList;

        /// <summary>
        /// Execution Result List
        /// </summary>
        private List<myExecutionDeviceResult> runExecutionResultList;

        /// <summary>
        /// RunTimeCaseDictionary
        /// </summary>
        private Dictionary<int, Dictionary<int, CaseCell>> runTimeCaseDictionary;

        /// <summary>
        /// ProjctCollection
        /// </summary>
        private ProjctCollection runCellProjctCollection;

        /// <summary>
        /// Actuator Task Thread
        /// </summary>
        private Thread myActuatorTaskThread;
        private Thread myActuatorTryThread;

        /// <summary>
        /// the thread not used and do not make it out of control
        /// </summary>
        private List<Thread> invalidThreadList;

        private string nowExecutiveData;
        private string myErrorInfo;

        private int executiveThinkTime;
        private int caseThinkTime;

        public delegate void delegateGetExecutiveDataEventHandler(string yourTitle, string yourContent);
        public delegate void delegateGetActionErrorEventHandler(string yourContent);
        public delegate void delegateGetExecutiveResultEventHandler(string sender, myExecutionDeviceResult yourResult);
        public delegate void delegateGetActuatorStateEventHandler(string sender, CaseActuatorState yourState);
        public delegate void delegateActuatorParameterListEventHandler();


        public event delegateGetExecutiveData OnGetExecutiveData;
        public event delegateGetExecutiveData OnGetActionError;
        public event delegateGetExecutiveResultEventHandler OnExecutiveResult;
        public event delegateGetActuatorStateEventHandler OnActuatorStateChanged;
        public delegateActuatorParameterListEventHandler OnActuatorParameterListChanged;  //外部需要访问 event修饰后，会禁止非创建类服务


        /// <summary>
        /// 构造函数
        /// </summary>
        public CaseActionActuator()
        {
            rootActuator = null;
            myExecutionDeviceList = new Dictionary<string, ICaseExecutionDevice>();
            runActuatorParameterList = new Dictionary<string, string>();
            runActuatorStaticDataList = new Dictionary<string, IRunTimeStaticData>();
            runActuatorStaticDataSouceList = new Dictionary<string, IRunTimeDataSource>();
            runExecutionResultList = new List<myExecutionDeviceResult>();
            invalidThreadList = new List<Thread>();
            myErrorInfo = "";
            myName = "Main Actuator";
            runState = CaseActuatorState.Stop;
            executiveThinkTime = 0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="yourName">当前执行器的名称</param>
        public CaseActionActuator(string yourName):this()
        {
            myName = yourName;
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns>克隆对象</returns>
        public object Clone()
        {
            CaseActionActuator cloneActuator = new CaseActionActuator();
            cloneActuator.rootActuator = null;
            cloneActuator.myExecutionDeviceList = myExecutionDeviceList.MyClone();
            cloneActuator.runActuatorParameterList = runActuatorParameterList.MyClone<string,string>();
            cloneActuator.runActuatorStaticDataList = runActuatorStaticDataList.MyClone();
            cloneActuator.runActuatorStaticDataSouceList = runActuatorStaticDataSouceList.MyClone();
            //cloneActuator.runExecutionResultList = new List<myExecutionDeviceResult>();
            cloneActuator.SetCaseRunTime(this.runTimeCaseDictionary, this.runCellProjctCollection);
            cloneActuator.caseThinkTime = this.caseThinkTime;
            return cloneActuator;
        }

        

        /// <summary>
        /// 获取或设置执行器标识名
        /// </summary>
        public string MyName
        {
            get
            {
                return myName;
            }
            set
            {
                myName = value;
            }
        }

        /// <summary>
        /// 获取或设置执行器的全局思考/等待时间
        /// </summary>
        public int ExecutiveThinkTime
        {
            get
            {
                return executiveThinkTime;
            }
            set
            {
                executiveThinkTime = value;
            }
        }

        /// <summary>
        /// 获取【CaseActionActuator】运行状态
        /// </summary>
        public CaseActuatorState Runstate
        {
            get
            {
                return runState;
            }
        }

        /// <summary>
        /// 获取当前任务执行进度
        /// </summary>
        public List<KeyValuePair<int ,int >> RunProgress
        {
            get
            {
                if (caseRunTime != null)
                {
                    return caseRunTime.GetNowCountProgress;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取ErrorInfo属性
        /// </summary>
        public string ErrorInfo
        {
            get
            {
                return myErrorInfo;
            }
        }

        /// <summary>
        /// 获取执行过程
        /// </summary>
        public string NowExecutiveData
        {
            get
            {
                return nowExecutiveData;

            }
        }

        /// <summary>
        /// 获取当前任务执行结果列表
        /// </summary>
        public List<myExecutionDeviceResult> NowExecutionResultList
        {
            get
            {
                return runExecutionResultList;
            }
        }

        /// <summary>
        /// 获取当前参数化数据列表
        /// </summary>
        public Dictionary<string, string> NowParameterList
        {
            get
            {
                return runActuatorParameterList;
            }
        }

        /// <summary>
        /// 获取当前静态参数化数据列表
        /// </summary>
        public Dictionary<string, IRunTimeStaticData> NowStaticDataList
        {
            get
            {
                return runActuatorStaticDataList;
            }
        }

        public Dictionary<string, IRunTimeDataSource> NowStaticDataSouceList
        {
            get
            {
                return runActuatorStaticDataSouceList;
            }
        }

        /// <summary>
        /// 获取当前执行器列表
        /// </summary>
        public Dictionary<string, ICaseExecutionDevice> NowExecutionDeviceList
        {
            get
            {
                return myExecutionDeviceList;
            }
        }

        /// <summary>
        /// 获取当前CASE列表
        /// </summary>
        public Dictionary<int, Dictionary<int, CaseCell>> RunTimeCaseDictionary
        {
            get
            {
                return runTimeCaseDictionary;
            }
        }

        /// <summary>
        /// 获取当前ProjctCollection
        /// </summary>
        public ProjctCollection RunCellProjctCollection
        {
            get
            {
                return runCellProjctCollection;
            }
        }

        /// <summary>
        /// 获取当前执行器是否填充过数据
        /// </summary>
        public bool IsActuatorDataFill
        {
            get
            {
                return ((runCellProjctCollection != null) && (runCellProjctCollection != null));
            }
        }

        /// <summary>
        /// i can updata you myRunTimeCaseDictionary()
        /// </summary>
        /// <param name="yourCaseDictionary">you myRunTimeCaseDictionary</param>
        public void UpdataRunTimeCaseDictionary(Dictionary<int, Dictionary<int, CaseCell>> yourCaseDictionary)
        {
            runTimeCaseDictionary = yourCaseDictionary;
        }

        //RunTime Queue队列变化时通知
        void caseRunTime_OnQueueChangeEvent(CaseCell yourTarget, string yourMessage)
        {
            if (yourMessage != "")
            {
                if (yourMessage.StartsWith("▲"))
                {
                    //附加任务起始节点
                    myActionActuator.SetCaseNodeExpand(yourTarget);
                }
                else if (yourMessage.StartsWith("◆"))
                {
                    //主任务起始节点
                    while(yourTarget.CaseType!=CaseType.Case)
                    {
                        if(yourTarget.IsHasChild)
                        {
                            yourTarget=yourTarget.ChildCells[0];
                        }
                        else
                        {
                            break;
                        }
                    }
                    myActionActuator.SetCaseNodeExpand(yourTarget);
                }
                yourMessage = "【" + yourMessage + "】";
            }

            myActionActuator.SetCaseNodeLoopChange(yourTarget, yourMessage);
        }

        //RunTime Queue 中Loop变化时通知
        void caseRunTime_OnLoopChangeEvent(CaseCell yourTarget, string yourMessage)
        {
            if (yourMessage!="")
            {
                myActionActuator.SetCaseNodeExpand(yourTarget);
                myActionActuator.SetCaseNodeLoopRefresh(yourTarget);
                yourMessage = "【" + yourMessage + "】";
            }
            myActionActuator.SetCaseNodeLoopChange(yourTarget, yourMessage);
        }


        /// <summary>
        /// i will load your ActionActuator (if your have another rule file ,please override or add a new realize)
        /// </summary>
        /// <param name="sourceNode">source Node</param>
        public void LoadScriptRunTime(XmlNode sourceNode)
        {
            if (sourceNode != null)
            {
                if (sourceNode.HasChildNodes)
                {
                    foreach (XmlNode tempNode in sourceNode.ChildNodes)
                    {
                        switch (tempNode.Name)
                        {
                            //此处获取默认参数化数据
                            #region RunTimeParameter
                            case "RunTimeParameter": 
                                if (tempNode.HasChildNodes)
                                {
                                    foreach (XmlNode tempNodeChild in tempNode.ChildNodes)
                                    {
                                        if (tempNodeChild.Name == "NewParameter")
                                        {
                                            if (tempNodeChild.Attributes["name"] != null)
                                            {
                                                AddRunActuatorParameter(tempNodeChild.Attributes["name"].Value, tempNodeChild.InnerText);
                                            }
                                            else
                                            {
                                                SetNowActionError(string.Format("can not find name in RunTimeStaticData - ScriptRunTime with [{0}]", tempNodeChild.InnerXml));
                                            }
                                        }
                                        else
                                        {
                                            SetNowActionError(string.Format("find unkonw data in RunTimeStaticData - ScriptRunTime with [{0}]", tempNodeChild.InnerXml));
                                        }
                                    }
                                }
                                break;
                            #endregion

                            //此处获取默认运行时数据，并使用【AddExecutionDevice】添加
                            #region RunTimeActuator
                            case "RunTimeActuator":
                                if (tempNode.HasChildNodes)
                                {
                                    string tempActuatorName = "";
                                    CaseProtocol tempActuatorProtocol = CaseProtocol.unknownProtocol;
                                    foreach (XmlNode tempNodeChild in tempNode.ChildNodes)
                                    {
                                        if (tempNodeChild.Name == "NewActuator")
                                        {
                                            if (tempNodeChild.Attributes["name"] != null && tempNodeChild.Attributes["protocol"] != null)
                                            {
                                                tempActuatorName = tempNodeChild.Attributes["name"].Value;
                                                if (!Enum.TryParse<CaseProtocol>(tempNodeChild.Attributes["protocol"].Value,out tempActuatorProtocol))
                                                {

                                                }
                                                else
                                                {
                                                    switch (tempActuatorProtocol)
                                                    {
                                                        case CaseProtocol.vanelife_http:
                                                            myConnectForVanelife_http ConnectInfo = new myConnectForVanelife_http(tempActuatorProtocol, CaseTool.getXmlInnerVaule(tempNodeChild, "dev_key"), CaseTool.getXmlInnerVaule(tempNodeChild, "dev_secret"), CaseTool.getXmlInnerVaule(tempNodeChild, "default_url"));
                                                            AddExecutionDevice(tempActuatorName, ConnectInfo);
                                                            break;
                                                        case CaseProtocol.http:
                                                            myConnectForHttp ConnectInfo_http = new myConnectForHttp(tempActuatorProtocol, CaseTool.getXmlInnerVaule(tempNodeChild, "default_url"));
                                                            AddExecutionDevice(tempActuatorName, ConnectInfo_http);
                                                            break;
                                                        default:
                                                            SetNowActionError(string.Format("find nonsupport Protocol in ScriptRunTime  with {0} ", tempActuatorName));
                                                            break;
                                                    }
                                                }

                                                /*
                                                try
                                                {
                                                    tempActuatorProtocol = (CaseProtocol)Enum.Parse(typeof(CaseProtocol), tempNodeChild.Attributes["protocol"].Value);
                                                }
                                                catch
                                                {
                                                    tempActuatorProtocol = CaseProtocol.unknownProtocol;
                                                    SetNowActionError("find unknown Protocol in ScriptRunTime - RunTimeActuator");
                                                }
                                                switch (tempActuatorProtocol)
                                                {
                                                    case CaseProtocol.vanelife_http:
                                                        myConnectForVanelife_http ConnectInfo = new myConnectForVanelife_http(tempActuatorProtocol, CaseTool.getXmlInnerVaule(tempNodeChild, "dev_key"), CaseTool.getXmlInnerVaule(tempNodeChild, "dev_secret"), CaseTool.getXmlInnerVaule(tempNodeChild, "default_url"));
                                                        AddExecutionDevice(tempActuatorName, ConnectInfo);
                                                        break;
                                                    case CaseProtocol.http:
                                                        myConnectForHttp ConnectInfo_http = new myConnectForHttp(tempActuatorProtocol, CaseTool.getXmlInnerVaule(tempNodeChild, "default_url"));
                                                        AddExecutionDevice(tempActuatorName, ConnectInfo_http);
                                                        break;
                                                    default:
                                                        SetNowActionError("find nonsupport Protocol in ScriptRunTime ");
                                                        break;
                                                }
                                                */
                                            }
                                            else
                                            {
                                                SetNowActionError("can not find name or protocol in ScriptRunTime - RunTimeActuator");
                                            }
                                        }
                                        else
                                        {
                                            SetNowActionError("find unkonw data in ScriptRunTime - RunTimeActuator");
                                        }
                                    }
                                }
                                break;
                            #endregion

                            //此处获取静态可运算参数化数据
                            #region RunTimeStaticData
                            case "RunTimeStaticData":
                                if (tempNode.HasChildNodes)
                                {
                                    foreach (XmlNode tempNodeChild in tempNode.ChildNodes)
                                    {
                                        if (tempNodeChild.Name == "NewStaticData")
                                        {
                                            if (tempNodeChild.Attributes["name"] != null && tempNodeChild.Attributes["type"] != null)
                                            {
                                                CaseStaticDataType tempType;
                                                string tempName = tempNodeChild.Attributes["name"].Value;
                                                string tempTypeStr = tempNodeChild.Attributes["type"].Value;
                                                string tempVaule = tempNodeChild.InnerText;
                                                try
                                                {
                                                    tempType = (CaseStaticDataType)Enum.Parse(typeof(CaseStaticDataType), "staticData_"+tempTypeStr);
                                                }
                                                catch
                                                {
                                                    SetNowActionError(string.Format("find unknown type in RunTimeStaticData - ScriptRunTime in [{0}] with [{1}]", tempNodeChild.InnerXml, tempTypeStr));
                                                    continue;
                                                }
                                                switch (tempType)
                                                {
                                                    case CaseStaticDataType.staticData_index:
                                                        MyStaticDataIndex tempStaticDataIndex;
                                                        string tempTypeError;
                                                        if (MyCaseDataTypeEngine.GetIndexStaticData(out tempStaticDataIndex, out tempTypeError, tempVaule))
                                                        {
                                                            runActuatorStaticDataList.MyAdd(tempName, tempStaticDataIndex);
                                                        }
                                                        else
                                                        {
                                                            //runActuatorStaticDataList.MyAdd(tempName, tempStaticDataIndex);
                                                            SetNowActionError(string.Format("find error in 【RunTimeStaticData】->【{0}】:value:【{1}】 by {2}", tempName, tempVaule, tempTypeError));
                                                        }
                                                        break;
                                                    case CaseStaticDataType.staticData_long:
                                                        MyStaticDataLong tempStaticDataLong;
                                                        if (MyCaseDataTypeEngine.GetLongStaticData(out tempStaticDataLong, out tempTypeError, tempVaule))
                                                        {
                                                            runActuatorStaticDataList.MyAdd(tempName, tempStaticDataLong);
                                                        }
                                                        else
                                                        {
                                                            //runActuatorStaticDataList.MyAdd(tempName, tempStaticDataLong);
                                                            SetNowActionError(string.Format("find error in 【RunTimeStaticData】->【{0}】:value:【{1}】 by {2}", tempName, tempVaule, tempTypeError));
                                                        }
                                                        break;
                                                    case CaseStaticDataType.staticData_random:                                              
                                                        MyStaticDataRandomStr tempStaticDataRandomStr;
                                                        if(MyCaseDataTypeEngine.GetRandomStaticData(out tempStaticDataRandomStr,out tempTypeError,tempVaule))
                                                        {
                                                            runActuatorStaticDataList.MyAdd(tempName, tempStaticDataRandomStr);
                                                        }
                                                        else
                                                        {
                                                            //runActuatorStaticDataList.MyAdd(tempName, tempStaticDataRandomStr);
                                                            SetNowActionError(string.Format("find error in 【RunTimeStaticData】->【{0}】:value:【{1}】 by {2}", tempName, tempVaule, tempTypeError));
                                                        }
                                                        break;
                                                    case CaseStaticDataType.staticData_time:
                                                        MyStaticDataNowTime tempStaticDataNowTime;
                                                        MyCaseDataTypeEngine.GetTimeStaticData(out tempStaticDataNowTime, tempVaule);
                                                        runActuatorStaticDataList.MyAdd(tempName, tempStaticDataNowTime);
                                                        break;
                                                    case CaseStaticDataType.staticData_list:
                                                        MyStaticDataList tempStaticDataList;
                                                        if (MyCaseDataTypeEngine.GetListStaticData(out tempStaticDataList, out tempTypeError, tempVaule))
                                                        {
                                                            runActuatorStaticDataList.MyAdd(tempName, tempStaticDataList);
                                                        }
                                                        else
                                                        {
                                                            //runActuatorStaticDataList.MyAdd(tempName, tempStaticDataList);
                                                            SetNowActionError(string.Format("find error in 【RunTimeStaticData】->【{0}】:value:【{1}】 by {2}", tempName, tempVaule, tempTypeError));
                                                        }
                                                        break;
                                                    default:
                                                        SetNowActionError(string.Format("find nonsupport Protocol 【{0}】 in RunTimeStaticData - ScriptRunTime ", tempType));
                                                        break;
                                                }

                                            }
                                            else
                                            {
                                                SetNowActionError(string.Format("can not find name or type in RunTimeStaticData - ScriptRunTime with [{0}]", tempNodeChild.InnerXml));
                                            }
                                        }
                                        else 
                                        {
                                            SetNowActionError(string.Format("find unkonw data in RunTimeStaticData - ScriptRunTime with [{0}]", tempNodeChild.InnerXml));
                                        }
                                    }
                                }
                                break;
                            #endregion

                            //此处获取静态可运算数据源数据
                            #region RunTimeDataSource
                            case "RunTimeDataSouce":
                                if (tempNode.HasChildNodes)
                                {
                                    foreach (XmlNode tempNodeChild in tempNode.ChildNodes)
                                    {
                                        if (tempNodeChild.Name == "NewDataSouce")
                                        {
                                            if (tempNodeChild.Attributes["name"] != null && tempNodeChild.Attributes["type"] != null)
                                            {
                                                CaseStaticDataSourceType tempType;
                                                string tempName = tempNodeChild.Attributes["name"].Value;
                                                string tempTypeStr = tempNodeChild.Attributes["type"].Value;
                                                string tempVaule = tempNodeChild.InnerText;
                                                try
                                                {
                                                    tempType = (CaseStaticDataSourceType)Enum.Parse(typeof(CaseStaticDataSourceType), "staticDataSource_" + tempTypeStr);
                                                }
                                                catch
                                                {
                                                    SetNowActionError(string.Format("find unknown type in RunTimeStaticData - ScriptRunTime in [{0}] with [{1}]", tempNodeChild.InnerXml, tempTypeStr));
                                                    continue;
                                                }
                                                switch (tempType)
                                                {
                                                    case CaseStaticDataSourceType.staticDataSource_csv:
                                                        MyStaticDataSourceCsv tempStaticDataSourceSsv;
                                                        string tempTypeError;
                                                        if (MyCaseDataTypeEngine.GetCsvStaticDataSource(out tempStaticDataSourceSsv, out tempTypeError, tempVaule))
                                                        {
                                                            runActuatorStaticDataSouceList.MyAdd<IRunTimeDataSource>(tempName, tempStaticDataSourceSsv);
                                                        }
                                                        else
                                                        {
                                                            //runActuatorStaticDataSouceList.MyAdd<IRunTimeDataSource>(tempName, tempStaticDataSourceSsv);
                                                            SetNowActionError(string.Format("find error in 【RunTimeStaticData】->【{0}】:value:【{1}】 by {2}", tempName, tempVaule, tempTypeError));
                                                        }
                                                        break;
                                                    default:
                                                        SetNowActionError(string.Format("find nonsupport Protocol 【{0}】 in RunTimeStaticData - ScriptRunTime ",tempType));
                                                        break;
                                                }

                                            }
                                            else
                                            {
                                                SetNowActionError(string.Format("can not find name or type in RunTimeStaticData - ScriptRunTime with [{0}]", tempNodeChild.InnerXml));
                                            }
                                        }
                                        else
                                        {
                                            SetNowActionError(string.Format("find unkonw data in RunTimeStaticData - ScriptRunTime with [{0}]", tempNodeChild.InnerXml));
                                        }
                                    }
                                }
                                break;
                            #endregion

                            default:
                                SetNowActionError(string.Format("find unkonw data in ScriptRunTime with [{0}]",tempNode.InnerXml));
                                break;
                        }
                    }
                }
                else
                {
                    SetNowActionError(string.Format("find error Source Nodewith [{0}]", sourceNode.InnerXml));
                }
            }
        }

        /// <summary>
        /// 连接这些器
        /// </summary>
        public void ConnectExecutionDevice()
        {
            SetNowExecutiveData("CaseExecutionDevice connecting ......");
            foreach (KeyValuePair<string, ICaseExecutionDevice> tempKvp in myExecutionDeviceList)
            {
                if (tempKvp.Value.executionDeviceConnect())
                {
                    SetNowExecutiveData(string.Format("【RunTimeActuator】:{0} 连接成功", tempKvp.Key));
                }
                else
                {
                    SetNowActionError(tempKvp.Key + "connect fail");
                }
            }
            SetNowExecutiveData("Connect complete");
        }

        /// <summary>
        /// 为执行器断开连接
        /// </summary>
        public void DisconnectExecutionDevice()
        {
            foreach (KeyValuePair<string, ICaseExecutionDevice> tempKvp in myExecutionDeviceList)
            {
                tempKvp.Value.executionDeviceClose();
            }
        }

        /// <summary>
        /// 创建任务
        /// </summary>
        private void  CreateNewActuatorTask()
        {
            //Thread myThreadTest = new Thread(new ThreadStart(ExecutiveActuatorTask),10240);
            if (myActuatorTaskThread!=null)
            {
                if(myActuatorTaskThread.IsAlive)
                {
                    invalidThreadList.Add(myActuatorTaskThread);
                    ClearInvalidThreadList();
                    SetNowActionError("Forced to terminate the residual task");
                }
            }
            myActuatorTaskThread = new Thread(new ThreadStart(ExecutiveActuatorTask));
            myActuatorTaskThread.Name = myName + "_ExecutiveActuatorTask";
            myActuatorTaskThread.Priority = ThreadPriority.Normal;
            myActuatorTaskThread.IsBackground = true;
            myActuatorTaskThread.Start();
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        private void ExecutiveActuatorTask()
        {
            ConnectExecutionDevice();
            CaseCell nowExecutiveNode = null;
            myRunCaseData<ICaseExecutionContent> nowRunCaseData = null;
            ExecutiveAdditionalInfo nowAdditionalInfo;

            while((nowExecutiveNode=caseRunTime.nextCase())!=null)
            {
                if ((nowRunCaseData = nowExecutiveNode.CaseRunData) != null)
                {
                    nowAdditionalInfo = null;
                    ExecutiveAnCases(nowRunCaseData, nowExecutiveNode,ref nowAdditionalInfo);

                    while (nowAdditionalInfo != null)
                    {
                        //Stoping
                        if (nowAdditionalInfo.IsStoping)
                        {
                            SetNowExecutiveData("操作者主动终止任务");
                            goto EndTask;
                        }
                        //ReTry
                        if(nowAdditionalInfo.IsReTry)
                        {
                            nowAdditionalInfo.IsReTry = false;
                            ExecutiveAnCases(nowRunCaseData, nowExecutiveNode,ref nowAdditionalInfo);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    //没有执行数据请处理
                    SetNowActionError("严重异常，未找到合法执行数据");

                }
            }
            EndTask:
            DisconnectExecutionDevice();
            SetRunState(CaseActuatorState.Stop);
            SetNowExecutiveData("任务已结束");
        }

        /// <summary>
        /// 创建一个定项执行任务
        /// </summary>
        /// <param name="yourTryNode"></param>
        private void CreateNewActuatorTry(CaseCell yourTryNode)
        {
            if (myActuatorTryThread != null)
            {
                if (myActuatorTryThread.IsAlive)
                {
                    invalidThreadList.Add(myActuatorTryThread);
                    ClearInvalidThreadList();
                    SetNowActionError("Forced to terminate the residual task");
                }
            }
            myActuatorTryThread = new Thread(new ParameterizedThreadStart(ExecutiveActuatorTry));
            myActuatorTryThread.Name = myName + "_ExecutiveActuatorTry";
            myActuatorTryThread.Priority = ThreadPriority.Normal;
            myActuatorTryThread.IsBackground = true;
            myActuatorTryThread.Start(yourTryNode);
        }

        /// <summary>
        /// 执行一次定项测试
        /// </summary>
        private void ExecutiveActuatorTry(object yourTryNode)
        {
            ConnectExecutionDevice();
            CaseCell nowExecutiveNode = (CaseCell)yourTryNode;
            myRunCaseData<ICaseExecutionContent> nowRunCaseData = nowExecutiveNode.CaseRunData;
            ExecutiveAdditionalInfo nowAdditionalInfo;

            if (nowRunCaseData != null)
            {
                nowAdditionalInfo = new ExecutiveAdditionalInfo(false, true);
                ExecutiveAnCases(nowRunCaseData, nowExecutiveNode, ref nowAdditionalInfo);
            }
            else
            {
                //没有执行数据请处理
                SetNowActionError("严重异常，未找到合法执行数据");
            }

            DisconnectExecutionDevice();
            SetRunState(CaseActuatorState.Stop);
            SetNowExecutiveData("定项执行完成");
        }

        /// <summary>
        /// 执行指定的Case
        /// </summary>
        /// <param name="nowRunCaseData">myRunCaseData</param>
        /// <param name="nowExecutiveNode">now CaseCell</param>
        private void ExecutiveAnCases(myRunCaseData<ICaseExecutionContent> nowRunCaseData, CaseCell nowExecutiveNode, ref ExecutiveAdditionalInfo nowAdditionalInfo)
        {
            bool tempIsBreakError = false;
            myExecutionDeviceResult executionResult;

            if(runState==CaseActuatorState.Pause)
            {
                myActionActuator.SetCaseNodePause(nowExecutiveNode);
            }
            myManualResetEvent.WaitOne();
            if (runState == CaseActuatorState.Stoping)
            {
                nowAdditionalInfo = new ExecutiveAdditionalInfo(true);
                myActionActuator.SetCaseNodeStop(nowExecutiveNode);
                return;
            }

            if (nowRunCaseData.errorMessages == null)
            {
                if (myExecutionDeviceList.ContainsKey(nowRunCaseData.testContent.myCaseActuator))
                {
                    var nowDevice = myExecutionDeviceList[nowRunCaseData.testContent.myCaseActuator];
                ExecutionDeviceRunLink:
                    if (nowDevice.isDeviceConnect)
                    {
                        //nowDevice.executionDeviceRun()
                        myActionActuator.SetCaseNodeRunning(nowExecutiveNode);
                        executionResult = nowDevice.executionDeviceRun(nowRunCaseData.testContent, OnGetExecutiveData, myName, runActuatorParameterList, runActuatorStaticDataList, runActuatorStaticDataSouceList, nowRunCaseData.id);
                        HandleCaseExecutiveResul(nowRunCaseData, nowExecutiveNode, executionResult,ref nowAdditionalInfo);

                    }
                    else
                    {
                        //Device没有连接
                        SetNowExecutiveData(string.Format("【ID:{0}】 {1}连接中断，尝试连接中···", nowRunCaseData.id, nowRunCaseData.testContent.myCaseActuator));
                        if (nowDevice.executionDeviceConnect())
                        {
                            //nowDevice.executionDeviceRun()
                            goto ExecutionDeviceRunLink;
                        }
                        else
                        {
                            SetNowExecutiveData(string.Format("【ID:{0}】 {1}连接失败", nowRunCaseData.id, nowRunCaseData.testContent.myCaseActuator));
                            myActionActuator.SetCaseNodeConnectInterrupt(nowExecutiveNode);

                            tempIsBreakError = true;
                            executionResult = new myExecutionDeviceResult(nowRunCaseData.id, "CaseActuator连接失败");
                        }
                    }
                }
                else
                {
                    //testContent没有找到合适的myCaseActuator
                    SetNowExecutiveData(string.Format("【ID:{0}】 未找到指定CaseActuator", nowRunCaseData.id));
                    myActionActuator.SetCaseNodeNoActuator(nowExecutiveNode);
                    myActionActuator.SetCaseNodeContentWarning(nowExecutiveNode);

                    tempIsBreakError = true;
                    executionResult = new myExecutionDeviceResult(nowRunCaseData.id, "未找到指定CaseActuator");
                }
            }
            else
            {
                //nowRunCaseData有错误
                SetNowActionError(string.Format("【ID:{0}】 执行数据脚本存在错误", nowRunCaseData.id));
                myActionActuator.SetCaseNodeAbnormal(nowExecutiveNode);

                tempIsBreakError = true;
                executionResult = new myExecutionDeviceResult(nowRunCaseData.id, "执行数据脚本存在错误" + nowRunCaseData.errorMessages);
            }

            //AddExecutionResult
            AddExecutionResult(executionResult);

            //Sleep
            if (!tempIsBreakError)
            {
                int tempSleepTime = executiveThinkTime + caseThinkTime;
                if(tempSleepTime>0)
                {
                    SetNowExecutiveData(string.Format("sleep {0} ms···", tempSleepTime));
                    Thread.Sleep(tempSleepTime);
                }
            }

            //nowExecutiveNode.TreeView.Invoke(new delegateBasicAnonymous(() => { }));
            //nowExecutiveNode.TreeView.Invoke(new delegateBasicAnonymous(() => nowExecutiveNode.BackColor = System.Drawing.Color.LightSkyBlue));  
           
        }


        /// <summary>
        /// 处理断言，及结果封装.用于【ExecutiveActuatorTask】
        /// </summary>
        /// <param name="yourRunData">确保其不为null</param>
        /// <param name="yourExecutionResult">确保其不为null</param>
        /// <param name="nowAdditionalInfo"></param>
        private void HandleCaseExecutiveResul(myRunCaseData<ICaseExecutionContent> yourRunData, CaseCell nowExecutiveNode, myExecutionDeviceResult yourExecutionResult, ref ExecutiveAdditionalInfo nowAdditionalInfo)
        {
            string tempError;
            yourExecutionResult.caseId = yourRunData.id;
            if (yourExecutionResult.additionalEroor != null)
            {
                myActionActuator.SetCaseNodeContentWarning(nowExecutiveNode);
            }
            yourExecutionResult.expectMethod = yourRunData.caseExpectInfo.myExpectType;
            yourExecutionResult.expectContent = yourRunData.caseExpectInfo.myExpectContent.getTargetContentData(runActuatorParameterList, runActuatorStaticDataList, runActuatorStaticDataSouceList,yourExecutionResult.staticDataResultCollection, out tempError);
            if (tempError != null)
            {
                myActionActuator.SetCaseNodeContentWarning(nowExecutiveNode);
                yourExecutionResult.additionalEroor = yourExecutionResult.additionalEroor.MyAddValue(tempError);
            }
            if (CaseTool.CheckBackData(yourExecutionResult.backContent, yourExecutionResult.expectContent, yourRunData.caseExpectInfo.myExpectType))
            {
                yourExecutionResult.result = CaseResult.Pass;
                myActionActuator.SetCaseNodePass(nowExecutiveNode);
            }
            else
            {
                yourExecutionResult.result = CaseResult.Fail;
                myActionActuator.SetCaseNodeFial(nowExecutiveNode);
            }

            #region ParameterSaves
            if(yourRunData.caseAttribute.myParameterSaves!=null)
            {
                foreach (ParameterSave tempParameterSave in yourRunData.caseAttribute.myParameterSaves)
                {
                    string tempPickVaule = null;
                    switch (tempParameterSave.parameterFunction)
                    {
                        case PickOutFunction.pick_json:
                            tempPickVaule = CaseTool.PickJsonParameter(tempParameterSave.parameterFindVaule, yourExecutionResult.backContent);
                            break;
                        case PickOutFunction.pick_str:
                            string tempFindVaule;
                            int tempLen;
                            CaseTool.GetStrPickData(tempParameterSave.parameterFindVaule,out tempFindVaule,out tempLen);
                            if (tempFindVaule!=null)
                            {
                                tempPickVaule = CaseTool.PickStrParameter(tempFindVaule, tempLen , yourExecutionResult.backContent);
                            }
                            else
                            {
                                tempError = string.Format("【ID:{0}】ParameterSave 脚本数据不合法", yourRunData.id);
                                yourExecutionResult.additionalRemark = yourExecutionResult.additionalRemark.MyAddValue(tempError);
                                SetNowActionError(tempError);
                                myActionActuator.SetCaseNodeContentWarning(nowExecutiveNode);
                            }
                            
                            break;
                        case PickOutFunction.pick_xml:
                            tempPickVaule = CaseTool.PickXmlParameter(tempParameterSave.parameterFindVaule, yourExecutionResult.backContent);
                            break;
                        default:
                            tempError = string.Format("【ID:{0}】 ParameterSave 暂不支持该数据提取方式", yourRunData.id);
                            yourExecutionResult.additionalRemark = yourExecutionResult.additionalRemark.MyAddValue(tempError);
                            SetNowActionError(tempError);
                            myActionActuator.SetCaseNodeContentWarning(nowExecutiveNode);
                            break;
                    }
                    if(tempPickVaule==null)
                    {
                        SetNowActionError(string.Format("【ID:{0}】 ParameterSave 在执行结果中未找到指定参数", yourRunData.id));
                    }
                    else
                    {
                        AddRunActuatorParameter(tempParameterSave.parameterName, tempPickVaule);
                    }
                }
            }
            #endregion

            #region actions
            if (yourRunData.actions != null)
            {
                if (yourRunData.actions.Keys.Contains(yourExecutionResult.result))
                {
                    switch (yourRunData.actions[yourExecutionResult.result].caseAction)
                    {
                        case CaseAction.action_alarm:
                            if (yourRunData.actions[yourExecutionResult.result].addInfo != null)
                            {
                                VoiceService.Speak(yourRunData.actions[yourExecutionResult.result].addInfo);
                                SetNowExecutiveData("【action_alarm】");
                            }
                            else
                            {
                                VoiceService.Beep();
                            }
                            break;
                        case CaseAction.action_continue:
                            //do nothing
                            break;
                        case CaseAction.action_goto:
                            if (nowAdditionalInfo != null)
                            {
                                //定项不执行goto
                                if(nowAdditionalInfo.IsTryCase)
                                {
                                    break;
                                }
                            }
                            if (yourRunData.actions[yourExecutionResult.result].addInfo == null)
                            {
                                tempError = string.Format("【ID:{0}】 CaseAction Case数据中部没有发现目的ID", yourRunData.id);
                                yourExecutionResult.additionalRemark = yourExecutionResult.additionalRemark.MyAddValue(tempError);
                                SetNowActionError(tempError);
                                myActionActuator.SetCaseNodeContentWarning(nowExecutiveNode);
                            }
                            else
                            {
                                int tempCaseID;
                                int tempProjectID;
                                if (CaseTool.getTargetCaseID(yourRunData.actions[yourExecutionResult.result].addInfo, out tempProjectID, out tempCaseID))
                                {
                                    if (caseRunTime.gotoMyCase(tempProjectID, tempCaseID, runTimeCaseDictionary ))
                                    {
                                        SetNowExecutiveData("【action_goto】");
                                        yourExecutionResult.additionalRemark = yourExecutionResult.additionalRemark.MyAddValue(string.Format("【action_goto】触发，已经跳转到Project：{0}  Case：{1}", tempProjectID, tempCaseID));
                                    }
                                    else
                                    {
                                        tempError = string.Format("【ID:{0}】action_goto跳转任务未成功", yourRunData.id);
                                        yourExecutionResult.additionalRemark = yourExecutionResult.additionalRemark.MyAddValue(tempError);
                                        SetNowActionError(tempError);
                                    }
                                }
                                else
                                {
                                    tempError = string.Format("【ID:{0}】 CaseAction 目标跳转Case不合法", yourRunData.id);
                                    yourExecutionResult.additionalRemark = yourExecutionResult.additionalRemark.MyAddValue(tempError);
                                    SetNowActionError(tempError);
                                    myActionActuator.SetCaseNodeContentWarning(nowExecutiveNode);
                                }
                            }
                            break;
                        case CaseAction.action_retry:
                            if (nowAdditionalInfo != null)
                            {
                                //定项不执行goto
                                if (nowAdditionalInfo.IsTryCase)
                                {
                                    break;
                                }
                            }
                            if (yourRunData.actions[yourExecutionResult.result].addInfo != null)
                            {
                                try
                                {
                                    int tempTryTimes = int.Parse(yourRunData.actions[yourExecutionResult.result].addInfo);
                                    if (tempTryTimes > 0)
                                    {
                                        if (nowAdditionalInfo == null)
                                        {
                                            nowAdditionalInfo = new ExecutiveAdditionalInfo(true, tempTryTimes);
                                            if (nowAdditionalInfo.TryTimes > 0)
                                            {
                                                SetNowExecutiveData("【action_retry】将被触发");
                                            }
                                            else
                                            {
                                                nowAdditionalInfo.IsReTry = false;
                                            }
                                        }
                                        else
                                        {
                                            nowAdditionalInfo.TryTimes--;
                                            yourExecutionResult.additionalRemark += string.Format("retry: {0}/{1}", tempTryTimes, tempTryTimes - nowAdditionalInfo.TryTimes);
                                            if(  nowAdditionalInfo.TryTimes > 0)
                                            {
                                                nowAdditionalInfo.IsReTry = true;
                                            }
                                        }
                                    }

                                }
                                catch
                                {
                                    tempError = string.Format("【ID:{0}】 retry 解析错误", yourRunData.id);
                                    yourExecutionResult.additionalRemark = yourExecutionResult.additionalRemark.MyAddValue(tempError);
                                    SetNowActionError(tempError);
                                    myActionActuator.SetCaseNodeContentWarning(nowExecutiveNode);
                                }
                            }
                            else
                            {
                                if (nowAdditionalInfo == null)
                                {
                                    nowAdditionalInfo = new ExecutiveAdditionalInfo(true, -99);
                                }
                                else
                                {
                                    yourExecutionResult.additionalRemark += "【action_retry】 always";
                                    nowAdditionalInfo.IsReTry = true;
                                }
                            }
                            break;
                        case CaseAction.action_stop:
                            PauseCaseScript();
                            break;
                        case CaseAction.action_unknow:
                            tempError = string.Format("【ID:{0}】 CaseAction 未能解析", yourRunData.id);
                            yourExecutionResult.additionalRemark = yourExecutionResult.additionalRemark.MyAddValue(tempError);
                            SetNowActionError(tempError);
                            myActionActuator.SetCaseNodeContentWarning(nowExecutiveNode);
                            break;
                        default:
                            //do nothing
                            break;

                    }
                }
            }
            #endregion

            #region Sleep
            if(yourRunData.caseAttribute.attributeDelay>0)
            {
                caseThinkTime = yourRunData.caseAttribute.attributeDelay;
            }
            else
            {
                if(caseThinkTime!=0)
                {
                    caseThinkTime = 0;
                }
            }
            #endregion
           
        }

        /// <summary>
        /// 重置ErrorInfo
        /// </summary>
        public void ResetErrorInfo()
        {
            myErrorInfo = "";
        }

        /// <summary>
        /// 重置NowExecutiveData
        /// </summary>
        public void ResetNowExecutiveData()
        {
            nowExecutiveData = "";
        }

        /// <summary>
        /// 触发【OnGetExecutiveData】
        /// </summary>
        /// <param name="yourContent"></param>
        private void SetNowExecutiveData(string yourContent)
        {
            if (OnGetExecutiveData != null)
            {
                this.OnGetExecutiveData(myName, yourContent);
            }
        }

        /// <summary>
        /// 设置nowExecutiveData 及触发【OnGetExecutiveData】
        /// </summary>
        /// <param name="yourContent"></param>
        private void SetAndSaveNowExecutiveData(string yourContent)
        {
            nowExecutiveData = yourContent;
            if (OnGetExecutiveData != null)
            {
                this.OnGetExecutiveData(myName, yourContent);
            }
        }

        /// <summary>
        /// 触发【OnGetActionError】
        /// </summary>
        /// <param name="yourContent">Action Error Content</param>
        private void SetNowActionError(string yourContent)
        {
            if (OnGetActionError != null)
            {
                this.OnGetActionError(myName, yourContent);
            }
        }

        /// <summary>
        ///  设置 myErrorInfo 并 触发【OnGetActionError】（若不想触发OnGetActionError请直接操作myErrorInfo）
        /// </summary>
        /// <param name="yourContent">Action Error Content</param>
        private void SetAndSaveNowActionError(string yourContent)
        {
            myErrorInfo = yourContent;
            if (OnGetActionError != null)
            {
                this.OnGetActionError(myName, myErrorInfo);
            }
        }

        /// <summary>
        /// 设置 runState 并 触发【OnActuatorStateChanged】
        /// </summary>
        /// <param name="yourStae"></param>
        private void SetRunState(CaseActuatorState yourStae)
        {
            runState = yourStae;
            if(OnActuatorStateChanged!=null)
            {
                this.OnActuatorStateChanged(myName, yourStae);
            }
        }

        /// <summary>
        /// 添加执行结果到结果集并触发【OnExecutiveResult】
        /// </summary>
        /// <param name="yourExecutionResult">your ExecutionResult</param>
        private void AddExecutionResult(myExecutionDeviceResult yourExecutionResult)
        {
            runExecutionResultList.Add(yourExecutionResult);
            if (OnExecutiveResult != null)
            {
                this.OnExecutiveResult(myName, yourExecutionResult);
            }
        }

        /// <summary>
        /// 添加ExecutionDevice
        /// </summary>
        /// <param name="yourDeviceConnectInfo"></param>
        /// <returns></returns>
        private bool AddExecutionDevice(string yourDeviceName, IConnectExecutiveData yourDeviceConnectInfo)
        {
            switch (yourDeviceConnectInfo.myCaseProtocol)
            {
                case CaseProtocol.vanelife_http:
                    myExecutionDeviceList.MyAdd(yourDeviceName, new CaseProtocolExecutionForVanelife_http((myConnectForVanelife_http)yourDeviceConnectInfo));
                    break;
                case CaseProtocol.http:
                     myExecutionDeviceList.MyAdd(yourDeviceName, new CaseProtocolExecutionForHttp((myConnectForHttp)yourDeviceConnectInfo));
                    break;
                default:
                    SetNowActionError(yourDeviceName + " is an nonsupport Protocol");
                    break;
            }
            return true;
        }

        /// <summary>
        /// 添加或修改【runActuatorParameterList】
        /// </summary>
        /// <param name="yourParameterName"> Parameter Name</param>
        /// <param name="yourParameterVaule">Parameter Vaule</param>
        public void AddRunActuatorParameter(string yourParameterName, string yourParameterVaule)
        {
            runActuatorParameterList.MyAdd(yourParameterName, yourParameterVaule);
            if (OnActuatorParameterListChanged!=null)
            {
                this.OnActuatorParameterListChanged();
            }
        }

        /// <summary>
        /// 设置 【case guide diver】
        /// </summary>
        /// <param name="yourCaseDictionary">your Case ID list</param>
        public void SetCaseRunTime(Dictionary<int, Dictionary<int, CaseCell>> yourCaseDictionary, ProjctCollection yourProjctCollection)
        {
            if (yourCaseDictionary != null && yourProjctCollection!=null)
            {
                runTimeCaseDictionary = yourCaseDictionary;
                runCellProjctCollection = yourProjctCollection;
                caseRunTime = new myCaseRunTime();
                caseRunTime.OnLoopChangeEvent += caseRunTime_OnLoopChangeEvent;
                caseRunTime.OnQueueChangeEvent += caseRunTime_OnQueueChangeEvent;
            }
            else
            {
                SetNowActionError("your CaseDictionary or ProjctCollection is null");
            }
        }

        /// <summary>
        /// 获取执行器是否可以执行
        /// </summary>
        /// <returns>is ok</returns>
        private bool IsActionActuatorCanRun(CaseCell yourStartNode)
        {
            if (runCellProjctCollection == null)
            {
                SetAndSaveNowActionError("your CellProjctCollection is null");
                return false;
            }
            if (runTimeCaseDictionary==null)
            {
                SetAndSaveNowActionError("your RunTimeCaseDictionary is null");
                return false;
            }
            if (caseRunTime == null)
            {
                SetAndSaveNowActionError("your CaseRuntime is null");
                return false;
            }
            if (myExecutionDeviceList.Count == 0)
            {
                SetAndSaveNowActionError("can not find any ExecutionDevice");
                return false;
            }
            if (yourStartNode==null)
            {
                SetAndSaveNowActionError("your StartNode is null");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 执行项目任务
        /// </summary>
        /// <param name="yourStartNode">起始节点</param>
        /// <returns>Success</returns>
        public bool RunCaseScript(CaseCell yourStartNode)
        {
            switch (runState)
            {
                case CaseActuatorState.Running:
                    SetAndSaveNowActionError("当前任务还未结束");
                    return false;
                case CaseActuatorState.Stoping:
                    SetAndSaveNowActionError("当前任务正在终止中");
                    return false;
                case  CaseActuatorState.Pause:
                    SetRunState(CaseActuatorState.Running);
                    myManualResetEvent.Set();
                    SetNowExecutiveData("任务恢复");
                    return true;
                case CaseActuatorState.Stop:
                    if (yourStartNode == null)
                    {
                        SetAndSaveNowActionError("未发现任何可用节点");
                        return false;
                    }
                    else if (!IsActionActuatorCanRun(yourStartNode))
                    {
                        return false;
                    }
                    caseRunTime.readyStart(yourStartNode);
                    runExecutionResultList.Clear();
                    SetRunState(CaseActuatorState.Running);
                    myManualResetEvent.Set();
                    CreateNewActuatorTask();
                    SetNowExecutiveData("任务开始");
                    return true;
                case CaseActuatorState.Trying:
                    SetAndSaveNowActionError("存在未还未结束指定项任务");
                    return false;
                default:
                    return false;
            }
            
        }

        /// <summary>
        /// 暂停当前项目任务（可恢复）
        /// </summary>
        /// <returns>Success</returns>
        public bool PauseCaseScript()
        {
            if (runState == CaseActuatorState.Running)
            {
                myManualResetEvent.Reset();
                SetRunState(CaseActuatorState.Pause);
                SetNowExecutiveData("任务已暂停");
                return true;
            }
            else
            {
                SetAndSaveNowActionError("未发现处于运行状态中的任务");
                return false;
            }
        }

        /// <summary>
        /// 停止项目（不可恢复）
        /// </summary>
        /// <returns></returns>
        public bool StopCaseScript()
        {
            if (runState == CaseActuatorState.Running)
            {
                SetRunState(CaseActuatorState.Stoping);
                SetNowExecutiveData("正在终止任务");
                return true;
            }
            else if (runState == CaseActuatorState.Pause)
            {
                myManualResetEvent.Set();
                SetRunState(CaseActuatorState.Stoping);
                SetNowExecutiveData("正在终止任务");
                return true;
            }
            else if (runState == CaseActuatorState.Stoping)
            {
                SetAndSaveNowActionError("正在终止任务");
                return false;
            }
            else
            { 
                SetAndSaveNowActionError("当前项目已经停止");
                return false;
            }
        }

        /// <summary>
        /// 单步执行项目成员（必须在Pause状态，即时不在Pause状态，也会先进行Pause）
        /// </summary>
        /// <param name="yourNode">当前项目(如果项目已经开始可以为null)</param>
        /// <returns>Success</returns>
        public bool TryNextCaseScript(CaseCell yourNode)
        {
            if (runState == CaseActuatorState.Running)
            {
                PauseCaseScript();
                myManualResetEvent.Set();
                SetNowExecutiveData("单步执行>");
                myManualResetEvent.Reset();
                return true;

            }
            else if (runState == CaseActuatorState.Stop)
            {
                if (RunCaseScript(yourNode))
                {
                    PauseCaseScript();
                    SetNowExecutiveData("单步执行>");
                    myManualResetEvent.Set();
                    myManualResetEvent.Reset();
                    return true;
                }
                else
                {
                    SetAndSaveNowActionError("无法进行单步执行");
                    return false;
                }
            }
            else if (runState == CaseActuatorState.Pause)
            {
                SetNowExecutiveData("单步执行>");
                myManualResetEvent.Set();
                myManualResetEvent.Reset();
                return true;
            }
            else if (runState == CaseActuatorState.Running)
            {
                SetAndSaveNowActionError("正在结束项目，无法进行单步执行");
                return false;
            }
            else if (runState == CaseActuatorState.Trying)
            {
                SetAndSaveNowActionError("存在正在执行指定项任务");
                return false;
            }
            return false;
        }

        /// <summary>
        /// 定项执行指定项（一直执行同一条CASE，仅在项目停止后可以使用，且goto及retry在这种任务中无效）
        /// </summary>
        /// <param name="yourNode">定项 数据</param>
        /// <returns>is Success</returns>
        public bool TryNowCaseScript(CaseCell yourNode)
        {
            if (runState == CaseActuatorState.Stop)
            {
                SetRunState(CaseActuatorState.Trying);
                myManualResetEvent.Set();
                CreateNewActuatorTry(yourNode);
                return true;
            }
            else if (runState == CaseActuatorState.Trying)
            {
                SetAndSaveNowActionError("上一个指定项任务还未结束");
                return false;
            }
            else
            {
                SetAndSaveNowActionError("要进行定向执行前，必须先停止任务");
                return false;
            }
        }

        /// <summary>
        /// 强制关闭所有正在执行的任务（谨慎调用）
        /// </summary>
        public void KillAll()
        {
            if (myActuatorTaskThread != null)
            {
                if (myActuatorTaskThread.IsAlive)
                {
                    myActuatorTaskThread.Abort();
                    DisconnectExecutionDevice();
                    SetRunState(CaseActuatorState.Stop);
                }
            }
            if (myActuatorTryThread != null)
            {
                if (myActuatorTryThread.IsAlive)
                {
                    myActuatorTryThread.Abort();
                    DisconnectExecutionDevice();
                    SetRunState(CaseActuatorState.Stop);
                }
            }

            ClearInvalidThreadList();
        }

        /// <summary>
        /// 清理无效线程列表
        /// </summary>
        private void ClearInvalidThreadList()
        {
            for (int i = invalidThreadList.Count - 1; i >= 0; i--)
            {
                ErrorLog.PutInLog(string.Format("fing InvalidThread Name:{0}  State:{1}" , invalidThreadList[i].Name  ,invalidThreadList[i].ThreadState.ToString()));
                if (invalidThreadList[i].IsAlive)
                {
                    invalidThreadList[i].Abort();
                }
                else
                {
                    invalidThreadList.Remove(invalidThreadList[i]);
                }
            }
            for (int i = invalidThreadList.Count - 1; i >= 0; i--)
            {
                if (!invalidThreadList[i].IsAlive)
                {
                    invalidThreadList.Remove(invalidThreadList[i]);
                }
            }
        }

        /// <summary>
        /// 实现【IDisposable】强烈建议结束前调用（即使当前可用使用disconnectExecutionDevice替代）
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if(disposing)
            {
                KillAll();
                DisconnectExecutionDevice();
                myExecutionDeviceList.Clear();
                runActuatorParameterList.Clear();
                runActuatorStaticDataList.Clear();
                runActuatorStaticDataSouceList.Clear();
                runTimeCaseDictionary = null;
                runCellProjctCollection = null;
                if (caseRunTime != null)
                {
                    caseRunTime.OnLoopChangeEvent -= caseRunTime_OnLoopChangeEvent;
                    caseRunTime.OnQueueChangeEvent -= caseRunTime_OnQueueChangeEvent;
                    caseRunTime = null;
                }
            }
            else
            {
                //Clean up the native resources
            }
        }

        //如果没有非托管资源的释放，需谨慎添加析构函数，其可能影响GC性能
        //~CaseActionActuator()
        //{
        //    Dispose(false);
        //}
    }
}
