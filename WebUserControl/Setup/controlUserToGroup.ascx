<%@ Control Language="C#" AutoEventWireup="true" CodeFile="controlUserToGroup.ascx.cs" Inherits="controlUserToGroup" %>
<style>
/**/
table.tabGroup {
    border: 1px solid #999999;
    color: #000000;
    border-radius: 3px;
    background-color: #F7F6F3;
    font-family: 'Arial Narrow';
    width: 600px;
    margin-top: 20px;
    font-size: 14px;
}
.tabGroup th {
    padding: 5px;
    border: 1px solid #999999;
    color: #5B5B5B;
    font-weight: bold;
    word-wrap: hyphenate;
    text-align: center;
    text-transform: uppercase;
    background-color: #F7F6F3;
}
.tabGroup th a {
    color: #0000FF;
    text-transform: uppercase;
    text-decoration: underline;

}
.tabGroup td {
    padding: 5px;
    border: 1px solid #999999;
    color: #000000;
    text-align: center;
    background-color: #FFFFFF;
}


.tabGroup th.icon {
    width: 50px;
}
.tabGroup th.UserEnterprise {
    width: 200px;
}
.tabGroup th.Description {
    width: 300px;
}
.tabGroup td.icon {
    text-align: center;
}
.tabGroup td.UserEnterprise {
    text-align: left;
    font-weight: bold;
    text-transform: uppercase;
}
.tabGroup td.Description {
    text-align: left;
    vertical-align: top;
    font-style: italic;
}
.tabGroup td.detali {
    text-align: left;
    vertical-align: top;
    font-style: italic;
}
</style>
<asp:ObjectDataSource ID="odsListGroupsUsers" runat="server" SelectMethod="SelectGroupUsers" TypeName="WebBase.classUsers" OnSelected="odsListGroupsUsers_Selected" OnSelecting="odsListGroupsUsers_Selecting">
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="IDUser" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ListView ID="lvGroup" runat="server" DataSourceID="odsListGroupsUsers">
    <LayoutTemplate>
        <table class="tabGroup">
            <tr>
                <th class="icon"></th>
                <th class="UserEnterprise">
                    <asp:LinkButton ID="lbUserEnterprise" runat="server" CommandName="sort" CommandArgument="UserEnterprise" Text="<%$ Resources:ResourceBase, nfUserEnterprise %>" /></th>
                <th class="Description">
                    <asp:LinkButton ID="lbDescription" runat="server" CommandName="sort" CommandArgument="Description" Text="<%$ Resources:ResourceBase, nfDescription %>" /></th>
            </tr>
            <tr id="itemPlaceholder" runat="server" />
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr class="item">
            <td class="upr"><asp:Image ID="imFolder" runat="server" ImageUrl='<%# GetSRCFolder(Container.DataItem) %>' Height="32px" Width="32px" /></td>
            <td class="UserEnterprise"><%# Eval("UserEnterprise") %></td>
            <td class="Description"><%# Eval("Description") %></td>

        </tr>
    </ItemTemplate>

</asp:ListView>
