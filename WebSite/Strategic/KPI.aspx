<%@ Page Title="" Language="C#" MasterPageFile="~/WebSite/Strategic/Strategic.master" AutoEventWireup="true" CodeFile="KPI.aspx.cs" Inherits="WebSite_Strategic_KPI" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>
<%@ Register TagPrefix="wuc" TagName="controlKPI" Src="~/WebUserControl/Strategic/controlKPI.ascx" %>
<%@ Register TagPrefix="wuc" TagName="controlProject" Src="~/WebUserControl/Strategic/controlProject.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/kpi.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- -->
    <asp:ObjectDataSource ID="odsListKPI" runat="server" SelectMethod="SelectCultureKPI" TypeName="Strategic.classKPI"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsListProject" runat="server" SelectMethod="SelectCultureProject" TypeName="Strategic.classProject"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsKPIProject" runat="server" SelectMethod="SelectKPIProject" TypeName="Strategic.classKPI" OnUpdating="odsKPIProject_Updating" UpdateMethod="UpdateKPIProject" InsertMethod="InsertKPIProject" OnInserting="odsKPIProject_Inserting" OnInserted="odsKPIProject_Inserted" DeleteMethod="DeleteKPIProject" OnDeleted="odsKPIProject_Deleted" OnDeleting="odsKPIProject_Deleting">
        <DeleteParameters>
            <asp:Parameter Name="IDKPIProject" Type="Int32" />
            <asp:Parameter Name="IDKPIYear" Type="Int32" />
            <asp:Parameter Name="OutResult" Type="Boolean" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="IDProject" Type="Int32" />
            <asp:Parameter Name="IDKPI" Type="Int32" />
            <asp:Parameter Name="IndexKPI" Type="Int32" />
            <asp:Parameter Name="YearStart" Type="Int32" />
            <asp:Parameter Name="YearStop" Type="Int32" />
            <asp:Parameter Name="OutResult" Type="Boolean" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlKPI" DefaultValue="0" Name="IDKPI" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="ddlProject" DefaultValue="0" Name="IDProject" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="ddlYear" DefaultValue="" Name="Year" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="IDKPIYear" Type="Int32" />
            <asp:Parameter Name="IDKPIProject" Type="Int32" />
            <asp:Parameter Name="IndexKPI" Type="Int32" />
            <asp:Parameter Name="Q1" Type="Decimal" />
            <asp:Parameter Name="Q2" Type="Decimal" />
            <asp:Parameter Name="Q3" Type="Decimal" />
            <asp:Parameter Name="Q4" Type="Decimal" />
            <asp:Parameter Name="ROI" Type="Decimal" />
            <asp:Parameter Name="NPV" Type="Decimal" />
            <asp:Parameter Name="OutResult" Type="Boolean" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsListIndexKPI" runat="server" SelectMethod="SelectCultureIndexKPI" TypeName="Strategic.classKPI"></asp:ObjectDataSource>
    <!-- -->
    <div class="Page">
        <div class="divSelectKPI">
            <table class="selectkpi">
                <tr>
                    <td class="name">
                        <asp:Literal ID="ltName" runat="server" Text="<%$ Resources:ResourceStrategic, nfKPI%>"></asp:Literal>:&nbsp;
                    </td>
                    <td class="nameddl">
                        <asp:DropDownList ID="ddlKPI" runat="server" DataSourceID="odsListKPI" DataTextField="Name" DataValueField="IDKPI" OnDataBound="ddlKPI_DataBound" AutoPostBack="true" CssClass="ddlbtselect" OnSelectedIndexChanged="ddlKPI_SelectedIndexChanged"></asp:DropDownList>
                        &nbsp;<wuc:panelUpr ID="pnSelect" runat="server" StylePanel="InsUpdDel" StyleButton="img" OnInsertClick="pnSelect_InsertClick" OnUpdateClick="pnSelect_UpdateClick" OnDeleteClick="pnSelect_DeleteClick" SizeImage="24" ConfirmDelete="MessageDeleteKPI" />
                    </td>
                    <td class="project">
                        <asp:Literal ID="ltNameProject" runat="server" Text="<%$ Resources:ResourceStrategic, nfKPIProject %>"></asp:Literal>:&nbsp;
                    </td>
                    <td class="projectddl">
                        <asp:DropDownList ID="ddlProject" runat="server" DataSourceID="odsListProject" DataTextField="Name" DataValueField="IDProject" OnDataBound="ddlProject_DataBound" AutoPostBack="true" CssClass="ddlselect" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td class="year">
                        <asp:Literal ID="ltNameYare" runat="server" Text="<%$ Resources:ResourceStrategic, nfNameYare %>"></asp:Literal>:&nbsp;
                    </td>
                    <td class="yearddl">
                        <asp:DropDownList ID="ddlYear" runat="server" OnDataBound="ddlYear_DataBound" CssClass="ddlselect" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <wuc:controlKPI ID="controlKPI" runat="server" OutInfoText="true"  OnDataInserted="controlKPI_DataInserted" OnDataUpdated="controlKPI_DataUpdated" OnDataDeleted="controlKPI_DataDeleted" Caption="Добавить KPI"/>
                        <asp:FormView ID="fvInsertKPIProject" runat="server" DataSourceID="odsKPIProject">
                            <ItemTemplate>

                            </ItemTemplate>
                            <EditItemTemplate>
                                <table class="tabInsertKPIProject">
                                    <caption>
                                        <asp:Literal ID="Literal1" runat="server" Text='<%$ Resources:ResourceStrategic, insertKPIProject %>' ></asp:Literal></caption>
                                    <tr>
                                        <td colspan="2" class="Upr">
                                            <wuc:panelUpr ID="pnInsertKPIProject" runat="server" StylePanel="InsCan" Change="true" ValidationGroup="InsertKPIProject" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:Literal ID="ltKPI" runat="server" Text="<%$ Resources:ResourceStrategic, nfKPI %>"></asp:Literal>:&nbsp;
                                        </th>
                                        <td>
                                            <asp:DropDownList ID="ddlKPI" runat="server" DataSourceID="odsListKPI" DataTextField="Name" DataValueField="IDKPI" CssClass="ddlselect" ></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:Literal ID="ltKPIProject" runat="server" Text="<%$ Resources:ResourceStrategic, nfKPIProject %>"></asp:Literal>:&nbsp;
                                        </th>
                                        <td>
                                            <asp:DropDownList ID="ddlProject" runat="server" DataSourceID="odsListProject" DataTextField="Name" DataValueField="IDProject" CssClass="ddlselect" ></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:Literal ID="ltIndexKPI" runat="server" Text="<%$ Resources:ResourceStrategic, nfIndexKPI %>"></asp:Literal>:&nbsp;
                                        </th>
                                        <td>
                                            <asp:DropDownList ID="ddlIndexKPI" DataSourceID="odsListIndexKPI"  DataTextField="IndexName" DataValueField="IndexKPI" runat="server" CssClass="ddlselect" ></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:Literal ID="ltYearStart" runat="server" Text="<%$ Resources:ResourceStrategic, nfYearStart %>"></asp:Literal>:&nbsp;
                                        </th>
                                        <td>
                                            <asp:DropDownList ID="ddlYearStart" runat="server" CssClass="ddlselect" OnDataBound="ddlYearStart_DataBound"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:Literal ID="ltYearStop" runat="server" Text="<%$ Resources:ResourceStrategic, nfYearStop %>"></asp:Literal>:&nbsp;
                                        </th>
                                        <td>
                                            <asp:DropDownList ID="ddlYearStop" runat="server" CssClass="ddlselect" OnDataBound="ddlYearStop_DataBound"></asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </EditItemTemplate>
                        </asp:FormView>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divListKPI">
            <asp:Label ID="lbTitle" runat="server" Text="<%$ Resources:ResourceStrategic, titleListKPIProject %>" CssClass="title"></asp:Label>
            <div class="upr"><wuc:panelUpr ID="PanelUprKPIProject" runat="server" StylePanel="InsUpdDel" VisibleDelete="false" VisibleUpdate="false" StyleButton="img" OnInsertClick="PanelUprKPIProject_InsertClick" /></div>
            <asp:ListView ID="lvKPIProject" runat="server" DataSourceID="odsKPIProject" DataKeyNames="IDKPIYear" OnSelectedIndexChanged="lvKPIProject_SelectedIndexChanged">
                <LayoutTemplate>
                    <table class="tabKPIProject">
                        <tr>
                            <th class="upr"></th>
                            <th class="kpi">
                                <asp:LinkButton ID="lbKPI" runat="server" CommandName="sort" CommandArgument="IDKPI" Text="<%$ Resources:ResourceStrategic, nfNameDetaliKPI %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" OnClick="lbSelect_Click" /></th>
                            <th class="project"><asp:LinkButton ID="lbProject" runat="server" CommandName="sort" CommandArgument="IDProject" Text="<%$ Resources:ResourceStrategic, nfKPIProject %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" OnClick="lbSelect_Click" /></th>
                            <th class="index"><asp:LinkButton ID="lbIndexKPI" runat="server" CommandName="sort" CommandArgument="IndexKPI" Text="<%$ Resources:ResourceStrategic, nfIndexKPI %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" OnClick="lbSelect_Click" /></th>
                            <th class="q1"><asp:LinkButton ID="lbQ1" runat="server" CommandName="sort" CommandArgument="Q1" Text="<%$ Resources:ResourceStrategic, nfQ1 %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" OnClick="lbSelect_Click" /></th>
                            <th class="q2"><asp:LinkButton ID="lbQ2" runat="server" CommandName="sort" CommandArgument="Q2" Text="<%$ Resources:ResourceStrategic, nfQ2 %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" OnClick="lbSelect_Click" /></th>
                            <th class="q3"><asp:LinkButton ID="lbQ3" runat="server" CommandName="sort" CommandArgument="Q3" Text="<%$ Resources:ResourceStrategic, nfQ3 %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" OnClick="lbSelect_Click" /></th>
                            <th class="q4"><asp:LinkButton ID="lbQ4" runat="server" CommandName="sort" CommandArgument="Q4" Text="<%$ Resources:ResourceStrategic, nfQ4 %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" OnClick="lbSelect_Click" /></th>
                            <th class="roi"><asp:LinkButton ID="lbROI" runat="server" CommandName="sort" CommandArgument="ROI" Text="<%$ Resources:ResourceStrategic, nfROI %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" OnClick="lbSelect_Click" /></th>
                            <th class="npv"><asp:LinkButton ID="lbNPV" runat="server" CommandName="sort" CommandArgument="NPV" Text="<%$ Resources:ResourceStrategic, nfNPV %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" OnClick="lbSelect_Click" /></th>
                        
                        </tr>
                        <tr id="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="item">
                        <td class="upr">
                            <%--<asp:ImageButton ID="ImageButton1" ImageUrl="~/image/wuc/Current.png" runat="server" ToolTip="<%$ Resources:ResourceBase, ToolTipSelect %>"  CommandArgument='<%# Eval("IDKPIYear") %>' CommandName="Select" CausesValidation="false"/>--%>
                            <asp:LinkButton ID="lbSelect" runat="server" Text="<%$ Resources:ResourceBase, ToolTipSelect %>" CommandArgument='<%# Eval("IDKPIYear") %>' CommandName="Select" CausesValidation="false"></asp:LinkButton>
                        </td>
                        <td class="kpi"><%# base.ckpi.GetKPI(Container.DataItem) %></td>
                        <td class="project"><%# base.cproject.GetNameProject(Container.DataItem) %></td>
                        <td class="index"><%# base.ckpi.GetIndexKPI(Container.DataItem) %></td>
                        <td class="q1"><%# Eval("Q1") %></td>
                        <td class="q2"><%# Eval("Q2") %></td>
                        <td class="q3"><%# Eval("Q3") %></td>
                        <td class="q4"><%# Eval("Q4") %></td>
                        <td class="roi"><%# Eval("ROI") %></td>
                        <td class="npv"><%# Eval("NPV") %></td>
                    </tr>
                </ItemTemplate>
                <SelectedItemTemplate>
                    <tr class="select">
                        <td class="upr" rowspan="2">
                            <wuc:panelUpr ID="pnKPIProject" runat="server" StylePanel="InsUpdDel" VisibleInsert="false" ConfirmDelete="MessageDeleteKPIProject" StyleButton="img" SizeImage="32" OnInit="pnKPIProject_Init" /></td>
                        <td class="kpi"><%# base.ckpi.GetKPI(Container.DataItem) %></td>
                        <td class="project"><asp:LinkButton ID="lbProject" runat="server" Text='<%# base.cproject.GetNameProject(Container.DataItem) %>' OnClick="lbProject_Click"></asp:LinkButton></td>

                        <%--<td class="project"><%# base.cproject.GetNameProject(Container.DataItem) %></td>--%>
                        <td class="index"><%# base.ckpi.GetIndexKPI(Container.DataItem) %></td>
                        <td class="q1"><%# Eval("Q1") %></td>
                        <td class="q2"><%# Eval("Q2") %></td>
                        <td class="q3"><%# Eval("Q3") %></td>
                        <td class="q4"><%# Eval("Q4") %></td>
                        <td class="roi"><%# Eval("ROI") %></td>
                        <td class="npv"><%# Eval("NPV") %></td>
                    </tr>
                    <tr><td colspan="9">
                        <wuc:controlProject ID="controlProject" runat="server" OutInfoText="true" Change="false" />
                    </td></tr>
                </SelectedItemTemplate>
                <EditItemTemplate>
                    <tr class="select">
                        <td class="edit">
                            <wuc:panelUpr ID="pnKPIProject" runat="server" StylePanel="UpdCan" StyleButton="img" SizeImage="32" Change="true" ValidationGroup="KPIProject" /></td>
                        <td class="kpi"><%# base.ckpi.GetKPI(Container.DataItem) %></td>
                        <td class="project"><%# base.cproject.GetNameProject(Container.DataItem) %></td>
                        <td class="index">
                            <asp:DropDownList ID="ddlIndexKPI" DataSourceID="odsListIndexKPI"  DataTextField="IndexName" DataValueField="IndexKPI"  
                                runat="server" AutoPostBack="true" OnDataBound="ddlIndexKPI_DataBound" SelectedValue='<%# Eval("IndexKPI") %>' CssClass="ddlselect"  ></asp:DropDownList></td>
                        <td class="q1">
                            <asp:TextBox ID="tbQ1" runat="server" CssClass="ddlselect" Text='<%# Bind("Q1")%>' TextMode="Number"></asp:TextBox>
