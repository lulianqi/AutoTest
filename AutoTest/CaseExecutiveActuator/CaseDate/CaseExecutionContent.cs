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
    #region ICaseExecutionContent

    /// <summary>
    /// vanelife_http协议类型Content内容
    /// 处理时请注意除了ErrorMessage可能为null，其他成员初始化全部不为null(所有请覆盖默认构造函数，为成员指定初始值)
    /// </summary>
    public class MyVaneHttpExecutionContent : ICaseExecutionContent
    {
        public string errorMessage;
        public string httpTarget;
        public caseParameterizationContent caseExecutionContent;
        public string httpMethod;
        public string caseActuator;
        public CaseProtocol caseProtocol;
        public HttpAisleConfig myHttpAisleConfig;
        public HttpMultipart myHttpMultipart;

        public MyVaneHttpExecutionContent()
        {
            errorMessage = null;
            caseExecutionContent = new caseParameterizationContent();
            httpMethod = "";
            httpTarget = "";
            caseActuator = "";
            myHttpMultipart = new HttpMultipart();
            myHttpAisleConfig = new HttpAisleConfig();
            caseProtocol = CaseProtocol.unknownProtocol;
        }

        public MyVaneHttpExecutionContent(string tempVal)
        {
            errorMessage = null;
            caseExecutionContent = new caseParameterizationContent();
            httpMethod = tempVal;
            httpTarget = tempVal;
            caseActuator = tempVal;
            myHttpMultipart = new HttpMultipart();
            myHttpAisleConfig = new HttpAisleConfig();
            caseProtocol = CaseProtocol.unknownProtocol;
        }

        public CaseProtocol MyCaseProtocol
        {
            get
            {
                return caseProtocol;
            }
        }

        public string MyExecutionTarget
        {
            get
            {
                return httpTarget;
            }
        }

        public string MyExecutionContent
        {
            get
            {
                return caseExecutionContent.contentData;
            }
        }


        public string MyCaseActuator
        {
            get
            {
                return caseActuator;
            }
        }


        public string MyErrorMessage
        {
            get
            {
                return String.IsNullOrEmpty(errorMessage) ? null : errorMessage;
            }
        }


    }

    public class MyBasicHttpExecutionContent : ICaseExecutionContent
    {
        public string errorMessage;
        public CaseProtocol caseProtocol;
        public string caseActuator;

        public caseParameterizationContent httpUri;
        public string httpMethod;
        public List<KeyValuePair<string, caseParameterizationContent>> httpHeads;
        public caseParameterizationContent httpBody;
        public HttpAisleConfig myHttpAisleConfig;
        public List<HttpMultipart> myMultipartList;

        public MyBasicHttpExecutionContent()
        {
            errorMessage = null;
            caseProtocol = CaseProtocol.unknownProtocol;
            caseActuator = "";
            httpUri = new caseParameterizationContent();
            httpMethod = "";
            httpHeads = new List<KeyValuePair<string, caseParameterizationContent>>();
            httpBody = new caseParameterizationContent();
            myHttpAisleConfig = new HttpAisleConfig();
            myMultipartList = new List<HttpMultipart>();

        }

        public CaseProtocol MyCaseProtocol
        {
            get { return caseProtocol; }
        }

        public string MyCaseActuator
        {
            get { return caseActuator; }
        }

        public string MyExecutionTarget
        {
            get { return httpUri.GetTargetContentData(); }
        }

        public string MyExecutionContent
        {
            get { return httpBody.GetTargetContentData(); }
        }

        public string MyErrorMessage
        {
            get
            {
                return String.IsNullOrEmpty(errorMessage) ? null : errorMessage;
            }
        }
    }

    public class MyConsoleExecutionContent : ICaseExecutionContent
    {
        #region inner class
        public class StaticDataAdd
        {
            public CaseStaticDataType StaticDataType { get; set; }
            public String Name { get; set; }
            public caseParameterizationContent ConfigureData { get; set; }

            public StaticDataAdd(CaseStaticDataType yourStaticDataType, String yourName, caseParameterizationContent yourConfigureData)
            {
                StaticDataType = yourStaticDataType;
                Name = yourName;
                ConfigureData = yourConfigureData;
            }
        }

        #endregion

        public string errorMessage;
        public CaseProtocol caseProtocol;
        public string caseActuator;

        public caseParameterizationContent showContent;
        public List<KeyValuePair<string, caseParameterizationContent>> staticDataSetList;
        public List<StaticDataAdd> staticDataAddList;
        public List<KeyValuePair<bool, caseParameterizationContent>> staticDataDelList;

        public MyConsoleExecutionContent()
        {
            errorMessage = null;
            caseProtocol = CaseProtocol.unknownProtocol;
            caseActuator = "";
            showContent = new caseParameterizationContent();
            staticDataSetList = new List<KeyValuePair<string, caseParameterizationContent>>();
            staticDataAddList = new List<StaticDataAdd>();
            staticDataDelList = new List<KeyValuePair<bool, caseParameterizationContent>>();
        }

        public CaseProtocol MyCaseProtocol
        {
            get { return caseProtocol; }
        }

        public string MyCaseActuator
        {
            get { return caseActuator; }
        }

        public string MyExecutionTarget
        {
            get { return showContent.GetTargetContentData(); }
        }

        public string MyExecutionContent
        {
            get { return null; }
        }

        public string MyErrorMessage
        {
            get
            {
                return String.IsNullOrEmpty(errorMessage) ? null : errorMessage;
            }
        }
    }

    public class MyActiveMQExecutionContent : ICaseExecutionContent
    {
        #region inner class
        public class ConsumerData
        {
            public string ConsumerName { get; set; }
            public string ConsumerType { get; set; }
            public string ConsumerTopicDurable { get; set; }
            /// <summary>
            /// 构造函数 描述一个MQ消费者
            /// </summary>
            /// <param name="name">必填不能为null</param>
            /// <param name="type">必填不能为null</param>
            /// <param name="durable">可设置为null</param>
            public ConsumerData(string name, string type, string durable)
            {
                ConsumerName = name;
                ConsumerType = type;
                ConsumerTopicDurable = durable;
            }
        }

        public class ProducerData
        {
            public string ProducerName { get; set; }
            public string ProducerType { get; set; }
            public string MessageType { get; set; }

            /// <summary>
            /// 构造函数 描述一个MQ信息发布者
            /// </summary>
            /// <param name="name">必填不能为null</param>
            /// <param name="type">必填不能为null</param>
            /// <param name="messageType">必填不能为null</param>
            public ProducerData(string name, string type, string messageType)
            {
                ProducerName = name;
                ProducerType = type;
                MessageType = messageType;
            }
        }

        #endregion

        public string errorMessage;
        public CaseProtocol caseProtocol;
        public string caseActuator;

        public List<KeyValuePair<ProducerData, caseParameterizationContent>> producerDataSendList;
        public List<ConsumerData> consumerSubscribeList;
        public List<ConsumerData> unConsumerSubscribeList;
        public List<ConsumerData> consumerMessageReceiveList;

        public MyActiveMQExecutionContent()
        {
            errorMessage = null;
            caseProtocol = CaseProtocol.unknownProtocol;
            caseActuator = "";

            producerDataSendList = new List<KeyValuePair<ProducerData, caseParameterizationContent>>();
            consumerSubscribeList = new List<ConsumerData>();
            unConsumerSubscribeList = new List<ConsumerData>();
            consumerMessageReceiveList = new List<ConsumerData>();
        }

        public CaseProtocol MyCaseProtocol
        {
            get { return caseProtocol; }
        }

        public string MyCaseActuator
        {
            get { return caseActuator; }
        }

        public string MyExecutionTarget
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (consumerSubscribeList.Count > 0)
                {
                    sb.Append("Subscribe:");
                    foreach (ConsumerData tempConsumer in consumerSubscribeList)
                    {
                        sb.Append(string.Format(" {0}://{1} ", tempConsumer.ConsumerType, tempConsumer.ConsumerName));
                    }
                }
                if (unConsumerSubscribeList.Count > 0)
                {
                    sb.Append("UnSubscribe:");
                    foreach (ConsumerData tempConsumer in unConsumerSubscribeList)
                    {
                        sb.Append(string.Format(" {0}://{1} ", tempConsumer.ConsumerType, tempConsumer.ConsumerName));
                    }
                }
                if (producerDataSendList.Count > 0)
                {
                    sb.Append("Send:");
                    foreach (KeyValuePair<ProducerData, caseParameterizationContent> tempProducer in producerDataSendList)
                    {
                        sb.Append(string.Format(" {0}://{1} ", tempProducer.Key.ProducerType, tempProducer.Key.ProducerName));
                    }
                }
                if (consumerMessageReceiveList.Count > 0)
                {
                    sb.Append("Receive:");
                    foreach (ConsumerData tempConsumer in consumerMessageReceiveList)
                    {
                        sb.Append(string.Format(" {0}://{1} ", tempConsumer.ConsumerType, tempConsumer.ConsumerName));
                    }
                }
                return sb.ToString();
            }
        }

        public string MyExecutionContent
        {
            get { return null; }
        }

        public string MyErrorMessage
        {
            get
            {
                return String.IsNullOrEmpty(errorMessage) ? null : errorMessage;
            }
        }
    }

    public class MyMySqlExecutionContent : ICaseExecutionContent
    {
        public string errorMessage;
        public CaseProtocol caseProtocol;
        public string caseActuator;

        public caseParameterizationContent sqlContent;
        public bool isPosition;
        public int rowIndex;
        public int columnIndex;

        public MyMySqlExecutionContent()
        {
            errorMessage = null;
            caseProtocol = CaseProtocol.unknownProtocol;
            caseActuator = "";
            sqlContent = new caseParameterizationContent();
            isPosition = false;
        }

        public CaseProtocol MyCaseProtocol
        {
            get { return caseProtocol; }
        }

        public string MyCaseActuator
        {
            get { return caseActuator; }
        }

        public string MyExecutionTarget
        {
            get { return sqlContent.GetTargetContentData(); }
        }

        public string MyExecutionContent
        {
            get { return null; }
        }

        public string MyErrorMessage
        {
            get
            {
                return String.IsNullOrEmpty(errorMessage) ? null : errorMessage;
            }
        }
    }

    public class MyMySshExecutionContent : ICaseExecutionContent
    {
        public string errorMessage;
        public CaseProtocol caseProtocol;
        public string caseActuator;

        public caseParameterizationContent sshContent;


        public MyMySshExecutionContent()
        {
            errorMessage = null;
            caseProtocol = CaseProtocol.unknownProtocol;
            sshContent = new caseParameterizationContent();
        }

        public CaseProtocol MyCaseProtocol
        {
            get { return caseProtocol; }
        }

        public string MyCaseActuator
        {
            get { return caseActuator; }
        }

        public string MyExecutionTarget
        {
            get { return sshContent.GetTargetContentData(); }
        }

        public string MyExecutionContent
        {
            get { return null; }
        }

        public string MyErrorMessage
        {
            get
            {
                return String.IsNullOrEmpty(errorMessage) ? null : errorMessage;
            }
        }
    }
    
    #endregion
}
