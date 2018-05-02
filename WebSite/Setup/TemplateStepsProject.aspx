<%@ Page Title="" Language="C#" MasterPageFile="~/WebSite/Setup/Setup.master" AutoEventWireup="true" CodeFile="TemplateStepsProject.aspx.cs" Inherits="WebSite_Setup_TemplateStepsProject" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>
<%@ Register TagPrefix="wuc" TagName="controlBigStepProject" Src="~/WebUserControl/Strategic/controlBigStepProject.ascx" %>
<%@ Register TagPrefix="wuc" TagName="controlStepProject" Src="~/WebUserControl/Strategic/controlStepProject.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/templatestepsproject.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <!-- -->
    <asp:ObjectDataSource ID="odsBigStep" runat="server" SelectMethod="SelectCultureBigStepsProject" TypeName="Strategic.classTemplatesSteps"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsStep" runat="server" SelectMethod="SelectCultureStepsProjectToBigStep" TypeName="Strategic.classTemplatesSteps">
        <SelectParameters>
            <asp:Parameter Name="IDBigStep" Type="Int32" />
            <asp:Parameter Name="IDTemplateStepProject" Type="Int32" />
        </SelectParameters>
        </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsListTemplate" runat="server" SelectMethod="SelectCultureTemplatesSteps" TypeName="Strategic.classTemplatesSteps"></asp:ObjectDataSource>
<!-- -->
    <div class="Page">
        <div class="divPanelControl">
            <wuc:panelUpr ID="pnSelectBigStep" runat="server" StylePanel="InsUpdDel" StyleButton="img" VisibleDelete="false" VisibleUpdate="false" OnInsertClick="pnSelectBigStep_InsertClick"/>
        </div>
        <div class="divList">
            <asp:DropDownList ID="ddlTemplate" runat="server" AutoPostBack="True" DataSourceID="odsListTemplate" DataTextField="TemplateStep" DataValueField="IDTemplateStepProject" OnSelectedIndexChanged="ddlTemplate_SelectedIndexChanged"></asp:DropDownList>
            <asp:ListView ID="lvBigStep" runat="server" DataSourceID="odsBigStep" DataKeyNames="IDBigStep" OnItemCreated="lvBigStep_ItemCreated" OnItemDataBound="lvBigStep_ItemDataBound" OnSelectedIndexChanging="lvBigStep_SelectedIndexChanging" OnSelectedIndexChanged="lvBigStep_SelectedIndexChanged" >
                <LayoutTemplate>
                    <table class="tabBigStep">
                        <tr>
                            <th class="BigStep">
                                 <asp:LinkButton ID="lbBigStep" runat="server" CommandName="sort" CommandArgument="BigStep" Text="<%$ Resources:ResourceStrategic, nfBigStepProject %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                            <th class="button">
                                <wuc:panelUpr ID="pnSelectStep" runat="server" StylePanel="InsUpdDel" StyleButton="img" VisibleDelete="false" VisibleUpdate="false" OnInsertClick="pnSelectStep_InsertClick" OnInit="pnSelectStep_Init"/>
                            </th>
                            <th class="Step">
                                <asp:LinkButton ID="lbStep" runat="server" Text="<%$ Resources:ResourceStrategic, nfStepProject %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="item">
                        <td class="BigStep"><b>
                            <asp:LinkButton ID="lbURL" runat="server" Text='<%# Eval("BigStep") %>' CommandName="Select" CausesValidation="false"></asp:LinkButton></b></td>
                        <td class="Step" colspan="2">
                            <asp:ListView ID="lvStep" runat="server" DataKeyNames="IDStep" OnSelectedIndexChanging="lvStep_SelectedIndexChanging" OnSelectedIndexChanged="lvStep_SelectedIndexChanged">
                                <LayoutTemplate>
                                    <table class="tabStep">
                                        <tr id="itemPlaceholder" runat="server" />
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item">
                                        <td class="Step"><b>
                                            <asp:LinkButton ID="lbURL" runat="server" Text='<%# Eval("Step") %>' CommandName="Select" CausesValidation="false"></asp:LinkButton></b></td>
                                    </tr>
                                </ItemTemplate>
                                <SelectedItemTemplate>
                                    <tr class="select">
                                        <td class="Step"><b>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Step") %>'></asp:Label>
                                        </b></td>
                                    </tr>
                                </SelectedItemTemplate>
                            </asp:ListView>
                        </td>
                    </tr>
                </ItemTemplate>
                <SelectedItemTemplate>
                    <tr class="select">
                        <td class="BigStep" ><b>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("BigStep") %>'></asp:Label></b></td>
                        <td class="Step" colspan="2">
                            <asp:ListView ID="lvStep" runat="server" DataKeyNames="IDStep" OnSelectedIndexChanging="lvStep_SelectedIndexChanging" OnSelectedIndexChanged="lvStep_SelectedIndexChanged">
                                <LayoutTemplate>
                                    <table class="tabStep">
                                        <tr id="itemPlaceholder" runat="server" />
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item">
                                        <td class="Step"><b>
                                            <asp:LinkButton ID="lbURL" runat="server" Text='<%# Eval("Step") %>' CommandName="Select" CausesValidation="false"></asp:LinkButton></b></td>
                                    </tr>
                                </ItemTemplate>
                                <SelectedItemTemplate>
                                    <tr class="select">
                                        <td class="Step"><b>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Step") %>'></asp:Label>
                                        </b></td>
                                    </tr>
                                </SelectedItemTemplate>
                            </asp:ListView>
                        </td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
        </div>
        <div class="divDetali">
            <wuc:controlBigStepProject ID="BigStepProject" runat="server" OutInfoText="true" 
                OnDataInserted="BigStepProject_DataInserted" OnDataUpdated="BigStepProject_DataUpdated" OnDataCancelClick="BigStepProject_DataCancelClick" OnDataDeleted="BigStepProject_DataDeleted" />
            <wuc:controlStepProject ID="StepProject" runat="server" OutInfoText="true" 
                OnDataInserted="BigStepProject_DataInserted" OnDataUpdated="BigStepProject_DataUpdated" OnDataCancelClick="BigStepProject_DataCancelClick" OnDataDeleted="BigStepProject_DataDeleted" OnSourceRefresh="StepProject_SourceRefresh" />
        </div>
        <div class="clear">

        </div>
    </div>
</asp:Content>

