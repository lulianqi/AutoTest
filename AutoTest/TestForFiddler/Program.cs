using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace TestForFiddler
{
    class Program
    {

        internal const int WM_COPYDATA = 0x4A;

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        internal static extern IntPtr SendWMCopyMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref SendDataStruct lParam);
        [DllImport("user32.dll")]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [StructLayout(LayoutKind.Sequential)]
        internal struct SendDataStruct { public IntPtr dwData; public int cbData; public string strData; }
        static void Main(string[] args)
        {
            StringDictionary sd = new StringDictionary();
            sd.Add("key", "value");
            sd.Add("key2", "value");
            foreach(var kv in sd)
            {
                kv.ToString();
            }

            Console.ReadLine();
            SendDataStruct oStruct = new SendDataStruct(); 
            oStruct.dwData = (IntPtr)61181; oStruct.strData = "TheString"; 
            oStruct.cbData = Encoding.Unicode.GetBytes(oStruct.strData).Length; 
            //IntPtr hWnd = FindWindow(null, "Fiddler - HTTP Debugging Proxy");
            IntPtr hWnd = FindWindow(null, "Progress Telerik Fiddler Web Debugger");
            Console.WriteLine("Fiddler Ptr :" + hWnd);
            Console.WriteLine("SendWMCopyMessage return :"  + SendWMCopyMessage(hWnd, WM_COPYDATA, IntPtr.Zero, ref oStruct));
            Console.ReadLine();


        }


    }
}
