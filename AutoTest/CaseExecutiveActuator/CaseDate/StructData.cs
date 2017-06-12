using MyCommonHelper;
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
    public enum CaseActuatorOutPutType
    {
        ExecutiveInfo,
        ExecutiveError,
        ActuatorInfo,
        ActuatorError
    }


    /// <summary>
    /// case 运行状态
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
        console=3,
        vanelife_http = 4,
        vanelife_comm = 5,
        vanelife_tcp = 6,
        vanelife_telnet = 7,
        http = 8,
        tcp = 9,
        comm = 10,
        telnet = 11
    }

    /// <summary>
    /// 静态参数化数据大分类
    /// </summary>
    public enum CaseStaticDataClass
    {
        caseStaticDataKey=0,
        caseStaticDataParameter = 1,
        caseStaticDataSource=2
    }

    /// <summary>
    /// 描述框架脚本支持的静态参数化数据(请再此处扩展)
    /// </summary>
    public enum CaseStaticDataType
    {
        caseStaticData_vaule = 10000,

        caseStaticData_index = 20000,
        caseStaticData_long = 20001,
        caseStaticData_random = 20002,
        caseStaticData_time = 20003,
        caseStaticData_list = 20004,

        caseStaticData_csv = 30000,
        caseStaticData_mysql = 30001,
        caseStaticData_redis = 30002,
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
        encode_default = 0,
        encode_base64 = 1,
        decode_base64 = 2,
        encode_hex16 = 3,
        decode_hex16 = 4,
        encode_hex2 = 5,
        decode_hex2 = 6

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

        /// <summary>
        ///返回一个值指示该caseParameterizationContent是否有被任何数据填充过
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 获取运算后的值，掉用此法的该版本的重载将会改变涉及到的staticData数据的游标
        /// </summary>
        /// <param name="yourActuatorStaticDataCollection">可用staticData集合</param>
        /// <param name="yourDataResultCollection">返回对所有staticData数据运算后的结果列表</param>
        /// <param name="errorMessage">错误消息（如果没有错误则为null）</param>
        /// <returns>运算结果</returns>
        public string getTargetContentData(ActuatorStaticDataCollection yourActuatorStaticDataCollection, NameValueCollection yourDataResultCollection, out string errorMessage)
        {
            string myTargetContentData = contentData;
            errorMessage = null;
            if (hasParameter)
            {
                myTargetContentData = CaseTool.GetCurrentParametersData(contentData, MyConfiguration.ParametersDataSplitStr, yourActuatorStaticDataCollection, yourDataResultCollection, out errorMessage);
            }
            if (encodetype != ParameterizationContentEncodingType.encode_default)
            {
                switch (encodetype)
                {
                    //base64
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
                    //hex 16
                    case ParameterizationContentEncodingType.encode_hex16:
                        myTargetContentData = MyCommonHelper.MyEncryption.StringToHexString(myTargetContentData);
                        break;
                    case ParameterizationContentEncodingType.decode_hex16:
                        try
                        {
                            byte[] nowBytes = MyCommonHelper.MyEncryption.HexStringToByte(myTargetContentData, MyEncryption.HexaDecimal.hex16, MyEncryption.ShowHexMode.space);
                            myTargetContentData = Encoding.UTF8.GetString(nowBytes);
                        }
                        catch (Exception ex)
                        {
                            myTargetContentData = "ContentEncoding Error:" + ex.Message;
                        }
                        break;
                    //hex 2
                    case ParameterizationContentEncodingType.encode_hex2:
                        myTargetContentData = MyCommonHelper.MyEncryption.StringToHexString(myTargetContentData, Encoding.UTF8, MyEncryption.HexaDecimal.hex2, MyEncryption.ShowHexMode.space);
                        break;
                    case ParameterizationContentEncodingType.decode_hex2:
                        try
                        {
                            byte[] nowBytes = MyCommonHelper.MyEncryption.HexStringToByte(myTargetContentData, MyEncryption.HexaDecimal.hex2, MyEncryption.ShowHexMode.space);
                            myTargetContentData = Encoding.UTF8.GetString(nowBytes);
                        }
                        catch (Exception ex)
                        {
                            myTargetContentData = "ContentEncoding Error:" + ex.Message;
                        }
                        break;
                        break;
                    default:
                        errorMessage = "[getTargetContentData] unknow or not supported this encodetype";
                        break;
                }
            }
            return myTargetContentData;
        }

        /// <summary>
        /// 获取原始数据，掉用此法的该版本的重载将不会会改变涉及到的staticData数据的游标，也不会对其进行运算
        /// </summary>
        /// <returns>原始数据数据</returns>
        public string getTargetContentData()
        {
            return contentData;
        }
    }

    /// <summary>
    /// 描述脚本动作行为及附加信息
    /// </summary>
    public class CaseActionDescription
    {
        public CaseAction caseAction;
        public string addInfo;
        public CaseActionDescription(CaseAction yourCaseAction, string yourInfo)
        {
            caseAction = yourCaseAction;
            addInfo = yourInfo;
        }


    }

    #region ExecutionDevice初始化连接信息

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
        public CaseProtocol MyCaseProtocol
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
        public CaseProtocol MyCaseProtocol
        {
            get
            {
                return caseProtocol;
            }
        }
    }

    /// <summary>
    /// Console 【IConnectExecutiveData】
    /// </summary>
    public struct myConnectForConsole:IConnectExecutiveData
    {
        public CaseProtocol caseProtocol;

        public myConnectForConsole(CaseProtocol yourCaseProtocol)
        {
            caseProtocol = yourCaseProtocol;
        }
        public CaseProtocol MyCaseProtocol
        {
            get
            {
                return caseProtocol;
            }
        }
    }


    public struct myConnectForActiveMQ : IConnectExecutiveData
    {
        public CaseProtocol caseProtocol;

        public string brokerUri;
        public string clientId;
        public string factoryUserName;
        public string factoryPassword;

        public myConnectForActiveMQ(CaseProtocol yourCaseProtocol)
        {
            caseProtocol = yourCaseProtocol;
            brokerUri = null;
        }
        public CaseProtocol MyCaseProtocol
        {
            get
            {
                return caseProtocol;
            }
        }
    }
    #endregion

    

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
    public class MyExecutionDeviceResult
    {
        public int caseId;                                              //case ID
        public CaseProtocol caseProtocol;                               //case 协议类型
        public string requestTime;                                      //case 请求或执行时间 （这个时间指确认请求已经发出达到服务端，而不包括服务端的处理时间）(默认以毫秒为单位，可以支持更高的精度)
        public string spanTime;                                         //整个CASE 请求的执行时间 (默认以毫秒为单位，可以支持更高的精度)
        public string startTime;                                        //case 请求的开始时间     (尽量使用该种格式，并至少保证秒精度 DateTime.Now.ToString("HH:mm:ss");)
        public string caseTarget;                                       //当前case 请求的目标简述如接口名
        public string backContent;                                      //case 请求的返回数据
        public CaseExpectType expectMethod;                             //case 断言类型
        public string expectContent;                                    //case 断言数据
        public CaseResult result;                                       //case 执行结果    
        public NameValueCollection staticDataResultCollection;          //当前case在执行中所有静态初始化数据的运算结果
        public string additionalRemark;                                 //case 辅助备注
        public string additionalError;                                  //case 错误的辅助备注 （主要是请求本身失败或错误）
        
        public MyExecutionDeviceResult()
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
            additionalError = null;
        }

        public MyExecutionDeviceResult(int yourCaseId, string yourAdditionalError):this()
        {
            caseId = yourCaseId;
            backContent = yourAdditionalError;
            additionalError = yourAdditionalError;
        }
        public MyExecutionDeviceResult(CaseProtocol yourCaseProtocol, string yourSpanTime, string yourStartTime, string yourBackContent):this()
        {
            caseProtocol = yourCaseProtocol;
            spanTime = yourSpanTime;
            startTime = yourStartTime;
            backContent = yourBackContent;
            expectMethod = CaseExpectType.judge_default;
        }
        public MyExecutionDeviceResult(CaseProtocol yourCaseProtocol, string yourSpanTime, string yourStartTime, string yourBackContent, string yourAdditionalContent): this(yourCaseProtocol, yourStartTime, yourStartTime, yourBackContent)
        {
            additionalError = yourAdditionalContent;
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
        public MyRunCaseData<ICaseExecutionContent> tagCaseRunData;
        public myTreeTagInfo(CaseType yourCaseType, XmlNode yourXmlNode)
        {
            tagCaseType = yourCaseType;
            tagCaseXmlNode = yourXmlNode;
        }

        public myTreeTagInfo(CaseType yourCaseType, XmlNode yourXmlNode, MyRunCaseData<ICaseExecutionContent> yourCaseRunData)
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
        public MyCaseRunTime caseRunTime;
        public CaseActionActuator actionActuator;
        public myNewTestInfo(MyCaseRunTime yourRunTime, CaseActionActuator yourActuator)
        {
            caseRunTime = yourRunTime;
            actionActuator = yourActuator;
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
    public class MyRunCaseData<T> where T : ICaseExecutionContent
    {
        public List<string> errorMessages;
        public int id;
        public CaseProtocol contentProtocol;
        public T testContent;
        public myExpectInfo caseExpectInfo;
        public Dictionary<CaseResult, CaseActionDescription> actions;
        public myCaseAttribute caseAttribute;

        public MyRunCaseData()
        {
        }
        public MyRunCaseData(T yourContent, int yourId, CaseProtocol yourcontentProtocol)
        {
            errorMessages = null;
            testContent = yourContent;
            id = yourId;
            contentProtocol = yourcontentProtocol;
            actions = null;
            caseExpectInfo = new myExpectInfo();
            caseAttribute = new myCaseAttribute();
        }

        public MyRunCaseData(T yourContent, int yourId, CaseProtocol yourcontentProtocol, string yourErrorMessage)
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

        public void AddErrorMessage(string yourErrorMessage)
        {
            if (errorMessages == null)
            {
                errorMessages = new List<string>();
            }
            errorMessages.Add(yourErrorMessage);
        }

        public void AddCaseAction(CaseResult yourCaseResult, CaseActionDescription yourDescription)
        {
            if (actions == null)
            {
                actions = new Dictionary<CaseResult, CaseActionDescription>();
            }
            actions.Add(yourCaseResult, yourDescription);
        }
    }



    #region ICaseExecutionContent

    /// <summary>
    /// vanelife_http协议类型Content内容
    /// 处理时请注意除了ErrorMessage可能为null，其他成员初始化全部不为null(所有请覆盖默认构造函数，为成员指定初始值)
    /// </summary>
    public class MyVaneHttpExecutionContent : ICaseExecutionContent
    {
        public string errorMessage;
        public string httpTarget;
        public caseParameterizationContent caseExecutionContent;
        public string httpMethod;
        public string caseActuator;
        public CaseProtocol caseProtocol;
        public HttpAisleConfig myHttpAisleConfig;
        public HttpMultipart myHttpMultipart;

        public MyVaneHttpExecutionContent()
        {
            errorMessage = null;
            caseExecutionContent = new caseParameterizationContent();
            httpMethod = "";
            httpTarget = "";
            caseActuator = "";
            myHttpMultipart = new HttpMultipart();
            myHttpAisleConfig = new HttpAisleConfig();
            caseProtocol = CaseProtocol.unknownProtocol;
        }

        public MyVaneHttpExecutionContent(string tempVal)
        {
            errorMessage = null;
            caseExecutionContent = new caseParameterizationContent();
            httpMethod = tempVal;
            httpTarget = tempVal;
            caseActuator = tempVal;
            myHttpMultipart = new HttpMultipart();
            myHttpAisleConfig = new HttpAisleConfig();
            caseProtocol = CaseProtocol.unknownProtocol;
        }

        public CaseProtocol MyCaseProtocol
        {
            get
            {
                return caseProtocol;
            }
        }

        public string MyExecutionTarget
        {
            get
            {
                return httpTarget;
            }
        }

        public string MyExecutionContent
        {
            get
            {
                return caseExecutionContent.contentData;
            }
        }


        public string MyCaseActuator
        {
            get
            {
                return caseActuator;
            }
        }


        public string MyErrorMessage
        {
            get
            {
                return String.IsNullOrEmpty(errorMessage) ? null : errorMessage;
            }
        }


    }


    public class MyBasicHttpExecutionContent : ICaseExecutionContent
    {
        public string errorMessage;
        public CaseProtocol caseProtocol;
        public string caseActuator;

        public caseParameterizationContent httpUri;
        public string httpMethod;
        public List<KeyValuePair<string, caseParameterizationContent>> httpHeads;
        public caseParameterizationContent httpBody;
        public HttpAisleConfig myHttpAisleConfig;
        public List<HttpMultipart> myMultipartList;

        public MyBasicHttpExecutionContent()
        {
            errorMessage = null;
            caseProtocol = CaseProtocol.unknownProtocol;
            caseActuator = "";
            httpUri = new caseParameterizationContent();
            httpMethod = "";
            httpHeads = new List<KeyValuePair<string, caseParameterizationContent>>();
            httpBody = new caseParameterizationContent();
            myHttpAisleConfig = new HttpAisleConfig();
            myMultipartList = new List<HttpMultipart>();
            
        }

        public CaseProtocol MyCaseProtocol
        {
            get { return caseProtocol; }
        }

        public string MyCaseActuator
        {
            get { return caseActuator; }
        }

        public string MyExecutionTarget
        {
            get { return httpUri.getTargetContentData(); }
        }

        public string MyExecutionContent
        {
            get { return httpBody.getTargetContentData(); }
        }

        public string MyErrorMessage
        {
            get
            {
                return String.IsNullOrEmpty(errorMessage) ? null : errorMessage;
            }
        }
    }


    public class MyConsoleExecutionContent : ICaseExecutionContent
    {
        #region inner class
        public class StaticDataAdd
        {
            public CaseStaticDataType StaticDataType { get; set; }
            public String Name { get; set; }
            public caseParameterizationContent ConfigureData { get; set; }

            public StaticDataAdd(CaseStaticDataType yourStaticDataType, String yourName, caseParameterizationContent yourConfigureData)
            {
                StaticDataType = yourStaticDataType;
                Name = yourName;
                ConfigureData = yourConfigureData;
            }
        }

        #endregion

        public string errorMessage;
        public CaseProtocol caseProtocol;
        public string caseActuator;

        public caseParameterizationContent showContent;
        public List<KeyValuePair<string, caseParameterizationContent>> staticDataSetList;
        public List<StaticDataAdd> staticDataAddList;
        public List<KeyValuePair<bool, caseParameterizationContent>> staticDataDelList;

        public MyConsoleExecutionContent()
        {
            errorMessage = null;
            caseProtocol = CaseProtocol.unknownProtocol;
            caseActuator = "";
            showContent = new caseParameterizationContent();
            staticDataSetList = new List<KeyValuePair<string, caseParameterizationContent>>();
            staticDataAddList = new List<StaticDataAdd>();
            staticDataDelList = new List<KeyValuePair<bool, caseParameterizationContent>>();
        }

        public CaseProtocol MyCaseProtocol
        {
            get { return caseProtocol; }
        }

        public string MyCaseActuator
        {
            get { return caseActuator; }
        }

        public string MyExecutionTarget
        {
            get { return showContent.getTargetContentData(); }
        }

        public string MyExecutionContent
        {
            get { return null; }
        }

        public string MyErrorMessage
        {
            get
            {
                return String.IsNullOrEmpty(errorMessage) ? null : errorMessage;
            }
        }
    }
    #endregion



    #region IRunTimeStaticData

    /// <summary>
    /// 为StaticData提供类似索引递增的动态数据【IRunTimeStaticData】
    /// </summary>
    public struct MyStaticDataIndex : IRunTimeStaticData
    {
        private bool isNew;
        private int dataIndex;
        private int defaultStart;
        private int defaultEnd;
        private int defaultStep;

        public string RunTimeStaticDataType
        {
            get { return "staticData_index"; }
        }

        public MyStaticDataIndex(int yourStart, int yourEnd,int yourStep)
        {
            isNew = true;
            dataIndex = defaultStart = yourStart;
            defaultEnd = yourEnd;
            defaultStep = yourStep;
        }

        public object Clone()
        {
            return new MyStaticDataIndex(defaultStart, defaultEnd, defaultStep);
        }


        public string DataCurrent()
        {
            return dataIndex.ToString();
        }

        public string DataMoveNext()
        {
            if(isNew)
            {
                isNew = false;
                return dataIndex.ToString();
            }
            if (dataIndex >= defaultEnd)
            {
                DataReset();
                return DataMoveNext();
            }
            else
            {
                dataIndex += defaultStep;
            }
            return dataIndex.ToString();
        }


        public void DataReset()
        {
            isNew = true;
            dataIndex = defaultStart;
        }


        public bool DataSet(string expectData)
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
    public struct MyStaticDataLong : IRunTimeStaticData
    {
        private bool isNew;
        private long dataIndex;
        private long defaultStart;
        private long defaultEnd;
        private long defaultStep;

        public string RunTimeStaticDataType
        {
            get { return "staticData_long"; }
        }
        public MyStaticDataLong(long yourStart, long yourEnd ,long yourStep)
        {
            isNew = true;
            dataIndex = defaultStart = yourStart;
            defaultEnd = yourEnd;
            defaultStep = yourStep;
        }

        public object Clone()
        {
            return new MyStaticDataLong(defaultStart, defaultEnd, defaultStep);
        }


        public string DataCurrent()
        {
            return dataIndex.ToString();
        }

        public string DataMoveNext()
        {
            if (isNew)
            {
                isNew = false;
                return dataIndex.ToString();
            }
            if (dataIndex >= defaultEnd)
            {
                DataReset();
                return DataMoveNext();
            }
            else
            {
                dataIndex += defaultStep;
            }
            return dataIndex.ToString();
        }


        public void DataReset()
        {
            isNew = true;
            dataIndex = defaultStart;
        }


        public bool DataSet(string expectData)
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
    public struct MyStaticDataRandomStr : IRunTimeStaticData
    {
        string myNowStr;
        int myStrNum;
        int myStrType;

        public string RunTimeStaticDataType
        {
            get { return "staticData_random"; }
        }

        public MyStaticDataRandomStr(int yourStrNum, int yourStrType)
        {
            myNowStr = "";
            myStrNum = yourStrNum;
            myStrType = yourStrType;
        }

        public object Clone()
        {
            return new MyStaticDataRandomStr(myStrNum, myStrType);
        }

        public string DataCurrent()
        {
            return myNowStr;
        }

        public string DataMoveNext()
        {
            myNowStr = MyCommonTool.GenerateRandomStr(myStrNum, myStrType);
            return myNowStr;
        }

        public void DataReset()
        {
            myNowStr = "";
        }


        public bool DataSet(string expectData)
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
    public struct MyStaticDataNowTime : IRunTimeStaticData
    {
        string myNowStr;
        string myDataFormatInfo;

        public string RunTimeStaticDataType
        {
            get { return "staticData_time"; }
        }

        public MyStaticDataNowTime(string yourRormatInfo)
        {
            myNowStr = "";
            myDataFormatInfo = yourRormatInfo;
        }

        public object Clone()
        {
            return new MyStaticDataNowTime(myDataFormatInfo);
        }

        public string DataCurrent()
        {
            return myNowStr;
        }

        public string DataMoveNext()
        {
            myNowStr = System.DateTime.Now.ToString(myDataFormatInfo);
            return myNowStr;
        }

        public void DataReset()
        {
            myNowStr = "";
        }


        public bool DataSet(string expectData)
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
    ///  为StaticData提供当基于List的列表数据支持据【IRunTimeStaticData】
    /// </summary>
    public struct MyStaticDataList : IRunTimeStaticData
    {
        private bool isNew;
        private string souseData;
        private List<string> souseListData;
        private int nowIndex;
        private bool isRandom;
        private Random ran;

        public string RunTimeStaticDataType
        {
            get { return "staticData_list"; }
        }

        public MyStaticDataList(string yourSourceData, bool isRandomNext)
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
            return new MyStaticDataList(souseData, isRandom);
        }

        public string DataCurrent()
        {
            return souseListData[nowIndex];
        }

        public string DataMoveNext()
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

        public void DataReset()
        {
            isNew = true;
            nowIndex = 0;
        }

        public bool DataSet(string expectData)
        {
            if(souseListData.Contains(expectData))
            {
                nowIndex = souseListData.IndexOf(expectData);
            }
            return false;
        }
    }

    #endregion


    #region IRunTimeDataSource 
    public struct MyStaticDataSourceCsv:IRunTimeDataSource
    {
        private bool isNew;
        private int nowRowIndex;
        private int nowColumnIndex;
        private List<List<string>> csvData;

        public string RunTimeStaticDataType
        {
            get { return "staticDataSource_csv"; }
        }

        public MyStaticDataSourceCsv(List<List<string>> yourCsvData)
        {
            isNew = true;
            nowRowIndex = 0;
            nowColumnIndex = 0;
            csvData = yourCsvData;
        }
        public object Clone()
        {
            return new MyStaticDataSourceCsv(csvData);
        }
        public bool IsConnected
        {
            get { return true; }
        }

        public bool ConnectDataSource()
        {
            return true;
        }

        public bool DisConnectDataSource()
        {
            return true;
        }

        public string GetDataVaule(string vauleAddress)
        {
            if (vauleAddress != null)
            {
                int[] csvPosition;
                if (vauleAddress.MySplitToIntArray('-',out csvPosition))
                {
                    if(csvPosition.Length==2)
                    {
                        return GetDataVaule(csvPosition[0], csvPosition[1]);
                    }
                }
            }
            return null;
        }

        public string GetDataVaule(int yourRowIndex,int yourColumnIndex)
        {
            if(yourRowIndex<csvData.Count)
            {
                if(yourColumnIndex<csvData[yourRowIndex].Count)
                {
                    return csvData[yourRowIndex][yourColumnIndex];
                }
            }
            return null;
        }

        public string DataCurrent()
        {
            //不需要检查 Index ，索引在内部操作，不可能越界
            return csvData[nowRowIndex][nowColumnIndex];
        }

        public string DataMoveNext()
        {
            if(isNew)
            {
                isNew = false;
            }
            else
            {
                //内部游标没有变化前不会越界
                if (nowColumnIndex + 1 < csvData[nowRowIndex].Count)
                {
                    nowColumnIndex++;
                }
                else if (nowRowIndex + 1 < csvData.Count)
                {
                    nowColumnIndex = 0;
                    nowRowIndex++;
                }
                else
                {
                    DataReset();
                }
            }
            return DataCurrent();
        }

        public void DataReset()
        {
            //对于csv文件解析出来的数据不可能出现空行空列的情况，所以（0,0）
            nowRowIndex = 0;
            nowColumnIndex = 0;
            isNew = true;
        }

        /// <summary>
        ///  设置源数据（使用|分割数据地址及数据值，如果以|开头则表示设置当前地址的值，不含有|的数据也表示当前值）
        /// </summary>
        /// <param name="expectData">数据地址及数据内容字符串</param>
        /// <returns>是否完成</returns>
        public bool DataSet(string expectData)
        {
            if (expectData!=null)
            {
                int splitIndex = expectData.IndexOf('|');
                if (splitIndex>0)
                {
                    return DataSet(expectData.Substring(0, splitIndex), expectData.Remove(0,splitIndex)+1);
                }
                else
                {
                    csvData[nowRowIndex][nowColumnIndex] = expectData;
                }
                return true;
            }
            return false;
        }

        public bool DataSet(int yourRowIndex,int yourColumnIndex ,string expectData)
        {
            if (yourRowIndex > csvData.Count-1)
            {
                for(int i=0 ;i<yourRowIndex-csvData.Count+1;i++)
                {
                    csvData.Add(new List<string>{""});
                }
            }
            if (yourColumnIndex > csvData[yourRowIndex].Count-1)
            {
                for (int i = 0; i < yourColumnIndex - csvData[yourRowIndex].Count + 1; i++)
                {
                    csvData[yourRowIndex].Add("");
                }
            }
            csvData[yourRowIndex][yourColumnIndex] = expectData;
            return false;
        }

        public bool DataSet(string vauleAddress, string expectData)
        {
            if (vauleAddress != null)
            {
                int[] csvPosition;
                if (vauleAddress.MySplitToIntArray('-', out csvPosition))
                {
                    if (csvPosition.Length == 2)
                    {
                        DataSet(csvPosition[0], csvPosition[1], expectData);
                        return true;
                    }
                }
            }
            return false;
        }
    }

    #endregion
}
