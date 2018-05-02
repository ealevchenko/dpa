<%@ Page Title="" Language="C#" MasterPageFile="~/WebSite/Setup/Setup.master" AutoEventWireup="true" CodeFile="MenagerProject.aspx.cs" Inherits="WebSite_Setup_MenagerProject" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>
<%@ Register TagPrefix="wuc" TagName="controlMenagerProject" Src="~/WebUserControl/Strategic/controlMenagerProject.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/menagerproject.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- -->
    <asp:ObjectDataSource ID="odsList" runat="server" SelectMethod="SelectMenagerProject" TypeName="Strategic.classMenagerProject"></asp:ObjectDataSource>
<!-- -->
    <div class="Page">
        <div class="divPanelControl">
            <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsUpdDel" StyleButton="img" VisibleDelete="false" VisibleUpdate="false" OnInsertClick="pnSelect_InsertClick"/>
        </div>
        <div class="divList">
            <asp:ListView ID="lvList" runat="server" DataSourceID="odsList" DataKeyNames="IDMenagerProject" OnSelectedIndexChanged="lvList_SelectedIndexChanged">
                <LayoutTemplate>
                    <table class="tabList">
                        <tr>
                            <th class="username">
                                <asp:Literal ID="ltUserEnterprise" runat="server" Text="<%$ Resources:ResourceStrategic, nfUserEnterprise %>"></asp:Literal></th>
                            <th class="fio">
                                <asp:Literal ID="lbFIO" runat="server" Text="<%$ Resources:ResourceStrategic, nfFIO %>"></asp:Literal></th>
                            <th class="email">
                                <asp:Literal ID="lbEmail" runat="server" Text="<%$ Resources:ResourceStrategic, nfEmail %>"></asp:Literal></th>
                            <th class="wphone">
                                <asp:Literal ID="lbWPhone" runat="server" Text="<%$ Resources:ResourceStrategic, nfWPhone %>"></asp:Literal></th>
                            <th class="mphone">
                                <asp:Literal ID="lbMPhone" runat="server" Text="<%$ Resources:ResourceStrategic, nfMPhone %>"></asp:Literal></th>
                            <th class="super">
                                <asp:Literal ID="lbSuperMenager" runat="server" Text="<%$ Resources:ResourceStrategic, nfSuperMenager %>"></asp:Literal></th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="item">
                        <td class="username">
                           <asp:LinkButton ID="lbTypeProject" runat="server" Text='<%# base.cmenager.GetUserEnterprise(Container.DataItem) %>' CommandArgument="IDMenagerProject"  CommandName="Select" CausesValidation="false"></asp:LinkButton>
                        </td>
                        <td class="fio"><b><%# base.cmenager.GetFIO(Container.DataItem) %></b>
                        </td>
                        <td class="email"><a href='mailto:<%# base.cmenager.GetEmail(Container.DataItem) %>'><asp:Literal ID="Literal4" runat="server" Text='<%# base.cmenager.GetEmail(Container.DataItem) %>'></asp:Literal></a>
                        </td>
                        <td class="wphone"><%# Eval("WPhone","{0:#####-##-##}")%> 
                        </td>
                        <td class="mphone"><%# Eval("MPhone","{0:8(0##)##-##-###}")%> 
                        </td>
                        <td class="super"><asp:Image ID="imSuperMenager" runat="server" ImageUrl='<%# base.dir.GetSRCCheckBox(Container.DataItem,"SuperMenager") %>' Height="32px" Width="32px" /></td>
                    </tr>
                </ItemTemplate>
                <SelectedItemTemplate>
                    <tr class="select">
                        <td class="username">
                           <asp:LinkButton ID="lbTypeProject" runat="server" Text='<%# base.cmenager.GetUserEnterprise(Container.DataItem) %>' CommandArgument="IDMenagerProject"  CommandName="Select" CausesValidation="false"></asp:LinkButton>
                        </td>
                        <td class="fio"><b><%# base.cmenager.GetFIO(Container.DataItem) %></b>
                        </td>
                        <td class="email"><a href='mailto:<%# base.cmenager.GetEmail(Container.DataItem) %>'><asp:Literal ID="Literal4" runat="server" Text='<%# base.cmenager.GetEmail(Container.DataItem) %>'></asp:Literal></a>
                        </td>
                        <td class="wphone"><%# Eval("WPhone","{0:#####-##-##}")%> 
                        </td>
                        <td class="mphone"><%# Eval("MPhone","{0:8(0##)##-##-###}")%> 
                        </td>
                        <td class="super"><asp:Image ID="imSuperMenager" runat="server" ImageUrl='<%# base.dir.GetSRCCheckBox(Container.DataItem,"SuperMenager") %>' Height="32px" Width="32px" /></td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
        </div>
        <div class="divDetali">
            <wuc:controlMenagerProject ID="controlMenagerProject" runat="server" OnDataInserted="controlTypeProject_SiteInserted" OutInfoText="true" 
                OnDataUpdated="controlTypeProject_SiteUpdated" OnDataDeleted="controlTypeProject_SiteDeleted"/>
        </div>
        <div class="clear">

        </div>
    </div>
</asp:Content>

