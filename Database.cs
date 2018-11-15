using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Database
    {
        public static readonly Dictionary<string, Korisnik> korisnici = new Dictionary<string, Korisnik>();
        public static readonly Dictionary<string, Vozilo> vozila = new Dictionary<string, Vozilo>();
        public static readonly List<Rezervacija> rezervacijeUObradi = new List<Rezervacija>();
        public static readonly Dictionary<string,Rezervacija> Rezervacije = new Dictionary<string, Rezervacija>();
        public static readonly List<Korisnik> ZlatniClan = new List<Korisnik>();
        public static readonly List<Korisnik> ZahtevZC = new List<Korisnik>();
        public static readonly List<Korisnik> UklanjanjeZC = new List<Korisnik>();
    }
}
