<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SMTPEmael.aspx.cs" Inherits="SMTPEmael" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1"
                runat="server" Text="Кому:"></asp:Label>
            <br />
            <asp:TextBox ID="txtSendTo" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label2"
                runat="server" Text="Заголовок:"></asp:Label>
            <br />
            <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label3"
                runat="server" Text="Содержание:"></asp:Label>
            <br />
            <asp:TextBox ID="txtBody"
                runat="server" TextMode="MultiLine"
                Height="160px"></asp:TextBox>
            <br />
            <asp:Button ID="btnSend" runat="server"
                Text="Отправить" OnClick="btnSend_Click" />
            <br />
            <asp:Label ID="lblStatus" runat="server" Text="Статус"
                ForeColor="Red" Visible="False"></asp:Label>
            <br />
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
