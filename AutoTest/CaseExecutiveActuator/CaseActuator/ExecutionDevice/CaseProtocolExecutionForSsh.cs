using MySqlHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using MyCommonHelper;
using CaseExecutiveActuator.Tool;
using Tamir.SharpSsh;

/*******************************************************************************
* Copyright (c) 2015 lijie
* All rights reserved.
* 
* 文件名称: 
* 内容摘要: mycllq@hotmail.com
* 
* 历史记录:
* 日	  期:   201708003           创建人: 李杰 15158155511
* 描    述: 创建
*******************************************************************************/

namespace CaseExecutiveActuator.CaseActuator.ExecutionDevice
{
    /// <summary>
    /// Ssh Device 
    /// </summary>
    class CaseProtocolExecutionForSsh : BasicProtocolPars, ICaseExecutionDevice
    {
        private bool isConnect;
        private myConnectForSsh myExecutionDeviceInfo;
        public event delegateGetExecutiveData OnGetExecutiveData;

        private SshShell sshShell;

        public CaseProtocolExecutionForSsh(myConnectForSsh yourConnectInfo)
        {
            isConnect = false;
            myExecutionDeviceInfo = yourConnectInfo;
            sshShell = new SshShell(myExecutionDeviceInfo.host, myExecutionDeviceInfo.user, myExecutionDeviceInfo.password);
            if(myExecutionDeviceInfo.expectPattern!=null)
            {
                sshShell.ExpectPattern = myExecutionDeviceInfo.expectPattern;
            }
        }

        public new static MySshExecutionContent GetRunContent(XmlNode yourContentNode)
        {
            MySshExecutionContent myRunContent = new MySshExecutionContent();
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

                    XmlNode nowSshNode = yourContentNode["SshCmd"];
                    if (nowSshNode != null)
                    {
                        myRunContent.sshContent = CaseTool.GetXmlParametContent(nowSshNode);
                    }
                    else
                    {
                        myRunContent.errorMessage = "Error :can not find any SshCmd ";
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
                sshShell.Connect();
                sshShell.Expect();
                isConnect = true;
            }
            catch(Exception ex)
            {
                ErrorLog.PutInLog(ex);
                isConnect = false;
            }
            return isConnect;
        }

        public void ExecutionDeviceClose()
        {
            sshShell.Close();
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

            if (yourExecutionContent.MyCaseProtocol == CaseProtocol.ssh)
            {
                //在调用该函数前保证nowExecutionContent.ErrorMessage为空，且as一定成功
                MySshExecutionContent nowExecutionContent = yourExecutionContent as MySshExecutionContent;
                myResult.caseProtocol = CaseProtocol.ssh;
                myResult.caseTarget = nowExecutionContent.MyExecutionTarget;
                myResult.startTime = DateTime.Now.ToString("HH:mm:ss");
                StringBuilder tempCaseOutContent = new StringBuilder();

                System.Diagnostics.Stopwatch myWatch = new System.Diagnostics.Stopwatch();
                myWatch.Start();

                ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, string.Format("【ID:{0}】[ssh]Executive···", caseId));

                string nowSqlCmd = nowExecutionContent.sshContent.GetTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError);
                if (tempError != null)
                {
                    DealExecutiveError(string.Format("this case get static data errer with [{0}]", nowExecutionContent.sshContent.GetTargetContentData()));
                    tempCaseOutContent.AppendLine("error with static data");
                }
                else
                {
                    try
                    {
                        sshShell.WriteLine(nowSqlCmd);
                        string tempResult = sshShell.Expect();

                        ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, tempResult);
                        tempCaseOutContent.AppendLine(tempResult);
                    }
                    catch(Exception ex)
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
            return new CaseProtocolExecutionForSsh(myExecutionDeviceInfo);
        }

    }
}
