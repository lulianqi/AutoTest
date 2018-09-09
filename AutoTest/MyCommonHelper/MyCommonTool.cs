using MyCommonHelper.FileHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace MyCommonHelper
{

    public class MyCommonTool
    {

        /// <summary>
        /// seed for GenerateRandomStr
        /// </summary>
        private static int externRandomSeed=0;


        public static string GenerateRandomNum(int codeCount)
        {
            externRandomSeed++;
            string myRandomStr = string.Empty;
            long mySeed = DateTime.Now.Ticks + externRandomSeed;
            Random random = new Random((int)(mySeed & 0x0000ffff));
            for (int i = 0; i < codeCount; i++)
            {
                int num = random.Next();
                myRandomStr = myRandomStr + ((char)(0x30 + ((ushort)(num % 10)))).ToString();
            }
            return myRandomStr;
        }

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="strCount">字符串长度</param>
        /// <param name="GenerateType">生成模式： 0-是有可见ASCII / 1-数字 / 2-大写字母 / 3-小写字母 / 4-特殊字符 / 5-大小写字母 / 6-字母和数字</param>
        /// <returns>随机字符串</returns>
        public static string GenerateRandomStr(int strCount, int GenerateType)
        {
            externRandomSeed++;
            StringBuilder myRandomStr = new StringBuilder(strCount);
            long mySeed = DateTime.Now.Ticks + externRandomSeed;
            Random random = new Random((int)(mySeed & 0x0000ffff));
            for (int i = 0; i < strCount; i++)
            {
                char tempCh;
                int num = random.Next();
                switch (GenerateType)
                {
                    case 1:
                        tempCh = (char)(0x30 + (num % 10));
                        break;
                    case 2:
                        tempCh = (char)(0x41 + (num % 26));
                        break;
                    case 3:
                        tempCh = (char)(0x61 + (num % 26));
                        break;
                    case 4:
                        int tempValue = 0x20 + ((num % 95));
                        if ((tempValue>=0x30&&tempValue<=0x39)||(tempValue>=0x41&&tempValue<=0x5a)||(tempValue>=0x61&&tempValue<=0x7a))
                        {
                            i--;
                            continue;
                        }
                        else
                        {
                            tempCh = (char)tempValue;
                        }
                        break;
                    case 5:
                        tempValue = 0x20 + ((num % 95));
                        if ((tempValue >= 0x41 && tempValue <= 0x5a) || (tempValue >= 0x61 && tempValue <= 0x7a))
                        {
                            tempCh = (char)tempValue;
                        }
                        else
                        {
                            i--;
                            continue;
                        }
                        break;
                    case 6:
                        tempValue = 0x20 + ((num % 95));
                        if ((tempValue >= 0x30 && tempValue <= 0x39) || (tempValue >= 0x41 && tempValue <= 0x5a) || (tempValue >= 0x61 && tempValue <= 0x7a))
                        {
                            tempCh = (char)tempValue;
                        }
                        else
                        {
                            i--;
                            continue;
                        }
                        break;
                    default:
                        tempCh = (char)(0x20 + (num % 95));
                        break;
                }
                myRandomStr.Append(tempCh);
            }
            return myRandomStr.ToString();
        }

        /// <summary>
        /// 检查两组数组里面的值是否一致（用于数组中的数据都是唯一项，对于引用对象则比较引用地址）
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="yourSouceAr">源数组</param>
        /// <param name="yourTargetAr">目标数组</param>
        /// <param name="addAr">要添加的项（如果不一致）</param>
        /// <param name="delAr">要删除的项（如果不一致）</param>
        /// <returns></returns>
        public static  bool IsMyArrayIndexSame<T>(T[] yourSouceAr,T[]yourTargetAr,out List<T>addAr,out List<T>delAr)
        {
            return IsMyArrayIndexSame<T>(new List<T>(yourSouceAr), new List<T>(yourTargetAr), out addAr, out delAr);
        }

        /// <summary>
        /// 检查两组数组里面的值是否一致（用于数组中的数据都是唯一项，对于引用对象则比较引用地址）
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="yourSouceAr">源数组</param>
        /// <param name="yourTargetAr">目标数组</param>
        /// <param name="addAr">要添加的项（如果不一致）</param>
        /// <param name="delAr">要删除的项（如果不一致）</param>
        /// <returns></returns>
        public static bool IsMyArrayIndexSame<T>(List<T> yourSouceAr, List<T> yourTargetAr,out List<T> addAr,out List<T> delAr)
        {
            addAr = new List<T>();
            delAr = yourSouceAr.LightClone<T>();
            foreach (var tempVaule in yourTargetAr)
            {
                if (delAr.Contains(tempVaule))
                {
                    delAr.Remove(tempVaule);
                }
                else
                {
                    addAr.Add(tempVaule);
                }
            }
            if (addAr.Count == 0 && delAr.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 启动其他程序
        /// </summary>
        /// <param name="FileName">需要启动的外部程序名称</param>
        public static bool OpenPress(string FileName, string Arguments)
        {
            System.Diagnostics.Process pro = new System.Diagnostics.Process();
            if (System.IO.File.Exists(FileName))
            {
                pro.StartInfo.FileName = FileName;
                pro.StartInfo.Arguments = Arguments;
                try
                {
                    pro.Start();
                }
                catch(Exception ex)
                {
                    ErrorLog.PutInLog(ex);
                    return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// find the file name from filepath
        /// </summary>
        /// <param name="myPath"> file path</param>
        /// <returns>file name</returns>
        public static string findFileName(string myPath)
        {
            string tempFileName = myPath.Remove(0, myPath.LastIndexOf('\\') + 1);
            return tempFileName;
        }

        /// <summary>
        /// find the file Directory name from filepath
        /// </summary>
        /// <param name="myPath"> file path</param>
        /// <returns>file Directory</returns>
        public static string findFileDirectory(string myPath)
        {
            string tempFileName = myPath.Remove(myPath.LastIndexOf('\\'));
            return tempFileName;
        }

        /// <summary>
        /// 将xml字符串格式化
        /// </summary>
        /// <param name="originalString">原始字符串</param>
        /// <param name="formatString">格式化后的数据，或错误信息</param>
        /// <returns>是否成功</returns>
        public static bool FormatXmlString(string originalString,out string formatString)
        {
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Xml.XmlTextWriter xtw = new System.Xml.XmlTextWriter(sw);
            xtw.Formatting = System.Xml.Formatting.Indented;
            xtw.Indentation = 1;
            xtw.IndentChar = '\t';
            try
            {
                xmlDoc.LoadXml(originalString);
                xmlDoc.WriteTo(xtw);
                formatString = sw.ToString();
                return true;
            }
            catch(Exception ex)
            {
                formatString = ex.Message;
                return false;
            }
            finally
            {
                sw.Dispose();
            }

        }

        /// <summary>
        /// 将json字符串格式化
        /// </summary>
        /// <param name="originalString">原始字符串</param>
        /// <param name="formatString">格式化后的数据</param>
        /// <returns>是否成功（未判断格式是否合法，只要不为null都返回成功）</returns>
        public static bool FormatJsonString(string originalString,out string formatString)
        {
            // { [ 后面 换行，缩进+1
            // } ] 前面 缩进-1，换行
            // ,   后面 换行
            // ""  名字 或 string 值内含的 {} [] , 不处理
            // ""  名字 或 string 外的多余空格 去除  （ascii 0x20 以下的不可见字符包括换行回车全部去掉）
            // ""  内可能含有未转义过的引号，不能以此作为引号的结束
            //去除 unicode  C1 控制符处理 

            formatString = string.Empty;
            char jsonRetract = '\t';
            char[] jsonNewLine=new char[]{'\r','\n'};
            int nowRetractCount = 0;
            bool isInString = false;

            #region 仅会在函数内使用的匿名函数

            Func<char, bool> IsEndString = (dealChar) =>
                {
                    if (dealChar=='"')
                    { 

                    }
                    return false;
                };

            Func<char,int> GetNowRetractType = (dealChar) =>
                {
                    if (dealChar < 0x21 || (dealChar>127 && dealChar<162))
                    {
                        return -99;
                    }
                    switch (dealChar)
                    {
                        case '{':
                            return 1;
                            //break;
                        case '[':
                            return 1;
                            //break;
                        case '}':
                            return -1;
                            //break;
                        case ']':
                            return -1;
                            //break;
                        case ',':
                            return 0;
                            //break;
                        case '"':
                            return 2;
                        default:
                            return -101;
                            //break;
                    }
                };

            Func<int, char[]> GetNowRetractChars = (retractCount) =>
                {
                    if(retractCount<0)
                    { return null; }
                    char[] retractChars = new char[retractCount];
                    for(int i =0;i<retractCount;i++)
                    {
                        retractChars[i] = jsonRetract;
                    }
                    return retractChars;
                };
            #endregion
           
            if (originalString==null)
            {
                return false;
            }

            StringBuilder jsonSb = new StringBuilder(originalString);  //不应该直接使用Insert/Remove反复操作StringBuilder，性能明显若与使用新的StringBuilder拼接
            StringBuilder FormatJsonSb = new StringBuilder((int)(originalString.Length * 1.2));
            int retractTypeRusult = 0;;
            for (int i = 0; i < jsonSb.Length;i++ )
            {
                if (isInString)
                {
                    if (jsonSb[i] == '"')
                    {
                        if(jsonSb[i-1]!='\\')
                        {
                            isInString = false;
                        }
                    }
                    FormatJsonSb.Append(jsonSb[i]);
                }
                else
                {
                    retractTypeRusult = GetNowRetractType(jsonSb[i]);
                    if (retractTypeRusult > -100)
                    {
                        if (retractTypeRusult == -99)
                        {
                            //jsonSb.Remove(i, 1);
                            //i--;
                            continue;
                        }
                        else if (retractTypeRusult == 1)
                        {
                            nowRetractCount++;
                            //jsonSb.Insert(i + 1, jsonNewLine);
                            //i += jsonNewLine.Length;
                            //jsonSb.Insert(i + 1, GetNowRetractChars(nowRetractCount));
                            //i += nowRetractCount;
                            FormatJsonSb.Append(jsonSb[i]);
                            FormatJsonSb.Append(jsonNewLine);
                            FormatJsonSb.Append(GetNowRetractChars(nowRetractCount));
                            
                        }
                        else if (retractTypeRusult == 0)
                        {
                            //jsonSb.Insert(i + 1, jsonNewLine);
                            //i += jsonNewLine.Length;
                            //jsonSb.Insert(i + 1, GetNowRetractChars(nowRetractCount));
                            //i += nowRetractCount;
                            FormatJsonSb.Append(jsonSb[i]);
                            FormatJsonSb.Append(jsonNewLine);
                            FormatJsonSb.Append(GetNowRetractChars(nowRetractCount));
                            
                        }
                        else if (retractTypeRusult == -1)
                        {
                            nowRetractCount--;
                            //jsonSb.Insert(i, jsonNewLine);
                            //i += jsonNewLine.Length;
                            //jsonSb.Insert(i, GetNowRetractChars(nowRetractCount));
                            //i += nowRetractCount;
                            FormatJsonSb.Append(jsonNewLine);
                            FormatJsonSb.Append(GetNowRetractChars(nowRetractCount));
                            FormatJsonSb.Append(jsonSb[i]);
                        }
                        else if (retractTypeRusult == 2)
                        {
                            FormatJsonSb.Append(jsonSb[i]);
                            if (jsonSb[i - 1] != '\\')
                            {
                                isInString = true;
                            }
                        }
                    }
                    else
                    {
                        FormatJsonSb.Append(jsonSb[i]);
                    }
                }
            }


            //formatString = jsonSb.ToString();
            formatString = FormatJsonSb.ToString();
            return true;;
        }

        

    }

    public class StringHelper
    {
        /// <summary>
        /// Combination of string increase spaces
        /// </summary>
        /// <param name="strA">stringA</param>
        /// <param name="strB">stringB</param>
        /// <param name="num">spaces number </param>
        /// <returns></returns>
        public static string SameLenStringAdd(string strA, string strB, int num)
        {
            try
            {
                StringBuilder tempContent = new StringBuilder(strA);
                while (strA.Length < num)
                {
                    tempContent.Append(" ");
                    num--;
                }
                tempContent.Append(strB);
                return tempContent.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string EncodeXml(string strHtml)
        {
            if (string.IsNullOrEmpty(strHtml))
            {
                return "";
            }
            strHtml = strHtml.Replace("&", "&amp;");
            strHtml = strHtml.Replace("<", "&lt;");
            strHtml = strHtml.Replace(">", "&gt;");
            strHtml = strHtml.Replace("'", "&apos;");
            strHtml = strHtml.Replace("\"", "&quot;");
            return strHtml;
        }
        public static string EncodeXmlEx(string strHtml)
        {
            if (string.IsNullOrEmpty(strHtml))
            {
                return "";
            }
            return System.Web.HttpUtility.HtmlEncode(strHtml);
        }

        public static string StrListAdd(List<string> strList, string yourSplit)
        {
            StringBuilder myOurStb = new StringBuilder();
            if (strList != null)
            {
                for (int i = 0; i < strList.Count; i++)
                {
                    myOurStb.Append(strList[i]);
                    if (i != strList.Count - 1)
                    {
                        myOurStb.Append(yourSplit);
                    }
                }
            }
            return myOurStb.ToString();
        }

        public static string[] AddStringArr(string[] stringOne,string[]stringTow)
        {
            if (stringOne == null && stringTow==null)
            {
                return null;
            }
            else if(stringOne == null)
            {
                return stringTow;
            }
            else if (stringTow == null)
            {
                return stringOne;
            }
            else
            {
                string[] backStringArr = new string[stringOne.Length + stringTow.Length];
                //Array.Copy()
                stringOne.CopyTo(backStringArr, 0);
                stringTow.CopyTo(backStringArr, stringOne.Length);
                return backStringArr;
            }
        }
    }

}
