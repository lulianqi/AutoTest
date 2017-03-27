using AutoTest.RemoteServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;

using AutoTest.myDialogWindow;
using AutoTest.myControl;


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


namespace AutoTest.myTool
{
    class ExecuteServiceCallback : IExecuteServiceCallback
    {

        public ExecuteServiceCallback()
        {

        }

        public delegate void RunnerStateChangeEventHandler(ExecuteServiceCallback sender, RemoteRunnerInfo remoteRunnerInfo);

        public event RunnerStateChangeEventHandler OnRunnerStateChange;

        public void ReportState(RemoteRunnerInfo remoteRunnerInfo)
        {
            if (OnRunnerStateChange!=null)
            {
                this.OnRunnerStateChange(this, remoteRunnerInfo);
            }
        }
    }

    public class RemoteClient:IDisposable
    {

        /// <summary>
        /// 描述当前Client连接状态
        /// </summary>
        public enum RemoteClientState
        {
            Connecting=0,               //正在连接
            Connected=1,                //连接成功
            Break = 2,                  //连接中断，并且正在进行重连
            Lost = 3                    //连接中断，且不会重新连接（必须触发重连）
        }

        ExecuteServiceClient executeServiceClient = null;
        InstanceContext instanceContext = null;
        //EndpointAddress myEp = new EndpointAddress("http://localhost:8087/SelService");
        EndpointAddress myEp = null;

        private RemoteRunner showWindow;
        private bool isLive = false;
        private RemoteClientState clientState = RemoteClientState.Lost;
        private int reCounectTime=5;

        private Thread clientLife;


        public delegate void RunnerStateChangeEventHandler(RemoteClient sender, RemoteRunnerInfo remoteRunnerInfo);
        public delegate void AllRunnerInforEventHandler(RemoteClient sender, RemoteRunnerInfo remoteRunnerInfo);
        public delegate void ClientStateChangeEventHandler(RemoteClient sender, RemoteClientState nowSate);
        public delegate void ClientErrorEventHandler(RemoteClient sender, string errorInfo);

        public event RunnerStateChangeEventHandler OnRunnerStateChange;
        public event AllRunnerInforEventHandler OnPutAllRunnerInfor;
        public event ClientStateChangeEventHandler OnClientStateChange;
        public event ClientErrorEventHandler OnClientErrorInfor;

        

        public RemoteClient(EndpointAddress yourEp, RemoteRunner yourWindow )
        {
            myEp = yourEp;
            showWindow = yourWindow;
        }

        public override string ToString()
        {
            if (myEp != null)
            {
                return myEp.Uri.Host + ":" + myEp.Uri.Port;
            }
            else
            {
                return "Null Ep";
            }
        }

        /// <summary>
        /// 获取或设置RemoteClient 的基础地址
        /// </summary>
        public EndpointAddress ClientEp
        {
            get { return myEp;}
            set { myEp = value; }
        }

        /// <summary>
        /// 获取或设置ShowWindow
        /// </summary>
        public RemoteRunner ShowWindow
        {
            get { return showWindow; }
            set { showWindow = value; }
        }

        /// <summary>
        /// 获取当前Client连接状态(自维护状态，该状态同时提示生命线程运行情况)
        /// </summary>
        public RemoteClientState ClientState
        {
            get { return clientState; }
        }

        /// <summary>
        /// 获取当前executeServiceClient通道状态
        /// </summary>
        public CommunicationState  ExecuteServiceClientState
        {
            get 
            {
                if (executeServiceClient==null)
                {
                    return CommunicationState.Closed;
                }
                return executeServiceClient.State; 
            }
        }

        /// <summary>
        /// 获取或设置断线重连次数限制
        /// </summary>
        public int ReCounectTime
        {
            get { return reCounectTime; }
            set { reCounectTime = value; }
        }

        /// <summary>
        /// 报告当前Client所有Runner状态
        /// </summary>
        /// <param name="remoteRunnerInfo"></param>
        private void PutAllRunnerInfor(RemoteRunnerInfo remoteRunnerInfo)
        {
            if (OnPutAllRunnerInfor != null)
            {
                this.OnPutAllRunnerInfor(this, remoteRunnerInfo);
            }
            if (showWindow != null)
            {
                showWindow.RefreshRemoteRunnerView(remoteRunnerInfo);
            }       
        }

