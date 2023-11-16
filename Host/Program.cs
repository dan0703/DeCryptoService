using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Service;
using log4net;

namespace Host
{
    internal class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(Implementations)))
            {
                host.Open();
                
                Console.WriteLine("Host is starting");
                Console.ReadLine();
            }
        }
    }
}
