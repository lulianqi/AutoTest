using CaseExecutiveActuator.CaseActuator;
using CaseExecutiveActuator.CaseActuator.ExecutionDevice;
using CaseExecutiveActuator.Cell;
using CaseExecutiveActuator.Tool;
using MyCommonHelper;
using MyCommonHelper.NetHelper;
using MyVoiceHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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


namespace CaseExecutiveActuator.CaseActuator
{
    

    /// <summary>
    /// ActuatorStaticData 集合
    /// </summary>
    public class ActuatorStaticDataCollection : IDisposable, ICloneable
    {
        /// <summary>
        /// RunTimeParameter List
        /// </summary>
        private Dictionary<string, string> runActuatorStaticDataKeyList;

        /// <summary>
        /// RunTimeStaticData List
        /// </summary>
        private Dictionary<string, IRunTimeStaticData> runActuatorStaticDataParameterList;

        /// <summary>
        /// RunTimeDataSouce List
        /// </summary>
        private Dictionary<string, IRunTimeDataSource> runActuatorStaticDataSouceList;

        private CaseActionActuator innerCaseActionActuator;

        private readonly object padlock = new object();

        public ActuatorStaticDataCollection()
        {
            runActuatorStaticDataKeyList = new Dictionary<string, string>();
            runActuatorStaticDataParameterList = new Dictionary<string, IRunTimeStaticData>();
            runActuatorStaticDataSouceList = new Dictionary<string, IRunTimeDataSource>();
        }

        public ActuatorStaticDataCollection(Dictionary<string, string> yourActuatorParameterList, Dictionary<string, IRunTimeStaticData> yourActuatorStaticDataList, Dictionary<string, IRunTimeDataSource> yourActuatorStaticDataSouceList)
        {
            runActuatorStaticDataKeyList = yourActuatorParameterList;
            runActuatorStaticDataParameterList = yourActuatorStaticDataList;
            runActuatorStaticDataSouceList = yourActuatorStaticDataSouceList;
        }

        /// <summary>
        /// set inner CaseActionActuator that this ActuatorStaticDataCollectionwill use in the CaseActionActuator and when ActuatorStaticDataCollectionwill change will call delegateActuatorParameterListEventHandler
        /// </summary>
        /// <param name="yourCaseActionActuator"> your CaseActionActuator</param>
        public void SetCaseActionActuator(CaseActionActuator yourCaseActionActuator)
        {
            innerCaseActionActuator = yourCaseActionActuator;
        }

        private object IsHasSameKey(string key, int ignoreListIndex)
        {
            if (runActuatorStaticDataKeyList.ContainsKey(key) && ignoreListIndex!=1)
            {
                return runActuatorStaticDataKeyList;
            }
            if (runActuatorStaticDataParameterList.ContainsKey(key) && ignoreListIndex != 2)
            {
                return runActuatorStaticDataParameterList;
            }
            if (runActuatorStaticDataSouceList.ContainsKey(key) && ignoreListIndex != 3)
            {
                return runActuatorStaticDataSouceList;
            }
            return null;
        }

        private void OnListChanged()
        {
            if(innerCaseActionActuator!=null)
            {
                if(innerCaseActionActuator.OnActuatorParameterListChanged!=null)
                {
                    innerCaseActionActuator.OnActuatorParameterListChanged();
                }
            }
        }

        public Dictionary<string, string> RunActuatorStaticDataKeyList
        {
            get { return runActuatorStaticDataKeyList; }
        }

        public Dictionary<string, IRunTimeStaticData> RunActuatorStaticDataParameterList
        {
            get { return runActuatorStaticDataParameterList; }
        }

        public Dictionary<string, IRunTimeDataSource> RunActuatorStaticDataSouceList
        {
            get { return runActuatorStaticDataSouceList; }
        }

        /// <summary>
        /// Is the StaticDataCollection has th same key name 
        /// </summary>
        /// <param name="yourKey">your Key</param>
        /// <returns>is has </returns>
        public bool IsHaveSameKey(string yourKey)
        {
            return (IsHasSameKey(yourKey, 0) != null) ;
        }

        /// <summary>
        /// Add Data into runActuatorStaticDataKeyList (if DataParameter or DataSouce has same key retrun false , if DataKey has same key cover the vaule)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="vaule">vaule</param>
        /// <returns>is success</returns>
        [MethodImplAttribute(MethodImplOptions.Synchronized)] 
        public bool AddStaticDataKey(string key, string vaule)
        {
            if(IsHasSameKey(key,1)!=null)
            {
                if (!RemoveStaticData(key,false))
                {
                    return false;
                }
            }
            runActuatorStaticDataKeyList.MyAdd(key, vaule);
            OnListChanged();
            return true;
        }

        /// <summary>
        /// Add Data into runActuatorStaticDataParameterList (if DataKey or DataSouce has same key retrun false , if DataParameter has same key cover the vaule) 
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="vaule">vaule</param>
        /// <returns>is success</returns>
        [MethodImplAttribute(MethodImplOptions.Synchronized)] 
        public bool AddStaticDataParameter(string key, IRunTimeStaticData vaule)
        {
            if(IsHasSameKey(key,2)!=null)
            {
                if (!RemoveStaticData(key, false))
                {
                    return false;
                }
            }
            runActuatorStaticDataParameterList.MyAdd<IRunTimeStaticData>(key, vaule);
            OnListChanged();
            return true;
        }

