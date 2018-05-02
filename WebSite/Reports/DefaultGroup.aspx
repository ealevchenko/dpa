<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultGroup.aspx.cs" Inherits="Reports_DefaultGroup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" /> 
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>
        <asp:Literal ID="ltTitle" runat="server"></asp:Literal></title>
    <link href="css/defaultgroup.css" rel="stylesheet" type="text/css" />
    <link href='http://fonts.googleapis.com/css?family=Josefin+Slab' rel='stylesheet' type='text/css' />
    <script type="text/javascript" src="scripts/default/jquery.min.js"></script> 
</head>
<body>
    <form id="form1" runat="server">
                <div class="container">
            <div class="header">
                <a href="Default.aspx">
                    <strong>&laquo;<asp:Literal ID="ltmenu" runat="server" Text="<%$ Resources: menu %>"></asp:Literal></strong><asp:Literal ID="ltmesHome" runat="server" Text="<%$ Resources: mesHome %>"></asp:Literal>
                </a>
                <span class="right">
                    <strong> <asp:Label ID="lbLanguage" runat="server" Text="<%$ Resources:Resource, Language %>"></asp:Label></strong>
                    <asp:DropDownList ID="Language1" runat="server" AutoPostBack="True" SelectedValue='<%$ Resources:Resource, Culture %>'>
                        <asp:ListItem Value="ru-RU">Russian (RU)</asp:ListItem>
                        <asp:ListItem Value="en-US">English (US)</asp:ListItem>
                    </asp:DropDownList>
                    <a href="mailto:Eduard.Levchenko@arcelormittal.com">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: FooterEmail %>"></asp:Literal></a>
                </span>
                <div class="clr"></div>
            </div>
            <h1>
                <asp:Literal ID="ltTitlemenu" runat="server"></asp:Literal><span>
                <asp:Literal ID="ltrwebsiteType" runat="server" Text="<%$ Resources: websiteType %>" /></span></h1>
            <div class="content">
                <ul class="bmenu">
                    <asp:Literal ID="ltmensub" runat="server"></asp:Literal>
                </ul>
                <div class="more">
                <ul>
                    <li><asp:Literal ID="ltmm" runat="server" Text="<%$ Resources: menumore %>"></asp:Literal></li>
                    <li><a href="Account/HELP/help1_1.aspx?tab=1"><asp:Literal ID="ltmmHelp" runat="server" Text="<%$ Resources: mmHelp %>"></asp:Literal></a></li>
                    <li><a href="#"><asp:Literal ID="ltmmAccess" runat="server" Text="<%$ Resources: mmAccess %>"></asp:Literal></a></li>
                    <li><a href="#"><asp:Literal ID="ltmmContact" runat="server" Text="<%$ Resources: mmContact %>"></asp:Literal></a></li>
                    <li><a href="#"><asp:Literal ID="ltmmAbout" runat="server" Text="<%$ Resources: mmAbout %>"></asp:Literal></a></li>
                </ul>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
