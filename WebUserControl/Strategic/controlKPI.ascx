<%@ Control Language="C#" AutoEventWireup="true" CodeFile="controlKPI.ascx.cs" Inherits="controlKPI" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>

<style type="text/css">
table.tabKPIInsert, table.tabKPISelect, table.tabKPIUpdate {
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
.tabKPIInsert th, .tabKPISelect th, .tabKPIUpdate th {
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
.tabKPIInsert td, .tabKPIUpdate td, .tabKPISelect td, .tabKPIInsert td.Upr, .tabKPIUpdate td.Upr, .tabKPISelect td.Upr {
    padding: 5px;
    border: 1px solid #999999;
    color: #000000;
    text-align: left;
    background-color: #FFFFFF;
    width: 300px;
}
    caption {
    text-align: center;
    margin-top: 10px;
    margin-bottom: 10px;
    text-transform: uppercase;
    font-weight: bold;
}

.LineText {
    width: 85%;
}
.MultiText {
    width: 98%;
    height: 95%;
}
.failure {
    /*font-size: 1.2em;*/
    color: Red;
}
</style>
<asp:ObjectDataSource ID="odsTable" runat="server" SelectMethod="SelectKPI" TypeName="Strategic.classKPI" OnSelecting="odsTable_Selecting" OnInserted="odsTable_Inserted" OnInserting="odsTable_Inserting" OnUpdated="odsTable_Updated" OnUpdating="odsTable_Updating" OnDeleted="odsTable_Deleted" OnDeleting="odsTable_Deleting" OnSelected="odsTable_Selected" InsertMethod="InsertKPI" UpdateMethod="UpdateKPI" DeleteMethod="DeleteKPI">
    <DeleteParameters>
        <asp:Parameter Name="IDKPI" Type="Int32" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="Name" Type="String" />
        <asp:Parameter Name="NameEng" Type="String" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </InsertParameters>
    <SelectParameters>
        <asp:Parameter Name="IDKPI" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="IDKPI" Type="Int32" />
        <asp:Parameter Name="Name" Type="String" />
        <asp:Parameter Name="NameEng" Type="String" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </UpdateParameters>
</asp:ObjectDataSource>
<div class="panelTable">
    <asp:FormView ID="fvTable" runat="server" DataKeyNames="IDKPI" DataSourceID="odsTable" OnDataBound="fvTable_DataBound" OnModeChanging="fvTable_ModeChanging">
        <ItemTemplate>
            <table class="tabKPISelect">
                <caption><%# this.caption %></caption>
                <tr>
                    <td colspan="3" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsUpdDel" ConfirmDelete="MessageDeleteKPI" OnInit="pnSelect_Init" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltName" runat="server" Text="<%$ Resources:ResourceStrategic, tsNameKPI %>"></asp:Literal>:&nbsp;</th>
                    <td><b>На русском:</b><br />
                        <%# Eval("Name") %></td>
                    <td><b>In English:</b><br />
                        <%# Eval("NameEng") %></td>
                </tr>
            </table>
        </ItemTemplate>
        <EditItemTemplate>
            <table class="tabKPIUpdate">
                <caption><%# this.caption %></caption>
                <tr>
                    <td colspan="3" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="UpdCan" OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick" Change="true" ValidationGroup="KPI"  />
                   </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltName" runat="server" Text="<%$ Resources:ResourceStrategic, tsNameKPI %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:TextBox ID="tbName" runat="server" Text='<%# Eval("Name") %>' CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revName" runat="server" ControlToValidate="tbName" ValidationGroup="KPI"
                            ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,1024}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="false">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>" ValidationGroup="KPI"
                            ControlToValidate="tbName" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="tbNameEng" runat="server" Text='<%# Eval("NameEng") %>' CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revNameEng" runat="server" ControlToValidate="tbNameEng" ValidationGroup="KPI"
                            ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,1024}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescriptionEng %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvNameEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>" ValidationGroup="KPI"
                            ControlToValidate="tbNameEng" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </EditItemTemplate>
        <InsertItemTemplate>
            <table class="tabKPIInsert">
                <caption><%# this.caption %></caption>
                <tr>
                    <td colspan="3" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsCan" OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick" Change="true" ValidationGroup="KPI"  />
                   </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltName" runat="server" Text="<%$ Resources:ResourceStrategic, tsNameKPI %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:TextBox ID="tbName" runat="server" CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revName" runat="server" ControlToValidate="tbName" ValidationGroup="KPI"
                            ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,1024}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="false">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>" ValidationGroup="KPI"
                            ControlToValidate="tbName" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="tbNameEng" runat="server" CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revNameEng" runat="server" ControlToValidate="tbNameEng" ValidationGroup="KPI"
                            ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,1024}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescriptionEng %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvNameEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>" ValidationGroup="KPI"
                            ControlToValidate="tbNameEng" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </InsertItemTemplate>
    </asp:FormView>
</div>