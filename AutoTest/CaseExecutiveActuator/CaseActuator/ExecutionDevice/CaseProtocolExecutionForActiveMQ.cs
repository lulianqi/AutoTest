using MyActiveMQHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using MyCommonHelper;
using CaseExecutiveActuator.Tool;

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
    /// ActiveMQ Device 
    /// </summary>
    public class CaseProtocolExecutionForActiveMQ : BasicProtocolPars, ICaseExecutionDevice
    {
        private bool isConnect;
        private myConnectForActiveMQ myExecutionDeviceInfo;
        public event delegateGetExecutiveData OnGetExecutiveData;

        private MyActiveMQ activeMQ;

        public CaseProtocolExecutionForActiveMQ(myConnectForActiveMQ yourConnectInfo)
        {
            isConnect = false;
            myExecutionDeviceInfo = yourConnectInfo;
            activeMQ = new MyActiveMQ(myExecutionDeviceInfo.brokerUri, myExecutionDeviceInfo.clientId, myExecutionDeviceInfo.factoryUserName, myExecutionDeviceInfo.factoryPassword, myExecutionDeviceInfo.queuesList, myExecutionDeviceInfo.topicList, false);
        }

        public new static MyActiveMQExecutionContent GetRunContent(XmlNode yourContentNode)
        {
            MyActiveMQExecutionContent myRunContent = new MyActiveMQExecutionContent();
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

                    //Subscribe List
                    #region Subscribe
                    List<string[]> tempSubscribeRawList = CaseTool.GetXmlInnerMetaDataList(yourContentNode, "Subscribe", new string[] { "type", "durable" });
                    foreach (string[] tempOneSubscribeRaw in tempSubscribeRawList)
                    {
                        if (tempOneSubscribeRaw[1] != null && tempOneSubscribeRaw[0] != "")
                        {
                            myRunContent.consumerSubscribeList.Add(new MyActiveMQExecutionContent.ConsumerData(tempOneSubscribeRaw[0], tempOneSubscribeRaw[1], tempOneSubscribeRaw[2]));
                        }
                        else
                        {
                            myRunContent.errorMessage = string.Format("Error :error data in Subscribe List with [{0}]", tempOneSubscribeRaw[0]);
                            return myRunContent;
                        }
                    }
                    #endregion
                    //UnSubscribe List
                    #region UnSubscribe
                    List<string[]> tempUnSubscribeRawList = CaseTool.GetXmlInnerMetaDataList(yourContentNode, "UnSubscribe", new string[] { "type" });
                    foreach (string[] tempOneUnSubscribeRaw in tempUnSubscribeRawList)
                    {
                        if (tempOneUnSubscribeRaw[1] != null && tempOneUnSubscribeRaw[0] != "")
                        {
                            myRunContent.unConsumerSubscribeList.Add(new MyActiveMQExecutionContent.ConsumerData(tempOneUnSubscribeRaw[0], tempOneUnSubscribeRaw[1], null));
                        }
                        else
                        {
                            myRunContent.errorMessage = string.Format("Error :error data in UnSubscribe List with [{0}]", tempOneUnSubscribeRaw[0]);
                            return myRunContent;
                        }
                    }
                    #endregion
                    //Message Send List
                    #region Message Send
                    List<KeyValuePair<XmlNode, string[]>> tempMessageSendList = CaseTool.GetXmlInnerMetaDataListEx(yourContentNode, "Send", new string[] { "name", "type", "isHaveParameters" });
                    foreach (KeyValuePair<XmlNode, string[]> tempOneMessageSendRaw in tempMessageSendList)
                    {
                        if (!string.IsNullOrEmpty(tempOneMessageSendRaw.Value[1]) && tempOneMessageSendRaw.Value[2] != null && tempOneMessageSendRaw.Value[0] != "")
                        {
                            MyActiveMQExecutionContent.ProducerData tempProducerData = new MyActiveMQExecutionContent.ProducerData(tempOneMessageSendRaw.Value[1], tempOneMessageSendRaw.Value[2], "TextMessage");
                            caseParameterizationContent tempProducerMessage = CaseTool.GetXmlParametContent(tempOneMessageSendRaw.Key);
                            myRunContent.producerDataSendList.Add(new KeyValuePair<MyActiveMQExecutionContent.ProducerData, caseParameterizationContent>(tempProducerData, tempProducerMessage));
                        }
                        else
                        {
                            myRunContent.errorMessage = string.Format("Error :error data in Send List with [{0}]", tempOneMessageSendRaw.Value[0]);
                            return myRunContent;
                        }
                    }
                    #endregion
                    //Receive
                    #region Receive
                    List<string[]> tempConsumerReceiveList = CaseTool.GetXmlInnerMetaDataList(yourContentNode, "Receive", new string[] { "type" });
                    foreach (string[] tempOneConsumerReceiveRaw in tempConsumerReceiveList)
                    {
                        if (tempOneConsumerReceiveRaw[0] == "")
                        {
                            myRunContent.consumerMessageReceiveList.Add(new MyActiveMQExecutionContent.ConsumerData(null, null, null));
                        }
                        else
                        {
                            if (tempOneConsumerReceiveRaw[1] != null)
                            {
                                myRunContent.consumerMessageReceiveList.Add(new MyActiveMQExecutionContent.ConsumerData(tempOneConsumerReceiveRaw[0], tempOneConsumerReceiveRaw[1], null));
                            }
                            else
                            {
                                myRunContent.errorMessage = string.Format("Error :error data in Receive List with [{0}]", tempOneConsumerReceiveRaw[0]);
                                return myRunContent;
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
            isConnect = activeMQ.Connect();
            return isConnect;
        }

        public void ExecutionDeviceClose()
        {
            activeMQ.DisConnect();
            isConnect = false;
        }

        public MyExecutionDeviceResult ExecutionDeviceRun(ICaseExecutionContent yourExecutionContent, delegateGetExecutiveData yourExecutiveDelegate, string sender, ActuatorStaticDataCollection yourActuatorStaticDataCollection, int caseId)
        {
            List<string> errorList = new List<string>();
            string tempError = null;
            MyExecutionDeviceResult myResult = new MyExecutionDeviceResult();
            myResult.staticDataResultCollection = new System.Collections.Specialized.NameValueCollection();

            Action<string, CaseActuatorOutPutType, string> ExecutiveDelegate = (innerSender, outType, yourContent) =>
            {
                if (yourExecutiveDelegate != null)
                {
                    yourExecutiveDelegate(innerSender, outType, yourContent);
                }
            };

            Action<string> DealExecutiveError = (errerData) =>
            {
                if (errerData != null)
                {
                    ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveError, errerData);
                    errorList.Add(errerData);
                }
            };

            if (yourExecutionContent.MyCaseProtocol == CaseProtocol.activeMQ)
            {
                //在调用该函数前保证nowExecutionContent.ErrorMessage为空，且as一定成功
                MyActiveMQExecutionContent nowExecutionContent = yourExecutionContent as MyActiveMQExecutionContent;
                myResult.caseProtocol = CaseProtocol.activeMQ;
                myResult.caseTarget = nowExecutionContent.MyExecutionTarget;
                myResult.startTime = DateTime.Now.ToString("HH:mm:ss");
                StringBuilder tempCaseOutContent = new StringBuilder();

                System.Diagnostics.Stopwatch myWatch = new System.Diagnostics.Stopwatch();
                myWatch.Start();

                ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, string.Format("【ID:{0}】[activeMQ]Executive···", caseId));

                #region Subscribe
                foreach (var tempConsumer in nowExecutionContent.consumerSubscribeList)
                {
                    if (tempConsumer.ConsumerType == "queue" || tempConsumer.ConsumerType == "topic")
                    {
                        if (activeMQ.SubscribeConsumer(tempConsumer.ConsumerName, tempConsumer.ConsumerType == "queue", tempConsumer.ConsumerTopicDurable))
                        {
                            ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, string.Format("{0}://{1} subscribe success", tempConsumer.ConsumerType, tempConsumer.ConsumerName));
                        }
                        else
                        {
                            DealExecutiveError(string.Format("{0}://{1} subscribe fail", tempConsumer.ConsumerType, tempConsumer.ConsumerName));
                        }
                    }
                    else
                    {
                        DealExecutiveError(string.Format("{0}://{1} subscribe fail [not support this consumer type]", tempConsumer.ConsumerType, tempConsumer.ConsumerName));
                    }
                }
                #endregion

                #region UnSubscribe
                foreach (var tempConsumer in nowExecutionContent.unConsumerSubscribeList)
                {
                    if (tempConsumer.ConsumerType == "queue" || tempConsumer.ConsumerType == "topic")
                    {
                        if (activeMQ.UnSubscribeConsumer(string.Format("{0}://{1}", tempConsumer.ConsumerType, tempConsumer.ConsumerName)) > 0)
                        {
                            ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, string.Format("{0}://{1} unsubscribe success", tempConsumer.ConsumerType, tempConsumer.ConsumerName));
                        }
                        else
                        {
                            DealExecutiveError(string.Format("{0}://{1} unsubscribe fail", tempConsumer.ConsumerType, tempConsumer.ConsumerName));
                        }
                    }
                    else
                    {
                        DealExecutiveError(string.Format("{0}://{1} unsubscribe fail [not support this consumer type]", tempConsumer.ConsumerType, tempConsumer.ConsumerName));
                    }
                }
                #endregion

                #region Send
                foreach (var tempOneSender in nowExecutionContent.producerDataSendList)
                {
                    if (tempOneSender.Key.ProducerType == "queue" || tempOneSender.Key.ProducerType == "topic")
                    {
                        string tempMessageSend = tempOneSender.Value.GetTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError);
                        if (tempError != null)
                        {
                            DealExecutiveError(string.Format("this case get static data errer with [{0}]", tempOneSender.Value.GetTargetContentData()));
                            tempCaseOutContent.AppendLine(string.Format("{0}://{1} send message fail [get static data errer]", tempOneSender.Key.ProducerType, tempOneSender.Key.ProducerName));
                        }
                        else
                        {
                            MessageType tempMessageType;
                            if (Enum.TryParse<MessageType>(tempOneSender.Key.MessageType, out tempMessageType))
                            {
                                if (activeMQ.PublishMessage(tempOneSender.Key.ProducerName, tempMessageSend, tempOneSender.Key.ProducerType == "topic", tempMessageType))
                                {
                                    string tempReportStr = string.Format("{0}://{1} send message success", tempOneSender.Key.ProducerType, tempOneSender.Key.ProducerName);
                                    ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, tempReportStr);
                                    tempCaseOutContent.AppendLine(tempReportStr);
                                }
                                else
                                {
                                    string tempReportStr = string.Format("{0}://{1} send message fail [{2}]", tempOneSender.Key.ProducerType, tempOneSender.Key.ProducerName, activeMQ.NowErrorMes);
                                    ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, tempReportStr);
                                    tempCaseOutContent.AppendLine(tempReportStr);
                                }
                            }
                            else
                            {
                                DealExecutiveError(string.Format("get MessageType errer with [{0}]", tempOneSender.Key.MessageType));
                                tempCaseOutContent.AppendLine(string.Format("{0}://{1} send message fail [get MessageType errer]", tempOneSender.Key.ProducerType, tempOneSender.Key.ProducerName));
                            }

                        }
                    }
                    else
                    {
                        tempCaseOutContent.Append(string.Format("{0}://{1} send message fail [not support this producer type]", tempOneSender.Key.ProducerType, tempOneSender.Key.ProducerName));
                    }
                }
                #endregion

                #region Receive
                if (nowExecutionContent.consumerMessageReceiveList.Count > 0)
                {
                    ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, "Receive your activeMQ messages");
                }
                foreach (var tempConsumer in nowExecutionContent.consumerMessageReceiveList)
                {
                    if (tempConsumer.ConsumerName != null)
                    {
                        if (tempConsumer.ConsumerType == "queue" || tempConsumer.ConsumerType == "topic")
                        {
                            List<KeyValuePair<string, string>> oneMessageReceive = activeMQ.ReadConsumerMessage(string.Format("{0}://{1}", tempConsumer.ConsumerType, tempConsumer.ConsumerName));
                            foreach (KeyValuePair<string, string> tempMessage in oneMessageReceive)
                            {
                                string tempNowReceviceData = string.Format("{0} Receive {1}", tempMessage.Key, tempMessage.Value);
                                ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, tempNowReceviceData);
                                tempCaseOutContent.AppendLine(tempNowReceviceData);
                            }
                        }
                        else
                        {
                            DealExecutiveError(string.Format("{0}://{1} receive fail [not support this consumer type]", tempConsumer.ConsumerType, tempConsumer.ConsumerName));
                            tempCaseOutContent.AppendLine(string.Format("{0}://{1} receive fail ", tempConsumer.ConsumerType, tempConsumer.ConsumerName));
                        }
                    }
                    else
                    {
                        List<KeyValuePair<string, string>> oneMessageReceive = activeMQ.ReadConsumerMessage();
                        foreach (KeyValuePair<string, string> tempMessage in oneMessageReceive)
                        {
                            string tempNowReceviceData = string.Format("{0} Receive {1}", tempMessage.Key, tempMessage.Value);
                            ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, tempNowReceviceData);
                            tempCaseOutContent.AppendLine(tempNowReceviceData);
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
            return new CaseProtocolExecutionForActiveMQ(myExecutionDeviceInfo);
        }
    }
}
