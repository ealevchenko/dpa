using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controlUserToGroup : BaseControlUpdateListView
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ controlUserToGroup
    // ID выборки
    public int? IDUser
    {
        get;
        set;
    }

    protected int? iduser { get { return this.IDUser != null ? this.IDUser : null; } }

    // События
    public event Data_Selecting DataSelecting;
    public event Data_Selected DataSelected;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region МЕТОДЫ controlUserToGroup

    #region Методы работы с источником данных
    /// <summary>
    /// Событие перед выборкой
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsListGroupsUsers_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["IDUser"] = this.iduser;
        if (DataSelecting != null) DataSelecting(this, e);
    }
    /// <summary>
    /// Событие после выборки
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsListGroupsUsers_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataSelected != null) DataSelected(this, e);
    }    
    #endregion

    #endregion
}