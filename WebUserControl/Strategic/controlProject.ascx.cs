using Strategic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controlProject : BaseControlUpdateFormView
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ controlProject
    // ID выборки
    public int? IDProject { get; set; }
    // ID менеджер
    public int? Insert_IDMenagerProject { get; set; }
    /// <summary>
    /// Выводить информацию 
    /// </summary>
    public bool OutInfoText
    {
        get;
        set;
    }

    // Получить текущий режим таблицы
    public FormViewMode CurrentMode { get { return fvTable.CurrentMode; } }

    protected int? idfile 
    {
        get 
        { 
            if (ViewState["IDFILE"] == null) { return null; } 
            return (int)ViewState["IDFILE"]; 
        }
        set { ViewState["IDFILE"] = value; } 
    }

    protected ProjectEntity project  { get; set; }

    protected int? idproject { get { return this.IDProject != null ? this.IDProject : null; } }

    protected bool outinfo { get { return this.OutInfoText != null ? this.OutInfoText : false; } }

    protected string wsignature { get { return base.GetStringBaseResource("WPhone"); } }
    protected string msignature { get { return base.GetStringBaseResource("MPhone"); } }

    protected int save_idtemplatestepproject
    {
        get
        {
            DropDownList ddl = base.fmc.GetDropDownList(fvTable, "ddlTemplate");
            if (ddl != null)
            {
                if (ddl.SelectedIndex >= 0) { return int.Parse(ddl.SelectedValue); }

            }
            return 0;
        }
    }    
    protected int save_idtypeproject
    {
        get
        {
            DropDownList ddl = base.fmc.GetDropDownList(fvTable, "ddlTypeProject");
            if (ddl != null)
            {
                if (ddl.SelectedIndex >= 0) { return int.Parse(ddl.SelectedValue); }

            }
            return 0;
        }
    }
    protected int? save_idimplementationprogram
    {
        get
        {
            DropDownList ddl = base.fmc.GetDropDownList(fvTable, "ddlImplementationProgram");
            if (ddl != null)
            {
                if (ddl.SelectedIndex > 0) { return int.Parse(ddl.SelectedValue); }

            }
            return null;
        }
    }
    protected int? save_lineowner
    {
        get
        {
            DropDownList ddl = base.fmc.GetDropDownList(fvTable, "ddlLineOwner");
            if (ddl != null)
            {
                if (ddl.SelectedIndex > 0) { return int.Parse(ddl.SelectedValue); }

            }
            return null;
        }
    }
    protected int save_idmenagerproject
    {
        get
        {
            DropDownList ddl = base.fmc.GetDropDownList(fvTable, "ddlNameMenager");
            if (ddl != null)
            {
                if (ddl.SelectedIndex >= 0) { return int.Parse(ddl.SelectedValue); }

            }
            return 0;
        }
    }
    protected int? save_idreplacementmenagerproject
    {
        get
        {
            DropDownList ddl = base.fmc.GetDropDownList(fvTable, "ddlActingNameMenager");
            if (ddl != null)
            {
                if (ddl.SelectedIndex > 0) { return int.Parse(ddl.SelectedValue); }

            }
            return null;
        }
    }
    protected int save_idsection
    {
        get
        {
            DropDownList ddl = base.fmc.GetDropDownList(fvTable, "ddlSection");
            if (ddl != null)
            {
                if (ddl.SelectedIndex >= 0) { return int.Parse(ddl.SelectedValue); }

            }
            return 0;
        }
    }
    protected string save_sapcode { get { return base.fmc.GetTextTextBox(fvTable, "tbSAPCode"); } }
    protected string save_typestring { get { return base.fmc.GetTextTextBox(fvTable, "tbTypeString"); } }
    protected int save_typestatus
    {
        get
        {
            DropDownList ddl = base.fmc.GetDropDownList(fvTable, "ddlTypeStatus");
            if (ddl != null)
            {
                if (ddl.SelectedIndex >= 0) { return int.Parse(ddl.SelectedValue); }

            }
            return 0;
        }
    }
    protected decimal? save_funding
    {
        get
        {
            if (base.fmc.GetTextTextBox(fvTable, "tbFunding") != null)
            {
                return decimal.Parse(base.fmc.GetTextTextBox(fvTable, "tbFunding"));
            } 
            return null;
        }
    }
    protected int? save_currency
    {
        get
        {
            DropDownList ddl = base.fmc.GetDropDownList(fvTable, "ddlCurrency");
            if (ddl != null)
            {
                if (ddl.SelectedIndex >= 0) { return int.Parse(ddl.SelectedValue); }

            }
            return null;
        }
    }
    protected string save_fundingdescription { get { return base.fmc.GetTextTextBox(fvTable, "tbFundingDescription"); } }
    protected bool save_allocationfunds { get { return base.fmc.GetCheckedCheckBox(fvTable, "cbAllocationFunds"); } }
    protected int save_year
    {
        get
        {
            DropDownList ddl = base.fmc.GetDropDownList(fvTable, "ddlYear");
            if (ddl != null)
            {
                if (ddl.SelectedIndex >= 0) { return int.Parse(ddl.SelectedValue); }

            }
            return 0;
        }
    }
    protected string save_name { get { return base.fmc.GetTextTextBox(fvTable, "tbName"); } }
    protected string save_nameeng { get { return base.fmc.GetTextTextBox(fvTable, "tbNameEng"); } }
    protected string save_description { get { return base.fmc.GetTextTextBox(fvTable, "tbDescription"); } }
    protected string save_descriptioneng { get { return base.fmc.GetTextTextBox(fvTable, "tbDescriptionEng"); } }
    protected string save_contractor { get { return base.fmc.GetTextTextBox(fvTable, "tbContractor"); } }
    protected string save_datecontractor { get { return base.fmc.GetTextTextBox(fvTable, "tbDateContractor"); } }
    protected int? save_idorder
    {
        get
        {
            DropDownList ddl = base.fmc.GetDropDownList(fvTable, "ddlListOrder");
            if (ddl != null)
            {
                if (ddl.SelectedIndex > 0) { return int.Parse(ddl.SelectedValue); }

            }
            return null;
        }
    }
    protected int save_status
    {
        get
        {
            DropDownList ddl = base.fmc.GetDropDownList(fvTable, "ddlStatus");
            if (ddl != null)
            {
                if (ddl.SelectedIndex >= 0) { return int.Parse(ddl.SelectedValue); }

            }
            return 0;
        }
    }
    protected int save_typeconstruction
    {
        get
        {
            DropDownList ddl = base.fmc.GetDropDownList(fvTable, "ddlTypeConstruction");
            if (ddl != null)
            {
                if (ddl.SelectedIndex >= 0) { return int.Parse(ddl.SelectedValue); }

            }
            return 0;
        }
    }
    protected int? insert_idmenager { get { return this.Insert_IDMenagerProject != null ? this.Insert_IDMenagerProject : null; } }

    // делегаты
    public delegate void StepClear_Click(object sender, EventArgs e);
    public delegate void StepCreate_Click(object sender, EventArgs e);
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
    public event StepClear_Click StepClearClick;
    public event StepCreate_Click StepCreateClick;

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
        base.fmc.SetEnableRegularExpressionValidator(fvTable, "revName", value);
        base.fmc.SetEnableRegularExpressionValidator(fvTable, "revNameEng", value);
        base.fmc.SetEnableRegularExpressionValidator(fvTable, "revDescription", value);
        base.fmc.SetEnableRegularExpressionValidator(fvTable, "revDescriptionEng", value);
        base.fmc.SetEnableRegularExpressionValidator(fvTable, "revFunding", value);
        base.fmc.SetEnableRequiredFieldValidator(fvTable, "rfvName", value);
        base.fmc.SetEnableRequiredFieldValidator(fvTable, "rfvNameEng", value);
        base.fmc.SetEnableRequiredFieldValidator(fvTable, "rfvDescription", value);
        base.fmc.SetEnableRequiredFieldValidator(fvTable, "rfvDescriptionEng", value);
        base.fmc.SetEnableRequiredFieldValidator(fvTable, "rfvFunding", value);
    }
    #endregion

    #region Методы привязки данных
    /// <summary>
    /// Переопределим
    /// </summary>
    public override void DataBind()
    {
        base.OnDataBinding(EventArgs.Empty);
        fvTable.Visible = this.Change;
        lvTable.Visible = !this.Change;
        OutFormView(fvTable, base.ModeTable);   // Обновить форму для редактирования
        lvTable.DataBind();                     // Обновить форму для просмотра
    }
    /// <summary>
    /// Привязка данных тип проекта
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlTypeProject_DataBound(object sender, EventArgs e)
    {
        if (fvTable.CurrentMode == FormViewMode.Edit)
        { ((DropDownList)sender).SelectedValue = this.project.IDTypeProject.ToString(); }
        //if (fvTable.CurrentMode == FormViewMode.Insert)
        //{ ((DropDownList)sender).SelectedValue = DateTime.Now.Year.ToString(); }
    }
    /// <summary>
    /// Привязка данных тип статуса
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlTypeStatus_DataBound(object sender, EventArgs e)
    {
        if (fvTable.CurrentMode == FormViewMode.Edit)
        { ((DropDownList)sender).SelectedValue = ((int)this.project.TypeStatus).ToString(); }
        //if (fvTable.CurrentMode == FormViewMode.Insert)
        //{ ((DropDownList)sender).SelectedValue = DateTime.Now.Year.ToString(); }
    }
    /// <summary>
    /// Привязка данных менеджер проекта
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlNameMenager_DataBound(object sender, EventArgs e)
    {
        if (fvTable.CurrentMode == FormViewMode.Edit)
        { ((DropDownList)sender).SelectedValue = this.project.IDMenagerProject.ToString(); }
        if (fvTable.CurrentMode == FormViewMode.Insert)
        {
            if (this.insert_idmenager != null)
            {
                ((DropDownList)sender).SelectedValue = this.insert_idmenager.ToString();
                ((DropDownList)sender).Enabled = false;
            }
            else { ((DropDownList)sender).Enabled = true;}
        }
    }
    /// <summary>
    /// Привязка данных исполняющий
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlActingNameMenager_DataBound(object sender, EventArgs e)
    {
        if (fvTable.CurrentMode == FormViewMode.Edit)
        {
            if (this.project.IDReplacementProject != null)
            {
                ((DropDownList)sender).SelectedValue = this.project.IDReplacementProject.ToString();
            }
        }
        //if (fvTable.CurrentMode == FormViewMode.Insert)
        //{ ((DropDownList)sender).SelectedValue = DateTime.Now.Year.ToString(); }
    }
    /// <summary>
    /// Привязка данных подразделение
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlSection_DataBound(object sender, EventArgs e)
    {
        if (fvTable.CurrentMode == FormViewMode.Edit)
        { ((DropDownList)sender).SelectedValue = this.project.IDSection.ToString(); }
        //if (fvTable.CurrentMode == FormViewMode.Insert)
        //{ ((DropDownList)sender).SelectedValue = DateTime.Now.Year.ToString(); }
    }
    /// <summary>
    /// Привязка данных владелец строки
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlLineOwner_DataBound(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Insert(0, new ListItem(base.GetStringBaseResource("ddlIndeterminately"), "0"));
        if (fvTable.CurrentMode == FormViewMode.Edit)
        {
            ((DropDownList)sender).SelectedValue = this.project.LineOwner != null ? ((int)this.project.LineOwner).ToString() : "0";
        }
        if (fvTable.CurrentMode == FormViewMode.Insert)
        { ((DropDownList)sender).SelectedValue = "0"; }
    }
    /// <summary>
    /// Привязка данных к ddlYear
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlYear_DataBound(object sender, EventArgs e)
    {
        int Yare = DateTime.Now.Year;
        int index = Yare - 3;
        while (index <= Yare + 3)
        {
            ((DropDownList)sender).Items.Add(new ListItem(index.ToString(), index.ToString()));
            index++;
        }
        if (fvTable.CurrentMode == FormViewMode.Edit)
        { ((DropDownList)sender).SelectedValue = this.project.Year.ToString(); }
        if (fvTable.CurrentMode == FormViewMode.Insert)
        { ((DropDownList)sender).SelectedValue = DateTime.Now.Year.ToString(); }
    }
    /// <summary>
    /// Привязка данных к ddlCurrency
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCurrency_DataBound(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Clear();
        foreach (typeCurrency tr in Enum.GetValues(typeof(typeCurrency)))
        {
            ((DropDownList)sender).Items.Add(new ListItem(tr.ToString(), ((int)tr).ToString()));
        }
        if (fvTable.CurrentMode == FormViewMode.Edit)
        {
            if (this.project.Currency != null)
            {
                ((DropDownList)sender).SelectedValue = ((int)this.project.Currency).ToString();
            }
        }
        if (fvTable.CurrentMode == FormViewMode.Insert)
        { ((DropDownList)sender).SelectedValue = ((int)typeCurrency.UAH).ToString(); }
    }
    /// <summary>
    /// Привязка к данным ddlStatus
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlStatus_DataBound(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Clear();
        foreach (statusProject sp in Enum.GetValues(typeof(statusProject)))
        {
            if (sp != statusProject.Deleted)
            {
                ((DropDownList)sender).Items.Add(new ListItem(base.cproject.GetStringStrategicResource(sp.ToString()), ((int)sp).ToString()));
            }
            
        }
        if (fvTable.CurrentMode == FormViewMode.Edit)
        { ((DropDownList)sender).SelectedValue = ((int)this.project.Status).ToString(); }
        if (fvTable.CurrentMode == FormViewMode.Insert)
        { ((DropDownList)sender).SelectedValue = ((int)statusProject.Study).ToString(); }
    }
    /// <summary>
    /// Привязка данных тип строительства
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlTypeConstruction_DataBound(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Clear();
        foreach (typeConstruction tc in Enum.GetValues(typeof(typeConstruction)))
        {
            ((DropDownList)sender).Items.Add(new ListItem(base.cproject.GetStringStrategicResource(tc.ToString()), ((int)tc).ToString()));
        }
        if (fvTable.CurrentMode == FormViewMode.Edit)
        { ((DropDownList)sender).SelectedValue = ((int)this.project.TypeConstruction).ToString(); }
        if (fvTable.CurrentMode == FormViewMode.Insert)
        { ((DropDownList)sender).SelectedValue = ((int)typeConstruction.Not).ToString(); }
    }
    /// <summary>
    /// Привязка к данным программа внедрения
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlImplementationProgram_DataBound(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Clear();
        foreach (implementationProgram ip in Enum.GetValues(typeof(implementationProgram)))
        {
            ((DropDownList)sender).Items.Add(new ListItem(base.cproject.GetStringStrategicResource(ip.ToString()), ((int)ip).ToString()));
        }
        ((DropDownList)sender).Items.Insert(0, new ListItem(base.GetStringBaseResource("ddlNot"), "0"));
        if (fvTable.CurrentMode == FormViewMode.Edit)
        { 
            ((DropDownList)sender).SelectedValue = this.project.IDImplementationProgram!= null ? ((int)this.project.IDImplementationProgram).ToString(): "0" ; 
        }
        if (fvTable.CurrentMode == FormViewMode.Insert)
        { ((DropDownList)sender).SelectedValue = "0"; }
    }
    /// <summary>
    /// Привязка данных приказ проекта
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlListOrder_DataBound(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Insert(0, new ListItem(base.GetStringBaseResource("ddlNot"), "0"));
        if (fvTable.CurrentMode == FormViewMode.Edit)
        { if (this.project.IDOrder != null) { ((DropDownList)sender).SelectedValue = ((int)this.project.IDOrder).ToString(); } }
        else { ((DropDownList)sender).SelectedValue = "0"; }
        if (fvTable.CurrentMode == FormViewMode.Insert)
        { ((DropDownList)sender).SelectedValue = "0"; }
    }
    /// <summary>
    /// Общая привязка данных
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
    /// сделать выборку исполнителей
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsListActingMenager_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (this.project != null)
        {
            e.InputParameters["IDMenagerProject"] = this.project.IDMenagerProject;
        }
        else { e.InputParameters["IDMenagerProject"] = null; }
        e.InputParameters["value"] = "ddlNot";
    }
    /// <summary>
    /// Выбрать
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        this.project = this.idproject!=null ? cproject.GetCultureProject((int)this.idproject): null;
        if (this.project != null) { this.idfile = this.project.Effect; }
        e.InputParameters["IDProject"] = this.idproject;
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
        panelFile uf = (panelFile)fvTable.FindControl("panelFile");
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
        e.InputParameters["IDTemplateStepProject"] = this.save_idtemplatestepproject;        
        e.InputParameters["IDTypeProject"] = this.save_idtypeproject;
        e.InputParameters["IDImplementationProgram"] = this.save_idimplementationprogram;
        e.InputParameters["IDMenagerProject"] = this.save_idmenagerproject;
        e.InputParameters["IDReplacementProject"] = this.save_idreplacementmenagerproject;
        e.InputParameters["IDSection"] = this.save_idsection;
        e.InputParameters["SAPCode"] = this.save_sapcode;
        e.InputParameters["TypeString"] = this.save_typestring;
        e.InputParameters["TypeStatus"] = this.save_typestatus;
        e.InputParameters["Funding"] = this.save_funding;
        e.InputParameters["Currency"] = this.save_currency;
        e.InputParameters["FundingDescription"] = this.save_fundingdescription;
        e.InputParameters["AllocationFunds"] = this.save_allocationfunds;
        e.InputParameters["LineOwner"] = this.save_lineowner;
        e.InputParameters["Year"] = this.save_year;
        e.InputParameters["Name"] = this.save_name;
        e.InputParameters["NameEng"] = this.save_nameeng;
        e.InputParameters["Description"] = this.save_description;
        e.InputParameters["DescriptionEng"] = this.save_descriptioneng;
        e.InputParameters["Contractor"] = this.save_contractor;
        e.InputParameters["DateContractor"] = this.save_datecontractor;
        e.InputParameters["Effect"] = resultFile;
        e.InputParameters["Status"] = this.save_status;
        e.InputParameters["IDOrder"] = this.save_idorder;
        e.InputParameters["TypeConstruction"] = this.save_typeconstruction;      
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
        int? resultFile = null;
        panelFile uf = (panelFile)fvTable.FindControl("panelFile");
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
        e.InputParameters["IDProject"] = this.idproject;
        e.InputParameters["IDTypeProject"] = this.save_idtypeproject;
        e.InputParameters["IDImplementationProgram"] = this.save_idimplementationprogram;
        e.InputParameters["IDMenagerProject"] = this.save_idmenagerproject;
        e.InputParameters["IDReplacementProject"] = this.save_idreplacementmenagerproject;
        e.InputParameters["IDSection"] = this.save_idsection;
        e.InputParameters["SAPCode"] = this.save_sapcode;
        e.InputParameters["TypeString"] = this.save_typestring;
        e.InputParameters["TypeStatus"] = this.save_typestatus;
        e.InputParameters["Funding"] = this.save_funding;
        e.InputParameters["Currency"] = this.save_currency;
        e.InputParameters["FundingDescription"] = this.save_fundingdescription;
        e.InputParameters["AllocationFunds"] = this.save_allocationfunds;
        e.InputParameters["LineOwner"] = this.save_lineowner;
        e.InputParameters["Year"] = this.save_year;
        e.InputParameters["Name"] = this.save_name;
        e.InputParameters["NameEng"] = this.save_nameeng;
        e.InputParameters["Description"] = this.save_description;
        e.InputParameters["DescriptionEng"] = this.save_descriptioneng;
        e.InputParameters["Contractor"] = this.save_contractor;
        e.InputParameters["DateContractor"] = this.save_datecontractor;
        e.InputParameters["Effect"] = resultFile;
        e.InputParameters["IDOrder"] = this.save_idorder; 
        e.InputParameters["TypeConstruction"] = this.save_typeconstruction;
        e.InputParameters["Status"] = this.save_status;
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
            int? resultFile = null;
            panelFile uf = (panelFile)fvTable.FindControl("panelFile");
            if (uf != null)
            {
                resultFile = uf.DelFile();
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
        e.InputParameters["IDProject"] = this.idproject;
        e.InputParameters["FullDelete"] = false;
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

    protected void btDelete_Click(object sender, EventArgs e)
    {

    }

    protected void btSave_Click(object sender, EventArgs e)
    {

    }

    protected void panelUnloadFile_Init(object sender, EventArgs e)
    {
        ((panelFile)sender).IDFile = this.idfile;
    }
    /// <summary>
    /// Создать детальные шаги внедрения проекта
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelect_StepCreateClick(object sender, EventArgs e)
    {
        if (this.idproject == null) return;
        base.cproject.CreateStepProject((int)this.idproject, 1, this.outinfo);
        if (StepCreateClick != null) StepCreateClick(this, e);
    }
    /// <summary>
    /// Очистить этапы выполнения
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelect_StepClearClick(object sender, EventArgs e)
    {
        base.cproject.ClearStepProject((int)this.idproject, this.outinfo);
        if (StepClearClick != null) StepClearClick(this, e);
    }

}