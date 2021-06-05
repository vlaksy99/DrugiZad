using K_StudentskaSluzba;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace K_Klijent
{
    class Program
    {
        static void Main(string[] args)
        {

            ChannelFactory<IBezbednosniMehanizmi> channelFactoryBezbednost
                      = new ChannelFactory<IBezbednosniMehanizmi>("bezbednost");

            IBezbednosniMehanizmi proxyBezbednost = channelFactoryBezbednost.CreateChannel();
            string token = "";
            try
            {
                token = proxyBezbednost.Autentifikacija("admin", "pr3Ax4dmin");
                Console.WriteLine("Uspesno logovanje");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            ChannelFactory<IStudentskaSluzba> channelFactory
                      = new ChannelFactory<IStudentskaSluzba>("studentskaSluzba");

            IStudentskaSluzba proxy = channelFactory.CreateChannel();
            try
            {
                proxy.DodajStudenta(token, new Student("Jovan", "Jovic", "123"));
                Console.WriteLine("Uspesno dodavanje studenta");
            }
            catch (FaultException<BezbednosniIzuzetak> ex)
            {
                Console.WriteLine("Error : " + ex.Detail.Poruka);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
