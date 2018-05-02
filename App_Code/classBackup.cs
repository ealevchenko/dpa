using Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBase;



/// <summary>
/// Сводное описание для classBackup
/// </summary>
public class classBackup
{
    ResourceManager resourceManager = new ResourceManager(typeof(ResourceBase)); 
    protected classLog log = new classLog();

    protected String _OldconnectionString_ASPServer; // базовая строка подключения к служебному серверу ASP.Net
    protected string _ServerName = "KRR-PA-REP-SBF";
    protected string _OldServerName = "aspnetdb";
 
    private String _NameTableOldSection; 
    private String _NameTableOldUsers;
    private String _NameTableOldGroupUser;

    // базовая строка подключения к служебному серверу ASP.Net
    protected string _connectionString_ASPServer = WebConfigurationManager.ConnectionStrings["aspserver"].ConnectionString;


    // WebBase
    //------------------------------------------------
    protected string _NameTableWeb = WebConfigurationManager.AppSettings["tb_Web"].ToString();
    protected string _NameTableListSite = WebConfigurationManager.AppSettings["tb_ListSite"].ToString();
    protected string _NameTableUsers = WebConfigurationManager.AppSettings["tb_Users"].ToString();
    protected string _NameTableGroupUser = WebConfigurationManager.AppSettings["tb_GroupUser"].ToString(); 

    protected string _NameTableSection = WebConfigurationManager.AppSettings["tb_Section"].ToString();
    protected string _NameTableSiteMap = WebConfigurationManager.AppSettings["tb_SiteMap"].ToString();
    protected string _NameTableAccessSiteMap = WebConfigurationManager.AppSettings["tb_AccessSiteMap"].ToString();

    protected string _NameTableAccessUsers = WebConfigurationManager.AppSettings["tb_AccessUsers"].ToString();
    protected string _NameTableHangFireListJobs = WebConfigurationManager.AppSettings["tb_HangFireListJobs"].ToString();
    // Strategic
    //------------------------------------------------  
    protected string _NameTableTemplateStepsProject = WebConfigurationManager.AppSettings["tb_TemplateStepsProject"].ToString();
    protected string _NameTableBigStepsProject = WebConfigurationManager.AppSettings["tb_BigStepsProject"].ToString();
    protected string _NameTableStepsProject = WebConfigurationManager.AppSettings["tb_StepsProject"].ToString();
    protected string _NameTableTypeProject = WebConfigurationManager.AppSettings["tb_TypeProject"].ToString();
    protected string _NameTableMenagerProject = WebConfigurationManager.AppSettings["tb_MenagerProject"].ToString();
    protected string _NameTableListProject = WebConfigurationManager.AppSettings["tb_ListProject"].ToString();
    protected string _NameTableStepDetali = WebConfigurationManager.AppSettings["tb_StepDetali"].ToString();
    protected string _NameTableFilesStepDetali = WebConfigurationManager.AppSettings["tb_FilesStepDetali"].ToString();
    protected string _NameTableListFiles = WebConfigurationManager.AppSettings["tb_ListFiles"].ToString();
    protected string _NameTableKPI = WebConfigurationManager.AppSettings["tb_KPI"].ToString();
    protected string _NameTableKPIProject = WebConfigurationManager.AppSettings["tb_KPIProject"].ToString();
    protected string _NameTableKPIYear = WebConfigurationManager.AppSettings["tb_KPIYear"].ToString();
    protected string _NameTableIndexKPI = WebConfigurationManager.AppSettings["tb_IndexKPI"].ToString();
    protected string _NameTableTypeOrder = WebConfigurationManager.AppSettings["tb_TypeOrder"].ToString();
    protected string _NameTableListOrder = WebConfigurationManager.AppSettings["tb_ListOrder"].ToString();


    protected string _OldNameTableProject = WebConfigurationManager.AppSettings["old_tb_Project"].ToString();

