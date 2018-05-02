<%@ Page Title="" Language="C#" MasterPageFile="~/WebSite/Setup/Setup.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="WebSite_Setup_Users" MaintainScrollPositionOnPostback="true" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>
<%@ Register TagPrefix="wuc" TagName="controlUserGroup" Src="~/WebUserControl/Setup/controlUserGroup.ascx" %>
<%@ Register TagPrefix="wuc" TagName="controlUserToGroup" Src="~/WebUserControl/Setup/controlUserToGroup.ascx" %>
<%@ Register TagPrefix="wuc" TagName="controlInsDelUserToGroup" Src="~/WebUserControl/Setup/controlInsDelUserToGroup.ascx" %>
<%@ Register TagPrefix="wuc" TagName="controlUserGroupAccess" Src="~/WebUserControl/Setup/controlUserGroupAccess.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/users.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- -->
    <asp:ObjectDataSource ID="odsListUsersSelect" runat="server" SelectMethod="SelectUsers" TypeName="WebBase.classUsers" OnSelecting="odsListUsersSelect_Selecting" DeleteMethod="DeleteUser" InsertMethod="InsertUser" UpdateMethod="UpdateUser">
        <DeleteParameters>
            <asp:Parameter Name="IDUser" Type="Int32" />
            <asp:Parameter Name="OutResult" Type="Boolean" DefaultValue="true" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="IDWeb" Type="Int32" />
            <asp:Parameter Name="UserEnterprise" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="bDistribution" Type="Boolean" />
            <asp:Parameter Name="Surname" Type="String" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Patronymic" Type="String" />
            <asp:Parameter Name="Post" Type="String" />
            <asp:Parameter Name="IDSection" Type="Int32" />
            <asp:Parameter Name="OutResult" Type="Boolean" DefaultValue="true" />
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="type" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="IDWeb" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="IDSection" Type="Int32" />
            <asp:Parameter DefaultValue="" Name="UserEnterprise" Type="String" />
            <asp:Parameter DefaultValue="" Name="Description" Type="String" />

        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="IDUser" Type="Int32" />
            <asp:Parameter Name="IDWeb" Type="Int32" />
            <asp:Parameter Name="UserEnterprise" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="bDistribution" Type="Boolean" />
            <asp:Parameter Name="Surname" Type="String" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Patronymic" Type="String" />
            <asp:Parameter Name="Post" Type="String" />
            <asp:Parameter Name="IDSection" Type="Int32" />
            <asp:Parameter Name="OutResult" Type="Boolean" DefaultValue="true"  />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <!-- -->
    <div class="Page">
        <wuc:panelUpr ID="pnAddUser" runat="server" StylePanel="InsUpdDel" VisibleDelete="false" VisibleUpdate="false" OnInsertClick="pnAddUser_InsertClick" /> 
    </div>
    <div class="listUsers">
        <wuc:controlUserGroup ID="InsertUserGroup" runat="server" OnDataInserted="InsertUserGroup_DataInserted" OnDataCancelClick="InsertUserGroup_DataCancelClick" Change="true" />
        <asp:ListView ID="lvUsers" runat="server" DataSourceID="odsListUsersSelect" DataKeyNames="IDUser" OnItemDataBound="lvUsers_ItemDataBound">
            <LayoutTemplate>
                <table class="tabUsers">
                    <tr>
                        <th class="icon">
                            <asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/WebSite/Setup/image/SearchUser.png" Height="50px" Width="50px" EnableTheming="True" CausesValidation="false" />
                        </th>
                        <th class="UserEnterprise">
                            <asp:LinkButton ID="lbUserEnterprise" runat="server" CommandName="sort" CommandArgument="UserEnterprise" Text="<%$ Resources:ResourceBase, nfUserEnterprise %>" CausesValidation="false" /></th>
                        <th class="Web">
                            <asp:LinkButton ID="lbWeb" runat="server" CommandName="sort" CommandArgument="IDWeb" Text="<%$ Resources:ResourceBase, nfWeb %>" CausesValidation="false"  /></th>
                        <th class="Description">
                            <asp:LinkButton ID="lbDescription" runat="server" CommandName="sort" CommandArgument="Description" Text="<%$ Resources:ResourceBase, nfDescription %>" CausesValidation="false" /></th>
                        <th class="Email">
                            <asp:LinkButton ID="lbEmail" runat="server" CommandName="sort" CommandArgument="Email" Text="<%$ Resources:ResourceBase, nfEmail %>" CausesValidation="false" /></th>
                        <th class="Distribution">
                            <asp:LinkButton ID="lbnfDistribution" runat="server" CommandName="sort" CommandArgument="bDistribution" Text="<%$ Resources:ResourceBase, nfDistribution %>" CausesValidation="false"  /></th>
                        <th class="Surname">
                            <asp:LinkButton ID="lbSurname" runat="server" CommandName="sort" CommandArgument="Surname" Text="<%$ Resources:ResourceBase, nfSurname %>" CausesValidation="false" /></th>
                        <th class="Name">
                            <asp:LinkButton ID="lbName" runat="server" CommandName="sort" CommandArgument="Name" Text="<%$ Resources:ResourceBase, nfName %>" CausesValidation="false" /></th>
                        <th class="Patronymic">
                            <asp:LinkButton ID="lbPatronymic" runat="server" CommandName="sort" CommandArgument="Patronymic" Text="<%$ Resources:ResourceBase, nfPatronymic %>" CausesValidation="false"  /></th>
                        <th class="Post">
                            <asp:LinkButton ID="lbPost" runat="server" CommandName="sort" CommandArgument="Post" Text="<%$ Resources:ResourceBase, nfPost %>" CausesValidation="false"  /></th>
                        <th class="Section">
                            <asp:LinkButton ID="lbSection" runat="server" CommandName="sort" CommandArgument="IDSection" Text="<%$ Resources:ResourceBase, nfSection %>" CausesValidation="false" /></th>
                    </tr>
                    <tr id="itemPlaceholder" runat="server" />
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr class="item">
                    <td class="upr">
                        <asp:Image ID="imFolder" runat="server" ImageUrl='<%# GetSRCFolder(Container.DataItem) %>' Height="32" Width="32" /></td>
                    <td class="UserEnterprise"><b>
                        <asp:LinkButton ID="lbUserEnterprise" runat="server" Text='<%# Eval("UserEnterprise") %>' CommandName="Select" CausesValidation="false" OnClick="lbUserEnterprise_Click" ></asp:LinkButton></b></td>
                    <td class="Web"><%# base.cweb.GetWeb(Container.DataItem) %></td>
                    <td class="Description"><%# Eval("Description") %></td>
                    <td class="Email"><a href='mailto:<%# Eval("Email") %>'>
                        <asp:Literal ID="Literal4" runat="server" Text='<%# Eval("Email") %>'></asp:Literal></a></td>
                    <td class="Distribution">
                        <asp:Image ID="imDistribution" runat="server" ImageUrl='<%# GetSRCCheckBox(Container.DataItem,"bDistribution") %>' Height="32" Width="32" /></td>
                    <td class="Surname"><%# Eval("Surname") %></td>
                    <td class="Name"><%# Eval("Name") %></td>
                    <td class="Patronymic"><%# Eval("Patronymic") %></td>
                    <td class="Post"><%# Eval("Post") %></td>
                    <td class="Section"><%# base.csection.GetSection(Container.DataItem, false) %></td>
                </tr>
            </ItemTemplate>
            <SelectedItemTemplate>
                <tr class="select">
                    <td class="upr" rowspan="2">
                        <asp:ImageButton ID="ibClose" runat="server" ImageUrl="~/WebSite/Setup/image/Close2.png" Height="50px" Width="50px" CausesValidation="false" OnClick="ibClose_Click"/>
                        <asp:ImageButton ID="ibEditUser" runat="server" ImageUrl="~/WebSite/Setup/image/Edit2.png" CommandName="Edit" Height="50px" Width="50px" CausesValidation="false" Visible='<%# base.Change %>' />
                        <asp:ImageButton ID="ibDelUser" runat="server" ImageUrl="~/WebSite/Setup/image/Delete2.png" Height="50px" Width="50px" CommandName="Delete" Visible='<%# base.Change %>'  OnClientClick="<%$ Resources:ResourceBase, MessageDeleteUser %>" />
                    </td>
                    <td class="UserEnterprise"><b>
                        <asp:LinkButton ID="lbUserEnterprise" runat="server" Text='<%# Eval("UserEnterprise") %>' CommandName="Select" CausesValidation="false" ></asp:LinkButton></b></td>
                    <td class="Web"><%# base.cweb.GetWeb(Container.DataItem) %></td>
                    <td class="Description"><%# Eval("Description") %></td>
                    <td class="Email"><a href='mailto:<%# Eval("Email") %>'>
                        <asp:Literal ID="Literal4" runat="server" Text='<%# Eval("Email") %>'></asp:Literal></a></td>
                    <td class="Distribution">
                        <asp:Image ID="imDistribution" runat="server" ImageUrl='<%# GetSRCCheckBox(Container.DataItem,"bDistribution") %>' Height="32" Width="32" /></td>
                    <td class="Surname"><%# Eval("Surname") %></td>
                    <td class="Name"><%# Eval("Name") %></td>
                    <td class="Patronymic"><%# Eval("Patronymic") %></td>
                    <td class="Post"><%# Eval("Post") %></td>
                    <td class="Section"><%# base.csection.GetSection(Container.DataItem, false) %></td>
                </tr>
                <tr>
                    <td colspan="10" class="detali">
                        <div class="detaliUpr">
                            <asp:ImageButton ID="ibGroup" runat="server" ImageUrl="~/WebSite/Setup/image/Folder.png" Height="32px" Width="32px" OnClientClick="<%$ Resources:Resource, MessageLock %>" OnClick="ibGroup_Click" />
                            <asp:ImageButton ID="ibAddGroup" runat="server" ImageUrl="~/WebSite/Setup/image/addGroup.png" Height="32px" Width="32px" OnClientClick="<%$ Resources:Resource, MessageLock %>" OnClick="ibAddGroup_Click" />
                            <asp:ImageButton ID="ibDelGroup" runat="server" ImageUrl="~/WebSite/Setup/image/delGroup.png" Height="32px" Width="32px" OnClientClick="<%$ Resources:Resource, MessageLock %>" OnClick="ibDelGroup_Click" />
                            <asp:ImageButton ID="ibSiteMap" runat="server" ImageUrl="~/WebSite/Setup/image/menu.png" Height="32px" Width="32px" OnClientClick="<%$ Resources:Resource, MessageLock %>" OnClick="ibSiteMap_Click"/>
                        </div>
                         <asp:MultiView ID="mvDetali" runat="server">
                             <asp:View ID="vGroup" runat="server">
                                 <asp:PlaceHolder ID="phGroup" runat="server" OnInit="phGroup_Init">

                                 </asp:PlaceHolder>
                             </asp:View>
                             <asp:View ID="vAddDelGroup" runat="server">
                                 <asp:PlaceHolder ID="phAddDelGroup" runat="server" OnInit="phAddDelGroup_Init">

                                 </asp:PlaceHolder>
                             </asp:View>

                             <asp:View ID="vSiteMap" runat="server">
                                 <asp:PlaceHolder ID="phSiteMap" runat="server" OnInit="phSiteMap_Init">

                                 </asp:PlaceHolder>
                             </asp:View>
                         </asp:MultiView>
                    </td>
                </tr>
            </SelectedItemTemplate>
            <EditItemTemplate>
                <tr class="edit">
                    <td class="upr">
                        <asp:ImageButton ID="ibCloseEdit" runat="server" ImageUrl="~/WebSite/Setup/image/Close2.png" Height="50px" Width="50px" CausesValidation="false" OnClick="ibCloseEdit_Click" />
                    </td>
                    <td colspan="10">
                        <asp:PlaceHolder ID="phUser" runat="server" OnInit="phUser_Init">

                        </asp:PlaceHolder>
                    </td>
                </tr>
            </EditItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>

