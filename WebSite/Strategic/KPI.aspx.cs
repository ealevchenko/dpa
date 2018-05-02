using Strategic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_Strategic_KPI : BaseStrategicPages
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ
    protected lvComponent lvc = new lvComponent();
    protected fmComponent fmc = new fmComponent();

    protected int? IDProject 
    {
        set { ViewState["IDProject"] = value; }
        get
        {
            if (ViewState["IDProject"] == null) { return null; }
            else { return (int)ViewState["IDProject"]; }
        }
    }

    protected int? IDKPI 
    {
        set { ViewState["IDKPI"] = value; }
        get
        {
            if (ViewState["IDKPI"] == null) { return null; }
            else { return (int)ViewState["IDKPI"]; }
        }
    }
    protected int save_indexkpi
    {
        get
        {
            return (int)lvc.GetValueDropDownList(lvKPIProject, lvKPIProject.EditIndex, "ddlIndexKPI", -1);
        }
    }    
    protected decimal? save_q1
    {
        get
        {
            string res = lvc.GetTextTextBox(lvKPIProject, lvKPIProject.EditIndex, "tbQ1");
            if ( res!= null)
            {
                return decimal.Parse(res);
            }
            return null;
        }
    }
    protected decimal? save_q2
    {
        get
        {
            string res = lvc.GetTextTextBox(lvKPIProject, lvKPIProject.EditIndex, "tbQ2");
            if ( res!= null)
            {
                return decimal.Parse(res);
            }
            return null;
        }
    }    
    protected decimal? save_q3
    {
        get
        {
            string res = lvc.GetTextTextBox(lvKPIProject, lvKPIProject.EditIndex, "tbQ3");
            if ( res!= null)
            {
                return decimal.Parse(res);
            }
            return null;
        }
    }
    protected decimal? save_q4
    {
        get
        {
            string res = lvc.GetTextTextBox(lvKPIProject, lvKPIProject.EditIndex, "tbQ4");
            if ( res!= null)
            {
                return decimal.Parse(res);
            }
            return null;
        }
    }        
    protected decimal save_roi
    {
        get
        {
            string res = lvc.GetTextTextBox(lvKPIProject, lvKPIProject.EditIndex, "tbROI");
            if ( res!= null)
            {
                return decimal.Parse(res);
            }
            return 0;
        }
    }        
    protected decimal save_npv
    {
        get
        {
            string res = lvc.GetTextTextBox(lvKPIProject, lvKPIProject.EditIndex, "tbNPV");
            if ( res!= null)
            {
                return decimal.Parse(res);
            }
            return 0;
        }
    }        

    protected int insert_idproject
    {
        get
        {
            return (int)fmc.GetValueDropDownList(fvInsertKPIProject, "ddlProject", -1);
        }
    }    
    protected int insert_idkpi
    {
        get
        {
            return (int)fmc.GetValueDropDownList(fvInsertKPIProject, "ddlKPI", -1);
        }
    }  
    protected int insert_indexkpi
    {
        get
        {
            return (int)fmc.GetValueDropDownList(fvInsertKPIProject, "ddlIndexKPI", -1);
        }
    } 
    protected int insert_yearstart
    {
        get
        {
            return (int)fmc.GetValueDropDownList(fvInsertKPIProject, "ddlYearStart", -1);
        }
    } 
    protected int insert_yearstop
    {
        get
        {
            return (int)fmc.GetValueDropDownList(fvInsertKPIProject, "ddlYearStop", -1);
        }
    }
    #endregion

    #region Методы привязки данных
    /// <summary>
    /// Привязка данных ddlKPI
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlKPI_DataBound(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Insert(0, new ListItem(base.GetStringBaseResource("ddlAll"),"0"));
    }
    /// <summary>
    /// Привязка данных ddlProject
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlProject_DataBound(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Insert(0, new ListItem(base.GetStringBaseResource("ddlAll"),"0"));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlYear_DataBound(object sender, EventArgs e)
    {
        YearEntity ye = base.ckpi.GetYearKPI();
        int index;
        int max;
        if (ye != null)
        {
            index = ye.Min;
            max = ye.Max;
        } else 
        {
            index = DateTime.Now.Year;
            max = DateTime.Now.Year;            
        }
        while (index <= max)
        {
            ((DropDownList)sender).Items.Add(new ListItem(index.ToString(), index.ToString()));
            index++;
        }
        ((DropDownList)sender).SelectedValue =  DateTime.Now.Year.ToString();
    }
    /// <summary>
    /// Привязка данных год начала
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlYearStart_DataBound(object sender, EventArgs e)
    {
        int index = DateTime.Now.Year - 5;
        while (index <= DateTime.Now.Year + 10)
        {
            ((DropDownList)sender).Items.Add(new ListItem(index.ToString(), index.ToString()));
            index++;
        }
        ((DropDownList)sender).SelectedValue = DateTime.Now.Year.ToString();
    }
    /// <summary>
    /// Привязка данных год конец
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlYearStop_DataBound(object sender, EventArgs e)
    {
        int index = DateTime.Now.Year - 5;
        while (index <= DateTime.Now.Year + 10)
        {
            ((DropDownList)sender).Items.Add(new ListItem(index.ToString(), index.ToString()));
            index++;
        }
        ((DropDownList)sender).SelectedValue = (DateTime.Now.Year + 10).ToString();
    }
    /// <summary>
    /// Привязка данных index
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlIndexKPI_DataBound(object sender, EventArgs e)
    {

    }
    #endregion

    #region Методы загрузки компонентов
    /// <summary>
    /// Загрузка страницы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Первый запуск
        if (!IsPostBack)
        {
            ((StrategicMaster)Master).SetHeader(base.GetStringBaseResource("titleMenuProject"), base.GetStringStrategicResource("menuProjectKPI"));
        }
        pnSelect.Change = base.Change;
        PanelUprKPIProject.Change = base.Change;
        //Если есть выбранный сайт привязать к компоненту
        //if ((ddlKPI.SelectedIndex != -1) & (controlKPI.ModeTable == FormViewMode.Edit))
        //{
            controlKPI.IDKPI = this.IDKPI;
        //    controlKPI.Caption = "Обновить KPI";
        //    controlKPI.ModeTable = FormViewMode.Edit;
        //    controlKPI.Change = this.Change;
        //    controlKPI.DataBind();
        //}
        //if (lvListKPI.SelectedDataKey != null)
        //{
        //    controlKPI.IDKPI = (int)lvListKPI.SelectedDataKey.Value;
        //    controlKPI.Change = this.Change;
        //    pnSelectProject.Change = base.Change;
        //}
        //else { pnSelectProject.Change = false; }

        // Требуется обновить компоненты
        if (base.bReloading)
        {
            //controlSite.ReloadingControl();
            lvKPIProject.DataBind();
            base.bReloading = false;
        }
    }
    #endregion

    #region Методы обработки действий пользователя
    /// <summary>
    /// Добавить KPI
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelect_InsertClick(object sender, EventArgs e)
    {
        controlKPI.IDKPI = null;
        controlKPI.Caption = base.GetStringStrategicResource("insertKPI");
        controlKPI.ModeTable = FormViewMode.Insert;
        controlKPI.Change = this.Change;
        controlKPI.DataBind();
    }
    /// <summary>
    /// Обновить KPI
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelect_UpdateClick(object sender, EventArgs e)
    {
        this.IDKPI = int.Parse(ddlKPI.SelectedValue);
        if (ddlKPI.SelectedIndex != -1)
        {
            controlKPI.IDKPI = this.IDKPI;
            controlKPI.Caption = base.GetStringStrategicResource("updateKPI");
            controlKPI.ModeTable = FormViewMode.Edit;
            controlKPI.Change = this.Change;
            controlKPI.DataBind();
        }
    }
    /// <summary>
    /// Удалить KPI
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelect_DeleteClick(object sender, EventArgs e)
    {
        if (ddlKPI.SelectedIndex != -1)
        {
            base.ckpi.DeleteKPI(int.Parse(ddlKPI.SelectedValue), true);
        }
        ddlKPI.DataBind();
    }
    /// <summary>
    /// Выбран KPI
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlKPI_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvKPIProject.EditIndex = -1;        
        lvKPIProject.SelectedIndex = -1;
        this.IDProject = null;
        controlKPI.IDKPI = null;
        controlKPI.ModeTable = FormViewMode.ReadOnly;
        controlKPI.DataBind();
    }
    /// <summary>
    /// Выбран проект
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvKPIProject.SelectedIndex = -1;
        lvKPIProject.EditIndex = -1;
        this.IDProject = null;
    }
    /// <summary>
    /// Выбран год
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvKPIProject.SelectedIndex = -1;
        lvKPIProject.EditIndex = -1;
        this.IDProject = null;
    }
    /// <summary>
    /// Сортировка перечня проектов
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        lvKPIProject.SelectedIndex = -1;
        lvKPIProject.EditIndex = -1;
        this.IDProject = null;
    }
    /// <summary>
    /// Выбран новый KPI проекта
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvKPIProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvKPIProject.EditIndex = -1;
        this.IDProject = null;
    }
    /// <summary>
    /// Показать или убрать информацию по проекту
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbProject_Click(object sender, EventArgs e)
    {
        controlProject cp = (controlProject)lvKPIProject.Items[lvKPIProject.SelectedIndex].FindControl("controlProject");
        if (cp != null)
        {
            KPIProjectEntity kpiproject = base.ckpi.GetKPIProject((int)lvKPIProject.SelectedDataKey.Value);
            if (this.IDProject != null)
            { this.IDProject = null; }
            else
            { this.IDProject = kpiproject.IDProject; }
            cp.IDProject = this.IDProject;
            cp.DataBind();
        }
    }
    /// <summary>
    /// Добавить KPI проета
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void PanelUprKPIProject_InsertClick(object sender, EventArgs e)
    {
        fvInsertKPIProject.ChangeMode(FormViewMode.Insert);
        //fvInsertKPIProject.DataBind();
    }

    #endregion

    #region Методы обработки данных
    /// <summary>
    /// KPI - Добавлен 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlKPI_DataInserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.IDKPI = null;
        ddlKPI.DataBind();
    }
    /// <summary>
    /// KPI - Обновлен
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlKPI_DataUpdated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ddlKPI.DataBind();
        this.IDKPI = null;
        //lvListKPI.DataBind();
    }
    /// <summary>
    /// KPI - Удален 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void controlKPI_DataDeleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        //lvListKPI.SelectedIndex = -1;
        this.IDKPI = null;
        controlKPI.IDKPI = null;
        controlKPI.DataBind();
    }
    /// <summary>
    /// Добавить kpi проекта
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsKPIProject_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["IDProject"] = this.insert_idproject;
        e.InputParameters["IDKPI"] = this.insert_idkpi;
        e.InputParameters["IndexKPI"] = this.insert_indexkpi;
        e.InputParameters["YearStart"] = this.insert_yearstart;
        e.InputParameters["YearStop"] = this.insert_yearstop;
        e.InputParameters["OutResult"] = true;
    }
    /// <summary>
    /// Добавлен новый kpi проекта
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsKPIProject_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ddlYear.DataBind();
    }
    /// <summary>
    /// Обновить показателе KPI
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsKPIProject_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        KPIYearEntity kpiyear = base.ckpi.GetKPIYear((int)lvKPIProject.SelectedDataKey.Value);
        e.InputParameters["IDKPIYear"] = (int)lvKPIProject.SelectedDataKey.Value;
        e.InputParameters["IDKPIProject"] = kpiyear.IDKPIProject;
        e.InputParameters["IndexKPI"] = this.save_indexkpi;
        e.InputParameters["Q1"] = this.save_q1;
        e.InputParameters["Q2"] = this.save_q2;
        e.InputParameters["Q3"] = this.save_q3;
        e.InputParameters["Q4"] = this.save_q4;
        e.InputParameters["ROI"] = this.save_roi;
        e.InputParameters["NPV"] = this.save_npv;
        e.InputParameters["OutResult"] = true;
    }
    /// <summary>
    /// Удалить KPI проекта
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsKPIProject_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["IDKPIProject"] = null;
        e.InputParameters["IDKPIYear"] = (int)lvKPIProject.SelectedDataKey.Value;
        e.InputParameters["OutResult"] = true;
    }
    /// <summary>
    /// KPI проекта удален
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsKPIProject_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ddlYear.DataBind();
        lvKPIProject.DataBind();
    }
    #endregion
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnKPIProject_Init(object sender, EventArgs e)
    {
        ((panelUpr)sender).Change = this.Change;
    }
}