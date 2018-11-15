using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class Rezervacija
    {
        private string registracija;
        private double cena;
        private int broj_dana;
        private DateTime datum;
        private string korisnickoIme;
        

        public Rezervacija() { }

        public Rezervacija(double cena, int broj_dana, DateTime datum, string korisnickoIme)
        {
            this.Cena = 10;
            this.Broj_dana = broj_dana;
            this.Datum = datum;
            this.KorisnickoIme = korisnickoIme;
        }

        [DataMember]
        public double Cena
        {
            get
            {
                return cena;
            }

            set
            {
                cena = value;
            }
        }


        [DataMember]
        public string Registracija
        {
            get
            {
                return registracija;
            }

            set
            {
                registracija = value;
            }
        }

        [DataMember]
        public int Broj_dana
        {
            get
            {
                return broj_dana;
            }

            set
            {
                broj_dana = value;
            }
        }

        [DataMember]
        public DateTime Datum
        {
            get
            {
                return datum;
            }

            set
            {
                datum = value;
            }
        }

        [DataMember]
        public string KorisnickoIme
        {
            get
            {
                return korisnickoIme;
            }

            set
            {
                korisnickoIme = value;
            }
        }



    }
}
