using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;


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
    static class myShareData
    {
        //vaneConfig协议字典
        public static Dictionary<byte, string> myAssignedNumberDictionary = new Dictionary<byte, string>();          //协议类型
        public static Dictionary<byte, string> myProtocolTypeDictionary = new Dictionary<byte, string>();            //报文类型
        public static Dictionary<byte, string> myStatusDictionary = new Dictionary<byte, string>();                  //状态
        public static Dictionary<byte, string> myContentTypeDictionary = new Dictionary<byte, string>();             //报文协议
        public static Dictionary<byte, string> myNewVersionFlagDictionary = new Dictionary<byte, string>();          //版本状态
        public static Dictionary<byte, string> myGWAbilityDictionary = new Dictionary<byte, string>();               //网关能力
        public static Dictionary<byte, string> myRouterAuthModeDictionary = new Dictionary<byte, string>();          //路由加密模式
        public static Dictionary<byte, string> myWifiAssignedNumberDictionary = new Dictionary<byte, string>();      //协议类型(WIFI配置影响)
        public static Dictionary<byte, string> myVersionTypeNumberDictionary = new Dictionary<byte, string>();       //版本状态

        //vaneConfig报文默认值
        public static byte[] nowMagicNumber = new byte[] { 0xdc, 0xc4, 0xc7, 0x6d };
        public static byte[] nowSequenceNumber = new byte[] { 0x00, 0x00, 0x00, 0x00 };
        public static byte nowContentTypeApptoGw = 0x0a;
        public static byte nowContentTypeGwtoApp = 0x14;
        public static byte nowVanelifeSmartConfiguration = 0x1e;
        public static byte[] nowProtocolVersion = new byte[] { 0x00, 0x01, 0x00, 0x00 };

        //vaneAT基础设置


        //广播端口
        public static int sdBroadcastPort = 12713;//12713
        public static int sdWifiCfgPort = 19980;

        //广播时效
        public static int sdWifiCfgBroadcastLifeTime = 30;

        //UI 位置
        public static Point sdExpandablePanel_dataAdd_Position;
        public static Point sdExpandablePanel_testMode_Position;
    }
}
