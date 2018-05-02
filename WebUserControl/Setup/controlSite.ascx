<%@ Control Language="C#" AutoEventWireup="true" CodeFile="controlSite.ascx.cs" Inherits="controlSite" %>
<%@ Register TagPrefix="wuc" TagName="ControlFileBrowser" Src="~/WebUserControl/controlFileBrowser.ascx" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>

<style type="text/css">
table.tabInsertSite, table.tabSelectSite, table.tabUpdateSite {
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
.tabInsertSite th, .tabSelectSite th, .tabUpdateSite th {
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
.tabInsertSite td, .tabUpdateSite td, .tabSelectSite td{
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
<asp:ObjectDataSource ID="odsTableSite" runat="server" SelectMethod="SelectListSite" TypeName="WebBase.classSite" OnSelecting="odsTableSite_Selecting" InsertMethod="InsertSite" OnInserted="odsTableSite_Inserted" OnInserting="odsTableSite_Inserting" OnUpdated="odsTableSite_Updated" OnUpdating="odsTableSite_Updating" DeleteMethod="DeleteSite" OnDeleted="odsTableSite_Deleted" OnDeleting="odsTableSite_Deleting" OnSelected="odsTableSite_Selected" UpdateMethod="UpdateSite">
    <DeleteParameters>
        <asp:Parameter Name="IDSite" Type="Int32" />
        <asp:Parameter Name="OutResult" Type="Boolean" DefaultValue="true" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="URL" Type="String" />
        <asp:Parameter Name="Description" Type="String" />
        <asp:Parameter Name="DescriptionEng" Type="String" />
        <asp:Parameter Name="URLHelp" Type="String" />
        <asp:Parameter Name="OutResult" Type="Boolean" DefaultValue="true" />
    </InsertParameters>
    <SelectParameters>
        <asp:Parameter Name="IDSite" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="IDSite" Type="Int32" />
        <asp:Parameter Name="URL" Type="String" />
        <asp:Parameter Name="Description" Type="String" />
        <asp:Parameter Name="DescriptionEng" Type="String" />
        <asp:Parameter Name="URLHelp" Type="String" />
        <asp:Parameter Name="OutResult" Type="Boolean" DefaultValue="true" />
    </UpdateParameters>
</asp:ObjectDataSource>
<div class="panelSite">
    <asp:FormView ID="fvSite" runat="server" DataKeyNames="IDSite" DataSourceID="odsTableSite" OnDataBound="fvSite_DataBound" OnModeChanging="fvSite_ModeChanging">
        <ItemTemplate>
            <table class="tabSelectSite">
                <tr>
                    <td colspan="3" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsUpdDel" ConfirmDelete="MessageDeleteSite" OnInit="pnSelect_Init" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltDescription" runat="server" Text="<%$ Resources:ResourceBase, tsDescription %>"></asp:Literal>:&nbsp;</th>
                    <td><b>На русском:</b><br />
                        <%# Eval("Description") %></td>
                    <td><b>In English:</b><br />
                        <%# Eval("DescriptionEng") %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltURL" runat="server" Text="<%$ Resources:ResourceBase, tsURL %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# Eval("URL") %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltURLHelp" runat="server" Text="<%$ Resources:ResourceBase, tsURLHelp %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# Eval("URLHelp") %></td>
                </tr>
            </table>
        </ItemTemplate>
        <EditItemTemplate>
            <table class="tabUpdateSite">
                <tr>
                    <td colspan="3" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="UpdCan" OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick" Change="true"  />
                   </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltDescription" runat="server" Text="<%$ Resources:ResourceBase, tsDescription %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:TextBox ID="tbDescription" runat="server" Text='<%# Eval("Description") %>' CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revDescription" runat="server" ControlToValidate="tbDescription"
                            ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,1000}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="false">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbDescription" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="tbDescriptionEng" runat="server" Text='<%# Eval("DescriptionEng") %>' CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revDescriptionEng" runat="server" ControlToValidate="tbDescriptionEng"
                            ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,1000}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescriptionEng %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvDescriptionEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbDescriptionEng" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltURL" runat="server" Text="<%$ Resources:ResourceBase, tsURL %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:TextBox ID="tbURL" runat="server" Text='<%# Eval("URL") %>' CssClass="LineText" ReadOnly="True" ></asp:TextBox>
                        &nbsp;&nbsp;<asp:Button ID="btURL" runat="server" Text="+" OnClick="btURL_Click" CausesValidation="false" />
                        &nbsp;&nbsp;<asp:Button ID="btURLClear" runat="server" Text="-" CausesValidation="false" OnClick="btURLClear_Click" />
                        <asp:RequiredFieldValidator ID="rfvURL" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbURL" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltURLHelp" runat="server" Text="<%$ Resources:ResourceBase, tsURLHelp %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:TextBox ID="tbURLHelp" runat="server" Text='<%# Eval("URLHelp") %>' CssClass="LineText" ReadOnly="True" ></asp:TextBox>
                        &nbsp;&nbsp;<asp:Button ID="btURLHelp" runat="server" Text="+" OnClick="btURLHelp_Click" CausesValidation="false" />
                        &nbsp;&nbsp;<asp:Button ID="btURLHelpClear" runat="server" Text="-" CausesValidation="false" OnClick="btURLHelpClear_Click" />
                    </td>
                </tr>
            </table>
        </EditItemTemplate>
        <InsertItemTemplate>
            <table class="tabInsertSite">
                <tr>
                    <td colspan="3" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsCan"  OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick" Change="true"  />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltDescription" runat="server" Text="<%$ Resources:ResourceBase, tsDescription %>"></asp:Literal>:&nbsp;</th>
                    <td>
                        <asp:TextBox ID="tbDescription" runat="server" CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revDescription" runat="server" ControlToValidate="tbDescription"
                            ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,1000}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="false">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbDescription" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="tbDescriptionEng" runat="server" CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revDescriptionEng" runat="server" ControlToValidate="tbDescriptionEng"
                            ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,1000}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescriptionEng %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvDescriptionEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbDescriptionEng" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltURL" runat="server" Text="<%$ Resources:ResourceBase, tsURL %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:TextBox ID="tbURL" runat="server" CssClass="LineText" ReadOnly="True" ></asp:TextBox>
                        &nbsp;&nbsp;<asp:Button ID="btURL" runat="server" Text="+" OnClick="btURL_Click" CausesValidation="false" />
                        &nbsp;&nbsp;<asp:Button ID="btURLClear" runat="server" Text="-" CausesValidation="false" OnClick="btURLClear_Click" />
                        <asp:RequiredFieldValidator ID="rfvURL" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbURL" CssClass="failure"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltURLHelp" runat="server" Text="<%$ Resources:ResourceBase, tsURLHelp %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:TextBox ID="tbURLHelp" runat="server" CssClass="LineText" ReadOnly="True" ></asp:TextBox>
                        &nbsp;&nbsp;<asp:Button ID="btURLHelp" runat="server" Text="+" OnClick="btURLHelp_Click" CausesValidation="false" />
                        &nbsp;&nbsp;<asp:Button ID="btURLHelpClear" runat="server" Text="-" CausesValidation="false" OnClick="btURLHelpClear_Click" />
                    </td>
                </tr>
            </table>
        </InsertItemTemplate>
    </asp:FormView>
    <wuc:ControlFileBrowser ID="FileBrowser" runat="server" pnVisible="false" TypeFile="*.aspx" OnFileListSelected="FileBrowser_FileListSelected" OnCloseClick="FileBrowser_CloseClick" />
</div>