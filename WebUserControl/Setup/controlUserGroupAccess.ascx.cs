using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBase;

public partial class controlUserGroupAccess : BaseControlUpdateListView
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ controlUserGroupAccess
    // ID выборки
    public int? IDUser
    {
        get;
        set;
    }
    // Индекс редактируемой строки
    protected int indexEdit
    {
        get
        {
            if (ViewState["indexEdit"] == null)
            { ViewState["indexEdit"] = -1; }
            return (int)ViewState["indexEdit"];
        }
        set
        {
            ViewState["indexEdit"] = value;
        }
    }
    // Получить ID пользователя
    protected int? iduser { get { return this.IDUser != null ? this.IDUser : null; } }
    // Получить отредактированное правило
    protected string Rules
    {
        get
        {
            if (lvAccessSiteMap.Items[lvAccessSiteMap.EditIndex].FindControl("ControlRules") != null)
            {
                controlRules ca = (controlRules)lvAccessSiteMap.Items[lvAccessSiteMap.EditIndex].FindControl("ControlRules");
                return ca.stringRules;
            }
            else return null;
        }
    }
    // Получить выбранный доступ
    protected int Access
    {
        get
        {
            return base.lvc.GetValueDropDownList(lvAccessSiteMap.Items[lvAccessSiteMap.EditIndex], "ddlAccess");
        }
    }

    // События
    public event Data_Selecting DataSelecting;
    public event Data_Selected DataSelected;
    public event Data_Updating DataUpdating;
    public event Data_Updated DataUpdated;

    #endregion

    #region МЕТОДЫ controlUserGroupAccess

    #region Методы привязки данных
    /// <summary>
    /// Построчная привязка данных
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvAccessSiteMap_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (lvAccessSiteMap.EditIndex == e.Item.DataItemIndex)
        {
            if (e.Item.FindControl("ddlAccess") != null) 
            {
                DropDownList ddl = (DropDownList)e.Item.FindControl("ddlAccess");
                if (ddl != null) 
                {
                    ddl.Items.Clear();
                    foreach (Access tr in Enum.GetValues(typeof(Access)))
                    {
                        ddl.Items.Add(new ListItem(tr.ToString(), ((int)tr).ToString()));
                    }
                    int key = (int)lvAccessSiteMap.SelectedDataKey.Value;
                    Access access = casm.GetAccess(key);
                    ddl.SelectedValue = ((int)access).ToString();
                }
            }
            if (e.Item.FindControl("ControlRules") != null)
            {
                int key = (int)lvAccessSiteMap.SelectedDataKey.Value;

                controlRules crules = (controlRules)e.Item.FindControl("ControlRules");
                if ((this.indexEdit != e.Item.DataItemIndex))
                {
                    crules.stringRules = casm.GetAccessRules(key);
                    this.indexEdit = e.Item.DataItemIndex;
                }
                else crules.DataBind();

            }
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
        lvAccessSiteMap.DataBind();
    }
    #endregion

    #region Методы работы с источником данных
    /// <summary>
    /// Событие перед выборкой
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsUserGroupAccess_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["IDUser"] = this.iduser;
        if (DataSelecting != null) DataSelecting(this, e);
    }
    /// <summary>
    /// Событие после выборки
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsUserGroupAccess_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataSelected != null) DataSelected(this, e);
    }
    /// <summary>
    /// Событие перед обновлением
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsUserGroupAccess_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["IDAccessSiteMap"] = (int)lvAccessSiteMap.SelectedDataKey.Value;
        e.InputParameters["Access"] = this.Access;
        e.InputParameters["AccessRules"] = this.Rules;
        if (DataUpdating != null) DataUpdating(this, e);
    }
    /// <summary>
    /// Событие после обновления
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsUserGroupAccess_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataUpdated != null) DataUpdated(this, e);
    }
    #endregion

    #region Методы работы panelUpr
    /// <summary>
    /// Загрузить кнопки управления
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelect_Init(object sender, EventArgs e)
    {
        ((panelUpr)sender).Change = this.change_button; 
        ((panelUpr)sender).StyleButton = base.style_button;
        ((panelUpr)sender).VisibleInsert = false;
        ((panelUpr)sender).VisibleUpdate = this.visible_update;
        ((panelUpr)sender).VisibleDelete = false;
        ((panelUpr)sender).VisibleSave = this.visible_save;
        ((panelUpr)sender).VisibleCancel = this.visible_cancel;
    }
    #endregion

    #region Методы обработки действий пользователей
    /// <summary>
    /// Нажата отмена
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelect_CancelClick(object sender, EventArgs e)
    {
        this.indexEdit = -1;
    }
    #endregion

    #endregion
}