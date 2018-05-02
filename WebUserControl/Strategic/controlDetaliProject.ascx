<%@ Control Language="C#" AutoEventWireup="true" CodeFile="controlDetaliProject.ascx.cs" Inherits="controlDetaliProject" %>
<%@ Register TagPrefix="wuc" TagName="panelUpr" Src="~/WebUserControl/panelUpr.ascx" %>
<style type="text/css">
    table.tabList
    {
        border: 1px solid #999999;
        color: #000000;
        border-radius: 3px;
        background-color: #F7F6F3;
        font-family: 'Arial Narrow';
        width: 1400px;
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
    }

    .tabList td
    {
        padding: 5px;
        border: 1px solid #999999;
        /*color: #000000;
        background-color: #FFFFFF;*/
    }

    .tabList tr.item
    {
        background-color: #FFFFFF;
        color: #000000;
    }

    .tabList tr.select
    {
        background-color: #999999;
        color: #FFFFFF;
        text-transform: uppercase;
    }

    .tabList tr.current
    {
        background-color: #33CCCC;
        color: #FFFFFF;
        text-transform: uppercase;
        font-weight: bold;
    }

    .tabList tr.edit
    {
        background-color: #999999;
        color: #FFFFFF;
        text-transform: uppercase;
    }

    .tabList #skip
    {
        background-color: #FFFFFF;
        color: #C0C0C0;
        text-decoration: line-through;
    }

    th.upr
    {
        padding: 0px;
        width: 50px;
    }
    th.step
    {
        width: 250px;
    }
    th.start
    {
        width: 100px;
    }
    th.stop
    {
        width: 100px;
    }
    th.persent
    {
        width: 50px;
    }
    th.coment
    {
        width: 250px;
    }
    th.responsible
    {
        width: 250px;
    }
    th.files
    {
        width: 300px;
    }
    td.upr
    {
        text-align: center;
    }
    td.start, td.stop, td.persent
    {
        text-align: center;
    }
    td.step
    {
        text-align: left;
    }
    td.coment, td.responsible {
        text-align: left;
        font-style: italic;
    }
    td.files
    {
        text-align: left;
        text-transform:none;
    }
     tr.edit td.files a {
            color:yellow;
            text-transform: lowercase;
        }
    .LineText {
    width: 98%;
}
.MultiText {
    width: 98%;
    height: 50px;
}
.failure {
    text-transform: lowercase;
    color: Red;
}
.title
{
    font-family: Georgia, Times, 'Times New Roman' , serif;
    font-size: 24px;
    color: #808000;
    text-align: center;
}
</style>

<link rel="stylesheet" href="<%# base.csitemap.GetPathUrl("/css/date/daterangepicker.css") %>" type="text/css" />
<script src="<%# base.csitemap.GetPathUrl("/css/date/jquery-1.11.2.min.js") %>" type="text/javascript"></script>
<script src="<%# base.csitemap.GetPathUrl("/css/date/moment.min.js") %>" type="text/javascript"></script>
<script src="<%# base.csitemap.GetPathUrl("/css/date/jquery.daterangepicker.js") %>" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        var obj = '#<%=base.lvc.GetIDClientTextBox(lvTable,"tbFactStart") %>'
            if (obj == '#') return;
            $(obj).dateRangePicker(
            {
                autoClose: true,
                singleDate: true,
                showShortcuts: false,
                format: 'DD-MM-YYYY',
            }).bind('datepicker-change', function (evt, obj) {
                //LockScreen('Мы обрабатываем ваш запрос...');
                //location = CorectLocationSearch('selected', obj.date1);

            });
    });

    $(function () {
        var obj = '#<%=base.lvc.GetIDClientTextBox(lvTable,"tbFactStop") %>'
        if (obj == '#') return;
        $(obj).dateRangePicker(
        {
            autoClose: true,
            singleDate: true,
            showShortcuts: false,
            format: 'DD-MM-YYYY',
        }).bind('datepicker-change', function (evt, obj) {
            //LockScreen('Мы обрабатываем ваш запрос...');
            //location = CorectLocationSearch('selected', obj.date1);

        });
    });

</script>

<asp:ObjectDataSource ID="odsTable" runat="server" OnSelected="odsTable_Selected" OnSelecting="odsTable_Selecting" SelectMethod="SelectStepDetaliProject" TypeName="Strategic.classProject" OnUpdated="odsTable_Updated" OnUpdating="odsTable_Updating" UpdateMethod="UpdateStepDetaliProject" DeleteMethod="DeleteFilesStepDetali" InsertMethod="InsertFilesStepDetali">
    <DeleteParameters>
        <asp:Parameter Name="IDStepDetali" Type="Int32" />
        <asp:Parameter Name="IDFile" Type="Decimal" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IDStepDetali" Type="Int32" />
        <asp:Parameter Name="IDFile" Type="Decimal" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </InsertParameters>
    <SelectParameters>
        <asp:Parameter Name="IDProject" Type="Int32" />
        <asp:Parameter Name="skip" Type="Boolean" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="IDStepDetali" Type="Int32" />
        <asp:Parameter Name="FactStart" Type="DateTime" />
        <asp:Parameter Name="FactStop" Type="DateTime" />
        <asp:Parameter Name="Persent" Type="Int32" />
        <asp:Parameter Name="Coment" Type="String" />
        <asp:Parameter Name="Responsible" Type="String" />
        <asp:Parameter Name="OutResult" Type="Boolean" />
    </UpdateParameters>
