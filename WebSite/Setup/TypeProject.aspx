<%@ Page Title="" Language="C#" MasterPageFile="~/WebSite/Setup/Setup.master" AutoEventWireup="true" CodeFile="TypeProject.aspx.cs" Inherits="WebSite_Setup_TypeProject" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>
<%@ Register TagPrefix="wuc" TagName="controlTypeProject" Src="~/WebUserControl/Strategic/controlTypeProject.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/typeproject.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- -->
    <asp:ObjectDataSource ID="odsList" runat="server" SelectMethod="SelectCultureTypeProject" TypeName="Strategic.classProject"></asp:ObjectDataSource>
<!-- -->
    <div class="Page">
        <div class="divPanelControl">
            <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsUpdDel" StyleButton="img" VisibleDelete="false" VisibleUpdate="false" OnInsertClick="pnSelect_InsertClick"/>
        </div>
        <div class="divList">
            <asp:ListView ID="lvList" runat="server" DataSourceID="odsList" DataKeyNames="IDTypeProject" OnSelectedIndexChanged="lvList_SelectedIndexChanged">
                <LayoutTemplate>
                    <table class="tabList">
                        <tr>
                            <th class="type">
                                <asp:LinkButton ID="lbTypeProject" runat="server" CommandName="sort" CommandArgument="TypeProject" Text="<%$ Resources:ResourceStrategic, nfTypeProject %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                            <th class="description">
                                <asp:LinkButton ID="lbDescription" runat="server" CommandName="sort" CommandArgument="Description" Text="<%$ Resources:ResourceStrategic, nfTypeProjectDescription %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="item">
                        <td class="type"><b>
                            <asp:LinkButton ID="lbTypeProject" runat="server" Text='<%# Eval("TypeProject") %>' CommandName="Select" CausesValidation="false" ></asp:LinkButton></b></td>
                        <td class="description"><%# Eval("Description") %></td>
                    </tr>
                </ItemTemplate>
                <SelectedItemTemplate>
                    <tr class="select">
                        <td class="type"><asp:LinkButton ID="lbTypeProject" runat="server" Text='<%# Eval("TypeProject") %>' CommandName="Select" CausesValidation="false"  ></asp:LinkButton></td>
                        <td class="description"><%# Eval("Description") %></td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
        </div>
        <div class="divDetali">
            <wuc:controlTypeProject ID="controlTypeProject" runat="server" OnDataInserted="controlTypeProject_SiteInserted" OutInfoText="true" 
                OnDataUpdated="controlTypeProject_SiteUpdated" OnDataDeleted="controlTypeProject_SiteDeleted"/>
        </div>
        <div class="clear">

        </div>
    </div>
</asp:Content>

