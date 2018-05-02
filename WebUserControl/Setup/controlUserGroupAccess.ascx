<%@ Control Language="C#" AutoEventWireup="true" CodeFile="controlUserGroupAccess.ascx.cs" Inherits="controlUserGroupAccess" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>
<%@ Register TagPrefix="wuc" TagName="ControlRules" Src="~/WebUserControl/Setup/controlRules.ascx" %>
<style type="text/css">
    table.tabAccessSiteMap {
        border: 1px solid #999999;
        color: #000000;
        border-radius: 3px;
        background-color: #F7F6F3;
        font-family: 'Arial Narrow';
        width: 1000px;
        margin-top: 20px;
        /*font-size: 12px;*/
        margin-bottom: 20px;
    }

    .tabAccessSiteMap th {
        padding: 5px 10px 5px 5px;
        border: 1px solid #999999;
        color: #5B5B5B;
        font-weight: bold;
        word-wrap: hyphenate;
        text-align: center;
        text-transform: uppercase;
        background-color: #F7F6F3;
    }

    .tabAccessSiteMap td {
        padding: 5px;
        border: 1px solid #999999;
        /*color: #000000;*/
        /*background-color: #FFFFFF;*/
    }
    .tabAccessSiteMap tr.item {
        background-color: #FFFFFF;
        color: #000000;
    }
    .tabAccessSiteMap tr.select {
    background-color: #999999;
    color: #FFFFFF;
    text-transform: uppercase;
}
    .tabAccessSiteMap tr.edit {
    background-color: #999999;
    color: #FFFFFF;
    text-transform: uppercase;
}
    th.upr {
        width :50px;
    }
    th.title {
        width :200px;
    }
    th.description {
        width :200px;
    }
    th.web {
        width :100px;
    }
    th.owner {
        width :100px;
    }
    th.access {
        width :100px;
    }
    th.rules {
        width :250px;
    }
</style>
<asp:ObjectDataSource ID="odsUserAccessSiteMap" runat="server" OnSelected="odsUserGroupAccess_Selected" OnSelecting="odsUserGroupAccess_Selecting" SelectMethod="SelectUserToAccessSiteMapCulture" TypeName="WebBase.classAccessSiteMap" OnUpdated="odsUserGroupAccess_Updated" OnUpdating="odsUserGroupAccess_Updating" UpdateMethod="SetRulesAccessSiteMap">
    <SelectParameters>
        <asp:Parameter Name="IDUser" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="IDAccessSiteMap" Type="Int32" />
        <asp:Parameter Name="Access" Type="Int32" />
        <asp:Parameter Name="AccessRules" Type="String" />
        <asp:Parameter Name="OutResult" Type="Boolean" DefaultValue="true" />
    </UpdateParameters>
</asp:ObjectDataSource>
<asp:ListView ID="lvAccessSiteMap" runat="server" DataSourceID="odsUserAccessSiteMap" DataKeyNames="IDAccessSiteMap" OnItemDataBound="lvAccessSiteMap_ItemDataBound" >
    <LayoutTemplate>
        <table class="tabAccessSiteMap">
            <tr>
                <th class="upr"></th>
                <th class="title">
                    <asp:LinkButton ID="lbTitle" runat="server" Text="<%$ Resources:ResourceBase, tsTitle %>" CommandName="sort" CommandArgument="Title" CausesValidation="false"></asp:LinkButton></th>
                <th class="description">
                    <asp:LinkButton ID="lbDescription" runat="server" Text="<%$ Resources:ResourceBase, tsDescription %>" CommandName="sort" CommandArgument="Description" CausesValidation="false"></asp:LinkButton></th>
                <th class="web">
                    <asp:LinkButton ID="lbWeb" runat="server" Text="<%$ Resources:ResourceBase, tsWeb %>" CommandName="sort" CommandArgument="IDWeb" CausesValidation="false"  ></asp:LinkButton></th>
                <th class="owner">
                    <asp:LinkButton ID="lbSection" runat="server" Text="<%$ Resources:ResourceBase, tsOwner %>" CommandName="sort" CommandArgument="IDSection" CausesValidation="false"></asp:LinkButton></th>
                <th class="access">
                    <asp:LinkButton ID="lbAccess" runat="server" Text="<%$ Resources:ResourceBase, nfAccess %>" CommandName="sort" CommandArgument="Access" CausesValidation="false" ></asp:LinkButton></th>
                <th class="rules"><asp:Literal ID="ltRules" runat="server" Text="<%$ Resources:ResourceBase, nfListRules %>"></asp:Literal></th>
            </tr>
            <tr id="itemPlaceholder" runat="server" />
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr class="item">
            <td class="upr">

            </td>
            <td class="title">
                <asp:LinkButton ID="lbTitle" runat="server" Text='<%# Eval("Title") %>' CommandName="Select"></asp:LinkButton></td>
            <td class="description"><%# Eval("Description") %></td>
            <td class="web"><%# base.cw.GetWeb(Container.DataItem) %></td>            
            <td class="owner"><%# base.csection.GetSection(Container.DataItem, false) %></td>
            <td class="access"><%# GetAccess(Container.DataItem) %></td>
            <td class="rules"><%# Eval("AccessRules") %></td>
        </tr>
    </ItemTemplate>
    <SelectedItemTemplate>
        <tr class="select">
            <td class="upr">
                <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsUpdDel" OnInit="pnSelect_Init" />
            </td>
            <td class="title"><%# Eval("Title") %></td>
            <td class="description"><%# Eval("Description") %></td>
            <td class="web"><%# base.cw.GetWeb(Container.DataItem) %></td>            
            <td class="owner"><%# base.csection.GetSection(Container.DataItem, false) %></td>
            <td class="access"><%# GetAccess(Container.DataItem) %></td>
            <td class="rules"><%# Eval("AccessRules") %></td>
        </tr>
    </SelectedItemTemplate>
    <EditItemTemplate>
        <tr class="edit">
            <td class="upr">
                <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="UpdCan" OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick" Change="true"  />
            </td>
            <td class="title"><%# Eval("Title") %><%# Eval("Title") %></td>
            <td class="description"><%# Eval("Description") %></td>
            <td class="web"><%# base.cw.GetWeb(Container.DataItem) %></td>            
            <td class="owner"><%# base.csection.GetSection(Container.DataItem, false) %></td>
            <td class="access">
                <asp:DropDownList ID="ddlAccess" runat="server" ></asp:DropDownList></td>
            <td class="rules">
                 <wuc:ControlRules ID="ControlRules" runat="server" />
            </td>
        </tr>
    </EditItemTemplate>
</asp:ListView>