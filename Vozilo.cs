using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{

    public enum TipVozila {Limuzina= 0, Kombi, Auto }
    [DataContract]
    public class Vozilo
    {

        private string registracija;
        private TipVozila tip;
        private bool iznajmljen = false;

        public Vozilo() { }

        public Vozilo(string reg, TipVozila t)
        {
            Registracija = reg;
            Tip = t;
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
        public TipVozila Tip
        {
            get
            {
                return tip;
            }

            set
            {
                tip = value;
            }
        }

        [DataMember]
        public bool Iznajmljen
        {
            get
            {
                return iznajmljen;
            }

            set
            {
                iznajmljen = value;
            }
        }

  


    }
}
