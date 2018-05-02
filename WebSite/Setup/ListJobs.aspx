<%@ Page Title="" Language="C#" MasterPageFile="~/WebSite/Setup/Setup.master" AutoEventWireup="true" CodeFile="ListJobs.aspx.cs" Inherits="WebSite_Setup_ListJobs" %>

<%@ Register Assembly="leaControls" Namespace="leaControls" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="css/listjobs.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ObjectDataSource ID="odsListJobs" runat="server" SelectMethod="SelectSetupJob" TypeName="WebBase.classHangfireJobsDB" OnUpdated="odsListJobs_Updated" OnUpdating="odsListJobs_Updating" UpdateMethod="UpdateHangFireJob" DeleteMethod="DeleteHangFireJob" OnDeleted="odsListJobs_Deleted" OnDeleting="odsListJobs_Deleting">
        <DeleteParameters>
            <asp:Parameter Name="IDJob" Type="Int32" />
            <asp:Parameter Name="OutResult" Type="Boolean" DefaultValue="True" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="IDJob" Type="Int32" />
            <asp:Parameter Name="Metod" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="Cron" Type="String" />
            <asp:Parameter Name="DistributionList" Type="String" />
            <asp:Parameter Name="OutResult" Type="Boolean" DefaultValue="True"/>
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsAllJob" runat="server" InsertMethod="InsertHangFireJob" SelectMethod="SelectSetupJob" TypeName="WebBase.classHangfireJobsDB" OnInserted="odsAllJob_Inserted" OnInserting="odsAllJob_Inserting">
        <InsertParameters>
            <asp:Parameter Name="Metod" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="Enable" Type="Boolean" />
            <asp:Parameter Name="Cron" Type="String" />
            <asp:Parameter Name="DistributionList" Type="String" />
            <asp:Parameter Name="OutResult" Type="Boolean" DefaultValue="True" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="lvlistjobs" DefaultValue="0" Name="IDJob" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div>
        <asp:LinkButton ID="lblistjobs" runat="server" Text="<%$ Resources:ResourceBase, lbListJobs %>" CssClass="link"></asp:LinkButton>
        <asp:LinkButton ID="lblogjobs" runat="server" Text="<%$ Resources:ResourceBase, lbLogJobs %>" CssClass="link"></asp:LinkButton>
    </div>
    <asp:MultiView ID="mvlistjobs" runat="server">
        <asp:View ID="vlistjobs" runat="server">
            <cc1:TableControl ID="tabControl" runat="server" StyleButton="img" StylePanel="InsUpdDel" ModeChange="true" OnUpdateClick="tabControl_UpdateClick" OnInsertClick="tabControl_InsertClick" OnDeleteClick="tabControl_DeleteClick" />
            <asp:ImageButton ID="imbRun" runat="server" Width="50" Height="50" ImageUrl="~/WebSite/Setup/image/Run.png" ToolTip="Выполнить задачу" OnClick="imbRun_Click" />
            <asp:ImageButton ID="imbStop" runat="server" Width="50" Height="50" ImageUrl="~/image/wuc/True.png" ToolTip="Активировать/деактивировать" OnClick="imbStop_Click"  />
            <asp:ImageButton ID="imbSetSetup" runat="server" Width="50" Height="50" ImageUrl="~/WebSite/Setup/image/SetSetup.png" ToolTip="Применить настройки" OnClick="imbSetSetup_Click"  />
            <asp:FormView ID="fvaddjob" runat="server" DataSourceID="odsAllJob" DataKeyNames="IDJob"  >
                <InsertItemTemplate>
                    <table class="addjob">
                        <tr>
                            <th colspan="2" class="upr">
                                <cc1:TableControl ID="tc" runat="server" StylePanel="InsCan" StyleButton="img" ModeChange="true" />
                            </th>
                        </tr>
                        <tr>
                            <th><asp:Literal ID="ltDescription" runat="server" Text="<%$ Resources:ResourceBase, ljDescription %>"></asp:Literal></th>
                            <td class="Description"><asp:TextBox ID="tbDescription" runat="server" TextMode="MultiLine" CssClass="tvMultiText"></asp:TextBox></td>
                        </tr>
                        <tr><th><asp:Literal ID="ltEnable" runat="server" Text="<%$ Resources:ResourceBase, ljEnable %>"></asp:Literal></th>
                            <td>
                                <asp:CheckBox ID="cbEnable" runat="server" /></td>
                        </tr>
                        <tr><th>
                            <asp:Literal ID="ltMetod" runat="server" Text="<%$ Resources:ResourceBase, ljMetod %>"></asp:Literal></th>
                            <td><asp:DropDownList ID="ddlMetod" runat="server" OnDataBound="ddlMetod_DataBound" CssClass="LineText"></asp:DropDownList></td>
                        </tr>
                        <tr><th>
                            <asp:Literal ID="ltCron" runat="server" Text="<%$ Resources:ResourceBase, ljCron %>"></asp:Literal></th>
                            <td><asp:TextBox ID="tbCron" runat="server" TextMode="MultiLine" CssClass="tvMultiText"></asp:TextBox></td>
                        </tr>
                        <tr><th>
                            <asp:Literal ID="ltDistributionList" runat="server" Text="<%$ Resources:ResourceBase, ljDistributionList %>"></asp:Literal></th>
                            <td><asp:TextBox ID="tbDistributionList" runat="server" TextMode="MultiLine" CssClass="tvMultiText"></asp:TextBox></td>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ListView ID="lvlistjobs" runat="server" DataSourceID="odsListJobs" DataKeyNames="IDJob" OnSelectedIndexChanged="lvlistjobs_SelectedIndexChanged">
                <LayoutTemplate>
                    <table class="tablistjobs">
                        <tr>
                            <th class="upr"></th>
                            <th class="Description">
                                <asp:LinkButton ID="lbDescription" runat="server" CommandName="sort" CommandArgument="Description" Text="<%$ Resources:ResourceBase, ljDescription %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                            <th class="Enable">
                                <asp:LinkButton ID="lbEnable" runat="server" CommandName="sort" CommandArgument="Enable" Text="<%$ Resources:ResourceBase, ljEnable %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                            <th class="Metod">
                                <asp:LinkButton ID="lbMetod" runat="server" CommandName="sort" CommandArgument="Metod" Text="<%$ Resources:ResourceBase, ljMetod %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                            <th class="Start">
                                <asp:LinkButton ID="lbStart" runat="server" CommandName="sort" CommandArgument="Start" Text="<%$ Resources:ResourceBase, ljStart %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                            <th class="Stop">
                                <asp:LinkButton ID="lbStop" runat="server" CommandName="sort" CommandArgument="Stop" Text="<%$ Resources:ResourceBase, ljStop %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                            <th class="Cron">
                                <asp:LinkButton ID="lbCron" runat="server" CommandName="sort" CommandArgument="Cron" Text="<%$ Resources:ResourceBase, ljCron %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                            <th class="DistributionList">
                                <asp:LinkButton ID="lbDistributionList" runat="server" CommandName="sort" CommandArgument="DistributionList" Text="<%$ Resources:ResourceBase, ljDistributionList %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" /></th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="item">
                        <td class="upr"></td>
                        <td class="Description">
                            <asp:LinkButton ID="lbDescription" runat="server" Text='<%# Eval("Description") %>' CommandName="Select" CausesValidation="false"></asp:LinkButton></td>
                        <td class="Enable"><asp:Image ID="imPageProcessor" runat="server" ImageUrl='<%# GetSRCCheckBox(Container.DataItem,"Enable") %>' Height="32px" Width="32px" /></td>
                        <td class="Metod"><%# Eval("Metod") %></td>
                        <td class="Start"><%# Eval("Start", "{0:dd-MM-yyyy HH:mm:ss}") %></td>
                        <td class="Stop"><%# Eval("Stop", "{0:dd-MM-yyyy HH:mm:ss}") %></td>
                        <td class="Cron"><%# Eval("Cron") %></td>
                        <td class="DistributionList">
                            <cc1:EmailLabel ID="EmailLabel1" DistributionList='<%# Eval("DistributionList") %>' runat="server" />
                            </td>
                    </tr>
                </ItemTemplate>
                <SelectedItemTemplate>
                    <tr class="select">
                        <td class="upr">
                            <cc1:TableControl ID="tcEdit" runat="server" StyleButton="img" StylePanel="InsUpdDel" ModeChange="true" VisibleInsert="false"  />
                        </td>
                        <td class="Description">
                            <asp:LinkButton ID="lbDescription" runat="server" Text='<%# Eval("Description") %>' CommandName="Select" CausesValidation="false"></asp:LinkButton></td>
                        <td class="Enable"><asp:Image ID="imPageProcessor" runat="server" ImageUrl='<%# GetSRCCheckBox(Container.DataItem,"Enable") %>' Height="32px" Width="32px" /></td>
                        <td class="Metod"><%# Eval("Metod") %></td>
                        <td class="Start"><%# Eval("Start", "{0:dd-MM-yyyy HH:mm:ss}") %></td>
                        <td class="Stop"><%# Eval("Stop", "{0:dd-MM-yyyy HH:mm:ss}") %></td>
                        <td class="Cron"><%# Eval("Cron") %></td>
                        <td class="DistributionList">
                            <cc1:EmailLabel ID="EmailLabel1" DistributionList='<%# Eval("DistributionList") %>' runat="server" />
                        </td>

                    </tr>
                </SelectedItemTemplate>
                <EditItemTemplate>
                    <tr class="edit">
                        <td class="upr">
                            <cc1:TableControl ID="tabedit" runat="server" StylePanel="UpdCan" StyleButton="img" ModeChange="true"/>
                        </td>
                        <td class="Description"><asp:TextBox ID="tbDescription" runat="server" TextMode="MultiLine" Text='<%# Eval("Description") %>' CssClass="MultiText"></asp:TextBox></td>
                        <td class="Enable"><asp:Image ID="imPageProcessor" runat="server" ImageUrl='<%# GetSRCCheckBox(Container.DataItem,"Enable") %>' Height="32px" Width="32px" /></td>
                        <td class="Metod">
                            <asp:DropDownList ID="ddlMetod" runat="server" OnDataBound="ddlMetod_DataBound"></asp:DropDownList>
                        </td>
                        <td class="Start"><%# Eval("Start", "{0:dd-MM-yyyy HH:mm:ss}") %></td>
                        <td class="Stop"><%# Eval("Stop", "{0:dd-MM-yyyy HH:mm:ss}") %></td>
                        <td class="Cron"><asp:TextBox ID="tbCron" runat="server" TextMode="MultiLine" Text='<%# Eval("Cron") %>' CssClass="MultiText"></asp:TextBox></td>
                        <td class="DistributionList">
                            <asp:TextBox ID="tbDistributionList" runat="server" TextMode="MultiLine" Text='<%# Eval("DistributionList") %>' CssClass="MultiText"></asp:TextBox>
                        </td>
                    </tr>
                </EditItemTemplate>
            </asp:ListView>
        </asp:View>
        <asp:View ID="vlogjobs" runat="server">

        </asp:View>
    </asp:MultiView>
</asp:Content>

