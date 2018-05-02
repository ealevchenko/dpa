using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Net.Mail;


using NetMailMessage = System.Net.Mail.MailMessage;
using SmtpClient = System.Net.Mail.SmtpClient;
using WebBase;
using System.Web.Configuration;
using System.Text;
using System.Net.Mime;

namespace Web.Email
{
    public class SendEmailUser 
    {
        public string sendTo { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }
    /// <summary>
    /// Тип данных список рассылки
    /// </summary>
    public class DistributionListEmail 
    { 
        public string NameList { get; set; }
        public string EmailList { get; set; }

        public DistributionListEmail(string NameList, string EmailList)
        {
            if (NameList == null) { throw new System.ArgumentException("NameList is not null");}
            if (EmailList == null) { throw new System.ArgumentException("EmailList is not null"); }
            this.NameList = NameList;
            this.EmailList = EmailList;
        }
    }
    
    /// <summary>
    /// Класс отправки почты по SMTP протоколу
    /// </summary>
    public sealed class SMTPHelper
    {
        // возвращает строку ошибки
        private string smtpError;
        public string SMTPError
        {
            get
            {
                return smtpError;
            }
        }
        public bool Send(string sendTo, string subject, string body)
        {
            smtpError = string.Empty;
            // SMTP-конфигурация
            string smtpServetHost =
            ConfigurationManager.AppSettings["SmtpServerHost"];
            int smtpServerPort =
            Convert.ToInt32(ConfigurationManager.
            AppSettings["SmtpServerPort"]);
            bool smtpEnableSsl =
            bool.Parse(ConfigurationManager.
            AppSettings["SmtpEnableSsl"]);
            // конфигурация SMTP пользователя
            string smtpUserName =
            ConfigurationManager.AppSettings["SmtpUserName"];
            string smtpUserPassword =
            ConfigurationManager.AppSettings["SmtpUserPassword"];
            bool smtpUseDefaultCredentials =
            bool.Parse(ConfigurationManager.
            AppSettings["SmtpUseDefaultCredentials"]);
            // "от кого"
            string smtpFromName =
            ConfigurationManager.AppSettings["SmtpFromName"];
            string smtpFromEmail =
            ConfigurationManager.AppSettings["SmtpFromEmail"];
            // создаем SMTP
            SmtpClient smtpClient = new SmtpClient(
            smtpServetHost, smtpServerPort);
            // включить ssl, если нужно
            smtpClient.EnableSsl = smtpEnableSsl;
            // использовать имперсонацию по умолчанию?
            if (smtpUseDefaultCredentials)
            {
                smtpClient.UseDefaultCredentials = true;
            }
            else
            {
                // имперсонация
                smtpClient.UseDefaultCredentials = false;
                if (smtpUserName.Length > 0)
                {
                    System.Net.NetworkCredential cred =
                    new System.Net.NetworkCredential(
                    smtpUserName, smtpUserPassword);
                }
            }
            try
            {
                // создаем e-mail
                NetMailMessage mail =
                CreateMailMessage(smtpFromName, smtpFromEmail,
                sendTo, subject, body);
                // отправляем
                smtpClient.Send(mail);
            }
            catch (Exception e)
            {
                smtpError = e.Message;
                return false;
            }
            return true;
        }
        /// <summary>
        /// Отправить по почте файлы
        /// </summary>
        /// <param name="sendTo"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="listfile"></param>
        /// <returns></returns>
        public bool Send(string sendTo, string subject, string body, string[] listfile)
        {
            smtpError = string.Empty;
            // SMTP-конфигурация
            string smtpServetHost =
            ConfigurationManager.AppSettings["SmtpServerHost"];
            int smtpServerPort =
            Convert.ToInt32(ConfigurationManager.
            AppSettings["SmtpServerPort"]);
            bool smtpEnableSsl =
            bool.Parse(ConfigurationManager.
            AppSettings["SmtpEnableSsl"]);
            // конфигурация SMTP пользователя
            string smtpUserName =
            ConfigurationManager.AppSettings["SmtpUserName"];
            string smtpUserPassword =
            ConfigurationManager.AppSettings["SmtpUserPassword"];
            bool smtpUseDefaultCredentials =
            bool.Parse(ConfigurationManager.
            AppSettings["SmtpUseDefaultCredentials"]);
            // "от кого"
            string smtpFromName =
            ConfigurationManager.AppSettings["SmtpFromName"];
            string smtpFromEmail =
            ConfigurationManager.AppSettings["SmtpFromEmail"];
            // создаем SMTP
            SmtpClient smtpClient = new SmtpClient(
            smtpServetHost, smtpServerPort);
            // включить ssl, если нужно
            smtpClient.EnableSsl = smtpEnableSsl;
            // использовать имперсонацию по умолчанию?
            if (smtpUseDefaultCredentials)
            {
                smtpClient.UseDefaultCredentials = true;
            }
            else
            {
                // имперсонация
                smtpClient.UseDefaultCredentials = false;
                if (smtpUserName.Length > 0)
                {
                    System.Net.NetworkCredential cred =
                    new System.Net.NetworkCredential(
                    smtpUserName, smtpUserPassword);
                }
            }
            try
            {
                // создаем e-mail
                NetMailMessage mail =
                CreateMailMessage(smtpFromName, smtpFromEmail,
                sendTo, subject, body);

                if (listfile != null)
                {
                    foreach (string filename in listfile) { 
                        var attachment = new Attachment(filename, MediaTypeNames.Application.Pdf); // файл (путь)
                        mail.Attachments.Add(attachment);
                    }
                }

                // отправляем
                smtpClient.Send(mail);
            }
            catch (Exception e)
            {
                smtpError = e.Message;
                return false;
            }
            return true;
        }
        /// <summary>
        /// Создание e-mail
        /// </summary>
        private NetMailMessage CreateMailMessage(string fromName,
        string fromEmail, string toEmail,
        string subject, string body)
        {
            NetMailMessage mailMessage = new NetMailMessage();
            mailMessage.From = new
            System.Net.Mail.MailAddress(fromEmail, fromName);
            // если адресатов несколько — создаем список
            foreach (string toAdress in toEmail.Split(';'))
            {
                if (toAdress != "") { mailMessage.To.Add(toAdress); }
            }
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            return mailMessage;
        }
    }
    /// <summary>
    /// Класс работы с почтой на сервере
    /// </summary>
    public class classSMTPWeb : classBaseDB
    {
        #region Поля класса smptWeb
        protected SMTPHelper smtp = new SMTPHelper();
        protected classAccessUsers caccessusers = new classAccessUsers();
        protected classSection csection = new classSection();

