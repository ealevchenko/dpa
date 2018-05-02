using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controlBigStepProject : BaseControlUpdateFormView
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ controlBigStepProject
    // ID выборки
    public int? IDBigStep
    {
        get;
        set;
    }
    /// <summary>
    /// Выводить информацию 
    /// </summary>
    public bool OutInfoText
    {
        get;
        set;
    }

    // Получить текущий режим таблицы
    public FormViewMode CurrentMode { get { return fvStepProject.CurrentMode; } }

    protected int? idbigstep { get { return this.IDBigStep != null ? this.IDBigStep : null; } }

    protected bool outinfo { get { return this.OutInfoText != null ? this.OutInfoText : false; } }

    protected string save_bigstep { get { return base.fmc.GetTextTextBox(fvStepProject, "tbBigStep"); } }
    protected string save_bigstepeng { get { return base.fmc.GetTextTextBox(fvStepProject, "tbBigStepEng"); } }


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
        base.fmc.SetEnableRegularExpressionValidator(fvStepProject, "revBigStep", value);
        base.fmc.SetEnableRegularExpressionValidator(fvStepProject, "revBigStepEng", value);
        base.fmc.SetEnableRequiredFieldValidator(fvStepProject, "rfvBigStep", value);
        base.fmc.SetEnableRequiredFieldValidator(fvStepProject, "rfvBigStepEng", value);
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
    #endregion

    #region Методы работы с источником данных
    /// <summary>
    /// Выбрать
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTableStep_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["IDBigStep"] = this.idbigstep;
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

        e.InputParameters["BigStep"] = this.save_bigstep;
        e.InputParameters["BigStepEng"] = this.save_bigstepeng;        
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
        e.InputParameters["IDBigStep"] = this.idbigstep;        
        e.InputParameters["BigStep"] = this.save_bigstep;
        e.InputParameters["BigStepEng"] = this.save_bigstepeng;        
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
        e.InputParameters["IDBigStep"] = this.idbigstep;
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

}