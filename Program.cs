using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace K_Servis
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(StudentskaSluzba)),
                hostObradaStanja = new ServiceHost(typeof(ObradaStanja)))
            {
                host.Open();
                hostObradaStanja.Open();
                Console.WriteLine("Servis je uspesno otvoren");
                Console.WriteLine("Studentska sluzba : " + host.BaseAddresses.FirstOrDefault());
                Console.WriteLine("ObradaStanja : " + hostObradaStanja.BaseAddresses.FirstOrDefault());
                Console.ReadKey();
            }
        }
    }
}
