using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_Strategic_ProcatProgram : BaseStrategicPages
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
            ((StrategicMaster)Master).SetHeader(base.GetStringBaseResource("titleMenuProject"), base.GetStringStrategicResource("menuProcatProgram"));
            mvStatus.ActiveViewIndex = 0;
        }
        // Требуется обновить компоненты
        if (base.bReloading)
        {
            controlProgramProject.DataBind();
            base.bReloading = false;
        }
    }
    /// <summary>
    /// Вернуть статус
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlStatusProject_PanelClose(object sender, ImageClickEventArgs e)
    {
        mvStatus.ActiveViewIndex = 0;
        controlProgramProject.DataBind();
    }
    /// <summary>
    /// Выбран проект
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlProgramProject_DateSelect(object sender, controlProgramProject.selectProject e)
    {
        controlStatusProject.IDProject = e.IDProject;
        controlStatusProject.Change = this.Change;
        controlStatusProject.DataBind();
        mvStatus.ActiveViewIndex = 1;
    }
}