using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBase;

public partial class controlRules : BaseControl
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ controlRules
    /// <summary>
    /// Текущая строка правил
    /// </summary>
    public string stringRules
    {
        get { return (string)Session["AllRules"]; }
        set
        {
            string buf = (string)Session["AllRules"];
            if (buf != value)
            {
                Session["AllRules"] = value;
                OutPlaceHolder(typeRules.Section);
                OutPlaceHolder(typeRules.Rules1);
            }
        }
    }

    /// <summary>
    /// Событие изменения правила
    /// </summary>
    public class RulesChangeEventArgs : EventArgs
    {
        private string _Rules;
        public string Rules { get { return this._Rules; } }
        public RulesChangeEventArgs(string Rules)
        {
            this._Rules = Rules;
        }
    }

    // Делегаты
    public delegate void Date_ChangeRules(object sender, RulesChangeEventArgs e);
    public delegate void Object_SelectedIndexChanged(object sender, EventArgs e);
    public delegate void Access_SelectedIndexChanged(object sender, EventArgs e);

    // определим события
    public event Object_SelectedIndexChanged ObjectSelectedIndexChanged;
    public event Access_SelectedIndexChanged AccessSelectedIndexChanged;
    public event Date_ChangeRules DateChangeRules;

    #endregion

    #region МЕТОДЫ

    #region Методы установки и преобразования
    /// <summary>
    /// Добавить правило в секцию
    /// </summary>
    /// <param name="rules"></param>
    /// <param name="type"></param>
    /// <param name="section"></param>
    /// <returns></returns>
    protected string AddRules(string rules, typeRules type, int idobject)
    {
        string result = null;
        foreach (typeRules tr in Enum.GetValues(typeof(typeRules)))
        {
            RulesCollection rcol = new RulesCollection(tr);
            rcol.LoadString(rules);           
            if (tr == type)
            {
                rcol.AddRules(new RulesDetali(idobject, Access.Not));
            }
            if (rcol.Count > 0)
            {
                result += rcol.ToString() + ",";
            }
        }
        return result;
    }
    /// <summary>
    /// Изменить правило в секции
    /// </summary>
    /// <param name="rules"></param>
    /// <param name="type"></param>
    /// <param name="idobject"></param>
    /// <param name="access"></param>
    /// <returns></returns>
    protected string UpdateRules(string rules, typeRules type, int idobject, int access)
    {
        if (rules == null) return null;
        string result = null;
        foreach (typeRules tr in Enum.GetValues(typeof(typeRules)))
        {
            RulesCollection rcol = new RulesCollection(tr);
            rcol.LoadString(rules);           
            if (tr == type)
            {
                rcol.ChangeRules(idobject, (Access)access);
            }
            if (rcol.Count > 0)
            {
                result += rcol.ToString() + ",";
            }
        }
        return result;        
    }
    /// <summary>
    /// Удалить правило из секции
    /// </summary>
    /// <param name="rules"></param>
    /// <param name="type"></param>
    /// <param name="idobject"></param>
    /// <returns></returns>
    protected string DeleteRules(string rules, typeRules type, int idobject)
    {
        if (rules == null) return null;
        string result = null;
        foreach (typeRules tr in Enum.GetValues(typeof(typeRules)))
        {
            RulesCollection rcol = new RulesCollection(tr);
            rcol.LoadString(rules);           
            if (tr == type)
            {
                rcol.DeleteRules(idobject);
            }
            if (rcol.Count > 0)
            {
                result += rcol.ToString() + ",";
            }
        }
        return result;
    }
    #endregion

    #region Методы загрузки компонентов и страницы
    /// <summary>
    /// Получит коллекцию по типу
    /// </summary>
    /// <param name="tr"></param>
    /// <returns></returns>
    protected RulesCollection RulesCollection(typeRules tr)
    {
        RulesCollection rc = new RulesCollection(tr);
        rc.LoadString(this.stringRules);
        return rc;
    }
    /// <summary>
    /// Вывести панель PlaceHolder с элементами указанного типа
    /// </summary>
    /// <param name="type"></param>
    protected void OutPlaceHolder(typeRules type)
    {
        if (this.FindControl("ph" + type.ToString()) == null) { return; }
        PlaceHolder ph = (PlaceHolder)this.FindControl("ph" + type.ToString());
        if (ph == null) return;
        RulesCollection rcol = RulesCollection(type);

        ph.Controls.Clear();
        ph.Controls.Add(new LiteralControl("<table class='tabRules'>"));        
        ph.Controls.Add(new LiteralControl("<tr><td class='nametype' colspan ='2'>"));
        ph.Controls.Add(new LiteralControl(type.ToString()+": "));
        DropDownList ddlObject = new DropDownList();
        List<IObj> li = rcol.GetNotObject();
        if (li != null)
        {
            li.Insert(0,new ListSection(0,base.GetStringBaseResource("ddlSelect")));
            ddlObject.Enabled = true;
            ddlObject.ID = "ddl" + type.ToString();
            ddlObject.Items.Clear();
            ddlObject.DataTextField = "NameObject";
            ddlObject.DataValueField = "IDObject";
            ddlObject.DataSource = li;
            //ddlObject.SelectedIndex = -1;
            ddlObject.AutoPostBack = true;
            ddlObject.SelectedIndexChanged += ddlObject_SelectedIndexChanged;
            ddlObject.DataBind();
        }
        else { ddlObject.Enabled = false; }
        ph.Controls.Add(ddlObject);
        ph.Controls.Add(new LiteralControl("</td></tr>"));
        foreach (RulesDetali rd in RulesCollection(type)) 
        {
            ph.Controls.Add(new LiteralControl("<tr><td class='object'>"));
            ph.Controls.Add(new LiteralControl(rd.nameObject + ":"));
            ph.Controls.Add(new LiteralControl("</td><td class='access'>"));
            DropDownList ddlAccess = new DropDownList();
            
            ddlAccess.Items.Clear();
            foreach (Access tr in Enum.GetValues(typeof(Access)))
            {
                ddlAccess.Items.Add(new ListItem(tr.ToString(), ((int)tr).ToString()));
            }
            ddlAccess.Items.Add(new ListItem("Clear", "4"));
            ddlAccess.ID = type.ToString() + "_" + rd.IDObject.ToString();
            ddlAccess.AutoPostBack = true;
            ddlAccess.SelectedIndexChanged += ddlAccess_SelectedIndexChanged;
            ddlAccess.SelectedValue = ((int)rd.Access).ToString();
            ddlAccess.DataBind();
            ph.Controls.Add(ddlAccess);

        }
        ph.Controls.Add(new LiteralControl("</table>"));
    }
    /// <summary>
    /// Показать компонент
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        OutPlaceHolder(typeRules.Section);
        OutPlaceHolder(typeRules.Rules1);
    }
    #endregion

    #region Методы обработки действий пользователей
    /// <summary>
    /// Добавлен объект
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlObject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (((DropDownList)sender).SelectedIndex > 0)
        {
            typeRules type = (typeRules)Enum.Parse(typeof(typeRules), ((DropDownList)sender).ID.Remove(0,3));
            this.stringRules = AddRules(this.stringRules, type, int.Parse(((DropDownList)sender).SelectedValue));
        }
        ((DropDownList)sender).SelectedIndex = -1;
        if (ObjectSelectedIndexChanged != null) ObjectSelectedIndexChanged(this, e);
        if (DateChangeRules != null) DateChangeRules(this, new RulesChangeEventArgs(this.stringRules));
    }
    /// <summary>
    /// Изменен доступ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlAccess_SelectedIndexChanged(object sender, EventArgs e)
    {
        int access = int.Parse(((DropDownList)sender).SelectedValue);
        string[] type_id = ((DropDownList)sender).ID.Split('_');
        typeRules tr = (typeRules)Enum.Parse(typeof(typeRules), type_id[0].Trim());
        int idobj = int.Parse(type_id[1].Trim());
        if (access < 4)
        {
            this.stringRules = UpdateRules(this.stringRules, tr, idobj, access);
        }
        else
        {
            this.stringRules = DeleteRules(this.stringRules, tr, idobj);
        }
        if (AccessSelectedIndexChanged != null) AccessSelectedIndexChanged(this, e);
        if (DateChangeRules != null) DateChangeRules(this, new RulesChangeEventArgs(this.stringRules));
    }
    #endregion

    #endregion
}