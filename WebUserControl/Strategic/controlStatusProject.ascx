<%@ Control Language="C#" AutoEventWireup="true" CodeFile="controlStatusProject.ascx.cs" Inherits="controlStatusProject" %>
<%@ Register TagPrefix="wuc" TagName="controlProject" Src="~/WebUserControl/Strategic/controlProject.ascx" %>
<%@ Register TagPrefix="wuc" TagName="controlDetaliProject" Src="~/WebUserControl/Strategic/controlDetaliProject.ascx" %>

<style type="text/css">
table.tabDetaliProject {
    border: 1px solid #999999;
    color: #000000;
    border-radius: 3px;
    background-color: #F7F6F3;
    font-family: 'Arial Narrow';
    width: auto;
    margin-top: 20px;
    font-size: 14px;
}
.tabDetaliProject th {
    padding: 5px;
    border: 1px solid #999999;
    color: #5B5B5B;
    font-weight: bold;
    word-wrap: hyphenate;
    text-align: right;
    text-transform: uppercase;
    background-color: #F7F6F3;
    width: 200px;
}
.tabDetaliProject td {
    padding: 5px;
    border: 1px solid #999999;
    color: #000000;
    text-align: left;
    background-color: #FFFFFF;
    font-style: italic;
    width: 300px;
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
<%--    <asp:ObjectDataSource ID="odsTable" runat="server" SelectMethod="SelectCultureProject" TypeName="Strategic.classProject" OnSelecting="odsTable_Selecting" OnSelected="odsTable_Selected">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="IDProject" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>--%>
<asp:ImageButton ID="ibReturnList" runat="server" ImageUrl="~/WebSite/Strategic/image/Return.png" ToolTip="<%$ Resources:ResourceStrategic, spToolTipReturnList %>" OnClick="ibReturnList_Click" OnClientClick="<%$ Resources:Resource, MessageLock %>" />
<%--<asp:ListView ID="lvTable" runat="server" DataSourceID="odsTable" DataKeyNames="IDProject" >
    <LayoutTemplate>
        <table class="tabDetaliProject">
            <caption><asp:Label ID="lbCaption" runat="server" Text="<%$ Resources:ResourceStrategic, titleCardProject %>" CssClass="title"></asp:Label></caption>
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
        <tr><td><%# Eval("Name") %></td></tr>
        <tr><td><%# base.cproject.GetEffect(Container.DataItem)%></td></tr>
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
        <tr><td><b>
                <asp:Literal ID="ltOrder" runat="server" Text="<%$ Resources:ResourceStrategic, nfOrder %>"></asp:Literal>:&nbsp;</b><%# base.corder.GetNumDateOrder(Container.DataItem)%></td></tr>
    </ItemTemplate>
</asp:ListView>--%>
<wuc:controlproject id="controlProject" runat="server" outinfotext="true" />
    <%--ondatainserted="controlProject_DataInserted" ondataupdated="controlProject_DataUpdated" ondatadeleted="controlProject_DataDeleted" onstepcreateclick="controlProject_StepCreateClick" onstepclearclick="controlProject_StepClearClick" --%>
<wuc:controlDetaliProject ID="controlDetaliProject" runat="server" OutInfoText="false" />
