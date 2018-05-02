<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test"  MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="leaControls" Namespace="leaControls" TagPrefix="lea" %>


<%--<%@ Register TagPrefix="wuc" TagName="controlStatusProject" Src="~/WebUserControl/Strategic/controlStatusProject.ascx" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<%--    <script type="text/javascript">
        function LockScreen(message) {
            var lock = document.getElementById('lockPane');
            if (lock)
                lock.className = 'LockOn';
            lock.innerHTML = message;
        }
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">

<%--        <asp:Literal ID="lbllnfo" runat="server"></asp:Literal>
        <lea:lockPanel runat="server"></lea:lockPanel>--%>
        <%--<div id="lockPane" class="LockOff"></div>--%>
        <%--<wuc:controlStatusProject ID="controlStatusProject" runat="server" Change="true" IDProject="2" OutInfoText="false" Caption="" />--%>
<%--        <lea:TableControl ID="TableControl1" runat="server" StylePanel="InsUpdDel" StyleButton="img" ModeChange="true" MessageConfirmDelete="hjhjhjhvhvhvhv"/>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="task run" OnClick="Button2_Click" />

        <br />--%>
        <lea:InitInputDate ID="InitInputDate1" runat="server" />
        <lea:InputPeriodDateTime ID="InputPeriodDateTime1" runat="server" OnDateTimeChange="InputPeriodDateTime1_DateTimeChange1" />



        <lea:InputPeriodDateTime ID="InputPeriodDateTime2" runat="server"  OnDateTimeChange="InputPeriodDateTime2_DateTimeChange"/>

    </form>
</body>
</html>
