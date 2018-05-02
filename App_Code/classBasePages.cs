using Resources;
using Strategic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBase;


public class fmComponent 

{
    #region Методы чтения и установки данных из компонентов размещенных на FormView формы

    #region Методы TextBox
    /// <summary>
    /// Получить ClientID компонента TextBox
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <returns></returns>
    public string GetClientIDTextBox(FormView fv, string Control)
    {
        TextBox tb = GetTextBox(fv, Control);
        if (tb != null) { return tb.ClientID; }
        return null;
    }
    /// <summary>
    /// Установить свойство Text в компоненте TextBox
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public void SetTextTextBox(FormView fv, string Control, string value)
    {
        if (fv.FindControl(Control) != null)
        {
            TextBox tb = ((TextBox)fv.FindControl(Control));
            if (tb != null) { tb.Text = value; }
        }
    }
    /// <summary>
    /// Получить ссылку на комонент TextBox
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <returns></returns>
    public TextBox GetTextBox(FormView fv, string Control)
    {
        if (fv.FindControl(Control) != null)
        {
            return ((TextBox)fv.FindControl(Control));
        }
        return null;
    }
    /// <summary>
    /// Получает активный компанет TextBox
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <returns></returns>
    public bool GetEnableTextBox(FormView fv, string Control)
    {
        if (fv.FindControl(Control) != null)
        {
            return ((TextBox)fv.FindControl(Control)).Enabled;
        }
        return false;
    }
    /// <summary>
    /// Получить текст компонента TextBox или null
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <returns></returns>
    public string GetTextTextBox(FormView fv, string Control)
    {
        if (fv.FindControl(Control) != null)
        {
            TextBox tb = ((TextBox)fv.FindControl(Control));
            if (tb != null)
            {
                if (tb.Text.Trim() != "")
                {
                    return tb.Text.Trim();
                }
            }
        }
        return null;
    }
    #endregion

