using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

using Klase;

public partial class _Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (ImageCurrent.ImageUrl == string.Empty)
        {
            ispisiRadnomFilm();
        }

    }

    protected void ispisiRadnomFilm()
    {
        List<Film> tempListaFilmova = Metode.dohvatiListuFilmova();

        Film tempFilm = tempListaFilmova[new Random().Next(tempListaFilmova.Count)];

        ImageCurrent.ImageUrl = "~/Images/MoviePosters/" + tempFilm.nazivDatoteke + ".jpg";

        MovieNameLabel.Text = tempFilm.nazivFilma;

        if (tempFilm.opisFilma.Length < 45)
        {
            MovieDescLabel.Text = tempFilm.opisFilma;
        }
        else
        {
            MovieDescLabel.Text = tempFilm.opisFilma.Substring(0, 45) + "...";
        }

        ImageHyperLink.NavigateUrl = "~/Filmovi.aspx?movienum=" + tempFilm.idFilma;

        NameHyperLink.NavigateUrl = "~/Filmovi.aspx?movienum=" + tempFilm.idFilma;
    }

    protected void TimerUvecaj_Tick(object sender, EventArgs e)
    {
        ispisiRadnomFilm();
    }
}