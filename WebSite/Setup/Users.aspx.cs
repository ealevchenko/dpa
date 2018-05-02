using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBase;

public partial class WebSite_Setup_Users : BaseUsersPages
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ

    protected int mvIndex { set; get; }
    protected bool mvAddDel 
    {
        get
        {
            if (ViewState["mvAddDel"] == null) { ViewState["mvAddDel"] = false; }
            return (bool)ViewState["mvAddDel"];
        }
        set { ViewState["mvAddDel"] = value; }
    }
    #endregion

    #region МЕТОДЫ

    #region Методы отображения информации страницы
    /// <summary>
    /// Обновление данных построчно
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvUsers_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.DataItemIndex == lvUsers.SelectedIndex) 
        {
            if (e.Item.FindControl("mvDetali") != null) 
            {
                MultiView mv = (MultiView)e.Item.FindControl("mvDetali");
                if (mv != null) 
                {
                    mv.ActiveViewIndex = this.mvIndex;
                }
            }
        }
    }
    /// <summary>
    /// Загрузка
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ((WebSite_Setup_Setup)Master).SetHeader(cusers.GetStringResource("titleMenuAdmin"), cusers.GetStringResource("menuUsers"));
        }
        pnAddUser.Change = base.Change;
        //InsertUserGroup.DataBind();
    }
    #endregion

    #region Методы отработки действий пользователей
    /// <summary>
    /// Добавить новый Аккаутн
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnAddUser_InsertClick(object sender, EventArgs e)
    {
        lvUsers.EditIndex = -1;
        lvUsers.SelectedIndex = -1;
        InsertUserGroup.ModeTable = FormViewMode.Insert;
        //InsertUserGroup.Change = this.Change;
        InsertUserGroup.DataBind();
    }
    /// <summary>
    /// Нажата кнопка закрыть
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibClose_Click(object sender, ImageClickEventArgs e)
    {
        lvUsers.SelectedIndex = -1;
    }
    /// <summary>
    /// Закрыть редактирование
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibCloseEdit_Click(object sender, ImageClickEventArgs e)
    {
        lvUsers.EditIndex = -1;
    }
    /// <summary>
    /// Переход на другую строку
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbUserEnterprise_Click(object sender, EventArgs e)
    {
        if (InsertUserGroup.CurrentMode == FormViewMode.Insert)
        {
            InsertUserGroup.ModeTable = FormViewMode.ReadOnly;
            InsertUserGroup.DataBind();
        }
        lvUsers.EditIndex = -1;
    }
    /// <summary>
    /// Добавлен пользователь
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void InsertUserGroup_DataInserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        lvUsers.DataBind();
    }
    /// <summary>
    /// Нажата отмена добавления
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void InsertUserGroup_DataCancelClick(object sender, EventArgs e)
    {

    }    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void UserGroup_DataUpdated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        lvUsers.EditIndex = -1;        
        //lvUsers.DataBind();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void UserGroup_DataCancelClick(object sender, EventArgs e)
    {
        lvUsers.EditIndex = -1;
    }
    /// <summary>
    /// Показать группы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibGroup_Click(object sender, ImageClickEventArgs e)
    {
        this.mvIndex = 0;
        lvUsers.DataBind();
    }
    /// <summary>
    /// Добавить в группу
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibAddGroup_Click(object sender, ImageClickEventArgs e)
    {
        this.mvIndex = 1;
        this.mvAddDel = false;
        lvUsers.DataBind();
    }
    /// <summary>
    /// Убрать из группы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibDelGroup_Click(object sender, ImageClickEventArgs e)
    {
        this.mvIndex = 1;
        this.mvAddDel = true;
        lvUsers.DataBind();
    }
    /// <summary>
    /// Карта сайта
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibSiteMap_Click(object sender, ImageClickEventArgs e)
    {
        this.mvIndex = 2;
        lvUsers.DataBind();
    }
    #endregion

    #region Методы работы с источником данных
    /// <summary>
    /// Событие перед выборкой
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsListUsersSelect_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["type"] = 0;
        e.InputParameters["IDWeb"] = -1;
        e.InputParameters["IDSection"] = 0;
        e.InputParameters["UserEnterprise"] = null;
        e.InputParameters["Description"] = null;
    }
    /// <summary>
    /// Инициализация панели правка пользователя
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void phUser_Init(object sender, EventArgs e)
    {
        if (lvUsers.SelectedDataKey != null)
        {
            ((PlaceHolder)sender).Controls.Clear();
            var control = Page.LoadControl(@"~/WebUserControl/Setup/controlUserGroup.ascx");
            ((controlUserGroup)control).ModeTable = FormViewMode.Edit;
            ((controlUserGroup)control).IDUser = (int)lvUsers.SelectedDataKey.Value;
            ((controlUserGroup)control).VisibleInsert = false;
            ((controlUserGroup)control).VisibleDelete = false;
            ((controlUserGroup)control).DataUpdated += UserGroup_DataUpdated;
            ((controlUserGroup)control).DataCancelClick += UserGroup_DataCancelClick;
            ((controlUserGroup)control).Change = base.Change;
            ((controlUserGroup)control).DataBind();
            ((PlaceHolder)sender).Controls.Add(control);
        }
    }
    #endregion

    #region Инициализация пользовательских элементов управления 
    /// <summary>
    /// Инициализация представления группы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void phGroup_Init(object sender, EventArgs e)
    {
        if (lvUsers.SelectedDataKey != null)
        {
            ((PlaceHolder)sender).Controls.Clear();
            var control = Page.LoadControl(@"~/WebUserControl/Setup/controlUserToGroup.ascx");
            ((controlUserToGroup)control).IDUser = (int)lvUsers.SelectedDataKey.Value;
            ((controlUserToGroup)control).DataBind();
            ((PlaceHolder)sender).Controls.Add(control);
        }
    }
    /// <summary>
    /// Инициализация панели добавить удалить
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void phAddDelGroup_Init(object sender, EventArgs e)
    {
        if (lvUsers.SelectedDataKey != null)
        {
            ((PlaceHolder)sender).Controls.Clear();
            var control = Page.LoadControl(@"~/WebUserControl/Setup/controlInsDelUserToGroup.ascx");
            ((controlInsDelUserToGroup)control).IDUser = (int)lvUsers.SelectedDataKey.Value;
            ((controlInsDelUserToGroup)control).AddDel = this.mvAddDel;
            ((controlInsDelUserToGroup)control).Change = base.Change;
            ((controlInsDelUserToGroup)control).DataBind();
            ((PlaceHolder)sender).Controls.Add(control);
        }
    }
    /// <summary>
    /// Инициализация панели доступа к карте сайта
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void phSiteMap_Init(object sender, EventArgs e)
    {
        if (lvUsers.SelectedDataKey != null)
        {
            ((PlaceHolder)sender).Controls.Clear();
            var control = Page.LoadControl(@"~/WebUserControl/Setup/controlUserGroupAccess.ascx");
            ((controlUserGroupAccess)control).IDUser = (int)lvUsers.SelectedDataKey.Value;
            ((controlUserGroupAccess)control).Change = base.Change;
            ((controlUserGroupAccess)control).DataBind();
            ((PlaceHolder)sender).Controls.Add(control);
        }
    }
    #endregion

    #endregion

}