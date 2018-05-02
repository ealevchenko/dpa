using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBase;

public partial class WebSite_Setup_MenagerProject : BaseStrategicPages
{
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
            ((WebSite_Setup_Setup)Master).SetHeader(base.GetStringBaseResource("titleMenuProject"), base.GetStringBaseResource("menuMenagerProject"));
        }
        pnSelect.Change = this.Change;
         //Если есть выбранный сайт привязать к компоненту
        if (lvList.SelectedDataKey != null) 
        {
            controlMenagerProject.IDMenagerProject = (int)lvList.SelectedDataKey.Value;
            controlMenagerProject.Change = this.Change;
        }
        // Требуется обновить компоненты
        if (base.bReloading)
        {
            //controlSite.ReloadingControl();
            lvList.DataBind();
            base.bReloading = false;
        }
    }

    /// <summary>
    /// Выбран сайт
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        controlMenagerProject.IDMenagerProject = (int)lvList.SelectedDataKey.Value;
        controlMenagerProject.Change = this.Change;
        controlMenagerProject.DataBind();

    }
    /// <summary>
    /// Добавить новый сайт
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelect_InsertClick(object sender, EventArgs e)
    {
        controlMenagerProject.ModeTable = FormViewMode.Insert;
        controlMenagerProject.Change = this.Change;
        controlMenagerProject.DataBind();
    }
    /// <summary>
    /// Сайт добавлен
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlTypeProject_SiteInserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        lvList.DataBind();
    }
    /// <summary>
    /// Сайт обновлен
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlTypeProject_SiteUpdated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        lvList.DataBind();
    }
    /// <summary>
    /// Сайт удален
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlTypeProject_SiteDeleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        lvList.DataBind();
    }

}