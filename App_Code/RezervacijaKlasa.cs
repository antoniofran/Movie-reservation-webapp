using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Klase
{
    public class RezervacijaKlasa
    {
        public int idRezervacije { get; set; }
        public string nazivFilma { get; set; }
        public string odabraniDan { get; set; }
        public string odabraniSat { get; set; }
        public int brojDvorane { get; set; }
        public string rezervSjed { get; set; }
        public string potvrdjeno { get; set; }

        public RezervacijaKlasa()
        {
            this.idRezervacije = -1;
            this.nazivFilma = "";
            this.odabraniDan = "";
            this.odabraniSat = "";
            this.brojDvorane = -1;
            this.rezervSjed = "";
            this.potvrdjeno = "NE";
        }

        public RezervacijaKlasa(int idRezervacije, string nazivFilma, string odabraniDan, string odabraniSat, int brojDvorane, string rezervSjed, string potvrdjeno)
        {
            this.idRezervacije = idRezervacije;
            this.nazivFilma = nazivFilma;
            this.odabraniDan = odabraniDan;
            this.odabraniSat = odabraniSat;
            this.brojDvorane = brojDvorane;
            this.rezervSjed = rezervSjed;
            this.potvrdjeno = potvrdjeno;
        }

        public override string ToString()
        {
            return "#" + idRezervacije.ToString().PadLeft(4, '0') + " | \"" + nazivFilma + "\" | " + odabraniDan + " u " + odabraniSat + " | Dv. " + brojDvorane + " | Sjed. " + rezervSjed + " | \"" + potvrdjeno + "\"";
        }
    }
}