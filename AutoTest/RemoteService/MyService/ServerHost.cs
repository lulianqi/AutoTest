using CaseExecutiveActuator;
using RemoteService.MyTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;


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


namespace RemoteService.MyService
{
    class ServerHost
    {
        Uri baseAddress = new Uri("http://localhost:8087/SelService");//初始默认值在运行时由设置值来决定
        ServiceHost baseHost = null;

        public delegate void ServerHostMessageEventHandler(string sender, string message);
        public event ServerHostMessageEventHandler OnServerHostMessage;

        public delegate List<CaseRunner> BackNowRunnerListEventHandler();
        public event BackNowRunnerListEventHandler OnBackNowRunnerList;

        //public delegate void ServerHostCommandEventHandler(RunnerCommand command, List<int> runners);
        //public event ServerHostCommandEventHandler OnServerHostCommand;

        /// <summary>
        /// 获取或设置当前BaseAddress
        /// </summary>
        public Uri BaseAddress
        {
            get { return baseAddress; }
            set { baseAddress = value; }
        }

        /// <summary>
        /// 获取当前BaseHost
        /// </summary>
        public ServiceHost BaseHost
        {
            get { return baseHost; }
        }

        /// <summary>
        /// 获取BaseHost当前连接状态
        /// </summary>
        public CommunicationState BaseHostState
        {
            get
            {
                if (baseHost == null)
                {
                    return CommunicationState.Closed;
                }
                else
                {
                    return baseHost.State;
                }
            }
        }

        /// <summary>
        /// 初始化ServerHost
        /// </summary>
        /// <param name="yourBaseAddress"></param>
        public ServerHost(Uri yourBaseAddress)
        {
            baseAddress = yourBaseAddress;
            MessageTransferChannel.MessageCallback += new Action<object, string>((a, b) => AddInfo(b));

            MessageTransferChannel.OnRunnerCommand += MessageTransferChannel_RunnerCommandCallback;
            MessageTransferChannel.OnGetAllRemoteRunnerInfo += MessageTransferChannel_GetAllRemoteRunnerInfoCallback;
        }

        /// <summary>
        /// 处理ExecuteService的远程指令【runner状态获取】
        /// </summary>
        RemoteRunnerInfo MessageTransferChannel_GetAllRemoteRunnerInfoCallback()
        {
            List<CaseRunner> caseRunnerList = CaseRunnerList;
            if (caseRunnerList == null)
            {
                return new RemoteRunnerInfo();
            }
            return GetRunnerInfo(caseRunnerList, true);
        }

        /// <summary>
        /// 处理ExecuteService的远程指令【runner控制】
        /// </summary>
        void MessageTransferChannel_RunnerCommandCallback(ExecuteService sender, RunnerCommand command, List<int> runners)
        {
            List<CaseRunner> caseRunnerList=CaseRunnerList;
            if(caseRunnerList==null)
            {
                return;
            }
            RunnerCommandExecute(caseRunnerList, command, runners);
        }

        /// <summary>
        /// 触发更新CaseRunner状态双工回调（局部更新）
        /// </summary>
        /// <param name="caseRunnerList">CaseRunner 列表</param>
        public void SendStateCallBack(List<CaseRunner> caseRunnerList)
        {
            SendStateInfo(caseRunnerList, false);
        }

        /// <summary>
        /// 触发更新CaseRunner状态双工回调（全部更新）
        /// </summary>
        /// <param name="caseRunnerList">CaseRunner 列表</param>
        public void SendAllStateCallBack(List<CaseRunner> caseRunnerList)
        {
            SendStateInfo(caseRunnerList, true);
        }

        /// <summary>
        /// 像执行行体请求CaseRunner 最新列表
        /// </summary>
        private List<CaseRunner> CaseRunnerList
        {
            get
            {
                if (OnBackNowRunnerList() != null)
                {
                    return OnBackNowRunnerList();
                }
                return null;
            }
        }

        /// <summary>
        /// 输出提示信息
        /// </summary>
        /// <param name="message"></param>
        private void AddInfo(string message)
        {
            if(OnServerHostMessage!=null)
            {
                this.OnServerHostMessage("baseHost", message);
            }
        }

