using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controlSite : BaseControlUpdateFormView
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ controlUserGroup
    // ID выборки
    public int? IDSite
    {
        get;
        set;
    }

    private bool bURL // бит выбран
    {
        get {
            if (ViewState["bURL"] == null) { ViewState["bURL"] = false; }
            return (bool)ViewState["bURL"]; 
        }
        set { ViewState["bURL"] = value; }
    }
    private bool bURLHelp // бит выбран
    {
        get {
            if (ViewState["bURLHelp"] == null) { ViewState["bURLHelp"] = false; }            
            return (bool)ViewState["bURLHelp"]; }
        set { ViewState["bURLHelp"] = value; }
    }

    // Получить текущий режим таблицы
    public FormViewMode CurrentMode { get { return fvSite.CurrentMode; } }

    protected int? idsite { get { return this.IDSite != null ? this.IDSite : null; } }

    protected string save_description { get { return base.fmc.GetTextTextBox(fvSite, "tbDescription"); } }
    protected string save_descriptioneng { get { return base.fmc.GetTextTextBox(fvSite, "tbDescriptionEng"); } }
    protected string save_url { get { return base.fmc.GetTextTextBox(fvSite, "tbURL"); } }
    protected string save_urlhelp { get { return base.fmc.GetTextTextBox(fvSite, "tbURLHelp"); } }

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
    protected void fvSite_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        OutFormView(fvSite, e.NewMode);
    }
    /// <summary>
    /// Включить или отключить режим контроля на стороне клиентов (необходим когда внедряется окно добавить новый список выбора)
    /// </summary>
    /// <param name="value"></param>
    public void ValidateClient(bool value)
    {
        base.fmc.SetEnableRegularExpressionValidator(fvSite, "revDescription", value);
        base.fmc.SetEnableRegularExpressionValidator(fvSite, "revDescriptionEng", value);
        base.fmc.SetEnableRequiredFieldValidator(fvSite, "rfvDescription", value);
        base.fmc.SetEnableRequiredFieldValidator(fvSite, "rfvDescriptionEng", value);
    }
    #endregion

    #region Методы привязки данных
    /// <summary>
    /// Переопределим
    /// </summary>
    public override void DataBind()
    {
        base.OnDataBinding(EventArgs.Empty);
        OutFormView(fvSite, base.ModeTable);
    }
    protected void fvSite_DataBound(object sender, EventArgs e)
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
            OutFormView(fvSite, this.ModeTable); // если стоит сдесь тогда работает все события кнопок
        }
    }
    #endregion

    #region Методы работы с источником данных

    protected void odsTableSite_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["IDSite"] = this.idsite;
        if (DataSelecting != null) DataSelecting(this, e);
    }
    protected void odsTableSite_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataSelected != null) DataSelected(this, e);
    }
    protected void odsTableSite_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["URL"] = this.save_url;
        e.InputParameters["Description"] = this.save_description;
        e.InputParameters["DescriptionEng"] = this.save_descriptioneng;
        e.InputParameters["URLHelp"] = this.save_urlhelp;
        if (DataInserting != null) DataInserting(this, e);
    }
    protected void odsTableSite_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataInserted != null) DataInserted(this, e);
    }
    protected void odsTableSite_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["URL"] = this.save_url;
        e.InputParameters["Description"] = this.save_description;
        e.InputParameters["DescriptionEng"] = this.save_descriptioneng;
        e.InputParameters["URLHelp"] = this.save_urlhelp;
        if (DataUpdating != null) DataUpdating(this, e);
    }
    protected void odsTableSite_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataUpdated != null) DataUpdated(this, e);
    }
    protected void odsTableSite_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["IDSite"] = this.idsite;
        if (DataDeleting != null) DataDeleting(this, e);
    }
    protected void odsTableSite_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataDeleted != null) DataDeleted(this, e);
    }
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


    protected void btURL_Click(object sender, EventArgs e)
    {
        this.bURL = true; this.bURLHelp = false;
        FileBrowser.VisibleFileBrowser(true);
        FileBrowser.SetStartDirectory();
    }
    protected void btURLHelp_Click(object sender, EventArgs e)
    {
        this.bURL = false; this.bURLHelp = true;
        FileBrowser.VisibleFileBrowser(true);
        FileBrowser.SetStartDirectory();
    }
    /// <summary>
    /// Файл выбран
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void FileBrowser_FileListSelected(object sender, EventArgs e)
    {
        if ((((TextBox)fvSite.FindControl("tbURL")) != null) & (this.bURL))
        {
            this.bURL = false;
            ((TextBox)fvSite.FindControl("tbURL")).Text = FileBrowser.SelectPathFile;

        }
        if ((((TextBox)fvSite.FindControl("tbURLHelp")) != null) & (this.bURLHelp))
        {
            this.bURLHelp = false;
            ((TextBox)fvSite.FindControl("tbURLHelp")).Text = FileBrowser.SelectPathFile;
        }
        FileBrowser.VisibleFileBrowser(false);
    }
    /// <summary>
    /// Закрыть панель выбора файла
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void FileBrowser_CloseClick(object sender, EventArgs e)
    {
        FileBrowser.VisibleFileBrowser(false); this.bURL = false; this.bURLHelp = false;
    }


    protected void btURLClear_Click(object sender, EventArgs e)
    {
        base.fmc.SetTextTextBox(fvSite, "tbURL", "");
    }

    protected void btURLHelpClear_Click(object sender, EventArgs e)
    {
        base.fmc.SetTextTextBox(fvSite, "tbURLHelp", "");
    }
}