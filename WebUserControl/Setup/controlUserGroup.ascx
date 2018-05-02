<%@ Control Language="C#" AutoEventWireup="true" CodeFile="controlUserGroup.ascx.cs" Inherits="controlUserGroup" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>
<style type="text/css">
    table.tabAddUser, table.tabSelectUser, table.tabEditUser {
    border: 1px solid #999999;
    color: #000000;
    border-radius: 3px;
    background-color: #F7F6F3;
    font-family: 'Arial Narrow';
    width: auto;
    margin-top: 20px;
    font-size: 14px;
    margin-bottom: 20px;
}
.tabAddUser th, .tabSelectUser th, .tabEditUser th {
    padding: 5px 10px 5px 5px;
    border: 1px solid #999999;
    color: #5B5B5B;
    font-weight: bold;
    word-wrap: hyphenate;
    text-align: right;
    text-transform: uppercase;
    background-color: #F7F6F3;
    width: 150px;
}
.tabAddUser td, .tabEditUser td {
    padding: 5px;
    border: 1px solid #999999;
    color: #000000;
    text-align: center;
    background-color: #FFFFFF;
    width: 300px;
}

.tabSelectUser td {
    padding: 5px;
    border: 1px solid #999999;
    color: #000000;
    text-align: left;
    background-color: #FFFFFF;
    width: 300px;
}

    .tabAddUser td.Upr, .tabSelectUser td.Upr, .tabEditUser td.Upr {
    text-align: left;
}


.failure {
    /*font-size: 1.2em;*/
    color: Red;
}
.LineText {
    width: 98%;
}
.MultiText {
    width: 98%;
    height: 50px;
}
</style>
<asp:ObjectDataSource ID="odsListWeb" runat="server" SelectMethod="SelectWeb" TypeName="WebBase.classWeb"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsListSection" runat="server" SelectMethod="DDLListSectionCulture" TypeName="WebBase.classSection">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="Full" Type="Boolean" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsUserGroup" runat="server" OnSelected="odsUserGroup_Selected" OnSelecting="odsUserGroup_Selecting" SelectMethod="SelectUsers" TypeName="WebBase.classUsers" InsertMethod="InsertUser" UpdateMethod="UpdateUser" OnDeleted="odsUserGroup_Deleted" OnDeleting="odsUserGroup_Deleting" OnInserted="odsUserGroup_Inserted" OnInserting="odsUserGroup_Inserting" OnUpdated="odsUserGroup_Updated" OnUpdating="odsUserGroup_Updating">
    <InsertParameters>
        <asp:Parameter Name="IDWeb" Type="Int32" />
        <asp:Parameter Name="UserEnterprise" Type="String" />
        <asp:Parameter Name="Description" Type="String" DefaultValue="dkjdkjkdjksjd" />
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
        <asp:Parameter Name="IDUser" Type="Int32" />
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
        <asp:Parameter Name="OutResult" Type="Boolean" DefaultValue="true" />
    </UpdateParameters>

</asp:ObjectDataSource>

