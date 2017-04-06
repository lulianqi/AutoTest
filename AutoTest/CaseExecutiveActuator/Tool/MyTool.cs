using MyCommonHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;


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


namespace CaseExecutiveActuator
{
    public static class MyExtensionMethods
    {
        /// <summary>
        /// 添加键值，若遇到已有key则覆盖
        /// </summary>
        /// <param name="dc">Dictionary</param>
        /// <param name="yourKey">Key</param>
        /// <param name="yourValue">Value</param>
        public static void MyAdd(this Dictionary<string, ICaseExecutionDevice> dc, string yourKey, ICaseExecutionDevice yourValue)
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
        /// 添加键值，若遇到已有key则覆盖
        /// </summary>
        /// <param name="dc">Dictionary</param>
        /// <param name="yourKey">Key</param>
        /// <param name="yourValue">Value</param>
        public static void MyAdd(this Dictionary<string, IRunTimeStaticData> dc, string yourKey, IRunTimeStaticData yourValue)
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
        /// 添加键值，若遇到已有key则覆盖
        /// </summary>
        /// <typeparam name="T">T Type</typeparam>
        /// <param name="dc">Dictionary</param>
        /// <param name="yourKey">yourKey</param>
        /// <param name="yourValue">yourValue</param>
        public static void MyAdd<T>(this Dictionary<string, T> dc, string yourKey, T yourValue)
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
        /// 返回对象的深度克隆(泛型数据，要求T必须为值类型，或类似string的特殊引用类型[因为虽然使用string的克隆会有相同的引用，但对string进行修改的时都会创建新的对象])
        /// </summary>
        /// <typeparam name="TKey">TKey</typeparam>
        /// <typeparam name="TValue">TKey</typeparam>
        /// <param name="dc">目标Dictionary</param>
        /// <returns>对象的深度克隆</returns>
        public static Dictionary<TKey, TValue> MyClone<TKey, TValue>(this Dictionary<TKey, TValue> dc)
        {
            Dictionary<TKey, TValue> cloneDc = new Dictionary<TKey, TValue>();
            foreach (KeyValuePair<TKey, TValue> tempKvp in dc)
            {
                cloneDc.Add(tempKvp.Key, tempKvp.Value);
            }
            return cloneDc;
        }

        
        //public static Dictionary<string, ICaseExecutionDevice> MyClone(this Dictionary<string, ICaseExecutionDevice> dc)
        //{
        //    Dictionary<string, ICaseExecutionDevice> cloneDc = new Dictionary<string, ICaseExecutionDevice>();
        //    foreach(KeyValuePair<string, ICaseExecutionDevice> tempKvp in dc)
        //    {
        //        cloneDc.Add(tempKvp.Key, (ICaseExecutionDevice)tempKvp.Value.Clone());
        //    }
        //    return cloneDc;
        //}

        //public static Dictionary<string, IRunTimeStaticData> MyClone(this Dictionary<string, IRunTimeStaticData> dc)
        //{
        //    Dictionary<string, IRunTimeStaticData> cloneDc = new Dictionary<string, IRunTimeStaticData>();
        //    foreach (KeyValuePair<string, IRunTimeStaticData> tempKvp in dc)
        //    {
        //        cloneDc.Add(tempKvp.Key, (IRunTimeStaticData)tempKvp.Value.Clone());
        //    }
        //    return cloneDc;
        //}

        /// <summary>
        /// 返回对象的深度克隆
        /// </summary>
        /// <param name="dc">目标Dictionary</param>
        /// <returns>对象的深度克隆</returns>
        public static Dictionary<string, ICloneable> MyClone(this Dictionary<string, ICloneable> dc)
        {
            Dictionary<string, ICloneable> cloneDc = new Dictionary<string, ICloneable>();
            foreach (KeyValuePair<string, ICloneable> tempKvp in dc)
            {
                cloneDc.Add(tempKvp.Key, (ICloneable)tempKvp.Value.Clone());
            }
            return cloneDc;
        }

        public static List<IRunTimeStaticData> MyClone(this List<IRunTimeStaticData> lt)
        {
            List<IRunTimeStaticData> cloneLt = new List<IRunTimeStaticData>();
            foreach (IRunTimeStaticData tempKvp in lt)
            {
                cloneLt.Add((IRunTimeStaticData)tempKvp.Clone());
            }
            return cloneLt;
        }
    }

