using CaseExecutiveActuator.Tool;
using MyCommonHelper;
using MyCommonHelper.NetHelper;
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
* 日	  期:   201708015           创建人: 李杰 15158155511
* 描    述: 创建
*******************************************************************************/

namespace CaseExecutiveActuator.CaseActuator.ExecutionDevice
{
    class CaseProtocolExecutionForCom : BasicProtocolPars, ICaseExecutionDevice
    {
        private bool isConnect;
        private myConnectForCom myExecutionDeviceInfo;
        public event delegateGetExecutiveData OnGetExecutiveData;

        private MySerialPort mySerialPort;

        public CaseProtocolExecutionForCom(myConnectForCom yourConnectInfo)
        {
            isConnect = false;
            myExecutionDeviceInfo = yourConnectInfo;
            mySerialPort = new MySerialPort(false);
            mySerialPort.PortName = yourConnectInfo.portName;
            mySerialPort.BaudRate = yourConnectInfo.baudRate;
            mySerialPort.DataBits = yourConnectInfo.dataBits;
            mySerialPort.StopBits = yourConnectInfo.stopBits;
            mySerialPort.Parity = yourConnectInfo.parity;
            mySerialPort.Encoding = yourConnectInfo.encoding;
        }

        public new static MyComExecutionContent GetRunContent(XmlNode yourContentNode)
        {
            MyComExecutionContent myRunContent = new MyComExecutionContent();
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

                    #region send
                    XmlNode nowComNode = yourContentNode["Send"];
                    if (nowComNode != null)
                    {
                        myRunContent.comSendEncoding = null;
                        if (nowComNode.Attributes["encoding"] == null)
                        {
                            myRunContent.comSendEncoding = Encoding.UTF8;
                        }
                        else
                        {
                            if (nowComNode.Attributes["encoding"].Value != "raw")
                            {
                                try
                                {
                                    myRunContent.comSendEncoding = Encoding.GetEncoding(nowComNode.Attributes["encoding"].Value);
                                }
                                catch
                                {
                                    myRunContent.comSendEncoding = null;
                                }
                            }
                        }
                        myRunContent.comContentToSend = CaseTool.GetXmlParametContent(nowComNode);
                        myRunContent.isSend = true;
                    } 
                    #endregion

                    #region receive
                    nowComNode = yourContentNode["Receive"];
                    if (nowComNode != null)
                    {
                        myRunContent.comReceiveEncoding = null;
                        if (nowComNode.Attributes["encoding"] == null)
                        {
                            myRunContent.comReceiveEncoding = Encoding.UTF8;
                        }
                        else
                        {
                            if (nowComNode.Attributes["encoding"].Value != "raw")
                            {
                                try
                                {
                                    myRunContent.comReceiveEncoding = Encoding.GetEncoding(nowComNode.Attributes["encoding"].Value);
                                }
                                catch
                                {
                                    myRunContent.comReceiveEncoding = null;
                                }
                            }
                        }
                        if (nowComNode.InnerText != "")
                        {
                            if (!int.TryParse(nowComNode.InnerText, out myRunContent.comSleepTime))
                            {
                                myRunContent.comSleepTime = -1;
                            }
                        }
                        else
                        {
                            myRunContent.comSleepTime = -1;
                        }
                        myRunContent.isReceive = true;
                    } 
                    #endregion

                    if (!(myRunContent.isReceive || myRunContent.isSend))
                    {
                        myRunContent.errorMessage = "Error :can not find any send or receive node in Content ";
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

        public CaseProtocol ProtocolType
        {
            get
            {
                return myExecutionDeviceInfo.caseProtocol;
            }
        }

        public bool IsDeviceConnect
        {
            get { return isConnect; }
        }

        public bool ExecutionDeviceConnect()
        {
            isConnect = mySerialPort.OpenSerialPort();
            return isConnect;
        }

        public void ExecutionDeviceClose()
        {
            mySerialPort.CloseSerialPort();
            isConnect = false;
        }

        public MyExecutionDeviceResult ExecutionDeviceRun(ICaseExecutionContent yourExecutionContent, delegateGetExecutiveData yourExecutiveDelegate, string sender, ActuatorStaticDataCollection yourActuatorStaticDataCollection, int caseId)
        {
            List<string> errorList = new List<string>();
            string tempError = null;
            MyExecutionDeviceResult myResult = new MyExecutionDeviceResult();
            myResult.staticDataResultCollection = new System.Collections.Specialized.NameValueCollection();

            //向UI推送执行过程信息
            Action<string, CaseActuatorOutPutType, string> ExecutiveDelegate = (innerSender, outType, yourContent) =>
            {
                if (yourExecutiveDelegate != null)
                {
                    yourExecutiveDelegate(innerSender, outType, yourContent);
                }
            };

            //处理执行错误（执行器无法执行的错误）
            Action<string> DealExecutiveError = (errerData) =>
            {
                if (errerData != null)
                {
                    ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveError, errerData);
                    errorList.Add(errerData);
                }
            };

            if (yourExecutionContent.MyCaseProtocol == CaseProtocol.com)
            {
                //在调用该函数前保证nowExecutionContent.ErrorMessage为空，且as一定成功
                MyComExecutionContent nowExecutionContent = yourExecutionContent as MyComExecutionContent;
                myResult.caseProtocol = CaseProtocol.com;
                myResult.caseTarget = nowExecutionContent.MyExecutionTarget;
                myResult.startTime = DateTime.Now.ToString("HH:mm:ss");
                StringBuilder tempCaseOutContent = new StringBuilder();

                System.Diagnostics.Stopwatch myWatch = new System.Diagnostics.Stopwatch();
                myWatch.Start();

                ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, string.Format("【ID:{0}】[com]Executive···", caseId));

                #region Send
                if (nowExecutionContent.isSend)
                {
                    string nowComData = nowExecutionContent.comContentToSend.GetTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError);
                    if (tempError != null)
                    {
                        DealExecutiveError(string.Format("this case get static data errer with [{0}]", nowExecutionContent.comContentToSend.GetTargetContentData()));
                        tempCaseOutContent.AppendLine("error with static data");
                    }
                    else
                    {
                        byte[] nowSendBytes;
                        if (nowExecutionContent.comSendEncoding == null)
                        {
                            try
                            {
                                nowSendBytes = MyBytes.HexStringToByte(nowComData, HexaDecimal.hex16, ShowHexMode.space);
                            }
                            catch
                            {
                                nowSendBytes = null;
                            }
                        }
                        else
                        {
                            try
                            {
                                nowSendBytes = nowExecutionContent.comSendEncoding.GetBytes(nowComData);
                            }
                            catch
                            {
                                nowSendBytes = null;
                            }
                        }
                        if (nowSendBytes == null)
                        {
                            DealExecutiveError(string.Format("can not change data to bytes with [{0}]", nowExecutionContent.comContentToSend.GetTargetContentData()));
                            tempCaseOutContent.AppendLine("error with com data");
                        }
                        else
                        {
                            if (mySerialPort.Send(nowSendBytes))
                            {
                                ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, "send sucess");
                                tempCaseOutContent.AppendLine("send sucess");
                            }
                            else
                            {
                                ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveError, mySerialPort.ErroerMessage);
                                tempCaseOutContent.AppendLine(mySerialPort.ErroerMessage);
                            }
                        }
                    }
                }
                #endregion

