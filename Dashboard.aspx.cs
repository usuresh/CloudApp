using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NexusJobs;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Drawing.Design;


namespace NexusJobs
{
    public partial class Dashboard : System.Web.UI.Page
    {
        DataTable dtTable = new DataTable();
        DataTable dtLogdetails = new DataTable();
        BLmodel BL = new BLmodel();
        string logIds = null;
        string LogDetails = null;
        string[] strArrUser = null;
        string val1 = null; string val2 = null; string val3 = null;
        string cstext1 = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUser = (string)Session["ssUser"];

            if (!string.IsNullOrEmpty(strUser))
            {
                strArrUser = strUser.Split('&');
                val1 = strArrUser[0].ToString();
                val2 = strArrUser[2].ToString();
                val3 = strArrUser[3].ToString();
            }
            else
            {
                cstext1 = "<script type=\"text/javascript\">" +
               "alert('Select job from the list');</" + "script>";
                RegisterStartupScript("MSG", cstext1);
                Response.Redirect("Login.aspx");
            }

            
            if (!IsPostBack)
            {
                logIds = BL.GetUserMethodIDs(strArrUser[0].ToString());
                if (!string.IsNullOrEmpty(logIds))
                {
                    LogDetails = "select * from View_DashboardLogDetails where fKey_Methods in (" + logIds + ") order by ERPs asc ";
                    dtLogdetails = BL.returnDataTable(LogDetails);
                    LoadLogDetails(dtLogdetails);
                }
                else
                {
                    rptLogDeails.Visible = false;
                    imgbtnRemovejobs.Visible = false;
                }
            }

          //  PopulateGridItems();

            string query = "SELECT * FROM View_ERP_DETAILS ORDER BY ERPS";

            dtTable = BL.returnDataTable(query);
            LoadERPDetails(dtTable);


            if (dtTable.Rows.Count > 0)
            {
                PieCharting(dtTable);
                StackedColumnchart(dtTable);
            }
        }

          private void PopulateGridItems()
        {
            logIds = BL.GetUserMethodIDs(strArrUser[0].ToString());
            if (!string.IsNullOrEmpty(logIds))
            {
                LogDetails = "select * from View_DashboardLogDetails where fKey_Methods in (" + logIds + ") order by ERPs asc ";
                dtLogdetails = BL.returnDataTable(LogDetails);
                LoadLogDetails(dtLogdetails);
            }
            else
            {
                rptLogDeails.Visible = false;
                imgbtnRemovejobs.Visible = false;
            }
        }

        private void PieCharting(DataTable dtTABLE)
        {
            ClientChart.DataSource = dtTable;
            ClientChart.Series["Series1"].XValueMember = "ERPs";
            ClientChart.Series["Series1"].YValueMembers = "TOTAL_COUNT";
            ClientChart.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            ClientChart.DataBind();
            ClientChart.Legends[0].Enabled = true;

        }

        // Populating Stacked column chart.
        private void StackedColumnchart(DataTable dtTABLE)
        {
            StackedChart.DataSource = dtTable;
            StackedChart.Series["Total"].XValueMember = "ERPs";
            StackedChart.Series["Total"].YValueMembers = "TOTAL_COUNT";

            StackedChart.Series["Active"].XValueMember = "ERPs";
            StackedChart.Series["Active"].YValueMembers = "ACTIVE_COUNT";

            StackedChart.Series["InActive"].XValueMember = "ERPs";
            StackedChart.Series["InActive"].YValueMembers = "INACTIVE_COUNT";

            StackedChart.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
            StackedChart.DataBind();

            foreach (System.Web.UI.DataVisualization.Charting.Series series in StackedChart.Series)
            {
                series.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
            }

            Color[] MyCustomeColorPalette = new Color[3]{Color.FromKnownColor(KnownColor.DarkOliveGreen),
            Color.FromKnownColor(KnownColor.Firebrick),Color.FromKnownColor(KnownColor.DarkGoldenrod)};

            this.StackedChart.Palette = System.Web.UI.DataVisualization.Charting.ChartColorPalette.None;
            StackedChart.PaletteCustomColors = MyCustomeColorPalette;
            StackedChart.Legends[0].Enabled = true;
        }

