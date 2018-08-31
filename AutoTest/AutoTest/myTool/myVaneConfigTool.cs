using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Net;
using MyCommonHelper;


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


namespace AutoTest.MyTool
{

    /// <summary>
    /// 报文数据结构
    /// </summary>
    public struct vaneConfig
    {
        //public byte[] MagicNumber = new byte[4];
        //public byte ContentType;
        //public byte[] SequenceNumber = new byte[4];
        //public List<Dictionary<byte, byte[]>> Contents = new List<Dictionary<byte, byte[]>>();
        public string errorMsg;
        public byte[] MagicNumber;
        public byte ContentType;
        public int MsgLen;
        public byte[] SequenceNumber;
        public Dictionary<byte[], byte[]> Contents;

        public vaneConfig(string tempVal)
        {
            errorMsg = "";
            MsgLen = 0;
            MagicNumber = new byte[4];
            SequenceNumber = new byte[4];
            ContentType = 0;
            Contents = new Dictionary<byte[], byte[]>();
        }
    }

    /// <summary>
    /// 局域网广播信息报文结构
    /// </summary>
    public class vaneConfigBroadcast
    {
        public vaneConfig myVaneConfig;
        public int liveClick;
        public vaneConfigBroadcast(int yourClick, vaneConfig yourVaneConfig)
        {
            myVaneConfig = yourVaneConfig;
            liveClick = yourClick;
        }
    }

    /// <summary>
    /// 连接设备所需要的信息
    /// </summary>
    public struct vaneConfigConnectInfo
    {
        public string myGW_ID;
        public IPEndPoint myIpEp;
        public vaneConfigConnectInfo(string yourGW_ID, IPEndPoint yourIpEp)
        {
            myGW_ID = yourGW_ID;
            myIpEp = yourIpEp;
        }
    }

    /// <summary>
    /// vaneConfig 返回的EP信息
    /// </summary>
    public struct vaneConfigEpInfo
    {
        public string errorMsg;
        public string epId;
        public string epName;
        public string epSignal;
        public string epBattery;
        public string epOnline;
        public string epType;
        public string epVendor;
        public string epModel;
        public string epVersion;

        public vaneConfigEpInfo(string yourVal)
        {
            errorMsg = "";
            epId = epName = epSignal = epBattery = epOnline = epType = epVendor = epModel = epVersion = yourVal;
        }

    }

    /// <summary>
    /// 版本信息
    /// </summary>
    public struct vaneConfigNewVerInfo
    {
        public string errorMsg;
        public string verType;
        public string verCurrentVersion;
        public string verEpId;
        public string verCode;
        public string verDesc;
        public string verString;
        public vaneConfigNewVerInfo(string yourVal)
        {
            errorMsg = "";
            verType = verCurrentVersion = verEpId = verCode = verDesc = verString = yourVal;
        }
    }

    /// <summary>
    /// 提供对vanelife局域网配置等功能的数据组装及解析
    /// </summary>
    public static class myVaneConfigTool
    {

