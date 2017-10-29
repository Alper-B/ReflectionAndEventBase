using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalLibrary
{
    [ContentAssociation("ExternalModules.ServiceModule1", "ExternalModules.ServiceModule2")]
    public class ExternalClass2
    {
        public int Ex2_p1 { get; set; }
        public string Ex2_p2 { get; set; }
    }
}
