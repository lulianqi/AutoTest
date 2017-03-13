using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using MyCommonTool;


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
    public class myCaseProtocolEngine
    {
        public class vanelife_httpEngine
        {
            /// <summary>
            /// here i can get the data your need in the  XmlNode the for the 【ICaseExecutionDevice】
            /// </summary>
            /// <param name="yourContentNode">souce XmlNode</param>
            /// <returns>the data your need</returns>
            public static myHttpExecutionContent getRunContent(XmlNode yourContentNode)
            {
                myHttpExecutionContent myRunContent = new myHttpExecutionContent();
                if (yourContentNode!=null)
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
                            myRunContent.ErrorMessage = "Error :error protocol in Content";
                            return myRunContent;
                        }
                        myRunContent.caseActuator = yourContentNode.Attributes["actuator"].Value;

                        //ContentData
                        XmlNode tempContentDataNode = yourContentNode["ContentData"];
                        if(tempContentDataNode!=null)
                        {
                            if (tempContentDataNode.Attributes["target"] != null)
                            {
                                myRunContent.HttpTarget = tempContentDataNode.Attributes["target"].Value;
                            }
                            else
                            {
                                myRunContent.HttpTarget = "";
                            }

                            if (tempContentDataNode.Attributes["isHaveParameters"] != null)
                            {
                                if(tempContentDataNode.Attributes["isHaveParameters"].Value=="true")
                                {
                                    myRunContent.caseExecutionContent.hasParameter = true;//hasParameter默认为false，其他情况无需设置该值
                                }
                            }
                            myRunContent.caseExecutionContent.contentData = tempContentDataNode.InnerText;
                        }
                        else
                        {
                            myRunContent.ErrorMessage = "Error :can not find ContentData , it is necessary in [vanelife_http]";
                            return myRunContent;
                        }

                        //HttpConfig
                        XmlNode tempHttpConfigDataNode = yourContentNode["HttpConfig"];
                        if (tempHttpConfigDataNode != null)
                        {
                            if (tempHttpConfigDataNode.Attributes["httpMethod"] != null)
                            {
                                myRunContent.HttpMethod = tempHttpConfigDataNode.Attributes["httpMethod"].Value;
                            }
                            else
                            {
                                myRunContent.HttpMethod = "POST";
                            }

                            if(tempHttpConfigDataNode["AisleConfig"] !=null)
                            {
                                myRunContent.myHttpAisleConfig.httpAddress = CaseTool.getXmlParametContent(tempHttpConfigDataNode["AisleConfig"], "HttpAddress");
                                myRunContent.myHttpAisleConfig.httpDataDown = CaseTool.getXmlParametContent(tempHttpConfigDataNode["AisleConfig"], "HttpDataDown");
                            }
                            if (tempHttpConfigDataNode["HttpMultipart"] != null)
                            {
                                if(tempHttpConfigDataNode["HttpMultipart"]["MultipartData"]!=null)
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
                                    myRunContent.myHttpMultipart.fileData = CaseTool.GetFullPath(tempHttpConfigDataNode["HttpMultipart"]["MultipartFile"].InnerText);
                                }
                            }
                        }
                        else
                        {
                            myRunContent.HttpMethod = "POST";
                        }

                    }
                    else
                    {
                        myRunContent.ErrorMessage = "Error :can not find protocol or actuator in Content ";
                    }
                }
                else
                {
                    myRunContent.ErrorMessage = "Error :yourContentNode is null";
                }
                return myRunContent;
            }
        }

        public class httpEngine
        {
            public static myBasicHttpExecutionContent getRunContent(XmlNode yourContentNode)
            {
                myBasicHttpExecutionContent myRunContent = new myBasicHttpExecutionContent();
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
                            if(tempHttpHeadsDataNode.HasChildNodes)
                            {
                                foreach(XmlNode headNode in tempHttpHeadsDataNode.ChildNodes)
                                {
                                    if(headNode.Attributes["name"]!=null)
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
        }
    }

    #region ExecutionDevice

    /// <summary>
    /// Vanelife_http Device 
    /// </summary>
    public class CaseProtocolExecutionForVanelife_http : ICaseExecutionDevice
    {
        private bool isConnect;
        private myConnectForVanelife_http myExecutionDeviceInfo;

        public event delegateGetExecutiveData OnGetExecutiveData;

        public CaseProtocolExecutionForVanelife_http(myConnectForVanelife_http yourConnectInfo)
        {
            myExecutionDeviceInfo = yourConnectInfo;
        }

        public object Clone()
        {
            return new CaseProtocolExecutionForVanelife_http(myExecutionDeviceInfo);
        }

        public CaseProtocol getProtocolType
        {
            get
            {
                return myExecutionDeviceInfo.caseProtocol;
            }
        }

        public bool executionDeviceConnect()
        {
            isConnect = true;
            return true;
        }

        public bool isDeviceConnect
        {
            get
            {
                return isConnect;
            }
        }

        public void executionDeviceClose()
        {
            isConnect = false;
        }


        public myExecutionDeviceResult executionDeviceRun(ICaseExecutionContent yourExecutionContent, delegateGetExecutiveData yourExecutiveDelegate, string sender, Dictionary<string, string> yourParameterList, Dictionary<string, IRunTimeStaticData> yourStaticDataList,int caseId)
        {
            myExecutionDeviceResult myResult = new myExecutionDeviceResult();
            myResult.staticDataResultCollection = new System.Collections.Specialized.NameValueCollection();//默认该值为null，不会输出参数数据结果（如果不需要输出可以保持该字段为null）
            if(yourExecutionContent.myCaseProtocol==CaseProtocol.vanelife_http)
            {
                //在调用该函数前保证nowExecutionContent.ErrorMessage为空，且as一定成功
                myHttpExecutionContent nowExecutionContent = yourExecutionContent as myHttpExecutionContent;
                myResult.caseProtocol = CaseProtocol.vanelife_http;
                myResult.caseTarget = nowExecutionContent.myExecutionTarget;
                string tempError;
                string tempUrlAddress;
                string vanelifeData = CreatVanelifeSendData(nowExecutionContent.caseExecutionContent.getTargetContentData(yourParameterList, yourStaticDataList, myResult.staticDataResultCollection, out tempError));
                if (nowExecutionContent.myHttpAisleConfig.httpAddress.IsFilled())
                {
                    tempUrlAddress = nowExecutionContent.myHttpAisleConfig.httpAddress.getTargetContentData(yourParameterList, yourStaticDataList, myResult.staticDataResultCollection, out tempError) + nowExecutionContent.HttpTarget;
                }
                else
                {
                    tempUrlAddress = myExecutionDeviceInfo.default_url + nowExecutionContent.HttpTarget;
                }

                //report Executive Data
                if (yourExecutiveDelegate != null)
                {
                    yourExecutiveDelegate(sender, string.Format("【ID:{0}】Executive···\r\n{1}\r\n{2}", caseId, tempUrlAddress, vanelifeData));
                }

                //Start Http 
                if (nowExecutionContent.myHttpAisleConfig.httpDataDown.IsFilled())
                {
                    myWebTool.HttpClient.SendData(tempUrlAddress, vanelifeData, nowExecutionContent.HttpMethod, myResult, CaseTool.GetFullPath(nowExecutionContent.myHttpAisleConfig.httpDataDown.getTargetContentData(yourParameterList, yourStaticDataList, myResult.staticDataResultCollection, out tempError)));
                }
                else
                {
                    if(nowExecutionContent.myHttpMultipart.IsFilled())
                    {
                        //由于vanelife协议要求在Multipart把业务数据全部放在了url中
                        myWebTool.HttpClient.HttpPostData(tempUrlAddress + "?" + vanelifeData, 30000, nowExecutionContent.myHttpMultipart.name, nowExecutionContent.myHttpMultipart.fileName, nowExecutionContent.myHttpMultipart.isFile, nowExecutionContent.myHttpMultipart.fileData, null, myResult);
                    }
                    else
                    {
                        myWebTool.HttpClient.SendData(tempUrlAddress, vanelifeData, nowExecutionContent.HttpMethod, myResult);
                    }
                }

                if (tempError != null)
                {
                    myResult.additionalEroor = ("error:" + tempError);
                }

            }
            else
            {
                myResult.additionalEroor=("error:your CaseProtocol is not Matching RunTimeActuator");
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
            tempSign = myEncryption.CreateMD5Key(myStrBld.ToString());
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
    /// Vanelife_http Device 
    /// </summary>
    public class CaseProtocolExecutionForHttp : ICaseExecutionDevice
    {
        private bool isConnect;
        private myConnectForHttp myExecutionDeviceInfo;

        public event delegateGetExecutiveData OnGetExecutiveData;

        public CaseProtocolExecutionForHttp(myConnectForHttp yourConnectInfo)
        {
            myExecutionDeviceInfo = yourConnectInfo;
        }

        public object Clone()
        {
            return new CaseProtocolExecutionForHttp(myExecutionDeviceInfo);
        }

        public CaseProtocol getProtocolType
        {
            get
            {
                return myExecutionDeviceInfo.caseProtocol;
            }
        }

        public bool executionDeviceConnect()
        {
            isConnect = true;
            return true;
        }

        public bool isDeviceConnect
        {
            get
            {
                return isConnect;
            }
        }

        public void executionDeviceClose()
        {
            isConnect = false;
        }


        public myExecutionDeviceResult executionDeviceRun(ICaseExecutionContent yourExecutionContent, delegateGetExecutiveData yourExecutiveDelegate, string sender, Dictionary<string, string> yourParameterList, Dictionary<string, IRunTimeStaticData> yourStaticDataList, int caseId)
        {
            myExecutionDeviceResult myResult = new myExecutionDeviceResult();
            myResult.staticDataResultCollection = new System.Collections.Specialized.NameValueCollection();
            if (yourExecutionContent.myCaseProtocol == CaseProtocol.http)
            {
                //在调用该函数前保证nowExecutionContent.ErrorMessage为空，且as一定成功
                myBasicHttpExecutionContent nowExecutionContent = yourExecutionContent as myBasicHttpExecutionContent;
                myResult.caseProtocol = CaseProtocol.vanelife_http;
                myResult.caseTarget = nowExecutionContent.myExecutionTarget;
                string tempError;
                string httpUri;
                string httpBody = null;
                List<KeyValuePair<string, string>> httpHeads = null;

                httpUri = nowExecutionContent.httpUri.getTargetContentData(yourParameterList, yourStaticDataList, myResult.staticDataResultCollection, out tempError);
                if (nowExecutionContent.httpBody.IsFilled())
                {
                    httpBody = nowExecutionContent.httpBody.getTargetContentData(yourParameterList, yourStaticDataList, myResult.staticDataResultCollection, out tempError);
                }
                if (nowExecutionContent.httpHeads.Count>0)
                {
                    httpHeads = new List<KeyValuePair<string, string>>();
                    foreach(var tempHead in nowExecutionContent.httpHeads)
                    {
                        httpHeads.Add(new KeyValuePair<string, string>(tempHead.Key, tempHead.Value.getTargetContentData(yourParameterList, yourStaticDataList, myResult.staticDataResultCollection, out tempError)));
                    }
                }
                
                //report Executive Data
                if (yourExecutiveDelegate != null)
                {
                    if (httpBody != null)
                    {
                        yourExecutiveDelegate(sender, string.Format("【ID:{0}】Executive···\r\n{1}\r\n{2}", caseId, httpUri, httpBody));
                    }
                    else
                    {
                        yourExecutiveDelegate(sender, string.Format("【ID:{0}】Executive···\r\n{1}", caseId, httpUri));
                    }
                }

                //Start Http 
                if (nowExecutionContent.myHttpAisleConfig.httpDataDown.IsFilled())
                {
                    myWebTool.HttpClient.SendData(httpUri, httpBody, nowExecutionContent.httpMethod, httpHeads, myResult, CaseTool.GetFullPath(nowExecutionContent.myHttpAisleConfig.httpDataDown.getTargetContentData(yourParameterList, yourStaticDataList, myResult.staticDataResultCollection, out tempError)));
                }
                else
                {
                    myWebTool.HttpClient.SendData(httpUri, httpBody, nowExecutionContent.httpMethod,httpHeads, myResult);
                }

                if (tempError != null)
                {
                    myResult.additionalEroor = ("error:" + tempError);
                }

            }
            else
            {
                myResult.additionalEroor = ("error:your CaseProtocol is not Matching RunTimeActuator");
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
        #region IRunTimeStaticData
        
        public static bool GetIndexStaticData(out MyStaticDataIndex yourStaticData, out string errorMes, string yourFormatData)
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

        public static bool GetLongStaticData(out MyStaticDataLong yourStaticData, out string errorMes, string yourFormatData)
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

        public static void GetTimeStaticData(out MyStaticDataNowTime yourStaticData, string yourFormatData)
        {
              yourStaticData=new MyStaticDataNowTime(yourFormatData);
        }

        public static bool GetRandomStaticData(out MyStaticDataRandomStr yourStaticData, out string errorMes, string yourFormatData)
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

        public static bool GetListStaticData(out MyStaticDataList yourStaticData, out string errorMes, string yourFormatData)
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
        
        public static bool GetCsvStaticDataSource(out MyStaticDataSourceCsv yourStaticData, out string errorMes,string yourFormatData)
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
            csvPath = csvPath.StartsWith("@") ? System.Windows.Forms.Application.StartupPath + csvPath.Remove(0,1) : csvPath;
            if(!System.IO.File.Exists(csvPath))
            {
                errorMes = string.Format("[GetCsvStaticDataSource]error in csv path [path not exixts] [{0}]", yourFormatData);
                return false;
            }
            MyCommonTool.FileHelper.CsvFileHelper myCsv = new MyCommonTool.FileHelper.CsvFileHelper(csvPath, csvEncoding);
            yourStaticData = new MyStaticDataSourceCsv(myCsv.GetListCsvData());
            return true;
        }

        #endregion 

    }

    /// <summary>
    ///  完成脚本不包含可变协议的通用解析，如果您想新增一种类型的协议支持，这里需要添加支持
    /// </summary>
    public class myCaseScriptAnalysisEngine
    {
        /// <summary>
        /// i will get a myRunCaseData that will give caseActionActuator from XmlNode
        /// </summary>
        /// <param name="yourRunNode">your XmlNode</param>
        /// <returns>myRunCaseData you want</returns>
        public static myRunCaseData<ICaseExecutionContent> getCaseRunData(XmlNode sourceNode)
        {
            myRunCaseData<ICaseExecutionContent> myCaseData = new myRunCaseData<ICaseExecutionContent>();
            CaseProtocol contentProtocol = CaseProtocol.unknownProtocol;
            if (sourceNode == null)
            {
                myCaseData.addErrorMessage("Error :source data is null");
            }
            else
            {
                if (sourceNode.Name == "Case")
                {
                    #region Basic information
                    if (sourceNode.Attributes["id"] == null)
                    {
                        myCaseData.addErrorMessage("Error :not find the ID");
                    }
                    else
                    {
                        try
                        {
                            myCaseData.id = int.Parse(sourceNode.Attributes["id"].Value);
                        }
                        catch (Exception)
                        {
                            myCaseData.addErrorMessage("Error :find the error ID");
                        }
                    }
                    #endregion

                    #region Content
                    //XmlNode tempCaseContent = sourceNode.SelectSingleNode("Content"); //sourceNode["Content"] 具有同样的功能
                    XmlNode tempCaseContent = sourceNode["Content"];
                    if (tempCaseContent == null)
                    {
                        myCaseData.addErrorMessage("Error :can not find Content");

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
                                myCaseData.addErrorMessage("Error :error protocol in Content");
                            }
                            switch (contentProtocol)
                            {
                                case CaseProtocol.vanelife_http:
                                    myCaseData.testContent = myCaseProtocolEngine.vanelife_httpEngine.getRunContent(tempCaseContent);
                                    if (myCaseData.testContent.myErrorMessage != null)
                                    {
                                        myCaseData.addErrorMessage("Error :the Content not analyticaled Because:"+ myCaseData.testContent.myErrorMessage);
                                        return myCaseData;
                                    }
                                    break;
                                case CaseProtocol.http:
                                    myCaseData.testContent = myCaseProtocolEngine.httpEngine.getRunContent(tempCaseContent);
                                    if (myCaseData.testContent.myErrorMessage != null)
                                    {
                                        myCaseData.addErrorMessage("Error :the Content not analyticaled Because:"+ myCaseData.testContent.myErrorMessage);
                                        return myCaseData;
                                    }
                                    break;
                                case CaseProtocol.tcp:
                                    myCaseData.addErrorMessage("Error :this protocol not supported for now");
                                    break;
                                case CaseProtocol.telnet:
                                    myCaseData.addErrorMessage("Error :this protocol not supported for now");
                                    break;
                                case CaseProtocol.comm:
                                    myCaseData.addErrorMessage("Error :this protocol not supported for now");
                                    break;
                                case CaseProtocol.vanelife_comm:
                                    myCaseData.addErrorMessage("Error :this protocol not supported for now");
                                    break;
                                case CaseProtocol.vanelife_tcp:
                                    myCaseData.addErrorMessage("Error :this protocol not supported for now");
                                    break;
                                case CaseProtocol.vanelife_telnet:
                                    myCaseData.addErrorMessage("Error :this protocol not supported for now");
                                    break;
                                case CaseProtocol.defaultProtocol:
                                    myCaseData.addErrorMessage("Error :this protocol not supported for now");
                                    break;
                                default:
                                    myCaseData.addErrorMessage("Error :this protocol not supported for now");
                                    break;
                            }


                        }
                        else
                        {
                            myCaseData.addErrorMessage("Error :can not find protocol or actuator in Content");
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
                                myCaseData.addErrorMessage("Error :find error CaseExpectType in Expect");
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
                                    myCaseData.addErrorMessage("Error :find error CaseResult in Action");
                                    continue;
                                }
                                try
                                {
                                    tempAction = (CaseAction)Enum.Parse(typeof(CaseAction), "action_" + CaseTool.getXmlAttributeVaule(tempNode, "action"));
                                }
                                catch
                                {
                                    myCaseData.addErrorMessage("Error :find error CaseAction in Action");
                                    continue;
                                }
                                if (tempNode.InnerText!="")
                                {
                                    myCaseData.addCaseAction(tempResult, new caseActionDescription(tempAction, tempNode.InnerText));
                                }
                                else
                                {
                                    myCaseData.addCaseAction(tempResult, new caseActionDescription(tempAction, null));
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
                                    myCaseData.addErrorMessage("Error :find error Delay data in Attribute");
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
                                    myCaseData.addErrorMessage("Error :find error Level data in Attribute");
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
                                                myCaseData.addErrorMessage("Error :can not find name data in NewParameter in Attribute");
                                                continue;
                                            }
                                            if (tempParameterName == "")
                                            {
                                                myCaseData.addErrorMessage("Error :the name data in NewParameter is empty in Attribute");
                                                continue;
                                            }
                                            if (tempParameterMode == null)
                                            {
                                                myCaseData.addErrorMessage("Error :can not find mode data in NewParameter in Attribute");
                                                continue;
                                            }
                                            if (tempFindVaule == "")
                                            {
                                                myCaseData.addErrorMessage("Error :the NewParameter vaule is empty in Attribute");
                                                continue;
                                            }
                                            try
                                            {
                                                tempPickOutFunction = (PickOutFunction)Enum.Parse(typeof(PickOutFunction), "pick_" + tempParameterMode);
                                            }
                                            catch
                                            {
                                                myCaseData.addErrorMessage("Error :find error ParameterSave mode in Attribute");
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
                    myCaseData.addErrorMessage("Error :source data is error");
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

                        XmlNode tempCaseContentData = tempCaseContent.SelectSingleNode("ContentData");
                        if (tempCaseContentData == null)
                        {
                            if (tempCaseContent.HasChildNodes)
                            {
                                myInfo.content = " > " + tempCaseContent.ChildNodes[0].InnerText;
                            }
                            else
                            {
                                myInfo.ErrorMessage = "can not find TestData";
                            }
                        }
                        else
                        {
                            string tempTarget = CaseTool.getXmlAttributeVaule(tempCaseContentData, "target");
                            string ContentempData = tempCaseContentData.InnerText;
                            myInfo.content = " > " + tempTarget + "★" + ContentempData;
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
