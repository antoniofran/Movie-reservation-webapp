using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Klase
{
    public class Film
    {
        public int idFilma { get; set; }
        public string nazivFilma { get; set; }
        public string opisFilma { get; set; }
        public string nazivDatoteke { get; set; }

        public Film()
        {
            this.idFilma = -1;
            this.nazivFilma = "";
            this.opisFilma = "";
            this.nazivDatoteke = "";
        }

        public Film(int idFilma, string nazivFilma, string opisFilma, string nazivDatoteke)
        {
            this.idFilma = idFilma;
            this.nazivFilma = nazivFilma;
            this.opisFilma = opisFilma;
            this.nazivDatoteke = nazivDatoteke;
        }
    }
}