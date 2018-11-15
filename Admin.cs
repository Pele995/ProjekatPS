using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Service
{
    public class Admin : IAdmin
    {
 

        //admin
        public void DodajVIP(string username) // potrebno je da se promeni korisnici u xml sa vip=true
        {

            string rola;
            try
            {
                string name = ServiceSecurityContext.Current.PrimaryIdentity.Name;
                string[] aa = name.Split(';');
                string[] aaa = aa[0].Split(',');
                string celo = aaa[1];
                rola = celo.Split('=')[1];
            }
            catch
            {
                return ;
            }

            if (rola != "admin") { return ; }

            foreach (Korisnik k in Database.ZahtevZC)
            {
                if(k.Vip == false)
                {
                    if (k.IznajmljenaVozila.Count > 4)
                    {
                        if (Database.korisnici.ContainsKey(username))
                        {
                            Database.korisnici[username].Vip = true;

                            List<Korisnik> lista = new List<Korisnik>();

                            foreach (var item in Database.korisnici.Values)
                            {
                                lista.Add(item);
                            }

                            Database.ZahtevZC.Remove(k);
                            UpisivanjeKorisnika("Zahtevi.xml", Database.ZahtevZC);
                            UpisivanjeKorisnika("Korisnici.xml", lista);

                        }
                    }
                    else
                    {
                        Database.ZahtevZC.Remove(k);
                        UpisivanjeKorisnika("Zahtevi.xml", Database.ZahtevZC);

                    }
                }  
            }
        }

        //admin
        public bool DodajVozilo(Vozilo vozilo)
        {
            string rola;
            try
            {
                string name = ServiceSecurityContext.Current.PrimaryIdentity.Name;
                string[] aa = name.Split(';');
                string[] aaa = aa[0].Split(',');
                string celo = aaa[1];
                rola = celo.Split('=')[1];
            }
            catch
            {
                return false;
            }

            if (rola != "admin") { return false; }
            // u xml ito dodaj vozilo
            if (!Database.vozila.ContainsKey(vozilo.Registracija))
            {
                Database.vozila.Add(vozilo.Registracija, vozilo);
            
            
                List<Vozilo> lista = new List<Vozilo>();

                foreach (var item in Database.vozila.Values)
                {
                    lista.Add(item);
                }

                UpisivanjeVozila("Vozila.xml", lista);

                return true;
            }
            else
            {
                return false;
            }
        }

   
        //admin
        public bool IzmeniRezVozila(Rezervacija rezervacija)
        {
            //if (Database.Rezervacije.ContainsKey(rezervacija.Registracija))
            {
                //Database.Rezervacije[rezervacija.Registracija].Datum = rezervacija.Datum;
                List<Rezervacija> pom = Database.korisnici[rezervacija.KorisnickoIme].IznajmljenaVozila;

                int index = 0;
                foreach (var item in pom)
                {
                    if(item.Registracija == rezervacija.Registracija)
                    {
                        break;
                    }
                    index++;
                }
                
                Database.korisnici[rezervacija.KorisnickoIme].IznajmljenaVozila[index].Datum = rezervacija.Datum;

                List<Korisnik> lista = new List<Korisnik>();

                foreach (var item in Database.korisnici.Values)
                {
                    lista.Add(item);
                }

                UpisivanjeKorisnika("Korisnici.xml", lista);
                return true;
            }
            //return false;
        }

        //admin

        public bool IzmeniVozilo(Vozilo vozilo)
        {

            string rola;
            try
            {
                string name = ServiceSecurityContext.Current.PrimaryIdentity.Name;
                string[] aa = name.Split(';');
                string[] aaa = aa[0].Split(',');
                string celo = aaa[1];
                rola = celo.Split('=')[1];
            }
            catch
            {
                return false;
            }

            if (rola != "admin") { return false; }

            if (Database.vozila.ContainsKey(vozilo.Registracija))
            {
                Database.vozila[vozilo.Registracija] = vozilo;

                List<Vozilo> lista = new List<Vozilo>();

                foreach (var item in Database.vozila.Values)
                {
                    lista.Add(item);
                }

                UpisivanjeVozila("Vozila.xml", lista);
                return true;
            }
            else
            {
                return false;
            }
        }

        //admin
        public void ObrisiRezVozila(Rezervacija rezervacija)
        {
            //if (Database.Rezervacije.ContainsKey(rezervacija.Registracija))
            {
                
                List<Rezervacija> pom = Database.korisnici[rezervacija.KorisnickoIme].IznajmljenaVozila;
                Database.vozila[rezervacija.Registracija].Iznajmljen = false;
                //Database.Rezervacije.Remove(rezervacija.Registracija);
                int index = 0;
                foreach (var item in pom)
                {
                    if (item.Registracija == rezervacija.Registracija)
                    {
                        break;
                    }
                    index++;
                }

                Database.korisnici[rezervacija.KorisnickoIme].IznajmljenaVozila.Remove(Database.korisnici[rezervacija.KorisnickoIme].IznajmljenaVozila[index]);

                List<Korisnik> lista = new List<Korisnik>();

                foreach (var item in Database.korisnici.Values)
                {
                    lista.Add(item);
                }

                List<Vozilo> listaV = new List<Vozilo>();

                foreach (var item in Database.vozila.Values)
                {
                    listaV.Add(item);
                }


                UpisivanjeVozila("Vozila.xml", listaV);

                UpisivanjeKorisnika("Korisnici.xml", lista);
            }
        }


        //admin
        public void ObrisiVIP(string username)
        {
            string rola;
            try
            {
                string name = ServiceSecurityContext.Current.PrimaryIdentity.Name;
                string[] aa = name.Split(';');
                string[] aaa = aa[0].Split(',');
                string celo = aaa[1];
                rola = celo.Split('=')[1];
            }
            catch
            {
                return;
            }

            if (rola != "admin") { return; }

            foreach (Korisnik k in Database.ZahtevZC)
            {
                if (k.Vip == true)
                {
                        if (Database.korisnici.ContainsKey(username))
                        {
                            Database.korisnici[username].Vip = false;
                            Database.ZahtevZC.Remove(k);
                            UpisivanjeKorisnika("Zahtevi.xml", Database.ZahtevZC);
                    }   
                }
            }
        }


        //admin
        public bool UkloniVozilo(string registracija)
        {
            string rola;
            try
            {
                string name = ServiceSecurityContext.Current.PrimaryIdentity.Name;
                string[] aa = name.Split(';');
                string[] aaa = aa[0].Split(',');
                string celo = aaa[1];
                rola = celo.Split('=')[1];
            }
            catch
            {
                return false;
            }

            if (rola != "admin") { return false; }

            if (Database.vozila.ContainsKey(registracija))
            {
                Database.vozila.Remove(registracija);
                List<Vozilo> lista = new List<Vozilo>();

                foreach (var item in Database.vozila.Values)
                {
                    lista.Add(item);
                }

                UpisivanjeVozila("Vozila.xml", lista);
                return true;
            }
            else
            {
                return false;
            }
        }

        //korisnik i vip
        public bool RezervisiVozilo(Rezervacija rezervacija)
        {
            if (!Database.korisnici.ContainsKey(rezervacija.KorisnickoIme))
            {
                Database.korisnici[rezervacija.KorisnickoIme]=(new Korisnik(rezervacija.KorisnickoIme));
            }
            if (Database.vozila.ContainsKey(rezervacija.Registracija))
            {
                if (Database.vozila[rezervacija.Registracija].Iznajmljen == false)
                {
                    Database.vozila[rezervacija.Registracija].Iznajmljen = true;

                    if (Database.korisnici[rezervacija.KorisnickoIme].Vip == true)
                    {
                        double aa = rezervacija.Cena * rezervacija.Broj_dana;
                        Database.korisnici[rezervacija.KorisnickoIme].Money = Database.korisnici[rezervacija.KorisnickoIme].Money - (aa * 0.95);
                    }
                    else
                    {
                        double aa = rezervacija.Cena * rezervacija.Broj_dana;
                        Database.korisnici[rezervacija.KorisnickoIme].Money = Database.korisnici[rezervacija.KorisnickoIme].Money - (aa);
                    }

                    Database.Rezervacije[rezervacija.Registracija] = rezervacija;
                    Database.korisnici[rezervacija.KorisnickoIme].IznajmljenaVozila.Add(rezervacija);

                    List<Korisnik> lista = new List<Korisnik>();

                    foreach (var item in Database.korisnici.Values)
                    {
                        lista.Add(item);
                    }

                    List<Vozilo> listaV = new List<Vozilo>();

                    foreach (var item in Database.vozila.Values)
                    {
                        listaV.Add(item);
                    }

                    
                    UpisivanjeKorisnika("Korisnici.xml", lista);
                    UpisivanjeVozila("Vozila.xml", listaV);
                    return true;
                }
                return false;
            }
            return false;

        }

        //vip
        public void UkinutiVIP(string username)
        {
            string rola;
            try
            {
                string name = ServiceSecurityContext.Current.PrimaryIdentity.Name;
                string[] aa = name.Split(';');
                string[] aaa = aa[0].Split(',');
                string celo = aaa[1];
                rola = celo.Split('=')[1];
            }
            catch
            {
                return ;
            }

            if (rola != "clan") { return ; }

            if (Database.korisnici.ContainsKey(username))
            {
                Database.ZahtevZC.Add(Database.korisnici[username]);

                UpisivanjeKorisnika("Zahtevi.xml", Database.ZahtevZC);
            }
        }

        //korisnik
        public void TraziVIP(string username)
        {
            string rola;
            try
            {
                string name = ServiceSecurityContext.Current.PrimaryIdentity.Name;
                string[] aa = name.Split(';');
                string[] aaa = aa[0].Split(',');
                string celo = aaa[1];
                rola = celo.Split('=')[1];
            }
            catch
            {
                return;
            }

            if (rola != "clan") { return; }

            if (Database.korisnici.ContainsKey(username))
            {
                Database.ZahtevZC.Add(Database.korisnici[username]);

                UpisivanjeKorisnika("Zahtevi.xml", Database.ZahtevZC);
            }
        }

        public void UpisivanjeKorisnika(string filename, List<Korisnik>korisnici)
        {
            DataContractSerializer dcs = new DataContractSerializer(typeof(List<Korisnik>));

            using (Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(stream, Encoding.UTF8))
                {
                    writer.WriteStartDocument();
                    dcs.WriteObject(writer, korisnici);
                }
            }
        }

        public void UpisivanjeVozila(string filename, List<Vozilo> vozila)
        {
            DataContractSerializer dcs = new DataContractSerializer(typeof(List<Vozilo>));

            using (Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(stream, Encoding.UTF8))
                {
                    writer.WriteStartDocument();
                    dcs.WriteObject(writer, vozila);
                }
            }
        }

    }
}
