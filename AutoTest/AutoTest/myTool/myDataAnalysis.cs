using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AutoTest.myTool;
using System.Runtime.InteropServices;
using System.Net;

using System.Collections;
using System.Security.Cryptography;
using MyCommonHelper;
using CaseExecutiveActuator;


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


namespace AutoTest.myTool
{
    static class myDataAnalysis
    {

        public static myDataIn myGetData(ref string myDataFromPOS)
        {
            myDataIn mdi;
            mdi=new myDataIn("");
            //mdi.caseID = mdi.interfaceName = mdi.interfaceOutData_1 = mdi.interfaceOutData_2 = mdi.isbreak = mdi.otherOutData = "";
            //mdi.ret = "";
            if(!myDataFromPOS.StartsWith("B"))
            {
                mdi.interfaceName="error of start";
                return mdi;
            }
            if (!myDataFromPOS.EndsWith("E"))
            {
                mdi.interfaceName = "error of end";
                return mdi;
            }

            try
            {
                myDataFromPOS = myDataFromPOS.Remove(0, 1);
                myDataFromPOS = myDataFromPOS.Remove(myDataFromPOS.Length - 1, 1);
                string[] sArray = myDataFromPOS.Split('#');
                mdi.caseID = sArray[0];
                mdi.interfaceName = sArray[1];
                mdi.ret = sArray[2];
                mdi.isbreak = sArray[3];
                mdi.interfaceOutData_1 = sArray[4];
                mdi.interfaceOutData_2 = sArray[5];
                mdi.otherOutData = sArray[6];
                return mdi;
            }
            catch (Exception ex)
            {
                mdi.interfaceName = "error of data";
                ErrorLog.PutInLog("ID:12427  " + ex.Message);
                return mdi;
            }
        }

        public static myDataOut myOutDataAnalysis(ref string myDataToPOS)
        {
            myDataOut mdt;
            int tempIndex = 0;
            int tempNum = 0;
            mdt = new myDataOut("");
            myDataToPOS = myDataToPOS.Remove(0, 1);
            myDataToPOS = myDataToPOS.Remove(myDataToPOS.Length - 1, 1);
            tempIndex=myDataToPOS.IndexOf('@');
            if (tempIndex >= 0)
            {
                mdt.longData = myDataToPOS.Remove(0, tempIndex + 1);
                myDataToPOS = myDataToPOS.Remove(tempIndex);
            }
            string[] sArray = myDataToPOS.Split('#');
            tempNum = sArray.Length;
            mdt.caseID = sArray[0];
            mdt.interfaceName = sArray[1];
            mdt.caseTime = sArray[2];
            if (tempNum <= 3)
            {
                return mdt;
            }
            mdt.interfaceOutData_1 = sArray[3];
            if (tempNum <= 4)
            {
                return mdt;
            }
            mdt.interfaceOutData_2 = sArray[4];
            if (tempNum <= 5)
            {
                return mdt;
            }
            mdt.interfaceOutData_3 = sArray[5];
            if (tempNum <= 6)
            {
                return mdt;
            }
            mdt.interfaceOutData_4 = sArray[6];
            if (tempNum <= 7)
            {
                return mdt;
            }
            mdt.interfaceOutData_5 = sArray[7];
            if (tempNum <= 8)
            {
                return mdt;
            }
            mdt.interfaceOutData_6 = sArray[8];
            if (tempNum <= 9)
            {
                return mdt;
            }
            mdt.interfaceOutData_7 = sArray[9];
            if (tempNum <= 10)
            {
                return mdt;
            }
            mdt.interfaceOutData_8 = sArray[10];
            return mdt;
            
        }

        public static string myCreateContent(string caseID, string interfaceName, string caseTime, string interfaceOutData_1, string interfaceOutData_2, string interfaceOutData_3, string interfaceOutData_4, string interfaceOutData_5, string interfaceOutData_6, string interfaceOutData_7, string interfaceOutData_8, string longData)
        {
            StringBuilder tempContent = new StringBuilder("B");
            tempContent.Append(caseID);
            tempContent.Append("#");
            tempContent.Append(interfaceName);
            tempContent.Append("#");
            tempContent.Append(caseTime);
            tempContent.Append("#");
            if (interfaceOutData_1 != "" && interfaceOutData_1 != "不可用")
            {
                tempContent.Append(interfaceOutData_1);
                tempContent.Append("#");
            }
            if (interfaceOutData_2 != "" && interfaceOutData_2 != "不可用")
            {
                tempContent.Append(interfaceOutData_2);
                tempContent.Append("#");
            }
            if (interfaceOutData_3 != "" && interfaceOutData_3 != "不可用")
            {
                tempContent.Append(interfaceOutData_3);
                tempContent.Append("#");
            }
            if (interfaceOutData_4 != "" && interfaceOutData_4 != "不可用")
            {
                tempContent.Append(interfaceOutData_4);
                tempContent.Append("#");
            }
            if (interfaceOutData_5 != "" && interfaceOutData_5 != "不可用")
            {
                tempContent.Append(interfaceOutData_5);
                tempContent.Append("#");
            }
            if (interfaceOutData_6 != "" && interfaceOutData_6 != "不可用")
            {
                tempContent.Append(interfaceOutData_6);
                tempContent.Append("#");
            }
            if (interfaceOutData_7 != "" && interfaceOutData_7 != "不可用")
            {
                tempContent.Append(interfaceOutData_7);
                tempContent.Append("#");
            }
            if (interfaceOutData_8 != "" && interfaceOutData_8 != "不可用")
            {
                tempContent.Append(interfaceOutData_8);
                tempContent.Append("#");
            }
            if (longData != "" && longData != "不可用")
            {
                tempContent.Append("@");
                tempContent.Append(longData);
            }
            tempContent.Append("E");
            return tempContent.ToString();
        }

