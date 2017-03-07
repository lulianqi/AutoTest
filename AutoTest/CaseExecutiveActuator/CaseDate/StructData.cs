using MyCommonTool;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
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
    class StructData
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public enum CaseActuatorState
    {
        Running = 0,
        Pause = 1,
        Stop = 2,
        Stoping = 3,
        Trying = 4
    }

    /// <summary>
    /// 描述框架支持的协议(请再此处扩展)
    /// </summary>
    public enum CaseProtocol
    {
        extendProtocol = 0,
        unknownProtocol = 1,
        defaultProtocol = 2,
        vanelife_http = 3,
        vanelife_comm = 4,
        vanelife_tcp = 5,
        vanelife_telnet = 6,
        http = 7,
        tcp = 8,
        comm = 9,
        telnet = 10
    }

    /// <summary>
    /// 描述框架脚本支持的静态参数化数据(请再此处扩展)
    /// </summary>
    public enum CaseStaticDataType
    {
        staticData_index = 0,
        staticData_long = 1,
        staticData_random = 2,
        staticData_time = 3,
        staticData_list = 4,
    }


    /// <summary>
    /// 描述框架脚本支持的数据摘取方式(请再此处扩展)
    /// </summary>
    public enum PickOutFunction
    {
        pick_json = 0,
        pick_xml = 1,
        pick_str = 2
    }

    /// <summary>
    /// 描述框架脚本文件基础根节点类型(请再此处扩展)
    /// </summary>
    public enum CaseType
    {
        Unknown = 0,
        Project = 1,
        Case = 2,
        Repeat = 3,
        ScriptRunTime = 4
    }

    /// <summary>
    /// 描述执行结构
    /// </summary>
    public enum CaseResult
    {
        Pass = 0,
        Fail = 1,
        Unknow = 2,
    }

    /// <summary>
    /// 描述脚本动作(请再此处扩展)
    /// </summary>
    public enum CaseAction
    {
        action_unknow = 0,
        action_goto = 1,
        action_retry = 2,
        action_stop = 3,
        action_continue = 4,
        action_alarm = 5,
    }

    /// <summary>
    /// 描述脚本断言方式(请再此处扩展)
    /// </summary>
    public enum CaseExpectType
    {
        judge_default = 0,
        judge_is = 1,
        judge_not = 2,
        judge_like = 3,
        judge_endwith = 4,
        judge_startwith = 5,
        judge_contain = 6,
        judge_uncontain = 7,
    }

    /// <summary>
    /// 描述[caseParameterizationContent]使用的数据附加编码类型，0标识不进行操作，基数encode偶数decode
    /// </summary>
    public enum ParameterizationContentEncodingType
    {
        encode_default=0,
        encode_base64=1,
        decode_base64=2
    }


    /// <summary>
    /// 描述可参数化数据结构
    /// </summary>
    public struct caseParameterizationContent
    {
        public string contentData;
        public bool hasParameter;
        public ParameterizationContentEncodingType encodetype;
        public caseParameterizationContent(string yourContentData, bool isParameter)
        {
            contentData = yourContentData;
            hasParameter = isParameter;
            encodetype = ParameterizationContentEncodingType.encode_default;
        }
        public caseParameterizationContent(string yourContentData)
        {
            contentData = yourContentData;
            hasParameter = false;
            encodetype = ParameterizationContentEncodingType.encode_default;
        }

        public bool IsFilled()
        {
            if (contentData != null)
            {
                if (contentData != "")
                {
                    return true;
                }
            }
            return false;
        }

        public string getTargetContentData(Dictionary<string, string> yourParameterList, Dictionary<string, IRunTimeStaticData> yourStaticDataList,NameValueCollection yourDataResultCollection, out string errorMessage)
        {
            string myTargetContentData = contentData;
            errorMessage = null;
            if (hasParameter)
            {
                myTargetContentData = CaseTool.GetCurrentParametersData(contentData, yourParameterList, yourStaticDataList, yourDataResultCollection, out errorMessage);
            }
            if (encodetype != ParameterizationContentEncodingType.encode_default)
            {
                switch (encodetype)
                {
                    case ParameterizationContentEncodingType.encode_base64:
                        myTargetContentData = Convert.ToBase64String(Encoding.UTF8.GetBytes(myTargetContentData));
                        break;
                    case ParameterizationContentEncodingType.decode_base64:
                        try
                        {
                            myTargetContentData = Encoding.UTF8.GetString(Convert.FromBase64String(myTargetContentData));
                        }
                        catch(Exception ex)
                        {
                            myTargetContentData = "ContentEncoding Error:" + ex.Message;
                        }
                        break;
                    default:
                        errorMessage = "[getTargetContentData] unknow or not supported this encodetype";
                        break;
                }
            }
            return myTargetContentData;
        }

        public string getTargetContentData()
        {
            return contentData;
        }
    }

    /// <summary>
    /// 描述脚本动作行为及附加信息
    /// </summary>
    public class caseActionDescription
    {
        public CaseAction caseAction;
        public string addInfo;
        public caseActionDescription(CaseAction yourCaseAction, string yourInfo)
        {
            caseAction = yourCaseAction;
            addInfo = yourInfo;
        }


    }

    /// <summary>
    /// Vanelife_http 【IConnectExecutiveData】
    /// </summary>
    public struct myConnectForVanelife_http : IConnectExecutiveData
    {
        public CaseProtocol caseProtocol;
        public string dev_key;
        public string dev_secret;
        public string default_url;

        public myConnectForVanelife_http(CaseProtocol yourCaseProtocol, string yourDev_key, string yourDev_secret, string yourDefault_url)
        {
            caseProtocol = yourCaseProtocol;
            dev_key = yourDev_key;
            dev_secret = yourDev_secret;
            default_url = yourDefault_url;
        }
        public CaseProtocol myCaseProtocol
        {
            get
            {
                return caseProtocol;
            }
        }
    }

    /// <summary>
    /// Http 【IConnectExecutiveData】
    /// </summary>
    public struct myConnectForHttp : IConnectExecutiveData
    {

        public CaseProtocol caseProtocol;
        public string default_url;

        public myConnectForHttp(CaseProtocol yourCaseProtocol, string yourDefault_url)
        {
            caseProtocol = yourCaseProtocol;
            default_url = yourDefault_url;
        }
        public CaseProtocol myCaseProtocol
        {
            get
            {
                return caseProtocol;
            }
        }
    }



    public struct myHttpBackData
    {
        public long spanTime;
        public string startTime;
        public string backContent;

        public myHttpBackData(long spanTime_, string startTime_, string backContent_)
        {
            spanTime = spanTime_;
            startTime = startTime_;
            backContent = backContent_;
        }
    }


    /// <summary>
    /// ExecutionDevice Result
    /// </summary>
    public class myExecutionDeviceResult
    {
        public int caseId;
        public CaseProtocol caseProtocol;
        public string requestTime;
        public string spanTime;
        public string startTime;
        public string caseTarget;
        public string backContent;
        public CaseExpectType expectMethod;
        public string expectContent;
        public CaseResult result;
        public NameValueCollection staticDataResultCollection;
        public string additionalRemark;
        public string additionalEroor;
        
        public myExecutionDeviceResult()
        {
            caseId = 0;
            caseProtocol = CaseProtocol.unknownProtocol;
            requestTime = null;
            spanTime = null;
            startTime = null;
            caseTarget = null;
            backContent = null;
            expectMethod = CaseExpectType.judge_default;
            expectContent = null;
            result = CaseResult.Unknow;
            staticDataResultCollection = null;
            additionalRemark = null;
            additionalEroor = null;
        }

        public myExecutionDeviceResult(int yourCaseId, string yourAdditionalError):this()
        {
            caseId = yourCaseId;
            backContent = yourAdditionalError;
            additionalEroor = yourAdditionalError;
        }
        public myExecutionDeviceResult(CaseProtocol yourCaseProtocol, string yourSpanTime, string yourStartTime, string yourBackContent):this()
        {
            caseProtocol = yourCaseProtocol;
            spanTime = yourSpanTime;
            startTime = yourStartTime;
            backContent = yourBackContent;
            expectMethod = CaseExpectType.judge_default;
        }
        public myExecutionDeviceResult(CaseProtocol yourCaseProtocol, string yourSpanTime, string yourStartTime, string yourBackContent, string yourAdditionalContent): this(yourCaseProtocol, yourStartTime, yourStartTime, yourBackContent)
        {
            additionalEroor = yourAdditionalContent;
        }
    }

    /// <summary>
    /// Case Data Source (大批量数据不适合使用静态数据，可以使用数据源)
    /// </summary>
    public class CaseDataSource
    {
        private List<List<string>> dataSource;


        public string GetData(int rowIndex,int columnIndex)
        {
            return null;
        }

        public bool SetData(int rowIndex,int columnIndex)
        {
            return false;
        }
    }

    public struct myAutoHttpTest
    {
        public string startTime;    //开始时间
        public string spanTime;     //持续时间
        public string caseId;       //用例ID
        public string result;       //返回报文
        public string ret;          //最终结果 pass fail break error skip
        public string remark;       //备注
        public myAutoHttpTest(string tempVal)
        {
            startTime = tempVal;
            spanTime = tempVal;
            caseId = tempVal;
            result = tempVal;
            ret = tempVal;
            remark = tempVal;
        }
    }

    /// <summary>
    /// 描述HTTP中Multipart数据 用于【myHttpExecutionContent】
    /// </summary>
    public struct HttpMultipart
    {
        public string name;
        public string fileName;
        public bool isFile;
        public string fileData;
        public HttpMultipart(string tempVal)
        {
            name = tempVal;
            fileName = tempVal;
            isFile = false;
            fileData = tempVal;
        }

        public bool IsFilled()
        {
            if (fileData != null)
            {
                if (fileData != "")
                {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// 描述HTTP中附加行为数据（强制地址，保存数据等） 用于【myHttpExecutionContent】
    /// </summary>
    public struct HttpAisleConfig
    {
        public caseParameterizationContent httpAddress;
        public caseParameterizationContent httpDataDown;
        public HttpAisleConfig(string yourHttpAddress, string yourHttpDataDown)
        {
            httpAddress = new caseParameterizationContent(yourHttpAddress);
            httpDataDown = new caseParameterizationContent(yourHttpDataDown);
        }
    }


    /// <summary>
    /// treeNode节点Tag信息 (未使用)
    /// </summary>
    public struct myTreeTagInfo_
    {
        public CaseType tagCaseType;
        public CaseProtocol tagCaseProtocol;
        public XmlNode tagCaseXmlNode;
        public myTreeTagInfo_(CaseType yourCaseType, XmlNode yourXmlNode)
        {
            tagCaseType = yourCaseType;
            tagCaseProtocol = CaseProtocol.unknownProtocol;
            tagCaseXmlNode = yourXmlNode;
        }
        public myTreeTagInfo_(CaseType yourCaseType, XmlNode yourXmlNode, CaseProtocol yourCaseProtocol)
        {
            tagCaseType = yourCaseType;
            tagCaseProtocol = yourCaseProtocol;
            tagCaseXmlNode = yourXmlNode;
        }
    }

    /// <summary>
    /// treeNode节点Tag信息 使用class将数据转移到托管堆
    /// </summary>
    public class myTreeTagInfo
    {
        public CaseType tagCaseType;
        public XmlNode tagCaseXmlNode;
        public myRunCaseData<ICaseExecutionContent> tagCaseRunData;
        public myTreeTagInfo(CaseType yourCaseType, XmlNode yourXmlNode)
        {
            tagCaseType = yourCaseType;
            tagCaseXmlNode = yourXmlNode;
        }

        public myTreeTagInfo(CaseType yourCaseType, XmlNode yourXmlNode, myRunCaseData<ICaseExecutionContent> yourCaseRunData)
        {
            tagCaseType = yourCaseType;
            tagCaseXmlNode = yourXmlNode;
            tagCaseRunData = yourCaseRunData;
        }
    }


    /// <summary>
    /// 为新任务提供必要创建信息  【重新评估是应该使用class还是struct】
    /// </summary>
    public struct myNewTestInfo
    {
        public myCaseRunTime caseRunTime;
        public CaseActionActuator actionActuator;
        public myNewTestInfo(myCaseRunTime yourRunTime, CaseActionActuator yourActuator)
        {
            caseRunTime = yourRunTime;
            actionActuator = yourActuator;
        }
    }

    /// <summary>
    /// 为Loop队列提供返回结构(未使用)
    /// </summary>
    public struct myLoopNodeInfo
    {
        public CaseType loopCaseType;
        public TreeNode loopCaseTreeNode;
        public myLoopNodeInfo(CaseType yourCaseType, TreeNode yourTreeNode)
        {
            loopCaseType = yourCaseType;
            loopCaseTreeNode = yourTreeNode;
        }
    }

    /// <summary>
    ///断言信息
    /// </summary>
    public struct myExpectInfo
    {
        public CaseExpectType myExpectType;
        public caseParameterizationContent myExpectContent;
        public myExpectInfo(CaseExpectType yourExpectType, caseParameterizationContent yourExpectContent)
        {
            myExpectType = yourExpectType;
            myExpectContent = yourExpectContent;
        }
    }

    /// <summary>
    /// 描述脚本参数保存动作
    /// </summary>
    public struct ParameterSave
    {
        public string parameterName;
        public string parameterFindVaule;
        public PickOutFunction parameterFunction;

        public ParameterSave(PickOutFunction yourFunction, string yourName, string yourFindVaule)
        {
            parameterFunction = yourFunction;
            parameterFindVaule = yourFindVaule;
            parameterName = yourName;
        }
    }

    /// <summary>
    /// Case的额外属性（可随时扩展）
    /// </summary>
    public struct myCaseAttribute
    {
        public int attributeDelay;
        public int attributeLevel;
        public List<ParameterSave> myParameterSaves;
        public myCaseAttribute(int yourDelay, int yourLevel)
        {
            attributeDelay = yourDelay;
            attributeLevel = yourLevel;
            myParameterSaves = null;
        }

        public void addParameterSave(string yourName, string yourFindVaule, PickOutFunction yourFunction)
        {
            if (myParameterSaves == null)
            {
                myParameterSaves = new List<ParameterSave>();
            }
            myParameterSaves.Add(new ParameterSave(yourFunction, yourName, yourFindVaule));
        }
    }

    /// <summary>
    /// 加载时所需要的节点/case信息
    /// </summary>
    public struct myCaseLaodInfo
    {
        public string ErrorMessage;
        public int id;
        public int times;
        public string remark;
        public string name;
        public CaseType caseType;
        public CaseProtocol caseProtocol;
        public string content;
        public Dictionary<CaseResult, CaseAction> actions;
        public myCaseLaodInfo(string tempVal)
        {
            ErrorMessage = "";
            remark = name = content = tempVal;
            id = 0;
            times = 1;
            caseType = CaseType.Unknown;
            caseProtocol = CaseProtocol.unknownProtocol;
            actions = new Dictionary<CaseResult, CaseAction>();
        }
    }

    /// <summary>
    /// 执行当前case所需要的信息
    /// </summary>
    public class myRunCaseData<T> where T : ICaseExecutionContent
    {
        public List<string> errorMessages;
        public int id;
        public CaseProtocol contentProtocol;
        public T testContent;
        public myExpectInfo caseExpectInfo;
        public Dictionary<CaseResult, caseActionDescription> actions;
        public myCaseAttribute caseAttribute;

        public myRunCaseData()
        {
        }
        public myRunCaseData(T yourContent, int yourId, CaseProtocol yourcontentProtocol)
        {
            errorMessages = null;
            testContent = yourContent;
            id = yourId;
            contentProtocol = yourcontentProtocol;
            actions = null;
            caseExpectInfo = new myExpectInfo();
            caseAttribute = new myCaseAttribute();
        }

        public myRunCaseData(T yourContent, int yourId, CaseProtocol yourcontentProtocol, string yourErrorMessage)
        {
            errorMessages = new List<string>();
            errorMessages.Add(yourErrorMessage);
            testContent = yourContent;
            id = yourId;
            contentProtocol = yourcontentProtocol;
            actions = null;
            caseExpectInfo = new myExpectInfo();
            caseAttribute = new myCaseAttribute();
        }

        public void addErrorMessage(string yourErrorMessage)
        {
            if (errorMessages == null)
            {
                errorMessages = new List<string>();
            }
            errorMessages.Add(yourErrorMessage);
        }

        public void addCaseAction(CaseResult yourCaseResult, caseActionDescription yourDescription)
        {
            if (actions == null)
            {
                actions = new Dictionary<CaseResult, caseActionDescription>();
            }
            actions.Add(yourCaseResult, yourDescription);
        }
    }



    #region ICaseExecutionContent

    /// <summary>
    /// vanelife_http协议类型Content内容
    /// 处理时请注意除了ErrorMessage可能为null，其他成员初始化全部不为null(所有请覆盖默认构造函数，为成员指定初始值)
    /// </summary>
    public class myHttpExecutionContent : ICaseExecutionContent
    {
        public string ErrorMessage;
        public string HttpTarget;
        public caseParameterizationContent caseExecutionContent;
        public string HttpMethod;
        public string caseActuator;
        public CaseProtocol caseProtocol;
        public HttpAisleConfig myHttpAisleConfig;
        public HttpMultipart myHttpMultipart;

        public myHttpExecutionContent()
        {
            ErrorMessage = null;
            caseExecutionContent = new caseParameterizationContent();
            HttpMethod = "";
            HttpTarget = "";
            caseActuator = "";
            myHttpMultipart = new HttpMultipart();
            myHttpAisleConfig = new HttpAisleConfig();
            caseProtocol = CaseProtocol.unknownProtocol;
        }

        public myHttpExecutionContent(string tempVal)
        {
            ErrorMessage = null;
            caseExecutionContent = new caseParameterizationContent();
            HttpMethod = tempVal;
            HttpTarget = tempVal;
            caseActuator = tempVal;
            myHttpMultipart = new HttpMultipart();
            myHttpAisleConfig = new HttpAisleConfig();
            caseProtocol = CaseProtocol.unknownProtocol;
        }

        public CaseProtocol myCaseProtocol
        {
            get
            {
                return caseProtocol;
            }
        }

        public string myExecutionTarget
        {
            get
            {
                return HttpTarget;
            }
        }

        public string myExecutionContent
        {
            get
            {
                return caseExecutionContent.contentData;
            }
        }


        public string myCaseActuator
        {
            get
            {
                return caseActuator;
            }
        }


        public string myErrorMessage
        {
            get
            {
                if (ErrorMessage != null)
                {
                    if (ErrorMessage == "")
                    {
                        return null;
                    }
                    else
                    {
                        return ErrorMessage;
                    }
                }
                else
                {
                    return null;
                }
            }
        }


    }


    public class myBasicHttpExecutionContent : ICaseExecutionContent
    {
        public string errorMessage;
        public CaseProtocol caseProtocol;
        public string caseActuator;

        public caseParameterizationContent httpUri;
        public string httpMethod;
        public List<KeyValuePair<string, caseParameterizationContent>> httpHeads;
        public caseParameterizationContent httpBody;
        public HttpAisleConfig myHttpAisleConfig;

        public myBasicHttpExecutionContent()
        {
            errorMessage = null;
            caseProtocol = CaseProtocol.unknownProtocol;
            caseActuator = "";
            httpUri = new caseParameterizationContent();
            httpMethod = "";
            httpHeads = new List<KeyValuePair<string, caseParameterizationContent>>();
            httpBody = new caseParameterizationContent();
            myHttpAisleConfig = new HttpAisleConfig();
        }

        public CaseProtocol myCaseProtocol
        {
            get { return caseProtocol; }
        }

        public string myCaseActuator
        {
            get { return caseActuator; }
        }

        public string myExecutionTarget
        {
            get { return httpUri.getTargetContentData(); }
        }

        public string myExecutionContent
        {
            get { return httpBody.getTargetContentData(); }
        }

        public string myErrorMessage
        {
            get
            {
                if (errorMessage != null)
                {
                    if (errorMessage == "")
                    {
                        return null;
                    }
                    else
                    {
                        return errorMessage;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }

    #endregion


    /// <summary>
    /// 老版本使用的结构，避免编译错误。 完成后将删除
    /// </summary>
    public struct myHttpConfig
    {
        public string ErrorMessage;
        public string HttpMethod;
        public string HttpUrl;
        public string HttpDataDown;
        public CaseProtocol caseProtocol;
        public HttpMultipart myHttpMultipart;
        public ParameterSave_old myParameterSave;
        public myHttpConfig(string tempVal)
        {
            ErrorMessage = null;
            HttpMethod = tempVal;
            HttpUrl = tempVal;
            HttpDataDown = tempVal;
            myHttpMultipart = new HttpMultipart();
            myParameterSave = new ParameterSave_old();
            caseProtocol = CaseProtocol.unknownProtocol;
        }

        public CaseProtocol myCaseProtocol
        {
            get
            {
                return caseProtocol;
            }
        }
    }

    /// <summary>
    /// 老版本使用的结构，避免编译错误。 完成后将删除
    /// </summary>
    public struct ParameterSave_old
    {
        public Dictionary<string, string> myParameter;
    }

    #region IRunTimeStaticData
    /// <summary>
    /// 为StaticData提供类似索引递增的动态数据【IRunTimeStaticData】
    /// </summary>
    public struct myStaticDataIndex : IRunTimeStaticData
    {
        private bool isNew;
        private int dataIndex;
        private int defaultStart;
        private int defaultEnd;
        private int defaultStep;

        public myStaticDataIndex(int yourStart, int yourEnd,int yourStep)
        {
            isNew = true;
            dataIndex = defaultStart = yourStart;
            defaultEnd = yourEnd;
            defaultStep = yourStep;
        }

        public object Clone()
        {
            return new myStaticDataIndex(defaultStart, defaultEnd, defaultStep);
        }


        public string dataCurrent()
        {
            return dataIndex.ToString();
        }

        public string dataMoveNext()
        {
            if(isNew)
            {
                isNew = false;
                return dataIndex.ToString();
            }
            if (dataIndex >= defaultEnd)
            {
                dataReset();
                return dataMoveNext();
            }
            else
            {
                dataIndex += defaultStep;
            }
            return dataIndex.ToString();
        }


        public void dataReset()
        {
            isNew = true;
            dataIndex = defaultStart;
        }


        public bool dataSet(string expectData)
        {
            int tempData;
            if(int.TryParse(expectData,out tempData))
            {
                if(tempData>=defaultStart &&tempData<=defaultEnd)
                {
                    dataIndex = tempData;
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// 为StaticData提供长数字索引支持【IRunTimeStaticData】
    /// </summary>
    public struct myStaticDataLong : IRunTimeStaticData
    {
        private bool isNew;
        private long dataIndex;
        private long defaultStart;
        private long defaultEnd;
        private long defaultStep;

        public myStaticDataLong(long yourStart, long yourEnd ,long yourStep)
        {
            isNew = true;
            dataIndex = defaultStart = yourStart;
            defaultEnd = yourEnd;
            defaultStep = yourStep;
        }

        public object Clone()
        {
            return new myStaticDataLong(defaultStart, defaultEnd, defaultStep);
        }


        public string dataCurrent()
        {
            return dataIndex.ToString();
        }

        public string dataMoveNext()
        {
            if (isNew)
            {
                isNew = false;
                return dataIndex.ToString();
            }
            if (dataIndex >= defaultEnd)
            {
                dataReset();
                return dataMoveNext();
            }
            else
            {
                dataIndex += defaultStep;
            }
            return dataIndex.ToString();
        }


        public void dataReset()
        {
            isNew = true;
            dataIndex = defaultStart;
        }


        public bool dataSet(string expectData)
        {
            long tempData;
            if (long.TryParse(expectData, out tempData))
            {
                if (tempData >= defaultStart && tempData <= defaultEnd)
                {
                    dataIndex = tempData;
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// 为StaticData提供随机字符串动态数据【IRunTimeStaticData】
    /// </summary>
    public struct myStaticDataRandomStr : IRunTimeStaticData
    {
        string myNowStr;
        int myStrNum;
        int myStrType;

        public myStaticDataRandomStr(int yourStrNum, int yourStrType)
        {
            myNowStr = "";
            myStrNum = yourStrNum;
            myStrType = yourStrType;
        }

        public object Clone()
        {
            return new myStaticDataRandomStr(myStrNum, myStrType);
        }

        public string dataCurrent()
        {
            return myNowStr;
        }

        public string dataMoveNext()
        {
            myNowStr = myCommonTool.GenerateRandomStr(myStrNum, myStrType);
            return myNowStr;
        }

        public void dataReset()
        {
            myNowStr = "";
        }


        public bool dataSet(string expectData)
        {
            if (expectData != null)
            {
                myNowStr = expectData;
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// 为StaticData提供当前时间的动态数据【IRunTimeStaticData】
    /// </summary>
    public struct myStaticDataNowTime : IRunTimeStaticData
    {
        string myNowStr;
        string myDataFormatInfo;

        public myStaticDataNowTime(string yourRormatInfo)
        {
            myNowStr = "";
            myDataFormatInfo = yourRormatInfo;
        }

        public object Clone()
        {
            return new myStaticDataNowTime(myDataFormatInfo);
        }

        public string dataCurrent()
        {
            return myNowStr;
        }

        public string dataMoveNext()
        {
            myNowStr = System.DateTime.Now.ToString(myDataFormatInfo);
            return myNowStr;
        }

        public void dataReset()
        {
            myNowStr = "";
        }


        public bool dataSet(string expectData)
        {
            if (expectData != null)
            {
                myNowStr = expectData;
                return true;
            }
            return false;
        }
    }

    public struct myStaticDataList : IRunTimeStaticData
    {
        private bool isNew;
        private string souseData;
        private List<string> souseListData;
        private int nowIndex;
        private bool isRandom;
        private Random ran;

        public myStaticDataList(string yourSourceData, bool isRandomNext)
        {
            isNew = true;
            souseData = yourSourceData;
            souseListData = yourSourceData.Split(',').ToList();
            nowIndex = 0;
            isRandom = isRandomNext;
            if(isRandom)
            {
                ran = new Random();
            }
            else
            {
                ran = null;
            }
        }

        public object Clone()
        {
            return new myStaticDataList(souseData, isRandom);
        }

        public string dataCurrent()
        {
            return souseListData[nowIndex];
        }

        public string dataMoveNext()
        {
            if (isRandom)
            {
                nowIndex = ran.Next(0, souseListData.Count - 1);
                return souseListData[nowIndex];
            }
            else
            {
                if (isNew)
                {
                    isNew = false;
                }
                else
                {
                    nowIndex++;
                    if (nowIndex > (souseListData.Count - 1))
                    {
                        nowIndex = 0;
                    }
                }
                return souseListData[nowIndex];
            }
        }

        public void dataReset()
        {
            isNew = true;
            nowIndex = 0;
        }

        public bool dataSet(string expectData)
        {
            if(souseListData.Contains(expectData))
            {
                nowIndex = souseListData.IndexOf("expectData");
            }
            return false;
        }
    }

    #endregion



}
