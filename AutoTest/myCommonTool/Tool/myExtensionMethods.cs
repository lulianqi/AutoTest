using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Forms;


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


namespace MyCommonTool
{
    public static class myExtensionMethods
    {
        /// <summary>
        /// 获取文本url编码值
        /// </summary>
        /// <param name="tb">文本框</param>
        /// <returns>文本url编码值</returns>
        public static string myText(this TextBox tb)
        {
            return System.Web.HttpUtility.UrlEncode(tb.Text);
        }

        /// <summary>
        /// 以xml的数据要求格式化string中的特殊字符（null时返回""）
        /// </summary>
        /// <param name="str">String</param>
        /// <returns>格式化过后的字符串</returns>
        public static string ToXmlValue(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return System.Web.HttpUtility.HtmlEncode(str);
        }

        /// <summary>
        /// 获取String文本，未null时返回NULL
        /// </summary>
        /// <param name="str">String</param>
        /// <returns>String文本</returns>
        public static string MyValue(this string str)
        {
            if(str==null)
            {
                return "NULL";
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// 向str中添加信息
        /// </summary>
        /// <param name="str">string</param>
        /// <param name="yourValue">your Value</param>
        /// <returns>添加后的结果</returns>
        public static string MyAddValue(this string str,string yourValue)
        {
            if (str == null)
            {
                return yourValue;
            }
            else if(str =="")
            {
                return str = yourValue;
            }
            else
            {
                return str + ("\r\n" + yourValue);
            }
        }

        /// <summary>
        /// 添加键值，若遇到已有key则覆盖
        /// </summary>
        /// <param name="dc">Dictionary</param>
        /// <param name="yourKeyValuePair">KeyValuePair</param>
        public static void MyAdd(this Dictionary<string, string> dc,  KeyValuePair<string, string> yourKeyValuePair)
        {
            if(dc.ContainsKey(yourKeyValuePair.Key))
            {
                dc[yourKeyValuePair.Key] = yourKeyValuePair.Value;
            }
            else
            {
                dc.Add(yourKeyValuePair.Key, yourKeyValuePair.Value);
            }
        }

        /// <summary>
        /// 去除首尾指定字符串
        /// </summary>
        /// <param name="str">指定字符串</param>
        /// <param name="startValue">首部匹配（如果为null则忽略首部匹配）</param>
        /// <param name="endVaule">尾部匹配（如果为null则忽略尾部匹配）</param>
        /// <returns>返回结果</returns>
        public static string MyTrimStr(this string str, string startValue, string endVaule)
        {
            if (str != null)
            {
                if (startValue != null)
                {
                    if (startValue.Length <= str.Length)
                    {
                        int tempTrimStartIndex = str.IndexOf(startValue);
                        if (tempTrimStartIndex == 0)
                        {
                            str = str.Remove(0, startValue.Length);
                        }
                    }
                }
                if (endVaule != null)
                {
                    if (endVaule.Length <= str.Length)
                    {
                        int tempTrimEndIndex = str.LastIndexOf(endVaule);
                        if (tempTrimEndIndex == str.Length - endVaule.Length)
                        {
                            str = str.Remove(tempTrimEndIndex, endVaule.Length);
                        }
                    }
                }
            }
            return str;
        }

        /// <summary>
        /// 添加键值，若遇到已有key则覆盖
        /// </summary>
        /// <param name="dc">Dictionary</param>
        /// <param name="yourKey">Key</param>
        /// <param name="yourValue">Value</param>
        public static void myAdd(this Dictionary<string, string> dc, string yourKey,string yourValue)
        {
            if (dc.ContainsKey(yourKey))
            {
                dc[yourKey] = yourValue;
            }
            else
            {
                dc.Add(yourKey, yourValue);
            }
        }

        /// <summary>
        /// 【Dictionary<string, string>】添加键值，若遇到已有key则将Key改名(追加索引)
        /// </summary>
        /// <param name="dc">Dictionary</param>
        /// <param name="yourKey">Key</param>
        /// <param name="yourValue">Value</param>
        public static void MyAddEx(this Dictionary<string, string> dc, string yourKey, string yourValue)
        {
            if (dc.ContainsKey(yourKey))
            {
                int tempSameKeyIndex = 0;
                while (dc.ContainsKey(yourKey + "(" + tempSameKeyIndex + ")"))
                {
                    tempSameKeyIndex++;
                }
                dc.Add(yourKey + "(" + tempSameKeyIndex + ")", yourValue);
            }
            else
            {
                dc.Add(yourKey, yourValue);
            }
        }

        /// <summary>
        /// 【NameValueCollection】添加键值，若遇到已有key则将Key改名(追加索引)
        /// </summary>
        /// <param name="dc">NameValueCollection</param>
        /// <param name="yourKey">Key</param>
        /// <param name="yourValue">Value</param>
        public static void MyAddEx(this NameValueCollection dc, string yourKey, string yourValue)
        {
            if (dc.AllKeys.Contains<string>(yourKey))
            {
                int tempSameKeyIndex = 0;
                while (dc.AllKeys.Contains<string>(yourKey + "(" + tempSameKeyIndex + ")"))
                {
                    tempSameKeyIndex++;
                }
                dc.Add(yourKey + "(" + tempSameKeyIndex + ")", yourValue);
            }
            else
            {
                dc.Add(yourKey, yourValue);
            }
        }

        /// <summary>
        /// 【NameValueCollection】添加键值，检查NameValueCollection是否为null
        /// </summary>
        /// <param name="nvc">NameValueCollection</param>
        /// <param name="yourKey">Key</param>
        /// <param name="yourValue">Value</param>
        public static void myAdd(this NameValueCollection nvc, string yourName, string yourValue)
        {
           if(nvc!=null)
           {
               nvc.Add(yourName, yourValue);
           }
        }

        public static string MyToString(this NameValueCollection nvc)
        {
            if (nvc != null)
            {
                if(nvc.Count>0)
                {
                    if(nvc.Count<2)
                    {
                        return string.Format("{{ [{0}:{1}] }}", nvc.Keys[0], nvc[nvc.Keys[0]]);
                    }
                    else
                    {
                        StringBuilder tempStrBld=new StringBuilder("{ ");
                        foreach(string tempKey in nvc.Keys)
                        {
                            tempStrBld.AppendFormat("[{0}:{1}] ", tempKey, nvc[tempKey]);
                        }
                        tempStrBld.Append("}");
                        return tempStrBld.ToString();
                    }
                }
            }
            return "";
        }

        /*
        /// <summary>
        /// 添加键值，若遇到已有key则覆盖
        /// </summary>
        /// <param name="dc">Dictionary</param>
        /// <param name="yourKey">Key</param>
        /// <param name="yourValue">Value</param>
        public static void myAdd(this Dictionary<string, ICaseExecutionDevice> dc, string yourKey, ICaseExecutionDevice yourValue)
        {
            if (dc.ContainsKey(yourKey))
            {
                dc[yourKey] = yourValue;
            }
            else
            {
                dc.Add(yourKey, yourValue);
            }
        }
         * */

        /*
        /// <summary>
        /// 添加键值，若遇到已有key则覆盖
        /// </summary>
        /// <param name="dc">Dictionary</param>
        /// <param name="yourKey">Key</param>
        /// <param name="yourValue">Value</param>
        public static void myAdd(this Dictionary<string, IRunTimeStaticData> dc, string yourKey, IRunTimeStaticData yourValue)
        {
            if (dc.ContainsKey(yourKey))
            {
                dc[yourKey] = yourValue;
            }
            else
            {
                dc.Add(yourKey, yourValue);
            }
        }
         * */

        /// <summary>
        /// 添加键值，若遇到已有则不添加
        /// </summary>
        /// <param name="myArratList">ArratList</param>
        /// <param name="yourIp">IPAddress</param>
        public static void MyAdd(this System.Collections.ArrayList myArratList,  System.Net.IPAddress yourIp)
        {
            if (!myArratList.Contains(yourIp))
            {
                myArratList.Add(yourIp);
            }
        }

        /// <summary>
        /// 摘取List数组指定列重新生成数组（超出索引返回null）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="myArratList"></param>
        /// <param name="yourIndex">指定列</param>
        /// <returns>重新生成的数组</returns>
        public static T[] MyGetAppointArray<T>(this List<T[]> myArratList, int yourIndex)
        {
            if (myArratList != null && yourIndex > -1)
            {
                try
                {
                    int myArLong = myArratList.Count;
                    T[] myTAr = new T[myArLong];
                    for (int i = 0; i < myArLong; i++)
                    {
                        myTAr[i] = myArratList[i][yourIndex];
                    }
                    return myTAr;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 返回对象的深度克隆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dc"></param>
        /// <returns>对象的深度克隆</returns>
        public static Dictionary<string, T> CloneEx<T>(this Dictionary<string, T> dc) where T : ICloneable
        {
            Dictionary<string, T> cloneDc = new Dictionary<string, T>();
            foreach (KeyValuePair<string, T> tempKvp in dc)
            {
                cloneDc.Add(tempKvp.Key, (T)tempKvp.Value.Clone());
            }
            return cloneDc;
        }

        /// <summary>
        /// 返回对象的浅度克隆(如果T为引用对象，浅度克隆将只复制其对象的引用)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="souceList"></param>
        /// <returns>对象的浅度克隆</returns>
        public static List<T> LightClone<T>(this List<T> souceList)
        {
            return new List<T>(souceList.ToArray());
        }

    }
}