</asp:ObjectDataSource>
<asp:ListView ID="lvTable" runat="server" DataSourceID="odsTable" DataKeyNames="IDStepDetali" OnSelectedIndexChanged="lvTable_SelectedIndexChanged" >
    <LayoutTemplate>
        <table class="tabList">
            <caption><asp:Label ID="lbStepProject" runat="server" Text="<%$ Resources:ResourceStrategic, titleStepProject %>" CssClass="title"></asp:Label></caption>
            <tr>
                <asp:Panel ID="upr" runat="server" OnLoad="upr_Load">
                    <th class="upr"></th>
                </asp:Panel>
                <th class="step">
                    <asp:Literal ID="ltProjectStep" runat="server" Text="<%$ Resources:ResourceStrategic, tsProjectStep %>"></asp:Literal></th>
                <th class="start">
                    <asp:Literal ID="ltStepStart" runat="server" Text="<%$ Resources:ResourceStrategic, tsStepStart %>"></asp:Literal></th>
                <th class="stop">
                    <asp:Literal ID="ltStepStop" runat="server" Text="<%$ Resources:ResourceStrategic, tsStepStop %>"></asp:Literal></th>
                <th class="persent">
                    <asp:Literal ID="ltStepPersent" runat="server" Text="<%$ Resources:ResourceStrategic, tsStepPersent %>"></asp:Literal></th>
                <th class="coment">
                    <asp:Literal ID="ltStepComent" runat="server" Text="<%$ Resources:ResourceStrategic, tsStepComent %>"></asp:Literal></th>
                <th class="responsible">
                    <asp:Literal ID="ltStepResponsible" runat="server" Text="<%$ Resources:ResourceStrategic, tsStepResponsible %>"></asp:Literal></th>
                <th class="files">
                    <asp:Literal ID="ltFiles" runat="server" Text="<%$ Resources:ResourceStrategic, tsStepFiles %>"></asp:Literal></th>
            </tr>
            <tr id="itemPlaceholder" runat="server" />
        </table>
    </LayoutTemplate>
    <ItemTemplate>
