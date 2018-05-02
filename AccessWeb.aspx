<%@ Page Title="" Language="C#" MasterPageFile="~/StartSite.master" AutoEventWireup="true" CodeFile="AccessWeb.aspx.cs" Inherits="AccessWeb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/accessweb.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ObjectDataSource ID="odsListSection" runat="server" SelectMethod="DDLListSectionCulture" TypeName="WebBase.classSection">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="Full" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div class="accessUsers">
        <asp:Wizard ID="wzAccessUsers" runat="server" OnFinishButtonClick="wzAccessUsers_FinishButtonClick" OnNextButtonClick="wzAccessUsers_NextButtonClick" OnLoad="wzAccessUsers_Load" OnSideBarButtonClick="wzAccessUsers_SideBarButtonClick">
            <LayoutTemplate>
                <div class="wsheader"><asp:PlaceHolder ID="headerPlaceholder" runat="server"></asp:PlaceHolder></div>
                <div class="wssidebar">
                    <asp:PlaceHolder ID="sideBarPlaceholder" runat="server"></asp:PlaceHolder>

                </div>
                <div class="wscontent">
                    <div class="wsnavigation">
                        <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
                    </div>
                    <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
                <div class="wsfooter"></div>
            </LayoutTemplate>
            <SideBarTemplate>
                <asp:DataList ID="SideBarList" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton ID="SideBarButton" runat="server" Enabled="false"></asp:LinkButton>
                    </ItemTemplate>
                    <SelectedItemStyle Font-Bold="True" />
                </asp:DataList>
            </SideBarTemplate>
            <StartNavigationTemplate>
                <asp:Button ID="StartNextButton" runat="server" CommandName="MoveNext" Text="<%$ Resources:ResourceBase, btNextStep %>" />
            </StartNavigationTemplate>
            <StepNavigationTemplate>
                <asp:Button ID="StepPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious"
                    Text="<%$ Resources:ResourceBase, btPreviousStep %>" />
                <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="<%$ Resources:ResourceBase, btNextStep %>" />
            </StepNavigationTemplate>
            <FinishNavigationTemplate>
                <asp:Button ID="FinishPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious"
                    Text="<%$ Resources:ResourceBase, btPreviousStep %>" />
                <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" Text="<%$ Resources:ResourceBase, btFinishStep %>" Enabled="true"/>
            </FinishNavigationTemplate>
             <HeaderTemplate>
                 <b><i><%= wzAccessUsers.ActiveStep.Title %></i></b>
                 <table class="tabHeader">
                     <tr>
                         <th><asp:Literal ID="lth1" runat="server" Text="<%$ Resources:ResourceBase, awHeader1 %>"></asp:Literal></th>
                         <th><asp:Literal ID="lth2" runat="server" Text="<%$ Resources:ResourceBase, awHeader2 %>"></asp:Literal></th>
                         <th><asp:Literal ID="lth3" runat="server" Text="<%$ Resources:ResourceBase, awHeader3 %>"></asp:Literal></th>
                         <th><asp:Literal ID="lth4" runat="server" Text="<%$ Resources:ResourceBase, awHeader4 %>"></asp:Literal></th>

                     </tr>
                     <tr>
                         <td><%# HttpContext.Current.User.Identity.Name %></td>
                         <td><%# this.GetDateCreate %></td>
                         <td><%# this.GetDateChange %></td>
                         <td><%# this.GetDateAccess %></td>
                     </tr>
                 </table>
             </HeaderTemplate>
            <WizardSteps>
                <asp:WizardStep ID="wsAccessUsers" runat="server" Title="<%$ Resources:ResourceBase, awTitleStep1 %>" StepType="Start">
                    <table class="InsertAccessUsers">
                        <tr>
                            <th>
                                <asp:Literal ID="ltEmail" runat="server" Text="<%$ Resources:ResourceBase, auEmail %>"></asp:Literal>:&nbsp;</th>
                            <td>
                                <asp:TextBox ID="tbEmail" runat="server" CssClass="LineTextNotNull"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="tbEmail"
                                    ValidationExpression=".*@.{2,}\..{2,}" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_Email %>"
                                    Display="dynamic" CssClass="failure">
                                </asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                                    ControlToValidate="tbEmail" CssClass="failure"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltDistribution" runat="server" Text="<%$ Resources:ResourceBase, auDistribution %>"></asp:Literal>:&nbsp;</th>
                            <td>
                                <asp:CheckBox ID="cbDistribution" runat="server" /></td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltSurname" runat="server" Text="<%$ Resources:ResourceBase, auSurname %>"></asp:Literal>:&nbsp;</th>
                            <td>
                                <asp:TextBox ID="tbSurname" runat="server" CssClass="LineTextNotNull"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSurname" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                                    ControlToValidate="tbSurname" CssClass="failure"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltName" runat="server" Text="<%$ Resources:ResourceBase, auName %>"></asp:Literal>:&nbsp;</th>
                            <td>
                                <asp:TextBox ID="tbName" runat="server" CssClass="LineTextNotNull"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                                    ControlToValidate="tbName" CssClass="failure"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltPatronymic" runat="server" Text="<%$ Resources:ResourceBase, auPatronymic %>"></asp:Literal>:&nbsp;</th>
                            <td>
                                <asp:TextBox ID="tbPatronymic" runat="server" CssClass="LineTextNotNull"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPatronymic" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                                    ControlToValidate="tbPatronymic" CssClass="failure"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltPost" runat="server" Text="<%$ Resources:ResourceBase, auPost %>"></asp:Literal>:&nbsp;</th>
                            <td>
                                <asp:TextBox ID="tbPost" runat="server" CssClass="MultiTextNotNull" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPost" runat="server" ErrorMessage="<%$ Resources:ResourceMessage, mes_err_NoNull %>"
                                    ControlToValidate="tbPost" CssClass="failure"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Literal ID="ltSection" runat="server" Text="<%$ Resources:ResourceBase, auSection %>"></asp:Literal>:&nbsp;</th>
                            <td>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="LineTextNotNull" DataSourceID="odsListSection" DataTextField="Section" DataValueField="IDSection"></asp:DropDownList></td>
                        </tr>
                    </table>
                </asp:WizardStep>
                <asp:WizardStep ID="wsAccessWeb" runat="server" Title="<%$ Resources:ResourceBase, awTitleStep2 %>" StepType="Step">
                    <asp:Label ID="lbResultAccessWeb" runat="server" Text="" CssClass="failure"></asp:Label>
                    <asp:ListView ID="lvAccessWeb" runat="server" DataSource="<%# this.list_awue %>" DataKeyNames="ID">
                        <LayoutTemplate>
                            <table class="tabAccessWeb">
                                <tr>
                                    <th class="access">
                                        <asp:Literal ID="ltAccess" runat="server" Text="<%$ Resources:ResourceBase, auAccess %>"></asp:Literal></th>
                                    <th class="accessweb">
                                        <asp:Literal ID="ltAccessWeb" runat="server" Text="<%$ Resources:ResourceBase, auAccessWeb %>"></asp:Literal></th>
                                    <th class="ownerweb">
                                        <asp:Literal ID="ltOwnerWeb" runat="server" Text="<%$ Resources:ResourceBase, auOwnerWeb %>"></asp:Literal></th>
                                    <th class="daterequest">
                                        <asp:Literal ID="ltDateRequest" runat="server" Text="<%$ Resources:ResourceBase, auDateRequest %>"></asp:Literal></th>
                                    <th class="dateapproval">
                                        <asp:Literal ID="ltDateApproval" runat="server" Text="<%$ Resources:ResourceBase, auDateApproval %>"></asp:Literal></th>
                                    <th class="solution">
                                        <asp:Literal ID="ltSolution" runat="server" Text="<%$ Resources:ResourceBase, auSolution %>"></asp:Literal></th>
                                    <th class="coment">
                                        <asp:Literal ID="ltComent" runat="server" Text="<%$ Resources:ResourceBase, auComent %>"></asp:Literal></th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server" />
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class="item">
                                <td class="access">
                                    <asp:Label ID="lbID" runat="server" Visible="false" Text='<%# Eval("ID") %>' ToolTip='<%# Eval("IDWeb") %>'></asp:Label>
                                    <asp:CheckBox ID="cbAccess" runat="server" Checked='<%# caccessusers.GetAccessChecked(Container.DataItem) %>' Enabled='<%# caccessusers.GetAccessEnabled(Container.DataItem) %>' />
                                </td>
                                <td class="accessweb">
                                    <%# cweb.GetDescriptionCulture(Container.DataItem) %>
                                </td>
                                <td class="ownerweb">
                                    <a href='mailto:<%# cweb.GetEmailOwner(Container.DataItem) %>'><asp:Literal ID="lttxtemmail" runat="server" Text='<%# cweb.GetEmailOwner(Container.DataItem) %>'></asp:Literal></a>
                                </td>
                                <td class="daterequest"><%# Eval("DateRequest") %></td>
                                <td class="dateapproval"><%# Eval("DateApproval") %></td>
                                <td class="solution"><%# caccessusers.GetSolution(Container.DataItem,"ID") %></td>
                                <td class="coment"><%# Eval("Coment") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </asp:WizardStep>
                <asp:WizardStep ID="wsAccessInfo" runat="server" Title="<%$ Resources:ResourceBase, awTitleStep3 %>" StepType="Finish">
                <asp:Label ID="lbResultAccessInfo" runat="server" Text="" CssClass="failure"></asp:Label>
                    <table class="tabTotal">
                        <tr>
                            <th><asp:Literal ID="lttEmail" runat="server" Text="<%$ Resources:ResourceBase, auEmail %>"></asp:Literal>:&nbsp;</th>
                            <td>
                                <asp:Label ID="lbEmail" runat="server" CssClass="change"></asp:Label>&nbsp;-&nbsp;
                                <asp:Label ID="lbNewEmail" runat="server" CssClass="new"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th><asp:Literal ID="lttDistribution" runat="server" Text="<%$ Resources:ResourceBase, auDistribution %>"></asp:Literal>:&nbsp;</th>
                            <td>
                                <asp:Label ID="lbDistribution" runat="server" CssClass="change"></asp:Label>&nbsp;-&nbsp;
                                <asp:Label ID="lbNewDistribution" runat="server" CssClass="new"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th><asp:Literal ID="lttSurname" runat="server" Text="<%$ Resources:ResourceBase, auSurname %>"></asp:Literal>:&nbsp;</th>
                            <td>
                                <asp:Label ID="lbSurname" runat="server" CssClass="change"></asp:Label>&nbsp;-&nbsp;
                                <asp:Label ID="lbNewSurname" runat="server" CssClass="new"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th><asp:Literal ID="lttName" runat="server" Text="<%$ Resources:ResourceBase, auName %>"></asp:Literal>:&nbsp;</th>
                            <td>
                                <asp:Label ID="lbName" runat="server" CssClass="change"></asp:Label>&nbsp;-&nbsp;
                                <asp:Label ID="lbNewName" runat="server" CssClass="new"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th><asp:Literal ID="lttPatronymic" runat="server" Text="<%$ Resources:ResourceBase, auPatronymic %>"></asp:Literal>:&nbsp;</th>
                            <td>
                                <asp:Label ID="lbPatronymic" runat="server" CssClass="change"></asp:Label>&nbsp;-&nbsp;
                                <asp:Label ID="lbNewPatronymic" runat="server" CssClass="new"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th><asp:Literal ID="lttPost" runat="server" Text="<%$ Resources:ResourceBase, auPost %>"></asp:Literal>:&nbsp;</th>
                            <td>
                                <asp:Label ID="lbPost" runat="server" CssClass="change"></asp:Label>&nbsp;-&nbsp;
                                <asp:Label ID="lbNewPost" runat="server" CssClass="new"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th><asp:Literal ID="lttSection" runat="server" Text="<%$ Resources:ResourceBase, auSection %>"></asp:Literal>:&nbsp;</th>
                            <td>
                                <asp:Label ID="lbSection" runat="server" CssClass="change"></asp:Label>&nbsp;-&nbsp;
                                <asp:Label ID="lbNewSection" runat="server" CssClass="new"></asp:Label>
                            </td>
                        </tr>
                        <asp:PlaceHolder ID="phtr" runat="server"></asp:PlaceHolder>
                    </table>
                </asp:WizardStep>
                <asp:WizardStep ID="wsAccessResult"  runat="server" StepType="Complete" Title="<%$ Resources:ResourceBase, awTitleStep4 %>">
                    <div class="divResult">
                        <asp:Literal ID="ltResult1" runat="server" Text="<%$ Resources:ResourceBase, awResult1 %>"></asp:Literal>:&nbsp;<%# HttpContext.Current.User.Identity.Name %><br /><asp:Image ID="Image1" runat="server" ImageUrl="~/image/access/AccessWebUsers.jpg" /><br />
                        <asp:Literal ID="ltResult2" runat="server" Text="<%$ Resources:ResourceBase, awResult2 %>" Visible="false"></asp:Literal>
                        <asp:Literal ID="ltResult21" runat="server" Text='<%# DateTime.Now.ToString("dd:MM:yyyy HH:mm:ss") %>' Visible="false"></asp:Literal>
                        <asp:Literal ID="ltResult3" runat="server" Text="<%$ Resources:ResourceBase, awResult3 %>" Visible="true"></asp:Literal>
                    </div>

                </asp:WizardStep>
            </WizardSteps>
        </asp:Wizard>
    </div>
</asp:Content>

