using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBase;


namespace Strategic
{
    #region КЛАССЫ ШАБЛОНОВ ПРОЕКТОВ
    [Serializable()]
    public class TemplateStepEntity
    {
        public int? Id
        {
            get;
            set;
        }
        public string TemplateStep
        {
            get;
            set;
        }
    }

    [Serializable()]
    public class TemplateStepContent : TemplateStepEntity
    {
        public string TemplateStepEng
        {
            get;
            set;
        }
        public TemplateStepContent()
            : base()
        {

        }
    }   

    [Serializable()]
    public class BigStepEntity
    {
        public int? Id
        {
            get;
            set;
        }
        public string BigStep
        {
            get;
            set;
        }
    }

    [Serializable()]
    public class BigStepContent : BigStepEntity
    {
        public string BigStepEng
        {
            get;
            set;
        }
        public BigStepContent()
            : base()
        {

        }
    }

    [Serializable()]
    public class StepEntity
    {
        public int? Id
        {
            get;
            set;
        }
        public int IDTemplate
        {
            get;
            set;
        }
        public int IdBigStep
        {
            get;
            set;
        }
        public string Step
        {
            get;
            set;
        }
    }

    [Serializable()]
    public class StepContent : StepEntity
    {
        public string StepEng
        {
            get;
            set;
        }
        public StepContent()
            : base()
        {

        }
    }   

    /// <summary>
    /// Сводное описание для classTemplatesSteps
    /// </summary>
    public class classTemplatesSteps : classBaseDB
    {
        #region ПОЛЯ classTemplatesSteps
        protected string _FieldTemplateStepsProject = "[IDTemplateStepProject],[TemplateStep],[TemplateStepEng]";
        protected string _NameTableTemplateStepsProject = WebConfigurationManager.AppSettings["tb_TemplateStepsProject"].ToString();
        protected string _FieldBigStepsProject = "[IDBigStep],[BigStep],[BigStepEng]";
        protected string _NameTableBigStepsProject = WebConfigurationManager.AppSettings["tb_BigStepsProject"].ToString();
        protected string _NameSPInsertBigStepsProject = WebConfigurationManager.AppSettings["sp_InsertBigStep"].ToString();
        protected string _NameSPUpdateBigStepsProject = WebConfigurationManager.AppSettings["sp_UpdateBigStep"].ToString();
        protected string _NameSPDeleteBigStepsProject = WebConfigurationManager.AppSettings["sp_DeleteBigStep"].ToString();

        protected string _FieldStepsProject = "[IDStep],[IDTemplateStepProject],[IDBigStep],[Step],[StepEng]";
        protected string _NameTableStepsProject  = WebConfigurationManager.AppSettings["tb_StepsProject"].ToString();
        protected string _NameSPInsertStepProject = WebConfigurationManager.AppSettings["sp_InsertStepProject"].ToString();
        protected string _NameSPUpdateStepProject = WebConfigurationManager.AppSettings["sp_UpdateStepProject"].ToString();
        protected string _NameSPDeleteStepProject = WebConfigurationManager.AppSettings["sp_DeleteStepProject"].ToString();

        #endregion 
        
        public classTemplatesSteps()
        {
            //
            // TODO: добавьте логику конструктора
            //
        }

        #region МЕТОДЫ classTemplatesSteps

        #region Общие методы
        /// <summary>
        /// Получить название шаблона
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetTemplate(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDTemplateStepProject") != DBNull.Value)
            {
                TemplateStepEntity tse = GetCultureTemplatesSteps(int.Parse(DataBinder.Eval(dataItem, "IDTemplateStepProject").ToString()));
                return tse.TemplateStep;
            }
            return "-";
        }
        /// <summary>
        /// Получить название основного шага
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetBigStep(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDBigStep") != DBNull.Value)
            {
                BigStepEntity bse = GetCultureBigSteps(int.Parse(DataBinder.Eval(dataItem, "IDBigStep").ToString()));
                return bse.BigStep;
            }
            return "-";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetBigStepInStep(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDStep") != DBNull.Value)
            {
                StepEntity se = GetCultureStepsProject(int.Parse(DataBinder.Eval(dataItem, "IDStep").ToString()));
                BigStepEntity bse = GetCultureBigSteps(se.IdBigStep);
                return bse.BigStep;
            }
            return null;
        }
        /// <summary>
        /// Получить название детального шага
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetStep(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDStep") != DBNull.Value)
            {
                //StepEntity se = GetCultureStepsProject();
                //return se.Step;
                return GetStep(int.Parse(DataBinder.Eval(dataItem, "IDStep").ToString()));
            }
            return null;
        }
        /// <summary>
        /// Получить название детального шага
        /// </summary>
        /// <param name="IDStep"></param>
        /// <returns></returns>
        public string GetStep(int IDStep)
        {
            StepEntity se = GetCultureStepsProject(IDStep);
            return se.Step;
        }
        /// <summary>
        /// Получить полное название шага
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetAllStep(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDStep") != DBNull.Value)
            {
                StepEntity se = GetCultureStepsProject(int.Parse(DataBinder.Eval(dataItem, "IDStep").ToString()));
                BigStepEntity bse = GetCultureBigSteps(se.IdBigStep);
                return "<b>" + bse.BigStep + "</b><br/>" + " " + se.Step;
            }
            return null;
        }
        #endregion

        #region МЕТОДЫ TemplateStepsProject
        /// <summary>
        /// Вернуть все строчки таблицы
        /// </summary>
        /// <returns></returns>
        public DataTable SelectTemplatesSteps() 
        {
            string sql = "SELECT " + this._FieldTemplateStepsProject + " FROM " + this._NameTableTemplateStepsProject + " ORDER BY [IDTemplateStepProject]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную строку 
        /// </summary>
        /// <param name="IDTemplateStepProject"></param>
        /// <returns></returns>
        public DataTable SelectTemplatesSteps(int IDTemplateStepProject) 
        {
            string sql = "SELECT " + this._FieldTemplateStepsProject + " FROM " + this._NameTableTemplateStepsProject + " WHERE ([IDTemplateStepProject] = " + IDTemplateStepProject.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть все строчки таблицы с учетом культуры
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]          
        public DataTable SelectCultureTemplatesSteps() 
        {
            string sql = "SELECT [IDTemplateStepProject]" + base.GetCultureField("TemplateStep") + " FROM " + this._NameTableTemplateStepsProject + " ORDER BY [IDTemplateStepProject]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную строку с учетом культуры
        /// </summary>
        /// <param name="IDTemplateStepProject"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]        
        public DataTable SelectCultureTemplatesSteps(int IDTemplateStepProject) 
        {
            string sql = "SELECT [IDTemplateStepProject]" + base.GetCultureField("TemplateStep") + " FROM " + this._NameTableTemplateStepsProject + " WHERE ([IDTemplateStepProject] = " + IDTemplateStepProject.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную TemplateStepEntity
        /// </summary>
        /// <param name="IDTemplateStepProject"></param>
        /// <returns></returns>
        public TemplateStepEntity GetCultureTemplatesSteps(int IDTemplateStepProject) 
        {
            DataRow[] rows = SelectCultureTemplatesSteps(IDTemplateStepProject).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDTemplateStepProject"] != DBNull.Value)
                {
                    return new TemplateStepEntity()
                    {
                        Id = int.Parse(rows[0]["IDTemplateStepProject"].ToString()),
                        TemplateStep = rows[0]["TemplateStep"].ToString(),
                    };
                }
            }
            return null;
        }
        #endregion

        #region МЕТОДЫ BigStepsProject
        /// <summary>
        /// Вернуть все строчки таблицы
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectBigStepsProject()
        {
            string sql = "SELECT " + this._FieldBigStepsProject + " FROM " + this._NameTableBigStepsProject + " ORDER BY [IDBigStep]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную строку 
        /// </summary>
        /// <param name="IDTemplateStepProject"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectBigStepsProjects(int IDBigStep)
        {
            string sql = "SELECT " + this._FieldBigStepsProject + " FROM " + this._NameTableBigStepsProject + " WHERE ([IDBigStep] = " + IDBigStep.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть все строчки таблицы с учетом культуры
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureBigStepsProject()
        {
            string sql = "SELECT [IDBigStep]" + base.GetCultureField("BigStep") + " FROM " + this._NameTableBigStepsProject + " ORDER BY [IDBigStep]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную строку с учетом культуры
        /// </summary>
        /// <param name="IDTemplateStepProject"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureBigStepsProject(int IDBigStep)
        {
            string sql = "SELECT [IDBigStep]" + base.GetCultureField("BigStep") + " FROM " + this._NameTableBigStepsProject + " WHERE ([IDBigStep] = " + IDBigStep.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Добавить основной шаг
        /// </summary>
        /// <param name="BigStep"></param>
        /// <param name="BigStepEng"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public int InsertBigStepsProject(string BigStep, string BigStepEng, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPInsertBigStepsProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@BigStep", SqlDbType.NVarChar, 100));
            cmd.Parameters["@BigStep"].Value = (BigStep != null) ? BigStep.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@BigStepEng", SqlDbType.NVarChar, 100));
            cmd.Parameters["@BigStepEng"].Value = (BigStepEng != null) ? BigStepEng.Trim() : SqlString.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_ibigstep");
            }
            return Result;
        }
        /// <summary>
        /// Обновить основной шаг
        /// </summary>
        /// <param name="IDBigStep"></param>
        /// <param name="BigStep"></param>
        /// <param name="BigStepEng"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public int UpdateBigStepsProject(int IDBigStep ,string BigStep, string BigStepEng, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPUpdateBigStepsProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDBigStep", SqlDbType.Int));
            cmd.Parameters["@IDBigStep"].Value = IDBigStep;
            cmd.Parameters.Add(new SqlParameter("@BigStep", SqlDbType.NVarChar, 100));
            cmd.Parameters["@BigStep"].Value = (BigStep != null) ? BigStep.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@BigStepEng", SqlDbType.NVarChar, 100));
            cmd.Parameters["@BigStepEng"].Value = (BigStepEng != null) ? BigStepEng.Trim() : SqlString.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_ubigstep");
            }
            return Result;
        }
        /// <summary>
        /// Удалить основной шаг
        /// </summary>
        /// <param name="IDBigStep"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int DeleteBigStepsProject(int IDBigStep, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPDeleteBigStepsProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDBigStep", SqlDbType.Int));
            cmd.Parameters["@IDBigStep"].Value = IDBigStep;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_dbigstep");
            }
            return Result;
        }
        /// <summary>
        /// Получить BigStepEntity по указанному IDBigStep
        /// </summary>
        /// <param name="IDBigStep"></param>
        /// <returns></returns>
        public BigStepEntity GetCultureBigSteps(int IDBigStep) 
        {
            DataRow[] rows = SelectCultureBigStepsProject(IDBigStep).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDBigStep"] != DBNull.Value)
                {
                    return new BigStepEntity()
                    {
                        Id = int.Parse(rows[0]["IDBigStep"].ToString()),
                        BigStep = rows[0]["BigStep"].ToString(),
                    };
                }
            }
            return null;
        }
        #endregion

        #region МЕТОДЫ StepsProject
        /// <summary>
        /// Вернуть все строчки таблицы
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectStepsProject()
        {
            string sql = "SELECT " + this._FieldStepsProject + " FROM " + this._NameTableStepsProject + " ORDER BY [IDStep]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную строку 
        /// </summary>
        /// <param name="IDTemplateStepProject"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectStepsProject(int IDStep)
        {
            string sql = "SELECT " + this._FieldStepsProject + " FROM " + this._NameTableStepsProject + " WHERE ([IDStep] = " + IDStep.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть все строчки таблицы с учетом культуры
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureStepsProject()
        {
            string sql = "SELECT [IDStep],[IDTemplateStepProject],[IDBigStep]" + base.GetCultureField("Step") + " FROM " + this._NameTableStepsProject + " ORDER BY [IDStep]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную строку с учетом культуры
        /// </summary>
        /// <param name="IDTemplateStepProject"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureStepsProject(int IDStep)
        {
            string sql = "SELECT [IDStep],[IDTemplateStepProject],[IDBigStep]" + base.GetCultureField("Step") + " FROM " + this._NameTableStepsProject + " WHERE ([IDStep] = " + IDStep.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть строки детальных шагов пренадлежащих основному шагу
        /// </summary>
        /// <param name="IDBigStep"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureStepsProjectToBigStep(int IDBigStep)
        {
            string sql = "SELECT [IDStep],[IDTemplateStepProject],[IDBigStep]" + base.GetCultureField("Step") + " FROM " + this._NameTableStepsProject + " WHERE ([IDBigStep] = " + IDBigStep.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть строки детальных шагов пренадлежащих основному шагу и выбранному шаблону
        /// </summary>
        /// <param name="IDBigStep"></param>
        /// <param name="IDTemplateStepProject"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureStepsProjectToBigStep(int IDBigStep, int IDTemplateStepProject)
        {
            string sql = "SELECT [IDStep],[IDTemplateStepProject],[IDBigStep]" + base.GetCultureField("Step") + " FROM " + this._NameTableStepsProject + " WHERE (([IDBigStep] = " + IDBigStep.ToString() + ") AND ([IDTemplateStepProject]=" + IDTemplateStepProject .ToString()+ "))";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить StepEntity по указанному IDStep
        /// </summary>
        /// <param name="IDStep"></param>
        /// <returns></returns>
        public StepEntity GetCultureStepsProject(int IDStep)
        {
            DataRow[] rows = SelectCultureStepsProject(IDStep).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDStep"] != DBNull.Value)
                {
                    return new StepEntity()
                    {
                        Id = int.Parse(rows[0]["IDStep"].ToString()),
                        IDTemplate = int.Parse(rows[0]["IDTemplateStepProject"].ToString()),
                        IdBigStep = int.Parse(rows[0]["IDBigStep"].ToString()),
                        Step = rows[0]["Step"].ToString(),
                    };
                }
            }
            return null;
        }
        /// <summary>
        /// Добавить детальный шаг внедрения проектов
        /// </summary>
        /// <param name="IDTemplateStepProject"></param>
        /// <param name="IDBigStep"></param>
        /// <param name="Step"></param>
        /// <param name="StepEng"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public int InsertStepsProject(int IDTemplateStepProject,int IDBigStep, string Step, string StepEng, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPInsertStepProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDTemplateStepProject", SqlDbType.Int));
            cmd.Parameters["@IDTemplateStepProject"].Value = IDTemplateStepProject;
            cmd.Parameters.Add(new SqlParameter("@IDBigStep", SqlDbType.Int));
            cmd.Parameters["@IDBigStep"].Value = IDBigStep;
            cmd.Parameters.Add(new SqlParameter("@Step", SqlDbType.NVarChar, 100));
            cmd.Parameters["@Step"].Value = (Step != null) ? Step.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@StepEng", SqlDbType.NVarChar, 100));
            cmd.Parameters["@StepEng"].Value = (StepEng != null) ? StepEng.Trim() : SqlString.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_ustep");
            }
            return Result;
        }
        /// <summary>
        /// Обновить детальный шаг внедрения проектов
        /// </summary>
        /// <param name="IDStep"></param>
        /// <param name="IDTemplateStepProject"></param>
        /// <param name="IDBigStep"></param>
        /// <param name="Step"></param>
        /// <param name="StepEng"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public int UpdateStepsProject(int IDStep,int IDTemplateStepProject,int IDBigStep, string Step, string StepEng, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPUpdateStepProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDStep", SqlDbType.Int));
            cmd.Parameters["@IDStep"].Value = IDStep;
            cmd.Parameters.Add(new SqlParameter("@IDTemplateStepProject", SqlDbType.Int));
            cmd.Parameters["@IDTemplateStepProject"].Value = IDTemplateStepProject;
            cmd.Parameters.Add(new SqlParameter("@IDBigStep", SqlDbType.Int));
            cmd.Parameters["@IDBigStep"].Value = IDBigStep;
            cmd.Parameters.Add(new SqlParameter("@Step", SqlDbType.NVarChar, 100));
            cmd.Parameters["@Step"].Value = (Step != null) ? Step.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@StepEng", SqlDbType.NVarChar, 100));
            cmd.Parameters["@StepEng"].Value = (StepEng != null) ? StepEng.Trim() : SqlString.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_ustep");
            }
            return Result;
        }
        /// <summary>
        /// Удалить детальный шаг проекта
        /// </summary>
        /// <param name="IDStep"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int DeleteStepsProject(int IDStep, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPDeleteStepProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDStep", SqlDbType.Int));
            cmd.Parameters["@IDStep"].Value = IDStep;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_dstep");
            }
            return Result;
        }

        #endregion

