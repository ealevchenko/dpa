<%@ Page Title="" Language="C#" MasterPageFile="~/StartSite.master" AutoEventWireup="true" CodeFile="AccessSite.aspx.cs" Inherits="AccessSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/accesssite.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="NotAccess">
        <asp:Panel ID="LoginOn" runat="server">
            <asp:LoginView ID="HeadLoginView" runat="server" ViewStateMode="Enabled">
                <LoggedInTemplate>
                    <span class="bold">
                        <asp:LoginName ID="HeadLoginName" runat="server" />
                        <asp:Literal ID="ltText" runat="server" Text="<%$ Resources:Resource, as_Title %>"></asp:Literal>
                    </span>
                </LoggedInTemplate>
            </asp:LoginView>
        </asp:Panel>

        <asp:Panel ID="loginError" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/image/access/LoginError.png" /></td>
                    <td>

                        <h3>
                            <asp:Literal ID="ltTitle" runat="server" Text="<%$ Resources:Resource, as_TitleLoginError %>"></asp:Literal><span>


                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:Resource, as_TextLoginErrorSite %>"></asp:Literal><asp:Label ID="lbResult" runat="server" Text=""></asp:Label>


                            </span>
                        </h3>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <h3><span>
                            <asp:Literal ID="lt2" runat="server" Text="<%$ Resources:Resource, as_TextRequestAccess %>"></asp:Literal></span></h3>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div class="main">
<%--            <div class="view view-first">
                <img src="/image/access/Access.jpg" />
                <div class="mask">
                    <h2>
                        <asp:Literal ID="ltText" runat="server" Text="<%$ Resources:Resource, as_Link1 %>"></asp:Literal></h2>
                    <p>
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Resource, as_LinkDescription1 %>"></asp:Literal>
                    </p>
                    <a href="#" class="info">
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Resource, as_LinkText1 %>"></asp:Literal></a>
                </div>
            </div>--%>
            <div class="view view-first">
                <img src="/image/access/Inquiry.jpg" />
                <div class="mask">
                    <h2>
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:Resource, as_Link2 %>"></asp:Literal></h2>
                    <p>
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:Resource, as_LinkDescription2 %>"></asp:Literal>
                    </p>
                    <a href="AccessWeb.aspx" class="info">
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:Resource, as_LinkText2 %>"></asp:Literal></a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