        //Populating ERP Details.
        private void LoadERPDetails(DataTable dtTable)
        {
            if (dtTable.Rows.Count > 0)
            {
                rptTable.DataSource = dtTable;
                rptTable.DataBind();
            }
        }

        //Populating Log Details.
        protected void LoadLogDetails(DataTable dtTable)
        {
            if (dtTable.Rows.Count > 0)
            {
                rptLogDeails.DataSource = dtTable;
                rptLogDeails.DataBind();

            }

        }
        protected void lnkHere_click(object sender, EventArgs e)
        {
            Session["GetUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect("JobDetails.aspx");
        }

        protected void rptTable_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LinkButton lnkClientErps = (LinkButton)e.Item.FindControl("lnkClientName");
            Session["ssERPClients"] = lnkClientErps.Text;
            Response.Redirect("JobDetails.aspx");

        }

        protected void rptLogDeails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LinkButton lnkClients = (LinkButton)e.Item.FindControl("lnkClientName");
            Session["ssClients"] = lnkClients.Text;
            Response.Redirect("JobDetails.aspx");
        }

        protected void imgbtnRemovejobs_Click(object sender, ImageClickEventArgs e)
        {
            Hashtable hsTable = new Hashtable();

            StringBuilder strbd = new StringBuilder();

            foreach (RepeaterItem rpt in rptLogDeails.Items)
            {
                CheckBox chkjobs = (CheckBox)rpt.FindControl("chkJobsId1s");
                if (chkjobs.Checked == true)
                {
                    //TextBox tbjobs = (TextBox)rpt.FindControl("txtGetDgPkey");
                    TextBox tbjobs = (TextBox)rpt.FindControl("txtGetFkeyMethod");

                    strbd.Append(tbjobs.Text + ",");
                }
            }

            if (strbd.Length > 0)
            {
                string strval = strbd.ToString();
                strval = strval.Substring(0, strval.Length - 1);
                logIds = BL.GetUserMethodIDs(strArrUser[0].ToString());
                string[] logMethodIds = strval.Split(',');
                string[] logUserMthIds = logIds.Split(',');

                strbd.Length = 0;

                List<string> ls1 = new List<string>(logUserMthIds);

                foreach (string MthIds in logMethodIds)
                {
                    ls1.Remove(MthIds);
                }

                logMethodIds = ls1.ToArray();

                foreach (string MthIds in logMethodIds)
                {
                    strbd.Append(MthIds + ',');
                }

                strval = strbd.ToString();
                if (strval.Length != 0)
                {
                    strval = strval.Substring(0, strval.Length - 1);
                }

                BL.UpdateUserMethodId("update jmc_users set Priority_methods = '" + strval + "' where User_Names = '" + val1 + "'");

                if (strval.Length != 0)
                {
                    LogDetails = "select * from View_DashboardLogDetails where fKey_Methods in (" + strval + ") order by ERPs asc ";
                    dtLogdetails = BL.returnDataTable(LogDetails);
                    LoadLogDetails(dtLogdetails);
                }
                else
                {
                    rptLogDeails.Visible = false;
                    imgbtnRemovejobs.Visible = false;
                }
            }
            else
            {
                cstext1 = "<script type=\"text/javascript\">" +
               "alert('Select job from the list');</" + "script>";
                RegisterStartupScript("MSG", cstext1);
            }
        }

        protected void rptLogDeails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            foreach (RepeaterItem rp in rptLogDeails.Items)
            {
                Label lnkValues = (Label)rp.FindControl("lbJobStatus");

                if (lnkValues.Text == "Running")
                {
                    lnkValues.ForeColor = System.Drawing.Color.Orange;
                }
                else if (lnkValues.Text == "Success")
                {
                    lnkValues.ForeColor = System.Drawing.Color.Green;
                }
                else if (lnkValues.Text == "Failed")
                {
                    lnkValues.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void IntervalTimer_Tick(object sender, EventArgs e)
        {

        }
        protected void IntervalTimer_Tick1(object sender, EventArgs e)
        {

        }
}
}