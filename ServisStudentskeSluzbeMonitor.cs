using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using K_StudentskaSluzba;

namespace K_SistemMonitor
{
    class ServisStudentskeSluzbeMonitor : IStudentskaSluzba, IBezbednosniMehanizmi
    {
        public string Autentifikacija(string korisnik, string lozinka)
        {
            if (Program.stanjePrvog == EStanjeServera.Primarni)
            {
                try
                {
                    return Program.prviServisBezbednost.Autentifikacija(korisnik, lozinka);
                }
                catch (FaultException<BezbednosniIzuzetak>)
                {
                    throw;
                }
            }
            else if (Program.stanjeDrugog == EStanjeServera.Primarni)
            {
                try
                {
                    return Program.drugiServisBezbednost.Autentifikacija(korisnik, lozinka);
                }
                catch (FaultException<BezbednosniIzuzetak>)
                {
                    throw;
                }                
            }

            return "";
        }

        public bool DobaviStudenta(string token, string brojIndeksa, out Student student)
        {
            student = null;

            if (Program.stanjePrvog == EStanjeServera.Primarni)
            {
                try
                {
                    return Program.prviServis.DobaviStudenta(token, brojIndeksa, out student);
                }
                catch (FaultException<BezbednosniIzuzetak>)
                {
                    throw;
                }
            }
            else if (Program.stanjeDrugog == EStanjeServera.Primarni)
            {
                try
                {
                    return Program.drugiServis.DobaviStudenta(token, brojIndeksa, out student);
                }
                catch (FaultException<BezbednosniIzuzetak>)
                {
                    throw;
                }
            }

            return false;            
        }

        public bool DodajPredmetStudentu(string token, string brojIndeksa, string nazivPredmeta, int ocena)
        {
            if (Program.stanjePrvog == EStanjeServera.Primarni)
            {
                try
                {
                    return Program.prviServis.DodajPredmetStudentu(token, brojIndeksa, nazivPredmeta, ocena);
                }
                catch (FaultException<BezbednosniIzuzetak>)
                {
                    throw;
                }
            }
            else if (Program.stanjeDrugog == EStanjeServera.Primarni)
            {
                try
                {
                    return Program.drugiServis.DodajPredmetStudentu(token, brojIndeksa, nazivPredmeta, ocena);
                }
                catch (FaultException<BezbednosniIzuzetak>)
                {
                    throw;
                }
            }
            return false;
        }

        public void DodajStudenta(string token, Student student)
        {
            if (Program.stanjePrvog == EStanjeServera.Primarni)
            {
                try
                {
                    Program.prviServis.DodajStudenta(token, student);
                }
                catch (FaultException<BezbednosniIzuzetak>)
                {
                    throw;
                }
            }
            else if (Program.stanjeDrugog == EStanjeServera.Primarni)
            {
                try
                {
                    Program.drugiServis.DodajStudenta(token, student);
                }
                catch (FaultException<BezbednosniIzuzetak>)
                {
                    throw;
                }
            }
        }

        public void IzbrisiStudenta(string token, string brojIndeksa)
        {
            if (Program.stanjePrvog == EStanjeServera.Primarni)
            {
                try
                {
                    Program.prviServis.IzbrisiStudenta(token, brojIndeksa);
                }
                catch (FaultException<BezbednosniIzuzetak>)
                {
                    throw;
                }
            }
            else if (Program.stanjeDrugog == EStanjeServera.Primarni)
            {
                try
                {
                    Program.drugiServis.IzbrisiStudenta(token, brojIndeksa);
                }
                catch (FaultException<BezbednosniIzuzetak>)
                {
                    throw;
                }
            }
        }

        public void IzmeniStudenta(string token, Student student)
        {
            if (Program.stanjePrvog == EStanjeServera.Primarni)
            {
                try
                {
                    Program.prviServis.IzmeniStudenta(token, student);
                }
                catch (FaultException<BezbednosniIzuzetak>)
                {
                    throw;
                }
            }
            else if (Program.stanjeDrugog == EStanjeServera.Primarni)
            {
                try
                {
                    Program.drugiServis.IzmeniStudenta(token, student);
                }
                catch (FaultException<BezbednosniIzuzetak>)
                {
                    throw;
                }
            }
        }

        public bool ObrisiPredmetStudentu(string token, string brojIndeksa, string nazivPredmeta)
        {
            if (Program.stanjePrvog == EStanjeServera.Primarni)
            {
                try
                {
                    return Program.prviServis.ObrisiPredmetStudentu(token, brojIndeksa, nazivPredmeta);
                }
                catch (FaultException<BezbednosniIzuzetak>)
                {
                    throw;
                }
            }
            else if (Program.stanjeDrugog == EStanjeServera.Primarni)
            {
                try
                {
                    return Program.drugiServis.ObrisiPredmetStudentu(token, brojIndeksa, nazivPredmeta);
                }
                catch (FaultException<BezbednosniIzuzetak>)
                {
                    throw;
                }
            }
            return false;
        }
    }
}
