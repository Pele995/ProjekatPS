using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IAdmin
    {

        [OperationContract]
        bool DodajVozilo(Vozilo vozilo); //-----

        [OperationContract]
        bool UkloniVozilo(string registracija); //-----

        [OperationContract]
        bool IzmeniVozilo(Vozilo vozilo); //-----

        [OperationContract]
        void DodajVIP(string username);//, List<Korisnik>zahtevi); //-----

        [OperationContract]
        void ObrisiVIP(string username);//, List<Korisnik> zahtevi); //-----

        [OperationContract]
        void ObrisiRezVozila(Rezervacija rezervacija);

        [OperationContract]
        bool IzmeniRezVozila(Rezervacija rezervacija);


        [OperationContract]
        bool RezervisiVozilo(Rezervacija rezervacija);

        [OperationContract]
        void UkinutiVIP(string username);

        [OperationContract]
        void TraziVIP(string username);

    }
}
