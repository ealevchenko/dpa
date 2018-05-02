<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WebSite_RailWay_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>RailWay</title>
  <meta charset="utf-8"/>
  <meta http-equiv="X-UA-Compatible" content="IE=edge" />
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
  <link rel="stylesheet" href="css/default/bootstrap.css" type="text/css" />
  <link rel="stylesheet" href="css/default/prism.css" type="text/css" />
  <link rel="stylesheet" href="css/default/font-awesome.min.css" type="text/css" />
  <link rel="stylesheet" href="css/default/animate.min.css" type="text/css" />
  <link rel="stylesheet" href="css/default/creative.css" type="text/css" />

  <!-- popSelect -->
  <link rel="stylesheet" href="css/default/jquery.popSelect.css" type="text/css" />

</head>

<body id="page-top">

  <nav id="mainNav" class="navbar navbar-default navbar-fixed-top">
    <div class="container-fluid">
      <!-- Brand and toggle get grouped for better mobile display -->
      <div class="navbar-header">
<%--        <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#toggle-navbar">
          <span class="sr-only">Toggle navigation</span>
          <span class="icon-bar"></span>
          <span class="icon-bar"></span>
          <span class="icon-bar"></span>
        </button>--%>
        <a class="navbar-brand page-scroll" href="#page-top"><asp:Literal ID="ltnw" runat="server" Text="<%$ Resources:ResourceRailWay, nameWeb %>"></asp:Literal></a>
      </div>

      <!-- Collect the nav links, forms, and other content for toggling -->
      <div class="collapse navbar-collapse" id="toggle-navbar">
        <ul class="nav navbar-nav navbar-right">
          <li>
            <a class="page-scroll" href="#setup"><asp:Literal ID="ltAdmin" runat="server" Text="<%$ Resources:ResourceRailWay, mmAdmin %>"></asp:Literal></a>
          </li>
          <li>
            <a class="page-scroll" href="#instructions"><asp:Literal ID="ltInstructions" runat="server" Text="<%$ Resources:ResourceRailWay, mmInstructions %>"></asp:Literal></a>
          </li>
          <li>
            <a class="page-scroll" href="#report"><asp:Literal ID="ltReport" runat="server" Text="<%$ Resources:ResourceRailWay, mmReport %>"></asp:Literal></a>
          </li>
          <li>
            <a class="page-scroll" href="#contact"><asp:Literal ID="ltContact" runat="server" Text="<%$ Resources:ResourceRailWay, mmContact %>"></asp:Literal></a>
          </li>
        </ul>
      </div>
      <!-- /.navbar-collapse -->
    </div>
    <!-- /.container-fluid -->
  </nav>

  <header>
    <div class="header-content">
      <div class="header-content-inner">
          <h1><asp:Literal ID="ltNameWeb" runat="server" Text="<%$ Resources:ResourceRailWay, nameWeb %>"></asp:Literal></h1>
          <hr />
          <p><asp:Literal ID="ltDescriptionWeb" runat="server" Text="<%$ Resources:ResourceRailWay, descriptonWeb %>"></asp:Literal></p>
        <form class="form form-horizontal">
          <div class="form-group m-t-4xl">
            <div class="col-xs-12">
              <div class="center-block" id="main-example-container">
                <select name="main-example" id="main-example" class="form-control">
                  <option value="India">Аглофабрика</option>
                  <option value="US">Восточная-Приемоотправочная</option>
                  <option value="Germany">Восточная-Разгрузочная</option>
                  <option value="UK">Копровая-1</option>
                  <option value="Australia" selected="selected">Прокатная-1</option>
                </select>
              </div>
            </div>
          </div>
        </form>
      </div>
    </div>
  </header>

  <section class="bg-primary" id="setup">
    <div class="container">
      <div class="row">
        <div class="col-lg-8 col-lg-offset-2 text-center">
          <h2 class="section-heading"><asp:Literal ID="lttitle1" runat="server" Text="<%$ Resources:ResourceRailWay, titleAdmin %>"></asp:Literal></h2>
          <hr class="light"/>
        </div>
      </div>
      <div class="row m-3t">
        <div class="col-md-4 text-center">
          <div class="service-box">
            <i class="fa fa-users fa-3x wow bounceIn" data-wow-delay=".1s"></i>
            <h4><a href="#"><asp:Literal ID="ltl1" runat="server" Text="<%$ Resources:ResourceRailWay, linkAdmin1 %>"></asp:Literal></a></h4>
          </div>
        </div>
        <div class="col-md-4 text-center">
          <div class="service-box">
