using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;

namespace WebBase
{
    public enum eTypeError : int { System = 0, Users }

    #region КЛАСС ЛОГИРОВАНИЯ ВХОДОВ, ОШИБОК, КОРЕКЦИИ [classLog]
    public class classLog
    {
        protected String _connectionString_ASPServer; // базовая строка подключения к служебному серверу ASP.Net
        private int iLogVisits; // Признак писать логирование визитов
        private int iLogErrors; // Признак писать логирование ошибок
        private int iLogChanges; // Признак писать логирование изменений
        private String _NameTableLogErrors;
        //private String _NameTableLogChanges;
        private String _NameTableLogVisits;
        //private String _NameTableSiteMap;
        private string _NameUsers = "system users";
        private string _NamePageSite ="Not pege";
        public classLog()
        {
            this._connectionString_ASPServer = WebConfigurationManager.ConnectionStrings["aspserver"].ConnectionString;
            try // считаем признак писать сообщения визитов или нет
            { iLogVisits = Int32.Parse(WebConfigurationManager.AppSettings["LogVisits"].ToString()); }
            catch { iLogVisits = 0; };
            try // считаем признак писать сообщения ошибок
            { iLogErrors = Int32.Parse(WebConfigurationManager.AppSettings["LogErrors"].ToString()); }
            catch { iLogErrors = 0; };
            try // считаем признак писать сообщения изменений
            { iLogChanges = Int32.Parse(WebConfigurationManager.AppSettings["LogChanges"].ToString()); }
            catch { iLogChanges = 0; };
            this._NameTableLogErrors = WebConfigurationManager.AppSettings["tb_LogErrors"].ToString();
            //this._NameTableLogChanges = WebConfigurationManager.AppSettings["tb_LogChanges"].ToString();
            this._NameTableLogVisits = WebConfigurationManager.AppSettings["tb_LogVisits"].ToString();
            //this._NameTableSiteMap = WebConfigurationManager.AppSettings["tb_SiteMap"].ToString();
            if (HttpContext.Current != null)
            {
                this._NamePageSite = Path.GetFileName(HttpContext.Current.Request.Url.AbsolutePath);

                if (HttpContext.Current.Request.IsAuthenticated)
                { _NameUsers = HttpContext.Current.User.Identity.Name; }

                else { _NameUsers = "Anonymous"; }
            }
        }
        /// <summary>
        /// Метод выполняет запрос к таблице
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected DataTable Select(String sql)
        {
            return Select(sql, "log");
        }
        /// <summary>
        /// Метод выполняет запрос к таблице
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected DataTable Select(String sql, String TableName)
        {
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds, TableName.Trim());
            }
            catch (Exception err)
            {
                //SaveSysError(err);
                return null;
            }
            finally
            {
                con.Close();
            }
            return ds.Tables[TableName.Trim()].Copy();
        }
        /// <summary>
        /// Метод обновляет данные в таблице (возвращает количество задействованных строк)
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected Int32 Update(String sql)
        {
            int res;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                res = cmd.ExecuteNonQuery();
                //SaveChange(sql);
            }
            catch (Exception err)
            {
                //SaveSysError(err);
                return -1;
            }
            finally
            {
                con.Close();
            }
            return res;
        }
        /// <summary>
        /// Получить номер стиля по настройке культуры
        /// </summary>
        /// <returns></returns>
        public int GetNumStyleFormat()
        {
            switch (Thread.CurrentThread.CurrentCulture.Name)
            {
                case "en-US": return 101;
                case "ru-RU": return 103;
                default: return 102;
            }
        }
        /// <summary>
        /// Получить формат по настройке культуры
        /// </summary>
        /// <returns></returns>
        public String GetStyleFormat()
        {
            switch (Thread.CurrentThread.CurrentCulture.Name)
            {
                case "en-US": return "MM/dd/yyyy";
                case "ru-RU": return "dd.MM.yyyy";
                default: return "dd/MM/yyyy";
            }
        }
        /// <summary>
        /// Метод преобразует строку для формирования запросов вставки и правки в зависимости от состояния (если null возвращает слово null иначе убирает лишние пробелы и обрамляет в "")
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetIUSQLString(string str)
        {
            return ((str == null) || (str.Trim() == "")) ? "null" : "'" + str.Trim() + "'";
        }
        /// <summary>
        /// Метод преобразует строку + HtmlEncode для формирования запросов вставки и правки в зависимости от состояния (если null возвращает слово null иначе убирает лишние пробелы и обрамляет в "")
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetIUSQLStringHtmlEncode(string str)
        {
            return ((str == null) || (str.Trim() == "")) ? "null" : "'" + HttpUtility.HtmlEncode(str.Trim()) + "'";
        }

        #region Методы для работы с визитами
        /// <summary>
        /// Метод сохраняет лог визита к защищенному сайту
        /// </summary>
        /// <param name="AWS"></param>
        /// <returns></returns>
        public int SaveLogVisits(classAccessPages AWS)
        {
            if (iLogVisits != 1) { return 0; } // Проверака писать сообщения визитов
            if (AWS != null)
            {
                //AWS.GetAccess();
                string sql = "INSERT INTO " + this._NameTableLogVisits + " ([DateTime],[NameGroup],[NameUsers],[IDSiteMap],[URL],[Access],[View],[Change],[Ban],[Rules]) " +
                                "VALUES (GETDATE() ,N'" + AWS.NameGroup.Trim() + "'" +
                                ",N'" + HttpContext.Current.User.Identity.Name + "'" +
                                "," + AWS.IDSiteMap.ToString() +
                                ",N'" + Path.GetFileName(HttpContext.Current.Request.Url.AbsolutePath) + "'" +
                                "," + ((int)AWS.Assess).ToString() +
                                ",'" + AWS.View.ToString() + "'" +
                                ",'" + AWS.Change.ToString() + "'" +
                                ",'" + AWS.Ban.ToString() + "'" + ",null)";
                return Update(sql);
            }
            else return -1;
        }
        /// <summary>
        /// Метод сохраняет лог визита к незащещенному сайту
        /// </summary>
        /// <returns></returns>
        public int SaveLogVisits()
        {
            if (iLogVisits != 1) { return 0; } // Проверака писать сообщения визитов
            string sql = "INSERT INTO " + this._NameTableLogVisits + " ([DateTime],[NameGroup],[NameUsers],[IDSiteMap],[URL],[Access],[View],[Change],[Ban],[Rules]) " +
                                "VALUES (GETDATE() ,N'Not protected' ,N'" + HttpContext.Current.User.Identity.Name + "',null,N'" + Path.GetFileName(HttpContext.Current.Request.Url.AbsolutePath) + "', 1 ,'true' ,'false','false',null)";
            return Update(sql);
        }
        ///// <summary>
        ///// Показать визиты за период
        ///// </summary>
        ///// <param name="dtStart"></param>
        ///// <param name="dtStop"></param>
        ///// <param name="Admin"></param>
        ///// <param name="Developer"></param>
        ///// <returns></returns>
        //[DataObjectMethod(DataObjectMethodType.Select, true)]
        //public DataTable GetVisits(DateTime dtStart, DateTime dtStop, Boolean Admin, Boolean Developer, Boolean Access)
        //{
        //    String sql = "SELECT tr_LogVisits.IDLogVisits, tr_LogVisits.DateTime, tr_LogVisits.NameGroup, tr_LogVisits.NameUsers, tr_LogVisits.URL, " +
        //                 "tr_LogVisits.Access, tr_LogVisits.[View], tr_LogVisits.Change, tr_LogVisits.Ban, tr_LogVisits.IDSiteMap, tr_SiteMap.Title, " +
        //                 "tr_SiteMap.Description, tr_SiteMap.URLHelp, tr_SiteMap.URL AS URLFull " +
        //                 "FROM " + this._NameTableLogVisits + " as tr_LogVisits LEFT OUTER JOIN " + this._NameTableSiteMap + " as tr_SiteMap ON tr_LogVisits.IDSiteMap = tr_SiteMap.IDSiteMap " +
        //                 "WHERE (tr_LogVisits.DateTime >= CONVERT(DATETIME, '" + dtStart.ToString(CultureInfo.CurrentUICulture.DateTimeFormat) + "', " + GetNumStyleFormat().ToString() + ")) AND (tr_LogVisits.DateTime <= CONVERT(DATETIME, '" + dtStop.ToString(CultureInfo.CurrentUICulture.DateTimeFormat) + "', " + GetNumStyleFormat().ToString() + "))";
        //    if (!Admin) { sql = sql + " and (tr_LogVisits.NameGroup <> 'Администраторы') "; }
        //    if (!Developer) { sql = sql + " and (tr_LogVisits.NameGroup <> 'Разработчики') "; }
        //    if (!Access) { sql = sql + " and (tr_LogVisits.NameGroup <> 'not access') "; }
        //    sql = sql + " ORDER BY tr_LogVisits.DateTime DESC;";
        //    return Select(sql);
        //}
        ///// <summary>
        ///// Возвращает количество посещений
        ///// </summary>
        ///// <param name="Admin"></param>
        ///// <param name="Developer"></param>
        ///// <param name="Access"></param>
        ///// <returns></returns>
        //public Int64 GetCountVisits(Boolean Admin, Boolean Developer, Boolean Access)
        //{
        //    String sql = "SELECT COUNT(IDLogVisits) AS Count FROM " + this._NameTableLogVisits + " where NameGroup is not null ";
        //    if (!Admin) { sql = sql + " and (tr_LogVisits.NameGroup <> 'Администраторы') "; }
        //    if (!Developer) { sql = sql + " and (tr_LogVisits.NameGroup <> 'Разработчики') "; }
        //    if (!Access) { sql = sql + " and (tr_LogVisits.NameGroup <> 'not access') "; }
        //    DataView dv = new DataView(Select(sql));
        //    if (dv.Count > 0)
        //    { return Int64.Parse(dv[0]["Count"].ToString().Trim()); }
        //    else return 0;
        //}
        ///// <summary>
        ///// Возвращает количество посещений за период
        ///// </summary>
        ///// <param name="dtStart"></param>
        ///// <param name="dtStop"></param>
        ///// <param name="Admin"></param>
        ///// <param name="Developer"></param>
        ///// <param name="Access"></param>
        ///// <returns></returns>
        //public Int64 GetCountVisits(DateTime dtStart, DateTime dtStop, Boolean Admin, Boolean Developer, Boolean Access)
        //{
        //    String sql = "SELECT COUNT(IDLogVisits) AS Count FROM " + this._NameTableLogVisits + " WHERE (tr_LogVisits.DateTime >= CONVERT(DATETIME, '" + dtStart.ToString(CultureInfo.CurrentUICulture.DateTimeFormat) + "', " + GetNumStyleFormat().ToString() + ")) AND (tr_LogVisits.DateTime <= CONVERT(DATETIME, '" + dtStop.ToString(CultureInfo.CurrentUICulture.DateTimeFormat) + "', " + GetNumStyleFormat().ToString() + "))";
        //    if (!Admin) { sql = sql + " and (tr_LogVisits.NameGroup <> 'Администраторы') "; }
        //    if (!Developer) { sql = sql + " and (tr_LogVisits.NameGroup <> 'Разработчики') "; }
        //    if (!Access) { sql = sql + " and (tr_LogVisits.NameGroup <> 'not access') "; }
        //    DataView dv = new DataView(Select(sql));
        //    if (dv.Count > 0)
        //    { return Int64.Parse(dv[0]["Count"].ToString().Trim()); }
        //    else return 0;
        //}
        ///// <summary>
        ///// Метод выводит статистику по страницам сайта без выбора времени
        ///// </summary>
        ///// <param name="Admin"></param>
        ///// <param name="Developer"></param>
        ///// <param name="Access"></param>
        ///// <returns></returns>
        //[DataObjectMethod(DataObjectMethodType.Select, true)]
        //public DataTable GetStaticPage(Boolean Admin, Boolean Developer, Boolean Access)
        //{
        //    String sql = "SELECT tr_LogVisits.URL, COUNT( tr_LogVisits.URL) AS Count " +
        //             "FROM " + this._NameTableLogVisits + " as tr_LogVisits LEFT OUTER JOIN " + this._NameTableSiteMap + " as tr_SiteMap ON tr_LogVisits.IDSiteMap = tr_SiteMap.IDSiteMap " +
        //             "WHERE NameGroup is not null";
        //    if (!Admin) { sql = sql + " and (tr_LogVisits.NameGroup <> 'Администраторы') "; }
        //    if (!Developer) { sql = sql + " and (tr_LogVisits.NameGroup <> 'Разработчики') "; }
        //    if (!Access) { sql = sql + " and (tr_LogVisits.NameGroup <> 'not access') "; }
        //    sql = sql + " GROUP BY tr_LogVisits.URL ORDER BY Count DESC ";
        //    return Select(sql);
        //}
        ///// <summary>
        ///// Метод выводит стстистику по страницам сайта
        ///// </summary>
        ///// <param name="dtStart"></param>
        ///// <param name="dtStop"></param>
        ///// <param name="Admin"></param>
        ///// <param name="Developer"></param>
        ///// <param name="Access"></param>
        ///// <returns></returns>
        //[DataObjectMethod(DataObjectMethodType.Select, true)]
        //public DataTable GetStaticPage(DateTime dtStart, DateTime dtStop, Boolean Admin, Boolean Developer, Boolean Access)
        //{
        //    String sql = "SELECT tr_LogVisits.URL, COUNT( tr_LogVisits.URL) AS Count " +
        //             "FROM " + this._NameTableLogVisits + " as tr_LogVisits LEFT OUTER JOIN " + this._NameTableSiteMap + " as tr_SiteMap ON tr_LogVisits.IDSiteMap = tr_SiteMap.IDSiteMap " +
        //             "WHERE (tr_LogVisits.DateTime >= CONVERT(DATETIME, '" + dtStart.ToString(CultureInfo.CurrentUICulture.DateTimeFormat) + "', " + GetNumStyleFormat().ToString() + ")) AND (tr_LogVisits.DateTime <= CONVERT(DATETIME, '" + dtStop.ToString(CultureInfo.CurrentUICulture.DateTimeFormat) + "', " + GetNumStyleFormat().ToString() + "))";
        //    if (!Admin) { sql = sql + " and (tr_LogVisits.NameGroup <> 'Администраторы') "; }
        //    if (!Developer) { sql = sql + " and (tr_LogVisits.NameGroup <> 'Разработчики') "; }
        //    if (!Access) { sql = sql + " and (tr_LogVisits.NameGroup <> 'not access') "; }
        //    sql = sql + " GROUP BY tr_LogVisits.URL ORDER BY Count DESC ";
        //    return Select(sql);
        //}

        //[DataObjectMethod(DataObjectMethodType.Select, true)]
        //public DataTable SelectStaticPage(String User, Boolean Admin, Boolean Developer)
        //{
        //    String sql = "SELECT URL, COUNT(URL) AS Count FROM " + this._NameTableLogVisits + " WHERE NameUsers = '" + User.Trim() + "' ";
        //    if (!Admin) { sql = sql + " and (tr_LogVisits.NameGroup <> 'Администраторы') "; }
        //    if (!Developer) { sql = sql + " and (tr_LogVisits.NameGroup <> 'Разработчики') "; }
        //    sql = sql + " GROUP BY URL ORDER BY Count DESC ";
        //    return Select(sql);
        //}
        ///// <summary>
        ///// Метод выводит статистику по пользователям за выбранный период времени
        ///// </summary>
        ///// <param name="dtStart"></param>
        ///// <param name="dtStop"></param>
        ///// <param name="Admin"></param>
        ///// <param name="Developer"></param>
        ///// <param name="Access"></param>
        ///// <returns></returns>
        //[DataObjectMethod(DataObjectMethodType.Select, true)]
        //public DataTable GetStaticUsers(DateTime dtStart, DateTime dtStop, Boolean Admin, Boolean Developer, Boolean Access)
        //{
        //    String sql = "SELECT tr_LogVisits.NameUsers, COUNT( tr_LogVisits.NameUsers) AS Count " +
        //             "FROM " + this._NameTableLogVisits + " as tr_LogVisits LEFT OUTER JOIN " + this._NameTableSiteMap + " as tr_SiteMap ON tr_LogVisits.IDSiteMap = tr_SiteMap.IDSiteMap " +
        //             "WHERE (tr_LogVisits.DateTime >= CONVERT(DATETIME, '" + dtStart.ToString(CultureInfo.CurrentUICulture.DateTimeFormat) + "', " + GetNumStyleFormat().ToString() + ")) AND (tr_LogVisits.DateTime <= CONVERT(DATETIME, '" + dtStop.ToString(CultureInfo.CurrentUICulture.DateTimeFormat) + "', " + GetNumStyleFormat().ToString() + "))";
        //    if (!Admin) { sql = sql + " and (tr_LogVisits.NameGroup <> 'Администраторы') "; }
        //    if (!Developer) { sql = sql + " and (tr_LogVisits.NameGroup <> 'Разработчики') "; }
        //    if (!Access) { sql = sql + " and (tr_LogVisits.NameGroup <> 'not access') "; }
        //    sql = sql + " GROUP BY tr_LogVisits.NameUsers ORDER BY Count DESC ";
        //    return Select(sql);
        //}
        ///// <summary>
        ///// Метод выводит статистику по всем пользователям
        ///// </summary>
        ///// <param name="Admin"></param>
        ///// <param name="Developer"></param>
        ///// <param name="Access"></param>
        ///// <returns></returns>
        //[DataObjectMethod(DataObjectMethodType.Select, true)]
        //public DataTable GetStaticUsers(Boolean Admin, Boolean Developer, Boolean Access)
        //{
        //    String sql = "SELECT tr_LogVisits.NameUsers, COUNT( tr_LogVisits.NameUsers) AS Count " +
        //             "FROM " + this._NameTableLogVisits + " as tr_LogVisits LEFT OUTER JOIN " + this._NameTableSiteMap + " as tr_SiteMap ON tr_LogVisits.IDSiteMap = tr_SiteMap.IDSiteMap " +
        //             " WHERE NameGroup is not null ";
        //    if (!Admin) { sql = sql + " and (tr_LogVisits.NameGroup <> 'Администраторы') "; }
        //    if (!Developer) { sql = sql + " and (tr_LogVisits.NameGroup <> 'Разработчики') "; }
        //    if (!Access) { sql = sql + " and (tr_LogVisits.NameGroup <> 'not access') "; }
        //    sql = sql + " GROUP BY tr_LogVisits.NameUsers ORDER BY Count DESC ";
        //    return Select(sql);
        //}
        ///// <summary>
        ///// Метод выводит статистику по пользователям
        ///// </summary>
        ///// <param name="Pages"></param>
        ///// <param name="Admin"></param>
        ///// <param name="Developer"></param>
        ///// <returns></returns>
        //[DataObjectMethod(DataObjectMethodType.Select, true)]
        //public DataTable SelectStaticUsers(String Pages, Boolean Admin, Boolean Developer)
        //{
        //    String sql = "SELECT NameUsers, COUNT(NameUsers) AS Count FROM " + this._NameTableLogVisits + " WHERE URL = '" + Pages.Trim() + "' ";
        //    if (!Admin) { sql = sql + " and (tr_LogVisits.NameGroup <> 'Администраторы') "; }
        //    if (!Developer) { sql = sql + " and (tr_LogVisits.NameGroup <> 'Разработчики') "; }
        //    sql = sql + " GROUP BY NameUsers ORDER BY Count DESC ";
        //    return Select(sql);
        //}

        #endregion

        #region Методы для работы с ошибками
        /// <summary>
        ///  Метод записывает лог ошибки
        /// </summary>
        /// <param name="TypeError"></param>
        /// <param name="Source"></param>
        /// <param name="TargetSite"></param>
        /// <param name="TargetSite_DeclaringType"></param>
        /// <param name="TargetSite_MemberType"></param>
        /// <param name="CodeError"></param>
        /// <param name="MessageError"></param>
        /// <param name="StackTrace"></param>
        /// <returns></returns>
        private int SaveErrors(int TypeError, String Source, String TargetSite, String TargetSite_DeclaringType, String TargetSite_MemberType,
            int? CodeError, String MessageError, String StackTrace)
        {
            if (iLogErrors != 1) { return 0; } // Проверака писать сообщения визитов
            String sql = "INSERT INTO " + this._NameTableLogErrors +
            " ([DateTime],[UserEnterprise],[url],[TypeError],[Source] ,[TargetSite] ,[TargetSite_DeclaringType] ,[TargetSite_MemberType],[CodeError],[MessageError],[StackTrace]) " +
            "VALUES (GETDATE(),N'" + this._NameUsers + "',N'" +
            this._NamePageSite + "'," +
            TypeError.ToString() + ",N'" +
            ((Source == null) ? "": Source.Trim()) + "'," +
            GetIUSQLString(TargetSite) + "," +
            GetIUSQLString(TargetSite_DeclaringType) + "," +
            GetIUSQLString(TargetSite_MemberType) + "," +
            ((CodeError == null) ? "0" : CodeError.ToString()) + ",N'" +
            ((MessageError == null) ? "" : HttpUtility.HtmlEncode(MessageError.Trim())) + "'," +
            GetIUSQLStringHtmlEncode(StackTrace)+ ")";
            return Update(sql);
        }
        /// <summary>
        /// Метод записывает лог системной ошибки
        /// </summary>
        /// <param name="err"></param>
        /// <returns></returns>
        public Int32 SaveSysError(Exception err)
        {
            int TypeError = (int)eTypeError.System;
            String Source = err.Source;
            String TargetSite = err.TargetSite.ToString();
            String TargetSite_DeclaringType = err.TargetSite.DeclaringType.ToString();
            String TargetSite_MemberType = err.TargetSite.MemberType.ToString();
            int? CodeError = null;
            String MessageError = err.Message;
            String StackTrace = err.StackTrace;
            if (err is FormatException)
            {
                Source = ((FormatException)err).Source;
                TargetSite = ((FormatException)err).TargetSite.ToString();
                TargetSite_DeclaringType = ((FormatException)err).TargetSite.DeclaringType.ToString();
                TargetSite_MemberType = ((FormatException)err).TargetSite.MemberType.ToString();
                CodeError = null;
                MessageError = ((FormatException)err).Message;
                StackTrace = ((FormatException)err).StackTrace;

            }
            if (err is SqlException)
            {
                Source = ((SqlException)err).Source;
                TargetSite = ((SqlException)err).TargetSite.ToString();
                TargetSite_DeclaringType = ((SqlException)err).TargetSite.DeclaringType.ToString();
                TargetSite_MemberType = ((SqlException)err).TargetSite.MemberType.ToString();
                CodeError = ((SqlException)err).ErrorCode;
                MessageError = ((SqlException)err).Message;
                StackTrace = ((SqlException)err).StackTrace;
            }
            return SaveErrors(TypeError, Source, TargetSite, TargetSite_DeclaringType, TargetSite_MemberType, CodeError, MessageError, StackTrace);
        }
        /// <summary>
        /// Метод записывает лог ошибки пользователя
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="MessageError"></param>
        /// <returns></returns>
        public Int32 SaveUsersError(String Source, String MessageError)
        {

            int TypeError = (int)eTypeError.Users;
            String TargetSite = null;
            String TargetSite_DeclaringType = null;
            String TargetSite_MemberType = null;
            int? CodeError = null;
            String StackTrace = null;
            return SaveErrors(TypeError, Source, TargetSite, TargetSite_DeclaringType, TargetSite_MemberType, CodeError, MessageError, StackTrace);
        }
        #endregion

        //#region Методы для работы c изменениями
        ///// <summary>
        /////  Метод записывает лог изменения на web-сайте
        ///// </summary>
        ///// <param name="change"></param>
        ///// <returns></returns>
        //public Int32 SaveChange(String change)
        //{
        //    if (iLogChanges != 1) { return 0; } // Проверака писать сообщения визитов
        //    if (change != null) { change = "'" + HttpUtility.HtmlEncode(change) + "'"; } else { change = "null"; }
        //    String sql = "INSERT INTO " + this._NameTableLogChanges +
        //    " ([DateTime],[UserEnterprise],[url],[Changes]) " +
        //    "VALUES (GETDATE(),'" + this._NameUsers + "','" + this._NamePageSite + "'," + change + ")";
        //    return Update(sql);
        //}
        //#endregion

    }

    #endregion

}