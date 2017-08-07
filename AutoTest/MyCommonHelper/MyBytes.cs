using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCommonHelper
{
    public class MyBytes
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
            int tempLen = yourBytes.Length;
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
        /// i will change your bytes to int,and bytes can not more than 4 byte(小端序，低位在前)
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
        /// i will change a int to byte and change the 1,2(小端序，低位在前)
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
    }
}
