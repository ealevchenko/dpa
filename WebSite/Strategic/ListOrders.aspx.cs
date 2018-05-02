using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_Strategic_ListOrders : BaseStrategicPages
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ
    //protected lvComponent lvc = new lvComponent();
    //protected fmComponent fmc = new fmComponent();

    protected int? IDTypeOrder
    {
        set { ViewState["IDTypeOrder"] = value; }
        get
        {
            if (ViewState["IDTypeOrder"] == null) { return null; }
            else { return (int)ViewState["IDTypeOrder"]; }
        }
    }
    #endregion

    #region Методы привязки данных
    /// <summary>
    /// Привязка данных тип документа
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlListTypeOrder_DataBound(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Insert(0, new ListItem(base.GetStringBaseResource("ddlAll"), "0"));
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        // Первый запуск
        if (!IsPostBack)
        {
            ((StrategicMaster)Master).SetHeader(base.GetStringBaseResource("titleMenuProject"), base.GetStringStrategicResource("menuOrder"));
        }
        pnUprTypeOrder.Change = this.Change;
        PanelUprOrder.Change = this.Change;
        contrTypeOrder.IDTypeOrder = this.IDTypeOrder;
        // Окно документа
        if (lvListOrder.SelectedDataKey != null)
        {
            controlOrder.IDOrder = (int)lvListOrder.SelectedDataKey.Value;
            controlOrder.Change = this.Change;

        }
        // Требуется обновить компоненты
        if (base.bReloading)
        {

            ddlListTypeOrder.DataBind();
            lvListOrder.DataBind();
            base.bReloading = false;
        }
    }

    #region Методы обработки действий пользователя
    /// <summary>
    /// Добавить новый тип
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnUprTypeOrder_InsertClick(object sender, EventArgs e)
    {
        contrTypeOrder.IDTypeOrder = null;
        contrTypeOrder.ModeTable = FormViewMode.Insert;
        contrTypeOrder.Change = this.Change;
        contrTypeOrder.DataBind();
    }
    /// <summary>
    /// Изменить тип
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnUprTypeOrder_UpdateClick(object sender, EventArgs e)
    {
        this.IDTypeOrder = int.Parse(ddlListTypeOrder.SelectedValue);
        if (ddlListTypeOrder.SelectedIndex != -1)
        {
            contrTypeOrder.IDTypeOrder = this.IDTypeOrder;
            contrTypeOrder.ModeTable = FormViewMode.Edit;
            contrTypeOrder.Change = this.Change;
            contrTypeOrder.DataBind();
        }
    }
    /// <summary>
    /// Удалить тип
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnUprTypeOrder_DeleteClick(object sender, EventArgs e)
    {
        if (ddlListTypeOrder.SelectedIndex != -1)
        {
            base.corder.DeleteTypeOrder(int.Parse(ddlListTypeOrder.SelectedValue), true);
            this.IDTypeOrder = null;
            ddlListTypeOrder.DataBind();
        }

    }
    /// <summary>
    /// выбран новый тип документа
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlListTypeOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        contrTypeOrder.IDTypeOrder = null;
        contrTypeOrder.ModeTable = FormViewMode.ReadOnly;
        contrTypeOrder.DataBind();
    }
    #endregion

    #region Методы обработки controlTypeOrder
    /// <summary>
    /// Добавлен тип
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void contrTypeOrder_DataInserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.IDTypeOrder = null;
        contrTypeOrder.IDTypeOrder = null;
        contrTypeOrder.DataBind();
        ddlListTypeOrder.DataBind();
    }
    /// <summary>
    /// Изменен тип
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void contrTypeOrder_DataUpdated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.IDTypeOrder = null;
        contrTypeOrder.IDTypeOrder = null;
        contrTypeOrder.DataBind();
        ddlListTypeOrder.DataBind();
    }
    /// <summary>
    /// Отмена редактирования типа
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void contrTypeOrder_DataCancelClick(object sender, EventArgs e)
    {
        this.IDTypeOrder = null;
        contrTypeOrder.IDTypeOrder = null;
        contrTypeOrder.DataBind();
    }
    #endregion

    /// <summary>
    /// Добавить новый документ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void PanelUprOrder_InsertClick(object sender, EventArgs e)
    {
        controlOrder.IDOrder = null;
        controlOrder.Change = this.Change;
        controlOrder.ModeTable = FormViewMode.Insert;
        controlOrder.DataBind();
    }
    /// <summary>
    /// Выбран документ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvListOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.Change)
        {
            controlOrder.IDOrder = (int)lvListOrder.SelectedDataKey.Value;
            controlOrder.Change = this.Change;
            controlOrder.DataBind();
        }
    }
    /// <summary>
    /// Сортировка перечня проектов
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        lvListOrder.SelectedIndex = -1;
        lvListOrder.EditIndex = -1;

    }
    /// <summary>
    /// Добавлен документ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlOrder_DataInserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        lvListOrder.DataBind();
    }
    /// <summary>
    /// Правка документа
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlOrder_DataUpdated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        lvListOrder.DataBind();
    }
    /// <summary>
    /// Удален документ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlOrder_DataDeleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        lvListOrder.SelectedIndex = -1;
        lvListOrder.DataBind();
    }
}