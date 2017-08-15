using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using MyCommonHelper;
using MyActiveMQHelper;

using CaseExecutiveActuator.CaseActuator.ExecutionDevice;
using CaseExecutiveActuator.ProtocolExecutive;
using CaseExecutiveActuator.Tool;
using MySqlHelper;

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


namespace CaseExecutiveActuator.CaseActuator
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
    //move to ExecutionDevice folder
    #endregion
    

    
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
                    myCaseData.name = CaseTool.GetXmlAttributeVauleEx(sourceNode, "remark", "NULL");
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
                                case CaseProtocol.activeMQ:
                                    myCaseData.testContent = CaseProtocolExecutionForActiveMQ.GetRunContent(tempCaseContent);
                                    break;
                                case CaseProtocol.mysql:
                                    myCaseData.testContent = CaseProtocolExecutionForMysql.GetRunContent(tempCaseContent);
                                    break;
                                case CaseProtocol.ssh:
                                    myCaseData.testContent = CaseProtocolExecutionForSsh.GetRunContent(tempCaseContent);
                                    break;
                                case CaseProtocol.vanelife_http:
                                    myCaseData.testContent = CaseProtocolExecutionForVanelife_http.GetRunContent(tempCaseContent);
                                    break;
                                case CaseProtocol.http:
                                    myCaseData.testContent = CaseProtocolExecutionForHttp.GetRunContent(tempCaseContent);
                                    break;
                                case CaseProtocol.tcp:
                                    myCaseData.testContent = CaseProtocolExecutionForTcp.GetRunContent(tempCaseContent);
                                    break;
                                case CaseProtocol.telnet:
                                    myCaseData.AddErrorMessage("Error :this protocol not supported for now");
                                    break;
                                case CaseProtocol.com:
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
                            if (myCaseData.testContent!=null)
                            {
                                if (myCaseData.testContent.MyErrorMessage != null)  //将testContent错误移入MyRunCaseData，执行case时会检查MyRunCaseData中的错误
                                {
                                    myCaseData.AddErrorMessage("Error :the Content not analyticaled Because:" + myCaseData.testContent.MyErrorMessage);
                                    //return myCaseData;
                                }
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
                        myCaseData.caseExpectInfo.myExpectContent = CaseTool.GetXmlParametContent(tempCaseExpect);
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
                                    tempAction = (CaseAction)Enum.Parse(typeof(CaseAction), "action_" + CaseTool.GetXmlAttributeVauleWithEmpty(tempNode, "action"));
                                }
                                catch
                                {
                                    myCaseData.AddErrorMessage(string.Format("Error :find error CaseAction in Action with [{0}] in [{1}]", tempNode.InnerXml, CaseTool.GetXmlAttributeVauleWithEmpty(tempNode, "action")));
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
                                            string tempParameterName = CaseTool.GetXmlAttributeVaule(tempNode, "name");
                                            string tempParameterMode = CaseTool.GetXmlAttributeVaule(tempNode, "mode");
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
        /// i will get the csae info that you need to show in your Tree(使用MyRunCaseData的重载版本针对不同执行协议对content的内容有优化)
        /// </summary>
        /// <param name="runCaseData">MyRunCaseData</param>
        /// <returns>the info with myCaseLaodInfo</returns>
        public static myCaseLaodInfo getCaseLoadInfo(MyRunCaseData<ICaseExecutionContent> runCaseData)
        {
            myCaseLaodInfo myInfo = new myCaseLaodInfo("NULL");
            myInfo.id = runCaseData.id;
            myInfo.remark = runCaseData.name;
            myInfo.caseType = CaseType.Case;
            myInfo.content = runCaseData.testContent.MyExecutionContent == null ? string.Format("> {0}", runCaseData.testContent.MyExecutionTarget) : string.Format("> {0}{1}{2}", runCaseData.testContent.MyExecutionTarget, MyConfiguration.CaseShowTargetAndContent, runCaseData.testContent.MyExecutionContent);
            myInfo.caseProtocol = runCaseData.testContent.MyCaseProtocol;
            if (runCaseData.actions != null)
            {
                foreach (var tempAction in runCaseData.actions)
                {
                    myInfo.actions.Add(tempAction.Key, tempAction.Value.caseAction);
                }
            }
            return myInfo;
        }

        /// <summary>
        /// i will get the csae info that you need to show in your Tree （对于CASE 的content 仅供参考）
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
                            string tempTarget = CaseTool.GetXmlAttributeVauleEx(tempCaseContent.ChildNodes[0], "target",null);
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
