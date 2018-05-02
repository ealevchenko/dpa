<%@ Page Language="C#" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" /> 
    <title>О нас</title>
    <link href="http://fonts.googleapis.com/css?family=Lato:100,900" rel="stylesheet" type="text/css" />
    <link href="css/about.css" rel="stylesheet" type="text/css" />

    <script src="scripts/about/prefixfree.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="codrops-top">
            <a href="Default.aspx">
                <strong>&laquo;<asp:Literal ID="ltmenu" runat="server" Text="<%$ Resources:Resource, menu %>"></asp:Literal></strong>
                <asp:Literal ID="ltmesHome" runat="server" Text="<%$ Resources:Resource, mesStrategic %>"></asp:Literal></a>
            <span class="right">
                <a href="mailto:Eduard.Levchenko@arcelormittal.com">
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Resource, FooterEmail %>"></asp:Literal></a>
            </span>
            <div class="clr"></div>
        </div>
    <div id="wrapper">
    <h1>
        <asp:Literal ID="lttitle1" runat="server" Text="<%$ Resources: title1 %>"></asp:Literal><small><asp:Literal ID="lttitle2" runat="server" Text="<%$ Resources: title2 %>"></asp:Literal></small></h1>
    <div id="content">
        <h3><asp:Literal ID="ltsection1" runat="server" Text="<%$ Resources: section1 %>"></asp:Literal></h3>
        <div id="Description">
        <ul>
            <li><asp:Literal ID="ltsection11" runat="server" Text="<%$ Resources: section11 %>"></asp:Literal></li>
            <li><asp:Literal ID="ltsection12" runat="server" Text="<%$ Resources: section12 %>"></asp:Literal></li>
            <li><asp:Literal ID="ltsection13" runat="server" Text="<%$ Resources: section13 %>"></asp:Literal></li>
            <li><asp:Literal ID="ltsection14" runat="server" Text="<%$ Resources: section14 %>"></asp:Literal></li>
            <li><asp:Literal ID="ltsection15" runat="server" Text="<%$ Resources: section15 %>"></asp:Literal></li>
            <li><asp:Literal ID="ltsection16" runat="server" Text="<%$ Resources: section16 %>"></asp:Literal></li>
        </ul>
        </div>
        <ul id="push" class="profiles cf">
            <li>
                <img class="pic" src="image/About/Shidlovskiy.jpg" alt="Shef" />
                <ul class="info">
                    <li><a href="mailto:Nikita.Shidlovskiy@arcelormittal.com"><asp:Literal ID="ltShidlovskiy" runat="server" Text="<%$ Resources: Shidlovskiy %>"></asp:Literal></a></li>
                    <li><asp:Literal ID="ltpost1" runat="server" Text="<%$ Resources: post1 %>"></asp:Literal></li>
                    <li>Nikita.Shidlovskiy@arcelormittal.com</li>
                    <li>T 49  92113 | M +3 067 568 78 07</li>
                </ul>
            </li>
        </ul>
        <h3><br /><asp:Literal ID="ltsection2" runat="server" Text="<%$ Resources: section2 %>"></asp:Literal></h3>        
        <div id="Description">
            <ul>
                <li><asp:Literal ID="ltsection21" runat="server" Text="<%$ Resources: section21 %>"></asp:Literal></li>
                <li><asp:Literal ID="ltsection22" runat="server" Text="<%$ Resources: section22 %>"></asp:Literal></li>
                <li><asp:Literal ID="ltsection23" runat="server" Text="<%$ Resources: section23 %>"></asp:Literal></li>
                <li><asp:Literal ID="ltsection24" runat="server" Text="<%$ Resources: section24 %>"></asp:Literal></li>
                <li><asp:Literal ID="ltsection25" runat="server" Text="<%$ Resources: section25 %>"></asp:Literal></li>
                <li><asp:Literal ID="ltsection26" runat="server" Text="<%$ Resources: section26 %>"></asp:Literal></li>
                <li><asp:Literal ID="ltsection27" runat="server" Text="<%$ Resources: section27 %>"></asp:Literal></li>
            </ul>
        </div>
        <ul id="slide" class="profiles cf">
            <li>
                <img class="pic" src="image/About/Levchenko.jpg" alt="Admin" />
                <ul class="info">
                    <li><a href="mailto:Eduard.Levchenko@arcelormittal.com"><asp:Literal ID="ltLevchenko" runat="server" Text="<%$ Resources: Levchenko %>"></asp:Literal></a></li>
                    <li><asp:Literal ID="ltpost2" runat="server" Text="<%$ Resources: post2 %>"></asp:Literal></li>
                    <li>Eduard.Levchenko@arcelormittal.com</li>
                    <li>0974760178</li>
                </ul>
            </li>
            <li>
                <img class="pic" src="image/About/Stryukachev.jpg" alt="Razrab" />
                <ul class="info">
                    <li><a href="mailto:Konstantin.Stryukachev@arcelormittal.com"><asp:Literal ID="ltStryukachev" runat="server" Text="<%$ Resources: Stryukachev %>"></asp:Literal></a></li>
                    <li><asp:Literal ID="ltpost31" runat="server" Text="<%$ Resources: post3 %>"></asp:Literal></li>
                    <li>Konstantin.Stryukachev@arcelormittal.com</li>
                    <li>T 4996335 |M +380679327068 </li>
                </ul>
            </li>
            <li>
                <img class="pic" src="image/About/Vasilenko.jpg" alt="Razrab" />
                <ul class="info">
                    <li><a href="mailto:Igor.Vasilenko@arcelormittal.com"><asp:Literal ID="ltVasilenko" runat="server" Text="<%$ Resources: Vasilenko %>"></asp:Literal></a></li>
                    <li><asp:Literal ID="ltpost32" runat="server" Text="<%$ Resources: post3 %>"></asp:Literal></li>
                    <li>Igor.Vasilenko@arcelormittal.com</li>
                    <li>T 4996335 </li>
                </ul>
            </li>
        </ul>
        
        <h3><br /><asp:Literal ID="ltsection3" runat="server" Text="<%$ Resources: section3 %>"></asp:Literal></h3>
        <div id="Description">
            <ul>
                <li><asp:Literal ID="ltsection31" runat="server" Text="<%$ Resources: section31 %>"></asp:Literal></li>
                <li><asp:Literal ID="ltsection32" runat="server" Text="<%$ Resources: section32 %>"></asp:Literal></li>
                <li><asp:Literal ID="ltsection33" runat="server" Text="<%$ Resources: section33 %>"></asp:Literal></li>
                <li><asp:Literal ID="ltsection34" runat="server" Text="<%$ Resources: section34 %>"></asp:Literal></li>
                <li><asp:Literal ID="ltsection35" runat="server" Text="<%$ Resources: section35 %>"></asp:Literal></li>
                <li><asp:Literal ID="ltsection36" runat="server" Text="<%$ Resources: section36 %>"></asp:Literal></li>
                <li><asp:Literal ID="ltsection37" runat="server" Text="<%$ Resources: section37 %>"></asp:Literal></li>
            </ul>
        </div>
        <ul id="slide" class="profiles cf">
            <li>
                <img class="pic" src="image/About/Savenkov.jpg" alt="Menager" />
                <ul class="info">
                    <li><a href="mailto:Aleksandr.Savenkov@arcelormittal.com"><asp:Literal ID="ltSavenkov" runat="server" Text="<%$ Resources: Savenkov %>"></asp:Literal></a></li>
                    <li><asp:Literal ID="ltpost41" runat="server" Text="<%$ Resources: post4 %>"></asp:Literal></li>
                    <li>Aleksandr.Savenkov@arcelormittal.com</li>
                </ul>
            </li>
            <li>
                <img class="pic" src="image/About/Telegin.jpg" alt="Menager" />
                <ul class="info">
                    <li><a href="mailto:Aleksandr.Telegin@arcelormittal.com"><asp:Literal ID="ltTelegin" runat="server" Text="<%$ Resources: Telegin %>"></asp:Literal></a></li>
                    <li><asp:Literal ID="ltpost42" runat="server" Text="<%$ Resources: post4 %>"></asp:Literal></li>
                    <li>Aleksandr.Telegin@arcelormittal.com</li>
                </ul>
            </li>
            <li>
                <img class="pic" src="image/About/Leutskiy.jpg" alt="Menager" />
                <ul class="info">
                    <li><a href="mailto:Andrey.Leutskiy@arcelormittal.com"><asp:Literal ID="ltLeutskiy" runat="server" Text="<%$ Resources: Leutskiy %>"></asp:Literal></a></li>
                    <li><asp:Literal ID="ltpost43" runat="server" Text="<%$ Resources: post4 %>"></asp:Literal></li>
                    <li>Andrey.Leutskiy@arcelormittal.com</li>
                </ul>
            </li>
            <li>
                <img class="pic" src="image/About/Burak.png" alt="Menager" />
                <ul class="info">
                    <li><a href="mailto:Andrej.Burak@arcelormittal.com"><asp:Literal ID="ltBurak" runat="server" Text="<%$ Resources: Burak %>"></asp:Literal></a></li>
                    <li><asp:Literal ID="ltpost44" runat="server" Text="<%$ Resources: post4 %>"></asp:Literal></li>
                    <li>Andrej.Burak@arcelormittal.com</li>
                </ul>
            </li>
            <li>
                <img class="pic" src="image/About/bokiy.jpg" />
                <ul class="info">
                    <li><a href="mailto:Aleksandr.Bokiy@arcelormittal.com"><asp:Literal ID="ltBokiy" runat="server" Text="<%$ Resources: Bokiy %>"></asp:Literal></a></li>
                    <li><asp:Literal ID="ltpost45" runat="server" Text="<%$ Resources: post4 %>"></asp:Literal></li>
                    <li>Aleksandr.Bokiy@arcelormittal.com</li>
                </ul>
            </li>
            <li>
                <img class="pic" src="image/About/Romanets.jpg"/>
                <ul class="info">
                    <li><a href="mailto:Albert.Romanets@arcelormittal.com"><asp:Literal ID="ltRomanets" runat="server" Text="<%$ Resources: Romanets %>"></asp:Literal></a></li>
                    <li><asp:Literal ID="ltpost46" runat="server" Text="<%$ Resources: post4 %>"></asp:Literal></li>
                    <li>Albert.Romanets@arcelormittal.com</li>
                </ul>
            </li>
        </ul>
        <%--
        <h3>Техническая поддержка развития web-сайта</h3>
        <ul id="explode" class="profiles cf">
            <li>
                <img class="pic" src="images/addison.jpg" alt="Addison" />
                <ul class="info">
                    <li><a href="http://www.sxc.hu/photo/1287588" title="The girl near a combine">ввв</a></li>
                    <li>-</li>
                    <li>-</li>
                </ul>
            </li>
            <li>
                <img class="pic" src="images/natalie.jpg" alt="Natalie" />
                <ul class="info">
                    <li><a href="http://www.sxc.hu/photo/1317164" title="Girl poses on a coastal line">Natalie</a></li>
                    <li>natalie@mail.net</li>
                    <li>132 - 93 742 322</li>
                </ul>
            </li>
            <li>
                <img class="pic" src="images/lily.jpg" alt="Lily" />
                <ul class="info">
                    <li><a href="http://www.sxc.hu/photo/1310211" title="The girl poses on ruins">Lily</a></li>
                    <li>lily@mail.net</li>
                    <li>149 - 93 328 148</li>
                </ul>
            </li>
            <li>
                <img class="pic" src="images/grace.jpg" alt="Grace" />
                <ul class="info">
                    <li><a href="http://www.sxc.hu/photo/1376800" title="The girl puts shoes on in a snowfall">Grace</a></li>
                    <li>grace@mail.net</li>
                    <li>157 - 86 918</li>
                </ul>
            </li>
        </ul>
        --%>
    </div>
    </div>
    </form>
</body>
</html>
