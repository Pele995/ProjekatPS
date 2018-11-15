using Common;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Clan : IClan
    {
        public void RezervisiVozilo(Rezervacija rezervacija)
        {
            if (Database.vozila.ContainsKey(rezervacija.Registracija))
            {
                Database.rezervacijeUObradi.Add(rezervacija);
            }
        }

        public void TraziVIP(Korisnik korisnik)
        {
            if (Database.korisnici.ContainsKey(korisnik.Username))
            {
                Database.ZahtevZC.Add(korisnik);
            }
        }
    }
}
