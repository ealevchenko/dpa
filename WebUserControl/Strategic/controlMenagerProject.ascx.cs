﻿using Strategic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controlMenagerProject : BaseControlUpdateFormView
{
    #region ПОЛЯ И ИНИЦИАЛИЗАЦИЯ controlMenagerProject
    // ID выборки
    public int? IDMenagerProject
    {
        get;
        set;
    }
    /// <summary>
    /// Выводить информацию 
    /// </summary>
    public bool OutInfoText
    {
        get;
        set;
    }

    // Получить текущий режим таблицы
    public FormViewMode CurrentMode { get { return fvTable.CurrentMode; } }

    protected int? idmenagerproject { get { return this.IDMenagerProject != null ? this.IDMenagerProject : null; } }

    protected bool outinfo { get { return this.OutInfoText != null ? this.OutInfoText : false; } }

    // переменные для записи
    protected int save_iduser
    {
        get
        {
            DropDownList ddlUserEnterprise = base.fmc.GetDropDownList(fvTable, "ddlUserEnterprise");
            if (ddlUserEnterprise != null)
            {
                if (ddlUserEnterprise.SelectedIndex >= 0) { return int.Parse(ddlUserEnterprise.SelectedValue); }

            }
            return 0;
        }
    }
    protected string save_wphone { get { return base.fmc.GetTextTextBox(fvTable, "tbWPhone"); } }
    protected string save_mphone { get { return base.fmc.GetTextTextBox(fvTable, "tbMPhone"); } }
    protected bool save_supermenager { get { return base.fmc.GetCheckedCheckBox(fvTable, "cbSuperMenager"); } }

    // События
    public event Data_Selecting DataSelecting;
    public event Data_Selected DataSelected;
    public event Data_Inserting DataInserting;
    public event Data_Inserted DataInserted;
    public event Data_Updating DataUpdating;
    public event Data_Updated DataUpdated;
    public event Data_Deleting DataDeleting;
    public event Data_Deleted DataDeleted;
    public event Data_CancelClick DataCancelClick;

    #endregion

    #region Методы управления режимами
    /// <summary>
    /// Событие перед обновлением режима
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void fvTable_ModeChanging(object sender, FormViewModeEventArgs e)
    {
        OutFormView(fvTable, e.NewMode);
    }
    /// <summary>
    /// Включить или отключить режим контроля на стороне клиентов (необходим когда внедряется окно добавить новый список выбора)
    /// </summary>
    /// <param name="value"></param>
    public void ValidateClient(bool value)
    {
        base.fmc.SetEnableRegularExpressionValidator(fvTable, "revTypeProject", value);
        base.fmc.SetEnableRegularExpressionValidator(fvTable, "revTypeProjectEng", value);
        base.fmc.SetEnableRegularExpressionValidator(fvTable, "revDescription", value);
        base.fmc.SetEnableRegularExpressionValidator(fvTable, "revDescriptionEng", value);
        base.fmc.SetEnableRequiredFieldValidator(fvTable, "rfvTypeProject", value);
        base.fmc.SetEnableRequiredFieldValidator(fvTable, "rfvTypeProjectEng", value);
        base.fmc.SetEnableRequiredFieldValidator(fvTable, "rfvDescription", value);
        base.fmc.SetEnableRequiredFieldValidator(fvTable, "rfvDescriptionEng", value);
    }
    #endregion

    #region Методы привязки данных
    /// <summary>
    /// Переопределим
    /// </summary>
    public override void DataBind()
    {
        base.OnDataBinding(EventArgs.Empty);
        OutFormView(fvTable, base.ModeTable);
    }
    /// <summary>
    /// Обновление данных по пользователю
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlUserEnterprise_DataBound(object sender, EventArgs e)
    {
        if (fvTable.CurrentMode == FormViewMode.Edit)
        {
            MenagerProjectEntity mpe = base.cmenager.GetMenagerProject((int)this.idmenagerproject);
            if (mpe == null) return;
            DropDownList ddlUserEnterprise = base.fmc.GetDropDownList(fvTable, "ddlUserEnterprise");
            if (ddlUserEnterprise != null)
            {
                if (mpe.IDUser != null)
                {
                    ddlUserEnterprise.SelectedValue = mpe.IDUser.ToString();
                }
                else
                {
                    ddlUserEnterprise.SelectedIndex = 0;
                }
            }
        }
        //if (fvStepProject.CurrentMode == FormViewMode.Insert)
        //{
        //    DropDownList ddlTemplate = GetDropDownList(fvStepProject, "ddlTemplate");
        //    if (ddlTemplate != null)
        //    {
        //        if (this.insert_idtemplate != -1)
        //        {
        //            ddlTemplate.SelectedValue = this.insert_idtemplate.ToString();
        //            ddlTemplate.Enabled = false;
        //            base.SetEnableButton(fvStepProject, "btInsertTemplate", false);
        //        }
        //        else
        //        {
        //            ddlTemplate.SelectedIndex = -1;
        //            ddlTemplate.Enabled = true;
        //            base.SetEnableButton(fvStepProject, "btInsertTemplate", true);
        //        }
        //    }
        //}
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void fvTable_DataBound(object sender, EventArgs e)
    {

    }
    #endregion

    #region Методы загрузки компонентов и страницы выборка данных
    /// <summary>
    /// Загрузка страницы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            OutFormView(fvTable, this.ModeTable); // если стоит сдесь тогда работает все события кнопок
        }
    }
    #endregion

    #region Методы работы с источником данных
    /// <summary>
    /// Выбрать
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["IDMenagerProject"] = this.idmenagerproject;
        if (DataSelecting != null) DataSelecting(this, e);
    }
    /// <summary>
    /// Выбрано
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataSelected != null) DataSelected(this, e);
    }
    /// <summary>
    /// Добавить
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["IDUser"] = this.save_iduser;
        e.InputParameters["WPhone"] = this.save_wphone;
        e.InputParameters["MPhone"] = this.save_mphone;
        e.InputParameters["SuperMenager"] = this.save_supermenager;      
        e.InputParameters["OutResult"] = this.outinfo;
        if (DataInserting != null) DataInserting(this, e);
    }
    /// <summary>
    /// Добавлено
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataInserted != null) DataInserted(this, e);
    }
    /// <summary>
    /// Править
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["IDMenagerProject"] = this.idmenagerproject;
        e.InputParameters["IDUser"] = this.save_iduser;
        e.InputParameters["WPhone"] = this.save_wphone;
        e.InputParameters["MPhone"] = this.save_mphone;
        e.InputParameters["SuperMenager"] = this.save_supermenager;
        e.InputParameters["OutResult"] = this.outinfo;
        if (DataUpdating != null) DataUpdating(this, e);
    }
    /// <summary>
    /// Исправленно
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataUpdated != null) DataUpdated(this, e);
    }
    /// <summary>
    /// Удалить
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["IDMenagerProject"] = this.idmenagerproject;
        e.InputParameters["OutResult"] = this.outinfo;
        if (DataDeleting != null) DataDeleting(this, e);
    }
    /// <summary>
    /// Удалено
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsTable_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (DataDeleted != null) DataDeleted(this, e);
    }
    /// <summary>
    /// Отмененно
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelect_CancelClick(object sender, EventArgs e)
    {
        if (DataCancelClick != null) DataCancelClick(this, e);
    }
    #endregion

    #region Методы работы panelUpr
    /// <summary>
    /// Настройка панели управления
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pnSelect_Init(object sender, EventArgs e)
    {
        ((panelUpr)sender).Change = this.change_button;
        ((panelUpr)sender).StyleButton = base.style_button;
        ((panelUpr)sender).VisibleInsert = this.visible_insert;
        ((panelUpr)sender).VisibleUpdate = this.visible_update;
        ((panelUpr)sender).VisibleDelete = this.visible_delete;
        ((panelUpr)sender).VisibleSave = this.visible_save;
        ((panelUpr)sender).VisibleCancel = this.visible_cancel;
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsListUserEnterprise_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["type"] = 2;
        e.InputParameters["IDWeb"] = null;
        e.InputParameters["IDSection"] = 0;
        e.InputParameters["UserEnterprise"] = null;
        e.InputParameters["Description"] = null;
    }

}