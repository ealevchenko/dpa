<%@ Control Language="C#" AutoEventWireup="true" CodeFile="controlOrder.ascx.cs" Inherits="controlOrder" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>
<%@ Register TagPrefix="wuc" TagName="panelUnloadFile" Src="~/WebUserControl/panelFile.ascx" %>
<%@ Register TagPrefix="wuc" TagName="controlTypeOrder" Src="~/WebUserControl/Strategic/controlTypeOrder.ascx" %>

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
<link rel="stylesheet" href="../../css/date/daterangepicker.css" type="text/css" />
<script src="../../css/date/jquery-1.11.2.min.js" type="text/javascript"></script>
<script src="../../css/date/moment.min.js" type="text/javascript"></script>
<script src="../../css/date/jquery.daterangepicker.js" type="text/javascript"></script>
<%--<link rel="stylesheet" href="<%# base.csitemap.GetPathUrl("/css/date/daterangepicker.css") %>" type="text/css" />
<script src="<%# base.csitemap.GetPathUrl("/css/date/jquery-1.11.2.min.js") %>" type="text/javascript"></script>
<script src="<%# base.csitemap.GetPathUrl("/css/date/moment.min.js") %>" type="text/javascript"></script>
<script src="<%# base.csitemap.GetPathUrl("/css/date/jquery.daterangepicker.js") %>" type="text/javascript"></script>--%>
<script type="text/javascript">
    $(function () {
        var obj = '#<%=base.fmc.GetClientIDTextBox(fvTable,"tbDateOrder") %>'
        if (obj == '#') return;
        $(obj).dateRangePicker(
        {
            autoClose: true,
            singleDate: true,
            showShortcuts: false,
            format: 'DD-MM-YYYY',
        }).bind('datepicker-change', function (evt, obj) {


        });
    });

</script>
<asp:ObjectDataSource ID="odsTable" runat="server" SelectMethod="SelectListOrder" TypeName="Strategic.classOrder" OnSelecting="odsTable_Selecting" OnInserted="odsTable_Inserted" OnInserting="odsTable_Inserting" OnUpdated="odsTable_Updated" OnUpdating="odsTable_Updating" OnDeleted="odsTable_Deleted" OnDeleting="odsTable_Deleting" OnSelected="odsTable_Selected" UpdateMethod="UpdateOrder" InsertMethod="InsertOrder" DeleteMethod="DeleteOrder">
    <DeleteParameters>
        <asp:Parameter Name="IDOrder" Type="Int32" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IDTypeOrder" Type="Int32" />
        <asp:Parameter Name="NumOrder" Type="Int32" />
        <asp:Parameter Name="DateOrder" Type="DateTime" />
        <asp:Parameter Name="Order" Type="String" />
        <asp:Parameter Name="OrderEng" Type="String" />
        <asp:Parameter Name="IDFile" Type="Int32" />
        <asp:Parameter Name="IDFileEng" Type="Int32" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </InsertParameters>
    <SelectParameters>
        <asp:Parameter Name="IDOrder" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="IDOrder" Type="Int32" />
        <asp:Parameter Name="IDTypeOrder" Type="Int32" />
        <asp:Parameter Name="NumOrder" Type="Int32" />
        <asp:Parameter Name="DateOrder" Type="DateTime" />
        <asp:Parameter Name="Order" Type="String" />
        <asp:Parameter Name="OrderEng" Type="String" />
        <asp:Parameter Name="IDFile" Type="Int32" />
        <asp:Parameter Name="IDFileEng" Type="Int32" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </UpdateParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsListTypeOrder" runat="server" SelectMethod="SelectCultureTypeOrder" TypeName="Strategic.classOrder"></asp:ObjectDataSource>
