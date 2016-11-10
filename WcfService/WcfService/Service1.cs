using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }


        public void GetDataOneWay(int vaule)
        {
            System.Threading.Thread.Sleep(10000);
        }


        public void GetDataOutWay(int vaule ,out string str)
        {
            str = vaule.ToString();
        }


        public void GetDataOut(int vaule, out string str)
        {
            str = vaule.ToString();
        }


        public void GetDataRef(int vaule, ref string str)
        {
            str = vaule.ToString();
        }
    }

    //public class MyProgram
    //{
    //    public static void Main()
    //    {
    //        using (ServiceHost serviceHost=new ServiceHost(typeof(IService1)))
    //        {
    //            serviceHost.Open();
    //            Console.WriteLine("Open ");
    //            Console.ReadKey();
    //        }
    //    }
    //} 
}
