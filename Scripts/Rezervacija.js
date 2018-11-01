function otvoriDvoranu(idFilma) {

    var danSelect = document.getElementById("listaDanaTjedna" + idFilma);

    var odabraniDan = danSelect.options[danSelect.selectedIndex].value;

    var satSelect = document.getElementById("listaVremena" + idFilma);

    var odabraniSat = satSelect.options[satSelect.selectedIndex].value;

    document.location.href = "Rezervacija.aspx?film=" + idFilma + "&dan=" + odabraniDan + "&sat=" + odabraniSat;
}

function spremiRezervaciju(idFilma, odabraniDan, odabraniSat, rezervacijaId) {
    var odabranaSjedala = "";

    var pregledSjedalaChildren = document.getElementById("pregledSjedala").children;

    for (var i = 0; i < pregledSjedalaChildren.length; i++) {
        if (pregledSjedalaChildren[i].hasAttribute("name")) {
            if (pregledSjedalaChildren[i].checked && !pregledSjedalaChildren[i].disabled) {
                odabranaSjedala += pregledSjedalaChildren[i].name.substring(8) + ",";
            }
        }
    }

    if (odabranaSjedala.length > 0) {
        odabranaSjedala = odabranaSjedala.substring(0, odabranaSjedala.length - 1);
    }

    if (rezervacijaId == null) {
        document.location.href = "Rezervacija.aspx?film=" + idFilma + "&dan=" + odabraniDan + "&sat=" + odabraniSat + "&sjed=" + odabranaSjedala;
    } else {
        document.location.href = "Rezervacija.aspx?film=" + idFilma + "&dan=" + odabraniDan + "&sat=" + odabraniSat + "&sjed=" + odabranaSjedala + "&rez=" + rezervacijaId;
    }
    
}