<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WebSite_Strategic_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title></title>
    <link href="css/default.css" rel="stylesheet" type="text/css" />
<%--    <link href="../../Styles/DefaultSSD.css" rel="stylesheet" type="text/css" />--%>
    		<script type="text/javascript" src="scripts/default/jquery.min.js"></script>
		<script type="text/javascript" src="scripts/default/jquery.easing.1.3.js"></script>
		<script type="text/javascript" src="scripts/default/jquery.bgImageMenu.js"></script>
		<script type="text/javascript">
		    $(function () {
		        $('#sbi_container').bgImageMenu({
		            defaultBg: 'image/default.jpg',
		            border: 1,
		            type: {
		                mode: 'seqHorizontalSlide',
		                speed: 250,
		                easing: 'jswing',
		                seqfactor: 100
		            }
		        });
		    });
		</script>

</head>
<body>
    <form id="form1" runat="server">
		<div class="container">
			<div class="header">
				<h1><asp:Literal ID="ltTitle1" runat="server" Text="<%$ Resources:ResourceStrategic, df_Title1 %>"></asp:Literal><span><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:ResourceStrategic, df_Title2 %>"></asp:Literal></span></h1>
				<h2><asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:ResourceStrategic, df_Title3 %>"></asp:Literal></h2>
			</div>
			<div class="content">
				<div id="sbi_container" class="sbi_container">
					<div class="sbi_panel" data-bg="image/1.jpg">
						<a href="#" class="sbi_label"><asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:ResourceStrategic, df_menu1 %>"></asp:Literal></a>
						<div class="sbi_content">
							<ul>
                                <li><a href="<%# csitemap.GetPathUrl(17) %>"><asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:ResourceStrategic, df_menu11 %>"></asp:Literal></a></li>
								<li><a href="<%# csitemap.GetPathUrl(20) %>"><asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:ResourceStrategic, df_menu12 %>"></asp:Literal></a></li>
								<li><a href="<%# csitemap.GetPathUrl(23) %>"><asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:ResourceStrategic, df_menu13 %>"></asp:Literal></a></li>
								<li><a href="#"><asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:ResourceStrategic, df_menu14 %>"></asp:Literal></a></li>
							</ul>
						</div>
					</div>
					<div class="sbi_panel" data-bg="image/2.jpg">
						<a href="#" class="sbi_label"><asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:ResourceStrategic, df_menu2 %>"></asp:Literal></a>
						<div class="sbi_content">
							<ul>
<%--								<li><a href="<%# csitemap.GetPathUrl(14) %>%26IDPrj=0"><asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:ResourceStrategic, df_menu21 %>"></asp:Literal></a></li>--%>
								<li><a href="<%# csitemap.GetPathUrl(15) %>"><asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:ResourceStrategic, df_menu22 %>"></asp:Literal></a></li>
								<li><a href="<%# csitemap.GetPathUrl(16) %>"><asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:ResourceStrategic, df_menu23 %>"></asp:Literal></a></li>
							</ul>
						</div>
					</div>
					<div class="sbi_panel" data-bg="image/3.jpg">
						<a href="#" class="sbi_label"><asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:ResourceStrategic, df_menu3 %>"></asp:Literal></a>
						<div class="sbi_content">
							<ul>
								<li><a href="<%# csitemap.GetPathUrl(18) %>"><asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:ResourceStrategic, df_menu31 %>"></asp:Literal></a></li>
<%--								<li><a href="#"><asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:ResourceStrategic, df_menu32 %>"></asp:Literal></a></li>--%>
								<li><a href="About.aspx"><asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:ResourceStrategic, df_menu33 %>"></asp:Literal></a></li>
							</ul>
						</div>
					</div>
				</div>
			</div>
			<%-- <div class="more">
				<ul>
					<li>More examples:</li>
					<li><a href="example1.html">Show/hide</a></li>
					<li><a href="example2.html">Fade</a></li>
					<li><a href="example3.html">Sequential fade</a></li>
					<li><a href="example4.html">Side slide</a></li>
					<li><a href="example4_1.html">Side slide (bounce)</a></li>
					<li class="selected"><a href="example5.html">Sequential slide</a></li>
					<li><a href="example6.html">Up/down</a></li>
					<li><a href="example7.html">Sequential up/down</a></li>
					<li><a href="example8.html">Alternating up/down</a></li>
					<li><a href="example8_1.html">Alternating up/down (2)</a></li>
					<li><a href="example9.html">Seq. alt. up/down</a></li>
				</ul>
			</div>--%>
            <div class="topbar">
                <a href="../../Default.aspx">
                    <strong>&laquo;<asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Resource, menu %>"></asp:Literal></strong>
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Resource, mesHome %>"></asp:Literal></a>
                <span class="right_ab">
                    <a href='mailto:Nikita.Shidlovskiy@arcelormittal.com'>
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:ResourceStrategic, FooterEmailBoss %>"></asp:Literal></a>
                </span>
            </div>
		</div>
    </form>
</body>
</html>