        /// <summary>
        /// 改变连接状态
        /// </summary>
        private void SetClientState(RemoteClientState nowState)
        {
            clientState = nowState;
            if (OnClientStateChange != null)
            {
                this.OnClientStateChange(this, nowState);
            }
        }

        /// <summary>
        /// 向订阅者报告错误信息
        /// </summary>
        /// <param name="errorInfo">错误信息</param>
        private void SetClientErrorInfo(string errorInfo)
        {
            if(errorInfo!=null && OnClientErrorInfor!=null)
            {
                this.OnClientErrorInfor(this, errorInfo);
            }
        }

        /// <summary>
        /// 创建一个【ExecuteServiceClient】实例
        /// </summary>
        /// <returns>【ExecuteServiceClient】实例</returns>
        private ExecuteServiceClient RestartClient()
        {
            if (instanceContext==null)
            {
                //InstanceContext
                ExecuteServiceCallback executeServiceCallback = new ExecuteServiceCallback();
                executeServiceCallback.OnRunnerStateChange += executeServiceCallback_OnRunnerStateChange;
                instanceContext = new InstanceContext(executeServiceCallback);
                //Binding
                System.ServiceModel.Channels.Binding binding = new WSDualHttpBinding();
                ((System.ServiceModel.WSDualHttpBinding)(binding)).Security.Mode = WSDualHttpSecurityMode.None;
                ((System.ServiceModel.WSDualHttpBinding)(binding)).Security.Message.ClientCredentialType = MessageCredentialType.UserName;
                binding.SendTimeout = new TimeSpan(0, 0, 10);
                binding.OpenTimeout = new TimeSpan(0, 0, 10);
                binding.ReceiveTimeout = new TimeSpan(0, 0, 10);

                System.ServiceModel.Channels.Binding tcpBinding = new NetTcpBinding();
                ((System.ServiceModel.NetTcpBinding)(tcpBinding)).Security.Mode = SecurityMode.None;
                ((System.ServiceModel.NetTcpBinding)(tcpBinding)).Security.Message.ClientCredentialType = MessageCredentialType.UserName;
                tcpBinding.SendTimeout = new TimeSpan(0, 0, 10);
                tcpBinding.OpenTimeout = new TimeSpan(0, 0, 10);
                tcpBinding.ReceiveTimeout = new TimeSpan(0, 0, 10);

                executeServiceClient = new ExecuteServiceClient(instanceContext, binding, myEp);
                //executeServiceClient = new ExecuteServiceClient(instanceContext, new WSDualHttpBinding(), myEp);

                instanceContext.Closed += instanceContext_Closed;
                instanceContext.Opened += instanceContext_Opened;
                return executeServiceClient;
            }
            else
            {
                instanceContext.Closed -= instanceContext_Closed;
                instanceContext.Opened -= instanceContext_Opened;
                instanceContext = null;
                return RestartClient();
            }
        }

        #region RestartClient通信
        
        /// <summary>
        /// 处理收到的双工回调（仅报告有变化的Runner）
        /// </summary>
        void executeServiceCallback_OnRunnerStateChange(ExecuteServiceCallback sender, RemoteRunnerInfo remoteRunnerInfo)
        {
            if(OnRunnerStateChange!=null)
            {
                this.OnRunnerStateChange(this, remoteRunnerInfo);
            }
            if (remoteRunnerInfo == null)
            {
                if (showWindow != null)
                {
                    showWindow.ShowError("Null Data" + "\r\n");
                }
                return;
            }
            if (remoteRunnerInfo.RunnerStateList != null)
            {
                if (showWindow != null)
                {
                    showWindow.UpdataRemoteRunnerView(remoteRunnerInfo);
                }
            }
        }

        /// <summary>
        /// 获取当前Client所有Runner状态
        /// </summary>
        public void GetAllRunnerInfor()
        {
            if (ExecuteServiceClientState == CommunicationState.Opened)
            {
                try
                {
                    RemoteRunnerInfo nowRemoteRunnerInfo = executeServiceClient.GetAllRunnerSate();
                    if (nowRemoteRunnerInfo != null)
                    {
                        PutAllRunnerInfor(nowRemoteRunnerInfo);
                    }
                }
                catch (Exception ex)
                {
                    SetClientErrorInfo(ex.Message);
                }
            }
            else
            {
                SetClientErrorInfo("连接未打开");
            }
        }

