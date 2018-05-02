using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBase;

public partial class controlSection : BaseControlUpdateFormView
{

    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ controlProject
    // ID выборки
    public int? IDSection { get; set; }

    public int? Insert_ParentSection { get; set; }

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

    protected string default_caption { get { return GetStringBaseResource("captionSection"); } }

    // Получить текущий режим таблицы
    public FormViewMode CurrentMode { get { return fvTable.CurrentMode; } }

    protected SectionContent section { get; set; }
    
    /// <summary>
    /// Получить ID подразделения
    /// </summary>
    protected int? idsection { get { return this.IDSection != null ? this.IDSection : null; } }
    /// <summary>
    /// Получить признак выводить сообщения
    /// </summary>
    protected bool outinfo { get { return this.OutInfoText != null ? this.OutInfoText : false; } }
    /// <summary>
    /// Получить подпись таблицы
    /// </summary>
    protected string caption { get { return this.Caption != null ? this.Caption : this.default_caption; } }

    protected string save_section { get { return base.fmc.GetTextTextBox(fvTable, "tbSection"); } }
    protected string save_sectioneng { get { return base.fmc.GetTextTextBox(fvTable, "tbSectionEng"); } }
    protected string save_sectionfull { get { return base.fmc.GetTextTextBox(fvTable, "tbSectionFull"); } }
    protected string save_sectionfulleng { get { return base.fmc.GetTextTextBox(fvTable, "tbSectionFullEng"); } }
    protected int? save_cipher { get 
    { 
        string chp = base.fmc.GetTextTextBox(fvTable, "tbCipher");
        if (chp != "" & chp!=null) { return int.Parse(chp); }
        return null;
    } 
    }

    protected int save_typesection
    {
        get
        {
            DropDownList ddl = base.fmc.GetDropDownList(fvTable, "ddlTypeSection");
            if (ddl != null)
            {
                if (ddl.SelectedIndex >= 0) { return int.Parse(ddl.SelectedValue); }

            }
            return 0;
        }
    }
    protected int? save_parent
    {
        get
        {
            DropDownList ddl = base.fmc.GetDropDownList(fvTable, "ddlListParent");
            if (ddl != null)
            {
                if (ddl.SelectedIndex > 0) { return int.Parse(ddl.SelectedValue); }

            }
            return null;
        }
    }

    protected int? insert_parent { get { return this.Insert_ParentSection != null ? this.Insert_ParentSection : null; } }

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

    #endregion

    #region Методы привязки данных
    /// <summary>
    /// Привязка данных тип подразделения
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlTypeSection_DataBound(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Clear();
        foreach (typeSection ts in Enum.GetValues(typeof(typeSection)))
        {
            ((DropDownList)sender).Items.Add(new ListItem(base.GetStringBaseResource(ts.ToString()), ((int)ts).ToString()));
        }
        if (fvTable.CurrentMode == FormViewMode.Edit)
        { ((DropDownList)sender).SelectedValue = ((int)this.section.TypeSection).ToString(); }
        if (fvTable.CurrentMode == FormViewMode.Insert)
        { ((DropDownList)sender).SelectedValue = ((int)typeSection.Not).ToString(); }
    }
    /// <summary>
    /// Привязка данных владелец
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlListParent_DataBound(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Insert(0, new ListItem(base.GetStringBaseResource("ddlNot"), "0"));
        if (fvTable.CurrentMode == FormViewMode.Edit)
        { if (this.section.ParentID != null) { ((DropDownList)sender).SelectedValue = ((int)this.section.ParentID).ToString(); } }
        else { ((DropDownList)sender).SelectedValue = "0"; }
        if (fvTable.CurrentMode == FormViewMode.Insert)
        {
            if (insert_parent != null) { ((DropDownList)sender).SelectedValue = ((int)this.insert_parent).ToString(); }
            else
            {
                ((DropDownList)sender).SelectedValue = "0";
            }
        }
    }
    /// <summary>
    /// Переопределим
    /// </summary>
    public override void DataBind()
    {
        base.OnDataBinding(EventArgs.Empty);
        OutFormView(fvTable, base.ModeTable);
    }

    #endregion

    #region Методы загрузки компонентов и страницы выборка данных
    /// <summary>
    /// 
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
    /// Выбор владельца
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsListParent_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["Full"] = true;
        if (fvTable.CurrentMode == FormViewMode.Edit)
        { e.InputParameters["IDSection"] = this.idsection; }
        if (fvTable.CurrentMode == FormViewMode.Insert)
        { e.InputParameters["IDSection"] = null; }
    }
    /// <summary>
    /// Выбрать
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        this.section = this.idsection != null ? base.csection.GetSection((int)this.idsection) : null;
        e.InputParameters["IDSection"] = this.idsection;
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
        e.InputParameters["Section"] = this.save_section;
        e.InputParameters["SectionEng"] = this.save_sectioneng;
        e.InputParameters["SectionFull"] = this.save_sectionfull;
        e.InputParameters["SectionFullEng"] = this.save_sectionfulleng;
        e.InputParameters["TypeSection"] = this.save_typesection;
        e.InputParameters["Cipher"] = this.save_cipher;
        e.InputParameters["ParentID"] = this.save_parent;
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
        e.InputParameters["IDSection"] = this.idsection;
        e.InputParameters["Section"] = this.save_section;
        e.InputParameters["SectionEng"] = this.save_sectioneng;
        e.InputParameters["SectionFull"] = this.save_sectionfull;
        e.InputParameters["SectionFullEng"] = this.save_sectionfulleng;
        e.InputParameters["TypeSection"] = this.save_typesection;
        e.InputParameters["Cipher"] = this.save_cipher;
        e.InputParameters["ParentID"] = this.save_parent;
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
        e.InputParameters["IDSection"] = this.idsection;
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