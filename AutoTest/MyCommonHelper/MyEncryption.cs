﻿using System;
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
        private static Dictionary<HexaDecimal, int> DictionaryHexaDecimal = new Dictionary<HexaDecimal, int>() { { HexaDecimal.hex2, 8 }, { HexaDecimal.hex10, 3 }, { HexaDecimal.hex16, 3 } };
        private static Dictionary<ShowHexMode, string> DictionaryShowHexMode = new Dictionary<ShowHexMode, string>() { { ShowHexMode.@null ,""},{ShowHexMode.space," "},{ShowHexMode.spit_,"_"},{ShowHexMode.spitM_,"-"},{ShowHexMode.spit0b,"0b"},{ShowHexMode.spitSpace0b," 0b"},{ShowHexMode.spit0d,"0d"},{ShowHexMode.spitSpace0d," 0d"},{ShowHexMode.spit0x,"0x"},{ShowHexMode.spitSpace0x," 0x"} };
        /// <summary>
        /// hex 字符串显示时的分割方式
        /// </summary>
        public enum ShowHexMode
        {
            @null = 0,    //不风格每个字节
            space = 1,   //以空格分割
            spit0x = 2,   //以0x分割 (用于显示16进制)
            spitSpace0x = 3,   //以 0x分割 (用于显示16进制)
            spit0b = 4,   //以0b分割 (用于显示2进制)
            spitSpace0b = 5,   //以 0b分割 (用于显示2进制)
            spit0d = 6,   //以0d分割 (用于显示10进制)
            spitSpace0d = 7,   //以 0d分割 (用于显示10进制)
            spit_= 8,    //以下划线分割
            spitM_ = 9  //以中划线分割

        }

        /// <summary>
        /// 表示要代表数据的进制
        /// </summary>
        public enum HexaDecimal
        {
            hex2 = 2,
            hex10 = 10,
            hex16 = 16
        }

        public static string StringToHexString(string yourStr)
        {
            return StringToHexString(yourStr, Encoding.UTF8, HexaDecimal.hex16, ShowHexMode.spit0x);
        }
        public static string StringToHexString(string yourStr, Encoding encode, HexaDecimal hexaDecimal, ShowHexMode stringMode)
        {
            byte[] tempBytes = encode.GetBytes(yourStr);
            return ByteToHexString(tempBytes, hexaDecimal, stringMode);
        }

        public static string ByteToHexString(byte[] yourBytes, HexaDecimal hexaDecimal, ShowHexMode stringMode)
        {
            // 如果只考虑16进制对格式没有特殊要求 可以直接使用 ((byte)233).ToString("X2"); 或 BitConverter.ToString(new byte[]{1,2,3,10,12,233})
            if(yourBytes==null)
            {
                return null;
            }
            string modeStr = string.Empty;
            StringBuilder result = new StringBuilder(DictionaryHexaDecimal[hexaDecimal] + DictionaryShowHexMode[stringMode].Length);

            /*
            int stringBuilderCapacity = 0;
            switch (stringMode)
            {
                case ShowHexMode.@null:
                    modeStr = null;
                    stringBuilderCapacity = yourBytes.Length * 2;
                    break;
                case ShowHexMode.space:
                    modeStr = " ";
                    stringBuilderCapacity = yourBytes.Length * (2+1);
                    break;
                case ShowHexMode.spit0x:
                    modeStr = "0x";
                    stringBuilderCapacity = yourBytes.Length * (2+2);
                    break;
                case ShowHexMode.spitSpace0x:
                    modeStr = " 0x";
                    stringBuilderCapacity = yourBytes.Length * (2+3);
                    break;
                case ShowHexMode.spit0b:
                    modeStr = "0b";
                    stringBuilderCapacity = yourBytes.Length * (8+2);
                    break;
                case ShowHexMode.spitSpace0b:
                    modeStr = " 0b";
                    stringBuilderCapacity = yourBytes.Length * (8+3);
                    break;
                case ShowHexMode.spit0d:
                    modeStr = "0d";
                    stringBuilderCapacity = yourBytes.Length * (3+2);
                    break;
                case ShowHexMode.spitSpace0d:
                    modeStr = " 0d";
                    stringBuilderCapacity = yourBytes.Length * (3+3);
                    break;
                case ShowHexMode.spit_:
                    modeStr = "_";
                    stringBuilderCapacity = yourBytes.Length * (2+1);
                    break;
                case ShowHexMode.spitM_:
                    modeStr = "-";
                    stringBuilderCapacity = yourBytes.Length * (2+1);
                    break;
                default:
                    //no this way
                    break;
            }
            result = new StringBuilder(stringBuilderCapacity);
             * */

            for (int i = 0; i < yourBytes.Length; i++)
            {
                result.Append(modeStr);
                result.Append(Convert.ToString(yourBytes[i], (int)hexaDecimal).PadLeft(8,'0'));
            }
            return result.ToString();
        }

        public static byte[] HexStringToByte(string yourStr, int hexNum, ShowHexMode stringMode)
        {
            string[] hexStrs;
            byte[] resultBytes;
            string modeStr = string.Empty;   //string.Empty 不等于 null
            switch (stringMode)
            {
                case ShowHexMode.@null:
                    modeStr = null;
                    break;
                case ShowHexMode.space:
                    modeStr = " ";
                    break;
                case ShowHexMode.spit0x:
                    modeStr = "0x";
                    break;
                case ShowHexMode.spitSpace0x:
                    modeStr = " 0x";
                    break;
                case ShowHexMode.spit_:
                    modeStr = "_";
                    break;
                case ShowHexMode.spitM_:
                    modeStr = "-";
                    break;
                default:
                    //no this way
                    break;
            }
            if (modeStr==null)
            {
                if (yourStr.Length%2!=0)
                {
                    return null;
                }
                int tempHexNum = yourStr.Length / 2;
                hexStrs = new string[tempHexNum];
                for (int startIndex = 0; startIndex < tempHexNum; startIndex++)
                {
                    hexStrs[startIndex] = yourStr.Substring(startIndex * 2, 2);
                }
            }
            else
            {
                hexStrs = yourStr.Split(new string[]{modeStr}, StringSplitOptions.RemoveEmptyEntries);
            }
            resultBytes = new byte[hexStrs.Length];
            for (int i = 0; i < hexStrs.Length;i++ )
            {
                resultBytes[i] = Convert.ToByte(hexStrs[i], hexNum);
            }
            return resultBytes;
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
