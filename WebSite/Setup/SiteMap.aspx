<%@ Page Title="" Language="C#" MasterPageFile="~/WebSite/Setup/Setup.master" AutoEventWireup="true" CodeFile="SiteMap.aspx.cs" Inherits="WebSite_Setup_SiteMap" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>
<%@ Register TagPrefix="wuc" TagName="ControlSiteMap" Src="~/WebUserControl/Setup/controlSiteMap.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/sitemap.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- -->

    <!-- -->
    <div class="Page">
        <div class="divPanelControl">
            <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsUpdDelUpDow" StyleButton="img" VisibleDelete="false" VisibleUpdate="false" OnInsertClick="pnSelect_InsertClick" OnUpClick="pnSelect_UpClick" OnDownClick="pnSelect_DownClick" />
        </div>
        <div class="divList">
            <asp:TreeView ID="tvSiteMap" runat="server" ImageSet="WindowsHelp" OnSelectedNodeChanged="tvSiteMap_SelectedNodeChanged" OnDataBound="tvSiteMap_DataBound">
                <HoverNodeStyle CssClass="tvHover"/>
                <LevelStyles>
                    <asp:TreeNodeStyle CssClass="tnslevel1" />
                </LevelStyles>
                <ParentNodeStyle CssClass="tvFolder"/>
                <LeafNodeStyle CssClass="tvFile"/>
                <SelectedNodeStyle CssClass="tvSelect"/>
            </asp:TreeView>
        </div>
        <div class="divDetali">
                        <wuc:ControlSiteMap ID="controlSiteMap" runat="server" 
                            OnDataInserted="controlSiteMap_DataInserted" OnDataUpdated="controlSiteMap_DataUpdated" OnDataDeleted="controlSiteMap_DataDeleted"/>
        </div>
        <div class="clear">
        </div>

    </div>
</asp:Content>

