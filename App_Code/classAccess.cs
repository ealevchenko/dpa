using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace WebBase
{

    public enum typeRules : int { Section = 0, Rules1 = 1}
    public enum Access : int { Not = 0, View = 1, Change = 2, Ban = 3 }

    /// <summary>
    /// Пакет данных правил
    /// </summary>
    public class  RulesDetali
    {
        private int _IDObject;
        public int IDObject { get { return this._IDObject; } }
        private int _Access;
        public Access Access { get { return (Access)this._Access; } set { this._Access = (int)value; } }
        public bool View { get { if ((this._Access == 1) | (this._Access == 2)) { return true; } else { return false; } } }
        public bool Change { get { if (this._Access == 2) { return true; } else { return false; } } }
        public bool Ban { get { if (this._Access == 3) { return true; } else { return false; } } }
        private string _nameObject = null;
        public string nameObject { get { return this._nameObject; }  }
        public RulesDetali(int IDObject, Access Access)
        {
            this._IDObject = IDObject;
            this._Access = (int)Access;
        }
        public RulesDetali(int IDObject, Access Access, string nameObject)
        {
            this._IDObject = IDObject;
            this._Access = (int)Access;
            this._nameObject = nameObject;
        }

    }
    /// <summary>
    /// Коллекция правил
    /// </summary>
    public class RulesCollection : IEnumerable
    {
        private classSection cs = new classSection();
        private List<RulesDetali> _ListRules = new List<RulesDetali>();    // Перечень перьев
        private int _TypeRules;
        public typeRules TypeRules { get { return (typeRules)this._TypeRules; } }
        // Конструктор
        public RulesCollection(typeRules type) 
        {
            this._TypeRules = (int)type;
        }
        // Конструктор 
        public RulesCollection(string rules) 
        {
            SetRules(rules);
        }
        // Получить имя типа
        private string GetName(int IDObject)
        {
            switch (this._TypeRules)
            {
                case (int)typeRules.Section:
                    return cs.GetSectionCulture(IDObject);
                default:
                    return null;
            }
        }
        // Преобразовать строку в тип 
        private void SetRules(string rules, typeRules type)
        {
            if (rules == null) return;
            this._ListRules.Clear();
            string[] array_type = rules.Split(',');
            foreach (string st in array_type)
            {
                string[] array_section = st.Split(':');
                if (array_section[0] == type.ToString())
                {
                    string[] array_param = array_section[1].Split(';');
                    foreach (string param in array_param)
                    {
                        string[] array_value = param.Split('-');
                        if (array_value[0].ToString() != "")
                        {
                            int id = int.Parse(array_value[0].ToString());
                            int access = int.Parse(array_value[1].ToString());
                            this._ListRules.Add(new RulesDetali(id, (Access)access, GetName(id)));
                        }
                    }
                }
            }
        }
        // Преобразовать строку в тип 
        private void SetRules(string rules)
        {
            if (rules == null) return;
            this._ListRules.Clear();
            string[] array_type = rules.Split(',');
            foreach (string st in array_type)
            {
                string[] array_section = st.Split(':');
                if (array_section[0] != "")
                {
                    this._TypeRules = (int)(typeRules)Enum.Parse(typeof(typeRules), array_section[0].Trim());
                    string[] array_param = array_section[1].Split(';');
                    foreach (string param in array_param)
                    {
                        string[] array_value = param.Split('-');
                        if (array_value[0].ToString() != "")
                        {
                            int id = int.Parse(array_value[0].ToString());
                            int access = int.Parse(array_value[1].ToString());
                            this._ListRules.Add(new RulesDetali(id, (Access)access, GetName(id)));
                        }
                    }
                    return;
                } 
            }
        }
        // Преобразование для вызывающей стороны.  
        public RulesDetali GetRules(int pos) { return (RulesDetali)this._ListRules[pos]; }
        // Вставка только типов RulesDetali.  
        public void AddRules(RulesDetali rd) 
        {
            this._ListRules.Add(new RulesDetali((int)rd.IDObject, (Access)rd.Access, GetName(rd.IDObject)));
        }
        // Очистить RulesDetali.
        public void ClearRules() { this._ListRules.Clear(); }
        /// <summary>
        /// Изменить защиту
        /// </summary>
        /// <param name="IDObject"></param>
        /// <param name="access"></param>
        public void ChangeRules(int IDObject, Access access) 
        {
            foreach (RulesDetali rd in this._ListRules) 
            {
                if (rd.IDObject == IDObject)
                { rd.Access = access; return; }
            }

        }
        /// <summary>
        /// Удалить защиту
        /// </summary>
        /// <param name="IDObject"></param>
        public void DeleteRules(int IDObject) 
        { 
            int index = 0;
            while (index < this._ListRules.Count)
            {
                if (this._ListRules[index].IDObject == IDObject)
                { this._ListRules.RemoveAt(index); return; }
                index++;
            }
        }
        // Количество RulesDetali.
        public int Count { get { return this._ListRules.Count; } }
        // Загрузить строку
        public void LoadString(string rules)
        {
            this.SetRules(rules, (typeRules)this._TypeRules);
        }
        // Загрузить строку нового типа
        public void LoadString(string rules, typeRules type)
        {
            this._TypeRules = (int)type;
            this.SetRules(rules, (typeRules)this._TypeRules);
        }
        // Вернуть строку
        public override string ToString()
        {
            StringBuilder Html = new StringBuilder(((typeRules)this._TypeRules).ToString() + ":");
            foreach (RulesDetali rd in this._ListRules)
            {
                Html.Append(rd.IDObject.ToString() + "-" + ((int)rd.Access).ToString() + ";");
            }
            return Html.ToString();
        }
        //
        public RulesCollection RC { get { return this; } }
        // Вернуть список объектов которых нет в правилах
        public List<IObj> GetNotObject() 
        {
            List<int> list_id = new List<int>();
            foreach (RulesDetali rd in this._ListRules)
            {
                list_id.Add(rd.IDObject);
            }
            switch (this._TypeRules)
            {
                case (int)typeRules.Section:
                    return cs.GetNotListSectionCulture(list_id);
                default:
                    return null;
            }
        }
        // Поддержка foreach нумератора.  
        IEnumerator IEnumerable.GetEnumerator() { return this._ListRules.GetEnumerator(); }
    } 

    #region КЛАСС УПРАВЛЕНИЯ ДОСТУПОМ УЧЕТНЫХ ЗАПИСЕЙ К КАРТЕ САЙТОВ
    /// <summary>
    /// Сводное описание для classAccessSiteMap
    /// </summary>
    public class classAccessSiteMap : classSiteMap
    {
        #region ПОЛЯ classAccessSiteMap
        protected string _FieldListAccessSiteMap = "[IDAccessSiteMap],[IDUsers],[IDSiteMap],[Access],[AccessRules]";
        protected string _FieldListAccessSiteMapSiteMap = "AccessSiteMap.IDAccessSiteMap, SiteMap.IDSiteMap, SiteMap.IDWeb, SiteMap.Position, SiteMap.IDSite, SiteMap.Title, SiteMap.TitleEng, SiteMap.Description, " +
            "SiteMap.DescriptionEng,  SiteMap.Protection,  SiteMap.PageProcessor,  SiteMap.ParentID,  SiteMap.IDSection,  AccessSiteMap.Access, AccessSiteMap.AccessRules";
        protected string _NameTableAccessSiteMap;
        protected string _NameSPSetRulesAccessSiteMap;


        #endregion
        
        public classAccessSiteMap()
        {
            this._NameTableAccessSiteMap = WebConfigurationManager.AppSettings["tb_AccessSiteMap"].ToString();
            this._NameSPSetRulesAccessSiteMap = WebConfigurationManager.AppSettings["sp_SetRulesAccessSiteMap"].ToString();
        }

        #region МЕТОДЫ classAccessSiteMap
        #region Общие методы класса
        /// <summary>
        /// Получить коллекцию правил
        /// </summary>
        /// <param name="Rules"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public RulesCollection GetRules(string rules, typeRules type)
        {
            RulesCollection rc = new RulesCollection(type);
            rc.LoadString(rules);
            return rc;
        }

        #endregion

        #region Методы работы с таблицей AccessSiteMap
        /// <summary>
        /// Получить список доступа к карте сайта
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectAccessSiteMap()
        {
            string sql = "SELECT " + this._FieldListAccessSiteMap + " FROM " + this._NameTableAccessSiteMap + "Order by [IDSiteMap],[IDUsers]";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить список доступа к выбранной карте сайта
        /// </summary>
        /// <param name="IDAccessSiteMap"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectAccessSiteMap(int IDAccessSiteMap)
        {
            string sql = "SELECT " + this._FieldListAccessSiteMap + " FROM " + this._NameTableAccessSiteMap + " WHERE (IDAccessSiteMap = " + IDAccessSiteMap.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать доступ к переченю карты сайта доступного пользователю
        /// </summary>
        /// <param name="IDUser"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectUserToAccessSiteMapCulture(int IDUser) 
        {
            string sql = "SELECT AccessSiteMap.IDAccessSiteMap, SiteMap.IDSiteMap, SiteMap.IDWeb, SiteMap.Position, SiteMap.IDSite" + base.GetCultureField("Title") + base.GetCultureField("Description") + 
                ", SiteMap.Protection,  SiteMap.PageProcessor,  SiteMap.ParentID,  SiteMap.IDSection,  AccessSiteMap.Access, AccessSiteMap.AccessRules FROM " + this._NameTableAccessSiteMap + " AS AccessSiteMap INNER JOIN " + base._NameTableSiteMap + " AS SiteMap ON AccessSiteMap.IDSiteMap = SiteMap.IDSiteMap WHERE (AccessSiteMap.IDUsers = " + IDUser.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить строку правил
        /// </summary>
        /// <param name="IDAccessSiteMap"></param>
        /// <returns></returns>
        public string GetAccessRules(int IDAccessSiteMap)
        {
            DataRow[] rows = SelectAccessSiteMap(IDAccessSiteMap).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDAccessSiteMap"] != DBNull.Value)
                {
                    return rows[0]["AccessRules"].ToString().Trim();
                }
                else
                { return null; }
            }
            else return null;
        }
        /// <summary>
        /// Получить доступ
        /// </summary>
        /// <param name="IDAccessSiteMap"></param>
        /// <returns></returns>
        public Access GetAccess(int IDAccessSiteMap)
        {
            DataRow[] rows = SelectAccessSiteMap(IDAccessSiteMap).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDAccessSiteMap"] != DBNull.Value)
                {
                    return (Access)int.Parse(rows[0]["Access"].ToString().Trim());
                }
                else
                { return Access.Not; }
            }
            else return Access.Not;
        }
        /// <summary>
        /// Обновить доступ к карте сайта
        /// </summary>
        /// <param name="IDAccessSiteMap"></param>
        /// <param name="Access"></param>
        /// <param name="AccessRules"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public int SetRulesAccessSiteMap(int IDAccessSiteMap, int Access, string AccessRules, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPSetRulesAccessSiteMap, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDAccessSiteMap", SqlDbType.Int));
            cmd.Parameters["@IDAccessSiteMap"].Value = IDAccessSiteMap;
            cmd.Parameters.Add(new SqlParameter("@Access", SqlDbType.Int));
            cmd.Parameters["@Access"].Value = Access;
            cmd.Parameters.Add(new SqlParameter("@Rules", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@Rules"].Value = (AccessRules != null) ? AccessRules.Trim() : SqlString.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_raccesssitemap");
            }
            return Result;
        }
        #endregion

        #region Методы работы с Rules
        /// <summary>
        /// Получить список колекций правил
        /// </summary>
        /// <param name="rules"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<RulesCollection> SelectAccessRules(string rules) 
        {
            List<RulesCollection> listAllRules = new List<RulesCollection>();
            if ((rules == null) | (rules.Trim() == "")) {return null;}
            string[] array_type = rules.Split(',');
            foreach (string st in array_type)
            {
                string[] array_section = st.Split(':');

                if (array_section[0] != "")
                {
                    typeRules tr = (typeRules)Enum.Parse(typeof(typeRules), array_section[0].Trim());
                    RulesCollection rc = new RulesCollection(tr);
                    rc.LoadString(rules);
                    listAllRules.Add(rc);
                }
            }
            return listAllRules;
        }
        #endregion

        #endregion
    }
    #endregion

    #region КЛАСС УПРАВЛЕНИЯ ДОСТУПОМ К СТРАНИЦАМ САЙТА
    public class classAccessPages : classBaseDB
    {
        #region ПОЛЯ classAccessPages

        protected int? _IDSiteMap = null;
        public int? IDSiteMap { get { return this._IDSiteMap; } }
        protected int? _Owner = null;
        public int? Owner { get { return this._Owner; } }
        protected string _PathcRedirect = WebConfigurationManager.AppSettings["path_SiteNotAccess"].ToString();
        protected string _NameSPGetAccessPage = WebConfigurationManager.AppSettings["sp_GetAccessPage"].ToString();
        protected string _NameTableListSite = WebConfigurationManager.AppSettings["tb_ListSite"].ToString();
        protected string _NameTableSiteMap = WebConfigurationManager.AppSettings["tb_SiteMap"].ToString();
        protected bool _View = true;
        public bool View { get { return this._View; } }
        protected bool _Change = true;
        public bool Change { get { return this._Change; } }
        protected bool _Ban = false;
        public bool Ban { get { return this._Ban; } }
        protected string _UserName = HttpContext.Current.User.Identity.Name;
        public string UserName { get { return this._UserName; } }
        protected string _NamePage = Path.GetFileName(HttpContext.Current.Request.Url.AbsolutePath);
        public string NamePage { get { return this._NamePage; } }
        protected string _NamePageFull = "~" + HttpContext.Current.Request.Url.AbsolutePath;
        public string NamePageFull { get { return this._NamePageFull; } }
        protected string _NameGroup = "No access";
        public string NameGroup { get { return this._NameGroup; } }
        protected int _Access = 0;
        public Access Assess { get { return (Access)this._Access; } }
        #endregion

        public classAccessPages() 
        {
            if (HttpContext.Current.Request.QueryString["Owner"] != null)       // Определим владельца данного сайта
            {
                this._Owner = int.Parse(HttpContext.Current.Request.QueryString["Owner"]);
            }
            this._IDSiteMap = GetIDSiteMap(this._NamePageFull, this._Owner);    // Определим IDSiteMap
        }

        #region МЕТОДЫ classAccessPages
        /// <summary>
        /// Получить IDSiteMap
        /// </summary>
        /// <param name="url"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        protected int? GetIDSiteMap(string url, int? owner) 
        {
            string sql = "SELECT SiteMap.IDSiteMap FROM "+this._NameTableListSite+" as ListSite INNER JOIN "+this._NameTableSiteMap+" as SiteMap ON  ListSite.IDSite =  SiteMap.IDSite " +
                        "WHERE ( ListSite.URL = N'"+url.Trim()+"')";
            if (owner != null)
            {sql+="AND ( SiteMap.IDSection = "+ owner.ToString()+")";}
            DataRow[] rows = base.Select(sql).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDSiteMap"] != DBNull.Value) { return int.Parse(rows[0]["IDSiteMap"].ToString()); }
            }
            return null;
        }
        /// <summary>
        /// Прочесть доступ к файлу
        /// </summary>
        protected void GetAccess() 
        {
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPGetAccessPage, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar, 50));
            cmd.Parameters["@User"].Value = (this._UserName != null) ? this._UserName.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@IDSiteMap", SqlDbType.Int));
            cmd.Parameters["@IDSiteMap"].Value = (this._IDSiteMap != null) ? (int)this._IDSiteMap : SqlInt32.Null; 
            this._View = false;
            this._Change = false;
            this._Ban = false;
            ////!!!!! Убрать
            //this._View = true;
            //this._Change = true;
            //return;
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Access acs = Access.Not;
                        bool group = false;
                        string groupname = null;
                        if (reader["Access"] != DBNull.Value) { acs = (Access)int.Parse(reader["Access"].ToString()); }
                        if (reader["IDWeb"] != DBNull.Value) { group = true; groupname = reader["GroupName"].ToString(); }
                        if (!group)
                        {
                            if (acs == Access.View) { this._View = true; this._NameGroup = "User account"; this._Access = (int)acs; }
                            if (acs == Access.Change) { this._View = true; this._Change = true; this._NameGroup = "User account"; this._Access = (int)acs; }
                            if (acs == Access.Ban) { this._View = false; this._Change = false; this._Ban = true; this._NameGroup = "User account"; this._Access = (int)acs; }
                            if (acs > 0) return; // Учетка имеет приоритет
                        }
                        else
                        {
                            if (acs == Access.Not) { this._NameGroup = groupname; this._Access = (int)acs; }
                            if (acs == Access.View) { this._View = true; this._NameGroup = groupname; this._Access = (int)acs; }
                            if (acs == Access.Change) { this._View = true; this._Change = true; this._NameGroup = groupname; this._Access = (int)acs; }
                            if (acs == Access.Ban) { this._View = false; this._Change = false; this._Ban = true; this._NameGroup = groupname; this._Access = (int)acs; }
                            //if (asc == 3) return;
                        }
                    }
                }
                reader.Close();
            }
            catch (Exception err)
            {
                log.SaveSysError(err);
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// Проверить доступ (пропустить или отправить на страницу ошибок)
        /// </summary>
        /// <param name="LogVisits"></param>
        public void GetAccessToErrorSite(bool LogVisits)
        {
            classLog Log = new classLog();
            //GetAccess();
            if (LogVisits) { Log.SaveLogVisits(this); }
            if ((!this._View) | (this._Ban)) { HttpContext.Current.Response.Redirect(this._PathcRedirect + "?ErrorUrl=" + this.NamePage); }
        }

        #endregion
    }

    #endregion
}