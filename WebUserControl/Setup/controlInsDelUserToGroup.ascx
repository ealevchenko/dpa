<%@ Control Language="C#" AutoEventWireup="true" CodeFile="controlInsDelUserToGroup.ascx.cs" Inherits="controlInsDelUserToGroup" %>
<style>

</style>
    <asp:ObjectDataSource ID="odsAddDelGroup" runat="server" SelectMethod="SelectAddDelGroupsUsers" TypeName="WebBase.classUsers" OnSelecting="odsAddDelGroup_Selecting" OnSelected="odsAddDelGroup_Selected">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="IDUser" Type="Int32" />
            <asp:Parameter Name="AddDel" Type="Boolean" />
            <asp:Parameter DefaultValue="" Name="IDWeb" Type="Int32" />
            <asp:Parameter Name="IDSection" Type="Int32" />
            <asp:Parameter Name="UserEnterprise" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
<asp:ListView ID="lvAddDelGroup" runat="server" DataKeyNames="IDUser" DataSourceID="odsAddDelGroup" >
    <LayoutTemplate>
        <table class="tabAddDelGroup">
            <tr>
                <th class="command"></th>
                <th class="icon"></th>
                <th class="UserEnterprise">
                    <asp:LinkButton ID="lbUserEnterprise" runat="server" CommandName="sort" CommandArgument="UserEnterprise" Text="<%$ Resources:ResourceBase, nfUserEnterprise %>" CausesValidation="false" /></th>
                <th class="Web">
                    <asp:LinkButton ID="lbWeb" runat="server" CommandName="sort" CommandArgument="IDWeb" Text="<%$ Resources:ResourceBase, nfWeb %>" CausesValidation="false" /></th>
                <th class="Description">
                    <asp:LinkButton ID="lbDescription" runat="server" CommandName="sort" CommandArgument="Description" Text="<%$ Resources:ResourceBase, nfDescription %>" CausesValidation="false" /></th>
                <th class="Section">
                    <asp:LinkButton ID="lbSection" runat="server" CommandName="sort" CommandArgument="IDSection" Text="<%$ Resources:ResourceBase, nfSection %>" CausesValidation="false" />
                </th>
            </tr>
            <tr id="itemPlaceholder" runat="server" />
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr class="item">
            <td class="command">
                <asp:Button ID="btApply" ToolTip='<%# Eval("IDUser") %>' runat="server" Text='<%# GetButtonText() %>' OnClientClick="<%$ Resources:Resource, MessageLock %>" OnClick="btApply_Click" Visible='<%# base.change_button %>'/>
            <td class="icon"><asp:Image ID="imFolder" runat="server" ImageUrl='<%# GetSRCFolder(Container.DataItem) %>' Height="32px" Width="32px" /></td>
            <td class="UserEnterprise"><%# Eval("UserEnterprise") %></td>
            <td class="Web"><%# base.cw.GetWeb(Container.DataItem) %></td>
            <td class="Description"><%# Eval("Description") %></td>
            <td class="Section"><%# base.csection.GetSection(Container.DataItem, false) %></td>
        </tr>
    </ItemTemplate>
</asp:ListView>
