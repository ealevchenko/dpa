using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBase;

public partial class WebSite_Setup_AccessRequests : BaseAccessPages
{
    protected classAccessUsers caccessusers = new classAccessUsers();
    protected classWeb cweb = new classWeb();
    
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ

    protected int IDAccessUsers
    {
        get
        {
            return (int)ViewState["IDAccessUsers"];
        }
        set { ViewState["IDAccessUsers"] = value; }
    }
    /// <summary>
    ///  Иницилизация объектов
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (HttpContext.Current.Request.QueryString["ID"] != null)
        {
            ViewState["IDAccessUsers"] = int.Parse(HttpContext.Current.Request.QueryString["ID"]);
        }
        else ViewState["IDAccessUsers"] = 0;// 18;
    }
    #endregion

    #region Методы загрузки компонентов
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataItem"></param>
    /// <returns></returns>
    public bool EnableButtonClose(object dataItem)
    {
        if (DataBinder.Eval(dataItem, "DateAccess") != DBNull.Value)
        {
            return false;
        }
        else return true;
    }

    public string GetSolutionItem(object dataItem)
    {
        if (DataBinder.Eval(dataItem, "IDAccessWebUsers") != null)
        {
            AccessWebUsersEntity awue = caccessusers.GetAccessWebUsers(int.Parse(DataBinder.Eval(dataItem, "IDAccessWebUsers").ToString()));
            if (awue != null)
            {
                if (awue.Solution != null)
                {
                    if ((bool)awue.Solution) { return  "Yes" ; } else { return  "No" ; }
                }
                else
                {
                    if (awue.DateRequest != null)
                    {
                        return  "Owner" ;
                    }
                }
            }
        }
        return "Null";
    }
    /// <summary>
    /// Загрузка страницы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ((WebSite_Setup_Setup)Master).SetHeader(caccessusers.GetStringResource("titleMenuAdmin"), caccessusers.GetStringResource("menuAccessRequests"));
            if (this.IDAccessUsers > 0) 
            { 

            }
        }
    }
    #endregion

    protected void btClose_Click(object sender, EventArgs e)
    {
        caccessusers.CloseAccessUsers((int)lvlistRequests.SelectedDataKey.Value);
        lvlistRequests.DataBind();
    }
}