        /// <summary>
        /// Add Data into runActuatorStaticDataSouceList (if DataKey or DataParameter has same key retrun false , if DataSouce has same key cover the vaule)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="vaule">vaule</param>
        /// <returns>is success</returns>
        [MethodImplAttribute(MethodImplOptions.Synchronized)] 
        public bool AddStaticDataSouce(string key, IRunTimeDataSource vaule)
        {
            if (IsHasSameKey(key, 3) != null)
            {
                if (!RemoveStaticData(key, false))
                {
                    return false;
                }
            }
            runActuatorStaticDataSouceList.MyAdd<IRunTimeDataSource>(key, vaule);
            OnListChanged();
            return true;
        }

        /// <summary>
        /// Remove Static Data in any list (if there not has any same key retrun false)
        /// </summary>
        /// <param name="key">key or Regex</param>
        /// <param name="isRegex">is use Regex</param>
        /// <returns>is success</returns>
        [MethodImplAttribute(MethodImplOptions.Synchronized)] 
        public bool RemoveStaticData(string key, bool isRegex)
        {
            if (!isRegex)
            {
                var tempDataList = IsHasSameKey(key, 0);
                if(tempDataList==null)
                {
                    return false;
                }
                else if (tempDataList == runActuatorStaticDataKeyList)
                {
                    runActuatorStaticDataKeyList.Remove(key);
                }
                else if (tempDataList == runActuatorStaticDataParameterList)
                {
                    runActuatorStaticDataParameterList.Remove(key);
                }
                else if (tempDataList == runActuatorStaticDataSouceList)
                {
                    runActuatorStaticDataSouceList.Remove(key);
                }
                else
                {
                    ErrorLog.PutInLog(string.Format("error to [RemoveStaticData] in ActuatorStaticDataCollection  the key is {0} ",key));
                    return false;
                }
            }
            else
            {
                try
                {
                    bool isFindAndRegexKey = false;
                    System.Text.RegularExpressions.Regex sr;
                    sr = new System.Text.RegularExpressions.Regex(key, System.Text.RegularExpressions.RegexOptions.None);
                    List<string> dataToDel = new List<string>();

                    foreach(var tempKey in runActuatorStaticDataKeyList.Keys)
                    {
                        if(sr.IsMatch(tempKey))
                        {
                            dataToDel.Add(tempKey);
                        }
                    }
                    foreach (string tempKey in dataToDel)
                    {
                        runActuatorStaticDataKeyList.Remove(tempKey);
                    }
                    if(dataToDel.Count>0)
                    {
                        isFindAndRegexKey = true;
                        dataToDel.Clear();
                    }
                    
                    foreach (var tempKey in runActuatorStaticDataParameterList.Keys)
                    {
                        if (sr.IsMatch(tempKey))
                        {
                            dataToDel.Add(tempKey);
                        }
                    }
                    foreach (string tempKey in dataToDel)
                    {
                        runActuatorStaticDataParameterList.Remove(tempKey);
                    }
                    if (dataToDel.Count > 0)
                    {
                        isFindAndRegexKey = true;
                        dataToDel.Clear();
                    }

                    foreach (var tempKey in runActuatorStaticDataSouceList.Keys)
                    {
                        if (sr.IsMatch(tempKey))
                        {
                            dataToDel.Add(tempKey);
                        }
                    }
                    foreach (string tempKey in dataToDel)
                    {
                        runActuatorStaticDataSouceList.Remove(tempKey);
                    }
                    if (dataToDel.Count > 0)
                    {
                        isFindAndRegexKey = true;
                        dataToDel.Clear();
                    }

                    if(!isFindAndRegexKey)
                    {
                        return false;
                    }
                }
                catch(Exception ex)
                {
                    ErrorLog.PutInLog(ex);
                    return false;
                }
                
            }
            OnListChanged();
            return true;
        }

        /// <summary>
        /// set or change data in any list
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="configVaule">config Vaule</param>
        /// <returns>is success</returns>
        [MethodImplAttribute(MethodImplOptions.Synchronized)] 
        public bool SetStaticData(string key, string configVaule)
        {
            var tempDataList = IsHasSameKey(key, 0);
            if (tempDataList == null)
            {
                return false;
            }
            else if (tempDataList == runActuatorStaticDataKeyList)
            {
                runActuatorStaticDataKeyList[key] = configVaule;
            }
            else if (tempDataList == runActuatorStaticDataParameterList)
            {
                if(! runActuatorStaticDataParameterList[key].DataSet(configVaule))
                    return false;
            }
            else if (tempDataList == runActuatorStaticDataSouceList)
            {
                if (!runActuatorStaticDataSouceList[key].DataSet(configVaule))
                    return false;
            }
            else
            {
                ErrorLog.PutInLog(string.Format("error to [RemoveStaticData] in ActuatorStaticDataCollection  the key is {0} ", key));
                return false;
            }
            OnListChanged();
            return true;
        }

        public object Clone()
        {
            return new ActuatorStaticDataCollection(runActuatorStaticDataKeyList.MyClone<string, string>(), runActuatorStaticDataParameterList.MyClone(), runActuatorStaticDataSouceList.MyClone());
        }

