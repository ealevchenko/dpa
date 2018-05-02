using Strategic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_Strategic_Project : BaseStrategicPages
{
    
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ
    /// <summary>
    ///  Текущий менедженр проекта
    /// </summary>
    protected int? IDMenagerProject
    {
        get
        {
            if (ViewState["IDMenagerProject"] == null) return null;
            return (int)ViewState["IDMenagerProject"];
        }
        set { ViewState["IDMenagerProject"] = value; }
    }
    /// <summary>
    /// Признак наччальник службы
    /// </summary>
    protected bool Boss 
    { 
        get
        {
            if (ViewState["Boss"] == null) { ViewState["Boss"] = false; };
            return (bool)ViewState["Boss"];
        }
        set { ViewState["Boss"] = value; }    
    }
    /// <summary>
    /// Индефикатор проекта
    /// </summary>
    protected int? IDProject
    {
        get
        {
            if (ViewState["IDProject"] == null) return null;
            return (int)ViewState["IDProject"];
        }
        set { ViewState["IDProject"] = value; }
    }
    /// <summary>
    /// Текущий пользователь
    /// </summary>
    protected MenagerProjectEntity currentMenager  { get; set;}
    /// <summary>
    ///  Иницилизация объектов
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.currentMenager = base.cmenager.GetMenagerProjectToUser((int)base.cmenager.GetIDUser(HttpContext.Current.User.Identity.Name)); // Получим все данные менеджера запросившего эту страницу
        ViewState["IDMenagerProject"] = this.currentMenager.ID; // определим ID менеджера
        ViewState["Boss"] = this.currentMenager.SuperMenager; // определим менеджер главный или так:)
        if (HttpContext.Current.Request.QueryString["prj"] != null)
        {
            ViewState["IDProject"] = int.Parse(HttpContext.Current.Request.QueryString["prj"]);
        }

    }
    /// <summary>
    /// Получить выбранного менеджера проетов
    /// </summary>
    protected int select_menagerproject 
    {
        get {
            if (ddlMenagerProject.SelectedIndex != -1) return int.Parse(ddlMenagerProject.SelectedValue);
            return -1;
        }   
    }

    #endregion

    #region Методы привязки данных
    /// <summary>
    /// Привязка данных статус проекта
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlStatus_DataBound(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Clear();
        foreach (statusProject sp in Enum.GetValues(typeof(statusProject)))
        {
            ((DropDownList)sender).Items.Add(new ListItem(base.cproject.GetStringStrategicResource(sp.ToString()), ((int)sp).ToString()));
        }
        ((DropDownList)sender).Items.Insert(0, new ListItem(base.GetStringBaseResource("ddlAll"), "-1"));
    }
    /// <summary>
    /// Привязка данных тип проекта
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rblProject_DataBound(object sender, EventArgs e)
    {
        ((RadioButtonList)sender).SelectedIndex = 0;
    }
    /// <summary>
    /// Привязка данных менеджер проекта текущий
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlMenagerProject_DataBound(object sender, EventArgs e)
    {
        if (this.IDMenagerProject != null)
        {
            ((DropDownList)sender).SelectedValue = ((int)this.IDMenagerProject).ToString();
        }
        ((DropDownList)sender).Items.Insert(0, new ListItem(base.GetStringBaseResource("ddlAll"), "0"));
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
            ((StrategicMaster)Master).SetHeader(base.GetStringBaseResource("titleMenuProject"), base.GetStringStrategicResource("menuProjectAdd"));
            if (IDProject != null) 
            {
                controlProject.IDProject = (int)IDProject;
                controlProject.Change = this.Change;
                controlProject.DataBind();
                controlDetaliProject.IDProject = (int)IDProject;
                controlDetaliProject.Change = this.Change;
                controlDetaliProject.DataBind();
                mvStatus.ActiveViewIndex = 1;
            
            } else { 
                mvStatus.ActiveViewIndex = 0;
            }
            
        }
        pnInsertProject.Change = this.Change;
        //Если есть выбранный сайт привязать к компоненту
        if (lvList.SelectedDataKey != null)
        {
            controlProject.IDProject = (int)lvList.SelectedDataKey.Value;
            controlProject.Insert_IDMenagerProject = this.IDMenagerProject;
            controlProject.Change = this.Change;
            controlDetaliProject.IDProject = (int)lvList.SelectedDataKey.Value;
            controlDetaliProject.Change = this.Change;
        }
        // Требуется обновить компоненты
        if (base.bReloading)
        {
            //controlSite.ReloadingControl();
            lvList.DataBind();
            base.bReloading = false;
        }
    }

    protected void CloseInfoProject() 
    { 
        lvList.SelectedIndex = -1;
        controlProject.IDProject = null;
        controlProject.DataBind();
        controlDetaliProject.IDProject = null;
        controlDetaliProject.DataBind();    
    }
    #endregion

    #region Методы обработки действий пользователя
    /// <summary>
    /// Добавить новый проект
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnInsertProject_InsertClick(object sender, EventArgs e)
    {
        controlProject.IDProject = null;
        if (!this.Boss) { controlProject.Insert_IDMenagerProject = this.IDMenagerProject; }
        controlProject.Change = this.Change;
        controlProject.ModeTable = FormViewMode.Insert;
        controlProject.DataBind();
        mvStatus.ActiveViewIndex = 1;
    }
    /// <summary>
    /// Выбран проект
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        controlProject.IDProject = (int)lvList.SelectedDataKey.Value;
        controlProject.Change = this.Change;
        controlProject.DataBind();
        controlDetaliProject.IDProject = (int)lvList.SelectedDataKey.Value;
        controlDetaliProject.Change = this.Change;
        controlDetaliProject.DataBind();
        mvStatus.ActiveViewIndex = 1;
    }
    /// <summary>
    /// Выбран тип проектов (мои проекты или мое исполнение)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rblProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        CloseInfoProject();
    }
    /// <summary>
    /// Boss-ом выбран менеджер проекта
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlMenagerProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        CloseInfoProject();        
        lvList.DataBind();
    }
    /// <summary>
    /// Выбран новый статус проекта
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CloseInfoProject();
    }
    #endregion

    #region Методы обработки данных
    /// <summary>
    /// Выбор проекта в зависисмости от типа менеджера (главный\обычный)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsList_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.Boss)
        {
            e.InputParameters["TypeMenager"] = 0;
            e.InputParameters["IDMenagerProject"] = this.select_menagerproject;
        }
        else 
        { 
            e.InputParameters["IDMenagerProject"] = this.IDMenagerProject;        
        }
    }
    /// <summary>
    /// Обновили карточку
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlProject_DataUpdated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        lvList.DataBind();
    }
    /// <summary>
    /// Добавили карточку
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlProject_DataInserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        lvList.DataBind();
    }
    /// <summary>
    /// Отправили в архив карточку
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlProject_DataDeleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        lvList.DataBind();
    }
    /// <summary>
    /// Созданы шаги
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlProject_StepCreateClick(object sender, EventArgs e)
    {
        controlDetaliProject.DataBind();
    }
    /// <summary>
    /// Очищены шаги
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlProject_StepClearClick(object sender, EventArgs e)
    {
        controlDetaliProject.DataBind();
    }
    #endregion

    protected void pnBoss_Load(object sender, EventArgs e)
    {
        ((Panel)sender).Visible = this.Boss;
    }

    /// <summary>
    /// Вернуть список
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibStatus_Click(object sender, ImageClickEventArgs e)
    {
        mvStatus.ActiveViewIndex = 0;
    }
}