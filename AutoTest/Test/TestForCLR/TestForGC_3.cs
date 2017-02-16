using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestForCLR
{
    class TestForGC_3
    {
        class TypeA
        {
            string fiedName;
            long filedIndex;
            char[] bytes = new char[] { 'l', 'u', 'l', 'i', 'a' };
            TypeB b;
            public TypeA(string name, long index)
            {
                fiedName = name;
                filedIndex = index;
                b = new TypeB(15158155511);
            }
        }

        class TypeB
        {

            long filedIndex;
            long typeId;
            public TypeB(long index)
            {
                filedIndex = index;
                typeId = 555555555556;
            }
        }


        public void Run()
        {
            while (true)
            {
                TypeA a1 = new TypeA("xinyunlian", 15158155512);
                TypeA a2 = new TypeA("xinyunlian", 15158155512);
                TypeA a3 = new TypeA("xinyunlian", 15158155512);
                TypeB b1 = new TypeB(15158155513);
                TypeB b2 = new TypeB(15158155513);
                TypeB b3 = new TypeB(15158155513);

                Console.WriteLine("creat A,B");
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    return;
                }
                GC.Collect(2, GCCollectionMode.Forced);
                GC.WaitForPendingFinalizers();
                Console.WriteLine("GC.Collect complet");
            }
        }

        public void RunCreat(string name, long index)
        {
            for(int i =0 ;i<10;i++)
            {
                TypeA a1 = new TypeA(name, index);
            }
        }

        public void RunTest()
        {
            byte[] bytesStart = new byte[1024];
            for (int i = 0; i < bytesStart.Length; i++)
            {
                bytesStart[i] = 0xbb;
            }
            Console.ReadLine();
            Console.WriteLine("creat");
            RunCreat("testforclr", 5555555555);
            Console.ReadLine();
            GC.Collect(2, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            Console.WriteLine("GC.Collect complet");
            byte[] bytes = new byte[2048];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = 0xaa;
            }
            Console.ReadLine();
        }
    }
}
