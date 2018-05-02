using Strategic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controlOrder : BaseControlUpdateFormView
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ controlTypeProject
    // ID выборки
    public int? IDOrder
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

    protected OrderContent order { get; set; }

    protected int? idfile
    {
        get
        {
            if (ViewState["IDFILE"] == null) { return null; }
            return (int)ViewState["IDFILE"];
        }
        set { ViewState["IDFILE"] = value; }
    }
    protected int? idfileeng
    {
        get
        {
            if (ViewState["IDFILEENG"] == null) { return null; }
            return (int)ViewState["IDFILEENG"];
        }
        set { ViewState["IDFILEENG"] = value; }
    }

    protected int? idnewfile
    {
        get;
        set;
    }
    protected int? idnewfileeng
    {
        get;
        set;
    }

    // Получить текущий режим таблицы
    public FormViewMode CurrentMode { get { return fvTable.CurrentMode; } }

    protected int? idorder { get { return this.IDOrder != null ? this.IDOrder : null; } }

    protected bool outinfo { get { return this.OutInfoText != null ? this.OutInfoText : false; } }


    protected int save_idtypeorder
    {
        get
        {
            return (int)base.fmc.GetValueDropDownList(fvTable, "ddlTypeOrder", 0);
        }
    }
    protected int? save_numorder { 
        get { 
            string numorder = base.fmc.GetTextTextBox(fvTable, "tbNumOrder");
            if (numorder != null) { return int.Parse(numorder); }
            return null;
        } 
    }

    protected DateTime? save_dateorder
    {
        get
        {
            string date = base.fmc.GetTextTextBox(fvTable, "tbDateOrder");
            if (date != null)
            {
                return DateTime.Parse(date);
            }
            return null;
        }
    }
    protected string save_order { get { return base.fmc.GetTextTextBox(fvTable, "tbOrder"); } }
    protected string save_ordereng { get { return base.fmc.GetTextTextBox(fvTable, "tbOrderEng"); } }


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
        if (e.NewMode == FormViewMode.Insert) { this.idfile = null; this.idfileeng = null; }
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void fvTable_DataBound(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// Привязка данных 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlTypeOrder_DataBound(object sender, EventArgs e)
    {
        if (this.order != null)
        {
            if (fvTable.CurrentMode == FormViewMode.Edit)
            { ((DropDownList)sender).SelectedValue = this.order.IDTypeOrder.ToString(); }
        }
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
           // this.DataBind();
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
        this.order = this.idorder != null ? corder.GetCultureOrderContent((int)this.idorder) : null;
        if (this.order != null) { this.idfile = this.order.IDFile; this.idfileeng = this.order.IDFileEng; }
        e.InputParameters["IDOrder"] = this.idorder;
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
        // Сначало сохраним файл если есть 
        int? resultFile = null;
        panelFile uf = (panelFile)fvTable.FindControl("panelUnloadFile");
        if (uf != null)
        {
            resultFile = uf.SaveFile();
            if (resultFile < 0)
            {
                // Ошибка записи файла
                e.Cancel = true;
                return;
            }
        }
        // Сначало сохраним файл если есть 
        int? resultFileEng = null;
        panelFile ufe = (panelFile)fvTable.FindControl("panelUnloadFileEng");
        if (ufe != null)
        {
            resultFileEng = ufe.SaveFile();
            if (resultFileEng < 0)
            {
                // Ошибка записи файла
                e.Cancel = true;
                return;
            }
        }
        e.InputParameters["IDTypeOrder"] = this.save_idtypeorder;
        e.InputParameters["NumOrder"] = this.save_numorder;
        e.InputParameters["DateOrder"] = this.save_dateorder;
        e.InputParameters["Order"] = this.save_order;
        e.InputParameters["OrderEng"] = this.save_ordereng;
        e.InputParameters["IDFile"] = resultFile;
        e.InputParameters["IDFileEng"] = resultFileEng;      
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
        // Сначало сохраним файл если есть 
        //int? resultFile = null;
        panelFile uf = (panelFile)fvTable.FindControl("panelUnloadFile");
        if (uf != null)
        {
            this.idnewfile = uf.SaveFile();
            if (this.idnewfile < 0)
            {
                // Ошибка записи файла
                e.Cancel = true;
                return;
            }
        }
        // Сначало сохраним файл если есть 
        //int? resultFileEng = null;
        panelFile ufe = (panelFile)fvTable.FindControl("panelUnloadFileEng");
        if (ufe != null)
        {
            this.idnewfileeng = ufe.SaveFile();
            if (this.idnewfileeng < 0)
            {
                // Ошибка записи файла
                e.Cancel = true;
                return;
            }
        }        
        e.InputParameters["IDOrder"] = this.idorder;
        e.InputParameters["IDTypeOrder"] = this.save_idtypeorder;
        e.InputParameters["NumOrder"] = this.save_numorder;
        e.InputParameters["DateOrder"] = this.save_dateorder;
        e.InputParameters["Order"] = this.save_order;
        e.InputParameters["OrderEng"] = this.save_ordereng;
        e.InputParameters["IDFile"] = this.idnewfile;
        e.InputParameters["IDFileEng"] = this.idnewfileeng;
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
        if ((int)e.ReturnValue >= 0)
        {
            if ((this.idfile != this.idnewfile) & (this.idfile != null))
            {
                base.cfile.DelFile((decimal)this.idfile);
            }

            if ((this.idfileeng != this.idnewfileeng) & (this.idfileeng != null))
            {
                base.cfile.DelFile((decimal)this.idfileeng);
            }
        }
        if (DataUpdated != null) DataUpdated(this, e);

    }
    /// <summary>
    /// Удалить
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["IDOrder"] = this.idorder;
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

    #region Обработка панели panelUnloadFile
    /// <summary>
    /// Инициализация панели выбора файла
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void panelUnloadFile_Init(object sender, EventArgs e)
    {
        ((panelFile)sender).IDFile = this.idfile;
    }
    /// <summary>
    /// Инициализация панели выбора файла на анг
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void panelUnloadFileEng_Init(object sender, EventArgs e)
    {
        ((panelFile)sender).IDFile = this.idfileeng;
    }
    #endregion

    #region Обработка панели controlTypeOrder
    /// <summary>
    /// Добавить новый тип документа
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btInsertTypeOrder_Click(object sender, EventArgs e)
    {
        if (fvTable.FindControl("InsertcontrolTypeOrder") != null)
        {
            controlTypeOrder cto = (controlTypeOrder)fvTable.FindControl("InsertcontrolTypeOrder");
            if (cto != null) 
            { 
                cto.ModeTable = FormViewMode.Insert;
                cto.DataBind();
            }
        }
    }
    /// <summary>
    /// Добавлен новый тип документа
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void InsertcontrolTypeOrder_DataInserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        base.fmc.DataBindDropDownList(fvTable, "ddlTypeOrder");
    }
    /// <summary>
    /// Отмена добавления нового типа
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void InsertcontrolTypeOrder_DataCancelClick(object sender, EventArgs e)
    {

    }

    #endregion
}