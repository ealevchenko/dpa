<%@ Control Language="C#" AutoEventWireup="true" CodeFile="controlProgramProject.ascx.cs" Inherits="controlProgramProject" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>
<style type="text/css">
    table.tabList
    {
        border: 1px solid #999999;
        color: #000000;
        border-radius: 3px;
        background-color: #F7F6F3;
        font-family: 'Arial';
        width: 1700px;
        margin-top: 20px;
        font-size: 14px;
        margin-bottom: 20px;
    }

    .tabList th
    {
        padding: 5px 10px 5px 5px;
        border: 1px solid #999999;
        color: #5B5B5B;
        font-weight: bold;
        word-wrap: hyphenate;
        text-align: center;
        text-transform: uppercase;
        background-color: #F7F6F3;
        /*font-size: 16px;*/
    }

    th.departmen {
    font-family: 'Calibri';
    background-color: #FFEB9C;
    text-transform: uppercase;
    text-align: left;
    padding-left: 10px;
    /*font-size: 18px;*/
}

    .tabList td
    {
        padding: 5px;
        border: 1px solid #999999;
        font-size: 14px;
    }

    .tabList tr.item
    {
        background-color: #FFFFFF;
        color: #000000;
    }

    .tabList tr.close {
        background-color: #99FFCC;
        color: #000000;
    }

    th.name {
        width: 300px;
        background-color: #538DD5;
        color: #FFFFFF;
        text-transform: uppercase;
    }
    td.name
    {
        text-align: left;
    }
    th.order
    {
        width: 100px;
        background-color: #538DD5;
        color: #FFFFFF;
        text-transform: uppercase;
    }
    td.order
    {
        text-align: center;
    }
    th.construction
    {
        width: 100px;
        background-color: #538DD5;
        color: #FFFFFF;
        text-transform: uppercase;
    }
    td.construction
    {
        text-align: center;
    }
    th.cost {
        width: 200px;
        background-color: #E6B8B7;
        color: #FFFFFF;
        text-transform: uppercase;
    }
    td.cost
    {
        text-align: center;
    }
    th.budget
    {
        width: 100px;
        background-color: #E6B8B7;
        color: #FFFFFF;
        text-transform: uppercase;
    }
    td.budget
    {
        text-align: center;
    }
    th.status {
        width: 200px;
        background-color: #76933C;
        color: #FFFFFF;
        text-transform: uppercase;
    }
    td.status
    {
        text-align: center;
        background-color: #FFFFFF;
    }
    th.coment {
        width: 300px;
        background-color: #76933C;
        color: #FFFFFF;
        text-transform: uppercase;
    }
    td.coment
    {
        text-align: left;
        background-color: #FFFFFF;
    }
    th.comentstop {
        width: 100px;
        background-color: #76933C;
        color: #FFFFFF;
        text-transform: uppercase;
    }
    td.comentstop
    {
        text-align: center;
        background-color: #FFFFFF;
    }
    th.datecontractor {
        width: 100px;
        background-color: #FABF8F;
        color: #FFFFFF;
        text-transform: uppercase;
    }
    td.datecontractor
    {
        text-align: center;
    }
    th.contractor {
        width: 200px;
        background-color: #FABF8F;
        color: #FFFFFF;
        text-transform: uppercase;
    }
    td.contractor
    {
        text-align: left;
    }
    .yes {
        color: green
    }

    .no {
        color: red;
    }
    .title
{
    font-family: Georgia, Times, 'Times New Roman' , serif;
    font-size: 24px;
    color: #808000;
    text-align: center;
}

</style>

