<%@ Control Language="C#" AutoEventWireup="true" CodeFile="controlProject.ascx.cs" Inherits="controlProject" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>
<%@ Register TagPrefix="wuc" TagName="panelFile" Src="~/WebUserControl/panelFile.ascx" %>

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
.yes {
        color: green
    }

.no {
        color: red
    }
.title
{
    font-family: Georgia, Times, 'Times New Roman' , serif;
    font-size: 24px;
    color: #808000;
    text-align: center;
}
</style>

<script type="text/javascript">
    // Установить статус проекта в зависимости от CheckBox финансирование
    function ShowStatus(obj) {
        var ddlStatus = document.getElementById('<%=base.fmc.GetClientIDDropDownList(fvTable,"ddlStatus") %>');
        if (ddlStatus) {
            
            if (ddlStatus.selectedIndex <= "1") {

                if (!obj.checked) {
                    ddlStatus.selectedIndex = 0;
                }
                else {
                    ddlStatus.selectedIndex = 1;
                }
            }
        }
    }
    // Установить CheckBox финансирование в завистмости от статуса проекта
    function ShowAllocationFunds(obj) {
        var cbAllocationFunds = document.getElementById('<%=base.fmc.GetClientIDCheckBox(fvTable,"cbAllocationFunds") %>');
        if (cbAllocationFunds) {
            if (obj) {
                if (obj.selectedIndex == "0") { cbAllocationFunds.checked = false;}
                if (obj.selectedIndex == "1") { cbAllocationFunds.checked = true;}
            }
        }
    }
</script>
<asp:ObjectDataSource ID="odsTableCulture" runat="server" SelectMethod="SelectCultureProject" TypeName="Strategic.classProject" OnSelecting="odsTable_Selecting" OnSelected="odsTable_Selected">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="IDProject" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsTable" runat="server" SelectMethod="SelectProject" TypeName="Strategic.classProject" OnSelecting="odsTable_Selecting" OnInserted="odsTable_Inserted" OnInserting="odsTable_Inserting" OnUpdated="odsTable_Updated" OnUpdating="odsTable_Updating" OnDeleted="odsTable_Deleted" OnDeleting="odsTable_Deleting" OnSelected="odsTable_Selected" UpdateMethod="UpdateProject" InsertMethod="InsertProject" DeleteMethod="DeleteProject">
    <DeleteParameters>
        <asp:Parameter Name="IDProject" Type="Int32" />
        <asp:Parameter Name="FullDelete" Type="Boolean" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IDTemplateStepProject" Type="Int32" />
        <asp:Parameter Name="IDImplementationProgram" Type="Int32" />
        <asp:Parameter Name="IDTypeProject" Type="Int32" />
        <asp:Parameter Name="IDMenagerProject" Type="Int32" />
        <asp:Parameter Name="IDReplacementProject" Type="Int32" />
        <asp:Parameter Name="IDSection" Type="Int32" />
        <asp:Parameter Name="SAPCode" Type="String" />
        <asp:Parameter Name="TypeString" Type="String" />
        <asp:Parameter Name="TypeStatus" Type="Int32" />
        <asp:Parameter Name="Funding" Type="Decimal" />
        <asp:Parameter Name="Currency" Type="Int32" />
        <asp:Parameter Name="FundingDescription" Type="String" />
        <asp:Parameter Name="AllocationFunds" Type="Boolean" />
        <asp:Parameter Name="LineOwner" Type="Int32" />
        <asp:Parameter Name="Year" Type="Int32" />
        <asp:Parameter Name="Name" Type="String" />
        <asp:Parameter Name="NameEng" Type="String" />
        <asp:Parameter Name="Description" Type="String" />
        <asp:Parameter Name="DescriptionEng" Type="String" />
        <asp:Parameter Name="Contractor" Type="String" />
        <asp:Parameter Name="DateContractor" Type="String" />
        <asp:Parameter Name="Effect" Type="Int32" />
        <asp:Parameter Name="Status" Type="Int32" />
        <asp:Parameter Name="IDOrder" Type="Int32" />
        <asp:Parameter Name="TypeConstruction" Type="Int32" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </InsertParameters>
    <SelectParameters>
        <asp:Parameter Name="IDProject" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="IDProject" Type="Int32" />
        <asp:Parameter Name="IDTypeProject" Type="Int32" />
        <asp:Parameter Name="IDImplementationProgram" Type="Int32" />
        <asp:Parameter Name="IDMenagerProject" Type="Int32" />
        <asp:Parameter Name="IDReplacementProject" Type="Int32" />
        <asp:Parameter Name="IDSection" Type="Int32" />
        <asp:Parameter Name="SAPCode" Type="String" />
        <asp:Parameter Name="TypeString" Type="String" />
        <asp:Parameter Name="TypeStatus" Type="Int32" />
        <asp:Parameter Name="Funding" Type="Decimal" />
        <asp:Parameter Name="Currency" Type="Int32" />
        <asp:Parameter Name="FundingDescription" Type="String" />
        <asp:Parameter Name="AllocationFunds" Type="Boolean" />
        <asp:Parameter Name="LineOwner" Type="Int32" />
        <asp:Parameter Name="Year" Type="Int32" />
        <asp:Parameter Name="Name" Type="String" />
        <asp:Parameter Name="NameEng" Type="String" />
        <asp:Parameter Name="Description" Type="String" />
        <asp:Parameter Name="DescriptionEng" Type="String" />
        <asp:Parameter Name="Contractor" Type="String" />
        <asp:Parameter Name="DateContractor" Type="String" />
        <asp:Parameter Name="Effect" Type="Int32" />
        <asp:Parameter Name="Status" Type="Int32" />
        <asp:Parameter Name="IDOrder" Type="Int32" />
        <asp:Parameter Name="TypeConstruction" Type="Int32" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </UpdateParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsListTypeProject" runat="server" SelectMethod="SelectCultureTypeProject" TypeName="Strategic.classProject"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsListMenager" runat="server" SelectMethod="ListMenagerProject" TypeName="Strategic.classMenagerProject">
    <SelectParameters>
        <asp:Parameter Name="IDMenagerProject" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsListActingMenager" runat="server" SelectMethod="DDLListMenagerProject" TypeName="Strategic.classMenagerProject" OnSelecting="odsListActingMenager_Selecting">
    <SelectParameters>
        <asp:Parameter Name="IDMenagerProject" Type="Int32" />
        <asp:Parameter Name="value" Type="String"/>
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsListSection" runat="server" SelectMethod="DDLListSectionCulture" TypeName="WebBase.classSection">
    <SelectParameters>
        <asp:Parameter DefaultValue="true" Name="Full" Type="Boolean" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsListTemplate" runat="server" SelectMethod="SelectCultureTemplatesSteps" TypeName="Strategic.classTemplatesSteps"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsListOrder" runat="server" SelectMethod="SelectCultureListOrder" TypeName="Strategic.classOrder"></asp:ObjectDataSource>
