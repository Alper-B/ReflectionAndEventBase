using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalLibrary
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ContentAssociationAttribute : Attribute
    {
        public string[] AssociatedTypes { get; private set; }

        public ContentAssociationAttribute(params string[] associatedTypes)
        {
            this.AssociatedTypes = associatedTypes;
        }
    }
}
