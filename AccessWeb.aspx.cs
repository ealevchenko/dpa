using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Email;
using WebBase;

public partial class AccessWeb : BasePages
{
    protected classAccessUsers caccessusers = new classAccessUsers();
    protected classWeb cweb = new classWeb();
    protected classSection csection = new classSection();
    protected classSMTPWeb smtpw = new classSMTPWeb();

    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ
    protected int Step
    {
        get
        {
            return (int)ViewState["Step"];
        }
        set { ViewState["Step"] = value; }
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

    protected int? IDAccessUser
    {
        get
        {
            if (ViewState["IDAccessUser"] == null) return null;
            return (int)ViewState["IDAccessUser"];
        }
        set { ViewState["IDAccessUser"] = value; }
    }
    /// <summary>
    ///  Иницилизация объектов
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ViewState["IDUser"] = caccessusers.GetIDUser(HttpContext.Current.User.Identity.Name);
        //ViewState["IDUser"] = null;
        this.accessusers = caccessusers.GetAccessUsersToUser(this.IDUser);

        if (this.accessusers != null){
            ViewState["IDAccessUser"] = this.accessusers.ID;
        }
        this.list_awue = caccessusers.GetListAccessWebUsers(this.IDAccessUser);
        if (HttpContext.Current.Request.QueryString["Step"] != null)
        {
            ViewState["Step"] = int.Parse(HttpContext.Current.Request.QueryString["Step"]);
        } else ViewState["Step"] = 0;
    }

    protected AccessUsersEntity accessusers { get; set; }

    protected List<AccessWebUsersEntity> list_awue { get; set; } 

