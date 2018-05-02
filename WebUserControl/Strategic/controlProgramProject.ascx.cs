using Strategic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controlProgramProject : BaseControlUpdateListView
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ controlProgramProject
    /// <summary>
    /// ID выборки программы 
    /// </summary>
    public int? IDImplementationProgram { get; set; }
    /// <summary>
    /// Выводить информацию 
    /// </summary>
    public bool OutInfoText { get; set; }

    protected int? idimplementationprogram { get { return this.IDImplementationProgram != null ? this.IDImplementationProgram : null; } }

    protected bool outinfo { get { return this.OutInfoText != null ? this.OutInfoText : false; } }

    /// <summary>
    /// Описание помещается в оболочку специального объекта EventArgs
    /// </summary>
    public class selectProject : EventArgs
    {
        private int _IDProject;
        public int IDProject { get { return this._IDProject; } } // Возвращаем только дату
        public selectProject(int IDProject)
        {
            this._IDProject = IDProject;
        }
    }
    // Объявим делегатов
    public delegate void Date_Select(object sender, selectProject e);

    public event Data_Selecting DataSelecting;
    public event Data_Selected DataSelected;
    public event Date_Select DateSelect;
    #endregion

    #region Методы работы с источником данных
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
    /// Выборка по программе
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsSection_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["IDImplementationProgram"] = this.idimplementationprogram;
    }
    /// <summary>
    /// Прорисовка проектов
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvSection_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.FindControl("lvProject") != null)
        {
            ListView lv = (ListView)e.Item.FindControl("lvProject");
            if (lv != null)
            {
                DataRowView drw = (DataRowView)e.Item.DataItem;
                if (drw != null)
                {
                    
                    odsProject.SelectParameters["IDSection"].DefaultValue = drw.Row[0].ToString();
                    odsProject.SelectParameters["IDImplementationProgram"].DefaultValue = this.idimplementationprogram.ToString();
                    odsProject.SelectParameters["Status"].DefaultValue = "";
                    lv.DataSource = odsProject.Select();
                }
            }
        }
    }
    /// <summary>
    /// Прорисовка шагов
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvProject_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        // Покажем первое вложение
        if (e.Item.FindControl("lvStatus1") != null)
        {
            ListView lv = (ListView)e.Item.FindControl("lvStatus1");
            if (lv != null)
            {
                DataRowView drw = (DataRowView)e.Item.DataItem;
                if (drw != null)
                {
                    odsSatus.SelectParameters["IDProject"].DefaultValue = drw.Row[0].ToString();
                    odsSatus.SelectParameters["top1"].DefaultValue = "True";
                    lv.DataSource = odsSatus.Select();
                }
            }
        }
        // Покажем остальные вложения (добапвляется <tr>)
        if (e.Item.FindControl("lvStatus2") != null)
        {
            ListView lv = (ListView)e.Item.FindControl("lvStatus2");
            if (lv != null)
            {
                DataRowView drw = (DataRowView)e.Item.DataItem;
                if (drw != null)
                {
                    odsSatus.SelectParameters["IDProject"].DefaultValue = drw.Row[0].ToString();
                    odsSatus.SelectParameters["top1"].DefaultValue = "False";
                    lv.DataSource = odsSatus.Select();
                }
            }
        }
    }
    #endregion

    #region Методы загрузки компонентов и страницы выборка данных
    

    public string GetStyleItem(object dataItem)
    {
        int Status = int.Parse(DataBinder.Eval(dataItem, "Status").ToString());
        if (Status == 2) 
        { return "close"; } 
        else { return "item"; } 
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
            lvSection.DataBind();
        }
    }
    /// <summary>
    /// Отобразить депортамент
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSection_Load(object sender, EventArgs e)
    {
        ((Panel)sender).DataBind();
    }
    #endregion
    /// <summary>
    /// Показать детали
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvProject_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (DateSelect != null) DateSelect(this, new selectProject(int.Parse(e.CommandArgument.ToString())));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lvProject_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {

    }
}