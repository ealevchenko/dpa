using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_DefaultGroup : BasePages
{
    /// <summary>
    /// 
    /// </summary>
    public int Owner // ID Графика
    {
        get
        {
            if (ViewState["Owner"] == null)
            {
                if (HttpContext.Current.Request.QueryString["Owner"] != null)
                {
                    return int.Parse(HttpContext.Current.Request.QueryString["Owner"]);
                }
                else { return 0; }
            }
            else
            {
                return (int)ViewState["Owner"];
            }
        }
        set { ViewState["Owner"] = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) { this.DataBind(); }
    }
}