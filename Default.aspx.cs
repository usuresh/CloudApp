using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data;
using System.Text;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;



namespace NexusJobs
{
    public partial class _Default : System.Web.UI.Page
    {
        BLmodel bl = new BLmodel();

        Connection dbcon = new Connection();

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Welcome");
            CalDate1.Visible = true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strName = "Suresh";
            string[] arrGetVal = bl.SendUserEmail(strName);

            string strCredential = arrGetVal[0];
            string strEmail = arrGetVal[1];

            MailMessage message = new MailMessage();

            if (!string.IsNullOrEmpty(strCredential) && !string.IsNullOrEmpty(strEmail))
            {

                message.To.Add(strEmail);
                message.Subject = "JellyBeans - Credential Recover";
                message.From = new MailAddress("JellyBeans@Support.com");
                message.Body = " Hi " + strName + " here is your password :<b> " + strCredential + " </b> for JellyBeans. Please Do not reply to this email";
                SmtpClient smtp = new SmtpClient("ntmail");
                smtp.Send(message);
            }
        }

        protected void lnkbtnExportExcel_Click(object sender, EventArgs e)
        {
            string strval = "select * from LOGDETAILS_VIEW";
            DataTable dtTable = dbcon.getDatatable(strval);
            ExportToExcel(dtTable);

        }


        public static void ExportToSpreadsheet(DataTable table, string name)
        {
            DataGrid dgGrid = new DataGrid();

            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            foreach (DataColumn column in table.Columns)
            {
                context.Response.Write(column.ColumnName + ";");
            }
            context.Response.Write(Environment.NewLine);
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    context.Response.Write(row[i].ToString().Replace(";", string.Empty) + ";");
                }
                context.Response.Write(Environment.NewLine);
            }
            context.Response.ContentType = "text/csv";
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + name + ".csv");
            context.Response.End();
        }


        public void ExportToExcel(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                string filename = "Test1234.xls";
                string excelHeader = "Quiz Report";

                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();

                // Report Header
                hw.WriteLine("<b><u><font size=’3′> " + excelHeader + " </font></u></b>");

                //Get the HTML for the control.
                dgGrid.RenderControl(hw);

                //Write the HTML back to the browser.
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                this.EnableViewState = false;
                Response.Write(tw.ToString());
                Response.End();
            }
        }

        protected void CalDate1_SelectionChanged(object sender, EventArgs e)
        {
            txtCalDate1.Text = CalDate1.SelectedDate.ToString();
            CalDate1.Visible = false;
        }
    }
}