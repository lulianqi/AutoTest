using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestForMergeSrt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            MergeSrt(@"D:\srtmerge\subtitles1.vtt", @"D:\srtmerge\subtitles2.vtt", @"D:\srtmerge\subtitles3.vtt");
            Console.ReadLine();
        }

        public static bool MergeSrt(string srtPath_1,string srtPath_2,string yourFileName)
        {
             FileStream fs;
             StreamWriter sw;
             if (!(File.Exists(srtPath_1) && File.Exists(srtPath_2)))
             {
                 throw (new Exception("not find your file"));
             }
             fs = new FileStream(yourFileName, File.Exists(yourFileName) ? FileMode.Append : FileMode.Create, FileAccess.Write);
             sw = new StreamWriter(fs, Encoding.UTF8);
             StreamReader sr1 = new StreamReader(srtPath_1, Encoding.UTF8);
             StreamReader sr2 = new StreamReader(srtPath_2, Encoding.UTF8);

             int index = 1;
             string nextLine = sr1.ReadLine();
             while (nextLine!=null)
             {
                 if (nextLine == index.ToString())
                 {
                     sw.WriteLine(index.ToString());
                     string tempStr = sr1.ReadLine();
                     while (tempStr != "")
                     {
                         if (tempStr==null)
                         {
                             Console.WriteLine("stt1 over");
                             break;
                         }
                         sw.WriteLine(tempStr);
                         tempStr = sr1.ReadLine();
                     }

                     tempStr = sr2.ReadLine();
                     while (tempStr != index.ToString())
                     {
                         if (tempStr == null)
                         {
                             Console.WriteLine("stt2 over");
                             break;
                         }
                         tempStr = sr2.ReadLine();
                     }
                     sr2.ReadLine();
                     tempStr = sr2.ReadLine();
                     while (tempStr != "")
                     {
                         if (tempStr == null)
                         {
                             Console.WriteLine("stt2 over");
                             break;
                         }
                         sw.WriteLine(tempStr);
                         tempStr = sr2.ReadLine();
                     }
                     sw.WriteLine("");
                     index++;
                 }
                 nextLine = sr1.ReadLine();
             }

             sr1.Dispose();
             sr2.Dispose();
             sw.Close();
             sw.Dispose();
             return true;
        }
    }
}
