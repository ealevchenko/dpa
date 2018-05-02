using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_Setup_TemplateStepsProject : BaseStrategicPages
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ
    // ID детального шага
    private int? IDStep
    {
        get
        {
            if (ViewState["IDStep"] == null) { return null; }
            return (int)ViewState["IDStep"];
        }
        set
        {
            ViewState["IDStep"] = value;
        }
    }
    // запомним выбранный индекс детального шага
    private int SelectIndexStep 
    {
        get 
        {
            if (ViewState["SelectIndexStep"] == null)
            { ViewState["SelectIndexStep"] = -1; }
            return (int)ViewState["SelectIndexStep"];
        }
        set 
        {
            ViewState["SelectIndexStep"] = value;
        }
    }
    // Запомним ListView в котором выбран индекс детального шага
    private string IDlvStep
    {
        get
        {
            return (string)ViewState["IDlvStep"];
        }
        set
        {
            ViewState["IDlvStep"] = value;
        }
    }
    #endregion
    
    #region Методы привязки данных
    /// <summary>
    /// Создадим построчно основные шаги
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvBigStep_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.FindControl("lvStep") != null)
        {
            ListView lv = (ListView)e.Item.FindControl("lvStep");
            if (lv != null)
            {
                DataRowView drw = (DataRowView)e.Item.DataItem;
                if (drw != null)
                {
                    odsStep.SelectParameters["IDBigStep"].DefaultValue = drw.Row[0].ToString();
                    odsStep.SelectParameters["IDTemplateStepProject"].DefaultValue = ddlTemplate.SelectedValue;
                    lv.DataSource = odsStep.Select();
                }
            }
        }
    }
    /// <summary>
    /// Прорисовка прострочно основных шагов
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvBigStep_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.FindControl("lvStep") != null)
        {
            ListView lv = (ListView)e.Item.FindControl("lvStep");
            if (lv != null)
            {
                if (lv.ClientID == this.IDlvStep)
                {
                    lv.SelectedIndex = this.SelectIndexStep;
                    lv.DataBind();
                }
            }
        }
    }
    /// <summary>
    /// Событие перед выбором детального шага
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvStep_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        this.SelectIndexStep = e.NewSelectedIndex;
        this.IDlvStep = ((ListView)sender).ClientID;
        lvBigStep.SelectedIndex = -1;
        lvBigStep.DataBind();        
       
    }
    /// <summary>
    /// Событие перед выбором основного шага
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvBigStep_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        this.IDlvStep = null;
        this.SelectIndexStep = -1;
    }
    #endregion

    #region Методы загрузки компонентов и страницы
    /// <summary>
    /// Сбросить панель Step
    /// </summary>
    protected void ClearStep() 
    {
        this.IDStep = null;
        this.SelectIndexStep = -1;
        this.IDlvStep = null;
        StepProject.IDStep = null;
        StepProject.DataBind();
    }
    /// <summary>
    /// Загрузка
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Первый запуск
        if (!IsPostBack)
        {
            ((WebSite_Setup_Setup)Master).SetHeader(base.GetStringBaseResource("titleMenuProject"), base.GetStringBaseResource("menuTemplateStepsProject"));
        }
        pnSelectBigStep.Change = this.Change;
        //Если есть выбранный сайт привязать к компоненту
        if (lvBigStep.SelectedDataKey != null)
        {
            BigStepProject.IDBigStep = (int)lvBigStep.SelectedDataKey.Value;
            BigStepProject.Change = this.Change;
            StepProject.Insert_IDBigStep = (int)lvBigStep.SelectedDataKey.Value;
        }
        StepProject.IDStep = this.IDStep;
        if (ddlTemplate.SelectedIndex != -1)
        {
            StepProject.Insert_IDTemplateStepProject = int.Parse(ddlTemplate.SelectedValue);
        }
        StepProject.Change = this.Change;
        // Требуется обновить компоненты
        if (base.bReloading)
        {
            StepProject.ReloadingControl();
            ddlTemplate.DataBind();
            lvBigStep.DataBind();
            base.bReloading = false;
        }
    }
    #endregion

    #region методы обработки основного шага
    /// <summary>
    /// Выбран основной шаг
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvBigStep_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearStep();
        BigStepProject.IDBigStep = (int)lvBigStep.SelectedDataKey.Value;
        BigStepProject.Change = this.Change;
        BigStepProject.DataBind();
    }
    /// <summary>
    /// Добавить основной шаг
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelectBigStep_InsertClick(object sender, EventArgs e)
    {
        BigStepProject.ModeTable = FormViewMode.Insert;
        BigStepProject.Change = this.Change;
        BigStepProject.DataBind();
    }
    /// <summary>
    /// Основной шаг добавлен
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BigStepProject_DataInserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        lvBigStep.DataBind(); 
    }
    /// <summary>
    /// Основной шаг изменен
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BigStepProject_DataUpdated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        lvBigStep.DataBind(); 
    }
    /// <summary>
    /// Основной шаг Удален
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BigStepProject_DataDeleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        string f = sender.GetType().ToString();
        if (sender is controlStepProject) 
        {
            ClearStep();
        }
        if (sender is controlBigStepProject) 
        {
            lvBigStep.SelectedIndex = -1;
        }
        lvBigStep.DataBind();
 
    }
    /// <summary>
    /// Отмена над основным шагом
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BigStepProject_DataCancelClick(object sender, EventArgs e)
    {
        lvBigStep.DataBind(); 
    }

    #endregion

    #region методы обработки детального шага
    /// <summary>
    /// Добавить детальный шаг
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelectStep_InsertClick(object sender, EventArgs e)
    {
        StepProject.ModeTable = FormViewMode.Insert;
        if (ddlTemplate.SelectedIndex != -1) { StepProject.Insert_IDTemplateStepProject = int.Parse(ddlTemplate.SelectedValue); }
        if (lvBigStep.SelectedDataKey != null) { StepProject.Insert_IDBigStep = (int)lvBigStep.SelectedDataKey.Value; }
        StepProject.Change = this.Change;
        StepProject.DataBind();
    }
    /// <summary>
    /// Выбран детальный шаг
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvStep_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.IDStep = (int)((ListView)sender).SelectedDataKey.Value;
        StepProject.IDStep = this.IDStep;
        StepProject.Change = this.Change;
        StepProject.DataBind();
    }
    /// <summary>
    /// Обновить таблицу добавлен элемент
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void StepProject_SourceRefresh(object sender, EventArgs e)
    {
        lvBigStep.DataBind();
    }
    #endregion

    /// <summary>
    /// Выбран новый шаблон
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearStep();
        lvBigStep.DataBind();  
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelectStep_Init(object sender, EventArgs e)
    {
        ((panelUpr)sender).Change = this.Change;
    }
}