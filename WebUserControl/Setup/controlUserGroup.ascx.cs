using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBase;

public partial class controlUserGroup : BaseControlUpdateFormView
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ controlUserGroup
    // ID выборки
    public int? IDUser
    {
        get;
        set;
    }
    // Переменные для вставки поумолчанию
    public UserDetali InsertUser { get; set; }
    // Получить текущий режим таблицы
    public FormViewMode CurrentMode { get { return fvUserGroup.CurrentMode; } }

    protected int? iduser { get { return this.IDUser != null ? this.IDUser : null; } }
    // Инициализация по умолчанию для втавки данных
    protected UserDetali insert_user { get { return this.InsertUser == null ? new UserDetali(0, null, null, null, null, false, null, null, null, null, 0) : this.InsertUser; } }
    protected int? insert_idweb { get { return this.InsertUser == null ? null : (int?)this.InsertUser.IDWeb; } }
    protected string insert_userenterprise { get { return this.InsertUser == null ? null : (string)this.InsertUser.UserEnterprise; } }
    protected string insert_description { get { return this.InsertUser == null ? null : (string)this.InsertUser.Description; } }
    protected string insert_email { get { return this.InsertUser == null ? null : (string)this.InsertUser.Email; } }
    protected bool insert_bdistribution { get { return this.InsertUser == null ? false : (bool)this.InsertUser.bDistribution; } }
    protected string insert_surname { get { return this.InsertUser == null ? null : (string)this.InsertUser.Surname; } }
    protected string insert_name { get { return this.InsertUser == null ? null : (string)this.InsertUser.Name; } }
    protected string insert_patronymic { get { return this.InsertUser == null ? null : (string)this.InsertUser.Patronymic; } }
    protected string insert_post { get { return this.InsertUser == null ? null : (string)this.InsertUser.Post; } }
    protected int insert_idsection { get { return this.InsertUser == null ? -1 : (int)this.InsertUser.IDSection; } }
    // Тип в режиме вставки
    protected bool insert_type_folder
    {
        get
        {
            RadioButtonList grbg = fmc.GetRadioButtonList(fvUserGroup, "rblist");
            if (grbg != null) {
                if (grbg.SelectedIndex >= 0)
                {
                    return bool.Parse(grbg.SelectedValue);
                }
            }
            return false;
        }
    }

    protected bool type_folder 
    { 
        get 
        {
            if (ViewState["type_folder"] == null)
            { ViewState["type_folder"] = false; }
            return (bool)ViewState["type_folder"];
        }
        set { ViewState["type_folder"] = value; }
    }

    // переменные для записи
    protected int? save_idweb
    {
        get
        {
            DropDownList ddlWeb = base.fmc.GetDropDownList(fvUserGroup, "ddlWeb");
            if (ddlWeb != null)
            {
                if (ddlWeb.SelectedIndex >= 0) { return int.Parse(ddlWeb.SelectedValue); }

            }
            return null;
        }
    }
    protected string save_userenterprise { get { return base.fmc.GetTextTextBox(fvUserGroup, "tbAccount"); } }
    protected string save_description { get { return base.fmc.GetTextTextBox(fvUserGroup, "tbDescription"); } }
    protected string save_email { get { return base.fmc.GetTextTextBox(fvUserGroup, "tbEmail"); } }
    protected bool save_bdistribution { get { return base.fmc.GetCheckedCheckBox(fvUserGroup, "cbDistribution"); } }
    protected string save_surname { get { return base.fmc.GetTextTextBox(fvUserGroup, "tbSurname"); } }
    protected string save_name { get { return base.fmc.GetTextTextBox(fvUserGroup, "tbName"); } }
    protected string save_patronymic { get { return base.fmc.GetTextTextBox(fvUserGroup, "tbPatronymic"); } }
    protected string save_post { get { return base.fmc.GetTextTextBox(fvUserGroup, "tbPost"); } }
    protected int save_idsection
    {
        get
        {
            DropDownList ddlSection = base.fmc.GetDropDownList(fvUserGroup, "ddlSection");
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

    #region МЕТОДЫ controlUserGroup

    #region Методы управления режимами
    /// <summary>
    /// Событие перед обновлением режима
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void fvUserGroup_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        OutFormView(fvUserGroup, e.NewMode);
    }
    /// <summary>
    /// Включить или отключить режим контроля на стороне клиентов (необходим когда внедряется окно добавить новый список выбора)
    /// </summary>
    /// <param name="value"></param>
    public void ValidateClient(bool value)
    {
        base.fmc.SetEnableRegularExpressionValidator(fvUserGroup, "revEMail", value);
        base.fmc.SetEnableRegularExpressionValidator(fvUserGroup, "revUser", value);
        base.fmc.SetEnableRegularExpressionValidator(fvUserGroup, "revInsEMail", value);
        base.fmc.SetEnableRegularExpressionValidator(fvUserGroup, "revInsUser", value);
        base.fmc.SetEnableRequiredFieldValidator(fvUserGroup, "rfvUser", value);
    }
    #endregion

    #region Методы привязки данных
    /// <summary>
    /// Переопределим
    /// </summary>
    public override void DataBind()
    {
        base.OnDataBinding(EventArgs.Empty);
        OutFormView(fvUserGroup, base.ModeTable);
    }
    /// <summary>
    /// Обновили подключения
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void fvUserGroup_DataBound(object sender, EventArgs e)
    {
        if (fvUserGroup.CurrentMode == FormViewMode.Insert)
        {
            base.fmc.SetValueRadioButtonList(fvUserGroup, "rblist", this.insert_user.isFolder.ToString());
            this.type_folder = this.insert_user.isFolder;
            DropDownList ddlWeb = base.fmc.GetDropDownList(fvUserGroup, "ddlWeb");
            if (ddlWeb != null)
            {
                if (this.insert_user.IDWeb != null)
                { ddlWeb.SelectedValue = this.insert_user.IDWeb.ToString(); }
                else { ddlWeb.SelectedIndex = -1; }
            }
            base.fmc.SetTextTextBox(fvUserGroup, "tbAccount", this.insert_user.UserEnterprise);
            base.fmc.SetTextTextBox(fvUserGroup, "tbDescription", this.insert_user.Description);
            base.fmc.SetTextTextBox(fvUserGroup, "tbPost", this.insert_user.Post);
            DropDownList ddlSection = base.fmc.GetDropDownList(fvUserGroup, "ddlSection");
            if (ddlSection != null)
            {
                if (this.insert_user.IDSection >= 0)
                { ddlSection.SelectedValue = this.insert_user.IDSection.ToString(); }
                else { ddlSection.SelectedIndex = -1; }
            }
            base.fmc.SetTextTextBox(fvUserGroup, "tbSurname", this.insert_user.Surname);
            base.fmc.SetTextTextBox(fvUserGroup, "tbName", this.insert_user.Name);
            base.fmc.SetTextTextBox(fvUserGroup, "tbPatronymic", this.insert_user.Patronymic);
            base.fmc.SetTextTextBox(fvUserGroup, "tbEmail", this.insert_user.Email);
            base.fmc.SetCheckedCheckBox(fvUserGroup, "cbDistribution", this.insert_bdistribution);
        }
        if (fvUserGroup.CurrentMode == FormViewMode.Edit)
        {
            DropDownList ddlWeb = base.fmc.GetDropDownList(fvUserGroup, "ddlWeb");
            if (ddlWeb != null)
            {
                int idweb = base.cusers.GetIDWeb((int)this.iduser);

                if (idweb != -1)
                {
                    ddlWeb.SelectedValue = idweb.ToString(); type_folder = true;
                }
                else
                {
                    ddlWeb.SelectedIndex = -1; this.type_folder = false;
                }
        
            }
            DropDownList ddlSection = base.fmc.GetDropDownList(fvUserGroup, "ddlSection");
            if (ddlSection != null)
            {
                int idsec = base.cusers.GetIDSection((int)this.iduser);

                if (idsec != -1)
                {
                    ddlSection.SelectedValue = idsec.ToString();
                }
                else
                {
                    ddlSection.SelectedIndex = -1;
                }
            }
        }
    }
    #endregion

    #region Методы загрузки компонентов и страницы выборка данных
    /// <summary>
    /// Определить значение display для пользователя
    /// </summary>
    /// <param name="dataItem"></param>
    /// <returns></returns>
    public string GetDisplayUser(object dataItem)
    {
        if (DataBinder.Eval(dataItem, "IDWeb") != DBNull.Value)
        {
            return "none";
        }
        else { return "display"; }
    }
    /// <summary>
    /// Определить значение display для группы доступа
    /// </summary>
    /// <param name="dataItem"></param>
    /// <returns></returns>
    public string GetDisplayGroup(object dataItem)
    {
        if (DataBinder.Eval(dataItem, "IDWeb") != DBNull.Value)
        {
            return "display";
        }
        else { return "none"; }
    }
    /// <summary>
    /// Первый запуск
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            OutFormView(fvUserGroup, this.ModeTable); // если стоит сдесь тогда работает все события кнопок
        }
    }
    /// <summary>
    /// Загрузка RadioButtonList
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rblist_Load(object sender, EventArgs e)
    {
        if (((RadioButtonList)sender).SelectedIndex != -1)
        {
            this.type_folder = bool.Parse(((RadioButtonList)sender).SelectedValue);
        }
    }
    /// <summary>
    /// Загрузка компонента Web
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlWeb_Load(object sender, EventArgs e)
    {
        if (fvUserGroup.CurrentMode == FormViewMode.Insert)
        {
            ((DropDownList)sender).Enabled = this.insert_type_folder;
        }
    }
    /// <summary>
    /// Загрузка надписи Аккаунт
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ltAccount_Load(object sender, EventArgs e)
    {
        if (fvUserGroup.CurrentMode == FormViewMode.Insert)
        {
            if (this.insert_type_folder)
            {
                ((Literal)sender).Text = base.GetStringBaseResource("tsGroup");
            }
            else { ((Literal)sender).Text = base.GetStringBaseResource("tsAccount"); }
        }
    }
    /// <summary>
    /// Должность
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tbPost_Load(object sender, EventArgs e)
    {
        if (fvUserGroup.CurrentMode == FormViewMode.Insert)
        {
            ((TextBox)sender).Enabled = !this.insert_type_folder;
        }
    }
    /// <summary>
    /// Фамилия
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tbSurname_Load(object sender, EventArgs e)
    {
        if (fvUserGroup.CurrentMode == FormViewMode.Insert)
        {
            ((TextBox)sender).Enabled = !this.insert_type_folder;
        }
    }
    /// <summary>
    /// Имя
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tbName_Load(object sender, EventArgs e)
    {
        if (fvUserGroup.CurrentMode == FormViewMode.Insert)
        {
            ((TextBox)sender).Enabled = !this.insert_type_folder;
        }
    }
    /// <summary>
    /// Отчество
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tbPatronymic_Load(object sender, EventArgs e)
    {
        if (fvUserGroup.CurrentMode == FormViewMode.Insert)
        {
            ((TextBox)sender).Enabled = !this.insert_type_folder;
        }
    }
    /// <summary>
    /// Отчество
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tbEmail_Load(object sender, EventArgs e)
    {
        if (fvUserGroup.CurrentMode == FormViewMode.Insert)
        {
            ((TextBox)sender).Enabled = !this.insert_type_folder;
        }
    }
    /// <summary>
    /// Отправка
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cbDistribution_Load(object sender, EventArgs e)
    {
        if (fvUserGroup.CurrentMode == FormViewMode.Insert)
        {
            ((CheckBox)sender).Enabled = !this.insert_type_folder;
        }
    }
    /// <summary>
    /// Проверка введенного пользователя
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void revUser_Load(object sender, EventArgs e)
    {
        //((RegularExpressionValidator)sender).Enabled = !this.edit_type_folder;
    }
    /// <summary>
    /// Проверка введенного пользователя
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void revInsUser_Load(object sender, EventArgs e)
    {
        ((RegularExpressionValidator)sender).Enabled = !this.type_folder;
    }
    /// <summary>
    /// Проверка введенного Email
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void revEMail_Load(object sender, EventArgs e)
    {
        //((RegularExpressionValidator)sender).Enabled = !this.edit_type_folder;
    }
    /// <summary>
    /// Проверка введенного Email
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void revInsEMail_Load(object sender, EventArgs e)
    {
        ((RegularExpressionValidator)sender).Enabled = !this.type_folder;
    }
    #endregion

    #region Методы работы с источником данных
    /// <summary>
    /// Настройка перед выборкой данных
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsUserGroup_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["IDUser"] = this.iduser;
        if (DataSelecting != null) DataSelecting(this, e);
    }
    /// <summary>
    /// Данные выбранны
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsUserGroup_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataSelected != null) DataSelected(this, e);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsUserGroup_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        if (this.type_folder)
        {
            e.InputParameters["IDWeb"] = this.save_idweb;
            e.InputParameters["UserEnterprise"] = this.save_userenterprise;
            e.InputParameters["Description"] = this.save_description;
            e.InputParameters["IDSection"] = this.save_idsection;
            e.InputParameters["Post"] = null;
            e.InputParameters["Surname"] = null;
            e.InputParameters["Name"] = null;
            e.InputParameters["Patronymic"] = null;
            e.InputParameters["Email"] = null;
            e.InputParameters["bDistribution"] = false;
        }
        else
        {
            e.InputParameters["IDWeb"] = null;
            e.InputParameters["UserEnterprise"] = this.save_userenterprise;
            e.InputParameters["Description"] = this.save_description;
            e.InputParameters["IDSection"] = this.save_idsection;
            e.InputParameters["Post"] = this.save_post;
            e.InputParameters["Surname"] = this.save_surname;
            e.InputParameters["Name"] = this.save_name;
            e.InputParameters["Patronymic"] = this.save_patronymic;
            e.InputParameters["Email"] = this.save_email;
            e.InputParameters["bDistribution"] = this.save_bdistribution;
        }
        if (DataInserting != null) DataInserting(this, e);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsUserGroup_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataInserted != null) DataInserted(this, e);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsUserGroup_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        if (this.type_folder)
        {
            e.InputParameters["IDUser"] = this.iduser;
            e.InputParameters["IDWeb"] = this.save_idweb;
            e.InputParameters["UserEnterprise"] = this.save_userenterprise;
            e.InputParameters["Description"] = this.save_description;
            e.InputParameters["IDSection"] = this.save_idsection;
            e.InputParameters["Post"] = null;
            e.InputParameters["Surname"] = null;
            e.InputParameters["Name"] = null;
            e.InputParameters["Patronymic"] = null;
            e.InputParameters["Email"] = null;
            e.InputParameters["bDistribution"] = false;
        }
        else
        {
            e.InputParameters["IDUser"] = this.iduser;
            e.InputParameters["IDWeb"] = null;
            e.InputParameters["UserEnterprise"] = this.save_userenterprise;
            e.InputParameters["Description"] = this.save_description;
            e.InputParameters["IDSection"] = this.save_idsection;
            e.InputParameters["Post"] = this.save_post;
            e.InputParameters["Surname"] = this.save_surname;
            e.InputParameters["Name"] = this.save_name;
            e.InputParameters["Patronymic"] = this.save_patronymic;
            e.InputParameters["Email"] = this.save_email;
            e.InputParameters["bDistribution"] = this.save_bdistribution;
        }
        if (DataUpdating != null) DataUpdating(this, e);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsUserGroup_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataUpdated != null) DataUpdated(this, e);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsUserGroup_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["IDUser"] = this.iduser;
        if (DataDeleting != null) DataDeleting(this, e);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsUserGroup_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataDeleted != null) DataDeleted(this, e);
    }
    /// <summary>
    /// Нажата Cancel
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

    #endregion


}