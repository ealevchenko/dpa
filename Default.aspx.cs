using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Email;

public partial class _Default : BasePages
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Первый запуск
        if (!IsPostBack)
        {
            ((StartSite)Master).SetHeader(GetStringResource("Title1"), GetStringResource("Title2"));
        }
        // Требуется обновить компоненты
        if (base.bReloading)
        {
            ((StartSite)Master).SetHeader(GetStringResource("Title1"), GetStringResource("Title2"));
            base.bReloading = false;
        }
    }
}