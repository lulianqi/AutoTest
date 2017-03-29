using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    public class MyEncryption
    {
        /// <summary>
        /// hex 字符串显示时的分割方式
        /// </summary>
        public enum ShowHexMode
        {
            @null=0,    //不风格每个字节
            space =1,   //以空格分割
            spit0x=2,   //以0x分割
            spit_=3,    //以下划线分割
            spitM_ = 4  //以中划线分割

        }

        public static string StringToHexString(string yourStr, Encoding encode, int hexNum, ShowHexMode stringMode)
        {
            byte[] tempBytes = encode.GetBytes(yourStr);//按照指定编码将string编程字节数组
            string result = string.Empty;
            
            return result;
        }

        public static string ByteToHexString(byte[] yourBytes, int hexNum, ShowHexMode stringMode)
        {
            if(yourBytes==null)
            {
                return null;
            }
            string modeStr = string.Empty;
            StringBuilder result;
            int stringBuilderCapacity = 0;

            switch (stringMode)
            {
                case ShowHexMode.@null:
                    modeStr = null;
                    stringBuilderCapacity = yourBytes.Length * 2;
                    break;
                case ShowHexMode.space:
                    modeStr = " ";
                    stringBuilderCapacity = yourBytes.Length * 3;
                    break;
                case ShowHexMode.spit0x:
                    modeStr = "0x";
                    stringBuilderCapacity = yourBytes.Length * 4;
                    break;
                case ShowHexMode.spit_:
                    modeStr = "_";
                    stringBuilderCapacity = yourBytes.Length * 3;
                    break;
                case ShowHexMode.spitM_:
                    modeStr = "-";
                    stringBuilderCapacity = yourBytes.Length * 3;
                    break;
                default:
                    //no this way
                    break;
            }
            result = new StringBuilder(stringBuilderCapacity);
            for (int i = 0; i < yourBytes.Length; i++)
            {
                result.Append(modeStr);
                result.Append(Convert.ToString(yourBytes[i], hexNum));
            }
            return result.ToString();
        }

        /// <summary>
        /// MD5计算
        /// </summary>
        /// <param name="data">加密数据</param>
        /// <returns>加密结果</returns>
        public static string CreateMD5Key(string data)
        {
            byte[] result = Encoding.UTF8.GetBytes(data);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }
    }
}
