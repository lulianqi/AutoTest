using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;
using System.Threading;
using System.Data;

/*******************************************************************************
* Copyright (c) 2016 lulianqi 
* All rights reserved.
* 
* 文件名称: 
* 内容摘要: mycllq@hotmail.com
* 
* 历史记录:
* 日	  期:   201700120          创建人: lulianqi mycllq@hotmail.com
* 描    述: 创建
*******************************************************************************/

namespace MySqlHelper
{
    public class MySqlDrive : IDisposable
    {
        #region inner class

        #endregion

        #region delegate and event
        public delegate void delegateGetErrorMessageEventHandler(object sender, string ErrorMessage);
        public delegate void delegateGetInfoMessageEventHandler(object sender, string InfoMessage);
        public delegate void delegateDriveStateChangeEventHandler(object sender, bool isCounect);

        //public delegate void delegateGetAliveTaskDataTableInfoEventHandler(object sender, DataTable dataTable);
        //public delegate void delegateGetMonitorTaskDataTableInfoEventHandler(object sender, string dataVaule);

        public event delegateGetErrorMessageEventHandler OnGetErrorMessage;       //报告错误信息
        public event delegateGetInfoMessageEventHandler OnGetInfoMessage;         //报告提示信息
        public event delegateDriveStateChangeEventHandler OnDriveStateChange;     //报告状态变化信息
        #endregion

        #region field
        private Dictionary<string, AliveTaskInfo> aliveTaskList = new Dictionary<string, AliveTaskInfo>();
        private Dictionary<string, SqlMonitor> aliveMonitorList = new Dictionary<string, SqlMonitor>();
        private AutoResetEvent myAutoResetEvent = new AutoResetEvent(false);    //由于直接的查询执行公用了MySqlDataAdapter等信息所以要保证同步执行，如果要在未返回结果的时候执行其他查询请使用多个MySqlDrive，或重载ExecuteQuery执行方法（不建议）
        private string data_source = ""; //Server=192.168.200.152;UserId=root;Password=xpsh;Database=huala_test
        private bool isDriveConnect = false;
        private MySqlConnection myConnection;
        private MySqlCommand myCommand;
        private MySqlDataAdapter myAdapter;
        private MySqlTransaction myTransaction;
        private DataTable myTable;
        private string nowError;
        private int defaultReconnectTime;
        private int reconnectTime;
        #endregion

        #region attribute
        /// <summary>
        /// get Conninfo string
        /// </summary>
        public string ConnStr
        {
            get { return data_source; }
        }

        /// <summary>
        /// get a vaule that mean MySqlDrive is connected
        /// </summary>
        public bool IsDriveConnect
        {
            get { return isDriveConnect; }
        }


        /// <summary>
        /// get the last error message
        /// </summary>
        public string NowError
        {
            get { return nowError; }
        }

        /// <summary>
        /// get or set the times of reconnect to mysql
        /// </summary>
        public int ReconnectTime
        {
            get { return defaultReconnectTime; }
            set { defaultReconnectTime = reconnectTime = value; }
        }

        #endregion

        #region function
        /// <summary>
        /// initialize a new MySqlDrive
        /// </summary>
        /// <param name="connStr">conn Str</param>
        public MySqlDrive(string connStr)
        {
            data_source = connStr;
            isDriveConnect = false;
            defaultReconnectTime = reconnectTime = 1;
            myAutoResetEvent.Set();
        }

        /// <summary>
        /// set error message your should use it deal your error that you want tell the user
        /// </summary>
        /// <param name="errorMes">error message</param>
        internal void SetErrorMes(string errorMes)
        {
            nowError = errorMes;
            if (OnGetErrorMessage != null)
            {
                OnGetErrorMessage(this, errorMes);
            }
        }

        /// <summary>
        /// set info message your should use it deal your error that you want tell the user
        /// </summary>
        /// <param name="errorMes">error message</param>
        private void SetInfoMes(string infoMes)
        {
            if (OnGetInfoMessage != null)
            {
                OnGetInfoMessage(this, infoMes);
            }
        }

        /// <summary>
        /// updata Drive Connect State
        /// </summary>
        /// <param name="isConnect">is Connect</param>
        private void UpdataDriveConnectState(bool isConnect)
        {
            if (isConnect != isDriveConnect)
            {
                isDriveConnect = isConnect;
                if (OnDriveStateChange != null)
                {
                    OnDriveStateChange(this, this.IsDriveConnect);
                }
            }
        }

