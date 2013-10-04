using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data; //arivu
using System.Globalization;//arivu



namespace NexusJobs
{
    public partial class HeaderPage : System.Web.UI.MasterPage
    {
        string pageval = null;
        string[] strArrUser = null;
        string val1 = null;
        string UserName = string.Empty; //arivu
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ssUser"] != null)
            {

                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
                Response.Cache.SetNoStore();
                pageval = this.Page.Page.ToString();
                string strUser = (string)Session["ssUser"];

                if (!string.IsNullOrEmpty(strUser))
                {

                    strArrUser = strUser.Split('&');
                    UserName = strArrUser[0].ToString(); //arivu
                    val1 = strArrUser[7].ToString();
                }

                lbUserLogin.Text = PropCase(Convert.ToString(strArrUser[3]) + " " + PropCase(Convert.ToString(strArrUser[2])));
                lnkAdmin.Visible = false;

                /*Arivu*/
                if (val1.ToUpper() == "ADMIN")
                {
                    lnkAdmin.Visible = true;
                }
                /**/

                switch (pageval)
                {
                    case "ASP.dashboard_aspx":
                        lnkPrevious.Visible = false;

                        if (val1.ToUpper() == "ADMIN")
                        {
                            lnkAdmin.Visible = true;
                        }
                        break;

                    case "ASP.JobDetails_aspx":
                        lnkPrevious.Visible = true;
                        break;

                    case "ASP.ClientJobs_aspx":
                        lnkPrevious.Visible = true;
                        break;

                    case "ASP.JobStatus_aspx":
                        lnkPrevious.Visible = true;
                        break;
                }

            }

            else
            {

                Response.Redirect("Login.aspx");

            }

        }
        //Returns init cap for given string.
        public static string PropCase(string strText)
        {
            return new CultureInfo("en").TextInfo.ToTitleCase(strText.ToLower());
        }
 
        protected void imgbtLogout_Click(object sender, ImageClickEventArgs e)
        {
            Session["ssUser"] = null;
            Response.Redirect("Login.aspx");
        }

        protected void imgbtHome_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            pageval = this.Page.Page.ToString();

            switch (pageval)
            {
                case "ASP.jobdetails_aspx":
                    Session["ssERPClients"] = null;
                    Session["ssClients"] = null;
                    Response.Redirect("Dashboard.aspx");
                    break;

                case "ASP.clientjobs_aspx":
                    Response.Redirect("JobDetails.aspx");
                    break;

                case "ASP.jobstatus_aspx":
                    Response.Redirect("JobDetails.aspx");
                    break;
                case "ASP.manageusers_aspx":
                    Response.Redirect("Dashboard.aspx");
                    break;
            }
        }

        protected void lnkAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageUsers.aspx");
        }



    }
}