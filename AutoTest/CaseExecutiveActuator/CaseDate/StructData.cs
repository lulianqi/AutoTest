using CaseExecutiveActuator.CaseActuator;
using CaseExecutiveActuator.Tool;
using MyCommonHelper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
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
    /// 描述可参数化数据结构 (请尽量不要自行解析数据，建议使用getXmlParametContent解析xml数据)
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
        /// <param name="errorMessage">错误消息（如果没有错误则为null）(在获取参数化数据产生错误后因对当前case设置警示)</param>
        /// <returns>运算结果</returns>
        public string GetTargetContentData(ActuatorStaticDataCollection yourActuatorStaticDataCollection, NameValueCollection yourDataResultCollection, out string errorMessage)
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
                        myTargetContentData = MyCommonHelper.MyBytes.StringToHexString(myTargetContentData);
                        break;
                    case ParameterizationContentEncodingType.decode_hex16:
                        try
                        {
                            byte[] nowBytes = MyCommonHelper.MyBytes.HexStringToByte(myTargetContentData, HexaDecimal.hex16, ShowHexMode.space);
                            myTargetContentData = Encoding.UTF8.GetString(nowBytes);
                        }
                        catch (Exception ex)
                        {
                            myTargetContentData = "ContentEncoding Error:" + ex.Message;
                        }
                        break;
                    //hex 2
                    case ParameterizationContentEncodingType.encode_hex2:
                        myTargetContentData = MyCommonHelper.MyBytes.StringToHexString(myTargetContentData, Encoding.UTF8, HexaDecimal.hex2,ShowHexMode.space);
                        break;
                    case ParameterizationContentEncodingType.decode_hex2:
                        try
                        {
                            byte[] nowBytes = MyCommonHelper.MyBytes.HexStringToByte(myTargetContentData, HexaDecimal.hex2, ShowHexMode.space);
                            myTargetContentData = Encoding.UTF8.GetString(nowBytes);
                        }
                        catch (Exception ex)
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

        /// <summary>
        /// 获取原始数据，掉用此法的该版本的重载将不会会改变涉及到的staticData数据的游标，也不会对其进行运算
        /// </summary>
        /// <returns>原始数据数据</returns>
        public string GetTargetContentData()
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
        public string parameterAdditionalVaule;
        public PickOutFunction parameterFunction;

        public ParameterSave(PickOutFunction yourFunction, string yourName, string yourFindVaule)
        {
            parameterFunction = yourFunction;
            parameterFindVaule = yourFindVaule;
            parameterName = yourName;
            parameterAdditionalVaule = null;
        }

        public ParameterSave(PickOutFunction yourFunction, string yourName, string yourFindVaule,string yourAdditon)
        {
            parameterFunction = yourFunction;
            parameterFindVaule = yourFindVaule;
            parameterName = yourName;
            parameterAdditionalVaule = yourAdditon;
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

        public void addParameterSave(string yourName, string yourFindVaule, PickOutFunction yourFunction, string yourAdditional)
        {
            if (myParameterSaves == null)
            {
                myParameterSaves = new List<ParameterSave>();
            }
            myParameterSaves.Add(new ParameterSave(yourFunction, yourName, yourFindVaule, yourAdditional));
        }
    }

    /// <summary>
    /// 加载时所需要的节点/case信息
    /// </summary>
    public struct myCaseLaodInfo
    {
        public string ErrorMessage;
        public int id;
        public int times;   //仅repeat节点有意义
        public string remark;
        public string name;  //仅project节点有意义
        public CaseType caseType;
        public CaseProtocol caseProtocol;  //仅case节点有意义
        public string content;  //仅case节点有意义
        public Dictionary<CaseResult, CaseAction> actions;  //仅case节点有意义
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
        public string name;
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
            name = null;
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
            name = null;
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


}