        #endregion
    }
    #endregion

    #region КЛАССЫ ПРОЕКТОВ
    public enum typeStatusProject : int { New = 0, Carryover = 1 }

    public enum typeCurrency : int { UAH = 980, USD = 840, EUR=978, RUB=643  }

    public enum statusProject : int { Study = 0, Run = 1, Closed = 2, Deleted = 3  }

    public enum typeConstruction : int { Not = 0, NewConstruction = 1, Upgrading = 2, Purchase = 3}

    public enum implementationProgram : int { Scales = 1, Procat =2 }

    [Serializable()]
    public class TypeProjectEntity
    {
        public int? Id
        {
            get;
            set;
        }
        public string TypeProject
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
    }  
 
    [Serializable()]
    public class TypeProjectContent : TypeProjectEntity
    {
        public string TypeProjectEng
        {
            get;
            set;
        }
        public string DescriptionEng
        {
            get;
            set;
        }
        public TypeProjectContent()
            : base()
        {

        }
    }   
    
    [Serializable()]
    public class ProjectEntity
    {
        public int? Id
        {
            get;
            set;
        }
        public int IDTypeProject
        {
            get;
            set;
        }
        public implementationProgram? IDImplementationProgram
        {
            get;
            set;
        }
        public int IDMenagerProject
        {
            get;
            set;
        }
        public int? IDReplacementProject
        {
            get;
            set;
        }
        public int IDSection
        {
            get;
            set;
        }
        public string SAPCode
        {
            get;
            set;
        }
        public string TypeString
        {
            get;
            set;
        }
        public typeStatusProject TypeStatus
        {
            get;
            set;
        }
        public decimal? Funding
        {
            get;
            set;
        }
        public typeCurrency? Currency
        {
            get;
            set;
        }
        public string FundingDescription
        {
            get;
            set;
        }
        public bool AllocationFunds
        {
            get;
            set;
        }
        public int? LineOwner
        {
            get;
            set;
        }
        public int Year
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public string Contractor
        {
            get;
            set;
        }
        public string DateContractor
        {
            get;
            set;
        }
        public statusProject Status
        {
            get;
            set;
        }
        public int? Effect
        {
            get;
            set;
        }
        public int? IDOrder
        {
            get;
            set;
        }
        public typeConstruction TypeConstruction
        {
            get;
            set;
        }
        public DateTime? Change
        {
            get;
            set;
        }
    }

    [Serializable()]
    public class ProjectContent : ProjectEntity
    {
        public string NameEng
        {
            get;
            set;
        }
        public string DescriptionEng
        {
            get;
            set;
        }
        public ProjectContent()
            : base()
        {

        }
    }   

    [Serializable()]
    public class ProjectStepDetaliContent 
    {
        public int? IDStepDetali
        {
            get;
            set;
        }
        public int IDProject
        {
            get;
            set;
        }
        public int IDStep
        {
            get;
            set;
        }
        public int Position
        {
            get;
            set;
        }
        public DateTime? FactStart
        {
            get;
            set;
        }
        public DateTime? FactStop
        {
            get;
            set;
        }
        public int Persent
        {
            get;
            set;
        }
        public string Coment
        {
            get;
            set;
        }
        public string Responsible
        {
            get;
            set;
        }
        public bool SkipStep
        {
            get;
            set;
        }
        public bool CurrentStep
        {
            get;
            set;
        }
    }

    [Serializable()]
    public class FilesStepDetaliEntity
    {
        public int? ID
        {
            get;
            set;
        }
        public int IDFile
        {
            get;
            set;
        }
    }

    [Serializable()]
    public class FilesStepDetaliContent : FilesStepDetaliEntity
    {
        public int IDStepDetali
        {
            get;
            set;
        }
        public FilesStepDetaliContent()
            : base()
        {

        }
    }

    [Serializable()]
    public class ProjectStatus 
    {
        public ProjectEntity project;
        public ProjectStepDetaliContent[] stepproject;
    }
    /// <summary>
    /// Колекция проектов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ProjectCollection<T> : IEnumerable<T> where T : ProjectStatus
    {
        classProject cp = new classProject();
        private List<T> _listProject = new List<T>();    // Перечень перьев
        /// <summary>
        /// Получить Проект по индексу
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public T GetProject(int pos)
        { return this._listProject[pos]; }
        /// <summary>
        /// Добавить Проект
        /// </summary>
        /// <param name="p"></param>
        public void AddProject(T p)
        {
            this._listProject.Add(p);
        }
        /// <summary>
        /// Очитить Проекты
        /// </summary>
        public void Clear()
        {
            this._listProject.Clear();
        }
        /// <summary>
        /// Получить количество Проектов
        /// </summary>
        public int Count { get { return this._listProject.Count; } }
        /// <summary>
        /// Загрузить проекты пренадлежащие программе , по определенным подразделениям, с указаным статусом
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="idsection"></param>
        /// <param name="status"></param>
        public void LoadImplementationProgram(implementationProgram ip, int[] idsection, statusProject? status) 
        {
            Clear();
            DataRow[] rows = cp.SelectCultureProgramProject((int)ip, idsection, (int?)status).Select();
            if (idsection != null)
            {
                // Показать указанные подразделения
                foreach (int id in idsection)
                {
                    DataRow[] rows_section = GetProject(id, rows);
                    if (rows_section.Count() > 0)
                    {
                        foreach (DataRow row in rows_section)
                        {
                            DataRow[] rows_step = cp.SelectStepDetaliProject((int)row["IDProject"], false).Select();
                            if (rows_step.Count() > 0)
                            {
                                //создадим ProjectStepDetaliContent[]
                                ProjectStepDetaliContent[] psd = new ProjectStepDetaliContent[rows_step.Count()];
                                int index = 0;
                                foreach (DataRow row_step in rows_step)
                                {
                                    psd[index] = GetProjectStepDetali(row_step);
                                    index++;
                                }
                                this.AddProject((T)new ProjectStatus() { project = GetCultureProject(row), stepproject = psd });
                            }
                        }
                    }
                }
            } else { 
            // Показать все
                foreach (DataRow row in rows)
                {
                    DataRow[] rows_step = cp.SelectStepDetaliProject((int)row["IDProject"], false).Select();
                    if (rows_step.Count() > 0)
                    {
                        //создадим ProjectStepDetaliContent[]
                        ProjectStepDetaliContent[] psd = new ProjectStepDetaliContent[rows_step.Count()];
                        int index = 0;
                        foreach (DataRow row_step in rows_step)
                        {
                            psd[index] = GetProjectStepDetali(row_step);
                            index++;
                        }
                        this.AddProject((T)new ProjectStatus() { project = GetCultureProject(row), stepproject = psd });
                    }
                }        
            }
        }
        /// <summary>
        /// Загрузить проекты пренадлежащие программе , по определенным подразделениям, с указаным статусом и учетом выбранного языка
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="idsection"></param>
        /// <param name="status"></param>
        /// <param name="ci"></param>
        public void LoadImplementationProgram(implementationProgram ip, int[] idsection, statusProject? status, CultureInfo ci) 
        {
            string currentLanguage = Thread.CurrentThread.CurrentCulture.Name;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(ci.Name);
            LoadImplementationProgram(ip, idsection, status);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(currentLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(currentLanguage);
        }
        /// <summary>
        /// вернуть проекты пренадлежащие указаному подразделению
        /// </summary>
        /// <param name="idsection"></param>
        /// <returns></returns>
        private DataRow[] GetProject(int idsection, DataRow[] rows) 
        {
            List<DataRow> list = new List<DataRow>();
            foreach (DataRow row in rows) 
            {
                if ((int)row["IDSection"] == idsection) { list.Add(row); }
            }
            DataRow[] resultrows = new DataRow[list.Count()];
            int index = 0;
            foreach (DataRow dr in list) { resultrows[index] = dr; index++; }
            return resultrows;
        }
        /// <summary>
        /// Вернуть тип ProjectStepDetaliContent
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private ProjectStepDetaliContent GetProjectStepDetali(DataRow row) 
        { 
            if (row == null) return null;
            return new ProjectStepDetaliContent()
            {
                IDStepDetali = row["IDStepDetali"] != DBNull.Value ? row["IDStepDetali"] as int? : null,
                IDProject = int.Parse(row["IDProject"].ToString()),
                IDStep = int.Parse(row["IDStep"].ToString()),
                Position = int.Parse(row["Position"].ToString()),
                FactStart = row["FactStart"] != DBNull.Value ? row["FactStart"] as DateTime? : null,
                FactStop = row["FactStop"] != DBNull.Value ? row["FactStop"] as DateTime? : null,
                Persent = int.Parse(row["Persent"].ToString()),
                Coment = row["Coment"].ToString(),
                Responsible = row["Responsible"].ToString(),
                SkipStep = bool.Parse(row["SkipStep"].ToString()),
                CurrentStep = bool.Parse(row["CurrentStep"].ToString()),
            };
        }
        /// <summary>
        /// вернуть тип ProjectStepDetaliContent
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private ProjectEntity GetCultureProject(DataRow row) 
        {
            if (row == null) return null;
            return new ProjectEntity()
                {
                    Id = row["IDProject"] != DBNull.Value ? row["IDProject"] as int? : null,
                    IDTypeProject = int.Parse(row["IDTypeProject"].ToString()),
                    IDImplementationProgram = row["IDImplementationProgram"] != DBNull.Value ? (implementationProgram?)int.Parse(row["IDImplementationProgram"].ToString()) : null,
                    IDMenagerProject = int.Parse(row["IDMenagerProject"].ToString()),
                    IDReplacementProject = row["IDReplacementProject"] != DBNull.Value ? row["IDReplacementProject"] as int? : null,
                    IDSection = int.Parse(row["IDSection"].ToString()),
                    SAPCode = row["SAPCode"] != DBNull.Value ? row["SAPCode"] as string : null,
                    TypeString = row["TypeString"] != DBNull.Value ? row["TypeString"] as string : null,
                    TypeStatus = (typeStatusProject)int.Parse(row["TypeStatus"].ToString()),
                    Funding = row["Funding"] != DBNull.Value ? row["Funding"] as decimal? : null,
                    Currency = row["Funding"] != DBNull.Value ? (typeCurrency?)int.Parse(row["Currency"].ToString()) : null,
                    FundingDescription = row["FundingDescription"] != DBNull.Value ? row["FundingDescription"] as string : null,
                    AllocationFunds = bool.Parse(row["AllocationFunds"].ToString()),
                    LineOwner = row["LineOwner"] != DBNull.Value ? row["LineOwner"] as int? : null,
                    Year = int.Parse(row["Year"].ToString()),
                    Name = row["Name"].ToString(),
                    //NameEng = row["NameEng"].ToString(), 
                    Description = row["Description"].ToString(),
                    //DescriptionEng = row["DescriptionEng"].ToString(),
                    Contractor = row["Contractor"] != DBNull.Value ? row["Contractor"] as string : null,
                    DateContractor = row["DateContractor"] != DBNull.Value ? row["DateContractor"] as string : null,
                    Status = (statusProject)int.Parse(row["Status"].ToString()),
                    Effect = row["Effect"] != DBNull.Value ? row["Effect"] as int? : null,
                    IDOrder = row["IDOrder"] != DBNull.Value ? row["IDOrder"] as int? : null,
                    TypeConstruction = (typeConstruction)int.Parse(row["TypeConstruction"].ToString()),
                    Change = row["Change"] != DBNull.Value ? row["Change"] as DateTime? : null,
                };
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        { return _listProject.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator()
        { return _listProject.GetEnumerator(); }
    }


    /// <summary>
    /// Класс управления проектами
    /// </summary>
    public class classProject : classBaseDB
    {
        #region ПОЛЯ classProject

        protected classFiles cfiles = new classFiles();
        protected classSiteMap csitemap = new classSiteMap();
        protected classSection csection = new classSection();

        protected string _FieldTypeProject = "[IDTypeProject] ,[TypeProject] ,[TypeProjectEng] ,[Description] ,[DescriptionEng]";
        protected string _NameTableTypeProject = WebConfigurationManager.AppSettings["tb_TypeProject"].ToString();
        protected string _NameSPInsertTypeProject = WebConfigurationManager.AppSettings["sp_InsertTypeProject"].ToString();
        protected string _NameSPUpdateTypeProject = WebConfigurationManager.AppSettings["sp_UpdateTypeProject"].ToString();
        protected string _NameSPDeleteTypeProject = WebConfigurationManager.AppSettings["sp_DeleteTypeProject"].ToString();
        protected string _FieldListProject = "[IDProject],[IDTypeProject],[IDImplementationProgram],[IDMenagerProject],[IDReplacementProject],[IDSection],[SAPCode],[TypeString],[TypeStatus],[Funding],[Currency],[FundingDescription],[AllocationFunds],[LineOwner] " +
        ",[Year],[Name],[NameEng],[Description],[DescriptionEng],[Contractor],[DateContractor],[Status],[Effect],[IDOrder],[TypeConstruction],[Change]";
        protected string _NameTableListProject = WebConfigurationManager.AppSettings["tb_ListProject"].ToString();
        protected string _NameSPInsertProject = WebConfigurationManager.AppSettings["sp_InsertProject"].ToString();
        protected string _NameSPUpdateProject = WebConfigurationManager.AppSettings["sp_UpdateProject"].ToString();
        protected string _NameSPDeleteProject = WebConfigurationManager.AppSettings["sp_DeleteProject"].ToString();
        protected string _FieldStepDetali = "[IDStepDetali],[IDProject],[IDStep],[Position],[FactStart],[FactStop],[Persent],[Coment],[Responsible],[SkipStep],[CurrentStep]";
        protected string _NameTableStepDetali = WebConfigurationManager.AppSettings["tb_StepDetali"].ToString();
        protected string _NameSPUpdateStepDetaliProject = WebConfigurationManager.AppSettings["sp_UpdateStepDetaliProject"].ToString();
        protected string _NameSPSkipStepDetaliProject = WebConfigurationManager.AppSettings["sp_SkipStepDetaliProject"].ToString();
        protected string _NameSPCurrentStepDetaliProject = WebConfigurationManager.AppSettings["sp_CurrentStepDetaliProject"].ToString();

        protected string _FieldFilesStepDetali = "[IDFileStepDetali],[IDStepDetali],[IDFile]";
        protected string _NameTableFilesStepDetali = WebConfigurationManager.AppSettings["tb_FilesStepDetali"].ToString();
        protected string _NameSPInsertFileStepDetali = WebConfigurationManager.AppSettings["sp_InsertFileStepDetali"].ToString();
        protected string _NameSPDeleteFileStepDetali = WebConfigurationManager.AppSettings["sp_DeleteFileStepDetali"].ToString();
        protected string _NameSPCreateStepProject = WebConfigurationManager.AppSettings["sp_CreateStepProject"].ToString();
        protected string _NameSPClearStepProject = WebConfigurationManager.AppSettings["sp_ClearStepProject"].ToString();

        #endregion 

        public classProject() 
        { 
        
        }

        #region Общие методы
        /// <summary>
        /// Прочесть строку ресурса (может переопределятся в потомках)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual string GetStringStrategicResource(string key)
        {
            ResourceManager resourceStrategic = new ResourceManager(typeof(ResourceStrategic));
            return resourceStrategic.GetString(key, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Получить статус проекта
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetStatusProject(object dataItem)
        {
            statusProject Status = (statusProject)int.Parse(DataBinder.Eval(dataItem, "Status").ToString());
            return GetStringStrategicResource(Status.ToString());
        }
        /// <summary>
        /// Вернуть название перечисления статуса проекта
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetEnumStatusProject(object dataItem)
        {
            statusProject Status = (statusProject)int.Parse(DataBinder.Eval(dataItem, "Status").ToString());
            return Status.ToString();
        }
        /// <summary>
        /// Вкрнуть статус проекта 
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetTypeStatus(object dataItem)
        {
            int Status = int.Parse(DataBinder.Eval(dataItem, "TypeStatus").ToString());
            return ((typeStatusProject)Status).ToString();
        }
        /// <summary>
        /// Вернуть денежные единицы
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetCurrency(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "Currency") == DBNull.Value) return null;            
            if (DataBinder.Eval(dataItem, "Funding") != DBNull.Value)
            {
                
                int Currency = int.Parse(DataBinder.Eval(dataItem, "Currency").ToString());
                //return ((typeCurrency)Currency).ToString();
                return GetCurrency(Currency);
            }
            return null;
        }
        /// <summary>
        /// Вернуть денежные единицы
        /// </summary>
        /// <param name="Currency"></param>
        /// <returns></returns>
        public string GetCurrency(int Currency)
        {
            return ((typeCurrency)Currency).ToString();
        }
        /// <summary>
        /// Вернуть состояние финансирования
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetAllocationFunds(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "AllocationFunds") != DBNull.Value)
            {
                //bool Currency = bool.Parse(DataBinder.Eval(dataItem, "AllocationFunds").ToString());
                //if (Currency) { return GetStringStrategicResource("AllocationFundsYes"); } else { return GetStringStrategicResource("AllocationFundsNo"); }
                return GetAllocationFunds(bool.Parse(DataBinder.Eval(dataItem, "AllocationFunds").ToString()));
            }
            return null;
        }
        /// <summary>
        /// Вернуть состояние финансирования
        /// </summary>
        /// <param name="AllocationFunds"></param>
        /// <returns></returns>
        public string GetAllocationFunds(bool AllocationFunds)
        {
                if (AllocationFunds) { return GetStringStrategicResource("AllocationFundsYes"); } else { return GetStringStrategicResource("AllocationFundsNo"); }
        }

        /// <summary>
        /// Получить название программы внедрения
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetImplementationProgram(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDImplementationProgram") != DBNull.Value)
            {
                implementationProgram ip = (implementationProgram)(int.Parse(DataBinder.Eval(dataItem, "IDImplementationProgram").ToString()));
                return GetStringStrategicResource(ip.ToString());
            }
            return null;
        }
        /// <summary>
        /// Получить название шаблона
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetTypeProject(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDTypeProject") != DBNull.Value)
            {
                TypeProjectEntity tp = GetCultureTypeProject(int.Parse(DataBinder.Eval(dataItem, "IDTypeProject").ToString()));
                return tp.TypeProject;
            }
            return "-";
        }
        /// <summary>
        /// Получить тип строительства
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetTypeConstruction(object dataItem)
        {
            return GetTypeConstruction(int.Parse(DataBinder.Eval(dataItem, "TypeConstruction").ToString()));
        }
        /// <summary>
        /// Получить тип строительства 
        /// </summary>
        /// <param name="typeConstruction"></param>
        /// <returns></returns>
        public string GetTypeConstruction(int typeConstruction)
        {
            typeConstruction TypeConstruction = (typeConstruction)typeConstruction;
            return GetStringStrategicResource(TypeConstruction.ToString());
        }
        /// <summary>
        /// Получить список файлов для просмотра
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetListFile(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDStepDetali") != DBNull.Value)
            {
                StringBuilder result = new StringBuilder(); 
                List<FilesStepDetaliEntity> list_fsd = GetFilesStepDetali(int.Parse(DataBinder.Eval(dataItem, "IDStepDetali").ToString()));
                if (list_fsd != null)
                {
                    result.Append("<ul>");
                    foreach (FilesStepDetaliEntity fsd in list_fsd)
                    {
                        FileEntity fe = cfiles.GetEntityFile(fsd.IDFile);
                        if (fe != null)
                        {
                            //result.Append(" <li><a href='File.ashx?id=" + fe.Id + "'>" + fe.FileName + "</a></li>");
                            result.Append(" <li><a href='" + csitemap.GetPathUrl("/File.ashx?id=") + fe.Id + "'>" + fe.FileName + "</a></li>");
                        }
                    }
                }
                result.Append("</ul>");
                return result.ToString();
            }
            return null;
        }
        /// <summary>
        /// вернуть файл с эконномическим эффектом
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetEffect(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "Effect") != DBNull.Value)
            {
                int idFile = int.Parse(DataBinder.Eval(dataItem, "Effect").ToString());
                FileEntity fe = cfiles.GetEntityFile(idFile);
                if (fe != null)
                {
                    //return "<a href='File.ashx?id=" + fe.Id + "'>" + fe.FileName + "</a>";
                    return "<a href='" + csitemap.GetPathUrl("/File.ashx?id=") + fe.Id + "'>" + fe.FileName + "</a>";
                }
            }
            return null;
        }
        /// <summary>
        /// Вернуть ID файла
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public int? GetIDFile(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "Effect") != DBNull.Value)
            {
                return int.Parse(DataBinder.Eval(dataItem, "Effect").ToString());
            }
            return null;
        }
        /// <summary>
        /// Получить название проекта
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetNameProject(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDProject") != DBNull.Value)
            {
                ProjectEntity tp = GetCultureProject(int.Parse(DataBinder.Eval(dataItem, "IDProject").ToString()));
                return tp.Name;
            }
            return null;
        }
        /// <summary>
        /// Вернуть СПП элемент и департамент 
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetSapDepartment(object dataItem)
        {
            string SAPCode = DataBinder.Eval(dataItem, "SAPCode").ToString();
            string Section = null;
            if (DataBinder.Eval(dataItem, "IDSection") != DBNull.Value) 
            {
                Section = csection.GetSectionCulture(int.Parse(DataBinder.Eval(dataItem, "IDSection").ToString()));
            }
            if (SAPCode != "") { return SAPCode + "/" + Section;}
            return Section;
        }
        /// <summary>
        /// Вернуть прямую ссылку на описание проекта
        /// </summary>
        /// <param name="IDProject"></param>
        /// <returns></returns>
        public string GetLinkProject(int IDProject)
        {
            return WebConfigurationManager.AppSettings["urlServer"].ToString() + "WebSite/Strategic/Project.aspx?Owner=11&prj=" + IDProject;
        }
        #endregion

        #region МЕТОДЫ TypeProject
        /// <summary>
        /// Вернуть все строчки таблицы
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]        
        public DataTable SelectTypeProject()
        {
            string sql = "SELECT " + this._FieldTypeProject + " FROM " + this._NameTableTypeProject + " ORDER BY [IDTypeProject]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную строку 
        /// </summary>
        /// <param name="IDTemplateStepProject"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]        
        public DataTable SelectTypeProject(int IDTypeProject)
        {
            string sql = "SELECT " + this._FieldTypeProject + " FROM " + this._NameTableTypeProject + " WHERE ([IDTypeProject] = " + IDTypeProject.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть все строки с учетом культуры
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureTypeProject()
        {
            string sql = "SELECT [IDTypeProject]" + base.GetCultureField("TypeProject") + base.GetCultureField("Description") + " FROM " + this._NameTableTypeProject + " ORDER BY [IDTypeProject]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную строку с учетом культуры
        /// </summary>
        /// <param name="IDTypeProject"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureTypeProject(int IDTypeProject)
        {
            string sql = "SELECT [IDTypeProject]" + base.GetCultureField("TypeProject") + base.GetCultureField("Description") + " FROM " + this._NameTableTypeProject + " WHERE ([IDTypeProject] = " + IDTypeProject.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить TypeProjectEntity типа проектов
        /// </summary>
        /// <param name="IDTypeProject"></param>
        /// <returns></returns>
        public TypeProjectEntity GetCultureTypeProject(int IDTypeProject) 
        {
            DataRow[] rows = SelectCultureTypeProject(IDTypeProject).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDTypeProject"] != DBNull.Value)
                {
                    return new TypeProjectEntity()
                    {
                        Id = int.Parse(rows[0]["IDTypeProject"].ToString()),
                        TypeProject = rows[0]["TypeProject"].ToString(),
                        Description = rows[0]["Description"].ToString(),
                    };
                }
            }
            return null;
        }


        /// <summary>
        /// Добавить новый тип проекта
        /// </summary>
        /// <param name="TypeProject"></param>
        /// <param name="TypeProjectEng"></param>
        /// <param name="Description"></param>
        /// <param name="DescriptionEng"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public int InsertTypeProject(string TypeProject, string TypeProjectEng, string Description, string DescriptionEng, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPInsertTypeProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TypeProject", SqlDbType.NVarChar, 50));
            cmd.Parameters["@TypeProject"].Value = (TypeProject != null) ? TypeProject.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@TypeProjectEng", SqlDbType.NVarChar, 50));
            cmd.Parameters["@TypeProjectEng"].Value = (TypeProjectEng != null) ? TypeProjectEng.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@Description"].Value = (Description != null) ? Description.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@DescriptionEng", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@DescriptionEng"].Value = (DescriptionEng != null) ? DescriptionEng.Trim() : SqlString.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_itypeproject");
            }
            return Result;
        }
        /// <summary>
        /// Обновить тип проекта
        /// </summary>
        /// <param name="IDTypeProject"></param>
        /// <param name="TypeProject"></param>
        /// <param name="TypeProjectEng"></param>
        /// <param name="Description"></param>
        /// <param name="DescriptionEng"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public int UpdateTypeProject(int IDTypeProject, string TypeProject, string TypeProjectEng, string Description, string DescriptionEng, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPUpdateTypeProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDTypeProject", SqlDbType.Int));
            cmd.Parameters["@IDTypeProject"].Value = IDTypeProject;
            cmd.Parameters.Add(new SqlParameter("@TypeProject", SqlDbType.NVarChar, 50));
            cmd.Parameters["@TypeProject"].Value = (TypeProject != null) ? TypeProject.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@TypeProjectEng", SqlDbType.NVarChar, 50));
            cmd.Parameters["@TypeProjectEng"].Value = (TypeProjectEng != null) ? TypeProjectEng.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@Description"].Value = (Description != null) ? Description.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@DescriptionEng", SqlDbType.NVarChar, 1000));
            cmd.Parameters["@DescriptionEng"].Value = (DescriptionEng != null) ? DescriptionEng.Trim() : SqlString.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_utypeproject");
            }
            return Result;
        }
        /// <summary>
        /// Удалить тип проекта
        /// </summary>
        /// <param name="IDTypeProject"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int DeleteTypeProject(int IDTypeProject, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPDeleteTypeProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDTypeProject", SqlDbType.Int));
            cmd.Parameters["@IDTypeProject"].Value = IDTypeProject;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_dtypeproject");
            }
            return Result;
        }
        #endregion

        #region МЕТОДЫ ListProject
        /// <summary>
        /// Получить список проектов
        /// 
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectProject()
        {
            string sql = "SELECT " + this._FieldListProject + " FROM " + this._NameTableListProject + " ORDER BY [IDProject]";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить выбранный проект
        /// </summary>
        /// <param name="IDProject"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectProject(int IDProject)
        {
            string sql = "SELECT " + this._FieldListProject + " FROM " + this._NameTableListProject + " WHERE ([IDProject] = " + IDProject.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить список проектов с учетом культуры и статусом не равным удален
        /// </summary>
        /// <param name="IDProject"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureProject()
        {
            string sql = "SELECT [IDProject],[IDTypeProject],[IDImplementationProgram],[IDMenagerProject],[IDReplacementProject],[IDSection],[SAPCode],[TypeString],[TypeStatus],[Funding],[Currency],[FundingDescription],[AllocationFunds],[LineOwner],[Year]" + 
                base.GetCultureField("Name") + base.GetCultureField("Description") +
                ",[Contractor],[DateContractor],[Status],[Effect],[IDOrder],[TypeConstruction],[Change] FROM " + this._NameTableListProject + " WHERE ([Status] < 3) ORDER BY IDTypeProject, IDProject ";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить выбранный проект с учетом культуры
        /// </summary>
        /// <param name="IDProject"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureProject(int IDProject)
        {
            string sql = "SELECT [IDProject],[IDTypeProject],[IDImplementationProgram],[IDMenagerProject],[IDReplacementProject],[IDSection],[SAPCode],[TypeString],[TypeStatus],[Funding],[Currency],[FundingDescription],[AllocationFunds],[LineOwner],[Year]" + 
                base.GetCultureField("Name") + base.GetCultureField("Description") +
                ",[Contractor],[DateContractor],[Status],[Effect],[IDOrder],[TypeConstruction],[Change] FROM " + this._NameTableListProject + " WHERE ([IDProject] = " + IDProject.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить выбранный проект с учетом культуры по указанному менеджеру с указанным статусом
        /// </summary>
        /// <param name="TypeMenager"></param>
        /// <param name="IDMenagerProject"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureProject(int TypeMenager, int? IDMenagerProject, int Status)
        {
            string sql = "SELECT [IDProject],[IDTypeProject],[IDImplementationProgram],[IDMenagerProject],[IDReplacementProject],[IDSection],[SAPCode],[TypeString],[TypeStatus],[Funding],[Currency],[FundingDescription],[AllocationFunds],[LineOwner],[Year]" +
                base.GetCultureField("Name") + base.GetCultureField("Description") +
                ",[Contractor],[DateContractor],[Status],[Effect],[IDOrder],[TypeConstruction],[Change] FROM " + this._NameTableListProject + " WHERE ([IDProject] > 0) ";//" WHERE ([Status] = " + Status.ToString() + ")";

            if (Status >= 0)
            { sql += " and ([Status] = " + Status.ToString() + ") "; }

            if (TypeMenager == 0)
            { sql += " and ([IDMenagerProject] ";}
            if (TypeMenager == 1)
            { sql += " and ([IDReplacementProject] ";}
            if (IDMenagerProject != 0) { sql += IDMenagerProject == null ? " is null )" : "= " + IDMenagerProject.ToString() + ")"; } else { sql += " is not null )"; }
            return base.Select(sql);
        }
        /// <summary>
        /// Получить выбранный проект с учетом культуры по указанному типу, менеджеру, году с указанным статусом.
        /// </summary>
        /// <param name="IDTypeProject"></param>
        /// <param name="Year"></param>
        /// <param name="IDMenagerProject"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureProject(int IDTypeProject, int Year, int IDMenagerProject, int Status)
        {
            string sql = "SELECT [IDProject],[IDTypeProject],[IDImplementationProgram],[IDMenagerProject],[IDReplacementProject],[IDSection],[SAPCode],[TypeString],[TypeStatus],[Funding],[Currency],[FundingDescription],[AllocationFunds],[LineOwner],[Year]" +
                base.GetCultureField("Name") + base.GetCultureField("Description") +
                ",[Contractor],[DateContractor],[Status],[Effect],[IDOrder],[TypeConstruction],[Change] FROM " + this._NameTableListProject + " WHERE ([IDProject] > 0)";
            if (IDTypeProject > 0)
            { sql += " and ([IDTypeProject] = " + IDTypeProject.ToString() + ") "; }
            if (Year > 0)
            { sql += " and ([Year] = " + Year.ToString() + ") "; }
            if (IDMenagerProject > 0)
            { sql += " and ([IDMenagerProject] = " + IDMenagerProject.ToString() + ") "; }
            if (Status >= 0)
            { sql += " and ([Status] = " + Status.ToString() + ") "; }
            else { sql += " and ([Status] < 3) ";}
            return base.Select(sql);
        }
        /// <summary>
        /// Получить список подразделений участвующих ав программе
        /// </summary>
        /// <param name="IDImplementationProgram"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectSectionProgramProject(int IDImplementationProgram)
        {
            string sql = "SELECT IDSection FROM " + this._NameTableListProject +
                " WHERE (IDImplementationProgram = " + IDImplementationProgram.ToString() + ") AND (IDImplementationProgram IS NOT NULL) GROUP BY IDSection ORDER BY IDSection";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить список проектов выбранной программы
        /// </summary>
        /// <param name="IDImplementationProgram"></param>
        /// <returns></returns>
        public DataTable SelectProgramProject(int IDImplementationProgram, int? Status)
        {
            string sql = "SELECT " + this._FieldListProject + " FROM " + this._NameTableListProject + " WHERE (IDImplementationProgram =" + IDImplementationProgram.ToString() + ") ";
            if (Status != null)
            { sql += " AND ([Status] = " + Status.ToString() + ") "; }
            else { sql += " AND ([Status] < 3)"; }
            return base.Select(sql);
        }
        /// <summary>
        /// Полусить список проектов выбранной программы с указаным статусом и по указаным подразделениям
        /// </summary>
        /// <param name="IDImplementationProgram"></param>
        /// <param name="idsection"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public DataTable SelectProgramProject(int IDImplementationProgram, int[] idsection, int? Status)
        {
            string WHERESection = "";
            if (idsection != null) 
            {
                WHERESection = " AND (IDSection IN (0";
                foreach (int id in idsection) 
                {
                    WHERESection += ", " + id.ToString();
                }
                WHERESection += ")) ";
            }
            string sql = "SELECT " + this._FieldListProject + " FROM " + this._NameTableListProject + " WHERE ((IDImplementationProgram =" + IDImplementationProgram.ToString() + ") " + WHERESection;
            if (Status != null)
            { sql += " AND ([Status] = " + Status.ToString() + ")) "; }
            else { sql += " AND ([Status] < 3)) "; }
            sql += " ORDER BY IDSection";
            return base.Select(sql);
        }
        /// <summary>
        /// Полусить список проектов выбранной программы с указаным статусом и по указаным подразделениям с учетом культуры
        /// </summary>
        /// <param name="IDImplementationProgram"></param>
        /// <param name="idsection"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public DataTable SelectCultureProgramProject(int IDImplementationProgram, int[] idsection, int? Status)
        {
            string WHERESection = "";
            if (idsection != null) 
            {
                WHERESection = " AND (IDSection IN (0";
                foreach (int id in idsection) 
                {
                    WHERESection += ", " + id.ToString();
                }
                WHERESection += ")) ";
            }
            string sql = "SELECT [IDProject],[IDTypeProject],[IDImplementationProgram],[IDMenagerProject],[IDReplacementProject],[IDSection],[SAPCode],[TypeString],[TypeStatus],[Funding],[Currency],[FundingDescription],[AllocationFunds],[LineOwner],[Year]" +
                base.GetCultureField("Name") + base.GetCultureField("Description") +
                ",[Contractor],[DateContractor],[Status],[Effect],[IDOrder],[TypeConstruction],[Change] FROM " + this._NameTableListProject + " WHERE ((IDImplementationProgram =" + IDImplementationProgram.ToString() + ") " + WHERESection;
            if (Status != null)
            { sql += " AND ([Status] = " + Status.ToString() + ")) "; }
            else { sql += " AND ([Status] < 3)) "; }
            sql += " ORDER BY IDSection";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить проекты пренадлежащие указаному подразделению и указанной программы
        /// </summary>
        /// <param name="IDSection"></param>
        /// <param name="IDImplementationProgram"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureProgramProject(int IDSection, int IDImplementationProgram, int? Status)
        {
           
            string sql = "SELECT [IDProject],[IDTypeProject],[IDImplementationProgram],[IDMenagerProject],[IDReplacementProject],[IDSection],[SAPCode],[TypeString],[TypeStatus],[Funding],[Currency],[FundingDescription],[AllocationFunds],[LineOwner],[Year]" +
                base.GetCultureField("Name") + base.GetCultureField("Description") +
                ",[Contractor],[DateContractor],[Status],[Effect],[IDOrder],[TypeConstruction],[Change] FROM " + this._NameTableListProject + " WHERE (IDSection = "+IDSection.ToString()+") AND (IDImplementationProgram =" +IDImplementationProgram.ToString()+ ") ";
            if (Status != null)
            { sql += " AND ([Status] = " + Status.ToString() + ") "; }
            else { sql += " AND ([Status] < 3)"; }
            return base.Select(sql);
        }
        /// <summary>
        /// Получить текущие шаги выполнения программы указанного проекта
        /// </summary>
        /// <param name="IDProject"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureProgramStepProject(int IDProject)
        {
            string sql = "SELECT IDStepDetali, IDProject, IDStep, Position, FactStart, FactStop, Persent, Coment, Responsible, SkipStep, CurrentStep FROM " + this._NameTableStepDetali +
                " WHERE (IDProject = " + IDProject.ToString() + ") AND (SkipStep = 0) AND (Persent > 0 AND Persent < 100)";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить текущие шаги выполнения программы указанного проекта c условием top1 = true - 1 строку top1 = false - все строки после первой 
        /// </summary>
        /// <param name="IDProject"></param>
        /// <param name="top1"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureProgramStepProject(int IDProject, bool top1)
        {
            string sql = "SELECT ";
            if (top1) {sql += "TOP(1) ";}
            sql +=  "IDStepDetali, IDProject, IDStep, Position, FactStart, FactStop, Persent, Coment, Responsible, SkipStep, CurrentStep FROM " + this._NameTableStepDetali +
                " WHERE (IDProject = " + IDProject.ToString() + ") AND (SkipStep = 0) AND (Persent > 0 AND Persent < 100) ";
            if (!top1)
            {
                sql += "and (IDStepDetali not in(SELECT top(1) IDStepDetali FROM " + this._NameTableStepDetali + " WHERE (IDProject = " + IDProject.ToString() + ") AND (SkipStep = 0) AND (Persent > 0 AND Persent < 100)))";            
            }
            return base.Select(sql);
        }
        /// <summary>
        /// Получить ProjectEntity по IDProject
        /// </summary>
        /// <param name="IDProject"></param>
        /// <returns></returns>
        public ProjectEntity GetCultureProject(int IDProject)
        {
            DataRow[] rows = SelectCultureProject(IDProject).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDProject"] != DBNull.Value)
                {

                    return new ProjectEntity()
                    {
                        Id = rows[0]["IDProject"] != DBNull.Value ? rows[0]["IDProject"] as int? : null,
                        IDTypeProject = int.Parse(rows[0]["IDTypeProject"].ToString()),
                        IDImplementationProgram = rows[0]["IDImplementationProgram"] != DBNull.Value ? (implementationProgram?)int.Parse(rows[0]["IDImplementationProgram"].ToString()) : null,
                        IDMenagerProject = int.Parse(rows[0]["IDMenagerProject"].ToString()),
                        IDReplacementProject = rows[0]["IDReplacementProject"] != DBNull.Value ? rows[0]["IDReplacementProject"] as int? : null,
                        IDSection = int.Parse(rows[0]["IDSection"].ToString()),
                        SAPCode = rows[0]["SAPCode"] != DBNull.Value ? rows[0]["SAPCode"] as string : null,
                        TypeString = rows[0]["TypeString"] != DBNull.Value ? rows[0]["TypeString"] as string : null,
                        TypeStatus = (typeStatusProject)int.Parse(rows[0]["TypeStatus"].ToString()),
                        Funding = rows[0]["Funding"] != DBNull.Value ? rows[0]["Funding"] as decimal? : null,
                        Currency = rows[0]["Funding"] != DBNull.Value ? (typeCurrency?)int.Parse(rows[0]["Currency"].ToString()) : null,
                        FundingDescription = rows[0]["FundingDescription"] != DBNull.Value ? rows[0]["FundingDescription"] as string : null,
                        AllocationFunds = bool.Parse(rows[0]["AllocationFunds"].ToString()),
                        LineOwner = rows[0]["LineOwner"] != DBNull.Value ? rows[0]["LineOwner"] as int? : null,
                        Year = int.Parse(rows[0]["Year"].ToString()),
                        Name = rows[0]["Name"].ToString(),
                        Description = rows[0]["Description"].ToString(),
                        Contractor = rows[0]["Contractor"] != DBNull.Value ? rows[0]["Contractor"] as string : null,
                        DateContractor = rows[0]["DateContractor"] != DBNull.Value ? rows[0]["DateContractor"] as string : null,
                        Status = (statusProject)int.Parse(rows[0]["Status"].ToString()),
                        Effect = rows[0]["Effect"] != DBNull.Value ? rows[0]["Effect"] as int? : null,
                        IDOrder = rows[0]["IDOrder"] != DBNull.Value ? rows[0]["IDOrder"] as int? : null,
                        TypeConstruction = (typeConstruction)int.Parse(rows[0]["TypeConstruction"].ToString()),
                        Change = rows[0]["Change"] != DBNull.Value ? rows[0]["Change"] as DateTime? : null,
                    };
                }
            }
            return null;
        }
        /// <summary>
        /// Добавить проект
        /// </summary>
        /// <param name="IDTemplateStepProject"></param>
        /// <param name="IDImplementationProgram"></param>
        /// <param name="IDTypeProject"></param>
        /// <param name="IDMenagerProject"></param>
        /// <param name="IDReplacementProject"></param>
        /// <param name="IDSection"></param>
        /// <param name="SAPCode"></param>
        /// <param name="TypeString"></param>
        /// <param name="TypeStatus"></param>
        /// <param name="Funding"></param>
        /// <param name="Currency"></param>
        /// <param name="FundingDescription"></param>
        /// <param name="AllocationFunds"></param>
        /// <param name="LineOwner"></param>
        /// <param name="Year"></param>
        /// <param name="Name"></param>
        /// <param name="NameEng"></param>
        /// <param name="Description"></param>
        /// <param name="DescriptionEng"></param>
        /// <param name="Contractor"></param>
        /// <param name="DateContractor"></param>
        /// <param name="Effect"></param>
        /// <param name="Status"></param>
        /// <param name="IDOrder"></param>
        /// <param name="TypeConstruction"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public int InsertProject(int IDTemplateStepProject, int? IDImplementationProgram, int IDTypeProject, int IDMenagerProject, int? IDReplacementProject, int IDSection, string SAPCode, string TypeString,
            int TypeStatus, decimal? Funding, int? Currency, string FundingDescription, bool AllocationFunds, int? LineOwner, int Year,
            string Name, string NameEng, string Description, string DescriptionEng, string Contractor, string DateContractor, int? Effect, int Status, int? IDOrder, int TypeConstruction, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPInsertProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDTemplateStepProject", SqlDbType.Int));
            cmd.Parameters["@IDTemplateStepProject"].Value = IDTemplateStepProject;
            cmd.Parameters.Add(new SqlParameter("@IDTypeProject", SqlDbType.Int));
            cmd.Parameters["@IDTypeProject"].Value = IDTypeProject;
            cmd.Parameters.Add(new SqlParameter("@IDImplementationProgram", SqlDbType.Int));
            cmd.Parameters["@IDImplementationProgram"].Value = (IDImplementationProgram != null) ? (int)IDImplementationProgram : SqlInt32.Null;   
            cmd.Parameters.Add(new SqlParameter("@IDMenagerProject", SqlDbType.Int));
            cmd.Parameters["@IDMenagerProject"].Value = IDMenagerProject;
            cmd.Parameters.Add(new SqlParameter("@IDReplacementProject", SqlDbType.Int));
            cmd.Parameters["@IDReplacementProject"].Value = (IDReplacementProject != null) ? (int)IDReplacementProject : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@IDSection", SqlDbType.Int));
            cmd.Parameters["@IDSection"].Value = IDSection;
            cmd.Parameters.Add(new SqlParameter("@SAPCode", SqlDbType.NVarChar, 50));
            cmd.Parameters["@SAPCode"].Value = (SAPCode != null) ? SAPCode.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@TypeString", SqlDbType.NVarChar, 50));
            cmd.Parameters["@TypeString"].Value = (TypeString != null) ? TypeString.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@TypeStatus", SqlDbType.Int));
            cmd.Parameters["@TypeStatus"].Value = TypeStatus;
            cmd.Parameters.Add(new SqlParameter("@Funding", SqlDbType.Money));
            cmd.Parameters["@Funding"].Value = (Funding != null) ? (decimal)Funding : SqlMoney.Null;
            cmd.Parameters.Add(new SqlParameter("@Currency", SqlDbType.Int));
            cmd.Parameters["@Currency"].Value = (Currency != null) ? (int)Currency : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@FundingDescription", SqlDbType.NVarChar, 50));
            cmd.Parameters["@FundingDescription"].Value = (FundingDescription != null) ? FundingDescription.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@AllocationFunds", SqlDbType.Bit));
            cmd.Parameters["@AllocationFunds"].Value = AllocationFunds;
            cmd.Parameters.Add(new SqlParameter("@LineOwner", SqlDbType.Int));
            cmd.Parameters["@LineOwner"].Value = (LineOwner != null) ? (int)LineOwner : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@Year", SqlDbType.Int));
            cmd.Parameters["@Year"].Value = Year;
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 1024));
            cmd.Parameters["@Name"].Value = (Name != null) ? Name.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@NameEng", SqlDbType.NVarChar, 1024));
            cmd.Parameters["@NameEng"].Value = (NameEng != null) ? NameEng.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 2048));
            cmd.Parameters["@Description"].Value = (Description != null) ? Description.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@DescriptionEng", SqlDbType.NVarChar, 2048));
            cmd.Parameters["@DescriptionEng"].Value = (DescriptionEng != null) ? DescriptionEng.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Contractor", SqlDbType.NVarChar, 100));
            cmd.Parameters["@Contractor"].Value = (Contractor != null) ? Contractor.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@DateContractor", SqlDbType.NVarChar, 100));
            cmd.Parameters["@DateContractor"].Value = (DateContractor != null) ? DateContractor.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Effect", SqlDbType.Int));
            cmd.Parameters["@Effect"].Value = (Effect != null) ? (int)Effect : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@IDOrder", SqlDbType.Int));
            cmd.Parameters["@IDOrder"].Value = (IDOrder != null) ? (int)IDOrder : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@TypeConstruction", SqlDbType.Int));
            cmd.Parameters["@TypeConstruction"].Value = TypeConstruction;
            cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int));
            cmd.Parameters["@Status"].Value = Status;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_iproject");
            }
            return Result;
        }
        /// <summary>
        /// Обновить проект
        /// </summary>
        /// <param name="IDProject"></param>
        /// <param name="IDTypeProject"></param>
        /// <param name="IDImplementationProgram"></param>
        /// <param name="IDMenagerProject"></param>
        /// <param name="IDReplacementProject"></param>
        /// <param name="IDSection"></param>
        /// <param name="SAPCode"></param>
        /// <param name="TypeString"></param>
        /// <param name="TypeStatus"></param>
        /// <param name="Funding"></param>
        /// <param name="Currency"></param>
        /// <param name="FundingDescription"></param>
        /// <param name="AllocationFunds"></param>
        /// <param name="LineOwner"></param>
        /// <param name="Year"></param>
        /// <param name="Name"></param>
        /// <param name="NameEng"></param>
        /// <param name="Description"></param>
        /// <param name="DescriptionEng"></param>
        /// <param name="Contractor"></param>
        /// <param name="DateContractor"></param>
        /// <param name="Effect"></param>
        /// <param name="Status"></param>
        /// <param name="IDOrder"></param>
        /// <param name="TypeConstruction"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public int UpdateProject(int IDProject, int IDTypeProject, int? IDImplementationProgram, int IDMenagerProject, int? IDReplacementProject, int IDSection, string SAPCode, string TypeString,
            int TypeStatus, decimal? Funding, int? Currency, string FundingDescription, bool AllocationFunds, int? LineOwner, int Year,
            string Name, string NameEng, string Description, string DescriptionEng, string Contractor, string DateContractor, int? Effect, int Status, int? IDOrder, int TypeConstruction, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPUpdateProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDProject", SqlDbType.Int));
            cmd.Parameters["@IDProject"].Value = IDProject;
            cmd.Parameters.Add(new SqlParameter("@IDTypeProject", SqlDbType.Int));
            cmd.Parameters["@IDTypeProject"].Value = IDTypeProject;
            cmd.Parameters.Add(new SqlParameter("@IDImplementationProgram", SqlDbType.Int));
            cmd.Parameters["@IDImplementationProgram"].Value = (IDImplementationProgram != null) ? (int)IDImplementationProgram : SqlInt32.Null;            
            cmd.Parameters.Add(new SqlParameter("@IDMenagerProject", SqlDbType.Int));
            cmd.Parameters["@IDMenagerProject"].Value = IDMenagerProject;
            cmd.Parameters.Add(new SqlParameter("@IDReplacementProject", SqlDbType.Int));
            cmd.Parameters["@IDReplacementProject"].Value = (IDReplacementProject != null) ? (int)IDReplacementProject : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@IDSection", SqlDbType.Int));
            cmd.Parameters["@IDSection"].Value = IDSection;
            cmd.Parameters.Add(new SqlParameter("@SAPCode", SqlDbType.NVarChar, 50));
            cmd.Parameters["@SAPCode"].Value = (SAPCode != null) ? SAPCode.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@TypeString", SqlDbType.NVarChar, 50));
            cmd.Parameters["@TypeString"].Value = (TypeString != null) ? TypeString.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@TypeStatus", SqlDbType.Int));
            cmd.Parameters["@TypeStatus"].Value = TypeStatus;
            cmd.Parameters.Add(new SqlParameter("@Funding", SqlDbType.Money));
            cmd.Parameters["@Funding"].Value = (Funding != null) ? (decimal)Funding : SqlMoney.Null;
            cmd.Parameters.Add(new SqlParameter("@Currency", SqlDbType.Int));
            cmd.Parameters["@Currency"].Value = (Currency != null) ? (int)Currency : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@FundingDescription", SqlDbType.NVarChar, 50));
            cmd.Parameters["@FundingDescription"].Value = (FundingDescription != null) ? FundingDescription.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@AllocationFunds", SqlDbType.Bit));
            cmd.Parameters["@AllocationFunds"].Value = AllocationFunds;
            cmd.Parameters.Add(new SqlParameter("@LineOwner", SqlDbType.Int));
            cmd.Parameters["@LineOwner"].Value = (LineOwner != null) ? (int)LineOwner : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@Year", SqlDbType.Int));
            cmd.Parameters["@Year"].Value = Year;
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 1024));
            cmd.Parameters["@Name"].Value = (Name != null) ? Name.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@NameEng", SqlDbType.NVarChar, 1024));
            cmd.Parameters["@NameEng"].Value = (NameEng != null) ? NameEng.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 2048));
            cmd.Parameters["@Description"].Value = (Description != null) ? Description.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@DescriptionEng", SqlDbType.NVarChar, 2048));
            cmd.Parameters["@DescriptionEng"].Value = (DescriptionEng != null) ? DescriptionEng.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Contractor", SqlDbType.NVarChar, 100));
            cmd.Parameters["@Contractor"].Value = (Contractor != null) ? Contractor.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@DateContractor", SqlDbType.NVarChar, 100));
            cmd.Parameters["@DateContractor"].Value = (DateContractor != null) ? DateContractor.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Effect", SqlDbType.Int));
            cmd.Parameters["@Effect"].Value = (Effect != null) ? (int)Effect : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@IDOrder", SqlDbType.Int));
            cmd.Parameters["@IDOrder"].Value = (IDOrder != null) ? (int)IDOrder : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@TypeConstruction", SqlDbType.Int));
            cmd.Parameters["@TypeConstruction"].Value = TypeConstruction;
            cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int));
            cmd.Parameters["@Status"].Value = Status;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_uproject");
            }
            return Result;
        }
        /// <summary>
        /// Удалить проект
        /// </summary>
        /// <param name="IDProject"></param>
        /// <param name="FullDelete"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int DeleteProject(int IDProject, bool FullDelete, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPDeleteProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDProject", SqlDbType.Int));
            cmd.Parameters["@IDProject"].Value = IDProject;
            cmd.Parameters.Add(new SqlParameter("@FullDelete", SqlDbType.Bit));
            cmd.Parameters["@FullDelete"].Value = FullDelete;
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
                if (FullDelete) { base.OutResultFormatText(Result, "mes_err_", "mes_info_dfullproject"); } 
                else { base.OutResultFormatText(Result, "mes_err_", "mes_info_dproject");}
            }
            return Result;
        }
        /// <summary>
        /// Имеет право указанный менеджер вносить изменения в указаный проект
        /// </summary>
        /// <param name="IDProject"></param>
        /// <param name="IDMenager"></param>
        /// <returns></returns>
        public bool BelongsMenegerToProject(int IDProject, int IDMenager) 
        {
            string sql = "SELECT IDProject FROM "+this._NameTableListProject +" WHERE ((IDMenagerProject = "+IDMenager.ToString()+") AND (IDProject = "+IDProject.ToString()+")) OR ((IDReplacementProject = "+IDMenager.ToString()+") AND (IDProject = "+IDProject.ToString()+"))";
            DataRow[] rows = base.Select(sql).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region МЕТОДЫ StepDetali
        /// <summary>
        /// Получить список делалных шагов всех проектов
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectStepDetali()
        {
            string sql = "SELECT " + this._FieldStepDetali + " FROM " + this._NameTableStepDetali + " ORDER BY [IDProject], [IDStepDetali]";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить детальный шаг
        /// </summary>
        /// <param name="IDProject"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectStepDetali(int IDStepDetali)
        {
            string sql = "SELECT " + this._FieldStepDetali + " FROM " + this._NameTableStepDetali + " WHERE ([IDStepDetali] = " + IDStepDetali.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IDStepDetali"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectStepDetaliProject(int IDProject)
        {
            string sql = "SELECT " + this._FieldStepDetali + " FROM " + this._NameTableStepDetali + " WHERE ([IDProject] = " + IDProject.ToString() + ") ORDER BY [IDStepDetali]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть шаги выполнения проекта по указанному проекту с учетом (показвыать все,пропущеные,непропущенные)
        /// </summary>
        /// <param name="IDStepDetali"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectStepDetaliProject(int IDProject, bool? skip)
        {
            string sql = "SELECT " + this._FieldStepDetali + " FROM " + this._NameTableStepDetali + " WHERE ([IDProject] = " + IDProject.ToString();
            if (skip != null) 
            {
                sql += " AND [SkipStep] = '" + ((bool)skip).ToString() + "'";
            }            
            sql += ") ORDER BY [IDStepDetali]";
            return base.Select(sql);
        }
        /// <summary>
        /// Обновить детальный шаг внедрения проекта
        /// </summary>
        /// <param name="IDStepDetali"></param>
        /// <param name="FactStart"></param>
        /// <param name="FactStop"></param>
        /// <param name="Persent"></param>
        /// <param name="Coment"></param>
        /// <param name="Responsible"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public int UpdateStepDetaliProject(int IDStepDetali, DateTime? FactStart,DateTime? FactStop, int Persent,string Coment,string Responsible,bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPUpdateStepDetaliProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDStepDetali", SqlDbType.Int));
            cmd.Parameters["@IDStepDetali"].Value = IDStepDetali;
            cmd.Parameters.Add(new SqlParameter("@FactStart", SqlDbType.DateTime));
            cmd.Parameters["@FactStart"].Value = (FactStart != null) ? (DateTime)FactStart : SqlDateTime.Null;
            cmd.Parameters.Add(new SqlParameter("@FactStop", SqlDbType.DateTime));
            cmd.Parameters["@FactStop"].Value = (FactStop != null) ? (DateTime)FactStop : SqlDateTime.Null;
            cmd.Parameters.Add(new SqlParameter("@Persent", SqlDbType.Int));
            cmd.Parameters["@Persent"].Value = Persent;
            cmd.Parameters.Add(new SqlParameter("@Coment", SqlDbType.NVarChar, 1024));
            cmd.Parameters["@Coment"].Value = (Coment != null) ? Coment.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@Responsible", SqlDbType.NVarChar, 1024));
            cmd.Parameters["@Responsible"].Value = (Responsible != null) ? Responsible.Trim() : SqlString.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_ustepdetaliproject");
            }
            return Result;
        }
        /// <summary>
        /// Пропустить детальный шаг
        /// </summary>
        /// <param name="IDStepDetali"></param>
        /// <param name="Skip"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Fill, true)]
        public int SkipStepDetaliProject(int IDStepDetali, bool Skip, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPSkipStepDetaliProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDStepDetali", SqlDbType.Int));
            cmd.Parameters["@IDStepDetali"].Value = IDStepDetali;
            cmd.Parameters.Add(new SqlParameter("@Skip", SqlDbType.Bit));
            cmd.Parameters["@Skip"].Value = Skip;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_statusstepdetaliproject");
            }
            return Result;
        }
        /// <summary>
        /// Установить текущий шаг внедрения проекта
        /// </summary>
        /// <param name="IDStepDetali"></param>
        /// <param name="Value"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Fill, true)]
        public int CurrentStepDetaliProject(int IDStepDetali, bool Value, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPCurrentStepDetaliProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDStepDetali", SqlDbType.Int));
            cmd.Parameters["@IDStepDetali"].Value = IDStepDetali;
            cmd.Parameters.Add(new SqlParameter("@Value", SqlDbType.Bit));
            cmd.Parameters["@Value"].Value = Value;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_statusstepdetaliproject");
            }
            return Result;
        }
        /// <summary>
        /// Создать детальные шаги внедрения проекта
        /// </summary>
        /// <param name="IDProject"></param>
        /// <param name="IDTemplateStepProject"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Fill, true)]
        public int CreateStepProject(int IDProject, int IDTemplateStepProject, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPCreateStepProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDProject", SqlDbType.Int));
            cmd.Parameters["@IDProject"].Value = IDProject;
            cmd.Parameters.Add(new SqlParameter("@IDTemplateStepProject", SqlDbType.Int));
            cmd.Parameters["@IDTemplateStepProject"].Value = IDTemplateStepProject;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_createstepproject");
            }
            return Result;
        }
        /// <summary>
        /// Сбросить детальные шаги внедрения проекта
        /// </summary>
        /// <param name="IDProject"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Fill, true)]
        public int ClearStepProject(int IDProject, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPClearStepProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDProject", SqlDbType.Int));
            cmd.Parameters["@IDProject"].Value = IDProject;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_clearstepproject");
            }
            return Result;
        }
        /// <summary>
        /// Вернуть количество активных шагов проекта 
        /// </summary>
        /// <param name="IDProject"></param>
        /// <returns></returns>
        public int CountActiveStepProject(int IDProject) 
        {
            string sql = "SELECT COUNT(IDProject) AS Count FROM " + this._NameTableStepDetali + " WHERE (Persent > 0 AND Persent < 100) AND (SkipStep = 0) and (IDProject=" + IDProject.ToString() + ") GROUP BY IDProject";
            DataRow[] rows = base.Select(sql).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["Count"] != DBNull.Value)
                {
                    return int.Parse(rows[0]["Count"].ToString());
                }
            }
            return 0;
        }
        /// <summary>
        /// Показать шаги проектов по которым подошол срок, Day указать за скоко дней заранее предупредить
        /// </summary>
        /// <param name="Day"></param>
        /// <returns></returns>
        public DataTable SelectCultureEndStepProject(int Day)
        {
            string sql = "SELECT ListProjects.IDProject " + base.GetCultureField("Name")+", StepDetali.IDStep, StepDetali.IDStepDetali, StepDetali.FactStop, StepDetali.Persent, ListProjects.IDImplementationProgram, ListProjects.IDMenagerProject, ListProjects.Change " +
                            "FROM " + this._NameTableListProject + " as ListProjects INNER JOIN " + this._NameTableStepDetali + " as StepDetali ON ListProjects.IDProject = StepDetali.IDProject " +
                            "WHERE (StepDetali.SkipStep = 0) AND ( StepDetali.FactStop IS NOT NULL) AND ( StepDetali.Persent > 0 AND StepDetali.Persent < 100) and ( StepDetali.FactStop <= DATEADD(Day,"+Day.ToString()+",GetDate()) and ListProjects.Status <2) " +
                            "ORDER BY  ListProjects.IDMenagerProject,  StepDetali.FactStop";
            return Select(sql);
        }
        /// <summary>
        /// Показать шаги проектов по которым подошол срок для указанного менеджера, Day указать за скоко дней заранее предупредить
        /// </summary>
        /// <param name="Day"></param>
        /// <returns></returns>
        public DataTable SelectCultureEndStepProject(int IDMenagerProject, int Day)
        {
            string sql = "SELECT ListProjects.IDProject " + base.GetCultureField("Name")+", StepDetali.IDStep, StepDetali.IDStepDetali, StepDetali.FactStop, StepDetali.Persent, ListProjects.IDImplementationProgram, ListProjects.IDMenagerProject, ListProjects.Change " +
                            "FROM " + this._NameTableListProject + " as ListProjects INNER JOIN " + this._NameTableStepDetali + " as StepDetali ON ListProjects.IDProject = StepDetali.IDProject " +
                            "WHERE (ListProjects.IDMenagerProject = "+IDMenagerProject.ToString()+") AND (StepDetali.SkipStep = 0) AND ( StepDetali.FactStop IS NOT NULL) AND ( StepDetali.Persent > 0 AND StepDetali.Persent < 100) and ( StepDetali.FactStop <= DATEADD(Day," + Day.ToString() + ",GetDate()) and ListProjects.Status <2) " +
                            "ORDER BY  ListProjects.IDMenagerProject,  StepDetali.FactStop";
            return Select(sql);
        }
        #endregion

        #region МЕТОДЫ FilesStepDetali
        /// <summary>
        /// Получить список файлов для детального шага внедрения проекта
        /// </summary>
        /// <param name="IDStepDetali"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectFilesStepDetali(int IDStepDetali)
        {
            string sql = "SELECT " + this._FieldFilesStepDetali + " FROM " + this._NameTableFilesStepDetali + " WHERE ([IDStepDetali] = " + IDStepDetali.ToString() + ") ORDER BY [IDFile]";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить список файлов FilesStepDetaliEntity
        /// </summary>
        /// <param name="IDStepDetali"></param>
        /// <returns></returns>
        public List<FilesStepDetaliEntity> GetFilesStepDetali(int IDStepDetali)
        {
            List<FilesStepDetaliEntity> list = new List<FilesStepDetaliEntity>();
            DataRow[] rows = SelectFilesStepDetali(IDStepDetali).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                foreach (DataRow dr in rows)
                {
                    list.Add(new FilesStepDetaliEntity()
                    {
                        ID = dr["IDFileStepDetali"] != DBNull.Value ? dr["IDFileStepDetali"] as int? : null,
                        IDFile = int.Parse(dr["IDFile"].ToString())
                    }
                    );
                }
                return list;
            }
            return null;
        }
        /// <summary>
        /// Получить расширенный список файлов FileEntity
        /// </summary>
        /// <param name="IDStepDetali"></param>
        /// <returns></returns>
        public List<FileEntity> GetFilesEntityStepDetali(int IDStepDetali)
        {
            List<FileEntity> list = new List<FileEntity>();
            DataRow[] rows = SelectFilesStepDetali(IDStepDetali).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                foreach (DataRow dr in rows)
                {
                    list.Add(cfiles.GetEntityFile(decimal.Parse(dr["IDFile"].ToString())));
                }
                return list;
            }
            return null;
        }
        /// <summary>
        /// Добавить файл
        /// </summary>
        /// <param name="IDStepDetali"></param>
        /// <param name="IDFile"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public int InsertFilesStepDetali(int IDStepDetali, decimal IDFile, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPInsertFileStepDetali, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDStepDetali", SqlDbType.Int));
            cmd.Parameters["@IDStepDetali"].Value = IDStepDetali;
            cmd.Parameters.Add(new SqlParameter("@IDFile", SqlDbType.Int));
            cmd.Parameters["@IDFile"].Value = IDFile;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_ifilestepdetali");
            }
            return Result;
        }
        /// <summary>
        /// Удалить файл из детального шага внедрения проекта
        /// </summary>
        /// <param name="IDStepDetali"></param>
        /// <param name="IDFile"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int DeleteFilesStepDetali(int IDStepDetali, decimal IDFile, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPDeleteFileStepDetali, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDStepDetali", SqlDbType.Int));
            cmd.Parameters["@IDStepDetali"].Value = IDStepDetali;
            cmd.Parameters.Add(new SqlParameter("@IDFile", SqlDbType.Int));
            cmd.Parameters["@IDFile"].Value = IDFile;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_dfilestepdetali");
            }
            return Result;
        }
        #endregion

        #region МЕТОДЫ ProgramProject


        /// <summary>
        /// Получить ProjectCollection программы внедрения проектов
        /// </summary>
        /// <param name="listSection"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public ProjectCollection<ProjectStatus> GetProgramProject(int[] listSection, implementationProgram ip) 
        {
            ProjectCollection<ProjectStatus> collproject = new ProjectCollection<ProjectStatus>();
            collproject.LoadImplementationProgram(ip, listSection, null);
            return collproject;
        }
        /// <summary>
        /// Получить ProjectCollection программы внедрения проектов с учетом языка
        /// </summary>
        /// <param name="listSection"></param>
        /// <param name="ip"></param>
        /// <param name="ci"></param>
        /// <returns></returns>
        public ProjectCollection<ProjectStatus> GetProgramProject(int[] listSection, implementationProgram ip, CultureInfo ci) 
        {
            ProjectCollection<ProjectStatus> collproject = new ProjectCollection<ProjectStatus>();
            collproject.LoadImplementationProgram(ip, listSection, null, ci);
            return collproject;
        }
        public class SectionProject
        {
            classSection cs = new classSection();
            private int IDSection;
            public string SectionFull { get { return cs.GetCultureSection(this.IDSection).SectionFull; } }
            private DataRow[] RowsSection;
            public DataRow[] ProjectsSection { get { return this.RowsSection; } }
            public SectionProject(int IDSection, DataRow[] RowsSection) 
            {
                this.IDSection = IDSection;
                this.RowsSection = RowsSection;
            }
        }
        /// <summary>
        /// Метод возвращает Html строку сформированного отчета по программе
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public StringBuilder HtmlStausProject(List<SectionProject> list, CultureInfo ci, string caption)
        {
            StringBuilder HtmlTable = new StringBuilder();
            //HtmlTable.Append("<style type='text/css'>");
            //HtmlTable.Append(" table { border: 1px solid #999999; color: #000000; border-radius: 3px; background-color: #F7F6F3; font-family: 'Arial'; width: 1700px; margin-top: 20px; font-size: 14px; margin-bottom: 20px; }");
            //HtmlTable.Append(" th { padding: 5px 10px 5px 5px; border: 1px solid #999999; color: #5B5B5B; font-weight: bold; word-wrap: hyphenate; text-align: center; text-transform: uppercase; background-color: #F7F6F3; }");
            //HtmlTable.Append(" th.departmen { font-family: 'Calibri'; background-color: #FFEB9C; text-transform: uppercase; text-align: left; padding-left: 10px; }");
            //HtmlTable.Append(" td { padding: 5px; border: 1px solid #999999; font-size: 14px; }");
            //HtmlTable.Append("</style>");
            HtmlTable.Append("<body face='Arial' encoding='koi8-r'>");
            classOrder co = new classOrder();
            if (list == null) { return HtmlTable; }
            if (caption != null) { HtmlTable.Append("<h1>" + caption + "</h1>"); }
            HtmlTable.Append("<table border='1' bgcolor='#F7F6F3' width='800'>");
            // заголовок
            HtmlTable.Append("<tr>");
            HtmlTable.Append("<th align='center' width='300'><font color='#000000' size='1'>НАИМЕНОВАНИЕ</font></th>");
            HtmlTable.Append("<th align='center' width='100'><font color='#000000' size='1'>ПРИКАЗ</font></th>");
            HtmlTable.Append("<th align='center' width='100'><font color='#000000' size='1'>ВИД СТРОИТЕЛЬСТВА</font></th>");
            HtmlTable.Append("<th align='center' width='200'><font color='#000000' size='1'>ЗАТРАТЫ (ТЫС.)</font></th>");
            HtmlTable.Append("<th align='center' width='100'><font color='#000000' size='1'>БЮДЖЕТ/КОНТРАКТ</font></th>");
            HtmlTable.Append("<th align='center' width='200'><font color='#000000' size='1'>СТАТУС</font></th>");
            HtmlTable.Append("<th align='center' width='300'><font color='#000000' size='1'>ПРИМЕЧАНИЕ</font></th>");
            HtmlTable.Append("<th align='center' width='100'><font color='#000000' size='1'>ОЖИДАЕМАЯ ДАТА</font></th>");
            HtmlTable.Append("<th align='center' width='100'><font color='#000000' size='1'>ДАТА ВЫПОЛНЕНИЯ РАБОТ ПО КОНТРАКТУ</font></th>");
            HtmlTable.Append("<th align='center' width='200'><font color='#000000' size='1'>ИСПОЛНИТЕЛЬ РАБОТ</font></th>");
            HtmlTable.Append("</tr>");
            foreach (SectionProject sp in list)
            {
                // заголовок подразделение
                HtmlTable.Append("<tr>");
                HtmlTable.Append("<th colspan='10' align='left' bgcolor='#FFEB9C' ><font color='#000000' size='2'>" + sp.SectionFull.ToUpper() + "</font></th>");
                HtmlTable.Append("</tr>");
                // Проекты
                foreach (DataRow dr in sp.ProjectsSection)
                {
                    HtmlTable.Append("<tr>");
                    HtmlTable.Append("<td align='left'><font color='#000000' size='1'>" + dr["Name"] + "</font></td>");
                    HtmlTable.Append("<td align='center'><font color='#000000' size='1'>" + co.GetNumDateOrder(dr["IDOrder"] as int?) + "&nbsp;" + "</font></td>");
                    HtmlTable.Append("<td align='center'><font color='#000000' size='1'>" + GetTypeConstruction((int)dr["TypeConstruction"]) + "&nbsp;" + "</font></td>");
                    HtmlTable.Append("<td align='center'><font color='#000000' size='1'>" + (dr["Funding"] != DBNull.Value ? (dr["Funding"] as decimal?).Value.ToString("###,###,##0.00") + "&nbsp;" + ((typeCurrency)(int)dr["Currency"]).ToString() : "&nbsp;") + "<br />" + GetAllocationFunds((bool)dr["AllocationFunds"]) + "</font></td>");
                    HtmlTable.Append("<td align='center'><font color='#000000' size='1'>" + dr["SAPCode"] + "&nbsp;" + "</font></td>");
                    HtmlTable.Append("<td align='center'><font color='#000000' size='1'>" + "&nbsp;" + "</font></td>");
                    HtmlTable.Append("<td align='left'><font color='#000000' size='1'>" + "&nbsp;" + "</font></td>");
                    HtmlTable.Append("<td align='center'><font color='#000000' size='1'>" + "&nbsp;" + "</font></td>");
                    HtmlTable.Append("<td align='center'><font color='#000000' size='1'>" + dr["DateContractor"] + "&nbsp;" + "</font></td>");
                    HtmlTable.Append("<td align='left'><font color='#000000' size='1'>" + dr["Contractor"] + "&nbsp;" + "</font></td>");
                    HtmlTable.Append("</tr>");
                }
            }
            HtmlTable.Append("</table>");
            HtmlTable.Append("</body>");
            return HtmlTable;
        }

        public List<SectionProject> GetProgramProject(int[] listSection, int IDImplementationProgram) 
        {
            DataSet ds = new DataSet();
            List<SectionProject> list = new List<SectionProject>();
            ds.Tables.Add(SelectProgramProject(IDImplementationProgram,null)); // получим список по программе
            // Сортируем по типу участкам
            var ResultTable = from ListTable in ds.Tables["Table"].AsEnumerable()
                              //orderby ListTable.Field<Int32>("TypeTable") descending
                              group ListTable by ListTable.Field<Int32>("IDSection") into g
                              select g;
            //Пройдемся по всем заказаным участкам
            if (listSection != null)
            {
                foreach (int idsection in listSection)
                {
                    DataRow[] drsection = getRowSection(idsection, ResultTable);
                    if (drsection != null)
                    {
                        list.Add(new SectionProject(idsection,drsection));
                        // название группы
                        // запустить цикл строк                        
                    }
                }
            }
            else 
            {
                foreach (IGrouping<Int32, DataRow> group in ResultTable)
                {
                    DataRow[] drsection = getRowSection(group.Key, ResultTable);
                    if (drsection != null)
                    {
                        list.Add(new SectionProject(group.Key, drsection));
                        // название группы
                        // запустить цикл строк                        
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// Возвращает перечень строк пренадлежащих выбранному подразделению
        /// </summary>
        /// <param name="idsection"></param>
        /// <param name="ResultTable"></param>
        /// <returns></returns>
        protected DataRow[] getRowSection(int idsection, IEnumerable ResultTable) 
        {
            foreach (IGrouping<Int32, DataRow> group in ResultTable)
            {
                // Цикл по всем объектам текущей группы.
                if (group.Key == idsection)
                {
                    DataRow[] drsection = new DataRow[group.Count()];
                    int index = 0;
                    foreach (DataRow dr in group)
                    {
                        drsection[index] = dr;
                        index++;
                    }
                    return drsection;
                }
            }
            return null;
        }

        #endregion

    }
    #endregion

    #region КЛАССЫ МЕНЕДЖЕРОВ ПРОЕКТОВ
    [Serializable()]
    public class MenagerProjectEntity
    {
        public int? ID
        {
            get;
            set;
        }
        public int IDUser
        {
            get;
            set;
        }
        public int? WPhone
        {
            get;
            set;
        }
        public Int64? MPhone
        {
            get;
            set;
        }
        public bool SuperMenager
        {
            get;
            set;
        }
    }
    [Serializable()]
    public class UserProjectEntity : MenagerProjectEntity 
    {
        public UserProjectEntity() : base() { }
        public string UserEnterprise { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public bool bDistribution { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Post { get; set; }
        public int IDSection { get; set; }
    }
  
    /// <summary>
    /// Класс управления менеджерами проектов
    /// </summary>
    public class classMenagerProject : classUsers
    {
        #region ПОЛЯ classProject
        protected string _FieldMenagerProject = "[IDMenagerProject],[IDUser],[WPhone],[MPhone],[SuperMenager]";
        protected string _NameTableMenagerProject = WebConfigurationManager.AppSettings["tb_MenagerProject"].ToString();
        protected string _NameSPInsertMenagerProject = WebConfigurationManager.AppSettings["sp_InsertMenagerProject"].ToString();
        protected string _NameSPUpdateMenagerProject = WebConfigurationManager.AppSettings["sp_UpdateMenagerProject"].ToString();
        protected string _NameSPDeleteMenagerProject = WebConfigurationManager.AppSettings["sp_DeleteMenagerProject"].ToString();

        #endregion 

        public classMenagerProject() { }

        #region Общие методы
        /// <summary>
        /// Получить ФИО
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public override string GetFIO(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDMenagerProject") != DBNull.Value)
            {
                MenagerProjectEntity mpe = GetMenagerProject(int.Parse(DataBinder.Eval(dataItem, "IDMenagerProject").ToString()));
                if (mpe != null)
                {
                    UserDetali ud = base.GetUserDetali(mpe.IDUser);
                    return ud.Surname + " " + ud.Name + " " + ud.Patronymic;
                }
            }
            return "-";
        }
        /// <summary>
        /// Получить имя пользователя
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public override string GetUserEnterprise(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDMenagerProject") != DBNull.Value)
            {
                MenagerProjectEntity mpe = GetMenagerProject(int.Parse(DataBinder.Eval(dataItem, "IDMenagerProject").ToString()));
                if (mpe != null)
                {
                    UserDetali ud = base.GetUserDetali(mpe.IDUser);
                    return ud.UserEnterprise;
                }
            }
            return "-";
        }
        /// <summary>
        /// Вернуть Email
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public override string GetEmail(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDMenagerProject") != DBNull.Value)
            {
                MenagerProjectEntity mpe = GetMenagerProject(int.Parse(DataBinder.Eval(dataItem, "IDMenagerProject").ToString()));
                if (mpe != null)
                {
                    UserDetali ud = base.GetUserDetali(mpe.IDUser);
                    return ud.Email;
                }
            }
            return null;
        }
        /// <summary>
        /// Получить рабочий телефон
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetWPhone(object dataItem, string signature)
        {
            if (DataBinder.Eval(dataItem, "IDMenagerProject") != DBNull.Value)
            {
                MenagerProjectEntity mpe = GetMenagerProject(int.Parse(DataBinder.Eval(dataItem, "IDMenagerProject").ToString()));
                if (mpe != null)
                {
                    if (mpe.WPhone != null) { return signature + ((int)mpe.WPhone).ToString("###-##-##"); }
                }
            }
            return "-";
        }
        /// <summary>
        /// Получить мобильный телефон
        /// </summary>
        /// <param name="dataItem"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public string GetMPhone(object dataItem, string signature)
        {
            if (DataBinder.Eval(dataItem, "IDMenagerProject") != DBNull.Value)
            {
                MenagerProjectEntity mpe = GetMenagerProject(int.Parse(DataBinder.Eval(dataItem, "IDMenagerProject").ToString()));
                if (mpe != null)
                {
                    if (mpe.MPhone != null) { return signature + ((int)mpe.MPhone).ToString("+38(0##)##-##-###"); }
                }
            }
            return "-";
        }
        /// <summary>
        /// Получить ФИО замещение менеджера проекта
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetReplacementFIO(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDReplacementProject") != DBNull.Value)
            {
                MenagerProjectEntity mpe = GetMenagerProject(int.Parse(DataBinder.Eval(dataItem, "IDReplacementProject").ToString()));
                if (mpe != null) 
                {
                    UserDetali ud = base.GetUserDetali(mpe.IDUser);
                    return ud.Surname + " " + ud.Name + " " + ud.Patronymic ;
                }
            }
            return null;
        }
        /// <summary>
        /// Получить Email заместителя
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetReplacementEmail(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDReplacementProject") != DBNull.Value)
            {
                MenagerProjectEntity mpe = GetMenagerProject(int.Parse(DataBinder.Eval(dataItem, "IDReplacementProject").ToString()));
                if (mpe != null) 
                {
                    UserDetali ud = base.GetUserDetali(mpe.IDUser);
                    return ud.Email;
                }
            }
            return null;
        }
        /// <summary>
        /// Получить рабочий телефон заместителя
        /// </summary>
        /// <param name="dataItem"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public string GetReplacementWPhone(object dataItem, string signature)
        {
            if (DataBinder.Eval(dataItem, "IDReplacementProject") != DBNull.Value)
            {
                MenagerProjectEntity mpe = GetMenagerProject(int.Parse(DataBinder.Eval(dataItem, "IDReplacementProject").ToString()));
                if (mpe != null) 
                {
                    if (mpe.WPhone != null) { return signature + ((int)mpe.WPhone).ToString("###-##-##"); }
                }
            }
            return null;
        }
        /// <summary>
        /// Получить мобильный телефон заместителя
        /// </summary>
        /// <param name="dataItem"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public string GetReplacementMPhone(object dataItem, string signature)
        {
            if (DataBinder.Eval(dataItem, "IDReplacementProject") != DBNull.Value)
            {
                MenagerProjectEntity mpe = GetMenagerProject(int.Parse(DataBinder.Eval(dataItem, "IDReplacementProject").ToString()));
                if (mpe != null) 
                {
                    if (mpe.MPhone != null) { return signature + ((int)mpe.MPhone).ToString("+38(0##)##-##-###"); }
                }
            }
            return null;
        }
        #endregion

        #region МЕТОДЫ MenagerProject
        /// <summary>
        /// Вернуть все строчки таблицы
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectMenagerProject()
        {
            string sql = "SELECT " + this._FieldMenagerProject + " FROM " + this._NameTableMenagerProject + " ORDER BY [IDMenagerProject]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную строку по IDMenagerProject
        /// </summary>
        /// <param name="IDTemplateStepProject"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectMenagerProject(int IDMenagerProject)
        {
            string sql = "SELECT " + this._FieldMenagerProject + " FROM " + this._NameTableMenagerProject + " WHERE ([IDMenagerProject] = " + IDMenagerProject.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную строку по IDUser
        /// </summary>
        /// <param name="IDUser"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectMenagerProjectToUser(int IDUser)
        {
            string sql = "SELECT " + this._FieldMenagerProject + " FROM " + this._NameTableMenagerProject + " WHERE ([IDUser] = " + IDUser.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить список менеджеров проектов без учета указаного менеджера
        /// </summary>
        /// <param name="IDMenagerProject"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable ListMenagerProject(int? IDMenagerProject) 
        {
            string sql = "SELECT  MenagerProject.IDMenagerProject, Users.UserEnterprise, Users.Surname, Users.Name, Users.Patronymic FROM " +
                this._NameTableMenagerProject + " AS MenagerProject INNER JOIN " + base._NameTableUsers + " AS Users ON  MenagerProject.IDUser =  Users.IDUser";
            if (IDMenagerProject != null)
            {
                sql += " WHERE ( MenagerProject.IDMenagerProject <> " + IDMenagerProject.ToString() + ")";
            }
            return base.Select(sql);
        }
        /// <summary>
        /// Получить список менеджеров проектов
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable ListMenagerProject() 
        {
            string sql = "SELECT MenagerProject.IDMenagerProject, Users.Surname + ' ' + Users.Name as MenagerProject FROM " + this._NameTableUsers + " as Users INNER JOIN " +
                            this._NameTableMenagerProject + " AS MenagerProject ON Users.IDUser = MenagerProject.IDUser";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить список менеджеров проектов без учета указаного менеджера с дополнительным условием выборки 0:value
        /// </summary>
        /// <param name="IDMenagerProject"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable DDLListMenagerProject(int? IDMenagerProject, string value)
        {
            DataTable dt = ListMenagerProject(IDMenagerProject);
            DataRow newRow = dt.NewRow();
            newRow[0] = 0;
            newRow[1] = base.GetStringResource(value);
            newRow[2] = null;
            newRow[3] = null;
            newRow[4] = null;
            dt.Rows.InsertAt(newRow, 0);
            return dt;
        }
        /// <summary>
        /// Получить MenagerProjectEntity по IDMenagerProject
        /// </summary>
        /// <param name="IDMenagerProject"></param>
        /// <returns></returns>
        public MenagerProjectEntity GetMenagerProject(int IDMenagerProject)
        {
            DataRow[] rows = SelectMenagerProject(IDMenagerProject).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDMenagerProject"] != DBNull.Value)
                {
                    return new MenagerProjectEntity()
                    {
                        ID = rows[0]["IDMenagerProject"] != DBNull.Value ? rows[0]["IDMenagerProject"] as int? : null,
                        IDUser = int.Parse(rows[0]["IDUser"].ToString()),
                        WPhone = rows[0]["WPhone"] != DBNull.Value ? rows[0]["WPhone"] as int? : null,
                        MPhone = rows[0]["MPhone"] != DBNull.Value ? rows[0]["MPhone"] as Int64? : null,
                        SuperMenager = bool.Parse(rows[0]["SuperMenager"].ToString())
                    };
                }
            }
            return null;
        }
        /// <summary>
        /// Получить MenagerProjectEntity по IDUser
        /// </summary>
        /// <param name="IDUser"></param>
        /// <returns></returns>
        public MenagerProjectEntity GetMenagerProjectToUser(int IDUser)
        {
            DataRow[] rows = SelectMenagerProjectToUser(IDUser).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDMenagerProject"] != DBNull.Value)
                {
                    return new MenagerProjectEntity()
                    {
                        ID = rows[0]["IDMenagerProject"] != DBNull.Value ? rows[0]["IDMenagerProject"] as int? : null,
                        IDUser = int.Parse(rows[0]["IDUser"].ToString()),
                        WPhone = rows[0]["WPhone"] != DBNull.Value ? rows[0]["WPhone"] as int? : null,
                        MPhone = rows[0]["MPhone"] != DBNull.Value ? rows[0]["MPhone"] as Int64? : null,
                        SuperMenager = bool.Parse(rows[0]["SuperMenager"].ToString())
                    };
                }
            }
            return null;
        }
        /// <summary>
        /// Получить список пользователей-менеджеров проектов
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable ListUserProject() 
        {
            string sql = "SELECT MenagerProject.IDMenagerProject, Users.IDUser, Users.UserEnterprise, Users.Description, Users.Email, Users.bDistribution, Users.Surname, " +
                            "Users.Name,  Users.Patronymic,  Users.Post,  MenagerProject.WPhone,  MenagerProject.MPhone,  MenagerProject.SuperMenager, Users.IDSection " +
                            "FROM " + this._NameTableMenagerProject + " as MenagerProject INNER JOIN " + this._NameTableUsers + " as Users ON  MenagerProject.IDUser =  Users.IDUser ";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить список UserProjectEntity (пользователей менеджеров)
        /// </summary>
        /// <returns></returns>
        public List<UserProjectEntity> GetUsersProject()
        {
            List<UserProjectEntity> list = new List<UserProjectEntity>();
            DataRow[] rows = ListUserProject().Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                foreach (DataRow dr in rows)
                {
                    if (dr["IDMenagerProject"] != DBNull.Value)
                    {
                        list.Add(new UserProjectEntity()
                        {
                            ID = dr["IDMenagerProject"] != DBNull.Value ? dr["IDMenagerProject"] as int? : null,
                            IDUser = int.Parse(dr["IDUser"].ToString()),
                            WPhone = dr["WPhone"] != DBNull.Value ? dr["WPhone"] as int? : null,
                            MPhone = dr["MPhone"] != DBNull.Value ? dr["MPhone"] as Int64? : null,
                            SuperMenager = bool.Parse(dr["SuperMenager"].ToString()),
                            UserEnterprise = dr["UserEnterprise"].ToString(),
                            Description = dr["Description"].ToString(),
                            Email = dr["Email"].ToString(),
                            bDistribution = bool.Parse(dr["bDistribution"].ToString()),
                            Surname = dr["Surname"].ToString(),
                            Name = dr["Name"].ToString(),
                            Patronymic = dr["Patronymic"].ToString(),
                            IDSection = int.Parse(dr["IDSection"].ToString()),
                            Post = dr["Post"].ToString(), 
                        });
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Получить IDMenagerProject по IDUser
        /// </summary>
        /// <param name="IDUser"></param>
        /// <returns></returns>
        public int? GetIDMenagerProject(int IDUser) 
        {
            MenagerProjectEntity mpe = GetMenagerProjectToUser(IDUser);
            if (mpe != null) { return mpe.ID; }
            return null;
        }
        /// <summary>
        /// Получить признак Boss менеджеров проетов
        /// </summary>
        /// <param name="IDUser"></param>
        /// <returns></returns>
        public bool GetBossMenagerProject(int IDUser) 
        {
            MenagerProjectEntity mpe = GetMenagerProjectToUser(IDUser);
            if (mpe != null) { return mpe.SuperMenager; }
            return false;
        }
        /// <summary>
        /// Добавить менеджера проектов
        /// </summary>
        /// <param name="IDUser"></param>
        /// <param name="WPhone"></param>
        /// <param name="MPhone"></param>
        /// <param name="SuperMenager"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public int InsertMenagerProject(int IDUser, int? WPhone, Int64? MPhone, bool SuperMenager, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPInsertMenagerProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDUser", SqlDbType.Int));
            cmd.Parameters["@IDUser"].Value = IDUser;
            cmd.Parameters.Add(new SqlParameter("@WPhone", SqlDbType.Int));
            cmd.Parameters["@WPhone"].Value = (WPhone != null) ? (int)WPhone : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@MPhone", SqlDbType.BigInt));
            cmd.Parameters["@MPhone"].Value = (MPhone != null) ? (int)MPhone : SqlInt64.Null;
            cmd.Parameters.Add(new SqlParameter("@SuperMenager", SqlDbType.Bit));
            cmd.Parameters["@SuperMenager"].Value = SuperMenager;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_imenagerproject");
            }
            return Result;
        }
        /// <summary>
        /// Обновить менеджера проектов
        /// </summary>
        /// <param name="IDMenagerProject"></param>
        /// <param name="IDUser"></param>
        /// <param name="WPhone"></param>
        /// <param name="MPhone"></param>
        /// <param name="SuperMenager"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public int UpdateMenagerProject(int IDMenagerProject, int IDUser, int? WPhone, Int64? MPhone, bool SuperMenager, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPUpdateMenagerProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDMenagerProject", SqlDbType.Int));
            cmd.Parameters["@IDMenagerProject"].Value = IDMenagerProject;
            cmd.Parameters.Add(new SqlParameter("@IDUser", SqlDbType.Int));
            cmd.Parameters["@IDUser"].Value = IDUser;
            cmd.Parameters.Add(new SqlParameter("@WPhone", SqlDbType.Int));
            cmd.Parameters["@WPhone"].Value = (WPhone != null) ? (int)WPhone : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@MPhone", SqlDbType.BigInt));
            cmd.Parameters["@MPhone"].Value = (MPhone != null) ? (int)MPhone : SqlInt64.Null;
            cmd.Parameters.Add(new SqlParameter("@SuperMenager", SqlDbType.Bit));
            cmd.Parameters["@SuperMenager"].Value = SuperMenager;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_umenagerproject");
            }
            return Result;
        }
        /// <summary>
        /// Удалить менеджера проектов
        /// </summary>
        /// <param name="IDMenagerProject"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int DeleteMenagerProject(int IDMenagerProject, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPDeleteMenagerProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDMenagerProject", SqlDbType.Int));
            cmd.Parameters["@IDMenagerProject"].Value = IDMenagerProject;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_dmenagerproject");
            }
            return Result;
        }
        #endregion

    }
    #endregion

    #region КЛАССЫ KPI

    [Serializable()]
    public class KPIEntity
    {
        public int? ID
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
    }

    [Serializable()]
    public class KPIContent : KPIEntity
    {
        public string NameEng
        {
            get;
            set;
        }
        public KPIContent()
            : base()
        {

        }
    }   

    [Serializable()]
    public class YearEntity
    {
        public int Min
        {
            get;
            set;
        }
        public int Max
        {
            get;
            set;
        }
    }

    [Serializable()]
    public class IndexKPIEntity
    {
        public int? ID
        {
            get;
            set;
        }
        public string IndexName
        {
            get;
            set;
        }
        public string IndexDescription
        {
            get;
            set;
        }
    }

    [Serializable()]
    public class IndexKPIContent : IndexKPIEntity
    {
        public string IndexNameEng
        {
            get;
            set;
        }
        public string IndexDescriptionEng
        {
            get;
            set;
        }
        public IndexKPIContent()
            : base()
        {

        }
    }   

    [Serializable()]
    public class KPIYearEntity
    {
        public int? ID { get; set; }
        public int IDKPIProject { get; set; }
        public int Year { get; set; }
        public decimal? Q1 { get; set; }
        public decimal? Q2 { get; set; }
        public decimal? Q3 { get; set; }
        public decimal? Q4 { get; set; }
    }

    [Serializable()]
    public class KPIProjectEntity : KPIYearEntity
    {
        public int IDKPI { get; set; }
        public int IDProject { get; set; }
        public int IndexKPI { get; set; }
        public decimal ROI { get; set; }
        public decimal NPV { get; set; }
        public KPIProjectEntity() : base() { }
    }

    /// <summary>
    /// Класс управления KPI
    /// </summary>
    public class classKPI : classBaseDB
    {
        #region ПОЛЯ classKPI
        protected string _FieldKPI= "[IDKPI],[Name],[NameEng]";
        protected string _NameTableKPI = WebConfigurationManager.AppSettings["tb_KPI"].ToString();
        protected string _NameSPInsertKPI = WebConfigurationManager.AppSettings["sp_InsertKPI"].ToString();
        protected string _NameSPUpdateKPI = WebConfigurationManager.AppSettings["sp_UpdateKPI"].ToString();
        protected string _NameSPDeleteKPI = WebConfigurationManager.AppSettings["sp_DeleteKPI"].ToString();

        protected string _FieldKPIYear= "[IDKPIYear],[IDKPIProject],[Year],[Q1],[Q2],[Q3],[Q4]";
        protected string _NameTableKPIYear = WebConfigurationManager.AppSettings["tb_KPIYear"].ToString();
        protected string _FieldKPIProject = "[IDKPIProject],[IDKPI],[IDProject],[IndexKPI],[ROI],[NPV]";
        protected string _NameTableKPIProject = WebConfigurationManager.AppSettings["tb_KPIProject"].ToString();
        protected string _NameSPInsertKPIProject = WebConfigurationManager.AppSettings["sp_InsertKPIProject"].ToString();
        protected string _NameSPUpdateKPIProject = WebConfigurationManager.AppSettings["sp_UpdateKPIProject"].ToString();
        protected string _NameSPDeleteKPIProject = WebConfigurationManager.AppSettings["sp_DeleteKPIProject"].ToString();
        protected string _FieldIndexKPI = "[IndexKPI],[IndexName],[IndexNameEng],[IndexDescription],[IndexDescriptionEng]";
        protected string _NameTableIndexKPI = WebConfigurationManager.AppSettings["tb_IndexKPI"].ToString();

        #endregion     

        public classKPI() { }

        #region Общие методы
        /// <summary>
        /// Вернуть название KPI
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetKPI(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDKPI") != DBNull.Value)
            {
                KPIEntity kpi = GetCultureKPI(int.Parse(DataBinder.Eval(dataItem, "IDKPI").ToString()));
                if (kpi != null){
                    return kpi.Name;
                }
            }
            return null;
        }
        /// <summary>
        /// Получить показатель KPI
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetIndexKPI(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IndexKPI") != DBNull.Value)
            {
                IndexKPIEntity index = GetCultureIndexKPI(int.Parse(DataBinder.Eval(dataItem, "IndexKPI").ToString()));
                if (index != null)
                {
                    return index.IndexName;
                }
            }
            return null;
        }
        #endregion

        #region МЕТОДЫ classKPI

        #region МЕТОДЫ KPI
        /// <summary>
        /// Вернуть все строчки таблицы
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectKPI()
        {
            string sql = "SELECT " + this._FieldKPI + " FROM " + this._NameTableKPI + " ORDER BY [IDKPI]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную строку 
        /// </summary>
        /// <param name="IDTemplateStepProject"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectKPI(int IDKPI)
        {
            string sql = "SELECT " + this._FieldKPI + " FROM " + this._NameTableKPI + " WHERE ([IDKPI] = " + IDKPI.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть все строки с учетом культуры
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureKPI()
        {
            string sql = "SELECT [IDKPI]" + base.GetCultureField("Name") + " FROM " + this._NameTableKPI + " ORDER BY [IDKPI]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную строку с учетом культуры
        /// </summary>
        /// <param name="IDTypeProject"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureKPI(int IDKPI)
        {
            string sql = "SELECT [IDKPI]" + base.GetCultureField("Name") + " FROM " + this._NameTableKPI + " WHERE ([IDKPI] = " + IDKPI.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить KPIEntity 
        /// </summary>
        /// <param name="IDTypeProject"></param>
        /// <returns></returns>
        public KPIEntity GetCultureKPI(int IDKPI)
        {
            DataRow[] rows = SelectCultureKPI(IDKPI).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDKPI"] != DBNull.Value)
                {
                    return new KPIEntity()
                    {
                        ID = int.Parse(rows[0]["IDKPI"].ToString()),
                        Name = rows[0]["Name"].ToString(),
                    };
                }
            }
            return null;
        }
        /// <summary>
        /// Добавить KPI
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="NameEng"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public int InsertKPI(string Name, string NameEng, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPInsertKPI, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 1024));
            cmd.Parameters["@Name"].Value = (Name != null) ? Name.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@NameEng", SqlDbType.NVarChar, 1024));
            cmd.Parameters["@NameEng"].Value = (NameEng != null) ? NameEng.Trim() : SqlString.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_ikpi");
            }
            return Result;
        }
        /// <summary>
        /// Обновить KPI
        /// </summary>
        /// <param name="IDKPI"></param>
        /// <param name="Name"></param>
        /// <param name="NameEng"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public int UpdateKPI(int IDKPI, string Name, string NameEng, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPUpdateKPI, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDKPI", SqlDbType.Int));
            cmd.Parameters["@IDKPI"].Value = IDKPI;
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 1024));
            cmd.Parameters["@Name"].Value = (Name != null) ? Name.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@NameEng", SqlDbType.NVarChar, 1024));
            cmd.Parameters["@NameEng"].Value = (NameEng != null) ? NameEng.Trim() : SqlString.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_ukpi");
            }
            return Result;
        }
        /// <summary>
        /// Удалить KPI
        /// </summary>
        /// <param name="IDKPI"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int DeleteKPI(int IDKPI, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPDeleteKPI, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDKPI", SqlDbType.Int));
            cmd.Parameters["@IDKPI"].Value = IDKPI;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_dkpi");
            }
            return Result;
        }
        #endregion

        #region МЕТОДЫ KPIProject
        /// <summary>
        /// Вернуть все строчки таблицы
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectKPIProject()
        {
            string sql = "SELECT " + this._FieldKPIProject + " FROM " + this._NameTableKPIProject + " ORDER BY [IDKPIProject]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную строку 
        /// </summary>
        /// <param name="IDKPIProject"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectKPIProject(int IDKPIProject)
        {
            string sql = "SELECT " + this._FieldKPIProject + " FROM " + this._NameTableKPIProject + " WHERE ([IDKPIProject] = " + IDKPIProject.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить KPIProjectEntity по IDKPIYear
        /// </summary>
        /// <param name="IDKPIYear"></param>
        /// <returns></returns>
        public KPIProjectEntity GetKPIProject(int IDKPIYear)
        {
            KPIYearEntity kpiy = GetKPIYear(IDKPIYear);
            if (kpiy == null) return null;
            DataRow[] rows = SelectKPIProject(kpiy.IDKPIProject).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDKPIProject"] != DBNull.Value)
                {
                    return new KPIProjectEntity()
                    {
                        ID = kpiy.ID,
                        IDKPIProject = kpiy.IDKPIProject,
                        IDProject = int.Parse(rows[0]["IDProject"].ToString()),
                        IDKPI = int.Parse(rows[0]["IDKPI"].ToString()),
                        IndexKPI = int.Parse(rows[0]["IndexKPI"].ToString()),
                        Year = kpiy.Year,
                        Q1 = kpiy.Q1,
                        Q2 = kpiy.Q2,
                        Q3 = kpiy.Q3,
                        Q4 = kpiy.Q4,
                        ROI = Decimal.Parse(rows[0]["ROI"].ToString()),
                        NPV = Decimal.Parse(rows[0]["NPV"].ToString()),
                    };
                }
            }
            return null;
        }
        /// <summary>
        /// Получить KPI выбранных пректов
        /// </summary>
        /// <param name="IDKPI"></param>
        /// <param name="IDProject"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectKPIProject(int IDKPI, int IDProject, int Year)
        {
            string sql = "SELECT KPIYear.IDKPIYear, KPIProject.IDKPIProject, KPIProject.IDKPI, KPIProject.IDProject, KPIProject.IndexKPI, KPIYear.Year, KPIYear.Q1, KPIYear.Q2, KPIYear.Q3, KPIYear.Q4, KPIProject.ROI, KPIProject.NPV " +
                            "FROM " + this._NameTableKPIProject + " AS KPIProject LEFT OUTER JOIN " + this._NameTableKPIYear + " AS KPIYear ON  KPIProject.IDKPIProject = KPIYear.IDKPIProject WHERE (KPIYear.Year = " + Year.ToString()+ ") ";
                            if (IDKPI>0)
                            {sql += " AND (KPIProject.IDKPI = "+IDKPI.ToString()+")";}
                            if (IDProject > 0)
                            { sql += " AND (KPIProject.IDProject = "+IDProject.ToString()+") "; } 
                            sql += "ORDER BY KPIProject.IDKPI, KPIProject.IDProject";
            return base.Select(sql);
        }
        /// <summary>
        /// Добавить KPI проекта
        /// </summary>
        /// <param name="IDProject"></param>
        /// <param name="IDKPI"></param>
        /// <param name="IndexKPI"></param>
        /// <param name="YearStart"></param>
        /// <param name="YearStop"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public int InsertKPIProject(int IDProject, int IDKPI, int IndexKPI, int YearStart,int YearStop, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPInsertKPIProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDProject", SqlDbType.Int));
            cmd.Parameters["@IDProject"].Value = IDProject;
            cmd.Parameters.Add(new SqlParameter("@IDKPI", SqlDbType.Int));
            cmd.Parameters["@IDKPI"].Value = IDKPI;
            cmd.Parameters.Add(new SqlParameter("@IndexKPI", SqlDbType.Int));
            cmd.Parameters["@IndexKPI"].Value = IndexKPI;
            cmd.Parameters.Add(new SqlParameter("@YearStart", SqlDbType.Int));
            cmd.Parameters["@YearStart"].Value = YearStart;
            cmd.Parameters.Add(new SqlParameter("@YearStop", SqlDbType.Int));
            cmd.Parameters["@YearStop"].Value = YearStop;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_ikpiproject");
            }
            return Result;
        }
        /// <summary>
        /// Обновить KPI проекта
        /// </summary>
        /// <param name="IDKPIYear"></param>
        /// <param name="IDKPIProject"></param>
        /// <param name="IndexKPI"></param>
        /// <param name="Q1"></param>
        /// <param name="Q2"></param>
        /// <param name="Q3"></param>
        /// <param name="Q4"></param>
        /// <param name="ROI"></param>
        /// <param name="NPV"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public int UpdateKPIProject(int IDKPIYear, int IDKPIProject, int IndexKPI, decimal? Q1, decimal? Q2, decimal? Q3, decimal? Q4, decimal ROI, decimal NPV, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPUpdateKPIProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDKPIYear", SqlDbType.Int));
            cmd.Parameters["@IDKPIYear"].Value = IDKPIYear;
            cmd.Parameters.Add(new SqlParameter("@IDKPIProject", SqlDbType.Int));
            cmd.Parameters["@IDKPIProject"].Value = IDKPIProject;
            cmd.Parameters.Add(new SqlParameter("@IndexKPI", SqlDbType.Int));
            cmd.Parameters["@IndexKPI"].Value = IndexKPI;
            cmd.Parameters.Add(new SqlParameter("@Q1", SqlDbType.Real));
            cmd.Parameters["@Q1"].Value = (Q1 != null) ? (Single)Q1 : SqlSingle.Null;
            cmd.Parameters.Add(new SqlParameter("@Q2", SqlDbType.Real));
            cmd.Parameters["@Q2"].Value = (Q2 != null) ? (Single)Q2 : SqlSingle.Null;
            cmd.Parameters.Add(new SqlParameter("@Q3", SqlDbType.Real));
            cmd.Parameters["@Q3"].Value = (Q3 != null) ? (Single)Q3 : SqlSingle.Null;
            cmd.Parameters.Add(new SqlParameter("@Q4", SqlDbType.Real));
            cmd.Parameters["@Q4"].Value = (Q4 != null) ? (Single)Q4 : SqlSingle.Null;
            cmd.Parameters.Add(new SqlParameter("@ROI", SqlDbType.Real));
            cmd.Parameters["@ROI"].Value = ROI;
            cmd.Parameters.Add(new SqlParameter("@NPV", SqlDbType.Real));
            cmd.Parameters["@NPV"].Value = NPV;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_ukpiproject");
            }
            return Result;
        }
        /// <summary>
        /// Удалить строек KPI проекта
        /// </summary>
        /// <param name="IDKPIProject"></param>
        /// <param name="IDKPIYear"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int DeleteKPIProject(int? IDKPIProject, int? IDKPIYear, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPDeleteKPIProject, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDKPIProject", SqlDbType.Int));
            cmd.Parameters["@IDKPIProject"].Value = (IDKPIProject != null) ? (int)IDKPIProject : SqlInt32.Null; 
            cmd.Parameters.Add(new SqlParameter("@IDKPIYear", SqlDbType.Int));
            cmd.Parameters["@IDKPIYear"].Value = (IDKPIYear != null) ? (int)IDKPIYear : SqlInt32.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_dkpiproject");
            }
            return Result;
        }
        #endregion

        #region МЕТОДЫ KPIYear
        /// <summary>
        /// Вернуть все строчки таблицы
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectKPIYear()
        {
            string sql = "SELECT " + this._FieldKPIYear + " FROM " + this._NameTableKPIYear + " ORDER BY [IDKPIYear]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную строку 
        /// </summary>
        /// <param name="IDKPIYear"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectKPIYear(int IDKPIYear)
        {
            string sql = "SELECT " + this._FieldKPIYear + " FROM " + this._NameTableKPIYear + " WHERE ([IDKPIYear] = " + IDKPIYear.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить KPIYearEntity по IDKPIYear
        /// </summary>
        /// <param name="IDKPIYear"></param>
        /// <returns></returns>
        public KPIYearEntity GetKPIYear(int IDKPIYear)
        {
            DataRow[] rows = SelectKPIYear(IDKPIYear).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDKPIYear"] != DBNull.Value)
                {
                    return new KPIYearEntity()
                    {
                        ID = int.Parse(rows[0]["IDKPIYear"].ToString()),
                        IDKPIProject = int.Parse(rows[0]["IDKPIProject"].ToString()),
                        Year = int.Parse(rows[0]["Year"].ToString()),
                        Q1 = rows[0]["Q1"] != DBNull.Value ? rows[0]["Q1"] as decimal? : null,
                        Q2 = rows[0]["Q2"] != DBNull.Value ? rows[0]["Q2"] as decimal? : null,
                        Q3 = rows[0]["Q3"] != DBNull.Value ? rows[0]["Q3"] as decimal? : null,
                        Q4 = rows[0]["Q4"] != DBNull.Value ? rows[0]["Q4"] as decimal? : null,
                    };
                }
            }
            return null;
        }
        /// <summary>
        /// Получить YearEntity минимальное значение и макстмальное значение года 
        /// </summary>
        /// <param name="IDTypeProject"></param>
        /// <returns></returns>
        public YearEntity GetYearKPI()
        {
            string sql = "SELECT MIN(DISTINCT Year) AS MIN, MAX(DISTINCT Year) AS MAX  FROM " + this._NameTableKPIYear;
            DataRow[] rows = base.Select(sql).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if ((rows[0]["MIN"] != DBNull.Value) & (rows[0]["MAX"] != DBNull.Value))
                {
                    return new YearEntity()
                    {
                        Min = int.Parse(rows[0]["MIN"].ToString()),
                        Max = int.Parse(rows[0]["MAX"].ToString()),
                    };
                }
            }
            return null;
        }
        #endregion

        #region МЕТОДЫ IndexKPI
        /// <summary>
        /// Вернуть все строчки таблицы
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectIndexKPI()
        {
            string sql = "SELECT " + this._FieldIndexKPI + " FROM " + this._NameTableIndexKPI + " ORDER BY [IndexKPI]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную строку 
        /// </summary>
        /// <param name="IDTemplateStepProject"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectIndexKPI(int IndexKPI)
        {
            string sql = "SELECT " + this._FieldIndexKPI + " FROM " + this._NameTableIndexKPI + " WHERE ([IndexKPI] = " + IndexKPI.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть все строки с учетом культуры
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureIndexKPI()
        {
            string sql = "SELECT [IndexKPI]" + base.GetCultureField("IndexName") + base.GetCultureField("IndexDescription") + " FROM " + this._NameTableIndexKPI + " ORDER BY [IndexKPI]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную строку с учетом культуры
        /// </summary>
        /// <param name="IDTypeProject"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureIndexKPI(int IndexKPI)
        {
            string sql = "SELECT [IndexKPI]" + base.GetCultureField("IndexName") + base.GetCultureField("IndexDescription") + " FROM " + this._NameTableIndexKPI + " WHERE ([IndexKPI] = " + IndexKPI.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить IndexKPIEntity
        /// </summary>
        /// <param name="IndexKPI"></param>
        /// <returns></returns>
        public IndexKPIEntity GetCultureIndexKPI(int IndexKPI)
        {
            DataRow[] rows = SelectCultureIndexKPI(IndexKPI).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IndexKPI"] != DBNull.Value)
                {
                    return new IndexKPIEntity()
                    {
                        ID = int.Parse(rows[0]["IndexKPI"].ToString()),
                        IndexName = rows[0]["IndexName"].ToString(),
                        IndexDescription = rows[0]["IndexDescription"].ToString(),
                    };
                }
            }
            return null;
        }
        #endregion

        #endregion

    }
    #endregion

    #region КЛАСС classOrder

    [Serializable()]
    public class TypeOrderEntity
    {
        public int? ID
        {
            get;
            set;
        }
        public string TypeOrder
        {
            get;
            set;
        }
    }

    [Serializable()]
    public class TypeOrderContent : TypeOrderEntity
    {
        public string TypeOrderEng
        {
            get;
            set;
        }
        public TypeOrderContent()
            : base()
        {

        }
    }

    [Serializable()]
    public class OrderEntity
    {
        public int? ID
        {
            get;
            set;
        }
        public int IDTypeOrder
        {
            get;
            set;
        }
        public int? NumOrder
        {
            get;
            set;
        }
        public DateTime? DateOrder
        {
            get;
            set;
        }
        public string Order
        {
            get;
            set;
        }
        public int? IDFile
        {
            get;
            set;
        }
    }

    [Serializable()]
    public class OrderContent : OrderEntity
    {
        public string OrderEng
        {
            get;
            set;
        }
        public int? IDFileEng
        {
            get;
            set;
        }
        public OrderContent()
            : base()
        {

        }
    }

    public class classOrder : classBaseDB
    {
        #region ПОЛЯ classOrder
        protected classFiles cfiles = new classFiles();
        protected classSiteMap csitemap = new classSiteMap();
        ResourceManager resourceStrategic = new ResourceManager(typeof(ResourceStrategic));

        protected string _FieldTypeOrder = "[IDTypeOrder],[TypeOrder],[TypeOrderEng]";
        protected string _NameTableTypeOrder = WebConfigurationManager.AppSettings["tb_TypeOrder"].ToString();
        protected string _NameSPUpdateTypeOrder = WebConfigurationManager.AppSettings["sp_UpdateTypeOrder"].ToString();
        protected string _NameSPInsertTypeOrder = WebConfigurationManager.AppSettings["sp_InsertTypeOrder"].ToString();
        protected string _NameSPDeleteTypeOrder = WebConfigurationManager.AppSettings["sp_DeleteTypeOrder"].ToString();
        protected string _FieldListOrder = "[IDOrder],[IDTypeOrder],[NumOrder],[DateOrder],[Order],[OrderEng],[IDFile],[IDFileEng]";
        protected string _NameTableListOrder = WebConfigurationManager.AppSettings["tb_ListOrder"].ToString();
        protected string _NameSPUpdateOrder = WebConfigurationManager.AppSettings["sp_UpdateOrder"].ToString();
        protected string _NameSPInsertOrder = WebConfigurationManager.AppSettings["sp_InsertOrder"].ToString();
        protected string _NameSPDeleteOrder = WebConfigurationManager.AppSettings["sp_DeleteOrder"].ToString();
        #endregion     

        public classOrder() { }

        #region Общие методы
        /// <summary>
        /// Прочесть ресурсы resourceStrategic
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual string GetStringStrategicResource(string key)
        {
            return resourceStrategic.GetString(key, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Вернуть тип документа
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetTypeOrder(object dataItem)
        {
            if (DataBinder.Eval(dataItem, "IDTypeOrder") != DBNull.Value)
            {
                TypeOrderEntity to = GetCultureTypeOrder(int.Parse(DataBinder.Eval(dataItem, "IDTypeOrder").ToString()));
                if (to != null)
                {
                    return to.TypeOrder;
                }
            }
            return null;
        }
        /// <summary>
        /// Вернуть файл документа
        /// </summary>
        /// <param name="dataItem"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetFileOrder(object dataItem, string name)
        {
            if (name == null) return null;            
            if (DataBinder.Eval(dataItem, name) != DBNull.Value)
            {

                int idFile = int.Parse(DataBinder.Eval(dataItem, name).ToString());
                FileEntity fe = cfiles.GetEntityFile(idFile);
                if (fe != null)
                {
                    return "<a href='" + csitemap.GetPathUrl("/File.ashx?id=") + fe.Id + "'>" + fe.FileName + "</a>";
                }
            }
            return null;
        }
        /// <summary>
        /// Вернуть номер дату и ссылку на файл документа
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetNumDateOrder(object dataItem)
        {

            if (DataBinder.Eval(dataItem, "IDOrder") != DBNull.Value)
            {

                int IDOrder = int.Parse(DataBinder.Eval(dataItem, "IDOrder").ToString());
                OrderEntity oe = GetCultureOrderEntity(IDOrder);
                if (oe.IDFile != null)
                {
                    FileEntity fe = cfiles.GetEntityFile((decimal)oe.IDFile);
                    if (fe != null)
                    {
                        return "<a href='" + csitemap.GetPathUrl("/File.ashx?id=") + fe.Id + "'>" +
                            GetNumDateOrder(IDOrder)  + "</a>";
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Получить номер и дату приказа
        /// </summary>
        /// <param name="IDOrder"></param>
        /// <returns></returns>
        public string GetNumDateOrder(int? IDOrder)
        {
            if (IDOrder==null) return null;
            OrderEntity oe = GetCultureOrderEntity((int)IDOrder);
            if (oe.IDFile != null)
            {
                FileEntity fe = cfiles.GetEntityFile((decimal)oe.IDFile);
                if (fe != null)
                {
                    return this.GetStringStrategicResource("OrderNum") + oe.NumOrder + " " + this.GetStringStrategicResource("From") + " " + ((DateTime)oe.DateOrder).ToString("dd-MM-yyyy");
                }
            }
            return null;
        }
        /// <summary>
        /// Получить ссылку на приказ
        /// </summary>
        /// <param name="IDOrder"></param>
        /// <returns></returns>
        public string GetLinkOrder(int? IDOrder)
        {
            if (IDOrder==null) return null;
            OrderEntity oe = GetCultureOrderEntity((int)IDOrder);
            if (oe.IDFile != null)
            {
                FileEntity fe = cfiles.GetEntityFile((decimal)oe.IDFile);
                if (fe != null)
                {
                    return WebConfigurationManager.AppSettings["urlServer"].ToString()+"File.ashx?id=" + fe.Id;
                    //"http://krr-www-parep01.europe.mittalco.com/File.ashx?id=" + fe.Id;
                }
            }
            return null;
        }
        #endregion

        #region Методы classOrder

        #region МЕТОДЫ TypeOrder
        /// <summary>
        /// Вернуть все строчки таблицы
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectTypeOrder()
        {
            string sql = "SELECT " + this._FieldTypeOrder + " FROM " + this._NameTableTypeOrder + " ORDER BY [IDTypeOrder]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную строку 
        /// </summary>
        /// <param name="IDTypeOrder"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectTypeOrder(int IDTypeOrder)
        {
            string sql = "SELECT " + this._FieldTypeOrder + " FROM " + this._NameTableTypeOrder + " WHERE ([IDTypeOrder] = " + IDTypeOrder.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть все строки с учетом культуры
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureTypeOrder()
        {
            string sql = "SELECT [IDTypeOrder]" + base.GetCultureField("TypeOrder") + " FROM " + this._NameTableTypeOrder + " ORDER BY [IDTypeOrder]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную строку с учетом культуры
        /// </summary>
        /// <param name="IDTypeOrder"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureTypeOrder(int IDTypeOrder)
        {
            string sql = "SELECT [IDTypeOrder]" + base.GetCultureField("TypeOrder") + " FROM " + this._NameTableTypeOrder + " WHERE ([IDTypeOrder] = " + IDTypeOrder.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить TypeOrderEntity 
        /// </summary>
        /// <param name="IDTypeOrder"></param>
        /// <returns></returns>
        public TypeOrderEntity GetCultureTypeOrder(int IDTypeOrder)
        {
            DataRow[] rows = SelectCultureTypeOrder(IDTypeOrder).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDTypeOrder"] != DBNull.Value)
                {
                    return new TypeOrderEntity()
                    {
                        ID = int.Parse(rows[0]["IDTypeOrder"].ToString()),
                        TypeOrder = rows[0]["TypeOrder"].ToString(),
                    };
                }
            }
            return null;
        }
        /// <summary>
        /// Добавить тип документа
        /// </summary>
        /// <param name="TypeOrder"></param>
        /// <param name="TypeOrderEng"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public int InsertTypeOrder(string TypeOrder, string TypeOrderEng, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPInsertTypeOrder, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TypeOrder", SqlDbType.NVarChar, 512));
            cmd.Parameters["@TypeOrder"].Value = (TypeOrder != null) ? TypeOrder.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@TypeOrderEng", SqlDbType.NVarChar, 512));
            cmd.Parameters["@TypeOrderEng"].Value = (TypeOrderEng != null) ? TypeOrderEng.Trim() : SqlString.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_itypeorder");
            }
            return Result;
        }
        /// <summary>
        /// Обновить тип документа
        /// </summary>
        /// <param name="IDTypeOrder"></param>
        /// <param name="TypeOrder"></param>
        /// <param name="TypeOrderEng"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public int UpdateTypeOrder(int IDTypeOrder, string TypeOrder, string TypeOrderEng, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPUpdateTypeOrder, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDTypeOrder", SqlDbType.Int));
            cmd.Parameters["@IDTypeOrder"].Value = IDTypeOrder;
            cmd.Parameters.Add(new SqlParameter("@TypeOrder", SqlDbType.NVarChar, 512));
            cmd.Parameters["@TypeOrder"].Value = (TypeOrder != null) ? TypeOrder.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@TypeOrderEng", SqlDbType.NVarChar, 512));
            cmd.Parameters["@TypeOrderEng"].Value = (TypeOrderEng != null) ? TypeOrderEng.Trim() : SqlString.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_utypeorder");
            }
            return Result;
        }
        /// <summary>
        /// Удалить тип документа
        /// </summary>
        /// <param name="IDTypeOrder"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int DeleteTypeOrder(int IDTypeOrder, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPDeleteTypeOrder, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDTypeOrder", SqlDbType.Int));
            cmd.Parameters["@IDTypeOrder"].Value = IDTypeOrder;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_dtypeorder");
            }
            return Result;
        }
        #endregion

        #region МЕТОДЫ ListOrder
        /// <summary>
        /// Вернуть все строчки таблицы
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectListOrder()
        {
            string sql = "SELECT " + this._FieldListOrder + " FROM " + this._NameTableListOrder + " ORDER BY [IDOrder]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную строку 
        /// </summary>
        /// <param name="IDOrder"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectListOrder(int IDOrder)
        {
            string sql = "SELECT " + this._FieldListOrder + " FROM " + this._NameTableListOrder + " WHERE ([IDOrder] = " + IDOrder.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть все строки с учетом культуры
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureListOrder()
        {
            string sql = "SELECT [IDOrder],[IDTypeOrder],[NumOrder],[DateOrder]" + base.GetCultureField("Order") + base.GetCultureField("IDFile") + " FROM " + this._NameTableListOrder + " ORDER BY [IDOrder]";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть выбранную строку с учетом культуры
        /// </summary>
        /// <param name="IDTypeOrder"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureListOrder(int IDOrder)
        {
            string sql = "SELECT [IDOrder],[IDTypeOrder],[NumOrder],[DateOrder]" + base.GetCultureField("Order") + base.GetCultureField("IDFile") + " FROM " + this._NameTableListOrder + " WHERE ([IDOrder] = " + IDOrder.ToString() + ")";
            return base.Select(sql);
        }
        /// <summary>
        /// Вернуть список документов по выбранному типу
        /// </summary>
        /// <param name="IDTypeOrder"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectCultureListOrderIsType(int IDTypeOrder)
        {
            string sql = "SELECT [IDOrder],[IDTypeOrder],[NumOrder],[DateOrder]" + base.GetCultureField("Order") + ",[IDFile],[IDFileEng] " + " FROM " + this._NameTableListOrder;
            if (IDTypeOrder > 0)
            {
                sql += " WHERE ([IDTypeOrder] = " + IDTypeOrder.ToString() + ")";
            }
            sql += " ORDER BY [Order]";
            return base.Select(sql);
        }
        /// <summary>
        /// Получить OrderEntity 
        /// </summary>
        /// <param name="IDTypeOrder"></param>
        /// <returns></returns>
        public OrderEntity GetCultureOrderEntity(int IDOrder)
        {
            DataRow[] rows = SelectCultureListOrder(IDOrder).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDOrder"] != DBNull.Value)
                {
                    return new OrderEntity()
                    {
                        ID = int.Parse(rows[0]["IDOrder"].ToString()),
                        IDTypeOrder = int.Parse(rows[0]["IDTypeOrder"].ToString()),
                        NumOrder = rows[0]["NumOrder"] != DBNull.Value ? rows[0]["NumOrder"] as int? : null,
                        DateOrder = rows[0]["DateOrder"] != DBNull.Value ? rows[0]["DateOrder"] as DateTime? : null,
                        Order = rows[0]["Order"].ToString(),
                        IDFile = rows[0]["IDFile"] != DBNull.Value ? rows[0]["IDFile"] as int? : null,
                    };
                }
            }
            return null;
        }
        /// <summary>
        /// Получить OrderContent 
        /// </summary>
        /// <param name="IDOrder"></param>
        /// <returns></returns>
        public OrderContent GetCultureOrderContent(int IDOrder)
        {
            DataRow[] rows = SelectListOrder(IDOrder).Select();
            if ((rows != null) && (rows.Count() > 0))
            {
                if (rows[0]["IDOrder"] != DBNull.Value)
                {
                    return new OrderContent()
                    {
                        ID = int.Parse(rows[0]["IDOrder"].ToString()),
                        IDTypeOrder = int.Parse(rows[0]["IDTypeOrder"].ToString()),
                        NumOrder = rows[0]["NumOrder"] != DBNull.Value ? rows[0]["NumOrder"] as int? : null,
                        DateOrder = rows[0]["DateOrder"] != DBNull.Value ? rows[0]["DateOrder"] as DateTime? : null,
                        Order = rows[0]["Order"].ToString(),
                        OrderEng = rows[0]["OrderEng"].ToString(),
                        IDFile = rows[0]["IDFile"] != DBNull.Value ? rows[0]["IDFile"] as int? : null,
                        IDFileEng = rows[0]["IDFileEng"] != DBNull.Value ? rows[0]["IDFileEng"] as int? : null,
                    };
                }
            }
            return null;
        }
        /// <summary>
        /// Добавить тип документа
        /// </summary>
        /// <param name="IDOrder"></param>
        /// <param name="IDTypeOrder"></param>
        /// <param name="NumOrder"></param>
        /// <param name="DateOrder"></param>
        /// <param name="Order"></param>
        /// <param name="OrderEng"></param>
        /// <param name="IDFile"></param>
        /// <param name="IDFileEng"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public int InsertOrder(int IDTypeOrder, int? NumOrder, DateTime? DateOrder, string Order, string OrderEng, int? IDFile, int? IDFileEng, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPInsertOrder, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDTypeOrder", SqlDbType.Int));
            cmd.Parameters["@IDTypeOrder"].Value = IDTypeOrder;
            cmd.Parameters.Add(new SqlParameter("@NumOrder", SqlDbType.Int));
            cmd.Parameters["@NumOrder"].Value = NumOrder != null ? (int)NumOrder : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@DateOrder", SqlDbType.DateTime));
            cmd.Parameters["@DateOrder"].Value = DateOrder != null ? (DateTime)DateOrder : SqlDateTime.Null;
            cmd.Parameters.Add(new SqlParameter("@Order", SqlDbType.NVarChar, 512));
            cmd.Parameters["@Order"].Value = (Order != null) ? Order.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@OrderEng", SqlDbType.NVarChar, 512));
            cmd.Parameters["@OrderEng"].Value = (OrderEng != null) ? OrderEng.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@IDFile", SqlDbType.Int));
            cmd.Parameters["@IDFile"].Value = IDFile != null ? (int)IDFile : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@IDFileEng", SqlDbType.Int));
            cmd.Parameters["@IDFileEng"].Value = IDFileEng != null ? (int)IDFileEng : SqlInt32.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_iorder");
            }
            return Result;
        }
        /// <summary>
        /// Обновить тип документа
        /// </summary>
        /// <param name="IDTypeOrder"></param>
        /// <param name="TypeOrder"></param>
        /// <param name="TypeOrderEng"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public int UpdateOrder(int IDOrder, int IDTypeOrder, int? NumOrder, DateTime? DateOrder, string Order, string OrderEng, int? IDFile, int? IDFileEng, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPUpdateOrder, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDOrder", SqlDbType.Int));
            cmd.Parameters["@IDOrder"].Value = IDOrder;
            cmd.Parameters.Add(new SqlParameter("@IDTypeOrder", SqlDbType.Int));
            cmd.Parameters["@IDTypeOrder"].Value = IDTypeOrder;
            cmd.Parameters.Add(new SqlParameter("@NumOrder", SqlDbType.Int));
            cmd.Parameters["@NumOrder"].Value = NumOrder != null ? (int)NumOrder : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@DateOrder", SqlDbType.DateTime));
            cmd.Parameters["@DateOrder"].Value = DateOrder != null ? (DateTime)DateOrder : SqlDateTime.Null;
            cmd.Parameters.Add(new SqlParameter("@Order", SqlDbType.NVarChar, 512));
            cmd.Parameters["@Order"].Value = (Order != null) ? Order.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@OrderEng", SqlDbType.NVarChar, 512));
            cmd.Parameters["@OrderEng"].Value = (OrderEng != null) ? OrderEng.Trim() : SqlString.Null;
            cmd.Parameters.Add(new SqlParameter("@IDFile", SqlDbType.Int));
            cmd.Parameters["@IDFile"].Value = IDFile != null ? (int)IDFile : SqlInt32.Null;
            cmd.Parameters.Add(new SqlParameter("@IDFileEng", SqlDbType.Int));
            cmd.Parameters["@IDFileEng"].Value = IDFileEng != null ? (int)IDFileEng : SqlInt32.Null;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_uorder");
            }
            return Result;
        }
        /// <summary>
        ///  Удалить тип документа
        /// </summary>
        /// <param name="IDOrder"></param>
        /// <param name="OutResult"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int DeleteOrder(int IDOrder, bool OutResult)
        {
            int Result = 0;
            SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
            SqlCommand cmd = new SqlCommand(this._NameSPDeleteOrder, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IDOrder", SqlDbType.Int));
            cmd.Parameters["@IDOrder"].Value = IDOrder;
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
                base.OutResultFormatText(Result, "mes_err_", "mes_info_dorder");
            }
            return Result;
        }
        #endregion

        #endregion

    }

    #endregion

}