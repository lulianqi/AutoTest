using MyActiveMQHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoForMyHelper
{
    class DemoForActionMQ
    {
        public static MyActiveMQ myMq = new MyActiveMQ("tcp://192.168.200.150:61616", "tsetDeom", null, null, new List<string>() { "q1", "q2" },
            new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("t1", "t1id"), new KeyValuePair<string, string>("t2", null) }, false);
        public static void RunTest()
        {
            Console.WriteLine("Start");
            myMq.Connect();
            Console.WriteLine("Connected");
            myMq.SubscribeConsumer("xx", true, null);
            string mes =  Console.ReadLine();
            myMq.PublishMessage("q1", mes, false, MessageType.TextMessage);
            myMq.PublishMessage("q2", mes, false, MessageType.TextMessage);
            myMq.PublishMessage("t1", mes, true, MessageType.TextMessage);
            myMq.PublishMessage("t2", mes, true, MessageType.TextMessage);
            mes =  Console.ReadLine();
            while(mes!="e")
            {
                List<KeyValuePair<string,string>> tempMes = myMq.ReadAllConsumerMessage();
                foreach (var ms in tempMes)
                {
                    Console.WriteLine(ms.Value+"  "+ms.Key);
                }
                mes = Console.ReadLine();
            }
        }
    }
}
