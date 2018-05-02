<%@ Page Title="" Language="C#" MasterPageFile="~/WebSite/RailWay/RailWay.master" AutoEventWireup="true" CodeFile="CopyKIS.aspx.cs" Inherits="WebSite_RailWay_CopyKIS" %>

<%@ Register Assembly="leaControls" Namespace="leaControls" TagPrefix="lea" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/copykis.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <lea:InitInputDate ID="InitInputDate1" runat="server" jquery_script="true" />
    <lea:lockPanel ID="LockPanel1" runat="server" />
    <asp:ObjectDataSource ID="odsGetInfoCopySostav" runat="server" TypeName="leaRailWay.clORCSostav" SelectMethod="SelectInfoCopySostav" OnObjectCreating="odsGetInfoCopySostav_ObjectCreating1">
        <SelectParameters>
            <asp:ControlParameter ControlID="ipdt1" Name="dtStart" PropertyName="startDate" Type="DateTime" />
            <asp:ControlParameter ControlID="ipdt1" Name="dtStop" PropertyName="stopDate" Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsListSostav" runat="server" SelectMethod="GetPromVagon" TypeName="leaRailWay.clORC" OnObjectCreating="odsListSostav_ObjectCreating">
        <SelectParameters>
            <asp:ControlParameter ControlID="lvSostav" DefaultValue="0" Name="idORCSostav" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsMTListSostav" runat="server" SelectMethod="SelectListSostav" TypeName="leaRailWay.clMTList" OnObjectCreating="odsMTListSostav_ObjectCreating" OnSelecting="odsMTListSostav_Selecting">
        <SelectParameters>
            <asp:ControlParameter ControlID="tvMT" DefaultValue="0" Name="idsostav" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <div class="Upr">
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:ResourceRailWay, textcontrolDatetime %>"></asp:Literal>
        <lea:InputPeriodDateTime runat="server" ID="ipdt1" OnDateTimeChange="ipdtUpr_DateTimeChange" Separator=" ~ " minDays="1" maxDays="10" DateFormat="DD-MM-YYYY HH:mm" Size="50" />
    </div>
    <div class="detali">
        <asp:LinkButton ID="lbKIS" runat="server" CssClass="LinkSelectCommand" OnClick="lbKIS_Click">КИС</asp:LinkButton>
        <asp:LinkButton ID="lbMT" runat="server" CssClass="LinkCommand" OnClick="lbMT_Click">Металлургтранс</asp:LinkButton><br />
        <asp:MultiView ID="mvSource" runat="server">
            <asp:View ID="vKIS" runat="server">
                <div class="divKISSostav">
                    <asp:ListView ID="lvSostav" runat="server" DataSourceID="odsGetInfoCopySostav" DataKeyNames="IDOrcSostav">
                        <LayoutTemplate>
                            <table class="tabSostav">
                                <tr>
                                    <th class="DateTime">
                                        <asp:LinkButton ID="lbDateTime" runat="server" CommandName="sort" CommandArgument="DateTime" Text="<%$ Resources:ResourceRailWay, ckis_datatime %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                    <th class="NaturNum">
                                        <asp:LinkButton ID="lbNaturNum" runat="server" CommandName="sort" CommandArgument="NaturNum" Text="<%$ Resources:ResourceRailWay, ckis_NaturNum %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                    <th class="Station">
                                        <asp:LinkButton ID="lbStation" runat="server" CommandName="sort" CommandArgument="IDOrcStation" Text="<%$ Resources:ResourceRailWay, ckis_Station %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                    <th class="WayName">
                                        <asp:LinkButton ID="lbWayName" runat="server" CommandName="sort" CommandArgument="WayNum" Text="<%$ Resources:ResourceRailWay, ckis_WayName %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                    <th class="CountWagons">
                                        <asp:LinkButton ID="lbCountWagons" runat="server" CommandName="sort" CommandArgument="CountWagons" Text="<%$ Resources:ResourceRailWay, ckis_CountWagons %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                    <th class="CountNatHIist">
                                        <asp:LinkButton ID="lbCountNatHIist" runat="server" CommandName="sort" CommandArgument="CountNatHIist" Text="<%$ Resources:ResourceRailWay, ckis_CountNatHIist %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                    <th class="CountSetWagons">
                                        <asp:LinkButton ID="lbCountSetWagons" runat="server" CommandName="sort" CommandArgument="CountSetWagons" Text="<%$ Resources:ResourceRailWay, ckis_CountSetWagons %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                    <th class="CountSetNatHIist">
                                        <asp:LinkButton ID="lbCountSetNatHIist" runat="server" CommandName="sort" CommandArgument="CountSetNatHIist" Text="<%$ Resources:ResourceRailWay, ckis_CountSetNatHIist %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                    <th class="Close">
                                        <asp:LinkButton ID="lbClose" runat="server" CommandName="sort" CommandArgument="Close" Text="<%$ Resources:ResourceRailWay, ckis_Close %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                    <th class="Status">
                                        <asp:LinkButton ID="lbStatus" runat="server" CommandName="sort" CommandArgument="Status" Text="<%$ Resources:ResourceRailWay, ckis_Status %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                    <th class="ListNoUpdateWagons">
                                        <asp:LinkButton ID="lbListNoUpdateWagons" runat="server" CommandName="sort" CommandArgument="Status" Text="<%$ Resources:ResourceRailWay, ckis_ListNoUpdateWagonss %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>

                                </tr>
                                <tr id="itemPlaceholder" runat="server" />
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class='<%# orc_sostav.GetStatus(Container.DataItem,"Status",null) %>'>
                                <td class="datatime">
                                    <asp:LinkButton ID="lbSelectDataTime" runat="server" Text='<%# Eval("DateTime", "{0:dd-MM-yyyy HH:mm}") %>' CommandName="Select" CausesValidation="false" CommandArgument='<%# Eval("IDOrcSostav") %>'></asp:LinkButton></td>
                                <td class="NaturNum"><%# Eval("NaturNum") %></td>
                                <td class="Station"><%# old_st.GetNameIDORC(Container.DataItem,"IDOrcStation",null) %></td>
                                <td class="WayName"><%# old_ws.GetNameIDORC(Container.DataItem,"IDOrcStation","WayNum",null) %></td>
                                <td class="CountWagons"><%# Eval("CountWagons") %></td>
                                <td class="CountNatHIist"><%# Eval("CountNatHIist") %></td>
                                <td class="CountSetWagons"><%# Eval("CountSetWagons") %></td>
                                <td class="CountSetNatHIist"><%# Eval("CountSetNatHIist") %></td>
                                <td class="Close"><%# Eval("Close") %></td>
                                <td class="Status"><%# orc_sostav.GetStatus(Container.DataItem,"Status",null) %></td>
                                <td class="ListNoUpdateWagons"><%# Eval("ListNoUpdateWagons") %></td>
                            </tr>
                        </ItemTemplate>
                        <SelectedItemTemplate>
                            <tr class="select">
                                <td class="datatime">
                                    <asp:LinkButton ID="lbSelectDataTime" runat="server" Text='<%# Eval("DateTime", "{0:dd-MM-yyyy HH:mm}") %>' CommandName="Select" CausesValidation="false" CommandArgument='<%# Eval("IDOrcSostav") %>'></asp:LinkButton></td>
                                <td class="NaturNum"><%# Eval("NaturNum") %></td>
                                <td class="Station"><%# old_st.GetNameIDORC(Container.DataItem,"IDOrcStation",null) %></td>
                                <td class="WayName"><%# old_ws.GetNameIDORC(Container.DataItem,"IDOrcStation","WayNum",null) %></td>
                                <td class="CountWagons"><%# Eval("CountWagons") %></td>
                                <td class="CountNatHIist"><%# Eval("CountNatHIist") %></td>
                                <td class="CountSetWagons"><%# Eval("CountSetWagons") %></td>
                                <td class="CountSetNatHIist"><%# Eval("CountSetNatHIist") %></td>
                                <td class="Close"><%# Eval("Close") %></td>
                                <td class="Status"><%# orc_sostav.GetStatus(Container.DataItem,"Status",null) %></td>
                                <td class="ListNoUpdateWagons"><%# Eval("ListNoUpdateWagons") %></td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                </div>
                <div class="divKISList">
                    <asp:ListView ID="lvListSostav" runat="server" DataSourceID="odsListSostav">
                        <LayoutTemplate>
                            <table class="tabListSostav">
                                <tr>
                                    <th class="Position">
                                        <asp:LinkButton ID="lbPosition" runat="server" CommandName="sort" CommandArgument="npp" Text="<%$ Resources:ResourceRailWay, cmt_Position %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                    <th class="CarriageNumber">
                                        <asp:LinkButton ID="lbCarriageNumber" runat="server" CommandName="sort" CommandArgument="N_VAG" Text="<%$ Resources:ResourceRailWay, cmt_CarriageNumber %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server" />
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="Position"><%# Eval("npp") %></td>
                                <td class="CarriageNumber"><%# Eval("N_VAG") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </asp:View>
            <asp:View ID="vMT" runat="server">
                <div class="divtv">
                <asp:TreeView ID="tvMT" runat="server" ImageSet="WindowsHelp" OnSelectedNodeChanged="tvMT_SelectedNodeChanged" OnDataBound="tvMT_DataBound">
                    <HoverNodeStyle CssClass="tvHover" />
                    <LevelStyles>
                        <asp:TreeNodeStyle CssClass="tnslevel1" />
                    </LevelStyles>
                    <ParentNodeStyle CssClass="tvFolder" />
                    <LeafNodeStyle CssClass="tvFile" />
                    <SelectedNodeStyle CssClass="tvSelect" />
                </asp:TreeView>
                </div>
                <div class="mtlist">
                <asp:ListView ID="lvmtlistsostav" runat="server" DataSourceID="odsMTListSostav" DataKeyNames="IDMTList">
                    <LayoutTemplate>
                        <table class="tabMTList">
                            <tr>
                                <th class="Position">
                                    <asp:LinkButton ID="lbPosition" runat="server" CommandName="sort" CommandArgument="Position" Text="<%$ Resources:ResourceRailWay, cmt_Position %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <th class="CarriageNumber">
                                    <asp:LinkButton ID="lbCarriageNumber" runat="server" CommandName="sort" CommandArgument="CarriageNumber" Text="<%$ Resources:ResourceRailWay, cmt_CarriageNumber %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <th class="CountryCode">
                                    <asp:LinkButton ID="lbCountryCode" runat="server" CommandName="sort" CommandArgument="CountryCode" Text="<%$ Resources:ResourceRailWay, cmt_CountryCode %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <th class="Weight">
                                    <asp:LinkButton ID="lbWeight" runat="server" CommandName="sort" CommandArgument="Weight" Text="<%$ Resources:ResourceRailWay, cmt_Weight %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <th class="IDCargo">
                                    <asp:LinkButton ID="lbIDCargo" runat="server" CommandName="sort" CommandArgument="IDCargo" Text="<%$ Resources:ResourceRailWay, cmt_IDCargo %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <th class="Cargo">
                                    <asp:LinkButton ID="lbCargo" runat="server" CommandName="sort" CommandArgument="Cargo" Text="<%$ Resources:ResourceRailWay, cmt_Cargo %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <th class="IDStation">
                                    <asp:LinkButton ID="lbIDStation" runat="server" CommandName="sort" CommandArgument="IDStation" Text="<%$ Resources:ResourceRailWay, cmt_IDStation %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <th class="Station">
                                    <asp:LinkButton ID="lbStation" runat="server" CommandName="sort" CommandArgument="Station" Text="<%$ Resources:ResourceRailWay, cmt_Station %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <th class="Consignee">
                                    <asp:LinkButton ID="lbConsignee" runat="server" CommandName="sort" CommandArgument="Consignee" Text="<%$ Resources:ResourceRailWay, cmt_Consignee %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <th class="Operation">
                                    <asp:LinkButton ID="lbOperation" runat="server" CommandName="sort" CommandArgument="Operation" Text="<%$ Resources:ResourceRailWay, cmt_Operation %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <th class="CompositionIndex">
                                    <asp:LinkButton ID="lbCompositionIndex" runat="server" CommandName="sort" CommandArgument="CompositionIndex" Text="<%$ Resources:ResourceRailWay, cmt_CompositionIndex %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <th class="DateOperation">
                                    <asp:LinkButton ID="lbDateOperation" runat="server" CommandName="sort" CommandArgument="DateOperation" Text="<%$ Resources:ResourceRailWay, cmt_DateOperation %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <th class="TrainNumber">
                                    <asp:LinkButton ID="lbTrainNumber" runat="server" CommandName="sort" CommandArgument="TrainNumber" Text="<%$ Resources:ResourceRailWay, cmt_TrainNumber %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                                <th class="NaturList">
                                    <asp:LinkButton ID="lbNaturList" runat="server" CommandName="sort" CommandArgument="NaturList" Text="<%$ Resources:ResourceRailWay, cmt_NaturList %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                           
                                 </tr>
                            <tr id="itemPlaceholder" runat="server" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="Position"><%# Eval("Position") %></td>     
                            <td class="CarriageNumber"><%# Eval("CarriageNumber") %></td>   
                            <td class="CountryCode"><%# Eval("CountryCode") %></td>    
                            <td class="Weight"><%# Eval("Weight") %></td>                              
                            <td class="IDCargo"><%# Eval("IDCargo") %></td>    
                            <td class="Cargo"><%# Eval("Cargo") %></td>                             
                            <td class="IDStation"><%# Eval("IDStation") %></td>    
                            <td class="Station"><%# Eval("Station") %></td>                              
                            <td class="Consignee"><%# Eval("Consignee") %></td>    
                            <td class="Operation"><%# Eval("Operation") %></td>                               
                            <td class="CompositionIndex"><%# Eval("CompositionIndex") %></td>    
                            <td class="DateOperation"><%# Eval("DateOperation", "{0:dd-MM-yyyy HH:mm}") %></td>                              
                            <td class="TrainNumber"><%# Eval("TrainNumber") %></td> 
                            <td class="NaturList"><%# Eval("NaturList") %></td>                                
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
                </div>

            </asp:View>
        </asp:MultiView>
        <div class="cls"></div>
    </div>
</asp:Content>