<div class="panelTable">
    <asp:ListView ID="lvTable" runat="server" DataSourceID="odsTableCulture" DataKeyNames="IDProject">
        <LayoutTemplate>
            <table class="tabDetaliProject">
                <caption>
                    <asp:Label ID="lbCaption" runat="server" Text="<%$ Resources:ResourceStrategic, titleCardProject %>" CssClass="title"></asp:Label></caption>
                <tr id="itemPlaceholder" runat="server" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <th rowspan="3">
                    <asp:Literal ID="ltProject" runat="server" Text="<%$ Resources:ResourceStrategic, spStringProject %>"></asp:Literal>:&nbsp;</th>
                <td><%# base.csection.GetSection(Container.DataItem, true) %></td>
                <td colspan="3" rowspan="3"><%# Eval("Description") %></td>
            </tr>
            <tr>
                <td><%# Eval("Name") %></td>
            </tr>
            <tr>
                <td><%# base.cproject.GetEffect(Container.DataItem)%></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltFinancing" runat="server" Text="<%$ Resources:ResourceStrategic, spStringFinancing %>"></asp:Literal>:&nbsp;</th>
                <td><%# base.cproject.GetTypeProject(Container.DataItem) %></td>
                <td><b>
                    <asp:Literal ID="ltSAPCode" runat="server" Text="<%$ Resources:ResourceStrategic, nfSAPCode %>"></asp:Literal>:&nbsp;</b><%# Eval("SAPCode") %></td>
                <td><b>
                    <asp:Literal ID="ltSection" runat="server" Text="<%$ Resources:ResourceStrategic, nfLineOwner %>"></asp:Literal>:&nbsp;</b><%# base.csection.GetLineOwner(Container.DataItem) %></td>
                <td>
                    <asp:Label ID="lbFunding" runat="server" Text='<%# Eval("Funding", "{0:###,###,##0.00}") %>' CssClass='<%# base.GetStringBool(Container.DataItem,"AllocationFunds","yes","no") %>'></asp:Label>
                    &nbsp;<%# base.cproject.GetCurrency(Container.DataItem) %>&nbsp; <%# Eval("FundingDescription") %>
                &nbsp;<asp:Label ID="lbAllocationFunds" runat="server" Text='<%# base.cproject.GetAllocationFunds(Container.DataItem) %>' CssClass='<%# base.GetStringBool(Container.DataItem,"AllocationFunds","yes","no") %>'></asp:Label>

                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltImplementation" runat="server" Text="<%$ Resources:ResourceStrategic, spStringImplementation %>"></asp:Literal>:&nbsp;</th>
                <td><b>
                    <asp:Literal ID="ltTypeConstruction" runat="server" Text="<%$ Resources:ResourceStrategic, nfTypeConstruction %>"></asp:Literal>:&nbsp;</b><%# base.cproject.GetTypeConstruction(Container.DataItem) %></td>
                <td><b>
                    <asp:Literal ID="ltNameYare" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameYare %>"></asp:Literal>:&nbsp;</b><%# Eval("Year") %></td>
                <td><%# base.cproject.GetTypeStatus(Container.DataItem) %></td>
                <td><b>
                    <asp:Literal ID="ltNameStatus" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameStatus %>"></asp:Literal>:&nbsp;</b><%# base.cproject.GetStatusProject(Container.DataItem) %></td>
            </tr>
            <tr>
                <th rowspan="2">
                    <asp:Literal ID="ltExecutor" runat="server" Text="<%$ Resources:ResourceStrategic, spStringExecutor %>"></asp:Literal>:&nbsp;</th>
                <td><%# base.cproject.GetImplementationProgram(Container.DataItem) %></td>
                <td rowspan="2"><b>
                    <asp:Literal ID="ltNameMenager" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameMenager %>"></asp:Literal>:&nbsp;</b><br />
                    <%# base.cmenager.GetFIO(Container.DataItem) %>&nbsp;<a href='mailto:<%# base.cmenager.GetEmail(Container.DataItem) %>'>
                        <asp:Literal ID="lttxtemmail" runat="server" Text='<%# base.cmenager.GetEmail(Container.DataItem) %>'></asp:Literal></a></td>
                <td rowspan="2"><b>
                    <asp:Literal ID="ltContractor" runat="server" Text="<%$ Resources:ResourceStrategic, nfContractor %>"></asp:Literal>:&nbsp;</b><br />
                    <%# Eval("Contractor") %></td>
                <td rowspan="2"><b>
                    <asp:Literal ID="ltDateContractor" runat="server" Text="<%$ Resources:ResourceStrategic, nfDateContractor %>"></asp:Literal>:&nbsp;</b><%# Eval("DateContractor") %></td>
            </tr>
            <tr>
                <td><b>
                    <asp:Literal ID="ltOrder" runat="server" Text="<%$ Resources:ResourceStrategic, nfOrder %>"></asp:Literal>:&nbsp;</b><%# base.corder.GetNumDateOrder(Container.DataItem)%></td>
            </tr>
        </ItemTemplate>
    </asp:ListView>

    <asp:FormView ID="fvTable" runat="server" DataKeyNames="IDProject" DataSourceID="odsTable" OnDataBound="fvTable_DataBound" OnModeChanging="fvTable_ModeChanging">
        <ItemTemplate>
            <table class="tabSelect">
                <caption>
                    <asp:Label ID="lbProject" runat="server" Text="<%$ Resources:ResourceStrategic, titleCardProject %>" CssClass="title"></asp:Label></caption>
                <tr>
                    <td colspan="3" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsUpdDelClrCrt" ConfirmDelete="MessageToArhivProject" OnInit="pnSelect_Init"
                            ConfirmStepClear="MessageClearStepProject" ConfirmStepCreate="MessageCreateStepProject" OnStepCreateClick="pnSelect_StepCreateClick" OnStepClearClick="pnSelect_StepClearClick" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltNameProject" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameProject %>"></asp:Literal>:&nbsp;</th>
                    <td><b>На русском:</b><br />
                        <%# Eval("Name") %></td>
                    <td><b>In English:</b><br />
                        <%# Eval("NameEng") %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltSection" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameSection %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# base.csection.GetSection(Container.DataItem, false) %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeProject" runat="server" Text="<%$ Resources:ResourceStrategic, nfTypeProject  %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# base.cproject.GetTypeProject(Container.DataItem) %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltProgram" runat="server" Text="<%$ Resources:ResourceStrategic, nfImplementationProgram %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# base.cproject.GetImplementationProgram(Container.DataItem) %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeConstruction" runat="server" Text="<%$ Resources:ResourceStrategic, nfTypeConstruction %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# base.cproject.GetTypeConstruction(Container.DataItem) %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltSAPCode" runat="server" Text="<%$ Resources:ResourceStrategic, nfSAPCode %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# Eval("SAPCode") %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltLineOwner" runat="server" Text="<%$ Resources:ResourceStrategic, nfLineOwner %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# base.csection.GetLineOwner(Container.DataItem) %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeString" runat="server" Text="<%$ Resources:ResourceStrategic, nfTypeString %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# Eval("TypeString") %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltNameYare" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameYare %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# Eval("Year") %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeStatus" runat="server" Text="<%$ Resources:ResourceStrategic, nfTypeStatus %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# base.cproject.GetTypeStatus(Container.DataItem) %></td>
                </tr>
                <tr>
                    <th rowspan="3">
                        <asp:Literal ID="ltNameMenager" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameMenager %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# base.cmenager.GetFIO(Container.DataItem) %></td>
                </tr>
                <tr>
                    <td colspan="2"><a href='mailto:<%# base.cmenager.GetEmail(Container.DataItem) %>'>
                        <asp:Literal ID="lttxtemmail" runat="server" Text='<%# base.cmenager.GetEmail(Container.DataItem) %>'></asp:Literal></a></td>
                </tr>
                <tr>
                    <td colspan="2"><%# base.cmenager.GetWPhone(Container.DataItem, this.wsignature) %> &nbsp;&nbsp;<%# base.cmenager.GetMPhone(Container.DataItem, this.msignature) %></td>
                </tr>
                <tr>
                    <th rowspan="3">
                        <asp:Literal ID="ltActingNameMenager" runat="server" Text="<%$ Resources:ResourceStrategic, nfActingMenager %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# base.cmenager.GetReplacementFIO(Container.DataItem) %></td>
                </tr>
                <tr>
                    <td colspan="2"><a href='mailto:<%# base.cmenager.GetReplacementEmail(Container.DataItem) %>'>
                        <asp:Literal ID="lttxtActingemmail" runat="server" Text='<%# base.cmenager.GetReplacementEmail(Container.DataItem) %>'></asp:Literal></a></td>
                </tr>
                <tr>
                    <td colspan="2"><%# base.cmenager.GetReplacementWPhone(Container.DataItem, this.wsignature) %> &nbsp;&nbsp;<%# base.cmenager.GetReplacementMPhone(Container.DataItem, this.msignature) %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltContractor" runat="server" Text="<%$ Resources:ResourceStrategic, nfContractor %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# Eval("Contractor") %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltDateContractor" runat="server" Text="<%$ Resources:ResourceStrategic, nfDateContractor %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# Eval("DateContractor") %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltProjectDescription" runat="server" Text="<%$ Resources:ResourceStrategic, tsProjectDescription %>"></asp:Literal>:&nbsp;</th>
                    <td><b>На русском:</b><br />
                        <%# Eval("Description") %></td>
                    <td><b>In English:</b><br />
                        <%# Eval("DescriptionEng") %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltNameFunding" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameFunding %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:Label ID="lbFunding" runat="server" Text='<%# Eval("Funding", "{0:###,###,##0.00}") %>' CssClass='<%# GetStringBool(Container.DataItem,"AllocationFunds","yes","no") %>'></asp:Label>
                        &nbsp;<%# base.cproject.GetCurrency(Container.DataItem) %>&nbsp; <%# Eval("FundingDescription") %>
                        &nbsp;<%# base.cproject.GetAllocationFunds(Container.DataItem) %></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltEffect" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameEffect %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# base.cproject.GetEffect(Container.DataItem)%></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltOrder" runat="server" Text="<%$ Resources:ResourceStrategic, nfOrder %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# base.corder.GetNumDateOrder(Container.DataItem)%></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltNameStatus" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameStatus %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2"><%# base.cproject.GetStatusProject(Container.DataItem) %></td>
                </tr>
            </table>
        </ItemTemplate>
        <EditItemTemplate>
            <table class="tabUpdate">
                <tr>
                    <td colspan="3" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="UpdCan" OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick" Change="true" ValidationGroup="Project" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltNameProject" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameProject %>"></asp:Literal>:&nbsp;</th>
                    <td><b>На русском:</b><br />
                        <asp:TextBox ID="tbName" runat="server" Text='<%# Eval("Name") %>' CssClass="MultiText" TextMode="MultiLine"></asp:TextBox>
                        <%--                        <asp:RegularExpressionValidator ID="revName" runat="server" ControlToValidate="tbName" ValidationGroup="Project"
                            ValidationExpression="[а-яА-ЯёЁa-zA-Z0-9]" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="true">
                        </asp:RegularExpressionValidator>--%>
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbName" CssClass="failure" ValidationGroup="Project"></asp:RequiredFieldValidator></td>
                    <td><b>In English:</b><br />
                        <asp:TextBox ID="tbNameEng" runat="server" Text='<%# Eval("NameEng") %>' CssClass="MultiText" TextMode="MultiLine"></asp:TextBox>
                        <%--                        <asp:RegularExpressionValidator ID="revNameEng" runat="server" ControlToValidate="tbNameEng" ValidationGroup="Project"
                            ValidationExpression="[а-яА-ЯёЁ]" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescriptionEng %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator>--%>
                        <asp:RequiredFieldValidator ID="rfvNameEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbNameEng" CssClass="failure" ValidationGroup="Project"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltSection" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameSection %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlSection" runat="server" AutoPostBack="True" DataSourceID="odsListSection" DataTextField="Section" DataValueField="IDSection" OnDataBound="ddlSection_DataBound" CssClass="LineText"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeProject" runat="server" Text="<%$ Resources:ResourceStrategic, nfTypeProject %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlTypeProject" runat="server" AutoPostBack="True" DataSourceID="odsListTypeProject" DataTextField="TypeProject" DataValueField="IDTypeProject" OnDataBound="ddlTypeProject_DataBound" CssClass="LineText"></asp:DropDownList></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltProgram" runat="server" Text="<%$ Resources:ResourceStrategic, nfImplementationProgram %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlImplementationProgram" runat="server" OnDataBound="ddlImplementationProgram_DataBound" CssClass="LineText"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeConstruction" runat="server" Text="<%$ Resources:ResourceStrategic, nfTypeConstruction %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlTypeConstruction" runat="server" OnDataBound="ddlTypeConstruction_DataBound" CssClass="LineText"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltSAPCode" runat="server" Text="<%$ Resources:ResourceStrategic, nfSAPCode %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:TextBox ID="tbSAPCode" runat="server" Text='<%# Bind("SAPCode")%>' TextMode="SingleLine" CssClass="LineText"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltLineOwner" runat="server" Text="<%$ Resources:ResourceStrategic, nfLineOwner %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlLineOwner" runat="server" AutoPostBack="True" DataSourceID="odsListSection" DataTextField="Section" DataValueField="IDSection" OnDataBound="ddlLineOwner_DataBound" CssClass="LineText"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeString" runat="server" Text="<%$ Resources:ResourceStrategic, nfTypeString %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:TextBox ID="tbTypeString" runat="server" Text='<%# Bind("TypeString")%>' TextMode="SingleLine" CssClass="LineText"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltNameYare" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameYare %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlYear" runat="server" OnDataBound="ddlYear_DataBound"></asp:DropDownList></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeStatus" runat="server" Text="<%$ Resources:ResourceStrategic, nfTypeStatus %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlTypeStatus" runat="server" AutoPostBack="True" OnDataBound="ddlTypeStatus_DataBound">
                            <asp:ListItem Value="0" Text="New"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Carryover"></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltNameMenager" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameMenager %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlNameMenager" runat="server" AutoPostBack="True" DataSourceID="odsListMenager" DataTextField="UserEnterprise" DataValueField="IDMenagerProject" OnDataBound="ddlNameMenager_DataBound" CssClass="LineText"></asp:DropDownList></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltActingNameMenager" runat="server" Text="<%$ Resources:ResourceStrategic, nfActingMenager %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlActingNameMenager" runat="server" AutoPostBack="True" DataSourceID="odsListActingMenager" DataTextField="UserEnterprise" DataValueField="IDMenagerProject" OnDataBound="ddlActingNameMenager_DataBound" CssClass="LineText"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltContractor" runat="server" Text="<%$ Resources:ResourceStrategic, nfContractor %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:TextBox ID="tbContractor" runat="server" Text='<%# Eval("Contractor") %>' CssClass="MultiText" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltDateContractor" runat="server" Text="<%$ Resources:ResourceStrategic, nfDateContractor %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:TextBox ID="tbDateContractor" runat="server" Text='<%# Eval("DateContractor") %>' CssClass="MultiText" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltProjectDescription" runat="server" Text="<%$ Resources:ResourceStrategic, tsProjectDescription %>"></asp:Literal>:&nbsp;</th>
                    <td><b>На русском:</b><br />
                        <asp:TextBox ID="tbDescription" runat="server" Text='<%# Eval("Description") %>' CssClass="MultiText" TextMode="MultiLine" Height="200"></asp:TextBox>
                        <%--<asp:RegularExpressionValidator ID="revDescription" runat="server" ControlToValidate="tbDescription" ValidationGroup="Project"
                            ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,2048}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="true">
                        </asp:RegularExpressionValidator>--%>
                        <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbDescription" CssClass="failure" ValidationGroup="Project"></asp:RequiredFieldValidator></td>
                    <td><b>In English:</b><br />
                        <asp:TextBox ID="tbDescriptionEng" runat="server" Text='<%# Eval("DescriptionEng") %>' CssClass="MultiText" TextMode="MultiLine" Height="200"></asp:TextBox>
                        <%--<asp:RegularExpressionValidator ID="revDescriptionEng" runat="server" ControlToValidate="tbDescriptionEng" ValidationGroup="Project"
                            ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,2048}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="true">
                        </asp:RegularExpressionValidator>--%>
                        <asp:RequiredFieldValidator ID="rfvDescriptionEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbDescriptionEng" CssClass="failure" ValidationGroup="Project"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltNameFunding" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameFunding %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:TextBox ID="tbFunding" runat="server" Width="100px" Text='<%# Bind("Funding")%>'></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revFunding" runat="server" ControlToValidate="tbFunding" ValidationGroup="Project"
                            ValidationExpression="(^\d*\.?\d*[1-9]+\d*$)|(^[1-9]+\d*\.\d*$)|(^\d*\,?\d*[1-9]+\d*$)|(^[1-9]+\d*\,\d*$)" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_decimal %>"
                            Display="dynamic" CssClass="failure" Enabled="true">
                        </asp:RegularExpressionValidator>
                        &nbsp;<asp:DropDownList ID="ddlCurrency" runat="server" OnDataBound="ddlCurrency_DataBound"></asp:DropDownList>
                        &nbsp;<asp:TextBox ID="tbFundingDescription" runat="server" Width="200px" Text='<%# Bind("FundingDescription")%>' TextMode="SingleLine"></asp:TextBox>
                        &nbsp;<asp:CheckBox ID="cbAllocationFunds" runat="server" Checked='<%# Bind("AllocationFunds")%>' Text="<%$ Resources:ResourceStrategic, nfAllocationFunds %>" onclick="ShowStatus(this)" />
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltEffect" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameEffect %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <wuc:panelFile ID="panelFile" runat="server" OnInit="panelUnloadFile_Init" Width="300" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltOrder" runat="server" Text="<%$ Resources:ResourceStrategic, nfOrder %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlListOrder" runat="server" DataSourceID="odsListOrder" DataTextField="Order" DataValueField="IDOrder" CssClass="LineText" OnDataBound="ddlListOrder_DataBound"></asp:DropDownList></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltNameStatus" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameStatus %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlStatus" runat="server" OnDataBound="ddlStatus_DataBound" onChange="ShowAllocationFunds(this);"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </EditItemTemplate>
        <InsertItemTemplate>
            <table class="tabInsert">
                <tr>
                    <td colspan="3" class="Upr">
                        <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsCan" OnInit="pnSelect_Init" OnCancelClick="pnSelect_CancelClick" Change="true" ValidationGroup="Project" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeTemplate" runat="server" Text="<%$ Resources:ResourceStrategic, nfTypeTemplate %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlTemplate" runat="server" AutoPostBack="True" DataSourceID="odsListTemplate" DataTextField="TemplateStep" DataValueField="IDTemplateStepProject" CssClass="LineText"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltNameProject" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameProject %>"></asp:Literal>:&nbsp;</th>
                    <td><b>На русском:</b><br />
                        <asp:TextBox ID="tbName" runat="server" CssClass="MultiText" TextMode="MultiLine"></asp:TextBox>
                        <%--<asp:RegularExpressionValidator ID="revName" runat="server" ControlToValidate="tbName" ValidationGroup="Project"
                            ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,1024}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="true">
                        </asp:RegularExpressionValidator>--%>
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbName" CssClass="failure" ValidationGroup="Project"></asp:RequiredFieldValidator></td>
                    <td><b>In English:</b><br />
                        <asp:TextBox ID="tbNameEng" runat="server" CssClass="MultiText" TextMode="MultiLine"></asp:TextBox>
                        <%--<asp:RegularExpressionValidator ID="revNameEng" runat="server" ControlToValidate="tbNameEng" ValidationGroup="Project"
                            ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,1024}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescriptionEng %>"
                            Display="dynamic" CssClass="failure">
                        </asp:RegularExpressionValidator>--%>
                        <asp:RequiredFieldValidator ID="rfvNameEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbNameEng" CssClass="failure" ValidationGroup="Project"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltSection" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameSection %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlSection" runat="server" AutoPostBack="True" DataSourceID="odsListSection" DataTextField="Section" DataValueField="IDSection" OnDataBound="ddlSection_DataBound" CssClass="LineText"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeProject" runat="server" Text="<%$ Resources:ResourceStrategic, nfTypeProject %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlTypeProject" runat="server" AutoPostBack="True" DataSourceID="odsListTypeProject" DataTextField="TypeProject" DataValueField="IDTypeProject" OnDataBound="ddlTypeProject_DataBound" CssClass="LineText"></asp:DropDownList></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltProgram" runat="server" Text="<%$ Resources:ResourceStrategic, nfImplementationProgram %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlImplementationProgram" runat="server" OnDataBound="ddlImplementationProgram_DataBound" CssClass="LineText"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeConstruction" runat="server" Text="<%$ Resources:ResourceStrategic, nfTypeConstruction %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlTypeConstruction" runat="server" OnDataBound="ddlTypeConstruction_DataBound" CssClass="LineText"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltSAPCode" runat="server" Text="<%$ Resources:ResourceStrategic, nfSAPCode %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:TextBox ID="tbSAPCode" runat="server" Text='<%# Bind("SAPCode")%>' TextMode="SingleLine" CssClass="LineText"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltLineOwner" runat="server" Text="<%$ Resources:ResourceStrategic, nfLineOwner %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlLineOwner" runat="server" AutoPostBack="True" DataSourceID="odsListSection" DataTextField="Section" DataValueField="IDSection" OnDataBound="ddlLineOwner_DataBound" CssClass="LineText"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeString" runat="server" Text="<%$ Resources:ResourceStrategic, nfTypeString %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:TextBox ID="tbTypeString" runat="server" TextMode="SingleLine" CssClass="LineText"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltNameYare" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameYare %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlYear" runat="server" OnDataBound="ddlYear_DataBound"></asp:DropDownList></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltTypeStatus" runat="server" Text="<%$ Resources:ResourceStrategic, nfTypeStatus %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlTypeStatus" runat="server" AutoPostBack="True" OnDataBound="ddlTypeStatus_DataBound">
                            <asp:ListItem Value="0" Text="New"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Carryover"></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltNameMenager" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameMenager %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlNameMenager" runat="server" AutoPostBack="True" DataSourceID="odsListMenager" DataTextField="UserEnterprise" DataValueField="IDMenagerProject" OnDataBound="ddlNameMenager_DataBound" CssClass="LineText"></asp:DropDownList></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltActingNameMenager" runat="server" Text="<%$ Resources:ResourceStrategic, nfActingMenager %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlActingNameMenager" runat="server" AutoPostBack="True" DataSourceID="odsListActingMenager" DataTextField="UserEnterprise" DataValueField="IDMenagerProject" OnDataBound="ddlActingNameMenager_DataBound" CssClass="LineText"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltContractor" runat="server" Text="<%$ Resources:ResourceStrategic, nfContractor %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:TextBox ID="tbContractor" runat="server" CssClass="MultiText" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltDateContractor" runat="server" Text="<%$ Resources:ResourceStrategic, nfDateContractor %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:TextBox ID="tbDateContractor" runat="server" CssClass="MultiText" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltProjectDescription" runat="server" Text="<%$ Resources:ResourceStrategic, tsProjectDescription %>"></asp:Literal>:&nbsp;</th>
                    <td><b>На русском:</b><br />
                        <asp:TextBox ID="tbDescription" runat="server" CssClass="MultiText" TextMode="MultiLine" Height="200"></asp:TextBox>
                        <%--<asp:RegularExpressionValidator ID="revDescription" runat="server" ControlToValidate="tbDescription" ValidationGroup="Project"
                            ValidationExpression="[0-9a-zA-Zа-яА-Я\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=\№]{1,2048}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="true">
                        </asp:RegularExpressionValidator>--%>
                        <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbDescription" CssClass="failure" ValidationGroup="Project"></asp:RequiredFieldValidator></td>
                    <td><b>In English:</b><br />
                        <asp:TextBox ID="tbDescriptionEng" runat="server" CssClass="MultiText" TextMode="MultiLine" Height="200"></asp:TextBox>
                        <%--<asp:RegularExpressionValidator ID="revDescriptionEng" runat="server" ControlToValidate="tbDescriptionEng" ValidationGroup="Project"
                            ValidationExpression="[0-9a-zA-Z\s\(\)\.\,\#\;\:\[\]\%\$\&\'\!\?\+\-\=]{1,2048}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_SiteDescription %>"
                            Display="dynamic" CssClass="failure" Enabled="true">
                        </asp:RegularExpressionValidator>--%>
                        <asp:RequiredFieldValidator ID="rfvDescriptionEng" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                            ControlToValidate="tbDescriptionEng" CssClass="failure" ValidationGroup="Project"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltNameFunding" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameFunding %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:TextBox ID="tbFunding" runat="server" Width="100px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revFunding" runat="server" ControlToValidate="tbFunding" ValidationGroup="Project"
                            ValidationExpression="(^\d*\.?\d*[1-9]+\d*$)|(^[1-9]+\d*\.\d*$)|(^\d*\,?\d*[1-9]+\d*$)|(^[1-9]+\d*\,\d*$)" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_decimal %>"
                            Display="dynamic" CssClass="failure" Enabled="true">
                        </asp:RegularExpressionValidator>
                        &nbsp;<asp:DropDownList ID="ddlCurrency" runat="server" OnDataBound="ddlCurrency_DataBound"></asp:DropDownList>
                        &nbsp;<asp:TextBox ID="tbFundingDescription" runat="server" Width="200px" TextMode="SingleLine"></asp:TextBox>
                        &nbsp;<asp:CheckBox ID="cbAllocationFunds" runat="server" Checked="false" Text="<%$ Resources:ResourceStrategic, nfAllocationFunds %>" onclick="ShowStatus(this)" />
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltEffect" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameEffect %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <wuc:panelFile ID="panelFile" runat="server" Width="300" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltOrder" runat="server" Text="<%$ Resources:ResourceStrategic, nfOrder %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlListOrder" runat="server" DataSourceID="odsListOrder" DataTextField="Order" DataValueField="IDOrder" CssClass="LineText" OnDataBound="ddlListOrder_DataBound"></asp:DropDownList></td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltNameStatus" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameStatus %>"></asp:Literal>:&nbsp;</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlStatus" runat="server" OnDataBound="ddlStatus_DataBound" onChange="ShowAllocationFunds(this);"></asp:DropDownList></td>
                </tr>
            </table>
        </InsertItemTemplate>
    </asp:FormView>
</div>
