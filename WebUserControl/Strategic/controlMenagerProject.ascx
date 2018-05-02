<%@ Control Language="C#" AutoEventWireup="true" CodeFile="controlMenagerProject.ascx.cs" Inherits="controlMenagerProject" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>

<style type="text/css">
table.tabInsert, table.tabSelect, table.tabUpdate {
    border: 1px solid #999999;
    color: #000000;
    border-radius: 3px;
    background-color: #F7F6F3;
    font-family: 'Arial Narrow';
    width: 500px;
    margin-top: 20px;
    font-size: 14px;
    margin-bottom: 20px;
}
.tabInsert th, .tabSelect th, .tabUpdate th {
    padding: 5px 10px 5px 5px;
    border: 1px solid #999999;
    color: #5B5B5B;
    font-weight: bold;
    word-wrap: hyphenate;
    text-align: right;
    text-transform: uppercase;
    background-color: #F7F6F3;
    width: 200px;
}
.tabInsert td, .tabUpdate td, .tabSelect td{
    padding: 5px;
    border: 1px solid #999999;
    color: #000000;
    text-align: left;
    background-color: #FFFFFF;
    width: 300px;
}
.LineText {
    width: 98%;
}
.MultiText {
    width: 98%;
    height: 50px;
}
.failure {
    /*font-size: 1.2em;*/
    color: Red;
}
</style>
<asp:ObjectDataSource ID="odsTable" runat="server" SelectMethod="SelectMenagerProject" TypeName="Strategic.classMenagerProject" OnSelecting="odsTable_Selecting" OnInserted="odsTable_Inserted" OnInserting="odsTable_Inserting" OnUpdated="odsTable_Updated" OnUpdating="odsTable_Updating" OnDeleted="odsTable_Deleted" OnDeleting="odsTable_Deleting" OnSelected="odsTable_Selected" UpdateMethod="UpdateMenagerProject" DeleteMethod="DeleteMenagerProject" InsertMethod="InsertMenagerProject">
    <DeleteParameters>
        <asp:Parameter Name="IDMenagerProject" Type="Int32" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IDUser" Type="Int32" />
        <asp:Parameter Name="WPhone" Type="Int32" />
        <asp:Parameter Name="MPhone" Type="Int64" />
        <asp:Parameter Name="SuperMenager" Type="Boolean" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </InsertParameters>
    <SelectParameters>
        <asp:Parameter Name="IDMenagerProject" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="IDMenagerProject" Type="Int32" />
        <asp:Parameter Name="IDUser" Type="Int32" />
        <asp:Parameter Name="WPhone" Type="Int32" />
        <asp:Parameter Name="MPhone" Type="Int64" />
        <asp:Parameter Name="SuperMenager" Type="Boolean" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </UpdateParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsListUserEnterprise" runat="server" SelectMethod="SelectUsers" TypeName="WebBase.classUsers" OnSelecting="odsListUserEnterprise_Selecting">
    <SelectParameters>
        <asp:Parameter Name="type" Type="Int32"/>
        <asp:Parameter Name="IDWeb" Type="Int32"  />
        <asp:Parameter Name="IDSection" Type="Int32"/>
        <asp:Parameter Name="UserEnterprise" Type="String"  />
        <asp:Parameter Name="Description" Type="String"  />
    </SelectParameters>
