<%@ Control Language="C#" AutoEventWireup="true" CodeFile="controlBigStepProject.ascx.cs" Inherits="controlBigStepProject" %>
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
<asp:ObjectDataSource ID="odsTableStep" runat="server" SelectMethod="SelectBigStepsProjects" TypeName="Strategic.classTemplatesSteps" OnSelecting="odsTableStep_Selecting" OnInserted="odsTableStep_Inserted" OnInserting="odsTableStep_Inserting" OnUpdated="odsTableStep_Updated" OnUpdating="odsTableStep_Updating" OnDeleted="odsTableStep_Deleted" OnDeleting="odsTableStep_Deleting" OnSelected="odsTableStep_Selected" InsertMethod="InsertBigStepsProject" DeleteMethod="DeleteBigStepsProject" UpdateMethod="UpdateBigStepsProject">
    <DeleteParameters>
        <asp:Parameter Name="IDBigStep" Type="Int32" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="BigStep" Type="String" />
        <asp:Parameter Name="BigStepEng" Type="String" />
        <asp:Parameter Name="OutResult" Type="Boolean"/>
    </InsertParameters>
    <SelectParameters>
        <asp:Parameter Name="IDBigStep" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="IDBigStep" Type="Int32" />
        <asp:Parameter Name="BigStep" Type="String" />
        <asp:Parameter Name="BigStepEng" Type="String" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </UpdateParameters>
</asp:ObjectDataSource>
<div class="panelSite">
    <asp:FormView ID="fvStepProject" runat="server" DataKeyNames="IDBigStep" DataSourceID="odsTableStep" OnDataBound="fvStepProject_DataBound" OnModeChanging="fvStepProject_ModeChanging">
        <ItemTemplate>
            <table class="tabSelect">
                <tr>
                    <td colspan="3" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsUpdDel" ConfirmDelete="MessageDeleteBigStepProject" OnInit="pnSelect_Init" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltBigStep" runat="server" Text="<%$ Resources:ResourceStrategic, tsBigStep %>"></asp:Literal>:&nbsp;</th>
                    <td><b>На русском:</b><br />
                        <%# Eval("BigStep") %></td>
                    <td><b>In English:</b><br />
                        <%# Eval("BigStepEng") %></td>
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
                        <asp:Literal ID="ltBigStep" runat="server" Text="<%$ Resources:ResourceStrategic, tsBigStep %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:TextBox ID="tbBigStep" runat="server" Text='<%# Eval("BigStep") %>' CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revBigStep" runat="server" ControlToValidate="tbBigStep"
                            ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,100}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="false">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvBigStep" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbBigStep" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="tbBigStepEng" runat="server" Text='<%# Eval("BigStepEng") %>' CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revBigStepEng" runat="server" ControlToValidate="tbBigStepEng"
                            ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,100}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescriptionEng %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvBigStepEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbBigStepEng" CssClass="failure"></asp:RequiredFieldValidator>
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
                        <asp:Literal ID="ltBigStep" runat="server" Text="<%$ Resources:ResourceStrategic, tsBigStep %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:TextBox ID="tbBigStep" runat="server" CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revBigStep" runat="server" ControlToValidate="tbBigStep"
                            ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,100}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="false">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvBigStep" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbBigStep" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="tbBigStepEng" runat="server" CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revBigStepEng" runat="server" ControlToValidate="tbBigStepEng"
                            ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,100}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescriptionEng %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvBigStepEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbBigStepEng" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </InsertItemTemplate>
    </asp:FormView>
</div>