<%--                            <asp:RegularExpressionValidator ID="revQ1" runat="server" ControlToValidate="tbQ1" ValidationGroup="KPIProject"
                                ValidationExpression="(^\d*\.?\d*[1-9]+\d*$)|(^[1-9]+\d*\.\d*$)|(^\d*\,?\d*[1-9]+\d*$)|(^[1-9]+\d*\,\d*$)" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_decimal %>"
                                Display="dynamic" CssClass="failure" Enabled="true">
                            </asp:RegularExpressionValidator>--%>
                        </td>
                        <td class="q2"><asp:TextBox ID="tbQ2" runat="server"  CssClass="ddlselect"  Text='<%# Bind("Q2")%>' TextMode="Number"></asp:TextBox>
<%--                            <asp:RegularExpressionValidator ID="revQ2" runat="server" ControlToValidate="tbQ2" ValidationGroup="KPIProject"
                                ValidationExpression="(^\d*\.?\d*[1-9]+\d*$)|(^[1-9]+\d*\.\d*$)|(^\d*\,?\d*[1-9]+\d*$)|(^[1-9]+\d*\,\d*$)" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_decimal %>"
                                Display="dynamic" CssClass="failure" Enabled="true">
                            </asp:RegularExpressionValidator>--%>
                        </td>
                        <td class="q3"><asp:TextBox ID="tbQ3" runat="server"  CssClass="ddlselect"  Text='<%# Bind("Q3")%>' TextMode="Number"></asp:TextBox>