        private string _SmtpEmailAdmin = WebConfigurationManager.AppSettings["SmtpEmailAdmin"].ToString();

        #endregion

        #region Конструкторы класса smptWeb
        public classSMTPWeb() : base() { }
        #endregion

        #region Методы smptWeb
        /// <summary>
        /// Отправить сообщение по SMTP протоколу
        /// </summary>
        /// <param name="sendTo"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public bool EmailSend(string sendTo, string subject, string body) 
        {
            if (!smtp.Send(sendTo, subject, body))
            {
                base.log.SaveUsersError("smptWeb", smtp.SMTPError);
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Отправить сообщение по SMTP протоколу c вложеннием
        /// </summary>
        /// <param name="sendTo"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="listfile"></param>
        /// <returns></returns>
        public bool EmailSend(string sendTo, string subject, string body, string[] listfile) 
        {
            if (!smtp.Send(sendTo, subject, body, listfile))
            {
                base.log.SaveUsersError("smptWeb", smtp.SMTPError);
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Отправить сообщение по SMTP протоколу
        /// </summary>
        /// <param name="seu"></param>
        /// <returns></returns>
        public bool EmailSend(SendEmailUser seu) 
        {
            if (seu == null) return false;
            return EmailSend(seu.sendTo, seu.subject, seu.body);
        }
        /// <summary>
        /// Отправить почту администратору сайта
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public bool AdminEmailSend(string subject, string body) 
        {
            return this.EmailSend(this._SmtpEmailAdmin, subject, body);
        }
        /// <summary>
        /// Отправить почту администратору с вложением
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="listfile"></param>
        /// <returns></returns>
        public bool AdminEmailSend(string subject, string body, string[] listfile) 
        {
            return this.EmailSend(this._SmtpEmailAdmin, subject, body, listfile);
        }
        /// <summary>
        /// Отправить сообщение о поступлении\обновлении запроса на доступ к web-серверу
        /// </summary>
        /// <param name="IDAccessUser"></param>
        /// <returns></returns>
        public bool AccessWebUsersEmailSend(int IDAccessUser) 
        {
            string subject = "Поступил запрос на доступ к сайту (There is a request for access to the site)";
            string bodyhead = null;
            StringBuilder body = new StringBuilder("<table><tr><td colspan=2>");
            AccessUsersEntity aue = caccessusers.GetAccessUsers(IDAccessUser);
            if (aue != null)
            {
                if (aue.DateChange != null) { bodyhead = "Запрос на доступ обновлен:"; } else { bodyhead = "Создан новый запрос на доступ:"; }

                body.Append(bodyhead + "</td></tr>");
                body.Append("<tr><td>Пользователь :</td><td>" + aue.UserEnterprise + "</td></tr>");
                body.Append("<tr><td>IDUser :</td><td>" + aue.IDUser.ToString() + "</td></tr>");
                body.Append("<tr><td>Фамилия :</td><td>" + aue.Surname + "</td></tr>");
                body.Append("<tr><td>Имя :</td><td>" + aue.Name + "</td></tr>");
                body.Append("<tr><td>Отчество :</td><td>" + aue.Patronymic + "</td></tr>");
                body.Append("<tr><td>Должность :</td><td>" + aue.Post + "</td></tr>");
                body.Append("<tr><td>Подразделение :</td><td>" + csection.GetCultureSection(aue.IDSection).SectionFull + "</td></tr>");
                body.Append("<tr><td>Ссылка :</td><td>" + @"http://krr-www-parep01.europe.mittalco.com/WebSite/Setup/Users.aspx?Owner=15" + "</td></tr>");
                body.Append("</table>");
                base.log.SaveUsersError(subject, body.ToString());
                return AdminEmailSend(subject, body.ToString());
            }
            return false;
        }
        /// <summary>
        /// Получить перечень списков рассылки
        /// </summary>
        /// <param name="DistributionList"></param>
        /// <returns></returns>
        public List<DistributionListEmail> GetDistributionList(string DistributionList) 
        { 
            if (DistributionList == null) return null;            
            List<DistributionListEmail> ldl = new List<DistributionListEmail>();
            string[] array_DistributionList = DistributionList.Split(',');
            // пройдемся по спискам рассылки
            foreach (string dl in array_DistributionList)
            {
                if (dl != "")
                {
                    string[] array_list = dl.Split(':');
                    if ((array_list[0] != null) & (array_list[1] != null))
                    {
                        ldl.Add(new DistributionListEmail(array_list[0], array_list[1]));
                    }
                }
            }
            return ldl;
        }
        /// <summary>
        /// Вернуть список Email по названию списка рассылки
        /// </summary>
        /// <param name="list"></param>
        /// <param name="NameList"></param>
        /// <returns></returns>
        public string GetListEmail(List<DistributionListEmail> list, string NameList) 
        {
            if (list == null) return null;
            if (NameList == null) return null;
            foreach (DistributionListEmail dl in list) 
            {
                if (dl.NameList == NameList) return dl.EmailList;
            }
            return null;
        }
        #endregion

        #region Методы формирования стуктур таблиц
        /// <summary>
        /// Открыть таблицу
        /// </summary>
        /// <returns></returns>
        public StringBuilder OpenTable() 
        {
            return new StringBuilder("<table>");
        }
        /// <summary>
        /// Закрыть таблицу
        /// </summary>
        /// <param name="sb"></param>
        public void CloseTable(ref StringBuilder sb) 
        {
            sb.Append("</table>");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="caption"></param>
        public void AddCaption(ref StringBuilder sb, string caption) 
        {
            if (caption == null) return;
            sb.Append("<caption>" + caption + "</caption>");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="text"></param>
        public void AddTR(ref StringBuilder sb, string[] text) 
        {
            if (text == null) return;
            
            foreach (string st in text)
            {
                sb.Append("<td>"+st+"</td>");
            }
            sb.Append("</tr>");
        }

        #endregion


    }
}