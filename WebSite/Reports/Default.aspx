<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="reports_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><asp:Literal ID="lt1" runat="server" Text="<%$ Resources: websiteType %>"></asp:Literal></title>
    <link href="css/default.css" rel="stylesheet" type="text/css" />
    <link href='http://fonts.googleapis.com/css?family=PT+Sans+Narrow&v1' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Wire+One&v1' rel='stylesheet' type='text/css' />
    <script type="text/javascript" src="scripts/default/jquery.min.js"></script>  
    <script type="text/javascript" src="scripts/default/jquery-ui.min.js"></script>  
    <script type="text/javascript" src="scripts/default/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="scripts/default/jquery.iconmenu.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#sti-menu').iconmenu();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
                <div class="container">
            <h1>
                <asp:Literal ID="ltrwebsiteType" runat="server" Text="<%$ Resources: websiteType %>" />
                <span>&nbsp;&nbsp;
                <asp:Label ID="Language" runat="server" Text="<%$ Resources: Language %>"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="Language1" runat="server" AutoPostBack="True" SelectedValue='<%$ Resources:Resource, Culture %>'>
                    <asp:ListItem Value="ru-RU">Russian (RU)</asp:ListItem>
                    <asp:ListItem Value="en-US">English (US)</asp:ListItem>
                </asp:DropDownList>
                </span>
            </h1>
            <div class="Welcome">
                <asp:LoginView ID="HeadLoginView" runat="server" EnableViewState="true">
                    <LoggedInTemplate>
                        <span class="bold">
                            <asp:Literal ID="Welcome" runat="server" Text="<%$ Resources: Welcome %>"></asp:Literal>
                            <asp:LoginName ID="HeadLoginName" runat="server" />
                        </span>
                    </LoggedInTemplate>
                </asp:LoginView>
            </div>
            <ul id="sti-menu" class="sti-menu">
                <li data-hovercolor="#ff395e">
                    <a href="DefaultGroup.aspx?Index=2">
                        <h2 data-type="mText" class="sti-item"><asp:Literal ID="lttu" runat="server" Text="<%$ Resources: cto %>"></asp:Literal></h2>
                        <h3 data-type="sText" class="sti-item"><asp:Literal ID="lttufull" runat="server" Text="<%$ Resources: ctofull %>"></asp:Literal></h3>
                        <span data-type="icon" class="sti-icon sti-icon-scto sti-item"></span>
                    </a>
                </li>
                <li data-hovercolor="#37c5e9">
                    <a href="DefaultGroup.aspx?Index=1">
                        <h2 data-type="mText" class="sti-item"><asp:Literal ID="ltadd" runat="server" Text="<%$ Resources: add %>"></asp:Literal></h2>
                        <h3 data-type="sText" class="sti-item"><asp:Literal ID="Liaddfull" runat="server" Text="<%$ Resources: addfull %>"></asp:Literal></h3>
                        <span data-type="icon" class="sti-icon sti-icon-add sti-item"></span>
                    </a>
                </li>
                <li data-hovercolor="#ffdd3f">
                    <a href="DefaultGroup.aspx?Index=5">
                        <h2 data-type="mText" class="sti-item"><asp:Literal ID="ltsd" runat="server" Text="<%$ Resources: sd %>"></asp:Literal></h2>
                        <h3 data-type="sText" class="sti-item"><asp:Literal ID="ltsdfull" runat="server" Text="<%$ Resources: sdfull %>"></asp:Literal></h3>
                        <span data-type="icon" class="sti-icon sti-icon-sd sti-item"></span>
                    </a>
                </li>
                <li data-hovercolor="#57e676">
                    <a href="DefaultGroup.aspx?Index=3">
                        <h2 data-type="mText" class="sti-item"><asp:Literal ID="ltrd" runat="server" Text="<%$ Resources: rd %>"></asp:Literal></h2>
                        <h3 data-type="sText" class="sti-item"><asp:Literal ID="ltrdfull" runat="server" Text="<%$ Resources: rdfull %>"></asp:Literal></h3>
                        <span data-type="icon" class="sti-icon sti-icon-rd sti-item"></span>
                    </a>
                </li>
                <li data-hovercolor="#d869b2">
                    <a href="DefaultGroup.aspx?Index=4">
                        <h2 data-type="mText" class="sti-item"><asp:Literal ID="lted" runat="server" Text="<%$ Resources: ed %>"></asp:Literal></h2>
                        <h3 data-type="sText" class="sti-item"><asp:Literal ID="ltedfull" runat="server" Text="<%$ Resources: edfull %>"></asp:Literal></h3>
                        <span data-type="icon" class="sti-icon sti-icon-ed sti-item"></span>
                    </a>
                </li>

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
            <div class="footer">
                <a href="../Strategic/Default.aspx"><span>©&nbsp;Copyright 02.2015г.
                    <asp:Label ID="lbFooter" runat="server" Text="<%$ Resources: Footer %>"></asp:Label></span></a>
                <span class="right_ab">
                    <a href="mailto:Eduard.Levchenko@arcelormittal.com">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: FooterEmail %>"></asp:Literal></a>

                </span>
            </div>
        </div>
<%--    <div><asp:Label ID="lblInfo" runat="server" Text="Label"></asp:Label></div>--%>
    </form>
</body>
</html>
