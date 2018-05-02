using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBase;

public partial class WebSite_Setup_ListSite : BaseAccessPages
{
    private classSite cs = new classSite();

    /// <summary>
    /// Загрузка страницы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Первый запуск
        if (!IsPostBack)
        {
            ((WebSite_Setup_Setup)Master).SetHeader(cs.GetStringResource("titleMenuAdmin"), cs.GetStringResource("menuListSite"));
        }
        //pnSelect.Change = this.Change;
        tcselect.ModeChange = this.Change;
         //Если есть выбранный сайт привязать к компоненту
        if (lvSite.SelectedDataKey != null) 
        { 
            controlSite.IDSite = (int)lvSite.SelectedDataKey.Value;
            controlSite.Change= this.Change;
        }
        // Требуется обновить компоненты
        if (base.bReloading)
        {
            //controlSite.ReloadingControl();
            lvSite.DataBind();
            base.bReloading = false;
        }
    }

    /// <summary>
    /// Выбран сайт
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        controlSite.IDSite = (int)lvSite.SelectedDataKey.Value;
        controlSite.Change= this.Change;
        controlSite.DataBind();

    }
    /// <summary>
    /// Добавить новый сайт
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelect_InsertClick(object sender, EventArgs e)
    {
        controlSite.ModeTable = FormViewMode.Insert;
        controlSite.Change = this.Change;
        controlSite.DataBind();
    }
    /// <summary>
    /// Сайт добавлен
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlSite_SiteInserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        lvSite.DataBind();
    }
    /// <summary>
    /// Сайт обновлен
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlSite_SiteUpdated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        lvSite.DataBind();
    }
    /// <summary>
    /// Сайт удален
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlSite_SiteDeleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        lvSite.DataBind();
    }

}