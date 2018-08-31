using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Diagnostics;


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


namespace AutoTest.MyTool
{
    static class myGlobalStaticData
    {
        #region 线程同步
        //public volatile int volatileIndex; //不能在静态类中
        //声明为 volatile 的字段不受编译器优化（假定由单个线程访问）的限制。这样可以确保该字段在任何时间呈现的都是最新的值。

        public static AutoResetEvent myAutoResetEvent = new AutoResetEvent(false);
        //myAutoResetEvent.Set();  开锁      (Set 方法释放单个线程。 如果没有等待线程，等待句柄将一直保持等待线程尝试通过状态，或者直到它的 Reset 方法被调用。 )        
        //myAutoResetEvent.WaitOne(); 调用后主动上锁，即一个WaitOne通过会阻断所有其他的WaitOne，直到Set才会再通过一个WaitOne  （就是说正常情况下一个AutoResetEvent只能激活一个线程）

        public static ManualResetEvent myManualResetEvent = new ManualResetEvent(false); //如果为 true，则将初始状态设置为终止（等待 ManualResetEvent 的线程不阻塞）；如果为 false，则将初始状态设置为非终止(阻止状态)。
        //WaitOne调用后不会主动上锁  (调用set后所有WaitOne都会通过，需要手动调用Reset)（并且即使set后马上Reset，所有WaitOne也会通过，即ResetEvent使用的都是通知模式）
        //Set()    开锁
        //Reset()  上锁
        //WaitOne()可加时间间隔

        public static readonly object myThreadLock = new object();
        //通常，应避免锁定 public 类型，最佳做法是定义 private 对象来锁定, 或 private static 对象变量来保护所有实例所共有的数据。
        //lock (myThreadLock)
        //{
        //     it will let this code run olny one copies in task
        //}

        private static object myMonitorObject = new object();
        //功能基本同上
        //Monitor.Enter(myMonitorObject); Monitor.Exit(myMonitorObject); 或 Monitor.TryEnter(myMonitorObject) 同时具有Monitor.Pulse等方法

        //[System.Runtime.Remoting.Contexts.Synchronization] //将该属性应用于某个对象时，在共享该属性实例的所有上下文中只能执行一个线程。
        //需要注意的是，要实现上述机制，类必须继承至System.ContextBoundObject，换句话说,类必须是上下文绑定的。（实测不是不让函数进入，是不让函数返回）

        //[MethodImplAttribute(MethodImplOptions.Synchronized)] 加在方法前面
        //MethodImplAttribute类的一个构造函数把MethodImplOptions枚举作为其参数，MethodImplOptions 枚举有一个字段Synchronized，，它指定在任一时刻只允许一个线程访问这个方法。
        //指定同时只能由一个线程执行该方法。 静态方法锁定类型，而实例方法锁定实例(即每个实例都单独锁定)。 在任何实例函数中只能执行一个线程，并且在类的任何静态函数中只能执行一个线程。 
        #endregion

        public static Stopwatch myStopWatch = new Stopwatch();//提供给代码测试使用，请勿在场景中使用

        //Stopwatch.IsHighResolution = true  //https://msdn.microsoft.com/zh-cn/library/system.diagnostics.stopwatch.ishighresolution.aspx
        //Process.GetCurrentProcess().TotalProcessorTime;
        //Elapsed.Ticks

    }
}
