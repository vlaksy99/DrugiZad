using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace K_StudentskaSluzba
{
    [ServiceContract]
    public interface IStudentskaSluzba
    {
        [OperationContract]
        [FaultContract(typeof(BezbednosniIzuzetak))]
        void DodajStudenta(string token, Student student);
        [OperationContract]
        [FaultContract(typeof(BezbednosniIzuzetak))]
        void IzmeniStudenta(string token, Student student);
        [OperationContract]
        [FaultContract(typeof(BezbednosniIzuzetak))]
        void IzbrisiStudenta(string token, string brojIndeksa);
        [OperationContract]
        [FaultContract(typeof(BezbednosniIzuzetak))]
        bool DobaviStudenta(string token, string brojIndeksa, out Student student);
        [OperationContract]
        [FaultContract(typeof(BezbednosniIzuzetak))]
        bool DodajPredmetStudentu(string token, string brojIndeksa, string nazivPredmeta, int ocena);
        [OperationContract]
        [FaultContract(typeof(BezbednosniIzuzetak))]
        bool ObrisiPredmetStudentu(string token, string brojIndeksa, string nazivPredmeta);
    }
}
