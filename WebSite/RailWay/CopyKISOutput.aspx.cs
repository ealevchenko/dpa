using leaRailWay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_RailWay_CopyKISOutput : BaseRailWayAccessPages
{
    protected clOldStations old_st = new clOldStations("RailWay", "owner", "dbnameRailWay");
    protected clOLDWays old_ws = new clOLDWays("RailWay", "owner", "dbnameRailWay");
    protected clORCOutputStation orc_output_station = new clORCOutputStation("RailWay", "owner", "dbnameRailWay");
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // Первый запуск
        if (!IsPostBack)
        {
            ((WebSite_RailWay_RailWay)Master).SetHeader(GetStringRailWayResource("mmAdmin"), GetStringRailWayResource("linkAdmin6"));
        }
        
    }

    protected void ipdtUpr_DateTimeChange(object sender, leaControls.InputPeriodDateTime.selectDateTimeEventArgs e)
    {

    }

    protected void odsGetInfoCopySostav_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        e.ObjectInstance = new clORCOutputStation("RailWay", "owner", "dbnameRailWay");
    }
    /// <summary>
    /// Скорректируем название из-за orc
    /// </summary>
    /// <param name="dataItem"></param>
    /// <param name="field"></param>
    /// <param name="not"></param>
    /// <returns></returns>
    public string GetCorectNameIDORC(object dataItem, string field, string not)
    {
        string name = old_st.GetNameIDORC(dataItem, field, not);
        if (name.Trim() == "Восточная-Приемоотправочная") return "Восточная-Сортировочная";
        return name;
    }

}