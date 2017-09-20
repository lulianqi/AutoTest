using CaseExecutiveActuator.ProtocolExecutive;
using CaseExecutiveActuator.Tool;
using System;
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
    /// Http Device 
    /// </summary>
    public class CaseProtocolExecutionForHttp : BasicProtocolPars, ICaseExecutionDevice
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
                        myRunContent.httpUri = CaseTool.GetXmlParametContent(tempUriDataNode);

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
                                    myRunContent.httpHeads.Add(new KeyValuePair<string, caseParameterizationContent>(headNode.Attributes["name"].Value, CaseTool.GetXmlParametContent(headNode)));
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
                        myRunContent.httpBody = CaseTool.GetXmlParametContent(tempHttpBodyDataNode);
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
                                hmp.name = CaseTool.GetXmlAttributeVauleEx(multipartNode, "name", null);
                                hmp.fileName = CaseTool.GetXmlAttributeVauleEx(multipartNode, "filename", null);
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
                myResult.caseProtocol = CaseProtocol.http;
                myResult.caseTarget = nowExecutionContent.MyExecutionTarget;
                string tempError;
                string httpUri;
                string httpBody = null;
                List<KeyValuePair<string, string>> httpHeads = null;

                httpUri = nowExecutionContent.httpUri.GetTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError);
                if (httpUri.StartsWith("@"))
                {
                    httpUri = myExecutionDeviceInfo.default_url + httpUri.Remove(0, 1);
                }
                if (nowExecutionContent.httpBody.IsFilled())
                {
                    httpBody = nowExecutionContent.httpBody.GetTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError);
                }
                if (nowExecutionContent.httpHeads.Count > 0)
                {
                    httpHeads = new List<KeyValuePair<string, string>>();
                    foreach (var tempHead in nowExecutionContent.httpHeads)
                    {
                        httpHeads.Add(new KeyValuePair<string, string>(tempHead.Key, tempHead.Value.GetTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError)));
                    }
                }

                //report Executive Data
                if (yourExecutiveDelegate != null)
                {
                    if (httpBody != null)
                    {
                        yourExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, string.Format("【ID:{0}】[http]Executive···\r\n{1}\r\n{2}", caseId, httpUri, httpBody));
                    }
                    else
                    {
                        yourExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, string.Format("【ID:{0}】[http]Executive···\r\n{1}", caseId, httpUri));
                    }
                }

                //Start Http 
                if (nowExecutionContent.myMultipartList.Count > 0)
                {
                    List<MyCommonHelper.NetHelper.MyWebTool.HttpMultipartDate> myMultiparts = new List<MyCommonHelper.NetHelper.MyWebTool.HttpMultipartDate>();
                    foreach (HttpMultipart tempPt in nowExecutionContent.myMultipartList)
                    {
                        if (tempPt.IsFilled())
                        {
                            myMultiparts.Add(new MyCommonHelper.NetHelper.MyWebTool.HttpMultipartDate(tempPt.name, tempPt.fileName, null, tempPt.isFile, tempPt.fileData));
                        }
                    }
                    AtHttpProtocol.HttpClient.HttpPostData(httpUri, httpHeads, httpBody, myMultiparts, null, MyConfiguration.PostFileTimeOut, null, myResult);
                }
                else if (nowExecutionContent.myHttpAisleConfig.httpDataDown.IsFilled())
                {
                    AtHttpProtocol.HttpClient.SendData(httpUri, httpBody, nowExecutionContent.httpMethod, httpHeads, myResult, CaseTool.GetFullPath(nowExecutionContent.myHttpAisleConfig.httpDataDown.GetTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError), MyConfiguration.CaseFilePath));
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
                myResult.backContent = "error:your CaseProtocol is not Matching RunTimeActuator";
            }
            
            return myResult;
        }

    }
}
