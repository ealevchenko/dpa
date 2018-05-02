using Strategic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controlDetaliProject : BaseControlUpdateListView
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ controlDetaliProject
    // ID выборки
    public int? IDProject { get; set; }

    /// <summary>
    /// Выводить информацию 
    /// </summary>
    public bool OutInfoText { get; set; }

    protected int? idproject { get { return this.IDProject != null ? this.IDProject : null; } }

    protected bool outinfo { get { return this.OutInfoText != null ? this.OutInfoText : false; } }

    protected DateTime? save_factstart
    {
        get
        {
            string stdate = base.lvc.GetTextTextBox(lvTable, lvTable.EditIndex, "tbFactStart");
            if (stdate != null) 
            {
                return DateTime.Parse(stdate);
            }
            return null;
        }
    }
    protected DateTime? save_factstop
    {
        get
        {
            string stdate = base.lvc.GetTextTextBox(lvTable, lvTable.EditIndex, "tbFactStop");
            if (stdate != null) 
            {
                return DateTime.Parse(stdate);
            }
            return null;
        }
    }
    protected int save_persent
    {
        get
        {
            return (int)base.lvc.GetValueDropDownList(lvTable, lvTable.EditIndex, "ddlPersent", 0);
        }
    }
    protected string save_coment
    {
        get
        {
            return base.lvc.GetTextTextBox(lvTable, lvTable.EditIndex, "tbComent");
        }
    }
    protected string save_responsible
    {
        get
        {
            return base.lvc.GetTextTextBox(lvTable, lvTable.EditIndex, "tbResponsible");
        }
    }
    
    // Делегат
    public delegate void Data_Skip(object sender, EventArgs e);
    public delegate void Data_Current(object sender, EventArgs e);

    // События
    public event Data_Selecting DataSelecting;
    public event Data_Selected DataSelected;
    public event Data_Updating DataUpdating;
    public event Data_Updated DataUpdated;
    public event Data_Skip DataSkip;
    public event Data_Current DataCurrent;

    #endregion

    #region Методы привязки данных
    /// <summary>
    /// Привязать данные к Repeater
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void fileList_DataBinding(object sender, EventArgs e)
    {
        int key = (int)lvTable.SelectedDataKey.Value;
        List<FileEntity> list_fe = base.cproject.GetFilesEntityStepDetali(key);
        ((Repeater)sender).DataSource = list_fe;
    }
    #endregion

    #region Методы загрузки компонентов и страницы выборка данных
    /// <summary>
    /// Определение текущей строки по проценту выполнения проекта (если больше 0% и меньше 100%)
    /// </summary>
    /// <param name="dataItem"></param>
    /// <returns></returns>
    public string GetCurrentString(object dataItem)
    {
        if (DataBinder.Eval(dataItem, "Persent") != DBNull.Value)
        {
            int Persent = int.Parse(DataBinder.Eval(dataItem, "Persent").ToString());
            if ((Persent > 0) & (Persent < 100)) { return "current"; }
        }
        return "item";
    }
    /// <summary>
    /// Показать выбор
    /// </summary>
    /// <param name="dataItem"></param>
    /// <returns></returns>
    public bool GetVisibleSelect(object dataItem, bool change)
    {
        if (!change) return false;
        if (DataBinder.Eval(dataItem, "SkipStep") != DBNull.Value)
        {
            return !bool.Parse(DataBinder.Eval(dataItem, "SkipStep").ToString());
        }
        return false;
    }
    /// <summary>
    /// Показать текст
    /// </summary>
    /// <param name="dataItem"></param>
    /// <returns></returns>
    public bool GetVisibleStep(object dataItem, bool change)
    {
        if (!change) return true;
        if (DataBinder.Eval(dataItem, "SkipStep") != DBNull.Value)
        {
            return bool.Parse(DataBinder.Eval(dataItem, "SkipStep").ToString());
        }
        return false;
    }
    /// <summary>
    /// Загрузить страницу
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            lvTable.DataBind();
        }
    }
    /// <summary>
    /// Отобразить панель UPR
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void upr_Load(object sender, EventArgs e)
    {
        ((Panel)sender).Visible = this.Change;
    }
    /// <summary>
    /// Выбран шаг
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvTable.EditIndex = -1;
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
        e.InputParameters["skip"] = false;
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
    /// <summary>
    /// Обновить
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["IDStepDetali"] = (int)lvTable.SelectedDataKey.Value;
        e.InputParameters["FactStart"] = this.save_factstart;
        e.InputParameters["FactStop"] = this.save_factstop;
        e.InputParameters["Persent"] = this.save_persent;
        e.InputParameters["Coment"] = this.save_coment;
        e.InputParameters["Responsible"] = this.save_responsible;
        e.InputParameters["OutResult"] = this.outinfo;
        if (DataUpdating != null) DataUpdating(this, e);
    }
    /// <summary>
    /// Обновлено
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataUpdated != null) DataUpdated(this, e);
    }
    /// <summary>
    /// Пропустить шаг
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelect_SkipClick(object sender, EventArgs e)
    {
        base.cproject.SkipStepDetaliProject((int)lvTable.SelectedDataKey.Value,true,this.outinfo);
        lvTable.SelectedIndex = -1;
        lvTable.DataBind();
        if (DataSkip != null) DataSkip(this, e);
    }
    ///// <summary>
    ///// Текущий шаг
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void pnSelect_CurrentClick(object sender, EventArgs e)
    //{
    //    base.cproject.CurrentStepDetaliProject((int)lvTable.SelectedDataKey.Value, true, this.outinfo);
    //    lvTable.SelectedIndex = -1;
    //    lvTable.DataBind();
    //    if (DataCurrent != null) DataCurrent(this, e);
    //}
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

        ((panelUpr)sender).VisibleSave = this.visible_save;
        ((panelUpr)sender).VisibleCancel = this.visible_cancel;
    }
    #endregion

    #region Методы обработки файлов
    /// <summary>
    /// Добавить файл
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSave_Click(object sender, EventArgs e)
    {
        FileUpload fu = (FileUpload)lvTable.Items[lvTable.EditIndex].FindControl("fuAdd");
        if (fu != null)
        {
            // Если файла нет, то и загружать нечего
            if (!fu.HasFile)
                return;
            int id = base.cfile.AddFile(
            new FileContent()
            {
                // Имя файла
                FileName = fu.PostedFile.FileName,
                // Тип файла
                FileContent = fu.PostedFile.ContentType,
                // Данные
                FileImage = fu.FileBytes
            }
            );
            if (id <= 0) return;
            // Записываем в шаг
            int key = (int)lvTable.SelectedDataKey.Value;
            cproject.InsertFilesStepDetali(key, id, this.outinfo);
            Repeater fl = (Repeater)lvTable.Items[lvTable.EditIndex].FindControl("fileList");
            if (fl != null)
            {
                fl.DataBind();
            }
        }
    }
    /// <summary>
    /// Удалить
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbDel_Click(object sender, EventArgs e)
    {
        decimal id  = decimal.Parse(((LinkButton)sender).ToolTip);
        int key = (int)lvTable.SelectedDataKey.Value;
        cproject.DeleteFilesStepDetali(key, id, this.outinfo);
        base.cfile.DelFile(id);
        Repeater fl = (Repeater)lvTable.Items[lvTable.EditIndex].FindControl("fileList");
        if (fl != null)
        {
            fl.DataBind();
        }
    }
    #endregion







}