<%@ Page Title="Naslovna" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <h2>Dobro došli na stranice ASP.NET kina.</h2>

    <h3>Mi smo najbolje kino u Europi i na ovoj stranici možete vidjeti kratki prikaz filmova koje trenutno nudimo. Kako bi ste otovrili puni prikaz filma, kliknite na sliku ili naslov filma.</h3>

    <asp:ScriptManager ID="ScriptManager1" runat="server">

    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>Radim ...</ProgressTemplate>
            </asp:UpdateProgress>
            <section class="movieBlock">
                <div class="movieImage">
                    <asp:HyperLink ID="ImageHyperLink" runat="server">
                        <asp:Image ID="ImageCurrent" runat="server" Width="100%" />
                    </asp:HyperLink>
                </div>
                <div class="movieInfo">
                    <p class="movieName">
                        <asp:HyperLink ID="NameHyperLink" runat="server">
                            <asp:Label ID="MovieNameLabel" runat="server"></asp:Label>
                        </asp:HyperLink>
                    </p>
                    <p class="movieDesc">
                        <asp:Label ID="MovieDescLabel" runat="server"></asp:Label>
                    </p>
                </div>
                <div style="clear: both;"></div>
            </section>
            <asp:Timer ID="TimerUvecaj" runat="server" Interval="3500" OnTick="TimerUvecaj_Tick"></asp:Timer>
        </ContentTemplate>    
    </asp:UpdatePanel>
    
</asp:Content>

