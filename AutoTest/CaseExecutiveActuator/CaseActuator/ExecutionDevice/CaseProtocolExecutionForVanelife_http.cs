using CaseExecutiveActuator.ProtocolExecutive;
using CaseExecutiveActuator.Tool;
using MyCommonHelper;
using MyCommonHelper.EncryptionHelper;
using System;
using System.Collections;
using System.Collections.Generic;
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
*
* 历史记录:
* 日	  期:   201708001           修改: 李杰 15158155511
* 描    述: 拆分
*******************************************************************************/

namespace CaseExecutiveActuator.CaseActuator.ExecutionDevice
{
    /// <summary>
    /// Vanelife_http Device 
    /// </summary>
    public class CaseProtocolExecutionForVanelife_http : BasicProtocolPars, ICaseExecutionDevice
    {
        private bool isConnect;
        private myConnectForVanelife_http myExecutionDeviceInfo;

        public event CaseActionActuator.delegateGetExecutiveData OnGetExecutiveData;

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
                                myRunContent.myHttpMultipart.name = CaseTool.GetXmlAttributeVauleWithEmpty(tempHttpConfigDataNode["HttpMultipart"]["MultipartData"], "name");
                                myRunContent.myHttpMultipart.fileName = CaseTool.GetXmlAttributeVauleWithEmpty(tempHttpConfigDataNode["HttpMultipart"]["MultipartData"], "filename");
                                myRunContent.myHttpMultipart.fileData = tempHttpConfigDataNode["HttpMultipart"]["MultipartData"].InnerText;
                            }
                            else if (tempHttpConfigDataNode["HttpMultipart"]["MultipartFile"] != null)
                            {
                                myRunContent.myHttpMultipart.isFile = true;
                                myRunContent.myHttpMultipart.name = CaseTool.GetXmlAttributeVauleWithEmpty(tempHttpConfigDataNode["HttpMultipart"]["MultipartFile"], "name");
                                myRunContent.myHttpMultipart.fileName = CaseTool.GetXmlAttributeVauleWithEmpty(tempHttpConfigDataNode["HttpMultipart"]["MultipartFile"], "filename");
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


        public MyExecutionDeviceResult ExecutionDeviceRun(ICaseExecutionContent yourExecutionContent, CaseActionActuator.delegateGetExecutiveData yourExecutiveDelegate, string sender, ActuatorStaticDataCollection yourActuatorStaticDataCollection, int caseId)
        {
            MyExecutionDeviceResult myResult = new MyExecutionDeviceResult();
            myResult.staticDataResultCollection = new System.Collections.Specialized.NameValueCollection();//默认该值为null，不会输出参数数据结果（如果不需要输出可以保持该字段为null）
            if (yourExecutionContent.MyCaseProtocol == CaseProtocol.vanelife_http)
            {
                //在调用该函数前保证nowExecutionContent.ErrorMessage为空，且as一定成功
                MyVaneHttpExecutionContent nowExecutionContent = yourExecutionContent as MyVaneHttpExecutionContent;
                myResult.caseProtocol = CaseProtocol.vanelife_http;
                myResult.caseTarget = nowExecutionContent.MyExecutionTarget;
                string tempError;
                string tempUrlAddress;
                string vanelifeData = CreatVanelifeSendData(nowExecutionContent.caseExecutionContent.GetTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError));
                if (nowExecutionContent.myHttpAisleConfig.httpAddress.IsFilled())
                {
                    tempUrlAddress = nowExecutionContent.myHttpAisleConfig.httpAddress.GetTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError) + nowExecutionContent.httpTarget;
                }
                else
                {
                    tempUrlAddress = myExecutionDeviceInfo.default_url + nowExecutionContent.httpTarget;
                }

                //report Executive Data
                if (yourExecutiveDelegate != null)
                {
                    yourExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, string.Format("【ID:{0}】[vanelife_http]Executive···\r\n{1}\r\n{2}", caseId, tempUrlAddress, vanelifeData));
                }

                //Start Http 
                if (nowExecutionContent.myHttpAisleConfig.httpDataDown.IsFilled())
                {
                    AtHttpProtocol.HttpClient.SendData(tempUrlAddress, vanelifeData, nowExecutionContent.httpMethod, myResult, CaseTool.GetFullPath(nowExecutionContent.myHttpAisleConfig.httpDataDown.GetTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError), MyConfiguration.CaseFilePath));
                }
                else
                {
                    if (nowExecutionContent.myHttpMultipart.IsFilled())
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
                myResult.backContent = "error:your CaseProtocol is not Matching RunTimeActuator";
                myResult.additionalError = ("error:your CaseProtocol is not Matching RunTimeActuator");
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
}
