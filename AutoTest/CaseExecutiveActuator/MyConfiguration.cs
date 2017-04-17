using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaseExecutiveActuator
{
    public class MyConfiguration 
    {
        //◎●◐◑◒◓◔◕◖◗▼▲
        public static int PostFileTimeOut = 100000;                                                             //AtHttpProtocol中http文件上传超时时间
        public static string CaseFilePath = "testData";                                                         //文件上传的默认文件夹名
        public static string ParametersDataSplitStr = "*#";                                                     //参数化数据分隔符
        public static string ParametersExecuteSplitStr = "`";                                                   //参数再运算风格符
        public static string CaseShowTargetAndContent = "➤";
        public static string CaseShowCaseNodeStart = "◆";
        public static string CaseShowJumpGotoNode = "▼";
        public static string CaseShowGotoNodeStart = "▲";
    }
}
