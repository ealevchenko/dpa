using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebBase
{
    /// <summary>
    /// Пакет данных список подразделений поддерживающий интерфейс IObj
    /// </summary>
    public class ListSection : IObj
    {
        private int _IDObject;
        private string _Section;
        public int IDObject
        {
            get { return this._IDObject; }
        }
        public string NameObject
        {
            get { return this._Section; }
        }
        public ListSection(int IDObject, string Section)
        {
            this._IDObject = IDObject;
            this._Section = Section.Trim();
        }
    }

    #region КЛАСС УПРАВЛЕНИЯ СТРУКТУРНЫМИ ПОДРАЗДЕЛЕНИЯМИ

    public enum typeSection : int { Not = 0, Department = 1, Shop = 2, Sector = 3 }

    [Serializable()]
    public class SectionEntity
    {
        public int? ID
        {
            get;
            set;
        }
        public int Position
        {
            get;
            set;
        }
        public string Section
        {
            get;
            set;
        }
        public string SectionFull
        {
            get;
            set;
        }
        public typeSection TypeSection
        {
            get;
            set;
        }
        public int? Cipher
        {
            get;
            set;
        }
        public int? ParentID
        {
            get;
            set;
        }
    }

    [Serializable()]
    public class SectionContent : SectionEntity
    {
        public string SectionEng
        {
            get;
            set;
        }
        public string SectionFullEng
        {
            get;
            set;
        }
        public SectionContent()
            : base()
        {

        }
    }

    public class classSection : classBaseDB
    {
        #region Поля класса classSection
        private string _FieldSection = "[IDSection],[Position],[Section],[SectionEng],[SectionFull],[SectionFullEng],[TypeSection],[Cipher],[ParentID]";
        private string _NameTableSection= WebConfigurationManager.AppSettings["tb_Section"].ToString();
        private string _NameSPInsertSection = WebConfigurationManager.AppSettings["sp_InsertSection"].ToString();
        private string _NameSPUpdateSection = WebConfigurationManager.AppSettings["sp_UpdateSection"].ToString();
        private string _NameSPDeleteSection = WebConfigurationManager.AppSettings["sp_DeleteSection"].ToString();
        private string _NameSPUpPosition = WebConfigurationManager.AppSettings["sp_UpPosition"].ToString();
        private string _NameSPDownPosition = WebConfigurationManager.AppSettings["sp_DownPosition"].ToString();
        #endregion

        #region Конструкторы класса classSection
        public classSection() { }
        #endregion

        #region Методы класса classSection

        #region Общие методы
        /// <summary>
        /// Получить название подразделения
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetSection(object dataItem, bool full)
        {
            if (DataBinder.Eval(dataItem, "IDSection") != DBNull.Value)
            {
                SectionEntity se = GetCultureSection(int.Parse(DataBinder.Eval(dataItem, "IDSection").ToString()));
                if (full) { return se.SectionFull; } else { return se.Section; }
            }
            return null;
        }
        /// <summary>
        /// Получить владельца строки финансирования
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetLineOwner(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "LineOwner") != DBNull.Value)
            {
                return GetSectionCulture(int.Parse(DataBinder.Eval(dataItem, "LineOwner").ToString()));
            }
            else return null;
        }
        /// <summary>
        /// Получить владельца подразделения
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetParentSection(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "ParentID") != DBNull.Value)
            {
                return GetSectionCulture(int.Parse(DataBinder.Eval(dataItem, "ParentID").ToString()));
            }
            else return null;
        }
        /// <summary>
        /// Получить тип подразделения
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetTypeSection(object dataItem)
        {
            typeSection ts = (typeSection)int.Parse(DataBinder.Eval(dataItem, "TypeSection").ToString());
            return GetStringResource(ts.ToString());
        }
        /// <summary>
        /// Получить список ID потомков пренадлежащих IDSection
        /// </summary>
        /// <param name="result"></param>
        /// <param name="IDSiteMap"></param>
        private void GetIDChild(ref string result, int IDSiteMap)
        {
            DataView dv = new DataView(GetListNode(IDSiteMap));
            foreach (DataRowView drv in dv)
            {
                result += "," + drv["IDSection"].ToString().Trim();
                GetIDChild(ref result, int.Parse(drv["IDSection"].ToString().Trim()));
            }
        }
        #endregion

        #region Методы отображения подразделений в TreeView
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
                Childnode.Text = base.GetCultureField(drv, "Section");
                Childnode.Value = drv["IDSection"].ToString().Trim();
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
                root.Text = base.GetCultureField(drv, "Section");
                root.Value = drv["IDSection"].ToString().Trim();
                CreateNode(root);
                tv.Nodes.Add(root);
            }
        }
        /// <summary>
        /// Считать и показать Подразделения в дереве TreeView
        /// </summary>
        /// <param name="tv"></param>
        public void GetSectionToTreeView(TreeView tv)
        {
            tv.Nodes.Clear();
            CreateRootNode(tv);
            tv.DataBind();
            tv.ExpandAll();
        }
        #endregion

        #region Методы работы с подразделениями
        /// <summary>
        /// Получить список корневых меню
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public DataTable GetListRootNode()
        {
            string sql = "SELECT " + this._FieldSection +
                " FROM " + this._NameTableSection + " WHERE (ParentID IS NULL) ";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить список подменю по указаному владельцу
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public DataTable GetListNode(int ParentID)
        {
            string sql = "SELECT " + this._FieldSection +
                " FROM " + this._NameTableSection + "WHERE (ParentID = " + ParentID.ToString() + ") ORDER BY Position";
            return Select(sql);
        }
        /// <summary>
        /// Показать все подразделения
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectSection()
        {
            string sql = "SELECT " + this._FieldSection + " FROM " + this._NameTableSection + "Order by [ParentID]";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать подразделение
        /// </summary>
        /// <param name="IDSection"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectSection(int IDSection)
        {
            string sql = "SELECT " + this._FieldSection + " FROM " + this._NameTableSection + " WHERE (IDSection = " + IDSection.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureSection()
        {
            string sql = "SELECT [IDSection],[Position]" + GetCultureField("Section") + GetCultureField("SectionFull") + ",[TypeSection],[Cipher],[ParentID] FROM " + this._NameTableSection + "Order by [ParentID]";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать подразделение
        /// </summary>
        /// <param name="IDSection"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureSection(int IDSection)
        {
            string sql = "SELECT [IDSection],[Position]" + GetCultureField("Section") + GetCultureField("SectionFull") + ",[TypeSection],[Cipher],[ParentID] FROM " + this._NameTableSection + " WHERE (IDSection = " + IDSection.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Заполним выпадающий список участков добавив дополнительный выбор 0:value
        /// </summary>
        /// <param name="Full"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable DDLListSectionCulture(Boolean Full, string value)
        {
            DataTable dt = DDLListSectionCulture(Full);
            DataRow newRow = dt.NewRow();
            newRow[0] = 0;
            newRow[1] = base.GetStringResource(value);
            dt.Rows.InsertAt(newRow, 0);
            return dt;
        }
        /// <summary>
        /// Показать список подразделений с учетом культуры
        /// </summary>
        /// <param name="Full"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable DDLListSectionCulture(Boolean Full)
        {
            return ListSectionCulture(Full, null);
        }
        /// <summary>
        /// Показать список подразделений с учетом культуры исключив выбраное подразделение
        /// </summary>
        /// <param name="Full"></param>
        /// <param name="IDSection"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable ListSectionCulture(Boolean Full, int? IDSection)
        {
            string sql;
            string sqlWHERE = "";
            if (IDSection != null) { sqlWHERE = " WHERE (IDSection <> " + IDSection.ToString() + ")"; }
            if (Full)
            {
                    sql = "SELECT IDSection" + GetCultureField("Section", "SectionFull") +
                                " FROM " + this._NameTableSection + sqlWHERE;
            }
            else
            {
                    sql = "SELECT IDSection" + GetCultureField("Section") +
                            " FROM " + this._NameTableSection + sqlWHERE;
            }
            return Select(sql);
        }
        /// <summary>
        /// Получить список подразделений с учетом Full - показывать описание, IDSection - показывать текущее подразделение с потомками
        /// </summary>
        /// <param name="Full"></param>
        /// <param name="IDSection"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable ListSectionCultureNotChild(Boolean Full, int? IDSection)
        {
            string sql;
            string sqlWHERE = "";
            if (IDSection != null) 
            {
                string res = ((int)IDSection).ToString();
                GetIDChild(ref res, (int)IDSection);
                sqlWHERE = " WHERE ((IDSection NOT IN (" + res + ")))";
            }
            if (Full)
            {
                    sql = "SELECT IDSection" + GetCultureField("Section", "SectionFull") +
                                " FROM " + this._NameTableSection + sqlWHERE;
            }
            else
            {
                    sql = "SELECT IDSection" + GetCultureField("Section") +
                            " FROM " + this._NameTableSection + sqlWHERE;
            }
            return Select(sql);
        }
        /// <summary>
        /// Метод возвращает название подразделения с учетом культуры по указаному IDSection
        /// </summary>
        /// <param name="IDSection"></param>
        /// <returns></returns>
        public string GetSectionCulture(int IDSection)
        {
            DataRow[] rows = SelectSection(IDSection).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                return GetCultureField(rows[0], "Section");
            }
            else return "-";
        }
        /// <summary>
        /// Показать список подразделений не пренадлежащих списку ID
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        public DataTable GetNotSectionCulture(List<int> listID) 
        {
            string sqlwhere = "0";
            if (listID == null) return null;
            foreach (int i in listID) 
            {
                sqlwhere += "," + i.ToString();
            }
            string sql = "SELECT [IDSection]" + base.GetCultureField("Section") + " FROM " + this._NameTableSection + " WHERE ((IDSection NOT IN (" + sqlwhere + "))) Order by [IDSection]";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать список подразделений не пренадлежащих списку ID
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        public List<IObj> GetNotListSectionCulture(List<int> listID) 
        {
            List<IObj> list = new List<IObj>();
            DataRow[] rows = GetNotSectionCulture(listID).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                foreach (DataRow dr in rows) 
                {
                    ListSection ls = new ListSection(int.Parse(dr["IDSection"].ToString()), dr["Section"].ToString());
                    list.Add(ls);
                }
                return list;
            }
            else return null;
        }
        /// <summary>
        /// Получить  SectionContent по выбранному IDSection
        /// </summary>
        /// <param name="IDSection"></param>
        /// <returns></returns>
        public SectionContent GetSection(int IDSection)
        {
            DataRow[] rows = SelectSection(IDSection).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDSection"] != DBNull.Value)
                {

                    return new SectionContent()
                    {
                        ID = rows[0]["IDSection"] != DBNull.Value ? rows[0]["IDSection"] as int? : null,
                        Position = int.Parse(rows[0]["Position"].ToString()),
                        Section = rows[0]["Section"].ToString(),
                        SectionEng = rows[0]["SectionEng"].ToString(),
                        SectionFull = rows[0]["SectionFull"].ToString(),
                        SectionFullEng = rows[0]["SectionFullEng"].ToString(),
                        TypeSection = (typeSection)int.Parse(rows[0]["TypeSection"].ToString()),
                        Cipher = rows[0]["Cipher"] != DBNull.Value ? rows[0]["Cipher"] as int? : null, 
                        ParentID = rows[0]["ParentID"] != DBNull.Value ? rows[0]["ParentID"] as int? : null, 
                    };
                }
            }
            return null;
        }
        /// <summary>
        /// Получить SectionEntity по выбранному IDSection 
        /// </summary>
        /// <param name="IDSection"></param>
        /// <returns></returns>
        public SectionEntity GetCultureSection(int IDSection)
        {
            DataRow[] rows = SelectCultureSection(IDSection).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDSection"] != DBNull.Value)
                {
                    return new SectionEntity()
                    {
                        ID = rows[0]["IDSection"] != DBNull.Value ? rows[0]["IDSection"] as int? : null,
                        Position = int.Parse(rows[0]["Position"].ToString()),
                        Section = rows[0]["Section"].ToString(),
                        SectionFull = rows[0]["SectionFull"].ToString(),
                        TypeSection = (typeSection)int.Parse(rows[0]["TypeSection"].ToString()),
                        Cipher = rows[0]["Cipher"] != DBNull.Value ? rows[0]["Cipher"] as int? : null, 
                        ParentID = rows[0]["ParentID"] != DBNull.Value ? rows[0]["ParentID"] as int? : null, 
                    };
                }
            }
            return null;
        }
        /// <summary>
        /// Добавить структурное подразделение
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="SectionEng"></param>
        /// <param name="SectionFull"></param>
        /// <param name="SectionFullEng"></param>
        /// <param name="ParentID"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public int InsertSection(string Section, string SectionEng, string SectionFull, string SectionFullEng, int TypeSection, int? Cipher, int? ParentID, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPInsertSection, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Section", SqlDbType.NVarChar, 100));
            cmd.Parameters["@Section"].Value = (Section != null) ? Section.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@SectionEng", SqlDbType.NVarChar, 100));
            cmd.Parameters["@SectionEng"].Value = (SectionEng != null) ? SectionEng.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@SectionFull", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@SectionFull"].Value = (SectionFull != null) ? SectionFull.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@SectionFullEng", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@SectionFullEng"].Value = (SectionFullEng != null) ? SectionFullEng.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@TypeSection", SqlDbType.Int));
            cmd.Parameters["@TypeSection"].Value = TypeSection;
            cmd.Parameters.Add(new SqlParameter("@Cipher", SqlDbType.Int));
            cmd.Parameters["@Cipher"].Value = (Cipher != null) ? (int)Cipher : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@ParentID", SqlDbType.Int));
            cmd.Parameters["@ParentID"].Value = (ParentID != null) ? (int)ParentID : SqlInt32.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_isection");
            }
            return Result;
        }
        /// <summary>
        /// Обновить структурное подразделение
        /// </summary>
        /// <param name="IDSection"></param>
        /// <param name="Section"></param>
        /// <param name="SectionEng"></param>
        /// <param name="SectionFull"></param>
        /// <param name="SectionFullEng"></param>
        /// <param name="ParentID"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public int UpdateSection(int IDSection, string Section, string SectionEng, string SectionFull, string SectionFullEng, int TypeSection, int? Cipher, int? ParentID, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPUpdateSection, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDSection", SqlDbType.Int));
            cmd.Parameters["@IDSection"].Value = IDSection;
            cmd.Parameters.Add(new SqlParameter("@Section", SqlDbType.NVarChar, 100));
            cmd.Parameters["@Section"].Value = (Section != null) ? Section.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@SectionEng", SqlDbType.NVarChar, 100));
            cmd.Parameters["@SectionEng"].Value = (SectionEng != null) ? SectionEng.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@SectionFull", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@SectionFull"].Value = (SectionFull != null) ? SectionFull.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@SectionFullEng", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@SectionFullEng"].Value = (SectionFullEng != null) ? SectionFullEng.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@TypeSection", SqlDbType.Int));
            cmd.Parameters["@TypeSection"].Value = TypeSection;
            cmd.Parameters.Add(new SqlParameter("@Cipher", SqlDbType.Int));
            cmd.Parameters["@Cipher"].Value = (Cipher != null) ? (int)Cipher : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@ParentID", SqlDbType.Int));
            cmd.Parameters["@ParentID"].Value = (ParentID != null) ? (int)ParentID : SqlInt32.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_usection");
            }
            return Result;
        }
        /// <summary>
        /// Удалить структурное подразделение
        /// </summary>
        /// <param name="IDSection"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int DeleteSection(int IDSection, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPDeleteSection, con);
            cmd.CommandType = CommandType.StoredProcedure;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_dsection");
            }
            return Result;
        }
        /// <summary>
        /// Поднять СП на позицию выше
        /// </summary>
        /// <param name="IDSection"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        public int UpSiteMap(int IDSection, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPUpPosition, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDPosition", SqlDbType.Int));
            cmd.Parameters["@IDPosition"].Value = IDSection;
            cmd.Parameters.Add(new SqlParameter("@table_name", SqlDbType.NVarChar, 128));
            cmd.Parameters["@table_name"].Value = this._NameTableSection;
            cmd.Parameters.Add(new SqlParameter("@field_id", SqlDbType.NVarChar, 128));
            cmd.Parameters["@field_id"].Value = "IDSection";
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_upsection", IDSection);
            }
            return Result;
        }
        /// <summary>
        /// опустить СП на позицию ниже
        /// </summary>
        /// <param name="IDSection"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        public int DownSiteMap(int IDSection, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPDownPosition, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDPosition", SqlDbType.Int));
            cmd.Parameters["@IDPosition"].Value = IDSection;
            cmd.Parameters.Add(new SqlParameter("@table_name", SqlDbType.NVarChar, 128));
            cmd.Parameters["@table_name"].Value = this._NameTableSection;
            cmd.Parameters.Add(new SqlParameter("@field_id", SqlDbType.NVarChar, 128));
            cmd.Parameters["@field_id"].Value = "IDSection";
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_downsection", IDSection);
            }
            return Result;
        }
        #endregion

        #endregion

    }
    #endregion
}