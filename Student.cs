using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace K_StudentskaSluzba
{
    [DataContract]
    public class Student
    {
        string ime;
        string prezime;
        string brIndeksa;
        Dictionary<string, int> predmeti;

        public Student(string ime, string prezime, string brIndeksa)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.brIndeksa = brIndeksa;
            this.predmeti = new Dictionary<string, int>();
        }

        [DataMember]
        public string Ime { get => ime; set => ime = value; }
        [DataMember]
        public string Prezime { get => prezime; set => prezime = value; }
        [DataMember]
        public string BrIndeksa { get => brIndeksa; set => brIndeksa = value; }
        
        public bool DodajPredmet(string predmet, int ocena)
        {
            if (predmeti.ContainsKey(predmet))
                return false;
            else
            {
                predmeti[predmet] = ocena;
                return true;
            }
        }

        public bool ObrisiPredmet(string predmet)
        {
            return predmeti.Remove(predmet);
        }
    }
}