<%--            <i class="fa fa-compress fa-3x wow bounceIn" data-wow-delay=".2s"></i>--%>
            <i class="fa fa-home fa-3x wow bounceIn" data-wow-delay=".2s"></i>
            <h4><a href="#"><asp:Literal ID="ltl2" runat="server" Text="<%$ Resources:ResourceRailWay, linkAdmin2 %>"></asp:Literal></a></h4>
          </div>
        </div>
        <div class="col-md-4 text-center">
          <div class="service-box">
            <i class="fa fa-expand fa-3x wow bounceIn" data-wow-delay=".3s"></i>
            <h4><a href="#"><asp:Literal ID="ltl3" runat="server" Text="<%$ Resources:ResourceRailWay, linkAdmin3 %>"></asp:Literal></a></h4>
          </div>
        </div>
      </div>
      <div class="row m-3t">
        <div class="col-md-4 text-center">
          <div class="service-box">
            <i class="fa fa-copy fa-3x wow bounceIn" data-wow-delay=".4s"></i>
            <h4><a href="CopyKIS.aspx"><asp:Literal ID="ltl4" runat="server" Text="<%$ Resources:ResourceRailWay, linkAdmin4 %>"></asp:Literal></a></h4>
          </div>
        </div>
        <div class="col-md-4 text-center">
          <div class="service-box">
            <i class="fa fa-cog fa-3x wow bounceIn" data-wow-delay=".5s"></i>
            <h4><a href="CopyKISInput.aspx"><asp:Literal ID="ltl5" runat="server" Text="<%$ Resources:ResourceRailWay, linkAdmin5 %>"></asp:Literal></a></h4>
          </div>
        </div>
        <div class="col-md-4 text-center">
          <div class="service-box">
            <i class="fa fa-cog fa-3x wow bounceIn" data-wow-delay=".6s"></i>
            <h4><a href="CopyKISOutput.aspx"><asp:Literal ID="ltl6" runat="server" Text="<%$ Resources:ResourceRailWay, linkAdmin6 %>"></asp:Literal></a></h4>
          </div>
        </div>
      </div>
