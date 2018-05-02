using Hangfire;
using Strategic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Email;
using WebBase;
using WebReports;
//using leaTR;

public partial class Test : BasePages
{
    //private classDBJobs dbj = new classDBJobs(0);

    protected override void OnInit(EventArgs e)
    { 
    
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //dbj.SaveRunJob(50, DateTime.Now, DateTime.Now, 0, "Test");
        //dbj.SaveRunJob(50, DateTime.Now, DateTime.Now, 0, HttpContext.Current.User.Identity.Name);
        if (!IsPostBack)
        {
            if (Request.IsAuthenticated)
            {
                // Отобразить общую информацию о пользователе. 
                //lbllnfo.Text = "<b>Name: </b>" + User.Identity.Name;
                //lbllnfo.Text += "<br><b>Authenticated With: </b>";
                //lbllnfo.Text += User.Identity.AuthenticationType;

                //lbllnfo.Text = "<b>Name: </b>" + User.Identity.Name;
                //WindowsIdentity identity = (WindowsIdentity)User.Identity;
                //lbllnfo.Text += "<br><b>Token: </b>";
                //lbllnfo.Text += identity.Token.ToString();
                //lbllnfo.Text += "<br><b>Guest? </b>";
                //lbllnfo.Text += identity.IsGuest.ToString();
                //lbllnfo.Text += "<br><b>System? </b>";
                //lbllnfo.Text += identity.IsSystem.ToString();
                //lbllnfo.Text += "<br><b>Groups </b>";
                //lbllnfo.Text += identity.Groups.ToString();
            }

            ////if (User is WindowsPrincipal)
            ////{
            ////    // Прежде всего, получить общую информацию о пользователе. 
            ////    WindowsPrincipal principal = (WindowsPrincipal)User;
            ////    // 

            ////    WindowsIdentity identity = (WindowsIdentity)principal.Identity;
            ////    // ... 
            ////    // Теперь получить роли пользователя 
            ////    lbllnfo.Text += "<hr/>";
            ////    lbllnfo.Text += "<h2>Roles:</h2>";
            ////    foreach (IdentityReference SIDRef in identity.Groups)
            ////    {
            ////        lbllnfo.Text += "<br/> ";
            ////        // Получить системный код SID 
            ////        SecurityIdentifier sid =
            ////        (SecurityIdentifier)SIDRef.Translate(
            ////        typeof(SecurityIdentifier));
            ////        lbllnfo.Text += "<br><b>SID (code): </bx/br>";
            ////        lbllnfo.Text += sid.Value;
            ////        // Получить читабельный для человека SID. 
            ////        NTAccount account = (NTAccount)SIDRef.Translate(typeof(NTAccount));
            ////        lbllnfo.Text += "<br><b>SID (human-readable): </bx/br>";
            ////        lbllnfo.Text += account.Value;
            ////    }
            ////}

            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            //string d = Thread.CurrentThread.CurrentCulture.ToString();
            //TableControl1.DataBind();
            //UprControl1.StyleButton = ControlPanel.styleButton.link;
            //this.DataBind();
//             ControlUserGroup.InsertUser = new UserDetali(0, null, @"europe\ealev", "тест1", "Eduard.Levchenko@arcelormittal.com", true, "Лев", "Эдло", "Алек", "Нач", 3);

            //controlAR.SetAccessRules("Section:2-1;3-2;8-0,Rules1:1-1;2-2");
            //RulesCollection rc = new RulesCollection(typeRules.Section);
            //rc.AddRules(new RulesDetali(2, Access.Change));
            //rc.AddRules(new RulesDetali(3, Access.View));
            //rc.AddRules(new RulesDetali(8, Access.Change));
            //ControlRules.SetRules(rc);
            //controlAccess.SetRules("Section:2-1;3-2;8-0;,Rules1:1-3;2-1;,");
        }
        //ControlUser.DataBind();
        //ControlUser1.DataBind();

  //      // Create the Command and the Connection.
  //      string connectionString =
  //WebConfigurationManager.ConnectionStrings["aspserver"].ConnectionString;

  //      SqlConnection con = new SqlConnection(connectionString);
  //      string sql = "SELECT * FROM [KRR-PA-REP-SBF].[dbo].[Users]";
  //      SqlCommand cmd = new SqlCommand(sql, con);

  //      // Open the Connection and get the DataReader.
  //      con.Open();
  //      SqlDataReader reader = cmd.ExecuteReader();

  //      // Cycle through the records, and build the HTML string.
  //      StringBuilder htmlStr = new StringBuilder("");
  //      while (reader.Read())
  //      {
  //          htmlStr.Append("<li>");
  //          htmlStr.Append(reader["UserEnterprise"]);
  //          htmlStr.Append("</li>");
  //      }
  //      // Close the DataReader and the Connection.
  //      reader.Close();
  //      con.Close();

  //      // Show the generated HTML code on the page.
  //      HtmlContent.Text = htmlStr.ToString();
    }
    //protected void ControlRules_DateChangeRules(object sender, controlRules.RulesChangeEventArgs e)
    //{
    //    string s = e.RulesCollection.ToString();
    //}

