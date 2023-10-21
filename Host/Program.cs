using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Service;

namespace Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(Implementations)))
            {

                host.Open();
                Console.WriteLine("Host is starting");
                Implementations implementations = new Implementations();
                if (implementations.SendToken("dansegura8863@gmail.com", "Importante", "buenas noches", 43))
                {
                    Console.WriteLine("Mensaje enviado");
                }
                else
                {
                    Console.WriteLine("Mensaje no enviado");
                }
            }
            Console.WriteLine("hola");
           Console.ReadLine();
        }
    }
}