        /// <summary>
        /// Combination of string increase spaces
        /// </summary>
        /// <param name="strA">stringA</param>
        /// <param name="strB">stringB</param>
        /// <param name="num">spaces number </param>
        /// <returns></returns>
        public static string myStringAdd(string strA ,string strB,int num)
        {
            try
            {
                StringBuilder tempContent = new StringBuilder(strA);
                while(strA.Length < num)
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

        /// <summary>
        /// 按tree的值定要求重新排列数据
        /// </summary>
        /// <param name="step">步骤</param>
        /// <param name="Content">数据源</param>
        /// <returns></returns>
        public static string myStringAddEx(string step, string Content)
        {
            try
            {
                int tempLength = step.Length;
                StringBuilder tempContent = new StringBuilder(Content);
                string mystr = "Step:" + step;
                for (int i = 0; i < 4 - tempLength; i++)
                {
                    mystr += " ";
                }
                tempContent.Remove(0, 1);
                tempContent.Remove(tempContent.Length - 1, 1);
                tempLength = Content.IndexOf('#')-1;
                tempContent.Remove(tempLength, 1);
                for (int i = 0; i <10 - tempLength; i++)
                {
                    tempContent.Insert(tempLength, " ");
                }
                mystr += ("ID:" + tempContent);
                return mystr;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// MD5计算
        /// </summary>
        /// <param name="data">加密数据</param>
        /// <returns>加密结果</returns>
        public static string createMD5Key(string data)
        {
            byte[] result = Encoding.UTF8.GetBytes(data);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }


        /// <summary>
        /// 生成测试数据
        /// </summary>
        /// <param name="testData">用例数据</param>
        /// <returns>测试数据</returns>
        /// 
        public static string myCreatSendData(string testData )
        {     
            Hashtable myDataTable = new Hashtable();
            StringBuilder myStrBld = new StringBuilder();
            string tempSign = "";
        
            #region 填装数据
            string[] sArray = testData.Split('&');
            if (testData == "")
            {
            }
            else
            {
                foreach (string tempStr in sArray)
                {
                    int myBreak = tempStr.IndexOf('=');
                    if (myBreak == -1)
                    {
                        return "can't find =";
                    }
                    myDataTable.Add(tempStr.Substring(0, myBreak), tempStr.Substring(myBreak + 1));
                }
            }
            //foreach (DictionaryEntry de in publicValue)
            //change here
            myDataTable.Add("key", myReceiveData.vaneApp_key);
            //myDataTable.Add("v", myReceiveData.vaneV);
            myDataTable.Add("timestamp", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
            #endregion
       
            #region 生成Sign
            ArrayList akeys = new ArrayList(myDataTable.Keys); 
            akeys.Sort();
            //myStrBld.Append(myReceiveData.vaneApp_secret);
            foreach(string tempKey in akeys)
            {
                myStrBld.Append(tempKey + myDataTable[tempKey]);
            }
            myStrBld.Append(myReceiveData.vaneApp_secret);
            tempSign= createMD5Key(myStrBld.ToString());
            #endregion

            #region 组合数据
            myStrBld.Remove(0, myStrBld.Length);
            //change here
            myStrBld.Append("signature=" + tempSign);
            foreach (DictionaryEntry de in myDataTable)
            {
                //myStrBld.Append("&" + de.Key + "=" + de.Value);
                //对每次参数进行url编码
                myStrBld.Append("&" + de.Key + "=" + System.Web.HttpUtility.UrlEncode((de.Value).ToString()));
            }
            return myStrBld.ToString();
            #endregion

        }

    }

    public static class myReceiveData
    {
        //for sunyard
        public static List<myDataIn> myData_ = new List<myDataIn>();   
        //for vane
        public static List<myAutoHttpTest> myData = new List<myAutoHttpTest>();
        public static List<myHttpBackData> myBackData = new List<myHttpBackData>();

        public static string vaneUrl;
        public static string vaneData;
        public static string vaneMethod;
        public static int sendNum;
        public static string vaneApp_key;
        public static string vaneV;
        public static string vaneApp_secret;


    }


    
}
