<%@ Control Language="C#" AutoEventWireup="true" CodeFile="controlTypeProject.ascx.cs" Inherits="controlTypeProject" %>
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
<asp:ObjectDataSource ID="odsTable" runat="server" SelectMethod="SelectTypeProject" TypeName="Strategic.classProject" OnSelecting="odsTable_Selecting" OnInserted="odsTable_Inserted" OnInserting="odsTable_Inserting" OnUpdated="odsTable_Updated" OnUpdating="odsTable_Updating" OnDeleted="odsTable_Deleted" OnDeleting="odsTable_Deleting" OnSelected="odsTable_Selected" UpdateMethod="UpdateTypeProject" DeleteMethod="DeleteTypeProject" InsertMethod="InsertTypeProject">
    <DeleteParameters>
        <asp:Parameter Name="IDTypeProject" Type="Int32" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="TypeProject" Type="String" />
        <asp:Parameter Name="TypeProjectEng" Type="String" />
        <asp:Parameter Name="Description" Type="String" />
        <asp:Parameter Name="DescriptionEng" Type="String" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </InsertParameters>
    <SelectParameters>
        <asp:Parameter Name="IDTypeProject" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="IDTypeProject" Type="Int32" />
        <asp:Parameter Name="TypeProject" Type="String" />
        <asp:Parameter Name="TypeProjectEng" Type="String" />
        <asp:Parameter Name="Description" Type="String" />
        <asp:Parameter Name="DescriptionEng" Type="String" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </UpdateParameters>
</asp:ObjectDataSource>
<div class="panelTable">
    <asp:FormView ID="fvTable" runat="server" DataKeyNames="IDTypeProject" DataSourceID="odsTable" OnDataBound="fvTable_DataBound" OnModeChanging="fvTable_ModeChanging">
        <ItemTemplate>
            <table class="tabSelect">
                <tr>
                    <td colspan="3" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsUpdDel" ConfirmDelete="MessageDeleteTypeProject" OnInit="pnSelect_Init" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeProject" runat="server" Text="<%$ Resources:ResourceStrategic, tsTypeProject %>"></asp:Literal>:&nbsp;</th>
                    <td><b>На русском:</b><br />
                        <%# Eval("TypeProject") %></td>
                    <td><b>In English:</b><br />
                        <%# Eval("TypeProjectEng") %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeProjectDescription" runat="server" Text="<%$ Resources:ResourceStrategic, tsTypeProjectDescription %>"></asp:Literal>:&nbsp;</th>
                    <td><b>На русском:</b><br />
                        <%# Eval("Description") %></td>
                    <td><b>In English:</b><br />
                        <%# Eval("DescriptionEng") %></td>
                </tr>
            </table>
        </ItemTemplate>
        <EditItemTemplate>
            <table class="tabUpdate">
                <tr>
                    <td colspan="3" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="UpdCan" OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick" Change="true"  />
                   </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeProject" runat="server" Text="<%$ Resources:ResourceStrategic, tsTypeProject %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:TextBox ID="tbTypeProject" runat="server" Text='<%# Eval("TypeProject") %>' CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revTypeProject" runat="server" ControlToValidate="tbTypeProject"
                            ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,50}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="false">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvTypeProject" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbTypeProject" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="tbTypeProjectEng" runat="server" Text='<%# Eval("TypeProjectEng") %>' CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revTypeProjectEng" runat="server" ControlToValidate="tbTypeProjectEng"
                            ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,50}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescriptionEng %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvTypeProjectEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbTypeProjectEng" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeProjectDescription" runat="server" Text="<%$ Resources:ResourceStrategic, tsTypeProjectDescription %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:TextBox ID="tbDescription" runat="server" Text='<%# Eval("Description") %>' CssClass="MultiText" TextMode="MultiLine"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revDescription" runat="server" ControlToValidate="tbDescription"
                            ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,1000}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="false">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbDescription" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="tbDescriptionEng" runat="server" Text='<%# Eval("DescriptionEng") %>' CssClass="MultiText" TextMode="MultiLine"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revDescriptionEng" runat="server" ControlToValidate="tbDescriptionEng"
                            ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,1000}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescriptionEng %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvDescriptionEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbDescriptionEng" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </EditItemTemplate>
        <InsertItemTemplate>
            <table class="tabInsert">
                <tr>
                    <td colspan="3" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsCan"  OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick" Change="true"  />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeProject" runat="server" Text="<%$ Resources:ResourceStrategic, tsTypeProject %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:TextBox ID="tbTypeProject" runat="server" Text='<%# Eval("TypeProject") %>' CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revTypeProject" runat="server" ControlToValidate="tbTypeProject"
                            ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,50}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="false">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvTypeProject" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbTypeProject" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="tbTypeProjectEng" runat="server" Text='<%# Eval("TypeProjectEng") %>' CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revTypeProjectEng" runat="server" ControlToValidate="tbTypeProjectEng"
                            ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,50}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescriptionEng %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvTypeProjectEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbTypeProjectEng" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeProjectDescription" runat="server" Text="<%$ Resources:ResourceStrategic, tsTypeProjectDescription %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:TextBox ID="tbDescription" runat="server" Text='<%# Eval("Description") %>' CssClass="MultiText" TextMode="MultiLine"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revDescription" runat="server" ControlToValidate="tbDescription"
                            ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,1000}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="false">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbDescription" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="tbDescriptionEng" runat="server" Text='<%# Eval("DescriptionEng") %>' CssClass="MultiText" TextMode="MultiLine"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revDescriptionEng" runat="server" ControlToValidate="tbDescriptionEng"
                            ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,1000}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescriptionEng %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvDescriptionEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbDescriptionEng" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </InsertItemTemplate>
    </asp:FormView>
</div>