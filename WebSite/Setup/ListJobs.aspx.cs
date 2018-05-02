using Hangfire;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBase;

public partial class WebSite_Setup_ListJobs : BaseAccessPages
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ

    private classHangfireJobsDB chfjdb = new classHangfireJobsDB();
    private lvComponent lvc = new lvComponent();
    private fmComponent fmc = new fmComponent();
    
    protected JobEntity job
    {
        get
        {
            if (ViewState["IDJob"] == null) { return null; }
            return new JobEntity() { 
                IDJob = (int)ViewState["IDJob"],
                Description =(string)ViewState["Description"],
                Metod =(string)ViewState["Metod"],
                Enable = (bool)ViewState["Enable"],
                Start = ViewState["Start"] != null ? ViewState["Start"] as DateTime? : null,
                Stop = ViewState["Stop"] != null ? ViewState["Stop"] as DateTime? : null,                
                Cron =(string)ViewState["Cron"],
                DistributionList =(string)ViewState["DistributionList"]
            };

        }
        set {
            ViewState["IDJob"] = value.IDJob;
            ViewState["Description"] = value.Description;
            ViewState["Metod"] = value.Metod;
            ViewState["Enable"] = value.Enable;
            ViewState["Start"] = value.Start;
            ViewState["Stop"] = value.Stop;
            ViewState["Cron"] = value.Cron;
            ViewState["DistributionList"] = value.DistributionList;
        }
    }

    #endregion

    #region МЕТОДЫ

    #region Методы загрузки компонентов
    /// <summary>
    /// загрузить компонент DropDownList
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlMetod_DataBound(object sender, EventArgs e)
    {
        ((DropDownList)sender).Items.Clear();
        MethodInfo[] methods = typeof(classHangfireJobs).GetMethods();
        foreach (MethodInfo info in methods)
        {
            if (info.ReturnType.Name.ToString() == "Void")
            {
                if (info.Name != "StartJob")
                {
                    ((DropDownList)sender).Items.Add(new ListItem(info.Name, info.Name));
                }
            }
        }
        if (job != null) { ((DropDownList)sender).SelectedValue = job.Metod; }

    }
    #endregion

    #region Методы отображения компонентов
    /// <summary>
    /// Метод активирует кнопки управления
    /// </summary>
    private void activeUpr() 
    { 
        if (lvlistjobs.SelectedIndex != -1)
        {
            tabControl.VisibleUpdate = true;
            tabControl.VisibleDelete = true;
            imbRun.Visible = true;
            imbStop.Visible = true;
            imbSetSetup.Visible = true;
        }
        else
        {
            tabControl.VisibleUpdate = false;
            tabControl.VisibleDelete = false;
            imbRun.Visible = false;
            imbStop.Visible = false;
            imbSetSetup.Visible = false;
        }    
    }
    /// <summary>
    /// Отображение выбора
    /// </summary>
    /// <param name="dataItem"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    protected string GetSRCCheckBox(object dataItem, string field)
    {
        if (bool.Parse(DataBinder.Eval(dataItem, field).ToString()))
        { return "~/image/wuc/True.png"; }
        else { return "~/image/wuc/False.png"; }
    }
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
            ((WebSite_Setup_Setup)Master).SetHeader(base.GetStringBaseResource("titleMenuAdmin"), base.GetStringBaseResource("menuListJobs"));
            mvlistjobs.ActiveViewIndex = 0;
        }
        activeUpr();

    }
    #endregion

    #region Методы обработки действий пользователя
    /// <summary>
    /// Выбрана задача
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvlistjobs_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvlistjobs.EditIndex = -1;
        activeUpr();
        job = chfjdb.GetSetupJob((int)lvlistjobs.SelectedDataKey.Value);
    }
    /// <summary>
    /// Нажато редактировать
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tabControl_UpdateClick(object sender, EventArgs e)
    {
        lvlistjobs.SetEditItem(lvlistjobs.SelectedIndex);
    }
    /// <summary>
    /// Добавить задачу
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tabControl_InsertClick(object sender, EventArgs e)
    {
        lvlistjobs.EditIndex = -1;
        lvlistjobs.SelectedIndex = -1;
        activeUpr();
        fvaddjob.ChangeMode(FormViewMode.Insert);
        fvaddjob.DataBind();
    }
    /// <summary>
    /// Удалить задачу
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tabControl_DeleteClick(object sender, EventArgs e)
    {
        odsListJobs.Delete();
    }
    /// <summary>
    /// Активировать\деактивировать
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imbStop_Click(object sender, ImageClickEventArgs e)
    {
        if (chfjdb.EnableHangFireJob((int)lvlistjobs.SelectedDataKey.Value, !job.Enable, true) > 0) 
        {
            job = chfjdb.GetSetupJob((int)lvlistjobs.SelectedDataKey.Value);
        }
        lvlistjobs.DataBind();
    }
    /// <summary>
    /// Запустить задачу вручную
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imbRun_Click(object sender, ImageClickEventArgs e)
    {
        RecurringJob.Trigger("task-"+job.Metod.ToString());
    }
    /// <summary>
    /// Обновить настройки фоновых задач
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imbSetSetup_Click(object sender, ImageClickEventArgs e)
    {
        SetSetup();
    }
    #endregion

    #region Методы работы с источником данных
    /// <summary>
    /// Обновить фоновую задачу
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsListJobs_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["IDJob"] = (int)lvlistjobs.SelectedDataKey.Value;
        e.InputParameters["Metod"] = lvc.GetValueDropDownList(lvlistjobs, lvlistjobs.EditIndex, "ddlMetod");
        e.InputParameters["Description"] = lvc.GetTextTextBox(lvlistjobs, lvlistjobs.EditIndex, "tbDescription");
        e.InputParameters["Cron"] = lvc.GetTextTextBox(lvlistjobs, lvlistjobs.EditIndex, "tbCron");
        e.InputParameters["DistributionList"] = lvc.GetTextTextBox(lvlistjobs, lvlistjobs.EditIndex, "tbDistributionList");

    }
    /// <summary>
    /// Фоновая задача обновлена
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsListJobs_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if ((int)e.ReturnValue > 0) 
        { 
        
        }
    }
    /// <summary>
    /// Добавить задачу
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsAllJob_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["Metod"] = fmc.GetValueDropDownList(fvaddjob,"ddlMetod");
        e.InputParameters["Enable"] = fmc.GetCheckedCheckBox(fvaddjob, "cbEnable");
        e.InputParameters["Description"] = fmc.GetTextTextBox(fvaddjob,"tbDescription");
        e.InputParameters["Cron"] = fmc.GetTextTextBox(fvaddjob,"tbCron");
        e.InputParameters["DistributionList"] = fmc.GetTextTextBox(fvaddjob, "tbDistributionList");
    }
    /// <summary>
    /// Задача добавлена
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsAllJob_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if ((int)e.ReturnValue > 0)
        {

        }
        lvlistjobs.DataBind();
    }
    /// <summary>
    /// Удалить задачу
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsListJobs_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["IDJob"] = (int)lvlistjobs.SelectedDataKey.Value;
    }
    /// <summary>
    /// Задача удалена
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsListJobs_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if ((int)e.ReturnValue > 0)
        {

        }
        lvlistjobs.DataBind();
        
    }
    #endregion

    private void SetSetup() 
    {
        //classHangfireJobs chfj = new classHangfireJobs();
        //foreach (JobEntity je in chfj.ListJob)
        //{
        //    if ((je.Cron != null) & (je.Enable))
        //    {
        //        RecurringJob.AddOrUpdate("task-" + je.Metod.ToString(), () => chfj.StartJob(je), je.Cron.ToString());
        //        chfjdb.SaveStatus(je.IDJob, "Job изменен");
        //    }
        //}
    }

    #endregion



}