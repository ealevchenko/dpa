using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBase;

public partial class WebSite_Setup_Setup : BaseMaster
{
    protected classSiteMap csitemap = new classSiteMap();
    
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
