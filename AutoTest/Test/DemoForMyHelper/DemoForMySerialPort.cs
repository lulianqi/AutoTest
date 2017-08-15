using MyCommonHelper;
using MyCommonHelper.NetHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoForMyHelper
{
    class DemoForMySerialPort
    {
        MySerialPort myComm = new MySerialPort();
        public DemoForMySerialPort()
        {
            //myComm.BaudRate = 0;
            myComm.IsWantByte = false;
            myComm.PortName = "COM2";
            myComm.Encoding = Encoding.UTF8;
            myComm.comm.NewLine = "???????/////";
            myComm.OnMySerialPortReceiveData += myComm_OnMySerialPortReceiveData;
            myComm.OnMySerialPortThrowError += myComm_OnMySerialPortThrowError;
        }

        public void MySerialPortStart()
        {
            myComm.OpenSerialPort();
            for(int i =0 ;i<5;i++)
            {
                Console.ReadLine();
                myComm.Send("sss 订单");
            }

            for (int i = 0; i < 5; i++)
            {
                byte[] myBytes = Encoding.UTF8.GetBytes("sss 订单");
                Console.ReadLine();
                myComm.Send(myBytes);
            }

            //for (int i = 0; i < 3; i++)
            //{
            //    Console.ReadLine();
            //    Console.WriteLine(myComm.ReadAllStr());
            //}
            for (int i = 0; i < 3; i++)
            {
                Console.ReadLine();
                byte[] tp = myComm.ReadAllBytes();
                if (tp != null)
                {
                    Console.WriteLine(Encoding.UTF8.GetString(tp));
                }
                else
                {
                    Console.WriteLine("null");
                }
            }
        }

        void myComm_OnMySerialPortThrowError(string errorMes)
        {
            Console.WriteLine("find error > " + errorMes);
        }

        void myComm_OnMySerialPortReceiveData(byte[] yourbytes, string yourStr)
        {
            Console.WriteLine("get data" + yourStr);
        }
    }
}