</asp:ObjectDataSource>
<div class="panelTable">
    <asp:FormView ID="fvTable" runat="server" DataKeyNames="IDMenagerProject" DataSourceID="odsTable" OnDataBound="fvTable_DataBound" OnModeChanging="fvTable_ModeChanging">
        <ItemTemplate>
            <table class="tabSelect">
                <tr>
                    <td colspan="2" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsUpdDel" ConfirmDelete="MessageDeleteTypeProject" OnInit="pnSelect_Init" />
                    </td>
                </tr>
                <tr>
                    <th><asp:Literal ID="ltUserEnterprise" runat="server" Text="<%$ Resources:ResourceStrategic, nfUserEnterprise %>"></asp:Literal>:&nbsp;</th>
                    <td><%# base.cmenager.GetUserEnterprise(Container.DataItem) %></td>
                </tr>
                <tr>
                    <th><asp:Literal ID="lbFIO" runat="server" Text="<%$ Resources:ResourceStrategic, nfFIO %>"></asp:Literal>:&nbsp;</th>
                    <td><%# base.cmenager.GetFIO(Container.DataItem) %></td>
                </tr>
                <tr>
                    <th><asp:Literal ID="lbEmail" runat="server" Text="<%$ Resources:ResourceStrategic, nfEmail %>"></asp:Literal>:&nbsp;</th>
                    <td><a href='mailto:<%# base.cmenager.GetEmail(Container.DataItem) %>'><asp:Literal ID="Literal4" runat="server" Text='<%# base.cmenager.GetEmail(Container.DataItem) %>'></asp:Literal></a></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="lbWPhone" runat="server" Text="<%$ Resources:ResourceStrategic, nfWPhone %>"></asp:Literal>:&nbsp;</th>
                    <td><%# Eval("WPhone","{0:#####-##-##}")%></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="lbMPhone" runat="server" Text="<%$ Resources:ResourceStrategic, nfMPhone %>"></asp:Literal>:&nbsp;</th>
                    <td><%# Eval("MPhone","{0:8(0##)##-##-###}")%></td>
                </tr>
                <tr>
                    <th><asp:Literal ID="lbSuperMenager" runat="server" Text="<%$ Resources:ResourceStrategic, nfSuperMenager %>"></asp:Literal>:&nbsp;</th>
                    <td><asp:Image ID="imSuperMenager" runat="server" ImageUrl='<%# base.GetSRCCheckBox(Container.DataItem,"SuperMenager") %>' Height="32px" Width="32px" /></td>
                </tr>
            </table>
        </ItemTemplate>
        <EditItemTemplate>
            <table class="tabUpdate">
                <tr>
                    <td colspan="2" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="UpdCan" OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick" Change="true"  />
                   </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltUserEnterprise" runat="server" Text="<%$ Resources:ResourceStrategic, nfUserEnterprise %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:DropDownList ID="ddlUserEnterprise" runat="server" AutoPostBack="True" DataSourceID="odsListUserEnterprise" DataTextField="UserEnterprise" DataValueField="IDUser" OnDataBound="ddlUserEnterprise_DataBound" CssClass="LineText"></asp:DropDownList></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="lbWPhone" runat="server" Text="<%$ Resources:ResourceStrategic, nfWPhone %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:TextBox ID="tbWPhone" runat="server" Text='<%# Bind("WPhone") %>' TextMode="Phone" CssClass="LineText"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revWPhone" runat="server" ControlToValidate="tbWPhone"
                            ValidationExpression="[\d]{5,7}$" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_WPhone %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="lbMPhone" runat="server" Text="<%$ Resources:ResourceStrategic, nfMPhone %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:TextBox ID="tbMPhone" runat="server" Text='<%# Bind("MPhone") %>' TextMode="Phone" CssClass="LineText"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revMPhone" runat="server" ControlToValidate="tbMPhone"
                            ValidationExpression="^((8)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_MPhone %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="lbSuperMenager" runat="server" Text="<%$ Resources:ResourceStrategic, nfSuperMenager %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:CheckBox ID="cbSuperMenager" runat="server" Checked='<%# Eval("SuperMenager")%>' />
                    </td>
                </tr>
            </table>
        </EditItemTemplate>
        <InsertItemTemplate>
            <table class="tabInsert">
                <tr>
                    <td colspan="2" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsCan"  OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick" Change="true"  />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltUserEnterprise" runat="server" Text="<%$ Resources:ResourceStrategic, nfUserEnterprise %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:DropDownList ID="ddlUserEnterprise" runat="server" AutoPostBack="True" DataSourceID="odsListUserEnterprise" DataTextField="UserEnterprise" DataValueField="IDUser" OnDataBound="ddlUserEnterprise_DataBound" CssClass="LineText"></asp:DropDownList></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="lbWPhone" runat="server" Text="<%$ Resources:ResourceStrategic, nfWPhone %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:TextBox ID="tbWPhone" runat="server" CssClass="LineText"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revWPhone" runat="server" ControlToValidate="tbWPhone"
                            ValidationExpression="[\d]{5,7}$" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_WPhone %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="lbMPhone" runat="server" Text="<%$ Resources:ResourceStrategic, nfMPhone %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:TextBox ID="tbMPhone" runat="server" CssClass="LineText"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revMPhone" runat="server" ControlToValidate="tbMPhone"
                            ValidationExpression="^((8)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_MPhone %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="lbSuperMenager" runat="server" Text="<%$ Resources:ResourceStrategic, nfSuperMenager %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:CheckBox ID="cbSuperMenager" runat="server" Checked="false" />
                    </td>
                </tr>
            </table>
        </InsertItemTemplate>
    </asp:FormView>
</div>