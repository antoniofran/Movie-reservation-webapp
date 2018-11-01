using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Configuration;
using System.Data.OleDb;

namespace Klase
{
    public class Metode
    {
        public static string dohvatiUserStatus()
        {
            String tempUserStatus = "anonimus";

            OleDbConnection konekcija = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);

            konekcija.Open();

            OleDbCommand upit = new OleDbCommand("SELECT userstatus FROM korisnici WHERE [username]=@username;", konekcija);

            upit.Parameters.AddWithValue("@username", HttpContext.Current.User.Identity.Name);

            tempUserStatus = (string)upit.ExecuteScalar();

            konekcija.Close();

            return tempUserStatus;
        }

        public static Film dohvatiFilm(int idFilma)
        {
            Film tempFilm = null;

            OleDbConnection konekcija = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);

            konekcija.Open();

            OleDbCommand upit = new OleDbCommand("SELECT * FROM filmovi WHERE [ID]=@ID;", konekcija);

            upit.Parameters.AddWithValue("@ID", idFilma);

            OleDbDataReader dataSet = upit.ExecuteReader();

            while (dataSet.Read())
            {
                tempFilm = new Film();

                tempFilm.idFilma = idFilma;

                tempFilm.nazivFilma = dataSet["NazivFilma"].ToString();

                tempFilm.opisFilma = dataSet["OpisFilma"].ToString();

                tempFilm.nazivDatoteke = dataSet["NazivDatoteke"].ToString();
            }

            dataSet.Close();

            konekcija.Close();

