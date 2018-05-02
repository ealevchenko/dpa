using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Email;
using WebBase;

public partial class GrantAccess : BaseAccessPages
{
    protected classAccessUsers caccessusers = new classAccessUsers();
    protected classWeb cweb = new classWeb();
    protected classSection csection = new classSection();
    protected classSMTPWeb smtpw = new classSMTPWeb();

    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ

    protected int? IDAccessWebUsers
    {
        get
        {
            if (ViewState["IDAccessWebUsers"] == null) return null;
            return (int)ViewState["IDAccessWebUsers"];
        }
        set { ViewState["IDAccessWebUsers"] = value; }
    }

    protected int? IDUser
    {
        get
        {
            if (ViewState["IDUser"] == null) return null;
            return (int)ViewState["IDUser"];
        }
        set { ViewState["IDUser"] = value; }
    }

    protected AccessUsersEntity AccessUsers { get; set; }
    
    protected AccessWebUsersEntity AccessWebUsers { get; set; }

    protected string NameWebSite 
    {
        get 
        {
            if (this.AccessWebUsers != null) 
            {
                return cweb.GetCultureWeb(this.AccessWebUsers.IDWeb).Description;
            }
            return null;
        }
    }

    protected string FIO 
    {
        get 
        {
            if (this.AccessUsers != null) 
            {
                return this.AccessUsers.Surname + " " + this.AccessUsers.Name + " " + this.AccessUsers.Patronymic ;
            }
            return null;
        }
    }

    protected string Post 
    {
        get 
        {
            if (this.AccessUsers != null) 
            {
                return this.AccessUsers.Post;
            }
            return null;
        }
    }

    protected string Email 
    {
        get 
        {
            if (this.AccessUsers != null) 
            {
                return this.AccessUsers.Email;
            }
            return null;
        }
    }

    protected string Section 
    {
        get 
        {
            if (this.AccessUsers != null) 
            {
                return csection.GetCultureSection((int)this.AccessUsers.IDSection).SectionFull;
            }
            return null;
        }
    }

    protected string DateApproval
    {
        get
        {
            if (this.AccessWebUsers != null)
            {
                if (this.AccessWebUsers.DateApproval != null)
                {
                    return ((DateTime)this.AccessWebUsers.DateApproval).ToString("dd-MM-yyyy HH:mm:ss");
                }
            }
            return null;
        }
    }

    /// <summary>
    /// Иницилизация объектов
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ViewState["IDUser"] = caccessusers.GetIDUser(HttpContext.Current.User.Identity.Name);

        if (HttpContext.Current.Request.QueryString["ID"] != null)
        {
            ViewState["IDAccessWebUsers"] = int.Parse(HttpContext.Current.Request.QueryString["ID"]);
        }

    }
    #endregion

    #region Методы загрузки компонентов
    /// <summary>
    /// Загрузка страницы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Первый запуск
        if (!IsPostBack)
        {
            ((StartSite)Master).SetHeader(GetStringResource("Title1"), GetStringResource("TitleGrantAccess"));
            ((StartSite)Master).returnmenu = true;
        }
        // Требуется обновить компоненты
        if (base.bReloading)
        {
            ((StartSite)Master).SetHeader(GetStringResource("Title1"), GetStringResource("TitleGrantAccess"));
            ((StartSite)Master).returnmenu = true;
            base.bReloading = false;
        }
        if (this.IDAccessWebUsers != null) { this.AccessWebUsers = caccessusers.GetAccessWebUsers((int)this.IDAccessWebUsers); }
        if (this.AccessWebUsers != null)
        {
            this.AccessUsers = caccessusers.GetAccessUsers((int)this.AccessWebUsers.IDAccessUsers);
            if (this.AccessWebUsers.DateApproval != null) { pnGrantAccess.Visible = false; pnEnd.Visible = true; } else { pnGrantAccess.Visible = true; pnEnd.Visible = false; }
        }
    }
    #endregion

    #region Методы обработки действий пользователя
    /// <summary>
    /// Согласовать запрос
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btYes_Click(object sender, EventArgs e)
    {
        if (this.IDAccessWebUsers != null){
            caccessusers.SetApproval((int)this.IDAccessWebUsers, true, tbComent.Text);
        }
        pnGrantAccess.Visible = false;
        pnGrantAccess.DataBind();
        smtpw.AdminEmailSend("Результаты предоставления доступа к web-ресурсам ДАТП.", "Доступ к Web-ресурсу '" + this.NameWebSite + "' - разрешен. Владелц ресурса: " + HttpContext.Current.User.Identity.Name);
        smtpw.EmailSend(this.Email, "Результаты предоставления доступа к web-ресурсам ДАТП.", "Доступ к Web-ресурсу '" + this.NameWebSite + "' - cогласован владельцем ресурса: " + caccessusers.GetUserDetali((int)this.IDUser).Email + ". Ознакомится с результатами можно по ссылке: http://krr-www-parep01.europe.mittalco.com/AccessWeb.aspx?Step=1");

    }
    /// <summary>
    /// Отказать
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btNo_Click(object sender, EventArgs e)
    {
        if (this.IDAccessWebUsers != null){
            caccessusers.SetApproval((int)this.IDAccessWebUsers, false, tbComent.Text);
        }
        pnGrantAccess.Visible = false;
        pnGrantAccess.DataBind();
        smtpw.AdminEmailSend("Результаты предоставления доступа к web-ресурсам ДАТП.", "Доступ к Web-ресурсу '" + this.NameWebSite + "' -не предоставлен. Владелц ресурса: " + HttpContext.Current.User.Identity.Name);
        smtpw.EmailSend(this.Email, "Результаты предоставления доступа к web-ресурсам ДАТП.", "Доступ к Web-ресурсу '" + this.NameWebSite + "' - не предоставлен владельцем ресурса: " + caccessusers.GetUserDetali((int)this.IDUser).Email + ". Ознакомится с результатами можно по ссылке: http://krr-www-parep01.europe.mittalco.com/AccessWeb.aspx?Step=1");
    }
    #endregion

}