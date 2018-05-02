<%@ Control Language="C#" AutoEventWireup="true" CodeFile="controlSection.ascx.cs" Inherits="controlSection" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>

<style type="text/css">
table.tabInsertSection, table.tabSelectSection, table.tabUpdateSection {
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
.tabInsertSection th, .tabSelectSection th, .tabUpdateSection th {
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
.tabInsertSection td, .tabUpdateSection td, .tabSelectSection td{
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
.titleSection
{
    font-family: Georgia, Times, 'Times New Roman' , serif;
    font-size: 24px;
    color: #808000;
    text-align: center;
}
</style>

<asp:ObjectDataSource ID="odsTable" runat="server" OnDeleted="odsTable_Deleted" OnDeleting="odsTable_Deleting" OnInserted="odsTable_Inserted" OnInserting="odsTable_Inserting" OnSelected="odsTable_Selected" OnSelecting="odsTable_Selecting" OnUpdated="odsTable_Updated" OnUpdating="odsTable_Updating" SelectMethod="SelectSection" TypeName="WebBase.classSection" UpdateMethod="UpdateSection" DeleteMethod="DeleteSection" InsertMethod="InsertSection">
    <DeleteParameters>
        <asp:Parameter Name="IDSection" Type="Int32" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="Section" Type="String" />
        <asp:Parameter Name="SectionEng" Type="String" />
        <asp:Parameter Name="SectionFull" Type="String" />
        <asp:Parameter Name="SectionFullEng" Type="String" />
        <asp:Parameter Name="TypeSection" Type="Int32" />
        <asp:Parameter Name="Cipher" Type="Int32" />
        <asp:Parameter Name="ParentID" Type="Int32" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </InsertParameters>
    <SelectParameters>
        <asp:Parameter Name="IDSection" Type="Int32" />
    </SelectParameters>

    <UpdateParameters>
        <asp:Parameter Name="IDSection" Type="Int32" />
        <asp:Parameter Name="Section" Type="String" />
        <asp:Parameter Name="SectionEng" Type="String" />
        <asp:Parameter Name="SectionFull" Type="String" />
        <asp:Parameter Name="SectionFullEng" Type="String" />
        <asp:Parameter Name="TypeSection" Type="Int32" />
        <asp:Parameter Name="Cipher" Type="Int32" />
        <asp:Parameter Name="ParentID" Type="Int32" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </UpdateParameters>

</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsListParent" runat="server" SelectMethod="ListSectionCultureNotChild" TypeName="WebBase.classSection" OnSelecting="odsListParent_Selecting">
    <SelectParameters>
        <asp:Parameter DefaultValue="True" Name="Full" Type="Boolean" />
        <asp:Parameter DefaultValue="" Name="IDSection" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>

<asp:FormView ID="fvTable" runat="server" DataKeyNames="IDSection" DataSourceID="odsTable" OnModeChanging="fvTable_ModeChanging">
    <ItemTemplate>
        <table class="tabSelectSection">
            <caption>
                <asp:Label ID="lbProject" runat="server" Text="<%# this.caption %>" CssClass="titleSection"></asp:Label></caption>
            <tr>
                <td colspan="3" class="Upr">
                    <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsUpdDel" ConfirmDelete="MessageDeleteSection" OnInit="pnSelect_Init" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltSection" runat="server" Text="<%$ Resources:ResourceBase, nfSection %>"></asp:Literal>:&nbsp;</th>
                <td><b>На русском:</b><br />
                    <%# Eval("Section") %></td>
                <td><b>In English:</b><br />
                    <%# Eval("SectionEng") %></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltSectionFull" runat="server" Text="<%$ Resources:ResourceBase, nfSectionFull %>"></asp:Literal>:&nbsp;</th>
                <td><b>На русском:</b><br />
                    <%# Eval("SectionFull") %></td>
                <td><b>In English:</b><br />
                    <%# Eval("SectionFullEng") %></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltTypeSection" runat="server" Text="<%$ Resources:ResourceBase, nfTypeSection %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2"><%# base.csection.GetTypeSection(Container.DataItem) %></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltCipher" runat="server" Text="<%$ Resources:ResourceBase, nfSectionCipher %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2"><%# Eval("Cipher") %></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltParent" runat="server" Text="<%$ Resources:ResourceBase, nfSectionParent %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2"><%# base.csection.GetParentSection(Container.DataItem) %></td>
            </tr>
        </table>
    </ItemTemplate>
    <EditItemTemplate>
        <table class="tabUpdateSection">
            <tr>
                <td colspan="3" class="Upr">
                    <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="UpdCan" OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick" Change="true" ValidationGroup="Section" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltSection" runat="server" Text="<%$ Resources:ResourceBase, nfSection %>"></asp:Literal>:&nbsp;</th>
                <td><b>На русском:</b><br />
                    <asp:TextBox ID="tbSection" runat="server" Text='<%# Eval("Section") %>' CssClass="MultiText" TextMode="MultiLine"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revSection" runat="server" ControlToValidate="tbSection" ValidationGroup="Section"
                        ValidationExpression="[0-9а-яА-ЯёЁa-zA-Z\s\(\)\.\#\:\[\]$\&\-]{1,100}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_section %>"
                        Display="dynamic" CssClass="failure" Enabled="true">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvSection" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                        ControlToValidate="tbSection" CssClass="failure" ValidationGroup="Section"></asp:RequiredFieldValidator></td>
                <td><b>In English:</b><br />
                    <asp:TextBox ID="tbSectionEng" runat="server" Text='<%# Eval("SectionEng") %>' CssClass="MultiText" TextMode="MultiLine"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revSectionEng" runat="server" ControlToValidate="tbSectionEng" ValidationGroup="Section"
                        ValidationExpression="[0-9a-zA-Z\s\(\)\.\#\:\[\]$\&\-]{1,100}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_sectioneng %>"
                        Display="dynamic" CssClass="failure" Enabled="true">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvSectionEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                        ControlToValidate="tbSectionEng" CssClass="failure" ValidationGroup="Section"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltSectionFull" runat="server" Text="<%$ Resources:ResourceBase, nfSectionFull %>"></asp:Literal>:&nbsp;</th>
                <td><b>На русском:</b><br />
                    <asp:TextBox ID="tbSectionFull" runat="server" Text='<%# Eval("SectionFull") %>' CssClass="MultiText" TextMode="MultiLine"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revSectionFull" runat="server" ControlToValidate="tbSectionFull" ValidationGroup="Section"
                        ValidationExpression="[0-9а-яА-ЯёЁa-zA-Z\s\(\)\.\#\:\[\]$\&\-]{1,1000}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_section %>"
                        Display="dynamic" CssClass="failure" Enabled="true">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvSectionFull" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                        ControlToValidate="tbSectionFull" CssClass="failure" ValidationGroup="Section"></asp:RequiredFieldValidator>
                </td>
                <td><b>In English:</b><br />
                    <asp:TextBox ID="tbSectionFullEng" runat="server" Text='<%# Eval("SectionFullEng") %>' CssClass="MultiText" TextMode="MultiLine"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revSectionFullEng" runat="server" ControlToValidate="tbSectionFullEng" ValidationGroup="Section"
                        ValidationExpression="[0-9a-zA-Z\s\(\)\.\#\:\[\]$\&\-]{1,1000}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_sectioneng %>"
                        Display="dynamic" CssClass="failure" Enabled="true">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvSectionFullEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                        ControlToValidate="tbSectionFullEng" CssClass="failure" ValidationGroup="Section"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltTypeSection" runat="server" Text="<%$ Resources:ResourceBase, nfTypeSection %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2">
                    <asp:DropDownList ID="ddlTypeSection" runat="server" OnDataBound="ddlTypeSection_DataBound" CssClass="LineText"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltCipher" runat="server" Text="<%$ Resources:ResourceBase, nfSectionCipher %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2">
                    <asp:TextBox ID="tbCipher" runat="server" CssClass="LineText" TextMode="Number" Text='<%# Eval("Cipher") %>' ></asp:TextBox></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltParent" runat="server" Text="<%$ Resources:ResourceBase, nfSectionParent %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2">
                    <asp:DropDownList ID="ddlListParent" runat="server" DataSourceID="odsListParent" DataTextField="Section" DataValueField="IDSection" CssClass="LineText" OnDataBound="ddlListParent_DataBound"></asp:DropDownList></td>
            </tr>
        </table>
    </EditItemTemplate>
    <InsertItemTemplate>
        <table class="tabInsertSection">
            <tr>
                <td colspan="3" class="Upr">
                    <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsCan" OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick" Change="true" ValidationGroup="Section" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltSection" runat="server" Text="<%$ Resources:ResourceBase, nfSection %>"></asp:Literal>:&nbsp;</th>
                <td><b>На русском:</b><br />
                    <asp:TextBox ID="tbSection" runat="server" CssClass="MultiText" TextMode="MultiLine"></asp:TextBox>
                    <%--<asp:RegularExpressionValidator ID="revSection" runat="server" ControlToValidate="tbSection" ValidationGroup="Section"
                        ValidationExpression="[0-9а-яА-ЯёЁa-zA-Z]{1,100}\s{1,100}\S{1,100}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_section %>"
                        Display="dynamic" CssClass="failure" Enabled="true">
                    </asp:RegularExpressionValidator>--%>
                    <asp:RequiredFieldValidator ID="rfvSection" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                        ControlToValidate="tbSection" CssClass="failure" ValidationGroup="Section"></asp:RequiredFieldValidator></td>
                <td><b>In English:</b><br />
                    <asp:TextBox ID="tbSectionEng" runat="server" CssClass="MultiText" TextMode="MultiLine"></asp:TextBox>
                    <%--<asp:RegularExpressionValidator ID="revSectionEng" runat="server" ControlToValidate="tbSectionEng" ValidationGroup="Section"
                        ValidationExpression="[0-9a-zA-Z\s\№\(\)\.\#\:\[\]$\&\-]{1,100}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_sectioneng %>"
                        Display="dynamic" CssClass="failure" Enabled="true">
                    </asp:RegularExpressionValidator>--%>
                    <asp:RequiredFieldValidator ID="rfvSectionEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                        ControlToValidate="tbSectionEng" CssClass="failure" ValidationGroup="Section"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltSectionFull" runat="server" Text="<%$ Resources:ResourceBase, nfSectionFull %>"></asp:Literal>:&nbsp;</th>
                <td><b>На русском:</b><br />
                    <asp:TextBox ID="tbSectionFull" runat="server" CssClass="MultiText" TextMode="MultiLine"></asp:TextBox>
                    <%--<asp:RegularExpressionValidator ID="revSectionFull" runat="server" ControlToValidate="tbSectionFull" ValidationGroup="Section"
                        ValidationExpression="[0-9а-яА-ЯёЁa-zA-Z\s\№\(\)\.\#\:\[\]$\&\-]{1,1000}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_section %>"
                        Display="dynamic" CssClass="failure" Enabled="true">
                    </asp:RegularExpressionValidator>--%>
                    <asp:RequiredFieldValidator ID="rfvSectionFull" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                        ControlToValidate="tbSectionFull" CssClass="failure" ValidationGroup="Section"></asp:RequiredFieldValidator>
                </td>
                <td><b>In English:</b><br />
                    <asp:TextBox ID="tbSectionFullEng" runat="server" CssClass="MultiText" TextMode="MultiLine"></asp:TextBox>
                    <%--<asp:RegularExpressionValidator ID="revSectionFullEng" runat="server" ControlToValidate="tbSectionFullEng" ValidationGroup="Section"
                        ValidationExpression="[0-9a-zA-Z\s\№\(\)\.\#\:\[\]$\&\-]{1,1000}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_sectioneng %>"
                        Display="dynamic" CssClass="failure" Enabled="true">
                    </asp:RegularExpressionValidator>--%>
                    <asp:RequiredFieldValidator ID="rfvSectionFullEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                        ControlToValidate="tbSectionFullEng" CssClass="failure" ValidationGroup="Section"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltTypeSection" runat="server" Text="<%$ Resources:ResourceBase, nfTypeSection %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2">
                    <asp:DropDownList ID="ddlTypeSection" runat="server" OnDataBound="ddlTypeSection_DataBound" CssClass="LineText"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltCipher" runat="server" Text="<%$ Resources:ResourceBase, nfSectionCipher %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2">
                    <asp:TextBox ID="tbCipher" runat="server" CssClass="LineText" TextMode="Number"></asp:TextBox></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltParent" runat="server" Text="<%$ Resources:ResourceBase, nfSectionParent %>"></asp:Literal>:&nbsp;</th>
                <td colspan="2">
                    <asp:DropDownList ID="ddlListParent" runat="server" DataSourceID="odsListParent" DataTextField="Section" DataValueField="IDSection" CssClass="LineText" OnDataBound="ddlListParent_DataBound"></asp:DropDownList></td>
            </tr>
        </table>
    </InsertItemTemplate>
</asp:FormView>
