using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using K_StudentskaSluzba;

namespace K_Servis
{
    class StudentskaSluzba : IStudentskaSluzba, IBezbednosniMehanizmi
    {
        static readonly DirektorijumKorisnika direktorijum = new DirektorijumKorisnika();

        public string Autentifikacija(string korisnik, string lozinka)
        {
            return direktorijum.AutentifikacijaKorisnika(korisnik, lozinka);
        }

        public bool DobaviStudenta(string token, string brojIndeksa, out Student student)
        {
            direktorijum.KorisnikAutentifikovan(token);
            direktorijum.KorisnikAutorizovan(token, EPravaPristupa.Citanje);

            return BazaPodataka.studenti.TryGetValue(brojIndeksa, out student);
        }

        public bool DodajPredmetStudentu(string token, string brojIndeksa, string nazivPredmeta, int ocena)
        {
            direktorijum.KorisnikAutentifikovan(token);
            direktorijum.KorisnikAutorizovan(token, EPravaPristupa.Azuriranje);

            if (BazaPodataka.studenti.TryGetValue(brojIndeksa, out Student student))
            {
                return student.DodajPredmet(nazivPredmeta, ocena);
            }
            return false;
        }

        public void DodajStudenta(string token, Student student)
        {
            direktorijum.KorisnikAutentifikovan(token);
            direktorijum.KorisnikAutorizovan(token, EPravaPristupa.Azuriranje);

            if (!BazaPodataka.studenti.ContainsKey(student.BrIndeksa))
            {
                BazaPodataka.studenti[student.BrIndeksa] = student;
            }
        }

        public void IzbrisiStudenta(string token, string brojIndeksa)
        {
            direktorijum.KorisnikAutentifikovan(token);
            direktorijum.KorisnikAutorizovan(token, EPravaPristupa.Azuriranje);

            BazaPodataka.studenti.Remove(brojIndeksa);
        }

        public void IzmeniStudenta(string token, Student student)
        {
            direktorijum.KorisnikAutentifikovan(token);
            direktorijum.KorisnikAutorizovan(token, EPravaPristupa.Azuriranje);

            if (BazaPodataka.studenti.ContainsKey(student.BrIndeksa))
            {
                BazaPodataka.studenti[student.BrIndeksa] = student;
            }
        }

        public bool ObrisiPredmetStudentu(string token, string brojIndeksa, string nazivPredmeta)
        {
            direktorijum.KorisnikAutentifikovan(token);
            direktorijum.KorisnikAutorizovan(token, EPravaPristupa.Azuriranje);

            if (BazaPodataka.studenti.TryGetValue(brojIndeksa, out Student student))
            {
                return student.ObrisiPredmet(nazivPredmeta);
            }
            return false;
        }
    }
}