    public classBackup()
    {

        this._OldconnectionString_ASPServer = WebConfigurationManager.ConnectionStrings["old_aspserver"].ConnectionString;
        this._NameTableOldSection = WebConfigurationManager.AppSettings["old_tb_Section"].ToString();
        this._NameTableOldUsers = WebConfigurationManager.AppSettings["old_tb_Users"].ToString();
        this._NameTableOldGroupUser = WebConfigurationManager.AppSettings["old_tb_GroupUser"].ToString();
    

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sw"></param>
    public void USEOLDScript(ref StreamWriter sw)
    {
        sw.WriteLine("USE ["+this._OldServerName+"]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Скрипт всавки данных Section
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    public Boolean InsertOldSectionScript(ref StreamWriter sw) 
    {
        Boolean result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._OldconnectionString_ASPServer);
        string sqlSection = "SELECT [IDSection],[Position],[Section],[SectionEng],[SectionFull],[SectionFullEng],[ParentID] FROM " + this._NameTableOldSection + " ORDER BY [IDSection]";
        SqlDataAdapter daSection = new SqlDataAdapter(sqlSection, con);
        try
        {
            daSection.Fill(ds, "Section");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним структурные подразделения");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableSection + " ON");
                sw.WriteLine("GO");
                String sParentID;
                foreach (DataRow dr in ds.Tables["Section"].Rows)
                {
                    if (dr["ParentID"] != DBNull.Value) { sParentID = dr["ParentID"].ToString().Trim(); } else { sParentID = "null"; }
                    sw.WriteLine("INSERT INTO " + this._NameTableSection + " ([IDSection],[Position],[Section],[SectionEng],[SectionFull],[SectionFullEng],[ParentID]) ");
                    sw.WriteLine("VALUES(" + dr["IDSection"].ToString().Trim() + 
                        "," + dr["Position"].ToString().Trim() + 
                        ",N'" + dr["Section"].ToString().Trim() +
                        "','" + dr["SectionEng"].ToString().Trim() + 
                        "',N'" + dr["SectionFull"].ToString().Trim() +
                        "','" + dr["SectionFullEng"].ToString().Trim() + 
                        "'," + sParentID + ")");
                }
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableSection + " OFF");
                sw.WriteLine("GO");

            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;
        
    }

    public Boolean InsertOldGroupUserScript(ref StreamWriter sw) 
    {
        Boolean result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._OldconnectionString_ASPServer);
        string sqlGroupUser = "SELECT [IDGroupUser] ,[IDGroup] ,[IDUser] FROM " + this._NameTableOldGroupUser + " ORDER BY [IDGroupUser]";
        SqlDataAdapter daGroupUser = new SqlDataAdapter(sqlGroupUser, con);
        try
        {
            daGroupUser.Fill(ds, "GroupUser");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним группы пользователей");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableGroupUser + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["GroupUser"].Rows)
                {
                    sw.WriteLine("INSERT INTO " + this._NameTableGroupUser + " ([IDGroupUser] ,[IDGroup] ,[IDUser]) ");
                    sw.WriteLine("VALUES ( " + dr["IDGroupUser"].ToString() +
                    "," + dr["IDGroup"].ToString() +
                    "," + dr["IDUser"].ToString() + ")");
                }
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableGroupUser + " OFF");
                sw.WriteLine("GO");

            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;
        
    }
    /// <summary>
    /// Скрипт вставки данных пользователей
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    public Boolean InsertOldUsersScript(ref StreamWriter sw) 
    {
        Boolean result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._OldconnectionString_ASPServer);
        string sqlUsers = "SELECT [IDUser] ,[Group] ,[UserEnterprise] ,[Description] ,[Email] ,[bDistribution] ,[Surname] ,[Name] ,[Patronymic] ,[Post] ,[Section] FROM " + this._NameTableOldUsers + " ORDER BY [IDUser]";
        SqlDataAdapter daUsers = new SqlDataAdapter(sqlUsers, con);
        try
        {
            daUsers.Fill(ds, "Users");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним пользователей сайта");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableUsers + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Users"].Rows)
                {
                    sw.WriteLine("INSERT INTO " + this._NameTableUsers + " ([IDUser], [IDWeb] ,[UserEnterprise] ,[Description] ,[Email] ,[bDistribution] ,[Surname] ,[Name] ,[Patronymic] ,[Post] ,[IDSection]) ");
                    sw.WriteLine("VALUES ( "+ dr["IDUser"].ToString() + 
                    ","+ ((dr["Group"].ToString().ToLower() == "true") ? "1" :"null" )+
                    ",N'"+ dr["UserEnterprise"].ToString().Trim()+"'"+
                    "," + (((dr["Description"] == DBNull.Value) || (dr["Description"].ToString().Trim() == "")) ? "null":"N'"+dr["Description"].ToString().Trim()+"'" )+
                    "," + (((dr["Email"] == DBNull.Value) || (dr["Email"].ToString().Trim() == "")) ? "null":"N'"+dr["Email"].ToString().Trim()+"'" )+
                    ",'"+ dr["bDistribution"].ToString() +"'"+
                    "," + (((dr["Surname"] == DBNull.Value) || (dr["Surname"].ToString().Trim() == "")) ? "null":"N'"+dr["Surname"].ToString().Trim()+"'" )+
                    "," + (((dr["Name"] == DBNull.Value) || (dr["Name"].ToString().Trim() == "")) ? "null":"N'"+dr["Name"].ToString().Trim()+"'" )+
                    "," + (((dr["Patronymic"] == DBNull.Value) || (dr["Patronymic"].ToString().Trim() == "")) ? "null":"N'"+dr["Patronymic"].ToString().Trim()+"'" )+
                    ",null,1)");
                }
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableUsers + " OFF");
                sw.WriteLine("GO");

            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;
        
    }



    /// <summary>
    /// создать скрипт переноса данных из старой таблицы [Section] в новую
    /// </summary>
    /// <returns></returns>
    public Boolean CreateScriptInsertSection()
    {
        string PatchFile = "D:\\Backup\\Section_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".sql";
        FileStream fsScr = new FileStream(@PatchFile, FileMode.Create);
        StreamWriter sw = new StreamWriter(fsScr);
        TitleScript(ref sw);
        if (!InsertOldSectionScript(ref sw)) { sw.Close(); return false; }
        sw.Flush();
        sw.Close();
        return true;
    }
    /// <summary>
    /// создать скрипт переноса данных из старой таблицы [Users] в новую
    /// </summary>
    /// <returns></returns>
    public Boolean CreateScriptInsertUsers()
    {
        string PatchFile = "D:\\Backup\\Users_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".sql";
        FileStream fsScr = new FileStream(@PatchFile, FileMode.Create);
        StreamWriter sw = new StreamWriter(fsScr);
        TitleScript(ref sw);
        if (!InsertOldUsersScript(ref sw)) { sw.Close(); return false; }
        sw.Flush();
        sw.Close();
        return true;
    }
    
    public Boolean CreateScriptInsertGroupUser()
    {
        string PatchFile = "D:\\Backup\\GroupUser_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".sql";
        FileStream fsScr = new FileStream(@PatchFile, FileMode.Create);
        StreamWriter sw = new StreamWriter(fsScr);
        TitleScript(ref sw);
        if (!InsertOldGroupUserScript(ref sw)) { sw.Close(); return false; }
        sw.Flush();
        sw.Close();
        return true;
    }

    #region Общие методы
    /// <summary>
    /// Вывести сообщение
    /// </summary>
    /// <param name="result"></param>
    /// <param name="error"></param>
    /// <param name="info"></param>
    public void OutResultText(bool result, string error, string info)
    {
        if ((((Page)HttpContext.Current.CurrentHandler).Master.FindControl("ErrorMessage") != null) &&
            (((Page)HttpContext.Current.CurrentHandler).Master.FindControl("InfoMessage") != null))
        {
            Literal lError = (Literal)((Page)HttpContext.Current.CurrentHandler).Master.FindControl("ErrorMessage");
            Literal lInfo = (Literal)((Page)HttpContext.Current.CurrentHandler).Master.FindControl("InfoMessage");
            if (!result)
            {
                lError.Text = error;
            }
            else
            {
                lInfo.Text = info;
            }
        }
    }
    /// <summary>
    /// Прочесть строку ресурса (может переопределятся в потомках)
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public virtual string GetStringResource(string key)
    {
        return resourceManager.GetString(key, CultureInfo.CurrentCulture);
    }
    /// <summary>
    /// Подпись скриптового файла
    /// </summary>
    /// <param name="sw"></param>
    public void TitleScript(ref StreamWriter sw)
    {
        // Открываем для записи файл и заносим информацию
        // Заглавие файла
        sw.WriteLine("/*");
        sw.WriteLine(" Файл создан web-сайтом dpa");
        sw.WriteLine(" Дата и время создания файла :" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
        sw.WriteLine(" Владелец файла Левченко Эдуард");
        sw.WriteLine("*/");
    }
    /// <summary>
    /// База по умолчанию
    /// </summary>
    /// <param name="sw"></param>
    public void USEScript(ref StreamWriter sw)
    {
        sw.WriteLine("USE [" + this._ServerName + "]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Запись базы по умолчанию
    /// </summary>
    /// <param name="sw"></param>
    /// <param name="use"></param>
    public void USEScript(ref StreamWriter sw, string use)
    {
        sw.WriteLine("USE [" + use.Trim() + "]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Удалить указанную таблицу
    /// </summary>
    /// <param name="sw"></param>
    /// <param name="name"></param>
    protected void DeleteTable(ref StreamWriter sw, string name) 
    {
        if (name == null) return;
        sw.WriteLine("--> Удалим таблицу '"+name.Trim()+"'");
        sw.WriteLine("IF OBJECT_ID (N'"+name.Trim()+"', N'U') IS NOT NULL");
        sw.WriteLine("BEGIN");
        sw.WriteLine("  drop table " + name.Trim() + " ");
        sw.WriteLine("END");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Удалить ключ из таблицы
    /// </summary>
    /// <param name="sw"></param>
    /// <param name="table"></param>
    /// <param name="fk"></param>
    private void DropFK(ref StreamWriter sw, string table, string fk) 
    { 
        if ((table==null) | (fk==null)) return;
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Удалим ключ '" + fk.Trim() + "'");
        sw.WriteLine("ALTER TABLE " + table.Trim() + " DROP CONSTRAINT [" + fk.Trim() + "]");
        sw.WriteLine("GO");

        //sw.WriteLine("IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].["+fk.Trim()+"]') AND parent_object_id = OBJECT_ID(N'"+table.Trim()+"'))");
        //sw.WriteLine("ALTER TABLE "+table.Trim()+" DROP CONSTRAINT ["+fk.Trim()+"]");
        //sw.WriteLine("GO");
    }
    #endregion

    #region Strategic

    #region Формирование скриптов "Шаблоны внедрения проектов"
    /// <summary>
    /// Создать скрипт восстановления "Шаблонов внедрения проектов"
    /// </summary>
    /// <returns></returns>
    public bool CreateScriptTemplateProject()
    {
        string PatchFile = "D:\\Backup\\TemplateProject_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".sql";
        FileStream fsScr = new FileStream(@PatchFile, FileMode.Create);
        StreamWriter sw = new StreamWriter(fsScr);
        TitleScript(ref sw);
        DeleteTableTemplateProject(ref sw);
        CreateTableTemplateStepsProject(ref sw);
        if (!InsertTemplateStepsProject(ref sw)) { sw.Close(); return false; }
        CreateTableBigStepsProject(ref sw);
        if (!InsertBigStepsProject(ref sw)) { sw.Close(); return false; }
        CreateTableStepsProject(ref sw);
        if (!InsertStepsProject(ref sw)) { sw.Close(); return false; }
        sw.Flush();
        sw.Close();
        return true;
    }
    /// <summary>
    /// Удалить все таблицы Шаблонов внедрения проектов
    /// </summary>
    /// <param name="sw"></param>
    protected void DeleteTableTemplateProject(ref StreamWriter sw) 
    {
        sw.WriteLine("-------------------------------------------------------------------------------------------------");
        sw.WriteLine("--> Удалим таблицы 'Шаблонов внедрения проектов'");
        DeleteTable(ref sw, this._NameTableStepsProject);
        DeleteTable(ref sw, this._NameTableBigStepsProject);
        DeleteTable(ref sw, this._NameTableTemplateStepsProject);
    }
    /// <summary>
    /// Создать таблицу TemplateStepsProject
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableTemplateStepsProject(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'TemplateStepsProject'");
        sw.WriteLine("SET ANSI_NULLS ON ");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[TemplateStepsProject](");
        sw.WriteLine(" [IDTemplateStepProject] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine(" [TemplateStep] [nvarchar](100) NOT NULL,");
        sw.WriteLine(" [TemplateStepEng] [nvarchar](100) NOT NULL,");
        sw.WriteLine(" CONSTRAINT [PK_TemplateStepProject] PRIMARY KEY CLUSTERED");
        sw.WriteLine("(");
        sw.WriteLine("	[IDTemplateStepProject] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Создать таблицу BigStepsProject
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableBigStepsProject(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'BigStepsProject'");
        sw.WriteLine("SET ANSI_NULLS ON");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[BigStepsProject](");
        sw.WriteLine("	[IDBigStep] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine("	[BigStep] [nvarchar](100) NOT NULL,");
        sw.WriteLine("	[BigStepEng] [nvarchar](100) NOT NULL,");
        sw.WriteLine(" CONSTRAINT [PK_BigStepsProject] PRIMARY KEY CLUSTERED ");
        sw.WriteLine("(");
        sw.WriteLine("	[IDBigStep] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Создать таблицу StepsProject
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableStepsProject(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'StepsProject'");
        sw.WriteLine("SET ANSI_NULLS ON");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[StepsProject](");
        sw.WriteLine("	[IDStep] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine("	[IDTemplateStepProject] [int] NOT NULL,");
        sw.WriteLine("	[IDBigStep] [int] NOT NULL,");
        sw.WriteLine("	[Step] [nvarchar](100) NOT NULL,");
        sw.WriteLine("	[StepEng] [nvarchar](100) NOT NULL,");
        sw.WriteLine(" CONSTRAINT [PK_StepsProject] PRIMARY KEY CLUSTERED ");
        sw.WriteLine("(");
        sw.WriteLine("	[IDStep] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");

        sw.WriteLine("ALTER TABLE "+this._NameTableStepsProject+"  WITH CHECK ADD  CONSTRAINT [FK_StepsProject_BigStepsProject] FOREIGN KEY([IDBigStep])");
        sw.WriteLine("REFERENCES "+this._NameTableBigStepsProject+" ([IDBigStep])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableStepsProject + " CHECK CONSTRAINT [FK_StepsProject_BigStepsProject]");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableStepsProject + "  WITH CHECK ADD  CONSTRAINT [FK_StepsProject_TemplateStepsProject] FOREIGN KEY([IDTemplateStepProject])");
        sw.WriteLine("REFERENCES " + this._NameTableTemplateStepsProject + " ([IDTemplateStepProject])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableStepsProject + " CHECK CONSTRAINT [FK_StepsProject_TemplateStepsProject]");      
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Заполнить таблицу перечнем шаблонов
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertTemplateStepsProject(ref StreamWriter sw)
    {
        bool result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDTemplateStepProject] ,[TemplateStep] ,[TemplateStepEng] FROM "+this._NameTableTemplateStepsProject+" order by [IDTemplateStepProject]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним перечень шаблонов");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableTemplateStepsProject + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    sw.WriteLine("INSERT INTO " + this._NameTableTemplateStepsProject + " ([IDTemplateStepProject] ,[TemplateStep] ,[TemplateStepEng]) ");
                    sw.WriteLine("VALUES(" + dr["IDTemplateStepProject"].ToString().Trim() +
                        ",N'" + dr["TemplateStep"].ToString().Trim() +
                        "',N'" + dr["TemplateStepEng"].ToString().Trim() + "')");
                }
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableTemplateStepsProject + " OFF");
                sw.WriteLine("GO");

            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    /// <summary>
    /// Заполнить таблицу BigStepsProject
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertBigStepsProject(ref StreamWriter sw)
    {
        bool result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDBigStep] ,[BigStep] ,[BigStepEng] FROM " + this._NameTableBigStepsProject + " order by [IDBigStep]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним перечень основных шагов");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableBigStepsProject + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    sw.WriteLine("INSERT INTO " + this._NameTableBigStepsProject + " ([IDBigStep] ,[BigStep] ,[BigStepEng]) ");
                    sw.WriteLine("VALUES(" + dr["IDBigStep"].ToString().Trim() +
                        ",N'" + dr["BigStep"].ToString().Trim() +
                        "',N'" + dr["BigStepEng"].ToString().Trim() + "')");
                }
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableBigStepsProject + " OFF");
                sw.WriteLine("GO");

            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    /// <summary>
    /// Заполнить таблицу StepsProject
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertStepsProject(ref StreamWriter sw)
    {
        bool result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDStep] ,[IDTemplateStepProject] ,[IDBigStep] ,[Step] ,[StepEng] FROM " + this._NameTableStepsProject + " order by [IDStep]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним перечень детальных шагов");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableStepsProject + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    sw.WriteLine("INSERT INTO " + this._NameTableStepsProject + " ([IDStep] ,[IDTemplateStepProject] ,[IDBigStep] ,[Step] ,[StepEng]) ");
                    sw.WriteLine("VALUES(" + dr["IDStep"].ToString().Trim() +
                        "," + dr["IDTemplateStepProject"].ToString().Trim() +
                        "," + dr["IDBigStep"].ToString().Trim() +
                        ",N'" + dr["Step"].ToString().Trim() +
                        "',N'" + dr["StepEng"].ToString().Trim() + "')");
                }
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableStepsProject + " OFF");
                sw.WriteLine("GO");

            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    #endregion

    #region Формирование скриптов "Тип проектов"
    /// <summary>
    /// Создать скрипт восстановления типов проектов
    /// </summary>
    /// <returns></returns>
    public bool CreateScriptTypeProject()
    {
        string PatchFile = "D:\\Backup\\TypeProject_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".sql";
        FileStream fsScr = new FileStream(@PatchFile, FileMode.Create);
        StreamWriter sw = new StreamWriter(fsScr);
        TitleScript(ref sw);
        DropFK(ref sw, this._NameTableListProject, "FK_ListProjects_TypeProject");
        DeleteTable(ref sw, this._NameTableTypeProject);
        CreateTableTypeProject(ref sw);
        if (!InsertTypeProject(ref sw)) { sw.Close(); return false; }
        CreateFK_ListProjects_TypeProject(ref sw);
        sw.Flush();
        sw.Close();
        return true;
    }
    /// <summary>
    /// Создать таблицу TypeProject
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableTypeProject(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'TypeProject'");
        sw.WriteLine("SET ANSI_NULLS ON");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[TypeProject](");
        sw.WriteLine("	[IDTypeProject] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine("	[TypeProject] [nvarchar](50) NOT NULL,");
        sw.WriteLine("	[TypeProjectEng] [nvarchar](50) NOT NULL,");
        sw.WriteLine("	[Description] [nvarchar](1000) NOT NULL,");        
        sw.WriteLine("	[DescriptionEng] [nvarchar](1000) NOT NULL,");
        sw.WriteLine(" CONSTRAINT [PK_TypeProject] PRIMARY KEY CLUSTERED ");
        sw.WriteLine("(");
        sw.WriteLine("	[IDTypeProject] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Заполнить данными
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertTypeProject(ref StreamWriter sw)
    {
        bool result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDTypeProject],[TypeProject],[TypeProjectEng],[Description],[DescriptionEng] FROM " + this._NameTableTypeProject + " order by [IDTypeProject]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним типы проектов");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableTypeProject + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    sw.WriteLine("INSERT INTO " + this._NameTableTypeProject + " ([IDTypeProject],[TypeProject],[TypeProjectEng],[Description],[DescriptionEng]) ");
                    sw.WriteLine("VALUES(" + dr["IDTypeProject"].ToString().Trim() +
                        ",N'" + dr["TypeProject"].ToString().Trim() +
                        "',N'" + dr["TypeProjectEng"].ToString().Trim() +
                        "',N'" + dr["Description"].ToString().Trim() +
                        "',N'" + dr["DescriptionEng"].ToString().Trim() +                         
                        "')");
                }
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableTypeProject + " OFF");
                sw.WriteLine("GO");

            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    #endregion

    #region Формирование скриптов "Списка менеджеров проектов"
    /// <summary>
    /// Создать скрипт восстановления Списка менеджеров проектов
    /// </summary>
    /// <returns></returns>
    public bool CreateScriptMenagerProject()
    {
        string PatchFile = "D:\\Backup\\MenagerProject_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".sql";
        FileStream fsScr = new FileStream(@PatchFile, FileMode.Create);
        StreamWriter sw = new StreamWriter(fsScr);
        TitleScript(ref sw);
        DropFK(ref sw, this._NameTableListProject, "FK_ListProjects_MenagerProject");
        DeleteTable(ref sw, this._NameTableMenagerProject);
        CreateTableMenagerProject(ref sw);
        if (!InsertMenagerProject(ref sw)) { sw.Close(); return false; }
        CreateFK_ListProjects_MenagerProject(ref sw);
        sw.Flush();
        sw.Close();
        return true;
    }
    /// <summary>
    /// Создать таблицу TypeProject
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableMenagerProject(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'MenagerProject'");
        sw.WriteLine("SET ANSI_NULLS ON");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[MenagerProject](");
        sw.WriteLine("        	[IDMenagerProject] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine("	[IDUser] [int] NOT NULL,");
        sw.WriteLine("	[WPhone] [int] NULL,");
        sw.WriteLine("	[MPhone] [bigint] NULL,");
        sw.WriteLine("	[SuperMenager] [bit] NOT NULL,");
        sw.WriteLine(" CONSTRAINT [PK_MenagerProject] PRIMARY KEY CLUSTERED ");
        sw.WriteLine("(");
        sw.WriteLine("	[IDMenagerProject] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableMenagerProject + " ADD  CONSTRAINT [DF_MenagerProject_SuperMenager]  DEFAULT ((0)) FOR [SuperMenager]");
        sw.WriteLine("GO");
        //sw.WriteLine("ALTER TABLE " + this._NameTableMenagerProject + "  WITH CHECK ADD  CONSTRAINT [FK_MenagerProject_Users] FOREIGN KEY([IDUser])");
        //sw.WriteLine("REFERENCES " + this._NameTableUsers + " ([IDUser])");
        //sw.WriteLine("GO");
        //sw.WriteLine("ALTER TABLE " + this._NameTableMenagerProject + " CHECK CONSTRAINT [FK_MenagerProject_Users]");
        //sw.WriteLine("GO");
        CreateFK_MenagerProject_Users(ref sw);
    }
    /// <summary>
    /// Создать ключ FK_MenagerProject_Users
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_MenagerProject_Users(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_MenagerProject_Users'");
        sw.WriteLine("ALTER TABLE " + this._NameTableMenagerProject + "  WITH CHECK ADD  CONSTRAINT [FK_MenagerProject_Users] FOREIGN KEY([IDUser])");
        sw.WriteLine("REFERENCES " + this._NameTableUsers + " ([IDUser])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableMenagerProject + " CHECK CONSTRAINT [FK_MenagerProject_Users]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Заполнить данными
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertMenagerProject(ref StreamWriter sw)
    {
        bool result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDMenagerProject],[IDUser],[WPhone],[MPhone],[SuperMenager] FROM " + this._NameTableMenagerProject + " order by [IDMenagerProject]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним менеджеров проектов");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableMenagerProject + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    string WPhone;
                    if (dr["WPhone"] != DBNull.Value) { WPhone = dr["WPhone"].ToString().Trim(); } else { WPhone = "null"; }
                    string MPhone;
                    if (dr["MPhone"] != DBNull.Value) { MPhone = dr["MPhone"].ToString().Trim(); } else { MPhone = "null"; }
                    sw.WriteLine("INSERT INTO " + this._NameTableMenagerProject + " ([IDMenagerProject],[IDUser],[WPhone],[MPhone],[SuperMenager]) ");
                    sw.WriteLine("VALUES(" + dr["IDMenagerProject"].ToString().Trim() +
                        "," + dr["IDUser"].ToString().Trim()  +
                        "," + WPhone +"," + MPhone + ",'" + dr["SuperMenager"].ToString().Trim() + "')");
                }
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableMenagerProject + " OFF");
                sw.WriteLine("GO");

            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    #endregion

    #region Формирование скриптов "Проекты ДАТП"
    /// <summary>
    /// Перенос данных на новый сервер
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertOldProjectScript(ref StreamWriter sw)
    {
        Boolean result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._OldconnectionString_ASPServer);
        string sql = "SELECT [IDProject],[IDTypeProject],[IDMenagerProject],[IDSection],[SAPCode],[TypeString],[TypeStatus],[Funding],[Currency]," +
            "[FundingDescription],[Year],[Name],[Description],[KPI],[Status],[Name_Eng],[Description_Eng],[KPI_Eng],[Change] FROM " + this._OldNameTableProject + " WHERE [IDTypeProject]<4 order by 1";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним проекты ДАТП");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableListProject + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    int menager = 0;
                    switch (int.Parse(dr["IDMenagerProject"].ToString()))
                    {
                        case 1: menager = 2; break;
                        case 2: menager = 1; break;
                        case 3: menager = 5; break;
                        case 4: menager = 3; break;
                        case 5: menager = 4; break;
                        case 6: menager = 7; break;
                        case 7: menager = 6; break;
                        case 8: menager = 7; break;
                    }
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                    string SAPCode = "null";
                    if (dr["SAPCode"] != DBNull.Value) { SAPCode = "N'" + dr["SAPCode"].ToString().Trim() + "'"; }
                    string TypeString = "null";
                    if (dr["TypeString"] != DBNull.Value) { TypeString = "N'" + dr["TypeString"].ToString().Trim() + "'"; }
                    string Funding = "null";
                    if (dr["Funding"] != DBNull.Value) { Funding = dr["Funding"].ToString().Trim(); }
                    string Currency = "null";
                    if (dr["Currency"] != DBNull.Value) { Currency = dr["Currency"].ToString().Trim(); }
                    string FundingDescription = "null";
                    if (dr["FundingDescription"] != DBNull.Value) { FundingDescription = "N'" + dr["FundingDescription"].ToString().Trim() + "'"; }
                    string Change = "null";
                    if (dr["Change"] != DBNull.Value) { Change = "'"+dr["Change"].ToString().Trim()+"'"; }

                    sw.WriteLine("INSERT INTO " + this._NameTableListProject + " ([IDProject],[IDTypeProject],[IDMenagerProject],[IDReplacementProject],[IDSection],[SAPCode],[TypeString],[TypeStatus]" +
                        ",[Funding],[Currency],[FundingDescription],[AllocationFunds],[Year],[Name],[NameEng],[Description],[DescriptionEng],[IDKPI],[Contractor],[Status],[Change]) ");
                    sw.WriteLine("VALUES ( " + dr["IDProject"].ToString() +
                                "," + dr["IDTypeProject"].ToString() +
                                "," + menager.ToString() +
                                ",null" +
                                "," + dr["IDSection"].ToString() +
                                ","+ SAPCode +
                                "," + TypeString +
                                "," + dr["TypeStatus"].ToString() +
                                "," + Funding +
                                "," + Currency +
                                "," + FundingDescription +
                                ",'False'" +
                                "," + dr["Year"].ToString() +
                                ",N'" + dr["Name"].ToString().Trim() + "'" +
                                ",N'" + dr["Name_Eng"].ToString().Trim() + "'" +
                                ",N'" + dr["Description"].ToString().Trim() + "'" +
                                ",N'" + dr["Description_Eng"].ToString().Trim() + "'" +
                                ",0" +
                                ",null" +
                                "," + dr["Status"].ToString() +
                                "," + Change + ")");
                }
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU"); 
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableListProject + " OFF");
                sw.WriteLine("GO");

            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    /// <summary>
    /// создать скрипт переноса данных из старой таблицы [Section] в новую
    /// </summary>
    /// <returns></returns>
    public bool CreateScriptInsertOldProject()
    {
        string PatchFile = "D:\\Backup\\MovingProject_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".sql";
        FileStream fsScr = new FileStream(@PatchFile, FileMode.Create);
        StreamWriter sw = new StreamWriter(fsScr);
        TitleScript(ref sw);
        if (!InsertOldProjectScript(ref sw)) { sw.Close(); return false; }
        sw.Flush();
        sw.Close();
        return true;
    }
    /// <summary>
    /// Создать скрипт восстановления "Проекты ДАТП" 
    /// </summary>
    /// <returns></returns>
    public bool CreateScriptProject()
    {
        string PatchFile = "D:\\Backup\\Project_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".sql";
        FileStream fsScr = new FileStream(@PatchFile, FileMode.Create);
        StreamWriter sw = new StreamWriter(fsScr);
        TitleScript(ref sw);
        DropFK(ref sw, this._NameTableKPIProject, "FK_KPIProject_ListProjects");
        DeleteTableProject(ref sw);
        CreateTableListProject(ref sw);
        if (!InsertListProjectScript(ref sw)) { sw.Close(); return false; }
        CreateTableStepDetali(ref sw);
        if (!InsertStepDetaliScript(ref sw)) { sw.Close(); return false; }
        CreateTableFilesStepDetali(ref sw);
        if (!InsertFilesStepDetaliScript(ref sw)) { sw.Close(); return false; }
        CreateFK_KPIProject_ListProjects(ref sw);
        sw.Flush();
        sw.Close();
        return true;
    }
    /// <summary>
    /// Удалить таблицы 'Проекты ДАТП'
    /// </summary>
    /// <param name="sw"></param>
    protected void DeleteTableProject(ref StreamWriter sw)
    {
        sw.WriteLine("-------------------------------------------------------------------------------------------------");
        sw.WriteLine("--> Удалим таблицы 'Проекты ДАТП'");
        DeleteTable(ref sw, this._NameTableFilesStepDetali);
        DeleteTable(ref sw, this._NameTableStepDetali);
        DeleteTable(ref sw, this._NameTableListProject);
    }
    /// <summary>
    /// Создать таблицу ListProjects
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableListProject(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'ListProjects'");
        sw.WriteLine("SET ANSI_NULLS ON ");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[ListProjects](");
        sw.WriteLine("  [IDProject] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine("	[IDTypeProject] [int] NOT NULL,");
        sw.WriteLine("	[IDImplementationProgram] [int] NULL,");
        sw.WriteLine("	[IDMenagerProject] [int] NOT NULL,");
        sw.WriteLine("	[IDReplacementProject] [int] NULL,");
        sw.WriteLine("	[IDSection] [int] NOT NULL,");
        sw.WriteLine("	[SAPCode] [nvarchar](50) NULL,");
        sw.WriteLine("	[TypeString] [nvarchar](50) NULL,");
        sw.WriteLine("	[TypeStatus] [int] NOT NULL,");
        sw.WriteLine("	[Funding] [money] NULL,");
        sw.WriteLine("	[Currency] [int] NULL,");
        sw.WriteLine("	[FundingDescription] [nvarchar](50) NULL,");
        sw.WriteLine("	[AllocationFunds] [bit] NOT NULL,");
        sw.WriteLine("	[LineOwner] [int] NULL,");
        sw.WriteLine("	[Year] [int] NOT NULL,");
        sw.WriteLine("	[Name] [nvarchar](1024) NOT NULL,");
        sw.WriteLine("	[NameEng] [nvarchar](1024) NOT NULL,");
        sw.WriteLine("	[Description] [nvarchar](2048) NOT NULL,");
        sw.WriteLine("	[DescriptionEng] [nvarchar](2048) NOT NULL,");
        sw.WriteLine("	[Contractor] [nvarchar](100) NULL,");
        sw.WriteLine("	[DateContractor] [nvarchar](50) NULL,");
        sw.WriteLine("	[Status] [int] NOT NULL,");
        sw.WriteLine("	[Effect] [int] NULL,");
        sw.WriteLine("  [IDOrder] [int] NULL,");
        sw.WriteLine("  [TypeConstruction] [int] NOT NULL,");
        sw.WriteLine("	[Change] [datetime] NULL,");
        sw.WriteLine(" CONSTRAINT [PK_ssd_Project] PRIMARY KEY CLUSTERED");
        sw.WriteLine("(");
        sw.WriteLine("	[IDProject] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
        CreateFK_ListProjects_MenagerProject(ref sw);
        CreateFK_ListProjects_TypeProject(ref sw);
        CreateFK_ListProjects_Section(ref sw);
    }
    /// <summary>
    /// Создать ключ FK_ListProjects_TypeProject
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_ListProjects_TypeProject(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_ListProjects_TypeProject'");
        sw.WriteLine("ALTER TABLE "+this._NameTableListProject+"  WITH CHECK ADD  CONSTRAINT [FK_ListProjects_TypeProject] FOREIGN KEY([IDTypeProject])");
        sw.WriteLine("REFERENCES " + this._NameTableTypeProject + " ([IDTypeProject])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableListProject + " CHECK CONSTRAINT [FK_ListProjects_TypeProject]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Создать ключ FK_ListProjects_MenagerProject
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_ListProjects_MenagerProject(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_ListProjects_MenagerProject'");
        sw.WriteLine("ALTER TABLE "+this._NameTableListProject+"  WITH CHECK ADD  CONSTRAINT [FK_ListProjects_MenagerProject] FOREIGN KEY([IDMenagerProject])");
        sw.WriteLine("REFERENCES " + this._NameTableMenagerProject + " ([IDMenagerProject])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableListProject + " CHECK CONSTRAINT [FK_ListProjects_MenagerProject]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_ListProjects_Section(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_ListProjects_Section'");
        sw.WriteLine("ALTER TABLE "+this._NameTableListProject+"  WITH CHECK ADD  CONSTRAINT [FK_ListProjects_Section] FOREIGN KEY([IDSection])");
        sw.WriteLine("REFERENCES " + this._NameTableSection + " ([IDSection])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableListProject + " CHECK CONSTRAINT [FK_ListProjects_Section]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Перенос данных в таблицу ListProjects
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertListProjectScript(ref StreamWriter sw)
    {
        Boolean result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDProject],[IDTypeProject],[IDImplementationProgram],[IDMenagerProject],[IDReplacementProject],[IDSection],[SAPCode],[TypeString],[TypeStatus],[Funding],[Currency],[FundingDescription]" +
        ",[AllocationFunds],[LineOwner],[Year],[Name],[NameEng],[Description],[DescriptionEng],[Contractor],[DateContractor],[Status],[Effect],[IDOrder],[TypeConstruction],[Change] FROM " + this._NameTableListProject + " ORDER by [IDProject]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним проекты ДАТП");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableListProject + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                    string SAPCode = "null";
                    if (dr["SAPCode"] != DBNull.Value) { SAPCode = "N'" + dr["SAPCode"].ToString().Trim() + "'"; }
                    string TypeString = "null";
                    if (dr["TypeString"] != DBNull.Value) { TypeString = "N'" + dr["TypeString"].ToString().Trim() + "'"; }
                    string Funding = "null";
                    if (dr["Funding"] != DBNull.Value) { Funding = dr["Funding"].ToString().Trim(); }
                    string Currency = "null";
                    if (dr["Currency"] != DBNull.Value) { Currency = dr["Currency"].ToString().Trim(); }
                    string FundingDescription = "null";
                    if (dr["FundingDescription"] != DBNull.Value) { FundingDescription = "N'" + dr["FundingDescription"].ToString().Trim() + "'"; }
                    string Change = "null";
                    if (dr["Change"] != DBNull.Value) { Change = "CONVERT(DATETIME, '" + (DateTime.Parse(dr["Change"].ToString().Trim())).ToString("yyyy-MM-dd HH:mm:ss") + "', 102)";}
                    //if (dr["Change"] != DBNull.Value) { Change = "'" + (DateTime.Parse(dr["Change"].ToString().Trim())).ToString("yyyy-MM-dd HH:mm:ss") + "'"; }
                    string Contractor = "null";
                    if (dr["Contractor"] != DBNull.Value) { Contractor = "N'" + dr["Contractor"].ToString().Trim() + "'"; }
                    string DateContractor = "null";
                    if (dr["DateContractor"] != DBNull.Value) { DateContractor = "N'" + dr["DateContractor"].ToString().Trim() + "'"; }
                    string IDReplacementProject = "null";
                    if (dr["IDReplacementProject"] != DBNull.Value) { IDReplacementProject = dr["IDReplacementProject"].ToString().Trim(); }
                    string Effect = "null";
                    if (dr["Effect"] != DBNull.Value) { Effect = dr["Effect"].ToString().Trim(); }
                    string IDOrder = "null";
                    if (dr["IDOrder"] != DBNull.Value) { IDOrder = dr["IDOrder"].ToString().Trim(); }
                    string IDImplementationProgram = "null";
                    if (dr["IDImplementationProgram"] != DBNull.Value) { IDImplementationProgram = dr["IDImplementationProgram"].ToString().Trim(); }
                    string LineOwner = "null";
                    if (dr["LineOwner"] != DBNull.Value) { LineOwner = dr["LineOwner"].ToString().Trim(); }
                    sw.WriteLine("INSERT INTO " + this._NameTableListProject + " ([IDProject],[IDTypeProject],[IDImplementationProgram],[IDMenagerProject],[IDReplacementProject],[IDSection],[SAPCode],[TypeString],[TypeStatus]" +
                        ",[Funding],[Currency],[FundingDescription],[AllocationFunds],[LineOwner],[Year],[Name],[NameEng],[Description],[DescriptionEng],[Contractor],[DateContractor],[Status],[Effect],[IDOrder],[TypeConstruction],[Change]) ");
                    sw.WriteLine("VALUES ( " + dr["IDProject"].ToString() +
                                "," + dr["IDTypeProject"].ToString() +
                                "," + IDImplementationProgram +
                                "," + dr["IDMenagerProject"].ToString() +
                                "," + IDReplacementProject +
                                "," + dr["IDSection"].ToString() +
                                "," + SAPCode +
                                "," + TypeString +
                                "," + dr["TypeStatus"].ToString() +
                                "," + Funding +
                                "," + Currency +
                                "," + FundingDescription +
                                ",'" + dr["AllocationFunds"].ToString() + "'" +
                                "," + LineOwner +
                                "," + dr["Year"].ToString() +
                                ",N'" + dr["Name"].ToString().Trim() + "'" +
                                ",N'" + dr["NameEng"].ToString().Trim() + "'" +
                                ",N'" + dr["Description"].ToString().Trim() + "'" +
                                ",N'" + dr["DescriptionEng"].ToString().Trim() + "'" +
                                "," + Contractor +
                                "," + DateContractor +
                                "," + dr["Status"].ToString() +
                                "," + Effect +
                                "," + IDOrder +
                                "," + dr["TypeConstruction"].ToString() +
                                "," + Change + ")");
                }
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableListProject + " OFF");
                sw.WriteLine("GO");

            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    /// <summary>
    /// Создать таблицу StepDetali
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableStepDetali(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'StepDetali'");
        sw.WriteLine("SET ANSI_NULLS ON ");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[StepDetali](");
        sw.WriteLine("        	[IDStepDetali] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine("	[IDProject] [int] NOT NULL,");
        sw.WriteLine("	[IDStep] [int] NOT NULL,");
        sw.WriteLine("	[Position] [int] NOT NULL,");
        sw.WriteLine("	[FactStart] [datetime] NULL,");
        sw.WriteLine("	[FactStop] [datetime] NULL,");
        sw.WriteLine("	[Persent] [int] NOT NULL,");
        sw.WriteLine("	[Coment] [nvarchar](1024) NULL,");
        sw.WriteLine("	[Responsible] [nvarchar](1024) NULL,");
        sw.WriteLine("	[SkipStep] [bit] NOT NULL,");
        sw.WriteLine("	[CurrentStep] [bit] NOT NULL,");
        sw.WriteLine(" CONSTRAINT [PK_StepDetali] PRIMARY KEY CLUSTERED ");
        sw.WriteLine("(");
        sw.WriteLine("	[IDStepDetali] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
        CreateFK_StepDetali_ListProjects(ref sw);
    }
    /// <summary>
    /// Создать ключ FK_StepDetali_ListProjects
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_StepDetali_ListProjects(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_StepDetali_ListProjects'");
        sw.WriteLine("ALTER TABLE " + this._NameTableStepDetali + "  WITH CHECK ADD  CONSTRAINT [FK_StepDetali_ListProjects] FOREIGN KEY([IDProject])");
        sw.WriteLine("REFERENCES " + this._NameTableListProject + " ([IDProject])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableStepDetali + " CHECK CONSTRAINT [FK_StepDetali_ListProjects]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Перенос данных таблицы StepDetali
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertStepDetaliScript(ref StreamWriter sw)
    {
        Boolean result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDStepDetali],[IDProject],[IDStep],[Position],[FactStart],[FactStop],[Persent],[Coment],[Responsible],[SkipStep],[CurrentStep] FROM " + this._NameTableStepDetali + " ORDER by [IDStepDetali]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним шаги выполнения проектов ДАТП");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableStepDetali + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    string FactStart = "null";
                    if (dr["FactStart"] != DBNull.Value) { FactStart = "CONVERT(DATETIME, '" + (DateTime.Parse(dr["FactStart"].ToString().Trim())).ToString("yyyy-MM-dd HH:mm:ss") + "', 102)"; }
                    //if (dr["FactStart"] != DBNull.Value) { FactStart = "'" + (DateTime.Parse(dr["FactStart"].ToString().Trim())).ToString("yyyy-MM-dd HH:mm:ss") + "'"; }
                    string FactStop = "null";
                    if (dr["FactStop"] != DBNull.Value) { FactStop = "CONVERT(DATETIME, '" + (DateTime.Parse(dr["FactStop"].ToString().Trim())).ToString("yyyy-MM-dd HH:mm:ss") + "', 102)"; }
                    //if (dr["FactStop"] != DBNull.Value) { FactStop = "'" + (DateTime.Parse(dr["FactStop"].ToString().Trim())).ToString("yyyy-MM-dd HH:mm:ss") + "'"; }
                    string Coment = "null";
                    if (dr["Coment"] != DBNull.Value) { Coment = "N'" + dr["Coment"].ToString().Trim() + "'"; }
                    string Responsible = "null";
                    if (dr["Responsible"] != DBNull.Value) { Responsible = "N'" + dr["Responsible"].ToString().Trim() + "'"; }

                    sw.WriteLine("INSERT INTO " + this._NameTableStepDetali + " ([IDStepDetali],[IDProject],[IDStep],[Position],[FactStart],[FactStop],[Persent],[Coment],[Responsible],[SkipStep],[CurrentStep]) ");
                    sw.WriteLine("VALUES ( " + dr["IDStepDetali"].ToString() +
                                "," + dr["IDProject"].ToString() +
                                "," + dr["IDStep"].ToString() +
                                "," + dr["Position"].ToString() +
                                "," + FactStart + "," + FactStop +
                                "," + dr["Persent"].ToString() +
                                "," + Coment + "," + Responsible +
                                ",'" + dr["SkipStep"].ToString() + "'" +
                                ",'" + dr["CurrentStep"].ToString() + "'" + ")");
                }
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableStepDetali + " OFF");
                sw.WriteLine("GO");
            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    /// <summary>
    /// Создать таблицу FilesStepDetali
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableFilesStepDetali(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'FilesStepDetali'");
        sw.WriteLine("SET ANSI_NULLS ON ");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[FilesStepDetali](");
        sw.WriteLine("        	[IDFileStepDetali] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine("	[IDStepDetali] [int] NOT NULL,");
        sw.WriteLine("	[IDFile] [numeric](18, 0) NOT NULL,");
        sw.WriteLine(" CONSTRAINT [PK_FilesStepDetali] PRIMARY KEY CLUSTERED ");
        sw.WriteLine("(");
        sw.WriteLine("	[IDFileStepDetali] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
        CreateFK_FilesStepDetali_ListFiles(ref sw);
        CreateFK_FilesStepDetali_StepDetali(ref sw);
    }
    /// <summary>
    /// Создать ключ FK_FilesStepDetali_ListFiles
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_FilesStepDetali_ListFiles(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_FilesStepDetali_ListFiles'");
        sw.WriteLine("ALTER TABLE " + this._NameTableFilesStepDetali + "  WITH CHECK ADD  CONSTRAINT [FK_FilesStepDetali_ListFiles] FOREIGN KEY([IDFile])");
        sw.WriteLine("REFERENCES " + this._NameTableListFiles + " ([IDFile])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableFilesStepDetali + " CHECK CONSTRAINT [FK_FilesStepDetali_ListFiles]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Создать ключ FK_FilesStepDetali_StepDetali
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_FilesStepDetali_StepDetali(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_FilesStepDetali_ListFiles'");
        sw.WriteLine("ALTER TABLE " + this._NameTableFilesStepDetali + "  WITH CHECK ADD  CONSTRAINT [FK_FilesStepDetali_StepDetali] FOREIGN KEY([IDStepDetali])");
        sw.WriteLine("REFERENCES " + this._NameTableStepDetali + " ([IDStepDetali])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableFilesStepDetali + " CHECK CONSTRAINT [FK_FilesStepDetali_StepDetali]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Заполним таблицу FilesStepDetali
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertFilesStepDetaliScript(ref StreamWriter sw)
    {
        Boolean result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDFileStepDetali],[IDStepDetali],[IDFile] FROM " + this._NameTableFilesStepDetali + " ORDER by [IDFileStepDetali]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним список файлов шагов проектов ДАТП");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableFilesStepDetali + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    sw.WriteLine("INSERT INTO " + this._NameTableFilesStepDetali + " ([IDFileStepDetali],[IDStepDetali],[IDFile]) ");
                    sw.WriteLine("VALUES ( " + dr["IDFileStepDetali"].ToString() +
                                "," + dr["IDStepDetali"].ToString() +
                                "," + dr["IDFile"].ToString() +")");
                }
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableFilesStepDetali + " OFF");
                sw.WriteLine("GO");
            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    #endregion

    #region Формирование скриптов "KPI Project"
    /// <summary>
    /// Создать скрипт восстановления "KPI Проектов" 
    /// </summary>
    /// <returns></returns>
    public bool CreateScriptKPIProject()
    {
        string PatchFile = "D:\\Backup\\KPIProject_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".sql";
        FileStream fsScr = new FileStream(@PatchFile, FileMode.Create);
        StreamWriter sw = new StreamWriter(fsScr);
        TitleScript(ref sw);
        DeleteTableKPIProject(ref sw);
        CreateTableIndexKPI(ref sw);
        if (!InsertIndexKPIScript(ref sw)) { sw.Close(); return false; }
        CreateTableKPI(ref sw);
        if (!InsertKPIScript(ref sw)) { sw.Close(); return false; }
        CreateTableKPIProject(ref sw);
        if (!InsertKPIProjecScript(ref sw)) { sw.Close(); return false; }
        CreateTableKPIYear(ref sw);
        if (!InsertKPIYearScript(ref sw)) { sw.Close(); return false; }
        sw.Flush();
        sw.Close();
        return true;
    }
    /// <summary>
    /// Удалить таблицы 'KPI Проектов'
    /// </summary>
    /// <param name="sw"></param>
    protected void DeleteTableKPIProject(ref StreamWriter sw)
    {
        sw.WriteLine("-------------------------------------------------------------------------------------------------");
        sw.WriteLine("--> Удалим таблицы 'KPI Проектов'");
        DeleteTable(ref sw, this._NameTableKPIYear);
        DeleteTable(ref sw, this._NameTableKPIProject);
        DeleteTable(ref sw, this._NameTableKPI);
        DeleteTable(ref sw, this._NameTableIndexKPI);
    }
    /// <summary>
    /// Создать таблицу показателей
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableIndexKPI(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'IndexKPI'");
        sw.WriteLine("SET ANSI_NULLS ON ");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[IndexKPI](");
        sw.WriteLine("    [IndexKPI] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine("	[IndexName] [nvarchar](64) NOT NULL,");
        sw.WriteLine("	[IndexNameEng] [nvarchar](64) NOT NULL,");
        sw.WriteLine("	[IndexDescription] [nvarchar](256) NULL,");
        sw.WriteLine("	[IndexDescriptionEng] [nvarchar](256) NULL,");
        sw.WriteLine(" CONSTRAINT [PK_IndexKPI] PRIMARY KEY CLUSTERED ");
        sw.WriteLine("(");
        sw.WriteLine("	[IndexKPI] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Перенести список показателей KPI
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertIndexKPIScript(ref StreamWriter sw)
    {
        Boolean result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IndexKPI],[IndexName],[IndexNameEng],[IndexDescription],[IndexDescriptionEng] FROM " + this._NameTableIndexKPI + " ORDER by [IndexKPI]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним список показателей KPI");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableIndexKPI + " ON");
                sw.WriteLine("GO");
                
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    string IndexDescription = "null";
                    if (dr["IndexDescription"] != DBNull.Value) {  IndexDescription = ",N'" +dr["IndexDescription"].ToString().Trim() + "'"; }
                    string IndexDescriptionEng = "null";
                    if (dr["IndexDescriptionEng"] != DBNull.Value) { IndexDescriptionEng = ",N'" + dr["IndexDescriptionEng"].ToString().Trim() + "'"; }

                    sw.WriteLine("INSERT INTO " + this._NameTableIndexKPI + " ([IndexKPI],[IndexName],[IndexNameEng],[IndexDescription],[IndexDescriptionEng]) ");
                    sw.WriteLine("VALUES ( " + dr["IndexKPI"].ToString() +
                                ",N'" + dr["IndexName"].ToString().Trim() + "'" +
                                ",N'" + dr["IndexNameEng"].ToString().Trim() + "'" +
                                "," + IndexDescription + "," + IndexDescriptionEng + ")");
                }

                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableIndexKPI + " OFF");
                sw.WriteLine("GO");

            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;
    }
    /// <summary>
    /// Создать таблицу ListProjects
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableKPI(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'KPI'");
        sw.WriteLine("SET ANSI_NULLS ON ");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[KPI](");
        sw.WriteLine("  [IDKPI] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine("	[Name] [nvarchar](1024) NOT NULL,");
        sw.WriteLine("	[NameEng] [nvarchar](1024) NOT NULL,");
        sw.WriteLine(" CONSTRAINT [PK_KPI] PRIMARY KEY CLUSTERED ");
        sw.WriteLine("(");
        sw.WriteLine("	[IDKPI] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Перенос данных в таблицу KPI
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertKPIScript(ref StreamWriter sw)
    {
        Boolean result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDKPI] ,[Name] ,[NameEng] FROM " + this._NameTableKPI + " ORDER by [IDKPI]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним список KPI");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableKPI + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    sw.WriteLine("INSERT INTO " + this._NameTableKPI + " ([IDKPI] ,[Name] ,[NameEng]) ");
                    sw.WriteLine("VALUES ( " + dr["IDKPI"].ToString() +
                                ",N'" + dr["Name"].ToString().Trim() + "'" +
                                ",N'" + dr["NameEng"].ToString().Trim() + "')");
                }

                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableKPI + " OFF");
                sw.WriteLine("GO");

            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    /// <summary>
    /// Создать таблицу KPIProject
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableKPIProject(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'KPIProject'");
        sw.WriteLine("SET ANSI_NULLS ON ");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[KPIProject](");
        sw.WriteLine("	[IDKPIProject] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine("	[IDKPI] [int] NOT NULL,");
        sw.WriteLine("	[IDProject] [int] NOT NULL,");
        sw.WriteLine("	[IndexKPI] [int] NOT NULL,");
        sw.WriteLine("	[ROI] [real] NOT NULL,");
        sw.WriteLine("	[NPV] [real] NOT NULL,");
        sw.WriteLine(" CONSTRAINT [PK_KPIProject] PRIMARY KEY CLUSTERED ");
        sw.WriteLine("(");
        sw.WriteLine("	[IDKPIProject] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
        CreateFK_KPIProject_IndexKPI(ref sw);
        CreateFK_KPIProject_KPI(ref sw);
        CreateFK_KPIProject_ListProjects(ref sw);
    }
    /// <summary>
    /// Создать ключ FK_KPIProject_KPI
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_KPIProject_KPI(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_KPIProject_KPI'");
        sw.WriteLine("ALTER TABLE " + this._NameTableKPIProject + "  WITH CHECK ADD  CONSTRAINT [FK_KPIProject_KPI] FOREIGN KEY([IDKPI])");
        sw.WriteLine("REFERENCES " + this._NameTableKPI + " ([IDKPI])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableKPIProject + " CHECK CONSTRAINT [FK_KPIProject_KPI]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Создать ключ FK_KPIProject_ListProjects
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_KPIProject_ListProjects(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_KPIProject_ListProjects'");
        sw.WriteLine("ALTER TABLE " + this._NameTableKPIProject + "  WITH CHECK ADD  CONSTRAINT [FK_KPIProject_ListProjects] FOREIGN KEY([IDProject])");
        sw.WriteLine("REFERENCES " + this._NameTableListProject + " ([IDProject])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableKPIProject + " CHECK CONSTRAINT [FK_KPIProject_ListProjects]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Создать ключ FK_KPIProject_IndexKPI
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_KPIProject_IndexKPI(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_KPIProject_IndexKPI'");
        sw.WriteLine("ALTER TABLE " + this._NameTableKPIProject + "  WITH CHECK ADD  CONSTRAINT [FK_KPIProject_IndexKPI] FOREIGN KEY([IndexKPI])");
        sw.WriteLine("REFERENCES " + this._NameTableIndexKPI + " ([IndexKPI])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableKPIProject + " CHECK CONSTRAINT [FK_KPIProject_IndexKPI]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Перенос данных список KPI проектов
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertKPIProjecScript(ref StreamWriter sw)
    {
        Boolean result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDKPIProject],[IDKPI],[IDProject],[IndexKPI],[ROI],[NPV] FROM " + this._NameTableKPIProject + " ORDER by [IDKPIProject]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним список KPI проектов");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableKPIProject + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                    sw.WriteLine("INSERT INTO " + this._NameTableKPIProject + " ([IDKPIProject],[IDKPI],[IDProject],[IndexKPI],[ROI],[NPV]) ");
                    sw.WriteLine("VALUES ( " + dr["IDKPIProject"].ToString() +
                                "," + dr["IDKPI"].ToString() +
                                "," + dr["IDProject"].ToString() +
                                "," + dr["IndexKPI"].ToString() +
                                "," + dr["ROI"].ToString() +
                                "," + dr["NPV"].ToString() +")");
                }
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableKPIProject + " OFF");
                sw.WriteLine("GO");

            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    /// <summary>
    /// Создать таблицу KPIYear
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableKPIYear(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'KPIYear'");
        sw.WriteLine("SET ANSI_NULLS ON ");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[KPIYear](");
        sw.WriteLine("	[IDKPIYear] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine("	[IDKPIProject] [int] NOT NULL,");
        sw.WriteLine("	[Year] [int] NOT NULL,");
        sw.WriteLine("	[Q1] [real] NULL,");
        sw.WriteLine("	[Q2] [real] NULL,");
        sw.WriteLine("	[Q3] [real] NULL,");
        sw.WriteLine("	[Q4] [real] NULL,");
        sw.WriteLine(" CONSTRAINT [PK_KPIYear] PRIMARY KEY CLUSTERED ");
        sw.WriteLine("(");
        sw.WriteLine("	[IDKPIYear] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
        CreateFK_KPIYear_KPIProject(ref sw);
    }
    /// <summary>
    /// Создать ключ FK_KPIYear_KPIProject
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_KPIYear_KPIProject(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_KPIYear_KPIProject'");
        sw.WriteLine("ALTER TABLE " + this._NameTableKPIYear + "  WITH CHECK ADD  CONSTRAINT [FK_KPIYear_KPIProject] FOREIGN KEY([IDKPIProject])");
        sw.WriteLine("REFERENCES " + this._NameTableKPIProject + " ([IDKPIProject])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableKPIYear + " CHECK CONSTRAINT [FK_KPIYear_KPIProject]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Перенос данных таблицы KPIYear
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertKPIYearScript(ref StreamWriter sw)
    {
        Boolean result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDKPIYear],[IDKPIProject],[Year],[Q1],[Q2],[Q3],[Q4] FROM " + this._NameTableKPIYear + " ORDER by [IDKPIYear]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним KPI по годам");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableKPIYear + " ON");
                sw.WriteLine("GO");
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    string Q1 = "null";
                    if (dr["Q1"] != DBNull.Value) { Q1 =  dr["Q1"].ToString().Trim(); }
                    string Q2 = "null";
                    if (dr["Q2"] != DBNull.Value) { Q2 =  dr["Q2"].ToString().Trim(); }
                    string Q3 = "null";
                    if (dr["Q3"] != DBNull.Value) { Q3 =  dr["Q3"].ToString().Trim(); }
                    string Q4 = "null";
                    if (dr["Q4"] != DBNull.Value) { Q4 =  dr["Q4"].ToString().Trim(); }


                    sw.WriteLine("INSERT INTO " + this._NameTableKPIYear + " ([IDKPIYear],[IDKPIProject],[Year],[Q1],[Q2],[Q3],[Q4]) ");
                    sw.WriteLine("VALUES ( " + dr["IDKPIYear"].ToString() +
                                "," + dr["IDKPIProject"].ToString() +  
                                "," + dr["Year"].ToString() +   
                                "," + Q1 + "," + Q2 + "," + Q3 + "," + Q4 + ")");
                }
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableKPIYear + " OFF");
                sw.WriteLine("GO");
            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    #endregion

    #region Формирование скриптов "Нормативная документация"
    /// <summary>
    /// Создать скрипт восстановления "НД" 
    /// </summary>
    /// <returns></returns>
    public bool CreateScriptListOrder()
    {
        string PatchFile = "D:\\Backup\\ListOrder_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".sql";
        FileStream fsScr = new FileStream(@PatchFile, FileMode.Create);
        StreamWriter sw = new StreamWriter(fsScr);
        TitleScript(ref sw);
        DeleteTableOrder(ref sw);
        CreateTableTypeOrder(ref sw);
        if (!InsertTypeOrderScript(ref sw)) { sw.Close(); return false; }
        CreateTableListOrder(ref sw);
        if (!InsertListOrderScript(ref sw)) { sw.Close(); return false; }
        sw.Flush();
        sw.Close();
        return true;
    }
    /// <summary>
    /// Удалим таблицы НД
    /// </summary>
    /// <param name="sw"></param>
    protected void DeleteTableOrder(ref StreamWriter sw)
    {
        sw.WriteLine("-------------------------------------------------------------------------------------------------");
        sw.WriteLine("--> Удалим таблицы 'Нормативная документация");
        DeleteTable(ref sw, this._NameTableListOrder);
        DeleteTable(ref sw, this._NameTableTypeOrder);
    }
    /// <summary>
    /// Создать таблицу Тип документации
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableTypeOrder(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'TypeOrder'");
        sw.WriteLine("SET ANSI_NULLS ON ");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[TypeOrder](");
        sw.WriteLine("	[IDTypeOrder] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine("	[TypeOrder] [nvarchar](512) NOT NULL,");
        sw.WriteLine("	[TypeOrderEng] [nvarchar](512) NOT NULL,");
        sw.WriteLine(" CONSTRAINT [PK_TypeOrder] PRIMARY KEY CLUSTERED ");
        sw.WriteLine("(");
        sw.WriteLine("	[IDTypeOrder] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Заполним типы документации
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertTypeOrderScript(ref StreamWriter sw)
    {
        bool result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDTypeOrder],[TypeOrder],[TypeOrderEng] FROM " + this._NameTableTypeOrder + " order by [IDTypeOrder]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним типы документации");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableTypeOrder + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    sw.WriteLine("INSERT INTO " + this._NameTableTypeOrder + " ([IDTypeOrder],[TypeOrder],[TypeOrderEng]) ");
                    sw.WriteLine("VALUES(" + dr["IDTypeOrder"].ToString().Trim() +
                        ",N'" + dr["TypeOrder"].ToString().Trim() +
                        "',N'" + dr["TypeOrderEng"].ToString().Trim() +
                        "')");
                }
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableTypeOrder + " OFF");
                sw.WriteLine("GO");

            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    /// <summary>
    /// Создать таблицу 'ListOrder'
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableListOrder(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу ListOrder");
        sw.WriteLine("SET ANSI_NULLS ON ");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[ListOrder](");
        sw.WriteLine("	[IDOrder] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine("	[IDTypeOrder] [int] NOT NULL,");
        sw.WriteLine("	[NumOrder] [int] NULL,");
        sw.WriteLine("	[DateOrder] [datetime] NULL,");
        sw.WriteLine("	[Order] [nvarchar](512) NOT NULL,");
        sw.WriteLine("	[OrderEng] [nvarchar](512) NOT NULL,");
        sw.WriteLine("	[IDFile] [int] NULL,");
        sw.WriteLine("	[IDFileEng] [int] NULL,");
        sw.WriteLine(" CONSTRAINT [PK_ListOrder] PRIMARY KEY CLUSTERED ");
        sw.WriteLine("(");
        sw.WriteLine("	[IDOrder] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
        CreateFK_ListOrder_TypeOrder(ref sw);
    }
    /// <summary>
    /// Создать ключ 'FK_ListOrder_TypeOrder'
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_ListOrder_TypeOrder(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_ListOrder_TypeOrder'");
        sw.WriteLine("ALTER TABLE " + this._NameTableListOrder + "  WITH CHECK ADD  CONSTRAINT [FK_ListOrder_TypeOrder] FOREIGN KEY([IDTypeOrder])");
        sw.WriteLine("REFERENCES " + this._NameTableTypeOrder + " ([IDTypeOrder])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableListOrder + " CHECK CONSTRAINT [FK_ListOrder_TypeOrder]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Заполним список нормативной документации
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertListOrderScript(ref StreamWriter sw)
    {
        Boolean result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDOrder],[IDTypeOrder],[NumOrder],[DateOrder],[Order],[OrderEng],[IDFile],[IDFileEng] FROM " + this._NameTableListOrder + " ORDER by [IDOrder]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним список нормативной документации");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableListOrder + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    string NumOrder = "null";
                    if (dr["NumOrder"] != DBNull.Value) { NumOrder = dr["NumOrder"].ToString().Trim(); }
                    string DateOrder = "null";
                    if (dr["DateOrder"] != DBNull.Value) {
                        DateOrder = "CONVERT(DATETIME, '" + (DateTime.Parse(dr["DateOrder"].ToString().Trim())).ToString("yyyy-MM-dd HH:mm:ss") + "', 102)";
                    }
                    //DateOrder = "'" + (DateTime.Parse(dr["DateOrder"].ToString().Trim())).ToString("yyyy-MM-dd HH:mm:ss") + "'"; }
                    string IDFile = "null";
                    if (dr["IDFile"] != DBNull.Value) { IDFile = dr["IDFile"].ToString().Trim() ; }
                    string IDFileEng = "null";
                    if (dr["IDFileEng"] != DBNull.Value) { IDFileEng = dr["IDFileEng"].ToString().Trim() ; }
                    sw.WriteLine("INSERT INTO " + this._NameTableListOrder + " ([IDOrder],[IDTypeOrder],[NumOrder],[DateOrder],[Order],[OrderEng],[IDFile],[IDFileEng] ) ");
                    sw.WriteLine("VALUES ( " + dr["IDOrder"].ToString() +
                                "," + dr["IDTypeOrder"].ToString() +
                                "," + NumOrder + "," + DateOrder +
                                ",N'" + dr["Order"].ToString().Trim() +
                                "',N'" + dr["OrderEng"].ToString().Trim() +
                                "'," + IDFile + "," + IDFileEng + ")");
                }
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableListOrder + " OFF");
                sw.WriteLine("GO");
            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    #endregion

    #endregion

    #region WebBase

    #region Формирование скриптов "Web"
    /// <summary>
    /// Создать скрипт восстановления списка Web сайтов
    /// </summary>
    /// <returns></returns>
    public bool CreateScriptWeb()
    {
        string PatchFile = "D:\\Backup\\Web_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".sql";
        FileStream fsScr = new FileStream(@PatchFile, FileMode.Create);
        StreamWriter sw = new StreamWriter(fsScr);
        TitleScript(ref sw);
        DropFK(ref sw, this._NameTableSiteMap, "FK_SiteMap_Web");
        DeleteTable(ref sw, this._NameTableWeb);
        CreateTableWeb(ref sw);
        if (!InsertWeb(ref sw)) { sw.Close(); return false; }
        CreateFK_SiteMap_Web(ref sw);
        sw.Flush();
        sw.Close();
        return true;
    }
    /// <summary>
    /// Создать таблицу Web
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableWeb(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'Web'");
        sw.WriteLine("SET ANSI_NULLS ON");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[Web](");
        sw.WriteLine("    [IDWeb] [int] NOT NULL,");
        sw.WriteLine("    [Web] [nvarchar](50) NOT NULL,");
        sw.WriteLine("	[Description] [nvarchar](256) NOT NULL,");
        sw.WriteLine("	[DescriptionEng] [nvarchar](256) NOT NULL,");
        sw.WriteLine("	[URL] [nvarchar](1024) NOT NULL,");
        sw.WriteLine("	[IDUser] [int] NOT NULL,");
        sw.WriteLine(" CONSTRAINT [PK_Web] PRIMARY KEY CLUSTERED ");
        sw.WriteLine("(");
        sw.WriteLine("	[IDWeb] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
        CreateFK_Web_Users(ref sw);
    }
    /// <summary>
    /// Создать ключ FK_Web_Users
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_Web_Users(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_Web_Users'");
        sw.WriteLine("ALTER TABLE " + this._NameTableWeb + "  WITH CHECK ADD  CONSTRAINT [FK_Web_Users] FOREIGN KEY([IDUser])");
        sw.WriteLine("REFERENCES " + this._NameTableUsers + " ([IDUser])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableWeb + " CHECK CONSTRAINT [FK_Web_Users]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Заполнить данными
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertWeb(ref StreamWriter sw)
    {
        bool result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDWeb],[Web],[Description],[DescriptionEng],[URL],[IDUser] FROM " + this._NameTableWeb + " order by [IDWeb]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним список Web сайтов");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    sw.WriteLine("INSERT INTO " + this._NameTableWeb + " ([IDWeb],[Web],[Description],[DescriptionEng],[URL],[IDUser]) ");
                    sw.WriteLine("VALUES(" + dr["IDWeb"].ToString().Trim() +
                        ",N'" + dr["Web"].ToString().Trim() + "'" +
                        ",N'" + dr["Description"].ToString().Trim() + "'" +
                        ",N'" + dr["DescriptionEng"].ToString().Trim() + "'" +
                        ",N'" + dr["URL"].ToString().Trim() + "'" +
                        "," + dr["IDUser"].ToString().Trim() + ")");
                }
                sw.WriteLine("GO");
            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    #endregion

    #region Формирование скриптов "Список Web-страниц"
    /// <summary>
    /// Создать скрипт восстановления списка Web-страниц
    /// </summary>
    /// <returns></returns>
    public bool CreateScriptListSite()
    {
        string PatchFile = "D:\\Backup\\ListSite_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".sql";
        FileStream fsScr = new FileStream(@PatchFile, FileMode.Create);
        StreamWriter sw = new StreamWriter(fsScr);
        TitleScript(ref sw);
        DropFK(ref sw, this._NameTableSiteMap, "FK_SiteMap_ListSite");
        DeleteTable(ref sw, this._NameTableListSite);
        CreateTableListSite(ref sw);
        if (!InsertListSite(ref sw)) { sw.Close(); return false; }
        CreateFK_SiteMap_ListSite(ref sw);
        sw.Flush();
        sw.Close();
        return true;
    }
    /// <summary>
    /// Создать таблицу ListSite
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableListSite(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'ListSite'");
        sw.WriteLine("SET ANSI_NULLS ON");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[ListSite](");
        sw.WriteLine("	[IDSite] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine("	[URL] [nvarchar](1000) NOT NULL,");
        sw.WriteLine("	[Description] [nvarchar](1000) NOT NULL,");
        sw.WriteLine("	[DescriptionEng] [nvarchar](1000) NOT NULL,");
        sw.WriteLine("	[URLHelp] [nvarchar](1000) NULL,");
        sw.WriteLine(" CONSTRAINT [PK_tr_ListSite] PRIMARY KEY CLUSTERED");
        sw.WriteLine("(");
        sw.WriteLine("	[IDSite] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Заполним таблицу
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertListSite(ref StreamWriter sw)
    {
        bool result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDSite],[URL],[Description],[DescriptionEng],[URLHelp] FROM " + this._NameTableListSite + " order by [IDSite]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним список web-станиц");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableListSite + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    string URLHelp = "null";
                    if (dr["URLHelp"] != DBNull.Value) { URLHelp = "N'"+ dr["URLHelp"].ToString().Trim() +"'"; }
                    sw.WriteLine("INSERT INTO " + this._NameTableListSite + " ([IDSite],[URL],[Description],[DescriptionEng],[URLHelp]) ");
                    sw.WriteLine("VALUES(" + dr["IDSite"].ToString().Trim() +
                        ",N'" + dr["URL"].ToString().Trim() + "'" +
                        ",N'" + dr["Description"].ToString().Trim() + "'" +
                        ",N'" + dr["DescriptionEng"].ToString().Trim() + "'" +
                        "," + URLHelp + ")");
                }
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableListSite + " OFF");
                sw.WriteLine("GO");

            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    #endregion

    #region Формирование скриптов "Структурные подразделения"
    /// <summary>
    /// Создать скрипт восстановления Структурных подразделений
    /// </summary>
    /// <returns></returns>
    public bool CreateScriptSection()
    {
        string PatchFile = "D:\\Backup\\Section_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".sql";
        FileStream fsScr = new FileStream(@PatchFile, FileMode.Create);
        StreamWriter sw = new StreamWriter(fsScr);
        TitleScript(ref sw);
        DropFK(ref sw, this._NameTableSiteMap, "FK_SiteMap_Section");
        DropFK(ref sw, this._NameTableListProject, "FK_ListProjects_Section");
        DropFK(ref sw, this._NameTableUsers, "FK_Users_Section");
        DropFK(ref sw, this._NameTableAccessUsers, "FK_AccessUsers_Section");
        DeleteTable(ref sw, this._NameTableSection);
        CreateTableSection(ref sw);
        if (!InsertSection(ref sw)) { sw.Close(); return false; }
        CreateFK_SiteMap_Section(ref sw);
        CreateFK_ListProjects_Section(ref sw);
        CreateFK_Users_Section(ref sw);
        CreateFK_AccessUsers_Section(ref sw);
        sw.Flush();
        sw.Close();
        return true;
    }
    /// <summary>
    /// Создать таблицу Section
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableSection(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'Section'");
        sw.WriteLine("SET ANSI_NULLS ON");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[Section](");
        sw.WriteLine("    [IDSection] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine("	[Position] [int] NOT NULL,");
        sw.WriteLine("	[Section] [nvarchar](100) NOT NULL,");
        sw.WriteLine("	[SectionEng] [nvarchar](100) NOT NULL,");
        sw.WriteLine("	[SectionFull] [nvarchar](1000) NOT NULL,");
        sw.WriteLine("	[SectionFullEng] [nvarchar](1000) NOT NULL,");
        sw.WriteLine("	[TypeSection] [int]  NOT NULL,");
        sw.WriteLine("  [Cipher] [int] NULL,");
        sw.WriteLine("	[ParentID] [int] NULL,");
        sw.WriteLine(" CONSTRAINT [PK_tr_Section] PRIMARY KEY CLUSTERED ");
        sw.WriteLine("(");
        sw.WriteLine("	[IDSection] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Заполнить данными
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertSection(ref StreamWriter sw)
    {
        bool result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDSection],[Position],[Section],[SectionEng],[SectionFull],[SectionFullEng],[TypeSection],[Cipher],[ParentID] FROM " + this._NameTableSection + " order by [IDSection]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним структурные подразделения");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableSection + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    string ParentID = "null";
                    if (dr["ParentID"] != DBNull.Value) { ParentID = dr["ParentID"].ToString().Trim(); }
                    string Cipher = "null";
                    if (dr["Cipher"] != DBNull.Value) { Cipher = dr["Cipher"].ToString().Trim(); }

                    sw.WriteLine("INSERT INTO " + this._NameTableSection + " ([IDSection],[Position],[Section],[SectionEng],[SectionFull],[SectionFullEng],[TypeSection],[Cipher],[ParentID]) ");
                    sw.WriteLine("VALUES(" + dr["IDSection"].ToString().Trim() +
                        "," + dr["Position"].ToString().Trim() +
                        ",N'" + dr["Section"].ToString().Trim() +
                        "',N'" + dr["SectionEng"].ToString().Trim() +
                        "',N'" + dr["SectionFull"].ToString().Trim() +
                        "',N'" + dr["SectionFullEng"].ToString().Trim() + "'" +
                        "," + dr["TypeSection"].ToString().Trim() +
                        "," + Cipher +
                        "," + ParentID + ")");
                }
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableSection + " OFF");
                sw.WriteLine("GO");

            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    #endregion

    #region Формирование скриптов "Карта сайта и доступ к сайтам"
    /// <summary>
    /// Создать скрипт восстановления "Карта сайта и доступ к сайтам" 
    /// </summary>
    /// <returns></returns>
    public bool CreateScriptSiteMapAndAccess()
    {
        string PatchFile = "D:\\Backup\\SiteMapAndAccess_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".sql";
        FileStream fsScr = new FileStream(@PatchFile, FileMode.Create);
        StreamWriter sw = new StreamWriter(fsScr);
        TitleScript(ref sw);
        DeleteTableSiteMapAndAccess(ref sw);
        CreateTableSiteMap(ref sw);
        if (!InsertSiteMap(ref sw)) { sw.Close(); return false; }
        CreateTableAccessSiteMap(ref sw);
        if (!InsertAccessSiteMap(ref sw)) { sw.Close(); return false; }
        sw.Flush();
        sw.Close();
        return true;
    }
    /// <summary>
    /// Удалить таблицы 'Карты сайта и доступа к сайтам'
    /// </summary>
    /// <param name="sw"></param>
    protected void DeleteTableSiteMapAndAccess(ref StreamWriter sw)
    {
        sw.WriteLine("--------------------------------------------------------------------------------------------");
        sw.WriteLine("--> Удалим таблицы 'Карты сайта и доступа к сайтам'");
        DeleteTable(ref sw, this._NameTableAccessSiteMap);
        DeleteTable(ref sw, this._NameTableSiteMap);
    }
    /// <summary>
    /// Создать таблицу SiteMap
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableSiteMap(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'SiteMap'");
        sw.WriteLine("SET ANSI_NULLS ON");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[SiteMap](");
        sw.WriteLine("    [IDSiteMap] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine("	[IDWeb] [int] NOT NULL,");
        sw.WriteLine("	[Position] [int] NOT NULL,");
        sw.WriteLine("	[IDSite] [int] NULL,");
        sw.WriteLine("	[Title] [nvarchar](250) NOT NULL,");
        sw.WriteLine("	[TitleEng] [nvarchar](250) NOT NULL,");
        sw.WriteLine("	[Description] [nvarchar](1000) NOT NULL,");
        sw.WriteLine("	[DescriptionEng] [nvarchar](1000) NOT NULL,");
        sw.WriteLine("	[Protection] [bit] NOT NULL,");
        sw.WriteLine("	[PageProcessor] [bit] NOT NULL,");
        sw.WriteLine("	[ParentID] [int] NULL,");
        sw.WriteLine("	[IDSection] [int] NOT NULL,");
        sw.WriteLine("	[AccessRulesSection] [nvarchar](1000) NULL,");
        sw.WriteLine(" CONSTRAINT [PK_tr_SiteMap] PRIMARY KEY CLUSTERED ");
        sw.WriteLine("(");
        sw.WriteLine("	[IDSiteMap] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
        CreateFK_SiteMap_ListSite(ref sw);
        CreateFK_SiteMap_Section(ref sw);
        CreateFK_SiteMap_Web(ref sw);
    }
    /// <summary>
    /// Создать ключ FK_SiteMap_Section
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_SiteMap_Section(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_SiteMap_Section'");
        sw.WriteLine("ALTER TABLE " + this._NameTableSiteMap + "  WITH CHECK ADD  CONSTRAINT [FK_SiteMap_Section] FOREIGN KEY([IDSection])");
        sw.WriteLine("REFERENCES " + this._NameTableSection + " ([IDSection])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableSiteMap + " CHECK CONSTRAINT [FK_SiteMap_Section]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Создать ключ FK_SiteMap_Web
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_SiteMap_Web(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_SiteMap_Web'");
        sw.WriteLine("ALTER TABLE " + this._NameTableSiteMap + "  WITH CHECK ADD  CONSTRAINT [FK_SiteMap_Web] FOREIGN KEY([IDWeb])");
        sw.WriteLine("REFERENCES " + this._NameTableWeb + " ([IDWeb])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableSiteMap + " CHECK CONSTRAINT [FK_SiteMap_Web]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Создать ключ FK_SiteMap_ListSite
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_SiteMap_ListSite(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_SiteMap_ListSite'");
        sw.WriteLine("ALTER TABLE " + this._NameTableSiteMap + "  WITH CHECK ADD  CONSTRAINT [FK_SiteMap_ListSite] FOREIGN KEY([IDSite])");
        sw.WriteLine("REFERENCES " + this._NameTableListSite + " ([IDSite])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableSiteMap + " CHECK CONSTRAINT [FK_SiteMap_ListSite]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Заполнить данными
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertSiteMap(ref StreamWriter sw)
    {
        bool result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDSiteMap],[IDWeb],[Position],[IDSite],[Title],[TitleEng],[Description],[DescriptionEng],[Protection],[PageProcessor],[ParentID],[IDSection],[AccessRulesSection] FROM " + this._NameTableSiteMap + " order by [IDSiteMap]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним карту сайта");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableSiteMap + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    string IDSite = "null";
                    if (dr["IDSite"] != DBNull.Value) { IDSite = dr["IDSite"].ToString().Trim(); }
                    string ParentID = "null";
                    if (dr["ParentID"] != DBNull.Value) { ParentID = dr["ParentID"].ToString().Trim(); }
                    string AccessRulesSection = "null";
                    if (dr["AccessRulesSection"] != DBNull.Value) { AccessRulesSection = "N'" + dr["AccessRulesSection"].ToString().Trim() + "'"; }

                    sw.WriteLine("INSERT INTO " + this._NameTableSiteMap + " ([IDSiteMap],[IDWeb],[Position],[IDSite],[Title],[TitleEng],[Description],[DescriptionEng],[Protection],[PageProcessor],[ParentID],[IDSection],[AccessRulesSection]) ");
                    sw.WriteLine("VALUES(" + dr["IDSiteMap"].ToString().Trim() +
                        "," + dr["IDWeb"].ToString().Trim() +
                        "," + dr["Position"].ToString().Trim() +
                        "," + IDSite +
                        ",N'" + dr["Title"].ToString().Trim() + "'" +
                        ",N'" + dr["TitleEng"].ToString().Trim() + "'" +
                        ",N'" + dr["Description"].ToString().Trim() + "'" +
                        ",N'" + dr["DescriptionEng"].ToString().Trim() + "'" +
                        ",'" + @dr["Protection"].ToString().Trim() + "'" +
                        ",'" + @dr["PageProcessor"].ToString().Trim() + "'" +
                        "," + ParentID +
                        "," + dr["IDSection"].ToString().Trim() +
                        "," + AccessRulesSection + ")");
                }
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableSiteMap + " OFF");
                sw.WriteLine("GO");
            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    /// <summary>
    /// Создать таблицу AccessSiteMap
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableAccessSiteMap(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'AccessSiteMap'");
        sw.WriteLine("SET ANSI_NULLS ON");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[AccessSiteMap](");
        sw.WriteLine("  [IDAccessSiteMap] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine("	[IDUsers] [int] NOT NULL,");
        sw.WriteLine("	[IDSiteMap] [int] NOT NULL,");
        sw.WriteLine("	[Access] [int] NOT NULL,");
        sw.WriteLine("	[AccessRules] [nvarchar](1000) NULL,");
        sw.WriteLine(" CONSTRAINT [PK_tr_AccessSiteMap] PRIMARY KEY CLUSTERED");
        sw.WriteLine("(");
        sw.WriteLine("	[IDAccessSiteMap] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
        CreateFK_AccessSiteMap_Users(ref sw);
        CreateFK_AccessSiteMap_SiteMap(ref sw);
    }
    /// <summary>
    /// Создать ключ FK_AccessSiteMap_Users
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_AccessSiteMap_Users(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_AccessSiteMap_Users'");
        sw.WriteLine("ALTER TABLE " + this._NameTableAccessSiteMap + "  WITH CHECK ADD  CONSTRAINT [FK_AccessSiteMap_Users] FOREIGN KEY([IDUsers])");
        sw.WriteLine("REFERENCES " + this._NameTableUsers + " ([IDUser])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableAccessSiteMap + " CHECK CONSTRAINT [FK_AccessSiteMap_Users]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Создать ключ FK_AccessSiteMap_SiteMap
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_AccessSiteMap_SiteMap(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_AccessSiteMap_SiteMap'");
        sw.WriteLine("ALTER TABLE " + this._NameTableAccessSiteMap + "  WITH CHECK ADD  CONSTRAINT [FK_AccessSiteMap_SiteMap] FOREIGN KEY([IDSiteMap])");
        sw.WriteLine("REFERENCES " + this._NameTableSiteMap + " ([IDSiteMap])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableAccessSiteMap + " CHECK CONSTRAINT [FK_AccessSiteMap_SiteMap]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Заполним доступ к сайтам
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertAccessSiteMap(ref StreamWriter sw)
    {
        bool result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDAccessSiteMap],[IDUsers],[IDSiteMap],[Access],[AccessRules] FROM " + this._NameTableAccessSiteMap + " order by [IDAccessSiteMap]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним доступ к сайтам");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableAccessSiteMap + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    string AccessRules = "null";
                    if (dr["AccessRules"] != DBNull.Value) { AccessRules = "N'" + dr["AccessRules"].ToString().Trim() + "'"; }

                    sw.WriteLine("INSERT INTO " + this._NameTableAccessSiteMap + " ([IDAccessSiteMap],[IDUsers],[IDSiteMap],[Access],[AccessRules]) ");
                    sw.WriteLine("VALUES(" + dr["IDAccessSiteMap"].ToString().Trim() +
                        "," + dr["IDUsers"].ToString().Trim() +
                        "," + dr["IDSiteMap"].ToString().Trim() +
                        "," + dr["Access"].ToString().Trim() +
                        "," + AccessRules + ")");
                }
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableAccessSiteMap + " OFF");
                sw.WriteLine("GO");
            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    #endregion

    #region Формирование скриптов "Пользователи и группы"
    /// <summary>
    /// Создать скрипт восстановления "Пользователи и группы" 
    /// </summary>
    /// <returns></returns>
    public bool CreateScriptUsersAndGroups()
    {
        string PatchFile = "D:\\Backup\\UsersAndGroups_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".sql";
        FileStream fsScr = new FileStream(@PatchFile, FileMode.Create);
        StreamWriter sw = new StreamWriter(fsScr);
        TitleScript(ref sw);
        DropFK(ref sw, this._NameTableMenagerProject, "FK_MenagerProject_Users");
        DropFK(ref sw, this._NameTableAccessSiteMap, "FK_AccessSiteMap_Users");
        DropFK(ref sw, this._NameTableWeb, "FK_Web_Users");
        DeleteTableUsersAndGroups(ref sw);
        CreateTableUsers(ref sw);
        if (!InsertUsers(ref sw)) { sw.Close(); return false; }
        CreateTableGroupUser(ref sw);
        if (!InsertGroupUser(ref sw)) { sw.Close(); return false; }
        CreateFK_MenagerProject_Users(ref sw);
        CreateFK_AccessSiteMap_Users(ref sw);
        CreateFK_Web_Users(ref sw);
        sw.Flush();
        sw.Close();
        return true;
    }
    /// <summary>
    /// Удалить таблицы 'Пользователи и группы'
    /// </summary>
    /// <param name="sw"></param>
    protected void DeleteTableUsersAndGroups(ref StreamWriter sw)
    {
        sw.WriteLine("-------------------------------------------------------------------------------------------------");
        sw.WriteLine("--> Удалим таблицы 'Пользователи и группы'");
        DeleteTable(ref sw, this._NameTableGroupUser);
        DeleteTable(ref sw, this._NameTableUsers);
    }
    /// <summary>
    /// Создать таблицу Users
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableUsers(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'Users'");
        sw.WriteLine("SET ANSI_NULLS ON");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[Users](");
        sw.WriteLine("	[IDUser] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine("	[IDWeb] [int] NULL,");
        sw.WriteLine("	[UserEnterprise] [nvarchar](50) NOT NULL,");
        sw.WriteLine("	[Description] [nvarchar](1000) NULL,");
        sw.WriteLine("	[Email] [nvarchar](150) NULL,");
        sw.WriteLine("	[bDistribution] [bit] NOT NULL,");
        sw.WriteLine("	[Surname] [nvarchar](50) NULL,");
        sw.WriteLine("	[Name] [nvarchar](50) NULL,");
        sw.WriteLine("	[Patronymic] [nvarchar](50) NULL,");
        sw.WriteLine("	[Post] [nvarchar](250) NULL,");
        sw.WriteLine("	[IDSection] [int] NOT NULL,");
        sw.WriteLine(" CONSTRAINT [PK_tr_Users] PRIMARY KEY CLUSTERED ");
        sw.WriteLine("(");
        sw.WriteLine("	[IDUser] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
        CreateFK_Users_Section(ref sw);
    }
    /// <summary>
    /// Создать ключ FK_Users_Section
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_Users_Section(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_Users_Section'");
        sw.WriteLine("ALTER TABLE " + this._NameTableUsers + "  WITH CHECK ADD  CONSTRAINT [FK_Users_Section] FOREIGN KEY([IDSection])");
        sw.WriteLine("REFERENCES " + this._NameTableSection + " ([IDSection])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableUsers + " CHECK CONSTRAINT [FK_Users_Section]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Заполнить данными
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertUsers(ref StreamWriter sw)
    {
        bool result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDUser],[IDWeb],[UserEnterprise],[Description],[Email],[bDistribution],[Surname],[Name],[Patronymic],[Post],[IDSection] FROM " + this._NameTableUsers + " order by [IDUser]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним пользователей");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableUsers + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    string IDWeb = "null";
                    if (dr["IDWeb"] != DBNull.Value) { IDWeb = dr["IDWeb"].ToString().Trim(); }
                    string Description = "null";
                    if (dr["Description"] != DBNull.Value) { Description = "N'"+ dr["Description"].ToString().Trim() + "'"; }
                    string Email = "null";
                    if (dr["Email"] != DBNull.Value) { Email = "N'" + dr["Email"].ToString().Trim() + "'"; }
                    string Surname = "null";
                    if (dr["Surname"] != DBNull.Value) { Surname = "N'" + dr["Surname"].ToString().Trim() + "'"; }
                    string Name = "null";
                    if (dr["Name"] != DBNull.Value) { Name = "N'" + dr["Name"].ToString().Trim() + "'"; }
                    string Patronymic = "null";
                    if (dr["Patronymic"] != DBNull.Value) { Patronymic = "N'" + dr["Patronymic"].ToString().Trim() + "'"; }
                    string Post = "null";
                    if (dr["Post"] != DBNull.Value) { Post = "N'" + dr["Post"].ToString().Trim() + "'"; }
                    sw.WriteLine("INSERT INTO " + this._NameTableUsers + " ([IDUser],[IDWeb],[UserEnterprise],[Description],[Email],[bDistribution],[Surname],[Name],[Patronymic],[Post],[IDSection]) ");
                    sw.WriteLine("VALUES(" + dr["IDUser"].ToString().Trim() +
                        "," + IDWeb +
                        ",N'" + dr["UserEnterprise"].ToString().Trim() + "'" +
                        "," + Description +
                        "," + Email +
                        ",'" + dr["bDistribution"].ToString().Trim() + "'" +    
                        "," + Surname +                    
                        "," + Name +    
                        "," + Patronymic +    
                        "," + Post +
                        "," + dr["IDSection"].ToString().Trim() + ")");
                }
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableUsers + " OFF");
                sw.WriteLine("GO");
            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    /// <summary>
    /// Создать таблицу GroupUser
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableGroupUser(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'GroupUser'");
        sw.WriteLine("SET ANSI_NULLS ON");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[GroupUser](");
        sw.WriteLine("    [IDGroupUser] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine("	[IDGroup] [int] NOT NULL,");
        sw.WriteLine("	[IDUser] [int] NOT NULL,");
        sw.WriteLine(" CONSTRAINT [PK_tr_GroupUser] PRIMARY KEY CLUSTERED ");
        sw.WriteLine("(");
        sw.WriteLine("	[IDGroupUser] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
        CreateFK_GroupUser_Users(ref sw);
        CreateFK_GroupUser_Group(ref sw);
    }
    /// <summary>
    /// Создать ключ FK_GroupUser_Users
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_GroupUser_Users(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_GroupUser_Users'");
        sw.WriteLine("ALTER TABLE " + this._NameTableGroupUser + "  WITH CHECK ADD  CONSTRAINT [FK_GroupUser_Users] FOREIGN KEY([IDUser])");
        sw.WriteLine("REFERENCES " + this._NameTableUsers + " ([IDUser])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableGroupUser + " CHECK CONSTRAINT [FK_GroupUser_Users]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Создать ключ FK_GroupUser_Group
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_GroupUser_Group(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_GroupUser_Group'");
        sw.WriteLine("ALTER TABLE " + this._NameTableGroupUser + "  WITH CHECK ADD  CONSTRAINT [FK_GroupUser_Group] FOREIGN KEY([IDGroup])");
        sw.WriteLine("REFERENCES " + this._NameTableUsers + " ([IDUser])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableGroupUser + " CHECK CONSTRAINT [FK_GroupUser_Group]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Заполнить данными
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertGroupUser(ref StreamWriter sw)
    {
        bool result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDGroupUser],[IDGroup],[IDUser] FROM " + this._NameTableGroupUser + " order by [IDGroupUser]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним пользователей групп доступа");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableGroupUser + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {
                    sw.WriteLine("INSERT INTO " + this._NameTableGroupUser + " ([IDGroupUser],[IDGroup],[IDUser]) ");
                    sw.WriteLine("VALUES(" + dr["IDGroupUser"].ToString().Trim() +
                        "," + dr["IDGroup"].ToString().Trim() +
                        "," + dr["IDUser"].ToString().Trim() + ")");
                }
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableGroupUser + " OFF");
                sw.WriteLine("GO");
            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }

    #endregion

    #region Формирование скриптов "HangFireListJobs"
    /// <summary>
    /// Создать скрипт восстановления списка фоновых задач HangFireListJobs
    /// </summary>
    /// <returns></returns>
    public bool CreateScriptHangFireListJobs()
    {
        string PatchFile = "D:\\Backup\\HangFireListJobs_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".sql";
        FileStream fsScr = new FileStream(@PatchFile, FileMode.Create);
        StreamWriter sw = new StreamWriter(fsScr);
        TitleScript(ref sw);
        //DropFK(ref sw, this._NameTableSiteMap, "FK_SiteMap_Web");
        DeleteTable(ref sw, this._NameTableHangFireListJobs);
        CreateTableHangFireListJobs(ref sw);
        if (!InsertHangFireListJobs(ref sw)) { sw.Close(); return false; }
        sw.Flush();
        sw.Close();
        return true;
    }
    /// <summary>
    /// Создать таблицу HangFireListJobs
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateTableHangFireListJobs(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать таблицу 'HangFireListJobs'");
        sw.WriteLine("SET ANSI_NULLS ON");
        sw.WriteLine("GO");
        sw.WriteLine("SET QUOTED_IDENTIFIER ON");
        sw.WriteLine("GO");
        sw.WriteLine("CREATE TABLE [" + this._ServerName.Trim() + "].[dbo].[HangFireListJobs](");
        sw.WriteLine("  [IDJob] [int] IDENTITY(1,1) NOT NULL,");
        sw.WriteLine("	[Metod] [nvarchar](128) NOT NULL,");
        sw.WriteLine("	[Description] [nvarchar](1024) NOT NULL,");
        sw.WriteLine("	[Enable] [bit] NOT NULL,");
        sw.WriteLine("	[Start] [datetime] NULL,");
        sw.WriteLine("	[Stop] [datetime] NULL,");
        sw.WriteLine("	[Cron] [nvarchar](128) NULL,");
        sw.WriteLine("	[DistributionList] [nvarchar](2048) NULL,");
        sw.WriteLine(" CONSTRAINT [PK_HangFireListJobs] PRIMARY KEY CLUSTERED ");
        sw.WriteLine("(");
        sw.WriteLine("	[IDJob] ASC");
        sw.WriteLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        sw.WriteLine(") ON [PRIMARY]");
        sw.WriteLine("GO");
    }
    /// <summary>
    /// Заполнить данными
    /// </summary>
    /// <param name="sw"></param>
    /// <returns></returns>
    protected bool InsertHangFireListJobs(ref StreamWriter sw)
    {
        bool result = true;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        string sql = "SELECT [IDJob],[Metod],[Description],[Enable],[Start],[Stop],[Cron],[DistributionList] FROM " + this._NameTableHangFireListJobs + " order by [IDJob]";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "Table");
            try
            {
                sw.WriteLine("-------------------------------------------------------------------------------------------------");
                sw.WriteLine("--> Заполним список задач");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableHangFireListJobs + " ON");
                sw.WriteLine("GO");
                foreach (DataRow dr in ds.Tables["Table"].Rows)
                {

                    string Start = "null";
                    if (dr["Start"] != DBNull.Value) { Start = "CONVERT(DATETIME, '" + (DateTime.Parse(dr["Start"].ToString().Trim())).ToString("yyyy-MM-dd HH:mm:ss") + "', 102)"; }
                    string Stop = "null";
                    if (dr["Stop"] != DBNull.Value) { Stop = "CONVERT(DATETIME, '" + (DateTime.Parse(dr["Stop"].ToString().Trim())).ToString("yyyy-MM-dd HH:mm:ss") + "', 102)"; }
                    string Cron = "null";
                    if (dr["Cron"] != DBNull.Value) { Cron = "N'" + dr["Cron"].ToString().Trim() + "'"; }
                    string DistributionList = "null";
                    if (dr["DistributionList"] != DBNull.Value) { DistributionList = "N'" + dr["DistributionList"].ToString().Trim() + "'"; }
                    
                    sw.WriteLine("INSERT INTO " + this._NameTableHangFireListJobs + " ([IDJob],[Metod],[Description],[Enable],[Start],[Stop],[Cron],[DistributionList]) ");
                    sw.WriteLine("VALUES(" + dr["IDJob"].ToString().Trim() +
                        ",N'" + dr["Metod"].ToString().Trim() + "'" +
                        ",N'" + dr["Description"].ToString().Trim() + "'" +
                        ",'" + dr["Enable"].ToString() + "'" +
                        "," + Start +
                        "," + Stop +
                        "," + Cron +
                        "," + DistributionList + ")");
                }
                sw.WriteLine("GO");
                sw.WriteLine("SET IDENTITY_INSERT " + this._NameTableHangFireListJobs + " OFF");
                sw.WriteLine("GO");
            }
            catch (Exception err)
            {
                log.SaveSysError(err);
                result = false;
            }
        }
        catch (Exception err)
        {
            log.SaveSysError(err);
            result = false;
        }
        finally
        {
            con.Close();
        }
        return result;

    }
    #endregion

    #region Формирование скриптов "Доступ к сайту"

    /// <summary>
    /// Создать ключ FK_AccessUsers_Section
    /// </summary>
    /// <param name="sw"></param>
    protected void CreateFK_AccessUsers_Section(ref StreamWriter sw)
    {
        sw.WriteLine("----------------------------------------------------------------------------");
        sw.WriteLine("--> Создать ключ 'FK_AccessUsers_Section'");
        sw.WriteLine("ALTER TABLE " + this._NameTableAccessUsers + "  WITH CHECK ADD  CONSTRAINT [FK_AccessUsers_Section] FOREIGN KEY([IDSection])");
        sw.WriteLine("REFERENCES " + this._NameTableSection + " ([IDSection])");
        sw.WriteLine("GO");
        sw.WriteLine("ALTER TABLE " + this._NameTableAccessUsers + " CHECK CONSTRAINT [FK_AccessUsers_Section]");
        sw.WriteLine("GO");
    }


    #endregion

    #endregion

}