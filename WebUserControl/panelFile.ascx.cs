using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBase;

public partial class panelFile : BaseUprControl
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ
    private classFiles cfile = new classFiles();

    #region задаваеммые поля
    
    public int? IDFile { get; set; }

    public int Width { get; set; }

    //public string sriptnamesfn { get { return "Show" + this.ID + "FileNew()"; } }
    protected bool? type 
    {
        get
        {
            if (ViewState["type"] == null)
            {
                if (this.idfile != null)
                { ViewState["type"] = true; }
                else
                { ViewState["type"] = false; }
                //Out();
            }
            return (bool)ViewState["type"];
        }
        set { ViewState["type"] = value;
        Out();
        }

    }

    #endregion

    #region Определение полей

    protected int? idfile 
    {
        get { return this.IDFile == null ? null : this.IDFile as int?; }
    }

    protected int width
    {
        get { return this.Width == 0 ? 200 : this.Width; }
    }

    protected string namefile 
    {
        get { return cfile.GetEntityFile((decimal)this.idfile).FileName; }
    }

    #endregion

    #region Определение событий

    public class ResultEventArgs : EventArgs
    {
        private int _Result;
        public int Result { get { return this._Result; } } // Возвращаем только дату
        public ResultEventArgs(int Result)
        {
            this._Result = Result;
        }
    }

    // Обявим делегатов
    public delegate void Insert_File(object sender, ResultEventArgs e);
    public delegate void Update_File(object sender, ResultEventArgs e);
    public delegate void Delete_File(object sender, ResultEventArgs e);
    public delegate void ErrorInsert_File(object sender, ResultEventArgs e);
    public delegate void ErrorDelete_File(object sender, ResultEventArgs e);
    // Определим событие
    public event Insert_File InsertFile;
    public event Update_File UpdateFile;
    public event Delete_File DeleteFile;
    public event ErrorInsert_File ErrorInsertFile;
    public event ErrorDelete_File ErrorDeleteFile;
    #endregion

    #endregion

    #region МЕТОДЫ
    /// <summary>
    /// Сохранить файл в БД
    /// </summary>
    /// <param name="fu"></param>
    /// <returns></returns>
    protected int SaveFileDB(FileUpload fu)
    {
        return cfile.AddFile(
        new FileContent()
        {
            // Имя файла
            FileName = fu.PostedFile.FileName,
            // Тип файла
            FileContent = fu.PostedFile.ContentType,
            // Данные
            FileImage = fu.FileBytes
        }
        );
    }
    /// <summary>
    /// Сохранить файл
    /// </summary>
    /// <returns></returns>
    public int? SaveFile()
    {
        if (this.idfile == null)
        {
            if (!fileUpload.HasFile)
            {
                return null;
            }
            else
            {
                int id = SaveFileDB(fileUpload);
                if (id >= 0) { if (InsertFile != null) InsertFile(this, new ResultEventArgs(id)); }
                else { if (ErrorInsertFile != null) ErrorInsertFile(this, new ResultEventArgs(id)); }
                return id; 
            }
        }
        else
        {
            if ((bool)this.type)
            {
                return this.idfile;
            }
            else {
                if (!fileUpload.HasFile)
                {
                    return null;
                }
                else 
                {
                    int id = SaveFileDB(fileUpload);
                    if (id >= 0) { if (UpdateFile != null) UpdateFile(this, new ResultEventArgs(id)); }
                    else { if (ErrorInsertFile != null) ErrorInsertFile(this, new ResultEventArgs(id)); }
                    return id;                 
                }
            }
        }
    }
    /// <summary>
    /// Удалить файл
    /// </summary>
    /// <returns></returns>
    public int? DelFile() 
    {
        if (this.idfile != null) 
        {
            // файл удалить
            int id = cfile.DelFile((decimal)this.idfile);
            if (id >= 0) { if (DeleteFile != null) DeleteFile(this, new ResultEventArgs(id)); }
            else { if (ErrorDeleteFile != null) ErrorDeleteFile(this, new ResultEventArgs(id)); }
            return id;
        }
        return null;
    }
    /// <summary>
    /// Загрузка компонента
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
        }
        Out();
    }

    protected void Out() 
    { 
        tbFileOld.Width = Unit.Pixel(this.width);
        fileUpload.Width = Unit.Pixel(this.width + 60);
        if ((bool)this.type)
        {
            tbFileOld.Text = this.namefile;
            tbFileOld.Visible = true;
            btClr.Visible = true;
            btAdd.Visible = true;
            fileUpload.Visible = false;
        }
        else
        {
            tbFileOld.Visible = false;
            btClr.Visible = false;
            btAdd.Visible = false;
            fileUpload.Visible = true;
        }    
    }
    #endregion

    protected void btClr_Click(object sender, EventArgs e)
    {
        this.type = false;
    }

    protected void btAdd_Click(object sender, EventArgs e)
    {
        this.type = false;
    }
}