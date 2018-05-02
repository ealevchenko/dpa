using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Design : BasePages
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Первый запуск
        if (!IsPostBack)
        {
            ((StartSite)Master).SetHeader(GetStringResource("Title1"), GetStringResource("TitleDesign"));
            ((StartSite)Master).returnmenu = true;
        }
        // Требуется обновить компоненты
        if (base.bReloading)
        {
            ((StartSite)Master).SetHeader(GetStringResource("Title1"), GetStringResource("TitleDesign"));
            ((StartSite)Master).returnmenu = true;            
            base.bReloading = false;
        }
    }
}