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
        private static Dictionary<HexaDecimal, int> DictionaryHexaDecimal = new Dictionary<HexaDecimal, int>() { { HexaDecimal.hex2, 8 }, { HexaDecimal.hex10, 3 }, { HexaDecimal.hex16, 2 } };
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

        /// <summary>
        /// 将字符串转换成16进制的可读字符串（使用默认UTF8编码）
        /// </summary>
        /// <param name="yourStr">用户字符串</param>
        /// <returns>返回结果</returns>
        public static string StringToHexString(string yourStr)
        {
            return StringToHexString(yourStr, Encoding.UTF8, HexaDecimal.hex16, ShowHexMode.space);
        }

        /// <summary>
        /// 将字符串转换成指定进制的可读字符串 （使用指定编码指定进制及指定格式）
        /// </summary>
        /// <param name="yourStr">用户字符串</param>
        /// <param name="encode">指定编码</param>
        /// <param name="hexaDecimal">指定进制</param>
        /// <param name="stringMode">指定格式</param>
        /// <returns>返回结果</returns>
        public static string StringToHexString(string yourStr, Encoding encode, HexaDecimal hexaDecimal, ShowHexMode stringMode)
        {
            byte[] tempBytes = encode.GetBytes(yourStr);
            return ByteToHexString(tempBytes, hexaDecimal, stringMode);
        }

        /// <summary>
        /// 将字节数组转换为指定进制的可读字符串
        /// </summary>
        /// <param name="yourBytes">需要转换的字节数组</param>
        /// <param name="hexDecimal">指定进制</param>
        /// <param name="stringMode">指定格式</param>
        /// <returns>返回结果</returns>
        public static string ByteToHexString(byte[] yourBytes, HexaDecimal hexDecimal, ShowHexMode stringMode)
        {
            // 如果只考虑16进制对格式没有特殊要求 可以直接使用 ((byte)233).ToString("X2"); 或 BitConverter.ToString(new byte[]{1,2,3,10,12,233})
            if(yourBytes==null)
            {
                return null;
            }
            StringBuilder result = new StringBuilder(DictionaryHexaDecimal[hexDecimal] + DictionaryShowHexMode[stringMode].Length);

            for (int i = 0; i < yourBytes.Length; i++)
            {
                result.Append(DictionaryShowHexMode[stringMode]);
                result.Append(Convert.ToString(yourBytes[i], (int)hexDecimal).PadLeft(DictionaryHexaDecimal[hexDecimal], '0'));
            }
            return result.ToString();
        }

        /// <summary>
        /// 将可读指定进制的数据转换为字节数组（因为用户数据可能会是非法数据，遇到非法数据非法会抛出异常）
        /// </summary>
        /// <param name="yourStr">需要转换的字符串</param>
        /// <param name="hexDecimal">指定进制</param>
        /// <param name="stringMode">指定格式</param>
        /// <returns>返回结果</returns>
        public static byte[] HexStringToByte(string yourStr, HexaDecimal hexDecimal, ShowHexMode stringMode)
        {
            string[] hexStrs;
            byte[] resultBytes;
            string modeStr = string.Empty;   //string.Empty 不等于 null
            if (stringMode != ShowHexMode.@null)
            {
                modeStr = DictionaryShowHexMode[stringMode];
            }
            if (modeStr == string.Empty)
            {
                if (yourStr.Length % DictionaryHexaDecimal[hexDecimal] != 0)
                {
                    throw new Exception("error leng of your data"); 
                }
                long tempHexNum = yourStr.Length / DictionaryHexaDecimal[hexDecimal];
                hexStrs = new string[tempHexNum];
                for (int startIndex = 0; startIndex < tempHexNum; startIndex++)
                {
                    hexStrs[startIndex] = yourStr.Substring(startIndex * DictionaryHexaDecimal[hexDecimal], DictionaryHexaDecimal[hexDecimal]);
                }
            }
            else
            {
                hexStrs = yourStr.Split(new string[]{modeStr}, StringSplitOptions.RemoveEmptyEntries);
            }
            try
            {
                resultBytes = new byte[hexStrs.Length];
                for (int i = 0; i < hexStrs.Length; i++)
                {
                    resultBytes[i] = Convert.ToByte(hexStrs[i], (int)hexDecimal);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("error data ,can not change it to your hex",ex); 
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
