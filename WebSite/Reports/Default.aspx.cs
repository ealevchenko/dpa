using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class reports_Default : BasePages
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) { this.DataBind(); }
        //if (Request.IsAuthenticated)
        //{
        //    // Display generic identity information.
        //    lblInfo.Text = "<b>Name: </b>" + User.Identity.Name;
        //    //lblInfo.Text += "<br><b>Authenticated With: </b>";

        //    if (User is WindowsPrincipal)
        //    {
        //        WindowsPrincipal principal = (WindowsPrincipal)User;
        //        //lblInfo.Text += "<br><b>Power user? </b>";
        //        //lblInfo.Text += principal.IsInRole(
        //        //  WindowsBuiltInRole.PowerUser).ToString();

        //        WindowsIdentity identity = principal.Identity as WindowsIdentity;
        //        lblInfo.Text += "<br><b>Token: </b>";
        //        lblInfo.Text += identity.Token.ToString();
        //        lblInfo.Text += "<br><b>Guest? </b>";
        //        lblInfo.Text += identity.IsGuest.ToString();
        //        lblInfo.Text += "<br><b>System? </b>";
        //        lblInfo.Text += identity.IsSystem.ToString();
        //    }

        //}
    }
}