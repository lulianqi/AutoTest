using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyCommonHelper.FileHelper
{
    /// <summary>
    /// put in error message in file
    /// </summary>
    [System.Runtime.Remoting.Contexts.Synchronization] //将该属性应用于某个对象时，在共享该属性实例的所有上下文中只能执行一个线程。
    public class ErrorLog
    {
        #region 内部成员
        private static string CurrentProcessPath = Environment.CurrentDirectory;
        //private static string CurrentProcessPath2 = Directory.GetCurrentDirectory();
        //private static string CurrentProcessPath3 = AppDomain.CurrentDomain.BaseDirectory;
        private static string FilePath = CurrentProcessPath + "\\log\\" + DateTime.Now.ToString("yyyy.MM.dd") + ".txt";       //log path
        private static bool isStart = false;
        private static string logNewLineFlag = "-------------------------------------------------------";
        private static bool isUsing = false;                                                                                                            //this log path may using where you call him
        private static FileStream fs;
        private static StreamWriter sw;
        private static List<string> unHandleLogs = new List<string>();                                                                                  //this log you not deal with
        #endregion

        static ErrorLog()
        {
            if (!Directory.Exists(CurrentProcessPath + "\\log"))
            {
                Directory.CreateDirectory(CurrentProcessPath + "\\log");
            }
            Start();
        }

        /// <summary>
        /// 获取或设置一个值，表示当前日志是否启动
        /// </summary>
        public static bool IsStart
        {
            get { return isStart; }
            set
            {
                if (value)
                {
                    Start();
                }
                else
                {
                    Stop();
                }
            }
        }

        /// <summary>
        /// 启动日志
        /// </summary>
        public static void Start()
        {
            if (!isStart)
            {
                fs = new FileStream(FilePath, File.Exists(FilePath) ? FileMode.Append : FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
                isStart = true;
            }
        }

        /// <summary>
        /// 关闭日志
        /// </summary>
        public static void Stop()
        {
            if (isStart)
            {
                CloseLog();
                sw.Close();
                fs.Close();
                isStart = false;
            }
        }

        /// <summary>
        /// get now line
        /// </summary>
        /// <returns>line</returns>
        public static int GetLineNum(int skipFrames)
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(skipFrames, true);
            return st.GetFrame(0).GetFileLineNumber();
        }

        /// <summary>
        /// get now file
        /// </summary>
        /// <returns>file</returns>
        public static string GetCurSourceFileName(int skipFrames)
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(skipFrames, true);
            return st.GetFrame(0).GetFileName();
        }

        /// <summary>
        /// here i will save the log not handle 
        /// </summary>
        private static void SavaUnHandleLogs()
        {
            if (!isStart)
            {
                return;
            }
            if (unHandleLogs.Count > 0)
            {
                if (isUsing == false)
                {
                    isUsing = true;
                    foreach (string tempLog in unHandleLogs)
                    {
                        sw.WriteLine(logNewLineFlag);
                        sw.WriteLine(tempLog);
                    }
                    sw.Flush();
                    unHandleLogs.Clear();
                    isUsing = false;
                }
            }
        }


        /// <summary>
        /// i will pu int log in the file with any other message 
        /// </summary>
        /// <param name="errorMessage"> your message</param>
        public static void PutInLog(string errorMessage)
        {
            if (!isStart)
            {
                return;
            }
            SavaUnHandleLogs();
            if (isUsing == false)
            {
                isUsing = true;
                sw.WriteLine(logNewLineFlag);
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine(string.Format("ErrorFile:{0}\r\nErrorLine:{1}\r\n", GetCurSourceFileName(2), GetLineNum(2)));
                sw.WriteLine(errorMessage);//开始写入值
                sw.Flush();
                isUsing = false;
            }
            else
            {
                unHandleLogs.Add(errorMessage);
            }

        }

        public static void PutInLog(Exception errorException)
        {
            if (!isStart)
            {
                return;
            }
            SavaUnHandleLogs();
            if (isUsing == false)
            {
                isUsing = true;
                sw.WriteLine(logNewLineFlag);
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine(errorException.Message);
                sw.WriteLine(errorException.StackTrace);
                sw.Flush();
                isUsing = false;
            }
            else
            {
                unHandleLogs.Add(string.Format("{0}/r/n{1}/r/n{2}/r/n", DateTime.Now.ToString(), errorException.Message, errorException.StackTrace));
            }

        }


        /// <summary>
        /// when you close your application you can call me to deal with the log you not put in the file 
        /// </summary>
        public static void CloseLog()
        {
            SavaUnHandleLogs();
            Stop();
        }
    }
}
