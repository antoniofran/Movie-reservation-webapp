<%@ Page Title="Upravljac" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Upravljac.aspx.cs" Inherits="Upravljac" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="RegularPanel" runat="server" Visible="False" CssClass="upravljacPanel">
        <h3>Upravljanje vlastitim rezervacijama:</h3>
        <asp:ListBox ID="PopisKorisnikovihRezervacija" runat="server" CssClass="listBoxSelect" Rows="4"></asp:ListBox>
        <div class="listBoxButton">
            <asp:Button ID="OtkaziRezervacijuGumb" runat="server" Text="Otkaži rezervaciju" OnClick="OtkaziRezervacijuGumb_Click" />
        </div>
    </asp:Panel>
    <asp:Panel ID="AdminPanel" runat="server" Visible="False" CssClass="upravljacPanel">
        <h3>Upravljanje svim rezervacijama:</h3>
        <asp:ListBox ID="PopisSvihRezervacija" runat="server" CssClass="listBoxSelect" Rows="4"></asp:ListBox>
        <div class="listBoxButton">
            <asp:Button ID="PotvrdiRezervacijuGumb" runat="server" Text="Potvrdi rezervaciju" OnClick="PotvrdiRezervacijuGumb_Click" />
            <asp:Button ID="UrediRezervacijuGumb" runat="server" Text="Uredi rezervaciju" OnClick="UrediRezervacijuGumb_Click" />
        </div>
    </asp:Panel>
    <asp:Panel ID="BureaucratPanel" runat="server" Visible="False" CssClass="upravljacPanel">
        <h3>Upravljanje ovlastima korisnika:</h3>
        <asp:ListBox ID="PopisSvihKorisnika" runat="server" CssClass="listBoxSelect" Rows="4"></asp:ListBox>
        <div class="listBoxButton">
            <asp:Button ID="DemovirajKorisnikaGumb" runat="server" Text="<< Demoviraj korisnika" OnClick="DemovirajKorisnikaGumb_Click" />
            <asp:Button ID="PromovirajKorisnikaGumb" runat="server" Text="Promoviraj korisnika >>" OnClick="PromovirajKorisnikaGumb_Click" />
        </div>
    </asp:Panel>
</asp:Content>

