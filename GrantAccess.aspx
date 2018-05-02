<%@ Page Title="" Language="C#" MasterPageFile="~/StartSite.master" AutoEventWireup="true" CodeFile="GrantAccess.aspx.cs" Inherits="GrantAccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/grantaccess.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="GrantAccess">
        <asp:Panel ID="pnGrantAccess" runat="server"  Visible="false">
        <h3><%# caccessusers.GetUserDetali((int)this.IDUser).Name%>&nbsp;<%# caccessusers.GetUserDetali((int)this.IDUser).Patronymic%>&nbsp;
            <asp:Literal ID="ltTitle" runat="server" Text="<%$ Resources:ResourceBase, gaTitle %>"></asp:Literal><br />&laquo;<%# this.NameWebSite %>&raquo;

        </h3>
        <table class="tabUser">
            <tr>
                <th><asp:Literal ID="ltfio" runat="server" Text="<%$ Resources:ResourceBase, gaFIO %>"></asp:Literal>:&nbsp;</th>
                <td><%# this.FIO %></td>
            </tr>
            <tr>
                <th><asp:Literal ID="ltSection" runat="server" Text="<%$ Resources:ResourceBase, gaSection %>"></asp:Literal>:&nbsp;</th>
                <td><%# this.Section %></td>
            </tr>
            <tr>
                <th><asp:Literal ID="ltPost" runat="server" Text="<%$ Resources:ResourceBase, gaPost %>"></asp:Literal>:&nbsp;</th>
                <td><%# this.Post %></td>
            </tr>
            <tr>
                <th><asp:Literal ID="ltEmail" runat="server" Text="<%$ Resources:ResourceBase, gaEmail %>"></asp:Literal>:&nbsp;</th>
                <td><a href="mailto:<%# this.Email %>"><%# this.Email %></td>
            </tr>
            <tr>
                <th><asp:Literal ID="ltComment" runat="server" Text="<%$ Resources:ResourceBase, gaComent %>"></asp:Literal><br />
                    <span><asp:Literal ID="ltComment1" runat="server" Text="<%$ Resources:ResourceBase, gaComent1 %>"></asp:Literal></span>:&nbsp;</th>
                <td>
                    <asp:TextBox ID="tbComent" runat="server" TextMode="MultiLine" CssClass="MultiText" ></asp:TextBox></td>
            </tr>
            <tr>
                <td class="upr" colspan="2">
                    <asp:Button ID="btYes" runat="server" Text="<%$ Resources:ResourceBase, gabtYes %>" CssClass="ok" OnClick="btYes_Click"/>&nbsp;&nbsp;
                <asp:Button ID="btNo" runat="server" Text="<%$ Resources:ResourceBase, gabtNo %>" CssClass="no" OnClick="btNo_Click" />
                </td>
            </tr>
        </table>
        </asp:Panel>
        <asp:Panel ID="pnEnd" runat="server" Visible="false">
            <h3><asp:Literal ID="ltYes" runat="server" Text="<%$ Resources:ResourceBase, gaTitleEnd %>"></asp:Literal><br /><%# this.DateApproval %></h3>
        </asp:Panel>
    </div>
</asp:Content>

