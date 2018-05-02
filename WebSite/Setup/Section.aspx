<%@ Page Title="" Language="C#" MasterPageFile="~/WebSite/Setup/Setup.master" AutoEventWireup="true" CodeFile="Section.aspx.cs" Inherits="WebSite_Setup_Section" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>
<%@ Register TagPrefix="wuc" TagName="controlSection" Src="~/WebUserControl/Setup/controlSection.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/section.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- -->

    <!-- -->
    <div class="Page">
        <div class="divPanelControl">
            <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsUpdDelUpDow" StyleButton="img" VisibleDelete="false" VisibleUpdate="false" OnInsertClick="pnSelect_InsertClick" OnUpClick="pnSelect_UpClick" OnDownClick="pnSelect_DownClick" />
        </div>
        <div class="divList">
            <asp:TreeView ID="tvSection" runat="server" ImageSet="WindowsHelp" OnSelectedNodeChanged="tvSection_SelectedNodeChanged" OnDataBound="tvSection_DataBound">
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
                        <wuc:controlSection ID="controlSection" runat="server" OutInfoText="true"
                            OnDataInserted="controlSection_DataInserted" OnDataUpdated="controlSection_DataUpdated" OnDataDeleted="controlSection_DataDeleted"/>
        </div>
        <div class="clear">
        </div>

    </div>
</asp:Content>