    public static class CaseTool
    {
        //Current 属性指向集合中的当前成员。
        //MoveNext 属性将枚举数移到集合中的下一成员
        //Reset 属性将枚举数移回集合的开始处
        public static int myStaticAutoVaneIndex = 0;

        public static string rootPath = System.Environment.CurrentDirectory;

        /// <summary>
        /// Check Data with my mothod
        /// </summary>
        /// <param name="BackData">Back Data</param>
        /// <param name="ExpectData">Expect Data</param>
        /// <param name="method"> Check method</param>
        /// <returns>is ok </returns>
        public static bool CheckBackData(string BackData, string ExpectData, string method)
        {
            string trimA = BackData.Replace(" ", "");
            trimA = trimA.Replace("\r", "");
            trimA = trimA.Replace("\n", "");
            trimA = trimA.Replace("\t", "");
            string trimB = ExpectData.Replace(" ", "");
            trimB = trimB.Replace("\r", "");
            trimB = trimB.Replace("\n", "");
            trimB = trimB.Replace("\t", "");
            if (method == "is")
            {
                if (trimB.Contains("[or]"))
                {
                    //string[] tempTrimBs = trimB.Split(("[or]").ToCharArray());//以每一个字节分开
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
            else if (method == "not")
            {
                return !(trimA == trimB);
            }
            else if (method == "like")
            {
                return false;
            }
            else if (method == "endwith")
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
            else if (method == "startwith")
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
            else if (method == "contain")
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
            else if (method == "uncontain")
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
            else if (method == "null")
            {
                return true;
            }
            else
            {
                ErrorLog.PutInLog("ID:0326 " + "发现未知检查方法");
                return false;
            }
        }

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

        /*
        nodename	选取此节点的所有子节点。
        /	从根节点选取。
        //	从匹配选择的当前节点选择文档中的节点，而不考虑它们的位置。
        .	选取当前节点。
        ..	选取当前节点的父节点。
        @	选取属性。
        */
        /// <summary>
        /// Check the case file if there is some illegal data
        /// </summary>
        /// <param name="sourceNode"> the file any  </param>
        /// <param name="targetKey"> target Key</param>
        /// <param name="targetValue">target Values</param>
        /// <returns>result</returns>
        public static string CheckCase(XmlNode sourceNode, string targetKey, string[] targetValue)
        {
            XmlNodeList tempNodes;
            try
            {
                tempNodes = sourceNode.SelectNodes("//" + targetKey);
            }
            catch
            {
                return "匹配失败- 未发现目标 " + targetKey;
            }
            foreach (XmlNode tempNode in tempNodes)
            {
                if (!targetValue.Contains(tempNode.InnerText))
                {
                    return "匹配失败-" + "目标 " + targetKey + ":" + tempNode.InnerText;
                }
            }
            return "";
        }


        public static MyRunCaseData<T> getCaseRunData<T>(XmlNode sourceNode) where T : ICaseExecutionContent
        {
            return new MyRunCaseData<T>();
        }

        /// <summary>
        /// i will get the date in sourceNode child with specified tag name(Ex if it is inexistence i will return null)
        /// </summary>
        /// <param name="sourceNode">source Node (if it is null i will Throw error)</param>
        /// <param name="tagName">the name taht you want</param>
        /// <returns>data you want (if not find it will be null)</returns>
        public static string getXmlInnerVauleEx(XmlNode sourceNode, string tagName)
        {
            //XmlNode tempCantent = sourceNode.SelectSingleNode(tagName);
            XmlNode tempCantent = sourceNode[tagName];
            if (tempCantent != null)
            {
                //return tempCantent.Value;  //if Value is"" it will be null
                return tempCantent.InnerText;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// i will get the date in sourceNode child with specified tag name
        /// </summary>
        /// <param name="sourceNode">source Node(if it is null i will Throw error)</param>
        /// <param name="tagName">the name taht you want(only the fist)</param>
        /// <returns>data you want (if not find it will be "")</returns>
        public static string getXmlInnerVaule(XmlNode sourceNode, string tagName)
        {
            //XmlNode tempCantent = sourceNode.SelectSingleNode(tagName);
            XmlNode tempCantent = sourceNode[tagName];
            if (tempCantent != null)
            {
                //return tempCantent.Value;  //if Value is"" it will be null
                return tempCantent.InnerText;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// i will get the Attribute in sourceNode child with specified tag attribute(Ex if it is inexistence i will return null)
        /// </summary>
        /// <param name="sourceNode">source Node(if it is null i will Throw error)</param>
        /// <param name="tagAttribute">the Attribute taht you want</param>
        /// <returns>data you want </returns>
        public static string getXmlAttributeVauleEx(XmlNode sourceNode, string tagAttribute)
        {
            if (sourceNode.Attributes[tagAttribute] != null)
            {
                return sourceNode.Attributes[tagAttribute].Value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// i will get the Attribute in sourceNode child with specified tag attribute
        /// </summary>
        /// <param name="sourceNode">source Node(if it is null i will Throw error)</param>
        /// <param name="tagAttribute">the Attribute taht you want</param>
        /// <param name="nullData">if not find it will be nullData</param>
        /// <returns>data you want (if not find it will be nullData)</returns>
        public static string getXmlAttributeVaule(XmlNode sourceNode, string tagAttribute ,string nullData)
        {
            if (sourceNode.Attributes[tagAttribute] != null)
            {
                return sourceNode.Attributes[tagAttribute].Value;
            }
            else
            {
                return nullData;
            }
        }

        /// <summary>
        /// i will get the Attribute in sourceNode child with specified tag attribute
        /// </summary>
        /// <param name="sourceNode">source Node(if it is null i will Throw error)</param>
        /// <param name="tagAttribute">the Attribute taht you want</param>
        /// <returns>data you want (if not find it will be "")</returns>
        public static string getXmlAttributeVaule(XmlNode sourceNode, string tagAttribute)
        {
            return getXmlAttributeVaule(sourceNode, tagAttribute, "");
        }

        /// <summary>
        /// i will change the XmlNode to caseParameterizationContent
        /// </summary>
        /// <param name="sourceNode">source Node (please make sure the sourceNode not null)</param>
        /// <returns> caseParameterizationContent you want</returns>
        public static caseParameterizationContent getXmlParametContent(XmlNode sourceNode)
        {
            caseParameterizationContent myParameContent = new caseParameterizationContent();
            if (sourceNode != null)//if it is null caseParameterizationContent.contentData will be null
            {
                if (sourceNode.Attributes["isHaveParameters"] != null)
                {
                    if (sourceNode.Attributes["isHaveParameters"].Value == "true")
                    {
                        myParameContent.hasParameter = true;
                    }
                }
                if (sourceNode.Attributes["additionalEncoding"] != null)
                {
                    try
                    {
                        myParameContent.encodetype = (ParameterizationContentEncodingType)Enum.Parse(typeof(ParameterizationContentEncodingType), sourceNode.Attributes["additionalEncoding"].Value);
                    }
                    catch
                    {
                        myParameContent.encodetype = ParameterizationContentEncodingType.encode_default;
                    }
                }
                myParameContent.contentData = sourceNode.InnerText;
            }
            return myParameContent;

        }

        /// <summary>
        /// i will change the XmlNode 's child to caseParameterizationContent use his name (if has same tagName,olny use fist one)
        /// </summary>
        /// <param name="sourceNode">source Node (please make sure the sourceNode not null)</param>
        /// <param name="tagName">child name</param>
        /// <returns>caseParameterizationContent you want</returns>
        public static caseParameterizationContent getXmlParametContent(XmlNode sourceNode, string tagName)
        {
            caseParameterizationContent myParameContent = new caseParameterizationContent();
            if (sourceNode != null)
            {
                XmlNode tempCantent = sourceNode[tagName];
                if (tempCantent != null)
                {
                    return getXmlParametContent(tempCantent);
                }
            }
            return myParameContent;
        }

        /// <summary>
        /// i will get CaseProtocol in your XmlNode (未使用)
        /// </summary>
        /// <param name="sourceNode">XmlNode (you shoud make sure it is a case)</param>
        /// <returns>CaseProtocol you want </returns>
        public static CaseProtocol getCaseProtocol(XmlNode sourceNode)
        {
            return CaseProtocol.unknownProtocol;
        }

        /// <summary>
        /// get Case ID from your string like "P0001C0001"
        /// </summary>
        /// <param name="yourData">yourData</param>
        /// <param name="PorjectID">out PorjectID</param>
        /// <param name="CaseID">out CaseID</param>
        /// <returns>is ok</returns>
        public static bool getTargetCaseID(string yourData, out int PorjectID, out int CaseID)
        {
            if (yourData.StartsWith("P"))
            {
                yourData = yourData.Remove(0, 1);
                string[] myTargets = yourData.Split('C');
                if (myTargets.Length == 2)
                {
                    try
                    {
                        PorjectID = int.Parse(myTargets[0]);
                        CaseID = int.Parse(myTargets[1]);
                        return true;
                    }
                    catch
                    {
                        PorjectID = CaseID = 0;
                        return false;
                    }
                }
            }
            PorjectID = CaseID = 0;
            return false;
        }

        
        //仅用于【caseParameterizationContent】内部
        //如果没有任何valid identification，直接返回原始数据，不报错（为实现最大兼容）
        /// <summary>
        /// get the getTargetContentData in caseParameterizationContent
        /// </summary>
        /// <param name="yourSourceData">Source Data</param>
        /// <param name="yourParameterList">ParameterList</param>
        /// <param name="yourStaticDataList">StaticDataList</param>
        /// <param name="errorMessage">error Message</param>
        /// <returns></returns>
        public static string GetCurrentParametersData(string yourSourceData, string splitStr, ActuatorStaticDataCollection yourActuatorStaticDataCollection, NameValueCollection yourDataResultCollection, out string errorMessage)
        {
            errorMessage = null;
            if (yourSourceData.Contains(splitStr))
            {
                var yourParameterList=yourActuatorStaticDataCollection.RunActuatorParameterList;
                var yourStaticDataList = yourActuatorStaticDataCollection.RunActuatorStaticDataList;
                var yourStaticDataSourceList = yourActuatorStaticDataCollection.RunActuatorStaticDataSouceList;
                int tempStart, tempEnd = 0;
                string tempKeyVaule = null;
                string keyParameter = null;
                string keyAdditionData = null;
                string tempVaule = null;
                while (yourSourceData.Contains(splitStr))
                {
                    tempStart = yourSourceData.IndexOf(splitStr);
                    tempEnd = yourSourceData.IndexOf(splitStr, tempStart + splitStr.Length);
                    if (tempEnd == -1)
                    {
                        errorMessage = string.Format("the identification  not enough in Source[{0}]", yourSourceData);
                        return yourSourceData;
                    }
                    tempKeyVaule = yourSourceData.Substring(tempStart + splitStr.Length, tempEnd - (tempStart + splitStr.Length));
                    keyParameter = TryGetParametersAdditionData(tempKeyVaule, out keyAdditionData);
                    if (keyAdditionData!=null)
                    {
                        keyAdditionData = GetCurrentParametersData(keyAdditionData, MyConfiguration.ParametersExecuteSplitStr, yourActuatorStaticDataCollection, yourDataResultCollection, out errorMessage);
                    }

                    Func<string> DealErrorAdditionData = () =>
                    {
                        tempVaule = "[ErrorData]";
                        return string.Format("ParametersAdditionData error find in the runTime data with keyParameter:[{0}] keyAdditionData:[{1}]", keyParameter, keyAdditionData);
                    };

                    #region RunTimeParameter
                    if (yourParameterList.Keys.Contains(keyParameter))
                    {
                        //RunTimeParameter 不含有参数信息，所以不处理keyParameter
                        tempVaule = yourParameterList[keyParameter];
                        yourSourceData = yourSourceData.Replace(splitStr + tempKeyVaule + splitStr, tempVaule);
                        yourDataResultCollection.myAdd(tempKeyVaule, tempVaule);
                    }
                    #endregion
                    
                    #region RunTimeStaticData
                    else if (yourStaticDataList.Keys.Contains(keyParameter))
                    {
                        if (keyAdditionData == null)
                        {
                            tempVaule = yourStaticDataList[keyParameter].DataCurrent();
                        }
                        else if(keyAdditionData=="=")
                        {
                            tempVaule = yourStaticDataList[keyParameter].DataCurrent();
                        }
                        else if(keyAdditionData=="+")
                        {
                            tempVaule = yourStaticDataList[keyParameter].DataMoveNext();
                        }
                        else if(keyAdditionData.StartsWith("+")) //+10 前移10
                        {
                            int tempTimes;
                            if(int.TryParse(keyAdditionData.Remove(0,1),out tempTimes))
                            {
                                if(tempTimes>0)
                                {
                                    for(int i=0;i<tempTimes;i++)
                                    {
                                        yourStaticDataList[keyParameter].DataMoveNext();
                                    }
                                    tempVaule = yourStaticDataList[keyParameter].DataCurrent();
                                }
                                else
                                {
                                    errorMessage = DealErrorAdditionData();
                                }
                            }
                            else
                            {
                                errorMessage = DealErrorAdditionData();
                            }
                        }
                        else
                        {
                            errorMessage = DealErrorAdditionData();
                        }

                        yourSourceData = yourSourceData.Replace(splitStr + tempKeyVaule + splitStr, tempVaule);
                        yourDataResultCollection.myAdd(tempKeyVaule, tempVaule);
                    }
                    #endregion

                    #region RunTimeStaticDataSource
                    else if (yourStaticDataSourceList.Keys.Contains(keyParameter))
                    {
                        if (keyAdditionData == null)
                        {
                            tempVaule = yourStaticDataSourceList[tempKeyVaule].DataCurrent();
                        }
                        else if (keyAdditionData == "=")
                        {
                            tempVaule = yourStaticDataList[keyParameter].DataCurrent();
                        }
                        else if (keyAdditionData == "+")
                        {
                            tempVaule = yourStaticDataList[keyParameter].DataMoveNext();
                        }
                        else if (keyAdditionData.StartsWith("+")) //+10 前移10
                        {
                            int tempTimes;
                            if (int.TryParse(keyAdditionData.Remove(0, 1), out tempTimes))
                            {
                                if (tempTimes > 0)
                                {
                                    for (int i = 0; i < tempTimes; i++)
                                    {
                                        yourStaticDataSourceList[keyParameter].DataMoveNext();
                                    }
                                    tempVaule = yourStaticDataSourceList[keyParameter].DataCurrent();
                                }
                                else
                                {
                                    errorMessage = DealErrorAdditionData();
                                }
                            }
                            else
                            {
                                errorMessage = DealErrorAdditionData();
                            }
                        }
                        else
                        {
                            tempVaule = yourStaticDataSourceList[keyParameter].GetDataVaule(keyAdditionData);
                            if(tempVaule==null)
                            {
                                errorMessage = DealErrorAdditionData();
                            }
                        }

                        yourSourceData = yourSourceData.Replace(splitStr + tempKeyVaule + splitStr, tempVaule);
                        yourDataResultCollection.myAdd(tempKeyVaule, tempVaule);
                    }
                    #endregion
                    
                    else
                    {
                        tempVaule = "[ErrorData]";
                        errorMessage = string.Format("can not find your key [{0}] in StaticDataList", keyParameter);
                        yourSourceData = yourSourceData.Replace(splitStr + tempKeyVaule + splitStr, tempVaule);
                        yourDataResultCollection.myAdd(tempKeyVaule, tempVaule);
                    }
                }

            }

            return yourSourceData;
        }

        /// <summary>
        /// 处理ParametersData，解析静态数据名及其参数
        /// </summary>
        /// <param name="souceData">souce parameter data 原数据</param>
        /// <param name="additionData">返回辅助参数数据，若没有或无法解析返回null</param>
        /// <returns></returns>
        public static string TryGetParametersAdditionData(string souceData, out string additionData)
        {
            additionData = null;
            string parametersData = null;
            if (souceData!=null)
            {
                if (souceData.EndsWith(")"))
                {
                    int startIndex = souceData.LastIndexOf('(');
                    if (startIndex > 0)
                    {
                        parametersData = souceData.Remove(startIndex);
                        additionData = souceData.Substring(startIndex + 1, souceData.Length - startIndex - 2);
                    }
                    else
                    {
                        parametersData = souceData;
                    }
                }
                else
                {
                    parametersData = souceData;
                }
            }
            return parametersData;
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
            string myJsonBack =null ;
            if (!yourSouce.Contains(tempTarget))
            {
                return null;
            }
            try
            {
                yourSouce.Trim();
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
                                if(expectVaule!=null)
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
            if (vauleList.Count!=0)
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

        /// <summary>
        /// 处理case文件路径 （若以@开头则使用绝对路径，否则使用相对路径相对于程序运行路径）
        /// </summary>
        /// <param name="path">表述路径 （若使用相对路径则前面不需要加\）</param>
        /// <param name="depthPath">深目录(前后都不用加\)</param>
        /// <returns>实际路径</returns>
        public static string GetFullPath(string path, string depthPath)
        {
            if(path.StartsWith("@"))
            {
                return path.Remove(0, 1);
            }
            else
            {
                return depthPath == null ? string.Format("{0}\\{1}", rootPath, path) : string.Format("{0}\\{1}\\{2}", rootPath, depthPath, path);
            }
        }


    }

}
