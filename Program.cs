using CertificateMenager;
using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Iscitavanje iz baze
            Console.ReadLine();
            List<Vozilo> iscitanavozila = new List<Vozilo>();

            DataContractSerializer dcs = new DataContractSerializer(typeof(List<Vozilo>));

            using (Stream stream = new FileStream("Vozila.xml", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas()))
                {
                    reader.ReadContentAsObject();
                    iscitanavozila = (List<Vozilo>)dcs.ReadObject(reader);
                }
            }

            foreach (var item in iscitanavozila)
            {
                Database.vozila[item.Registracija] = item;
            }

            List<Korisnik> iscitaniKorisnici = new List<Korisnik>();

            DataContractSerializer dcs1 = new DataContractSerializer(typeof(List<Korisnik>));

            using (Stream stream = new FileStream("Korisnici.xml", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas()))
                {
                    reader.ReadContentAsObject();
                    iscitaniKorisnici = (List<Korisnik>)dcs1.ReadObject(reader);
                }
            }

            foreach (var item in iscitaniKorisnici)
            {
                Database.korisnici[item.Username] = item;
            }

            List<Korisnik> iscitaniZahtevi = new List<Korisnik>();

            DataContractSerializer dcs2 = new DataContractSerializer(typeof(List<Korisnik>));

            using (Stream stream = new FileStream("Zahtevi.xml", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas()))
                {
                    reader.ReadContentAsObject();
                    iscitaniZahtevi = (List<Korisnik>)dcs2.ReadObject(reader);
                }
            }
            foreach (var item in iscitaniKorisnici)
            {
                Database.ZahtevZC.Add(item);
            }

            #endregion

            //string n = WindowsIdentity.GetCurrent().Name;
            string srvCertCN = CertificateMenager.Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            string address = "net.tcp://localhost:9999/Receiver";

            ServiceHost host = new ServiceHost(typeof(Admin));
            host.AddServiceEndpoint(typeof(IAdmin), binding, address);
            host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.ChainTrust;

            host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            ///Set appropriate service's certificate on the host. Use CertManager class to obtain the certificate based on the "srvCertCN"
            host.Credentials.ServiceCertificate.Certificate = CertMenager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);

            host.Open();
            Console.WriteLine("Server pokrenut");
            Console.ReadLine();
            host.Close();
        }
    }
}
