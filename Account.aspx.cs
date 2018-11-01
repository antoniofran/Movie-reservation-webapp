using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;

public partial class Account : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated == true)
        {
            Response.Redirect("~/Upravljac.aspx");
        }
    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        OleDbConnection konekcija = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);

        try
        {
            konekcija.Open();

            OleDbCommand upit = new OleDbCommand("SELECT * FROM korisnici WHERE username=@ime AND password=@loznika", konekcija);

            upit.Parameters.AddWithValue("@ime", Login1.UserName);
            upit.Parameters.AddWithValue("@lozinka", Login1.Password);

            OleDbDataReader podaci = upit.ExecuteReader();

            if (podaci.HasRows)
            {
                e.Authenticated = true;
            }
            else
            {
                e.Authenticated = false;
            }

        }

        catch (Exception greska)
        {
            Response.Write("<div id=\"greskaPoruka\">");

            Response.Write("<p>" + greska.Message + "</p>");

            Response.Write("<p>" + greska.StackTrace.Replace("\n", "<br>") + "</p>");

            Response.Write("</div>");
        }

        finally
        {
            konekcija.Close();
        }

    }


    protected void RegisterButtonClick_Click(object sender, EventArgs e)
    {
        OleDbConnection konekcija = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);

        try
        {
            konekcija.Open();

            OleDbCommand upit = new OleDbCommand("INSERT INTO korisnici([username],[password],[useremail],[userstatus]) VALUES (@username,@password,@useremail,@userstatus)", konekcija);

            upit.Parameters.AddWithValue("@username", UserNameTextBox.Text);
            upit.Parameters.AddWithValue("@password", UserPassTextBox.Text);
            upit.Parameters.AddWithValue("@useremail", UserEmailTextBox.Text);
            upit.Parameters.AddWithValue("@userstatus", "regular");

            int broj = upit.ExecuteNonQuery();

            Response.Redirect("~/Default.aspx?view=5");
        }

        catch (Exception greska)
        {
            Response.Write("<div id=\"greskaPoruka\">");

            Response.Write("<p>" + greska.Message + "</p>");

            Response.Write("<p>" + greska.StackTrace.Replace("\n", "<br>") + "</p>");

            Response.Write("</div>");
        }

        finally
        {
            konekcija.Close();
        }
    }
}