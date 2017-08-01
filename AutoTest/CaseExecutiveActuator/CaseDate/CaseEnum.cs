using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaseExecutiveActuator
{
    public enum CaseActuatorOutPutType
    {
        ExecutiveInfo,
        ExecutiveError,
        ActuatorInfo,
        ActuatorError
    }


    /// <summary>
    /// case 运行状态
    /// </summary>
    public enum CaseActuatorState
    {
        Running = 0,
        Pause = 1,
        Stop = 2,
        Stoping = 3,
        Trying = 4
    }

    /// <summary>
    /// 描述框架支持的协议(请再此处扩展)
    /// </summary>
    public enum CaseProtocol
    {
        extendProtocol = 0,
        unknownProtocol = 1,
        defaultProtocol = 2,
        console = 3,
        vanelife_http = 4,
        vanelife_comm = 5,
        vanelife_tcp = 6,
        vanelife_telnet = 7,
        http = 8,
        tcp = 9,
        comm = 10,
        telnet = 11,
        activeMQ = 12,
        mysql = 13
    }

    /// <summary>
    /// 静态参数化数据大分类
    /// </summary>
    public enum CaseStaticDataClass
    {
        caseStaticDataKey = 0,
        caseStaticDataParameter = 1,
        caseStaticDataSource = 2
    }

    /// <summary>
    /// 描述框架脚本支持的静态参数化数据(请再此处扩展)
    /// </summary>
    public enum CaseStaticDataType
    {
        caseStaticData_vaule = 10000,

        caseStaticData_index = 20000,
        caseStaticData_long = 20001,
        caseStaticData_random = 20002,
        caseStaticData_time = 20003,
        caseStaticData_list = 20004,
        caseStaticData_strIndex = 20005,

        caseStaticData_csv = 30000,
        caseStaticData_mysql = 30001,
        caseStaticData_redis = 30002,
    }


    /// <summary>
    /// 描述框架脚本支持的数据摘取方式(请再此处扩展)
    /// </summary>
    public enum PickOutFunction
    {
        pick_json = 0,
        pick_xml = 1,
        pick_str = 2
    }

    /// <summary>
    /// 描述框架脚本文件基础根节点类型(请再此处扩展)
    /// </summary>
    public enum CaseType
    {
        Unknown = 0,
        Project = 1,
        Case = 2,
        Repeat = 3,
        ScriptRunTime = 4
    }

    /// <summary>
    /// 描述执行结构
    /// </summary>
    public enum CaseResult
    {
        Pass = 0,
        Fail = 1,
        Unknow = 2,
    }

    /// <summary>
    /// 描述脚本动作(请再此处扩展)
    /// </summary>
    public enum CaseAction
    {
        action_unknow = 0,
        action_goto = 1,
        action_retry = 2,
        action_stop = 3,
        action_continue = 4,
        action_alarm = 5,
    }

    /// <summary>
    /// 描述脚本断言方式(请再此处扩展)
    /// </summary>
    public enum CaseExpectType
    {
        judge_default = 0,
        judge_is = 1,
        judge_not = 2,
        judge_like = 3,
        judge_endwith = 4,
        judge_startwith = 5,
        judge_contain = 6,
        judge_uncontain = 7,
    }

    /// <summary>
    /// 描述[caseParameterizationContent]使用的数据附加编码类型，0标识不进行操作，基数encode偶数decode
    /// </summary>
    public enum ParameterizationContentEncodingType
    {
        encode_default = 0,
        encode_base64 = 1,
        decode_base64 = 2,
        encode_hex16 = 3,
        decode_hex16 = 4,
        encode_hex2 = 5,
        decode_hex2 = 6

    }
}
