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


namespace MyCommonTool
{
    public class mySerialPort
    {
        private System.Windows.Forms.Control myControl;
        private bool isControlInvoke  = false;

        private string myErrorMes = "";

        public SerialPort comm;
        private StringBuilder myBuilder ;
        private bool isWantByte = false;

        public string myNewLine = "\r\n";
        public Encoding myEncoding = System.Text.Encoding.GetEncoding("GB2312");

        //ReceiveData
        public delegate void delegateReceiveData(byte[] yourbytes, string yourStr);
        /// <summary>
        /// if you isControlInvoke is true it will do the event in your contorl thread else it will do in SerialPort thread(do not cost to much)
        /// </summary>
        public event delegateReceiveData OnMySerialPortReceiveData;


        //Trigger Error
        public delegate void delegateThrowError(string errorMes);
        public event delegateThrowError OnMySerialPortThrowError;

        /// <summary>
        /// get now Erroer Message
        /// </summary>
        public string myErroerMessage
        {
            get
            {
                return myErrorMes;
            }
        }

        /// <summary>
        /// get or set that wath data you want (string or bytes)
        /// </summary>
        public bool myIsByte
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

        /// <summary>
        /// get the mode the event will do ,if it is true the even will do in the control thread
        /// </summary>
        public bool myIsWantInvoke
        {
            get
            {
                return isControlInvoke;
            }
        }

        public mySerialPort()
        {
            creatNewSerialPort();
            myBuilder = new StringBuilder();
        }

        public mySerialPort(System.Windows.Forms.Control yourControl)
        {
            myControl = yourControl;
            isControlInvoke = true;
            creatNewSerialPort();
            myBuilder = new StringBuilder();
        }

        private void triggerError(string yourMes)
        {
            myErrorMes = yourMes;
            if (OnMySerialPortThrowError != null)
            {
                OnMySerialPortThrowError(yourMes);
            }
        }

        private void creatNewSerialPort()
        {
            comm = new SerialPort();
            //comm.NewLine = myNewLine;
            comm.Encoding = myEncoding;
            comm.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(comm_DataReceived);
        }

        /// <summary>
        /// get Serial name list
        /// </summary>
        /// <returns>names</returns>
        public string[] myGetSerialList()
        {
            return SerialPort.GetPortNames();
        }


        public bool openSerialPort(string yourPortName, int yourBaudRate)
        {
            if (comm == null)
            {
                myErrorMes = "this SerialPort is null";
                return false;
            }
            else
            {
                if (comm.IsOpen)
                {
                    myErrorMes = "this SerialPort is opened";
                    return false;
                }
                try
                {
                    comm.PortName = yourPortName;
                    comm.BaudRate = yourBaudRate;
                    comm.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    myErrorMes = ex.Message;
                    comm.DataReceived -= new SerialDataReceivedEventHandler(comm_DataReceived);
                    creatNewSerialPort();
                    return false;
                }
            }

        }

        /// <summary>
        /// close the SerialPort
        /// </summary>
        public void closeSerialPort()
        {
            comm.DataReceived -= new SerialDataReceivedEventHandler(comm_DataReceived);
            comm.Close();
        }

        //here i deal with the data i Received
        void comm_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
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
                    triggerError(ex.Message);
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
                            triggerError("read the end in BytesToRead");
                            break;
                        }
                        else
                        {
                            tempBytes[i] = (byte)tempByte;
                        }
                    }
                    if (isControlInvoke)
                    {
                        if (OnMySerialPortReceiveData != null)
                        {
                            myControl.Invoke(OnMySerialPortReceiveData, tempBytes, null);
                        }
                    }
                    else
                    {
                        if (OnMySerialPortReceiveData != null)
                        {
                            OnMySerialPortReceiveData(tempBytes, null);
                        }
                    }
                }
                else
                {
                    triggerError("received error len");
                }
            }
            else
            {
                if (comm.IsOpen)
                {
                    if (isControlInvoke)
                    {
                        if (OnMySerialPortReceiveData != null)
                        {
                            myControl.Invoke(OnMySerialPortReceiveData, null, comm.ReadExisting());
                            //OnMySerialPortReceiveData(null, comm.ReadExisting());
                        }
                    }
                    else
                    {
                        if (OnMySerialPortReceiveData != null)
                        {
                            OnMySerialPortReceiveData(null, comm.ReadExisting());
                        }
                    }
                }
                else
                {
                    triggerError("the port is closed");
                }
            }
        }

    }
}