<%--<%# base.GetStringBool(Container.DataItem, "CurrentStep", "current", "item") %>--%>
        <tr class='<%# this.GetCurrentString(Container.DataItem) %>' id='<%# base.GetStringBool(Container.DataItem, "SkipStep", "skip", "") %>'>
            <td class="upr"  style="display:<%# base.GetDisplay(this.Change) %>">
                <asp:ImageButton ID="ibSelect" runat="server" /></td>
            <td class="step">
                <b><asp:LinkButton ID="lbSelect" runat="server" Text='<%# base.ctemplatessteps.GetStep(Container.DataItem) %>' CausesValidation="false" CommandName="Select" CommandArgument="IDStepDetali"
                    Visible='<%# GetVisibleSelect(Container.DataItem,this.Change) %>'></asp:LinkButton>
                <asp:Label ID="lbStep" runat="server" Text='<%# base.ctemplatessteps.GetStep(Container.DataItem) %>' Visible='<%# GetVisibleStep(Container.DataItem,this.Change) %>'></asp:Label></b>
                <br />(<%# base.ctemplatessteps.GetBigStepInStep(Container.DataItem) %>)
<%--                <asp:LinkButton ID="lbSelect" runat="server" Text='<%# base.ctemplatessteps.GetAllStep(Container.DataItem) %>' CausesValidation="false" CommandName="Select" CommandArgument="IDStepDetali"
                    Visible='<%# GetVisibleSelect(Container.DataItem,this.Change) %>'></asp:LinkButton>
                <asp:Label ID="lbStep" runat="server" Text='<%# base.ctemplatessteps.GetAllStep(Container.DataItem) %>' Visible='<%# GetVisibleStep(Container.DataItem,this.Change) %>'></asp:Label>--%>
            </td>
            <td class="start"><%# Eval("FactStart", "{0:dd-MM-yyy}") %></td>
            <td class="stop"><%# Eval("FactStop", "{0:dd-MM-yyy}") %></td>
            <td class="persent"><%# Eval("Persent", "{0:0}") %></td>
            <td class="coment"><%# Eval("Coment") %></td>
            <td class="responsible"><%# Eval("Responsible") %></td>
            <td class="files"><%# base.cproject.GetListFile(Container.DataItem) %></td>
        </tr>
    </ItemTemplate>
    <SelectedItemTemplate>
        <tr class="select">
            <td class="upr" style="display:<%# base.GetDisplay(this.Change) %>">
                <%--OnCurrentClick="pnSelect_CurrentClick"--%> 
                <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="UpdSkip" ConfirmSkip="MessageSkipStepProject" OnInit="pnSelect_Init" SizeImage="32" OnSkipClick="pnSelect_SkipClick"/>
            </td>
            <td class="step">
<%--                <%# base.ctemplatessteps.GetAllStep(Container.DataItem) %>--%>
                <b><%# base.ctemplatessteps.GetStep(Container.DataItem) %></b><br />
                (<%# base.ctemplatessteps.GetBigStepInStep(Container.DataItem) %>)
            </td>
            <td class="start"><%# Eval("FactStart", "{0:dd-MM-yyyy}") %></td>
            <td class="stop"><%# Eval("FactStop", "{0:dd-MM-yyyy}") %></td>
            <td class="persent"><%# Eval("Persent", "{0:0}") %></td>
            <td class="coment"><%# Eval("Coment") %></td>
            <td class="responsible"><%# Eval("Responsible") %></td>
            <td class="files"><%# base.cproject.GetListFile(Container.DataItem) %></td>
        </tr>
    </SelectedItemTemplate>
    <EditItemTemplate>
        <tr class="edit">
            <td class="upr" style="display:<%# base.GetDisplay(this.Change) %>">
                <wuc:panelUpr ID="pnSelect" runat="server" StylePanel="UpdCan" OnInit="pnSelect_Init" SizeImage="32" ValidationGroup="StepProject"/>
            </td>
            <td class="step">
<%--                <%# base.ctemplatessteps.GetAllStep(Container.DataItem) %>--%>
                <b><%# base.ctemplatessteps.GetStep(Container.DataItem) %></b><br />
                (<%# base.ctemplatessteps.GetBigStepInStep(Container.DataItem) %>)
            </td>
            <td class="start">
                <asp:TextBox ID="tbFactStart" runat="server" Text='<%# Eval("FactStart", "{0:dd-MM-yyyy}") %>' ></asp:TextBox>
            </td>
            <td class="stop">
                <asp:TextBox ID="tbFactStop" runat="server" Text='<%# Eval("FactStop", "{0:dd-MM-yyyy}") %>'></asp:TextBox></td>
            <td class="persent">
                <asp:DropDownList ID="ddlPersent" runat="server" SelectedValue='<%# Bind("Persent") %>'>
                    <asp:ListItem Value="0">0%</asp:ListItem>
                    <asp:ListItem Value="10">10%</asp:ListItem>
                    <asp:ListItem Value="20">20%</asp:ListItem>
                    <asp:ListItem Value="30">30%</asp:ListItem>
                    <asp:ListItem Value="40">40%</asp:ListItem>
                    <asp:ListItem Value="50">50%</asp:ListItem>
                    <asp:ListItem Value="60">60%</asp:ListItem>
                    <asp:ListItem Value="70">70%</asp:ListItem>
                    <asp:ListItem Value="80">80%</asp:ListItem>
                    <asp:ListItem Value="90">90%</asp:ListItem>
                    <asp:ListItem Value="100">100%</asp:ListItem>
                </asp:DropDownList></td>
            <td class="coment">
                <asp:TextBox ID="tbComent" runat="server" Text='<%# Eval("Coment") %>' CssClass="MultiText" TextMode="MultiLine"></asp:TextBox></td>
            <td class="responsible">
                <asp:TextBox ID="tbResponsible" runat="server" Text='<%# Eval("Responsible") %>' CssClass="MultiText" TextMode="MultiLine"></asp:TextBox></td>
            <td class="files">
                <asp:Repeater ID="fileList" runat="server" OnDataBinding="fileList_DataBinding">
                    <HeaderTemplate>
                        <ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <%# Eval("FileName") %>&nbsp;-&nbsp;<asp:LinkButton ID="lbDel" runat="server" Text="<%$ Resources:ResourceStrategic, delete %>" OnClientClick="<%$ Resources:Resource, MessageLock %>" OnClick="lbDel_Click" ToolTip='<%# Eval("Id") %>'></asp:LinkButton>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate></ul></FooterTemplate>
                </asp:Repeater>
                <asp:FileUpload ID="fuAdd" runat="server" />&nbsp;&nbsp;<asp:Button ID="btSave" runat="server" Text="<%$ Resources:ResourceStrategic, addFile %>" OnClick="btSave_Click" OnClientClick="<%$ Resources:Resource, MessageLock %>" />
            </td>
        </tr>
    </EditItemTemplate>
</asp:ListView>
