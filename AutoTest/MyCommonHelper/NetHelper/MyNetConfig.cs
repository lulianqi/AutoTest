using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Collections;
using System.Net.Sockets;
using System.Management;
using System.Net.NetworkInformation;
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

namespace MyCommonHelper.NetHelper
{
    public class MyNetConfig
    {
        /// <summary>
        /// 返回当前主机的所有广播地址
        /// </summary>
        /// <returns>BroadcasetAddress List</returns>
        public static IPAddress[] getBroadcasetAddress()
        {
            ArrayList arr = new ArrayList();

            ManagementObjectSearcher query = new
            ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'");
            ManagementObjectCollection queryCollection = query.Get();
            foreach (ManagementObject mo in queryCollection)
            {
                string[] addresses = (string[])mo["IPAddress"];
                string[] subnets = (string[])mo["IPSubnet"];
                string[] defaultgateways = (string[])mo["DefaultIPGateway"];
                byte[] tip;
                byte[] tsub;
                try
                {
                    tip = IPAddress.Parse(addresses[0]).GetAddressBytes();
                    tsub = IPAddress.Parse(subnets[0]).GetAddressBytes();
                }
                catch (FormatException)
                {
                    continue;
                }
                catch (Exception)
                {
                    continue;
                }
                for (int i = 0; i < tip.Length; i++)
                {
                    tip[i] = (byte)((~tsub[i]) | tip[i]);
                }

                //arr.Add(new IPAddress(tip));
                arr.MyAdd(new IPAddress(tip));
            }

            IPAddress[] ret = new IPAddress[arr.Count];
            int count = 0;
            foreach (Object obj in arr)
            {
                ret[count++] = (IPAddress)obj;
            }
            return ret;
        }

        /// <summary>
        /// 获取所有网卡的IPV4地址
        /// </summary>
        /// <returns></returns>
        public static IPAddress[] getNetworkInterfaceAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            List<IPAddress> arr = new List<IPAddress>();
            foreach (NetworkInterface adapter in nics)
            {
                //获取以太网卡网络接口信息
                IPInterfaceProperties ip = adapter.GetIPProperties();
                //获取单播地址集
                UnicastIPAddressInformationCollection ipCollection = ip.UnicastAddresses;
                foreach (UnicastIPAddressInformation ipadd in ipCollection)
                {
                    //InterNetwork    IPV4地址      InterNetworkV6        IPV6地址
                    //Max            MAX 位址
                    if (ipadd.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        arr.Add(ipadd.Address);
                    }
                }
            }
            return arr.ToArray();
        }

        /// <summary>
        /// 用于检查IP地址或域名是否可以使用TCP/IP协议访问(使用Ping命令),true表示Ping成功,false表示Ping失败 
        /// </summary>
        /// <param name="strIpOrDName">输入参数,表示IP地址或URL</param>
        /// <returns></returns>
        public static bool PingIpOrDomainName(string strIpOrDName)
        {
            try
            {
                IPAddress myIPAddress ;
                string myPingAddress;
                if (IPAddress.TryParse(strIpOrDName, out myIPAddress))
                {
                    myPingAddress = myIPAddress.ToString();
                }
                else
                {
                    Uri tempHost = new Uri(strIpOrDName);
                    myPingAddress = tempHost.Host;
                }
                Ping objPingSender = new Ping();
                PingOptions objPinOptions = new PingOptions();
                objPinOptions.DontFragment = true;
                string data = " ";
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                int intTimeout = 1000;
                PingReply objPinReply = objPingSender.Send(myPingAddress, intTimeout, buffer, objPinOptions);
                string strInfo = objPinReply.Status.ToString();
                if (strInfo == "Success")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.PutInLog(ex);
                return false;
            }
        }

    }
}
