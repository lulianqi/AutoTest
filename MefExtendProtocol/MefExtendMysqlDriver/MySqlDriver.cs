using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CaseExecutiveActuator;
using System.ComponentModel.Composition;
using CaseExecutiveActuator.CaseMefHelper;

namespace MefExtendMysqlDriver
{

    [Export(typeof(IExtendProtocolDriver))]
    public class MySqlDriver : IExtendProtocolDriver
    {
        private string driverName = "Ex_MySql000";

        public MySqlDriver()
        {
            driverName = "Ex_MySql001";
        }

        public MySqlDriver(int xx)
        {
            driverName = "Ex_MySql";
        }

        public string ExtendProtocolName
        {
            get { return driverName; }
        }
    }
}
