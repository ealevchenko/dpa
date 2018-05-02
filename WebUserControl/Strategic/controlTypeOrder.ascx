<%@ Control Language="C#" AutoEventWireup="true" CodeFile="controlTypeOrder.ascx.cs" Inherits="controlTypeOrder" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>

<style type="text/css">
table.tabInsert, table.tabSelect, table.tabUpdate {
    border: 1px solid #999999;
    color: #000000;
    border-radius: 3px;
    background-color: #F7F6F3;
    font-family: 'Arial Narrow';
    width: 800px;
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
    width: 85%;
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
<asp:ObjectDataSource ID="odsTable" runat="server" SelectMethod="SelectTypeOrder" TypeName="Strategic.classOrder" OnSelecting="odsTable_Selecting" OnInserted="odsTable_Inserted" OnInserting="odsTable_Inserting" OnUpdated="odsTable_Updated" OnUpdating="odsTable_Updating" OnDeleted="odsTable_Deleted" OnDeleting="odsTable_Deleting" OnSelected="odsTable_Selected" InsertMethod="InsertTypeOrder" UpdateMethod="UpdateTypeOrder" DeleteMethod="DeleteTypeOrder">
    <DeleteParameters>
        <asp:Parameter Name="IDTypeOrder" Type="Int32" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="TypeOrder" Type="String" />
        <asp:Parameter Name="TypeOrderEng" Type="String" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </InsertParameters>
    <SelectParameters>
        <asp:Parameter Name="IDTypeOrder" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="IDTypeOrder" Type="Int32" />
        <asp:Parameter Name="TypeOrder" Type="String" />
        <asp:Parameter Name="TypeOrderEng" Type="String" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </UpdateParameters>
</asp:ObjectDataSource>
<div class="panelTable">
    <asp:FormView ID="fvTable" runat="server" DataKeyNames="IDTypeOrder" DataSourceID="odsTable" OnDataBound="fvTable_DataBound" OnModeChanging="fvTable_ModeChanging">
        <ItemTemplate>
            <table class="tabSelect">
                <tr>
                    <td colspan="3" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsUpdDel" ConfirmDelete="MessageDeleteTypeOrder" OnInit="pnSelect_Init" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeOrder" runat="server" Text="<%$ Resources:ResourceStrategic, tsTypeOrder %>"></asp:Literal>:&nbsp;</th>
                    <td><b>На русском:</b><br />
                        <%# Eval("TypeOrder") %></td>
                    <td><b>In English:</b><br />
                        <%# Eval("TypeOrderEng") %></td>
                </tr>
            </table>
        </ItemTemplate>
        <EditItemTemplate>
            <table class="tabUpdate">
                <tr>
                    <td colspan="3" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="UpdCan" OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick" Change="true" ValidationGroup="TypeOrder"  />
                   </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeOrder" runat="server" Text="<%$ Resources:ResourceStrategic, tsTypeOrder %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:TextBox ID="tbTypeOrder" runat="server" Text='<%# Eval("TypeOrder") %>' CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revTypeOrder" runat="server" ControlToValidate="tbTypeOrder" ValidationGroup="TypeOrder"
                            ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,512}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="false">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvTypeOrder" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbTypeOrder" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="tbTypeOrderEng" runat="server" Text='<%# Eval("TypeOrderEng") %>' CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revTypeOrderEng" runat="server" ControlToValidate="tbTypeOrderEng" ValidationGroup="TypeOrder"
                            ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,512}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescriptionEng %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvTypeOrderEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbTypeOrderEng" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </EditItemTemplate>
        <InsertItemTemplate>
            <table class="tabInsert">
                <tr>
                    <td colspan="3" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsCan"  OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick" Change="true" ValidationGroup="TypeOrder"  />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeOrder" runat="server" Text="<%$ Resources:ResourceStrategic, tsTypeOrder %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:TextBox ID="tbTypeOrder" runat="server" CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revTypeOrder" runat="server" ControlToValidate="tbTypeOrder" ValidationGroup="TypeOrder"
                            ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,512}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="false">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvTypeOrder" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbTypeOrder" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="tbTypeOrderEng" runat="server" CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revTypeOrderEng" runat="server" ControlToValidate="tbTypeOrderEng" ValidationGroup="TypeOrder"
                            ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,512}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescriptionEng %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvTypeOrderEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbTypeOrderEng" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </InsertItemTemplate>
    </asp:FormView>
</div>