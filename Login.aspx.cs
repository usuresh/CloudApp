using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net.Mail;



namespace NexusJobs
{
    public partial class Login : System.Web.UI.Page
    {

        string cstext1 = null;
        BLmodel bl = new BLmodel();



        protected void Page_Load(object sender, EventArgs e)
        {
            txtUserName.BackColor = System.Drawing.Color.LightYellow;
            txtPassword.BackColor = System.Drawing.Color.LightYellow;
            txtUserName.Focus();
        }

        protected void imgLogin_Click(object sender, ImageClickEventArgs e)
        {
            StringBuilder strSB = new StringBuilder();

            if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text))
            {
                string[] strArry = bl.GetUserAuthentication(txtUserName.Text, txtPassword.Text);
                string strLen = strArry[0].ToString();

                if (!string.IsNullOrEmpty(strLen))
                {
                    foreach (string str in strArry)
                    {
                        strSB.Append(str + "&");
                    }

                    string strVal = strSB.ToString();

                    strVal = strVal.Substring(0, strVal.Length - 1);
                    Session["ssUser"] = strVal;
                    Response.Redirect("Dashboard.aspx");
                }
                else
                {
                    cstext1 = "<script type=\"text/javascript\">" +
                    "alert('invalid Username/Password');</" + "script>";
                    RegisterStartupScript("MSG", cstext1);
                    txtUserName.Focus(); //arivu
                }
            }
            else
            {
                cstext1 = "<script type=\"text/javascript\">" +
                "alert('Enter Username/Password');</" + "script>";
                RegisterStartupScript("MSG", cstext1);
                txtUserName.Focus();//arivu
            }
        }

        protected void imgForgotPwd_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserName.Text))
            {
                string strName = txtUserName.Text;
                string[] arrGetVal = bl.SendUserEmail(strName);

                string strCredential = arrGetVal[0];
                string strEmail = arrGetVal[1];

                MailMessage message = new MailMessage();

                if (!string.IsNullOrEmpty(strCredential) && !string.IsNullOrEmpty(strEmail))
                {
                    message.To.Add(strEmail);
                    message.Subject = "JellyBeans - Credential Recover";
                    message.From = new MailAddress("JellyBeans@JBSupport.com");
                    message.Body = " Hi " + strName + " here is your password :<b> " + strCredential + " </b> for JellyBeans. Please Do not reply to this email";
                    SmtpClient smtp = new SmtpClient("ntmail");
                    smtp.Send(message);

                    cstext1 = "<script type=\"text/javascript\">" +
                    "alert('Mail as been sent to your registered email address');</" + "script>";
                    RegisterStartupScript("MSG", cstext1);

                }

                else
                {
                    {
                        cstext1 = "<script type=\"text/javascript\">" +
                        "alert('Please enter correct username');</" + "script>";
                        RegisterStartupScript("MSG", cstext1);
                    }

                }
            }
            else
            {
                {
                    cstext1 = "<script type=\"text/javascript\">" +
                    "alert('Enter your Username');</" + "script>";
                     RegisterStartupScript("MSG", cstext1);
                }
            }
        }
    }
}