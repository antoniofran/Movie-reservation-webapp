<%@ Page Title="Rezervacija" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Rezervacija.aspx.cs" Inherits="Rezervacija" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="Scripts/Rezervacija.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Rezervacija sjedala za film: "<asp:Label ID="NazivFilmaLabel" runat="server"></asp:Label>"</h2>
    <h3>Dan: <asp:Label ID="OdabraniDanLabel" runat="server"></asp:Label> | Sat: <asp:Label ID="OdabraniSatLabel" runat="server"></asp:Label> | Dvorana: <asp:Label ID="DvoranaLabel" runat="server"></asp:Label></h3>
    <asp:Panel ID="CheckBoxSjedalaPanel" runat="server" CssClass="rezervacijaPanel"></asp:Panel>
</asp:Content>

