using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalModules
{
    public class ServiceModule2
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void ModifyName(string name)
        {
            this.Name = name;
            CustomDataEventPublisher.PublishEvent(this);
        }

    }
}
