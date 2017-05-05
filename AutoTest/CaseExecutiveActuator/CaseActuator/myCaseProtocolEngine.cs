using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using MyCommonHelper;

using CaseExecutiveActuator.ProtocolExecutive;

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
    using HttpMultipartDate = MyCommonHelper.NetHelper.MyWebTool.HttpMultipartDate;
  
    public class BasicProtocolPars
    {
        /// <summary>
        /// 从XML源数据获取可执行case的执行器数据（请使用new关键字隐藏基类方法，在子类中实现解析）
        /// </summary>
        /// <param name="yourContentNode">xml 源数据</param>
        /// <returns>ICaseExecutionContent (在子类实现时返回值可以为实际类型)</returns>
        public static ICaseExecutionContent GetRunContent(XmlNode yourContentNode)
        {
            throw new NotImplementedException();
        }
    }

    #region ExecutionDevice

    /// <summary>
    /// Console Device 
    /// </summary>
    public class CaseProtocolExecutionForConsole:BasicProtocolPars, ICaseExecutionDevice
    {
        private bool isConnect;
        private myConnectForConsole myExecutionDeviceInfo;
        public event delegateGetExecutiveData OnGetExecutiveData;

        public new static MyConsoleExecutionContent GetRunContent(XmlNode yourContentNode)
        {
            MyConsoleExecutionContent myRunContent = new MyConsoleExecutionContent();
            if (yourContentNode != null)
            {
                if (yourContentNode.Attributes["protocol"] != null && yourContentNode.Attributes["actuator"] != null)
                {
                    //Content
                    try
                    {
                        myRunContent.caseProtocol = (CaseProtocol)Enum.Parse(typeof(CaseProtocol), yourContentNode.Attributes["protocol"].Value);
                    }
                    catch
                    {
                        myRunContent.errorMessage = "Error :error protocol in Content";
                        return myRunContent;
                    }
                    myRunContent.caseActuator = yourContentNode.Attributes["actuator"].Value;

                    //Show
                    XmlNode tempShowDataNode = yourContentNode["Show"];
                    if (tempShowDataNode != null)
                    {
                        myRunContent.showContent = CaseTool.getXmlParametContent(tempShowDataNode);
                    }
                    else
                    {
                        myRunContent.errorMessage = "Error :can not find [Show] node (this element is necessary)";
                        return myRunContent;
                    }

                    //ConsoleTask
                    #region ConsoleTask
                    XmlNode tempConsoleTaskDataNode = yourContentNode["ConsoleTask"];
                    if (tempConsoleTaskDataNode != null)
                    {
                        if (tempConsoleTaskDataNode.HasChildNodes)
                        {
                            foreach (XmlNode taskNode in tempConsoleTaskDataNode.ChildNodes)
                            {
                                if (taskNode.Name == "Set")
                                {
                                    if (taskNode.Attributes["name"] != null)
                                    {
                                        myRunContent.staticDataSetList.Add(new KeyValuePair<string, caseParameterizationContent>(taskNode.Attributes["name"].Value, CaseTool.getXmlParametContent(taskNode)));
                                    }
                                    else
                                    {
                                        myRunContent.errorMessage = "Error :can not find name in [Set] node";
                                        return myRunContent;
                                    }
                                }
                                else if (taskNode.Name == "Add")
                                {
                                    if (taskNode.Attributes["name"] != null && taskNode.Attributes["type"] != null)
                                    {
                                        CaseStaticDataType nowType;
                                        if (Enum.TryParse<CaseStaticDataType>("caseStaticData_"+taskNode.Attributes["type"].Value, out nowType))
                                        {
                                            myRunContent.staticDataAddList.Add(new MyConsoleExecutionContent.StaticDataAdd(nowType, taskNode.Attributes["name"].Value, CaseTool.getXmlParametContent(taskNode)));
                                        }
                                        else
                                        {
                                            myRunContent.errorMessage = string.Format("Error : find the unkonw  type in [ConsoleTask] with {0} when Add", taskNode.Attributes["type"].Value);
                                            return myRunContent;
                                        }
                                    }
                                    else
                                    {
                                        myRunContent.errorMessage = "Error :can not find name or type in [Add] node";
                                        return myRunContent;
                                    }
                                }
                                else if (taskNode.Name == "Del")
                                {
                                    bool isRegex=false;
                                    if (taskNode.Attributes["isRegex"] != null)
                                    {
                                        if(taskNode.Attributes["isRegex"].Value=="true")
                                        {
                                            isRegex = true;
                                        }
                                    }
                                    myRunContent.staticDataDelList.Add(new KeyValuePair<bool, caseParameterizationContent>(isRegex, CaseTool.getXmlParametContent(taskNode)));
                                }
                                else
                                {
                                    myRunContent.errorMessage = "find unknow node in [ConsoleTask]";
                                    return myRunContent;
                                }
                            }
                        }
                    } 
                    #endregion
                }
                else
                {
                    myRunContent.errorMessage = "Error :can not find protocol or actuator in Content ";
                }
            }
            else
            {
                myRunContent.errorMessage = "Error :yourContentNode is null";
            }
            return myRunContent;
        }
        public CaseProtocolExecutionForConsole(myConnectForConsole yourConnectInfo)
        {
            myExecutionDeviceInfo = yourConnectInfo;
        }

        public CaseProtocol ProtocolType
        {
            get
            {
                return myExecutionDeviceInfo.caseProtocol;
            }
        }

        public bool IsDeviceConnect
        {
            get
            {
                return isConnect;
            }
        }

        public bool ExecutionDeviceConnect()
        {
            isConnect = true;
            return true;
        }

        public void ExecutionDeviceClose()
        {
            isConnect = false;
        }

        public MyExecutionDeviceResult ExecutionDeviceRun(ICaseExecutionContent yourExecutionContent, delegateGetExecutiveData yourExecutiveDelegate, string sender, ActuatorStaticDataCollection yourActuatorStaticDataCollection, int caseId)
        {
            List<string> errorList = new List<string>();
            string tempError = null;
            MyExecutionDeviceResult myResult = new MyExecutionDeviceResult();
            myResult.staticDataResultCollection = new System.Collections.Specialized.NameValueCollection();

            Func<string ,bool> DealNowError = (errerData) =>
            {
                if (errerData != null)
                {
                    yourExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveError, errerData);
                    errorList.Add(errerData);
                    return true;
                }
                return false;
            };

            Func<string, string, string, bool> DealNowResultError = (errerData, actionType, keyName) =>
            {
                if(DealNowError(errerData))
                {
                    yourExecutiveDelegate(string.Format("[CaseProtocolExecutionForConsole][ExecutionDeviceRun][{0}]", actionType), CaseActuatorOutPutType.ExecutiveError, string.Format("static data {0} error with the key :{1} ",actionType ,keyName));
                    return true;
                }
                else
                {
                    return false;
                }
            };

            if (yourExecutionContent.MyCaseProtocol == CaseProtocol.console)
            {
                //在调用该函数前保证nowExecutionContent.ErrorMessage为空，且as一定成功
                MyConsoleExecutionContent nowExecutionContent = yourExecutionContent as MyConsoleExecutionContent;
                myResult.caseProtocol = CaseProtocol.console;
                myResult.caseTarget = nowExecutionContent.MyExecutionTarget;
                myResult.startTime = DateTime.Now.ToString("HH:mm:ss");
                System.Diagnostics.Stopwatch myWatch = new System.Diagnostics.Stopwatch();
                myWatch.Start();

                #region Show
                myResult.backContent=nowExecutionContent.showContent.getTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError);
                DealNowError(tempError);
                yourExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, string.Format("【ID:{0}】Executive···\r\n【Console】\r\n{1}", caseId, myResult.backContent));
                #endregion

                #region Add
                if (nowExecutionContent.staticDataAddList.Count > 0)
                {
                    foreach (var addInfo in nowExecutionContent.staticDataAddList)
                    {
                        switch (MyCaseDataTypeEngine.dictionaryStaticDataTypeClass[addInfo.StaticDataType])
                        {
                            //caseStaticDataKey 
                            case CaseStaticDataClass.caseStaticDataKey:
                                string tempRunTimeDataKey;
                                if (addInfo.StaticDataType == CaseStaticDataType.caseStaticData_vaule)
                                {
                                    tempRunTimeDataKey=addInfo.ConfigureData.getTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError);
                                }
                                else
                                {
                                    yourExecutiveDelegate("[CaseProtocolExecutionForConsole][ExecutionDeviceRun][Add]", CaseActuatorOutPutType.ExecutiveError, string.Format("find nonsupport Protocol"));
                                    break;
                                }
                                //如果使用提供的方式进行添加是不会出现错误的（遇到重复会覆盖，只有发现多个同名Key才会返回错误）, 不过getTargetContentData需要处理用户数据可能出现错误。
                                if (!DealNowResultError(tempError, "Add", addInfo.Name))
                                {
                                    yourActuatorStaticDataCollection.AddStaticDataKey(addInfo.Name, tempRunTimeDataKey);
                                    yourExecutiveDelegate("[CaseProtocolExecutionForConsole][ExecutionDeviceRun][Add]", CaseActuatorOutPutType.ExecutiveInfo, string.Format("static data add sucess with the key :{0} ", addInfo.Name));
                                }
                                break;
                            //caseStaticDataParameter
                            case CaseStaticDataClass.caseStaticDataParameter:
                                IRunTimeStaticData tempRunTimeStaticData;
                                string tempTypeError;
                                if (MyCaseDataTypeEngine.dictionaryStaticDataParameterAction[addInfo.StaticDataType](out tempRunTimeStaticData, out tempTypeError, addInfo.ConfigureData.getTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError)))
                                {
                                    if (!DealNowResultError(tempError,"Add",addInfo.Name))
                                    {
                                        yourActuatorStaticDataCollection.AddStaticDataParameter(addInfo.Name, tempRunTimeStaticData);
                                        yourExecutiveDelegate("[CaseProtocolExecutionForConsole][ExecutionDeviceRun][Add]", CaseActuatorOutPutType.ExecutiveInfo, string.Format("static data add sucess with the key :{0} ", addInfo.Name));
                                    }
                                }
                                else
                                {
                                    DealNowResultError("[yourActuatorStaticDataCollection][caseStaticDataParameter][GetStaticDataAction] error", "Add", addInfo.Name);
                                }
                                break;
                            //caseStaticDataSource
                            case CaseStaticDataClass.caseStaticDataSource:
                                IRunTimeDataSource tempRunTimeDataSource;
                                if (MyCaseDataTypeEngine.dictionaryStaticDataSourceAction[addInfo.StaticDataType](out tempRunTimeDataSource, out tempTypeError, addInfo.ConfigureData.getTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError)))
                                {
                                    if (!DealNowResultError(tempError,"Add",addInfo.Name))
                                    {
                                        yourActuatorStaticDataCollection.AddStaticDataSouce(addInfo.Name, tempRunTimeDataSource);
                                        yourExecutiveDelegate("[CaseProtocolExecutionForConsole][ExecutionDeviceRun][Add]", CaseActuatorOutPutType.ExecutiveInfo, string.Format("static data add sucess with the key :{0} ", addInfo.Name));
                                    }
                                }
                                else
                                {
                                    DealNowResultError("[yourActuatorStaticDataCollection][caseStaticDataSource][GetStaticDataAction] error", "Add", addInfo.Name);
                                }
                                break;
                            default:
                                DealNowError("[CaseProtocolExecutionForConsole][ExecutionDeviceRun][Add] find nonsupport Protocol");
                                break;
                        }
                    }
                } 
                #endregion

                #region Set
                if (nowExecutionContent.staticDataSetList.Count > 0)
                {
                    foreach (var addInfo in nowExecutionContent.staticDataSetList)
                    {
                        string tempSetVauleStr=addInfo.Value.getTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError);
                        if (!DealNowResultError(tempError,"Set",addInfo.Key))
                        {
                            if (yourActuatorStaticDataCollection.SetStaticData(addInfo.Key, tempSetVauleStr))
                            {
                                yourExecutiveDelegate("[CaseProtocolExecutionForConsole][ExecutionDeviceRun][Set]", CaseActuatorOutPutType.ExecutiveInfo, string.Format("static data set sucess with the key :{0} ", addInfo.Key));
                            }
                            else
                            {
                                DealNowResultError("[yourActuatorStaticDataCollection.SetStaticData] error", "Set", addInfo.Key);
                            }
                        }
                    }
                }
                #endregion

                #region Del
                if (nowExecutionContent.staticDataDelList.Count > 0)
                {
                    foreach (var delInfo in nowExecutionContent.staticDataDelList)
                    {
                        string tempDelVauleStr=delInfo.Value.getTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError);
                        if (!DealNowResultError(tempError, "Del", delInfo.Value.getTargetContentData()))
                        {
                            if (yourActuatorStaticDataCollection.RemoveStaticData(tempDelVauleStr, delInfo.Key))
                            {
                                yourExecutiveDelegate("[CaseProtocolExecutionForConsole][ExecutionDeviceRun][Del]", CaseActuatorOutPutType.ExecutiveInfo, string.Format("static data del sucess with the key :{0} ", tempDelVauleStr));
                            }
                            else
                            {
                                DealNowResultError("[yourActuatorStaticDataCollection.RemoveStaticData] error", "Del", delInfo.Value.getTargetContentData());
                                
                            }
                        }
                    }
                }
                #endregion
                myWatch.Stop();
                myResult.spanTime = myResult.requestTime = myWatch.ElapsedMilliseconds.ToString();
            }
            else
            {
                DealNowError("error:your CaseProtocol is not Matching RunTimeActuator");
            }

            if (errorList.Count > 0)
            {
                myResult.additionalError = errorList.MyToString("\r\n");
            }

            return myResult;
        }

        public object Clone()
        {
            return new CaseProtocolExecutionForConsole(myExecutionDeviceInfo);
        }
    }


    /// <summary>
    /// Vanelife_http Device 
    /// </summary>
    public class CaseProtocolExecutionForVanelife_http :BasicProtocolPars, ICaseExecutionDevice
    {
        private bool isConnect;
        private myConnectForVanelife_http myExecutionDeviceInfo;

        public event delegateGetExecutiveData OnGetExecutiveData;

        /// <summary>
        /// here i can get the data your need in the  XmlNode the for the 【ICaseExecutionDevice】
        /// </summary>
        /// <param name="yourContentNode">souce XmlNode</param>
        /// <returns>the data your need</returns>
        public new static MyVaneHttpExecutionContent GetRunContent(XmlNode yourContentNode)
        {
            MyVaneHttpExecutionContent myRunContent = new MyVaneHttpExecutionContent();
            if (yourContentNode != null)
            {
                if (yourContentNode.Attributes["protocol"] != null && yourContentNode.Attributes["actuator"] != null)
                {
                    //Content
                    try
                    {
                        myRunContent.caseProtocol = (CaseProtocol)Enum.Parse(typeof(CaseProtocol), yourContentNode.Attributes["protocol"].Value);
                    }
                    catch
                    {
                        myRunContent.errorMessage = "Error :error protocol in Content";
                        return myRunContent;
                    }
                    myRunContent.caseActuator = yourContentNode.Attributes["actuator"].Value;

                    //ContentData
                    XmlNode tempContentDataNode = yourContentNode["ContentData"];
                    if (tempContentDataNode != null)
                    {
                        if (tempContentDataNode.Attributes["target"] != null)
                        {
                            myRunContent.httpTarget = tempContentDataNode.Attributes["target"].Value;
                        }
                        else
                        {
                            myRunContent.httpTarget = "";
                        }

                        if (tempContentDataNode.Attributes["isHaveParameters"] != null)
                        {
                            if (tempContentDataNode.Attributes["isHaveParameters"].Value == "true")
                            {
                                myRunContent.caseExecutionContent.hasParameter = true;//hasParameter默认为false，其他情况无需设置该值
                            }
                        }
                        myRunContent.caseExecutionContent.contentData = tempContentDataNode.InnerText;
                    }
                    else
                    {
                        myRunContent.errorMessage = "Error :can not find ContentData , it is necessary in [vanelife_http]";
                        return myRunContent;
                    }

                    //HttpConfig
                    XmlNode tempHttpConfigDataNode = yourContentNode["HttpConfig"];
                    if (tempHttpConfigDataNode != null)
                    {
                        if (tempHttpConfigDataNode.Attributes["httpMethod"] != null)
                        {
                            myRunContent.httpMethod = tempHttpConfigDataNode.Attributes["httpMethod"].Value;
                        }
                        else
                        {
                            myRunContent.httpMethod = "POST";
                        }

                        if (tempHttpConfigDataNode["AisleConfig"] != null)
                        {
                            myRunContent.myHttpAisleConfig.httpAddress = CaseTool.getXmlParametContent(tempHttpConfigDataNode["AisleConfig"], "HttpAddress");
                            myRunContent.myHttpAisleConfig.httpDataDown = CaseTool.getXmlParametContent(tempHttpConfigDataNode["AisleConfig"], "HttpDataDown");
                        }
                        if (tempHttpConfigDataNode["HttpMultipart"] != null)
                        {
                            if (tempHttpConfigDataNode["HttpMultipart"]["MultipartData"] != null)
                            {
                                myRunContent.myHttpMultipart.isFile = false;
                                myRunContent.myHttpMultipart.name = CaseTool.getXmlAttributeVaule(tempHttpConfigDataNode["HttpMultipart"]["MultipartData"], "name");
                                myRunContent.myHttpMultipart.fileName = CaseTool.getXmlAttributeVaule(tempHttpConfigDataNode["HttpMultipart"]["MultipartData"], "filename");
                                myRunContent.myHttpMultipart.fileData = tempHttpConfigDataNode["HttpMultipart"]["MultipartData"].InnerText;
                            }
                            else if (tempHttpConfigDataNode["HttpMultipart"]["MultipartFile"] != null)
                            {
                                myRunContent.myHttpMultipart.isFile = true;
                                myRunContent.myHttpMultipart.name = CaseTool.getXmlAttributeVaule(tempHttpConfigDataNode["HttpMultipart"]["MultipartFile"], "name");
                                myRunContent.myHttpMultipart.fileName = CaseTool.getXmlAttributeVaule(tempHttpConfigDataNode["HttpMultipart"]["MultipartFile"], "filename");
                                myRunContent.myHttpMultipart.fileData = CaseTool.GetFullPath(tempHttpConfigDataNode["HttpMultipart"]["MultipartFile"].InnerText, MyConfiguration.CaseFilePath);
                            }
                        }
                    }
                    else
                    {
                        myRunContent.httpMethod = "POST";
                    }

                }
                else
                {
                    myRunContent.errorMessage = "Error :can not find protocol or actuator in Content ";
                }
            }
            else
            {
                myRunContent.errorMessage = "Error :yourContentNode is null";
            }
            return myRunContent;
        }

        public CaseProtocolExecutionForVanelife_http(myConnectForVanelife_http yourConnectInfo)
        {
            myExecutionDeviceInfo = yourConnectInfo;
        }

        public object Clone()
        {
            return new CaseProtocolExecutionForVanelife_http(myExecutionDeviceInfo);
        }

        public CaseProtocol ProtocolType
        {
            get
            {
                return myExecutionDeviceInfo.caseProtocol;
            }
        }

        public bool ExecutionDeviceConnect()
        {
            isConnect = true;
            return true;
        }

        public bool IsDeviceConnect
        {
            get
            {
                return isConnect;
            }
        }

        public void ExecutionDeviceClose()
        {
            isConnect = false;
        }


        public MyExecutionDeviceResult ExecutionDeviceRun(ICaseExecutionContent yourExecutionContent, delegateGetExecutiveData yourExecutiveDelegate, string sender, ActuatorStaticDataCollection yourActuatorStaticDataCollection, int caseId)
        {
            MyExecutionDeviceResult myResult = new MyExecutionDeviceResult();
            myResult.staticDataResultCollection = new System.Collections.Specialized.NameValueCollection();//默认该值为null，不会输出参数数据结果（如果不需要输出可以保持该字段为null）
            if(yourExecutionContent.MyCaseProtocol==CaseProtocol.vanelife_http)
            {
                //在调用该函数前保证nowExecutionContent.ErrorMessage为空，且as一定成功
                MyVaneHttpExecutionContent nowExecutionContent = yourExecutionContent as MyVaneHttpExecutionContent;
                myResult.caseProtocol = CaseProtocol.vanelife_http;
                myResult.caseTarget = nowExecutionContent.MyExecutionTarget;
                string tempError;
                string tempUrlAddress;
                string vanelifeData = CreatVanelifeSendData(nowExecutionContent.caseExecutionContent.getTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError));
                if (nowExecutionContent.myHttpAisleConfig.httpAddress.IsFilled())
                {
                    tempUrlAddress = nowExecutionContent.myHttpAisleConfig.httpAddress.getTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError) + nowExecutionContent.httpTarget;
                }
                else
                {
                    tempUrlAddress = myExecutionDeviceInfo.default_url + nowExecutionContent.httpTarget;
                }

                //report Executive Data
                if (yourExecutiveDelegate != null)
                {
                    yourExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo , string.Format("【ID:{0}】Executive···\r\n{1}\r\n{2}", caseId, tempUrlAddress, vanelifeData));
                }

                //Start Http 
                if (nowExecutionContent.myHttpAisleConfig.httpDataDown.IsFilled())
                {
                    AtHttpProtocol.HttpClient.SendData(tempUrlAddress, vanelifeData, nowExecutionContent.httpMethod, myResult, CaseTool.GetFullPath(nowExecutionContent.myHttpAisleConfig.httpDataDown.getTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError), MyConfiguration.CaseFilePath));
                }
                else
                {
                    if(nowExecutionContent.myHttpMultipart.IsFilled())
                    {
                        //由于vanelife协议要求在Multipart把业务数据全部放在了url中
                        AtHttpProtocol.HttpClient.HttpPostData(tempUrlAddress + "?" + vanelifeData, 30000, nowExecutionContent.myHttpMultipart.name, nowExecutionContent.myHttpMultipart.fileName, nowExecutionContent.myHttpMultipart.isFile, nowExecutionContent.myHttpMultipart.fileData, null, myResult);
                    }
                    else
                    {
                        AtHttpProtocol.HttpClient.SendData(tempUrlAddress, vanelifeData, nowExecutionContent.httpMethod, myResult);
                    }
                }

                if (tempError != null)
                {
                    myResult.additionalError = ("error:" + tempError);
                }

            }
            else
            {
                myResult.additionalError=("error:your CaseProtocol is not Matching RunTimeActuator");
            }
            return myResult;
        }

        /// <summary>
        /// 生成Vanelife协议数据
        /// </summary>
        /// <param name="testData">用例数据</param>
        /// <returns>协议数据</returns>
        /// 
        private string CreatVanelifeSendData(string testData)
        {
            Hashtable myDataTable = new Hashtable();
            StringBuilder myStrBld = new StringBuilder();
            string tempSign = "";

            #region 填装数据
            string[] sArray = testData.Split('&');
            if (testData == "")
            {
                //do nothing
            }
            else
            {
                foreach (string tempStr in sArray)
                {
                    int myBreak = tempStr.IndexOf('=');
                    if (myBreak == -1)
                    {
                        return "can't find =";
                    }
                    myDataTable.Add(tempStr.Substring(0, myBreak), tempStr.Substring(myBreak + 1));
                }
            }
            myDataTable.Add("key", myExecutionDeviceInfo.dev_key);
            myDataTable.Add("timestamp", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
            #endregion

            #region 生成Sign
            ArrayList akeys = new ArrayList(myDataTable.Keys);
            akeys.Sort();
            foreach (string tempKey in akeys)
            {
                myStrBld.Append(tempKey + myDataTable[tempKey]);
            }
            myStrBld.Append(myExecutionDeviceInfo.dev_secret);
            tempSign = MyEncryption.CreateMD5Key(myStrBld.ToString());
            #endregion

            #region 组合数据
            myStrBld.Remove(0, myStrBld.Length);
            //change here
            myStrBld.Append("signature=" + tempSign);
            foreach (DictionaryEntry de in myDataTable)
            {
                //对每次参数进行url编码
                myStrBld.Append("&" + de.Key + "=" + System.Web.HttpUtility.UrlEncode((de.Value).ToString()));
            }
            return myStrBld.ToString();
            #endregion

        }

    }

    /// <summary>
    /// Http Device 
    /// </summary>
    public class CaseProtocolExecutionForHttp :BasicProtocolPars, ICaseExecutionDevice
    {
        private bool isConnect;
        private myConnectForHttp myExecutionDeviceInfo;

        public event delegateGetExecutiveData OnGetExecutiveData;

        public new static MyBasicHttpExecutionContent GetRunContent(XmlNode yourContentNode)
        {
            MyBasicHttpExecutionContent myRunContent = new MyBasicHttpExecutionContent();
            if (yourContentNode != null)
            {
                if (yourContentNode.Attributes["protocol"] != null && yourContentNode.Attributes["actuator"] != null)
                {
                    //Content
                    try
                    {
                        myRunContent.caseProtocol = (CaseProtocol)Enum.Parse(typeof(CaseProtocol), yourContentNode.Attributes["protocol"].Value);
                    }
                    catch
                    {
                        myRunContent.errorMessage = "Error :error protocol in Content";
                        return myRunContent;
                    }
                    myRunContent.caseActuator = yourContentNode.Attributes["actuator"].Value;

                    //Uri
                    XmlNode tempUriDataNode = yourContentNode["Uri"];
                    if (tempUriDataNode != null)
                    {
                        if (tempUriDataNode.Attributes["httpMethod"] != null)
                        {
                            myRunContent.httpMethod = tempUriDataNode.Attributes["httpMethod"].Value;
                        }
                        else
                        {
                            myRunContent.httpMethod = "GET";
                        }
                        myRunContent.httpUri = CaseTool.getXmlParametContent(tempUriDataNode);

                    }
                    else
                    {
                        myRunContent.errorMessage = "Error :http uri (this element is necessary)";
                        return myRunContent;
                    }

                    //HttpHeads
                    XmlNode tempHttpHeadsDataNode = yourContentNode["Heads"];
                    if (tempHttpHeadsDataNode != null)
                    {
                        if (tempHttpHeadsDataNode.HasChildNodes)
                        {
                            foreach (XmlNode headNode in tempHttpHeadsDataNode.ChildNodes)
                            {
                                if (headNode.Attributes["name"] != null)
                                {
                                    myRunContent.httpHeads.Add(new KeyValuePair<string, caseParameterizationContent>(headNode.Attributes["name"].Value, CaseTool.getXmlParametContent(headNode)));
                                }
                                else
                                {
                                    myRunContent.errorMessage = "Error :can not find http Head name in heads";
                                }
                            }
                        }
                    }

                    //HttpBody
                    XmlNode tempHttpBodyDataNode = yourContentNode["Body"];
                    if (tempHttpHeadsDataNode != null)
                    {
                        myRunContent.httpBody = CaseTool.getXmlParametContent(tempHttpBodyDataNode);
                    }
                    //AisleConfig
                    if (yourContentNode["AisleConfig"] != null)
                    {
                        myRunContent.myHttpAisleConfig.httpDataDown = CaseTool.getXmlParametContent(yourContentNode["AisleConfig"], "HttpDataDown");
                    }
                    //HttpMultipart
                    XmlNode tempHttpMultipartNode = yourContentNode["HttpMultipart"];
                    if (tempHttpMultipartNode != null)
                    {
                        if (tempHttpMultipartNode.HasChildNodes)
                        {
                            foreach (XmlNode multipartNode in tempHttpMultipartNode.ChildNodes)
                            {
                                HttpMultipart hmp = new HttpMultipart();
                                if (multipartNode.Name == "MultipartData")
                                {
                                    hmp.isFile = false;
                                    hmp.fileData = multipartNode.InnerText;
                                }
                                else if (multipartNode.Name == "MultipartFile")
                                {
                                    hmp.isFile = true;
                                    hmp.fileData = CaseTool.GetFullPath(multipartNode.InnerText, MyConfiguration.CaseFilePath);
                                }
                                else
                                {
                                    continue;
                                }
                                hmp.name = CaseTool.getXmlAttributeVaule(multipartNode, "name", null);
                                hmp.fileName = CaseTool.getXmlAttributeVaule(multipartNode, "filename", null);
                                myRunContent.myMultipartList.Add(hmp);
                            }
                        }
                    }
                }
                else
                {
                    myRunContent.errorMessage = "Error :can not find protocol or actuator in Content ";
                }
            }
            else
            {
                myRunContent.errorMessage = "Error :yourContentNode is null";
            }
            return myRunContent;
        }

        public CaseProtocolExecutionForHttp(myConnectForHttp yourConnectInfo)
        {
            myExecutionDeviceInfo = yourConnectInfo;
        }

        public object Clone()
        {
            return new CaseProtocolExecutionForHttp(myExecutionDeviceInfo);
        }

        public CaseProtocol ProtocolType
        {
            get
            {
                return myExecutionDeviceInfo.caseProtocol;
            }
        }

        public bool ExecutionDeviceConnect()
        {
            isConnect = true;
            return true;
        }

        public bool IsDeviceConnect
        {
            get
            {
                return isConnect;
            }
        }

        public void ExecutionDeviceClose()
        {
            isConnect = false;
        }


        public MyExecutionDeviceResult ExecutionDeviceRun(ICaseExecutionContent yourExecutionContent, delegateGetExecutiveData yourExecutiveDelegate, string sender, ActuatorStaticDataCollection yourActuatorStaticDataCollection, int caseId)
        {
            MyExecutionDeviceResult myResult = new MyExecutionDeviceResult();
            myResult.staticDataResultCollection = new System.Collections.Specialized.NameValueCollection();
            if (yourExecutionContent.MyCaseProtocol == CaseProtocol.http)
            {
                //在调用该函数前保证nowExecutionContent.ErrorMessage为空，且as一定成功
                MyBasicHttpExecutionContent nowExecutionContent = yourExecutionContent as MyBasicHttpExecutionContent;
                myResult.caseProtocol = CaseProtocol.vanelife_http;
                myResult.caseTarget = nowExecutionContent.MyExecutionTarget;
                string tempError;
                string httpUri;
                string httpBody = null;
                List<KeyValuePair<string, string>> httpHeads = null;

                httpUri = nowExecutionContent.httpUri.getTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError);
                if(httpUri.StartsWith("@"))
                {
                    httpUri = myExecutionDeviceInfo.default_url + httpUri.Remove(0, 1);
                }
                if (nowExecutionContent.httpBody.IsFilled())
                {
                    httpBody = nowExecutionContent.httpBody.getTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError);
                }
                if (nowExecutionContent.httpHeads.Count>0)
                {
                    httpHeads = new List<KeyValuePair<string, string>>();
                    foreach(var tempHead in nowExecutionContent.httpHeads)
                    {
                        httpHeads.Add(new KeyValuePair<string, string>(tempHead.Key, tempHead.Value.getTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError)));
                    }
                }
                
                //report Executive Data
                if (yourExecutiveDelegate != null)
                {
                    if (httpBody != null)
                    {
                        yourExecutiveDelegate(sender,CaseActuatorOutPutType.ExecutiveInfo, string.Format("【ID:{0}】Executive···\r\n{1}\r\n{2}", caseId, httpUri, httpBody));
                    }
                    else
                    {
                        yourExecutiveDelegate(sender,CaseActuatorOutPutType.ExecutiveInfo, string.Format("【ID:{0}】Executive···\r\n{1}", caseId, httpUri));
                    }
                }

                //Start Http 
                if (nowExecutionContent.myMultipartList.Count > 0)
                {
                    List<HttpMultipartDate> myMultiparts = new List<HttpMultipartDate>();
                    foreach(HttpMultipart tempPt in nowExecutionContent.myMultipartList)
                    {
                        if(tempPt.IsFilled())
                        {
                            myMultiparts.Add(new HttpMultipartDate(tempPt.name, tempPt.fileName, null, tempPt.isFile, tempPt.fileData));
                        }
                    }
                    AtHttpProtocol.HttpClient.HttpPostData(httpUri, httpHeads, httpBody, myMultiparts, null, MyConfiguration.PostFileTimeOut, null, myResult);
                }
                else if (nowExecutionContent.myHttpAisleConfig.httpDataDown.IsFilled())
                {
                    AtHttpProtocol.HttpClient.SendData(httpUri, httpBody, nowExecutionContent.httpMethod, httpHeads, myResult, CaseTool.GetFullPath(nowExecutionContent.myHttpAisleConfig.httpDataDown.getTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError), MyConfiguration.CaseFilePath));
                }
                else
                {
                    AtHttpProtocol.HttpClient.SendData(httpUri, httpBody, nowExecutionContent.httpMethod, httpHeads, myResult);
                }

                if (tempError != null)
                {
                    myResult.additionalError = ("error:" + tempError);
                }

            }
            else
            {
                myResult.additionalError = ("error:your CaseProtocol is not Matching RunTimeActuator");
            }
            return myResult;
        }

    }

    #endregion
    

    /// <summary>
    /// 如果您想要添加新类型的【RunTimeStaticData】请在此处添加解释器，并为它添加相应的继承于【IRunTimeStaticData】存储的结构
    /// 然后在CaseStaticDataType枚举中直接新增自己的类型（请与原有格式保持一致），最后您还需要在执行器【LoadScriptRunTime】函数中添加自己的分支
    /// </summary>
    public class MyCaseDataTypeEngine 
    {
        #region TypeDictionary

        /// <summary>
        /// 参数化数据类型映射表
        /// </summary>
        public static Dictionary<CaseStaticDataType, CaseStaticDataClass> dictionaryStaticDataTypeClass = new Dictionary<CaseStaticDataType, CaseStaticDataClass>() { { CaseStaticDataType.caseStaticData_vaule, CaseStaticDataClass.caseStaticDataKey },
        { CaseStaticDataType.caseStaticData_index, CaseStaticDataClass.caseStaticDataParameter }, { CaseStaticDataType.caseStaticData_long, CaseStaticDataClass.caseStaticDataParameter},{ CaseStaticDataType.caseStaticData_list, CaseStaticDataClass.caseStaticDataParameter},
        { CaseStaticDataType.caseStaticData_time, CaseStaticDataClass.caseStaticDataParameter},{ CaseStaticDataType.caseStaticData_random, CaseStaticDataClass.caseStaticDataParameter},
        { CaseStaticDataType.caseStaticData_csv, CaseStaticDataClass.caseStaticDataSource},{ CaseStaticDataType.caseStaticData_mysql, CaseStaticDataClass.caseStaticDataSource},{ CaseStaticDataType.caseStaticData_redis, CaseStaticDataClass.caseStaticDataSource}};

        //参数化数据处理函数委托
        public delegate bool GetStaticDataAction<T>(out T yourStaticData, out string errorMes, string yourFormatData) where T : IRunTimeStaticData;

        /// <summary>
        /// CaseStaticDataType数据与处理函数映射表
        /// </summary>
        public static Dictionary<CaseStaticDataType, GetStaticDataAction<IRunTimeStaticData>> dictionaryStaticDataParameterAction = new Dictionary<CaseStaticDataType, GetStaticDataAction<IRunTimeStaticData>>() { 
        { CaseStaticDataType.caseStaticData_index, new GetStaticDataAction<IRunTimeStaticData>(MyCaseDataTypeEngine.GetIndexStaticData) } ,
        { CaseStaticDataType.caseStaticData_long, new GetStaticDataAction<IRunTimeStaticData>(MyCaseDataTypeEngine.GetLongStaticData) } ,
        { CaseStaticDataType.caseStaticData_list, new GetStaticDataAction<IRunTimeStaticData>(MyCaseDataTypeEngine.GetListStaticData) } ,
        { CaseStaticDataType.caseStaticData_time, new GetStaticDataAction<IRunTimeStaticData>(MyCaseDataTypeEngine.GetTimeStaticData) } ,
        { CaseStaticDataType.caseStaticData_random, new GetStaticDataAction<IRunTimeStaticData>(MyCaseDataTypeEngine.GetRandomStaticData) } 
        };

        /// <summary>
        /// CaseStaticDataType数据与处理函数映射表
        /// </summary>
        public static Dictionary<CaseStaticDataType, GetStaticDataAction<IRunTimeDataSource>> dictionaryStaticDataSourceAction = new Dictionary<CaseStaticDataType, GetStaticDataAction<IRunTimeDataSource>>() { 
        { CaseStaticDataType.caseStaticData_csv, new GetStaticDataAction<IRunTimeDataSource>(MyCaseDataTypeEngine.GetCsvStaticDataSource) } 
         };

        #endregion

        #region IRunTimeStaticData

        public static bool GetIndexStaticData(out IRunTimeStaticData yourStaticData, out string errorMes, string yourFormatData)
        {
            try
            {
                string[] tempStartEnd;
                tempStartEnd = yourFormatData.Split('-');
                if (tempStartEnd.Length == 2)
                {
                    yourStaticData = new MyStaticDataIndex(int.Parse(tempStartEnd[0]), int.Parse(tempStartEnd[1]),1);
                    errorMes = null;
                    return true;
                }
                if (tempStartEnd.Length == 3)
                {
                    yourStaticData = new MyStaticDataIndex(int.Parse(tempStartEnd[0]), int.Parse(tempStartEnd[1]), int.Parse(tempStartEnd[2]));
                    errorMes = null;
                    return true;
                }
                else
                {
                    yourStaticData = new MyStaticDataIndex(0, 2147483647, 1);
                    errorMes = "find error data[myStaticDataIndex] in RunTimeStaticData - ScriptRunTime :(find error number of parameters)";
                }
            }
            catch (Exception)
            {
                yourStaticData = new MyStaticDataIndex(0, 2147483647,1);
                errorMes = "find error data[myStaticDataIndex] in RunTimeStaticData - ScriptRunTime ";
            }
            return false;
        }

        public static bool GetLongStaticData(out IRunTimeStaticData yourStaticData, out string errorMes, string yourFormatData)
        {
            try
            {
                string[] tempStartEnd;
                tempStartEnd = yourFormatData.Split('-');
                if (tempStartEnd.Length == 2)
                {
                    yourStaticData = new MyStaticDataLong(long.Parse(tempStartEnd[0]), long.Parse(tempStartEnd[1]),1);
                    errorMes = null;
                    return true;              
                }
                else if(tempStartEnd.Length==3)
                {
                    yourStaticData = new MyStaticDataLong(long.Parse(tempStartEnd[0]), long.Parse(tempStartEnd[1]), long.Parse(tempStartEnd[2]));
                    errorMes = null;
                    return true;     
                }
                else
                {
                    yourStaticData = new MyStaticDataLong(0, 9223372036854775807,1);
                    errorMes = "find error data[myStaticDataLong] in RunTimeStaticData - ScriptRunTime  :(find error number of parameters)";
                }
            }
            catch (Exception)
            {
                yourStaticData = new MyStaticDataLong(0, 9223372036854775807,1);
                errorMes = "find error data[myStaticDataLong] in RunTimeStaticData - ScriptRunTime ";
            }
            return false;
        }

        public static bool GetTimeStaticData(out IRunTimeStaticData yourStaticData, out string errorMes, string yourFormatData)
        {
            errorMes = null;
            try
            {
                System.DateTime.Now.ToString(yourFormatData);
            }
            catch
            {
                errorMes = "find error data[myStaticDataNowTime] in RunTimeStaticData - ScriptRunTime ";
                yourStaticData=new MyStaticDataNowTime("");
                return false;
            }
            yourStaticData=new MyStaticDataNowTime(yourFormatData);
            return true;
        }

        public static bool GetRandomStaticData(out IRunTimeStaticData yourStaticData, out string errorMes, string yourFormatData)
        {
            try
            {
                string[] tempStartEnd;
                tempStartEnd = yourFormatData.Split('-');
                if (tempStartEnd.Length < 2)
                {
                    yourStaticData = new MyStaticDataRandomStr(10, 0);
                    errorMes = "find error data[myStaticDataRandomNumber] in RunTimeStaticData - ScriptRunTime ";
                }
                else
                {
                    yourStaticData = new MyStaticDataRandomStr(int.Parse(tempStartEnd[0]), int.Parse(tempStartEnd[1]));
                    errorMes = null;
                    return true;
                }
            }
            catch (Exception)
            {
                yourStaticData = new MyStaticDataRandomStr(10, 0);
                errorMes = "find error data[myStaticDataRandomNumber] in RunTimeStaticData - ScriptRunTime ";
            }
            return false;
        }

        public static bool GetListStaticData(out IRunTimeStaticData yourStaticData, out string errorMes, string yourFormatData)
        {
            try
            {
                if (yourFormatData.EndsWith("-1"))
                {
                    yourFormatData = yourFormatData.Remove(yourFormatData.Length - 2);
                    yourStaticData = new MyStaticDataList(yourFormatData, false);
                }
                else if(yourFormatData.EndsWith("-2"))
                {
                    yourFormatData = yourFormatData.Remove(yourFormatData.Length - 2);
                    yourStaticData = new MyStaticDataList(yourFormatData, true);
                }
                else
                {
                    yourStaticData = new MyStaticDataList(yourFormatData, false);
                }
                errorMes = null;
                return true;
            }
            catch (Exception)
            {
                yourStaticData = new MyStaticDataList("",false);
                errorMes = "find error data[myStaticDataList] in RunTimeStaticData - ScriptRunTime ";
            }
            return false;
        }
      
        #endregion

        #region IRunTimeDataSource

        public static bool GetCsvStaticDataSource(out IRunTimeDataSource yourStaticData, out string errorMes, string yourFormatData)
        {
            errorMes = null;
            yourStaticData = new MyStaticDataSourceCsv();
            string csvPath=null;
            int CodePage= 65001 ;
            Encoding csvEncoding = null;
            if (yourFormatData.Contains('-'))
            {
                if (!yourFormatData.MySplitIntEnd('-', out csvPath, out CodePage))
                {
                    errorMes = string.Format("[GetCsvStaticDataSource]error in [MySplitIntEnd] with :[{0}]", yourFormatData);
                    return false;
                }
            }
            else
            {
                csvPath = yourFormatData;
            }
            try
            {
                csvEncoding = System.Text.Encoding.GetEncoding(CodePage);
            }
            catch
            {
                errorMes = string.Format("[GetCsvStaticDataSource]error in 【CodePage】 [{0}]", yourFormatData);
                return false;
            }
            csvPath = csvPath.StartsWith("@") ? csvPath.Remove(0, 1) : string.Format("{0}\\casefile\\{1}", CaseTool.rootPath, csvPath);
            if(!System.IO.File.Exists(csvPath))
            {
                errorMes = string.Format("[GetCsvStaticDataSource]error in csv path [path not exixts] [{0}]", yourFormatData);
                return false;
            }
            MyCommonHelper.FileHelper.CsvFileHelper myCsv = new MyCommonHelper.FileHelper.CsvFileHelper(csvPath, csvEncoding);
            try
            {
                yourStaticData = new MyStaticDataSourceCsv(myCsv.GetListCsvData());
            }
            catch (Exception ex)
            {
                errorMes = ex.Message;
                return false;
            }
            finally
            {
                myCsv.Dispose();
            }
            return true;
        }

        #endregion 

    }

    /// <summary>
    ///  完成脚本不包含可变协议的通用解析，如果您想新增一种类型的协议支持，这里需要添加支持
    /// </summary>
    public class MyCaseScriptAnalysisEngine
    {
        /// <summary>
        /// i will get a myRunCaseData that will give caseActionActuator from XmlNode
        /// </summary>
        /// <param name="yourRunNode">your XmlNode</param>
        /// <returns>myRunCaseData you want</returns>
        public static MyRunCaseData<ICaseExecutionContent> getCaseRunData(XmlNode sourceNode)
        {
            MyRunCaseData<ICaseExecutionContent> myCaseData = new MyRunCaseData<ICaseExecutionContent>();
            CaseProtocol contentProtocol = CaseProtocol.unknownProtocol;
            if (sourceNode == null)
            {
                myCaseData.AddErrorMessage("Error :source data is null");
            }
            else
            {
                if (sourceNode.Name == "Case")
                {
                    #region Basic information
                    if (sourceNode.Attributes["id"] == null)
                    {
                        myCaseData.AddErrorMessage("Error :not find the ID");
                    }
                    else
                    {
                        try
                        {
                            myCaseData.id = int.Parse(sourceNode.Attributes["id"].Value);
                        }
                        catch (Exception)
                        {
                            myCaseData.AddErrorMessage("Error :find the error ID");
                        }
                    }
                    #endregion

                    #region Content
                    //XmlNode tempCaseContent = sourceNode.SelectSingleNode("Content"); //sourceNode["Content"] 具有同样的功能
                    XmlNode tempCaseContent = sourceNode["Content"];
                    if (tempCaseContent == null)
                    {
                        myCaseData.AddErrorMessage("Error :can not find Content");

                    }
                    else
                    {
                        if (tempCaseContent.Attributes["protocol"] != null && tempCaseContent.Attributes["actuator"] != null)
                        {
                            try
                            {
                                contentProtocol = (CaseProtocol)Enum.Parse(typeof(CaseProtocol), tempCaseContent.Attributes["protocol"].Value);
                                myCaseData.contentProtocol = contentProtocol;
                            }
                            catch
                            {
                                myCaseData.AddErrorMessage("Error :error protocol in Content");
                            }
                            switch (contentProtocol)
                            {
                                case CaseProtocol.console:
                                    myCaseData.testContent = CaseProtocolExecutionForConsole.GetRunContent(tempCaseContent);
                                    break;
                                case CaseProtocol.vanelife_http:
                                    myCaseData.testContent = CaseProtocolExecutionForVanelife_http.GetRunContent(tempCaseContent);
                                    break;
                                case CaseProtocol.http:
                                    myCaseData.testContent = CaseProtocolExecutionForHttp.GetRunContent(tempCaseContent);
                                    break;
                                case CaseProtocol.tcp:
                                    myCaseData.AddErrorMessage("Error :this protocol not supported for now");
                                    break;
                                case CaseProtocol.telnet:
                                    myCaseData.AddErrorMessage("Error :this protocol not supported for now");
                                    break;
                                case CaseProtocol.comm:
                                    myCaseData.AddErrorMessage("Error :this protocol not supported for now");
                                    break;
                                case CaseProtocol.vanelife_comm:
                                    myCaseData.AddErrorMessage("Error :this protocol not supported for now");
                                    break;
                                case CaseProtocol.vanelife_tcp:
                                    myCaseData.AddErrorMessage("Error :this protocol not supported for now");
                                    break;
                                case CaseProtocol.vanelife_telnet:
                                    myCaseData.AddErrorMessage("Error :this protocol not supported for now");
                                    break;
                                case CaseProtocol.defaultProtocol:
                                    myCaseData.AddErrorMessage("Error :this protocol not supported for now");
                                    break;
                                default:
                                    myCaseData.AddErrorMessage("Error :this protocol not supported for now");
                                    break;
                            }
                            if (myCaseData.testContent.MyErrorMessage != null)  //将testContent错误移入MyRunCaseData，执行case时会检查MyRunCaseData中的错误
                            {
                                myCaseData.AddErrorMessage("Error :the Content not analyticaled Because:" + myCaseData.testContent.MyErrorMessage);
                                return myCaseData;
                            }
                        }
                        else
                        {
                            myCaseData.AddErrorMessage("Error :can not find protocol or actuator in Content");
                        }
                    }
                    #endregion

                    #region Expect
                    XmlNode tempCaseExpect = sourceNode["Expect"];
                    if (tempCaseExpect == null)
                    {
                        myCaseData.caseExpectInfo.myExpectType = CaseExpectType.judge_default;
                    }
                    else
                    {
                        if (tempCaseExpect.Attributes["method"] != null)
                        {
                            try
                            {
                                myCaseData.caseExpectInfo.myExpectType = (CaseExpectType)Enum.Parse(typeof(CaseExpectType), "judge_" + tempCaseExpect.Attributes["method"].Value);
                            }
                            catch
                            {
                                myCaseData.AddErrorMessage("Error :find error CaseExpectType in Expect");
                                myCaseData.caseExpectInfo.myExpectType = CaseExpectType.judge_default;
                            }
                        }
                        else
                        {
                            myCaseData.caseExpectInfo.myExpectType = CaseExpectType.judge_is;
                        }
                        myCaseData.caseExpectInfo.myExpectContent = CaseTool.getXmlParametContent(tempCaseExpect);
                    }
                    #endregion

                    #region Action
                    XmlNode tempCaseAction = sourceNode["Action"];
                    if (tempCaseAction != null)
                    {
                        if (tempCaseAction.HasChildNodes)
                        {
                            foreach (XmlNode tempNode in tempCaseAction.ChildNodes)
                            {
                                CaseResult tempResult = CaseResult.Unknow;
                                CaseAction tempAction = CaseAction.action_unknow;
                                try
                                {
                                    tempResult = (CaseResult)Enum.Parse(typeof(CaseResult), tempNode.Name);
                                }
                                catch
                                {
                                    myCaseData.AddErrorMessage(string.Format("Error :find error CaseAction in Action with [{0}] in [{1}]", tempNode.InnerXml, tempNode.Name));
                                    continue;
                                }
                                try
                                {
                                    tempAction = (CaseAction)Enum.Parse(typeof(CaseAction), "action_" + CaseTool.getXmlAttributeVaule(tempNode, "action"));
                                }
                                catch
                                {
                                    myCaseData.AddErrorMessage(string.Format("Error :find error CaseAction in Action with [{0}] in [{1}]", tempNode.InnerXml, CaseTool.getXmlAttributeVaule(tempNode, "action")));
                                    continue;
                                }
                                if (tempNode.InnerText!="")
                                {
                                    myCaseData.AddCaseAction(tempResult, new CaseActionDescription(tempAction, tempNode.InnerText));
                                }
                                else
                                {
                                    myCaseData.AddCaseAction(tempResult, new CaseActionDescription(tempAction, null));
                                }
                            }
                        }
                    }
                    #endregion

                    #region Attribute
                    XmlNode tempCaseAttribute = sourceNode["Attribute"];
                    if (tempCaseAttribute != null)
                    {
                        if (tempCaseAttribute.HasChildNodes)
                        {
                            if (tempCaseAttribute["Delay"] != null)
                            {
                                try
                                {
                                    myCaseData.caseAttribute.attributeDelay = int.Parse(tempCaseAttribute["Delay"].InnerText);
                                }
                                catch
                                {
                                    myCaseData.AddErrorMessage("Error :find error Delay data in Attribute");
                                }
                            }
                            if (tempCaseAttribute["Level"] != null)
                            {
                                try
                                {
                                    myCaseData.caseAttribute.attributeLevel = int.Parse(tempCaseAttribute["Level"].InnerText);
                                }
                                catch
                                {
                                    myCaseData.AddErrorMessage("Error :find error Level data in Attribute");
                                }
                            }
                            if (tempCaseAttribute["ParameterSave"] != null)
                            {
                                if (tempCaseAttribute["ParameterSave"].HasChildNodes)
                                {
                                    foreach (XmlNode tempNode in tempCaseAttribute["ParameterSave"].ChildNodes)
                                    {
                                        if (tempNode.Name == "NewParameter")
                                        {
                                            string tempParameterName = CaseTool.getXmlAttributeVauleEx(tempNode, "name");
                                            string tempParameterMode = CaseTool.getXmlAttributeVauleEx(tempNode, "mode");
                                            string tempFindVaule = tempNode.InnerText;
                                            PickOutFunction tempPickOutFunction = PickOutFunction.pick_str;
                                            if (tempParameterName == null)
                                            {
                                                myCaseData.AddErrorMessage("Error :can not find name data in NewParameter in Attribute");
                                                continue;
                                            }
                                            if (tempParameterName == "")
                                            {
                                                myCaseData.AddErrorMessage("Error :the name data in NewParameter is empty in Attribute");
                                                continue;
                                            }
                                            if (tempParameterMode == null)
                                            {
                                                myCaseData.AddErrorMessage("Error :can not find mode data in NewParameter in Attribute");
                                                continue;
                                            }
                                            if (tempFindVaule == "")
                                            {
                                                myCaseData.AddErrorMessage("Error :the NewParameter vaule is empty in Attribute");
                                                continue;
                                            }
                                            try
                                            {
                                                tempPickOutFunction = (PickOutFunction)Enum.Parse(typeof(PickOutFunction), "pick_" + tempParameterMode);
                                            }
                                            catch
                                            {
                                                myCaseData.AddErrorMessage("Error :find error ParameterSave mode in Attribute");
                                                continue;
                                            }
                                            myCaseData.caseAttribute.addParameterSave(tempParameterName, tempFindVaule, tempPickOutFunction);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                }
                else
                {
                    myCaseData.AddErrorMessage("Error :source data is error");
                }
            }
            return myCaseData;
        }

        /// <summary>
        /// i will get the csae info that you need to show in your Tree
        /// </summary>
        /// <param name="sourceNode">source Node</param>
        /// <returns>the info with myCaseLaodInfo</returns>
        public static myCaseLaodInfo getCaseLoadInfo(XmlNode sourceNode)
        {
            myCaseLaodInfo myInfo = new myCaseLaodInfo("NULL");
            switch (sourceNode.Name)
            {
                #region Project show info
                case "Project":
                    //Type
                    myInfo.caseType = CaseType.Project;
                    //name
                    if (sourceNode.Attributes["name"] == null)
                    {
                        myInfo.name = "not find";
                    }
                    else
                    {
                        myInfo.name = sourceNode.Attributes["name"].Value;
                    }
                    //remark
                    if (sourceNode.Attributes["remark"] == null)
                    {
                        myInfo.remark = "not find";
                    }
                    else
                    {
                        myInfo.remark = sourceNode.Attributes["remark"].Value;
                    }
                    //id
                    if (sourceNode.Attributes["id"] == null)
                    {
                        myInfo.ErrorMessage = "not find the ID";
                    }
                    else
                    {
                        try
                        {
                            myInfo.id = int.Parse(sourceNode.Attributes["id"].Value);
                        }
                        catch (Exception ex)
                        {
                            myInfo.ErrorMessage = ex.Message;
                        }
                    }
                    break; 
                #endregion

                #region Case show info
                case "Case":
                    //Type
                    myInfo.caseType = CaseType.Case;
                    //remark
                    if (sourceNode.Attributes["remark"] == null)
                    {
                        myInfo.remark = "not find";
                    }
                    else
                    {
                        myInfo.remark = sourceNode.Attributes["remark"].Value;
                    }
                    //id
                    if (sourceNode.Attributes["id"] == null)
                    {
                        myInfo.ErrorMessage = "not find the ID";
                    }
                    else
                    {
                        try
                        {
                            myInfo.id = int.Parse(sourceNode.Attributes["id"].Value);
                        }
                        catch (Exception ex)
                        {
                            myInfo.ErrorMessage = "Analytical ID Fial" + ex.Message;
                        }
                    }
                    //case cantent and caseProtocol
                    XmlNode tempCaseContent = sourceNode.SelectSingleNode("Content");
                    if (tempCaseContent == null)
                    {
                        myInfo.ErrorMessage = "can not find Content";
                    }
                    else
                    {
                        try
                        {
                            myInfo.caseProtocol = (CaseProtocol)Enum.Parse(typeof(CaseProtocol), tempCaseContent.Attributes["protocol"].Value);
                        }
                        catch
                        {
                            myInfo.caseProtocol = CaseProtocol.unknownProtocol;
                            myInfo.ErrorMessage = "";
                        }

                        if (tempCaseContent.HasChildNodes)
                        {
                            string tempTarget = CaseTool.getXmlAttributeVaule(tempCaseContent.ChildNodes[0], "target",null);
                            if (tempTarget==null)
                            {
                                myInfo.content = " > " + tempCaseContent.ChildNodes[0].InnerText;
                            }
                            else
                            {
                                myInfo.content = " > " + tempTarget + MyConfiguration.CaseShowTargetAndContent + tempCaseContent.ChildNodes[0].InnerText;
                            }
                                
                        }
                        else
                        {
                            myInfo.ErrorMessage = "can not find [ContentData] TestData";
                        }

                    }
                    //case action
                    XmlNode tempCaseAction = sourceNode.SelectSingleNode("Action");
                    if (tempCaseAction != null)
                    {
                        if (tempCaseAction.SelectSingleNode("Pass") != null)
                        {
                            /*
                            if (tempCaseAction.SelectSingleNode("Pass").Attributes["action"].Value != null)
                            {
                                switch (tempCaseAction.SelectSingleNode("Pass").Attributes["action"].Value)
                                {

                                }
                            }
                            */
                            CaseAction tempAction;
                            try
                            {
                                tempAction = (CaseAction)Enum.Parse(typeof(CaseAction), "action_" + tempCaseAction.SelectSingleNode("Pass").Attributes["action"].Value);
                            }
                            catch
                            {
                                tempAction = CaseAction.action_unknow;
                            }
                            myInfo.actions.Add(CaseResult.Pass, tempAction);

                        }
                        if (tempCaseAction.SelectSingleNode("Fail") != null)
                        {
                            CaseAction tempAction;
                            try
                            {
                                tempAction = (CaseAction)Enum.Parse(typeof(CaseAction), "action_" + tempCaseAction.SelectSingleNode("Pass").Attributes["action"].Value);
                            }
                            catch
                            {
                                tempAction = CaseAction.action_unknow;
                            }
                            myInfo.actions.Add(CaseResult.Fail, tempAction);
                        }
                    }
                    break; 
                #endregion

                #region Repeat show info
                case "Repeat":
                    //Type
                    myInfo.caseType = CaseType.Repeat;
                    //remark
                    if (sourceNode.Attributes["remark"] == null)
                    {
                        myInfo.remark = "not find";
                    }
                    else
                    {
                        myInfo.remark = sourceNode.Attributes["remark"].Value;
                    }
                    //times
                    if (sourceNode.Attributes["times"] == null)
                    {
                        myInfo.ErrorMessage = "not find the Times";
                    }
                    else
                    {
                        try
                        {
                            myInfo.times = int.Parse(sourceNode.Attributes["times"].Value);
                        }
                        catch (Exception ex)
                        {
                            myInfo.ErrorMessage = "Analytical Repeat Times Fial" + ex.Message;
                        }
                    }
                    break; 
                #endregion

                case "ScriptRunTime":
                    myInfo.caseType = CaseType.ScriptRunTime;
                    break;
                default:
                    //unkonw CASE;
                    break;
            }
            return myInfo;
        }
    }

}