        /// <summary>
        /// 获取当前Client所有Runner状态(提供内部使用，不会捕捉错误)
        /// </summary>
        private void GetAllRunnerInforEx()
        {
            RemoteRunnerInfo nowRemoteRunnerInfo = executeServiceClient.GetAllRunnerSate();
            if (nowRemoteRunnerInfo != null)
            {
                PutAllRunnerInfor(nowRemoteRunnerInfo);
            }
        }

        /// <summary>
        /// 启动指定执行器
        /// </summary>
        /// <param name="runnerList">执行器列表</param>
        public void StartRunner(List<int> runnerList)
        {
            if (ExecuteServiceClientState == CommunicationState.Opened)
            {
                try
                {
                    executeServiceClient.StartRunner(runnerList.ToArray());
                }
                catch (Exception ex)
                {
                    SetClientErrorInfo(ex.Message);
                }
            }
            else
            {
                SetClientErrorInfo("连接未打开");
            }
        }

        /// <summary>
        /// 暂停指定执行器
        /// </summary>
        /// <param name="runnerList">执行器列表</param>
        public void PauseRunner(List<int> runnerList)
        {
            if (ExecuteServiceClientState == CommunicationState.Opened)
            {
                try
                {
                    executeServiceClient.PauseRunner(runnerList.ToArray());
                }
                catch (Exception ex)
                {
                    SetClientErrorInfo(ex.Message);
                }
            }
            else
            {
                SetClientErrorInfo("连接未打开");
            }
        }

        /// <summary>
        /// 停止指定执行器
        /// </summary>
        /// <param name="runnerList">执行器列表</param>
        public void StopRunner(List<int> runnerList)
        {
            if (ExecuteServiceClientState == CommunicationState.Opened)
            {
                try
                {
                    executeServiceClient.StopRunner(runnerList.ToArray());
                }
                catch(Exception ex)
                {
                    SetClientErrorInfo(ex.Message);
                }
            }
            else
            {
                SetClientErrorInfo("连接未打开");
            }
        }

        #endregion


        private void instanceContext_Closed(object sender, EventArgs e)
        {
            
        }

        void instanceContext_Opened(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 启动当前Client
        /// </summary>
        public void StartClient()
        {
            //Thread myThreadTest = new Thread(new ThreadStart(ExecutiveActuatorTask),10240);
            if (clientLife != null)
            {
                if (clientLife.IsAlive)
                {
                    if (showWindow != null)
                    {
                        showWindow.ShowError("IsAlive");
                    }
                }
                else
                {
                    clientLife = null;
                    StartClient();
                }
            }
            else
            {
                clientLife = new Thread(new ParameterizedThreadStart(ClientAliveTask));
                clientLife.Name = "ClientLife" + myEp.ToString();
                clientLife.Priority = ThreadPriority.Normal;
                clientLife.IsBackground = true;
                isLive = true;
                clientLife.Start(executeServiceClient);
            }
        }

        /// <summary>
        /// 停止当前Client
        /// </summary>
        public void StopClient()
        {
            isLive = false;
        }


        /// <summary>
        /// 保持连接的心跳及重新连接的线程任务
        /// </summary>
        private void ClientAliveTask(object yourExecuteClient)
        {
            ExecuteServiceClient executeClient = (ExecuteServiceClient)yourExecuteClient;
            int counectTime = reCounectTime;

            ReConnect:
            executeClient = RestartClient();
            try
            {
                SetClientState(RemoteClientState.Connecting);
                GetAllRunnerInforEx();
                SetClientState(RemoteClientState.Connected);
            }
            catch (Exception ex)
            {
                MyCommonHelper.ErrorLog.PutInLog(ex);
                if (counectTime > 0 && isLive)
                {
                    counectTime--;
                    SetClientState(RemoteClientState.Break);
                    Thread.Sleep(2000);
                    goto ReConnect;
                }
                else
                {
                    StopClient();
                }
            }

            while (isLive)
            {
                try
                {
                    executeClient.ExecuteServiceBeat();
                }
                catch (Exception ex)
                {
                    SetClientState(RemoteClientState.Break);
                    Thread.Sleep(2000);
                    MyCommonHelper.ErrorLog.PutInLog(ex);
                    counectTime = reCounectTime;
                    goto ReConnect;
                }
                Thread.Sleep(10000);
            }

            SetClientState(RemoteClientState.Lost);
        }

        public void Dispose()
        {
            if (clientLife != null)
            {
                if (clientLife.IsAlive)
                {
                    clientLife.Abort();
                }
            }
            executeServiceClient = null;
            instanceContext = null;
        }
    }
}
