using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Klase;

public partial class Filmovi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["movienum"] != null)
        {
            Film tempFilm = Metode.dohvatiFilm( Convert.ToInt32(Request.QueryString["movienum"]) );

            if (tempFilm != null)
            {
                Label tempMovieBlock = new Label();

                tempMovieBlock.ID = "MovieBlockLabel" + tempFilm.idFilma;

                tempMovieBlock.Text = Metode.napraviIspisFilma(tempFilm);

                MainPanel.Controls.Add(tempMovieBlock);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Nepostojeći film.');", true);
            }
        }
        else
        {
            List<Film> tempListaFilmova = Metode.dohvatiListuFilmova();

            foreach ( Film tempFilm in tempListaFilmova)
            {
                Label tempMovieBlock = new Label();

                tempMovieBlock.ID = "MovieBlockLabel" + tempFilm.idFilma;

                tempMovieBlock.Text = Metode.napraviIspisFilma(tempFilm);

                MainPanel.Controls.Add(tempMovieBlock);
            }
            
        }
    }
}