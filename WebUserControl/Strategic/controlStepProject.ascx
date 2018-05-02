<%@ Control Language="C#" AutoEventWireup="true" CodeFile="controlStepProject.ascx.cs" Inherits="controlStepProject" %>
<%@ Register TagPrefix="wuc" TagName="ControlFileBrowser" Src="~/WebUserControl/controlFileBrowser.ascx" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>
<%@ Register TagPrefix="wuc" TagName="controlBigStepProject" Src="~/WebUserControl/Strategic/controlBigStepProject.ascx" %>

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
    width: 90%;
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
<asp:ObjectDataSource ID="odsTableStep" runat="server" DeleteMethod="DeleteStepsProject" InsertMethod="InsertStepsProject" OnDeleted="odsTableStep_Deleted" OnDeleting="odsTableStep_Deleting" OnInserted="odsTableStep_Inserted" OnInserting="odsTableStep_Inserting" OnSelected="odsTableStep_Selected" OnSelecting="odsTableStep_Selecting" OnUpdated="odsTableStep_Updated" OnUpdating="odsTableStep_Updating" SelectMethod="SelectStepsProject" TypeName="Strategic.classTemplatesSteps" UpdateMethod="UpdateStepsProject">
    <DeleteParameters>
        <asp:Parameter Name="IDStep" Type="Int32" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IDTemplateStepProject" Type="Int32" />
        <asp:Parameter Name="IDBigStep" Type="Int32" />
        <asp:Parameter Name="Step" Type="String" />
        <asp:Parameter Name="StepEng" Type="String" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </InsertParameters>
    <SelectParameters>
        <asp:Parameter Name="IDStep" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="IDStep" Type="Int32" />
        <asp:Parameter Name="IDTemplateStepProject" Type="Int32" />
        <asp:Parameter Name="IDBigStep" Type="Int32" />
        <asp:Parameter Name="Step" Type="String" />
        <asp:Parameter Name="StepEng" Type="String" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </UpdateParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsListTemplate" runat="server" SelectMethod="SelectCultureTemplatesSteps" TypeName="Strategic.classTemplatesSteps"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsListBigStep" runat="server" SelectMethod="SelectCultureBigStepsProject" TypeName="Strategic.classTemplatesSteps"></asp:ObjectDataSource>
<div class="panelSite">
    <asp:FormView ID="fvStepProject" runat="server" DataKeyNames="IDStep" DataSourceID="odsTableStep" OnDataBound="fvStepProject_DataBound" OnModeChanging="fvStepProject_ModeChanging">
        <ItemTemplate>
            <table class="tabSelect">
                <tr>
                    <td colspan="3" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsUpdDel" ConfirmDelete="MessageDeleteStepProject" OnInit="pnSelect_Init" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTemplate" runat="server" Text="<%$ Resources:ResourceStrategic, tsTemplate %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# base.ctemplatessteps.GetTemplate(Container.DataItem) %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltBigStep" runat="server" Text="<%$ Resources:ResourceStrategic, tsBigStep %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# base.ctemplatessteps.GetBigStep(Container.DataItem) %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltStep" runat="server" Text="<%$ Resources:ResourceStrategic, tsStep %>"></asp:Literal>:&nbsp;</th>
                    <td><b>На русском:</b><br />
                        <%# Eval("Step") %></td>
                    <td><b>In English:</b><br />
                        <%# Eval("StepEng") %></td>
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
                        <asp:Literal ID="ltTemplate" runat="server" Text="<%$ Resources:ResourceStrategic, tsTemplate %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlTemplate" runat="server" AutoPostBack="True" DataSourceID="odsListTemplate" DataTextField="TemplateStep" DataValueField="IDTemplateStepProject" OnDataBound="ddlTemplate_DataBound" CssClass="LineText"></asp:DropDownList>
                        <asp:Button ID="btInsertTemplate" runat="server" Text="+" CausesValidation="false" ToolTip="<%$ Resources:ResourceBase, ToolTipAddTemplate %>" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltBigStep" runat="server" Text="<%$ Resources:ResourceStrategic, tsBigStep %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlBigStep" runat="server" AutoPostBack="True" DataSourceID="odsListBigStep" DataTextField="BigStep" DataValueField="IDBigStep" OnDataBound="ddlBigStep_DataBound" CssClass="LineText"></asp:DropDownList>
                        <asp:Button ID="btBigStep" runat="server" Text="+" CausesValidation="false" ToolTip="<%$ Resources:ResourceBase, ToolTipAddBigStep %>" OnClick="btBigStep_Click" />
                        
                        <wuc:controlBigStepProject ID="BigStepProject" runat="server" Change="true" OutInfoText="true" OnDataInserted="BigStepProject_DataInserted" OnDataCancelClick="BigStepProject_DataCancelClick"/>

                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltStep" runat="server" Text="<%$ Resources:ResourceStrategic, tsStep %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:TextBox ID="tbStep" runat="server" Text='<%# Eval("Step") %>' CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revBigStep" runat="server" ControlToValidate="tbStep"
                            ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,100}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="false">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvStep" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbStep" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="tbStepEng" runat="server" Text='<%# Eval("StepEng") %>' CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revBigStepEng" runat="server" ControlToValidate="tbStepEng"
                            ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,100}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescriptionEng %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvStepEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbStepEng" CssClass="failure"></asp:RequiredFieldValidator>
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
                        <asp:Literal ID="ltTemplate" runat="server" Text="<%$ Resources:ResourceStrategic, tsTemplate %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlTemplate" runat="server" AutoPostBack="True" DataSourceID="odsListTemplate" DataTextField="TemplateStep" DataValueField="IDTemplateStepProject" OnDataBound="ddlTemplate_DataBound" CssClass="LineText"></asp:DropDownList>
                        <asp:Button ID="btInsertTemplate" runat="server" Text="+" CausesValidation="false" ToolTip="<%$ Resources:ResourceBase, ToolTipAddTemplate %>" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:ResourceStrategic, tsBigStep %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlBigStep" runat="server" AutoPostBack="True" DataSourceID="odsListBigStep" DataTextField="BigStep" DataValueField="IDBigStep" OnDataBound="ddlBigStep_DataBound" CssClass="LineText"></asp:DropDownList>
                        <asp:Button ID="btBigStep" runat="server" Text="+" CausesValidation="false" ToolTip="<%$ Resources:ResourceBase, ToolTipAddBigStep %>" OnClick="btBigStep_Click"  />
                        <wuc:controlBigStepProject ID="BigStepProject" runat="server" Change="true" OutInfoText="true" OnDataInserted="BigStepProject_DataInserted" OnDataCancelClick="BigStepProject_DataCancelClick"/>

                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltStep" runat="server" Text="<%$ Resources:ResourceStrategic, tsStep %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:TextBox ID="tbStep" runat="server" CssClass="MultiText" TextMode="MultiLine"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revStep" runat="server" ControlToValidate="tbStep"
                            ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,100}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="false">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvStep" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbStep" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="tbStepEng" runat="server" CssClass="MultiText" TextMode="MultiLine"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revStepEng" runat="server" ControlToValidate="tbStepEng"
                            ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,100}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescriptionEng %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvStepEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbStepEng" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </InsertItemTemplate>
    </asp:FormView>
</div>