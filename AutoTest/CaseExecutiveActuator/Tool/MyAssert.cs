﻿using MyCommonHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CaseExecutiveActuator.Tool
{
    public static class MyAssert
    {
        /// <summary>
        /// Check Data with my mothod
        /// </summary>
        /// <param name="BackData">Back Data</param>
        /// <param name="ExpectData">Expect Data</param>
        /// <param name="method"> Check method</param>
        /// <returns>is pass </returns>
        public static bool CheckBackData(string BackData, string ExpectData, CaseExpectType method)
        {
            string trimA = BackData.Replace(" ", "");
            trimA = trimA.Replace("\r", "");
            trimA = trimA.Replace("\n", "");
            trimA = trimA.Replace("\t", "");
            string trimB = ExpectData.Replace(" ", "");
            trimB = trimB.Replace("\r", "");
            trimB = trimB.Replace("\n", "");
            trimB = trimB.Replace("\t", "");
            switch (method)
            {
                case CaseExpectType.judge_is:
                    {
                        if (trimB.Contains("[or]"))
                        {
                            string[] tempTrimBs = trimB.Split(new[] { "[or]" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string tempStr in tempTrimBs)
                            {
                                if (trimA == tempStr)
                                {
                                    return true; ;
                                }
                            }
                            return false;
                        }
                        return (trimA == trimB);
                    }
                //break;
                case CaseExpectType.judge_not:
                    return !(trimA == trimB);
                //break;
                case CaseExpectType.judge_like:
                    return false;
                //break;
                case CaseExpectType.judge_endwith:
                    {
                        if (trimB.Contains("[or]"))
                        {
                            string[] tempTrimBs = trimB.Split(new[] { "[or]" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string tempStr in tempTrimBs)
                            {
                                if (trimA.EndsWith(tempStr))
                                {
                                    return true; ;
                                }
                            }
                            return false;
                        }
                        return trimA.EndsWith(trimB);
                    }
                //break;
                case CaseExpectType.judge_startwith:
                    {
                        if (trimB.Contains("[or]"))
                        {
                            string[] tempTrimBs = trimB.Split(new[] { "[or]" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string tempStr in tempTrimBs)
                            {
                                if (trimA.StartsWith(tempStr))
                                {
                                    return true; ;
                                }
                            }
                            return false;
                        }
                        return trimA.StartsWith(trimB);
                    }
                //break;
                case CaseExpectType.judge_contain:
                    {
                        if (trimB.Contains("[and]"))
                        {
                            string[] tempTrimBs = trimB.Split(new[] { "[and]" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string tempStr in tempTrimBs)
                            {
                                if (!trimA.Contains(tempStr))
                                {
                                    return false;
                                }
                            }
                            return true;
                        }
                        else if (trimB.Contains("[or]"))
                        {
                            string[] tempTrimBs = trimB.Split(new[] { "[or]" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string tempStr in tempTrimBs)
                            {
                                if (trimA.Contains(tempStr))
                                {
                                    return true; ;
                                }
                            }
                            return false;
                        }
                        return trimA.Contains(trimB);
                    }
                //break;
                case CaseExpectType.judge_uncontain:
                    {
                        if (trimB.Contains("[and]"))
                        {
                            string[] tempTrimBs = trimB.Split(new[] { "[and]" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string tempStr in tempTrimBs)
                            {
                                if (trimA.Contains(tempStr))
                                {
                                    return false;
                                }
                            }
                        }
                        else if (trimB.Contains("[or]"))
                        {
                            string[] tempTrimBs = trimB.Split(new[] { "[or]" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string tempStr in tempTrimBs)
                            {
                                if (!trimA.Contains(tempStr))
                                {
                                    return true; ;
                                }
                            }
                            return false;
                        }
                        return !trimA.Contains(trimB);
                    }
                //break;
                case CaseExpectType.judge_default:
                    return true;
                //break;
                default:
                    return false;
                //break;

            }
        }

        /// <summary>
        /// i can find the value you need in the json (if the math value over one it will spit with ",")
        /// </summary>
        /// <param name="yourTarget">the key you want get</param>
        /// <param name="yourSouce">the json Souce</param>
        /// <returns>back value</returns>
        public static string PickJsonParameter(string yourTarget, string yourSouce)
        {
            string tempTarget = "\"" + yourTarget + "\"";
            string[] myJsonBackAr = null;
            string myJsonBack = null;
            if (!yourSouce.Contains(tempTarget))
            {
                return null;
            }
            try
            {
                yourSouce = yourSouce.Trim();
                if (yourSouce.StartsWith("["))
                {
                    JArray jAObj = (JArray)JsonConvert.DeserializeObject(yourSouce);
                    for (int i = 0; i < jAObj.Count; i++)
                    {
                        JObject jObj = (JObject)jAObj[i];
                        myJsonBackAr = GetJTokenValueEx(jObj, yourTarget);
                        if (myJsonBackAr != null)
                        {
                            foreach (string tempStr in myJsonBackAr)
                            {
                                myJsonBack = (myJsonBack + tempStr + ",");
                            }
                        }
                    }
                    if (myJsonBack != null)
                    {
                        myJsonBack = myJsonBack.Remove(myJsonBack.Length - 1);
                    }
                }
                else if (yourSouce.StartsWith("{"))
                {
                    JObject jObj = (JObject)JsonConvert.DeserializeObject(yourSouce);
                    myJsonBackAr = GetJTokenValueEx(jObj, yourTarget);
                    if (myJsonBackAr != null)
                    {
                        myJsonBack = String.Join(",", myJsonBackAr);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.PutInLog("ID:0243 " + ex.Message);
            }
            return myJsonBack;
        }

        public static string PickXmlParameter(string yourTarget, string yourSouce)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.LoadXml(yourSouce);

            }
            catch (Exception ex)
            {
                ErrorLog.PutInLog(ex);
                return null;
            }
            XmlNodeList tempNodeList = xml.SelectNodes("//" + yourTarget);
            if (tempNodeList.Count > 0)
            {
                return tempNodeList[0].InnerXml;
            }
            return null;
        }

        public static string PickStrParameter(string yourTarget, int yourStrLen, string yourSouce)
        {
            if (yourSouce.Contains(yourTarget))
            {
                string tempPickStr;
                int tempStart = yourSouce.IndexOf(yourTarget) + yourTarget.Length;
                tempPickStr = yourSouce.Remove(0, tempStart);
                if (tempPickStr.Length > yourStrLen)
                {
                    tempPickStr = tempPickStr.Remove(yourStrLen);
                }
                return tempPickStr;
            }
            return null;
        }

        /// <summary>
        /// get fist vlue from the json object by key 【仅取第一个配置值】
        /// </summary>
        /// <param name="yourJToken">json object</param>
        /// <param name="yourKey">your Key</param>
        /// <returns>value</returns>
        public static string GetJTokenValue(JToken yourJToken, string yourKey)
        {
            string expectVaule = null;
            if (yourJToken != null)
            {
                if (yourJToken is Newtonsoft.Json.Linq.JArray)
                {
                    foreach (JToken tempJToken in yourJToken)
                    {
                        if (((JObject)tempJToken)[yourKey] == null)
                        {
                            expectVaule = GetJTokenValue(tempJToken, yourKey);
                            if (expectVaule != null)
                            {
                                return expectVaule;
                            }
                        }
                        else
                        {
                            return ((JObject)tempJToken)[yourKey].ToString();
                        }
                    }
                }
                else if (yourJToken is JValue)
                {
                    return null;
                }
                else
                {
                    if (((JObject)yourJToken)[yourKey] == null)
                    {
                        foreach (JToken tempJToken in yourJToken.Values())
                        {
                            if (tempJToken.HasValues)
                            {
                                expectVaule = GetJTokenValue(tempJToken, yourKey);
                                if (expectVaule != null)
                                {
                                    return expectVaule;
                                }
                            }
                        }
                    }
                    else
                    {
                        return ((JObject)yourJToken)[yourKey].ToString();
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// get all the vlue from the json object by key 【将取出所有值】
        /// </summary>
        /// <param name="yourJToken">json object</param>
        /// <param name="yourKey">your Key</param>
        /// <returns>value list </returns>
        public static string[] GetJTokenValueEx(JToken yourJToken, string yourKey)
        {
            List<string> vauleList = new List<string>();
            string[] tempAr;
            if (yourJToken != null)
            {
                if (yourJToken is Newtonsoft.Json.Linq.JArray)
                {
                    foreach (JToken tempJToken in yourJToken)
                    {
                        //这里输出的不是values()，而是一个整体，如果这里使用[]会有重复
                        //if (((JObject)tempJToken)[yourKey] != null) { }
                        //{
                        //    vauleList.Add(((JObject)tempJToken)[yourKey].ToString());
                        //}
                        //System.Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++");
                        //System.Console.WriteLine(tempJToken.ToString());
                        tempAr = GetJTokenValueEx(tempJToken, yourKey);
                        if (tempAr != null)
                        {
                            foreach (string tempStr in tempAr)
                            {
                                vauleList.Add(tempStr);
                            }
                        }
                    }
                }
                else if (yourJToken is JValue)
                {
                    return null;
                }
                else
                {
                    if (yourJToken[yourKey] != null)
                    {
                        vauleList.Add(yourJToken[yourKey].ToString());
                    }
                    foreach (JToken tempJToken in yourJToken.Values())
                    {
                        if (tempJToken.HasValues)
                        {
                            //System.Console.WriteLine("---------------------------------------");
                            //System.Console.WriteLine(tempJToken.ToString());
                            tempAr = GetJTokenValueEx(tempJToken, yourKey);
                            if (tempAr != null)
                            {
                                foreach (string tempStr in tempAr)
                                {
                                    vauleList.Add(tempStr);
                                }
                            }
                        }
                    }
                }
            }
            if (vauleList.Count != 0)
            {
                return vauleList.ToArray();
            }
            return null;
        }

        /// <summary>
        ///  i will get a string and a len from your souce data by '-' (if fial the yourTarget will be null)
        /// </summary>
        /// <param name="yourSouce">your Souce</param>
        /// <param name="yourTarget">your string target</param>
        /// <param name="yourStrLen">your len</param>
        public static void GetStrPickData(string yourSouce, out string yourTarget, out int yourStrLen)
        {
            yourTarget = null;
            yourStrLen = 0;
            if (yourSouce.Contains("-"))
            {
                yourTarget = yourSouce.Remove(yourSouce.LastIndexOf("-"));
                try
                {
                    yourStrLen = int.Parse(yourSouce.Remove(0, yourSouce.LastIndexOf("-") + 1));
                }
                catch
                {
                    yourTarget = null;
                }
            }
        }

    }


}