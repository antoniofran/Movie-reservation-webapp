using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Klase
{
    public class Korisnik
    {
        public string userName { get; set; }
        public string userEmail { get; set; }
        public string userStatus { get; set; }

        public Korisnik()
        {
            this.userName = "none";
            this.userEmail = "none@none.com";
            this.userStatus = "anonimus";
        }

        public Korisnik(string userName, string userEmail, string userStatus)
        {
            this.userName = userName;
            this.userEmail = userEmail;
            this.userStatus = userStatus;
        }

        public override string ToString()
        {
            return userName + " | " + userEmail + " | " + userStatus;
        }
    }
}
