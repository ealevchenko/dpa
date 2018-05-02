using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Email;

public partial class SMTPEmael : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        SMTPHelper smtp = new SMTPHelper();
        if (!smtp.Send("Eduard.Levchenko@arcelormittal.com", "wwwww", "eeeeeeeeeeeee"))
        {
            lblStatus.Text = smtp.SMTPError;
            lblStatus.Visible = true;
        }
        else
        {
            lblStatus.Text = string.Empty;
            lblStatus.Visible = false;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        classBackup cb = new classBackup();
        cb.CreateScriptInsertGroupUser();
    }
}