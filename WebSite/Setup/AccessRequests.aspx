<%@ Page Title="" Language="C#" MasterPageFile="~/WebSite/Setup/Setup.master" AutoEventWireup="true" CodeFile="AccessRequests.aspx.cs" Inherits="WebSite_Setup_AccessRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/accessrequests.css" rel="stylesheet" type="text/css" />
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ObjectDataSource ID="odsListRequests" runat="server" SelectMethod="SelectAccessUsers" TypeName="WebBase.classAccessUsers">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="Active" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsListAccessWebUsers" runat="server" SelectMethod="SelectAccessWebUsersToAccessUsers" TypeName="WebBase.classAccessUsers">
        <SelectParameters>
            <asp:ControlParameter ControlID="lvlistRequests" DefaultValue="0" Name="IDAccessUsers" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div class="Page">
        <div class="listRequests">
            <asp:ListView ID="lvlistRequests" runat="server" DataSourceID="odsListRequests" DataKeyNames="IDAccessUsers">
                <LayoutTemplate>
                    <table class="tablistRequests">
                        <tr>
                            <th class="UserEnterprise">
                                <asp:LinkButton ID="lbUserEnterprise" runat="server" CommandName="sort" CommandArgument="UserEnterprise" Text="<%$ Resources:ResourceBase, arUserEnterprise %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                            <th class="Email">
                                <asp:LinkButton ID="lbEmail" runat="server" CommandName="sort" CommandArgument="Email" Text="<%$ Resources:ResourceBase, arEmail %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                            <th class="DateCreate">
                                <asp:LinkButton ID="lbDateCreate" runat="server" CommandName="sort" CommandArgument="DateCreate" Text="<%$ Resources:ResourceBase, arDateCreate %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                            <th class="DateChange">
                                <asp:LinkButton ID="lbDateChange" runat="server" CommandName="sort" CommandArgument="DateChange" Text="<%$ Resources:ResourceBase, arDateChange %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                            <th class="DateAccess">
                                <asp:LinkButton ID="lbDateAccess" runat="server" CommandName="sort" CommandArgument="DateAccess" Text="<%$ Resources:ResourceBase, arDateAccess %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="item">
                        <td class="UserEnterprise">
                            <asp:LinkButton ID="lbURL" runat="server" Text='<%# Eval("UserEnterprise") %>' CommandName="Select" CausesValidation="false"></asp:LinkButton></td>
                        <td class="Email"><a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a></td>
                        <td class="DateCreate"><%# Eval("DateCreate", "{0:dd-MM-yyyy HH:mm:ss}") %></td>
                        <td class="DateChange"><%# Eval("DateChange", "{0:dd-MM-yyyy HH:mm:ss}") %></td>
                        <td class="DateAccess"><%# Eval("DateAccess", "{0:dd-MM-yyyy HH:mm:ss}") %></td>
                    </tr>
                </ItemTemplate>
                <SelectedItemTemplate>
                    <tr class="select">
                        <td class="UserEnterprise"><b>
                            <asp:LinkButton ID="lbURL" runat="server" Text='<%# Eval("UserEnterprise") %>' CommandName="Select" CausesValidation="false"></asp:LinkButton></b></td>
                        <td class="Email"><a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a></td>
                        <td class="DateCreate"><%# Eval("DateCreate", "{0:dd-MM-yyyy HH:mm:ss}") %></td>
                        <td class="DateChange"><%# Eval("DateChange", "{0:dd-MM-yyyy HH:mm:ss}") %></td>
                        <td class="DateAccess"><%# Eval("DateAccess", "{0:dd-MM-yyyy HH:mm:ss}") %>
                            <asp:Button ID="btClose" runat="server" Text="Закрыть заявку" Visible='<%# EnableButtonClose(Container.DataItem) %>' OnClick="btClose_Click" /></td>
                    </tr>
                    <tr class="select">
                        <td colspan="5">
                            <asp:ListView ID="lvAccessWeb" runat="server" DataSourceID="odsListAccessWebUsers" DataKeyNames="IDAccessWebUsers">
                                <LayoutTemplate>
                                    <table class="tabAccessWeb">
                                        <tr>
                                            <th class="access">
                                                <asp:Literal ID="ltAccess" runat="server" Text="<%$ Resources:ResourceBase, auAccess %>"></asp:Literal></th>
                                            <th class="accessweb">
                                                <asp:Literal ID="ltAccessWeb" runat="server" Text="<%$ Resources:ResourceBase, auAccessWeb %>"></asp:Literal></th>
                                            <th class="ownerweb">
                                                <asp:Literal ID="ltOwnerWeb" runat="server" Text="<%$ Resources:ResourceBase, auOwnerWeb %>"></asp:Literal></th>
                                            <th class="daterequest">
                                                <asp:Literal ID="ltDateRequest" runat="server" Text="<%$ Resources:ResourceBase, auDateRequest %>"></asp:Literal></th>
                                            <th class="dateapproval">
                                                <asp:Literal ID="ltDateApproval" runat="server" Text="<%$ Resources:ResourceBase, auDateApproval %>"></asp:Literal></th>
                                            <th class="solution">
                                                <asp:Literal ID="ltSolution" runat="server" Text="<%$ Resources:ResourceBase, auSolution %>"></asp:Literal></th>
                                            <th class="coment">
                                                <asp:Literal ID="ltComent" runat="server" Text="<%$ Resources:ResourceBase, auComent %>"></asp:Literal></th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class='<%# GetSolutionItem(Container.DataItem) %>'>
                                        <td class="access">
                                            <asp:CheckBox ID="cbAccess" runat="server" Checked='<%# Eval("AccessWeb") %>' Enabled="false" />
                                        </td>
                                        <td class="accessweb">
                                            <%# cweb.GetDescriptionCulture(Container.DataItem) %>
                                        </td>
                                        <td class="ownerweb">
                                            <a href='mailto:<%# cweb.GetEmailOwner(Container.DataItem) %>'>
                                                <asp:Literal ID="lttxtemmail" runat="server" Text='<%# cweb.GetEmailOwner(Container.DataItem) %>'></asp:Literal></a>
                                        </td>
                                        <td class="daterequest"><%# Eval("DateRequest") %></td>
                                        <td class="dateapproval"><%# Eval("DateApproval") %></td>
                                        <td class="solution">
                                            <%# caccessusers.GetSolution(Container.DataItem,"IDAccessWebUsers") %>

                                        </td>
                                        <td class="coment"><%# Eval("Coment") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
        </div>
        <div class="DetaliRequests">

        </div>
        <div class="clear"></div>
    </div>
</asp:Content>

