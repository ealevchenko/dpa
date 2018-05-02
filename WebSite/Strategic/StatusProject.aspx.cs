using Strategic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_Strategic_scripts_StatusProject : BaseStrategicPages
{
    #region Методы привязки данных
    /// <summary>
    /// Привязка данных тип проета
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlTypeProject_DataBound(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Insert(0,new ListItem(base.GetStringBaseResource("ddlAll"),"0"));
    }
    /// <summary>
    /// Привязка данных год внедрения
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlYear_DataBound(object sender, EventArgs e)
    {
        int Yare = DateTime.Now.Year;
        int index = Yare - 5;
        while (index <= Yare + 1)
        {
            ((DropDownList)sender).Items.Add(new ListItem(index.ToString(), index.ToString()));
            index++;
        }
        ((DropDownList)sender).Items.Insert(0, new ListItem(base.GetStringBaseResource("ddlAll"), "0"));
        ((DropDownList)sender).SelectedValue = DateTime.Now.Year.ToString();
    }
    /// <summary>
    /// Привязка данных менеджер проекта
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlMenager_DataBound(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Insert(0, new ListItem(base.GetStringBaseResource("ddlAll"), "0"));
    }
    /// <summary>
    /// Привязка данных текущий статус
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlStatus_DataBound(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Clear();
        foreach (statusProject sp in Enum.GetValues(typeof(statusProject)))
        {
            if (sp != statusProject.Deleted)
            {
                ((DropDownList)sender).Items.Add(new ListItem(base.cproject.GetStringStrategicResource(sp.ToString()), ((int)sp).ToString()));
            }
        }
        ((DropDownList)sender).Items.Insert(0, new ListItem(base.GetStringBaseResource("ddlAll"), "-1"));
    }
    #endregion

    #region Методы загрузки компонентов
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
            ((StrategicMaster)Master).SetHeader(base.GetStringBaseResource("titleMenuProject"), base.GetStringStrategicResource("menuProjectStatus"));
            mvStatus.ActiveViewIndex = 0;
        }
        // Требуется обновить компоненты
        if (base.bReloading)
        {
            if (mvStatus.ActiveViewIndex == 0) { this.DataBind(); }
            base.bReloading = false;
        }
    }
    #endregion

    #region Методы работы с источником данных
    /// <summary>
    /// Выборка данных
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsListProject_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["Year"] = 0;
    }
    #endregion

    #region Методы обработки действий пользователей
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvListProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        controlStatusProject.IDProject = (int)lvListProject.SelectedDataKey.Value;
        controlStatusProject.DataBind();            
        mvStatus.ActiveViewIndex = 1;

    }
    /// <summary>
    /// Вернуть статус
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlStatusProject_PanelClose(object sender, ImageClickEventArgs e)
    {
        mvStatus.ActiveViewIndex = 0;
        lvListProject.DataBind();
    }
    #endregion

}