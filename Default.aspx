<%@ Page Title="" Language="C#" MasterPageFile="~/StartSite.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="description">
        <p>
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Resource, Descripton1 %>"></asp:Literal></p>
    </div>
    <ul class="ca-menu">
        <li>
            <a href="http://krr-fas41/dpa_reports/Default.aspx">
                <span class="ca-icon">I</span>
                <div class="ca-content">
                    <h2 class="ca-main">
                        <asp:Literal ID="lbweb1" runat="server" Text="<%$ Resources:Resource, web1 %>"></asp:Literal></h2>
                    <%--<h3 class="ca-sub">тест</h3>--%>
                </div>
            </a>
        </li>
        <li>
            <a href="WebSite/Strategic/Default.aspx">
                <span class="ca-icon">H</span>
                <div class="ca-content">
                    <h2 class="ca-main">
                        <asp:Literal ID="lbweb2" runat="server" Text="<%$ Resources:Resource, web2 %>"></asp:Literal></h2>
                    <%--<h3 class="ca-sub">Advanced use of technology</h3>--%>
                </div>
            </a>
        </li>
        <li>
            <a href="#">
                <span class="ca-icon" id="heart">A</span>
                <div class="ca-content">
                    <h2 class="ca-main">
                        <asp:Literal ID="lbweb3" runat="server" Text="<%$ Resources:Resource, web3 %>"></asp:Literal></h2>
                    <%--<h3 class="ca-sub">Understanding visually</h3>--%>
                </div>
            </a>
        </li>
        <li>
            <a href="http://krr-www-parlw01.europe.mittalco.com/">
                <span class="ca-icon">K</span>
                <div class="ca-content">
                    <h2 class="ca-main">
                        <asp:Literal ID="lbweb4" runat="server" Text="<%$ Resources:Resource, web4 %>"></asp:Literal></h2>
                    <%--<h3 class="ca-sub">Professionals in action</h3>--%>
                </div>
            </a>
        </li>
        <li>
            <a href="WebSite/Setup/Default.aspx">
                <span class="ca-icon">S</span>
                <div class="ca-content">
                    <h2 class="ca-main">
                        <asp:Literal ID="lbweb5" runat="server" Text="<%$ Resources:Resource, web5 %>"></asp:Literal></h2>
                    <%--<h3 class="ca-sub">24/7 for you needs</h3>--%>
                </div>
            </a>
        </li>
    </ul>
    <div class="more">
        <ul>
            <li><asp:Literal ID="ltmm" runat="server" Text="<%$ Resources:Resource, menumore %>"></asp:Literal></li>
            <li><a href="Design.aspx"><asp:Literal ID="ltmmHelp" runat="server" Text="<%$ Resources:Resource, mmHelp %>"></asp:Literal></a></li>
            <li><a href="AccessWeb.aspx"><asp:Literal ID="ltmmAccess" runat="server" Text="<%$ Resources:Resource, mmAccess %>"></asp:Literal></a></li>
            <li><a href="Design.aspx"><asp:Literal ID="ltmmStatistic" runat="server" Text="<%$ Resources:Resource, mmStatistic %>"></asp:Literal></a></li>
            <li><a href="WebSite/Strategic/About.aspx"><asp:Literal ID="ltmmAbout" runat="server" Text="<%$ Resources:Resource, mmAbout %>"></asp:Literal></a></li>
<%--            <li><a href="#">Example 5</a></li>
            <li><a href="#">Example 6</a></li>
            <li><a href="#">Example 7</a></li>
            <li><a href="#">Example 8</a></li>
            <li><a href="#">Example 9</a></li>
            <li><a href="#">Example 10</a></li>--%>
        </ul>
    </div>

    </asp:Content>

