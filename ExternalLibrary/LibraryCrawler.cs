using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ExternalLibrary
{
    public class LibraryCrawler
    {
        public Assembly CurrentAssembly { get; private set; }

        /// <summary>
        /// Ctor for inspectin the executing assembly itself
        /// </summary>
        public LibraryCrawler()
        {
            CurrentAssembly = Assembly.GetExecutingAssembly();    
        }

        /// <summary>
        /// Ctor for inspecting a specific assembly
        /// </summary>
        /// <param name="asm"></param>
        public LibraryCrawler(Assembly asm)
        {
            CurrentAssembly = asm;
        }

        /// <summary>
        /// Returns the full name of CurrentAssembly
        /// </summary>
        /// <returns></returns>
        public string GetNameOfAssembly()
        {
            return CurrentAssembly.FullName.ToString();
        }

        /// <summary>
        /// Gets all the types defined in this assembly
        /// </summary>
        /// <returns></returns>
        public Type[] GetAllTypes()
        {
            return CurrentAssembly.GetTypes();
        }

        /// <summary>
        /// Gets all the properties defined in the specified type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public PropertyInfo[] GetAllProperties(Type type)
        {
            return type.GetProperties();
        }

        /// <summary>
        /// Gets all the types decorated with the given attribute
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public List<Type> GetAllTypesByAttribute(Type attribute)
        {
            List<Type> retVal = new List<Type>();
            foreach (Type type in CurrentAssembly.GetTypes())
            {
                if (type.GetCustomAttributes(attribute, true).Length > 0)
                {
                    retVal.Add(type);
                }
            }
            return retVal;
        }

        /// <summary>
        /// Returns a dictionary with Type-Module associations
        /// </summary>
        /// <returns></returns>
        public Dictionary<Type, string[]> GetModuleAssociationsDictionary()
        {
            List<Type> decoratedTypes = GetAllTypesByAttribute(typeof(ContentAssociationAttribute));
            Dictionary<Type, string[]> retVal = new Dictionary<Type, string[]>();

            foreach (Type type in decoratedTypes)
            {
                object[] definedAttributes = type.GetCustomAttributes(true);
                foreach (object attr in definedAttributes)
                {
                    ContentAssociationAttribute a = attr as ContentAssociationAttribute;
                    if (a != null)
                    {
                        //Attribute found
                        retVal.Add(type, a.AssociatedTypes);
                    }
                }
            }
            return retVal;

        }

        /// <summary>
        /// Returns a string with types, properties and module association for given assembly
        /// </summary>
        /// <returns></returns>
        public string GetAssemblyDescription()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("\n-----Types in assembly {0}-----", this.CurrentAssembly.GetName()));
            foreach (var item in this.GetAllTypes())
            {
                sb.AppendLine(item.Name);
                sb.AppendLine("\t----Properties:");
                foreach (var property in this.GetAllProperties(item))
                    sb.AppendLine("\t\t" + property.Name);
            }
            sb.AppendLine("\n-----Types with ContentAssociationAttribute Defined-----");
            foreach (var withAttr in this.GetAllTypesByAttribute(typeof(ContentAssociationAttribute)))
            {
                sb.AppendLine("\tType:" + withAttr.Name);

            }
            sb.AppendLine("\n-----Module Associations-----");
            var dict = this.GetModuleAssociationsDictionary();
            foreach (var item in dict)
            {
                sb.AppendLine("\t" + item.Key.ToString());
                sb.AppendLine("\tModules:");
                foreach (string module in item.Value)
                {
                    sb.AppendLine("\t\t" + module);
                }
            }
            return sb.ToString();
        }
    }
}