        /// <summary>
        /// 比较两个字节数组
        /// </summary>
        /// <param name="bytesA"></param>
        /// <param name="bytesB"></param>
        /// <returns></returns>
        public static bool isBytesSame(byte[] bytesA, byte[] bytesB)
        {
            if (bytesA == null || bytesB == null)
            {
                return false;
            }
            if (bytesA.Length == bytesB.Length)
            {
                for (int i = 0; i < bytesB.Length; i++)
                {
                    if (bytesA[i] != bytesB[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 去除bytes尾部空值
        /// </summary>
        /// <param name="yourBytes"></param>
        /// <returns></returns>
        public static byte[] rmBytesEnd(byte[] yourBytes)
        {
            if (yourBytes == null)
            {
                return null;
            }
            int tempLen=yourBytes.Length;
            for (int i = yourBytes.Length - 1; i > 0; i--)
            {
                if (yourBytes[i] == 0x00)
                {
                    tempLen--;
                }
                else
                {
                    break;
                }
            }
            byte[] tempBytes = new byte[tempLen];
            for (int i = 0; i < tempLen; i++)
            {
                tempBytes[i] = yourBytes[i];
            }
            return tempBytes;
        }

        /// <summary> 
        /// 字节数组转16进制字符串 
        /// </summary> 
        /// <param name="bytes"></param> 
        /// <returns></returns> 
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        /// <summary> 
        /// 16进制字符串转字节数组 
        /// </summary> 
        /// <param name="hexString">16进制字符串</param> 
        /// <returns></returns> 
        public static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace("-", "");
            hexString = hexString.Replace(" ", "");
            hexString = hexString.Replace("\n", "");
            if ((hexString.Length % 2) != 0)
            {
                hexString += "0";
            }
            byte[] returnBytes = new byte[hexString.Length / 2];
            try
            {
                for (int i = 0; i < returnBytes.Length; i++)
                    returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            catch
            {
                //MessageBox.Show("find illegal data ", "STOP");
                return null;
            }
            return returnBytes;
        }

        /// <summary>
        /// i will get a byte from a string wth hex16
        /// </summary>
        /// <param name="hexString">your hexString</param>
        /// <returns>the byte you want</returns>
        public static byte myGetByte(string hexString)
        {
            try
            {
                return Convert.ToByte(hexString, 16);
            }
            catch (Exception ex)
            {
                ErrorLog.PutInLog(ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// i will change your bytes to string like 00-00-00
        /// </summary>
        /// <param name="yourData">your Data</param>
        /// <returns>hex string</returns>
        public static string getHexByBytes(byte[] yourData)
        {
            if (yourData == null)
            {
                return "";
            }
            else
            {
                return (BitConverter.ToString(yourData));
            }
        }

        /// <summary>
        /// i will change your bytes to string by UTF8
        /// </summary>
        /// <param name="yourData">your bytes</param>
        /// <returns>your string</returns>
        public static string getUtf8StrByBytes(byte[] yourData)
        {
            if (yourData == null)
            {
                return "";
            }
            else
            {
                return (Encoding.UTF8.GetString(yourData));
            }
        }

        /// <summary>
        /// i will change your string to bytes by UTF8
        /// </summary>
        /// <param name="yourStr">your data with string</param>
        /// <returns>the bytes you want</returns>
        public static byte[] getUtf8BytesByStr(string yourStr)
        {
            return Encoding.UTF8.GetBytes(yourStr);
        }

        /// <summary>
        /// i will change your bytes to IP by vanelife way
        /// </summary>3
        /// <param name="yourData">your bytes</param>
        /// <returns>your IP</returns>
        public static string getIpByBytes(byte[] yourData)
        {
            if (yourData == null)
            {
                return "null data";
            }
            if (yourData.Length > 6)
            {
                return "length error";
            }
            else
            {
                string tempIp = "";
                for (int i = 0; i < yourData.Length; i++)
                {
                    tempIp += (int)yourData[i] + ".";
                }
                tempIp = tempIp.TrimEnd(new char[] { '.' });
                return tempIp;
            }
        }

        /// <summary>
        /// i will change your bytes to int,and bytes can not more than 4 byte
        /// </summary>
        /// <param name="yourLen">your bytes</param>
        /// <returns>your len</returns>
        public static int getByteLen(byte[] yourLen)
        {
            //return BitConverter.ToUInt16(yourLen,0);
            if (yourLen.Length > 4)
            {
                return -1;
            }
            else
            {
                int tempLen = 0;
                for (int i = 0; i < yourLen.Length; i++)
                {
                    tempLen += (int)Math.Pow(256, yourLen.Length - i - 1) * yourLen[i];
                }
                return tempLen;
            }
        }

        /// <summary>
        /// i will change a int to byte and change the 1,2
        /// </summary>
        /// <param name="yourLen">your Len</param>
        /// <returns>your bytes</returns>
        public static byte[] createInt16Bytes(int yourLen)
        {
            byte[] tempData = BitConverter.GetBytes(yourLen);
            byte[] dataToBack = new byte[2];
            dataToBack[0] = tempData[1];
            dataToBack[1] = tempData[0];
            return dataToBack;
        }

        /// <summary>
        /// i will load load VaneConfigDictionary file in your Dictionary
        /// </summary>
        /// <param name="yourPath">your file path</param>
        /// <param name="yourDictionary">your souce Dictionary</param>
        /// <param name="infoIndex">Which node you want,start with 0</param>
        /// <returns>error message</returns>
        public static string loadVaneConfigFile(string yourPath, Dictionary<byte, string> yourDictionary, int infoIndex)
        {
            XmlDocument tempXmlDct = new XmlDocument();
            if (!File.Exists(yourPath))
            {
                return "can not find the file";
            }
            try
            {
                tempXmlDct.Load(yourPath);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            try
            {
                yourDictionary.Clear();
                foreach (XmlNode tempNode in tempXmlDct.ChildNodes[1].ChildNodes[infoIndex])
                {
                    yourDictionary.Add(myGetByte(tempNode.Attributes["AssignedNumber"].Value), tempNode.Name);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.PutInLog(ex);
                return ex.Message;
            }
            return "";
        }


        /// <summary>
        /// i will change a vaneConfig to byte[] that send with tcp
        /// </summary>
        /// <param name="yourVaneConfig">your vaneConfig</param>
        /// <returns>the bytes you want</returns>
        public static byte[] createDataToSent(vaneConfig yourVaneConfig)
        {
            //byte[] myDataToSend = new byte[yourVaneConfig.MsgLen];
            //myDataToSend.Concat(
            byte[] myDataToSend = yourVaneConfig.MagicNumber;
            myDataToSend = (myDataToSend.Concat(new byte[]{yourVaneConfig.ContentType})).ToArray();
            myDataToSend = (myDataToSend.Concat(new byte[] { 0x00 })).ToArray();
            myDataToSend = (myDataToSend.Concat(createInt16Bytes(yourVaneConfig.MsgLen))).ToArray();
            myDataToSend = (myDataToSend.Concat(yourVaneConfig.SequenceNumber)).ToArray();
            foreach (var tempTLV in yourVaneConfig.Contents)
            {
                myDataToSend = (myDataToSend.Concat(tempTLV.Key)).ToArray();
                myDataToSend = (myDataToSend.Concat(createInt16Bytes(tempTLV.Value.Length))).ToArray();
                myDataToSend = (myDataToSend.Concat(tempTLV.Value)).ToArray();
            }
            return myDataToSend;
        }

         
        /// <summary>
        /// i will analyze your bytes to vaneConfig
        /// </summary>
        /// <param name="yourConfig">your Config bytes</param>
        /// <returns>vaneConfig</returns>
        public static vaneConfig myConfigDataAnalyze(byte[] yourConfig)
        {
            vaneConfig nowCig = new vaneConfig("");
            if (yourConfig.Length > 12)
            {
                Array.Copy(yourConfig, 0, nowCig.MagicNumber, 0, 4);
                nowCig.ContentType = yourConfig[4];
                Array.Copy(yourConfig, 8, nowCig.SequenceNumber, 0, 4);
                byte[] tempLen = new byte[2];
                Array.Copy(yourConfig, 6, tempLen, 0, 2);
                int contentLenth = getByteLen(tempLen);
                nowCig.MsgLen = contentLenth;
                if (contentLenth > 0)
                {
                    byte[] tempContent = new byte[contentLenth];
                    Array.Copy(yourConfig, 12, tempContent, 0, contentLenth);
                    int tempTLVIndex = 0;
                    while ((contentLenth - tempTLVIndex) > 0)
                    {
                        byte[] tempProtocolType = new byte[2];
                        byte[] tempBytesTLVLen = new byte[2];
                        Array.Copy(tempContent, tempTLVIndex, tempProtocolType, 0, 2);
                        Array.Copy(tempContent, tempTLVIndex + 2, tempBytesTLVLen, 0, 2);
                        int tempTLVLen = getByteLen(tempBytesTLVLen);
                        byte[] tempTLVContent = new byte[tempTLVLen];
                        Array.Copy(tempContent, tempTLVIndex + 4, tempTLVContent, 0, tempTLVLen);
                        nowCig.Contents.Add(tempProtocolType, tempTLVContent);
                        tempTLVIndex = tempTLVIndex + 4 + tempTLVLen;
                    }
                }
            }
            else
            {
                nowCig.errorMsg = "not enough data";
            }
            return nowCig;
        }

        /// <summary>
        /// i will pick out the ep list with vaneConfigEpInfo from your data 
        /// </summary>
        /// <param name="yourEpItems">your souse data for ep list</param>
        /// <returns>vaneConfigEpInfo</returns>
        public static List<vaneConfigEpInfo> pickOutEpInfo(byte[] yourEpItems)
        {
            if (yourEpItems == null)
            {
                return null;
            }

            List<vaneConfigEpInfo> tempEpInfoList = new List<vaneConfigEpInfo>();
            List<byte[]> tempEpInfoDatas = new List<byte[]>();
            byte[] tempEpInfoData;
            byte[] tempContentLenth = new byte[2];
            byte[] tempProtocolType = new byte[2];
            int nowIndex = 0;
            int tempEpInfoLen = 0;
            while (yourEpItems.Length > nowIndex+2)
            {
                Array.Copy(yourEpItems, nowIndex, tempProtocolType, 0, 2);
                nowIndex += 2;
                if (!isBytesSame(tempProtocolType, new byte[] { 0x00, 0x90 }))
                {
                    return null;
                }
                else
                {
                    Array.Copy(yourEpItems, nowIndex, tempContentLenth, 0, 2);
                    tempEpInfoLen = getByteLen(tempContentLenth);
                    nowIndex += 2;
                    tempEpInfoData = new byte[tempEpInfoLen];
                    Array.Copy(yourEpItems, nowIndex, tempEpInfoData, 0, tempEpInfoLen);
                    nowIndex += tempEpInfoLen;
                    tempEpInfoDatas.Add(tempEpInfoData);
                }
            }

            if (tempEpInfoDatas.Count > 0)
            {
                foreach (byte[] tempData in tempEpInfoDatas)
                {
                    vaneConfigEpInfo myTempConfigEpInfo = new vaneConfigEpInfo("");
                    int tempTLVIndex = 0;
                    int contentLenth = tempData.Length;
                    while ((contentLenth - tempTLVIndex) > 0)
                    {
                        byte[] tempBytesTLVLen = new byte[2];
                        Array.Copy(tempData, tempTLVIndex, tempProtocolType, 0, 2);
                        Array.Copy(tempData, tempTLVIndex + 2, tempBytesTLVLen, 0, 2);
                        int tempTLVLen = getByteLen(tempBytesTLVLen);
                        byte[] tempTLVContent = new byte[tempTLVLen];
                        Array.Copy(tempData, tempTLVIndex + 4, tempTLVContent, 0, tempTLVLen);
                        //nowCig.Contents.Add(tempProtocolType, tempTLVContent);
                        tempTLVIndex = tempTLVIndex + 4 + tempTLVLen;
                        switch (tempProtocolType[1])
                        {
                            case 0x91 :
                                myTempConfigEpInfo.epId = getUtf8StrByBytes(tempTLVContent);
                                break;
                            case 0x92 :
                                myTempConfigEpInfo.epName = getUtf8StrByBytes(tempTLVContent);
                                break;
                            case 0x93 :
                                myTempConfigEpInfo.epSignal = (getByteLen(tempTLVContent)).ToString();
                                break;
                            case 0x94 :
                                myTempConfigEpInfo.epBattery = (getByteLen(tempTLVContent)).ToString();
                                break;
                            case 0x9A :
                                myTempConfigEpInfo.epOnline = (getByteLen(tempTLVContent)).ToString();
                                break;
                            case 0x9B:
                                myTempConfigEpInfo.epType = (getByteLen(tempTLVContent)).ToString();
                                break;
                            case 0x8C:
                                myTempConfigEpInfo.epVendor = getHexByBytes(tempTLVContent);
                                break;
                            case 0x8D:
                                myTempConfigEpInfo.epModel = getHexByBytes(tempTLVContent);
                                break;
                            case 0x95:
                                myTempConfigEpInfo.epVersion = getHexByBytes(tempTLVContent);
                                break;
                            default:
                                myTempConfigEpInfo.errorMsg += "find nuknow data";
                                break;
                        }
                    }
                    tempEpInfoList.Add(myTempConfigEpInfo);
                }
                return tempEpInfoList;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// i will pick out the ver info list with vaneConfigNewVerInfo from your data 
        /// </summary>
        /// <param name="yourEpItems">your souse data for ver list</param>
        /// <returns>vaneConfigNewVerInfo</returns>
        public static List<vaneConfigNewVerInfo> pickOutVerInfo(List<byte[]> yourVerItems)
        {
            if (yourVerItems == null)
            {
                return null;
            }

            List<vaneConfigNewVerInfo> tempVerInfoList = new List<vaneConfigNewVerInfo>();
            List<byte[]> tempVerInfoDatas = new List<byte[]>();
            
            byte[] tempContentLenth = new byte[2];
            byte[] tempProtocolType = new byte[2];
            //byte[] tempVerInfoData;

            /*
            int nowIndex = 0;
            int tempVerInfoLen = 0;
            while (yourVerItems.Length > nowIndex + 2)
            {
                Array.Copy(yourVerItems, nowIndex, tempProtocolType, 0, 2);
                nowIndex += 2;
                if (!isBytesSame(tempProtocolType, new byte[] { 0x00, 0xFF }))
                {
                    return null;
                }
                else
                {
                    Array.Copy(yourVerItems, nowIndex, tempContentLenth, 0, 2);
                    tempVerInfoLen = getByteLen(tempContentLenth);
                    nowIndex += 2;
                    tempVerInfoData = new byte[tempVerInfoLen];
                    Array.Copy(yourVerItems, nowIndex, tempVerInfoData, 0, tempVerInfoLen);
                    nowIndex += tempVerInfoLen;
                    tempVerInfoDatas.Add(tempVerInfoData);
                }
            }
             * */

            tempVerInfoDatas = yourVerItems;

            if (tempVerInfoDatas.Count > 0)
            {
                foreach (byte[] tempData in tempVerInfoDatas)
                {
                    vaneConfigNewVerInfo myTempConfigVerInfo = new vaneConfigNewVerInfo("");
                    int tempTLVIndex = 0;
                    int contentLenth = tempData.Length;
                    while ((contentLenth - tempTLVIndex) > 0)
                    {
                        byte[] tempBytesTLVLen = new byte[2];
                        Array.Copy(tempData, tempTLVIndex, tempProtocolType, 0, 2);
                        Array.Copy(tempData, tempTLVIndex + 2, tempBytesTLVLen, 0, 2);
                        int tempTLVLen = getByteLen(tempBytesTLVLen);
                        byte[] tempTLVContent = new byte[tempTLVLen];
                        Array.Copy(tempData, tempTLVIndex + 4, tempTLVContent, 0, tempTLVLen);
                        //nowCig.Contents.Add(tempProtocolType, tempTLVContent);
                        tempTLVIndex = tempTLVIndex + 4 + tempTLVLen;
                        switch (tempProtocolType[1])
                        {
                            case 0xA5:
                                myTempConfigVerInfo.verType = findInterpretation(myShareData.myVersionTypeNumberDictionary, tempTLVContent); 
                                break;
                            case 0xA8:
                                myTempConfigVerInfo.verCode = getHexByBytes(tempTLVContent);
                                break;
                            case 0xA9:
                                myTempConfigVerInfo.verCurrentVersion = getUtf8StrByBytes(tempTLVContent);
                                break;
                            case 0x91:
                                myTempConfigVerInfo.verEpId = getUtf8StrByBytes(tempTLVContent);
                                break;
                            case 0x9F:
                                myTempConfigVerInfo.verString = getUtf8StrByBytes(tempTLVContent);
                                break;
                            case 0xA0:
                                myTempConfigVerInfo.verDesc = getUtf8StrByBytes(tempTLVContent);
                                break;
                            default:
                                myTempConfigVerInfo.errorMsg += "find nuknow data";
                                break;
                        }
                    }
                    tempVerInfoList.Add(myTempConfigVerInfo);
                }
                return tempVerInfoList;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// i will pick out your data you want in vaneConfig
        /// </summary>
        /// <param name="yourvaneConfig">Sourse vaneConfig</param>
        /// <param name="yourAssignedNumber">Assigned Number</param>
        /// <returns>the data you want</returns>
        public static byte[] pickOutContent(vaneConfig yourvaneConfig,byte yourAssignedNumber)
        {
            foreach(KeyValuePair<byte[], byte[]> tempKvp in yourvaneConfig.Contents)
            {
                if (tempKvp.Key[1] == yourAssignedNumber)
                {
                    return tempKvp.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// i will pick out your datas you want in vaneConfig(that your may has more then one data)
        /// </summary>
        /// <param name="yourvaneConfig">Sourse vaneConfig</param>
        /// <param name="yourAssignedNumber">Assigned Number</param>
        /// <returns>the data list you want</returns>
        public static List<byte[]> pickOutContents(vaneConfig yourvaneConfig, byte yourAssignedNumber)
        {
            List<byte[]> tempContentList = new List<byte[]>();
            foreach (KeyValuePair<byte[], byte[]> tempKvp in yourvaneConfig.Contents)
            {
                if (tempKvp.Key[1] == yourAssignedNumber)
                {
                    tempContentList.Add(tempKvp.Value);
                }
            }
            return tempContentList;
        }

        /// <summary>
        /// i will pick out your data you want in vaneConfig
        /// </summary>
        /// <param name="yourvaneConfig">Sourse vaneConfig</param>
        /// <param name="yourAssignedNumber">Assigned Number</param>
        /// <returns>the data you want</returns>
        public static byte[] pickOutContent(vaneConfig yourvaneConfig, byte[] yourAssignedNumber)
        {
            foreach (KeyValuePair<byte[], byte[]> tempKvp in yourvaneConfig.Contents)
            {
                if (isBytesSame(tempKvp.Key, yourAssignedNumber))
                {
                    return tempKvp.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// 解释数据含义
        /// </summary>
        /// <param name="yourDic">数据字典</param>
        /// <param name="yourKey">数据索引</param>
        /// <returns>具体含义</returns>
        public static string findInterpretation(Dictionary<byte, string> yourDic, byte yourKey)
        {
           
            foreach (KeyValuePair<byte, string> tempKvp in yourDic)
            {
                if (tempKvp.Key == yourKey)
                {
                    return tempKvp.Value;
                }
            }
            return "unkonw: " + yourKey.ToString("X2");
        }

        /// <summary>
        /// 解释数据含义
        /// </summary>
        /// <param name="yourDic">数据字典</param>
        /// <param name="yourKey">数据索引</param>
        /// <returns>具体含义</returns>
        public static string findInterpretation(Dictionary<byte, string> yourDic, byte[] yourKey)
        {
            if (yourKey == null)
            {
                return "null";
            }
            else
            {
                byte nowKey = yourKey[yourKey.Length - 1];
                return findInterpretation(yourDic, nowKey);
            }
        }

        /// <summary>
        /// 获取vane配置报文的Contents长度
        /// </summary>
        /// <param name="yourContents"></param>
        /// <returns></returns>
        public static int getVaneContentsLen(Dictionary<byte[] ,byte[]> yourContents)
        {
            int tempLen = 0;
            foreach (KeyValuePair<byte[], byte[]> tempKvp in yourContents)
            {
                tempLen += (tempKvp.Value.Length + 4);
            }
            return tempLen;
        }

        /// <summary>
        /// 局部获取vaneContent里的可呈现值
        /// </summary>
        /// <param name="yourContent">ContentPair</param>
        /// <returns>可呈现值</returns>
        public static string getVaneContentVaule(KeyValuePair<byte[], byte[]> yourContent)
        {
            if (yourContent.Key == null || yourContent.Value == null)
            {
                return "Content Error";
            }
            string tempBack = "null";
            switch (yourContent.Key[yourContent.Key.Length - 1])
            {
                case 0x81:
                    tempBack = findInterpretation(myShareData.myProtocolTypeDictionary, yourContent.Value); 
                    break;
                case 0x82:
                    tempBack = getHexByBytes(yourContent.Value);
                    break;
                case 0x83:
                    tempBack = getHexByBytes(yourContent.Value);
                    break;
                case 0x84:
                    tempBack = findInterpretation(myShareData.myStatusDictionary, yourContent.Value); 
                    break;
                case 0x85:
                    tempBack = getHexByBytes(yourContent.Value);
                    break;
                case 0x86:
                    tempBack = getUtf8StrByBytes(yourContent.Value);
                    break;
                case 0x87:
                    tempBack = getUtf8StrByBytes(yourContent.Value);
                    break;
                case 0x88:
                    tempBack = getHexByBytes(yourContent.Value);
                    break;
                case 0x89:
                    tempBack = getIpByBytes(yourContent.Value); 
                    break;
                case 0x8a:
                    tempBack = (getByteLen(yourContent.Value)).ToString();
                    break;
                case 0x8b:
                    tempBack = getUtf8StrByBytes(yourContent.Value); 
                    break;
                case 0x8c:
                    tempBack = getHexByBytes(yourContent.Value);
                    break;
                case 0x8d:
                    tempBack = getHexByBytes(yourContent.Value);
                    break;
                case 0x8e:
                    tempBack = (getByteLen(yourContent.Value)).ToString();
                    break;
                case 0x8f:
                    tempBack = getHexByBytes(yourContent.Value);
                    break;
                case 0x91:
                    tempBack = getUtf8StrByBytes(yourContent.Value);
                    break;
                case 0x92:
                    tempBack = getUtf8StrByBytes(yourContent.Value);
                    break;
                case 0x93:
                    tempBack = (getByteLen(yourContent.Value)).ToString();
                    break;
                case 0x94:
                    tempBack = (getByteLen(yourContent.Value)).ToString();
                    break;
                case 0x95:
                    tempBack = getHexByBytes(yourContent.Value);
                    break;
                case 0x96:
                    tempBack = getHexByBytes(yourContent.Value);
                    break;
                case 0x97:
                    tempBack = getUtf8StrByBytes(yourContent.Value); 
                    break;
                case 0x98:
                    tempBack = getHexByBytes(yourContent.Value);
                    break;
                case 0x99:
                    tempBack = getUtf8StrByBytes(yourContent.Value); 
                    break;
                case 0x9a:
                    tempBack = getHexByBytes(yourContent.Value);
                    break;
                case 0x9b:
                    tempBack = getHexByBytes(yourContent.Value);
                    break;
                case 0x9c:
                    tempBack = (getByteLen(yourContent.Value)).ToString();
                    break;
                case 0x9d:
                    tempBack = findInterpretation(myShareData.myNewVersionFlagDictionary, yourContent.Value); 
                    break;
                case 0x9e:
                    tempBack = getHexByBytes(yourContent.Value);
                    break;
                case 0x9f:
                    tempBack = getUtf8StrByBytes(yourContent.Value); 
                    break;
                case 0xa0:
                    tempBack = getUtf8StrByBytes(yourContent.Value); 
                    break;
                default:
                    tempBack = "unkonw: " + getHexByBytes(yourContent.Value);
                    break;
            }
            return tempBack;
        }

        /// <summary>
        /// 局部获取vaneContent里的可呈现值(针对WIFI配置协议)
        /// </summary>
        /// <param name="yourContent">ContentPair</param>
        /// <returns>可呈现值</returns>
        public static string getVaneWifiContentVaule(KeyValuePair<byte[], byte[]> yourContent)
        {
            if (yourContent.Key == null || yourContent.Value == null)
            {
                return "Content Error";
            }
            string tempBack = "null";
            switch (yourContent.Key[yourContent.Key.Length - 1])
            {
                case 0x81:
                    tempBack = findInterpretation(myShareData.myProtocolTypeDictionary, yourContent.Value);
                    break;
                case 0x82:
                    tempBack = getHexByBytes(yourContent.Value);
                    break;
                case 0x83:
                    tempBack = getHexByBytes(yourContent.Value);
                    break;
                case 0x84:
                    tempBack = findInterpretation(myShareData.myStatusDictionary, yourContent.Value);
                    break;
                case 0x85:
                    tempBack = getUtf8StrByBytes(yourContent.Value);
                    break;
                case 0x86:
                    tempBack = getUtf8StrByBytes(yourContent.Value);
                    break;
                case 0x87:
                    tempBack = getHexByBytes(yourContent.Value); 
                    break;
                default:
                    tempBack = "unkonw: " + getHexByBytes(yourContent.Value);
                    break;
            }
            return tempBack;
        }

        /// <summary>
        /// 生成随机字节数组
        /// </summary>
        /// <param name="byteLen">数组长度</param>
        /// <returns>随机字节数组</returns>
        public static byte[] creatTransactionID(int byteLen)
        {
            byte[] tempBytes = new byte[byteLen];
            for (int i = 0; i < byteLen; i++)
            {
                Random ran = new Random(Guid.NewGuid().GetHashCode());
                {
                    tempBytes[i] = (byte)ran.Next(0x00, 0xff);
                }
            }
            return tempBytes;
        }
    }

    public class myVaneConfigRequestData
    {
        private byte[] myMagicNumber = myShareData.nowMagicNumber;
        private byte[] mySequenceNumber = myShareData.nowSequenceNumber;
        private byte myContentTypeApptoGw = myShareData.nowContentTypeApptoGw;
        private byte myContentTypeGwtoApp = myShareData.nowContentTypeGwtoApp;
        private byte myVanelifeSmartConfiguration = myShareData.nowVanelifeSmartConfiguration;

        private byte[] myProtocolVersion = myShareData.nowProtocolVersion;

        private byte[] _myRunTimeGwID;
        /// <summary>
        /// 设置或获取运行时GWID
        /// </summary>
        public byte[] myRunTimeGwID
        {
            get
            {
                return _myRunTimeGwID;
            }
            set
            {
                _myRunTimeGwID = value;
            }
        }

        private byte[] _myRunTimeGwToken;
        /// <summary>
        /// 设置或获取运行时GwToken
        /// </summary>
        public byte[] myRunTimeGwToken
        {
            get
            {
                return _myRunTimeGwToken;
            }
            set
            {
                _myRunTimeGwToken = value;
            }
        }

        public byte[] creatRequestData(vaneConfig yourvaneConfig)
        {
            return myVaneConfigTool.createDataToSent(yourvaneConfig);
        }

        /// <summary>
        /// 生成网关登录数据
        /// </summary>
        /// <param name="yourGW_ID">网关ID</param>
        /// <param name="yourGW_Key">网关密码</param>
        /// <returns>请求数据</returns>
        public byte[] devConfigurationQuery(string yourGW_ID, string yourGW_Key)
        {
            vaneConfig tempVaneConfig = new vaneConfig("");
            tempVaneConfig.MagicNumber = myMagicNumber;
            tempVaneConfig.SequenceNumber = mySequenceNumber;
            tempVaneConfig.ContentType = myContentTypeApptoGw;

            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x81 }, new byte[] { 0x00, 0x04 });
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x82 }, myVaneConfigTool.creatTransactionID(4));
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x83 }, myProtocolVersion);

            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x86 }, myVaneConfigTool.getUtf8BytesByStr(yourGW_ID));
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x8b }, myVaneConfigTool.getUtf8BytesByStr(yourGW_Key));

            tempVaneConfig.MsgLen = myVaneConfigTool.getVaneContentsLen(tempVaneConfig.Contents);

            return creatRequestData(tempVaneConfig);
        }

        /// <summary>
        /// 修改官网信息
        /// </summary>
        /// <param name="yourGW_Name">网关别名</param>
        /// <param name="yourGW_Key">网关KEY</param>
        /// <returns>请求数据</returns>
        public byte[] devConfigurationModifyRequest(string yourGW_Name, string yourGW_Key)
        {
            vaneConfig tempVaneConfig = new vaneConfig("");
            tempVaneConfig.MagicNumber = myMagicNumber;
            tempVaneConfig.SequenceNumber = mySequenceNumber;
            tempVaneConfig.ContentType = myContentTypeApptoGw;

            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x81 }, new byte[] { 0x00, 0x06 });
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x82 }, myVaneConfigTool.creatTransactionID(4));
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x83 }, myProtocolVersion);

            if (_myRunTimeGwID != null)
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x86 }, _myRunTimeGwID);
            }
            if (_myRunTimeGwToken != null)
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x98 }, _myRunTimeGwToken);
            }

