using CaseExecutiveActuator.Tool;
using MyCommonHelper;
using MyCommonHelper.NetHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CaseExecutiveActuator.CaseActuator.ExecutionDevice
{
    /// <summary>
    /// Ssh Device 
    /// </summary>
    class CaseProtocolExecutionForTelnet : BasicProtocolPars, ICaseExecutionDevice
    {
        private bool isConnect;
        private myConnectForTelnet myExecutionDeviceInfo;
        public event CaseExecutiveActuator.CaseActuator.CaseActionActuator.delegateGetExecutiveData OnGetExecutiveData;

        private MyTelnet telnetShell;

        public CaseProtocolExecutionForTelnet(myConnectForTelnet yourConnectInfo)
        {
            isConnect = false;
            myExecutionDeviceInfo = yourConnectInfo;
            telnetShell = new MyTelnet(myExecutionDeviceInfo.host, myExecutionDeviceInfo.port, 5);
            if (myExecutionDeviceInfo.expectPattern != null)
            {
                telnetShell.ExpectPattern = myExecutionDeviceInfo.expectPattern;
            }
        }

        public new static MyTelnetExecutionContent GetRunContent(XmlNode yourContentNode)
        {
            MyTelnetExecutionContent myRunContent = new MyTelnetExecutionContent();
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

                    XmlNode nowSshNode = yourContentNode["TelnetCmd"];
                    if (nowSshNode != null)
                    {
                        myRunContent.telnetContent = CaseTool.GetXmlParametContent(nowSshNode);
                    }
                    else
                    {
                        myRunContent.errorMessage = "Error :can not find any TelnetCmd ";
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
            try
            {
                if(telnetShell.Connect())
                {
                    telnetShell.WaitStr("login");
                    telnetShell.WriteLine(myExecutionDeviceInfo.user);
                    telnetShell.WaitStr("password");
                    telnetShell.ClearShowData();
                    telnetShell.WriteLine(myExecutionDeviceInfo.password);
                    isConnect = telnetShell.WaitStr("Last login");
                }
                else
                {
                    isConnect = false;
                }
                
            }
            catch (Exception ex)
            {
                ErrorLog.PutInLog(ex);
                isConnect = false;
            }
            return isConnect;
        }

        public void ExecutionDeviceClose()
        {
            telnetShell.DisConnect();
            isConnect = false;
        }

        public MyExecutionDeviceResult ExecutionDeviceRun(ICaseExecutionContent yourExecutionContent, CaseActionActuator.delegateGetExecutiveData yourExecutiveDelegate, string sender, ActuatorStaticDataCollection yourActuatorStaticDataCollection, int caseId)
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

            if (yourExecutionContent.MyCaseProtocol == CaseProtocol.telnet)
            {
                //在调用该函数前保证nowExecutionContent.ErrorMessage为空，且as一定成功
                MyTelnetExecutionContent nowExecutionContent = yourExecutionContent as MyTelnetExecutionContent;
                myResult.caseProtocol = CaseProtocol.telnet;
                myResult.caseTarget = nowExecutionContent.MyExecutionTarget;
                myResult.startTime = DateTime.Now.ToString("HH:mm:ss");
                StringBuilder tempCaseOutContent = new StringBuilder();

                System.Diagnostics.Stopwatch myWatch = new System.Diagnostics.Stopwatch();
                myWatch.Start();

                ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, string.Format("【ID:{0}】[telnet]Executive···", caseId));

                string nowTelnetCmd = nowExecutionContent.telnetContent.GetTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError);
                if (tempError != null)
                {
                    DealExecutiveError(string.Format("this case get static data errer with [{0}]", nowExecutionContent.telnetContent.GetTargetContentData()));
                    tempCaseOutContent.AppendLine("error with static data");
                }
                else
                {
                    try
                    {
                        string tempResult = telnetShell.DoRequest(nowTelnetCmd);

                        ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, tempResult);
                        tempCaseOutContent.AppendLine(tempResult);
                    }
                    catch (Exception ex)
                    {
                        DealExecutiveError(ex.Message);
                        tempCaseOutContent.AppendLine(ex.Message);
                    }

                }

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
            return new CaseProtocolExecutionForTelnet(myExecutionDeviceInfo);
        }

    }
}

