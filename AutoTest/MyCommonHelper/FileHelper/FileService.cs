using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyCommonHelper.FileHelper
{
    public class FileService
    {

        private static string CurrentProcessPath = Environment.CurrentDirectory;
        /* eg:
               FileStream fs = new FileStream(FilePathEx, File.Exists(FilePathEx) ? FileMode.Append : FileMode.Create, FileAccess.Write);
               StreamWriter sw = new StreamWriter(fs);
               swex.WriteLine("++++++++++++++++++++");
               sw.Close();
               fs.Close();
        * */
        // or  File.WriteAllBytes(saveFileName, bytesToSave);  or WriteAllText()


        /// <summary>
        /// 根据文件名返回文件路径（容错上可能受相对路径影响，未测试请勿使用可能出现错误的路径格式）
        /// </summary>
        /// <param name="yourPath">文件路径</param>
        /// <returns>文件夹路径</returns>
        public static string GetDirectory(string yourPath)
        {
            FileInfo fi = new FileInfo(yourPath);
            DirectoryInfo di = fi.Directory;
            return di.FullName;
        }

        /// <summary>
        /// 保存数据到文件【string】
        /// </summary>
        /// <param name="yourDate">需要保存的数据</param>
        /// <param name="yourPath">保存路径，如果为null即为默认路径</param>
        /// <param name="isAdd">是否使用追加模式</param>
        /// <param name="isAsLog">是否写入保存时间</param>
        /// <returns></returns>
        public static bool SaveFile(string yourDate, string yourPath, bool isAdd, bool isAsLog)
        {
            FileStream fs;
            if (yourPath == null)
            {
                yourPath = CurrentProcessPath + "\\unknowLog\\" + DateTime.Now.ToString("yyyy.MM.dd") + ".txt";
                if (!Directory.Exists(CurrentProcessPath + "\\unknowLog\\"))
                {
                    Directory.CreateDirectory(CurrentProcessPath + "\\unknowLog\\");
                }
            }
            if (yourDate == null)
            {
                return false;
            }
            if (File.Exists(yourPath))
            {
                if (isAdd)
                {
                    fs = new FileStream(yourPath, FileMode.Append, FileAccess.Write);
                }
                else
                {
                    for (int i = 0; i < 2000; i++)
                    {
                        if (!File.Exists(yourPath + ".bak" + i))
                        {
                            Directory.Move(yourPath, yourPath + ".bak" + i);
                            break;
                        }
                    }
                    fs = new FileStream(yourPath, FileMode.Create, FileAccess.Write);
                }
            }
            else
            {
                fs = new FileStream(yourPath, FileMode.Create, FileAccess.Write);
            }
            StreamWriter sw = new StreamWriter(fs);
            if (isAsLog)
            {
                sw.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            }
            sw.Write(yourDate);
            sw.Close();
            return true;
        }

        public static bool SaveFile(string yourDate, string yourPath)
        {
            return SaveFile(yourDate, yourPath, false, false);
        }

        /// <summary>
        /// 保存数据到文件【byte[]】
        /// </summary>
        /// <param name="yourDate"></param>
        /// <param name="yourPath"></param>
        /// <returns></returns>
        public static bool SaveFile(byte[] yourDate, string yourPath)
        {
            if (File.Exists(yourPath))
            {
                for (int i = 0; i < 2000; i++)
                {
                    if (!File.Exists(yourPath + ".bak" + i))
                    {
                        Directory.Move(yourPath, yourPath + ".bak" + i);
                        break;
                    }
                }
            }
            FileStream fs = new FileStream(yourPath, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(yourDate);
            sw.Close();
            return true;
        }

        /// <summary>
        /// 使用追加的方式保存文件【string[]】
        /// </summary>
        /// <param name="DataRecordLines">要保存的数据</param>
        /// <param name="lineLen">保存的数量，0表示全部保存</param>
        /// <param name="yourPath">保存的路径，null表示使用默认路径</param>
        private void SaveDataRecord(string[] DataRecordLines, int lineLen, string yourPath)
        {
            FileStream fs;
            if (yourPath == null)
            {
                yourPath = CurrentProcessPath + "\\unknowLog\\" + DateTime.Now.ToString("yyyy.MM.dd") + ".txt";
            }
            if (File.Exists(yourPath))
            {
                fs = new FileStream(yourPath, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(yourPath, FileMode.Create, FileAccess.Write);
            }
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            sw.WriteLine(DateTime.Now.ToString());
            sw.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            if (lineLen > DataRecordLines.Length || lineLen <= 0)
            {
                foreach (string tempData in DataRecordLines)
                {
                    sw.WriteLine(tempData);
                }
            }
            else
            {
                for (int i = 0; i < lineLen; i++)
                {
                    sw.WriteLine(DataRecordLines[i]);
                }
            }
            sw.Close();
        }

        public static bool SaveLogFile(string yourDate, string yourPath)
        {
            return SaveFile(yourDate, yourPath, true, true);
        }

        public static void CreateDirectory(string yourPath)
        {
            if (!Directory.Exists(yourPath))
            {
                Directory.CreateDirectory(yourPath);
            }
        }

        /// <summary>
        /// get all file in your path
        /// </summary>
        /// <param name="yourPath">your path</param>
        /// <returns>all file in path</returns>
        public static FileInfo[] GetAllFiles(string yourPath)
        {

            if (!Directory.Exists(yourPath))
            {
                return null;
            }
            DirectoryInfo theFolder = new DirectoryInfo(yourPath);
            return theFolder.GetFiles("*", SearchOption.AllDirectories);
        }

        public static void GetMyFiles(DirectoryInfo yourDirectory, out DirectoryInfo[] outDirectoryInfos, out FileInfo[] outFileInfos)
        {
            outDirectoryInfos = yourDirectory.GetDirectories();
            outFileInfos = yourDirectory.GetFiles();
        }
    }
}