            if (yourGW_Name != "")
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x87 }, myVaneConfigTool.getUtf8BytesByStr(yourGW_Name));
            }
            if (yourGW_Key != "")
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x8b }, myVaneConfigTool.getUtf8BytesByStr(yourGW_Key));
            }

            tempVaneConfig.MsgLen = myVaneConfigTool.getVaneContentsLen(tempVaneConfig.Contents);

            return creatRequestData(tempVaneConfig);
        }


        /// <summary>
        /// EP列表
        /// </summary>
        /// <returns>请求数据</returns>
        public byte[] devEpListRequest()
        {
            vaneConfig tempVaneConfig = new vaneConfig("");
            tempVaneConfig.MagicNumber = myMagicNumber;
            tempVaneConfig.SequenceNumber = mySequenceNumber;
            tempVaneConfig.ContentType = myContentTypeApptoGw;

            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x81 }, new byte[] { 0x00, 0x08 });
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x82 }, myVaneConfigTool.creatTransactionID(4));
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x83 }, myProtocolVersion);

            if (_myRunTimeGwID != null)
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x86 }, _myRunTimeGwID);
            }
            if (_myRunTimeGwToken != null)
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x98 }, _myRunTimeGwToken);
            }

            tempVaneConfig.MsgLen = myVaneConfigTool.getVaneContentsLen(tempVaneConfig.Contents);

            return creatRequestData(tempVaneConfig);
        }

        /// <summary>
        /// 添加EP
        /// </summary>
        /// <param name="EpId">EPID</param>
        /// <param name="EpName">EP名称</param>
        /// <returns>请求数据</returns>
        public byte[] devEpAddRequest(string EpId, string EpName)
        {
            vaneConfig tempVaneConfig = new vaneConfig("");
            tempVaneConfig.MagicNumber = myMagicNumber;
            tempVaneConfig.SequenceNumber = mySequenceNumber;
            tempVaneConfig.ContentType = myContentTypeApptoGw;

            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x81 }, new byte[] { 0x00, 0x0A });
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x82 }, myVaneConfigTool.creatTransactionID(4));
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x83 }, myProtocolVersion);

            if (_myRunTimeGwID != null)
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x86 }, _myRunTimeGwID);
            }
            if (_myRunTimeGwToken != null)
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x98 }, _myRunTimeGwToken);
            }

            if (EpId != "")
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x91 }, myVaneConfigTool.getUtf8BytesByStr(EpId));
            }
            if (EpName != "")
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x92 }, myVaneConfigTool.getUtf8BytesByStr(EpName));
            }

            tempVaneConfig.MsgLen = myVaneConfigTool.getVaneContentsLen(tempVaneConfig.Contents);

            return creatRequestData(tempVaneConfig);
        }

        /// <summary>
        /// 删除EP
        /// </summary>
        /// <param name="EpId">EPID</param>
        /// <returns>请求数据</returns>
        public byte[] devEpRemoveRequest(string EpId)
        {
            vaneConfig tempVaneConfig = new vaneConfig("");
            tempVaneConfig.MagicNumber = myMagicNumber;
            tempVaneConfig.SequenceNumber = mySequenceNumber;
            tempVaneConfig.ContentType = myContentTypeApptoGw;

            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x81 }, new byte[] { 0x00, 0x0C });
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x82 }, myVaneConfigTool.creatTransactionID(4));
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x83 }, myProtocolVersion);

            if (_myRunTimeGwID != null)
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x86 }, _myRunTimeGwID);
            }
            if (_myRunTimeGwToken != null)
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x98 }, _myRunTimeGwToken);
            }

            if (EpId != "")
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x91 }, myVaneConfigTool.getUtf8BytesByStr(EpId));
            }
          
            tempVaneConfig.MsgLen = myVaneConfigTool.getVaneContentsLen(tempVaneConfig.Contents);

            return creatRequestData(tempVaneConfig);
        }

        /// <summary>
        /// 修改EP别名
        /// </summary>
        /// <param name="EpId">EPID</param>
        /// <param name="EpName">EP名称</param>
        /// <returns>请求数据</returns>
        public byte[] devEpModifyRequest(string EpId, string EpName)
        {
            vaneConfig tempVaneConfig = new vaneConfig("");
            tempVaneConfig.MagicNumber = myMagicNumber;
            tempVaneConfig.SequenceNumber = mySequenceNumber;
            tempVaneConfig.ContentType = myContentTypeApptoGw;

            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x81 }, new byte[] { 0x00, 0x0E });
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x82 }, myVaneConfigTool.creatTransactionID(4));
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x83 }, myProtocolVersion);

            if (_myRunTimeGwID != null)
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x86 }, _myRunTimeGwID);
            }
            if (_myRunTimeGwToken != null)
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x98 }, _myRunTimeGwToken);
            }

            if (EpId != "")
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x91 }, myVaneConfigTool.getUtf8BytesByStr(EpId));
            }
            if (EpName != "")
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x92 }, myVaneConfigTool.getUtf8BytesByStr(EpName));
            }

            tempVaneConfig.MsgLen = myVaneConfigTool.getVaneContentsLen(tempVaneConfig.Contents);

            return creatRequestData(tempVaneConfig);
        }

        /// <summary>
        /// 新版本检查
        /// </summary>
        /// <returns>请求数据</returns>
        public byte[] devNewVersionCheckRequest()
        {
            vaneConfig tempVaneConfig = new vaneConfig("");
            tempVaneConfig.MagicNumber = myMagicNumber;
            tempVaneConfig.SequenceNumber = mySequenceNumber;
            tempVaneConfig.ContentType = myContentTypeApptoGw;

            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x81 }, new byte[] { 0x00, 0x15 });
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x82 }, myVaneConfigTool.creatTransactionID(4));
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x83 }, myProtocolVersion);

            if (_myRunTimeGwID != null)
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x86 }, _myRunTimeGwID);
            }
            if (_myRunTimeGwToken != null)
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x98 }, _myRunTimeGwToken);
            }

            tempVaneConfig.MsgLen = myVaneConfigTool.getVaneContentsLen(tempVaneConfig.Contents);

            return creatRequestData(tempVaneConfig);
        }

        /// <summary>
        /// 升级请求
        /// </summary>
        /// <param name="yourType">设备类型</param>
        /// <param name="yourVer">目标版本</param>
        /// <param name="yourEpId">EUI(填null为不传)</param>
        /// <returns>请求数据</returns>
        public byte[] devNewVersionUpgradeRequest(int yourType,byte[] yourVer,string yourEpId)
        {
            vaneConfig tempVaneConfig = new vaneConfig("");
            tempVaneConfig.MagicNumber = myMagicNumber;
            tempVaneConfig.SequenceNumber = mySequenceNumber;
            tempVaneConfig.ContentType = myContentTypeApptoGw;

            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x81 }, new byte[] { 0x00, 0x17 });
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x82 }, myVaneConfigTool.creatTransactionID(4));
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x83 }, myProtocolVersion);

            if (_myRunTimeGwID != null)
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x86 }, _myRunTimeGwID);
            }
            if (_myRunTimeGwToken != null)
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x98 }, _myRunTimeGwToken);
            }

            //creat ver info

            byte[] myUpVerCmd = new byte[] { 0x00, 0xA5 };
            myUpVerCmd = (myUpVerCmd.Concat(myVaneConfigTool.createInt16Bytes(2))).ToArray();
            myUpVerCmd = (myUpVerCmd.Concat(new byte[] { 0x00, (byte)yourType})).ToArray();

            if (yourVer != null)
            {
                myUpVerCmd = (myUpVerCmd.Concat(new byte[] { 0x00, 0xA8 })).ToArray();
                myUpVerCmd = (myUpVerCmd.Concat(myVaneConfigTool.createInt16Bytes(yourVer.Length))).ToArray();
                myUpVerCmd = (myUpVerCmd.Concat(yourVer)).ToArray();
            }

            if (yourEpId != null)
            {
                myUpVerCmd = (myUpVerCmd.Concat(new byte[] { 0x00, 0x91 })).ToArray();
                myUpVerCmd = (myUpVerCmd.Concat(myVaneConfigTool.createInt16Bytes(myVaneConfigTool.getUtf8BytesByStr(yourEpId).Length))).ToArray();
                myUpVerCmd = (myUpVerCmd.Concat(myVaneConfigTool.getUtf8BytesByStr(yourEpId))).ToArray();
            }

            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0xA7 }, myUpVerCmd);

            tempVaneConfig.MsgLen = myVaneConfigTool.getVaneContentsLen(tempVaneConfig.Contents);

            return creatRequestData(tempVaneConfig);
        }

        /// <summary>
        /// 一键添加
        /// </summary>
        /// <returns>请求数据</returns>
        public byte[] devAddAllStartRequest()
        {
            vaneConfig tempVaneConfig = new vaneConfig("");
            tempVaneConfig.MagicNumber = myMagicNumber;
            tempVaneConfig.SequenceNumber = mySequenceNumber;
            tempVaneConfig.ContentType = myContentTypeApptoGw;

            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x81 }, new byte[] { 0x00, 0x12 });
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x82 }, myVaneConfigTool.creatTransactionID(4));
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x83 }, myProtocolVersion);

            if (_myRunTimeGwID != null)
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x86 }, _myRunTimeGwID);
            }
            if (_myRunTimeGwToken != null)
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x98 }, _myRunTimeGwToken);
            }

            tempVaneConfig.MsgLen = myVaneConfigTool.getVaneContentsLen(tempVaneConfig.Contents);

            return creatRequestData(tempVaneConfig);
        }

        /// <summary>
        /// 停止一键添加
        /// </summary>
        /// <returns>请求数据</returns>
        public byte[] devAddAllStopRequest()
        {
            vaneConfig tempVaneConfig = new vaneConfig("");
            tempVaneConfig.MagicNumber = myMagicNumber;
            tempVaneConfig.SequenceNumber = mySequenceNumber;
            tempVaneConfig.ContentType = myContentTypeApptoGw;

            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x81 }, new byte[] { 0x00, 0x1E });
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x82 }, myVaneConfigTool.creatTransactionID(4));
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x83 }, myProtocolVersion);

            if (_myRunTimeGwID != null)
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x86 }, _myRunTimeGwID);
            }
            if (_myRunTimeGwToken != null)
            {
                tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x98 }, _myRunTimeGwToken);
            }

            tempVaneConfig.MsgLen = myVaneConfigTool.getVaneContentsLen(tempVaneConfig.Contents);

            return creatRequestData(tempVaneConfig);
        }


        /// <summary>
        /// 心跳
        /// </summary>
        /// <returns>请求数据</returns>
        public byte[] devHeartBeatRequest()
        {
            vaneConfig tempVaneConfig = new vaneConfig("");
            tempVaneConfig.MagicNumber = myMagicNumber;
            tempVaneConfig.SequenceNumber = mySequenceNumber;
            tempVaneConfig.ContentType = myContentTypeApptoGw;

            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x81 }, new byte[] { 0x00, 0x01 });
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x82 }, myVaneConfigTool.creatTransactionID(4));
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x83 }, myProtocolVersion);

            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x85 }, new byte[]{0x01});

            tempVaneConfig.MsgLen = myVaneConfigTool.getVaneContentsLen(tempVaneConfig.Contents);

            return creatRequestData(tempVaneConfig);
        }

        /// <summary>
        /// 生成wifi配置广播报文
        /// </summary>
        /// <param name="yourSSID">SSID</param>
        /// <param name="yourKey">密码</param>
        /// <param name="yourMode">加密模式</param>
        /// <returns>报文数据</returns>
        public byte[] vaneWifiConfigRequest(string yourSSID,string yourKey,int yourMode)
        {
            vaneConfig tempVaneConfig = new vaneConfig("");
            tempVaneConfig.MagicNumber = myMagicNumber;
            tempVaneConfig.SequenceNumber = mySequenceNumber;
            tempVaneConfig.ContentType = myVanelifeSmartConfiguration;

            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x81 }, new byte[] { 0x00, 0xA0 });
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x82 }, myVaneConfigTool.creatTransactionID(4));
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x83 }, myProtocolVersion);

            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x85 }, myVaneConfigTool.getUtf8BytesByStr(yourSSID));
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x86 }, myVaneConfigTool.getUtf8BytesByStr(yourKey));
            tempVaneConfig.Contents.Add(new byte[] { 0x00, 0x87 }, new byte[] { 0x00,0x00,0x00,(byte)yourMode });

            tempVaneConfig.MsgLen = myVaneConfigTool.getVaneContentsLen(tempVaneConfig.Contents);

            return creatRequestData(tempVaneConfig);
        }

    }
}
