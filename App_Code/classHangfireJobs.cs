using Hangfire;
using Strategic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using Web.Email;
using WebReports;



namespace WebBase
{
    [Serializable()]
    public class JobEntity
    {
        public int IDJob { get; set; }
        public string Metod { get; set; }
        public string Description { get; set; }
        public bool Enable { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Stop { get; set; }
        public string Cron { get; set; }
        public string DistributionList { get; set; }
    }

    public enum typeLogJob : int { status = 0, error = 1 }

    /// <summary>
    /// Класс для работы с базой данных хранения информации о задачах
    /// </summary>
    public class classHangfireJobsDB : classBaseDB 
    { 
        protected string _FieldHangFireListJobs = "[IDJob],[Metod],[Description],[Enable],[Start],[Stop],[Cron],[DistributionList]";
        protected string _NameTableHangFireListJobs = WebConfigurationManager.AppSettings["tb_HangFireListJobs"].ToString();
        protected string _FieldLogJobs = "[ID],[IDJob],[DateTime],[TypeLog],[Message]";
        protected string _NameTableLogJobs = WebConfigurationManager.AppSettings["tb_LogJobs"].ToString();
        protected string _NameSPUpdateHangFireJob = WebConfigurationManager.AppSettings["sp_UpdateHangFireJob"].ToString();
        protected string _NameSPInsertHangFireJob = WebConfigurationManager.AppSettings["sp_InsertHangFireJob"].ToString();
        protected string _NameSPDeleteHangFireJob = WebConfigurationManager.AppSettings["sp_DeleteHangFireJob"].ToString();
        protected string _NameSPEnableHangFireJob = WebConfigurationManager.AppSettings["sp_EnableHangFireJob"].ToString();

        public classHangfireJobsDB() : base()
        { 
        
        }

        #region Методы работы с HangFireListJobs
        /// <summary>
        /// Получить настройки по ID задания
        /// </summary>
        /// <param name="IDJob"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectSetupJob(int IDJob)
        {
            string sql = "SELECT " + this._FieldHangFireListJobs + " FROM " + this._NameTableHangFireListJobs + " WHERE ([IDJob] = " + IDJob.ToString() + ")";
            return Select(sql);
        }
        /// <summary>
        /// Получить настройки всех заданий
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectSetupJob()
        {
            string sql = "SELECT " + this._FieldHangFireListJobs + " FROM " + this._NameTableHangFireListJobs + " ORDER BY [IDJob] ";
            return Select(sql);
        }
        /// <summary>
        /// Получить настройки активных или не активных задач
        /// </summary>
        /// <param name="Enable"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectSetupJob(bool Enable)
        {
            string sql = "SELECT " + this._FieldHangFireListJobs + " FROM " + this._NameTableHangFireListJobs + " WHERE ([Enable] = '" + Enable.ToString() + "')"  + " ORDER BY [IDJob] ";
            return Select(sql);
        }
        /// <summary>
        /// Прочесть настройки из DataRow
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private JobEntity GetSetupJob(DataRow row)
        {
            if (row == null) return null;
            return new JobEntity()
            {
                IDJob = int.Parse(row["IDJob"].ToString()),
                Metod = row["Metod"].ToString(),
                Description = row["Description"].ToString(),
                Enable = bool.Parse(row["Enable"].ToString()),
                Start = row["Start"] != DBNull.Value ? row["Start"] as DateTime? : null,
                Stop = row["Stop"] != DBNull.Value ? row["Stop"] as DateTime? : null,
                Cron = row["Cron"] != DBNull.Value ? row["Cron"] as string : null,
                DistributionList = row["DistributionList"] != DBNull.Value ? row["DistributionList"] as string : null,
            };
        }
        /// <summary>
        /// Прочесть настройки по IDJob
        /// </summary>
        /// <param name="IDJob"></param>
        /// <returns></returns>
        public JobEntity GetSetupJob(int IDJob)
        {
            DataRow[] rows = SelectSetupJob(IDJob).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                return GetSetupJob(rows[0]);
            }
            return null;
        }
        /// <summary>
        /// Прочесть список настроек (null - всех, true - активных, false - не активных)
        /// </summary>
        /// <returns></returns>
        public List<JobEntity> GetSetupJob(bool? Enable) 
        {
            List<JobEntity> listje = new List<JobEntity>();
            
            DataRow[] rows;
            if (Enable!=null){
                rows = SelectSetupJob((bool)Enable).Select();
            } else {
                rows = SelectSetupJob().Select();            
            }
            foreach (DataRow dr in rows) 
            {
                listje.Add(GetSetupJob(dr));
            }
            return listje;
        }

