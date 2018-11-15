using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Vip : IVip
    {
        public void RezervisiVozilo(Rezervacija rezervacija)
        {
            if (Database.vozila.ContainsKey(rezervacija.Registracija))
            {
                Database.rezervacijeUObradi.Add(rezervacija);
            }
            
        }

        public void UkinutiVIP(Korisnik korisnik)
        {
            if (Database.korisnici.ContainsKey(korisnik.Username))
            {
                Database.UklanjanjeZC.Add(korisnik);
            }
        }
    }
}
