using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CaseExecutiveActuator.CaseActuator.ExecutionDevice
{
    /// <summary>
    /// Console Device 
    /// </summary>
    public class CaseProtocolExecutionForConsole : BasicProtocolPars, ICaseExecutionDevice
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
                                        if (Enum.TryParse<CaseStaticDataType>("caseStaticData_" + taskNode.Attributes["type"].Value, out nowType))
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
                                    bool isRegex = false;
                                    if (taskNode.Attributes["isRegex"] != null)
                                    {
                                        if (taskNode.Attributes["isRegex"].Value == "true")
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

            Action<string, CaseActuatorOutPutType, string> ExecutiveDelegate = (innerSender, outType, yourContent) =>
            {
                if (yourExecutiveDelegate != null)
                {
                    yourExecutiveDelegate(innerSender, outType, yourContent);
                }
            };

            Func<string, bool> DealNowError = (errerData) =>
            {
                if (errerData != null)
                {
                    ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveError, errerData);
                    errorList.Add(errerData);
                    return true;
                }
                return false;
            };

            Func<string, string, string, bool> DealNowResultError = (errerData, actionType, keyName) =>
            {
                if (DealNowError(errerData))
                {
                    ExecutiveDelegate(string.Format("[CaseProtocolExecutionForConsole][ExecutionDeviceRun][{0}]", actionType), CaseActuatorOutPutType.ExecutiveError, string.Format("static data {0} error with the key :{1} ", actionType, keyName));
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
                myResult.backContent = nowExecutionContent.showContent.getTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError);
                DealNowError(tempError);
                ExecutiveDelegate(sender, CaseActuatorOutPutType.ExecutiveInfo, string.Format("【ID:{0}】Executive···\r\n【Console】\r\n{1}", caseId, myResult.backContent));
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
                                    tempRunTimeDataKey = addInfo.ConfigureData.getTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError);
                                }
                                else
                                {
                                    ExecutiveDelegate("[CaseProtocolExecutionForConsole][ExecutionDeviceRun][Add]", CaseActuatorOutPutType.ExecutiveError, string.Format("find nonsupport Protocol"));
                                    break;
                                }
                                //如果使用提供的方式进行添加是不会出现错误的（遇到重复会覆盖，只有发现多个同名Key才会返回错误）, 不过getTargetContentData需要处理用户数据可能出现错误。
                                if (!DealNowResultError(tempError, "Add", addInfo.Name))
                                {
                                    yourActuatorStaticDataCollection.AddStaticDataKey(addInfo.Name, tempRunTimeDataKey);
                                    ExecutiveDelegate("[CaseProtocolExecutionForConsole][ExecutionDeviceRun][Add]", CaseActuatorOutPutType.ExecutiveInfo, string.Format("static data add success with the key :{0} ", addInfo.Name));
                                }
                                break;
                            //caseStaticDataParameter
                            case CaseStaticDataClass.caseStaticDataParameter:
                                IRunTimeStaticData tempRunTimeStaticData;
                                string tempTypeError;
                                if (MyCaseDataTypeEngine.dictionaryStaticDataParameterAction[addInfo.StaticDataType](out tempRunTimeStaticData, out tempTypeError, addInfo.ConfigureData.getTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError)))
                                {
                                    if (!DealNowResultError(tempError, "Add", addInfo.Name))
                                    {
                                        yourActuatorStaticDataCollection.AddStaticDataParameter(addInfo.Name, tempRunTimeStaticData);
                                        ExecutiveDelegate("[CaseProtocolExecutionForConsole][ExecutionDeviceRun][Add]", CaseActuatorOutPutType.ExecutiveInfo, string.Format("static data add success with the key :{0} ", addInfo.Name));
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
                                    if (!DealNowResultError(tempError, "Add", addInfo.Name))
                                    {
                                        yourActuatorStaticDataCollection.AddStaticDataSouce(addInfo.Name, tempRunTimeDataSource);
                                        ExecutiveDelegate("[CaseProtocolExecutionForConsole][ExecutionDeviceRun][Add]", CaseActuatorOutPutType.ExecutiveInfo, string.Format("static data add success with the key :{0} ", addInfo.Name));
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
                        string tempSetVauleStr = addInfo.Value.getTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError);
                        if (!DealNowResultError(tempError, "Set", addInfo.Key))
                        {
                            if (yourActuatorStaticDataCollection.SetStaticData(addInfo.Key, tempSetVauleStr))
                            {
                                ExecutiveDelegate("[CaseProtocolExecutionForConsole][ExecutionDeviceRun][Set]", CaseActuatorOutPutType.ExecutiveInfo, string.Format("static data set success with the key :{0} ", addInfo.Key));
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
                        string tempDelVauleStr = delInfo.Value.getTargetContentData(yourActuatorStaticDataCollection, myResult.staticDataResultCollection, out tempError);
                        if (!DealNowResultError(tempError, "Del", delInfo.Value.getTargetContentData()))
                        {
                            if (yourActuatorStaticDataCollection.RemoveStaticData(tempDelVauleStr, delInfo.Key))
                            {
                                ExecutiveDelegate("[CaseProtocolExecutionForConsole][ExecutionDeviceRun][Del]", CaseActuatorOutPutType.ExecutiveInfo, string.Format("static data del success with the key :{0} ", tempDelVauleStr));
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
}