<%--      <div class="row m-3t">
        <div class="col-md-4 text-center">
          <div class="service-box">
            <i class="fa fa-keyboard-o fa-3x wow bounceIn" data-wow-delay=".7s"></i>
            <h4>Delete Key Support</h4>
          </div>
        </div>
        <div class="col-md-4 text-center">
          <div class="service-box">
            <i class="fa fa-arrows fa-3x wow bounceIn" data-wow-delay=".8s"></i>
            <h4>Multiple Direction Popovers</h4>
          </div>
        </div>
        <div class="col-md-4 text-center">
          <div class="service-box">
            <i class="fa fa-code fa-3x wow bounceIn" data-wow-delay=".9s"></i>
            <h4>MIT Licensed, Open Source</h4>
          </div>
        </div>
      </div>--%>
    </div>
  </section>

  <section id="instructions">
    <div class="container">
      <div class="row m-3t">
        <div class="col-lg-12 text-center">
          <h2 class="section-heading"><asp:Literal ID="ltins" runat="server" Text="<%$ Resources:ResourceRailWay, titleInstructions %>"></asp:Literal></h2>
          <hr class="primary"/>
        </div>
      </div>
    </div>
    <div class="container">
      <p class="example-heading"><asp:Literal ID="lti1" runat="server" Text="<%$ Resources:ResourceRailWay, titleInstructions1 %>"></asp:Literal></p>
      <div class="row">
        <div class="text-center">
            <p><asp:Literal ID="ltpi11" runat="server" Text="<%$ Resources:ResourceRailWay, pinstructions1_1 %>"></asp:Literal>&nbsp;<a href="../../AccessWeb.aspx"><asp:Literal ID="ltpi12" runat="server" Text="<%$ Resources:ResourceRailWay, pinstructions1_2 %>"></asp:Literal></a>.</p>
            <p><asp:Literal ID="ltpi13" runat="server" Text="<%$ Resources:ResourceRailWay, pinstructions1_3 %>"></asp:Literal></p>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/WebSite/RailWay/image/help/Access1.png" />
            <p><asp:Literal ID="ltpi14" runat="server" Text="<%$ Resources:ResourceRailWay, pinstructions1_4 %>"></asp:Literal></p>
            <asp:Image ID="Image2" runat="server" ImageUrl="~/WebSite/RailWay/image/help/Access2.png" />
            <p><asp:Literal ID="ltpi15" runat="server" Text="<%$ Resources:ResourceRailWay, pinstructions1_5 %>"></asp:Literal></p>
        </div>
      </div>
      <hr />
      <div class="row m-3t">
        <p class="example-heading"><asp:Literal ID="lti2" runat="server" Text="<%$ Resources:ResourceRailWay, titleInstructions2 %>"></asp:Literal></p>
        <div class="col-md-6 text-center">
          <div class="service-box">

          </div>
        </div>
        <div class="col-md-6 text-center">
          <div class="service-box">
            <div class="select-container">

            </div>
          </div>
        </div>
      </div>
      <hr>
      <div class="row m-3t">
        <p class="example-heading"><asp:Literal ID="lti3" runat="server" Text="<%$ Resources:ResourceRailWay, titleInstructions3 %>"></asp:Literal></p>
        <div class="col-md-6 text-center">
          <div class="code-box">

          </div>
        </div>
        <div class="col-md-6 text-center">
          <div class="service-box">
            <div class="select-container">

            </div>
          </div>
        </div>
      </div>
    </div>
  </section>

  <section id="report" class="bg-primary">
    <div class="container">
      <div class="row">
        <div class="col-lg-12 text-center">
          <h2 class="section-heading">Группа отчетов #1</h2>
          <hr class="primary"/>
        </div>
      </div>
      <div class="row no-gutter">
        <table class="table table-responsive table-bordered">
          <thead>
            <th>Отчет</th>            
            <th>Описание</th>
          </thead>
          <tbody>
            <tr>
              <td><a href="#">Отчет #1_1</a></td>
              <td>Описание отчета #1_1</td>
            </tr>
            <tr>
              <td><a href="#">Отчет #1_2</a></td>
              <td>Описание отчета #1_2</td>
            </tr>
            <tr>
              <td><a href="#">Отчет #1_3</a></td>
              <td>Описание отчета #1_3</td>
            </tr>
          </tbody>
        </table>
      </div>
      <div class="row">
        <div class="col-lg-12 text-center">
          <h2 class="section-heading m-3t">Группа отчетов #2</h2>
          <hr class="primary">
        </div>
        <table class="table table-responsive table-bordered">
          <thead>
            <th>Отчет</th>            
            <th>Описание</th>
          </thead>
          <tbody>
            <tr>
              <td><a href="#">Отчет #2_1</a></td>
              <td>Описание отчета #2_1</td>
            </tr>
            <tr>
              <td><a href="#">Отчет #2_2</a></td>
              <td>Описание отчета #2_2</td>
            </tr>
            <tr>
              <td><a href="#">Отчет #2_3</a></td>
              <td>Описание отчета #2_3</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </section>

<%--  <aside class="bg-dark">
    <div class="container text-center">
      <div class="call-to-action">
        <h2>jQuery PopSelect is Open Source</h2>
        <a href="https://github.com/kanakiyajay/popSelect/archive/master.zip" class="btn btn-default btn-xl wow tada">Download Now!</a>
        <p></p>
        <p>You can install it from bower or npm</p>
        <div class="row">
          <div class="col-lg-4">
          </div>
          <div class="col-lg-4 div col-xs-12">
            <pre><code>bower install popSelect</code></pre>
            <pre><code>npm install popselect</code></pre>
          </div>
        </div>
      </div>
    </div>
  </aside>--%>

  <section id="contact">
    <div class="container">
      <div class="row">
        <div class="col-lg-8 col-lg-offset-2 text-center">
          <h2 class="section-heading"><asp:Literal ID="lttc" runat="server" Text="<%$ Resources:ResourceRailWay, titleContact %>"></asp:Literal></h2>
          <hr class="primary" />
          <p><asp:Literal ID="lttitc1" runat="server" Text="<%$ Resources:ResourceRailWay, titleContact1 %>"></asp:Literal></p>
        </div>
      </div>
      <div class="row m-3t">
        <div class="col-lg-4 text-center">
          <div class="row">
