<%@ Page Title="" Language="C#" MasterPageFile="~/WebSite/Strategic/Strategic.master" AutoEventWireup="true" CodeFile="StatusProject.aspx.cs" Inherits="WebSite_Strategic_scripts_StatusProject" MaintainScrollPositionOnPostback="true" %>
<%@ Register TagPrefix="wuc" TagName="controlStatusProject" Src="~/WebUserControl/Strategic/controlStatusProject.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/statusproject.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--  --%>
    <asp:ObjectDataSource ID="odsListProject" runat="server" SelectMethod="SelectCultureProject" TypeName="Strategic.classProject" OnSelecting="odsListProject_Selecting">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlTypeProject" DefaultValue="0" Name="IDTypeProject" PropertyName="SelectedValue" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="Year" Type="Int32" />
            <asp:ControlParameter ControlID="ddlMenager" DefaultValue="0" Name="IDMenagerProject" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="ddlStatus" DefaultValue="0" Name="Status" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsListTypeProject" runat="server" SelectMethod="SelectCultureTypeProject" TypeName="Strategic.classProject"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsListMenager" runat="server" SelectMethod="ListMenagerProject" TypeName="Strategic.classMenagerProject"></asp:ObjectDataSource>
    <%--  --%>
    <div class="Page">
        <asp:MultiView ID="mvStatus" runat="server">
            <asp:View ID="vList" runat="server">
                <div class="SelectStatus">
                    <asp:Literal ID="ltstypeproject" runat="server" Text="<%$ Resources:ResourceStrategic, tsTypeProject %>"></asp:Literal>:&nbsp;
            <asp:DropDownList ID="ddlTypeProject" runat="server" AutoPostBack="True" DataSourceID="odsListTypeProject"
                DataTextField="TypeProject" DataValueField="IDTypeProject" OnDataBound="ddlTypeProject_DataBound" CssClass="typeProject">
            </asp:DropDownList>
                    <%--&nbsp;&nbsp;<asp:Literal ID="ltsyear" runat="server" Text="<%$ Resources:ResourceStrategic, tsYear %>" ></asp:Literal>:&nbsp;
            <asp:DropDownList ID="ddlYear" runat="server" OnDataBound="ddlYear_DataBound" AutoPostBack="true"></asp:DropDownList>--%>
            &nbsp;&nbsp;<asp:Literal ID="ltsmenager" runat="server" Text="<%$ Resources:ResourceStrategic, tsNameMenager %>"></asp:Literal>:&nbsp;
            <asp:DropDownList ID="ddlMenager" runat="server" AutoPostBack="True" DataSourceID="odsListMenager" DataTextField="MenagerProject" DataValueField="IDMenagerProject" OnDataBound="ddlMenager_DataBound" CssClass="menagerProject"></asp:DropDownList>
                    &nbsp;&nbsp;<asp:Literal ID="ltnamestatus" runat="server" Text="<%$ Resources:ResourceStrategic, tsNameStatus %>"></asp:Literal>:&nbsp;
            <asp:DropDownList ID="ddlStatus" runat="server" OnDataBound="ddlStatus_DataBound" CssClass="statusProject" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="ListProject">
                    <asp:ListView ID="lvListProject" runat="server" DataSourceID="odsListProject" DataKeyNames="IDProject" OnSelectedIndexChanged="lvListProject_SelectedIndexChanged">
                        <LayoutTemplate>
                            <table class="tabListProject">
                                <tr>
                                    <th class="sapcode">
                                        <asp:LinkButton ID="lbSap" runat="server" CommandName="sort" CommandArgument="SAPCode" Text="<%$ Resources:ResourceStrategic, spSAPCode %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                    <th class="funding">
                                        <asp:LinkButton ID="lbFunding" runat="server" CommandName="sort" CommandArgument="Funding" Text="<%$ Resources:ResourceStrategic, spFunding %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                    <th class="name">
                                        <asp:LinkButton ID="lbProject" runat="server" CommandName="sort" CommandArgument="Name" Text="<%$ Resources:ResourceStrategic, spProject %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                    <th class="type">
                                        <asp:LinkButton ID="lbType" runat="server" CommandName="sort" CommandArgument="IDTypeProject" Text="<%$ Resources:ResourceStrategic, spType %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                    <th class="typestatus">
                                        <asp:LinkButton ID="lbTypeStatus" runat="server" CommandName="sort" CommandArgument="TypeStatus" Text="<%$ Resources:ResourceStrategic, spTypeStatus %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                    <th class="menager">
                                        <asp:LinkButton ID="lbMenager" runat="server" CommandName="sort" CommandArgument="IDMenagerProject" Text="<%$ Resources:ResourceStrategic, spMenager %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server" />
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class='<%# base.cproject.GetEnumStatusProject(Container.DataItem) %>'>
                                <td class="sapcode"><%# base.cproject.GetSapDepartment(Container.DataItem) %></td>
                                <td class="funding">
                                    <asp:Label ID="lbFunding" runat="server" Text='<%# Eval("Funding", "{0:###,###,##0.00}") %>' CssClass='<%# base.dir.GetStringBool(Container.DataItem,"AllocationFunds","yes","no") %>'></asp:Label>
                                    &nbsp;<%# base.cproject.GetCurrency(Container.DataItem) %>&nbsp; <%# Eval("FundingDescription") %>
                                </td>
                                <td class="name">
                                    <asp:LinkButton ID="lbName" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDProject") %>' CommandName="Select" CausesValidation="false" OnClientClick="<%$ Resources:Resource, MessageLock %>"></asp:LinkButton>
                                </td>
                                <td class="type"><%# base.cproject.GetTypeProject(Container.DataItem) %></td>
                                <td class="typestatus"><%# base.cproject.GetTypeStatus(Container.DataItem) %></td>
                                <td class="menager"><%# base.cmenager.GetFIO(Container.DataItem) %><br />
                                    <a href='mailto:<%# base.cmenager.GetEmail(Container.DataItem) %>'>
                                        <asp:Literal ID="lttxtemmail" runat="server" Text='<%# base.cmenager.GetEmail(Container.DataItem) %>'></asp:Literal></a></td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </asp:View>
            <asp:View ID="vDetali" runat="server">
            <wuc:controlStatusProject ID="controlStatusProject" runat="server" Change="true" OutInfoText="false" OnPanelClose="controlStatusProject_PanelClose" />
            </asp:View>
        </asp:MultiView>

    </div>
</asp:Content>