        /// <summary>
        /// Connect DataBase if it is need (each [ExecuteQuery] will check the Connect so you donot need call it befor ExecuteQuery)
        /// </summary>
        /// <returns>is sucusse</returns>
        public bool ConnectDataBase()
        {
            try
            {
                if (myConnection == null)
                {
                    myConnection = new MySqlConnection(data_source);
                }
                if (myConnection.State == ConnectionState.Closed || myConnection.State == ConnectionState.Broken)
                {
                    try
                    {
                        myConnection.Open();
                    }
                    catch
                    {
                        if (reconnectTime > 0)
                        {
                            SetInfoMes("Reconnect to the database");
                            reconnectTime--;
                            myConnection.Dispose();
                            myConnection = new MySqlConnection(data_source);
                            return ConnectDataBase();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                UpdataDriveConnectState(true);
                reconnectTime = defaultReconnectTime;
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                if (myConnection.State == ConnectionState.Open || myConnection.State == ConnectionState.Connecting)
                {
                    //MySql库中链接异常时也显示open状态，导致最后会跳过连接
                    myConnection.Close();
                }
                SetErrorMes("DataBase Connect Fial");
                SetErrorMes(ex.Message);
            }
            catch (Exception ex)
            {
                if (myConnection.State == ConnectionState.Open || myConnection.State == ConnectionState.Connecting)
                {
                    myConnection.Close();
                }
                SetErrorMes(ex.Message);
            }
            UpdataDriveConnectState(false);
            return false;
        }

        /// <summary>
        /// Connect DataBase it will DisConnect fist（and if it need connect it will use new sql connect str）
        /// </summary>
        /// <param name="connStr">new connect str (一定会使重新连接)</param>
        /// <returns>is sucusse</returns>
        public bool ConnectDataBase(string connStr)
        {
            DisConnect();
            data_source = connStr;
            return ConnectDataBase();
        }

        /// <summary>
        /// Dis Connect
        /// </summary>
        public void DisConnect()
        {
            if (myConnection != null)
            {
                myConnection.Close();
            }
        }

        /// <summary>
        /// Add a new AliveTask (it will be a new Thread)
        /// </summary>
        /// <param name="yourName">your task name</param>
        /// <param name="yourInfo">your task info</param>
        /// <returns></returns>
        private bool AddAliveTaskInfo(string yourName, AliveTaskInfo yourInfo)
        {
            if (aliveTaskList.ContainsKey(yourName))
            {
                SetErrorMes("existed task name");
                return false;
            }
            else
            {
                aliveTaskList.Add(yourName, yourInfo);
                return true;
            }
        }

        /// <summary>
        /// Add a new AliveMonitor (it will be a new Thread)
        /// </summary>
        /// <param name="yourName">your monitor name</param>
        /// <param name="yourInfo">your monitor info</param>
        /// <returns></returns>
        private bool AddAliveMonitorInfo(string yourName, SqlMonitor yourInfo)
        {
            if (aliveMonitorList.ContainsKey(yourName))
            {
                SetErrorMes("existed task name");
                return false;
            }
            else
            {
                aliveMonitorList.Add(yourName, yourInfo);
                return true;
            }
        }

        #endregion

        #region action

        #region AliveTask
        /// <summary>
        /// Create New AliveTask [it will not start ]
        /// </summary>
        /// <param name="yourTaskName">Task Name</param>
        /// <param name="sqlcmd">mysql sql (只要sql查询有结果即会在每次时间节点到达时触发事件，如果想要以此结果处理业务，可以在处理业务前Pause（StartAliveTask），处理完成后再Resume（StopAliveTask）)</param>
        /// <param name="intervalTime">ms</param>
        /// <param name="yourAction">delegateGetAliveTaskDataTableInfoEventHandler</param>
        /// <returns>is sucess</returns>
        public bool CreateNewAliveTask(string yourTaskName, String sqlcmd, int intervalTime, delegateGetAliveTaskDataTableInfoEventHandler yourAction)
        {
            AliveTaskInfo tempTaskInfo = new AliveTaskInfo(yourTaskName, sqlcmd, intervalTime, this);
            tempTaskInfo.OnGetAliveTaskDataTableInfo += yourAction;
            if (AddAliveTaskInfo(yourTaskName, tempTaskInfo))
            {
                tempTaskInfo.CreateAliveTaskThread();
                return true;
            }
            else
            {
                tempTaskInfo.Dispose();
                tempTaskInfo = null;
                return false;
            }
        }

        /// <summary>
        /// get Alive TaskList
        /// </summary>
        public string[] AliveTaskList
        {
            get
            {
                if (aliveTaskList.Count > 0) // aliveTaskList will never null
                {
                    return (aliveTaskList.Keys).ToArray<string>();
                }
                else
                {
                    return null;
                }

            }
        }

        /// <summary>
        /// you can update the sql cmd by Task name
        /// </summary>
        /// <param name="yourName">Task name</param>
        /// <param name="sqlcmd">sqlcmd</param>
        /// <returns>is succse</returns>
        public bool UpdateAliveTaskSqlCmd(string yourName, String sqlcmd)
        {
            if (aliveTaskList.ContainsKey(yourName))
            {
                aliveTaskList[yourName].TaskSqlcmd = sqlcmd;
                return true;
            }
            else
            {
                SetErrorMes("not find this task name : " + yourName);
                return false;
            }
        }

        /// <summary>
        /// Start Task（这里的Start/Stop实际上是指恢复暂停任务线程的意思）
        /// </summary>
        /// <param name="yourName">Task name</param>
        /// <returns>is succse</returns>
        public bool StartAliveTask(string yourName)
        {
            if (aliveTaskList.ContainsKey(yourName))
            {
                aliveTaskList[yourName].ResumeAliveTask();
                return true;
            }
            else
            {
                SetErrorMes("not find this task name : " + yourName);
                return false;
            }
        }

        /// <summary>
        /// Stop Task （这里的Start/Stop实际上是指恢复暂停任务线程的意思）
        /// </summary>
        /// <param name="yourName">task name</param>
        /// <returns>is success</returns>
        public bool StopAliveTask(string yourName)
        {
            if (aliveTaskList.ContainsKey(yourName))
            {
                aliveTaskList[yourName].PauseAliveTask();
                return true;
            }
            else
            {
                SetErrorMes("not find this task name : " + yourName);
                return false;
            }
        }

        /// <summary>
        /// remove a AliveTask by his name ( it will remove the list immediately and the Thread will over but not immediately)
        /// </summary>
        /// <param name="yourName">task name</param>
        /// <returns>is succes</returns>
        public bool DelAliveTask(string yourName)
        {
            if (aliveTaskList.ContainsKey(yourName))
            {
                //GC.SuppressFinalize(aliveTaskList[yourName]); //only for GC test
                aliveTaskList[yourName].StopAliveTask();
                aliveTaskList[yourName].Dispose();
                aliveTaskList[yourName] = null;
                aliveTaskList.Remove(yourName);
                return true;
            }
            else
            {
                SetErrorMes("not find this task name");
                return false;
            }
        }
        #endregion

        #region MonitorTask
        /// <summary>
        /// Create New MonitorTask [it will not start ]
        /// </summary>
        /// <param name="yourTaskName">Task Name</param>
        /// <param name="sqlcmd"></param>
        /// <param name="intervalTime">ms</param>
        /// <param name="yourAction"></param>
        /// <returns></returns>
        public bool CreateNewMonitorTask(string yourTaskName, String sqlcmd, int monitorRowIndex, int monitorColumnIndex, int intervalTime, delegateGetMonitorTaskDataTableInfoEventHandler yourAction)
        {
            SqlMonitor tempTaskInfo = new SqlMonitor(yourTaskName, sqlcmd,monitorRowIndex,monitorColumnIndex, intervalTime, this);
            tempTaskInfo.OnGetMonitorTaskDataTableInfo += yourAction;
            if (AddAliveMonitorInfo(yourTaskName, tempTaskInfo))
            {
                tempTaskInfo.CreateAliveTaskThread();
                return true;
            }
            else
            {
                tempTaskInfo.Dispose();
                tempTaskInfo = null;
                return false;
            }
        }

        /// <summary>
        /// get Monitor TaskList
        /// </summary>
        public string[] MonitorTaskList
        {
            get
            {
                if (aliveMonitorList.Count > 0) // aliveTaskList will never null
                {
                    return (aliveMonitorList.Keys).ToArray<string>();
                }
                else
                {
                    return null;
                }

            }
        }

        /// <summary>
        /// you can update the sql cmd by Task name
        /// </summary>
        /// <param name="yourName">Task name</param>
        /// <param name="sqlcmd">sqlcmd</param>
        /// <returns>is succse</returns>
        public bool UpdateMonitorTaskSqlCmd(string yourName, String sqlcmd, int monitorRowIndex, int monitorColumnIndex)
        {
            if (aliveMonitorList.ContainsKey(yourName))
            {
                aliveMonitorList[yourName].TaskSqlcmd = sqlcmd;
                aliveMonitorList[yourName].MonitorRowIndex = monitorRowIndex;
                aliveMonitorList[yourName].MonitorRowIndex = monitorRowIndex;
                return true;
            }
            else
            {
                SetErrorMes("not find this task name : " + yourName);
                return false;
            }
        }

        /// <summary>
        /// Start Monitor（这里的Start/Stop实际上是指恢复暂停任务线程的意思）
        /// </summary>
        /// <param name="yourName">Task name</param>
        /// <returns>is succse</returns>
        public bool StartMonitorTask(string yourName)
        {
            if (aliveMonitorList.ContainsKey(yourName))
            {
                aliveMonitorList[yourName].ResumeAliveTask();
                return true;
            }
            else
            {
                SetErrorMes("not find this task name : " + yourName);
                return false;
            }
        }

        /// <summary>
        /// Stop Monitor （这里的Start/Stop实际上是指恢复暂停任务线程的意思）
        /// </summary>
        /// <param name="yourName">task name</param>
        /// <returns>is success</returns>
        public bool StopMonitorTask(string yourName)
        {
            if (aliveMonitorList.ContainsKey(yourName))
            {
                aliveMonitorList[yourName].PauseAliveTask();
                return true;
            }
            else
            {
                SetErrorMes("not find this task name : " + yourName);
                return false;
            }
        }

        /// <summary>
        /// remove a MonitorTask by his name ( it will remove the list immediately and the Thread will over but not immediately)
        /// </summary>
        /// <param name="yourName">task name</param>
        /// <returns>is succes</returns>
        public bool DelMonitorTask(string yourName)
        {
            if (aliveMonitorList.ContainsKey(yourName))
            {
                //GC.SuppressFinalize(aliveTaskList[yourName]); //only for GC test
                aliveMonitorList[yourName].StopAliveTask();
                aliveMonitorList[yourName].Dispose();
                aliveMonitorList[yourName] = null;
                aliveMonitorList.Remove(yourName);
                return true;
            }
            else
            {
                SetErrorMes("not find this task name");
                return false;
            }
        }
        #endregion

        /// <summary>
        /// execute a query command with sql 
        /// </summary>
        /// <param name="sql">your sql commod</param>
        /// <returns>the result with DataTable(it will be null when find error)</returns>
        public DataTable ExecuteQuery(String sql)
        {
            DataTable myTable;
            myAutoResetEvent.WaitOne();
            if (!ConnectDataBase())
            {
                SetErrorMes("can not connect MySqlDrive");
                myAutoResetEvent.Set();
                return null;
            }
            try
            {
                myCommand = myConnection.CreateCommand();
                myCommand.CommandText = sql;
                myAdapter = new MySqlDataAdapter(myCommand);
                //DataSet mySet = new DataSet();
                //myAdapter.Fill(mySet, "selectDa");
                //myTable = mySet.Tables["selectDa"];
                myTable = new DataTable();
                myAdapter.Fill(myTable);
            }
            catch (Exception ex)
            {
                SetErrorMes(ex.Message);
                myTable = null;
            }
            myAutoResetEvent.Set();
            return myTable;
        }

        /// <summary>
        /// execute a query command with sql in MySqlParameter mode (如果暴露该重载会导致使用该方法必须引入MySqlParameter，即使不使用需要MySqlParameter参数的重载版本)
        /// </summary>
        /// <param name="sql">your sql commod</param>
        /// <param name="yourParameter">MySqlParameter</param>
        /// <returns>the result with DataTable</returns>
        private DataTable ExecuteQuery(String sql, MySqlParameter[] yourParameter)
        {
            DataTable myTable;
            myAutoResetEvent.WaitOne();
            if (!ConnectDataBase())
            {
                SetErrorMes("can not connect MySqlDrive");
                myTable = null;
            }
            try
            {
                myCommand = myConnection.CreateCommand();
                myCommand.CommandText = sql;
                if (yourParameter != null)
                {
                    foreach (MySqlParameter tempParameter in yourParameter)
                    {
                        myCommand.Parameters.Add(tempParameter);
                    }
                }
                myAdapter = new MySqlDataAdapter(myCommand);
                DataSet mySet = new DataSet();
                myAdapter.Fill(mySet, "selectDa");
                myTable = mySet.Tables["selectDa"];
                myAutoResetEvent.Set();
                return myTable;
            }
            catch (Exception ex)
            {
                SetErrorMes(ex.Message);
                myTable = null;
            }
            myAutoResetEvent.Set();
            return myTable;
        }

        private DataTable ExecuteQuery(String sql, MySqlParameter yourParameter)
        {
            return ExecuteQuery(sql, new MySqlParameter[] { yourParameter });
        }

        /// </summary>
        /// execute a query command with sql in MySqlParameter mode
        /// </summary>
        /// <param name="sql">your sql commod  ( eg : ExecuteQuery("select * from h_order where seller_id=?id and order_amount>?amt limit 10", new Dictionary<string, string> { { "?id", "562" },{ "?amt", "100" } });)</param>
        /// <param name="yourParameterStrings">MySqlParameter with name and value (like { { "?id", "562" },{ "?amt", "100" } })</param>
        /// <returns>the result with DataTable</returns>
        public DataTable ExecuteQuery(String sql, Dictionary<string, string> yourParameterStrings)
        {
            List<MySqlParameter> myParameter = new List<MySqlParameter>();
            foreach (var ps in yourParameterStrings)
            {
                myParameter.Add(new MySqlParameter(ps.Key, ps.Value));
            }
            return ExecuteQuery(sql, myParameter.ToArray());
        }

        /// <summary>
        /// execute a query command with sql with rowIndex and columnIndex
        /// </summary>
        /// <param name="sql">your sql commod</param>
        /// <param name="rowIndex">start with 0</param>
        /// <param name="columnIndex">start with 0</param>
        /// <returns>the result with string vaule</returns>
        public string ExecuteQuery(String sql, int rowIndex, int columnIndex)
        {
            DataTable tempTable = ExecuteQuery(sql);
            if (tempTable==null)
            {
                return null;
            }
            if (tempTable.Rows.Count <= rowIndex || tempTable.Columns.Count <= columnIndex)
            {
                return null;
            }
            return tempTable.Rows[rowIndex][columnIndex].ToString();
        }

        public string ExecuteQuery(String sql, int rowIndex, string columName)
        {
            DataTable tempTable = ExecuteQuery(sql);
            if (tempTable == null)
            {
                return null;
            }
            if (tempTable.Rows.Count <= rowIndex ||!tempTable.Columns.Contains(columName))
            {
                return null;
            }
            return tempTable.Rows[rowIndex][columName].ToString();
        }

        /// <summary>
        /// get data if it is change in the time (阻塞的方法)
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="getTimes"></param>
        /// <param name="eachTimeInterval"></param>
        /// <param name="changedData">out change data (if the return is true you can use it)</param>
        /// <returns>it will be ture when the data change </returns>
        public bool GetChangeDataSync(String sql, int rowIndex, string columnIndex,int getTimes ,int eachTimeInterval,out string changedData)
        {
            changedData = null;
            if(getTimes<1)
            {
                return false;
            }
            string lastValue = ExecuteQuery(sql, rowIndex, columnIndex);
            string nowValue = null;
            for (int i = 0; i < getTimes;i++ )
            {
                nowValue = ExecuteQuery(sql, rowIndex, columnIndex);
                if (lastValue != nowValue)
                {
                    changedData = nowValue;
                    return true;
                }
                Thread.Sleep(eachTimeInterval);
            }
            return false;
        }

        /// <summary>
        /// only call it when you want drop it 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (myConnection != null)
            {
                myConnection.Close();
                myConnection.Dispose();
                if (myCommand!=null)
                {
                    myCommand.Dispose();
                }
                if(myAdapter!=null)
                {
                    myAdapter.Dispose();
                }
                if (myTransaction != null)
                {
                    myTransaction.Dispose(); //myTransaction not use here it is null
                }
                if (myTable != null)
                {
                    myTable.Dispose();
                }

            }
        }

        //该类实际没有直接引入非托管资源，是不应该添加析构函数的，之后可能会有，先标记在这里
        //静态资源也不用手动回收，只能进程结束时释放(所以静态类的析构函数是不可能会执行到的)
        ~MySqlDrive()
        {
            Dispose(false);
        }

        #endregion

    }
}
