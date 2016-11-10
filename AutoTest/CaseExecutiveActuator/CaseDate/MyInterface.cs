using CaseExecutiveActuator.Cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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


namespace CaseExecutiveActuator
{
    class MyInterface
    {
    }

    #region delegate
    public delegate void delegateBasicAnonymous();

    public delegate void delegateGetExecutiveData(string sender, string yourContent);

    //【myCaseRunTime】
    public delegate void delegateQueueChangeEventHandler(CaseCell yourTarget, string yourMessage);
    public delegate void delegateLoopChangeEventHandler(CaseCell yourTarget, string yourMessage);
    #endregion

    public interface ICaseProtocolExecution
    {
        string executionProtocolCase();
    }

    //用于连接ExecutiveData数据结构的接口
    public interface IConnectExecutiveData
    {
        CaseProtocol myCaseProtocol
        {
            get;
            //set;
        }
    }

    //用于脚本执行数据结构的接口
    public interface ICaseExecutionContent
    {
        CaseProtocol myCaseProtocol
        {
            get;
            //set;
        }

        string myCaseActuator
        {
            get;
        }

        string myExecutionTarget
        {
            get;
        }

        string myExecutionContent
        {
            get;
        }

        string myErrorMessage
        {
            get;
        }
    }

    /// <summary>
    /// StaticData数据结构接口
    /// Current 属性指向集合中的当前成员。
    /// MoveNext 属性将枚举数移到集合中的下一成员
    /// Reset 属性将枚举数移回集合的开始处
    /// </summary>
    public interface IRunTimeStaticData : ICloneable
    {
        string dataCurrent();
        string dataMoveNext();
        void dataReset();
        bool dataSet(string expectData);
    }


    //用于脚本执行器的接口
    public interface ICaseExecutionDevice :ICloneable
    {
        /// <summary>
        /// 获取ExecutionDevice 协议类型
        /// </summary>
        CaseProtocol getProtocolType
        {
            get;
        }

        /// <summary>
        /// 是否连接成功
        /// </summary>
        bool isDeviceConnect
        {
            get;
        }

        /// <summary>
        /// 连接ExecutionDevice
        /// </summary>
        /// <returns>is sucess</returns>
        bool executionDeviceConnect();

        /// <summary>
        /// 关闭ExecutionDevice
        /// </summary>
        void executionDeviceClose();

        /// <summary>
        /// 使用case内容在当前执行器中执行该Case
        /// </summary>
        /// <param name="yourExecutionContent">执行数据</param>
        /// <param name="yourExecutiveDelegate">执行引擎委托（请使用此委托向外暴露执行过程）</param>
        /// <param name="sender">调用该执行器的执行引擎的标识</param>
        /// <param name="yourParameterList">在执行中可能会使用到的ParameterList</param>
        /// <param name="yourStaticDataList">在执行中可能会使用到的StaticDataList</param>
        /// <param name="caseId"></param>
        /// <returns>返回执行结果 务必保证myExecutionDeviceResult中spanTime,startTime,backContent,caseTarget,caseProtocol在此方法中填充</returns>
        myExecutionDeviceResult executionDeviceRun(ICaseExecutionContent yourExecutionContent, delegateGetExecutiveData yourExecutiveDelegate, string sender, Dictionary<string, string> yourParameterList, Dictionary<string, IRunTimeStaticData> yourStaticDataList, int caseId);

        //只包含方法、属性、事件或索引器的签名。(不包含委托,这个委托是类型定义)
        //delegate void delegateGetExecutiveData(string yourContent);

        /// <summary>
        /// 暴露 ExecutionDevice 执行过程
        /// </summary>
        event delegateGetExecutiveData OnGetExecutiveData;
    }

}
