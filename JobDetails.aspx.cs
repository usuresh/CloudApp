using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;



namespace NexusJobs
{
    public partial class JobDetails : System.Web.UI.Page
    {

        string strValues = null;
        Connection myCon = new Connection();
        BLmodel BL = new BLmodel();
        DataTable dtERPTable = new DataTable();
        DataTable dtClientList = new DataTable();
        DataTable dtAllClientList = new DataTable(); // arivu
        DataTable dtJobList = new DataTable();
        DataTable dtLogTable = new DataTable();
        DataTable dtLoadGrid = new DataTable();
        DataTable dtExportExcel = new DataTable();
        DataView dv = null;
        StringBuilder strValPkey = new StringBuilder();
        string[] strArrUser = null;
        string strFilter = null;
        string ViewQuery = null;
        string FinalQuery = null;
        string strval = null;
        string cstext1 = null;
        string LoadGridQuery = null;
        string strExcelQuery = null;
        string strERPsClient = null;
        string strClient = null;
        int msgflag = 0;
        int iPageMax = 0;
        int intRowsPerPage = 10;     //arivu
        public int PageNumber
        {
            get
            {
                if (ViewState["PageNumber"] != null)
                    return Convert.ToInt32(ViewState["PageNumber"]);
                else
                    return 0;
            }
            set
            {
                ViewState["PageNumber"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.AppendHeader("referesh", "60");

            string strUser = (string)Session["ssUser"];

            if (!string.IsNullOrEmpty(strUser))
            {
                strArrUser = strUser.Split('&');
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

            strERPsClient = (string)Session["ssERPClients"];
            strClient = (string)Session["ssClients"];

            if (!string.IsNullOrEmpty(strERPsClient))
            {
                ViewQuery = "select * from VIEW_LOGDETAILS where ERPS = '" + strERPsClient + "' order by Pkey desc";
            }
            else if (!string.IsNullOrEmpty(strClient))
            {
                ViewQuery = "select * from VIEW_LOGDETAILS where clientname='" + strClient + "' order by Pkey desc";
            }
            else
            {
                ViewQuery = "select * from VIEW_LOGDETAILS order by Pkey desc";
            }

            dtERPTable.Columns.Add("Pkey1");
            dtERPTable.Columns.Add("ERPs1");
            dtERPTable.Columns.Add("ContactInfo1");
            dtAllClientList = BL.returnDataTable("select * from JMC_CLIENTS");
            dtERPTable = BL.returnDataTable("select * from JMC_CLIENTTYPE");
            if (string.IsNullOrEmpty(strERPsClient))
            {
                strERPsClient = GetERPClient(strClient);
            }
            dtClientList = BL.returnDataTable("select * from JMC_CLIENTS where Fkey_ClientType in (Select Pkey from JMC_CLIENTTYPE where ERPs='" + strERPsClient + "')"); //arivu
            dtJobList = BL.returnDataTable("select * from JMC_METHODS");

            dgRptTable.Visible = true;

            if (!IsPostBack)
            {
                fillComboClientType(dtERPTable);
                fillComboClientName(dtClientList);
                fillComboMethods(dtJobList);
                fillComboStatus();
                fillComboIntegrationId();

                try
                {
                    dtLogTable = BL.returnDataTable(ViewQuery);
                    dtLoadGrid = BL.returnDataTable(ViewQuery);
                    if (dtLoadGrid.Rows.Count > 0)
                    {
                        LoadLogDetails(dtLoadGrid);
                    }
                    else
                    {
                        dgRptTable.Visible = false;
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            ddlClientName.Font.Name = "Verdana";
            ddlClientName.Font.Size = 8;
            ddlClientName.Height = 20;

            ddlClientType.Font.Name = "verdana";
            ddlClientType.Font.Size = 8;
            ddlClientType.Height = 20;

            ddlMethods.Font.Name = "verdana";
            ddlMethods.Font.Size = 8;
            ddlMethods.Height = 20;

            ddlStatus.Font.Name = "verdana";
            ddlStatus.Font.Size = 8;
            ddlStatus.Height = 20;

            ddlIntergrationId.Font.Name = "verdana";
            ddlIntergrationId.Font.Size = 8;
            ddlIntergrationId.Height = 20;

            Session["Date1"] = idCalendar.Value;
            Session["Date2"] = idCalendar1.Value;
            string strFromDate = idCalendar.Value;
            string strToDate = idCalendar1.Value;
        }

        private void LoadLogDetails(DataTable dt)
        {
            int cnt = 0;

            PagedDataSource pgDtSource = new PagedDataSource();
            pgDtSource.DataSource = dt.DefaultView;
            pgDtSource.AllowPaging = true;
            pgDtSource.PageSize = intRowsPerPage;
            pgDtSource.CurrentPageIndex = PageNumber;
            cnt = dt.Rows.Count;

            if (cnt != 0)
            {
                if (pgDtSource.PageCount > 1)
                {
                    PgTable.Visible = true;
                    Pagevaluelbl.Visible = true;
                    ArrayList pages = new ArrayList();
                    for (int i = 0; i < pgDtSource.PageCount; i++)
                        pages.Add((i + 1).ToString());
                    PgTable.DataSource = pages;
                    PgTable.DataBind();
                    int var1 = PageNumber + 1;
                    int var2 = pgDtSource.PageCount;
                    Pagevaluelbl.Text = "Page" + " " + var1.ToString() + " " + "of" + " " + var2.ToString();
                }
                else
                {
                    PgTable.Visible = false;
                    Pagevaluelbl.Visible = false;
                }

                dgRptTable.DataSource = pgDtSource;
                dgRptTable.DataBind();
                dgRptTable.Visible = true;
                fsTable.Visible = true;
            }
            else
            {
                dgRptTable.Visible = false;
                PgTable.Visible = false;
                Pagevaluelbl.Visible = false;
                fsTable.Visible = false;
            }

        }

        private void fillComboClientType(DataTable dt)
        {
            BL.FillCombo(ddlClientType, dt, "ERPs", "ERPs");
            ddlClientType.Items.Insert(0, "--Select--");
            /*arivu*/
            if (!string.IsNullOrEmpty(strERPsClient))
            {
                ddlClientType.SelectedValue = strERPsClient;
            }
            else
            {
                ddlClientType.SelectedValue = "--Select--";
            }
            /**/

        }

        private void fillComboClientName(DataTable dt)
        {
            BL.FillCombo(ddlClientName, dt, "ClientName", "ClientName");
            ddlClientName.Items.Insert(0, "--Select--");
            /*arivu*/
            if (!string.IsNullOrEmpty(strClient))
            {
                ddlClientName.SelectedValue = (strClient);
            }
            else
            {
                ddlClientName.SelectedValue = ("--Select--");
            }
            /**/
        }

        private void fillComboMethods(DataTable dt)
        {
            var distinctRows = (from DataRow dRow in dt.Rows select dRow["Job_Name"]).Distinct();
            ddlMethods.Items.Clear();                       // arivu
            foreach (var ddl in distinctRows)
            {
                ddlMethods.Items.Add(ddl.ToString());
            }
            ddlMethods.Items.Insert(0, "--Select--");
            ddlMethods.SelectedValue = ("--Select--");
        }
        private void fillComboStatus()
        {
            ddlStatus.Items.Insert(0, "--Select--");
            ddlStatus.Items.Add("Success");
            ddlStatus.Items.Add("Failed");
            ddlStatus.Items.Add("Running");
        }
        private void fillComboIntegrationId()
        {
            ddlIntergrationId.Items.Insert(0, "--Select--");
            ddlIntergrationId.Items.Add("1");
            ddlIntergrationId.Items.Add("2");
        }
        protected void ClientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtClientName = new DataTable();
            string ddlValue = ddlClientType.SelectedValue.ToString();
            var myList = from pk in dtERPTable.AsEnumerable() where pk.Field<string>("ERPs") == ddlValue select pk.Field<int>("Pkey");
            string str = null;
            foreach (var ddl in myList)
                str = ddl.ToString();

            if (str != null && str != "" && str != "--Select--")
            {
                int intlr = dtAllClientList.Select("Fkey_ClientType = " + str + "").Length;
                if (intlr != 0)
                {
                    dtClientName = dtAllClientList.Select("Fkey_ClientType = '" + str + "'").CopyToDataTable();
                    BL.FillCombo(ddlClientName, dtClientName, "ClientName", "ClientName");
                    ddlClientName.Items.Insert(0, "--Select--");
                    ddlClientName.SelectedValue = "--Select--";
                }
                else
                {
                    ddlClientName.Items.Clear();
                    ddlClientName.SelectedValue = "--Select--";
                }
            }
            else
            {
                ddlClientName.Items.Clear();
                ddlClientName.Items.Insert(0, "--Select--");
            }
        }

        protected void ClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ddlValue = ddlClientName.SelectedValue.ToString();
            DataTable dtClientMethod = new DataTable();
            var myList = from DataRow pk in dtAllClientList.Rows where pk.Field<string>("ClientName") == ddlValue select pk.Field<int>("Pkey");

            string Str = null;
            foreach (var ddl in myList)
            {
                Str = ddl.ToString();
            }
            if (Str != null && Str != "" && Str != "--Select--")
            {
                int inttr = dtJobList.Select("Fkey_Client = " + Str + "").Length;
                if (inttr != 0)
                {
                    dtClientMethod = dtJobList.Select("Fkey_Client = " + Str).CopyToDataTable();
                    ddlMethods.Items.Clear(); //arivu   
                    BL.FillCombo(ddlMethods, dtClientMethod, "Job_Name", "Job_Name");
                    ddlMethods.Items.Insert(0, "--Select--");
                    ddlMethods.SelectedValue = "--Select--";
                }
                else
                {
                    ddlMethods.Items.Clear();                 //arivu  
                    ddlMethods.Items.Insert(0, "--Select--");
                }
            }
            else
            {
                ddlMethods.Items.Clear();                   //arivu
                ddlMethods.Items.Insert(0, "--Select--");
            }
        }

        #region GetFilterCondition

        private string GetFilterCondition()
        {
            try
            {
                StringBuilder sbFilter = new StringBuilder();

                if (ddlClientType.SelectedValue != "--Select--")
                {
                    sbFilter.Append("ERPs " + " LIKE '");
                    sbFilter.Append(ddlClientType.SelectedValue.Trim());
                    sbFilter.Append("%' AND ");
                }

                if (ddlClientName.SelectedValue != "--Select--")
                {
                    sbFilter.Append("ClientName " + " LIKE '");
                    sbFilter.Append(ddlClientName.SelectedValue.Trim());
                    sbFilter.Append("%' AND ");
                }

                if (ddlMethods.SelectedValue != "--Select--")
                {
                    sbFilter.Append("Job_Name " + " LIKE '");
                    sbFilter.Append(ddlMethods.SelectedValue.Trim());
                    sbFilter.Append("%' AND ");
                }

                if (ddlStatus.SelectedValue != "--Select--")
                {
                    sbFilter.Append("Method_Status " + " LIKE '");
                    sbFilter.Append(ddlStatus.SelectedValue.Trim());
                    sbFilter.Append("%' AND ");
                }

                if (ddlIntergrationId.SelectedValue != "--Select--")
                {
                    sbFilter.Append("Integration_ID " + "= '");
                    sbFilter.Append(ddlIntergrationId.SelectedValue.Trim());
                    sbFilter.Append("' AND ");
                }

                if (!string.IsNullOrEmpty(idCalendar.Value) || !string.IsNullOrEmpty(idCalendar1.Value))
                {
                    if (!string.IsNullOrEmpty(idCalendar.Value))
                    {
                        if (!string.IsNullOrEmpty(idCalendar1.Value))
                        {
                            if (Convert.ToDateTime(idCalendar.Value) < Convert.ToDateTime(idCalendar1.Value))
                            {
                                sbFilter.Append("LastRunDate " + ">= '");
                                sbFilter.Append(idCalendar.Value);
                                sbFilter.Append("' AND ");
                                sbFilter.Append("LastRunDate " + "<= '");
                                sbFilter.Append(idCalendar1.Value);
                                sbFilter.Append("' AND ");
                            }
                            else
                            {
                                msgflag = 1;
                                string strScript = "<script language=JavaScript>alert('To date should be greater than From date') </script>";
                                Page.RegisterStartupScript("AlertMsg", strScript);
                                return string.Empty;
                            }

                        }
                        else
                        {
                            msgflag = 1;
                            string strScript = "<script language=JavaScript>alert('Please enter To Date') </script>";
                            Page.RegisterStartupScript("AlertMsg", strScript);
                        }
                    }
                    else
                    {
                        msgflag = 1;
                        string strScript = "<script language=JavaScript>alert('Please enter From Date') </script>";
                        Page.RegisterStartupScript("AlertMsg", strScript);
                    }
                }


                strFilter = sbFilter.ToString();

                if (strFilter.EndsWith(" AND "))
                {
                    if (strFilter.Length - 4 > 0)
                    {
                        strFilter = strFilter.Substring(0, strFilter.Length - 4);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strFilter;

        }
        #endregion

        protected void btShowDetails_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem rep in dgRptTable.Items)
            {
                CheckBox chkJobs = (CheckBox)rep.FindControl("chkJobids");
                chkJobs.Checked = false;
            }
            ViewQuery = "select * from VIEW_LOGDETAILS order by Pkey desc";
            dtLogTable = BL.returnDataTable(ViewQuery);

            string getQuery = GetFilterCondition();

            if (!string.IsNullOrEmpty(getQuery))
            {
                int inttr = dtLogTable.Select(GetFilterCondition(), "ClientName").Length;
                if (inttr > 0)
                {
                    dtLogTable = dtLogTable.Select(GetFilterCondition(), "ClientName").CopyToDataTable();
                    int inttr1 = dtLogTable.Rows.Count;

                    if (inttr1 != 0)
                    {
                        LoadLogDetails(dtLogTable);
                    }
                    else
                    {
                        dgRptTable.DataSource = null;
                        dgRptTable.DataBind();
                        PgTable.Visible = false;
                        Pagevaluelbl.Visible = false;
                        fsTable.Visible = false;
                    }
                }
                else
                {
                    dgRptTable.DataSource = null;
                    dgRptTable.DataBind();
                    PgTable.Visible = false;
                    Pagevaluelbl.Visible = false;
                    fsTable.Visible = false;
                }
            }
            else
            {
                if (msgflag == 0)
                {
                    string csname1 = "Set filter Condition";
                    string cstext1 = "<script type=\"text/javascript\">" +
               "alert('Select any filter values');</" + "script>";
                    RegisterStartupScript(csname1, cstext1);
                }
            }
        }

        protected void dgRptTable_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //TextBox txtbob = (TextBox)e.Item.FindControl("txtGetDgPkey");
            TextBox txtbob = (TextBox)e.Item.FindControl("txtGetFkeyMethods");
            TextBox txtERPs = (TextBox)e.Item.FindControl("txtERPType");
            TextBox txtJobName1 = (TextBox)e.Item.FindControl("txtDGJobName");
            LinkButton lnkClientName = (LinkButton)e.Item.FindControl("lnkClientName");
            LinkButton lnkJobStatus = (LinkButton)e.Item.FindControl("lnkJobStatus");
            
            string strClientID = txtbob.Text;
            string StrClientName = lnkClientName.Text;
            string strJobStatus = lnkJobStatus.Text;
            string strClientType = txtERPs.Text;

            if (e.CommandName.Equals("CommJobStatus"))
            {
                Session["ssScreenType"] = 1;
                Session["ssClientId"] = txtbob.Text;
                Session["ssClientName"] = lnkClientName.Text;
                Session["ssJobStatus"] = lnkJobStatus.Text;
                Session["ssClientType"] = txtERPs.Text;
                Response.Redirect("JobStatus.aspx");
            }

            if (e.CommandName.Equals("CommClientName"))
            {
                Session["ssScreenType"] = 1;
                Session["ssClientId"] = txtbob.Text;
                Session["ssClientName"] = lnkClientName.Text;
                Session["ssJobStatus"] = lnkJobStatus.Text;
                Session["ssClientType"] = txtERPs.Text;
                Response.Redirect("ClientJobs.aspx");
            }
           
        }


        protected void dgRptTable_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            int iRow = 1;
            foreach (RepeaterItem rp in dgRptTable.Items)
            {
                LinkButton lnkValues = (LinkButton)rp.FindControl("lnkJobStatus");
                Label lblSerial = (Label)rp.FindControl("lblSerial");

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
                lblSerial.Text = Convert.ToString((PageNumber * intRowsPerPage) + iRow);  // arivu
                iRow += 1;                                                                // arivu  
            }
        }

        protected void PgTable_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
           
            dtLogTable = BL.returnDataTable("select * from VIEW_LOGDETAILS order by Pkey desc");
            foreach (RepeaterItem rep in dgRptTable.Items)
            {
                CheckBox chkJobs = new CheckBox();
                chkJobs = (CheckBox)rep.FindControl("chkJobids");

                if (chkJobs.Checked == true)
                {
                    TextBox txtpkey = (TextBox)rep.FindControl("txtGetDgPkey");
                    strValPkey.Append(txtpkey.Text + ",");
                }
            }

            strval = strValPkey.ToString();

            if ((string)Session["ssStrVal"] != null)
            {
                Session["ssStrVal"] = strval + (string)Session["ssStrVal"];
            }
            else
            {
                Session["ssStrVal"] = strval;
            }

            strval = (string)Session["ssStrVal"];
            string[] strCheckedItems = strval.Split(',');

            PageNumber = int.Parse(e.CommandName) - 1;
            dv = (DataView)Session["dv"];

            dtLogTable = dtLogTable.Select(GetFilterCondition(), "ClientName").CopyToDataTable();
            LoadLogDetails(dtLogTable);

            if (strCheckedItems.Length > 0)
            {
                foreach (RepeaterItem rep in dgRptTable.Items)
                {
                    CheckBox chkJobs = (CheckBox)rep.FindControl("chkJobids");
                    TextBox txtpkey = (TextBox)rep.FindControl("txtGetDgPkey");

                    foreach (string str in strCheckedItems)
                    {
                        if (txtpkey.Text == str)
                        {
                            chkJobs.Checked = true;
                        }
                    }
                }
            }
        }
        protected void btClear_Click(object sender, EventArgs e)
        {
            fillComboClientType(dtERPTable);
            fillComboClientName(dtClientList);
            ddlClientName.Items.Clear();
            ddlClientName.Items.Insert(0, "--Select--"); //arivu
            fillComboMethods(dtJobList);
            ddlClientType.SelectedIndex = 0;
            ddlClientName.SelectedIndex = 0;
            ddlMethods.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            ddlIntergrationId.SelectedIndex = 0;
            idCalendar.Value = null;
            idCalendar1.Value = null;
            dgRptTable.DataSource = null;
            dgRptTable.DataBind();
            PgTable.DataSource = null;
            PgTable.DataBind();
            Pagevaluelbl.Text = ""; //arivu
            PgTable.Visible = false; //arivu
        }

        protected void lnkPrevious_Click1(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        protected void imgbtHome_Click1(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        protected void btnAddToDashboard_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem rep in dgRptTable.Items)
            {
                CheckBox chkJobs = (CheckBox)rep.FindControl("chkJobids");

                if (chkJobs.Checked == true)
                {
                    TextBox txtpkey = (TextBox)rep.FindControl("txtGetDgPkey");
                    strValPkey.Append(txtpkey.Text + ",");
                }
            }

            string SsKeyValue = (string)Session["ssStrVal"];
          
            if (!string.IsNullOrEmpty(SsKeyValue))
            {
                strValPkey.Append(SsKeyValue);
            }

            if (strValPkey.Length > 0)
            {
                strval = strValPkey.ToString();
                strval = strval.Substring(0, strval.Length - 1);
                DataTable dtMethods = BL.returnDataTable("Select Distinct FKey_Methods from JMC_LogDetails where PKey in (" + strval + ")");
                List<string> lst = new List<string>();
                foreach (DataRow dtRow in dtMethods.Rows)
                {
                    lst.Add( Convert.ToString(dtRow[0]));
                }
                //string[] str = strval.Split(',');         //arivu 
                //List<string> lst = new List<string>(str); //arivu
                string strMethodIds = BL.GetDashboardMethodIds(strArrUser[0].ToString()); //arivu
                if (!string.IsNullOrEmpty(strMethodIds))
                {
                    string[] strArrMethodIds = strMethodIds.Split(',');
                    foreach (string st in strArrMethodIds)
                    {
                        if (!lst.Contains(st))
                        {
                            lst.Add(st);
                        }
                    }
                }
                var lst1 = lst.Distinct().ToList();

                if (lst.Count <= 10)
                {
                    StringBuilder bd = new StringBuilder();

                    foreach (var ls in lst1)
                    {
                        bd.Append(ls + ',');
                    }

                    strval = bd.ToString();
                    strval = strval.Substring(0, strval.Length - 1);

                    BL.updateTable("update jmc_users set Priority_methods = '" + strval + "' where User_Names = '" + strArrUser[0].ToString() + "'");


                    foreach (RepeaterItem rep in dgRptTable.Items)
                    {
                        CheckBox chkJobs = (CheckBox)rep.FindControl("chkJobids");
                        chkJobs.Checked = false;
                    }

                    cstext1 = "<script type=\"text/javascript\">" +
               "alert('Added to Dashboard');</" + "script>";
                    RegisterStartupScript("Val", cstext1);
                }
                else
                {
                    cstext1 = "<script type=\"text/javascript\">" +
               "alert('Cannot add more than 10 items in the Dashboard');</" + "script>";
                    RegisterStartupScript("Val", cstext1);
                }
            }
            else
            {
                cstext1 = "<script type=\"text/javascript\">" +
           "alert('Select from the list');</" + "script>";
                RegisterStartupScript("Val", cstext1);
            }
        }

        protected void imgbtLogout_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void lnkExportExcel_Click(object sender, EventArgs e)
        {

            if (dgRptTable.Items.Count <= 0)
            {
                RegisterStartupScript("MSG", "<script>alert ('No Records to Export!')</script>;"); //arivu
                return;
            }

            foreach (RepeaterItem rep in dgRptTable.Items)
            {
                CheckBox chkJobs = (CheckBox)rep.FindControl("chkJobids");

                if (chkJobs.Checked == true)
                {
                    TextBox txtpkey = (TextBox)rep.FindControl("txtGetFkeyMethods");
                    strValPkey.Append(txtpkey.Text + ",");
                }
            }

            string SsKeyValue = (string)Session["ssStrVal"];

            if (!string.IsNullOrEmpty(SsKeyValue))
            {
                strValPkey.Append(SsKeyValue);
            }

            if (strValPkey.Length > 0)
            {
                strval = strValPkey.ToString();
                strval = strval.Substring(0, strval.Length - 1);
                string[] str = strval.Split(',');
                List<string> lst = new List<string>(str);

                var lst1 = lst.Distinct().ToList();
                StringBuilder bd = new StringBuilder();

                foreach (var ls in lst1)
                {
                    bd.Append(ls + ',');
                }

                strval = bd.ToString();
                strval = strval.Substring(0, strval.Length - 1);
                strExcelQuery = "select ClientName as 'Client Name',ERPS as 'Client Type', integration_id as 'Integration ID',Job_Name as 'Job Name',Method_Status as 'Job Status',Lastrundate,NextRunDate,Comments from VIEW_LOGDETAILS where pkey in (" + strval + ") order by Pkey desc ";

                dtExportExcel = BL.returnDataTable(strExcelQuery);
                ExportToExcel(dtExportExcel);
            }

            else
            {
                if (!string.IsNullOrEmpty(strERPsClient))
                {
                    strExcelQuery = "select ClientName as 'Client Name',ERPS as 'Client Type', integration_id as 'Integration ID',Job_Name as 'Job Name',Method_Status as 'Job Status',Lastrundate,NextRunDate,Comments from VIEW_LOGDETAILS where ERPS = '" + strERPsClient + "' order by Pkey desc";
                }
                else if (!string.IsNullOrEmpty(strClient))
                {
                    strExcelQuery = "select ClientName as 'Client Name',ERPS as 'Client Type', integration_id as 'Integration ID',Job_Name as 'Job Name',Method_Status as 'Job Status',Lastrundate,NextRunDate,Comments from VIEW_LOGDETAILS where clientname='" + strClient + "' order by Pkey desc";
                }
                else
                {
                    strExcelQuery = "select ClientName as 'Client Name',ERPS as 'Client Type', integration_id as 'Integration ID',Job_Name as 'Job Name',Method_Status as 'Job Status',Lastrundate,NextRunDate,Comments from VIEW_LOGDETAILS order by Pkey desc";
                }

                dtExportExcel = BL.returnDataTable(strExcelQuery);
                ExportToExcel(dtExportExcel);
            }


        }

        public void ExportToExcel(DataTable dt)
        {
            /*arivu declared vars start*/
            string strClientType = CheckDDLValues(ddlClientType, "CLIENT TYPE:");
            string strClientName = CheckDDLValues(ddlClientName, "CLIENT NAME:");
            string strMethods = CheckDDLValues(ddlMethods, "METHODS:");
            string strFromDate = "FROM DATE:" + Convert.ToString(idCalendar.Value);
            string strToDate = "FROM DATE:" + Convert.ToString(idCalendar.Value);
            string strStatus = CheckDDLValues(ddlStatus, "STATUS:");
            string strIntegID = CheckDDLValues(ddlIntergrationId, "INTEGRATION ID:");
            /*End*/
            if (dt.Rows.Count > 0)
            {

                string filename = "LogDetails.xls";
                string excelHeader = "Report generated by :" + strArrUser[0].ToString() + " ";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();
                dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightBlue;
                dgGrid.HeaderStyle.Font.Bold = true;
                dgGrid.GridLines = GridLines.Both;
                /*Arivu*/
                //Report Header Informations                
                // Report Header
                hw.WriteLine("<table>");
                hw.WriteLine("<tr><td><b><u><font size=’3′>" + excelHeader.ToUpper() + "</font></u></b></td><td><b><u><font size=’3′>" + "" + "</font></u></b></td><td><b><u><font size=’3′>" + "" + "</font></u></b></td></tr>");
                hw.WriteLine("<tr><td><b><u><font size=’3′>" + strClientType.ToUpper() + "</font></u></b></td><td><b><u><font size=’3′>" + strClientName.ToUpper() + "</font></u></b></td><td><b><u><font size=’3′>" + strMethods.ToUpper() + "</font></u></b></td></tr>");
                hw.WriteLine("<tr><td><b><u><font size=’3′>" + strFromDate.ToUpper() + "</font></u></b></td><td><b><u><font size=’3′>" + strToDate.ToUpper() + "</font></u></b></td><td><b><u><font size=’3′>" + strStatus.ToUpper() + "</font></u></b></td></tr>");
                hw.WriteLine("<tr><td><b><u><font size=’3′>" + strIntegID.ToUpper() + "</font></u></b></td><td><b><u><font size=’3′>" + "" + "</font></u></b></td><td><b><u><font size=’3′>" + "" + "</font></u></b></td></tr>");
                hw.WriteLine("</table>");
                /**/
                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                //this.EnableViewState = false;
                Response.Write(tw.ToString());
                Response.End();

            }

        }
        #region CheckDDLValues  //arivu
        protected string CheckDDLValues(DropDownList ddl, string lableName)
        {
            string Result = string.Empty;
            if (ddl.Text == "--Select--")
            {
                Result = lableName + "       ";
            }
            else
            {
                Result = lableName + ddl.Text.ToUpper();
            }
            return Result;
        }
        #endregion
        #region ShowContent //arivu
        protected string ShowContent(object o)
        {
            int iStringSize = 50;
            string content = string.Empty;
            if (o.ToString().Length > iStringSize)
            {
                content = Regex.Replace(o.ToString(), "<(.|\n)*?>", string.Empty).Replace("&nbsp;", string.Empty);
                if (content.Length >= iStringSize)
                {
                    content = content.Substring(0, iStringSize);
                    //content = content.Substring(0, content.LastIndexOf(" ")); //removes last word
                }
            }
            else
            {
                content = o.ToString();
            }
            return content;
        }
        #endregion
        #region MakeLink //arivu
        protected string MakeLink(object o, int iRowId)
        {
            string content = o.ToString();
            string url = string.Empty;
            if (content.Length > 50)
            {
                if (!string.IsNullOrEmpty(content))
                {
                    url = "<a  id=\"various" + iRowId + "\" href=\"#inline" + iRowId + "\"  >Readmore</a>";
                }
                else
                {
                    url = string.Empty;
                }
            }

            return url;
        }
        #endregion
        #region CreateHiddenField //arivu
        protected string CreateHiddenField(object o, int RowID)
        {
            string content = string.Empty;
            string value = string.Empty;

            if (!string.IsNullOrEmpty(o.ToString()))
            {
                content = Regex.Replace(o.ToString(), "'", string.Empty).Replace("&nbsp;", string.Empty);
            }

            if (o.ToString().Length > 50)
            {
                value = "<div style=\"display: none;\">";
                value += "<div id=\"inline" + RowID + "\" style=\"margin: 5px;width:600px;height:200px;overflow:auto;font-family: Verdana, Geneva, sans-serif; font-size: 12px;\">";
                value += content;
                value += "</div>";
            }
            return value;
        }
        #endregion
        #region Makescript //arivu
        protected string MakeScript(int RowId)
        {
            string Script = string.Empty;

            Script = "<script type=\"text/javascript\">";
            Script += "$(document).ready(function() {";
            Script += "$(\"#various" + RowId + "\").fancybox({";
            Script += "	'titlePosition'		: 'inside',";
            Script += "	'transitionIn'		: 'none',";
            Script += "	'transitionOut'		: 'none'";
            Script += "});";
            Script += "});";
            Script += "</script>";
            return Script;
        }
        #endregion
        #region GetERPClient //arivu
        private string GetERPClient(string strClient) //arivu
        {
            string ddlValue = strClient;
            var myList = from pk in dtAllClientList.AsEnumerable() where pk.Field<string>("ClientName") == ddlValue select pk.Field<int>("Fkey_ClientType");
            int fkey = 0;
            string str = string.Empty;
            foreach (var ddl in myList)
                fkey = Convert.ToInt16(ddl.ToString());

            if (fkey > 0)
            {
                var sTypeName = from erpType in dtERPTable.AsEnumerable() where erpType.Field<int>("Pkey") == fkey select erpType.Field<string>("ERPs");
                foreach (var Erps in sTypeName)
                    str = Erps.ToString();
            }

            return str;
        }
        #endregion


        protected void IntervalTimer_Tick(object sender, EventArgs e)
        {

        }
}
}