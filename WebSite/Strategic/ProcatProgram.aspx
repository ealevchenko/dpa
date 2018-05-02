<%@ Page Title="" Language="C#" MasterPageFile="~/WebSite/Strategic/Strategic.master" AutoEventWireup="true" CodeFile="ProcatProgram.aspx.cs" Inherits="WebSite_Strategic_ProcatProgram"  MaintainScrollPositionOnPostback="true" %>
<%@ Register TagPrefix="wuc" TagName="controlProgramProject" Src="~/WebUserControl/Strategic/controlProgramProject.ascx" %>
<%@ Register TagPrefix="wuc" TagName="controlStatusProject" Src="~/WebUserControl/Strategic/controlStatusProject.ascx" %>
<%--<%@ Register TagPrefix="wuc" TagName="controlUpdateProject" Src="~/WebUserControl/Strategic/controlUpdateProject.ascx" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:MultiView ID="mvStatus" runat="server">
        <asp:View ID="vList" runat="server">
            <wuc:controlProgramProject ID="controlProgramProject" runat="server" Change="false" OutInfoText="true" IDImplementationProgram="2" OnDateSelect="controlProgramProject_DateSelect" />
        </asp:View>
        <asp:View ID="vDetali" runat="server">
            <wuc:controlStatusProject ID="controlStatusProject" runat="server" OutInfoText="false" OnPanelClose="controlStatusProject_PanelClose" />
            <%--<wuc:controlUpdateProject ID="controlUpdateProject" runat="server" Change="true" OutInfoText="false" OnPanelClose="controlStatusProject_PanelClose" />--%>
        </asp:View>
    </asp:MultiView>

</asp:Content>

