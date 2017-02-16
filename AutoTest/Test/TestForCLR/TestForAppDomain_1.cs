using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestForCLR
{
    class TestForAppDomain_1
    {
        public void Run()
        {
            AppDomain adCallingThreadDomin = Thread.GetDomain();

        }
    }
}
