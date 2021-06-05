using K_StudentskaSluzba;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_Servis
{
    class KonfiguracijaServisa
    {
        private EStanjeServera stanjeServera;
        public EStanjeServera StanjeServera
        {
            get => stanjeServera;
            set => stanjeServera = value;
        }
        public KonfiguracijaServisa()
        {
            this.stanjeServera = EStanjeServera.Nepoznato;
            Console.WriteLine("Stanje servisa je: "
            + this.StanjeServera.ToString());
        }
    }
}