                #region receive
                if (nowExecutionContent.isReceive)
                
                {
                    if (nowExecutionContent.comSleepTime > 0)
                    {
                        System.Threading.Thread.Sleep(nowExecutionContent.comSleepTime);
                    }
                    byte[] recweiveBytes = mySerialPort.ReadAllBytes();
                    
                    if (recweiveBytes != null)
                    {
                        string receiveStr;
                        if (nowExecutionContent.comReceiveEncoding == null)
                        {
                            receiveStr = MyBytes.ByteToHexString(recweiveBytes, HexaDecimal.hex16, ShowHexMode.space);
                        }
                        else
                        {
                            try
                            {
                                receiveStr = nowExecutionContent.comReceiveEncoding.GetString(recweiveBytes);
                            }
                            catch
                            {
                                receiveStr = null;
                            }
                        }
                        if (receiveStr != null)
                        {
                            tempCaseOutContent.AppendLine(receiveStr);
                        }
                        else
                        {
                            ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveError, string.Format("can not Encoding your data with {0}", MyBytes.ByteToHexString(recweiveBytes, HexaDecimal.hex16, ShowHexMode.space)));
                            tempCaseOutContent.AppendLine("[error]receive data error can not encoding receive data");
                        }
                    }

                } 
                #endregion

                

                myWatch.Stop();
                myResult.spanTime = myResult.requestTime = myWatch.ElapsedMilliseconds.ToString();

                myResult.backContent = tempCaseOutContent.ToString();
            }
            else
            {
                myResult.backContent = "error:your CaseProtocol is not Matching RunTimeActuator";
                DealExecutiveError(myResult.backContent);
            }


            if (errorList.Count > 0)
            {
                myResult.additionalError = errorList.MyToString("\r\n");
            }

            return myResult;
        }

        public object Clone()
        {
            return new CaseProtocolExecutionForCom(myExecutionDeviceInfo);
        }
    }
}
