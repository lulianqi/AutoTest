using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;


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

    /// <summary>
    ///校验位
    ///None 不发生奇偶校验检查。 
    ///Odd 设置奇偶校验位，使位数等于奇数。 
    ///Even 设置奇偶校验位，使位数等于偶数。 
    ///Mark 将奇偶校验位保留为 1。 
    ///Space 将奇偶校验位保留为 0。 
    /// </summary>
    public enum SerialPortParity
    {
        None,
        Odd,
        Even,
        Mark,
        Space
    }

    /// <summary>
    /// 停止位
    /// None 不使用停止位。 StopBits  属性不支持此值。  
    ///One 使用一个停止位。 
    ///Two 使用两个停止位。 
    ///OnePointFive 使用 1.5 个停止位。 
    /// </summary>
    public enum SerialPortStopBits
    {
        None,
        One,
        Two,
        OnePointFive
    }


    public class MySerialPort
    {
        private string myErrorMes = "";
        public SerialPort comm;
        private StringBuilder myBuilder ;
        private bool isWantByte = false;
        private bool isAutoReceive = false;

        //public Encoding myEncoding = System.Text.Encoding.GetEncoding("GB2312");
        //public string myNewLine = "\r\n";

        /// <summary>
        /// get Serial name list
        /// </summary>
        /// <returns>names</returns>
        public static string[] MyGetSerialList()
        {
            return SerialPort.GetPortNames();
        }



        //ReceiveData
        public delegate void delegateReceiveData(byte[] yourbytes, string yourStr);
        /// <summary>
        /// if you isControlInvoke is true it will do the event in your contorl thread else it will do in SerialPort thread(do not cost to much)
        /// </summary>
        public event delegateReceiveData OnMySerialPortReceiveData;


        //Trigger Error
        public delegate void delegateThrowError(string errorMes);
        public event delegateThrowError OnMySerialPortThrowError;

        #region attribute
        /// <summary>
        /// get now Erroer Message
        /// </summary>
        public string ErroerMessage
        {
            get
            {
                return myErrorMes;
            }
        }

        /// <summary>
        /// get is the com opened
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return comm.IsOpen;
            }
        }

        /// <summary>
        /// get or set that wath data you want (string or bytes)
        /// </summary>
        public bool IsWantByte
        {
            get
            {
                return isWantByte;
            }
            set
            {
                isWantByte = value;
            }
        }

        public string PortName
        {
            get { return comm.PortName; }
            set { comm.PortName = value; }
        }

        public int BaudRate
        {
            get { return comm.BaudRate; }
            set { comm.BaudRate = value; }
        }

        /// <summary>
        ///校验位
        ///None 不发生奇偶校验检查。 
        ///Odd 设置奇偶校验位，使位数等于奇数。 
        ///Even 设置奇偶校验位，使位数等于偶数。 
        ///Mark 将奇偶校验位保留为 1。 
        ///Space 将奇偶校验位保留为 0。 
        /// </summary>
        public SerialPortParity Parity
        {
            get { return (SerialPortParity)Enum.Parse(typeof(SerialPortParity), comm.Parity.ToString()); }
            set { comm.Parity = (Parity)Enum.Parse(typeof(Parity), value.ToString()); }
        }

        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits
        {
            get { return comm.DataBits; }
            set { comm.DataBits = value; }
        }

        /// <summary>
        /// 停止位
        /// None 不使用停止位。 StopBits  属性不支持此值。  
        ///One 使用一个停止位。 
        ///Two 使用两个停止位。 
        ///OnePointFive 使用 1.5 个停止位。 
        /// </summary>
        public SerialPortStopBits StopBits
        {
            get { return (SerialPortStopBits)Enum.Parse(typeof(SerialPortStopBits), comm.StopBits.ToString()); }
            set { comm.StopBits = (StopBits)Enum.Parse(typeof(StopBits), value.ToString()); }
        }

        /// <summary>
        /// 获取或设置传输前后文本转换的字节编码
        /// SerialPort  类支持以下编码：ASCIIEncoding、UTF8Encoding、UnicodeEncoding、UTF32Encoding，以及 mscorlib.dll 中定义的、代码页小于 50000 或者为 54936 的所有编码。 可以使用备用编码，但必须使用 ReadByte 或 Write 方法并自己执行编码。 
        /// </summary>
        public Encoding Encoding
        {
            get { return comm.Encoding; }
            set { comm.Encoding = value; }
        } 
        #endregion

        public MySerialPort() : this(false) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="yourIsAutoReceive">是否使用事件模型接收数据，如果为true则需要订阅OnMySerialPortReceiveData</param>
        public MySerialPort(bool yourIsAutoReceive)
        {
            comm = new SerialPort();
            isAutoReceive = yourIsAutoReceive;
            if (isAutoReceive)
            {
                comm.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(Comm_DataReceived);
            }
            myBuilder = new StringBuilder();
        }

        #region action

        private void TriggerError(string yourMes)
        {
            myErrorMes = yourMes;
            if (OnMySerialPortThrowError != null)
            {
                OnMySerialPortThrowError(yourMes);
            }
        }

        //here i deal with the data i Received
        void Comm_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (isWantByte)
            {
                int tempLen = 0;
                try
                {
                    tempLen = comm.BytesToRead;
                }
                catch (Exception ex)
                {
                    TriggerError(ex.Message);
                    return;
                }
                if (tempLen > 0)
                {
                    byte[] tempBytes = new byte[tempLen];
                    int tempByte = 0;
                    for (int i = 0; i < tempLen; i++)
                    {
                        tempByte = comm.ReadByte();
                        if (tempByte == -1)
                        {
                            TriggerError("read the end in BytesToRead");
                            break;
                        }
                        else
                        {
                            tempBytes[i] = (byte)tempByte;
                        }
                    }
                    if (OnMySerialPortReceiveData != null)
                    {
                        OnMySerialPortReceiveData(tempBytes, null);
                    }
                }
                else
                {
                    TriggerError("received error len");
                }
            }
            else
            {
                if (comm.IsOpen)
                {
                    if (OnMySerialPortReceiveData != null)
                    {
                        OnMySerialPortReceiveData(null, comm.ReadExisting());
                    }
                }
                else
                {
                    TriggerError("the port is closed");
                }
            }
        } 
        #endregion


        #region function

        public bool OpenSerialPort()
        {
            if (comm == null)
            {
                TriggerError("this SerialPort is null");
                return false;
            }
            else
            {
                if (comm.IsOpen)
                {
                    TriggerError("this SerialPort is opened");
                    return false;
                }
                try
                {
                    comm.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    TriggerError(ex.Message);
                    return false;
                }
            }
        }

        public bool OpenSerialPort(string yourPortName, int yourBaudRate)
        {
            if (comm == null)
            {
                TriggerError("this SerialPort is null");
                return false;
            }
            else
            {
                comm.PortName = yourPortName;
                comm.BaudRate = yourBaudRate;
                return OpenSerialPort();
            }
        }

        /// <summary>
        /// close the SerialPort
        /// </summary>
        public void CloseSerialPort()
        {
            comm.DataReceived -= new SerialDataReceivedEventHandler(Comm_DataReceived);
            comm.Close();
        }

        /// <summary>
        /// 读取缓冲区所有字符串（使用指定编码comm.Encoding）
        /// </summary>
        /// <returns>接收到字符串，没有数据时返回""</returns>
        public string ReadAllStr()
        {
            if (isAutoReceive)
            {
                throw (new Exception("if you want read just set your isAutoReceive false"));
            }
            return comm.ReadExisting();
        }


        /// <summary>
        /// 读取缓冲区所有字节
        /// </summary>
        /// <returns>接收到字节，没有数据时返回null</returns>
        public byte[] ReadAllBytes()
        {
            if (isAutoReceive)
            {
                throw (new Exception("if you want read just set your isAutoReceive false"));
            }
            int tempLen = 0;
            try
            {
                tempLen = comm.BytesToRead;
            }
            catch (Exception ex)
            {
                TriggerError(ex.Message);
                return null;
            }
            if (tempLen > 0)
            {
                byte[] tempBytes = new byte[tempLen];
                int tempByte = 0;
                for (int i = 0; i < tempLen; i++)
                {
                    tempByte = comm.ReadByte();
                    if (tempByte == -1)
                    {
                        TriggerError("read the end in BytesToRead");
                        break;
                    }
                    else
                    {
                        tempBytes[i] = (byte)tempByte;
                    }
                }
                return tempBytes;
            }
            return null;
        }

        public bool Send(byte[] yourData)
        {
            if (comm == null)
            {
                TriggerError("this SerialPort is null");
            }
            else
            {
                if (comm.IsOpen)
                {
                    try
                    {
                        comm.Write(yourData, 0, yourData.Length);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        TriggerError(ex.Message);
                    }
                }
                else
                {
                    TriggerError("this SerialPort is closed");
                }
            }
            return false;
        }

        public bool Send(string yourData)
        {
            if (comm == null)
            {
                TriggerError("this SerialPort is null");
            }
            else
            {
                if (comm.IsOpen)
                {
                    try
                    {
                        comm.Write(yourData);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        TriggerError(ex.Message);
                    }
                }
                else
                {
                    TriggerError("this SerialPort is closed");
                }
            }
            return false;
        }

        
        #endregion
        
    }
}