            return tempFilm;
        }

        public static string napraviIspisFilma(Film tempFilm)
        {
            string tempInnerHtml = "";
            
            tempInnerHtml += "<section class=\"movieBlock\">";

            tempInnerHtml += "<div class=\"movieImage\">";

            tempInnerHtml += "<img src=\"Images/MoviePosters/" + tempFilm.nazivDatoteke + ".jpg\">";

            tempInnerHtml += "<a href=\"Galerija.aspx?movienum=" + tempFilm.idFilma + "\">(galerija slika)</a>";

            tempInnerHtml += "</div>";

            tempInnerHtml += "<div class=\"movieInfo\">";

            tempInnerHtml += "<p class=\"movieName\">";

            tempInnerHtml += tempFilm.nazivFilma;

            tempInnerHtml += "</p>";

            tempInnerHtml += "<p class=\"movieDesc\">";

            tempInnerHtml += tempFilm.opisFilma;

            tempInnerHtml += "</p>";

            tempInnerHtml += "<p class=\"movieRezerv\">";

            tempInnerHtml += "<span><b>Rezervacija:</b> </span>";

            tempInnerHtml += "<select id=\"listaDanaTjedna" + tempFilm.idFilma + "\">";

            tempInnerHtml += "<option value=\"1\">Ponedjeljak</option>";
            tempInnerHtml += "<option value=\"2\">Utorak</option>";
            tempInnerHtml += "<option value=\"3\">Srijeda</option>";
            tempInnerHtml += "<option value=\"4\">Četvrtak</option>";
            tempInnerHtml += "<option value=\"5\">Petak</option>";
            tempInnerHtml += "<option value=\"6\">Subota</option>";
            tempInnerHtml += "<option value=\"7\">Nedjelja</option>";

            tempInnerHtml += "</select>";

            tempInnerHtml += "<select id=\"listaVremena" + tempFilm.idFilma + "\">";

            tempInnerHtml += "<option value=\"9\">09:00</option>";
            tempInnerHtml += "<option value=\"12\">12:00</option>";
            tempInnerHtml += "<option value=\"15\">15:00</option>";
            tempInnerHtml += "<option value=\"18\">18:00</option>";
            tempInnerHtml += "<option value=\"21\">21:00</option>";

            tempInnerHtml += "</select>";

            tempInnerHtml += "<input type=\"button\" value=\"Rezerviraj\" onclick=\"otvoriDvoranu(" + tempFilm.idFilma + ");\"/>";

            tempInnerHtml += "</p>";

            tempInnerHtml += "</div>";

            tempInnerHtml += "<div style=\"clear: both;\"></div>";

            tempInnerHtml += "</section>";

            return tempInnerHtml;
        }

        public static string napraviIspisGalerijeFilma(Film tempFilm, string pathSlikaFilma)
        {
            string tempInnerHtml = "";

            tempInnerHtml += "<h2>Galerija slika za film: \"" + tempFilm.nazivFilma + "\"</h2>";

            tempInnerHtml += "<div class=\"movieGallery\">";

            foreach (string slikaFilma in dohvatiGalerijuSlikaFilma(pathSlikaFilma))
            {
                tempInnerHtml += "<img src=\"Images/MovieGalleries/" + tempFilm.nazivDatoteke + "/" + slikaFilma + "\">";
            }

            tempInnerHtml += "<div style=\"clear: both;\"></div>";

            tempInnerHtml += "</div>";

            return tempInnerHtml;
        }

        public static List<string> dohvatiGalerijuSlikaFilma(string dirPath)
        {
            List<string> tempGalerijuSlikaFilma = new List<string>();

            foreach ( var file in new DirectoryInfo(dirPath).GetFiles() ) {
                if (file.Name.ToLower().EndsWith(".jpg") || file.Name.ToLower().EndsWith(".png"))
                {
                    tempGalerijuSlikaFilma.Add(file.Name);
                }
            }

            return tempGalerijuSlikaFilma;
        }

        public static List<Film> dohvatiListuFilmova()
        {
            List<Film> tempListaFilmova = new List<Film>();

            OleDbConnection konekcija = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);

            konekcija.Open();

            OleDbCommand upit = new OleDbCommand("SELECT * FROM filmovi;", konekcija);

            OleDbDataReader dataSet = upit.ExecuteReader();

            while (dataSet.Read())
            {
                Film tempFilm = new Film();

                tempFilm.idFilma = Convert.ToInt32(dataSet["ID"].ToString());

                tempFilm.nazivFilma = dataSet["NazivFilma"].ToString();

                tempFilm.opisFilma = dataSet["OpisFilma"].ToString();

                tempFilm.nazivDatoteke = dataSet["NazivDatoteke"].ToString();

                tempListaFilmova.Add(tempFilm);
            }

            dataSet.Close();

            konekcija.Close();

            return tempListaFilmova;
        }

        public static string dohvatiStringSjedala(string filmDanSat)
        {
            //inicijalizacija variable

            object tempStringSjedalaObject = null;

            //otvaranje konekcije

            OleDbConnection konekcija = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);

            konekcija.Open();

            //pokušaj pronalaska elemenenta

            OleDbCommand upit = new OleDbCommand("SELECT StanjeDvorane FROM predstave WHERE [FilmDanSat]=@FilmDanSat;", konekcija);

            upit.Parameters.AddWithValue("@FilmDanSat", filmDanSat);

            tempStringSjedalaObject = upit.ExecuteScalar();

            //stvaranje novog elementa

            if (tempStringSjedalaObject == null)
            {
                OleDbCommand upit2 = new OleDbCommand("INSERT INTO predstave([FilmDanSat]) VALUES(@FilmDanSat);", konekcija);

                upit2.Parameters.AddWithValue("@FilmDanSat", filmDanSat);

                int brojRedakaUneseno = upit2.ExecuteNonQuery();

                /* ******************* */

                OleDbCommand upit3 = new OleDbCommand("SELECT StanjeDvorane FROM predstave WHERE [FilmDanSat]=@FilmDanSat;", konekcija);

                upit3.Parameters.AddWithValue("@FilmDanSat", filmDanSat);

                tempStringSjedalaObject = upit3.ExecuteScalar();
            }

            //zatvaranje konekcije

            konekcija.Close();

            //povratna vrijednost

            return tempStringSjedalaObject.ToString();
        }

        public static void spremiRezervaciju(string filmDanSat, int brojDvorane, string indexiRezervSjedala, string novoStanjeDvorane, string kreatorUser)
        {            
            OleDbConnection konekcija = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);

            konekcija.Open();

            /* ********************* */

            OleDbCommand upit = new OleDbCommand("SELECT * FROM rezervacije WHERE [FilmDanSat]=@FilmDanSat AND [BrojDvorane]=@BrojDvorane AND [IndexiRezervSjed]=@IndexiRezervSjed;", konekcija);

            upit.Parameters.AddWithValue("@FilmDanSat", filmDanSat);
            upit.Parameters.AddWithValue("@BrojDvorane", brojDvorane);
            upit.Parameters.AddWithValue("@IndexiRezervSjed", indexiRezervSjedala);

            bool pokusajUnosaDuplikata = upit.ExecuteReader().HasRows;

            /* ********************* */

            if (!pokusajUnosaDuplikata)
            {
                OleDbCommand upit2 = new OleDbCommand("UPDATE predstave SET [StanjeDvorane]=@StanjeDvorane WHERE [FilmDanSat]=@FilmDanSat;", konekcija);

                upit2.Parameters.AddWithValue("@StanjeDvorane", novoStanjeDvorane);

                upit2.Parameters.AddWithValue("@FilmDanSat", filmDanSat);

                int azuriranoRedaka2 = upit2.ExecuteNonQuery();

                /* ********************* */

                OleDbCommand upit3 = new OleDbCommand("INSERT INTO rezervacije([FilmDanSat], [BrojDvorane], [IndexiRezervSjed], [KreatorUser]) VALUES(@FilmDanSat, @BrojDvorane, @IndexiRezervSjed, @KreatorUser);", konekcija);

                upit3.Parameters.AddWithValue("@FilmDanSat", filmDanSat);
                upit3.Parameters.AddWithValue("@BrojDvorane", brojDvorane);
                upit3.Parameters.AddWithValue("@IndexiRezervSjed", indexiRezervSjedala);
                upit3.Parameters.AddWithValue("@KreatorUser", kreatorUser);

                int azuriranoRedaka3 = upit3.ExecuteNonQuery();
            }

            /* ********************* */

            konekcija.Close();
        }

        public static void urediRezervaciju(int idUredjivaneRezervacije, string filmDanSat, string noviIndexiSjedala, string novoStanjeDvorane)
        {
            OleDbConnection konekcija = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);

            konekcija.Open();

            /* **************** */

            OleDbCommand upit = new OleDbCommand("UPDATE predstave SET [StanjeDvorane]=@StanjeDvorane WHERE [FilmDanSat]=@FilmDanSat;", konekcija);

            upit.Parameters.AddWithValue("@StanjeDvorane", novoStanjeDvorane);

            upit.Parameters.AddWithValue("@FilmDanSat", filmDanSat);

            int azuriranoRedaka = upit.ExecuteNonQuery();

            /* **************** */

            OleDbCommand upit2 = new OleDbCommand("UPDATE rezervacije SET [IndexiRezervSjed]=@IndexiRezervSjed WHERE [ID]=@ID;", konekcija);

            upit2.Parameters.AddWithValue("@IndexiRezervSjed", noviIndexiSjedala);

            upit2.Parameters.AddWithValue("@ID", idUredjivaneRezervacije);

            int redovaAzurirano2 = upit2.ExecuteNonQuery();

            /* **************** */

            konekcija.Close();
        }

        public static void potvrdiRezervaciju(int idRezervacije)
        {
            OleDbConnection konekcija = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);

            konekcija.Open();

            OleDbCommand upit = new OleDbCommand("UPDATE rezervacije SET [Potvrdjeno]=@Potvrdjeno WHERE [ID]=@ID;", konekcija);

            upit.Parameters.AddWithValue("@Potvrdjeno", "DA");

            upit.Parameters.AddWithValue("@ID", idRezervacije);

            int obrisanoRedaka = upit.ExecuteNonQuery();

            konekcija.Close();
        }

        public static string dohvatiFilmDanSat(int idRezervacije)
        {
            string tempFilmDanSat = "";
            
            OleDbConnection konekcija = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);

            konekcija.Open();

            OleDbCommand upit = new OleDbCommand("SELECT [FilmDanSat] FROM rezervacije WHERE [ID]=@ID;", konekcija);

            upit.Parameters.AddWithValue("@ID", idRezervacije);

            tempFilmDanSat = upit.ExecuteScalar().ToString();

            konekcija.Close();

            return tempFilmDanSat;
        }

        public static void oslobodiSjedala(string filmDanSat, string sjedalaIndexi)
        {

            string tempNovoStanjeDvorane = "";

            /* ******************* */

            OleDbConnection konekcija = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);

            konekcija.Open();

            /* ******************* */

            OleDbCommand upit = new OleDbCommand("SELECT [StanjeDvorane] FROM predstave WHERE [FilmDanSat]=@FilmDanSat;", konekcija);

            upit.Parameters.AddWithValue("@FilmDanSat", filmDanSat);

            string tempStaroStanjeDvorane = upit.ExecuteScalar().ToString();

            /* ******************* */

            char[] tempNovoStanjeDvoraneArray = tempStaroStanjeDvorane.ToCharArray();

            foreach ( string index in sjedalaIndexi.Split(',') )
            {
                tempNovoStanjeDvoraneArray[Convert.ToInt32(index)] = 'O';
            }

            tempNovoStanjeDvorane = new string(tempNovoStanjeDvoraneArray);

            /* ******************* */

            OleDbCommand upit2 = new OleDbCommand("UPDATE predstave SET [StanjeDvorane]=@StanjeDvorane WHERE [FilmDanSat]=@FilmDanSat;", konekcija);

            upit2.Parameters.AddWithValue("@StanjeDvorane", tempNovoStanjeDvorane);

            upit2.Parameters.AddWithValue("@FilmDanSat", filmDanSat);

            int redovaAzurirano2 = upit2.ExecuteNonQuery();

            /* ******************* */

            konekcija.Close();
        }

        public static List<string> dohvatiListuRezervacijaString(string userNameKorisnika = "")
        {
            List<string> tempListaRezervacijaString = new List<string>();

            OleDbConnection konekcija = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);

            konekcija.Open();

            /* ****************** */

            OleDbCommand upit = null;

            if (userNameKorisnika == "")
            {
                upit = new OleDbCommand("SELECT * FROM rezervacije;", konekcija);
            }
            else
            {
                upit = new OleDbCommand("SELECT * FROM rezervacije WHERE [KreatorUser]=@KreatorUser;", konekcija); ;

                upit.Parameters.AddWithValue("@KreatorUser", userNameKorisnika);
            }

            /* ****************** */

            OleDbDataReader dataSet = upit.ExecuteReader();

            while (dataSet.Read())
            {
                RezervacijaKlasa tempRezervacija = new RezervacijaKlasa();

                tempRezervacija.idRezervacije = Convert.ToInt32(dataSet["ID"].ToString());

                /* ***************** */

                string[] filmDanSatPolje = dataSet["FilmDanSat"].ToString().Split('+');

                /* ***************** */

                tempRezervacija.nazivFilma = Metode.dohvatiFilm(Convert.ToInt32(filmDanSatPolje[0])).nazivFilma;

                /* ***************** */

                switch (Convert.ToInt32(filmDanSatPolje[1]))
                {
                    case 1: tempRezervacija.odabraniDan = "Pon"; break;
                    case 2: tempRezervacija.odabraniDan = "Uto"; break;
                    case 3: tempRezervacija.odabraniDan = "Sri"; break;
                    case 4: tempRezervacija.odabraniDan = "Čet"; break;
                    case 5: tempRezervacija.odabraniDan = "Pet"; break;
                    case 6: tempRezervacija.odabraniDan = "Sub"; break;
                    case 7: tempRezervacija.odabraniDan = "Ned"; break;
                }

                /* ***************** */

                switch (Convert.ToInt32(filmDanSatPolje[2]))
                {
                    case 9: tempRezervacija.odabraniSat = "09:00"; break;
                    case 12: tempRezervacija.odabraniSat = "12:00"; break;
                    case 15: tempRezervacija.odabraniSat = "15:00"; break;
                    case 18: tempRezervacija.odabraniSat = "18:00"; break;
                    case 21: tempRezervacija.odabraniSat = "21:00"; break;
                }

                /* ***************** */

                tempRezervacija.brojDvorane = Convert.ToInt32(dataSet["BrojDvorane"].ToString());

                tempRezervacija.rezervSjed = dataSet["IndexiRezervSjed"].ToString();

                tempRezervacija.potvrdjeno = dataSet["Potvrdjeno"].ToString();

                /* ***************** */

                if (tempRezervacija.rezervSjed != "")
                {
                    tempListaRezervacijaString.Add(tempRezervacija.ToString());
                }
                
            }

            dataSet.Close();

            konekcija.Close();

            return tempListaRezervacijaString;
        }

        public static List<string> dohvatiListuKorisnikaString()
        {
            List<string> tempListaKorisnikaString = new List<string>();

            OleDbConnection konekcija = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);

            konekcija.Open();

            OleDbCommand upit = new OleDbCommand("SELECT * FROM korisnici;", konekcija);

            OleDbDataReader dataSet = upit.ExecuteReader();

            while (dataSet.Read())
            {
                Korisnik tempKorisnik = new Korisnik();

                tempKorisnik.userName = dataSet["username"].ToString();

                tempKorisnik.userEmail = dataSet["useremail"].ToString();

                tempKorisnik.userStatus = dataSet["userstatus"].ToString();

                tempListaKorisnikaString.Add(tempKorisnik.ToString());
            }

            dataSet.Close();

            konekcija.Close();

            return tempListaKorisnikaString;
        }
    }
}
