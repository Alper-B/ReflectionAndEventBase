using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalLibrary;
using System.Reflection;
using ExternalModules;


namespace ReflectionAndEventsBase
{
    class Program
    {
        static void Main(string[] args)
        {
            //LibraryCrawler libCrawler = new LibraryCrawler(Assembly.GetAssembly(typeof(LibraryCrawler)));//Get assembly by a type 
            //ServiceModule1 svc1 = new ServiceModule1();//ExternalModules will only be listed as a referencedAssemblies if we use one of its types
            CustomDataEventPublisher.Subscribe(CustomHandler);
            ServiceModule1 svc1 = new ServiceModule1();
            ServiceModule2 svc2 = new ServiceModule2();

            //See if below method invocations can trigger the event successfully
            svc1.ModifyId(5);
            svc2.ModifyName("AB");

            LibraryCrawler lc = new LibraryCrawler(Assembly.Load("ExternalLibrary"));//Can find succesfully if the assembly is a referenced one
            Console.WriteLine(lc.GetAssemblyDescription());

            Console.ReadLine();

            AssemblyName[] references = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            List<LibraryCrawler> crawlers = new List<LibraryCrawler>();
            foreach (var asm in references)
            {
                crawlers.Add(new LibraryCrawler(Assembly.Load(asm)));
            }
            var selectedCrawlers = crawlers.Where(c => c.GetNameOfAssembly().StartsWith("External")).ToList();
            foreach (var crawler in selectedCrawlers)
            {
                Console.WriteLine(crawler.GetAssemblyDescription());
            }
           
        }

        public static void CustomHandler(object sender, CustomDataEventArgs e)
        {
            Console.WriteLine($"\nEvent Invoked By: {e.ModifiedItemType.ToString()}");
        }

    }
}