    #region Методы DropDownList
    /// <summary>
    /// Получить ссылку на DropDownList
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <returns></returns>
    public DropDownList GetDropDownList(FormView fv, string Control)
    {
        if (fv.FindControl(Control) != null)
        {
            return ((DropDownList)fv.FindControl(Control));
        }
        return null;
    }
    /// <summary>
    /// Обновить данные в DropDownList
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    public void DataBindDropDownList(FormView fv, string Control) 
    {
        DropDownList ddl = GetDropDownList(fv, Control);
        if (ddl != null)
        {
            ddl.DataBind();
        }
    }
    /// <summary>
    /// Получить ClientID DropDownList
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <returns></returns>
    public string GetClientIDDropDownList(FormView fv, string Control)
    {
        DropDownList ddl = GetDropDownList(fv, Control);
        if (ddl != null) { return ddl.ClientID; }
        return null;
    }
    /// <summary>
    /// Получить значение Value(int?) из DropDownList с учетом значения по умолчанию
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <param name="null_value"></param>
    /// <returns></returns>
    public int? GetValueDropDownList(FormView fv, string Control, int? null_value)
    {
        string res = GetValueDropDownList(fv, Control);
        if (res != null) { int.Parse(res); }
        return null_value;
    }
    /// <summary>
    /// Получить значение Value(string) из DropDownList
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <returns></returns>
    public string GetValueDropDownList(FormView fv, string Control)
    {
        DropDownList ddl = (DropDownList)fv.FindControl(Control);
        if (ddl != null)
        {
            if (ddl.SelectedIndex != -1)
            {
                return ddl.SelectedValue;
            }
        }
        return null;
    }
    /// <summary>
    /// Установить переменную value значением SelectedValue(DropDownList), если не выброно значение присвоить значение null_value
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <param name="value"></param>
    /// <param name="null_value"></param>
    public void SetValueDropDownList(FormView fv, string Control, ref int value, int null_value)
    {
        DropDownList ddl;
        ddl = GetDropDownList(fv, Control);
        if (ddl != null)
        {
            if (ddl.SelectedIndex != null_value)
            {
                value = int.Parse(ddl.SelectedValue);
            }
            else value = null_value;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <param name="value"></param>
    /// <param name="null_value"></param>
    public void SetValueDropDownList(FormView fv, string Control, ref int? value, int? null_value)
    {
        DropDownList ddl;
        ddl = GetDropDownList(fv, Control);
        if (ddl != null)
        {
            if (ddl.SelectedIndex != null_value)
            {
                value = int.Parse(ddl.SelectedValue);
            }
            else value = null_value;
        }
    }
    /// <summary>
    /// Установить активность компонента
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <param name="enable"></param>
    public void SetEnableDropDownList(FormView fv, string Control, bool enable)
    {
        DropDownList ddl;
        ddl = GetDropDownList(fv, Control);
        if (ddl != null)
        {
            ddl.Enabled = enable;
        }
    }

    #endregion

    #region Методы CheckBox
    /// <summary>
    /// Получить ссылку на CheckBox
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <returns></returns>
    public CheckBox GetCheckBox(FormView fv, string Control)
    {
        if (fv.FindControl(Control) != null)
        {
            return ((CheckBox)fv.FindControl(Control));
        }
        return null;
    }
    /// <summary>
    /// Получить ClientID CheckBox
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <returns></returns>
    public string GetClientIDCheckBox(FormView fv, string Control)
    {
        CheckBox cb = GetCheckBox(fv, Control);
        if (cb != null) { return cb.ClientID; }
        return null;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <returns></returns>
    public bool GetCheckedCheckBox(FormView fv, string Control)
    {
        if (fv.FindControl(Control) != null)
        {
            CheckBox cb = (CheckBox)fv.FindControl(Control);
            if (cb != null)
            {
                return cb.Checked;
            }
        }
        return false;
    }
    /// <summary>
    /// Установить Checked компонента CheckBox
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <param name="value"></param>
    public void SetCheckedCheckBox(FormView fv, string Control, bool value)
    {
        if (fv.FindControl(Control) != null)
        {
            ((CheckBox)fv.FindControl(Control)).Checked = value;
        }
    }
    /// <summary>
    /// Установить активность компанента CheckBox
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <param name="enable"></param>
    public void SetEnableCheckBox(FormView fv, string Control, bool enable)
    {
        if (fv.FindControl(Control) != null)
        {
            ((CheckBox)fv.FindControl(Control)).Enabled = enable;
        }
    }
    #endregion

    #region Методы RadioButtonList
    /// <summary>
    /// Получить ссылку на RadioButtonList
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <returns></returns>
    public RadioButtonList GetRadioButtonList(FormView fv, string Control)
    {
        if (fv.FindControl(Control) != null)
        {
            return ((RadioButtonList)fv.FindControl(Control));
        }
        return null;
    }
    /// <summary>
    /// Установить SelectedValue в RadioButtonList
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <param name="value"></param>
    public void SetValueRadioButtonList(FormView fv, string Control, string value)
    {
        if (fv.FindControl(Control) != null)
        {
            ((RadioButtonList)fv.FindControl(Control)).SelectedValue = value;
        }
    }

    #endregion

    #region Методы Literal
    /// <summary>
    /// Получить ссылку на Literal
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <returns></returns>
    public Literal GetLiteral(FormView fv, string Control)
    {
        if (fv.FindControl(Control) != null)
        {
            return ((Literal)fv.FindControl(Control));
        }
        return null;
    }
    /// <summary>
    /// Установить Text в Literal
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <param name="value"></param>
    public void SetTextLiteral(FormView fv, string Control, string value)
    {
        if (fv.FindControl(Control) != null)
        {
            ((Literal)fv.FindControl(Control)).Text = value;
        }
    }
    #endregion

    #region Методы Validation
    /// <summary>
    /// Получить ссылку на RegularExpressionValidator
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <returns></returns>
    public RegularExpressionValidator GetRegularExpressionValidator(FormView fv, string Control)
    {
        if (fv.FindControl(Control) != null)
        {
            return ((RegularExpressionValidator)fv.FindControl(Control));
        }
        return null;
    }
    /// <summary>
    /// Получить ссылку на RequiredFieldValidator
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <returns></returns>
    public RequiredFieldValidator GetRequiredFieldValidator(FormView fv, string Control)
    {
        if (fv.FindControl(Control) != null)
        {
            return ((RequiredFieldValidator)fv.FindControl(Control));
        }
        return null;
    }
    /// <summary>
    /// Активировать или деактивировать компонент RegularExpressionValidator
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <param name="value"></param>
    public void SetEnableRegularExpressionValidator(FormView fv, string Control, bool value)
    {
        RegularExpressionValidator rev = GetRegularExpressionValidator(fv, Control);
        if (rev != null) { rev.Enabled = value; }
    }
    /// <summary>
    /// Активировать или деактивировать компонент RequiredFieldValidator
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <param name="value"></param>
    public void SetEnableRequiredFieldValidator(FormView fv, string Control, bool value)
    {
        RequiredFieldValidator rev = GetRequiredFieldValidator(fv, Control);
        if (rev != null) { rev.Enabled = value; }
    }
    #endregion

    #region Методы Button
    /// <summary>
    /// Получить ссылку на Button
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <returns></returns>
    public Button GetButton(FormView fv, string Control)
    {
        if (fv.FindControl(Control) != null)
        {
            return ((Button)fv.FindControl(Control));
        }
        return null;
    }
    /// <summary>
    /// Установить Text в Button
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <param name="value"></param>
    public void SetTextButton(FormView fv, string Control, string value)
    {
        if (fv.FindControl(Control) != null)
        {
            ((Button)fv.FindControl(Control)).Text = value;
        }
    }
    /// <summary>
    /// Установить Text в Button
    /// </summary>
    /// <param name="fv"></param>
    /// <param name="Control"></param>
    /// <param name="value"></param>
    public void SetEnableButton(FormView fv, string Control, bool enable)
    {
        if (fv.FindControl(Control) != null)
        {

            ((Button)fv.FindControl(Control)).Enabled = enable;
        }
    }
    #endregion

    #endregion

}

public class lvComponent
{
    public lvComponent() { }

    #region Методы чтения и установки данных из компонентов размещенных на ListView формы

    #region Методы DropDownList
    /// <summary>
    /// Вернуть ссылку на DropDownList
    /// </summary>
    /// <param name="lvdi"></param>
    /// <param name="Control"></param>
    /// <returns></returns>
    public DropDownList GetDropDownList(ListViewDataItem lvdi, string Control)
    {
        if (lvdi.FindControl(Control) != null)
        {
            return ((DropDownList)lvdi.FindControl(Control));
        }
        return null;
    }
    /// <summary>
    /// Получить SelectedValue DropDownList
    /// </summary>
    /// <param name="lvdi"></param>
    /// <param name="Control"></param>
    /// <returns></returns>
    public int GetValueDropDownList(ListViewDataItem lvdi, string Control)
    {
        if (lvdi.FindControl(Control) != null)
        {
            DropDownList ddl = (DropDownList)lvdi.FindControl(Control);
            if (ddl != null)
            {
                if (ddl.SelectedIndex >= 0) return int.Parse(ddl.SelectedValue);
            }
        }
        return -1;
    }
    /// <summary>
    /// Вернуть Value(int?) из DropDownList, если не выбран вернуть null_value
    /// </summary>
    /// <param name="lv"></param>
    /// <param name="index"></param>
    /// <param name="Control"></param>
    /// <param name="null_value"></param>
    /// <returns></returns>
    public int? GetValueDropDownList(ListView lv, int index, string Control, int? null_value)
    {
        string res = GetValueDropDownList(lv, index, Control);
        if (res != null) { return int.Parse(res); }
        return null_value;
    }
    /// <summary>
    /// Вернуть Value(string) из DropDownList, если не выбран вернуть null
    /// </summary>
    /// <param name="lv"></param>
    /// <param name="index"></param>
    /// <param name="Control"></param>
    /// <returns></returns>
    public string GetValueDropDownList(ListView lv, int index, string Control)
    {
        if (index < 0) return null;
        DropDownList ddl = (DropDownList)lv.Items[index].FindControl(Control);
        if (ddl != null)
        {
            if (ddl.SelectedIndex != -1)
            {
                return ddl.SelectedValue;
            }
        }
        return null;
    }

    #endregion

    #region Методы TextBox
    /// <summary>
    /// Получить IDClient TextBox
    /// </summary>
    /// <param name="lv"></param>
    /// <param name="Control"></param>
    /// <returns></returns>
    public string GetIDClientTextBox(ListView lv, string Control)
    {
        if (lv.EditIndex != -1)
        {
            TextBox tb = (TextBox)lv.Items[lv.EditIndex].FindControl(Control);
            if (tb != null)
            { return tb.ClientID; }
        }
        return "";
    }
    /// <summary>
    /// Получить Text из TextBox
    /// </summary>
    /// <param name="lv"></param>
    /// <param name="index"></param>
    /// <param name="Control"></param>
    /// <returns></returns>
    public string GetTextTextBox(ListView lv, int index, string Control)
    {
        if (index < 0) return null;
        TextBox tb = (TextBox)lv.Items[index].FindControl(Control);
        if (tb != null)
        {
            if (tb.Text.Trim() != "")
            {
                return tb.Text.Trim();
            }
        }
        return null;
    }

    #endregion


    #endregion
}
/// <summary>
/// Класс чтения dataItem объектов на странице
/// </summary>
public class DIRead 
{
    public DIRead() { }
    
    #region Методы
    /// <summary>
    /// Определить картинку для загрузки CheckBox (true\false)
    /// </summary>
    /// <param name="dataItem"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public string GetSRCCheckBox(object dataItem, string field)
    {
        if (bool.Parse(DataBinder.Eval(dataItem, field).ToString()))
        { return "~/image/wuc/True.png"; }
        else { return "~/image/wuc/False.png"; }
    }
    /// <summary>
    /// Возвращает строку в зависимости от сотояния bool
    /// </summary>
    /// <param name="dataItem"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public string GetStringBool(object dataItem, string field, string styletrue, string stylefalse)
    {
        if (bool.Parse(DataBinder.Eval(dataItem, field).ToString()))
        { return styletrue; }
        else { return stylefalse; }
    }
    /// <summary>
    /// Возвращает строку в зависимости от сотояния bool
    /// </summary>
    /// <param name="value"></param>
    /// <param name="styletrue"></param>
    /// <param name="stylefalse"></param>
    /// <returns></returns>
    public string GetStringBool(bool value, string styletrue, string stylefalse)
    {
        if (value)
        { return styletrue; }
        else { return stylefalse; }
    }
    #endregion

}

#region СТРАНИЦЫ САЙТОВ
/// <summary>
/// базовый класс страницы
/// </summary>
public class BasePages : System.Web.UI.Page
{
    protected Boolean bReloading = false; // бит устанавливается если требуется перезагрузка компонентов отображающих данные из базы
    /// <summary>
    /// Хранение выбранной культуры 
    /// </summary>
    private string bufLanguage 
    {
        get { return (string)Session["Language"]; }
        set { Session["Language"] = value; }
    }
    /// <summary>
    /// Переменная хранения культуры в куках клиента
    /// </summary>
    private string currentLanguage 
    {
        get 
        {
            HttpCookie cookie = Request.Cookies["Report"];
            if (cookie == null) 
            {
                // Создать объект cookie-набора. 
                HttpCookie save_cookie = new HttpCookie("Report");
                save_cookie.Expires = DateTime.Now.AddYears(1); // храним год
                // Установить значение в нем. 
                save_cookie["Language"] = "ru-RU";
                // Добавить cookie-набор к текущему веб-ответу. 
                Response.Cookies.Add(save_cookie);
            }
            //HttpCookie cookie = Request.Cookies["Report"];
            if (cookie != null)
            { return cookie["Language"]; }
            return "ru-RU"; 
        }
        set {
            // Создать объект cookie-набора. 
            HttpCookie save_cookie = new HttpCookie("Report");
            save_cookie.Expires = DateTime.Now.AddYears(1); // храним год
            // Установить значение в нем. 
            save_cookie["Language"] = value;
            // Если изменили культуру, установить бит обновления на странице
            if (this.bufLanguage != value) { this.bufLanguage = value; this.bReloading = true; }
            // Добавить cookie-набор к текущему веб-ответу. 
            Response.Cookies.Add(save_cookie);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(value);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(value);
        }

    }

    //// Перекрываем метод
    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    //не вызываем базовый метод, просто игнорируем все проверки
    //    //base.VerifyRenderingInServerForm(control);
    //}
    //*/
    //// Перекрываем пулучение HTML кода из компонента
    ////!!! при использовании этого элемента, нужно отключить
    //// на странице <%@ Page EnableEventValidation="false" %>
    //public static string RenderControl(Control ctrl)
    //{
    //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //    System.IO.StringWriter tw = new System.IO.StringWriter(sb);
    //    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
    //    ctrl.RenderControl(hw);
    //    return sb.ToString();
    //}
    //// сохранение в файл Excel данных из компонента gridView
    //public void SaveToExcel(GridView gv, String NameFile)
    //{
    //    Response.Clear();
    //    Response.AddHeader("content-disposition", "attachment;filename=" + NameFile + ".xls");
    //    Response.Charset = "";
    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    Response.ContentType = "application/msexcel";
    //    StringWriter stringWrite = new System.IO.StringWriter();
    //    HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
    //    gv.RenderControl(htmlWrite);
    //    Response.Write(stringWrite.ToString());
    //    Response.End();
    //}
    //// сохранение в файл Excel данных из Html строки <table></table>
    //public void SaveToExcel(StringBuilder HtmlTable, String NameFile)
    //{
    //    Response.Clear();
    //    Response.AddHeader("content-disposition", "attachment;filename=" + NameFile + ".xls");
    //    Response.Charset = "";
    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    Response.ContentType = "application/msexcel";
    //    StringWriter stringWrite = new System.IO.StringWriter(HtmlTable);
    //    Response.Write(stringWrite.ToString());
    //    Response.End();
    //}
    /// <summary>
    /// Получить строку из Resource
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected virtual string GetStringResource(string key)
    {
        ResourceManager resourceManager = new ResourceManager(typeof(Resource));
        return resourceManager.GetString(key, CultureInfo.CurrentCulture);
    }
    /// <summary>
    /// Получить строку ресурса ResourceBase
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected virtual string GetStringBaseResource(string key)
    {
        ResourceManager resourceBaseManager = new ResourceManager(typeof(ResourceBase));
        return resourceBaseManager.GetString(key, CultureInfo.CurrentCulture);
    }
    /// <summary>
    /// Метод инициализации культуры 
    /// </summary>
    protected override void InitializeCulture()
    {
        if ((Request.Form["ctl00$Language1"] != null) || (Request.Form["Language1"] != null))
        {
            if (Request.Form["ctl00$Language1"] != null)
            { this.currentLanguage = Request.Form["ctl00$Language1"]; }
            if (Request.Form["Language1"] != null)
            { this.currentLanguage = Request.Form["Language1"]; }

        }
        else
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(this.currentLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(this.currentLanguage);
        }
        base.InitializeCulture();
    }
    /// <summary>
    /// Метод загрузки страницы
    /// </summary>
    /// <param name="e"></param>
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
    }
    /// <summary>
    /// Установить TreeNodeCollection в заданную позицию
    /// </summary>
    /// <param name="Nodes"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    protected bool SetTreeViewValue(TreeNodeCollection Nodes, string value)
    {
        foreach (TreeNode tree in Nodes)
        {
            if (tree.Value == value)
            {
                tree.Selected = true;
                return true;
            }
            SetTreeViewValue(tree.ChildNodes, value);
        }
        return false;
    }

}

/// <summary>
/// Страница с логированием
/// </summary>
public class LogPages : BasePages
{
    protected classLog Log = new classLog();
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack) Log.SaveLogVisits();
    }
}