<asp:ObjectDataSource ID="odsSection" runat="server" SelectMethod="SelectSectionProgramProject" TypeName="Strategic.classProject" OnSelecting="odsSection_Selecting">
    <SelectParameters>
        <asp:Parameter Name="IDImplementationProgram" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsProject" runat="server" SelectMethod="SelectCultureProgramProject" TypeName="Strategic.classProject" >
    <SelectParameters>
        <asp:Parameter Name="IDSection" Type="Int32" />
        <asp:Parameter Name="IDImplementationProgram" Type="Int32" />
        <asp:Parameter Name="Status" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsSatus" runat="server" SelectMethod="SelectCultureProgramStepProject" TypeName="Strategic.classProject">
    <SelectParameters>
        <asp:Parameter Name="IDProject" Type="Int32" />
        <asp:Parameter Name="top1" Type="Boolean" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ListView ID="lvSection" runat="server" DataSourceID="odsSection" DataKeyNames="IDSection" OnItemCreated="lvSection_ItemCreated">
    <LayoutTemplate>
        <table class="tabList">
            <tr>
                <th class="name">
                    <asp:Literal ID="ltNameProject" runat="server" Text="<%$ Resources:ResourceStrategic, ppNameProject %>"></asp:Literal></th>
                <th class="order">
                    <asp:Literal ID="ltOrderProject" runat="server" Text="<%$ Resources:ResourceStrategic, ppOrderProject %>"></asp:Literal></th>
                <th class="construction">
                    <asp:Literal ID="ltTypeConstruction" runat="server" Text="<%$ Resources:ResourceStrategic, ppTypeConstruction %>"></asp:Literal></th>
                <th class="cost">
                    <asp:Literal ID="ltCost" runat="server" Text="<%$ Resources:ResourceStrategic, ppCost %>"></asp:Literal></th>
                <th class="budget">
                    <asp:Literal ID="ltBudget" runat="server" Text="<%$ Resources:ResourceStrategic, ppBudget %>"></asp:Literal></th>
                <th class="status">
                    <asp:Literal ID="ltStatus" runat="server" Text="<%$ Resources:ResourceStrategic, ppStatus %>"></asp:Literal></th>
                <th class="coment">
                    <asp:Literal ID="ltComent" runat="server" Text="<%$ Resources:ResourceStrategic, ppComent %>"></asp:Literal></th>
                <th class="comentstop">
                    <asp:Literal ID="ltComentStop" runat="server" Text="<%$ Resources:ResourceStrategic, ppComentStop %>"></asp:Literal></th>
                <th class="datecontractor">
                    <asp:Literal ID="ltDateContractor" runat="server" Text="<%$ Resources:ResourceStrategic, ppDateContractor %>"></asp:Literal></th>
                <th class="contractor">
                    <asp:Literal ID="ltContractor" runat="server" Text="<%$ Resources:ResourceStrategic, ppContractor %>"></asp:Literal></th>
            </tr>
            <tr id="itemPlaceholder" runat="server" />
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr><th class="departmen"colspan="10"><%# base.csection.GetSection(Container.DataItem, true) %></th></tr>
        <asp:ListView ID="lvProject" runat="server" DataKeyNames="IDProject" OnItemCreated="lvProject_ItemCreated" OnItemCommand="lvProject_ItemCommand" OnSelectedIndexChanging="lvProject_SelectedIndexChanging">
            <ItemTemplate>
                <%--<tr class="item">--%>
                <tr class='<%# GetStyleItem(Container.DataItem) %>'>
                    <td class="name" rowspan='<%# base.cproject.CountActiveStepProject((int)Eval("IDProject")) %>'>
                        <asp:LinkButton ID="lbName" runat="server" Text='<%# Eval("Name") %>' CommandName="Select" CommandArgument='<%# Eval("IDProject") %>' ></asp:LinkButton>
                    </td>
                    <td class="order" rowspan='<%# base.cproject.CountActiveStepProject((int)Eval("IDProject")) %>'><%# base.corder.GetNumDateOrder(Container.DataItem)%></td>
                    <td class="construction" rowspan='<%# base.cproject.CountActiveStepProject((int)Eval("IDProject")) %>'><%# base.cproject.GetTypeConstruction(Container.DataItem) %></td>
                    <td class="cost" rowspan='<%# base.cproject.CountActiveStepProject((int)Eval("IDProject")) %>'>
                        <asp:Label ID="lbFunding" runat="server" Text='<%# Eval("Funding", "{0:###,###,##0.00}") %>' CssClass='<%# GetStringBool(Container.DataItem,"AllocationFunds","yes","no") %>'></asp:Label>
                        &nbsp;<%# base.cproject.GetCurrency(Container.DataItem) %><br />
                        <asp:Label ID="lbBudjet" runat="server" Text='<%# base.cproject.GetAllocationFunds(Container.DataItem) %>' CssClass='<%# GetStringBool(Container.DataItem,"AllocationFunds","yes","no") %>'></asp:Label>
                    </td>
                    <td class="budget" rowspan='<%# base.cproject.CountActiveStepProject((int)Eval("IDProject")) %>'>
                        <%# Eval("SAPCode") %>
                    </td>
                    <asp:ListView ID="lvStatus1" runat="server" DataKeyNames="IDStepDetali" >
                        <ItemTemplate>
                            <td class="status"><%# base.ctemplatessteps.GetStep(Container.DataItem) %></td>
                            <td class="coment"><%# Eval("Coment") %></td>
                            <td class="comentstop"><%# Eval("FactStop", "{0:dd-MM-yyy}") %></td>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <td class="status">&nbsp;</td>
                            <td class="coment">&nbsp;</td>
                            <td class="comentstop">&nbsp;</td>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <td class="datecontractor" rowspan='<%# base.cproject.CountActiveStepProject((int)Eval("IDProject")) %>'><%# Eval("DateContractor") %></td>
                    <td class="contractor" rowspan='<%# base.cproject.CountActiveStepProject((int)Eval("IDProject")) %>'><%# Eval("Contractor") %></td>
                </tr>
                    <asp:ListView ID="lvStatus2" runat="server" DataKeyNames="IDStepDetali">
                        <ItemTemplate>
                            <tr>
                                <td class="status"><%# base.ctemplatessteps.GetStep(Container.DataItem) %></td>
                                <td class="coment"><%# Eval("Coment") %></td>
                                <td class="comentstop"><%# Eval("FactStop", "{0:dd-MM-yyy}") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
            </ItemTemplate>
        </asp:ListView>
    </ItemTemplate>
</asp:ListView>
