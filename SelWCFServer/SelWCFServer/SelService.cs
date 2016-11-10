using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace SelWCFServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    class SelService:ISelService
    {
        public event Action<object,string> ShowMesEvent;
       
        private void ShowMes(string mes)
        {
            if(ShowMesEvent!=null)
            {
                this.ShowMesEvent(this, string.Format("NowThread ID:{0} Mes:{1}", Thread.CurrentThread.ManagedThreadId,mes));
            }

            if(MessageTransferChannel.MessageCallback!=null)
            {
                MessageTransferChannel.MessageCallback(this, string.Format("NowThread ID:{0} Mes:{1}", Thread.CurrentThread.ManagedThreadId, mes));
            }
        }
        public string SayHello(int vaule)
        {
            ShowMes("Call SayHello");
            return "Hello For" + vaule;
        }

        public void SayBye(int vaule)
        {
            ShowMes("Call SayBye");
            string str = "Bye For" + vaule;
        }

        public string WhoIs(MyData myData)
        {
            ShowMes("Call WhoIs");
            return myData.Name + "  High " + myData.High;
        }


       
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    class DuplexService:IDuplexService,IDisposable
    {
        public event Action<object, string> ShowMesEvent;
        Thread myThread = null;

        private void ShowMes(string mes)
        {
            if (ShowMesEvent != null)
            {
                this.ShowMesEvent(this, string.Format("NowThread ID:{0} Mes:{1}", Thread.CurrentThread.ManagedThreadId, mes));
            }

            if (MessageTransferChannel.MessageCallback != null)
            {
                MessageTransferChannel.MessageCallback(this, string.Format("NowThread ID:{0} Mes:{1}", Thread.CurrentThread.ManagedThreadId, mes));
            }
        }

        IServiceCallBack CallBack
        {
            get { return OperationContext.Current.GetCallbackChannel<IServiceCallBack>(); }
        }

        public string TryCallBack(string info)
        {
            ShowMes("Call TryCallBack");
            CallBack.ReportState("TryCallBack will do");
            return ("TryCallBack" + info);
        }


        public void TryOneWayCallBack(string info)
        {
            ShowMes("Call TryOneWayCallBack");
            CallBack.ReportState("TryOneWayCallBack will do");

            StartReport();
        }

        private void StartReport()
        {
            if(myThread!=null)
            {
                if(!myThread.IsAlive)
                {
                    myThread.Start();
                }
            }
            else
            {
                //myThread=new Thread(new ThreadStart(()=>{CallBack.ReportTime(DateTime.Now.ToString());CallBack.ReportTime(Thread.CurrentThread.ManagedThreadId.ToString());Thread.Sleep(3000);}));
                myThread = new Thread(new ParameterizedThreadStart(DoReport));
                myThread.IsBackground = true;
                myThread.Start(CallBack);
            }
        }

        public static void DoReport(object yourCallBack)
        {
            while(true)
            {
                ((IServiceCallBack)yourCallBack).ReportTime(DateTime.Now.ToString());
                ((IServiceCallBack)yourCallBack).ReportTime(Thread.CurrentThread.ManagedThreadId.ToString()); 
                Thread.Sleep(3000);
            }
        }

        public void Dispose()
        {
            if (myThread != null)
            {
                if (myThread.IsAlive)
                {
                    myThread.Abort();
                }
            }
        }
    }
}
