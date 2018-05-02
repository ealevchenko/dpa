using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StartSite : BaseMaster
{
    public bool returnmenu { get; set; }

    public void SetHeader(string header, string description)
    {
        lbTitle1.Text = header;
        lbTitle2.Text = description;
    }

    /// <summary>
    /// загрузка мастер страницы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) { this.DataBind(); }
    }
}
