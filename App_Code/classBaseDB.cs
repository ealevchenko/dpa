using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
    public enum styleButton : int { link = 0, button=1, img=2 }
    public enum styleUpr : int { InsUpdDel = 0, InsUpdDelUpDow = 1, UpdCan = 2, InsCan = 3, UpdSkip = 4, InsUpdDelClrCrt = 5}

    /// <summary>
    /// Интерфейс подключения к компонентам (ddl)
    /// </summary>
    public interface IObj
    {
        int IDObject { get; }
        string NameObject { get; }
    }

    #region КЛАСС БАЗОВОГО ПОДКЛЮЧЕНИЯ К СИСТЕМНЫМ ТАБЛИЦАМ
    /// <summary>
    /// Класс classBaseDB базовый класс для подключения к системным таблицам
    /// </summary>
    public class classBaseDB
    {
        #region Поля класса classBaseDB
        ResourceManager resourceBase = new ResourceManager(typeof(ResourceBase));
        ResourceManager resourceMassage = new ResourceManager(typeof(ResourceMessage));   
        protected classLog log = new classLog();
        protected String _connectionString_ASPServer; // базовая строка подключения к служебному серверу ASP.Net
        protected int _TypeServer;
        #endregion

        #region Конструкторы класса classBaseDB
        public classBaseDB()
        {
            this._connectionString_ASPServer = WebConfigurationManager.ConnectionStrings["aspserver"].ConnectionString;
            this._TypeServer = Int32.Parse(WebConfigurationManager.AppSettings["TypeServer"].ToString().Trim());
        }
        #endregion

        #region МЕТОДЫ classBaseDB

        #region Методы работы с базой данных
        /// <summary>
        /// Метод выполняет запрос к таблице
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected DataTable Select(String sql)
        {
            return Select(sql, "Table");
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
                log.SaveSysError(err);
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
        protected int Update(String sql)
        {
            int res;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                res = cmd.ExecuteNonQuery();
                //log.SaveChange(sql);
            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                return -1;
            }
            finally
            {
                con.Close();
            }
            return res;
        }
        #endregion

        #region Методы преобразования по условию указаной культуры
        /// <summary>
        /// Прочесть строку ресурса (может переопределятся в потомках)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual string GetStringResource(string key)
        {
            return resourceBase.GetString(key, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Вывести сообщение
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetStringMessage(string key)
        {
            return resourceMassage.GetString(key, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Получить номер стиля по настройке культуры
        /// </summary>
        /// <returns></returns>
        protected int GetNumStyleFormat()
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
        protected string GetStyleFormat()
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
        protected string GetIUSQLString(string str)
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
        /// <summary>
        /// Получить поле value с учетом культуры
        /// </summary>
        /// <returns></returns>
        protected string GetCultureField(string value)
        {
            if (Thread.CurrentThread.CurrentCulture.Name == "en-US")
            { return "," + value + "Eng AS [" + value + "]"; }
            else
            { return ",[" + value + "]"; }
        }
        /// <summary>
        /// Получить поле из двух полей Basic (Additional) с учетом культуры
        /// </summary>
        /// <param name="Basic"></param>
        /// <param name="Additional"></param>
        /// <returns></returns>
        protected string GetCultureField(string Basic, string Additional)
        {
            if (Thread.CurrentThread.CurrentCulture.Name == "en-US")
            {
                return ", " + Basic + "Eng + ' (' + " + Additional + "Eng + ')' AS " + Basic;
            }
            else
            { return ", " + Basic + "+ ' (' + " + Additional + " + ')' AS " + Basic; }
        }
        /// <summary>
        /// Получить поле с учетом культуры
        /// </summary>
        /// <returns></returns>
        protected string GetCultureField(DataRowView drv, string field)
        {
            if (Thread.CurrentThread.CurrentCulture.Name == "en-US")
            {
                return drv[field + "Eng"].ToString().Trim();
            }
            else
            {
                return drv[field].ToString().Trim();
            }
        }
        /// <summary>
        /// Получить поле с учетом культуры
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        protected string GetCultureField(DataRow dr, string field)
        {
            if (Thread.CurrentThread.CurrentCulture.Name == "en-US")
            {
                return dr[field + "Eng"].ToString().Trim();
            }
            else
            {
                return dr[field].ToString().Trim();
            }
        }
        #endregion

        /// <summary>
        /// Вывести сообщение о результатх с форматированием
        /// </summary>
        /// <param name="result"></param>
        /// <param name="Prefix_error"></param>
        /// <param name="Prefix_info"></param>
        protected void OutResultFormatText(int result, string Prefix_error, string Prefix_info)
        {
            string message;
            if ((((Page)HttpContext.Current.CurrentHandler).Master.FindControl("ErrorMessage") != null) &&
                (((Page)HttpContext.Current.CurrentHandler).Master.FindControl("InfoMessage") != null))
            {
                Literal lError = (Literal)((Page)HttpContext.Current.CurrentHandler).Master.FindControl("ErrorMessage");
                Literal lInfo = (Literal)((Page)HttpContext.Current.CurrentHandler).Master.FindControl("InfoMessage");
                if (result <= 0)
                {
                    message = GetStringMessage(Prefix_error + (result * -1).ToString());
                    if ((message != null) & (message.Trim() != ""))
                    {
                        lError.Text = String.Format(message, result);
                    } else {lError.Text = String.Format(GetStringMessage("mes_err_0"),result);}
                }
                else
                {
                    message = GetStringMessage(Prefix_info);
                    if ((message != null) & (message.Trim() != ""))
                    {
                        lInfo.Text = String.Format(message, result);
                    }
                    else { lInfo.Text = GetStringMessage("mes_info_0"); }
                }
            }
        }
        /// <summary>
        /// Вывести сообщение о результатх с форматированием
        /// </summary>
        /// <param name="result"></param>
        /// <param name="Prefix_error"></param>
        /// <param name="Prefix_info"></param>
        protected void OutResultFormatText(int result, string Prefix_error, string Prefix_info, int result1)
        {
            string message;
            if ((((Page)HttpContext.Current.CurrentHandler).Master.FindControl("ErrorMessage") != null) &&
                (((Page)HttpContext.Current.CurrentHandler).Master.FindControl("InfoMessage") != null))
            {
                Literal lError = (Literal)((Page)HttpContext.Current.CurrentHandler).Master.FindControl("ErrorMessage");
                Literal lInfo = (Literal)((Page)HttpContext.Current.CurrentHandler).Master.FindControl("InfoMessage");
                if (result <= 0)
                {
                    message = GetStringMessage(Prefix_error + (result * -1).ToString());
                    if ((message != null) & (message.Trim() != ""))
                    {
                        lError.Text = String.Format(message, result, result1);
                    } else {lError.Text = String.Format(GetStringMessage("mes_err_0"),result);}
                }
                else
                {
                    message = GetStringMessage(Prefix_info);
                    if ((message != null) & (message.Trim() != ""))
                    {
                        lInfo.Text = String.Format(message, result, result1);
                    }
                    else { lInfo.Text = GetStringMessage("mes_info_0"); }
                }
            }
        }
        #endregion
    }
    #endregion

    #region КЛАСС classWeb

    [Serializable()]
    public class WebEntity
    {
        public int? ID
        {
            get;
            set;
        }
        public string Web
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public string URL
        {
            get;
            set;
        }
        public int IDUser
        {
            get;
            set;
        }
    }

    [Serializable()]
    public class WebContent : WebEntity
    {
        public string DescriptionEng
        {
            get;
            set;
        }
        public WebContent()
            : base()
        {

        }
    }

    public class classWeb : classBaseDB
    { 
        #region Поля класса classWeb
        private string _FieldWeb = "[IDWeb],[Web],[Description],[DescriptionEng],[URL],[IDUser]";
        private string _NameTableWeb = WebConfigurationManager.AppSettings["tb_Web"].ToString();

        #endregion

        #region Конструкторы класса classWeb
        public classWeb() {  }
        #endregion

        #region Общие методы 
        /// <summary>
        /// Получить название сайта
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetWeb(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDWeb") != DBNull.Value)
            {
                WebEntity we = GetCultureWeb(int.Parse(DataBinder.Eval(dataItem, "IDWeb").ToString()));
                if (we != null) { return we.Web; }
            }
            return "-";
        }
        /// <summary>
        /// Получить описание сайта с учетом культуры
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetDescriptionCulture(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDWeb") != DBNull.Value)
            {
                WebEntity we = GetCultureWeb(int.Parse(DataBinder.Eval(dataItem, "IDWeb").ToString()));
                if (we != null) { return we.Description; }
            }
            return null;
        }
        /// <summary>
        /// Получить Email владельца ресурса
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetEmailOwner(object dataItem)
        {
            classUsers cu = new classUsers(); 
            if (DataBinder.Eval(dataItem, "IDWeb") != DBNull.Value)
            {
                WebEntity we = GetCultureWeb(int.Parse(DataBinder.Eval(dataItem, "IDWeb").ToString()));
                if (we != null) {
                    UserDetali ud = cu.GetUserDetali(we.IDUser);
                    return ud.Email;
                }
            }
            return null;
        }  

        #endregion    
    
        #region Методы класса classWeb
        /// <summary>
        /// Показать все Web сайты
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectWeb()
        {
            String sql = "SELECT " + this._FieldWeb + " FROM " + this._NameTableWeb + "Order by [IDWeb]";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать поля выбранного сайта
        /// </summary>
        /// <param name="IDWeb"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectWeb(int IDWeb)
        {
            String sql = "SELECT " + this._FieldWeb + " FROM " + this._NameTableWeb + " WHERE (IDWeb = " + IDWeb.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать список сайтов с учетом культуры
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelecCulturetWeb()
        {
            String sql = "SELECT [IDWeb],[Web]" + GetCultureField("Description") + ",[URL],[IDUser]" + this._NameTableWeb + "Order by [IDWeb]";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать сайт с учетом культуры
        /// </summary>
        /// <param name="IDWeb"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelecCulturetWeb(int IDWeb)
        {
            String sql = "SELECT [IDWeb],[Web]" + GetCultureField("Description") + ",[URL],[IDUser]" + this._FieldWeb + " FROM " + this._NameTableWeb + " WHERE (IDWeb = " + IDWeb.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить WebEntity с учетом культуры
        /// </summary>
        /// <param name="IDWeb"></param>
        /// <returns></returns>
        public WebEntity GetCultureWeb(int IDWeb)
        {
            DataRow[] rows = SelecCulturetWeb(IDWeb).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDWeb"] != DBNull.Value)
                {
                    return new WebEntity()
                    {
                        //[IDWeb],[Web],[Description],[DescriptionEng],[URL],[IDUser]
                        ID = rows[0]["IDWeb"] != DBNull.Value ? rows[0]["IDWeb"] as int? : null,
                        Web = rows[0]["Web"].ToString(),
                        Description = rows[0]["Description"].ToString(),
                        URL = rows[0]["URL"].ToString(),
                        IDUser = int.Parse(rows[0]["IDUser"].ToString()),
                    };
                }
            }
            return null;
        }

        /// <summary>
        /// Загрузить выподающий список добавляет в начале выбор по всем
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectDDLWeb()
        {
            DataTable dt = SelectWeb();
            DataRow newRow = dt.NewRow();
            newRow[0] = -1;
            newRow[1] = base.GetStringResource("ddlAll");
            newRow[2] = null;
            dt.Rows.InsertAt(newRow,0);
            return dt;
        }
        ///// <summary>
        ///// Получить название сайта по ID
        ///// </summary>
        ///// <param name="IDWeb"></param>
        ///// <returns></returns>
        //public string GetWebName(int IDWeb) 
        //{
        //    DataRow[] rows = SelectWeb(IDWeb).Select();
        //    if ((rows != null) && (rows.Count() > 0))
        //    {
        //        return rows[0]["Web"].ToString().Trim();
        //    }
        //    else return "-";
        //}

        #endregion

    }
    #endregion
}