/// <summary>
/// Защищенная страница
/// </summary>
public class BaseAccessPages : BasePages
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ
    protected classAccessPages AWS = new classAccessPages();
    protected classLog Log = new classLog();

    protected bool Change 
    {
        get 
        {
            if (ViewState["AccessChange"] == null) { ViewState["AccessChange"] = false; }
            return (bool)ViewState["AccessChange"];
        }
        set { ViewState["AccessChange"] = value;}
    }

    #endregion

    #region МЕТОДЫ
    protected override void OnLoad(EventArgs e)
    {

        if (!IsPostBack)
        {
            //String SiteMap = Request.QueryString["ID"];

            AWS.GetAccessToErrorSite(true);
            this.Change = AWS.Change;
            
        }
        base.OnLoad(e);
    }
    #endregion
}
/// <summary>
/// Базовая пользовательская защищенная страница
/// </summary>
public class BaseUsersPages : BaseAccessPages
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ

    protected classUsers cusers = new classUsers();
    protected classWeb cweb = new classWeb();
    protected classSection csection = new classSection();
    #endregion

    #region МЕТОДЫ
    /// <summary>
    /// Показать картинку папка или файл
    /// </summary>
    /// <param name="dataItem"></param>
    /// <returns></returns>
    protected string GetSRCFolder(object dataItem)
    {
        if (DataBinder.Eval(dataItem, "IDWeb") != DBNull.Value)
        { return "~/WebSite/Setup/image/Folder.png"; }
        else { return "~/WebSite/Setup/image/User2.png"; }
    }
    /// <summary>
    /// Определить картинку для загрузки CheckBox (true\false)
    /// </summary>
    /// <param name="dataItem"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    protected string GetSRCCheckBox(object dataItem, string field)
    {
        if (bool.Parse(DataBinder.Eval(dataItem, field).ToString()))
        { return "~/image/wuc/True.png"; }
        else { return "~/image/wuc/False.png"; }
    }
    #endregion

}
/// <summary>
/// Базовая страница для стратегии развития защищенная страница
/// </summary>
public class BaseStrategicPages : BaseAccessPages
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ

    protected classSiteMap csitemap = new classSiteMap();
    protected classTemplatesSteps ctemplatessteps = new classTemplatesSteps();
    protected classMenagerProject cmenager = new classMenagerProject();
    protected classProject cproject = new classProject();
    protected classKPI ckpi = new classKPI();
    protected classOrder corder = new classOrder();
    protected classSection csection = new classSection();

    protected DIRead dir = new DIRead();
    #endregion

    #region МЕТОДЫ
    /// <summary>
    /// Получить строку ресурса ResourceStrategic
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected virtual string GetStringStrategicResource(string key)
    {
        ResourceManager resourceStrategicManager = new ResourceManager(typeof(ResourceStrategic));
        return resourceStrategicManager.GetString(key, CultureInfo.CurrentCulture);
    }
    #endregion
}

