using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace MyCommonHelper
{
    public class MyHiPerformanceTick
    {
        #region Stopwatch  (使用前请先确定精度，Stopwatch精度可达100nm，如果没有明显高于该值可直接使用Stopwatch)
        //Stopwatch myStopWatch = new Stopwatch();
        //myStopWatch.Start();
        //myStopWatch.Stop();
        //myStopWatch.ElapsedTicks;
        //myStopWatch.Restart();
        //System.Environment.TickCount //获取系统启动后经过的毫秒数。
        //TimeSpan time = (System.Diagnostics.Process.GetCurrentProcess()).TotalProcessorTime;  //获取此进程的总的处理器时间。
	    #endregion
        
        [SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32")]
        static private extern bool QueryPerformanceFrequency(ref long PerformanceFrequency);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32")]
        static private extern bool QueryPerformanceCounter(ref long PerformanceCount);

        [DllImport("kernel32")]
        static extern IntPtr GetCurrentThread();

        [DllImport("kernel32")]
        static extern UIntPtr SetThreadAffinityMask(IntPtr hThread, UIntPtr dwThreadAffinityMask);


        private long ticksPerSecond = 0;
        private long lastTick = 0;
        private long nowTick = 0;  

        /// <summary>
        /// 获取当前计数器精度（1S）
        /// </summary>
        public long TicksPerSecond
        {
            get { return ticksPerSecond; }
        }

        /// <summary>
        /// 初始化MyHiPerformanceTick，如果不支持将抛出异常
        /// </summary>
        public MyHiPerformanceTick()
        {
            if (!QueryPerformanceFrequency(ref ticksPerSecond))
            {
                throw (new Exception("not support QueryPerformanceFrequency"));
            }
            UIntPtr previous = SetThreadAffinityMask(GetCurrentThread(), new UIntPtr(1));
            if(previous==new UIntPtr(0))
            {
                Console.WriteLine(previous);
            }
        }

        public long GetTick()
        {
            long tempTick=0;
            QueryPerformanceCounter(ref tempTick);
            return tempTick;
        }

        public double GetTime()
        {
            long tempTick = 0;
            QueryPerformanceCounter(ref tempTick);
            return (double)tempTick / ticksPerSecond;
        }

        public void StartTick()
        {
            QueryPerformanceCounter(ref lastTick);
        }

        public void EndTick()
        {
            QueryPerformanceCounter(ref nowTick);
        }

        public long GetElapsedTick()
        {
            return nowTick - lastTick;
        }

        public double GetElapsedTime()
        {
            return (double)GetElapsedTick() / ticksPerSecond;
        }

        public override string ToString()
        {
            return string.Format("the MyHiPerformanceTick ticksPerSecond is {0} ; time precision is {1} ns", ticksPerSecond, 1000000000d / ticksPerSecond);
        }
    }
}
