using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controlFileBrowser : BaseControl
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ classSite
    private classBackup cb = new classBackup();

    public string CurrentDir // Текущая директория (устанавливается относительно сервера)
    {
        get { return (string)ViewState["CurrentDir"]; }
        set { ViewState["CurrentDir"] = value; }
    }
    public string SelectFile // Имя выбранного файла
    {
        get { return (string)ViewState["SelectFile"]; }
        set { ViewState["SelectFile"] = value; }
    }
    public string SelectPathFile // Путь к выбраному файлу относительно сераера + (~)
    {
        get { return (string)ViewState["SelectPathFile"]; }
        set { ViewState["SelectPathFile"] = value; }
    }
    public string TypeFile // бит выбран
    {
        get { return (string)ViewState["TypeFile"]; }
        set { ViewState["TypeFile"] = value; }
    }
    public string NameServer // бит выбран
    {
        get { return (string)ViewState["NameServer"]; }
        set { ViewState["NameServer"] = value; }
    }
    public bool btClose
    {
        get { return (Boolean)ViewState["btClose"]; }
        set { ViewState["btClose"] = value; }
    }
    public bool pnVisible
    {
        get { return (Boolean)ViewState["pnVisible"]; }
        set { ViewState["pnVisible"] = value; }
    }

    /// <summary>
    ///  Иницилизация объектов
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (ViewState["btClose"] == null)
        { ViewState["btClose"] = true; }
        if (ViewState["NameServer"] == null)
        { ViewState["NameServer"] = WebConfigurationManager.AppSettings["NameServer"].ToString(); ; }
        if (ViewState["CurrentDir"] == null)
        { ViewState["CurrentDir"] = Server.MapPath("/"); }
        if (ViewState["TypeFile"] == null)
        { ViewState["TypeFile"] = "*.*"; }
        if (ViewState["pnVisible"] == null)
        { ViewState["pnVisible"] = true; }

    }

    public delegate void UpDir_Click(object sender, EventArgs e);
    public delegate void DirList_Selected(object sender, EventArgs e);
    public delegate void FileList_Selected(object sender, EventArgs e);
    public delegate void Close_Click(object sender, EventArgs e);
    // Определим событие
    public event UpDir_Click UpDirClick;
    public event DirList_Selected DirListSelected;
    public event FileList_Selected FileListSelected;
    public event Close_Click CloseClick;

    #endregion

    /// <summary>
    /// Загрузка страницы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            
        }
        cmdClose.Visible = this.btClose;
        pnFileBrowser.Visible = this.pnVisible;
    }
    /// <summary>
    /// Показать панель
    /// </summary>
    /// <param name="value"></param>
    public void VisibleFileBrowser(bool value) 
    {
        this.pnVisible = value;
        pnFileBrowser.Visible = this.pnVisible;
    }
    /// <summary>
    /// Установить начальную дирикторию
    /// </summary>
    public void SetStartDirectory() 
    {
        ShowDirectoryContents(this.CurrentDir);
    }
    /// <summary>
    /// Установить начальную дирикторию
    /// </summary>
    /// <param name="path"></param>
    public void SetStartDirectory(string path) 
    {
        this.CurrentDir = path;
        SetStartDirectory();
    }
    #region РАБОТА С ФАЙЛОВОЙ СИСТЕМОЙ
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    private void ShowDirectoryContents(string path)
    {
        // Define the current directory.
        DirectoryInfo dir = new DirectoryInfo(path);

        // Get the DirectoryInfo and FileInfo objects.
        FileInfo[] files = dir.GetFiles(this.TypeFile);
        DirectoryInfo[] dirs = dir.GetDirectories();

        // Show the directory listing.
        lblCurrentDir.Text = cb.GetStringResource("lsCurrentDir") + path;
        gridFileList.DataSource = files;
        gridDirList.DataSource = dirs;
        //Page.DataBind();
        pnFileBrowser.DataBind();
        // Clear any selection.
        gridFileList.SelectedIndex = -1;
        this.SelectFile = "";
        this.SelectPathFile = "";
        // Keep track of the current path.
        //ViewState["CurrentPath"] = path;
        this.CurrentDir = path;
    }
    /// <summary>
    /// Поднятся вверх по каталогу
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdUp_Click(object sender, EventArgs e)
    {
        //string path = (string)ViewState["CurrentPath"]        
        string path = this.CurrentDir;
        path = Path.Combine(path, "..");
        path = Path.GetFullPath(path);
        ShowDirectoryContents(path);
        if (UpDirClick != null) UpDirClick(this, e); // выполнить событие
    }
    /// <summary>
    /// Выбрана директория
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridDirList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string dir = (string)gridDirList.DataKeys[gridDirList.SelectedIndex].Value;
        ShowDirectoryContents(dir);
        this.CurrentDir = dir;
        if (DirListSelected != null) DirListSelected(this, e); // выполнить событие
    }
    /// <summary>
    /// Выбран файл
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridFileList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string file = (string)gridFileList.DataKeys[gridFileList.SelectedIndex].Value;
        FileInfo fileinfo = new FileInfo(file);
        this.SelectFile =  fileinfo.Name;
        int sizedel = file.IndexOf(this.NameServer) + this.NameServer.Count();
        if (sizedel > 0)
        {
            string newfile = file.Remove(0, sizedel).Replace(@"\", "/");
            this.SelectPathFile = "~" + newfile;
        }
        if (FileListSelected != null) FileListSelected(this, e); // выполнить событие
    }
    #endregion
    /// <summary>
    /// Закрыть 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdClose_Click(object sender, EventArgs e)
    {
        if (CloseClick != null) CloseClick(this, e); // выполнить событие
    }
}