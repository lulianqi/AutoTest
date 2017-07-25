using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;
using System.Threading;
using System.Data;

namespace MySqlHelper
{

    public delegate void delegateGetMonitorTaskDataTableInfoEventHandler(object sender, string dataVaule);

    /// <summary>
    /// SqlMonitor will check the specified position when it change he will report you the new key
    /// </summary>
    internal class SqlMonitor : IDisposable
    {

        /// <summary>
        /// SqlMonitor name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Task sql
        /// </summary>
        public string TaskSqlcmd { get; set; }

        /// <summary>
        /// Interval Time
        /// </summary>
        public int IntervalTime { get; set; }

        public int MonitorRowIndex { get; set; }

        public int MonitorColumnIndex { get; set; }

        /// <summary>
        /// if set it ture
        /// </summary>
        private bool IsKill { get; set; }

        private MySqlDrive executeMySqlDrive;

        private Thread myMonitorTaskThread;

        private ManualResetEvent myManualResetEvent = new ManualResetEvent(false);  //stop
        //myAutoResetEvent.Set();     //go           
        //myAutoResetEvent.WaitOne();    //stop

        public event delegateGetMonitorTaskDataTableInfoEventHandler OnGetMonitorTaskDataTableInfo;

        /// <summary>
        /// SqlMonitor constructor
        /// </summary>
        /// <param name="yourTaskName">task name</param>
        /// <param name="sqlcmd">sql</param>
        /// <param name="monitorRowIndex">RowIndex start with 0 （not x y）</param>
        /// <param name="monitorColumnIndex">ColumnIndex start with 0 （not x y）</param>
        /// <param name="intervalTime">interval Time</param>
        /// <param name="yourExecuteMySqlDrive">MySqlDrive</param>
        public SqlMonitor(string yourTaskName, String sqlcmd, int monitorRowIndex, int monitorColumnIndex, int intervalTime, MySqlDrive yourExecuteMySqlDrive)
        {
            Name = yourTaskName;
            TaskSqlcmd = sqlcmd;
            MonitorRowIndex = monitorRowIndex;
            MonitorColumnIndex = monitorColumnIndex;
            IntervalTime = intervalTime;
            IsKill = false;
            executeMySqlDrive = yourExecuteMySqlDrive;
        }

        private void PutOutMonitorTaskDataTableInfo(string yourPutData)
        {
            if (OnGetMonitorTaskDataTableInfo != null)
            {
                //[SqlMonitor] is private here so other class may not know this data type in IDE,so i put his name as sender
                OnGetMonitorTaskDataTableInfo(this.Name, yourPutData);
            }
        }

        /// <summary>
        /// Create  Thread  and it is Pauseed,if you wnat it run just call [ResumeAliveTask]
        /// </summary>
        /// <returns></returns>
        public bool CreateAliveTaskThread()
        {
            if (myMonitorTaskThread != null)
            {
                return false;
            }
            myMonitorTaskThread = new Thread(new ParameterizedThreadStart(MonitorTaskBody));
            myMonitorTaskThread.Name = Name + "_MonitorTask";
            myMonitorTaskThread.Priority = ThreadPriority.Normal;
            myMonitorTaskThread.IsBackground = true;
            myMonitorTaskThread.Start(null);
            return true;
        }

        /// <summary>
        /// Pause Task Thread
        /// </summary>
        public void PauseAliveTask()
        {
            if (myMonitorTaskThread != null)
            {
                myManualResetEvent.Reset();
            }
        }

        /// <summary>
        /// Resume Task Thread
        /// </summary>
        public void ResumeAliveTask()
        {
            if (myMonitorTaskThread != null)
            {
                myManualResetEvent.Set();
            }
        }

        /// <summary>
        /// Stop the Task and will set it null
        /// </summary>
        public void StopAliveTask()
        {
            if (myMonitorTaskThread != null)
            {
                IsKill = true;
                myMonitorTaskThread.Abort();
                myMonitorTaskThread = null;
            }
        }


        /// <summary>
        /// the main task work
        /// </summary>
        /// <param name="taskInfo"></param>
        private void MonitorTaskBody(object taskInfo)
        {
            string lastValue = executeMySqlDrive.ExecuteQuery(TaskSqlcmd, MonitorRowIndex, MonitorColumnIndex);
            string nowValue = null;
            while (!IsKill)
            {
                myManualResetEvent.WaitOne();
                nowValue = executeMySqlDrive.ExecuteQuery(TaskSqlcmd, MonitorRowIndex, MonitorColumnIndex);
                if (lastValue != nowValue)
                {
                    lastValue = nowValue;
                    PutOutMonitorTaskDataTableInfo(lastValue);
                }
                Thread.Sleep(IntervalTime);
            }
        }

        public void Dispose()
        {
            StopAliveTask();
            executeMySqlDrive.Dispose();
            myManualResetEvent.Dispose();
        }
    }
}
