<%@ Page Title="" Language="C#" MasterPageFile="~/WebSite/Setup/Setup.master" AutoEventWireup="true" CodeFile="Backup.aspx.cs" Inherits="WebSite_Setup_Backup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/backup.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="List">
        <caption>Сохранение данных</caption>
        <tr>
            <th class="title">Конструктор скрипта</th><th class="path">Путь сохранения скрипта</th><th class="button">Выполнить</th>
        </tr>
        <tr><th colspan="3"><b>namespace</b> WebBase</th></tr>
        <tr>
            <td class="title">Перечень Web сайтов</td>
            <td class="path"></td>
            <td class="button">
                <asp:Button ID="btWeb" runat="server" Text="Создать" OnClick="btWeb_Click" OnClientClick="<%$ Resources:Resource, MessageLock %>" Enabled='<%# base.Change %>' /></td>
        </tr>
        <tr>
            <td class="title">Перечень Web-страниц</td>
            <td class="path"></td>
            <td class="button">
                <asp:Button ID="btListSite" runat="server" Text="Создать" OnClick="btListSite_Click" OnClientClick="<%$ Resources:Resource, MessageLock %>" Enabled='<%# base.Change %>' /></td>
        </tr>
        <tr>
            <td class="title">Список пользователей и групп доступа</td>
            <td class="path"></td>
            <td class="button">
                <asp:Button ID="btUsersAndGroups" runat="server" Text="Создать" OnClick="btUsersAndGroups_Click" OnClientClick="<%$ Resources:Resource, MessageLock %>" Enabled='<%# base.Change %>' /></td>
        </tr>
        <tr>
            <td class="title">Карта сайтов и доступ к сайтам</td>
            <td class="path"></td>
            <td class="button">
                <asp:Button ID="btSiteMapAndAccess" runat="server" Text="Создать" OnClick="btSiteMapAndAccess_Click" OnClientClick="<%$ Resources:Resource, MessageLock %>" Enabled='<%# base.Change %>' /></td>
        </tr>
        <tr>
            <td class="title">Структурные подразделения</td>
            <td class="path"></td>
            <td class="button">
                <asp:Button ID="btSection" runat="server" Text="Создать" OnClick="btSection_Click" OnClientClick="<%$ Resources:Resource, MessageLock %>" Enabled='<%# base.Change %>' /></td>
        </tr>
        <tr>
            <td class="title">Фоновые задачи</td>
            <td class="path"></td>
            <td class="button">
                <asp:Button ID="btHangFireListJobs" runat="server" Text="Создать" OnClientClick="<%$ Resources:Resource, MessageLock %>" Enabled='<%# base.Change %>' OnClick="btHangFireListJobs_Click" /></td>
        </tr>
        <tr><th colspan="3"><b>namespace</b> Strategic</th></tr>
        <tr>
            <td class="title">Шаблоны внедрения проектов</td>
            <td class="path"></td>
            <td class="button">
                <asp:Button ID="btTemplateProject" runat="server" Text="Создать" OnClick="btTemplateProject_Click" OnClientClick="<%$ Resources:Resource, MessageLock %>" Enabled='<%# base.Change %>'/></td>
        </tr>
        <tr>
            <td class="title">Типы проектов</td>
            <td class="path"></td>
            <td class="button">
                <asp:Button ID="btTypeProject" runat="server" Text="Создать" OnClick="btTypeProject_Click" OnClientClick="<%$ Resources:Resource, MessageLock %>" Enabled='<%# base.Change %>'/></td>
        </tr>
        <tr>
            <td class="title">Менеджер проектов</td>
            <td class="path"></td>
            <td class="button">
                <asp:Button ID="btMenagerProject" runat="server" Text="Создать" OnClick="btMenagerProject_Click" OnClientClick="<%$ Resources:Resource, MessageLock %>" Enabled='<%# base.Change %>'/></td>
        </tr>
        <tr>
            <td class="title">Список всех проектов и шагов выполнения</td>
            <td class="path"></td>
            <td class="button">
                <asp:Button ID="btProject" runat="server" Text="Создать" OnClick="btProject_Click" OnClientClick="<%$ Resources:Resource, MessageLock %>" Enabled='<%# base.Change %>'/></td>
        </tr>
        <tr>
            <td class="title">Список всех KPI проектов</td>
            <td class="path"></td>
            <td class="button">
                <asp:Button ID="btKPI" runat="server" Text="Создать" OnClick="btKPI_Click" OnClientClick="<%$ Resources:Resource, MessageLock %>" Enabled='<%# base.Change %>'/></td>
        </tr>
        <tr>
            <td class="title">Нормативная документация</td>
            <td class="path"></td>
            <td class="button">
                <asp:Button ID="btOrder" runat="server" Text="Создать" OnClick="btOrder_Click" OnClientClick="<%$ Resources:Resource, MessageLock %>" Enabled='<%# base.Change %>'/></td>
        </tr>
    </table>

    <table class="List">
        <caption>Перенос данных</caption>
        <tr>
            <th class="title">Конструктор скрипта</th><th class="path">Путь сохранения скрипта</th><th class="button">Выполнить</th>

        </tr>
        <tr>
            <td class="title">Перенос списка проектов</td>
            <td class="path"></td>
            <td class="button">
                <asp:Button ID="btMovingProject" runat="server" Text="Создать" OnClick="btMovingProject_Click" OnClientClick="<%$ Resources:Resource, MessageLock %>" Enabled='<%# base.Change %>'/></td>
        </tr>
    </table>
</asp:Content>

