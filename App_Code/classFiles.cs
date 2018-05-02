using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WebBase;


public class Entity
{
    public decimal? Id
    {
        get;
        set;
    }
}

[Serializable()]
public class FileEntity : Entity
{
    public string FileName
    {
        get;
        set;
    }
    public string FileContent
    {
        get;
        set;
    }
    public DateTime? FileCreate
    {
        get;
        set;
    }
    public DateTime? FileChange
    {
        get;
        set;
    }
    public FileEntity()
    {
        this.Id = null;
    }
}

[Serializable()]
public class FileContent : FileEntity
{
    public byte[] FileImage
    {
        get;
        set;
    }
    public FileContent()
        : base()
    {
    }
}

/// <summary>
/// Сводное описание для classFiles
/// </summary>
public class classFiles : classBaseDB
{
    #region Поля класса classFiles
    private string _FieldListFiles = "[IDFile],[FileName] ,[FileContent] ,[FileCreate] ,[FileChange] ,[Size] ,[FileData]";
    private string _NameListFiles = WebConfigurationManager.AppSettings["tb_ListFiles"].ToString();
    private string _NameSPInsertFiles = WebConfigurationManager.AppSettings["sp_InsertFiles"].ToString();
    //private string _NameSPUpdateFiles = WebConfigurationManager.AppSettings["sp_UpdateFiles"].ToString();
    private string _NameSPDeleteFiles = WebConfigurationManager.AppSettings["sp_DeleteFiles"].ToString();
    #endregion

    public classFiles()
    {

    }
    #region Общие методы класса classFiles

    public string GetListFile(object dataItem)
    {
        //if (DataBinder.Eval(dataItem, "IDStepDetali") != DBNull.Value)
        //{
        //    TemplateStepEntity tse = GetCultureTemplatesSteps(int.Parse(DataBinder.Eval(dataItem, "IDTemplateStepProject").ToString()));
        //    return tse.TemplateStep;
        //}
        return null;
    }
    #endregion

    #region Методы класса classFiles
    /// <summary>
    /// Добавить файл
    /// </summary>
    /// <param name="file"></param>
    public int AddFile(FileContent file)
    {
        string fileName = Path.GetFileName(file.FileName);
        return InsertFile(fileName, file.FileContent, file.FileImage,false);
    }
    /// <summary>
    /// Удалить файл
    /// </summary>
    /// <param name="IDFile"></param>
    public int DelFile(decimal IDFile) 
    {
        return DeleteFile(IDFile, false);
    }
    /// <summary>
    /// Прочесть
    /// </summary>
    /// <param name="IDFile"></param>
    /// <returns></returns>
    public FileContent GetFile(decimal IDFile) 
    {
        DataRow[] rows = SelectFile(IDFile).Select();
        if ((rows != null) && (rows.Count() > 0))
        {
            if (rows[0]["IDFile"] != DBNull.Value) 
            {
                return new FileContent()
                {
                    Id = decimal.Parse(rows[0]["IDFile"].ToString()),
                    FileName = rows[0]["FileName"].ToString(),
                    FileContent = rows[0]["FileContent"].ToString(),
                    FileImage = rows[0]["FileData"] as byte[]
                };
            } 
        }
        return null;
    }
    /// <summary>
    /// Получить контент файла по указанному ID
    /// </summary>
    /// <param name="IDFile"></param>
    /// <returns></returns>
    public FileEntity GetEntityFile(decimal IDFile)
    {
        DataRow[] rows = SelectFile(IDFile).Select();
        if ((rows != null) && (rows.Count() > 0))
        {
            if (rows[0]["IDFile"] != DBNull.Value)
            {
                return new FileEntity()
                {
                    Id = decimal.Parse(rows[0]["IDFile"].ToString()),
                    FileName = rows[0]["FileName"].ToString(),
                    FileContent = rows[0]["FileContent"].ToString(),
                    FileCreate = rows[0]["FileCreate"] != DBNull.Value ? rows[0]["FileCreate"] as DateTime? : null,
                    FileChange = rows[0]["FileChange"] != DBNull.Value ? rows[0]["FileChange"] as DateTime? : null
                };
            }
        }
        return null;
    }
    /// <summary>
    /// Получить строку выбранного файла
    /// </summary>
    /// <param name="IDFile"></param>
    /// <returns></returns>
    public DataTable SelectFile(decimal IDFile) 
    {
        string sql = "SELECT " + this._FieldListFiles + " FROM " + this._NameListFiles + " WHERE ([IDFile] = "+IDFile.ToString()+")";
        return base.Select(sql);
    }
    /// <summary>
    /// Сохранить файл в БД и получить ID файла
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="FileContent"></param>
    /// <param name="FileData"></param>
    /// <param name="OutResult"></param>
    /// <returns></returns>
    [DataObjectMethod(DataObjectMethodType.Insert, true)]
    public int InsertFile(string FileName, string FileContent, byte[] FileData, bool OutResult)
    {
        int Result = 0;
        int size = 0;
        if (FileData != null)
            size = FileData.Length;

        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        SqlCommand cmd = new SqlCommand(this._NameSPInsertFiles, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FileName", SqlDbType.NVarChar, 1024));
        cmd.Parameters["@FileName"].Value = (FileName != null) ? FileName.Trim() : SqlString.Null;
        cmd.Parameters.Add(new SqlParameter("@FileContent", SqlDbType.NVarChar, 1024));
        cmd.Parameters["@FileContent"].Value = (FileContent != null) ? FileContent.Trim() : SqlString.Null;
        cmd.Parameters.Add(new SqlParameter("@FileSize", SqlDbType.Int));
        cmd.Parameters["@FileSize"].Value = size;
        cmd.Parameters.Add(new SqlParameter("@FileData", SqlDbType.Image, size,ParameterDirection.Input,true,1,1,"@FileData",DataRowVersion.Current,FileData));
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
            base.OutResultFormatText(Result, "mes_err_", "mes_info_ifile");
        }
        return Result;
    }
    /// <summary>
    /// Удалить файл
    /// </summary>
    /// <param name="IDFile"></param>
    /// <param name="OutResult"></param>
    /// <returns></returns>
    [DataObjectMethod(DataObjectMethodType.Delete, true)]
    public int DeleteFile(decimal IDFile, bool OutResult)
    {
        int Result = 0;
        SqlConnection con = new SqlConnection(this._connectionString_ASPServer);
        SqlCommand cmd = new SqlCommand(this._NameSPDeleteFiles, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@IDFile", SqlDbType.Decimal));
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
            base.OutResultFormatText(Result, "mes_err_", "mes_info_dfile");
        }
        return Result;
    }
    #endregion

}




