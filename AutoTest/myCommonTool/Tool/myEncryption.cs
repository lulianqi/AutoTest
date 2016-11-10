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

namespace MyCommonTool
{
    public class myEncryption
    {
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