<%--            <div class="col-xs-12">
              <a href="https://twitter.com/techiejayk" title="FED Pune" target="_blank">
                <span class="fa-stack fa-4x">
                  <i class="fa fa-circle fa-stack-2x"></i>
                  <i class="fa fa-twitter fa-stack-1x text-white wow bounceIn"></i>
                </span>
              </a>
            </div>--%>
            <div class="col-xs-12 m-3t">
              <a href="mailto:Evgeniy.Sidorenko@arcelormittal.com" title="Сидоренко Евгений">
                <span class="fa-stack fa-4x">
                  <i class="fa fa-circle fa-stack-2x"></i>
                  <i class="fa fa-envelope-o fa-stack-1x text-white wow bounceIn"></i>
                </span>
              </a>
            </div>
          </div>
        </div>

        <div class="col-lg-4 text-center" itemscope itemtype="http://schema.org/Person">
          <div class="row">
            <div class="col-xs-12">
              <img width="221" height="221" class="img-circle" src="image/default/Sidorenko.JPG" alt="Сидоренко Евгений">
            </div>
            <div class="col-xs-12 text-center m-t-xs">
              <h4 itemprop="name" class="text-uppercase text-muted">Сидоренко Евгений</h4>
              <div style="display:none">
                <p itemprop="jobTitle">Должность?</p>
              </div>
            </div>
          </div>
        </div>

        <div class="col-lg-4 text-center">
          <div class="row">
<%--            <div class="col-xs-12">
               <a href="http://jaykanakiya.com/" rel="author" title="Jay Kanakiya">
                <span class="fa-stack fa-4x">
                  <i class="fa fa-circle fa-stack-2x"></i>
                  <i class="fa fa-globe fa-stack-1x text-white bounceIn wow"></i>
                </span>
              </a>
            </div>
            <div class="col-xs-12 m-3t">
              <a href="https://github.com/kanakiyajay/" title="kanakiyajay">
                <span class="fa-stack fa-4x">
                  <i class="fa fa-circle fa-stack-2x"></i>
                  <i class="fa fa-github fa-stack-1x text-white bounceIn wow"></i>
                </span>
              </a>
            </div>--%>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-lg-8 col-lg-offset-2 text-center">
          <hr class="primary" />
          <p><asp:Literal ID="lttitc2" runat="server" Text="<%$ Resources:ResourceRailWay, titleContact2 %>"></asp:Literal></p>
        </div>
      </div>
      <div class="row m-3t">
        <div class="col-lg-4 text-center">
          <div class="row">
<%--            <div class="col-xs-12">
              <a href="https://twitter.com/techiejayk" title="FED Pune" target="_blank">
                <span class="fa-stack fa-4x">
                  <i class="fa fa-circle fa-stack-2x"></i>
                  <i class="fa fa-twitter fa-stack-1x text-white wow bounceIn"></i>
                </span>
              </a>
            </div>--%>
            <div class="col-xs-12 m-3t">
              <a href="mailto:Eduard.Levchenko@arcelormittal.com" title="Левченко Эдуард">
                <span class="fa-stack fa-4x">
                  <i class="fa fa-circle fa-stack-2x"></i>
                  <i class="fa fa-envelope-o fa-stack-1x text-white wow bounceIn"></i>
                </span>
              </a>
            </div>
          </div>
        </div>

        <div class="col-lg-4 text-center" itemscope itemtype="http://schema.org/Person">
          <div class="row">
            <div class="col-xs-12">
              <img width="221" height="221" class="img-circle" src="image/default/Levchenko.jpg" alt="Левченко Эдуард">
            </div>
            <div class="col-xs-12 text-center m-t-xs">
              <h4 itemprop="name" class="text-uppercase text-muted">Левченко Эдуард</h4>
              <div>
                <p itemprop="jobTitle">Начальник отдела промышленных систем автоматизации ДАТП</p>
              </div>
            </div>
          </div>
        </div>

        <div class="col-lg-4 text-center">
          <div class="row">
<%--            <div class="col-xs-12">
               <a href="http://jaykanakiya.com/" rel="author" title="Jay Kanakiya">
                <span class="fa-stack fa-4x">
                  <i class="fa fa-circle fa-stack-2x"></i>
                  <i class="fa fa-globe fa-stack-1x text-white bounceIn wow"></i>
                </span>
              </a>
            </div>
            <div class="col-xs-12 m-3t">
              <a href="https://github.com/kanakiyajay/" title="kanakiyajay">
                <span class="fa-stack fa-4x">
                  <i class="fa fa-circle fa-stack-2x"></i>
                  <i class="fa fa-github fa-stack-1x text-white bounceIn wow"></i>
                </span>
              </a>
            </div>--%>
          </div>
        </div>
      </div>
    </div>
  </section>

