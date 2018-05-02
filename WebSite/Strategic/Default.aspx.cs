using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBase;

public partial class WebSite_Strategic_Default : BasePages
{
    protected classSiteMap csitemap = new classSiteMap();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) { this.DataBind(); }
    }
}