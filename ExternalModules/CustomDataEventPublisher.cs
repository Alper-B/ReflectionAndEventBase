using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalModules
{
    public class CustomDataEventPublisher
    {
        public delegate void CustomDataEventHandler(object sender, CustomDataEventArgs e);
        //Used generic eventhandler instead of custom delegate 
        public static event CustomDataEventHandler CustomDataEvent;

        public static void PublishEvent(object sender)
        {
            CustomDataEvent(sender, new CustomDataEventArgs { ModifiedItemType = sender.GetType()});
        }
        public static void Subscribe(CustomDataEventHandler handler)
        {
            CustomDataEvent += handler;
        }
    }

    public class CustomDataEventArgs : EventArgs
    {
        public Type ModifiedItemType { get; set; }
    }
}