public class BaseRailWayAccessPages : BaseAccessPages
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ

    protected DIRead dir = new DIRead();
    #endregion

    #region МЕТОДЫ
    /// <summary>
    /// Получить строку ресурса ResourceRailWay
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected virtual string GetStringRailWayResource(string key)
    {
        ResourceManager resourceRailWayManager = new ResourceManager(typeof(ResourceRailWay));
        return resourceRailWayManager.GetString(key, CultureInfo.CurrentCulture);
    }
    #endregion
}
#endregion

#region МАСТЕР СТРАНИЦЫ САЙТОВ
/// <summary>
/// Базовый класс мастер страницы
/// </summary>
public class BaseMaster : System.Web.UI.MasterPage 
{
    protected override void OnLoad(EventArgs e)
    {
        // Уберем надписи
        if (this.FindControl("ErrorMessage") != null)
        {
            ((Literal)this.FindControl("ErrorMessage")).Text = "";
        }
        if (this.FindControl("InfoMessage") != null)
        {
            ((Literal)this.FindControl("InfoMessage")).Text = "";
        }
        base.OnLoad(e);        
    }
}
#endregion

#region ПОЛЬЗОВАТЕЛЬСКИЕ ЭЛЕМЕНТЫ УПРАВЛЕНИЯ
/// <summary>
/// Базовый класс для пользовательских элементов управления
/// </summary>
public partial class BaseControl : System.Web.UI.UserControl
{
    #region МЕТОДЫ
    /// <summary>
    /// Получить строку из Resource
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected virtual string GetStringResource(string key)
    {
        ResourceManager resourceManager = new ResourceManager(typeof(Resource));
        return resourceManager.GetString(key, CultureInfo.CurrentCulture);
    }
    /// <summary>
    /// Получить строку ресурса ResourceBase
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected virtual string GetStringBaseResource(string key)
    {
        ResourceManager resourceBaseManager = new ResourceManager(typeof(ResourceBase));
        return resourceBaseManager.GetString(key, CultureInfo.CurrentCulture);
    }
    #endregion
}
/// <summary>
/// Базовый клас для пользовательских элементов управления имеющих панель контроля
/// </summary>
public partial class BaseUprControl : BaseControl
{
    public bool Change 
    {
        get
        {
            if (ViewState["Change"] == null) { ViewState["Change"] = false; }
            return (bool)ViewState["Change"];
        }
        set { ViewState["Change"] = value;}
    
    }

