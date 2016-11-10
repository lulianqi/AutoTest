using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;

namespace CaseExecutiveActuator.CaseMefHelper
{
    public class MefPlugInDriver
    {
        [Import]
        public IExtendProtocolDriver ExtendTest { get; set; }

        public void Compose()
        {
            DirectoryCatalog directoryCatalog = new DirectoryCatalog("MefExtendDriver");
            var container = new CompositionContainer(directoryCatalog);
            container.ComposeParts(this);
        }  
    }
}
