using Strategic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controlStatusProject : BaseControlUpdateListView
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ controlStatusProject
    // ID выборки
    //public int? IDProject { get; set; }
    public int? IDProject
    {
        get
        {
            if (ViewState["IDProject"] == null) return null;
            return (int)ViewState["IDProject"];
        }
        set { ViewState["IDProject"] = value; }
    }
    /// <summary>
    /// Выводить информацию 
    /// </summary>
    public bool OutInfoText { get; set; }

    protected int? idproject { get { return this.IDProject != null ? this.IDProject : null; } }

    protected bool outinfo { get { return this.OutInfoText != null ? this.OutInfoText : false; } }

    // Делегат
    public delegate void Panel_Close(object sender, ImageClickEventArgs e);
    // События
    public event Data_Selecting DataSelecting;
    public event Data_Selected DataSelected;
    public event Panel_Close PanelClose;

    /// <summary>
    /// Текущий пользователь
    /// </summary>
    protected MenagerProjectEntity currentMenager { get; set; }
    #endregion

    protected void OutView()
    {
        controlProject.IDProject = this.idproject;
        controlProject.Change = false;
        controlDetaliProject.IDProject = this.idproject;
        controlDetaliProject.Change = false;        
        this.currentMenager = base.cmenager.GetMenagerProjectToUser((int)base.cmenager.GetIDUser(HttpContext.Current.User.Identity.Name)); // Получим все данные менеджера запросившего эту страницу
        if ((this.currentMenager != null) & (this.idproject != null))
        {
            if ((this.currentMenager.SuperMenager) | (base.cproject.BelongsMenegerToProject((int)this.idproject, (int)this.currentMenager.ID)))
            {
                controlProject.IDProject = this.idproject;
                controlProject.Change = true;
                controlDetaliProject.IDProject = this.idproject;
                controlDetaliProject.Change = true;
            }
        }
        //controlProject.DataBind();
        //controlDetaliProject.DataBind();
    }

    public override void DataBind()
    {
        base.OnDataBinding(EventArgs.Empty);
        OutView();
        controlProject.DataBind();
        controlDetaliProject.DataBind();
    }

    #region Методы загрузки компонентов и страницы выборка данных
    /// <summary>
    /// Загрузить страницу
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //lvTable.DataBind();
        }
        OutView();

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
        e.InputParameters["IDProject"] = this.idproject;
        if (DataSelecting != null) DataSelecting(this, e);
    }
    /// <summary>
    /// Выбран
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataSelected != null) DataSelected(this, e);
    }
    #endregion

    /// <summary>
    /// Вернутся к списку
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibReturnList_Click(object sender, ImageClickEventArgs e)
    {
        this.IDProject = null;
        if (PanelClose != null) PanelClose(this, e);
    }
}