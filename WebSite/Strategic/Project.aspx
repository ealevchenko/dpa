<%@ Page Title="" Language="C#" MasterPageFile="~/WebSite/Strategic/Strategic.master" AutoEventWireup="true" CodeFile="Project.aspx.cs" Inherits="WebSite_Strategic_Project" MaintainScrollPositionOnPostback="true" %>
<%@ Register TagPrefix="wuc" TagName="controlProject" Src="~/WebUserControl/Strategic/controlProject.ascx" %>
<%@ Register TagPrefix="wuc" TagName="controlDetaliProject" Src="~/WebUserControl/Strategic/controlDetaliProject.ascx" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <link href="css/project.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <!-- -->
        <asp:ObjectDataSource ID="odsList" runat="server" SelectMethod="SelectCultureProject" TypeName="Strategic.classProject" OnSelecting="odsList_Selecting">
            <SelectParameters>
                <asp:ControlParameter ControlID="rblProject" DefaultValue="0" Name="TypeMenager" PropertyName="SelectedValue" Type="Int32" />
                <asp:Parameter Name="IDMenagerProject" Type="Int32" />
                <asp:ControlParameter ControlID="ddlStatus" DefaultValue="-1" Name="Status" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsListMenagerProject" runat="server" SelectMethod="ListMenagerProject" TypeName="Strategic.classMenagerProject"></asp:ObjectDataSource>
    <!-- -->
    <div class="Page">
        <asp:MultiView ID="mvStatus" runat="server">
            <asp:View ID="vList" runat="server">
                <div class="divSelectProject">
                    <table class="tabSelectProject">
                        <tr>
                            <td>
                                <wuc:panelUpr ID="pnInsertProject" runat="server" StylePanel="InsUpdDel" StyleButton="img" VisibleDelete="false" VisibleUpdate="false" OnInsertClick="pnInsertProject_InsertClick" />
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblProject" runat="server" AutoPostBack="True" TextAlign="Right" RepeatDirection="Horizontal" OnDataBound="rblProject_DataBound" OnSelectedIndexChanged="rblProject_SelectedIndexChanged"
                                    Onchange="<%$ Resources:Resource, MessageLock %>" Visible='<%# !this.Boss %>'>
                                    <asp:ListItem Value="0" Text="<%$ Resources:ResourceStrategic, MyProjects %>"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="<%$ Resources:ResourceStrategic, MyPerformance %>">Мои исполнения</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:Label ID="lbMenagerProject" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameMenager %>" Visible='<%# this.Boss %>'></asp:Label><asp:Label ID="lbnbsp" runat="server" Text=": " Visible='<%# this.Boss %>'></asp:Label>
                                <asp:DropDownList ID="ddlMenagerProject" runat="server" DataSourceID="odsListMenagerProject" AutoPostBack="true" DataTextField="MenagerProject" DataValueField="IDMenagerProject"
                                    Visible='<%# this.Boss %>' OnSelectedIndexChanged="ddlMenagerProject_SelectedIndexChanged" OnDataBound="ddlMenagerProject_DataBound">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Literal ID="ltNameStatus" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameStatus %>"></asp:Literal>:&nbsp;
                            <asp:DropDownList ID="ddlStatus" runat="server" OnDataBound="ddlStatus_DataBound" AutoPostBack="true" Onchange="<%$ Resources:Resource, MessageLock %>" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                    </table>
                </div>
                <div class="divList">
                    <asp:ListView ID="lvList" runat="server" DataSourceID="odsList" DataKeyNames="IDProject" OnSelectedIndexChanged="lvList_SelectedIndexChanged">
                        <LayoutTemplate>
                            <table class="tabListProject">
                                <tr>
                                    <th class="type">
                                        <asp:LinkButton ID="lbName" runat="server" CommandName="sort" CommandArgument="Name" Text="<%$ Resources:ResourceStrategic, nfNameProject %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                    <th class="description">
                                        <asp:LinkButton ID="lbDescription" runat="server" CommandName="sort" CommandArgument="Description" Text="<%$ Resources:ResourceStrategic, nfProjecDescriptiont %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                    <asp:Panel ID="pnBoss" runat="server" OnLoad="pnBoss_Load">
                                        <th class="menager">
                                            <asp:LinkButton ID="lbMenager" runat="server" CommandName="sort" CommandArgument="IDMenagerProject" Text="<%$ Resources:ResourceStrategic, nfNameMenager %>" OnClientClick="<%$ Resources:Resource, MessageLock %>"></asp:LinkButton></th>
                                        <th class="datechange">
                                            <asp:LinkButton ID="lbDateChange" runat="server" CommandName="sort" CommandArgument="Change" Text="<%$ Resources:ResourceStrategic, nfDateChange %>" OnClientClick="<%$ Resources:Resource, MessageLock %>"></asp:LinkButton></th>
                                    </asp:Panel>
                                </tr>
                                <tr id="itemPlaceholder" runat="server" />
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class="item">
                                <td class="name"><b>
                                    <asp:LinkButton ID="lbName" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDProject") %>' CommandName="Select" CausesValidation="false" OnClientClick="<%$ Resources:Resource, MessageLock %>"></asp:LinkButton></b></td>
                                <td class="description"><%# Eval("Description") %></td>
                                <asp:Panel ID="pnBoss" runat="server" OnLoad="pnBoss_Load">
                                    <td class="menager">
                                        <%# base.cmenager.GetFIO(Container.DataItem) %><br />
                                        <a href='mailto:<%# base.cmenager.GetEmail(Container.DataItem) %>'>
                                            <asp:Literal ID="lttxtemmail" runat="server" Text='<%# base.cmenager.GetEmail(Container.DataItem) %>'></asp:Literal></a>
                                    </td>
                                    <td class="datechange">
                                        <%# Eval("Change", "{0:dd-MM-yyyy HH:mm:ss}") %>
                                    </td>
                                </asp:Panel>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </asp:View>
            <asp:View ID="vDetali" runat="server">
                <asp:ImageButton ID="ibStatus" runat="server" ImageUrl="~/WebSite/Strategic/image/Return.png" ToolTip="<%$ Resources:ResourceStrategic, spToolTipStatus %>" OnClick="ibStatus_Click" OnClientClick="<%$ Resources:Resource, MessageLock %>" />
                <wuc:controlProject ID="controlProject" runat="server" OutInfoText="true" OnDataInserted="controlProject_DataInserted" OnDataUpdated="controlProject_DataUpdated" OnDataDeleted="controlProject_DataDeleted" OnStepCreateClick="controlProject_StepCreateClick" OnStepClearClick="controlProject_StepClearClick" />
                <wuc:controlDetaliProject ID="controlDetaliProject" runat="server" OutInfoText="true" />
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>

