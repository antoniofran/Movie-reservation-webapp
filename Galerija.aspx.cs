using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Klase;

public partial class Galerija : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["movienum"] != null)
        {
            Film tempFilm = Metode.dohvatiFilm(Convert.ToInt32(Request.QueryString["movienum"]));

            if (tempFilm != null)
            {
                Label tempMovieGalleryLabel = new Label();

                tempMovieGalleryLabel.ID = "MovieGalleryLabel" + tempFilm.idFilma;

                tempMovieGalleryLabel.Text = Metode.napraviIspisGalerijeFilma(tempFilm, Server.MapPath("~/Images/MovieGalleries/" + tempFilm.nazivDatoteke + "/"));

                MainPanel.Controls.Add(tempMovieGalleryLabel);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Nepostojeći film.');",true);
            }
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Film nije izabran.');",true);
        }
    }
}