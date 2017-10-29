using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalModules
{
    public class ServiceModule1
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void ModifyId(int id)
        {
            this.Id = id;
            CustomDataEventPublisher.PublishEvent(this);
        }

    }
}
