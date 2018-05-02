using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AccessSite : LogPages
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ErrorUrl"] != null)
        {
            loginError.Visible = true;
            LoginOn.Visible = false;
            lbResult.Text = Request.QueryString["ErrorUrl"];
        }
        else
        {
            loginError.Visible = false;
            LoginOn.Visible = true;
        }
    }
}