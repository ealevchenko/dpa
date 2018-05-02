using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBase;

public partial class controlSiteMap : BaseControlUpdateFormView
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ controlSiteMap
    // ID выборки
    public int? IDSiteMap
    {
        get;
        set;
    }
    // Получить текущий режим таблицы
    public FormViewMode CurrentMode { get { return fvSiteMap.CurrentMode; } }

    protected int? idsitemap { get { return this.IDSiteMap != null ? this.IDSiteMap : null; } }
    // переменные для записи
    protected int save_idweb
    {
        get
        {
            DropDownList ddlWeb = base.fmc.GetDropDownList(fvSiteMap, "ddlWeb");
            if (ddlWeb != null)
            {
                if (ddlWeb.SelectedIndex >= 0) { return int.Parse(ddlWeb.SelectedValue); }

            }
            return -1;
        }
    }
    protected int? save_idsite
    {
        get
        {
            DropDownList ddlSite = base.fmc.GetDropDownList(fvSiteMap, "ddlSite");
            if (ddlSite != null)
            {
                if (ddlSite.SelectedIndex > 0) { return int.Parse(ddlSite.SelectedValue); }

            }
            return null;
        }
    }
    protected string save_title { get { return base.fmc.GetTextTextBox(fvSiteMap, "tbTitle"); } }
    protected string save_titleeng { get { return base.fmc.GetTextTextBox(fvSiteMap, "tbTitleEng"); } }
    protected string save_description { get { return base.fmc.GetTextTextBox(fvSiteMap, "tbDescription"); } }
    protected string save_descriptioneng { get { return base.fmc.GetTextTextBox(fvSiteMap, "tbDescriptionEng"); } }
    protected bool save_protection { get { return base.fmc.GetCheckedCheckBox(fvSiteMap, "cbProtection"); } }
    protected bool save_pageprocessor { get { return base.fmc.GetCheckedCheckBox(fvSiteMap, "cbPageProcessor"); } }
    protected int? save_parentid
    {
        get
        {
            DropDownList ddlParent = base.fmc.GetDropDownList(fvSiteMap, "ddlParent");
            if (ddlParent != null)
            {
                if (ddlParent.SelectedIndex > 0) { return int.Parse(ddlParent.SelectedValue); }

            }
            return null;
        }
    }
    protected int save_idsection
    {
        get
        {
            DropDownList ddlSection = base.fmc.GetDropDownList(fvSiteMap, "ddlSection");
            if (ddlSection != null)
            {
                if (ddlSection.SelectedIndex >= 0) { return int.Parse(ddlSection.SelectedValue); }

            }
            return 0;
        }
    }
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

    #region МЕТОДЫ controlSiteMap
    /// <summary>
    /// Установить состояние компонентов в зависимости от поля ParentID
    /// </summary>
    protected void SetParent(int? parentid) 
    {
        if (parentid == null)
        {
            base.fmc.SetEnableDropDownList(fvSiteMap, "ddlWeb", true);
            base.fmc.SetEnableButton(fvSiteMap, "btInsertWeb", true);
        }
        else 
        {
            base.fmc.SetEnableDropDownList(fvSiteMap, "ddlWeb", false);
            base.fmc.SetEnableButton(fvSiteMap, "btInsertWeb", false);
            DropDownList ddlWeb = base.fmc.GetDropDownList(fvSiteMap, "ddlWeb");
            if (ddlWeb != null)
            {
                int IDWeb = csitemap.GetIDWeb((int)parentid);
                if (IDWeb != -1)
                {
                    ddlWeb.SelectedValue = IDWeb.ToString();
                }
                else
                {
                    ddlWeb.SelectedIndex = -1;
                }
            }
        }
    }
    /// <summary>
    /// Установить состояние компонентов в зависимости от поля IDSite
    /// </summary>
    /// <param name="idsite"></param>
    protected void SetSite(int? idsite) 
    {
        if (idsite == null)
        {
            base.fmc.SetEnableCheckBox(fvSiteMap, "cbProtection", false);
            base.fmc.SetEnableCheckBox(fvSiteMap, "cbPageProcessor", false);
        }
        else 
        {
            base.fmc.SetEnableCheckBox(fvSiteMap, "cbProtection", true);
            base.fmc.SetEnableCheckBox(fvSiteMap, "cbPageProcessor", true);
        }
    }
    #region Методы управления режимами
    /// <summary>
    /// Событие перед обновлением режима
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void fvSiteMap_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        OutFormView(fvSiteMap, e.NewMode);
    }
    /// <summary>
    /// Включить или отключить режим контроля на стороне клиентов (необходим когда внедряется окно добавить новый список выбора)
    /// </summary>
    /// <param name="value"></param>
    public void ValidateClient(bool value)
    {
        base.fmc.SetEnableRegularExpressionValidator(fvSiteMap, "revTitle", value);
        base.fmc.SetEnableRegularExpressionValidator(fvSiteMap, "revTitleEng", value);
        base.fmc.SetEnableRegularExpressionValidator(fvSiteMap, "revDescription", value);
        base.fmc.SetEnableRegularExpressionValidator(fvSiteMap, "revDescriptionEng", value);
        base.fmc.SetEnableRequiredFieldValidator(fvSiteMap, "rfvTitle", value);
        base.fmc.SetEnableRequiredFieldValidator(fvSiteMap, "rfvTitleEng", value);
        base.fmc.SetEnableRequiredFieldValidator(fvSiteMap, "rfvDescription", value);
        base.fmc.SetEnableRequiredFieldValidator(fvSiteMap, "rfvDescriptionEng", value);
    }
    #endregion

    #region Методы привязки данных
    /// <summary>
    /// Переопределим
    /// </summary>
    public override void DataBind()
    {
        base.OnDataBinding(EventArgs.Empty);
        OutFormView(fvSiteMap, base.ModeTable);
    }
    /// <summary>
    /// Привязка данных сайт
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlSite_DataBinding(object sender, EventArgs e)
    {
        if (fvSiteMap.CurrentMode == FormViewMode.Edit)
        {
            SiteMapDetali smd = base.csitemap.GetSiteMapDetali((int)this.idsitemap);
            if (smd == null) return;
            DropDownList ddlSite = base.fmc.GetDropDownList(fvSiteMap, "ddlSite");
            if (ddlSite != null)
            {
                if (smd.IDSite != null)
                {
                    ddlSite.SelectedValue = smd.IDSite.ToString();
                }
                else
                {
                    ddlSite.SelectedIndex = 0;
                }
            }
        }
        if (fvSiteMap.CurrentMode == FormViewMode.Insert)
        {
            DropDownList ddlSite = base.fmc.GetDropDownList(fvSiteMap, "ddlSite");
            if (ddlSite != null)
            {
                if (this.save_idsite != null)
                {
                    ddlSite.SelectedValue = this.save_idsite.ToString();
                }
                else
                {
                    ddlSite.SelectedIndex = 0;
                }
            }            
        }
        SetSite(this.save_idsite);
    }
    /// <summary>
    /// Привязка данных Подразделение
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlSection_DataBound(object sender, EventArgs e)
    {
        if (fvSiteMap.CurrentMode == FormViewMode.Edit)
        {
            SiteMapDetali smd = base.csitemap.GetSiteMapDetali((int)this.idsitemap);
            if (smd == null) return;
            DropDownList ddlSection = base.fmc.GetDropDownList(fvSiteMap, "ddlSection");
            if (ddlSection != null)
            {
                if (smd.IDSection != null)
                {
                    ddlSection.SelectedValue = smd.IDSection.ToString();
                }
                else
                {
                    ddlSection.SelectedIndex = -1;
                }
            }
        }
        if (fvSiteMap.CurrentMode == FormViewMode.Insert)
        {
            DropDownList ddlSection = base.fmc.GetDropDownList(fvSiteMap, "ddlSection");
            if (ddlSection != null)
            {
                if (this.save_idsection != -1)
                {
                    ddlSection.SelectedValue = this.save_idsection.ToString();
                }
                else
                {
                    ddlSection.SelectedIndex = -1;
                }
            }
        }
    }
    /// <summary>
    /// Обновили подключения
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void fvSiteMap_DataBound(object sender, EventArgs e)
    {
        if (fvSiteMap.CurrentMode == FormViewMode.Insert)
        {

        }
        if (fvSiteMap.CurrentMode == FormViewMode.Edit)
        {
            SiteMapDetali smd = base.csitemap.GetSiteMapDetali((int)this.idsitemap);
            if (smd == null) return;
            DropDownList ddlParent = base.fmc.GetDropDownList(fvSiteMap, "ddlParent");
            if (ddlParent != null)
            {
                if (smd.IDParent != null)
                {
                    ddlParent.SelectedValue = smd.IDParent.ToString();
                }
                else
                {
                    ddlParent.SelectedIndex = 0;

                }
                SetParent(this.save_parentid);
            }
            //DropDownList ddlSection = GetDropDownList(fvSiteMap, "ddlSection");
            //if (ddlSection != null)
            //{
            //    if (smd.IDSection != null)
            //    {
            //        ddlSection.SelectedValue = smd.IDSection.ToString();
            //    }
            //    else
            //    {
            //        ddlSection.SelectedIndex = -1;
            //    }
            //}

        }
    }
    #endregion

    #region Методы загрузки компонентов и страницы выборка данных
    /// <summary>
    /// Подготовка перед выбором списка родителей меню (В режиме Edit исключает выбор дочерних меню (зацикливание))
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsListParent_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (fvSiteMap.CurrentMode == FormViewMode.Insert)
        {
            e.InputParameters["IDSiteMap"] = null;
        }
        if (fvSiteMap.CurrentMode == FormViewMode.Edit)
        {
            e.InputParameters["IDSiteMap"] = IDSiteMap;
        }
    }
    /// <summary>
    /// Загрузка страницы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            OutFormView(fvSiteMap, this.ModeTable); // если стоит сдесь тогда работает все события кнопок
        }
        SetParent(this.save_parentid);
        SetSite(this.save_idsite);
    }
    /// <summary>
    /// Требуется обновить компоненты загружаемые из бд
    /// </summary>
    public void ReloadingControl()
    {
        DropDownList ddl = base.fmc.GetDropDownList(fvSiteMap, "ddlSection");
        if (ddl != null) { ddl.DataBind(); }
    }
    #endregion

    #region Методы работы с источником данных
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTableSiteMap_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["IDSiteMap"] = this.idsitemap;
        if (DataSelecting != null) DataSelecting(this, e);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTableSiteMap_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataSelected != null) DataSelected(this, e);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTableSiteMap_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["IDWeb"] = this.save_idweb;
        e.InputParameters["IDSite"] = this.save_idsite;
        e.InputParameters["Title"] = this.save_title;
        e.InputParameters["TitleEng"] = this.save_titleeng;
        e.InputParameters["Description"] = this.save_description;
        e.InputParameters["DescriptionEng"] = this.save_descriptioneng;
        if (this.save_idsite != null)
        {
            e.InputParameters["Protection"] = this.save_protection;
            e.InputParameters["PageProcessor"] = this.save_pageprocessor;
        }
        else
        {
            e.InputParameters["Protection"] = false;
            e.InputParameters["PageProcessor"] = false;
        }
        e.InputParameters["ParentID"] = this.save_parentid;
        e.InputParameters["IDSection"] = this.save_idsection; 
        if (DataInserting != null) DataInserting(this, e);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTableSiteMap_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataInserted != null) DataInserted(this, e);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTableSiteMap_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["IDSiteMap"] = this.idsitemap;
        e.InputParameters["IDWeb"] = this.save_idweb;
        e.InputParameters["IDSite"] = this.save_idsite;
        e.InputParameters["Title"] = this.save_title;
        e.InputParameters["TitleEng"] = this.save_titleeng;
        e.InputParameters["Description"] = this.save_description;
        e.InputParameters["DescriptionEng"] = this.save_descriptioneng;
        if (this.save_idsite != null)
        {
            e.InputParameters["Protection"] = this.save_protection;
            e.InputParameters["PageProcessor"] = this.save_pageprocessor;
        }
        else
        {
            e.InputParameters["Protection"] = false;
            e.InputParameters["PageProcessor"] = false;
        }
        e.InputParameters["ParentID"] = this.save_parentid;
        e.InputParameters["IDSection"] = this.save_idsection;
        if (DataUpdating != null) DataUpdating(this, e);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTableSiteMap_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataUpdated != null) DataUpdated(this, e);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTableSiteMap_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["IDSiteMap"] = this.idsitemap;
        if (DataDeleting != null) DataDeleting(this, e);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTableSiteMap_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataDeleted != null) DataDeleted(this, e);
    }
    /// <summary>
    /// 
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

    #region Обработка панели controlWeb
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btInsertWeb_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region Обработка панели controlSite
    /// <summary>
    /// Открыть панель добавить
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btInsertSite_Click(object sender, EventArgs e)
    {
        if (fvSiteMap.FindControl("SiteInsert") != null)
        {
            ValidateClient(false);
            ((controlSite)fvSiteMap.FindControl("SiteInsert")).ModeTable = FormViewMode.Insert;
            ((controlSite)fvSiteMap.FindControl("SiteInsert")).DataBind();
        }
    }
    /// <summary>
    /// Добавлена новая позиция сайта
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlSite_DataInserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ValidateClient(true);
        base.fmc.GetDropDownList(fvSiteMap, "ddlSite").DataBind();
    }
    /// <summary>
    /// Отмена добавления новой позиции сайта
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlSite_DataCancelClick(object sender, EventArgs e)
    {
        ValidateClient(true);
        base.fmc.GetDropDownList(fvSiteMap, "ddlSite").DataBind();
    }
    #endregion

    #region Обработка панели controlSection
    /// <summary>
    /// Обработка controlSection
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btInsertSection_Click(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlSection_DataInserted(object sender, ObjectDataSourceStatusEventArgs e)
    {

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlSection_DataCancelClick(object sender, EventArgs e)
    {

    }
    #endregion

    #endregion

}