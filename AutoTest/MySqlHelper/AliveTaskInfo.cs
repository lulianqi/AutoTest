using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;
using System.Threading;
using System.Data;

namespace MySqlHelper
{

    public delegate void delegateGetAliveTaskDataTableInfoEventHandler(object sender, DataTable dataTable);

    /// <summary>
    /// AliveTask Info (if you want kill the AliveTask Thread  just set IsKill is true)
    /// </summary>
    internal class AliveTaskInfo : IDisposable
    {
        /// <summary>
        /// AliveTaskInfo name
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

        /// <summary>
        /// if set it ture
        /// </summary>
        private bool IsKill { get; set; }

        private MySqlDrive executeMySqlDrive;

        private Thread myAliveTaskThread;

        private ManualResetEvent myManualResetEvent = new ManualResetEvent(false);  //stop
        //myAutoResetEvent.Set();     //go           
        //myAutoResetEvent.WaitOne();    //stop

        

        public event delegateGetAliveTaskDataTableInfoEventHandler OnGetAliveTaskDataTableInfo;

        /// <summary>
        /// AliveTaskInfo (只要sql查询有结果即会在每次时间节点到达时触发事件，如果想要以此结果处理业务，可以在处理业务前Pause，处理完成后再Resume，或者更新sql cmd)
        /// </summary>
        /// <param name="yourTaskName">Task Name</param>
        /// <param name="sqlcmd">sql</param>
        /// <param name="intervalTime">interval Time</param>
        /// <param name="yourExecuteMySqlDrive">SqlDrive</param>
        public AliveTaskInfo(string yourTaskName, String sqlcmd, int intervalTime, MySqlDrive yourExecuteMySqlDrive)
        {
            Name = yourTaskName;
            TaskSqlcmd = sqlcmd;
            IntervalTime = intervalTime;
            IsKill = false;
            executeMySqlDrive = yourExecuteMySqlDrive;
        }

        private void PutOutAliveTaskDataTableInfo(DataTable yourPutData)
        {
            if (OnGetAliveTaskDataTableInfo != null)
            {
                //[AliveTaskInfo] is private here so other class may not know this data type in IDE,so i put his name as sender
                OnGetAliveTaskDataTableInfo(this.Name, yourPutData);
            }
        }

        /// <summary>
        /// Create  Thread  and it is Pauseed,if you wnat it run just call [ResumeAliveTask]
        /// </summary>
        /// <returns></returns>
        public bool CreateAliveTaskThread()
        {
            if (myAliveTaskThread != null)
            {
                return false;
            }
            myAliveTaskThread = new Thread(new ParameterizedThreadStart(AliveTaskBody));
            myAliveTaskThread.Name = Name + "_AliveTask";
            myAliveTaskThread.Priority = ThreadPriority.Normal;
            myAliveTaskThread.IsBackground = true;
            myAliveTaskThread.Start(null);

            return true;
        }

        /// <summary>
        /// Pause Task Thread
        /// </summary>
        public void PauseAliveTask()
        {
            if (myAliveTaskThread != null)
            {
                myManualResetEvent.Reset();
            }
        }

        /// <summary>
        /// Resume Task Thread
        /// </summary>
        public void ResumeAliveTask()
        {
            if (myAliveTaskThread != null)
            {
                myManualResetEvent.Set();
            }
        }

        /// <summary>
        /// Stop the Task and will set it null
        /// </summary>
        public void StopAliveTask()
        {
            if (myAliveTaskThread != null)
            {
                IsKill = true;
                myAliveTaskThread.Abort();
                myAliveTaskThread = null;
            }
        }

        /// <summary>
        /// the main task work
        /// </summary>
        /// <param name="taskInfo"></param>
        private void AliveTaskBody(object taskInfo)
        {
            DataTable lastTable = null;
            DataTable nowTable = null;
            while (!IsKill)
            {
                myManualResetEvent.WaitOne();
                nowTable = executeMySqlDrive.ExecuteQuery(TaskSqlcmd);
                if (nowTable != null)
                {
                    if (nowTable.Rows.Count > 0)
                    {
                        PutOutAliveTaskDataTableInfo(nowTable);
                    }
                    else
                    {
                        if (lastTable == null)
                        {
                            PutOutAliveTaskDataTableInfo(nowTable);
                        }
                        else if (lastTable.Rows.Count > 0)
                        {
                            PutOutAliveTaskDataTableInfo(nowTable);
                        }
                    }
                }
                else
                {
                    executeMySqlDrive.SetErrorMes(" [ExecuteQuery] fail in RunSynchronousAliveTask");
                }
                lastTable = nowTable;
                Thread.Sleep(IntervalTime);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            StopAliveTask();
            executeMySqlDrive.Dispose();
            myManualResetEvent.Dispose();
        }


        ~AliveTaskInfo()
        {
            Dispose(false);
        }

        //protected override void Finalize()
        //{
        //    try
        //    {
        //        //do ~AliveTaskInfo()
        //    }
        //    finally
        //    {
        //        //base.Finalize();
        //    }
        //}
    }
}
