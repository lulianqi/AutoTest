using MyCommonHelper.NetHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
* 日	  期:   201708008           创建人: 李杰 15158155511
* 描    述: 创建
*******************************************************************************/

namespace CaseExecutiveActuator.CaseActuator.ExecutionDevice
{
    public class CaseProtocolExecutionForTcp
    {
        private bool isConnect;
        private myConnectForTcp myExecutionDeviceInfo;
        public event delegateGetExecutiveData OnGetExecutiveData;

        private MyTcpClient myTcpClient;

        public CaseProtocolExecutionForTcp(myConnectForTcp yourConnectInfo)
        {
            isConnect = false;
            myExecutionDeviceInfo = yourConnectInfo;
            myTcpClient = new MyTcpClient(new IPEndPoint(IPAddress.Parse(myExecutionDeviceInfo.host), myExecutionDeviceInfo.port));
        }

        public new static MyMySqlExecutionContent GetRunContent(XmlNode yourContentNode)
        {
            MyMySqlExecutionContent myRunContent = new MyMySqlExecutionContent();
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

                    XmlNode nowSqlNode = yourContentNode["SqlCmd"];
                    if (nowSqlNode != null)
                    {
                        if (nowSqlNode.Attributes["row"] != null && nowSqlNode.Attributes["column"] != null)
                        {
                            myRunContent.isPosition = true;
                            if (!(int.TryParse(nowSqlNode.Attributes["row"].Value, out myRunContent.rowIndex) && int.TryParse(nowSqlNode.Attributes["column"].Value, out myRunContent.columnIndex)))
                            {
                                myRunContent.errorMessage = "Error :yourContentNode is error wirh [row] or [column] attribute";
                            }
                            else
                            {
                                if (myRunContent.rowIndex < 0 || myRunContent.columnIndex < 0)
                                {
                                    myRunContent.errorMessage = "Error :yourContentNode is error wirh [row] or [column] attribute";
                                }
                            }
                        }
                        myRunContent.sqlContent = CaseTool.GetXmlParametContent(nowSqlNode);

                    }
                    else
                    {
                        myRunContent.errorMessage = "Error :can not find any SqlCmd ";
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
            isConnect = myTcpClient.Connect();
            return isConnect;
        }

        public void ExecutionDeviceClose()
        {
            myTcpClient.DisConnect();
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

            if (yourExecutionContent.MyCaseProtocol == CaseProtocol.mysql)
            {
                //在调用该函数前保证nowExecutionContent.ErrorMessage为空，且as一定成功
                MyMySqlExecutionContent nowExecutionContent = yourExecutionContent as MyMySqlExecutionContent;
                myResult.caseProtocol = CaseProtocol.mysql;
                myResult.caseTarget = nowExecutionContent.MyExecutionTarget;
                myResult.startTime = DateTime.Now.ToString("HH:mm:ss");
                StringBuilder tempCaseOutContent = new StringBuilder();

                System.Diagnostics.Stopwatch myWatch = new System.Diagnostics.Stopwatch();
                myWatch.Start();

                ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, string.Format("【ID:{0}】[mysql]Executive···", caseId));

                string nowSqlCmd = nowExecutionContent.sqlContent.GetTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError);
                if (tempError != null)
                {
                    DealExecutiveError(string.Format("this case get static data errer with [{0}]", nowExecutionContent.sqlContent.GetTargetContentData()));
                    tempCaseOutContent.AppendLine("error with static data");
                }
                else
                {
                    if (nowExecutionContent.isPosition)
                    {
                        string sqlResult = mySqlDrive.ExecuteQuery(nowSqlCmd, nowExecutionContent.rowIndex, nowExecutionContent.columnIndex);
                        if (sqlResult == null)
                        {
                            tempCaseOutContent.AppendLine(string.Format("error in [ExecuteQuery] :{0}", mySqlDrive.NowError));
                            DealExecutiveError(mySqlDrive.NowError);
                        }
                        else
                        {
                            tempCaseOutContent.AppendLine(sqlResult);
                            ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, sqlResult);
                        }
                    }
                    else
                    {
                        System.Data.DataTable sqlResult = mySqlDrive.ExecuteQuery(nowSqlCmd);
                        if (sqlResult == null)
                        {
                            tempCaseOutContent.AppendLine(string.Format("error in [ExecuteQuery] :{0}", mySqlDrive.NowError));
                            DealExecutiveError(mySqlDrive.NowError);
                        }
                        else
                        {
                            string json = Newtonsoft.Json.JsonConvert.SerializeObject(sqlResult, Newtonsoft.Json.Formatting.Indented);
                            ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, json);
                            tempCaseOutContent.AppendLine(json);
                        }
                    }
                }

                myWatch.Stop();
                myResult.spanTime = myResult.requestTime = myWatch.ElapsedMilliseconds.ToString();

                myResult.backContent = tempCaseOutContent.ToString();
            }
            else
            {
                DealExecutiveError("error:your CaseProtocol is not Matching RunTimeActuator");
            }


            if (errorList.Count > 0)
            {
                myResult.additionalError = errorList.MyToString("\r\n");
            }

            return myResult;
        }

        public object Clone()
        {
            return new CaseProtocolExecutionForTcp(myExecutionDeviceInfo);
        }
    }
}
