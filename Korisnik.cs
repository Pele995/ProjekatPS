using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class Korisnik
    {
        private string username;
        private bool vip = false;
        private double money;

        private List<Rezervacija> iznajmljenaVozila;

        public Korisnik(string user)
        {
            Username = user;
            vip = false;
            Money = 10000;
            IznajmljenaVozila = new List<Rezervacija>();
        }


        [DataMember]
        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        [DataMember]
        public bool Vip
        {
            get
            {
                return vip;
            }

            set
            {
                vip = value;
            }
        }

        [DataMember]
        public List<Rezervacija> IznajmljenaVozila
        {
            get
            {
                return iznajmljenaVozila;
            }

            set
            {
                iznajmljenaVozila = value;
            }
        }

        [DataMember]
        public double Money
        {
            get
            {
                return money;
            }

            set
            {
                money = value;
            }
        }




        //public bool Vip { get => vip; set => vip = value; }
        //public List<Rezervacija> IznajmljenaVozila { get => iznajmljenaVozila; set => iznajmljenaVozila = value; }
    }
}
