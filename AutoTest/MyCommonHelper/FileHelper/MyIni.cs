using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

/// <summary>
/// read and write ini file
/// </summary>
namespace MyCommonHelper.FileHelper
{
    public class MyIni
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static void IniWriteValue(string Section, string Key, string Value, string filepath)//对ini文件进行写操作的函数
        {
            try
            {
                WritePrivateProfileString(Section, Key, Value, filepath);
            }
            catch (Exception ex)
            {
                ErrorLog.PutInLog("ID:D314  " + ex.Message);
            }
        }

        public static string IniReadValue(string Section, string Key, string filepath)//对ini文件进行读操作的函数
        {
            StringBuilder temp = new StringBuilder(255);
            try
            {
                int i = GetPrivateProfileString(Section, Key, "", temp, 255, filepath);
            }
            catch (Exception ex)
            {
                ErrorLog.PutInLog("ID:D327  " + ex.Message);
            }
            return temp.ToString();
        }

    }
}
