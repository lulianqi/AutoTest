using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
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


namespace RemoteService.MyService
{
    class IRunnerService
    {

    }

    
    [ServiceContract( SessionMode = SessionMode.Required, CallbackContract = typeof(IExecuteServiceCallBack))]
    public interface IExecuteService
    {
        [OperationContract(IsOneWay = true)]
        void ExecuteServiceBeat();

        [OperationContract]
        RemoteRunnerInfo GetAllRunnerSate();

        [OperationContract]
        void StartRunner(List<int> runnerList);

        [OperationContract]
        void PauseRunner(List<int> runnerList);

        [OperationContract]
        void StopRunner(List<int> runnerList);
    }

    public interface IExecuteServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void ReportState(RemoteRunnerInfo remoteRunnerInfo);
    }



    [DataContract]
    public class RunnerState
    {
        int runnerID;
        string runnerName;
        string nowCell;
        string runDetails;
        string cellResult;
        string time;
        string state;
        List<KeyValuePair<int, int>> runnerProgress;

        [DataMember]
        public int RunnerID
        {
            get { return runnerID; }
            set { runnerID = value; }
        }

        [DataMember]
        public string RunnerName
        {
            get { return runnerName; }
            set { runnerName = value; }
        }

        [DataMember]
        public string NowCell
        {
            get { return nowCell; }
            set { nowCell = value; }
        }

        [DataMember]
        public string RunDetails
        {
            get { return runDetails; }
            set { runDetails = value; }
        }

        [DataMember]
        public string CellResult
        {
            get { return cellResult; }
            set { cellResult = value; }
        }

        [DataMember]
        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        [DataMember]
        public string State
        {
            get { return state; }
            set { state = value; }
        }

        [DataMember]
        public List<KeyValuePair<int, int>> RunnerProgress
        {
            get { return runnerProgress; }
            set { runnerProgress = value; }
        }
    }

    [DataContract]
    public class RemoteRunnerInfo
    {
        List<RunnerState> runnerStateList;

        [DataMember]
        public List<RunnerState> RunnerStateList
        {
            get { return runnerStateList; }
        }

        public void AddRunnerState(RunnerState runnerState)
        {
            if(runnerState==null)
            {
                return;
            }
            if(runnerStateList==null)
            {
                runnerStateList = new List<RunnerState>();
            }
            runnerStateList.Add(runnerState);
        }
    }
}
