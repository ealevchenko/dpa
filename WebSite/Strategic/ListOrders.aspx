<%@ Page Title="" Language="C#" MasterPageFile="~/WebSite/Strategic/Strategic.master" AutoEventWireup="true" CodeFile="ListOrders.aspx.cs" Inherits="WebSite_Strategic_ListOrders" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>
<%@ Register TagPrefix="wuc" TagName="controlTypeOrder" Src="~/WebUserControl/Strategic/controlTypeOrder.ascx" %>
<%@ Register TagPrefix="wuc" TagName="controlOrder" Src="~/WebUserControl/Strategic/controlOrder.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
            <link href="css/listorders.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ObjectDataSource ID="odsListTypeOrder" runat="server" SelectMethod="SelectCultureTypeOrder" TypeName="Strategic.classOrder"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsListOrder" runat="server" SelectMethod="SelectCultureListOrderIsType" TypeName="Strategic.classOrder">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlListTypeOrder" DefaultValue="0" Name="IDTypeOrder" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
        <div class="Page">
            <div class="selectTypeOrder">
                <table>
                    <tr>
                        <td class="name">
                            <asp:Literal ID="ltName" runat="server" Text="<%$ Resources:ResourceStrategic, tsTypeOrder%>"></asp:Literal>:&nbsp;
                        </td>
                        <td class="nameddl">
                            <asp:DropDownList ID="ddlListTypeOrder" runat="server" DataSourceID="odsListTypeOrder" DataTextField="TypeOrder" DataValueField="IDTypeOrder" OnDataBound="ddlListTypeOrder_DataBound" AutoPostBack="true" CssClass="ddlbtselect" OnSelectedIndexChanged="ddlListTypeOrder_SelectedIndexChanged"></asp:DropDownList>
                            &nbsp;
                            <wuc:panelUpr ID="pnUprTypeOrder" runat="server" StylePanel="InsUpdDel" StyleButton="img" OnInsertClick="pnUprTypeOrder_InsertClick" OnUpdateClick="pnUprTypeOrder_UpdateClick" OnDeleteClick="pnUprTypeOrder_DeleteClick" SizeImage="24" ConfirmDelete="MessageDeleteTypeOrder" />
                        </td>

                    </tr>
                    <tr>
                        <td colspan="2">
                            <wuc:controlTypeOrder ID="contrTypeOrder" runat="server" Change="true" OutInfoText="true" OnDataInserted="contrTypeOrder_DataInserted" OnDataUpdated="contrTypeOrder_DataUpdated" OnDataCancelClick="contrTypeOrder_DataCancelClick" />
                            <wuc:controlOrder ID="controlOrder" runat="server" OutInfoText="true" OnDataInserted="controlOrder_DataInserted" OnDataUpdated="controlOrder_DataUpdated" OnDataDeleted="controlOrder_DataDeleted"/>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="listOrders">
                <asp:Label ID="lbTitle" runat="server" Text="<%$ Resources:ResourceStrategic, titleListOrder %>" CssClass="title"></asp:Label>
                <div class="upr">
                    <wuc:panelUpr ID="PanelUprOrder" runat="server" StylePanel="InsUpdDel" VisibleDelete="false" VisibleUpdate="false" StyleButton="img" OnInsertClick="PanelUprOrder_InsertClick" />
                </div>
                <asp:ListView ID="lvListOrder" runat="server" DataSourceID="odsListOrder" DataKeyNames="IDOrder" OnSelectedIndexChanged="lvListOrder_SelectedIndexChanged">
                    <LayoutTemplate>
                        <table class="tabListOrder">
                            <tr>
                                <th class="order">
                                    <asp:LinkButton ID="lbOrder" runat="server" CommandName="sort" CommandArgument="Order" Text="<%$ Resources:ResourceStrategic, tsOrder %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" OnClick="lbSelect_Click" /></th>
                                <th class="numorder">
                                    <asp:LinkButton ID="lbNumOrder" runat="server" CommandName="sort" CommandArgument="NumOrder" Text="<%$ Resources:ResourceStrategic, tsNumOrder %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" OnClick="lbSelect_Click" /></th>
                                <th class="dateorder">
                                    <asp:LinkButton ID="lbDateOrder" runat="server" CommandName="sort" CommandArgument="DateOrder" Text="<%$ Resources:ResourceStrategic, tsDateOrder %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" OnClick="lbSelect_Click" /></th>
                                <th class="file">
                                    <asp:LinkButton ID="lbProject" runat="server" Text="<%$ Resources:ResourceStrategic, tsOrderInRus %>" /></th>
                                <th class="fileeng">
                                    <asp:LinkButton ID="lbIndexKPI" runat="server" Text="<%$ Resources:ResourceStrategic, tsOrderInEng %>"  /></th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="item">
                            <td class="order">
                                <asp:LinkButton ID="lbSelectOrder" runat="server" Text='<%# Eval("Order") %>' CommandName="Select" CausesValidation="false" CommandArgument='<%# Eval("IDOrder") %>' ></asp:LinkButton></td>
                            <td class="numorder"><%# Eval("NumOrder") %></td>
                            <td class="dateorder"><%# Eval("DateOrder", "{0:dd-MM-yyyy}") %></td>
                            <td class="file"><%# base.corder.GetFileOrder(Container.DataItem, "IDFile") %></td>
                            <td class="fileeng"><%# base.corder.GetFileOrder(Container.DataItem, "IDFileEng") %></td>
                        </tr>
                    </ItemTemplate>
                    <SelectedItemTemplate>
                        <tr class="select">
                            <td class="order">
                                <asp:LinkButton ID="lbSelectOrder" runat="server" Text='<%# Eval("Order") %>' CommandName="Select" CausesValidation="false" CommandArgument='<%# Eval("IDOrder") %>' ></asp:LinkButton></td>
                            <td class="numorder"><%# Eval("NumOrder") %></td>
                            <td class="dateorder"><%# Eval("DateOrder", "{0:dd-MM-yyyy}") %></td>
                            <td class="file"><%# base.corder.GetFileOrder(Container.DataItem, "IDFile") %></td>
                            <td class="fileeng"><%# base.corder.GetFileOrder(Container.DataItem, "IDFileEng") %></td>
                        </tr>
                    </SelectedItemTemplate>
                </asp:ListView>

            </div>
        </div>
</asp:Content>