<asp:FormView ID="fvUserGroup" runat="server" DataSourceID="odsUserGroup" OnModeChanging="fvUserGroup_ModeChanging" OnDataBound="fvUserGroup_DataBound" >
    <ItemTemplate>
        <table class="tabSelectUser">
            <tr>
                <td colspan="2" class="Upr" >
                    <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsUpdDel" ConfirmDelete="MessageDeleteUser" OnInit="pnSelect_Init" />
                </td>
            </tr>
            <tr style="display: <%# GetDisplayGroup(Container.DataItem) %>">
                <th>
                    <asp:Literal ID="ltWeb" runat="server" Text="<%$ Resources:ResourceBase, tsWeb %>"></asp:Literal>:&nbsp;</th>
                <td><%# base.cw.GetWeb(Container.DataItem) %></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltAccount" runat="server" Text='<%# GetTypeAccount(Container.DataItem) %>'></asp:Literal>:&nbsp;</th>
                <td>
                    <%# Eval("UserEnterprise") %>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltDescription" runat="server" Text="<%$ Resources:ResourceBase, tsDescription %>"></asp:Literal>:&nbsp;</th>
                <td><%# Eval("Description") %></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltSection" runat="server" Text="<%$ Resources:ResourceBase, tsSection %>"></asp:Literal>:&nbsp;</th>
                <td><%# base.csection.GetSection(Container.DataItem, false) %></td>
            </tr>
            <tr style="display: <%# GetDisplayUser(Container.DataItem) %>">
                <th>
                    <asp:Literal ID="ltPost" runat="server" Text="<%$ Resources:ResourceBase, tsPost %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <%# Eval("Post") %></td>
            </tr>
            <tr style="display: <%# GetDisplayUser(Container.DataItem) %>">
                <th>
                    <asp:Literal ID="ltSurname" runat="server" Text="<%$ Resources:ResourceBase, tsSurname %>"></asp:Literal>:&nbsp;</th>
                <td><%# Eval("Surname") %></td>
            </tr>
            <tr style="display: <%# GetDisplayUser(Container.DataItem) %>">
                <th>
                    <asp:Literal ID="ltName" runat="server" Text="<%$ Resources:ResourceBase, tsName %>"></asp:Literal>:&nbsp;</th>
                <td><%# Eval("Name") %></td>
            </tr>
            <tr style="display: <%# GetDisplayUser(Container.DataItem) %>">
                <th>
                    <asp:Literal ID="ltPatronymic" runat="server" Text="<%$ Resources:ResourceBase, tsPatronymic %>"></asp:Literal>:&nbsp;</th>
                <td><%# Eval("Patronymic") %></td>
            </tr>
            <tr style="display: <%# GetDisplayUser(Container.DataItem) %>">
                <th>
                    <asp:Literal ID="lbEmail" runat="server" Text="<%$ Resources:ResourceBase, tsEmail %>"></asp:Literal>:&nbsp;</th>
                <td><%# Eval("Email") %></td>
            </tr>
            <tr style="display: <%# GetDisplayUser(Container.DataItem) %>">
                <th>
                    <asp:Literal ID="ltDistribution" runat="server" Text="<%$ Resources:ResourceBase, tsDistribution %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:Image ID="imbDistribution" runat="server" ImageUrl='<%# GetSRCCheckBox(Container.DataItem,"bDistribution") %>' Height="32px" Width="32px" />
            </tr>
        </table>
    </ItemTemplate>
    <EditItemTemplate>
        <table class="tabEditUser">
            <tr>
                <td colspan="2" class="Upr">
                    <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="UpdCan" OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick"  Change="true"  />
                 </td>
            </tr>
            <%----%>
            <tr style="display: <%# GetDisplayGroup(Container.DataItem) %>">
                <th>
                    <asp:Literal ID="ltWeb" runat="server" Text="<%$ Resources:ResourceBase, tsWeb %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:DropDownList ID="ddlWeb" runat="server" DataSourceID="odsListWeb" CssClass="LineText" DataTextField="Web" DataValueField="IDWeb" ></asp:DropDownList></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltAccount" runat="server" Text="<%$ Resources:ResourceBase, tsAccount %>" ></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:TextBox ID="tbAccount" runat="server" Text='<%# Eval("UserEnterprise") %>' CssClass="LineText" ></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revUser" runat="server" ControlToValidate="tbAccount" OnLoad="revInsUser_Load"
                        ValidationExpression=".*\\.*" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_User %>"
                        Display="dynamic" CssClass="failure" >
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvUser" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                        ControlToValidate="tbAccount" CssClass="failure"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltDescription" runat="server" Text="<%$ Resources:ResourceBase, tsDescription %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:TextBox ID="tbDescription" runat="server" Text='<%# Eval("Description") %>' CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltSection" runat="server" Text="<%$ Resources:ResourceBase, tsSection %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:DropDownList ID="ddlSection" runat="server" CssClass="LineText" DataSourceID="odsListSection" DataTextField="Section" DataValueField="IDSection" ></asp:DropDownList></td>
            </tr>
            <tr style="display: <%# GetDisplayUser(Container.DataItem) %>">
                <th>
                    <asp:Literal ID="ltPost" runat="server" Text="<%$ Resources:ResourceBase, tsPost %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:TextBox ID="tbPost" runat="server" Text='<%# Eval("Post") %>' CssClass="MultiText" CausesValidation="False" TextMode="MultiLine" ></asp:TextBox></td>
            </tr>
            <tr style="display: <%# GetDisplayUser(Container.DataItem) %>">
                <th>
                    <asp:Literal ID="ltSurname" runat="server" Text="<%$ Resources:ResourceBase, tsSurname %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:TextBox ID="tbSurname" runat="server" Text='<%# Eval("Surname") %>' CssClass="LineText" ></asp:TextBox></td>
            </tr>
            <tr style="display: <%# GetDisplayUser(Container.DataItem) %>">
                <th>
                    <asp:Literal ID="ltName" runat="server" Text="<%$ Resources:ResourceBase, tsName %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:TextBox ID="tbName" runat="server" Text='<%# Eval("Name") %>' CssClass="LineText" ></asp:TextBox></td>
            </tr>
            <tr style="display: <%# GetDisplayUser(Container.DataItem) %>">
                <th>
                    <asp:Literal ID="ltPatronymic" runat="server" Text="<%$ Resources:ResourceBase, tsPatronymic %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:TextBox ID="tbPatronymic" runat="server" Text='<%# Eval("Patronymic") %>' CssClass="LineText" ></asp:TextBox></td>
            </tr>
            <tr style="display: <%# GetDisplayUser(Container.DataItem) %>">
                <th>
                    <asp:Literal ID="lbEmail" runat="server" Text="<%$ Resources:ResourceBase, tsEmail %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:TextBox ID="tbEmail" runat="server" Text='<%# Eval("Email") %>' CssClass="LineText" ></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revEMail" runat="server" ControlToValidate="tbEmail" OnLoad="revInsEMail_Load"
                        ValidationExpression=".*@.{2,}\..{2,}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_Email %>"
                        Display="dynamic" CssClass="failure" >
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr style="display: <%# GetDisplayUser(Container.DataItem) %>">
                <th>
                    <asp:Literal ID="ltDistribution" runat="server" Text="<%$ Resources:ResourceBase, tsDistribution %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:CheckBox ID="cbDistribution" runat="server" /></td>
            </tr>
        </table>
    </EditItemTemplate>
    <InsertItemTemplate>
        <table class="tabAddUser">
            <tr>
                <td colspan="2" class="Upr">
                    <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsCan"  OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick"  Change="true" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltType" runat="server" Text="<%$ Resources:ResourceBase, tsType %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:RadioButtonList ID="rblist" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnLoad="rblist_Load" >
                        <asp:ListItem Value="False" Text="<%$ Resources:ResourceBase, tsAccount %>"></asp:ListItem>
                        <asp:ListItem Value="True" Text="<%$ Resources:ResourceBase, tsGroup %>"></asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltWeb" runat="server" Text="<%$ Resources:ResourceBase, tsWeb %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:DropDownList ID="ddlWeb" runat="server" Enabled="false" DataSourceID="odsListWeb" CssClass="LineText" DataTextField="Web" DataValueField="IDWeb" OnLoad="ddlWeb_Load"></asp:DropDownList></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltAccount" runat="server" Text="<%$ Resources:ResourceBase, tsAccount %>" OnLoad="ltAccount_Load"></asp:Literal>:&nbsp;</th>
                <td>

                    <asp:TextBox ID="tbAccount" runat="server" Text='<%# Eval("UserEnterprise") %>' CssClass="LineText"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revInsUser" runat="server" ControlToValidate="tbAccount" OnLoad="revInsUser_Load" 
                        ValidationExpression=".*\\.*" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_User %>"
                        Display="dynamic" CssClass="failure" >
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvUser" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                        ControlToValidate="tbAccount" CssClass="failure"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltDescription" runat="server" Text="<%$ Resources:ResourceBase, tsDescription %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:TextBox ID="tbDescription" runat="server" Text='<%# Eval("Description") %>' CssClass="MultiText" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltSection" runat="server" Text="<%$ Resources:ResourceBase, tsSection %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:DropDownList ID="ddlSection" runat="server" CssClass="LineText" DataSourceID="odsListSection" DataTextField="Section" DataValueField="IDSection"></asp:DropDownList></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltPost" runat="server" Text="<%$ Resources:ResourceBase, tsPost %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:TextBox ID="tbPost" runat="server" Text='<%# Eval("Post") %>' CssClass="MultiText" CausesValidation="False" TextMode="MultiLine" OnLoad="tbPost_Load"></asp:TextBox></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltSurname" runat="server" Text="<%$ Resources:ResourceBase, tsSurname %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:TextBox ID="tbSurname" runat="server" Text='<%# Eval("Surname") %>' CssClass="LineText" OnLoad="tbSurname_Load"></asp:TextBox></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltName" runat="server" Text="<%$ Resources:ResourceBase, tsName %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:TextBox ID="tbName" runat="server" Text='<%# Eval("Name") %>' CssClass="LineText" OnLoad="tbName_Load"></asp:TextBox></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltPatronymic" runat="server" Text="<%$ Resources:ResourceBase, tsPatronymic %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:TextBox ID="tbPatronymic" runat="server" Text='<%# Eval("Patronymic") %>' CssClass="LineText" OnLoad="tbPatronymic_Load"></asp:TextBox></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="lbEmail" runat="server" Text="<%$ Resources:ResourceBase, tsEmail %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:TextBox ID="tbEmail" runat="server" Text='<%# Eval("Email") %>' CssClass="LineText" OnLoad="tbEmail_Load"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revInsEMail" runat="server" ControlToValidate="tbEmail" OnLoad="revInsEMail_Load"
                        ValidationExpression=".*@.{2,}\..{2,}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_Email %>"
                        Display="dynamic" CssClass="failure" >
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltDistribution" runat="server" Text="<%$ Resources:ResourceBase, tsDistribution %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:CheckBox ID="cbDistribution" runat="server" OnLoad="cbDistribution_Load"/></td>
            </tr>
        </table>
    </InsertItemTemplate>
</asp:FormView>
