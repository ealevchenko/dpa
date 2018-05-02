using Strategic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controlStepProject : BaseControlUpdateFormView
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ controlStepProject
    // ID шаг детально
    public int? IDStep
    {
        get;
        set;
    }

    // ID выборки
    public int? Insert_IDBigStep
    {
        get;
        set;
    }
    // шаблон
    public int? Insert_IDTemplateStepProject
    {
        get;
        set;
    }
     ///<summary>
     ///Выводить информацию 
     ///</summary>
    public bool OutInfoText
    {
        get;
        set;
    }

    // Получить текущий режим таблицы
    public FormViewMode CurrentMode { get { return fvStepProject.CurrentMode; } }
    protected int? idstep { get { return this.IDStep != null ? this.IDStep : null; } }

    protected int insert_idtemplate { get { return this.Insert_IDTemplateStepProject == null ? -1 : (int)this.Insert_IDTemplateStepProject; } }
    protected int insert_idbigstep { get { return this.Insert_IDBigStep == null ? -1 : (int)this.Insert_IDBigStep; } }

    //protected int? idbigstep { get { return this.IDBigStep != null ? this.IDBigStep : null; } }

    protected bool outinfo { get { return this.OutInfoText != null ? this.OutInfoText : false; } }

    protected int save_idtemplate
    {
        get
        {
            DropDownList ddlTemplate = base.fmc.GetDropDownList(fvStepProject, "ddlTemplate");
            if (ddlTemplate != null)
            {
                if (ddlTemplate.SelectedIndex >= 0) { return int.Parse(ddlTemplate.SelectedValue); }

            }
            return -1;
        }
    }
    protected int save_idbigstep
    {
        get
        {
            DropDownList ddlBigStep = base.fmc.GetDropDownList(fvStepProject, "ddlBigStep");
            if (ddlBigStep != null)
            {
                if (ddlBigStep.SelectedIndex >= 0) { return int.Parse(ddlBigStep.SelectedValue); }

            }
            return -1;
        }
    }
    protected string save_step { get { return base.fmc.GetTextTextBox(fvStepProject, "tbStep"); } }
    protected string save_stepeng { get { return base.fmc.GetTextTextBox(fvStepProject, "tbStepEng"); } }

    // События
    public event Data_Selecting DataSelecting;
    public event Data_Selected DataSelected;
    public event Data_Inserting DataInserting;
    public event Data_Inserted DataInserted;
    public event Data_Updating DataUpdating;
    public event Data_Updated DataUpdated;
    public event Data_Deleting DataDeleting;
    public event Data_Deleted DataDeleted;
    public event Data_CancelClick DataCancelClick;
    public event Source_Refresh SourceRefresh;

    #endregion

    #region Методы управления режимами
    /// <summary>
    /// Событие перед обновлением режима
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void fvStepProject_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        OutFormView(fvStepProject, e.NewMode);
    }
    /// <summary>
    /// Включить или отключить режим контроля на стороне клиентов (необходим когда внедряется окно добавить новый список выбора)
    /// </summary>
    /// <param name="value"></param>
    public void ValidateClient(bool value)
    {
        base.fmc.SetEnableRegularExpressionValidator(fvStepProject, "revStep", value);
        base.fmc.SetEnableRegularExpressionValidator(fvStepProject, "revStepEng", value);
        base.fmc.SetEnableRequiredFieldValidator(fvStepProject, "rfvStep", value);
        base.fmc.SetEnableRequiredFieldValidator(fvStepProject, "rfvStepEng", value);
    }
    #endregion

    #region Методы привязки данных
    /// <summary>
    /// Переопределим
    /// </summary>
    public override void DataBind()
    {
        base.OnDataBinding(EventArgs.Empty);
        OutFormView(fvStepProject, base.ModeTable);
    }
    /// <summary>
    /// Проивязка поля ddlTemplate
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlTemplate_DataBound(object sender, EventArgs e)
    {
        if (fvStepProject.CurrentMode == FormViewMode.Edit)
        {
            StepEntity se = base.ctemplatessteps.GetCultureStepsProject((int)this.idstep);
            if (se == null) return;
            DropDownList ddlTemplate = base.fmc.GetDropDownList(fvStepProject, "ddlTemplate");
            if (ddlTemplate != null)
            {
                if (se.Id != null)
                {
                    ddlTemplate.SelectedValue = se.IDTemplate.ToString();
                }
                else
                {
                    ddlTemplate.SelectedIndex = 0;
                }
            }
        }
        if (fvStepProject.CurrentMode == FormViewMode.Insert)
        {
            DropDownList ddlTemplate = base.fmc.GetDropDownList(fvStepProject, "ddlTemplate");
            if (ddlTemplate != null)
            {
                if (this.insert_idtemplate != -1)
                {
                    ddlTemplate.SelectedValue = this.insert_idtemplate.ToString();
                    ddlTemplate.Enabled = false;
                    base.fmc.SetEnableButton(fvStepProject, "btInsertTemplate", false);
                }
                else
                {
                    ddlTemplate.SelectedIndex = -1;
                    ddlTemplate.Enabled = true;
                    base.fmc.SetEnableButton(fvStepProject, "btInsertTemplate", true);
                }
            }
        }
    }
    /// <summary>
    /// Привязка поля ddlBigStep
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlBigStep_DataBound(object sender, EventArgs e)
    {
        if (fvStepProject.CurrentMode == FormViewMode.Edit)
        {
            StepEntity se = base.ctemplatessteps.GetCultureStepsProject((int)this.idstep);
            if (se == null) return;
            DropDownList ddlBigStep = base.fmc.GetDropDownList(fvStepProject, "ddlBigStep");
            if (ddlBigStep != null)
            {
                if (se.Id != null)
                {
                    ddlBigStep.SelectedValue = se.IdBigStep.ToString();
                }
                else
                {
                    ddlBigStep.SelectedIndex = 0;
                }
            }
        }
        if (fvStepProject.CurrentMode == FormViewMode.Insert)
        {
            DropDownList ddlBigStep = base.fmc.GetDropDownList(fvStepProject, "ddlBigStep");
            if (ddlBigStep != null)
            {
                if (this.insert_idbigstep != -1)
                {
                    ddlBigStep.SelectedValue = this.insert_idbigstep.ToString();
                    ddlBigStep.Enabled = false;
                    base.fmc.SetEnableButton(fvStepProject, "btBigStep", false);
                }
                else
                {
                    ddlBigStep.SelectedIndex = -1;
                    ddlBigStep.Enabled = true;
                    base.fmc.SetEnableButton(fvStepProject, "btBigStep", true);
                }
            }
        }
    }
    /// <summary>
    /// Привязка вех полей
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void fvStepProject_DataBound(object sender, EventArgs e)
    {

    }
    #endregion

    #region Методы загрузки компонентов и страницы выборка данных
    /// <summary>
    /// Загрузка страницы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            OutFormView(fvStepProject, this.ModeTable); // если стоит сдесь тогда работает все события кнопок
        }
    }
    /// <summary>
    /// Требуется обновить компоненты загружаемые из бд
    /// </summary>
    public void ReloadingControl()
    {
        DropDownList ddlTemplate = base.fmc.GetDropDownList(fvStepProject, "ddlTemplate");
        if (ddlTemplate != null) { ddlTemplate.DataBind(); }
        DropDownList ddlBigStep = base.fmc.GetDropDownList(fvStepProject, "ddlBigStep");
        if (ddlBigStep != null) { ddlBigStep.DataBind(); }
    }
    #endregion

    #region Методы работы с источником данных
    /// <summary>
    /// Выбрать
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTableStep_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["IDStep"] = this.idstep;
        if (DataSelecting != null) DataSelecting(this, e);
    }
    /// <summary>
    /// Выбрано
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTableStep_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataSelected != null) DataSelected(this, e);
    }
    /// <summary>
    /// Добавить
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTableStep_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["IDTemplateStepProject"] = this.save_idtemplate;
        e.InputParameters["IDBigStep"] = this.save_idbigstep;
        e.InputParameters["Step"] = this.save_step;
        e.InputParameters["StepEng"] = this.save_stepeng;        
        e.InputParameters["OutResult"] = this.outinfo;
        if (DataInserting != null) DataInserting(this, e);
    }
    /// <summary>
    /// Добавлено
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTableStep_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataInserted != null) DataInserted(this, e);
    }
    /// <summary>
    /// Править
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTableStep_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["IDStep"] = this.idstep;
        e.InputParameters["IDTemplateStepProject"] = this.save_idtemplate;
        e.InputParameters["IDBigStep"] = this.save_idbigstep;
        e.InputParameters["Step"] = this.save_step;
        e.InputParameters["StepEng"] = this.save_stepeng;        
        e.InputParameters["OutResult"] = this.outinfo;
        if (DataUpdating != null) DataUpdating(this, e);
    }
    /// <summary>
    /// Исправленно
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTableStep_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataUpdated != null) DataUpdated(this, e);
    }
    /// <summary>
    /// Удалить
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTableStep_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["IDStep"] = this.idstep;
        e.InputParameters["OutResult"] = this.outinfo;
        if (DataDeleting != null) DataDeleting(this, e);
    }
    /// <summary>
    /// Удалено
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTableStep_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataDeleted != null) DataDeleted(this, e);
    }
    /// <summary>
    /// Отмененно
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelect_CancelClick(object sender, EventArgs e)
    {
        if (DataCancelClick != null) DataCancelClick(this, e);
    }
    #endregion

    #region Методы работы panelUpr
    /// <summary>
    /// Настройка панели управления
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelect_Init(object sender, EventArgs e)
    {
        ((panelUpr)sender).Change = this.change_button;
        ((panelUpr)sender).StyleButton = base.style_button;
        ((panelUpr)sender).VisibleInsert = this.visible_insert;
        ((panelUpr)sender).VisibleUpdate = this.visible_update;
        ((panelUpr)sender).VisibleDelete = this.visible_delete;
        ((panelUpr)sender).VisibleSave = this.visible_save;
        ((panelUpr)sender).VisibleCancel = this.visible_cancel;
    }
    #endregion

    #region Обработка панели controlSite
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btBigStep_Click(object sender, EventArgs e)
    {
        if (fvStepProject.FindControl("BigStepProject")!=null)
        {
            controlBigStepProject cbsp = (controlBigStepProject)fvStepProject.FindControl("BigStepProject");
            if (cbsp!=null)
            {
                ValidateClient(false);
                cbsp.ModeTable = FormViewMode.Insert;
                cbsp.DataBind();
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BigStepProject_DataInserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ValidateClient(true);
        base.fmc.GetDropDownList(fvStepProject, "ddlBigStep").DataBind();
        if (SourceRefresh != null) SourceRefresh(this, e);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BigStepProject_DataCancelClick(object sender, EventArgs e)
    {
        ValidateClient(true);
        base.fmc.GetDropDownList(fvStepProject, "ddlBigStep").DataBind();
    }
    #endregion

}