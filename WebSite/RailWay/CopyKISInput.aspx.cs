using leaRailWay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_RailWay_CopyKISInput : BaseRailWayAccessPages
{
    protected clOldStations old_st = new clOldStations("RailWay", "owner", "dbnameRailWay");
    protected clOLDWays old_ws = new clOLDWays("RailWay", "owner", "dbnameRailWay");
    protected clORCInputStation orc_input_station = new clORCInputStation("RailWay", "owner", "dbnameRailWay");
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // Первый запуск
        if (!IsPostBack)
        {
            ((WebSite_RailWay_RailWay)Master).SetHeader(GetStringRailWayResource("mmAdmin"), GetStringRailWayResource("linkAdmin5"));
            mvDetali.ActiveViewIndex = 0;        
            lbStatus.CssClass = "LinkSelectCommand";
            lbRules.CssClass = "LinkCommand";
        }
        
    }

    protected void ipdtUpr_DateTimeChange(object sender, leaControls.InputPeriodDateTime.selectDateTimeEventArgs e)
    {

    }

    protected void odsGetInfoCopySostav_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        e.ObjectInstance = new clORCInputStation("RailWay", "owner", "dbnameRailWay");
    }

    protected void lbStatus_Click(object sender, EventArgs e)
    {
        mvDetali.ActiveViewIndex = 0; 
        lbStatus.CssClass = "LinkSelectCommand";
        lbRules.CssClass = "LinkCommand";
    }

    protected void lbRules_Click(object sender, EventArgs e)
    {
        mvDetali.ActiveViewIndex = 1; 
        lbStatus.CssClass = "LinkCommand";
        lbRules.CssClass = "LinkSelectCommand";
    }
}