<%--  <aside id="links" class="bg-dark">
    <div class="container">
      <div class="row">
        <div class="col-lg-8 col-lg-offset-2 text-center">
          <h2 class="section-heading">You might also be interested in:</h2>
          <hr class="primary">
        </div>
        <div class="col-lg-3">
          <div class="service-box">
            <h4 class="text-center">
              <a href="http://jquer.in/" target="_blank" title="jQuery plugins">
                <img width="168" height="162" class="img-circle" src="img/jquer_in.jpg" alt="A jQuery plugin a Day">
              </a>
            </h4>
          </div>
        </div>
        <div class="col-lg-3">
          <div class="service-box">
            <h4 class="text-center">
              <a href="http://angular-js.in/" target="_blank" title="Angular Modules">
                <img width="162" height="161" src="img/angular-js.jpg" alt="Angular Directives" class="img-circle">
              </a></h4>
          </div>
        </div>
        <div class="col-lg-3">
          <div class="service-box">
            <h4 class="text-center">
              <a href="http://web-components.in/" target="_blank" title="Web Components">
                <img width="162" height="166" src="img/web-component.png" alt="Web Components" class="img-circle">
              </a>
            </h4>
          </div>
        </div>
        <div class="col-lg-3">
          <div class="service-box">
            <h4 class="text-center">
              <a href="http://grunt-tasks.com/" target="_blank" title="Grunt Tasks">
                <img width="162" height="162" src="img/grunt-tasks.png" alt="Grunt Tasks" class="img-circle">
              </a>
            </h4>
          </div>
        </div>
      </div>
    </div>
  </aside>--%>

<%--  <!-- Microdata for Software Application -->
  <div style="display:none" itemscope itemtype="http://schema.org/SoftwareApplication">
    <span itemprop="name">popSelect</span><br>
    <h1 itemprop="headline">A jQuery plugin to replace multiselect with sleek popovers</h1>
    <p itemprop="description">popSelect is a jQuery plugin to replace the traditional select box with a sleek Popover with options pre-populated for a better User interface.</p>
    <img src="http://jquer.in/wp-content/uploads/2015/07/popSelect.jpg" alt="popSelect jQuery plugin" itemprop="image">
    REQUIRES <span itemprop="operatingSystem">Windows</span><br>
    <div itemprop="aggregateRating" itemscope itemtype="http://schema.org/AggregateRating">
    RATING <span itemprop="ratingValue">5</span>
    (<span itemprop="ratingCount">23 ratings</span>)

    <div itemprop="offers" itemscope itemtype="http://schema.org/Offer">
        Price: $<span itemprop="price">0</span>
        <meta itemprop="priceCurrency" content="INR">
    </div>
</div>--%>

  <!-- jQuery -->
  <script src="scripts/default/jquery.js" type="text/javascript"></script>

  <!-- Bootstrap Core JavaScript -->
  <script src="scripts/default/bootstrap.min.js" type="text/javascript"></script>

  <!-- Plugin JavaScript -->
  <script src="scripts/default/jquery.easing.min.js" type="text/javascript"></script>
  <script src="scripts/default/jquery.fittext.js" type="text/javascript"></script>
  <script src="scripts/default/wow.min.js" type="text/javascript"></script>
  <script src="scripts/default/prism.js" type="text/javascript"></script>

  <!-- Custom Theme JavaScript -->
  <script src="scripts/default/creative.js" type="text/javascript"></script>

  <!-- popSelect jQuery plugin -->
  <script src="scripts/default/jquery.popSelect.min.js" type="text/javascript"></script>

<%--  <!-- Initilization Scripts -->
  <script>
      $(document).ready(function () {

          // The Main Example on the Top
          $('#main-example').popSelect({
              placeholderText: 'Which countries have you travelled to ?',
              showTitle: false
              //autofocus: true
          });

          // Example 1: With Max Allowed
          $('#example1').popSelect({
              showTitle: false,
              maxAllowed: 2
          });

          // With Bottom Popover
          $('#example2').popSelect({
              showTitle: false,
              placeholderText: 'Click to Add More',
              position: 'bottom'
          });

          // With Pre Selected Values
          $('#example3').popSelect({
              showTitle: false,
              autoIncrease: true
          });

      });
  </script>

  <!-- Twitter Snippet -->
  <script>
      !function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + '://platform.twitter.com/widgets.js'; fjs.parentNode.insertBefore(js, fjs); } }(document, 'script', 'twitter-wjs');
  </script>

  <!-- Google Analytics Snippet -->
  <script>
      (function (i, s, o, g, r, a, m) {
          i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
              (i[r].q = i[r].q || []).push(arguments)
          }, i[r].l = 1 * new Date(); a = s.createElement(o),
          m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
      })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

      ga('create', 'UA-33060097-1', 'auto');
      ga('send', 'pageview');
  </script>--%>
</body>
</html>
