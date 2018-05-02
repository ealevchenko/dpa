<%@ Page Title="" Language="C#" MasterPageFile="~/WebSite/RailWay/RailWay.master" AutoEventWireup="true" CodeFile="CopyKISInput.aspx.cs" Inherits="WebSite_RailWay_CopyKISInput" %>

<%@ Register Assembly="leaControls" Namespace="leaControls" TagPrefix="lea" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/copykis.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <lea:InitInputDate ID="InitInputDate1" runat="server" jquery_script="true" />
    <lea:lockPanel ID="LockPanel1" runat="server" />
    <div class="Upr">
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:ResourceRailWay, textcontrolDatetime %>"></asp:Literal>
        <lea:InputPeriodDateTime runat="server" ID="ipdt1" OnDateTimeChange="ipdtUpr_DateTimeChange" Separator=" ~ " minDays="1" maxDays="10" DateFormat="DD-MM-YYYY HH:mm" Size="50" />
        <asp:ObjectDataSource ID="odsGetInfoCopySostav" runat="server" TypeName="leaRailWay.clORCInputStation" SelectMethod="SelectInfoCopySostav" OnObjectCreating="odsGetInfoCopySostav_ObjectCreating">
            <SelectParameters>
                <asp:ControlParameter ControlID="ipdt1" Name="dtStart" PropertyName="startDate" Type="DateTime" />
                <asp:ControlParameter ControlID="ipdt1" Name="dtStop" PropertyName="stopDate" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    <div class="detali">
        <asp:LinkButton ID="lbStatus" runat="server" CssClass="LinkSelectCommand" OnClick="lbStatus_Click">Статистика</asp:LinkButton>
        <asp:LinkButton ID="lbRules" runat="server" CssClass="LinkCommand" OnClick="lbRules_Click">Правила</asp:LinkButton>
        <asp:MultiView ID="mvDetali" runat="server">
            <asp:View ID="vStatus" runat="server">
                <asp:ListView ID="lvSostav" runat="server" DataSourceID="odsGetInfoCopySostav" DataKeyNames="IDOrcInputStation">
                    <LayoutTemplate>
                        <table class="tabInputStation">
                            <tr>
                                <th class="DateTime">
                                    <asp:LinkButton ID="lbDateTime" runat="server" CommandName="sort" CommandArgument="DateTime" Text="<%$ Resources:ResourceRailWay, ckis_datatime %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <th class="DocNum">
                                    <asp:LinkButton ID="lbNaturNum" runat="server" CommandName="sort" CommandArgument="DocNum" Text="<%$ Resources:ResourceRailWay, ckis_DocNum %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <th class="SenderStation">
                                    <asp:LinkButton ID="lbSenderStation" runat="server" CommandName="sort" CommandArgument="IDOrcSenderStation" Text="<%$ Resources:ResourceRailWay, ckis_SenderStation %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <th class="SenderWayName">
                                    <asp:LinkButton ID="lbWayName" runat="server" CommandName="sort" CommandArgument="SenderWayNum" Text="<%$ Resources:ResourceRailWay, ckis_SenderWayName %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <th class="ReceiverStation">
                                    <asp:LinkButton ID="lbReceiverStation" runat="server" CommandName="sort" CommandArgument="IDOrcReceiverStation" Text="<%$ Resources:ResourceRailWay, ckis_ReceiverStation %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <th class="CountWagons">
                                    <asp:LinkButton ID="lbCountWagons" runat="server" CommandName="sort" CommandArgument="CountWagons" Text="<%$ Resources:ResourceRailWay, ckis_CountWagonsDoc %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <th class="CountSetWagons">
                                    <asp:LinkButton ID="lbCountSetWagons" runat="server" CommandName="sort" CommandArgument="CountSetWagons" Text="<%$ Resources:ResourceRailWay, ckis_CountSetWagonsInpDoc %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <%--                        <th class="CountUpdareWagons">
                            <asp:LinkButton ID="lbCountSetNatHIist" runat="server" CommandName="sort" CommandArgument="CountUpdareWagons" Text="<%$ Resources:ResourceRailWay, ckis_CountUpdareWagons %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>    --%>
                                <th class="Close">
                                    <asp:LinkButton ID="lbClose" runat="server" CommandName="sort" CommandArgument="Close" Text="<%$ Resources:ResourceRailWay, ckis_Close %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <th class="Status">
                                    <asp:LinkButton ID="lbStatus" runat="server" CommandName="sort" CommandArgument="Status" Text="<%$ Resources:ResourceRailWay, ckis_Status %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class='<%# orc_input_station.GetStatus(Container.DataItem,"Status",null) %>'>
                            <td class="datatime">
                                <asp:LinkButton ID="lbSelectDataTime" runat="server" Text='<%# Eval("DateTime", "{0:dd-MM-yyyy HH:mm}") %>' CommandName="Select" CausesValidation="false" CommandArgument='<%# Eval("IDOrcInputStation") %>'></asp:LinkButton></td>
                            <td class="DocNum"><%# Eval("DocNum") %></td>
                            <td class="SenderStation"><%# old_st.GetNameIDORC(Container.DataItem,"IDOrcSenderStation",null) %></td>
                            <td class="SenderWayName"><%# old_ws.GetNameIDORC(Container.DataItem,"IDOrcSenderStation","SenderWayNum",null) %></td>
                            <td class="ReceiverStation"><%# old_st.GetNameIDORC(Container.DataItem,"IDOrcReceiverStation",null) %></td>
                            <td class="CountWagons"><%# Eval("CountWagons") %></td>
                            <td class="CountSetWagons"><%# Eval("CountSetWagons") %></td>
                            <td class="Close"><%# Eval("Close") %></td>
                            <td class="Status"><%# orc_input_station.GetStatus(Container.DataItem,"Status",null) %></td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </asp:View>
            <asp:View ID="vRules" runat="server">

            </asp:View>
        </asp:MultiView>

    </div>
</asp:Content>

