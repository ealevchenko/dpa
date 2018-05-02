using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_Setup_Backup : BaseAccessPages
{
    protected classBackup bck = new classBackup();

    protected void Page_Load(object sender, EventArgs e)
    {
        // Первый запуск
        if (!IsPostBack)
        {
            ((WebSite_Setup_Setup)Master).SetHeader(base.GetStringBaseResource("titleMenuAdmin"), base.GetStringBaseResource("menuBackup"));
        }
    }
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btTemplateProject_Click(object sender, EventArgs e)
    {
        bck.OutResultText(bck.CreateScriptTemplateProject(), "Ошибка создания скрипта 'Шаблонов внедрения проектов'", "Скрипт 'Шаблонов внедрения проектов' - создан.");
    }
    protected void btTypeProject_Click(object sender, EventArgs e)
    {
        bck.OutResultText(bck.CreateScriptTypeProject(), "Ошибка создания скрипта 'Типы проектов'", "Скрипт 'Типы проектов' - создан.");
    }
    protected void btMenagerProject_Click(object sender, EventArgs e)
    {
        bck.OutResultText(bck.CreateScriptMenagerProject(), "Ошибка создания скрипта 'Список менеджеров проектов'", "Скрипт 'Список менеджеров проектов' - создан.");
    }
    protected void btProject_Click(object sender, EventArgs e)
    {
        bck.OutResultText(bck.CreateScriptProject(), "Ошибка создания скрипта 'Проекты ДАТП'", "Скрипт 'Проекты ДАТП' - создан.");
    }
    protected void btKPI_Click(object sender, EventArgs e)
    {
        bck.OutResultText(bck.CreateScriptKPIProject(), "Ошибка создания скрипта 'KPI Проектов'", "Скрипт 'KPI Проектов' - создан.");
    }
    protected void btOrder_Click(object sender, EventArgs e)
    {
        bck.OutResultText(bck.CreateScriptListOrder(), "Ошибка создания скрипта 'Нормативная документация'", "Скрипт 'Нормативная документация' - создан.");
    }
    protected void btSection_Click(object sender, EventArgs e)
    {
        bck.OutResultText(bck.CreateScriptSection(), "Ошибка создания скрипта 'Структурные подразделения'", "Скрипт 'Структурные подразделения' - создан.");
    }
    protected void btWeb_Click(object sender, EventArgs e)
    {
        bck.OutResultText(bck.CreateScriptWeb(), "Ошибка создания скрипта 'Список Web-сайтов'", "Скрипт 'Список Web-сайтов' - создан.");
    }
    protected void btListSite_Click(object sender, EventArgs e)
    {
        bck.OutResultText(bck.CreateScriptListSite(), "Ошибка создания скрипта 'Перечень Web-страниц'", "Скрипт 'Перечень Web-страниц' - создан.");
    }
    protected void btUsersAndGroups_Click(object sender, EventArgs e)
    {
        bck.OutResultText(bck.CreateScriptUsersAndGroups(), "Ошибка создания скрипта 'Список пользователей и групп доступа'", "Скрипт 'Список пользователей и групп доступа' - создан.");
    }
    protected void btSiteMapAndAccess_Click(object sender, EventArgs e)
    {
        bck.OutResultText(bck.CreateScriptSiteMapAndAccess(), "Ошибка создания скрипта 'Карта сайтов и доступ к сайтам'", "Скрипт 'Карта сайтов и доступ к сайтам' - создан.");
    }

    protected void btHangFireListJobs_Click(object sender, EventArgs e)
    {
        bck.OutResultText(bck.CreateScriptHangFireListJobs(), "Ошибка создания скрипта 'Список фоновых задач HangFireListJobs'", "Скрипт 'Список фоновых задач HangFireListJobs' - создан.");
    }
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    protected void btMovingProject_Click(object sender, EventArgs e)
    {
        bck.OutResultText(bck.CreateScriptInsertOldProject(), "Ошибка создания скрипта 'Перенос данных о проектах'", "Скрипт 'Перенос данных о проектах' - создан.");
    }
}