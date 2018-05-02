<%@ Page Title="" Language="C#" MasterPageFile="~/WebSite/Setup/Setup.master" AutoEventWireup="true" CodeFile="ListSite.aspx.cs" Inherits="WebSite_Setup_ListSite" %>

<%@ Register Assembly="leaControls" Namespace="leaControls" TagPrefix="cc1" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>
<%@ Register TagPrefix="wuc" TagName="ControlSite" Src="~/WebUserControl/Setup/controlSite.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/listsite.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- -->
    <asp:ObjectDataSource ID="odsListSite" runat="server" SelectMethod="SelectListSiteCulture" TypeName="WebBase.classSite"></asp:ObjectDataSource>
<!-- -->
    <div class="Page">
        <div class="divPanelControl">
<%--            <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsUpdDel" StyleButton="img" VisibleDelete="false" VisibleUpdate="false" OnInsertClick="pnSelect_InsertClick"/>--%>
            <cc1:TableControl ID="tcselect" StylePanel="InsUpdDel" StyleButton="img" VisibleDelete="false" VisibleUpdate="false" OnInsertClick="pnSelect_InsertClick" VisibleChange="true"  runat="server" />
        </div>
        <div class="divList">
            <asp:ListView ID="lvSite" runat="server" DataSourceID="odsListSite" DataKeyNames="IDSite" OnSelectedIndexChanged="lvSite_SelectedIndexChanged">
                <LayoutTemplate>
                    <table class="tabSite">
                        <tr>
                            <th class="URL">
                                <asp:LinkButton ID="lbURL" runat="server" CommandName="sort" CommandArgument="URL" Text="<%$ Resources:ResourceBase, nfURL %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                            <th class="Description">
                                <asp:LinkButton ID="lbDescription" runat="server" CommandName="sort" CommandArgument="Description" Text="<%$ Resources:ResourceBase, nfDescription %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="item">
                        <td class="URL"><b>
                            <asp:LinkButton ID="lbURL" runat="server" Text='<%# Eval("URL") %>' CommandName="Select" CausesValidation="false" ></asp:LinkButton></b></td>
                        <td class="Description"><%# Eval("Description") %></td>
                    </tr>
                </ItemTemplate>
                <SelectedItemTemplate>
                    <tr class="select">
                        <td class="URL"><asp:LinkButton ID="lbURL" runat="server" Text='<%# Eval("URL") %>' CommandName="Select" CausesValidation="false"  ></asp:LinkButton></td>
                        <td class="Description"><%# Eval("Description") %></td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
        </div>
        <div class="divDetali">
            <wuc:ControlSite ID="controlSite" runat="server" OnDataInserted="controlSite_SiteInserted" 
                OnDataUpdated="controlSite_SiteUpdated" OnDataDeleted="controlSite_SiteDeleted"/>
        </div>
        <div class="clear">

        </div>
    </div>
</asp:Content>

