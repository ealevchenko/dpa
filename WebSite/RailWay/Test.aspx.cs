using leaRailWay;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_RailWay_Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        clMTList mtlist = new clMTList("RailWay", "owner", "dbnameRailWay");
        //mtlist.InsertTable(new object[] { 1, 53190799, 201, 70, 16111, "УГОЛЬ КАМЕ", 46700, "КР.РОГ ГЛА", 3437, "ПРИБ", "8630-239-4670", "15.07.16 09:40", 2983 });

        DataSet ds = new DataSet();
        ds.Tables.Add(mtlist.SelectAll());
        string xmlFile = Server.MapPath("MTList.xml");
        ds.WriteXml(xmlFile, XmlWriteMode.WriteSchema); 
    }
}