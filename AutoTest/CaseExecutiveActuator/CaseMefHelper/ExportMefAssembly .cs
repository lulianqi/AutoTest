using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaseExecutiveActuator.CaseMefHelper
{
    class ExportMefAssembly
    {
    }

    public interface IExtendProtocolDriver
    {
        /// <summary>
        /// 扩展协议的名称【为与内置协议区分请使用Ex作为开头，加载组件时会过滤掉不符合名称规范的MEF组件】
        /// </summary>
        String ExtendProtocolName
        {
            get;
        }
    }


}
