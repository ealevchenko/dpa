using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controlKPI : BaseControlUpdateFormView
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ controlTypeProject
    // ID выборки
    public int? IDKPI
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

    public string Caption
    {
        get;
        set;
    }
    // Получить текущий режим таблицы
    public FormViewMode CurrentMode { get { return fvTable.CurrentMode; } }

    protected int? idkpi { get { return this.IDKPI != null ? this.IDKPI : null; } }

    protected bool outinfo { get { return this.OutInfoText != null ? this.OutInfoText : false; } }

    protected string caption { get { return this.Caption != null ? this.Caption : null; } }

    protected string save_name { get { return base.fmc.GetTextTextBox(fvTable, "tbName"); } }
    protected string save_nameeng { get { return base.fmc.GetTextTextBox(fvTable, "tbNameEng"); } }

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
    protected void fvTable_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        OutFormView(fvTable, e.NewMode);
    }
    /// <summary>
    /// Включить или отключить режим контроля на стороне клиентов (необходим когда внедряется окно добавить новый список выбора)
    /// </summary>
    /// <param name="value"></param>
    public void ValidateClient(bool value)
    {
        base.fmc.SetEnableRegularExpressionValidator(fvTable, "revTypeProject", value);
        base.fmc.SetEnableRegularExpressionValidator(fvTable, "revTypeProjectEng", value);
        base.fmc.SetEnableRegularExpressionValidator(fvTable, "revDescription", value);
        base.fmc.SetEnableRegularExpressionValidator(fvTable, "revDescriptionEng", value);
        base.fmc.SetEnableRequiredFieldValidator(fvTable, "rfvTypeProject", value);
        base.fmc.SetEnableRequiredFieldValidator(fvTable, "rfvTypeProjectEng", value);
        base.fmc.SetEnableRequiredFieldValidator(fvTable, "rfvDescription", value);
        base.fmc.SetEnableRequiredFieldValidator(fvTable, "rfvDescriptionEng", value);
    }
    #endregion

    #region Методы привязки данных
    /// <summary>
    /// Переопределим
    /// </summary>
    public override void DataBind()
    {
        base.OnDataBinding(EventArgs.Empty);
        OutFormView(fvTable, base.ModeTable);
    }
    protected void fvTable_DataBound(object sender, EventArgs e)
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
            OutFormView(fvTable, this.ModeTable); // если стоит сдесь тогда работает все события кнопок
        }
    }
    #endregion

    #region Методы работы с источником данных
    /// <summary>
    /// Выбрать
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["IDKPI"] = this.idkpi;
        if (DataSelecting != null) DataSelecting(this, e);
    }
    /// <summary>
    /// Выбрано
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataSelected != null) DataSelected(this, e);
    }
    /// <summary>
    /// Добавить
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["Name"] = this.save_name;
        e.InputParameters["NameEng"] = this.save_nameeng;
        e.InputParameters["OutResult"] = this.outinfo;
        if (DataInserting != null) DataInserting(this, e);
    }
    /// <summary>
    /// Добавлено
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataInserted != null) DataInserted(this, e);
    }
    /// <summary>
    /// Править
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["IDKPI"] = this.idkpi;
        e.InputParameters["Name"] = this.save_name;
        e.InputParameters["NameEng"] = this.save_nameeng;
        e.InputParameters["OutResult"] = this.outinfo;
        if (DataUpdating != null) DataUpdating(this, e);
    }
    /// <summary>
    /// Исправленно
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataUpdated != null) DataUpdated(this, e);
    }
    /// <summary>
    /// Удалить
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["IDKPI"] = this.idkpi; ;
        e.InputParameters["OutResult"] = this.outinfo;
        if (DataDeleting != null) DataDeleting(this, e);
    }
    /// <summary>
    /// Удалено
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
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