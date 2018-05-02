using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_RailWay_RailWay : System.Web.UI.MasterPage
{
    public void SetHeader(string header, string description)
    {
        ltHeader.Text = header;
        ltDescription.Text = description;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { this.DataBind(); }
    }
}
