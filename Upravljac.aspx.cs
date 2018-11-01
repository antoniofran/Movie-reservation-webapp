using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;

using Klase;

public partial class Upravljac : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            string currUserStatus = Metode.dohvatiUserStatus();

            if (currUserStatus == "regular" || currUserStatus == "admin" || currUserStatus == "bureaucrat")
            {
                RegularPanel.Visible = true;

                /* ******************* */

                if (!IsPostBack)
                {
                    List<string> listaRezervacijaKorisnikaString = Metode.dohvatiListuRezervacijaString(HttpContext.Current.User.Identity.Name);

                    listaRezervacijaKorisnikaString.Sort();

                    for (int i = 0; i < listaRezervacijaKorisnikaString.Count; i++)
                    {
                        PopisKorisnikovihRezervacija.Items.Add(listaRezervacijaKorisnikaString[i]);
                    }
                }

                /* ******************* */

                int tempTrenutniCount = PopisKorisnikovihRezervacija.Items.Count;

                if (tempTrenutniCount <= 4)
                {
                    PopisKorisnikovihRezervacija.Rows = 4;
                }
                else if (tempTrenutniCount > 4 && tempTrenutniCount < 10)
                {
                    PopisKorisnikovihRezervacija.Rows = tempTrenutniCount;
                }
                else
                {
                    PopisKorisnikovihRezervacija.Rows = 10;
                }
            }

            if (currUserStatus == "admin" || currUserStatus == "bureaucrat")
            {
                AdminPanel.Visible = true;

                /* ******************* */

                if (!IsPostBack)
                {
                    List<string> listaSvihRezervacijaString = Metode.dohvatiListuRezervacijaString();

                    listaSvihRezervacijaString.Sort();

                    for (int i = 0; i < listaSvihRezervacijaString.Count; i++)
                    {
                        PopisSvihRezervacija.Items.Add(listaSvihRezervacijaString[i]);
                    }
                }

                /* ******************* */

                int tempTrenutniCount = PopisSvihRezervacija.Items.Count;

                if (tempTrenutniCount <= 4)
                {
                    PopisSvihRezervacija.Rows = 4;
                }
                else if (tempTrenutniCount > 4 && tempTrenutniCount < 10)
                {
                    PopisSvihRezervacija.Rows = tempTrenutniCount;
                }
                else
                {
                    PopisSvihRezervacija.Rows = 10;
                }
            }

            if (currUserStatus == "bureaucrat")
            {
                BureaucratPanel.Visible = true;

                /* ******************* */

                if (!IsPostBack)
                {
                    List<string> listaSvihKorisnikaString = Metode.dohvatiListuKorisnikaString();

                    listaSvihKorisnikaString.Sort();

                    for (int i = 0; i < listaSvihKorisnikaString.Count; i++)
                    {
                        PopisSvihKorisnika.Items.Add(listaSvihKorisnikaString[i]);
                    }
                }

                /* ******************* */

                int tempTrenutniCount = PopisSvihKorisnika.Items.Count;

                if (tempTrenutniCount <= 4)
                {
                    PopisSvihKorisnika.Rows = 4;
                }
                else if (tempTrenutniCount > 4 && tempTrenutniCount < 10)
                {
                    PopisSvihKorisnika.Rows = tempTrenutniCount;
                }
                else
                {
                    PopisSvihKorisnika.Rows = 10;
                }
            }
        }
        else
        {
            Response.Redirect("~/Account.aspx");
        }
    }

    protected void OtkaziRezervacijuGumb_Click(object sender, EventArgs e)
    {
        if (PopisKorisnikovihRezervacija.SelectedItem != null)
        {
            int tempSelectedItemIndex = PopisKorisnikovihRezervacija.SelectedIndex;
            string tempSelectedItemValue = PopisKorisnikovihRezervacija.SelectedItem.ToString();

            /* *********************** */

            string[] tempRezervacijaStringSplit = tempSelectedItemValue.Split('|');

            int tempIdRezervacije = Convert.ToInt32( tempRezervacijaStringSplit[0].Trim(' ').Substring(1) );
            string tempIndexiSjedala = tempRezervacijaStringSplit[4].Trim(' ').Substring(6);

            /* *********************** */

            string tempFilmDanSat = Metode.dohvatiFilmDanSat(tempIdRezervacije);

            /* *********************** */

            Metode.oslobodiSjedala(tempFilmDanSat, tempIndexiSjedala);

            /* *********************** */

            OleDbConnection konekcija = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);

            konekcija.Open();

            /* ******************** */

            OleDbCommand upit = new OleDbCommand("DELETE FROM rezervacije WHERE [ID]=@ID;", konekcija);

            upit.Parameters.AddWithValue("@ID", tempIdRezervacije);

            int redovaObrisano = upit.ExecuteNonQuery();

            /* ******************** */

            konekcija.Close();

            /* ******************** */

            PopisKorisnikovihRezervacija.Items.RemoveAt(tempSelectedItemIndex);

            /* ******************** */

            for (int i = 0; i < PopisSvihRezervacija.Items.Count; i++)
            {
                if (PopisSvihRezervacija.Items[i].ToString().Equals(tempSelectedItemValue))
                {
                    PopisSvihRezervacija.Items.RemoveAt(i);
                }
            }
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Niste izabrali rezervaciju.');", true);
        }

    }

    protected void PotvrdiRezervacijuGumb_Click(object sender, EventArgs e)
    {
        if (PopisSvihRezervacija.SelectedItem != null)
        {
            int tempSelectedItemIndex = PopisSvihRezervacija.SelectedIndex;
            string tempSelectedItemValue = PopisSvihRezervacija.SelectedItem.ToString();

            /* *********************** */

            string[] tempRezervacijaStringSplit = tempSelectedItemValue.Split('|');

            int tempIdRezervacije = Convert.ToInt32(tempRezervacijaStringSplit[0].Trim(' ').Substring(1));
            string tempIsPotvrdjeno = tempRezervacijaStringSplit[5].Trim(' ').Trim('"');

            /* *********************** */

            if (tempIsPotvrdjeno == "NE")
            {
                Metode.potvrdiRezervaciju(tempIdRezervacije);

                /* *********************** */

                string tempAzuriranaRezervacijaString = "";

                for (int i = 0; i < tempRezervacijaStringSplit.Length; i++)
                {
                    if (i == 5)
                    {
                        tempAzuriranaRezervacijaString += tempRezervacijaStringSplit[i].Replace("NE", "DA") + "|";
                    }
                    else
                    {
                        tempAzuriranaRezervacijaString += tempRezervacijaStringSplit[i] + "|";
                    }

                }

                tempAzuriranaRezervacijaString = tempAzuriranaRezervacijaString.Substring(0, tempAzuriranaRezervacijaString.Length - 1);

                /* *********************** */

                for (int i = 0; i < PopisKorisnikovihRezervacija.Items.Count; i++)
                {
                    if (PopisKorisnikovihRezervacija.Items[i].ToString().Equals(tempSelectedItemValue))
                    {
                        PopisKorisnikovihRezervacija.Items.RemoveAt(i);

                        PopisKorisnikovihRezervacija.Items.Insert(i, tempAzuriranaRezervacijaString);
                    }
                }

                /* *********************** */

                PopisSvihRezervacija.Items.RemoveAt(tempSelectedItemIndex);

                PopisSvihRezervacija.Items.Insert(tempSelectedItemIndex, tempAzuriranaRezervacijaString);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Rezervacija je već potvrđena.');", true);
            }

        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Niste izabrali rezervaciju.');", true);
        }
        
    }

    protected void UrediRezervacijuGumb_Click(object sender, EventArgs e)
    {
        if (PopisSvihRezervacija.SelectedItem != null)
        {
            int tempSelectedItemIndex = PopisSvihRezervacija.SelectedIndex;
            string tempSelectedItemValue = PopisSvihRezervacija.SelectedItem.ToString();

            /* *********************** */

            string[] tempRezervacijaStringSplit = tempSelectedItemValue.Split('|');

            int tempIdRezervacije = Convert.ToInt32( tempRezervacijaStringSplit[0].Trim(' ').Substring(1) );
            string tempIndexiSjedala = tempRezervacijaStringSplit[4].Trim(' ').Substring(6);

            /* *********************** */

            string tempFilmDanSat = Metode.dohvatiFilmDanSat(tempIdRezervacije);

            /* *********************** */

            Metode.oslobodiSjedala(tempFilmDanSat, tempIndexiSjedala);

            /* *********************** */

            OleDbConnection konekcija = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);

            konekcija.Open();

            /* ******************** */

            OleDbCommand upit = new OleDbCommand("UPDATE rezervacije SET [IndexiRezervSjed]=@IndexiRezervSjed WHERE [ID]=@ID;", konekcija);

            upit.Parameters.AddWithValue("@IndexiRezervSjed", "");

            upit.Parameters.AddWithValue("@ID", tempIdRezervacije);

            int redovaAzurirano = upit.ExecuteNonQuery();

            /* ******************** */

            konekcija.Close();

            /* ******************** */

            Session["isUredjivanjeRezervacije"] = "true";

            Session["uredjivanaRezervacijaId"] = tempIdRezervacije;
            Session["uredjivanaRezervacijaSjed"] = tempIndexiSjedala;

            /* ******************** */

            string[] tempFilmDanSatSplit = tempFilmDanSat.Split('+');

            Response.Redirect("~/Rezervacija.aspx?film=" + tempFilmDanSatSplit[0] + "&dan=" + tempFilmDanSatSplit[1] + "&sat=" + tempFilmDanSatSplit[2]);
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Niste izabrali rezervaciju.');", true);
        }
    }

    protected void PromovirajKorisnikaGumb_Click(object sender, EventArgs e)
    {
        if (PopisSvihKorisnika.SelectedItem != null)
        {
            string tempLoggedInUserName = HttpContext.Current.User.Identity.Name;

            int tempSelectedItemIndex = PopisSvihKorisnika.SelectedIndex;
            string tempSelectedItemValue = PopisSvihKorisnika.SelectedItem.ToString();

            string[] tempSelectedItemSplit = tempSelectedItemValue.Split('|');

            string tempSelectedUserName = tempSelectedItemSplit[0].Trim(' ');
            string tempSelectedUserEamil = tempSelectedItemSplit[1].Trim(' ');
            string tempSelectedUserStatus = tempSelectedItemSplit[2].Trim(' ');

            /* *********************** */

            if (tempLoggedInUserName == tempSelectedUserName)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Ne može se promovirati sam sebe.');", true);
                return;
            }

            if (tempSelectedUserStatus == "bureaucrat")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Ne može se promovirati više od levela \"bureaucrat\".');", true);
                return;
            }

            /* *********************** */

            OleDbConnection konekcija = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);

            konekcija.Open();

            /* ********************* */

            string tempNoviUserStatus = "";

            switch (tempSelectedUserStatus)
            {
                case "regular": tempNoviUserStatus = "admin"; break;
                case "admin": tempNoviUserStatus = "bureaucrat"; break;
            }

            /* ********************* */

            OleDbCommand upit = new OleDbCommand("UPDATE korisnici SET [userstatus]=@userstatus WHERE [username]=@username;", konekcija);

            upit.Parameters.AddWithValue("@userStatus", tempNoviUserStatus);

            upit.Parameters.AddWithValue("@username", tempSelectedUserName);

            int redovaAzurirano = upit.ExecuteNonQuery();

            konekcija.Close();

            /* ********************** */

            PopisSvihKorisnika.Items.RemoveAt(tempSelectedItemIndex);

            string tempAzuriraniSelectedItem = tempSelectedUserName + " | " + tempSelectedUserEamil + " | " + tempNoviUserStatus;

            PopisSvihKorisnika.Items.Insert(tempSelectedItemIndex, tempAzuriraniSelectedItem);
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Niste izabrali korisnika.');", true);
        }
    }

    protected void DemovirajKorisnikaGumb_Click(object sender, EventArgs e)
    {
        if (PopisSvihKorisnika.SelectedItem != null)
        {
            string tempLoggedInUserName = HttpContext.Current.User.Identity.Name;

            int tempSelectedItemIndex = PopisSvihKorisnika.SelectedIndex;
            string tempSelectedItemValue = PopisSvihKorisnika.SelectedItem.ToString();

            string[] tempSelectedItemSplit = tempSelectedItemValue.Split('|');

            string tempSelectedUserName = tempSelectedItemSplit[0].Trim(' ');
            string tempSelectedUserEamil = tempSelectedItemSplit[1].Trim(' ');
            string tempSelectedUserStatus = tempSelectedItemSplit[2].Trim(' ');

            /* *********************** */

            if (tempSelectedUserStatus == "bureaucrat" && tempLoggedInUserName != tempSelectedUserName)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('User levela \"bureaucrat\" može biti demoviran samo od strane sebe.');", true);
                return;
            }

            if (tempSelectedUserStatus == "regular")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Ne može se demovirati niže od levela \"regular\".');", true);
                return;
            }

            /* *********************** */

            //kod za demoviranje

            OleDbConnection konekcija = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);

            konekcija.Open();

            /* ********************* */

            string tempNoviUserStatus = "";

            switch (tempSelectedUserStatus)
            {
                case "bureaucrat": tempNoviUserStatus = "admin"; break;
                case "admin": tempNoviUserStatus = "regular"; break;
            }

            /* ********************* */

            OleDbCommand upit = new OleDbCommand("UPDATE korisnici SET [userstatus]=@userstatus WHERE [username]=@username;", konekcija);

            upit.Parameters.AddWithValue("@userStatus", tempNoviUserStatus);

            upit.Parameters.AddWithValue("@username", tempSelectedUserName);

            int redovaAzurirano = upit.ExecuteNonQuery();

            konekcija.Close();

            /* *********************** */

            PopisSvihKorisnika.Items.RemoveAt(tempSelectedItemIndex);

            string tempAzuriraniSelectedItem = tempSelectedUserName + " | " + tempSelectedUserEamil + " | " + tempNoviUserStatus;

            PopisSvihKorisnika.Items.Insert(tempSelectedItemIndex, tempAzuriraniSelectedItem);
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Niste izabrali korisnika.');", true);
        }
    }
}