<div class="panelTable">
    <asp:FormView ID="fvTable" runat="server" DataKeyNames="IDOrder" DataSourceID="odsTable" OnDataBound="fvTable_DataBound" OnModeChanging="fvTable_ModeChanging">
        <ItemTemplate>
            <table class="tabSelect">
                <tr>
                    <td colspan="3" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsUpdDel" ConfirmDelete="MessageDeleteOrder" OnInit="pnSelect_Init" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeOrder" runat="server" Text="<%$ Resources:ResourceStrategic, tsTypeOrder %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# base.corder.GetTypeOrder(Container.DataItem) %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltNumOrder" runat="server" Text="<%$ Resources:ResourceStrategic, tsNumOrder %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# Eval("NumOrder") %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltDateOrder" runat="server" Text="<%$ Resources:ResourceStrategic, tsDateOrder %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# Eval("DateOrder") %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltOrder" runat="server" Text="<%$ Resources:ResourceStrategic, tsOrder %>"></asp:Literal>:&nbsp;</th>
                    <td><b>На русском:</b><br />
                        <%# Eval("Order") %></td>
                    <td><b>In English:</b><br />
                        <%# Eval("OrderEng") %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltFileOrder" runat="server" Text="<%$ Resources:ResourceStrategic, tsOrderFile %>"></asp:Literal>:&nbsp;</th>
                    <td><b>На русском:</b><br />
                        <%# base.corder.GetFileOrder(Container.DataItem, "IDFile") %></td>
                    <td><b>In English:</b><br />
                        <%# base.corder.GetFileOrder(Container.DataItem, "IDFileEng") %></td>
                </tr>
            </table>
        </ItemTemplate>
        <EditItemTemplate>
            <table class="tabUpdate">
                <tr>
                    <td colspan="3" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="UpdCan" OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick" Change="true" ValidationGroup="Order"  />
                   </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeOrder" runat="server" Text="<%$ Resources:ResourceStrategic, tsTypeOrder %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlTypeOrder" runat="server" DataSourceID="odsListTypeOrder" DataTextField="TypeOrder" DataValueField="IDTypeOrder" CssClass="LineText" OnDataBound="ddlTypeOrder_DataBound"></asp:DropDownList>
                        <asp:Button ID="btInsertTypeOrder" runat="server" Text="+" OnClick="btInsertTypeOrder_Click" CausesValidation="false" />
                        <wuc:controlTypeOrder ID="InsertcontrolTypeOrder" runat="server" Change="true" OnDataInserted="InsertcontrolTypeOrder_DataInserted" OnDataCancelClick="InsertcontrolTypeOrder_DataCancelClick"/>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltNumOrder" runat="server" Text="<%$ Resources:ResourceStrategic, tsNumOrder %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:TextBox ID="tbNumOrder" runat="server" Text='<%# Eval("NumOrder") %>' TextMode="Number" CssClass="LineText" ></asp:TextBox></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltDateOrder" runat="server" Text="<%$ Resources:ResourceStrategic, tsDateOrder %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:TextBox ID="tbDateOrder" runat="server" Text='<%# Eval("DateOrder", "{0:dd-MM-yyyy}") %>' TextMode="Date" ></asp:TextBox></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltOrder" runat="server" Text="<%$ Resources:ResourceStrategic, tsOrder %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:TextBox ID="tbOrder" runat="server" Text='<%# Eval("Order") %>' CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <%--<asp:RegularExpressionValidator ID="revOrder" runat="server" ControlToValidate="tbOrder" ValidationGroup="Order"
                            ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,512}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="false">
                        </asp:RegularExpressionValidator>--%>
                        <asp:RequiredFieldValidator ID="rfvOrder" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbOrder" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="tbOrderEng" runat="server" Text='<%# Eval("OrderEng") %>' CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <%--<asp:RegularExpressionValidator ID="revOrderEng" runat="server" ControlToValidate="tbOrderEng" ValidationGroup="Order"
                            ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,512}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescriptionEng %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator>--%>
                        <asp:RequiredFieldValidator ID="rfvOrderEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbOrderEng" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltFileOrder" runat="server" Text="<%$ Resources:ResourceStrategic, tsOrderFile %>"></asp:Literal>:&nbsp;</th>
                    <td><b>На русском:</b><br />
                        <wuc:panelUnloadFile ID="panelUnloadFile" runat="server" OnInit="panelUnloadFile_Init" Width="250" /></td>
                    <td><b>In English:</b><br />
                        <wuc:panelUnloadFile ID="panelUnloadFileEng" runat="server" OnInit="panelUnloadFileEng_Init" Width="250" />

                    </td>
                </tr>
            </table>
        </EditItemTemplate>
        <InsertItemTemplate>
            <table class="tabInsert">
                <tr>
                    <td colspan="3" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsCan"  OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick" Change="true" ValidationGroup="Order"  />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeOrder" runat="server" Text="<%$ Resources:ResourceStrategic, tsTypeOrder %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlTypeOrder" runat="server" DataSourceID="odsListTypeOrder" DataTextField="TypeOrder" DataValueField="IDTypeOrder" CssClass="LineText" OnDataBound="ddlTypeOrder_DataBound"></asp:DropDownList>
                        <asp:Button ID="btInsertTypeOrder" runat="server" Text="+" OnClick="btInsertTypeOrder_Click" CausesValidation="false"  />
                        <wuc:controlTypeOrder ID="InsertcontrolTypeOrder" runat="server" Change="true" OnDataInserted="InsertcontrolTypeOrder_DataInserted" OnDataCancelClick="InsertcontrolTypeOrder_DataCancelClick"/>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltNumOrder" runat="server" Text="<%$ Resources:ResourceStrategic, tsNumOrder %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:TextBox ID="tbNumOrder" runat="server" TextMode="Number" CssClass="LineText" ></asp:TextBox></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltDateOrder" runat="server" Text="<%$ Resources:ResourceStrategic, tsDateOrder %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:TextBox ID="tbDateOrder" runat="server" TextMode="Date" ></asp:TextBox></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltOrder" runat="server" Text="<%$ Resources:ResourceStrategic, tsOrder %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:TextBox ID="tbOrder" runat="server" CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <%--<asp:RegularExpressionValidator ID="revOrder" runat="server" ControlToValidate="tbOrder" ValidationGroup="Order"
                            ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,512}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="false">
                        </asp:RegularExpressionValidator>--%>
                        <asp:RequiredFieldValidator ID="rfvOrder" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbOrder" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="tbOrderEng" runat="server" CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <%--<asp:RegularExpressionValidator ID="revOrderEng" runat="server" ControlToValidate="tbOrderEng" ValidationGroup="Order"
                            ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,512}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescriptionEng %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator>--%>
                        <asp:RequiredFieldValidator ID="rfvOrderEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbOrderEng" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltFileOrder" runat="server" Text="<%$ Resources:ResourceStrategic, tsOrderFile %>"></asp:Literal>:&nbsp;</th>
                    <td><b>На русском:</b><br />
                        <wuc:panelUnloadFile ID="panelUnloadFile" runat="server" OnInit="panelUnloadFile_Init" Width="250" /></td>
                    <td><b>In English:</b><br />
                        <wuc:panelUnloadFile ID="panelUnloadFileEng" runat="server" OnInit="panelUnloadFileEng_Init" Width="250" />

                    </td>
                </tr>
            </table>
        </InsertItemTemplate>
    </asp:FormView>
</div>