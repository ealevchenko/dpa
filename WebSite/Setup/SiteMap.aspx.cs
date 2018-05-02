using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBase;

public partial class WebSite_Setup_SiteMap : BaseAccessPages
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ 

    private classSiteMap cs = new classSiteMap();
    /// <summary>
    /// ID Выбранной строки 
    /// </summary>
    private int IDSM
    {
        get { return (int)ViewState["IDSM"]; }
        set { ViewState["IDSM"] = value; }
    }
    /// <summary>
    ///  Иницилизация объектов
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (ViewState["IDSM"] == null)
        { ViewState["IDSM"] = 0; }
    }
    #endregion

    #region МЕТОДЫ 

    #region Методы загрузки компонентов и страницы выборка данных
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
            ((WebSite_Setup_Setup)Master).SetHeader(cs.GetStringResource("titleMenuAdmin"), cs.GetStringResource("menuSiteMap"));
            cs.GetSiteMapToTreeView(tvSiteMap);
        }
        pnSelect.Change = this.Change;
        //Если есть выбранный сайт привязать к компоненту
        if (tvSiteMap.SelectedValue != "")
        {
            controlSiteMap.IDSiteMap = int.Parse(tvSiteMap.SelectedValue);
            controlSiteMap.Change = this.Change;
        }
        // Требуется обновить компоненты
        if (base.bReloading)
        {
            controlSiteMap.ReloadingControl();
            cs.GetSiteMapToTreeView(tvSiteMap);
            base.bReloading = false;
        }
    }
    /// <summary>
    /// Установить выбранный узел
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvSiteMap_DataBound(object sender, EventArgs e)
    {
        base.SetTreeViewValue(tvSiteMap.Nodes, this.IDSM.ToString());
    }
    #endregion

    #region Методы обработки действий пользователя
    /// <summary>
    /// Добавить строку карты сайта
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelect_InsertClick(object sender, EventArgs e)
    {
        controlSiteMap.ModeTable = FormViewMode.Insert;
        controlSiteMap.Change = this.Change;
        controlSiteMap.DataBind();
    }
    /// <summary>
    /// Показать выбранную строку 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvSiteMap_SelectedNodeChanged(object sender, EventArgs e)
    {
        this.IDSM = int.Parse(tvSiteMap.SelectedValue);
        controlSiteMap.IDSiteMap = this.IDSM;
        controlSiteMap.Change = this.Change;
        controlSiteMap.DataBind();
    }    
    /// <summary>
    /// Опустить строку карты сайта на позицию ниже
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelect_DownClick(object sender, EventArgs e)
    {
        if (this.IDSM != 0)
        {
            cs.DownSiteMap(this.IDSM, true);
            cs.GetSiteMapToTreeView(tvSiteMap);
        }
    }  
    /// <summary>
    /// Поднять строку
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelect_UpClick(object sender, EventArgs e)
    {
        if (this.IDSM != 0)
        {
            cs.UpSiteMap(this.IDSM, true);
            cs.GetSiteMapToTreeView(tvSiteMap);
        }
    }
    #endregion

    #region Методы работы с источником данных
    /// <summary>
    /// Строка добавлена
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlSiteMap_DataInserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        cs.GetSiteMapToTreeView(tvSiteMap);
    }
    /// <summary>
    /// Строка исправлена
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlSiteMap_DataUpdated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        cs.GetSiteMapToTreeView(tvSiteMap);
    }
    /// <summary>
    /// Строка удалена
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlSiteMap_DataDeleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        cs.GetSiteMapToTreeView(tvSiteMap);
        this.IDSM = 0;
    }
    #endregion

    #endregion



}