    public styleButton? StyleButton
    {
        get;
        set;
    }

    public int SizeImage { get; set; }

    // Показывать кнопки
    public bool? VisibleInsert { get; set; }
    public bool? VisibleUpdate { get; set; }
    public bool? VisibleDelete { get; set; }
    public bool? VisibleSave { get; set; }
    public bool? VisibleCancel { get; set; }

    // Имя кнопок
    public string NameInsert { get; set; }
    public string NameUpdate { get; set; }
    public string NameDelete { get; set; }
    public string NameSave { get; set; }
    public string NameCancel { get; set; }

    protected virtual bool change_button
    {
        get { return this.Change == null ? false : (bool)this.Change; }
    }
    
    protected virtual styleButton style_button
    {
        get { return this.StyleButton == null ? styleButton.img : (styleButton)this.StyleButton; }
    }

    protected virtual int sizeimage
    {
        get { return this.SizeImage == 0 ? 50 : this.SizeImage; }
    }

    protected virtual bool visible_insert
    {
        get { return this.VisibleInsert == null ? true : (bool)this.VisibleInsert; }
    }
    protected virtual bool visible_update
    {
        get { return this.VisibleUpdate == null ? true : (bool)this.VisibleUpdate; }
    }
    protected virtual bool visible_delete
    {
        get { return this.VisibleDelete == null ? true : (bool)this.VisibleDelete; }
    }
    protected virtual bool visible_save
    {
        get { return this.VisibleSave == null ? true : (bool)this.VisibleSave; }
    }
    protected virtual bool visible_cancel
    {
        get { return this.VisibleCancel == null ? true : (bool)this.VisibleCancel; }
    }

