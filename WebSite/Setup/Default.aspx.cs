using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_Setup_Default : BasePages
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        { 
            this.DataBind();
            ((WebSite_Setup_Setup)Master).SetHeader("Разработка сайта", "Утилиты для разработки и отладки сайта" );
        }
    }
}