    //protected void ControlUserGroup_Init(object sender, EventArgs e)
    //{
    //    //((controlUserGroup)sender).InsertUser = new UserDetali(0, null, @"europe\ealev", "тест1", "Eduard.Levchenko@arcelormittal.com", true, "Лев", "Эдло", "Алек", "Нач", 3);
    //    //((controlUserGroup)sender).InsertUser = new UserDetali(0, 1, "группа доступа", "тест2", null, false, null, null, null, null, 3);
    //}
    //protected void uploadButton_Click(object sender, EventArgs e)
    //{
    //    // Если файла нет, то и загружать нечего
    //    if (!fileUpload.HasFile)
    //        return;
    //    // Вызываем метод бизнес-логики
    //    classFiles cfiles = new classFiles();
    //    cfiles.AddFile(
    //    new FileContent()
    //    {
    //        // Имя файла
    //        FileName = fileUpload.PostedFile.FileName,
    //        // Тип файла
    //        FileContent = fileUpload.PostedFile.ContentType,
    //        // Данные
    //        FileImage = fileUpload.FileBytes
    //    }
    //    );
    //}

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    classFiles cfiles = new classFiles();
    //    FileContent fc = cfiles.GetFile(2);

    //    // Выводим в поток
    //    Response.Clear();
    //    Response.ContentType = fc.FileContent;
    //    Response.AppendHeader("Content-Length", fc.FileImage.Length.ToString());
    //    Response.BinaryWrite(fc.FileImage);
    //    // Сбрасываем весь буфер
    //    //Response.Flush();
    //    Response.End();
    //}

    protected void Button1_Click(object sender, EventArgs e)
    {
        //classThreadAccessUser cl = new classThreadAccessUser();
        //cl.AccessUser();
        //clAggregates n = new clAggregates();
        //n.test();

    }
    protected void UprControl1_InsertClick(object sender, EventArgs e)
    {

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        classPDFReports pdfrep = new classPDFReports();
        classSMTPWeb smtp = new classSMTPWeb();
        classProject cp = new classProject();

        //string html = cp.HtmlStausProject(cp.GetProgramProject(null, 1), CultureInfo.GetCultureInfo("en-US"), "ntst").ToString();
        string path = Path.GetTempPath()+"scalesProgram"+DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")+".pdf";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        //pdfrep.CreatePDFDocument(path, html, iTextSharp.text.PageSize.A4.Rotate());   new int[] { 1, 3, 5, 16, 15 }
        pdfrep.CreatePDFDocumentStatusProgramm(path, CultureInfo.GetCultureInfo("en-US"), cp.GetProgramProject(new int[] { 17, 2, 16, 3 }, implementationProgram.Scales, CultureInfo.GetCultureInfo("en-US")), "тест");
        //smtp.AdminEmailSend("test PDF", "Отправка тестового файла", new string[] { path });
        WriteToResponse(path);
        File.Delete(path);

    }

    public void WriteToResponse(string fileName)
    {
        // Очищаем старое содержимое и заголовки
        Response.ClearContent();
        Response.ClearHeaders();
        // Создаем новые заголовки
        Response.AddHeader("Content-Disposition",
        "inline;filename='test.pdf'");
        Response.ContentType = "application/pdf";
        // Выводим
        Response.WriteFile(fileName);
        Response.Flush();
        Response.Clear();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        RecurringJob.Trigger("task-StepProject");
    }



    protected void InputPeriodDateTime1_DateTimeChange1(object sender, leaControls.InputPeriodDateTime.selectDateTimeEventArgs e)
    {

    }
    protected void InputPeriodDateTime2_DateTimeChange(object sender, leaControls.InputPeriodDateTime.selectDateTimeEventArgs e)
    {

    }
}