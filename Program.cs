using K_StudentskaSluzba;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace K_SistemMonitor
{
    class Program
    {
        internal static EStanjeServera stanjePrvog = EStanjeServera.Nepoznato; 
        internal static EStanjeServera stanjeDrugog = EStanjeServera.Nepoznato;

        internal static IStudentskaSluzba prviServis = null;
        internal static IStudentskaSluzba drugiServis = null;

        internal static IBezbednosniMehanizmi prviServisBezbednost = null;
        internal static IBezbednosniMehanizmi drugiServisBezbednost = null;

        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(ServisStudentskeSluzbeMonitor));
            host.Open();
            Console.WriteLine("Servis je uspesno otvoren");

            IStanjeServisa prvi = null;
            IStanjeServisa drugi = null;
            try
            {
                ChannelFactory<IStanjeServisa> cfPrvi
                = new ChannelFactory<IStanjeServisa>("prvi");
                prvi = cfPrvi.CreateChannel();
                prvi.AzuriranjeStanja(EStanjeServera.Primarni);

                ChannelFactory<IStudentskaSluzba> cfPrviSSluzba
                = new ChannelFactory<IStudentskaSluzba>("prviSSluzba");

                prviServis = cfPrviSSluzba.CreateChannel();

                ChannelFactory<IBezbednosniMehanizmi> cfPrviBezbednost
                    = new ChannelFactory<IBezbednosniMehanizmi>("prviBezbednost");

                prviServisBezbednost = cfPrviBezbednost.CreateChannel();
            }
            catch (CommunicationException cex)
            {
                Console.WriteLine("Prvi servis nedostupan. Razlog: "
                + cex.Message);
            }

            try
            {

                ChannelFactory<IStanjeServisa> cfDrugi
                = new ChannelFactory<IStanjeServisa>("drugi");
                drugi = cfDrugi.CreateChannel();
                if (prvi is null)
                    drugi.AzuriranjeStanja(EStanjeServera.Primarni);
                drugi.AzuriranjeStanja(EStanjeServera.Sekundarni);

                ChannelFactory<IStudentskaSluzba> cfDrugiSSluzba
                = new ChannelFactory<IStudentskaSluzba>("drugiSSluzba");

                drugiServis = cfDrugiSSluzba.CreateChannel();

                ChannelFactory<IBezbednosniMehanizmi> cfDrugiBezbednost
                        = new ChannelFactory<IBezbednosniMehanizmi>("drugiBezbednost");

                drugiServisBezbednost = cfDrugiBezbednost.CreateChannel();
            }
            catch (CommunicationException cex)
            {
                Console.WriteLine("Drugi servis nedostupan. Razlog: "
                + cex.Message);
            }

            if (prvi is null && drugi is null)
                {
                    Console.WriteLine("Neuspelo povezivanje na servise.");
                    Environment.Exit(0);
                }

            while (true)
            {
                try
                {
                    stanjePrvog = prvi.ProveraStanja();
                }
                catch (Exception ex)
                {
                    stanjePrvog = EStanjeServera.Nepoznato;
                    Console.WriteLine("Greska na primarnom: " + ex.Message);
                }
                try
                {
                    stanjeDrugog = drugi.ProveraStanja();
                }
                catch (Exception ex)
                {
                    stanjeDrugog = EStanjeServera.Nepoznato;
                    Console.WriteLine("Greska na sekundarnom: " + ex.Message);
                }
                Console.WriteLine("Stanja servisa.");
                Console.WriteLine("- primarni: " + stanjePrvog.ToString());
                Console.WriteLine("- sekundarni: " + stanjeDrugog.ToString());
                if (stanjePrvog == EStanjeServera.Nepoznato)
                {
                    if (stanjeDrugog == EStanjeServera.Sekundarni)
                    {
                        drugi.AzuriranjeStanja(EStanjeServera.Primarni);
                        Console.WriteLine("Sekundarni proglasen primarnim.");
                    }
                }
                if (stanjeDrugog == EStanjeServera.Nepoznato)
                {
                    Console.WriteLine("Sekundarni nije operativan.");
                    Environment.Exit(0);
                }
                Thread.Sleep(5000);
            }
        }
    }
}
