﻿using CaseExecutiveActuator.CaseActuator;
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
    #endregion

    public interface ICaseProtocolExecution
    {
        string ExecutionProtocolCase();
    }

    //用于连接ExecutiveData数据结构的接口
    public interface IConnectExecutiveData
    {
        CaseProtocol MyCaseProtocol
        {
            get;
            //set;
        }
    }

    //用于脚本执行数据结构的接口
    public interface ICaseExecutionContent
    {
        /// <summary>
        /// Case的协议类型
        /// </summary>
        CaseProtocol MyCaseProtocol
        {
            get;
            //set;
        }

        /// <summary>
        /// Case使用的执行器的名称
        /// </summary>
        string MyCaseActuator
        {
            get;
        }

        /// <summary>
        /// Cace目标地址简述(比如于接口地址，用于UI显示，不能返回null）
        /// </summary>
        string MyExecutionTarget
        {
            get;
        }

        /// <summary>
        /// Cace执行内容简述（比如于接口参数，用于UI显示，没有则应该返回为null）
        /// </summary>
        string MyExecutionContent
        {
            get;
        }

        /// <summary>
        /// Case内容如果解析有错误，将通过这里指明 (一旦设置该错误值则表明该ExecutionContent有错误，执行中也不会运行该case)
        /// </summary>
        string MyErrorMessage
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
        /// <summary>
        ///原始连接字符串
        /// </summary>
        string OriginalConnectString
        {
            get;
        }

        /// <summary>
        /// 获取当前初始化数据类型别名
        /// </summary>
        string RunTimeStaticDataTypeAlias
        {
            get;
        }

        /// <summary>
        /// 获取当前游标地址的值
        /// </summary>
        /// <returns></returns>
        string DataCurrent();
        /// <summary>
        /// 将游标下移，并返回下移之后的值（如何已经到达上边界，则重置游标）（为方便使用请特殊处理初始游标也包括重置后的DataMoveNext与DataCurrent一致，即此时DataMoveNext不向下移动）
        /// </summary>
        /// <returns></returns>
        string DataMoveNext();
        /// <summary>
        /// 重置游标
        /// </summary>
        void DataReset();
        /// <summary>
        /// 设置当前游标指示的数据的值
        /// </summary>
        /// <param name="expectData">期望值</param>
        /// <returns>设置是否成功</returns>
        bool DataSet(string expectData);
    }

    public interface IRunTimeDataSource : IRunTimeStaticData
    {
        /// <summary>
        /// 获取一个值指示该数据源是否已经连接
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 连接数据源
        /// </summary>
        /// <returns></returns>
        bool ConnectDataSource();

        /// <summary>
        /// 断开数据源连接
        /// </summary>
        /// <returns></returns>
        bool DisConnectDataSource();

        /// <summary>
        /// 以指定地址返回数据源中的数据（地址无效或错误请返回null）
        /// </summary>
        /// <param name="vauleAddress">地址字符串（需要按格式指定并定义）</param>
        /// <returns>目标数据</returns>
        string GetDataVaule(string vauleAddress);

        /// <summary>
        /// 设置指定地址的数据值 （IRunTimeStaticData 中的DataSet 设置的是当前值）
        /// </summary>
        /// <param name="vauleAddress">地址字符串（需要按格式指定并定义）</param>
        /// <param name="expectData">期望值</param>
        /// <returns>是否成功设置</returns>
        bool DataSet(string vauleAddress, string expectData);
    }

    //用于脚本执行器的接口
    public interface ICaseExecutionDevice :ICloneable
    {
        /// <summary>
        /// 获取ExecutionDevice 协议类型
        /// </summary>
        CaseProtocol ProtocolType
        {
            get;
        }

        /// <summary>
        /// 是否连接成功
        /// </summary>
        bool IsDeviceConnect
        {
            get;
        }


        /// <summary>
        /// 连接ExecutionDevice
        /// </summary>
        /// <returns>is success</returns>
        bool ExecutionDeviceConnect();

        /// <summary>
        /// 关闭ExecutionDevice (在重新加载CASE文件等地方可能会关闭已经关闭的执行器，所以请务必确保重复调用该方法不会有错误产生)
        /// </summary>
        void ExecutionDeviceClose();

        /// <summary>
        /// 使用case内容在当前执行器中执行该Case
        /// </summary>
        /// <param name="yourExecutionContent">执行数据</param>
        /// <param name="yourExecutiveDelegate">执行引擎委托（请使用此委托向外暴露执行过程）(如果执行器没有被订阅显示执行过程该参数可能为空,实现时需要考虑到这种情况)</param>
        /// <param name="sender">调用该执行器的执行引擎的标识</param>
        /// <param name="yourParameterList">在执行中可能会使用到的ParameterList</param>
        /// <param name="yourStaticDataList">在执行中可能会使用到的StaticDataList</param>
        /// <param name="caseId"></param>
        /// <returns>返回执行结果 务必保证myExecutionDeviceResult中spanTime,startTime,backContent,caseTarget,caseProtocol在此方法中填充</returns>
        MyExecutionDeviceResult ExecutionDeviceRun(ICaseExecutionContent yourExecutionContent, CaseActionActuator.delegateGetExecutiveData yourExecutiveDelegate, string sender, ActuatorStaticDataCollection yourActuatorStaticDataCollection, int caseId);

        //只包含方法、属性、事件或索引器的签名。(不包含委托,这个委托是类型定义)
        //delegate void delegateGetExecutiveData(string yourContent);

        /// <summary>
        /// 暴露 ExecutionDevice 执行过程
        /// </summary>
        event CaseActionActuator.delegateGetExecutiveData OnGetExecutiveData;
    }

}
