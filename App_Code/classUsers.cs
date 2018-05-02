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
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebBase
{
    /// <summary>
    /// Пакет данных пользователь Web-сервера
    /// </summary>
    public class UserDetali : IObj
    {
        private int _IDUser; 
        public int IDUser { get {return this._IDUser;} }
        private int? _IDWeb;
        public int? IDWeb { get { return this._IDWeb; } }
        private string _UserEnterprise;
        public string UserEnterprise  { get { return this._UserEnterprise; } }
        private string _Description;
        public string  Description { get { return this._Description; } }
        private string _Email;
        public string Email  { get { return this._Email; } }
        private bool _bDistribution;
        public bool bDistribution { get { return this._bDistribution; } }
        private string _Surname;
        public string  Surname { get { return this._Surname; } }
        private string _Name;
        public string  Name { get { return this._Name; } }
        private string _Patronymic;
        public string  Patronymic { get { return this._Patronymic; } }
        private string _Post;
        public string  Post { get { return this._Post; } }
        private int _IDSection;
        public int IDSection { get { return this._IDSection; } }
        public int IDObject
        {
            get { return this._IDUser; }
        }
        public string NameObject
        {
            get { return this._UserEnterprise; }
        }
        public bool isFolder
        {
            get
            {
                if ((this._IDWeb != null) & (this._IDWeb >=0))
                { return true; }
                else { return false; }
            }
        }
        public UserDetali(int IDUser, int? IDWeb, string UserEnterprise, string Description, string Email, bool bDistribution,
            string Surname, string Name, string Patronymic, string Post, int IDSection) 
        {
            this._IDUser = IDUser;
            this._IDWeb = IDWeb;
            this._UserEnterprise = UserEnterprise;
            this._Description = Description;
            this._Email = Email;
            this._bDistribution = bDistribution;
            this._Surname = Surname;
            this._Name = Name;
            this._Patronymic = Patronymic;
            this._Post = Post;
            this._IDSection = IDSection;
        }
    }
    
    #region КЛАСС УПРАВЛЕНИЯ УЧЕТНЫМИ ЗАПИСЯМИ
    public class classUsers : classBaseDB
    {
        #region Поля класса classUsers
        protected string _FieldUsers = "[IDUser],[IDWeb],[UserEnterprise],[Description],[Email],[bDistribution],[Surname],[Name],[Patronymic],[Post],[IDSection]";
        protected string _NameTableUsers;
        protected string _NameTableUsersGroup;
        protected string _NameSPInsertUser;
        protected string _NameSPUpdateUser;
        protected string _NameSPDeleteUser;
        protected string _NameSPInsertUserToGroup;
        protected string _NameSPDeleteUserToGroup;

        #endregion

        #region Конструкторы класса classUsers
        public classUsers()
        {
            this._NameTableUsers = WebConfigurationManager.AppSettings["tb_Users"].ToString();
            this._NameTableUsersGroup = WebConfigurationManager.AppSettings["tb_GroupUser"].ToString();
            this._NameSPInsertUser = WebConfigurationManager.AppSettings["sp_InsertUser"].ToString();
            this._NameSPUpdateUser = WebConfigurationManager.AppSettings["sp_UpdateUser"].ToString();
            this._NameSPDeleteUser = WebConfigurationManager.AppSettings["sp_DeleteUser"].ToString();
            this._NameSPInsertUserToGroup = WebConfigurationManager.AppSettings["sp_InsertUserToGroup"].ToString();
            this._NameSPDeleteUserToGroup = WebConfigurationManager.AppSettings["sp_DeleteUserToGroup"].ToString();
        }
        #endregion

        #region Методы класса classUsers

        #region Общие методы класса
        /// <summary>
        /// Получить ФИО
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public virtual string GetFIO(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDUser") != DBNull.Value)
            {
                UserDetali ud = GetUserDetali(int.Parse(DataBinder.Eval(dataItem, "IDUser").ToString()));
                return ud.Surname + " " + ud.Name + " " + ud.Patronymic ;
            }
            return "-";
        }
        /// <summary>
        /// Получить имя пользователя
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public virtual string GetUserEnterprise(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDUser") != DBNull.Value)
            {
                UserDetali ud = GetUserDetali(int.Parse(DataBinder.Eval(dataItem, "IDUser").ToString()));
                return ud.UserEnterprise;
            }
            return "-";
        }
        /// <summary>
        /// Вернуть Email
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public virtual string GetEmail(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDUser") != DBNull.Value)
            {
                UserDetali ud = GetUserDetali(int.Parse(DataBinder.Eval(dataItem, "IDUser").ToString()));
                return ud.Email;
            }
            return "-";
        }
        /// <summary>
        /// Определить строку выборки
        /// </summary>
        /// <param name="type"></param>
        /// <param name="IDWeb"></param>
        /// <param name="IDSection"></param>
        /// <param name="UserEnterprise"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        private string GetWhere(int type, int? IDWeb, int IDSection, string UserEnterprise, string Description) 
        {
            string sqltype = null;
            switch (type)
            {
                case 0: sqltype = null; IDWeb = null; break;
                case 1: sqltype = " AND (IDWeb is not null)"; break;
                case 2: sqltype = " AND (IDWeb is null)"; IDWeb = null; break;
            }
            return sqltype + GetWhere( IDWeb,  IDSection,  UserEnterprise,  Description);
        }
        /// <summary>
        /// Определить строку выборки
        /// </summary>
        /// <param name="IDWeb"></param>
        /// <param name="IDSection"></param>
        /// <param name="UserEnterprise"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        private string GetWhere(int? IDWeb, int IDSection, string UserEnterprise, string Description) 
        {
            string sqlweb = null;
            if ((IDWeb != null) && (IDWeb >= 0)) { sqlweb = " AND (IDWeb = " + ((int)IDWeb).ToString() + ")"; }
            string sqlSection = null;
            if (IDSection > 0) { sqlSection = " AND (IDSection = " + IDSection.ToString() + ")"; }
            string sqlUserEnterprise = null;
            if ((UserEnterprise != null) && (UserEnterprise.Trim() != "")) { sqlUserEnterprise = " AND (UserEnterprise LIKE N'%" + UserEnterprise.Trim() + "%')"; }
            string sqlDescription = null;
            if ((Description != null) && (Description.Trim() != "")) { sqlDescription = " AND (Description LIKE N'%" + Description.Trim() + "%')"; }
            return sqlweb + sqlSection + sqlUserEnterprise + sqlDescription;
        }
        #endregion

        #region Методы работы с пользователями и группами
        /// <summary>
        /// Показать всех пользователей
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectUsers()
        {
            string sql = "SELECT " + this._FieldUsers + " FROM " + this._NameTableUsers + " Order by [IDWeb]";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать пользователя по IDUser
        /// </summary>
        /// <param name="IDUser"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectUsers(int IDUser)
        {
            string sql = "SELECT " + this._FieldUsers + " FROM " + this._NameTableUsers + " WHERE (IDUser = " + IDUser.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать пользователя по UserEnterprise
        /// </summary>
        /// <param name="UserEnterprise"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectUsers(string UserEnterprise)
        {
            string sql = "SELECT " + this._FieldUsers + " FROM " + this._NameTableUsers + " WHERE ([UserEnterprise] = N'" + UserEnterprise + "')";
            return base.Select(sql);
        }


        /// <summary>
        /// Вернуть пакет данных UserDetali
        /// </summary>
        /// <param name="IDUser"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public UserDetali GetUserDetali(int IDUser)
        {
            DataRow[] rows = SelectUsers(IDUser).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDUser"] != DBNull.Value)
                {
                    int? idweb = null;
                    if (rows[0]["IDWeb"] != DBNull.Value) { idweb = int.Parse(rows[0]["IDWeb"].ToString()); }
                    return new UserDetali(
                        (int)rows[0]["IDUser"],
                        idweb,
                        rows[0]["UserEnterprise"].ToString().Trim(),
                        rows[0]["Description"].ToString().Trim(),
                        rows[0]["Email"].ToString().Trim(),
                        (bool)rows[0]["bDistribution"],
                        rows[0]["Surname"].ToString().Trim(),
                        rows[0]["Name"].ToString().Trim(),
                        rows[0]["Patronymic"].ToString().Trim(),
                        rows[0]["Post"].ToString().Trim(),
                        (int)rows[0]["IDSection"]);
                }
            }
            return null;
        }
        /// <summary>
        /// Вернуть пакет данных UserDetali
        /// </summary>
        /// <param name="UserEnterprise"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public UserDetali GetUserDetali(string UserEnterprise)
        {
            DataRow[] rows = SelectUsers(UserEnterprise).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDUser"] != DBNull.Value)
                {
                    int? idweb = null;
                    if (rows[0]["IDWeb"] != DBNull.Value) { idweb = int.Parse(rows[0]["IDWeb"].ToString()); }
                    return new UserDetali(
                        (int)rows[0]["IDUser"],
                        idweb,
                        rows[0]["UserEnterprise"].ToString().Trim(),
                        rows[0]["Description"].ToString().Trim(),
                        rows[0]["Email"].ToString().Trim(),
                        (bool)rows[0]["bDistribution"],
                        rows[0]["Surname"].ToString().Trim(),
                        rows[0]["Name"].ToString().Trim(),
                        rows[0]["Patronymic"].ToString().Trim(),
                        rows[0]["Post"].ToString().Trim(),
                        (int)rows[0]["IDSection"]);
                }
            }
            return null;
        }
        /// <summary>
        /// Получить IDUser
        /// </summary>
        /// <param name="UserEnterprise"></param>
        /// <returns></returns>
        public int? GetIDUser(string UserEnterprise)
        {
            UserDetali ud = GetUserDetali(UserEnterprise);
            if (ud != null) { return ud.IDUser; }
            return null;
        }

        /// <summary>
        /// Показать пользователей по условию выборки
        /// </summary>
        /// <param name="type"></param>
        /// <param name="IDWeb"></param>
        /// <param name="IDSection"></param>
        /// <param name="UserEnterprise"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectUsers(int type, int? IDWeb, int IDSection, string UserEnterprise, string Description)
        {

            string sql = "SELECT " + this._FieldUsers + " FROM " + this._NameTableUsers + " WHERE (IDUser is not null)" + GetWhere(type, IDWeb, IDSection, UserEnterprise, Description) + " Order by [IDWeb]";
            return base.Select(sql);
        }
        /// <summary>
        /// Метод определяет по IDUser это группа доступа (true) или пользователь (false)
        /// </summary>
        /// <param name="IDUser"></param>
        /// <returns></returns>
        public Boolean GetGroup(int IDUser)
        {
            DataRow[] rows = SelectUsers(IDUser).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDWeb"] != DBNull.Value) { return true; } else  { return false; }
            }
            else return false;
        }
        /// <summary>
        /// Получить IDWeb по IDUser 
        /// </summary>
        /// <param name="IDUser"></param>
        /// <returns></returns>
        public int GetIDWeb(int IDUser)
        {
            DataRow[] rows = SelectUsers(IDUser).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDWeb"] != DBNull.Value) { 
                    return int.Parse(rows[0]["IDWeb"].ToString()); } else 
                { return -1; }
            }
            else return -1;
        }
        /// <summary>
        /// Получить IDSection по IDUser 
        /// </summary>
        /// <param name="IDUser"></param>
        /// <returns></returns>
        public int GetIDSection(int IDUser)
        {
            DataRow[] rows = SelectUsers(IDUser).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDSection"] != DBNull.Value) { return int.Parse(rows[0]["IDSection"].ToString()); } else { return -1; }
            }
            else return -1;
        }
        /// <summary>
        /// Показать всех пользователей пренадлежащих указаной группе IDGroup
        /// </summary>
        /// <param name="IDGroup"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]          
        public DataTable SelectUsersToGroup(int IDGroup) 
        {
            string sql = "SELECT Users.IDUser, Users.IDWeb, Users.UserEnterprise, Users.Description, Users.Email, Users.bDistribution, Users.Surname, " +
                         "Users.Name, Users.Patronymic, Users.Post, Users.IDSection FROM " + this._NameTableUsersGroup + " as GroupUser INNER JOIN " + this._NameTableUsers + " as Users ON GroupUser.IDUser = Users.IDUser " +
                         "WHERE (GroupUser.IDGroup = " + IDGroup.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать всех пользователей пренадлежащих указаной группе IDGroup с дополнительной выборкой
        /// </summary>
        /// <param name="IDGroup"></param>
        /// <param name="IDSection"></param>
        /// <param name="UserEnterprise"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectUsersToGroup(int IDGroup, int IDSection, string UserEnterprise, string Description) 
        {
            string sql = "SELECT Users.IDUser, Users.IDWeb, Users.UserEnterprise, Users.Description, Users.Email, Users.bDistribution, Users.Surname, " +
                         "Users.Name, Users.Patronymic, Users.Post, Users.IDSection FROM " + this._NameTableUsersGroup + " as GroupUser INNER JOIN " + this._NameTableUsers + " as Users ON GroupUser.IDUser = Users.IDUser " +
                         "WHERE (GroupUser.IDGroup = " + IDGroup.ToString() + ")" + GetWhere(null, IDSection, UserEnterprise, Description);
            return base.Select(sql);
        }
        /// <summary>
        /// Показать все группы доступа для указаного пользователя IDUser
        /// </summary>
        /// <param name="IDUser"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]          
        public DataTable SelectGroupsToUser(int IDUser) 
        {
            string sql = "SELECT Users.IDUser, Users.IDWeb, Users.UserEnterprise, Users.Description, Users.Email, Users.bDistribution, Users.Surname, Users.Name, Users.Patronymic, Users.Post, Users.IDSection " +
                            "FROM " + this._NameTableUsersGroup + " as GroupUser INNER JOIN " + this._NameTableUsers + " as Users ON GroupUser.IDGroup = Users.IDUser WHERE (GroupUser.IDUser = " + IDUser.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать все группы доступа для указаного пользователя IDUser с дополнительной выборкой
        /// </summary>
        /// <param name="IDUser"></param>
        /// <param name="IDWeb"></param>
        /// <param name="IDSection"></param>
        /// <param name="UserEnterprise"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]          
        public DataTable SelectGroupsToUser(int IDUser, int IDWeb, int IDSection, string UserEnterprise, string Description) 
        {
            string sql = "SELECT Users.IDUser, Users.IDWeb, Users.UserEnterprise, Users.Description, Users.Email, Users.bDistribution, Users.Surname, Users.Name, Users.Patronymic, Users.Post, Users.IDSection " +
                            "FROM " + this._NameTableUsersGroup + " as GroupUser INNER JOIN " + this._NameTableUsers + " as Users ON GroupUser.IDGroup = Users.IDUser WHERE (GroupUser.IDUser = " + IDUser.ToString() + ")" + GetWhere(IDWeb, IDSection, UserEnterprise, Description);
            return base.Select(sql);
        }
        /// <summary>
        /// Показать всех пользователей непренадлежащих выбранной группе доступа
        /// </summary>
        /// <param name="IDGroup"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]          
        public DataTable SelectUsersNotGroup(int IDGroup) 
        {
            string sql = "SELECT " + this._FieldUsers + " FROM " + this._NameTableUsers + " WHERE ((IDWeb is null) AND (IDUser NOT IN (SELECT Users.IDUser FROM " + this._NameTableUsersGroup + " as GroupUser INNER JOIN " + this._NameTableUsers + " as Users ON GroupUser.IDUser = Users.IDUser " +
                         "WHERE (GroupUser.IDGroup = " + IDGroup.ToString() + "))))";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать всех пользователей непренадлежащих выбранной группе доступа с дополнительной выборкой
        /// </summary>
        /// <param name="IDGroup"></param>
        /// <param name="IDSection"></param>
        /// <param name="UserEnterprise"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectUsersNotGroup(int IDGroup, int IDSection, string UserEnterprise, string Description) 
        {
            string sql = "SELECT " + this._FieldUsers + " FROM " + this._NameTableUsers + " WHERE ((IDWeb is null) AND (IDUser NOT IN (SELECT Users.IDUser FROM " + this._NameTableUsersGroup + " as GroupUser INNER JOIN " + this._NameTableUsers + " as Users ON GroupUser.IDUser = Users.IDUser " +
                         "WHERE (GroupUser.IDGroup = " + IDGroup.ToString() + "))) " + GetWhere(null, IDSection, UserEnterprise, Description) + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать все группы непренадлежащие выбранному пользователю
        /// </summary>
        /// <param name="IDUser"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]        
        public DataTable SelectGroupsNotUser(int IDUser) 
        {
            string sql = "SELECT " + this._FieldUsers + " FROM " + this._NameTableUsers + " WHERE ((IDWeb is not null) AND (IDUser NOT IN (SELECT Users.IDUser " +
                            "FROM " + this._NameTableUsersGroup + " as GroupUser INNER JOIN " + this._NameTableUsers + " as Users ON GroupUser.IDGroup = Users.IDUser WHERE (GroupUser.IDUser = " + IDUser.ToString() + "))))";
            return base.Select(sql);        
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectGroupsNotUser(int IDUser, int IDWeb, int IDSection, string UserEnterprise, string Description) 
        {
            string sql = "SELECT " + this._FieldUsers + " FROM " + this._NameTableUsers + " WHERE ((IDWeb is not null) AND (IDUser NOT IN (SELECT Users.IDUser " +
                            "FROM " + this._NameTableUsersGroup + " as GroupUser INNER JOIN " + this._NameTableUsers + " as Users ON GroupUser.IDGroup = Users.IDUser WHERE (GroupUser.IDUser = " + IDUser.ToString() + "))) " + GetWhere(IDWeb, IDSection, UserEnterprise, Description) + ")";
            return base.Select(sql);        
        }
        /// <summary>
        /// Показать список групп или пользователей не пренадлежащих указаному объекту
        /// </summary>
        /// <param name="IDUser"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectAddGroupsUsers(int IDUser)
        {
            if (GetGroup(IDUser))
            { return SelectUsersNotGroup(IDUser); }
            else
            { return SelectGroupsNotUser(IDUser); }
        }
        /// <summary>
        /// Показать группы или пользователей принадлежащих данному пользователю или группе
        /// </summary>
        /// <param name="IDUser"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]        
        public DataTable SelectGroupUsers(int IDUser) 
        {
            if (GetGroup(IDUser))
            { return SelectUsersToGroup(IDUser); } 
            else
            { return SelectGroupsToUser(IDUser); }
        }

        /// <summary>
        /// Показать список групп или пользователей не пренадлежащих указаному объекту с дополнительной выборкой
        /// </summary>
        /// <param name="IDUser"></param>
        /// <param name="IDWeb"></param>
        /// <param name="IDSection"></param>
        /// <param name="UserEnterprise"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectAddGroupsUsers(int IDUser, int IDWeb, int IDSection, string UserEnterprise, string Description)
        {
            if (GetGroup(IDUser))
            { return SelectUsersNotGroup(IDUser, IDSection, UserEnterprise, Description); }
            else
            { return SelectGroupsNotUser(IDUser, IDWeb, IDSection, UserEnterprise, Description); }
        }
        /// <summary>
        /// Показать список групп или пользователей пренадлежащих указаному объекту с дополнительной выборкой
        /// </summary>
        /// <param name="IDUser"></param>
        /// <param name="IDWeb"></param>
        /// <param name="IDSection"></param>
        /// <param name="UserEnterprise"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectDelGroupsUsers(int IDUser, int IDWeb, int IDSection, string UserEnterprise, string Description) 
        {
            if (GetGroup(IDUser))
            { return SelectUsersToGroup(IDUser, IDSection,  UserEnterprise, Description); } 
            else
            { return SelectGroupsToUser(IDUser, IDWeb, IDSection, UserEnterprise, Description); }
        }
        /// <summary>
        /// Показать списки для добавления или удаления с дополнительной выборкой
        /// </summary>
        /// <param name="IDUser"></param>
        /// <param name="AddDel"></param>
        /// <param name="IDWeb"></param>
        /// <param name="IDSection"></param>
        /// <param name="UserEnterprise"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectAddDelGroupsUsers(int IDUser, Boolean AddDel, int IDWeb, int IDSection, string UserEnterprise, string Description) 
        {
            if (!AddDel)
            { return SelectAddGroupsUsers(IDUser, IDWeb, IDSection, UserEnterprise, Description); } 
            else {return SelectDelGroupsUsers(IDUser,IDWeb,IDSection,UserEnterprise,Description); }
        }
        /// <summary>
        /// Добавить пользователя в группу
        /// </summary>
        /// <param name="IDGroup"></param>
        /// <param name="IDUser"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public int InsertUserToGroup(int IDGroup, int IDUser, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPInsertUserToGroup, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDGroup", SqlDbType.Int));
            cmd.Parameters["@IDGroup"].Value = IDGroup;
            cmd.Parameters.Add(new SqlParameter("@IDUser", SqlDbType.Int));
            cmd.Parameters["@IDUser"].Value = IDUser;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_iusertogroup");
            }
            return Result;
        }
        /// <summary>
        /// Удалить пользователя из группы
        /// </summary>
        /// <param name="IDGroup"></param>
        /// <param name="IDUser"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int DeleteUserToGroup(int IDGroup, int IDUser, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPDeleteUserToGroup, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDGroup", SqlDbType.Int));
            cmd.Parameters["@IDGroup"].Value = IDGroup;
            cmd.Parameters.Add(new SqlParameter("@IDUser", SqlDbType.Int));
            cmd.Parameters["@IDUser"].Value = IDUser;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_dusertogroup");
            }
            return Result;
        }
        /// <summary>
        /// Добавить пользователя
        /// </summary>
        /// <param name="IDWeb"></param>
        /// <param name="UserEnterprise"></param>
        /// <param name="Description"></param>
        /// <param name="Email"></param>
        /// <param name="bDistribution"></param>
        /// <param name="Surname"></param>
        /// <param name="Name"></param>
        /// <param name="Patronymic"></param>
        /// <param name="Post"></param>
        /// <param name="IDSection"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public int InsertUser(int? IDWeb, string UserEnterprise, string  Description, string Email, bool bDistribution, 
            string Surname, string Name, string Patronymic, string Post, int IDSection, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPInsertUser, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDWeb", SqlDbType.Int));
            cmd.Parameters["@IDWeb"].Value = (IDWeb != null) ? (int)IDWeb : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@UserEnterprise", SqlDbType.NVarChar, 50));
            cmd.Parameters["@UserEnterprise"].Value = (UserEnterprise != null) ? UserEnterprise.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@Description"].Value = (Description != null) ? Description.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 150));
            cmd.Parameters["@Email"].Value = (Email != null) ? Email.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@bDistribution", SqlDbType.Bit));
            cmd.Parameters["@bDistribution"].Value = bDistribution;
            cmd.Parameters.Add(new SqlParameter("@Surname", SqlDbType.NVarChar, 50));
            cmd.Parameters["@Surname"].Value = (Surname != null) ? Surname.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50));
            cmd.Parameters["@Name"].Value = (Name != null) ? Name.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Patronymic", SqlDbType.NVarChar, 50));
            cmd.Parameters["@Patronymic"].Value = (Patronymic != null) ? Patronymic.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Post", SqlDbType.NVarChar, 250));
            cmd.Parameters["@Post"].Value = (Post != null) ? Post.Trim() : SqlString.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_iuser");
            }
            return Result;
        }
        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <param name="IDUser"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int DeleteUser(int IDUser, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPDeleteUser, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDUser", SqlDbType.Int));
            cmd.Parameters["@IDUser"].Value = IDUser;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_duser");
            }
            return Result;
        }
        /// <summary>
        /// Обновить данные по пользователю или группе доступа
        /// </summary>
        /// <param name="IDUser"></param>
        /// <param name="IDWeb"></param>
        /// <param name="UserEnterprise"></param>
        /// <param name="Description"></param>
        /// <param name="Email"></param>
        /// <param name="bDistribution"></param>
        /// <param name="Surname"></param>
        /// <param name="Name"></param>
        /// <param name="Patronymic"></param>
        /// <param name="Post"></param>
        /// <param name="IDSection"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public int UpdateUser(int IDUser, int? IDWeb, string UserEnterprise, string  Description, string Email, bool bDistribution, 
            string Surname, string Name, string Patronymic, string Post, int IDSection, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPUpdateUser, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDUser", SqlDbType.Int));
            cmd.Parameters["@IDUser"].Value = IDUser;
            cmd.Parameters.Add(new SqlParameter("@IDWeb", SqlDbType.Int));
            cmd.Parameters["@IDWeb"].Value = (IDWeb != null) ? (int)IDWeb : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@UserEnterprise", SqlDbType.NVarChar, 50));
            cmd.Parameters["@UserEnterprise"].Value = (UserEnterprise != null) ? UserEnterprise.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@Description"].Value = (Description != null) ? Description.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 150));
            cmd.Parameters["@Email"].Value = (Email != null) ? Email.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@bDistribution", SqlDbType.Bit));
            cmd.Parameters["@bDistribution"].Value = bDistribution;
            cmd.Parameters.Add(new SqlParameter("@Surname", SqlDbType.NVarChar, 50));
            cmd.Parameters["@Surname"].Value = (Surname != null) ? Surname.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50));
            cmd.Parameters["@Name"].Value = (Name != null) ? Name.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Patronymic", SqlDbType.NVarChar, 50));
            cmd.Parameters["@Patronymic"].Value = (Patronymic != null) ? Patronymic.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Post", SqlDbType.NVarChar, 250));
            cmd.Parameters["@Post"].Value = (Post != null) ? Post.Trim() : SqlString.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_uuser");
            }
            return Result;
        }
        #endregion

        #endregion

    }
    #endregion

    #region КЛАСС ПРЕДОСТАВЛЕНИЯ ДОСТУПА УЧЕТНЫМИ ЗАПИСЯМИ

    [Serializable()]
    public class AccessUsersEntity
    {
        public int? ID
        {
            get;
            set;
        }
        public int? IDUser
        {
            get;
            set;
        }

        public string UserEnterprise
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public bool bDistribution
        {
            get;
            set;
        }
        public string Surname
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Patronymic
        {
            get;
            set;
        }
        public string Post
        {
            get;
            set;
        }
        public int IDSection
        {
            get;
            set;
        }
        public DateTime DateCreate
        {
            get;
            set;
        }
        public DateTime? DateChange
        {
            get;
            set;
        }
        public DateTime? DateAccess
        {
            get;
            set;
        }
    }

    [Serializable()]
    public class AccessWebUsersEntity
    {
        public int? ID
        {
            get;
            set;
        }
        public int? IDAccessUsers
        {
            get;
            set;
        }
        public int IDWeb
        {
            get;
            set;
        }
        public bool AccessWeb
        {
            get;
            set;
        }
        public DateTime? DateRequest
        {
            get;
            set;
        }
        public DateTime? DateApproval
        {
            get;
            set;
        }
        public bool? Solution
        {
            get;
            set;
        }
        public string Coment
        {
            get;
            set;
        }
    }


    public class classAccessUsers : classUsers
    {
        #region Поля класса classAccessUsers
        protected string _FieldAccessUsers = "[IDAccessUsers],[IDUser],[UserEnterprise],[Email],[bDistribution],[Surname],[Name],[Patronymic],[Post],[IDSection],[DateCreate],[DateChange],[DateAccess]";
        protected string _FieldAccessWebUsers = "[IDAccessWebUsers],[IDAccessUsers],[IDWeb],[AccessWeb],[DateRequest],[DateApproval],[Solution],[Coment]";
        protected string _NameTableAccessUsers = WebConfigurationManager.AppSettings["tb_AccessUsers"].ToString();
        protected string _NameTableAccessWebUsers = WebConfigurationManager.AppSettings["tb_AccessWebUsers"].ToString();
        protected string _NameSPGetAccessWebUsers = WebConfigurationManager.AppSettings["sp_GetAccessWebUsers"].ToString();
        protected string _NameSPUpdateAccessUsers = WebConfigurationManager.AppSettings["sp_UpdateAccessUsers"].ToString();
        protected string _NameSPInsertAccessUsers = WebConfigurationManager.AppSettings["sp_InsertAccessUsers"].ToString();
        protected string _NameSPInsertAccessWebUsers = WebConfigurationManager.AppSettings["sp_InsertAccessWebUsers"].ToString();
        protected string _NameSPUpdateAccessWebUsers = WebConfigurationManager.AppSettings["sp_UpdateAccessWebUsers"].ToString();
        #endregion

        #region Конструкторы класса classAccessUsers
        public classAccessUsers()
            : base()
        {

        }
        #endregion

        #region Методы класса classAccessUsers

        #region Общие методы класса
        /// <summary>
        /// Получить состояние выбора запроса на доступ к сайту
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public bool GetAccessChecked(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "ID") != null)
            {
                //string d1 = DataBinder.Eval(dataItem, "ID").ToString();
                AccessWebUsersEntity awue =  GetAccessWebUsers(int.Parse(DataBinder.Eval(dataItem, "ID").ToString()));
                if (awue != null) 
                {
                    return awue.AccessWeb;
                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public bool GetAccessEnabled(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "ID") != null)
            {
                AccessWebUsersEntity awue = GetAccessWebUsers(int.Parse(DataBinder.Eval(dataItem, "ID").ToString()));
                if (awue != null) 
                {
                    if (awue.Solution != null) { return false; }
                }
            }
            return true;
        }
        /// <summary>
        /// Получить состояние заявки
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetSolution(object dataItem, string namefield)
        {
            if (namefield == null) return null;
            if (DataBinder.Eval(dataItem, namefield) != null)
            {
                AccessWebUsersEntity awue = GetAccessWebUsers(int.Parse(DataBinder.Eval(dataItem, namefield).ToString()));
                if (awue != null)
                {
                    if (awue.Solution != null)
                    {
                        if ((bool)awue.Solution) { return GetStringResource("SolutionYes"); } else { return GetStringResource("SolutionNo"); }
                    }
                    else
                    {
                        if (awue.DateRequest != null)
                        {
                            return GetStringResource("SolutionOwner");
                        }
                        else { 
                            if (awue.AccessWeb) return GetStringResource("SolutionNull"); 
                        }
                    }
                }
            }
            return null;
        }

        #endregion

        #region методы AccessUsers
        /// <summary>
        /// Показать все запросы на доступ пользователей
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectAccessUsers()
        {
            string sql = "SELECT " + this._FieldAccessUsers + " FROM " + this._NameTableAccessUsers + " Order by [IDAccessUsers]";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать выбранный запрос на доступ пользователей.
        /// </summary>
        /// <param name="IDUser"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectAccessUsers(int IDAccessUsers)
        {
            string sql = "SELECT " + this._FieldAccessUsers + " FROM " + this._NameTableAccessUsers + " WHERE ([IDAccessUsers] = " + IDAccessUsers.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать запрос пользователя
        /// </summary>
        /// <param name="IDAccessUsers"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectAccessUsersToUser(int IDUser)
        {
            string sql = "SELECT " + this._FieldAccessUsers + " FROM " + this._NameTableAccessUsers + " WHERE ([IDUser] = " + IDUser.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать активные запросы пользователей
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectActiveAccessUsers()
        {
            string sql = "SELECT " + this._FieldAccessUsers + " FROM " + this._NameTableAccessUsers + " WHERE (DateAccess IS NULL) OR (DateChange > DateAccess)";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать запросы на доступ (true-активные false-закрытые null-все)
        /// </summary>
        /// <param name="Active"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectAccessUsers(bool? Active)
        {
            string sql = "SELECT " + this._FieldAccessUsers + " FROM " + this._NameTableAccessUsers;
            if (Active!= null)  
            {
                sql +=" WHERE (DateAccess IS" + ((Active == true) ? "": " Not") +" NULL)";
            }
                sql +=" order by [DateCreate]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть AccessUsersEntity из DataRow[]
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        protected AccessUsersEntity GetAccessUsersEntity(DataRow row) 
        {
            if (row != null) 
            {
                return new AccessUsersEntity()
                {
                    ID = row["IDAccessUsers"] != DBNull.Value ? row["IDAccessUsers"] as int? : null,
                    IDUser = row["IDUser"] != DBNull.Value ? row["IDUser"] as int? : null,
                    UserEnterprise = row["UserEnterprise"].ToString(),
                    Email = row["Email"].ToString(),
                    bDistribution = bool.Parse(row["bDistribution"].ToString()),
                    Surname = row["Surname"].ToString(),
                    Name = row["Name"].ToString(),
                    Patronymic = row["Patronymic"].ToString(),
                    Post = row["Post"].ToString(),
                    IDSection = int.Parse(row["IDSection"].ToString()),
                    DateCreate = DateTime.Parse(row["DateCreate"].ToString()),
                    DateChange = row["DateChange"] != DBNull.Value ? row["DateChange"] as DateTime? : null,
                    DateAccess = row["DateAccess"] != DBNull.Value ? row["DateAccess"] as DateTime? : null,
                };            
            }
            return null;
        }
        /// <summary>
        /// Вернуть список  строку запроса на доступ для указанного пользователя
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        protected List<AccessUsersEntity> GetListAccessUsersEntit(DataRow[] rows) 
        {
            
            if ((rows != null) && (rows.Count() > 0))
            {
                List<AccessUsersEntity> list = new List<AccessUsersEntity>();
                foreach (DataRow rv in rows) 
                {
                    list.Add(GetAccessUsersEntity(rv));
                }
                return list;
            }
            return null;
        }
        /// <summary>
        /// Вернуть строку запроса на доступ для указанного пользователя
        /// </summary>
        /// <param name="IDUser"></param>
        /// <returns></returns>
        public AccessUsersEntity GetAccessUsersToUser(int? IDUser)
        {
            if (IDUser == null) return null;
            DataRow[] rows = SelectAccessUsersToUser((int)IDUser).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                return GetAccessUsersEntity(rows[0]);
            }
            return null;
        }
        /// <summary>
        /// Вернуть строку запроса на доступ
        /// </summary>
        /// <param name="IDAccessUsers"></param>
        /// <returns></returns>
        public AccessUsersEntity GetAccessUsers(int IDAccessUsers)
        {
            DataRow[] rows = SelectAccessUsers(IDAccessUsers).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                return GetAccessUsersEntity(rows[0]);
            }
            return null;            
        }
        /// <summary>
        /// Вернуть список активных запросов на доступ пользователей
        /// </summary>
        /// <returns></returns>
        public List<AccessUsersEntity> GetActiveAccessUsers() 
        {
            return GetListAccessUsersEntit(SelectActiveAccessUsers().Select());
        }
        /// <summary>
        /// Добавить строку запроса
        /// </summary>
        /// <param name="UserEnterprise"></param>
        /// <param name="Email"></param>
        /// <param name="bDistribution"></param>
        /// <param name="Surname"></param>
        /// <param name="Name"></param>
        /// <param name="Patronymic"></param>
        /// <param name="Post"></param>
        /// <param name="IDSection"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public int InsertAccessUsers(string UserEnterprise, string Email, bool bDistribution, string Surname, string Name, string Patronymic, string Post, int IDSection, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPInsertAccessUsers, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UserEnterprise", SqlDbType.NVarChar, 50));
            cmd.Parameters["@UserEnterprise"].Value = UserEnterprise;
            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 150));
            cmd.Parameters["@Email"].Value = Email;
            cmd.Parameters.Add(new SqlParameter("@bDistribution", SqlDbType.Bit));
            cmd.Parameters["@bDistribution"].Value = bDistribution;
            cmd.Parameters.Add(new SqlParameter("@Surname", SqlDbType.NVarChar, 50));
            cmd.Parameters["@Surname"].Value = Surname;
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50));
            cmd.Parameters["@Name"].Value = Name;
            cmd.Parameters.Add(new SqlParameter("@Patronymic", SqlDbType.NVarChar, 50));
            cmd.Parameters["@Patronymic"].Value = Patronymic;
            cmd.Parameters.Add(new SqlParameter("@Post", SqlDbType.NVarChar, 250));
            cmd.Parameters["@Post"].Value = Post;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_iaccessusers");
            }
            return Result;
        }
        /// <summary>
        /// Обновить строку запроса на доступ к серверу
        /// </summary>
        /// <param name="IDAccessUsers"></param>
        /// <param name="Email"></param>
        /// <param name="bDistribution"></param>
        /// <param name="Surname"></param>
        /// <param name="Name"></param>
        /// <param name="Patronymic"></param>
        /// <param name="Post"></param>
        /// <param name="IDSection"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public int UpdateAccessUsers(int IDAccessUsers, string Email, bool bDistribution, string Surname, string Name, string Patronymic, string Post, int IDSection, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPUpdateAccessUsers, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDAccessUsers", SqlDbType.Int));
            cmd.Parameters["@IDAccessUsers"].Value = IDAccessUsers;
            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 150));
            cmd.Parameters["@Email"].Value = Email;
            cmd.Parameters.Add(new SqlParameter("@bDistribution", SqlDbType.Bit));
            cmd.Parameters["@bDistribution"].Value = bDistribution;
            cmd.Parameters.Add(new SqlParameter("@Surname", SqlDbType.NVarChar, 50));
            cmd.Parameters["@Surname"].Value = Surname;
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50));
            cmd.Parameters["@Name"].Value = Name;
            cmd.Parameters.Add(new SqlParameter("@Patronymic", SqlDbType.NVarChar, 50));
            cmd.Parameters["@Patronymic"].Value = Patronymic;
            cmd.Parameters.Add(new SqlParameter("@Post", SqlDbType.NVarChar, 250));
            cmd.Parameters["@Post"].Value = Post;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_uaccessusers");
            }
            return Result;
        }
        /// <summary>
        /// Закрыть заявку
        /// </summary>
        /// <param name="IDAccessUsers"></param>
        /// <returns></returns>
        public int CloseAccessUsers(int IDAccessUsers)
        {
            string sql = "UPDATE " + this._NameTableAccessUsers + " SET [DateAccess] = GetDate() WHERE ([IDAccessUsers]=" + IDAccessUsers.ToString() + ")";
            return base.Update(sql);
        }
        #endregion

        #region методы AccessWebUsers
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectAccessWebUsers()
        {
            string sql = "SELECT " + this._FieldAccessWebUsers + " FROM " + this._NameTableAccessWebUsers + " Order by [IDAccessWebUsers]";
            return base.Select(sql);
        }
        /// <summary>
        /// Показать выбранный запрос на доступ пользователей.
        /// </summary>
        /// <param name="IDUser"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectAccessWebUsers(int IDAccessWebUsers)
        {
            string sql = "SELECT " + this._FieldAccessWebUsers + " FROM " + this._NameTableAccessWebUsers + " WHERE ([IDAccessWebUsers] = " + IDAccessWebUsers.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить перечень запросов к web-ресурсам для указаного запроса пользователя
        /// </summary>
        /// <param name="IDAccessUsers"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectAccessWebUsersToAccessUsers(int IDAccessUsers)
        {
            string sql = "SELECT " + this._FieldAccessWebUsers + " FROM " + this._NameTableAccessWebUsers + " WHERE ([IDAccessUsers] = " + IDAccessUsers.ToString() + ")";
            return base.Select(sql);
        }

        /// <summary>
        /// Обновить дату отправки запроса на согласование 
        /// </summary>
        /// <param name="IDAccessWebUsers"></param>
        /// <returns></returns>
        public int SetDateRequest(int IDAccessWebUsers)
        {
            string sql = "UPDATE " + this._NameTableAccessWebUsers + " SET [DateRequest] = GetDate() WHERE ([IDAccessWebUsers] = " + IDAccessWebUsers.ToString() + ")";
            return base.Update(sql);
        }
        /// <summary>
        /// Согласовать запрос
        /// </summary>
        /// <param name="IDAccessWebUsers"></param>
        /// <param name="Solution"></param>
        /// <param name="Coment"></param>
        /// <returns></returns>
        public int SetApproval(int IDAccessWebUsers,bool Solution, string Coment)
        {
            string sql = "UPDATE " + this._NameTableAccessWebUsers + " SET [DateApproval] = GetDate(), [Solution] = '"+Solution.ToString()+"',[Coment] = N'"+Coment+"' WHERE ([IDAccessWebUsers] = " + IDAccessWebUsers.ToString() + ")";
            return base.Update(sql);
        }
        /// <summary>
        /// Показать активные запросы на доступы к web-сайтам 
        /// </summary>
        /// <param name="IDAccessUsers"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectActiveAccessWebUsers(int IDAccessUsers)
        {
            string sql = "SELECT" + this._FieldAccessWebUsers + " FROM" + this._NameTableAccessWebUsers + " WHERE (IDAccessUsers = " + IDAccessUsers.ToString() + ") AND (AccessWeb = 1)"; //AND (DateApproval IS NULL)
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть AccessWebUsersEntity из DataRow
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        protected AccessWebUsersEntity GetAccessWebUsersEntity(DataRow row) 
        {
            if (row != null) 
            { 
                    return new AccessWebUsersEntity()
                    {
                        // [IDAccessWebUsers],[IDAccessUsers],[IDWeb],[AccessWeb],[DateRequest],[DateApproval],[Solution],[Coment]
                        ID = row["IDAccessWebUsers"] != DBNull.Value ? row["IDAccessWebUsers"] as int? : null,
                        IDAccessUsers = row["IDAccessUsers"] != DBNull.Value ? row["IDAccessUsers"] as int? : null,
                        IDWeb = int.Parse(row["IDWeb"].ToString()),
                        AccessWeb = bool.Parse(row["AccessWeb"].ToString()),
                        DateRequest = row["DateRequest"] != DBNull.Value ? row["DateRequest"] as DateTime? : null,
                        DateApproval = row["DateApproval"] != DBNull.Value ? row["DateApproval"] as DateTime? : null,
                        Solution = row["Solution"] != DBNull.Value ? row["Solution"] as bool? : null,
                        Coment = row["Coment"].ToString(),
                    };                
            }
            return null;        
        }
        /// <summary>
        /// Вернуть список AccessWebUsersEntity из DataRow[]
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        protected List<AccessWebUsersEntity> GetListAccessWebUsersEntity(DataRow[] rows) 
        {
            if ((rows != null) && (rows.Count() > 0))
            {
                List<AccessWebUsersEntity> list = new List<AccessWebUsersEntity>();
                foreach (DataRow rv in rows)
                {
                    list.Add(GetAccessWebUsersEntity(rv));
                }
                return list;
            }
            return null;
        }
        /// <summary>
        /// Вернуть строку запроса доступа к сайту
        /// </summary>
        /// <param name="IDAccessWebUsers"></param>
        /// <returns></returns>
        public AccessWebUsersEntity GetAccessWebUsers(int IDAccessWebUsers)
        {
            DataRow[] rows = SelectAccessWebUsers(IDAccessWebUsers).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                return GetAccessWebUsersEntity(rows[0]);
                //if (rows[0]["IDAccessWebUsers"] != DBNull.Value)
                //{
                //    return new AccessWebUsersEntity()
                //    {
                //        // [IDAccessWebUsers],[IDAccessUsers],[IDWeb],[AccessWeb],[DateRequest],[DateApproval],[Solution],[Coment]
                //        ID = rows[0]["IDAccessWebUsers"] != DBNull.Value ? rows[0]["IDAccessWebUsers"] as int? : null,
                //        IDAccessUsers = rows[0]["IDAccessUsers"] != DBNull.Value ? rows[0]["IDAccessUsers"] as int? : null,
                //        IDWeb = int.Parse(rows[0]["IDWeb"].ToString()),
                //        AccessWeb = bool.Parse(rows[0]["AccessWeb"].ToString()),
                //        DateRequest = rows[0]["DateRequest"] != DBNull.Value ? rows[0]["DateRequest"] as DateTime? : null,
                //        DateApproval = rows[0]["DateApproval"] != DBNull.Value ? rows[0]["DateApproval"] as DateTime? : null,
                //        Solution = rows[0]["Solution"] != DBNull.Value ? rows[0]["Solution"] as bool? : null,
                //        Coment = rows[0]["Coment"].ToString(),
                //    };
                //}
            }
            return null;
        }
        /// <summary>
        /// Вернуть список запросов на доступы к web-сайтам
        /// </summary>
        /// <param name="IDAccessUsers"></param>
        /// <returns></returns>
        public List<AccessWebUsersEntity> GetListAccessWebUsers(int? IDAccessUsers) 
        {
            List<AccessWebUsersEntity> list = new List<AccessWebUsersEntity>();
            DataRow[] rows = GetAccessWebUsers(IDAccessUsers).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                return GetListAccessWebUsersEntity(rows);
                //foreach (DataRow dr in rows)
                //{

                //    list.Add(new AccessWebUsersEntity()
                //    {
                //        // [IDAccessWebUsers],[IDAccessUsers],[IDWeb],[AccessWeb],[DateRequest],[DateApproval],[Solution],[Coment]
                //        ID = dr["IDAccessWebUsers"] != DBNull.Value ? dr["IDAccessWebUsers"] as int? : null,
                //        IDAccessUsers = dr["IDAccessUsers"] != DBNull.Value ? dr["IDAccessUsers"] as int? : null,
                //        IDWeb = int.Parse(dr["IDWeb"].ToString()),
                //        AccessWeb = bool.Parse(dr["AccessWeb"].ToString()),
                //        DateRequest = dr["DateRequest"] != DBNull.Value ? dr["DateRequest"] as DateTime? : null,
                //        DateApproval = dr["DateApproval"] != DBNull.Value ? dr["DateApproval"] as DateTime? : null,
                //        Solution = dr["Solution"] != DBNull.Value ? dr["Solution"] as bool? : null,
                //        Coment = dr["Coment"].ToString(),
                //    });
                //}
                //return list;
            }
            return null;
        }
        /// <summary>
        /// Вернуть список активных запросов на доступы к web-сайтам
        /// </summary>
        /// <param name="IDAccessUsers"></param>
        /// <returns></returns>
        public List<AccessWebUsersEntity> GetListActiveAccessWebUsers(int IDAccessUsers) 
        {
            List<AccessWebUsersEntity> list = new List<AccessWebUsersEntity>();
            DataRow[] rows = SelectActiveAccessWebUsers(IDAccessUsers).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                return GetListAccessWebUsersEntity(rows);
            }
            return null;
        }
        /// <summary>
        /// Получить запросы на доступ к сайтам
        /// </summary>
        /// <param name="IDAccessUsers"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable GetAccessWebUsers(int? IDAccessUsers)
        {
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPGetAccessWebUsers, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDAccessUsers", SqlDbType.Int));
            cmd.Parameters["@IDAccessUsers"].Value = IDAccessUsers!=null ? (int)IDAccessUsers : SqlInt32.Null;
            SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                sqlda.Fill(ds, "GetAccessWebUsers");
                return ds.Tables["GetAccessWebUsers"]; 
            }
            catch (Exception err)
            {
                log.SaveSysError(err);
            }
            finally
            {
                con.Close();
            }
            return null;
        }
        /// <summary>
        /// Добавить строку запроса на определенный web- сайт
        /// </summary>
        /// <param name="IDAccessUsers"></param>
        /// <param name="IDWeb"></param>
        /// <param name="AccessWeb"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public int InsertAccessWebUsers(int IDAccessUsers, int IDWeb, bool AccessWeb, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPInsertAccessWebUsers, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDAccessUsers", SqlDbType.Int));
            cmd.Parameters["@IDAccessUsers"].Value = IDAccessUsers;
            cmd.Parameters.Add(new SqlParameter("@IDWeb", SqlDbType.Int));
            cmd.Parameters["@IDWeb"].Value = IDWeb;
            cmd.Parameters.Add(new SqlParameter("@AccessWeb", SqlDbType.Bit));
            cmd.Parameters["@AccessWeb"].Value = AccessWeb;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_iaccesswebusers");
            }
            return Result;
        }
        /// <summary>
        /// Обновить строку зароса выбранного web-сайта
        /// </summary>
        /// <param name="IDAccessWebUsers"></param>
        /// <param name="AccessWeb"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public int UpdateAccessWebUsers(int IDAccessWebUsers, bool AccessWeb, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPUpdateAccessWebUsers, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDAccessWebUsers", SqlDbType.Int));
            cmd.Parameters["@IDAccessWebUsers"].Value = IDAccessWebUsers;
            cmd.Parameters.Add(new SqlParameter("@AccessWeb", SqlDbType.Bit));
            cmd.Parameters["@AccessWeb"].Value = AccessWeb;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_uaccesswebusers");
            }
            return Result;
        }
        #endregion

        #endregion

    }

    #endregion
}