        /// <summary>
        /// 启动BaseHost
        /// </summary>
        public void OpenBaseHost()
        {
            if (baseHost == null)
            {
                baseHost = new ServiceHost(typeof(ExecuteService), baseAddress);

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;

                baseHost.Description.Behaviors.Add(smb);
                //baseHost.AddServiceEndpoint(typeof(IExecuteService), new BasicHttpBinding(), "");
                //baseHost.AddServiceEndpoint(typeof(IExecuteService), new WSDualHttpBinding(), "");

                System.ServiceModel.Channels.Binding binding = new WSDualHttpBinding();
                ((System.ServiceModel.WSDualHttpBinding)(binding)).Security.Mode = WSDualHttpSecurityMode.None;
                ((System.ServiceModel.WSDualHttpBinding)(binding)).Security.Message.ClientCredentialType = MessageCredentialType.UserName;

                System.ServiceModel.Channels.Binding tcpBinding = new NetTcpBinding();
                ((System.ServiceModel.NetTcpBinding)(tcpBinding)).Security.Mode = SecurityMode.None;
                ((System.ServiceModel.NetTcpBinding)(tcpBinding)).Security.Message.ClientCredentialType = MessageCredentialType.UserName;

                //测试开安全双工只能在本机使用
                baseHost.AddServiceEndpoint(typeof(IExecuteService), binding, "");

                baseHost.Opening += new EventHandler((yourObject, yourEventAgrs) => AddInfo("Opening"));
                baseHost.Opened += new EventHandler((yourObject, yourEventAgrs) => AddInfo("Opened"));
                baseHost.Closed += new EventHandler((yourObject, yourEventAgrs) => AddInfo("Closed"));
                baseHost.Closing += new EventHandler((yourObject, yourEventAgrs) => AddInfo("Closing"));
                baseHost.Faulted += new EventHandler((yourObject, yourEventAgrs) => AddInfo("Faulted"));


                Thread openBaseThread = new Thread(new ThreadStart(BaseHostOpen));
                openBaseThread.IsBackground = true;
                openBaseThread.Start();
              
            }
            else
            {
                if (baseHost.State == CommunicationState.Opened)
                {
                    AddInfo("服务已经开启");
                }
                else if (baseHost.State == CommunicationState.Opening)
                {
                    AddInfo("服务正在开启");
                }
                else
                {
                    baseHost.Abort();
                    baseHost = null;
                    OpenBaseHost();
                }
            }
        }

        private void BaseHostOpen()
        {
            try
            {
                baseHost.Open();
            }
            catch (Exception ex)
            {
                AddInfo(ex.Message);
            }
        }

        /// <summary>
        /// 关闭BaseHost
        /// </summary>
        public void CloseBaseHost()
        {
            if (baseHost == null)
            {
                AddInfo("未发现服务");
            }
            else
            {
                if (baseHost.State != CommunicationState.Closed)
                {
                    AddInfo(baseAddress.ToString() + "服务关闭");
                    baseHost.Close();
                }
                else
                {
                    AddInfo(baseAddress.ToString() + "服务已经关闭");
                }
            }
        }


