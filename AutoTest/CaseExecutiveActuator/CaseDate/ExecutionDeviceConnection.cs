using System;
using System.Collections.Generic;
using System.Linq;
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
*
* 历史记录:
* 日	  期:   201708001           修改: 李杰 15158155511
* 描    述: 拆分
*******************************************************************************/

namespace CaseExecutiveActuator
{

    #region ExecutionDevice初始化连接信息

    /// <summary>
    /// Vanelife_http 【IConnectExecutiveData】
    /// </summary>
    public class myConnectForVanelife_http : IConnectExecutiveData
    {
        public CaseProtocol caseProtocol;
        public string dev_key;
        public string dev_secret;
        public string default_url;

        public myConnectForVanelife_http(CaseProtocol yourCaseProtocol, string yourDev_key, string yourDev_secret, string yourDefault_url)
        {
            caseProtocol = yourCaseProtocol;
            dev_key = yourDev_key;
            dev_secret = yourDev_secret;
            default_url = yourDefault_url;
        }
        public CaseProtocol MyCaseProtocol
        {
            get
            {
                return caseProtocol;
            }
        }
    }

    /// <summary>
    /// Http 【IConnectExecutiveData】
    /// </summary>
    public class myConnectForHttp : IConnectExecutiveData
    {

        public CaseProtocol caseProtocol;
        public string default_url;

        public myConnectForHttp(CaseProtocol yourCaseProtocol, string yourDefault_url)
        {
            caseProtocol = yourCaseProtocol;
            default_url = yourDefault_url;
        }
        public CaseProtocol MyCaseProtocol
        {
            get
            {
                return caseProtocol;
            }
        }
    }

    /// <summary>
    /// Console 【IConnectExecutiveData】
    /// </summary>
    public class myConnectForConsole : IConnectExecutiveData
    {
        public CaseProtocol caseProtocol;

        public myConnectForConsole(CaseProtocol yourCaseProtocol)
        {
            caseProtocol = yourCaseProtocol;
        }
        public CaseProtocol MyCaseProtocol
        {
            get
            {
                return caseProtocol;
            }
        }
    }

    /// <summary>
    /// ActiveMQ 【IConnectExecutiveData】
    /// </summary>
    public class myConnectForActiveMQ : IConnectExecutiveData
    {
        public CaseProtocol caseProtocol;

        public string brokerUri;
        public string clientId;
        public string factoryUserName;
        public string factoryPassword;

        public List<string> queuesList;
        public List<KeyValuePair<string, string>> topicList;

        public myConnectForActiveMQ(CaseProtocol yourCaseProtocol, string yourBrokerUri, string yourClientId, string yourFactoryUserName, string yourFactoryPassword, List<string> yourQueueList, List<KeyValuePair<string, string>> yourTopicList)
        {
            caseProtocol = yourCaseProtocol;
            brokerUri = yourBrokerUri;
            clientId = yourClientId;
            factoryUserName = yourFactoryUserName;
            factoryPassword = yourFactoryPassword;
            queuesList = yourQueueList;
            topicList = yourTopicList;
        }
        public CaseProtocol MyCaseProtocol
        {
            get
            {
                return caseProtocol;
            }
        }
    }

    /// <summary>
    /// mysql 【IConnectExecutiveData】
    /// </summary>
    public class myConnectForMysql : IConnectExecutiveData
    {
        public CaseProtocol caseProtocol;

        public string connectStr;

        public myConnectForMysql(CaseProtocol yourCaseProtocol, string yourConnectStr)
        {
            caseProtocol = yourCaseProtocol;
            connectStr = yourConnectStr;
        }
        public CaseProtocol MyCaseProtocol
        {
            get
            {
                return caseProtocol;
            }
        }
    }

    /// <summary>
    /// tcp 【IConnectExecutiveData】
    /// </summary>
    public class myConnectForTcp : IConnectExecutiveData
    {
        public CaseProtocol caseProtocol;

        public string host;
        public int port;

        public myConnectForTcp(CaseProtocol yourCaseProtocol, string yourHost,int yourPort)
        {
            caseProtocol = yourCaseProtocol;
            host = yourHost;
            port = yourPort;
        }
        public CaseProtocol MyCaseProtocol
        {
            get
            {
                return caseProtocol;
            }
        }
    }

    /// <summary>
    /// ssh 【IConnectExecutiveData】
    /// </summary>
    public class myConnectForSsh : IConnectExecutiveData
    {
        public CaseProtocol caseProtocol;

        public string host;
        public string user;
        public string password;
        public string expectPattern;

        public myConnectForSsh(CaseProtocol yourCaseProtocol, string yourHost, string yourUser, string yourPassword, string yourExpectPattern)
        {
            caseProtocol = yourCaseProtocol;
            host = yourHost;
            user = yourUser;
            password = yourPassword;
            expectPattern = yourExpectPattern;
        }
        public CaseProtocol MyCaseProtocol
        {
            get
            {
                return caseProtocol;
            }
        }
    }

    /// <summary>
    /// com 【IConnectExecutiveData】
    /// </summary>
    public class myConnectForCom : IConnectExecutiveData
    {
        public CaseProtocol caseProtocol;

        public string portName;
        public int baudRate;
        public MyCommonHelper.NetHelper.SerialPortParity parity;
        public int dataBits;
        public MyCommonHelper.NetHelper.SerialPortStopBits stopBits;
        public Encoding encoding;

        public myConnectForCom(CaseProtocol yourCaseProtocol, string yourPortName, int yourBaudRate, MyCommonHelper.NetHelper.SerialPortParity yourParity, int yourDataBits, MyCommonHelper.NetHelper.SerialPortStopBits yourStopBits,Encoding yourEncoding)
        {
            caseProtocol = yourCaseProtocol;
            portName = yourPortName;
            baudRate = yourBaudRate;
            parity = yourParity;
            dataBits = yourDataBits;
            stopBits = yourStopBits;
            encoding = yourEncoding;
        }
        public CaseProtocol MyCaseProtocol
        {
            get
            {
                return caseProtocol;
            }
        }
    }


    #endregion

}
