using CaseExecutiveActuator.CaseActuator;
using MyCommonHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
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

namespace CaseExecutiveActuator.Tool
{
    
    public static class CaseTool
    {

        public static string rootPath = System.Environment.CurrentDirectory;

        /// <summary>
        /// 处理case文件路径 （若以@开头则使用绝对路径，否则使用相对路径相对于程序运行路径）
        /// </summary>
        /// <param name="path">表述路径 （若使用相对路径则前面不需要加\）</param>
        /// <param name="depthPath">深目录(前后都不用加\)</param>
        /// <returns>实际路径</returns>
        public static string GetFullPath(string path, string depthPath)
        {
            if (path.StartsWith("@"))
            {
                return path.Remove(0, 1);
            }
            else
            {
                return depthPath == null ? string.Format("{0}\\{1}", rootPath, path) : string.Format("{0}\\{1}\\{2}", rootPath, depthPath, path);
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



        /// <summary>
        /// get the some same name child node in yourNode ,and it can with your attributs (if not find the attribut the attribut data will be null)
        /// </summary>
        /// <param name="sourceNode">source Node (if it is null i will Throw error)</param>
        /// <param name="tagName">the name taht you want</param>
        /// <param name="tagAttributes">if you do not care the attributs you can set this parameter null</param>
        /// <returns>data you want (if not find any match data it will be a 0 leng list)</returns>
        public static List<string[]> GetXmlInnerMetaDataList(XmlNode sourseNode,string tagName,string[] tagAttributes)
        {
            List<string[]> outData = new List<string[]>();
            if (sourseNode.HasChildNodes)
            {
                foreach (XmlNode tempNode in sourseNode.ChildNodes)
                {
                    if(tempNode.Name == tagName)
                    {
                        string[] tempOneNodeData ;
                        if (tagAttributes != null)
                        {
                            tempOneNodeData = new string[tagAttributes.Length + 1];
                            tempOneNodeData[0] = tempNode.InnerText;
                            for (int i = 0; i < tagAttributes.Length; i++)
                            {
                                tempOneNodeData[i + 1] = GetXmlAttributeVaule(tempNode, tagAttributes[i]);
                            }
                        }
                        else
                        {
                            tempOneNodeData = new string[] { tempNode.InnerText };
                        }
                        outData.Add(tempOneNodeData);
                    }
                }
            }
            return outData;
        }

        /// <summary>
        /// get the some same name child node in yourNode ,and it can with your attributs (if not find the attribut the attribut data will be null)(with souce xmlnode)
        /// </summary>
        /// <param name="sourceNode">source Node (if it is null i will Throw error)</param>
        /// <param name="tagName">the name taht you want</param>
        /// <param name="tagAttributes">if you do not care the attributs you can set this parameter null</param>
        /// <returns>data you want (if not find any match data it will be a 0 leng list)</returns>
        public static List<KeyValuePair<XmlNode, string[]>> GetXmlInnerMetaDataListEx(XmlNode sourseNode, string tagName, string[] tagAttributes)
        {
            List<KeyValuePair<XmlNode, string[]>> outData = new  List<KeyValuePair<XmlNode, string[]>>();
            if (sourseNode.HasChildNodes)
            {
                foreach (XmlNode tempNode in sourseNode.ChildNodes)
                {
                    if (tempNode.Name == tagName)
                    {
                        string[] tempOneNodeData;
                        if (tagAttributes != null)
                        {
                            tempOneNodeData = new string[tagAttributes.Length + 1];
                            tempOneNodeData[0] = tempNode.InnerText;
                            for (int i = 0; i < tagAttributes.Length; i++)
                            {
                                tempOneNodeData[i + 1] = GetXmlAttributeVaule(tempNode, tagAttributes[i]);
                            }
                        }
                        else
                        {
                            tempOneNodeData = new string[] { tempNode.InnerText };
                        }
                        outData.Add(new KeyValuePair<XmlNode, string[]>(tempNode,tempOneNodeData));
                    }
                }
            }
            return outData;
        }

        /// <summary>
        /// get a bool value from a string with "true" or "false"
        /// </summary>
        /// <param name="sourceString">source string</param>
        /// <param name="isTrue">is succeed</param>
        /// <returns>out result</returns>
        public static bool GetTureOrFalse(string sourceString, out bool isSucceed)
        {
            isSucceed = true;
            if(!string.IsNullOrEmpty(sourceString))
            {
                if(sourceString.ToLower()=="true")
                {
                    return true;
                }
                else if (sourceString.ToLower() == "false")
                {
                    return false;
                }
            }
            isSucceed = false;
            return false;
        }

        /// <summary>
        /// get a bool value from a string with "true" or "false"
        /// </summary>
        /// <param name="sourceString">source string</param>
        /// <returns>out result</returns>
        public static bool GetTureOrFalse(string sourceString)
        {
            if (!string.IsNullOrEmpty(sourceString))
            {
                if (sourceString.ToLower() == "true")
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// i will get the date in sourceNode child with specified tag name(Ex if it is inexistence i will return null)[获取第一个匹配项的内容，如果没有返回null]
        /// </summary>
        /// <param name="sourceNode">source Node (if it is null i will Throw error)</param>
        /// <param name="tagName">the name taht you want</param>
        /// <returns>data you want (if not find it will be null)</returns>
        public static string GetXmlInnerVaule(XmlNode sourceNode, string tagName)
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
        /// i will get the date in sourceNode child with specified tag name(if it is inexistence i will return "")[获取第一个匹配项的内容，如果没有返回""]
        /// </summary>
        /// <param name="sourceNode">source Node(if it is null i will Throw error)</param>
        /// <param name="tagName">the name taht you want(only the fist)</param>
        /// <returns>data you want (if not find it will be "")</returns>
        public static string GetXmlInnerVauleWithEmpty(XmlNode sourceNode, string tagName)
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
        /// i will get the Attribute in sourceNode child with specified tag attribute( if it is inexistence i will return null)
        /// </summary>
        /// <param name="sourceNode">source Node(if it is null i will Throw error)</param>
        /// <param name="tagAttribute">the Attribute taht you want</param>
        /// <returns>data you want </returns>
        public static string GetXmlAttributeVaule(XmlNode sourceNode, string tagAttribute)
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
        public static string GetXmlAttributeVauleEx(XmlNode sourceNode, string tagAttribute ,string nullData)
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
        public static string GetXmlAttributeVauleWithEmpty(XmlNode sourceNode, string tagAttribute)
        {
            return GetXmlAttributeVauleEx(sourceNode, tagAttribute, "");
        }

        /// <summary>
        /// i will change the XmlNode to caseParameterizationContent[获取由xml表示的caseParameterizationContent结构，为了扩展性任何时候都应该考虑优先使用该方法解析caseParameterizationContent]
        /// </summary>
        /// <param name="sourceNode">source Node (please make sure the sourceNode not null)</param>
        /// <returns> caseParameterizationContent you want</returns>
        public static caseParameterizationContent GetXmlParametContent(XmlNode sourceNode)
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
        public static caseParameterizationContent GetXmlParametContent(XmlNode sourceNode, string tagName)
        {
            caseParameterizationContent myParameContent = new caseParameterizationContent();
            if (sourceNode != null)
            {
                XmlNode tempCantent = sourceNode[tagName];
                if (tempCantent != null)
                {
                    return GetXmlParametContent(tempCantent);
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
                var yourParameterList=yourActuatorStaticDataCollection.RunActuatorStaticDataKeyList;
                var yourStaticDataList = yourActuatorStaticDataCollection.RunActuatorStaticDataParameterList;
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

                    #region RunTimeStaticKey
                    if (yourParameterList.Keys.Contains(keyParameter))
                    {
                        //RunTimeParameter 不含有参数信息，所以不处理keyParameter
                        tempVaule = yourParameterList[keyParameter];
                        yourSourceData = yourSourceData.Replace(splitStr + tempKeyVaule + splitStr, tempVaule);
                        yourDataResultCollection.myAdd(tempKeyVaule, tempVaule);
                    }
                    #endregion
                    
                    #region RunTimeStaticParameter
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
                            tempVaule = yourStaticDataSourceList[keyParameter].DataCurrent();
                        }
                        else if (keyAdditionData == "+")
                        {
                            tempVaule = yourStaticDataSourceList[keyParameter].DataMoveNext();
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
        private static string TryGetParametersAdditionData(string souceData, out string additionData)
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


    }

}
