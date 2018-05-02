using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBase;

public partial class panelUpr : BaseUprControl
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ

    #region задаваеммые поля
    // Тип панели
    public styleUpr StylePanel { get; set; }

    public bool? VisibleUp { get; set; }
    public bool? VisibleDown{ get; set; }
    public bool? VisibleSkip{ get; set; }
    public bool? VisibleCurrent{ get; set; }
    public bool? VisibleStepClear{ get; set; }
    public bool? VisibleStepCreate{ get; set; }

    public string ValidationGroupInsert { get; set; }
    public string ValidationGroupUpdate { get; set; }
    public string ValidationGroup { get; set; }

    public string NameUp { get; set; }
    public string NameDown { get; set; }
    public string NameSkip { get; set; }
    public string NameCurrent { get; set; }
    public string NameStepClear { get; set; }
    public string NameStepCreate { get; set; }
    // Сообщения при нажатии удалить
    public string ConfirmDelete
    {
        set;
        get;
    }
    // Сообщения при нажатии пропустить
    public string ConfirmSkip
    {
        set;
        get;
    }
    // Сообщения при блокировке экрана
    public string ConfirmLook
    {
        set;
        get;
    }
    // Сообщения о сбросе шагов
    public string ConfirmStepClear
    {
        set;
        get;
    }
    // Сообщение о создании шагов
    public string ConfirmStepCreate
    {
        set;
        get;
    }
    #endregion

    #region Определение полей

    protected styleUpr style_upr 
    {
        get { return this.StylePanel == null ? styleUpr.InsUpdDel : (styleUpr)this.StylePanel; }
    }

    protected virtual string validationgroup_insert
    {
        get { return this.ValidationGroupInsert == null ? this.ValidationGroup : this.ValidationGroupInsert; }
    }

    protected virtual string validationgroup_update
    {
        get { return this.ValidationGroupUpdate == null ? this.ValidationGroup : this.ValidationGroupUpdate; }
    }

    protected virtual bool visible_up
    {
        get { return this.VisibleUp == null ? true : (bool)this.VisibleUp; }
    }
    protected virtual bool visible_down
    {
        get { return this.VisibleDown == null ? true : (bool)this.VisibleDown; }
    }
    protected virtual bool visible_skip
    {
        get { return this.VisibleSkip == null ? true : (bool)this.VisibleSkip; }
    }
    protected virtual bool visible_current
    {
        get { return this.VisibleCurrent == null ? true : (bool)this.VisibleCurrent; }
    }
    protected virtual bool visible_stepclear
    {
        get { return this.VisibleStepClear == null ? true : (bool)this.VisibleStepClear; }
    }
    protected virtual bool visible_stepcreate
    {
        get { return this.VisibleStepCreate == null ? true : (bool)this.VisibleStepCreate; }
    }

    protected virtual string confirm_delete { get { return this.ConfirmDelete == null ? null : base.GetStringBaseResource(this.ConfirmDelete)+ ";"; } }
    protected virtual string confirm_skip { get { return this.ConfirmSkip == null ? null : base.GetStringBaseResource(this.ConfirmSkip)+ ";"; } }
    protected virtual string confirm_look { get { return this.ConfirmLook == null ? base.GetStringResource("MessageLock") + ";" : base.GetStringBaseResource(this.ConfirmLook) + ";"; } }
    protected virtual string confirm_stepclear { get { return this.ConfirmStepClear == null ? null : base.GetStringBaseResource(this.ConfirmStepClear) + ";"; } }
    protected virtual string confirm_stepcreate { get { return this.ConfirmStepCreate == null ? null : base.GetStringBaseResource(this.ConfirmStepCreate) + ";"; } }
    
    protected virtual string name_up { get { return this.NameUp == null ? base.GetStringBaseResource("ToolTipUp") : base.GetStringBaseResource(this.NameUp); } }
    protected virtual string name_down { get { return this.NameDown == null ? base.GetStringBaseResource("ToolTipDown") : base.GetStringBaseResource(this.NameDown); } }
    protected virtual string name_skip { get { return this.NameSkip == null ? base.GetStringBaseResource("ToolTipSkip") : base.GetStringBaseResource(this.NameSkip); } }
    protected virtual string name_current { get { return this.NameCurrent == null ? base.GetStringBaseResource("ToolTipCurrent") : base.GetStringBaseResource(this.NameCurrent); } }
    protected virtual string name_stepclear { get { return this.NameStepClear == null ? base.GetStringBaseResource("ToolTipStepClear") : base.GetStringBaseResource(this.NameStepClear); } }
    protected virtual string name_stepcreate { get { return this.NameStepCreate == null ? base.GetStringBaseResource("ToolTipStepCreate") : base.GetStringBaseResource(this.NameStepCreate); } }

    protected LinkButton lbutton_Insert 
    {
        get
        {
            LinkButton lbInsert = new LinkButton();
            lbInsert.ID = "Insert";
            lbInsert.Text = base.name_insert;
            lbInsert.CommandName = "New";
            lbInsert.CausesValidation = false;
            lbInsert.CssClass = "LinkCommand";
            lbInsert.Attributes.Add("onclick", this.confirm_look);
            lbInsert.Click += lbInsert_Click;
            lbInsert.Visible = base.visible_insert;
            return lbInsert;
        } 
    }
    protected LinkButton lbutton_Update 
    {
        get
        {
            LinkButton lbUpdate = new LinkButton();
            lbUpdate.ID = "Update";
            lbUpdate.Text = this.name_update;
            lbUpdate.CommandName = "Edit";
            lbUpdate.CausesValidation = false;
            lbUpdate.CssClass = "LinkCommand";
            lbUpdate.Attributes.Add("onclick", this.confirm_look);
            lbUpdate.Click += lbUpdate_Click;
            lbUpdate.Visible = base.visible_update;
            return lbUpdate;
        } 
    }
    protected LinkButton lbutton_Delete 
    {
        get
        {
            LinkButton lbDelete = new LinkButton();
            lbDelete.ID = "Delete";
            lbDelete.Text = this.name_delete;
            lbDelete.CommandName = "Delete";
            lbDelete.CausesValidation = false;
            lbDelete.CssClass = "LinkCommand";
            lbDelete.Attributes.Add("onclick", this.confirm_delete + this.confirm_look);
            lbDelete.Click += lbDelete_Click;
            lbDelete.Visible = base.visible_delete;
            return lbDelete;
        } 
    }
    protected LinkButton lbutton_SaveUpdate 
    {
        get
        {
            LinkButton lbSaveUpdate = new LinkButton();
            lbSaveUpdate.ID = "Save";
            lbSaveUpdate.Text = this.name_save;
            lbSaveUpdate.CommandName = "Update";
            lbSaveUpdate.CausesValidation = true;
            lbSaveUpdate.CssClass = "LinkCommand";
            //lbSaveUpdate.Attributes.Add("onclick", this.confirm_look);
            lbSaveUpdate.Click += lbSave_Click;
            lbSaveUpdate.Visible = base.visible_save;
            lbSaveUpdate.ValidationGroup = this.validationgroup_update;
            return lbSaveUpdate;
        } 
    }
    protected LinkButton lbutton_SaveInsert 
    {
        get
        {
            LinkButton lbSaveInsert = new LinkButton();
            lbSaveInsert.ID = "Save";
            lbSaveInsert.Text = this.name_save;
            lbSaveInsert.CommandName = "Insert";
            lbSaveInsert.CausesValidation = true;
            lbSaveInsert.CssClass = "LinkCommand";
            //lbSaveInsert.Attributes.Add("onclick", this.confirm_look);
            lbSaveInsert.Click += lbSave_Click;
            lbSaveInsert.Visible = base.visible_save;
            lbSaveInsert.ValidationGroup = this.validationgroup_insert;
            return lbSaveInsert;
        } 
    }
    protected LinkButton lbutton_Cancel 
    {
        get
        {
            LinkButton lbCancel = new LinkButton();
            lbCancel.ID = "Cancel";
            lbCancel.Text = this.name_cancel;
            lbCancel.CommandName = "Cancel";
            lbCancel.CausesValidation = false;
            lbCancel.CssClass = "LinkCommand";
            lbCancel.Attributes.Add("onclick", this.confirm_look);
            lbCancel.Click += lbCancel_Click;
            lbCancel.Visible = base.visible_cancel;
            return lbCancel;
        } 
    }
    protected LinkButton lbutton_Up 
    {
        get
        {
            LinkButton lbUp = new LinkButton();
            lbUp.ID = "Up";
            lbUp.Text = this.name_up;
            lbUp.CausesValidation = false;
            lbUp.CssClass = "LinkCommand";
            lbUp.Attributes.Add("onclick", this.confirm_look);
            lbUp.Click += lbUp_Click;
            lbUp.Visible = this.visible_up;
            return lbUp;
        } 
    }
    protected LinkButton lbutton_Down 
    {
        get
        {
            LinkButton lbDown = new LinkButton();
            lbDown.ID = "Down";
            lbDown.Text = this.name_down;
            lbDown.CausesValidation = false;
            lbDown.CssClass = "LinkCommand";
            lbDown.Attributes.Add("onclick", this.confirm_look);
            lbDown.Click += lbDown_Click;
            lbDown.Visible = this.visible_down;
            return lbDown;
        } 
    }
    protected LinkButton lbutton_Skip 
    {
        get
        {
            LinkButton lbSkip = new LinkButton();
            lbSkip.ID = "Skip";
            lbSkip.Text = this.name_skip;
            lbSkip.CausesValidation = false;
            lbSkip.CssClass = "LinkCommand";
            lbSkip.Attributes.Add("onclick", this.confirm_skip);
            lbSkip.Click += lbSkip_Click;
            lbSkip.Visible = this.visible_skip;
            return lbSkip;
        } 
    }
    protected LinkButton lbutton_Current 
    {
        get
        {
            LinkButton lbCurrent = new LinkButton();
            lbCurrent.ID = "Current";
            lbCurrent.Text = this.name_current;
            lbCurrent.CausesValidation = false;
            lbCurrent.CssClass = "LinkCommand";
            lbCurrent.Attributes.Add("onclick", this.confirm_skip);
            lbCurrent.Click += lbCurrent_Click;
            lbCurrent.Visible = this.visible_current;
            return lbCurrent;
        } 
    }
    protected LinkButton lbutton_StepClear
    {
        get
        {
            LinkButton lbStepClear = new LinkButton();
            lbStepClear.ID = "StepClear";
            lbStepClear.Text = this.name_stepclear;
            lbStepClear.CausesValidation = false;
            lbStepClear.CssClass = "LinkCommand";
            lbStepClear.Attributes.Add("onclick", this.confirm_stepclear);
            lbStepClear.Click += lbStepClear_Click;
            lbStepClear.Visible = this.visible_stepclear;
            return lbStepClear;
        } 
    }
    protected LinkButton lbutton_StepCreate
    {
        get
        {
            LinkButton lbStepCreate = new LinkButton();
            lbStepCreate.ID = "StepCreate";
            lbStepCreate.Text = this.name_stepcreate;
            lbStepCreate.CausesValidation = false;
            lbStepCreate.CssClass = "LinkCommand";
            lbStepCreate.Attributes.Add("onclick", this.confirm_stepcreate);
            lbStepCreate.Click += lbStepCreate_Click;
            lbStepCreate.Visible = this.visible_stepcreate;
            return lbStepCreate;
        } 
    }

    protected ImageButton ibutton_Insert 
    {
        get 
        {
            ImageButton ibInsert = new ImageButton();
            ibInsert.ID = "Insert";
            ibInsert.ToolTip = this.name_insert;
            ibInsert.CommandName = "New";
            ibInsert.ImageUrl = "~/image/wuc/Insert.png";
            ibInsert.Width = Unit.Pixel(this.sizeimage);
            ibInsert.Height = Unit.Pixel(this.sizeimage);
            ibInsert.CausesValidation = false;
            ibInsert.Attributes.Add("onclick", this.confirm_look);
            ibInsert.Click += ibInsert_Click;
            ibInsert.Visible = base.visible_insert;
            return ibInsert;
        }
    }
    protected ImageButton ibutton_Update 
    {
        get 
        {
            ImageButton ibUpdate = new ImageButton();
            ibUpdate.ID = "Update";
            ibUpdate.ToolTip = this.name_update;
            ibUpdate.CommandName = "Edit";
            ibUpdate.CausesValidation = false;
            ibUpdate.ImageUrl = "~/image/wuc/Edit.png";
            ibUpdate.Width = Unit.Pixel(this.sizeimage);
            ibUpdate.Height = Unit.Pixel(this.sizeimage);
            ibUpdate.Attributes.Add("onclick", this.confirm_look);
            ibUpdate.Click += ibUpdate_Click;
            ibUpdate.Visible = base.visible_update;
            return ibUpdate;
        }
    }
    protected ImageButton ibutton_Delete 
    {
        get 
        {
            ImageButton ibDelete = new ImageButton();
            ibDelete.ID = "Delete";
            ibDelete.ToolTip = this.name_delete;
            ibDelete.CommandName = "Delete";
            ibDelete.CausesValidation = false;
            ibDelete.ImageUrl = "~/image/wuc/Delete.png";
            ibDelete.Width = Unit.Pixel(this.sizeimage);
            ibDelete.Height = Unit.Pixel(this.sizeimage);
            ibDelete.Attributes.Add("onclick", this.confirm_delete + this.confirm_look);
            ibDelete.Click += ibDelete_Click;
            ibDelete.Visible = base.visible_delete;
            return ibDelete;
        }
    }
    protected ImageButton ibutton_SaveUpdate
    {
        get
        {
            ImageButton ibSaveUpdate = new ImageButton();
            ibSaveUpdate.ID = "Save";
            ibSaveUpdate.ToolTip = this.name_save;
            ibSaveUpdate.CommandName = "Update";
            ibSaveUpdate.CausesValidation = true;
            ibSaveUpdate.ImageUrl = "~/image/wuc/Save.png";
            ibSaveUpdate.Width = Unit.Pixel(this.sizeimage);
            ibSaveUpdate.Height = Unit.Pixel(this.sizeimage);
            //ibSaveUpdate.Attributes.Add("onclick", this.confirm_look);
            ibSaveUpdate.Click += ibSave_Click;
            ibSaveUpdate.Visible = base.visible_save;
            ibSaveUpdate.ValidationGroup = this.validationgroup_update;
            return ibSaveUpdate;
        }
    }
    protected ImageButton ibutton_SaveInsert
    {
        get
        {
            ImageButton ibSaveInsert = new ImageButton();
            ibSaveInsert.ID = "Save";
            ibSaveInsert.ToolTip = this.name_save;
            ibSaveInsert.CommandName = "Insert";
            ibSaveInsert.CausesValidation = true;
            ibSaveInsert.ImageUrl = "~/image/wuc/Save.png";
            ibSaveInsert.Width = Unit.Pixel(this.sizeimage);
            ibSaveInsert.Height = Unit.Pixel(this.sizeimage);
            //ibSaveInsert.Attributes.Add("onclick", this.confirm_look);
            ibSaveInsert.Visible = base.visible_save;
            ibSaveInsert.Click += ibSave_Click;
            ibSaveInsert.ValidationGroup = this.validationgroup_insert;
            return ibSaveInsert;
        }
    }
    protected ImageButton ibutton_Cancel
    {
        get
        {
            ImageButton ibCancel = new ImageButton();
            ibCancel.ID = "Cancel";
            ibCancel.ToolTip = this.name_cancel;
            ibCancel.CommandName = "Cancel";
            ibCancel.CausesValidation = false;
            ibCancel.ImageUrl = "~/image/wuc/Cancel.png";
            ibCancel.Width = Unit.Pixel(this.sizeimage);
            ibCancel.Height = Unit.Pixel(this.sizeimage);
            ibCancel.Attributes.Add("onclick", this.confirm_look);
            ibCancel.Click += ibCancel_Click;
            ibCancel.Visible = base.visible_cancel;
            return ibCancel;
        }
    }
    protected ImageButton ibutton_Up
    {
        get
        {
            ImageButton ibUp = new ImageButton();
            ibUp.ID = "Up";
            ibUp.ToolTip = this.name_up;
            ibUp.CausesValidation = false;
            ibUp.ImageUrl = "~/image/wuc/Up.png";
            ibUp.Width = Unit.Pixel(this.sizeimage);
            ibUp.Height = Unit.Pixel(this.sizeimage);
            ibUp.Attributes.Add("onclick", this.confirm_look);
            ibUp.Click += ibUp_Click;
            ibUp.Visible = this.visible_up;
            return ibUp;
        }
    }
    protected ImageButton ibutton_Down
    {
        get
        {
            ImageButton ibDown = new ImageButton();
            ibDown.ID = "Down";
            ibDown.ToolTip = this.name_down;
            ibDown.CausesValidation = false;
            ibDown.ImageUrl = "~/image/wuc/Down.png";
            ibDown.Width = Unit.Pixel(this.sizeimage);
            ibDown.Height = Unit.Pixel(this.sizeimage);
            ibDown.Attributes.Add("onclick", this.confirm_look);
            ibDown.Click += ibDown_Click;
            ibDown.Visible = this.visible_down;
            return ibDown;
        }
    }
    protected ImageButton ibutton_Skip
    {
        get
        {
            ImageButton ibSkip = new ImageButton();
            ibSkip.ID = "Skip";
            ibSkip.ToolTip = this.name_skip;
            ibSkip.CausesValidation = false;
            ibSkip.ImageUrl = "~/image/wuc/Skip.png";
            ibSkip.Width = Unit.Pixel(this.sizeimage);
            ibSkip.Height = Unit.Pixel(this.sizeimage);
            ibSkip.Attributes.Add("onclick", this.confirm_skip);
            ibSkip.Click += ibSkip_Click;
            ibSkip.Visible = this.visible_skip;
            return ibSkip;
        }
    }
    protected ImageButton ibutton_Current
    {
        get
        {
            ImageButton ibCurrent = new ImageButton();
            ibCurrent.ID = "Current";
            ibCurrent.ToolTip = this.name_current;
            ibCurrent.CausesValidation = false;
            ibCurrent.ImageUrl = "~/image/wuc/Current.png";
            ibCurrent.Width = Unit.Pixel(this.sizeimage);
            ibCurrent.Height = Unit.Pixel(this.sizeimage);
            ibCurrent.Attributes.Add("onclick", this.confirm_look);
            ibCurrent.Click += ibCurrent_Click;
            ibCurrent.Visible = this.visible_current;
            return ibCurrent;
        }
    }
    protected ImageButton ibutton_StepClear
    {
        get
        {
            ImageButton ibStepClear = new ImageButton();
            ibStepClear.ID = "StepClear";
            ibStepClear.ToolTip = this.name_stepclear;
            ibStepClear.CausesValidation = false;
            ibStepClear.ImageUrl = "~/image/wuc/StepClear.png";
            ibStepClear.Width = Unit.Pixel(this.sizeimage);
            ibStepClear.Height = Unit.Pixel(this.sizeimage);
            ibStepClear.Attributes.Add("onclick", this.confirm_stepclear);
            ibStepClear.Click += ibStepClear_Click;
            ibStepClear.Visible = this.visible_stepclear;
            return ibStepClear;
        }
    }
    protected ImageButton ibutton_StepCreate
    {
        get
        {
            ImageButton ibStepCreate = new ImageButton();
            ibStepCreate.ID = "StepCreate";
            ibStepCreate.ToolTip = this.name_stepcreate;
            ibStepCreate.CausesValidation = false;
            ibStepCreate.ImageUrl = "~/image/wuc/StepCreate.png";
            ibStepCreate.Width = Unit.Pixel(this.sizeimage);
            ibStepCreate.Height = Unit.Pixel(this.sizeimage);
            ibStepCreate.Attributes.Add("onclick", this.confirm_stepcreate);
            ibStepCreate.Click += ibStepCreate_Click;
            ibStepCreate.Visible = this.visible_stepcreate;
            return ibStepCreate;
        }
    }

    protected Control button_insert 
    {
        get 
        {
            switch (this.style_button)
            {
                case styleButton.link:
                    return this.lbutton_Insert;
                case styleButton.img:
                    return this.ibutton_Insert;
                case styleButton.button:
                    return null;
                default: return null;
            }
        }
    }
    protected Control button_update 
    {
        get 
        {
            switch (this.style_button)
            {
                case styleButton.link:
                    return this.lbutton_Update;
                case styleButton.img:
                    return this.ibutton_Update;
                case styleButton.button:
                    return null;
                default: return null;
            }
        }
    }
    protected Control button_delete 
    {
        get 
        {
            switch (this.style_button)
            {
                case styleButton.link:
                    return this.lbutton_Delete;
                case styleButton.img:
                    return this.ibutton_Delete;
                case styleButton.button:
                    return null;
                default: return null;
            }
        }
    }
    protected Control button_saveupdate
    {
        get 
        {
            switch (this.style_button)
            {
                case styleButton.link:
                    return this.lbutton_SaveUpdate;
                case styleButton.img:
                    return this.ibutton_SaveUpdate;
                case styleButton.button:
                    return null;
                default: return null;
            }
        }
    }
    protected Control button_saveinsert
    {
        get 
        {
            switch (this.style_button)
            {
                case styleButton.link:
                    return this.lbutton_SaveInsert;
                case styleButton.img:
                    return this.ibutton_SaveInsert;
                case styleButton.button:
                    return null;
                default: return null;
            }
        }
    }
    protected Control button_cancel
    {
        get 
        {
            switch (this.style_button)
            {
                case styleButton.link:
                    return this.lbutton_Cancel;
                case styleButton.img:
                    return this.ibutton_Cancel;
                case styleButton.button:
                    return null;
                default: return null;
            }
        }
    }
    protected Control button_up
    {
        get 
        {
            switch (this.style_button)
            {
                case styleButton.link:
                    return this.lbutton_Up;
                case styleButton.img:
                    return this.ibutton_Up;
                case styleButton.button:
                    return null;
                default: return null;
            }
        }
    }
    protected Control button_down
    {
        get 
        {
            switch (this.style_button)
            {
                case styleButton.link:
                    return this.lbutton_Down;
                case styleButton.img:
                    return this.ibutton_Down;
                case styleButton.button:
                    return null;
                default: return null;
            }
        }
    }
    protected Control button_skip
    {
        get 
        {
            switch (this.style_button)
            {
                case styleButton.link:
                    return this.lbutton_Skip;
                case styleButton.img:
                    return this.ibutton_Skip;
                case styleButton.button:
                    return null;
                default: return null;
            }
        }
    }
    protected Control button_current
    {
        get 
        {
            switch (this.style_button)
            {
                case styleButton.link:
                    return this.lbutton_Current;
                case styleButton.img:
                    return this.ibutton_Current;
                case styleButton.button:
                    return null;
                default: return null;
            }
        }
    }
    protected Control button_stepclear
    {
        get 
        {
            switch (this.style_button)
            {
                case styleButton.link:
                    return this.lbutton_StepClear;
                case styleButton.img:
                    return this.ibutton_StepClear;
                case styleButton.button:
                    return null;
                default: return null;
            }
        }
    }
    protected Control button_stepcreate
    {
        get 
        {
            switch (this.style_button)
            {
                case styleButton.link:
                    return this.lbutton_StepCreate;
                case styleButton.img:
                    return this.ibutton_StepCreate;
                case styleButton.button:
                    return null;
                default: return null;
            }
        }
    }
    #endregion

    #region Определение событий
    // Обявим делегатов
    public delegate void Insert_Click(object sender, EventArgs e);
    public delegate void Update_Click(object sender, EventArgs e);
    public delegate void Delete_Click(object sender, EventArgs e);
    public delegate void Save_Click(object sender, EventArgs e);
    public delegate void Cancel_Click(object sender, EventArgs e);
    public delegate void Up_Click(object sender, EventArgs e);
    public delegate void Down_Click(object sender, EventArgs e);
    public delegate void Skip_Click(object sender, EventArgs e);
    public delegate void Current_Click(object sender, EventArgs e);
    public delegate void StepClear_Click(object sender, EventArgs e);
    public delegate void StepCreate_Click(object sender, EventArgs e);

    // Определим событие
    public event Insert_Click InsertClick;
    public event Update_Click UpdateClick;
    public event Delete_Click DeleteClick;
    public event Save_Click SaveClick;
    public event Cancel_Click CancelClick;
    public event Up_Click UpClick;
    public event Down_Click DownClick;
    public event Skip_Click SkipClick;
    public event Current_Click CurrentClick;
    public event StepClear_Click StepClearClick;
    public event StepCreate_Click StepCreateClick;
    #endregion

    #endregion

    #region МЕТОДЫ

    /// <summary>
    /// Заполнить паренль PlaceHolder
    /// </summary>
    protected void OutPH()
    {
        if (!base.change_button) return;
        phButton.Controls.Clear();
        switch (this.style_upr) 
        { 
            case styleUpr.InsUpdDel:
                phButton.Controls.Add(this.button_insert);
                phButton.Controls.Add(this.button_update);
                phButton.Controls.Add(this.button_delete);
                break;
            case styleUpr.UpdCan:
                phButton.Controls.Add(this.button_saveupdate);
                phButton.Controls.Add(this.button_cancel);
                break;
            case  styleUpr.InsCan:
                phButton.Controls.Add(this.button_saveinsert);
                phButton.Controls.Add(this.button_cancel);
                break;
            case styleUpr.InsUpdDelUpDow:
                phButton.Controls.Add(this.button_insert);
                phButton.Controls.Add(this.button_update);
                phButton.Controls.Add(this.button_delete);
                phButton.Controls.Add(this.button_up);
                phButton.Controls.Add(this.button_down);
                break;
            case styleUpr.UpdSkip:
                phButton.Controls.Add(this.button_update);
                //phButton.Controls.Add(this.button_current);
                phButton.Controls.Add(this.button_skip);
                break;
            case styleUpr.InsUpdDelClrCrt:
                phButton.Controls.Add(this.button_insert);
                phButton.Controls.Add(this.button_update);
                phButton.Controls.Add(this.button_delete);
                phButton.Controls.Add(this.button_stepclear);
                phButton.Controls.Add(this.button_stepcreate);
                break;
        }
    }
    /// <summary>
    /// Показать кнопки
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }
        OutPH();       
    }
    /// <summary>
    /// Обработка событий
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibInsert_Click(object sender, ImageClickEventArgs e)
    {
        if (InsertClick != null) InsertClick(this, new EventArgs());
    }
    protected void lbInsert_Click(object sender, EventArgs e)
    {
        if (InsertClick != null) InsertClick(this, new EventArgs());
    }
    protected void ibUpdate_Click(object sender, ImageClickEventArgs e)
    {
        if (UpdateClick != null) UpdateClick(this, new EventArgs());
    }
    protected void lbUpdate_Click(object sender, EventArgs e)
    {
        if (UpdateClick != null) UpdateClick(this, new EventArgs());
    }
    protected void ibDelete_Click(object sender, ImageClickEventArgs e)
    {
        if (DeleteClick != null) DeleteClick(this, new EventArgs());
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        if (DeleteClick != null) DeleteClick(this, new EventArgs());
    }
    /// <summary>
    /// Сохранить
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibSave_Click(object sender, ImageClickEventArgs e)
    {
        if (SaveClick != null) SaveClick(this, new EventArgs());
    }
    protected void lbSave_Click(object sender, EventArgs e)
    {
        if (SaveClick != null) SaveClick(this, new EventArgs());
    }
    /// <summary>
    /// Отмена
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibCancel_Click(object sender, ImageClickEventArgs e)
    {
        if (CancelClick != null) CancelClick(this, new EventArgs());
    }
    protected void lbCancel_Click(object sender, EventArgs e)
    {
        if (CancelClick != null) CancelClick(this, new EventArgs());
    }
    /// <summary>
    /// Событие Up
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibUp_Click(object sender, ImageClickEventArgs e)
    {
        if (UpClick != null) UpClick(this, new EventArgs());
    }
    protected void lbUp_Click(object sender, EventArgs e)
    {
        if (UpClick != null) UpClick(this, new EventArgs());
    }
    /// <summary>
    /// Событие Down
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibDown_Click(object sender, ImageClickEventArgs e)
    {
        if (DownClick != null) DownClick(this, new EventArgs());
    }
    protected void lbDown_Click(object sender, EventArgs e)
    {
        if (DownClick != null) DownClick(this, new EventArgs());
    }
    /// <summary>
    /// Событие Skip
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibSkip_Click(object sender, ImageClickEventArgs e)
    {
        if (SkipClick != null) SkipClick(this, new EventArgs());
    }
    protected void lbSkip_Click(object sender, EventArgs e)
    {
        if (SkipClick != null) SkipClick(this, new EventArgs());
    }
    /// <summary>
    /// событие Current
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibCurrent_Click(object sender, ImageClickEventArgs e)
    {
        if (CurrentClick != null) CurrentClick(this, new EventArgs());
    }
    protected void lbCurrent_Click(object sender, EventArgs e)
    {
        if (CurrentClick != null) CurrentClick(this, new EventArgs());
    }
    /// <summary>
    /// событие StepClear
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibStepClear_Click(object sender, ImageClickEventArgs e)
    {
        if (StepClearClick != null) StepClearClick(this, new EventArgs());
    }
    protected void lbStepClear_Click(object sender, EventArgs e)
    {
        if (StepClearClick != null) StepClearClick(this, new EventArgs());
    }
    /// <summary>
    /// событие StepCreate
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibStepCreate_Click(object sender, ImageClickEventArgs e)
    {
        if (StepCreateClick != null) StepCreateClick(this, new EventArgs());
    }
    protected void lbStepCreate_Click(object sender, EventArgs e)
    {
        if (StepCreateClick != null) StepCreateClick(this, new EventArgs());
    }
    #endregion
}