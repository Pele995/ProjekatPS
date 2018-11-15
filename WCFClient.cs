using CertificateMenager;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class WCFClient : ChannelFactory<IAdmin>, IAdmin, IDisposable
    {

        IAdmin factory;

        public WCFClient(NetTcpBinding binding, EndpointAddress address)
            : base(binding, address)
        {
            /// cltCertCN.SubjectName should be set to the client's username. .NET WindowsIdentity class provides information about Windows user running the given process
            //string n = WindowsIdentity.GetCurrent().Name;
            string cltCertCN = CertificateMenager.Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.ChainTrust;
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
            this.Credentials.ClientCertificate.Certificate = CertMenager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);

            factory = this.CreateChannel();
        }


        public void DodajVIP(string username)
        {
            factory.DodajVIP(username);
        }

        public bool DodajVozilo(Vozilo vozilo)
        {
            bool retVal = false;
            try
            {
                retVal = factory.DodajVozilo(vozilo);
                return retVal;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public bool IzmeniRezVozila(Rezervacija rezervacija)
        {
            bool retVal = false;
            try
            {
                retVal = factory.IzmeniRezVozila(rezervacija);
                return retVal;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool IzmeniVozilo(Vozilo vozilo)
        {
            bool retVal = false;
            try
            {
                retVal = factory.IzmeniVozilo(vozilo);
                return retVal;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void ObrisiRezVozila(Rezervacija rezervacija)
        {
            factory.ObrisiRezVozila(rezervacija);
        }

        public void ObrisiVIP(string username)
        {
            factory.ObrisiVIP(username);
        }

        public bool RezervisiVozilo(Rezervacija rezervacija)
        {
            bool retVal = false;
            try
            {
                retVal = factory.RezervisiVozilo(rezervacija);
                return retVal;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void TraziVIP(string username)
        {
            factory.TraziVIP(username);
        }

        public void UkinutiVIP(string username)
        {
            factory.UkinutiVIP(username);
        }

        public bool UkloniVozilo(string registracija)
        {
            bool retVal = false;
            try
            {
                retVal = factory.UkloniVozilo(registracija);
                return retVal;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }

            this.Close();
        }

    }
}
