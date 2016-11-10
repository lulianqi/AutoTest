using RemoteService.MyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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


namespace RemoteService.MyTool
{
    public enum RunnerCommand
    {
        Start=0,
        Stop=1,
        Pause=2,
        Set=3
    }

    class MessageTransferChannel
    {
        public static Action<object, string> MessageCallback;

        public delegate void RunnerCommandCallback(ExecuteService sender, RunnerCommand command, List<int> runners);
        public delegate RemoteRunnerInfo GetAllRunnerInfoCallback();
        public delegate void GetRunnerInfoCallback(RemoteRunnerInfo remoteRunnerInfo);

        public static RunnerCommandCallback OnRunnerCommand;
        public static GetAllRunnerInfoCallback OnGetAllRemoteRunnerInfo;
        public static GetRunnerInfoCallback OnRunnerInfoCallback;

        public static string message;
        public static int index;
    }
}
