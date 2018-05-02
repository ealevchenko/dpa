<%@ Control Language="C#" AutoEventWireup="true" CodeFile="controlSiteMap.ascx.cs" Inherits="controlSiteMap" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>
<%@ Register TagPrefix="wuc" TagName="ControlSite" Src="~/WebUserControl/Setup/controlSite.ascx" %>
<%@ Register TagPrefix="wuc" TagName="controlSection" Src="~/WebUserControl/Setup/controlSection.ascx" %>
<style type="text/css">
table.tabInsertSiteMap, table.tabSelectSiteMap, table.tabUpdateSiteMap {
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
.tabInsertSiteMap th, .tabSelectSiteMap th, .tabUpdateSiteMap th {
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
.tabInsertSiteMap td, .tabUpdateSiteMap td, .tabSelectSiteMap td{
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
<!-- Источники данных-->
<asp:ObjectDataSource ID="odsTableSiteMap" runat="server" SelectMethod="SelectSiteMap" TypeName="WebBase.classSiteMap" OnSelecting="odsTableSiteMap_Selecting" OnInserted="odsTableSiteMap_Inserted" OnInserting="odsTableSiteMap_Inserting" OnUpdated="odsTableSiteMap_Updated" OnUpdating="odsTableSiteMap_Updating" OnDeleted="odsTableSiteMap_Deleted" OnDeleting="odsTableSiteMap_Deleting" OnSelected="odsTableSiteMap_Selected" InsertMethod="InsertSiteMap" UpdateMethod="UpdateSiteMap" DeleteMethod="DeleteSiteMap">
    <DeleteParameters>
        <asp:Parameter Name="IDSiteMap" Type="Int32" />
        <asp:Parameter Name="OutResult" Type="Boolean" DefaultValue="true"   />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IDWeb" Type="Int32" />
        <asp:Parameter Name="IDSite" Type="Int32" />
        <asp:Parameter Name="Title" Type="String" />
        <asp:Parameter Name="TitleEng" Type="String" />
        <asp:Parameter Name="Description" Type="String" />
        <asp:Parameter Name="DescriptionEng" Type="String" />
        <asp:Parameter Name="Protection" Type="Boolean" />
        <asp:Parameter Name="PageProcessor" Type="Boolean" />
        <asp:Parameter Name="ParentID" Type="Int32" />
        <asp:Parameter Name="IDSection" Type="Int32" />
        <asp:Parameter Name="OutResult" Type="Boolean" DefaultValue="true" />
    </InsertParameters>
    <SelectParameters>
        <asp:Parameter Name="IDSiteMap" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="IDSiteMap" Type="Int32" />
        <asp:Parameter Name="IDWeb" Type="Int32" />
        <asp:Parameter Name="IDSite" Type="Int32" />
        <asp:Parameter Name="Title" Type="String" />
        <asp:Parameter Name="TitleEng" Type="String" />
        <asp:Parameter Name="Description" Type="String" />
        <asp:Parameter Name="DescriptionEng" Type="String" />
        <asp:Parameter Name="Protection" Type="Boolean" />
        <asp:Parameter Name="PageProcessor" Type="Boolean" />
        <asp:Parameter Name="ParentID" Type="Int32" />
        <asp:Parameter Name="IDSection" Type="Int32" />
        <asp:Parameter Name="OutResult" Type="Boolean" DefaultValue="true" />
    </UpdateParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsListWeb" runat="server" SelectMethod="SelectWeb" TypeName="WebBase.classWeb"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsListURLSite" runat="server" SelectMethod="DDLListSiteCulture" TypeName="WebBase.classSite">
    <SelectParameters>
        <asp:Parameter DefaultValue="ddlNot" Name="value" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsListParent" runat="server" SelectMethod="DDLListSiteMapCulture" TypeName="WebBase.classSiteMap" OnSelecting="odsListParent_Selecting">
    <SelectParameters>
        <asp:Parameter DefaultValue="ddlRoot" Name="value" Type="String" />
        <asp:Parameter Name="IDSiteMap" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsListSection" runat="server" SelectMethod="DDLListSectionCulture" TypeName="WebBase.classSection">
    <SelectParameters>
        <asp:Parameter DefaultValue="true" Name="Full" Type="Boolean" />
    </SelectParameters>
</asp:ObjectDataSource>
<!-- Таблица отображения-->
<asp:FormView ID="fvSiteMap" runat="server" DataKeyNames="IDSiteMap" DataSourceID="odsTableSiteMap" OnDataBound="fvSiteMap_DataBound" OnModeChanging="fvSiteMap_ModeChanging">
    <ItemTemplate>
        <table class="tabSelectSiteMap">
            <tr>
                <td colspan="3" class="Upr">
                    <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsUpdDel" ConfirmDelete="MessageDeleteSiteMap" OnInit="pnSelect_Init" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltWeb" runat="server" Text="<%$ Resources:ResourceBase, tsWeb %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2"><%# base.cw.GetWeb(Container.DataItem) %></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltSite" runat="server" Text="<%$ Resources:ResourceBase, tsSite %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2"><%# base.csite.GetSiteURL(Container.DataItem) %></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltTitle" runat="server" Text="<%$ Resources:ResourceBase, tsTitle %>"></asp:Literal>:&nbsp;</th>
                <td><b>На русском:</b><br /><%# Eval("Title") %></td>
                <td><b>In English:</b><br /><%# Eval("TitleEng") %></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltDescription" runat="server" Text="<%$ Resources:ResourceBase, tsDescription %>"></asp:Literal>:&nbsp;</th>
                <td><b>На русском:</b><br /><%# Eval("Description") %></td>
                <td><b>In English:</b><br /><%# Eval("DescriptionEng") %></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltProtection" runat="server" Text="<%$ Resources:ResourceBase, tsProtection %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2"><asp:Image ID="imProtection" runat="server" ImageUrl='<%# GetSRCCheckBox(Container.DataItem,"Protection") %>' Height="32px" Width="32px" /></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltPageProcessor" runat="server" Text="<%$ Resources:ResourceBase, tsPageProcessor %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2"><asp:Image ID="imPageProcessor" runat="server" ImageUrl='<%# GetSRCCheckBox(Container.DataItem,"PageProcessor") %>' Height="32px" Width="32px" /></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltParent" runat="server" Text="<%$ Resources:ResourceBase, tsParent %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2"><%# base.csitemap.GetParentTitle(Container.DataItem) %></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltSection" runat="server" Text="<%$ Resources:ResourceBase, tsOwner %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2"><%# base.csection.GetSection(Container.DataItem, false) %></td>
            </tr>
        </table>
    </ItemTemplate>
    <EditItemTemplate>
        <table class="tabUpdateSiteMap">
            <tr>
                <td colspan="3" class="Upr">
                    <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="UpdCan" OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick" Change="true"  />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltWeb" runat="server" Text="<%$ Resources:ResourceBase, tsWeb %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2">
                    <asp:DropDownList ID="ddlWeb" runat="server" DataSourceID="odsListWeb" DataTextField="Web" DataValueField="IDWeb" CssClass="LineText" ></asp:DropDownList>
                    <asp:Button ID="btInsertWeb" runat="server" Text="+" CausesValidation="false" ToolTip="<%$ Resources:ResourceBase, ToolTipAddWeb %>" OnClick="btInsertWeb_Click" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltSite" runat="server" Text="<%$ Resources:ResourceBase, tsSite %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2">
                    <asp:DropDownList ID="ddlSite" runat="server" DataSourceID="odsListURLSite" DataTextField="URL" DataValueField="IDSite" CssClass="LineText" AutoPostBack="true" OnDataBinding="ddlSite_DataBinding"></asp:DropDownList>
                    <asp:Button ID="btInsertSite" runat="server" Text="+" OnClick="btInsertSite_Click" CausesValidation="false" ToolTip="<%$ Resources:ResourceBase, ToolTipAddSite %>"/>

                    <wuc:ControlSite ID="SiteInsert" runat="server"  OnDataInserted="controlSite_DataInserted" OnDataCancelClick="controlSite_DataCancelClick" Change="true"/>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltTitle" runat="server" Text="<%$ Resources:ResourceBase, tsTitle %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:TextBox ID="tbTitle" runat="server" CssClass="MultiText" TextMode="MultiLine" Text='<%# Eval("Title") %>' ></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revTitle" runat="server" ControlToValidate="tbTitle"
                        ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,250}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteMapTitle %>"
                        Display="dynamic" CssClass="failure" Enabled="false">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                        ControlToValidate="tbTitle" CssClass="failure"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="tbTitleEng" runat="server" CssClass="MultiText" TextMode="MultiLine" Text='<%# Eval("TitleEng") %>' ></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revTitleEng" runat="server" ControlToValidate="tbTitleEng"
                        ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,250}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteMapTitleEng %>"
                        Display="dynamic" CssClass="failure">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvTitleEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                        ControlToValidate="tbTitleEng" CssClass="failure"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltDescription" runat="server" Text="<%$ Resources:ResourceBase, tsDescription %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:TextBox ID="tbDescription" runat="server" CssClass="MultiText" TextMode="MultiLine" Text='<%# Eval("Description") %>' ></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revDescription" runat="server" ControlToValidate="tbDescription" 
                        ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,1000}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                        Display="dynamic" CssClass="failure" Enabled="false">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>" 
                        ControlToValidate="tbDescription" CssClass="failure"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="tbDescriptionEng" runat="server" CssClass="MultiText" TextMode="MultiLine" Text='<%# Eval("DescriptionEng") %>' ></asp:TextBox>
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
                    <asp:Literal ID="ltProtection" runat="server" Text="<%$ Resources:ResourceBase, tsProtection %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2">
                    <asp:CheckBox ID="cbProtection" runat="server" Checked='<%# Eval("Protection") %>' /></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltPageProcessor" runat="server" Text="<%$ Resources:ResourceBase, tsPageProcessor %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2">
                    <asp:CheckBox ID="cbPageProcessor" runat="server" Checked='<%# Eval("PageProcessor") %>'  /></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltParent" runat="server" Text="<%$ Resources:ResourceBase, tsParent %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2">
                    <asp:DropDownList ID="ddlParent" runat="server" DataSourceID="odsListParent" DataTextField="Title" DataValueField="IDSiteMap" CssClass="LineText" AutoPostBack="true" ></asp:DropDownList></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltSection" runat="server" Text="<%$ Resources:ResourceBase, tsOwner %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2">
                    <asp:DropDownList ID="ddlSection" runat="server" DataSourceID="odsListSection" DataTextField="Section" DataValueField="IDSection" CssClass="LineText" OnDataBound="ddlSection_DataBound" ></asp:DropDownList>
                    <asp:Button ID="btInsertSection" runat="server" Text="+" OnClick="btInsertSection_Click" CausesValidation="false" ToolTip="<%$ Resources:ResourceBase, ToolTipAddSection %>"/>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
    <InsertItemTemplate>
        <table class="tabInsertSiteMap">
            <tr>
                <td colspan="3" class="Upr">
                    <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsCan"  OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick" Change="true"  />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltWeb" runat="server" Text="<%$ Resources:ResourceBase, tsWeb %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2">
                    <asp:DropDownList ID="ddlWeb" runat="server" DataSourceID="odsListWeb" DataTextField="Web" DataValueField="IDWeb" CssClass="LineText"></asp:DropDownList>
                    <asp:Button ID="btInsertWeb" runat="server" Text="+" CausesValidation="false" ToolTip="<%$ Resources:ResourceBase, ToolTipAddWeb %>" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltSite" runat="server" Text="<%$ Resources:ResourceBase, tsSite %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2">
                    <asp:DropDownList ID="ddlSite" runat="server" DataSourceID="odsListURLSite" DataTextField="URL" DataValueField="IDSite" CssClass="LineText"  AutoPostBack="true" OnDataBinding="ddlSite_DataBinding"></asp:DropDownList>
                    <asp:Button ID="btInsertSite" runat="server" Text="+" OnClick="btInsertSite_Click" CausesValidation="false" ToolTip="<%$ Resources:ResourceBase, ToolTipAddSite %>"/>

                    <wuc:ControlSite ID="SiteInsert" runat="server"  OnDataInserted="controlSite_DataInserted" OnDataCancelClick="controlSite_DataCancelClick" Change="true"/>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltTitle" runat="server" Text="<%$ Resources:ResourceBase, tsTitle %>"></asp:Literal>:&nbsp;</th>
                <td>
                    <asp:TextBox ID="tbTitle" runat="server" CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revTitle" runat="server" ControlToValidate="tbTitle"
                        ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,250}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteMapTitle %>"
                        Display="dynamic" CssClass="failure" Enabled="false">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                        ControlToValidate="tbTitle" CssClass="failure"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="tbTitleEng" runat="server" CssClass="MultiText" TextMode="MultiLine" ></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revTitleEng" runat="server" ControlToValidate="tbTitleEng"
                        ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,250}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteMapTitleEng %>"
                        Display="dynamic" CssClass="failure">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvTitleEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                        ControlToValidate="tbTitleEng" CssClass="failure"></asp:RequiredFieldValidator>
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
                    <asp:Literal ID="ltProtection" runat="server" Text="<%$ Resources:ResourceBase, tsProtection %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2">
                    <asp:CheckBox ID="cbProtection" runat="server" Checked="false" /></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltPageProcessor" runat="server" Text="<%$ Resources:ResourceBase, tsPageProcessor %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2">
                    <asp:CheckBox ID="cbPageProcessor" runat="server" Checked="false" /></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltParent" runat="server" Text="<%$ Resources:ResourceBase, tsParent %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2">
                    <asp:DropDownList ID="ddlParent" runat="server" DataSourceID="odsListParent" DataTextField="Title" DataValueField="IDSiteMap" CssClass="LineText" AutoPostBack="true" ></asp:DropDownList></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltSection" runat="server" Text="<%$ Resources:ResourceBase, tsOwner %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2">
                    <asp:DropDownList ID="ddlSection" runat="server" DataSourceID="odsListSection" DataTextField="Section" DataValueField="IDSection" CssClass="LineText" OnDataBound="ddlSection_DataBound" ></asp:DropDownList>
                    <asp:Button ID="btInsertSection" runat="server" Text="+" OnClick="btInsertSection_Click" CausesValidation="false" ToolTip="<%$ Resources:ResourceBase, ToolTipAddSection %>"/>

                    <wuc:controlSection ID="controlSection" runat="server" OutInfoText="true" Change="true" OnDataInserted="controlSection_DataInserted" OnDataCancelClick="controlSection_DataCancelClick" />
                </td>
            </tr>
        </table>
    </InsertItemTemplate>
</asp:FormView>