        public void Dispose()
        {
            runActuatorStaticDataKeyList.Clear();
            runActuatorStaticDataParameterList.Clear();
            runActuatorStaticDataSouceList.Clear();
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
            MyRunCaseData<ICaseExecutionContent> runCaseData;
            TreeNode executiveNode;

            public ExecutivebasicData(MyRunCaseData<ICaseExecutionContent> yourCaseData,TreeNode yourExecutiveNode)
            {
                runCaseData = yourCaseData;
                executiveNode = yourExecutiveNode;
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
        private MyCaseRunTime caseRunTime;

        /// <summary>
        /// ExecutionDevice List with his name【执行驱动器映射表】
        /// </summary>
        private Dictionary<string, ICaseExecutionDevice> myExecutionDeviceList;

        /// <summary>
        /// RunActuatorStaticDataCollection
        /// </summary>
        private ActuatorStaticDataCollection runActuatorStaticDataCollection;

        /// <summary>
        /// Execution Result List
        /// </summary>
        private List<MyExecutionDeviceResult> runExecutionResultList;

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
        public delegate void delegateGetExecutiveResultEventHandler(string sender, MyExecutionDeviceResult yourResult);
        public delegate void delegateGetActuatorStateEventHandler(string sender, CaseActuatorState yourState);
        public delegate void delegateActuatorParameterListEventHandler();


        public event delegateGetExecutiveData OnGetExecutiveData;
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
            runActuatorStaticDataCollection = new ActuatorStaticDataCollection();
            runActuatorStaticDataCollection.SetCaseActionActuator(this);
            runExecutionResultList = new List<MyExecutionDeviceResult>();
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
            cloneActuator.runActuatorStaticDataCollection = (ActuatorStaticDataCollection)runActuatorStaticDataCollection.Clone();
            cloneActuator.runActuatorStaticDataCollection.SetCaseActionActuator(cloneActuator);
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
        public List<MyExecutionDeviceResult> NowExecutionResultList
        {
            get
            {
                return runExecutionResultList;
            }
        }


        /// <summary>
        /// 获取数据源管理器
        /// </summary>
        public ActuatorStaticDataCollection RunActuatorStaticDataCollection
        {
            get
            {
                return runActuatorStaticDataCollection;
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
        private void caseRunTime_OnQueueChangeEvent(CaseCell yourTarget, string yourMessage)
        {
            if (yourMessage != "")
            {
                if (yourMessage.StartsWith(MyConfiguration.CaseShowGotoNodeStart))  
                {
                    //附加任务起始节点
                    MyActionActuator.SetCaseNodeExpand(yourTarget);
                }
                else if (yourMessage.StartsWith(MyConfiguration.CaseShowCaseNodeStart))
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
                    MyActionActuator.SetCaseNodeExpand(yourTarget);
                }
                yourMessage = "【" + yourMessage + "】";
            }

            MyActionActuator.SetCaseNodeLoopChange(yourTarget, yourMessage);
        }

        //RunTime Queue 中Loop变化时通知
        private void caseRunTime_OnLoopChangeEvent(CaseCell yourTarget, string yourMessage)
        {
            if (yourMessage!="")
            {
                MyActionActuator.SetCaseNodeExpand(yourTarget);
                MyActionActuator.SetCaseNodeLoopRefresh(yourTarget);
                yourMessage = "【" + yourMessage + "】";
            }
            MyActionActuator.SetCaseNodeLoopChange(yourTarget, yourMessage);
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
                                                    SetNowActionError(string.Format("find unknow Protocol in ScriptRunTime  with {0} ", tempActuatorName));
                                                }
                                                else
                                                {
                                                    switch (tempActuatorProtocol)
                                                    {
                                                        case CaseProtocol.console:
                                                            myConnectForConsole ConnectInfo_console = new myConnectForConsole(tempActuatorProtocol);
                                                            AddExecutionDevice(tempActuatorName, ConnectInfo_console);
                                                            break;
                                                        case CaseProtocol.activeMQ:
                                                            List<string> tempMqQueueList = new List<string>();
                                                            List<KeyValuePair<string,string>> tempMqTopicList=new List<KeyValuePair<string,string>>();
                                                            #region Get Queues data
	                                                     	List<string[]> tempMqListData = CaseTool.GetXmlInnerMetaDataList(tempNodeChild, "queue", null);
                                                            if(tempMqListData.Count>0)
                                                            {
                                                                foreach(string[] tempOneData in tempMqListData)
                                                                {
                                                                    tempMqQueueList.Add(tempOneData[0]);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                tempMqQueueList = null;
                                                            } 
	                                                        #endregion
                                                            #region Get Topics data
                                                            tempMqListData = CaseTool.GetXmlInnerMetaDataList(tempNodeChild, "topic", new string[]{"durable"});
                                                            if(tempMqListData.Count>0)
                                                            {
                                                                foreach(string[] tempOneData in tempMqListData)
                                                                {
                                                                    tempMqTopicList.Add(new KeyValuePair<string, string>(tempOneData[0], tempOneData[1]));
                                                                }
                                                            }
                                                            else
                                                            {
                                                                tempMqTopicList = null;
                                                            } 
	                                                        #endregion
                                                            myConnectForActiveMQ ConnectInfo_activeMQ = new myConnectForActiveMQ(tempActuatorProtocol, CaseTool.GetXmlInnerVauleWithEmpty(tempNodeChild, "brokerUri"), CaseTool.GetXmlInnerVauleWithEmpty(tempNodeChild, "clientId"), CaseTool.GetXmlInnerVauleWithEmpty(tempNodeChild, "factoryUserName"), CaseTool.GetXmlInnerVauleWithEmpty(tempNodeChild, "factoryPassword"),tempMqQueueList,tempMqTopicList);
                                                            AddExecutionDevice(tempActuatorName, ConnectInfo_activeMQ);
                                                            break;
                                                        case CaseProtocol.vanelife_http:
                                                            myConnectForVanelife_http ConnectInfo_vanelifeHttp = new myConnectForVanelife_http(tempActuatorProtocol, CaseTool.GetXmlInnerVauleWithEmpty(tempNodeChild, "dev_key"), CaseTool.GetXmlInnerVauleWithEmpty(tempNodeChild, "dev_secret"), CaseTool.GetXmlInnerVauleWithEmpty(tempNodeChild, "default_url"));
                                                            AddExecutionDevice(tempActuatorName, ConnectInfo_vanelifeHttp);
                                                            break;
                                                        case CaseProtocol.http:
                                                            myConnectForHttp ConnectInfo_http = new myConnectForHttp(tempActuatorProtocol, CaseTool.GetXmlInnerVauleWithEmpty(tempNodeChild, "default_url"));
                                                            AddExecutionDevice(tempActuatorName, ConnectInfo_http);
                                                            break;
                                                        case CaseProtocol.mysql:
                                                            myConnectForMysql ConnectInfo_mysql = new myConnectForMysql(tempActuatorProtocol, CaseTool.GetXmlInnerVauleWithEmpty(tempNodeChild, "connect_str"));
                                                            AddExecutionDevice(tempActuatorName, ConnectInfo_mysql);
                                                            break;
                                                        case CaseProtocol.tcp:
                                                            #region Get EndPoint
                                                            string tempTcpHost = CaseTool.GetXmlInnerVaule(tempNodeChild, "host");
                                                            string tempTcpPort = CaseTool.GetXmlInnerVaule(tempNodeChild, "port");
                                                            int tempTcpNowPort = 0;
                                                            if (tempTcpHost != null && tempTcpPort!=null)
                                                            {
                                                                System.Net.IPAddress tempIp;
                                                                if (!(System.Net.IPAddress.TryParse(tempTcpHost, out tempIp) && int.TryParse(tempTcpPort, out tempTcpNowPort) && tempTcpNowPort < 65536 && tempTcpNowPort >0))
                                                                {
                                                                    SetNowActionError(string.Format("[add tcp actuator ]find error data in host or port data when add RunTimeActuator with [{0}]", tempActuatorName));
                                                                    break;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                SetNowActionError(string.Format("[add tcp actuator ]can not find host or port data when add RunTimeActuator with [{0}]", tempActuatorName));
                                                                break;
                                                            }
                                                            #endregion
                                                            myConnectForTcp ConnectInfo_tcp = new myConnectForTcp(tempActuatorProtocol, tempTcpHost,tempTcpNowPort);
                                                            AddExecutionDevice(tempActuatorName, ConnectInfo_tcp);
                                                            break;
                                                        case CaseProtocol.ssh:
                                                            myConnectForSsh ConnectInfo_ssh = new myConnectForSsh(tempActuatorProtocol, CaseTool.GetXmlInnerVauleWithEmpty(tempNodeChild, "host"), CaseTool.GetXmlInnerVauleWithEmpty(tempNodeChild, "user"),CaseTool.GetXmlInnerVauleWithEmpty(tempNodeChild, "password"), CaseTool.GetXmlInnerVaule(tempNodeChild, "expect_pattern"));
                                                            AddExecutionDevice(tempActuatorName, ConnectInfo_ssh);
                                                            break;
                                                        case CaseProtocol.telnet:
                                                            #region Get telnet connect info
                                                            string tempTelnetHost = CaseTool.GetXmlInnerVaule(tempNodeChild, "host");
                                                            string tempTelnetPort = CaseTool.GetXmlInnerVaule(tempNodeChild, "port");
                                                            string tempTelnetUser = CaseTool.GetXmlInnerVaule(tempNodeChild, "user");
                                                            string tempTelnetPassword = CaseTool.GetXmlInnerVaule(tempNodeChild, "password");
                                                            string tempTelnetExpectPattern = CaseTool.GetXmlInnerVaule(tempNodeChild, "expect_pattern");
                                                            string tempTelnetEncoding = CaseTool.GetXmlInnerVaule(tempNodeChild, "encoding");
                                                            Encoding tempTelnetnowEncoding = Encoding.UTF8;
                                                            //char nowExpectPattern = '\x0000';  // Hexadecimal:'\x0058'  integral type:(char)88 Unicode:'\u0058'
                                                            int tempTelnetNowPort = 0;
                                                            //endpoint
                                                            if (tempTelnetHost != null && tempTelnetPort != null)
                                                            {
                                                                System.Net.IPAddress tempIp;
                                                                if (!(System.Net.IPAddress.TryParse(tempTelnetHost, out tempIp) && int.TryParse(tempTelnetPort, out tempTelnetNowPort) && tempTelnetNowPort < 65536 && tempTelnetNowPort > 0))
                                                                {
                                                                    SetNowActionError(string.Format("[add telnet actuator ]find error data in host or port data when add RunTimeActuator with [{0}]", tempActuatorName));
                                                                    break;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                SetNowActionError(string.Format("[add telnet actuator ]can not find host or port data when add RunTimeActuator with [{0}]", tempActuatorName));
                                                                break;
                                                            }
                                                            //user name
                                                            if (tempTelnetUser == null || tempTelnetPassword == null)
                                                            {
                                                                SetNowActionError(string.Format("[add telnet actuator ]can not find user name or password data when add RunTimeActuator with [{0}]", tempActuatorName));
                                                                break;
                                                            }
                                                            //encodeing
                                                            //encoding

                                                            if (tempTelnetEncoding != null)
                                                            {
                                                                try
                                                                {
                                                                    nowTelnetEncoding = Encoding.GetEncoding(tempTelnetEncoding);
                                                                }
                                                                catch
                                                                {
                                                                    SetNowActionError(string.Format("[add telnet actuator ] your encoding is illegal when add RunTimeActuator with [{0}]", tempActuatorName));
                                                                    break;
                                                                }
                                                            }
                                                            #endregion
                                                            myConnectForTelnet ConnectInfo_telnet = new myConnectForTelnet(tempActuatorProtocol, tempTelnetHost, tempTelnetNowPort, tempTelnetUser, tempTelnetPassword, tempTelnetnowEncoding, tempTelnetExpectPattern);
                                                            AddExecutionDevice(tempActuatorName, ConnectInfo_telnet);
                                                            break;
                                                        case CaseProtocol.com:
                                                            #region ComInfo
                                                            string tempComStr;
                                                            string tempComPortName;
                                                            int tempComBaudRate ;
                                                            int tempComDataBits =8;
                                                            SerialPortParity tempComParity= SerialPortParity.None;
                                                            SerialPortStopBits tempComStopBits= SerialPortStopBits.One;
                                                            Encoding tempComEncoding=Encoding.UTF8;
                                                            //portName
                                                            tempComPortName=CaseTool.GetXmlInnerVaule(tempNodeChild,"portName");
                                                            if(tempComPortName==null)
                                                            {
                                                                SetNowActionError(string.Format("[add com actuator ]can not find portName when add RunTimeActuator with [{0}]", tempActuatorName));
                                                                break;
                                                            }
                                                            //baudRate
                                                            if (int.TryParse(CaseTool.GetXmlInnerVauleWithEmpty(tempNodeChild, "baudRate"),out tempComBaudRate ))
                                                            {
                                                                if(tempComBaudRate<1)
                                                                {
                                                                    SetNowActionError(string.Format("[add com actuator ] your baudRate is illegal (it must >0) when add RunTimeActuator with [{0}]", tempActuatorName));
                                                                    break;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                SetNowActionError(string.Format("[add com actuator ]can not find baudRate or your baudRate is illegal when add RunTimeActuator with [{0}]", tempActuatorName));
                                                                break;
                                                            }
                                                            //dataBits
                                                            tempComStr = CaseTool.GetXmlInnerVaule(tempNodeChild, "dataBits");
                                                            if (tempComStr!=null)
                                                            {
                                                                if (int.TryParse(tempComStr, out tempComDataBits))
                                                                {
                                                                    if(tempComDataBits<5 || tempComDataBits>8)
                                                                    {
                                                                        SetNowActionError(string.Format("[add com actuator ] your dataBits is illegal (it must in [5,8]) when add RunTimeActuator with [{0}]", tempActuatorName));
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                            //parity
                                                            tempComStr=CaseTool.GetXmlInnerVaule(tempNodeChild, "parity");
                                                            if (tempComStr != null)
                                                            {
                                                                if (!Enum.TryParse<SerialPortParity>(tempComStr, out tempComParity))
                                                                {
                                                                    SetNowActionError(string.Format("[add com actuator ] your parity is illegal when add RunTimeActuator with [{0}]", tempActuatorName));
                                                                    break;
                                                                }
                                                            }
                                                            //stopBits
                                                            tempComStr=CaseTool.GetXmlInnerVaule(tempNodeChild, "stopBits");
                                                            if (tempComStr != null)
                                                            {
                                                                if (!Enum.TryParse<SerialPortStopBits>(tempComStr, out tempComStopBits))
                                                                {
                                                                    SetNowActionError(string.Format("[add com actuator ] your stopBits is illegal when add RunTimeActuator with [{0}]", tempActuatorName));
                                                                    break;
                                                                }
                                                            }
                                                            //encoding
                                                            tempComStr = CaseTool.GetXmlInnerVaule(tempNodeChild, "encoding");
                                                            if (tempComStr != null)
                                                            {
                                                                try
                                                                {
                                                                    tempComEncoding = Encoding.GetEncoding(tempComStr);
                                                                }
                                                                catch
                                                                {
                                                                    SetNowActionError(string.Format("[add com actuator ] your encoding is illegal when add RunTimeActuator with [{0}]", tempActuatorName));
                                                                    break;
                                                                }
                                                            }
                                                            #endregion
                                                            myConnectForCom ConnectInfo_com = new myConnectForCom(tempActuatorProtocol, tempComPortName, tempComBaudRate, tempComParity, tempComDataBits, tempComStopBits, tempComEncoding);
                                                            AddExecutionDevice(tempActuatorName, ConnectInfo_com);
                                                            break;
                                                        default:
                                                            SetNowActionError(string.Format("find nonsupport Protocol in ScriptRunTime  with {0} ", tempActuatorName));
                                                            break;
                                                    }
                                                }

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

                            // 此处获取默认参数化数据
                            #region RunTimeParameter
                            case "RunTimeParameter":
                                if (tempNode.HasChildNodes)
                                {
                                    foreach (XmlNode tempNodeChild in tempNode.ChildNodes)
                                    {
                                        if (tempNodeChild.Name == "NewParameter")
                                        {
                                            if (tempNodeChild.Attributes["name"] != null && tempNodeChild.Attributes["type"] != null)
                                            {
                                                CaseStaticDataType tempType;
                                                string tempName = tempNodeChild.Attributes["name"].Value;
                                                string tempTypeStr = tempNodeChild.Attributes["type"].Value;
                                                string tempVaule = tempNodeChild.InnerText;
                                                try
                                                {
                                                    tempType = (CaseStaticDataType)Enum.Parse(typeof(CaseStaticDataType), "caseStaticData_" + tempTypeStr);
                                                }
                                                catch
                                                {
                                                    SetNowActionError(string.Format("find unknown type in RunTimeStaticData - ScriptRunTime in [{0}] with [{1}]", tempNodeChild.InnerXml, tempTypeStr));
                                                    continue;
                                                }
                                                switch (MyCaseDataTypeEngine.dictionaryStaticDataTypeClass[tempType])
                                                {
                                                    //caseStaticDataKey 
                                                    case  CaseStaticDataClass.caseStaticDataKey:
                                                        if (tempType== CaseStaticDataType.caseStaticData_vaule)
                                                        {
                                                            if (!runActuatorStaticDataCollection.IsHaveSameKey(tempName))
                                                            {
                                                                runActuatorStaticDataCollection.AddStaticDataKey(tempName, tempVaule);
                                                            }
                                                            else
                                                            {
                                                                SetNowActionError(string.Format("find same key 【{0}】in RunTimeParameter with [ CaseStaticDataClass.caseStaticDataKey] in - ScriptRunTime ,and will drop this key", tempName));
                                                                break;
                                                            }
                                                            //runActuatorStaticDataCollection.RunActuatorStaticDataKeyList.MyAdd(new KeyValuePair<string, string>());
                                                        }
                                                        else
                                                        {
                                                            SetNowActionError(string.Format("find nonsupport Protocol 【{0}】with [ CaseStaticDataClass.caseStaticDataKey] in - ScriptRunTime ", tempType));
                                                            break;
                                                        }
                                                        break ;
                                                    //caseStaticDataParameter
                                                    case CaseStaticDataClass.caseStaticDataParameter:
                                                        IRunTimeStaticData tempRunTimeStaticData;
                                                        string tempTypeError;
                                                        if (MyCaseDataTypeEngine.dictionaryStaticDataParameterAction[tempType](out tempRunTimeStaticData, out tempTypeError, tempVaule))
                                                        {
                                                            if (!runActuatorStaticDataCollection.IsHaveSameKey(tempName))
                                                            {
                                                                runActuatorStaticDataCollection.AddStaticDataParameter(tempName, tempRunTimeStaticData);
                                                            }
                                                            else
                                                            {
                                                                SetNowActionError(string.Format("find same key 【{0}】in RunTimeParameter with [ CaseStaticDataClass.caseStaticDataKey] in - ScriptRunTime ,and will drop this key", tempName));
                                                                break;
                                                            }
                                                            //runActuatorStaticDataCollection.RunActuatorStaticDataParameterList.MyAdd(tempName, tempRunTimeStaticData);
                                                        }
                                                        else
                                                        {
                                                            SetNowActionError(string.Format("find error in 【RunTimeStaticData】->【{0}】:value:【{1}】 by {2}", tempName, tempVaule, tempTypeError));
                                                        }
                                                        break;
                                                    //caseStaticDataSource
                                                    case CaseStaticDataClass.caseStaticDataSource:
                                                        IRunTimeDataSource tempRunTimeDataSource;
                                                        if (MyCaseDataTypeEngine.dictionaryStaticDataSourceAction[tempType](out tempRunTimeDataSource, out tempTypeError, tempVaule))
                                                        {
                                                            if (!runActuatorStaticDataCollection.IsHaveSameKey(tempName))
                                                            {
                                                                runActuatorStaticDataCollection.AddStaticDataSouce(tempName, tempRunTimeDataSource);
                                                            }
                                                            else
                                                            {
                                                                SetNowActionError(string.Format("find same key 【{0}】in RunTimeParameter with [ CaseStaticDataClass.caseStaticDataKey] in - ScriptRunTime ,and will drop this key", tempName));
                                                                break;
                                                            }
                                                            //runActuatorStaticDataCollection.RunActuatorStaticDataSouceList.MyAdd<IRunTimeDataSource>(tempName, tempRunTimeDataSource);
                                                        }
                                                        else
                                                        {
                                                            SetNowActionError(string.Format("find error in 【RunTimeStaticData】->【{0}】:value:【{1}】 by {2}", tempName, tempVaule, tempTypeError));
                                                        }
                                                        break;
                                                    default:
                                                        SetNowActionError(string.Format("find nonsupport Protocol 【{0}】with [ CaseStaticDataClass] in - ScriptRunTime ", tempType));
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
        /// 连接执行器
        /// </summary>
        public void ConnectExecutionDevice()
        {
            SetNowExecutiveData("CaseExecutionDevice connecting ......");
            foreach (KeyValuePair<string, ICaseExecutionDevice> tempKvp in myExecutionDeviceList)
            {
                SetNowExecutiveData(string.Format("【RunTimeActuator】:{0} connecting······", tempKvp.Key));
                if (tempKvp.Value.ExecutionDeviceConnect())
                {
                    SetNowExecutiveData(string.Format("【RunTimeActuator】:{0} connect scusess", tempKvp.Key));
                }
                else
                {
                    SetNowActionError(string.Format("【RunTimeActuator】:{0} connect fail", tempKvp.Key)); 
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
                tempKvp.Value.ExecutionDeviceClose();
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
            MyRunCaseData<ICaseExecutionContent> nowRunCaseData = null;
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
            MyRunCaseData<ICaseExecutionContent> nowRunCaseData = nowExecutiveNode.CaseRunData;
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
        private void ExecutiveAnCases(MyRunCaseData<ICaseExecutionContent> nowRunCaseData, CaseCell nowExecutiveNode, ref ExecutiveAdditionalInfo nowAdditionalInfo)
        {
            bool tempIsBreakError = false;
            MyExecutionDeviceResult executionResult;

            if(runState==CaseActuatorState.Pause)
            {
                MyActionActuator.SetCaseNodePause(nowExecutiveNode);
            }
            myManualResetEvent.WaitOne();
            if (runState == CaseActuatorState.Stoping)
            {
                nowAdditionalInfo = new ExecutiveAdditionalInfo(true);
                MyActionActuator.SetCaseNodeStop(nowExecutiveNode);
                return;
            }

            if (nowRunCaseData.errorMessages == null)
            {
                if (myExecutionDeviceList.ContainsKey(nowRunCaseData.testContent.MyCaseActuator))
                {
                    var nowDevice = myExecutionDeviceList[nowRunCaseData.testContent.MyCaseActuator];
                ExecutionDeviceRunLink:
                    if (nowDevice.IsDeviceConnect)
                    {
                        //nowDevice.executionDeviceRun()
                        MyActionActuator.SetCaseNodeRunning(nowExecutiveNode);
                        executionResult = nowDevice.ExecutionDeviceRun(nowRunCaseData.testContent, OnGetExecutiveData, myName, runActuatorStaticDataCollection, nowRunCaseData.id);
                        HandleCaseExecutiveResul(nowRunCaseData, nowExecutiveNode, executionResult,ref nowAdditionalInfo);

                    }
                    else
                    {
                        //Device没有连接
                        SetNowExecutiveData(string.Format("【ID:{0}】 {1}连接中断，尝试连接中···", nowRunCaseData.id, nowRunCaseData.testContent.MyCaseActuator));
                        if (nowDevice.ExecutionDeviceConnect())
                        {
                            //nowDevice.executionDeviceRun()
                            goto ExecutionDeviceRunLink;
                        }
                        else
                        {
                            SetNowExecutiveData(string.Format("【ID:{0}】 {1}连接失败", nowRunCaseData.id, nowRunCaseData.testContent.MyCaseActuator));
                            MyActionActuator.SetCaseNodeConnectInterrupt(nowExecutiveNode);

                            tempIsBreakError = true;
                            executionResult = new MyExecutionDeviceResult(nowRunCaseData.id, "CaseActuator连接失败");
                        }
                    }
                }
                else
                {
                    //testContent没有找到合适的myCaseActuator
                    SetNowExecutiveData(string.Format("【ID:{0}】 未找到指定CaseActuator", nowRunCaseData.id));
                    MyActionActuator.SetCaseNodeNoActuator(nowExecutiveNode);
                    MyActionActuator.SetCaseNodeContentWarning(nowExecutiveNode);

                    tempIsBreakError = true;
                    executionResult = new MyExecutionDeviceResult(nowRunCaseData.id, "未找到指定CaseActuator");
                }
            }
            else
            {
                //nowRunCaseData有错误
                SetNowActionError(string.Format("【ID:{0}】 执行数据脚本存在错误", nowRunCaseData.id));
                MyActionActuator.SetCaseNodeAbnormal(nowExecutiveNode);

                tempIsBreakError = true;
                executionResult = new MyExecutionDeviceResult(nowRunCaseData.id, "执行数据脚本存在错误" + nowRunCaseData.errorMessages.MyToString("\r\n"));
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
        private void HandleCaseExecutiveResul(MyRunCaseData<ICaseExecutionContent> yourRunData, CaseCell nowExecutiveNode, MyExecutionDeviceResult yourExecutionResult, ref ExecutiveAdditionalInfo nowAdditionalInfo)
        {
            string tempError;
            yourExecutionResult.caseId = yourRunData.id;
            if (yourExecutionResult.additionalError != null)
            {
                MyActionActuator.SetCaseNodeContentWarning(nowExecutiveNode);
            }
            yourExecutionResult.expectMethod = yourRunData.caseExpectInfo.myExpectType;
            yourExecutionResult.expectContent = yourRunData.caseExpectInfo.myExpectContent.GetTargetContentData(runActuatorStaticDataCollection, yourExecutionResult.staticDataResultCollection, out tempError);
            if (tempError != null)
            {
                MyActionActuator.SetCaseNodeContentWarning(nowExecutiveNode);
                yourExecutionResult.additionalError = yourExecutionResult.additionalError.MyAddValue(tempError);
            }
            if (MyAssert.CheckBackData(yourExecutionResult.backContent, yourExecutionResult.expectContent, yourRunData.caseExpectInfo.myExpectType))
            {
                yourExecutionResult.result = CaseResult.Pass;
                MyActionActuator.SetCaseNodePass(nowExecutiveNode);
            }
            else
            {
                yourExecutionResult.result = CaseResult.Fail;
                MyActionActuator.SetCaseNodeFial(nowExecutiveNode);
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
                            tempPickVaule = MyAssert.PickJsonParameter(tempParameterSave.parameterFindVaule, yourExecutionResult.backContent);
                            break;
                        case PickOutFunction.pick_str:
                            string tempFindVaule;
                            int tempLen;
                            MyAssert.GetStrPickData(tempParameterSave.parameterFindVaule, out tempFindVaule, out tempLen);
                            if (tempFindVaule!=null)
                            {
                                tempPickVaule = MyAssert.PickStrParameter(tempFindVaule, tempLen, yourExecutionResult.backContent);
                            }
                            else
                            {
                                tempError = string.Format("【ID:{0}】ParameterSave 脚本数据不合法", yourRunData.id);
                                yourExecutionResult.additionalRemark = yourExecutionResult.additionalRemark.MyAddValue(tempError);
                                SetNowActionError(tempError);
                                MyActionActuator.SetCaseNodeContentWarning(nowExecutiveNode);
                            }
                            
                            break;
                        case PickOutFunction.pick_xml:
                            tempPickVaule = MyAssert.PickXmlParameter(tempParameterSave.parameterFindVaule, yourExecutionResult.backContent);
                            break;
                        default:
                            tempError = string.Format("【ID:{0}】 ParameterSave 暂不支持该数据提取方式", yourRunData.id);
                            yourExecutionResult.additionalRemark = yourExecutionResult.additionalRemark.MyAddValue(tempError);
                            SetNowActionError(tempError);
                            MyActionActuator.SetCaseNodeContentWarning(nowExecutiveNode);
                            break;
                    }
                    if(tempPickVaule==null)
                    {
                        SetNowActionError(string.Format("【ID:{0}】 ParameterSave 在执行结果中未找到指定参数", yourRunData.id));
                    }
                    else
                    {
                        AddRunActuatorStaticDataKey(tempParameterSave.parameterName, tempPickVaule);
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
                        #region action_alarm
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
                        #endregion

                        #region action_continue
                        case CaseAction.action_continue:
                            //do nothing
                            break; 
                        #endregion

                        #region action_goto
                        case CaseAction.action_goto:
                            if (nowAdditionalInfo != null)
                            {
                                //定项不执行goto
                                if (nowAdditionalInfo.IsTryCase)
                                {
                                    break;
                                }
                            }
                            if (yourRunData.actions[yourExecutionResult.result].addInfo == null)
                            {
                                tempError = string.Format("【ID:{0}】 CaseAction Case数据中部没有发现目的ID", yourRunData.id);
                                yourExecutionResult.additionalRemark = yourExecutionResult.additionalRemark.MyAddValue(tempError);
                                SetNowActionError(tempError);
                                MyActionActuator.SetCaseNodeContentWarning(nowExecutiveNode);
                            }
                            else
                            {
                                int tempCaseID;
                                int tempProjectID;
                                if (CaseTool.getTargetCaseID(yourRunData.actions[yourExecutionResult.result].addInfo, out tempProjectID, out tempCaseID))
                                {
                                    if (caseRunTime.gotoMyCase(tempProjectID, tempCaseID, runTimeCaseDictionary))
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
                                    MyActionActuator.SetCaseNodeContentWarning(nowExecutiveNode);
                                }
                            }
                            break; 
                        #endregion

                        #region action_retry
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
                                            if (nowAdditionalInfo.TryTimes > 0)
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
                                    MyActionActuator.SetCaseNodeContentWarning(nowExecutiveNode);
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
                        #endregion

                        #region action_stop
                        case CaseAction.action_stop:
                            PauseCaseScript();
                            break; 
                        #endregion

                        case CaseAction.action_unknow:
                            tempError = string.Format("【ID:{0}】 CaseAction 未能解析", yourRunData.id);
                            yourExecutionResult.additionalRemark = yourExecutionResult.additionalRemark.MyAddValue(tempError);
                            SetNowActionError(tempError);
                            MyActionActuator.SetCaseNodeContentWarning(nowExecutiveNode);
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
                this.OnGetExecutiveData(myName,CaseActuatorOutPutType.ActuatorInfo, yourContent);
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
                this.OnGetExecutiveData(myName,CaseActuatorOutPutType.ActuatorInfo, yourContent);
            }
        }

        /// <summary>
        /// 触发【OnGetActionError】
        /// </summary>
        /// <param name="yourContent">Action Error Content</param>
        private void SetNowActionError(string yourContent)
        {
            if (OnGetExecutiveData != null)
            {
                this.OnGetExecutiveData(myName, CaseActuatorOutPutType.ActuatorError, yourContent);
            }
        }

        /// <summary>
        ///  设置 myErrorInfo 并 触发【OnGetActionError】（若不想触发OnGetActionError请直接操作myErrorInfo）
        /// </summary>
        /// <param name="yourContent">Action Error Content</param>
        private void SetAndSaveNowActionError(string yourContent)
        {
            myErrorInfo = yourContent;
            if (OnGetExecutiveData != null)
            {
                this.OnGetExecutiveData(myName, CaseActuatorOutPutType.ActuatorError, myErrorInfo);
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
        private void AddExecutionResult(MyExecutionDeviceResult yourExecutionResult)
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
            switch (yourDeviceConnectInfo.MyCaseProtocol)
            {
                case CaseProtocol.console:
                    myExecutionDeviceList.MyAdd(yourDeviceName, new CaseProtocolExecutionForConsole((myConnectForConsole)yourDeviceConnectInfo));
                    break;
                case CaseProtocol.activeMQ:
                    myExecutionDeviceList.MyAdd(yourDeviceName, new CaseProtocolExecutionForActiveMQ((myConnectForActiveMQ)yourDeviceConnectInfo));
                    break;
                case CaseProtocol.mysql:
                    myExecutionDeviceList.MyAdd(yourDeviceName, new CaseProtocolExecutionForMysql((myConnectForMysql)yourDeviceConnectInfo));
                    break;
                case CaseProtocol.tcp:
                    myExecutionDeviceList.MyAdd(yourDeviceName, new CaseProtocolExecutionForTcp((myConnectForTcp)yourDeviceConnectInfo));
                    break;
                case CaseProtocol.com:
                    myExecutionDeviceList.MyAdd(yourDeviceName, new CaseProtocolExecutionForCom((myConnectForCom)yourDeviceConnectInfo));
                    break;
                case CaseProtocol.ssh:
                    myExecutionDeviceList.MyAdd(yourDeviceName, new CaseProtocolExecutionForSsh((myConnectForSsh)yourDeviceConnectInfo));
                    break;
                case CaseProtocol.telnet:
                    myExecutionDeviceList.MyAdd(yourDeviceName, new CaseProtocolExecutionForTelnet((myConnectForTelnet)yourDeviceConnectInfo));
                    break;

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
        public void AddRunActuatorStaticDataKey(string yourParameterName, string yourParameterVaule)
        {
            runActuatorStaticDataCollection.RunActuatorStaticDataKeyList.MyAdd(yourParameterName, yourParameterVaule);
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
                caseRunTime = new MyCaseRunTime();
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
                runActuatorStaticDataCollection.Dispose();
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