    protected virtual string name_insert { get { return this.NameInsert == null ? base.GetStringBaseResource("ToolTipInsert") : base.GetStringBaseResource(this.NameInsert); } }
    protected virtual string name_update { get { return this.NameUpdate == null ? base.GetStringBaseResource("ToolTipUpdate") : base.GetStringBaseResource(this.NameUpdate); } }
    protected virtual string name_delete { get { return this.NameDelete == null ? base.GetStringBaseResource("ToolTipDelete") : base.GetStringBaseResource(this.NameDelete); } }
    protected virtual string name_save { get { return this.NameSave == null ? base.GetStringBaseResource("ToolTipSave") : base.GetStringBaseResource(this.NameSave); } }
    protected virtual string name_cancel { get { return this.NameCancel == null ? base.GetStringBaseResource("ToolTipCancel") : base.GetStringBaseResource(this.NameCancel); } }

}
/// <summary>
/// Базовый клас для пользовательских элементов управления работающих со стандартными базами данных
/// </summary>
public partial class BaseControlUpdate : BaseUprControl
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ
    protected classWeb cw = new classWeb();
    protected classUsers cusers = new classUsers();
    protected classSite csite = new classSite();
    protected classSiteMap csitemap = new classSiteMap();
    protected classSection csection = new classSection();
    protected classAccessSiteMap casm = new classAccessSiteMap();
    protected classTemplatesSteps ctemplatessteps = new classTemplatesSteps();
    protected classMenagerProject cmenager = new classMenagerProject();
    protected classProject cproject = new classProject();
    protected classFiles cfile = new classFiles();
    protected classOrder corder = new classOrder();

    // Определим делегатов
    public delegate void Data_Selecting(object sender, ObjectDataSourceSelectingEventArgs e);
    public delegate void Data_Selected(object sender, ObjectDataSourceStatusEventArgs e);
    public delegate void Data_Inserting(object sender, ObjectDataSourceMethodEventArgs e);
    public delegate void Data_Inserted(object sender, ObjectDataSourceStatusEventArgs e);
    public delegate void Data_Updating(object sender, ObjectDataSourceMethodEventArgs e);
    public delegate void Data_Updated(object sender, ObjectDataSourceStatusEventArgs e);
    public delegate void Data_Deleting(object sender, ObjectDataSourceMethodEventArgs e);
    public delegate void Data_Deleted(object sender, ObjectDataSourceStatusEventArgs e);
    public delegate void Data_CancelClick(object sender, EventArgs e);
    public delegate void Source_Refresh(object sender, EventArgs e);

    #endregion

    #region МЕТОДЫ

    #region Методы отображения информации на сайте
    /// <summary>
    /// Определить картинку для загрузки CheckBox (true\false)
    /// </summary>
    /// <param name="dataItem"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    protected string GetSRCCheckBox(object dataItem, string field)
    {
        if (bool.Parse(DataBinder.Eval(dataItem, field).ToString()))
        { return "~/image/wuc/True.png"; }
        else { return "~/image/wuc/False.png"; }
    }
    /// <summary>
    /// Показать доступ
    /// </summary>
    /// <param name="dataItem"></param>
    /// <returns></returns>
    protected string GetAccess(object dataItem)
    {
        if (DataBinder.Eval(dataItem, "Access") != DBNull.Value)
        {
            int acc = int.Parse(DataBinder.Eval(dataItem, "Access").ToString());
            Access access = (Access)acc;
            return access.ToString();
        }
        else return "-";
    }
    /// <summary>
    /// Определение типа учетной записи
    /// </summary>
    /// <param name="dataItem"></param>
    /// <returns></returns>
    protected string GetTypeAccount(object dataItem)
    {
        if (DataBinder.Eval(dataItem, "IDWeb") != DBNull.Value)
        {
            return base.GetStringBaseResource("tsGroup");
        }
        else { return base.GetStringBaseResource("tsAccount"); }
    }
    /// <summary>
    /// Показать картинку папка или файл
    /// </summary>
    /// <param name="dataItem"></param>
    /// <returns></returns>
    protected string GetSRCFolder(object dataItem)
    {
        if (DataBinder.Eval(dataItem, "IDWeb") != DBNull.Value)
        { return "~/WebSite/Setup/image/Folder.png"; }
        else { return "~/WebSite/Setup/image/User2.png"; }
    }
    /// <summary>
    /// Возвращает строку в зависимости от сотояния bool
    /// </summary>
    /// <param name="dataItem"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    protected string GetStringBool(object dataItem, string field, string styletrue, string stylefalse )
    {
        if (bool.Parse(DataBinder.Eval(dataItem, field).ToString()))
        { return styletrue; }
        else { return stylefalse; }
    }
    /// <summary>
    /// Вернуть состояние тега display от
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    protected string GetDisplay(bool value) 
    {
        return value ? "normal" : "none";
    }
    #endregion

    #endregion

}
/// <summary>
/// Базовый клас для пользовательских элементов управления работающих со стандартными базами данных при помощи комп FormView
/// </summary>
public partial class BaseControlUpdateFormView : BaseControlUpdate 
{
    protected fmComponent fmc = new fmComponent();

    /// <summary>
    /// Режим FormView
    /// </summary>
    public FormViewMode ModeTable
    {
        get;
        set;
    }

    #region МЕТОДЫ

    /// <summary>
    /// Отобразить компонент FormView 
    /// </summary>
    /// <param name="fvm"></param>
    protected void OutFormView(FormView fv, FormViewMode fvm)
    {
        if (fv.CurrentMode != fvm)
        {
            fv.ChangeMode(fvm);
            this.ModeTable = fv.CurrentMode;
        }
        fv.DataBind();
    }

    #endregion
}

public partial class BaseControlUpdateListView : BaseControlUpdate
{
    protected lvComponent lvc = new lvComponent();

    #region МЕТОДЫ

    #endregion
}

#endregion