        /// <summary>
        /// Обновить состояние выполнения задания
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public int UpdateTimeRunJob(JobEntity job)
        {
            if (job == null) return 0;
            string sql = "UPDATE " + this._NameTableHangFireListJobs + " SET [Start] = CONVERT(DATETIME, '" + ((DateTime)job.Start).ToString("yyyy-MM-dd HH:mm:ss") + "', 102) ,[Stop] = CONVERT(DATETIME, '" +
                ((DateTime)job.Stop).ToString("yyyy-MM-dd HH:mm:ss") + "', 102) WHERE [IDJob] = " + job.IDJob.ToString();
            return Update(sql);
        }
        /// <summary>
        /// Метод обновления фоновой задачи
        /// </summary>
        /// <param name="IDJob"></param>
        /// <param name="Metod"></param>
        /// <param name="Description"></param>
        /// <param name="Cron"></param>
        /// <param name="DistributionList"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public int UpdateHangFireJob(int IDJob, string Metod, string Description, string Cron, string DistributionList,bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPUpdateHangFireJob, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDJob", SqlDbType.Int));
            cmd.Parameters["@IDJob"].Value = IDJob;
            cmd.Parameters.Add(new SqlParameter("@Metod", SqlDbType.NVarChar, 128));
            cmd.Parameters["@Metod"].Value = (Metod != null) ? Metod.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 1024));
            cmd.Parameters["@Description"].Value = (Description != null) ? Description.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Cron", SqlDbType.NVarChar, 128));
            cmd.Parameters["@Cron"].Value = (Cron != null) ? Cron.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@DistributionList", SqlDbType.NVarChar, 2048));
            cmd.Parameters["@DistributionList"].Value = (DistributionList != null) ? DistributionList.Trim() : SqlString.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_uhangfirejob");
            }
            return Result;
        }
        /// <summary>
        /// Метод управления статусом фоновой задачи
        /// </summary>
        /// <param name="IDJob"></param>
        /// <param name="Enable"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public int EnableHangFireJob(int IDJob, bool Enable, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPEnableHangFireJob, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDJob", SqlDbType.Int));
            cmd.Parameters["@IDJob"].Value = IDJob;
            cmd.Parameters.Add(new SqlParameter("@Enable", SqlDbType.Bit));
            cmd.Parameters["@Enable"].Value = Enable;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_uenablehangfirejob");
            }
            return Result;
        }
        /// <summary>
        /// Метод добавления фоновой задачи
        /// </summary>
        /// <param name="Metod"></param>
        /// <param name="Description"></param>
        /// <param name="Enable"></param>
        /// <param name="Cron"></param>
        /// <param name="DistributionList"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public int InsertHangFireJob(string Metod, string Description, bool Enable, string Cron, string DistributionList,bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPInsertHangFireJob, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Metod", SqlDbType.NVarChar, 128));
            cmd.Parameters["@Metod"].Value = (Metod != null) ? Metod.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 1024));
            cmd.Parameters["@Description"].Value = (Description != null) ? Description.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Enable", SqlDbType.Bit));
            cmd.Parameters["@Enable"].Value = Enable;
            cmd.Parameters.Add(new SqlParameter("@Cron", SqlDbType.NVarChar, 128));
            cmd.Parameters["@Cron"].Value = (Cron != null) ? Cron.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@DistributionList", SqlDbType.NVarChar, 2048));
            cmd.Parameters["@DistributionList"].Value = (DistributionList != null) ? DistributionList.Trim() : SqlString.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_ihangfirejob");
            }
            return Result;
        }
        /// <summary>
        /// Удалить фоновую задачу
        /// </summary>
        /// <param name="IDJob"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int DeleteHangFireJob(int IDJob, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPDeleteHangFireJob, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDJob", SqlDbType.Int));
            cmd.Parameters["@IDJob"].Value = IDJob;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_dhangfirejob");
            }
            return Result;
        }

        #endregion

        #region Методы работы с LogWeb
        /// <summary>
        /// Сохранить лог состояния Joba
        /// </summary>
        /// <param name="IDJob"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public int SaveStatus(int IDJob, string message)
        {
            string sql = "INSERT INTO " + this._NameTableLogJobs + " ([IDJob] ,[DateTime] ,[TypeLog] ,[Message]) " +
                "Values (" + IDJob.ToString() +
                ", GetDate() " +
                ",N'" + typeLogJob.status.ToString() + "'" +
                ",N'" + message + "'" +
                ")";
            return Update(sql);
        }
        /// <summary>
        /// Сохранить ошибку
        /// </summary>
        /// <param name="IDJob"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int SaveError(int IDJob, Exception err)
        {
            string sql = "INSERT INTO " + this._NameTableLogJobs + " ([IDJob] ,[DateTime] ,[TypeLog] ,[Message]) " +
                "Values (" + IDJob.ToString() +
                ", GetDate() " +
                ",N'" + typeLogJob.error.ToString() + "'" +
                ",N'" + err.StackTrace + "'" +
                ")";
            return Update(sql);
        }
        #endregion
    }

    /// <summary>
    /// КЛАСС ВЫПОЛНЕНИЯ ЗАДАЧ ПО РАСПИСАНИЮ
    /// по расписанию запускается метод указаный в БД настроек (таблица HangFireListJobs)
    /// запущенный метод создает поток выполнения задания
    /// </summary>
    public class classHangfireJobs
    {
        protected classHangfireJobsDB chfdb = new classHangfireJobsDB();
        protected List<JobEntity> list = new List<JobEntity>();
        public List<JobEntity> ListJob { get { return this.list; } }

        public classHangfireJobs()
        {
            RefreshSetupJob((bool)true);
        }

        #region ОБЩИЕ МЕТОДЫ
        /// <summary>
        /// Обновить список настроек
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        public int RefreshSetupJob(bool? active) 
        {
            this.list.Clear();
            this.list = chfdb.GetSetupJob(active);
            return this.list.Count();
        }
        /// <summary>
        /// Запустить задачу
        /// </summary>
        /// <param name="je"></param>
        public void StartJob(JobEntity je) 
        {
            MethodInfo[] methods = typeof(classHangfireJobs).GetMethods();
            foreach (MethodInfo info in methods)
            {
                // Call Win method.
                if (info.Name == je.Metod)
                {
                    info.Invoke(this, new object[] { (object)je });
                }
            }
        }
        #endregion

        #region ПЕРЕЧЕНЬ МЕТОДОВ ЗАПУСКА ПОТОКОВ ЗАДАНИЙ
        /// <summary>
        /// Метод AccessUser
        /// </summary>
        /// <param name="je"></param>
        public void AccessUser(JobEntity je)
        {
            classThreadAccessUser au = new classThreadAccessUser(je);
            au.Start();
        }
        /// <summary>
        /// Метод StepProject
        /// </summary>
        /// <param name="je"></param>
        public void StepProject(JobEntity je)
        {
            classThreadStepProject sp = new classThreadStepProject(je);
            sp.Start();
        }
        /// <summary>
        /// Метод StatusProgramm
        /// </summary>
        /// <param name="je"></param>
        public void StatusProgramm(JobEntity je)
        {
            classThreadProgrammProject pp = new classThreadProgrammProject(je);
            pp.Start();
        }
        /// <summary>
        /// Минутный тригер
        /// </summary>
        /// <param name="je"></param>
        public void Test(JobEntity je)
        {
            chfdb.SaveStatus(je.IDJob, "Job запущен");
            je.Start = DateTime.Now;
            je.Stop = DateTime.Now;
            chfdb.UpdateTimeRunJob(je);
            chfdb.SaveStatus(je.IDJob, "Job выполнен");


        }
        #endregion
    }


    #region ПОТОКИ ВЫПОЛНЕНИЯ ЗАДАНИЙ
    /// <summary>
    /// Базовый поток ( с него наследуются потоки выполнения задачи по расписанию)
    /// </summary>
    public class classBaseThreadJob 
    {
        protected classHangfireJobsDB chfdb = new classHangfireJobsDB();
        private Thread thr;
        protected JobEntity Job = null;
        protected classSMTPWeb csmtp = new classSMTPWeb();
        protected classUsers cu = new classUsers();

        public classBaseThreadJob(JobEntity Job)
            : base()
        {
            chfdb.SaveStatus(Job.IDJob, "Job запущен");
            this.Job = Job;
            thr = new Thread(new ThreadStart(mainThread));
        }

        public void Start()
        {
            Job.Start = DateTime.Now;
            thr.Start();
        }

        public virtual void mainThread()
        {
            // Обновить время выполнения
            //Job.Stop = DateTime.Now;
            //chfdb.UpdateTimeRunJob(Job);
            return;
        }

        protected void Stop() 
        { 
            Job.Stop = DateTime.Now;
            chfdb.UpdateTimeRunJob(Job);
            chfdb.SaveStatus(Job.IDJob, "Job выполнен");        
        }
    }
    /// <summary>
    /// Поток проверки шагов (по которым подходит срок выполнения) проектов
    /// </summary>
    public class classThreadStepProject : classBaseThreadJob
    {
        public classThreadStepProject(JobEntity Job)
            : base(Job)
        {

        }

        public override void mainThread()
        {
            classProject cp = new classProject();
            classMenagerProject cmp = new classMenagerProject();
            classTemplatesSteps cts = new classTemplatesSteps();
            List<SendEmailUser> list_seu = new List<SendEmailUser>();
            List<DistributionListEmail> distribution_list = base.csmtp.GetDistributionList(Job.DistributionList);
            //List<string> List_boss = new List<string>();
            List<UserProjectEntity> list_upe = cmp.GetUsersProject();

            if (list_upe != null)
            {
                string cultureName = Thread.CurrentThread.CurrentCulture.Name;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
                //List_boss.Add("maksim.romanov@arcelormittal.com");

                foreach (UserProjectEntity upe in list_upe)
                {
                    StringBuilder body = null;
                    DataRow[] rows = cp.SelectCultureEndStepProject((int)upe.ID, 2).Select();
                    if ((rows != null) && (rows.Count() > 0))
                    {
                        body = base.csmtp.OpenTable();
                        foreach (DataRow dr in rows)
                        {
                            base.csmtp.AddTR(ref body, new string[] { "Проект:", dr["Name"].ToString() });
                            base.csmtp.AddTR(ref body, new string[] { "Шаг внедрения:", cts.GetCultureStepsProject(int.Parse(dr["IDStep"].ToString())).Step });
                            base.csmtp.AddTR(ref body, new string[] { "Процент выполнения:", dr["Persent"].ToString() });
                            base.csmtp.AddTR(ref body, new string[] { "Срок выполнения:", dr["FactStop"].ToString() });
                            base.csmtp.AddTR(ref body, new string[] { "Последняя правка проекта:", dr["Change"].ToString() });
                            base.csmtp.AddTR(ref body, new string[] { "Ссылка:", WebConfigurationManager.AppSettings["urlProject"].ToString() + "?Owner=11&prj=" + int.Parse(dr["IDProject"].ToString()) });
                            base.csmtp.AddTR(ref body, new string[] { "", "" });
                        }
                        base.csmtp.CloseTable(ref body);
                    }
                    if (body != null) list_seu.Add(new SendEmailUser() { sendTo = upe.Email, subject = "Подходит срок внедрения шагов Ваших проектов", body = body.ToString() });

                }
                //Отправим электронную почту
                string bodyboss = null;
                foreach (SendEmailUser seu in list_seu)
                {
                    base.csmtp.EmailSend(seu);
                    bodyboss = bodyboss + "<table><caption>Руководитель проектов: " + seu.sendTo + "</caption><tr><td>" + seu.body + "</td></tr></table>";
                }
                if (bodyboss != null)
                {

                    base.csmtp.EmailSend(base.csmtp.GetListEmail(distribution_list,"boss"), "Отчет о сроках внедрения проектов", bodyboss);
                }
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);

                base.Stop();
            }

        }
    }
    /// <summary>
    /// Поток рапределения шагов предоставления доступа к Web-сайту
    /// </summary>
    public class classThreadAccessUser : classBaseThreadJob 
    {
        public classThreadAccessUser(JobEntity Job)
            : base(Job)
        {

        }

        public override void mainThread()
        {
            classAccessUsers cau = new classAccessUsers();
            classWeb cweb = new classWeb();
            classSection csection = new classSection();
            List<AccessUsersEntity> list_active_aue = null; // список активных пользователей
            List<SendEmailUser> list_seu = new List<SendEmailUser>();

            list_active_aue = cau.GetActiveAccessUsers();
            if (list_active_aue == null) return;
            string cultureName = Thread.CurrentThread.CurrentCulture.Name;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
            foreach (AccessUsersEntity aue in list_active_aue)
            {
                List<AccessWebUsersEntity> list_active_wue = cau.GetListActiveAccessWebUsers((int)aue.ID);
                if (list_active_wue != null)
                {
                    bool Approval = true;
                    foreach (AccessWebUsersEntity awue in list_active_wue)
                    {
                        if ((awue.AccessWeb) & (awue.DateApproval == null)) Approval = false;
                        StringBuilder body = null;
                        bool again = false;
                        if (awue.DateRequest != null)
                        {
                            if (awue.DateRequest > DateTime.Now.AddDays(-1))
                            {
                                continue;
                            }
                            again = true;
                            // Повторно
                        }
                        // Формируем почту
                        body = base.csmtp.OpenTable();
                        if (again) { base.csmtp.AddCaption(ref body, "ПОВТОРНО!"); }
                        base.csmtp.AddTR(ref body, new string[] { "Запрос на доступ к Web-сайту:", cweb.GetCultureWeb(awue.IDWeb).Description });
                        base.csmtp.AddTR(ref body, new string[] { "Пользователь:", aue.Surname + " " + aue.Name + " " + aue.Patronymic });
                        base.csmtp.AddTR(ref body, new string[] { "Должность:", aue.Post });
                        base.csmtp.AddTR(ref body, new string[] { "Подразделение:", csection.GetCultureSection(aue.IDSection).SectionFull });
                        base.csmtp.AddTR(ref body, new string[] { "Email:", aue.Email });
                        base.csmtp.AddTR(ref body, new string[] { "Ссылка на согласование доступа:", WebConfigurationManager.AppSettings["urlAccessWeb"].ToString() + "?ID=" + ((int)awue.ID).ToString() });
                        base.csmtp.CloseTable(ref body);
                        list_seu.Add(new SendEmailUser() { sendTo = cau.GetUserDetali(cweb.GetCultureWeb(awue.IDWeb).IDUser).Email, subject = "Согласование доступа к web-сайту:" + cweb.GetCultureWeb(awue.IDWeb).Description, body = body.ToString() });
                        cau.SetDateRequest((int)awue.ID);
                    }
                    if (Approval) // Администратору сообщение все согласовали нареж прова
                    {
                        base.csmtp.AdminEmailSend("Заявка на доступ к Web-серверу ДАТП", "Необходимо обработать запрос на доступ к Web-ресурсам для пользователя " + aue.Surname + " " + aue.Name + " "
                            + aue.Patronymic + ". Просмотреть согласование и выполнить заявку вы можете перейдя по ссылке: http://localhost:57105/WebSite/Setup/AccessRequests.aspx?Owner=15" + "&ID=" + aue.ID.ToString());
                    }
                }
            }
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
            // Отправляем
            //string st = list_seu.ToString();
            foreach (SendEmailUser seu in list_seu)
            {
                base.csmtp.EmailSend(seu);
            }
            base.Stop();
        }

    }
    /// <summary>
    /// Поток рассылки pdf-файла статуса программ внедрения проектов
    /// </summary>
    public class classThreadProgrammProject : classBaseThreadJob 
    {
        public classThreadProgrammProject(JobEntity Job)
            : base(Job)
        {

        }

        public override void mainThread()
        {
            classProject cp = new classProject();
            classUsers cu = new classUsers();
            classPDFReports pdfrep = new classPDFReports();
            List<DistributionListEmail> distribution_list = base.csmtp.GetDistributionList(Job.DistributionList);
            // Весовая программа
            string file_scales_ru = Path.GetTempPath() + "scalesProgram(ru)" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".pdf";
            if (File.Exists(file_scales_ru))
            {
                File.Delete(file_scales_ru);
            }
            pdfrep.CreatePDFDocumentStatusProgramm(file_scales_ru, CultureInfo.GetCultureInfo("ru-RU"), cp.GetProgramProject(new int[] { 17, 2, 16, 3 }, implementationProgram.Scales, CultureInfo.GetCultureInfo("ru-RU")), "Статус выполнения весовой программы по состоянию на " + DateTime.Now.Date.ToString("dd-MM-yyyy") + "\n");
            // Прокатная программа
            string file_procat_ru = Path.GetTempPath() + "procatProgram(ru)" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".pdf";
            if (File.Exists(file_procat_ru))
            {
                File.Delete(file_procat_ru);
            }
            pdfrep.CreatePDFDocumentStatusProgramm(file_procat_ru, CultureInfo.GetCultureInfo("ru-RU"), cp.GetProgramProject(null, implementationProgram.Procat, CultureInfo.GetCultureInfo("ru-RU")), "Статус выполнения прокатной программы по состоянию на " + DateTime.Now.Date.ToString("dd-MM-yyyy") + "\n");
            base.csmtp.EmailSend(base.csmtp.GetListEmail(distribution_list, "email1"), "Статус внедрения программ ДАТП", "Статус внедрения программ проектов АСУТП по ДАТП. <br /> Служба по стратегическому развитию и внедрению АСУТП. <br /> Сайт службы: http://krr-www-parep01.europe.mittalco.com/WebSite/Strategic/Default.aspx", new string[] { file_scales_ru, file_procat_ru });
            //File.Delete(file_scales_ru);
            base.Stop();

        }

    }

    #endregion

}