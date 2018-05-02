using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBase;

public partial class WebSite_Setup_Section : BaseAccessPages
{
    
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ 

    protected classSection csection = new classSection();
    /// <summary>
    /// ID Выбранной строки 
    /// </summary>
    protected int IDSection
    {
        get { return (int)ViewState["IDSection"]; }
        set { ViewState["IDSection"] = value; }
    }
    /// <summary>
    ///  Иницилизация объектов
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (ViewState["IDSection"] == null)
        { ViewState["IDSection"] = 0; }
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
            ((WebSite_Setup_Setup)Master).SetHeader(csection.GetStringResource("titleMenuAdmin"), csection.GetStringResource("menuSection"));
            csection.GetSectionToTreeView(tvSection);
        }
        pnSelect.Change = this.Change;
        //Если есть выбранный сайт привязать к компоненту
        if (tvSection.SelectedValue != "")
        {
            if (this.IDSection != 0) { controlSection.Insert_ParentSection = this.IDSection; }
            controlSection.IDSection = int.Parse(tvSection.SelectedValue);
            controlSection.Change = this.Change;
        }
        // Требуется обновить компоненты
        if (base.bReloading)
        {
            //controlSection.ReloadingControl();
            csection.GetSectionToTreeView(tvSection);
            base.bReloading = false;
        }
    }
    /// <summary>
    /// Установить выбранный узел
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvSection_DataBound(object sender, EventArgs e)
    {
        base.SetTreeViewValue(tvSection.Nodes, this.IDSection.ToString());
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
        if (this.IDSection != 0) { controlSection.Insert_ParentSection = this.IDSection; }
        controlSection.ModeTable = FormViewMode.Insert;
        controlSection.Change = this.Change;
        controlSection.DataBind();
    }
    /// <summary>
    /// Показать выбранную строку 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvSection_SelectedNodeChanged(object sender, EventArgs e)
    {
        this.IDSection = int.Parse(tvSection.SelectedValue);
        controlSection.IDSection = this.IDSection;
        controlSection.Change = this.Change;
        controlSection.DataBind();
    }    
    /// <summary>
    /// Опустить строку карты сайта на позицию ниже
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelect_DownClick(object sender, EventArgs e)
    {
        if (this.IDSection != 0)
        {
            csection.DownSiteMap(this.IDSection, true);
            csection.GetSectionToTreeView(tvSection);
        }
    }  
    /// <summary>
    /// Поднять строку
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelect_UpClick(object sender, EventArgs e)
    {
        if (this.IDSection != 0)
        {
            csection.UpSiteMap(this.IDSection, true);
            csection.GetSectionToTreeView(tvSection);
        }
    }
    #endregion

    #region Методы работы с источником данных
    /// <summary>
    /// Строка добавлена
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlSection_DataInserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        csection.GetSectionToTreeView(tvSection);
    }
    /// <summary>
    /// Строка исправлена
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlSection_DataUpdated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        csection.GetSectionToTreeView(tvSection);
    }
    /// <summary>
    /// Строка удалена
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlSection_DataDeleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        csection.GetSectionToTreeView(tvSection);
        this.IDSection = 0;
    }
    #endregion

    #endregion

}