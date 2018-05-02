using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controlInsDelUserToGroup : BaseControlUpdateListView
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ controlInsDelUserToGroup
    // ID выборки
    public int? IDUser
    {
        get;
        set;
    }
    //Режим добавить или убрать
    public bool AddDel
    {
        get;
        set;
    }

    protected int? iduser { get { return this.IDUser != null ? this.IDUser : null; } }
    protected bool adddel { get { return this.AddDel != null ? this.AddDel : false; } }

    public event Data_Selecting DataSelecting;
    public event Data_Selected DataSelected;

    public event Data_Inserted DataInserted;
    public event Data_Deleted DataDeleted;

    #endregion

    #region МЕТОДЫ controlInsDelUserToGroup
    /// <summary>
    /// Отобразить команду на кнопке 
    /// </summary>
    /// <returns></returns>
    protected string GetButtonText() 
    {
        if (this.adddel) { return cusers.GetStringResource("btDel");}
            else { return cusers.GetStringResource("btAdd");}
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

        }
    }    
    #endregion


    #region Методы работы с источником данных
    /// <summary>
    /// Событие перед запросом
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsAddDelGroup_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["IDUser"] = this.iduser;
        e.InputParameters["AddDel"] = this.adddel;
        e.InputParameters["IDWeb"]  = -1;
        e.InputParameters["IDSection"] = 0;
        e.InputParameters["UserEnterprise"] = null;
        e.InputParameters["Description"] = null;
        if (DataSelecting != null) DataSelecting(this, e);       
    }
    /// <summary>
    /// Событие после запроса
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsAddDelGroup_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataSelected != null) DataSelected(this, e);
    }
    /// <summary>
    /// Нажата кнопка прменить
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btApply_Click(object sender, EventArgs e)
    {
        int IDInsert = int.Parse(((Button)sender).ToolTip.Trim());
        if (this.adddel)
        { // Удалить
            if (cusers.GetGroup((int)this.iduser)) 
            {
                // Пользователя из группы доступа
                cusers.DeleteUserToGroup((int)this.iduser, IDInsert, true);   
            } 
            else 
            {
                // Группу доступа 
                cusers.DeleteUserToGroup(IDInsert, (int)this.iduser, true);  
            }
            if (DataDeleted != null) DataDeleted(this, new ObjectDataSourceStatusEventArgs(this,null));
        }
        else 
        { // Добавить
            if (cusers.GetGroup((int)this.iduser)) 
            {
                // Пользователя в группу доступа                
                cusers.InsertUserToGroup((int)this.iduser, IDInsert, true);   
            } 
            else 
            {
                //Группу доступа
                cusers.InsertUserToGroup(IDInsert, (int)this.iduser, true);  
            }
            if (DataInserted != null) DataInserted(this, new ObjectDataSourceStatusEventArgs(this, null));
        }
        lvAddDelGroup.DataBind();
    }
    #endregion
}