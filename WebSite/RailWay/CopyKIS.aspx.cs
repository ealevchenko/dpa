using leaRailWay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_RailWay_CopyKIS : BaseRailWayAccessPages
{
    protected clOldStations old_st = new clOldStations("RailWay", "owner", "dbnameRailWay");
    protected clOLDWays old_ws = new clOLDWays("RailWay", "owner", "dbnameRailWay");
    protected clORCSostav orc_sostav = new clORCSostav("RailWay", "ownerRailWay", "dbnameRailWay");
    protected clMTList mtlist = new clMTList("RailWay", "ownerRailWay", "dbnameRailWay");
    protected clMTSostav mtSostav = new clMTSostav("RailWay", "ownerRailWay", "dbnameRailWay");
    protected clORC orc = new clORC("RailWay", "ownerRailWay", "dbnameRailWay");

    /// <summary>
    /// ID Выбранной строки 
    /// </summary>
    private int IDmtsostav
    {
        get { return (int)ViewState["IDmtsostav"]; }
        set { ViewState["IDmtsostav"] = value; }
    }
    /// <summary>
    ///  Иницилизация объектов
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (ViewState["IDmtsostav"] == null)
        { ViewState["IDmtsostav"] = 0; }
    }

    /// <summary>
    /// Установить активную панель
    /// </summary>
    /// <param name="index"></param>
    protected void ActiveView(int index) 
    {
        mvSource.ActiveViewIndex = index;
        switch (index) 
        { 
            case 0:
                lbKIS.CssClass = "LinkSelectCommand";
                lbMT.CssClass = "LinkCommand";
                break;
            case 1:
                lbKIS.CssClass = "LinkCommand";
                lbMT.CssClass = "LinkSelectCommand";
                break;
        }

    }

    protected bool SetTreeViewValue(TreeNodeCollection Nodes, string value)
    {
        foreach (TreeNode tree in Nodes)
        {
            if (tree.Value == value)
            {
                tree.Selected = true;
                return true;
            }
            SetTreeViewValue(tree.ChildNodes, value);
        }
        return false;
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // Первый запуск
        if (!IsPostBack)
        {
            ((WebSite_RailWay_RailWay)Master).SetHeader(GetStringRailWayResource("mmAdmin"), GetStringRailWayResource("linkAdmin4"));
            ActiveView(0);
            mtSostav.GetSostavToTreeView(tvMT, (DateTime)ipdt1.startDate, (DateTime)ipdt1.stopDate);
        }
        
    }

    protected void ipdtUpr_DateTimeChange(object sender, leaControls.InputPeriodDateTime.selectDateTimeEventArgs e)
    {
        mtSostav.GetSostavToTreeView(tvMT, (DateTime)e.StartDateTime, (DateTime)e.StopDateTime);
    }

    protected void odsGetInfoCopySostav_ObjectCreating1(object sender, ObjectDataSourceEventArgs e)
    {
        e.ObjectInstance = new clORCSostav("RailWay", "ownerRailWay", "dbnameRailWay");
    }
    protected void odsMTListSostav_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        e.ObjectInstance = new clMTList("RailWay", "ownerRailWay", "dbnameRailWay");
    }
    protected void odsListSostav_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        e.ObjectInstance = new clORC("RailWay", "ownerRailWay", "dbnameRailWay");
    }

    /// <summary>
    /// Исночник КИС
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbKIS_Click(object sender, EventArgs e)
    {
        ActiveView(0);
    }
    /// <summary>
    /// Исночник МТ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbMT_Click(object sender, EventArgs e)
    {
        ActiveView(1);
        mtSostav.GetSostavToTreeView(tvMT, (DateTime)ipdt1.startDate, (DateTime)ipdt1.stopDate);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvMT_SelectedNodeChanged(object sender, EventArgs e)
    {
        this.IDmtsostav = int.Parse(tvMT.SelectedValue);
        lvmtlistsostav.DataBind();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvMT_DataBound(object sender, EventArgs e)
    {
        base.SetTreeViewValue(tvMT.Nodes, this.IDmtsostav.ToString());
    }

    protected void odsMTListSostav_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        //if (String.IsNullOrWhiteSpace(tvMT.SelectedValue))
        //{
        //    e.InputParameters["idsostav"] = 0;
        //}
        //else
        //{
        //    e.InputParameters["idsostav"] = int.Parse(tvMT.SelectedValue);
        //}
    }

}