    protected string GetDateCreate { get { if (this.accessusers != null) { return ((DateTime)this.accessusers.DateCreate).ToString("dd-MM-yyyy HH:mm:ss"); } return null; } }
    protected string GetDateChange
    {
        get
        {
            if (this.accessusers != null)
            {
                if (this.accessusers.DateChange != null)
                {
                    return ((DateTime)this.accessusers.DateChange).ToString("dd-MM-yyyy HH:mm:ss");
                }
            }
            return null;
        }
    }
    protected string GetDateAccess
    {
        get
        {
            if (this.accessusers != null)
            {
                if (this.accessusers.DateAccess != null)
                {
                    return ((DateTime)this.accessusers.DateAccess).ToString("dd-MM-yyyy HH:mm:ss");
                }
            }
            return null;
        }
    }
    // Признак есть изменения в запросе на доступ
    protected bool DataChange 
    {
        get { if (ViewState["DataChange"] == null) { return false; } else { return (bool)ViewState["DataChange"]; } }
        set { ViewState["DataChange"] = value; }
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
            ((StartSite)Master).SetHeader(GetStringResource("Title1"), GetStringResource("TitleAccess"));
            ((StartSite)Master).returnmenu = true;
            wzAccessUsers.ActiveStepIndex = this.Step;
            if (this.accessusers != null)
            {
                tbEmail.Text = this.accessusers.Email;
                cbDistribution.Checked = this.accessusers.bDistribution;
                tbSurname.Text = this.accessusers.Surname;
                tbName.Text = this.accessusers.Name;
                tbPatronymic.Text = this.accessusers.Patronymic;
                tbPost.Text = this.accessusers.Post;
                ddlSection.SelectedValue = this.accessusers.IDSection.ToString();
            }
            this.DataChange = false;
        }
        // Требуется обновить компоненты
        if (base.bReloading)
        {
            ((StartSite)Master).SetHeader(GetStringResource("Title1"), GetStringResource("TitleAccess"));
            ((StartSite)Master).returnmenu = true;
            base.bReloading = false;
        }
    }
    /// <summary>
    /// Загрузка компонентов
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void wzAccessUsers_Load(object sender, EventArgs e)
    {
        lbResultAccessInfo.Text = "";
        lbResultAccessWeb.Text = "";
        // Доступы к web-сайтам
        if ((wzAccessUsers.ActiveStepIndex == 1) | (wzAccessUsers.ActiveStepIndex == 2))
        {
            if (this.accessusers != null)
            {
                if (this.accessusers.Email.ToUpper() != tbEmail.Text.ToUpper())
                {
                    lbEmail.Text = this.accessusers.Email; this.DataChange = true;
                }
                if (this.accessusers.bDistribution != cbDistribution.Checked)
                {
                    lbDistribution.Text = this.accessusers.bDistribution.ToString(); this.DataChange = true;
                }
                if (this.accessusers.Surname.ToUpper() != tbSurname.Text.ToUpper())
                {
                    lbSurname.Text = this.accessusers.Surname; this.DataChange = true;
                }
                if (this.accessusers.Name.ToUpper() != tbName.Text.ToUpper())
                {
                    lbName.Text = this.accessusers.Name; this.DataChange = true;
                }
                if (this.accessusers.Patronymic.ToUpper() != tbPatronymic.Text.ToUpper())
                {
                    lbPatronymic.Text = this.accessusers.Patronymic; this.DataChange = true;
                }
                if (this.accessusers.Post.ToUpper() != tbPost.Text.ToUpper())
                {
                    lbPost.Text = this.accessusers.Post; this.DataChange = true;
                }
                if (this.accessusers.IDSection != int.Parse(ddlSection.SelectedValue))
                {
                    lbSection.Text = csection.GetCultureSection(this.accessusers.IDSection).SectionFull; this.DataChange = true;
                }
            }
            else
            {
                this.DataChange = true;
            }
            lbNewEmail.Text = tbEmail.Text;
            lbNewDistribution.Text = cbDistribution.Checked.ToString();
            lbNewSurname.Text = tbSurname.Text;
            lbNewName.Text = tbName.Text;
            lbNewPatronymic.Text = tbPatronymic.Text;
            lbNewPost.Text = tbPost.Text;
            lbNewSection.Text = csection.GetCultureSection(int.Parse(ddlSection.SelectedValue)).SectionFull;
            // Доступы к web-сайтам
            int index = 0;
            phtr.Controls.Clear();
            while (index < lvAccessWeb.Items.Count)
            {
                CheckBox cb = (CheckBox)lvAccessWeb.Items[index].FindControl("cbAccess");
                Label lb = (Label)lvAccessWeb.Items[index].FindControl("lbID");

                if ((cb != null) & (lb != null))
                {
                    phtr.Controls.Add(new LiteralControl("<tr><th>"));
                    phtr.Controls.Add(new LiteralControl(cweb.GetCultureWeb(int.Parse(lb.ToolTip)).Description));
                    phtr.Controls.Add(new LiteralControl("</th><td>"));
                    Label lbchange = new Label();
                    lbchange.CssClass = "change";
                    foreach (AccessWebUsersEntity awse in list_awue)
                    {
                        if (awse.IDWeb == int.Parse(lb.ToolTip))
                        {
                            if (awse.AccessWeb != cb.Checked)
                            {
                                lbchange.Text = awse.AccessWeb.ToString(); this.DataChange = true;
                            }
                        }
                    }
                    phtr.Controls.Add(lbchange);
                    phtr.Controls.Add(new LiteralControl("&nbsp;-&nbsp;"));
                    Label lbnew = new Label();
                    lbnew.CssClass = "new";
                    lbnew.Text = cb.Checked.ToString();
                    phtr.Controls.Add(lbnew);
                    phtr.Controls.Add(new LiteralControl("</td></tr>"));
                }
                index++;
            }
            phtr.DataBind();
        }
    }
    #endregion

    #region Методы обработки действий пользователя
    /// <summary>
    /// Нажата кнопка финиш
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void wzAccessUsers_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {

        if (!this.DataChange)
        {
            e.Cancel = true;
            lbResultAccessInfo.Text = base.GetStringBaseResource("awMessageNotChange");
            return;
        }
        ltResult3.Visible = false;
        ltResult2.Visible = true;
        ltResult21.Visible = true;
        if (IDAccessUser != null)
        { // Обновить
           this.IDAccessUser = caccessusers.UpdateAccessUsers((int)IDAccessUser, tbEmail.Text.Trim(), cbDistribution.Checked,
                tbSurname.Text.Trim(), tbName.Text.Trim(), tbPatronymic.Text.Trim(), tbPost.Text.Trim(), int.Parse(ddlSection.SelectedValue), true);
        }
        else
        { // Создать
            this.IDAccessUser = caccessusers.InsertAccessUsers(HttpContext.Current.User.Identity.Name, tbEmail.Text.Trim(), cbDistribution.Checked,
                tbSurname.Text.Trim(), tbName.Text.Trim(), tbPatronymic.Text.Trim(), tbPost.Text.Trim(), int.Parse(ddlSection.SelectedValue), true);
        }
        if (this.IDAccessUser > 0)
        {
            // Записать доступы
            int index = 0;
            while (index < lvAccessWeb.Items.Count)
            {
                CheckBox cb = (CheckBox)lvAccessWeb.Items[index].FindControl("cbAccess");
                Label lb = (Label)lvAccessWeb.Items[index].FindControl("lbID");
                if ((cb != null) & (lb != null))
                {
                    if (lb.Text != "")
                    { // Обновить
                        caccessusers.UpdateAccessWebUsers(int.Parse(lb.Text), cb.Checked, false);
                    }
                    else
                    { // Добавить
                        caccessusers.InsertAccessWebUsers((int)this.IDAccessUser, int.Parse(lb.ToolTip), cb.Checked,false);
                    }
                }
                index++;
            }
        }
        if (this.IDAccessUser != null)
        {
            smtpw.AccessWebUsersEmailSend((int)this.IDAccessUser);
        }
    }
    /// <summary>
    /// Нажата кнопка "Далее"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void wzAccessUsers_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        bool bNotNull = false;
        if (e.CurrentStepIndex == 1) 
        {
            
            // Доступы к web-сайтам
            int index = 0;
            //phtr.Controls.Clear();
            while (index < lvAccessWeb.Items.Count)
            {
                CheckBox cb = (CheckBox)lvAccessWeb.Items[index].FindControl("cbAccess");
                Label lb = (Label)lvAccessWeb.Items[index].FindControl("lbID");

                if ((cb != null) & (lb != null))
                {
                    if (cb.Checked) { bNotNull = true; }
                }
                index++;
            }
            if (!bNotNull) 
            { 
                e.Cancel = true;
                lbResultAccessWeb.Text = base.GetStringBaseResource("awMessgeNotWeb"); 
            }
        }
    }
    /// <summary>
    /// Отмена перехода по SideBar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void wzAccessUsers_SideBarButtonClick(object sender, WizardNavigationEventArgs e)
    {
        e.Cancel = true;
    }
    #endregion
}