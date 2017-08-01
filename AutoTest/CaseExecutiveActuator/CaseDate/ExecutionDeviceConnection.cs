using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    #endregion

}
