using RemoteService.MyTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
    class RunnerService
    {
    }


    [ServiceBehavior(IncludeExceptionDetailInFaults = true ,InstanceContextMode = InstanceContextMode.Single)]
    class ExecuteService:IExecuteService,IDisposable
    {
        private static int instance = 0;

        private int instanceId;
        private OperationContext myOperationContext;      

        public ExecuteService()
        {
            instanceId = instance++;
            myOperationContext = OperationContext.Current;
            MessageTransferChannel.OnRunnerInfoCallback += MessageTransferChannel_OnRunnerInfoCallback;
        }

        public int InstanceId
        {
            get { return instanceId; }
        }

        void MessageTransferChannel_OnRunnerInfoCallback(RemoteRunnerInfo remoteRunnerInfo)
        {
            if (myOperationContext != null)
            {
                try
                {
                    (myOperationContext.GetCallbackChannel<IExecuteServiceCallBack>()).ReportState(remoteRunnerInfo);
                }
                catch
                {
                    myOperationContext = null;
                    System.Windows.Forms.MessageBox.Show("Test");
                }
            }
        }

        private IExecuteServiceCallBack CallBack
        {
            get
            {
                if (OperationContext.Current==null)
                {
                    return null;
                }
                return OperationContext.Current.GetCallbackChannel<IExecuteServiceCallBack>();
            }
        }

        public void ExecuteServiceBeat()
        {
            myOperationContext = OperationContext.Current;

            if (MessageTransferChannel.MessageCallback != null)
            {
                MessageTransferChannel.MessageCallback(null, "InstanceId:" + instanceId);
            }
        }


        public RemoteRunnerInfo GetAllRunnerSate()
        {
            if (MessageTransferChannel.OnGetAllRemoteRunnerInfo != null)
            {
                return MessageTransferChannel.OnGetAllRemoteRunnerInfo();
            }
            return null;
        }

        public void StartRunner(List<int> runnerList)
        {
            if (MessageTransferChannel.OnRunnerCommand != null)
            {
                MessageTransferChannel.OnRunnerCommand(this, RunnerCommand.Start, runnerList);
            }
        }

        public void PauseRunner(List<int> runnerList)
        {
            if (MessageTransferChannel.OnRunnerCommand != null)
            {
                MessageTransferChannel.OnRunnerCommand(this, RunnerCommand.Pause, runnerList);
            }
        }

        public void StopRunner(List<int> runnerList)
        {
            if (MessageTransferChannel.OnRunnerCommand != null)
            {
                MessageTransferChannel.OnRunnerCommand(this, RunnerCommand.Stop, runnerList);
            }
        }

        public void Dispose()
        {
            
        }

       
    }
}