        /// <summary>
        /// 执行远程命令
        /// </summary>
        /// <param name="caseRunnerList">CaseRunner 列表</param>
        /// <param name="command">命令类型</param>
        /// <param name="Runners">受影响Runner</param>
        private void RunnerCommandExecute(List<CaseRunner> caseRunnerList, RunnerCommand command, List<int> Runners)
        {
            switch (command)
            {
                case RunnerCommand.Start:
                    if (Runners != null)
                    {
                        if (Runners.Count > 0)
                        {
                            foreach (int tempRunnerIndex in Runners)
                            {
                                if (caseRunnerList.Count >= tempRunnerIndex)
                                {
                                    caseRunnerList[tempRunnerIndex].RunQuiet();
                                }
                                else
                                {
                                    AddInfo("tempRunnerIndex error");
                                }
                            }
                        }
                    }
                    else
                    {
                        AddInfo("Runners is null");
                    }
                    break;
                case RunnerCommand.Stop:
                    if (Runners != null)
                    {
                        if (Runners.Count > 0)
                        {
                            foreach (int tempRunnerIndex in Runners)
                            {
                                if (caseRunnerList.Count >= tempRunnerIndex)
                                {
                                    caseRunnerList[tempRunnerIndex].StopQuiet();
                                }
                                else
                                {
                                    AddInfo("tempRunnerIndex error");
                                }
                            }
                        }
                    }
                    else
                    {
                        AddInfo("Runners is null");
                    }
                    break;
                case RunnerCommand.Pause:
                    if (Runners != null)
                    {
                        if (Runners.Count > 0)
                        {
                            foreach (int tempRunnerIndex in Runners)
                            {
                                if (caseRunnerList.Count >= tempRunnerIndex)
                                {
                                    caseRunnerList[tempRunnerIndex].PauseQuiet();
                                }
                                else
                                {
                                    AddInfo("tempRunnerIndex error");
                                }
                            }
                        }
                    }
                    else
                    {
                        AddInfo("Runners is null");
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 触发更新CaseRunner状态双工回调
        /// </summary>
        /// <param name="caseRunnerList">CaseRunner 列表</param>
        /// <param name="isAll">是否全部更新</param>
        private void SendStateInfo(List<CaseRunner> caseRunnerList,bool isAll)
        {
            if (BaseHostState != CommunicationState.Opened)
            {
                return;
            }
            if (caseRunnerList.Count > 0)
            {
                RemoteRunnerInfo remoteRunnerInfo = GetRunnerInfo(caseRunnerList,isAll);
                if (remoteRunnerInfo.RunnerStateList != null)
                {
                    if (remoteRunnerInfo.RunnerStateList.Count > 0)
                    {
                        if (MessageTransferChannel.OnRunnerInfoCallback != null)
                        {
                            MessageTransferChannel.OnRunnerInfoCallback(remoteRunnerInfo);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取List<CaseRunner>中的更新信息
        /// </summary>
        /// <param name="runnerList">CaseRunner 列表</param>
        /// <param name="isUpdataAll">T表示全部更新，F标识局部更新</param>
        /// <returns>更新信息</returns>
        private RemoteRunnerInfo GetRunnerInfo(List<CaseRunner> runnerList, bool isUpdataAll)
        {
            RemoteRunnerInfo remoteRunnerInfo = new RemoteRunnerInfo();
            if (runnerList == null)
            {
                return null;
            }
            foreach (CaseRunner tempRunner in runnerList)
            {
                if (tempRunner.IsNeedUpdata || isUpdataAll)
                {
                    tempRunner.IsNeedUpdata = false;

                    RunnerState tempRunnerState = new RunnerState();
                    if (tempRunner.RunerActuator.NowExecutionResultList != null)
                    {
                        if (tempRunner.RunerActuator.NowExecutionResultList.Count > 0)
                        {
                            MyExecutionDeviceResult tempResult = tempRunner.RunerActuator.NowExecutionResultList[tempRunner.RunerActuator.NowExecutionResultList.Count - 1];
                            tempRunnerState.RunnerID = runnerList.IndexOf(tempRunner);
                            tempRunnerState.RunnerName = tempRunner.RunnerName;
                            tempRunnerState.NowCell = tempResult.caseId.ToString();
                            tempRunnerState.CellResult = tempResult.result.ToString();
                            tempRunnerState.State = tempRunner.RunnerState.ToString();
                            tempRunnerState.Time = tempResult.spanTime;
                            tempRunnerState.RunDetails = tempResult.backContent;
                            tempRunnerState.RunnerProgress = tempRunner.RunerActuator.RunProgress;
                            remoteRunnerInfo.AddRunnerState(tempRunnerState);
                        }
                        else if (isUpdataAll)//对于空NowExecutionResultList来说，如果要求全部更新则要求构造一个初始的数据体
                        {
                            tempRunnerState.RunnerID = runnerList.IndexOf(tempRunner);
                            tempRunnerState.RunnerName = tempRunner.RunnerName;
                            tempRunnerState.NowCell = "";
                            tempRunnerState.CellResult = "";
                            tempRunnerState.State = tempRunner.RunnerState.ToString();
                            tempRunnerState.Time = "";
                            tempRunnerState.RunDetails = "";
                            tempRunnerState.RunnerProgress = tempRunner.RunerActuator.RunProgress;
                            remoteRunnerInfo.AddRunnerState(tempRunnerState);
                        }
                        else
                        {
                            tempRunnerState.RunnerID = runnerList.IndexOf(tempRunner);
                            tempRunnerState.RunnerName = tempRunner.RunnerName;
                            tempRunnerState.State = tempRunner.RunnerState.ToString();
                            tempRunnerState.RunnerProgress = tempRunner.RunerActuator.RunProgress;
                            remoteRunnerInfo.AddRunnerState(tempRunnerState);
                        }
                    }
                }
            }
            return remoteRunnerInfo;
        }

        /// <summary>
        /// 获取List<CaseRunner>中的更新信息（局部更新）
        /// </summary>
        /// <param name="runnerList">CaseRunner 列表</param>
        /// <returns>更新信息</returns>
        private RemoteRunnerInfo GetRunnerInfo(List<CaseRunner> runnerList)
        {
            return GetRunnerInfo(runnerList, false);
        }

    }
}