<%--                            <asp:RegularExpressionValidator ID="revQ3" runat="server" ControlToValidate="tbQ3" ValidationGroup="KPIProject"
                                ValidationExpression="(^\d*\.?\d*[1-9]+\d*$)|(^[1-9]+\d*\.\d*$)|(^\d*\,?\d*[1-9]+\d*$)|(^[1-9]+\d*\,\d*$)" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_decimal %>"
                                Display="dynamic" CssClass="failure" Enabled="true">
                            </asp:RegularExpressionValidator>--%>
                        </td>
                        <td class="q4"><asp:TextBox ID="tbQ4" runat="server"  CssClass="ddlselect"  Text='<%# Bind("Q4")%>' TextMode="Number"></asp:TextBox>
<%--                            <asp:RegularExpressionValidator ID="revQ4" runat="server" ControlToValidate="tbQ4" ValidationGroup="KPIProject"
                                ValidationExpression="(^\d*\.?\d*[1-9]+\d*$)|(^[1-9]+\d*\.\d*$)|(^\d*\,?\d*[1-9]+\d*$)|(^[1-9]+\d*\,\d*$)" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_decimal %>"
                                Display="dynamic" CssClass="failure" Enabled="true">
                            </asp:RegularExpressionValidator>--%>
                        </td>
                        <td class="roi"><asp:TextBox ID="tbROI" runat="server"  CssClass="ddlselect"  Text='<%# Bind("ROI")%>' TextMode="Number"></asp:TextBox>
