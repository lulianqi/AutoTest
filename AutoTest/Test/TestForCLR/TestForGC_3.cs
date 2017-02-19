using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestForCLR
{
    class TestForGC_3
    {
        long name = 87564023523;//为了能找到TestForGC_3在托管堆的地址
        public long Name
        {
            get { return name; }
            set { name = value; }
        }
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

            public string Name
            {
                set { fiedName = value; }
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
            Console.ReadLine();
        }

        public long RunTest()
        {   //减少打印方便内存搜索
            Console.ReadLine();
            long myPhone = 25158155511;//为了能找到运行栈空间
            TypeA a1 = new TypeA("testtypea", 0);
            byte[] bytesStart = new byte[1024];
            for (int i = 0; i < bytesStart.Length; i++)
            {
                bytesStart[i] = 0xcc;
            }
            bytesStart[1] = 0Xc1;//为了方便搜索
            Console.ReadLine();
            byte[] bytesThen = new byte[1024];
            for (int i = 0; i < bytesStart.Length; i++)
            {
                bytesThen[i] = 0xcc;
            }
            bytesThen[1] = 0Xc2;
            Console.ReadLine();
            RunCreat("testforclr", 5555555555);
            Console.ReadLine();
            a1.Name = ""; //保证在第一次GC前都是可达的 ,未使用的变量可能直接被优化掉，后面没有由于的根则会在GC时认为不可达
            GC.Collect(0, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            Console.ReadLine();
            byte[] bytes = new byte[2048];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = 0xaa;
            }
            Console.ReadLine();
            bytesStart[1] = 0Xb2; //通过观察效果生效确定地址正确性
            bytesThen[1] = 0Xb1;
            Console.WriteLine("bytesStart{0} bytesThen {1} bytes {2} >>>[G0 times]{3}[G1 times]{4}[G2 times]{5}", GC.GetGeneration(bytesStart), GC.GetGeneration(bytesThen), GC.GetGeneration(bytes), GC.CollectionCount(0), GC.CollectionCount(1), GC.CollectionCount(2));
            Console.ReadLine();
            GC.Collect();
            Console.WriteLine("[G0 times]{0}[G1 times]{1}[G2 times]{2}", GC.GetGeneration(a1), GC.CollectionCount(0), GC.CollectionCount(1), GC.CollectionCount(2));
            TypeA a2 = new TypeA("a2", 151581515);
            a2.Name = "";
            return myPhone++;
        }
    }
}
