using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;

using Klase;

public partial class Rezervacija : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Film tempFilm = Metode.dohvatiFilm( Convert.ToInt32(Request.QueryString["film"]) );

        if (tempFilm != null)
        {
            NazivFilmaLabel.Text = tempFilm.nazivFilma;

            /* ***************** */

            int odabraniDan = Convert.ToInt32(Request.QueryString["dan"]);

            string odabraniDanString = "";

            switch (odabraniDan)
            {
                case 1: odabraniDanString = "Ponedjeljak"; break;
                case 2: odabraniDanString = "Utorak"; break;
                case 3: odabraniDanString = "Srijeda"; break;
                case 4: odabraniDanString = "Četvrtak"; break;
                case 5: odabraniDanString = "Petak"; break;
                case 6: odabraniDanString = "Subota"; break;
                case 7: odabraniDanString = "Nedjelja"; break;
                default: return;
            }

            OdabraniDanLabel.Text = odabraniDanString;

            /* ***************** */

            int odabraniSat = Convert.ToInt32(Request.QueryString["sat"]);

            string odabraniSatString = "";

            switch (odabraniSat)
            {
                case 9: odabraniSatString = "09:00"; break;
                case 12: odabraniSatString = "12:00"; break;
                case 15: odabraniSatString = "15:00"; break;
                case 18: odabraniSatString = "18:00"; break;
                case 21: odabraniSatString = "21:00"; break;
                default: return;
            }

            OdabraniSatLabel.Text = odabraniSatString;

            /* ***************** */

            DvoranaLabel.Text = tempFilm.idFilma.ToString();

            /* ***************** */

            string tempFilmDanSat = tempFilm.idFilma + "+" + odabraniDan + "+" + odabraniSat;

            string tempStringSjedala = Metode.dohvatiStringSjedala(tempFilmDanSat);

            /* ***************** */

            bool spremanjeRezervacije = (Request.QueryString["sjed"] != null);

            /* ***************** */

            if (spremanjeRezervacije)
            {
                string tempIndexiSjedalaString = Request.QueryString["sjed"];

                if (tempIndexiSjedalaString.Length > 0)
                {
                    int tempMaxIndexSjedala = tempStringSjedala.Length - 1;

                    /* ******************* */

                    char[] tempNoviStringSjedalaArray = tempStringSjedala.ToCharArray();

                    foreach ( string index in tempIndexiSjedalaString.Split(',') )
                    {
                        int tempIntIndex = Convert.ToInt32(index);

                        if ( (tempIntIndex >= 0) && (tempIntIndex <= tempMaxIndexSjedala) )
                        {
                            if (tempNoviStringSjedalaArray[tempIntIndex] != '|')
                            {
                                tempNoviStringSjedalaArray[tempIntIndex] = 'X';
                            }
                            else
                            {
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Krivo odabrana sjedala.');", true);

                                spremanjeRezervacije = false;

                                break;
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Krivo odabrana sjedala.');", true);

                            spremanjeRezervacije = false;

                            break;
                        }
                    }

                    string tempNovoStanjeDvorane = new string(tempNoviStringSjedalaArray);

                    /* ******************* */

                    string tempKreatorUser = HttpContext.Current.User.Identity.Name;

                    /* ******************* */

                    if (tempKreatorUser == string.Empty)
                    {
                        tempKreatorUser = "anonimus";
                    }

                    /* ******************* */

                    if (spremanjeRezervacije)
                    {
                        if ( Request.QueryString["rez"] != null )
                        {
                            if (Session["uredjivanaRezervacijaSjed"] != null)
                            {
                                int tempIdUredjivaneRezervacije = Convert.ToInt32(Request.QueryString["rez"]);

                                Metode.urediRezervaciju(tempIdUredjivaneRezervacije, tempFilmDanSat, tempIndexiSjedalaString, tempNovoStanjeDvorane);

                                Session.Remove("uredjivanaRezervacijaSjed");

                                tempStringSjedala = ""; //stanje dvorane neće biti ispisano
                            }
                            else
                            {
                                Response.Redirect("~/Default.aspx");
                            }
                        }
                        else
                        {
                            Metode.spremiRezervaciju(tempFilmDanSat, tempFilm.idFilma, tempIndexiSjedalaString, tempNovoStanjeDvorane, tempKreatorUser);

                            tempStringSjedala = ""; //stanje dvorane neće biti ispisano
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Niste odabrali sjedala.');", true);

                    spremanjeRezervacije = false;
                }
            }
            else
            {
                if (Session["isUredjivanjeRezervacije"] != null)
                {
                    string tempIndexiSjedalaString = Session["uredjivanaRezervacijaSjed"].ToString();

                    /* ******************* */

                    char[] tempNoviStringSjedalaArray = tempStringSjedala.ToCharArray();

                    foreach (string index in tempIndexiSjedalaString.Split(','))
                    {
                        tempNoviStringSjedalaArray[Convert.ToInt32(index)] = 'W';
                    }

                    tempStringSjedala = new string(tempNoviStringSjedalaArray);

                    /* ******************* */
                    
                    Session.Remove("isUredjivanjeRezervacije");
                }
            }

            /* ***************** */

            Label tempSjedalaIzbornik = new Label();

            tempSjedalaIzbornik.ID = "MovieGalleryLabel" + tempFilm.idFilma;

            /* ***************** */

            string tempInnerHtml = "";

            /* ***************** */

            tempInnerHtml += "<div id=\"pregledSjedala\">";

            for (int i = 0; i < tempStringSjedala.Length; i++)
            {
                switch (tempStringSjedala[i])
                {
                    case 'W': tempInnerHtml += "<input type=\"checkbox\" class=\"freeSit\" name=\"strIndex" + i + "\" value=\"true\" checked>"; break;
                    case 'O': tempInnerHtml += "<input type=\"checkbox\" class=\"freeSit\" name=\"strIndex" + i + "\" value=\"true\">"; break;
                    case 'X': tempInnerHtml += "<input type=\"checkbox\" class=\"nonFreeSit\" name=\"strIndex" + i + "\" value=\"true\" disabled readonly checked>"; break;
                    default: tempInnerHtml += "<br>"; break;
                }
            }

            tempInnerHtml += "</div>";

            /* ************************ */

            if (Session["uredjivanaRezervacijaId"] != null)
            {
                tempInnerHtml += "<input class=\"spremiRezervGumb\" type=\"button\" value=\"Spremi rezervaciju\" onclick=\"spremiRezervaciju(" + tempFilm.idFilma + ", " + odabraniDan + ", " + odabraniSat + ", " + Session["uredjivanaRezervacijaId"] + ");\">";

                Session.Remove("uredjivanaRezervacijaId");
            }
            else if (spremanjeRezervacije)
            {
                tempInnerHtml += "<input class=\"spremiRezervGumb\" type=\"button\" value=\"Rezervacija spremljena\" disabled>";
            }
            else
            {
                tempInnerHtml += "<input class=\"spremiRezervGumb\" type=\"button\" value=\"Spremi rezervaciju\" onclick=\"spremiRezervaciju(" + tempFilm.idFilma + ", " + odabraniDan + ", " + odabraniSat + ", " + "null" + ");\">";
            }

            /* ************************ */

            tempSjedalaIzbornik.Text = tempInnerHtml;

            /* ************************ */

            CheckBoxSjedalaPanel.Controls.Add(tempSjedalaIzbornik);
        }
    }
}