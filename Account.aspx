<%@ Page Title="Račun" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Account.aspx.cs" Inherits="Account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table cellpadding="2" cellspacing="7">
        <tr>
            <td style="border:none; border-right: 1px solid black;">
                <asp:Login ID="Login1" runat="server" DisplayRememberMe="False" OnAuthenticate="Login1_Authenticate" DestinationPageUrl="~/Upravljac.aspx" CssClass="loginForma"></asp:Login>
            </td>
            <td style="border:none; border-left: 1px solid black;">
                <table class="registerForma">
                    <tr>
                        <td colspan="2">
                            Register
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="UserNameLabel" runat="server" Text="User Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="UserNameTextBox" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="UserNameTextBox"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="UserPassLabel" runat="server" Text="Password:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="UserPassTextBox" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="UserPassTextBox"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="UserEmailLabel" runat="server" Text="Email:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="UserEmailTextBox" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="UserEmailTextBox"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*" ControlToValidate="UserEmailTextBox" ValidationExpression="^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan ="2">
                            <asp:Button ID="RegisterButtonClick" runat="server" Text="Register" OnClick="RegisterButtonClick_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

