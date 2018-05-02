using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebBase
{
    public class SiteDetali 
    {
        private string _URL;
        public string URL { get {return this._URL;} }
        private string _Description;
        public string Description { get { return this._Description; } }
        private string _DescriptionEng;
        public string DescriptionEng { get { return this._DescriptionEng; } }
        private string _URLHelp;
        public string URLHelp { get { return this._URLHelp; } }

        public SiteDetali(string URL, string Description, string DescriptionEng, string URLHelp) 
        {
            this._URL = URL;
            this._Description = Description;
            this._DescriptionEng = DescriptionEng;
            this._URLHelp = URLHelp;
        }
    }

    #region КЛАСС УПРАВЛЕНИЯ САЙТАМИ WEB-СЕРВЕРА
    /// <summary>
    /// Сводное описание для classSite
    /// </summary>
    public class classSite: classBaseDB
    {
        #region ПОЛЯ classSite
        protected string _FieldListSite = "[IDSite],[URL],[Description],[DescriptionEng],[URLHelp]";
        protected string _NameTableListSite;
        protected string _NameSPInsertSite;
        protected string _NameSPUpdateSite;
        protected string _NameSPDeleteSite;       

        #endregion 

        public classSite()
        {
            this._NameTableListSite = WebConfigurationManager.AppSettings["tb_ListSite"].ToString();
            this._NameSPInsertSite = WebConfigurationManager.AppSettings["sp_InsertSite"].ToString();
            this._NameSPUpdateSite = WebConfigurationManager.AppSettings["sp_UpdateSite"].ToString();
            this._NameSPDeleteSite = WebConfigurationManager.AppSettings["sp_DeleteSite"].ToString();
        }

        #region МЕТОДЫ classSite
        #region Общие методы класса
        /// <summary>
        /// Показать URL адрес выбранного сайта
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetSiteURL(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDSite") != DBNull.Value)
            {
                return GetSiteURL(int.Parse(DataBinder.Eval(dataItem, "IDSite").ToString()));
            }
            else return null;
        }
        #endregion 

        #region Методы работы с таблицей ListSite
        /// <summary>
        /// Показать список сайтов
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectListSite()
        {
            string sql = "SELECT " + this._FieldListSite + " FROM " + this._NameTableListSite + "Order by [URL]";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать сайт 
        /// </summary>
        /// <param name="IDSite"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectListSite(int IDSite)
        {
            string sql = "SELECT " + this._FieldListSite + " FROM " + this._NameTableListSite + " WHERE (IDSite = " + IDSite.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать перечень сайтов с учетом культуры
        /// </summary>
        /// <param name="IDSite"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectListSiteCulture()
        {
            string sql = "SELECT [IDSite],[URL]" + GetCultureField("Description") + " FROM" + this._NameTableListSite + " Order by [URL]";
            return Select(sql);
        }
        /// <summary>
        /// Получить список сайтов с дополнительным выбором указаным 0:value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable DDLListSiteCulture(string value)
        {
            DataTable dt = SelectListSiteCulture();
            DataRow newRow = dt.NewRow();
            newRow[0] = 0;
            newRow[1] = base.GetStringResource(value);
            newRow[2] = base.GetStringResource(value);
            dt.Rows.InsertAt(newRow, 0);
            return dt;
        }
        /// <summary>
        /// Получить набор данных SiteDetali
        /// </summary>
        /// <param name="IDSite"></param>
        /// <returns></returns>
        public SiteDetali GetSiteDetali(int IDSite)
        {
            DataRow[] rows = SelectListSite(IDSite).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDSite"] != DBNull.Value)
                {
                    return new SiteDetali(rows[0]["URL"].ToString().Trim(),
                        rows[0]["Description"].ToString().Trim(), 
                        rows[0]["DescriptionEng"].ToString().Trim(), 
                        rows[0]["URLHelp"].ToString().Trim());
                }
                else
                { return null; }
            }
            else return null;
        }
        /// <summary>
        /// Получить URL адрес выбраного сайта
        /// </summary>
        /// <param name="IDSite"></param>
        /// <returns></returns>
        public string GetSiteURL(int IDSite)
        {
            DataRow[] rows = SelectListSite(IDSite).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDSite"] != DBNull.Value)
                {
                    return rows[0]["URL"].ToString().Trim();
                }
                else
                { return null; }
            }
            else return null;
        }

        /// <summary>
        /// Добавить сайт в список сайтов Web-сервера
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="Description"></param>
        /// <param name="DescriptionEng"></param>
        /// <param name="URLHelp"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public int InsertSite(string URL, string Description, string DescriptionEng, string URLHelp, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPInsertSite, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@URL", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@URL"].Value = (URL != null) ? URL.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@Description"].Value = (Description != null) ? Description.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@DescriptionEng", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@DescriptionEng"].Value = (DescriptionEng != null) ? DescriptionEng.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@URLHelp", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@URLHelp"].Value = (URLHelp != null) ? URLHelp.Trim() : SqlString.Null;
            SqlParameter retValue = cmd.Parameters.Add("@ret", SqlDbType.Int);
            retValue.Direction = ParameterDirection.ReturnValue;
            try
            {
                con.Open();
                int tmp = cmd.ExecuteNonQuery();
                if (tmp != 0) { Result = (int)retValue.Value; }
            }
            catch (Exception err)
            {
                Result = -1;
                log.SaveSysError(err);

            }
            finally
            {
                con.Close();
            }
            if (OutResult)
            {
                base.OutResultFormatText(Result, "mes_err_", "mes_info_isite");
            }
            return Result;
        }
        /// <summary>
        /// Обновить сайт
        /// </summary>
        /// <param name="IDSite"></param>
        /// <param name="URL"></param>
        /// <param name="Description"></param>
        /// <param name="DescriptionEng"></param>
        /// <param name="URLHelp"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public int UpdateSite(int IDSite, string URL, string Description, string DescriptionEng, string URLHelp, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPUpdateSite, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDSite", SqlDbType.Int));
            cmd.Parameters["@IDSite"].Value =  IDSite;
            cmd.Parameters.Add(new SqlParameter("@URL", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@URL"].Value = (URL != null) ? URL.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@Description"].Value = (Description != null) ? Description.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@DescriptionEng", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@DescriptionEng"].Value = (DescriptionEng != null) ? DescriptionEng.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@URLHelp", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@URLHelp"].Value = (URLHelp != null) ? URLHelp.Trim() : SqlString.Null;
            SqlParameter retValue = cmd.Parameters.Add("@ret", SqlDbType.Int);
            retValue.Direction = ParameterDirection.ReturnValue;
            try
            {
                con.Open();
                int tmp = cmd.ExecuteNonQuery();
                if (tmp != 0) { Result = (int)retValue.Value; }
            }
            catch (Exception err)
            {
                Result = -1;
                log.SaveSysError(err);

            }
            finally
            {
                con.Close();
            }
            if (OutResult)
            {
                base.OutResultFormatText(Result, "mes_err_", "mes_info_usite");
            }
            return Result;
        }
        /// <summary>
        /// Удалить сайт
        /// </summary>
        /// <param name="IDSite"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int DeleteSite(int IDSite, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPDeleteSite, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDSite", SqlDbType.Int));
            cmd.Parameters["@IDSite"].Value =  IDSite;
            SqlParameter retValue = cmd.Parameters.Add("@ret", SqlDbType.Int);
            retValue.Direction = ParameterDirection.ReturnValue;
            try
            {
                con.Open();
                int tmp = cmd.ExecuteNonQuery();
                if (tmp != 0) { Result = (int)retValue.Value; }
            }
            catch (Exception err)
            {
                Result = -1;
                log.SaveSysError(err);

            }
            finally
            {
                con.Close();
            }
            if (OutResult)
            {
                base.OutResultFormatText(Result, "mes_err_", "mes_info_dsite");
            }
            return Result;
        }

        #endregion 
        #endregion 


    }
    #endregion 
   
    /// <summary>
    /// Пакет данных SiteMapDetali
    /// </summary>
    public class SiteMapDetali
    {
        private int _IDWeb;
        public int IDWeb { get { return this._IDWeb; } } 
        private int? _IDSite;
        public int? IDSite { get { return this._IDSite; } } 
        private string _Title;
        public string Title { get { return this._Title; } }
        private string _TitleEng;
        public string TitleEng { get { return this._TitleEng; } }
        private string _Description;
        public string Description { get { return this._Description; } }
        private string _DescriptionEng;
        public string DescriptionEng { get { return this._DescriptionEng; } }
        private bool _Protection;
        public bool Protection { get { return this._Protection; } }
        private bool _PageProcessor;
        public bool PageProcessor { get { return this._PageProcessor; } } 
        private int? _IDParent;
        public int? IDParent { get { return this._IDParent; } } 
        private int _IDSection;
        public int IDSection { get { return this._IDSection; } }

        public SiteMapDetali(int IDWeb, int? IDSite, string Title, string TitleEng, string Description, string DescriptionEng, 
            bool Protection, bool PageProcessor,int? IDParent, int IDSection)
        {
            this._IDWeb = IDWeb;
            this._IDSite = IDSite;
            this._Title = Title;
            this._TitleEng = TitleEng;
            this._Description = Description;
            this._DescriptionEng = DescriptionEng;
            this._Protection = Protection;
            this._PageProcessor = PageProcessor;
            this._IDParent = IDParent;
            this._IDSection = IDSection;
        }
    }

    #region КЛАСС УПРАВЛЕНИЯ КАРТАМИ САЙТА WEB-СЕРВЕРА
    /// <summary>
    /// Сводное описание для classSiteMap
    /// </summary>
    public class classSiteMap : classSite
    {
        #region ПОЛЯ classSiteMap
        private string _NameServer;
        private string _NamePageProcessor;
        protected string _FieldSiteMap = "[IDSiteMap],[IDWeb],[Position],[IDSite],[Title],[TitleEng],[Description],[DescriptionEng],[Protection],[PageProcessor],[ParentID],[IDSection],[AccessRulesSection]";
        protected string _FieldSiteMapListSite = "SiteMap.IDSiteMap, SiteMap.IDWeb, SiteMap.Position, SiteMap.IDSite, ListSite.URL, ListSite.URLHelp, SiteMap.Title, "+
            "SiteMap.TitleEng, SiteMap.Description,  SiteMap.DescriptionEng,  SiteMap.Protection,  SiteMap.PageProcessor,  SiteMap.ParentID,SiteMap.IDSection,  SiteMap.AccessRulesSection";
        protected string _NameTableSiteMap;
        protected string _NameSPInsertSiteMap;
        protected string _NameSPUpdateSiteMap;
        protected string _NameSPDeleteSiteMap;
        protected string _NameSPUpPosition;
        protected string _NameSPDownPosition;
        #endregion
        
        public classSiteMap()
        {
            this._NameServer = WebConfigurationManager.AppSettings["NameServer"].ToString();
            this._NamePageProcessor = WebConfigurationManager.AppSettings["PageProcessor"].ToString();            
            this._NameTableSiteMap = WebConfigurationManager.AppSettings["tb_SiteMap"].ToString();
            this._NameSPInsertSiteMap = WebConfigurationManager.AppSettings["sp_InsertSiteMap"].ToString();
            this._NameSPUpdateSiteMap = WebConfigurationManager.AppSettings["sp_UpdateSiteMap"].ToString();
            this._NameSPDeleteSiteMap = WebConfigurationManager.AppSettings["sp_DeleteSiteMap"].ToString();
            this._NameSPUpPosition = WebConfigurationManager.AppSettings["sp_UpPosition"].ToString();
            this._NameSPDownPosition = WebConfigurationManager.AppSettings["sp_DownPosition"].ToString();
        }

        #region МЕТОДЫ classSiteMap

        #region Общие методы класса
        /// <summary>
        /// Показать владельца этого раздела меню
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetParentTitle(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "ParentID") != DBNull.Value)
            {
                return GetParentTitleCulture(int.Parse(DataBinder.Eval(dataItem, "ParentID").ToString()));
            }
            else return null;
        }
        /// <summary>
        /// Получить список ID потомков пренадлежащих IDSiteMap
        /// </summary>
        /// <param name="result"></param>
        /// <param name="IDSiteMap"></param>
        private void GetIDChild(ref string result, int IDSiteMap) 
        {
            DataView dv = new DataView(GetListNode(IDSiteMap));
            foreach (DataRowView drv in dv)
            {
                result += ","+ drv["IDSiteMap"].ToString().Trim();
                GetIDChild(ref result, int.Parse(drv["IDSiteMap"].ToString().Trim()));
            }
        }
        #endregion

        #region Методы отображения карты сайта в TreeView
        /// <summary>
        /// Показать дочерние подменю
        /// </summary>
        /// <param name="node"></param>
        private void CreateNode(TreeNode node)
        {
            DataView dv = new DataView(GetListNode(Int32.Parse(node.Value)));
            foreach (DataRowView drv in dv)
            {
                TreeNode Childnode = new TreeNode();
                Childnode.Text = base.GetCultureField(drv, "Title");
                Childnode.Value = drv["IDSiteMap"].ToString().Trim();
                node.ChildNodes.Add(Childnode);
                CreateNode(Childnode);
            }
        }
        /// <summary>
        /// Показать родительское окно
        /// </summary>
        /// <param name="tv"></param>
        private void CreateRootNode(TreeView tv)
        {
            DataView dv = new DataView(GetListRootNode());
            foreach (DataRowView drv in dv)
            {
                TreeNode root = new TreeNode();
                root.Text = base.GetCultureField(drv, "Title");
                root.Value = drv["IDSiteMap"].ToString().Trim();
                CreateNode(root);
                tv.Nodes.Add(root);
            }
        }
        /// <summary>
        /// Считать и показать карту сайта в дереве TreeView
        /// </summary>
        /// <param name="tv"></param>
        public void GetSiteMapToTreeView(TreeView tv)
        {
            tv.Nodes.Clear();
            CreateRootNode(tv);
            tv.DataBind();
            tv.ExpandAll();
        }
        #endregion

        #region Методы работы с таблицей SiteMap
        /// <summary>
        /// Получить список корневых меню
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public DataTable GetListRootNode()
        {
            string sql = "SELECT " + this._FieldSiteMapListSite + 
                " FROM "+this._NameTableSiteMap+" as SiteMap LEFT OUTER JOIN "+this._NameTableListSite+" as ListSite ON  SiteMap.IDSite =  ListSite.IDSite "+
                "WHERE (SiteMap.ParentID IS NULL) "+
                "ORDER BY SiteMap.IDWeb";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить список подменю по указаному владельцу
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public DataTable GetListNode(int ParentID)
        {
            string sql = "SELECT " + this._FieldSiteMapListSite + 
                " FROM "+this._NameTableSiteMap+" as SiteMap LEFT OUTER JOIN "+this._NameTableListSite+" as ListSite ON  SiteMap.IDSite =  ListSite.IDSite "+
                "WHERE (SiteMap.ParentID = "+ParentID.ToString()+") "+
                "ORDER BY SiteMap.Position";            
            return Select(sql);
        }
        /// <summary>
        /// Показать все строки кырты сайтов
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectSiteMap()
        {
            string sql = "SELECT " + this._FieldSiteMap + " FROM " + this._NameTableSiteMap + "Order by [IDWeb],[ParentID],[Position]";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать выбранную строку карты сайтов
        /// </summary>
        /// <param name="IDSection"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectSiteMap(int IDSiteMap)
        {
            string sql = "SELECT " + this._FieldSiteMap + " FROM " + this._NameTableSiteMap + " WHERE ([IDSiteMap] = " + IDSiteMap.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить название владельца меню
        /// </summary>
        /// <param name="IDSite"></param>
        /// <returns></returns>
        public string GetParentTitleCulture(int ParentID)
        {
            DataRow[] rows = SelectSiteMap(ParentID).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDSiteMap"] != DBNull.Value)
                {
                    return GetCultureField(rows[0], "Title");
                }
                else
                { return null; }
            }
            else return null;
        }
        /// <summary>
        /// Показать все стоки с учетом культуры
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectSiteMapCulture()
        {
            string sql = "SELECT [IDSiteMap],[IDWeb],[Position],[IDSite]"+ base.GetCultureField("Title") + base.GetCultureField("Description") + "[Protection],[PageProcessor],[ParentID],[IDSection],[AccessRulesSection] FROM " + this._NameTableSiteMap + "Order by [IDWeb],[ParentID],[Position]";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить список с учетом культуры для DropDownList компонента
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable DDLListSiteMapCulture()
        {
            return DDLListSiteMapCulture(null);
        }
        /// <summary>
        /// Получить список с учетом культуры для DropDownList компонента (не показывая указанную строку и ее потомки)
        /// </summary>
        /// <param name="IDSiteMap"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable DDLListSiteMapCulture(int? IDSiteMap)
        {
            string sql = "SELECT [IDSiteMap], [Title] = ";
            if (Thread.CurrentThread.CurrentCulture.Name == "en-US")
            { sql += "[TitleEng] + ' (' + [DescriptionEng] + ')'"; }
            else
            { sql += "[Title] + ' (' + [Description] + ')'"; }
            sql += " FROM " + this._NameTableSiteMap;
            if (IDSiteMap != null)
            {
                string res = ((int)IDSiteMap).ToString();
                GetIDChild(ref res, (int)IDSiteMap);
                
                //sql += " WHERE (IDSiteMap <> " + ((int)IDSiteMap).ToString() + ")";
                sql += " WHERE ((IDSiteMap NOT IN ("+res+")))";
            }
           sql +=" Order by [IDWeb],[ParentID],[Position]";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить список с учетом культуры для DropDownList компонента с дополнительным выбором указаным 0:value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable DDLListSiteMapCulture(string value, int? IDSiteMap)
        {
            DataTable dt = DDLListSiteMapCulture(IDSiteMap);
            DataRow newRow = dt.NewRow();
            newRow[0] = 0;
            newRow[1] = base.GetStringResource(value);
            dt.Rows.InsertAt(newRow, 0);
            return dt;
        }
        /// <summary>
        /// Получить набор данных SiteMapDetali по указаному IDSiteMap
        /// </summary>
        /// <param name="IDSiteMap"></param>
        /// <returns></returns>
        public SiteMapDetali GetSiteMapDetali(int IDSiteMap)
        {
            DataRow[] rows = SelectSiteMap(IDSiteMap).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDSiteMap"] != DBNull.Value)
                {
                    int? IDSite = null;
                    if (rows[0]["IDSite"] != DBNull.Value) {IDSite = int.Parse(rows[0]["IDSite"].ToString());}
                    int? ParentID = null;
                    if (rows[0]["ParentID"] != DBNull.Value) { ParentID = int.Parse(rows[0]["ParentID"].ToString()); }
                    return new SiteMapDetali((int)rows[0]["IDWeb"],
                        IDSite,
                        rows[0]["Title"].ToString().Trim(),
                        rows[0]["TitleEng"].ToString().Trim(),
                        rows[0]["Description"].ToString().Trim(),
                        rows[0]["DescriptionEng"].ToString().Trim(),
                        (bool)rows[0]["Protection"],
                        (bool)rows[0]["PageProcessor"],
                        ParentID,
                        (int)rows[0]["IDSection"]);
                }
                else
                { return null; }
            }
            else return null;
        }
        /// <summary>
        /// Получить IDWeb выбранной строки сайта
        /// </summary>
        /// <param name="IDSiteMap"></param>
        /// <returns></returns>
        public int GetIDWeb(int IDSiteMap) 
        {
            SiteMapDetali smd = GetSiteMapDetali(IDSiteMap);
            if (smd != null) return smd.IDWeb;
            return -1;
        }
        /// <summary>
        /// Добавить строку карты сайта
        /// </summary>
        /// <param name="IDWeb"></param>
        /// <param name="IDSite"></param>
        /// <param name="Title"></param>
        /// <param name="TitleEng"></param>
        /// <param name="Description"></param>
        /// <param name="DescriptionEng"></param>
        /// <param name="Protection"></param>
        /// <param name="PageProcessor"></param>
        /// <param name="ParentID"></param>
        /// <param name="IDSection"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public int InsertSiteMap(int IDWeb, int? IDSite, string Title, string TitleEng, string Description, string DescriptionEng, 
            bool Protection, bool PageProcessor, int? ParentID, int IDSection, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPInsertSiteMap, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDWeb", SqlDbType.Int));
            cmd.Parameters["@IDWeb"].Value = IDWeb;
            cmd.Parameters.Add(new SqlParameter("@IDSite", SqlDbType.Int));
            cmd.Parameters["@IDSite"].Value = (IDSite != null) ? (int)IDSite : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 250));
            cmd.Parameters["@Title"].Value = (Title != null) ? Title.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@TitleEng", SqlDbType.NVarChar, 250));
            cmd.Parameters["@TitleEng"].Value = (TitleEng != null) ? TitleEng.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@Description"].Value = (Description != null) ? Description.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@DescriptionEng", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@DescriptionEng"].Value = (DescriptionEng != null) ? DescriptionEng.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Protection", SqlDbType.Bit));
            cmd.Parameters["@Protection"].Value = Protection;
            cmd.Parameters.Add(new SqlParameter("@PageProcessor", SqlDbType.Bit));
            cmd.Parameters["@PageProcessor"].Value = PageProcessor;
            cmd.Parameters.Add(new SqlParameter("@ParentID", SqlDbType.Int));
            cmd.Parameters["@ParentID"].Value = (ParentID != null) ? (int)ParentID : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@IDSection", SqlDbType.Int));
            cmd.Parameters["@IDSection"].Value = IDSection;
            SqlParameter retValue = cmd.Parameters.Add("@ret", SqlDbType.Int);
            retValue.Direction = ParameterDirection.ReturnValue;
            try
            {
                con.Open();
                int tmp = cmd.ExecuteNonQuery();
                if (tmp != 0) { Result = (int)retValue.Value; }
            }
            catch (Exception err)
            {
                Result = -1;
                log.SaveSysError(err);

            }
            finally
            {
                con.Close();
            }
            if (OutResult)
            {
                base.OutResultFormatText(Result, "mes_err_", "mes_info_isitemap");
            }
            return Result;
        }
        /// <summary>
        /// Обновить данные строки карты сайта
        /// </summary>
        /// <param name="IDSiteMap"></param>
        /// <param name="IDWeb"></param>
        /// <param name="IDSite"></param>
        /// <param name="Title"></param>
        /// <param name="TitleEng"></param>
        /// <param name="Description"></param>
        /// <param name="DescriptionEng"></param>
        /// <param name="Protection"></param>
        /// <param name="PageProcessor"></param>
        /// <param name="ParentID"></param>
        /// <param name="IDSection"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public int UpdateSiteMap(int IDSiteMap, int IDWeb, int? IDSite, string Title, string TitleEng, string Description, string DescriptionEng, 
            bool Protection, bool PageProcessor, int? ParentID, int IDSection, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPUpdateSiteMap, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDSiteMap", SqlDbType.Int));
            cmd.Parameters["@IDSiteMap"].Value = IDSiteMap;
            cmd.Parameters.Add(new SqlParameter("@IDWeb", SqlDbType.Int));
            cmd.Parameters["@IDWeb"].Value = IDWeb;
            cmd.Parameters.Add(new SqlParameter("@IDSite", SqlDbType.Int));
            cmd.Parameters["@IDSite"].Value = (IDSite != null) ? (int)IDSite : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 250));
            cmd.Parameters["@Title"].Value = (Title != null) ? Title.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@TitleEng", SqlDbType.NVarChar, 250));
            cmd.Parameters["@TitleEng"].Value = (TitleEng != null) ? TitleEng.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@Description"].Value = (Description != null) ? Description.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@DescriptionEng", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@DescriptionEng"].Value = (DescriptionEng != null) ? DescriptionEng.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Protection", SqlDbType.Bit));
            cmd.Parameters["@Protection"].Value = Protection;
            cmd.Parameters.Add(new SqlParameter("@PageProcessor", SqlDbType.Bit));
            cmd.Parameters["@PageProcessor"].Value = PageProcessor;
            cmd.Parameters.Add(new SqlParameter("@ParentID", SqlDbType.Int));
            cmd.Parameters["@ParentID"].Value = (ParentID != null) ? (int)ParentID : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@IDSection", SqlDbType.Int));
            cmd.Parameters["@IDSection"].Value = IDSection;
            SqlParameter retValue = cmd.Parameters.Add("@ret", SqlDbType.Int);
            retValue.Direction = ParameterDirection.ReturnValue;
            try
            {
                con.Open();
                int tmp = cmd.ExecuteNonQuery();
                if (tmp != 0) { Result = (int)retValue.Value; }
            }
            catch (Exception err)
            {
                Result = -1;
                log.SaveSysError(err);

            }
            finally
            {
                con.Close();
            }
            if (OutResult)
            {
                base.OutResultFormatText(Result, "mes_err_", "mes_info_usitemap");
            }
            return Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IDSiteMap"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int DeleteSiteMap(int IDSiteMap, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPDeleteSiteMap, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDSiteMap", SqlDbType.Int));
            cmd.Parameters["@IDSiteMap"].Value = IDSiteMap;
            SqlParameter retValue = cmd.Parameters.Add("@ret", SqlDbType.Int);
            retValue.Direction = ParameterDirection.ReturnValue;
            try
            {
                con.Open();
                int tmp = cmd.ExecuteNonQuery();
                if (tmp != 0) { Result = (int)retValue.Value; }
            }
            catch (Exception err)
            {
                Result = -1;
                log.SaveSysError(err);
            }
            finally
            {
                con.Close();
            }
            if (OutResult)
            {
                base.OutResultFormatText(Result, "mes_err_", "mes_info_dsitemap");
            }
            return Result;
        }
        /// <summary>
        /// Поднять строку карты сайта на позицию выше
        /// </summary>
        /// <param name="IDSiteMap"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        public int UpSiteMap(int IDSiteMap, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPUpPosition, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDPosition", SqlDbType.Int));
            cmd.Parameters["@IDPosition"].Value = IDSiteMap;
            cmd.Parameters.Add(new SqlParameter("@table_name", SqlDbType.NVarChar, 128));
            cmd.Parameters["@table_name"].Value = this._NameTableSiteMap;
            cmd.Parameters.Add(new SqlParameter("@field_id", SqlDbType.NVarChar, 128));
            cmd.Parameters["@field_id"].Value = "IDSiteMap";
            SqlParameter retValue = cmd.Parameters.Add("@ret", SqlDbType.Int);
            retValue.Direction = ParameterDirection.ReturnValue;
            try
            {
                con.Open();
                int tmp = cmd.ExecuteNonQuery();
                if (tmp != 0) { Result = (int)retValue.Value; }
            }
            catch (Exception err)
            {
                Result = -1;
                log.SaveSysError(err);
            }
            finally
            {
                con.Close();
            }
            if (OutResult)
            {
                base.OutResultFormatText(Result, "mes_err_", "mes_info_upsitemap", IDSiteMap);
            }
            return Result;
        }
        /// <summary>
        /// опустить строку карты сайта на позицию ниже
        /// </summary>
        /// <param name="IDSiteMap"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        public int DownSiteMap(int IDSiteMap, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPDownPosition, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDPosition", SqlDbType.Int));
            cmd.Parameters["@IDPosition"].Value = IDSiteMap;
            cmd.Parameters.Add(new SqlParameter("@table_name", SqlDbType.NVarChar, 128));
            cmd.Parameters["@table_name"].Value = this._NameTableSiteMap;
            cmd.Parameters.Add(new SqlParameter("@field_id", SqlDbType.NVarChar, 128));
            cmd.Parameters["@field_id"].Value = "IDSiteMap";
            SqlParameter retValue = cmd.Parameters.Add("@ret", SqlDbType.Int);
            retValue.Direction = ParameterDirection.ReturnValue;
            try
            {
                con.Open();
                int tmp = cmd.ExecuteNonQuery();
                if (tmp != 0) { Result = (int)retValue.Value; }
            }
            catch (Exception err)
            {
                Result = -1;
                log.SaveSysError(err);
            }
            finally
            {
                con.Close();
            }
            if (OutResult)
            {
                base.OutResultFormatText(Result, "mes_err_", "mes_info_downsitemap", IDSiteMap);
            }
            return Result;
        }
        /// <summary>
        /// Получить полный путь по IDSiteMap
        /// </summary>
        /// <param name="IDSiteMap"></param>
        /// <returns></returns>
        public string GetPathUrl(int IDSiteMap) 
        {
            if (IDSiteMap <= 0) return "#";
            string sql = "SELECT  ListSite.URL, SiteMap.PageProcessor, SiteMap.IDSection FROM " + this._NameTableListSite + " AS ListSite INNER JOIN " + 
                this._NameTableSiteMap + " AS SiteMap ON ListSite.IDSite = SiteMap.IDSite WHERE ( SiteMap.IDSiteMap = " + IDSiteMap.ToString() + ")";
            DataRow[] rows = base.Select(sql).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                string current_page = HttpContext.Current.Request.Url.AbsolutePath;

                //log.SaveUsersError(HttpContext.Current.Request.Url.AbsoluteUri, HttpContext.Current.Request.Url.AbsolutePath);
                bool PageProcessor = false;
                int IDSection = 0;
                string url = rows[0]["URL"].ToString().Trim();
                if (rows[0]["PageProcessor"] != DBNull.Value) { PageProcessor = bool.Parse(rows[0]["PageProcessor"].ToString());}
                if (rows[0]["IDSection"] != DBNull.Value) { IDSection = int.Parse(rows[0]["IDSection"].ToString()); }
                string[] array = current_page.Split('/');
                string new_path = null;
                foreach (string name in array)
                {
                    if (name.ToLower() == Path.GetFileName(HttpContext.Current.Request.Url.AbsolutePath).ToLower())
                    { break; }
                    else { new_path += "../"; }
                    //if (new_path != null)
                    //{ new_path += "../"; }
                    //if (name.ToLower() == this._NameServer.ToLower()) // имя сервера
                    //{ new_path = ""; }
                }
                if (PageProcessor) { new_path += this._NamePageProcessor.Remove(0, 2); }
                new_path += url.Remove(0, 2);
                if (PageProcessor) { new_path += "%3FOwner=" + IDSection.ToString(); } else { new_path += "?Owner=" + IDSection.ToString(); } 
                return new_path;
            }
            return "#";
        }
        /// <summary>
        /// Скорректировать путь
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetPathUrl(string path)
        {
                string current_page = HttpContext.Current.Request.Url.AbsolutePath;
                string[] array = current_page.Split('/');
                string new_path = null;
                foreach (string name in array)
                {
                    if (name.ToLower() == "")
                    { continue; }
                    if (name.ToLower() == Path.GetFileName(HttpContext.Current.Request.Url.AbsolutePath).ToLower())
                    { break; }
                    else { new_path += "../"; }
                }
                new_path += path.Remove(0, 1);
                return new_path;
        }
        #endregion

        #endregion


    }
    #endregion


}