<%--                            <asp:RegularExpressionValidator ID="revROI" runat="server" ControlToValidate="tbROI" ValidationGroup="KPIProject"
                                ValidationExpression="(^\d*\.?\d*[1-9]+\d*$)|(^[1-9]+\d*\.\d*$)|(^\d*\,?\d*[1-9]+\d*$)|(^[1-9]+\d*\,\d*$)" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_decimal %>"
                                Display="dynamic" CssClass="failure" Enabled="true">
                            </asp:RegularExpressionValidator>--%>
                            <asp:RequiredFieldValidator ID="rfvROI" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                                ControlToValidate="tbROI" CssClass="failureedit" ValidationGroup="KPIProject"></asp:RequiredFieldValidator>
                        </td>
                        <td class="npv"><asp:TextBox ID="tbNPV" runat="server"  CssClass="ddlselect"  Text='<%# Bind("NPV")%>' TextMode="Number"></asp:TextBox>
<%--                            <asp:RegularExpressionValidator ID="revNPV" runat="server" ControlToValidate="tbNPV" ValidationGroup="KPIProject"
                                ValidationExpression="(^\d*\.?\d*[1-9]+\d*$)|(^[1-9]+\d*\.\d*$)|(^\d*\,?\d*[1-9]+\d*$)|(^[1-9]+\d*\,\d*$)" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_decimal %>"
                                Display="dynamic" CssClass="failure" Enabled="true">
                            </asp:RegularExpressionValidator>--%>
                            <asp:RequiredFieldValidator ID="rfvNPV" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                                ControlToValidate="tbNPV" CssClass="failureedit" ValidationGroup="KPIProject"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </EditItemTemplate>
            </asp:ListView>
        </div>
    </div>
</asp:Content>

