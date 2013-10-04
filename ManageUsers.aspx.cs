using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace NexusJobs
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        BLmodel BL = new BLmodel();
        Connection CONN = new Connection();
        DataTable dtUsers = new DataTable("Users");
        DataView dv = null;
        string[] strArrUser = null;
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

            string strUser = (string)Session["ssUser"];

            if (!string.IsNullOrEmpty(strUser))
            {
                strArrUser = strUser.Split('&');
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                FillGrid();
            }
            Enable_Controls(false, false, false, false, false, false, false, false, false);
            ErpType_ControlEnable(false, false, false);
            ErpClient_ControlEnable(false, false, false, false, false, false);
        }

        protected void btnAddUsers_Click(object sender, EventArgs e)
        {
            clear_text();
            txtUserName.Focus();
            Session["Action"] = "A";
            Session["Pkey"] = 0; // arivu
            Enable_Controls(true, true, true, true, true, true, true, true, true);
        }
        protected void FillGrid()
        {
            dtUsers = BL.returnDataTable("SELECT PKEY,FIRST_NAME + ' ' + LAST_NAME AS 'USER', ACCESS_RIGHTS AS 'TYPE',PKEY AS 'PKEY',USER_NAMES,PASSWORDS,EMAIL,FIRST_NAME,LAST_NAME, NO_OF_LOG_DAYS,NO_OF_RECORDS_PER_GRID FROM JMC_USERS ORDER BY FIRST_NAME ASC");
            Session["dtUsers"] = dtUsers;
            Load_Grid(dtUsers);
        }

        protected void clear_text()
        {
            txtEmail.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPassword.Attributes.Add("value", "");
            txtSearch.Text = "";
            txtUserName.Text = "";
            ddlAccessType.Text = "Admin";
            ddlLogDays.Text = "15";
            ddlRecordsPerGrid.Text = "10";
            Enable_Controls(false, false, false, false, false, false, false, false, false);
        }
        protected void Enable_Controls(bool blnEmail, bool blnFirstName, bool blnLastName, bool blnPassword, bool blnUserName, bool blnAccessType, bool blnLogDays, bool blnRecordsperGrid, bool blnBtnSave)
        {
            txtEmail.Enabled = blnEmail;
            txtFirstName.Enabled = blnFirstName;
            txtLastName.Enabled = blnLastName;
            txtPassword.Enabled = blnPassword;
            txtUserName.Enabled = blnUserName;
            ddlAccessType.Enabled = blnAccessType;
            ddlLogDays.Enabled = blnLogDays;
            ddlRecordsPerGrid.Enabled = blnRecordsperGrid;
            btnSave.Enabled = blnBtnSave;
        }

        protected void Bindtextbox_Values(string strEmail, string strFirstName, string strLastName, string strPassword, string strUserName, string strAccessType, string strNoofDays, string strRecords)
        {
            txtEmail.Text = strEmail.Trim();
            txtFirstName.Text = strFirstName.Trim();
            txtLastName.Text = strLastName.Trim();
            txtPassword.Text = strPassword.Trim();
            txtPassword.Attributes.Add("value", strPassword.Trim());
            txtUserName.Text = strUserName.Trim();
            ddlAccessType.Text = strAccessType;
            ddlLogDays.Text = strNoofDays;
            ddlRecordsPerGrid.Text = strRecords;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save_user();
        }
        protected bool Save_user()
        {
            bool blnResult = false;

            string stringMessage = string.Empty;


            //if (txtUserName.Text.Trim() != string.Empty & txtPassword.Text.Trim() != string.Empty & IsValidEmailID() & txtFirstName.Text.Trim() != string.Empty & txtLastName.Text.Trim() != string.Empty)
            if (ValidateFields())
            {
                if (!IsExist("Select * from JMC_USERS where User_names='" + txtUserName.Text.Trim() + "' and Pkey <> " + Session["Pkey"])) // arivu
                {
                    string strAction = "";
                    if (Session["Action"] == "A")
                    {
                        CONN.ExecuteNonQuery("INSERT INTO [JMC_USERS]([User_Names],[Passwords],[First_Name],[Last_Name],[No_of_Records_Per_Grid]," +
                                            "[No_of_Log_Days],[Access_Rights],[Email]) VALUES ('" + txtUserName.Text.Trim().Replace("'", "''") + "','" + txtPassword.Text.Trim().Replace("'", "''") + "'," +
                                            "'" + txtFirstName.Text.Trim().Replace("'", "''") + "','" + txtLastName.Text.Trim().Replace("'", "''") + "','" + ddlRecordsPerGrid.SelectedItem.Text.Trim() + "'," +
                                            "'" + ddlLogDays.SelectedItem.Text.Trim() + "','" + ddlAccessType.SelectedItem.Text.Trim() + "','" + txtEmail.Text.Trim().Replace("'", "''") + "')");
                        strAction = "Created";

                    }
                    else if (Session["Action"] == "E")
                    {
                        CONN.ExecuteNonQuery("UPDATE JMC_USERS SET USER_NAMES='" + txtUserName.Text.Trim().Replace("'", "''") + "',PASSWORDS='" + txtPassword.Text.Trim().Replace("'", "''") + "'" +
                                            ",FIRST_NAME='" + txtFirstName.Text.Trim().Replace("'", "''") + "',LAST_NAME='" + txtLastName.Text.Trim().Replace("'", "''") + "'" +
                                            ",NO_OF_RECORDS_PER_GRID=" + ddlRecordsPerGrid.SelectedItem.Text.Trim() + ",NO_OF_LOG_DAYS=" + ddlLogDays.SelectedItem.Text.Trim() +
                                            ", ACCESS_RIGHTS='" + ddlAccessType.SelectedItem.Text.Trim() + "',EMAIL='" + txtEmail.Text.Trim().Replace("'", "''") + "' WHERE PKEY=" + Session["Pkey"]);
                        strAction = "Updated";
                    }
                    RegisterStartupScript("MSG", "<script>alert ('Successfully " + strAction + " the User')</script>;");
                    blnResult = true;
                    //arivu
                    if (txtUserName.Text.ToUpper() == strArrUser[0].ToString().ToUpper())
                    {
                        SetCurrentUserAccessRights(); //arivu
                    }
                    FillGrid();
                    clear_text();
                }
                else
                {
                    Enable_Controls(true, true, true, true, true, true, true, true, true);
                    RegisterStartupScript("MSG", "<script>alert ('This user name " + txtUserName.Text + " already exists!!')</script>;");
                    txtUserName.Focus();
                }

            }

            return blnResult;
        }

        // Fields validation Function
        protected bool ValidateFields()
        {
            if (txtPassword.Text.Trim() == string.Empty & txtUserName.Text.Trim() == string.Empty)
            {
                showMessage("User Name & Password");
                txtUserName.Focus();
                return false;
            }
            else if (txtUserName.Text.Trim() == string.Empty)
            {
                showMessage("User Name");
                txtUserName.Focus();
                return false;
            }
            else if (txtPassword.Text.Trim() == string.Empty)
            {
                showMessage("Password");
                txtPassword.Attributes.Add("value", "");
                txtPassword.Focus();
                return false;
            }
            else if (!IsValidEmailID())
            {
                showMessage(" Valid Email ID");
                txtEmail.Focus();
                return false;
            }
            else if (txtFirstName.Text.Trim() == string.Empty)
            {
                showMessage("First Name");
                txtFirstName.Focus();
                return false;
            }
            else if (txtLastName.Text.Trim() == string.Empty)
            {
                showMessage("Last Name");
                txtLastName.Focus();
                return false;
            }


            else
            {
                return true;
            }

        }

        protected void showMessage(string strMessage)
        {
            Enable_Controls(true, true, true, true, true, true, true, true, true);
            RegisterStartupScript("MSG", "<script>alert ('Please provide " + strMessage + "')</script>;");
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            dtUsers = (DataTable)Session["dtUsers"];
            if (dtUsers.Rows.Count > 0)
            {
                DataTable dt = BL.returnFilteredData(txtSearch.Text.Trim(), "User", dtUsers);
                if (dt.Rows.Count > 0)
                {
                    //Session["dtUsers"] = dt;
                    PageNumber = 0;
                }
                Load_Grid(dt);
            }
        }

        protected void rptUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Session["Action"] = "E";
            Enable_Controls(true, true, true, true, true, true, true, true, true);
            TextBox Pkey = (TextBox)e.Item.FindControl("txtHiddenPkey");

            Session["Pkey"] = Pkey.Text;
            if (e.CommandName.Equals("Delete"))
            {
                if (CONN.ExecuteNonQuery("DELETE FROM JMC_USERS where Pkey=" + Pkey.Text) > 0)
                {
                    PageNumber = 0;
                    FillGrid();
                    Enable_Controls(false, false, false, false, false, false, false, false, false);
                    RegisterStartupScript("MSG", "<script>alert ('Successfully deleted the User')</script>;");
                }
            }
            if (e.CommandName.Equals("lbtnUser") || e.CommandName.Equals("lbtnType"))
            {
                DataTable dt = BL.returnSingleRow(Pkey.Text.Trim(), "Pkey", (DataTable)Session["dtUsers"]);
                Bindtextbox_Values((string)dt.Rows[0]["EMAIL"], (string)dt.Rows[0]["FIRST_NAME"], (string)dt.Rows[0]["LAST_NAME"], (string)dt.Rows[0]["PASSWORDS"], (string)dt.Rows[0]["USER_NAMES"], (string)dt.Rows[0]["TYPE"], Convert.ToString(dt.Rows[0]["NO_OF_LOG_DAYS"]), Convert.ToString(dt.Rows[0]["NO_OF_RECORDS_PER_GRID"]));
            }

        }

        protected void rptPages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            PageNumber = int.Parse(e.CommandName) - 1;
            Load_Grid((DataTable)Session["dtUsers"]);
            //dv = (DataView)Session["dv"];
        }
        private void Load_Grid(DataTable dt)
        {
            int cnt = 0;

            PagedDataSource pgDtSource = new PagedDataSource();
            pgDtSource.DataSource = dt.DefaultView;
            pgDtSource.AllowPaging = true;
            pgDtSource.PageSize = 4;
            pgDtSource.CurrentPageIndex = PageNumber;
            cnt = dt.Rows.Count;
            if (cnt != 0)
            {
                if (pgDtSource.PageCount > 1)
                {
                    rptPages.Visible = true;
                    lblPageValue.Visible = true;
                    ArrayList pages = new ArrayList();
                    for (int i = 0; i < pgDtSource.PageCount; i++)
                        pages.Add((i + 1).ToString());
                    rptPages.DataSource = pages;
                    rptPages.DataBind();
                    int var1 = PageNumber + 1;
                    int var2 = pgDtSource.PageCount;
                    lblPageValue.Text = "Page" + " " + var1.ToString() + " " + "of" + " " + var2.ToString();
                }
                else
                {
                    rptPages.Visible = false;
                    lblPageValue.Visible = false;
                }

                rptUsers.DataSource = pgDtSource;
                rptUsers.DataBind();
                rptUsers.Visible = true;
                lblErrorMessage.Visible = false;
            }
            else
            {
                rptUsers.Visible = false;
                rptPages.Visible = false;
                lblPageValue.Visible = false;
                lblErrorMessage.Visible = true;
            }

        }
        #region CheckDuplication // arivu
        private bool IsExist(string strQuery)
        {
            bool bResult = false;
            DataTable dtCheck = new DataTable();

            dtCheck = BL.returnDataTable(strQuery);

            if (dtCheck.Rows.Count > 0)
            {
                bResult = true;
            }
            else
            {
                bResult = false;
            }
            return bResult;
        }
        #endregion
        private void SetCurrentUserAccessRights() // arivu
        {
            StringBuilder strSB = new StringBuilder();
            string strUser = (string)Session["ssUser"];
            string[] strInfo = strUser.Split('&');
            strInfo[7] = ddlAccessType.Text;
            string strLen = strInfo[0].ToString();
            if (!string.IsNullOrEmpty(strLen))
            {
                foreach (string str in strInfo)
                {
                    strSB.Append(str + "&");
                }

                string strVal = strSB.ToString();

                strVal = strVal.Substring(0, strVal.Length - 1);

                Session["ssUser"] = strVal;
            }


        }
        private bool IsValidEmailID() //arivu
        {
            bool bResult = false;
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

            System.Text.RegularExpressions.Match match = Regex.Match(txtEmail.Text.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                bResult = true;

            return bResult;
        }

        protected void ErpType_ControlEnable(bool blnErpType, bool blnContactInfo, bool blnErpSave)
        {
            txtErpType.Enabled = blnErpType;
            txtContactInfo.Enabled = blnContactInfo;
            btnSaveERP.Enabled = blnErpSave;
        }

        protected void ErpClient_ControlEnable(bool blnErpType, bool blnClientName, bool blnClientContact, bool blnRbWithout, bool blnRbWith, bool blnSaveClient)
        {
            ddlErps.Enabled = blnErpType;
            txtClientName.Enabled = blnClientName;
            txtClientContact.Enabled = blnClientContact;
            rbWithoutJobCost.Enabled = blnRbWithout;
            rbWithJobCost.Enabled = blnRbWith;
            btnSaveClient.Enabled = blnSaveClient;
        }

        protected void btnAddErp_Click(object sender, EventArgs e)
        {
            ErpType_ControlEnable(true, true, true);
            Erps_Control_Clear();
            txtErpType.Focus();
        }

        private bool IsValidErpTypeControls()
        {
            if (txtErpType.Text.Trim() == "")
            {
                RegisterStartupScript("MSG", "<script>alert ('Please provide ERP Type')</script>;");
                ErpType_ControlEnable(true, true,true);
                txtErpType.Focus();
                return false;
            }
            else if (txtContactInfo.Text.Trim() == "")
            {
                RegisterStartupScript("MSG", "<script>alert ('Please provide ERP Contact info')</script>;");
                ErpType_ControlEnable(true, true, true);
                txtContactInfo.Focus();
                return false;
            }
            else
            {
                return true;
            }

        }

        private void Clear_Client_Ctrl()
        {
            ddlErps.Items.Clear();
            txtClientName.Text = "";
            txtClientContact.Text = "";
            txtClientInfo.Text = "";
            rbWithoutJobCost.Checked = true;
        }

        protected void btnAddClient_Click(object sender, EventArgs e)
        {
            ErpClient_ControlEnable(true, true, true, true, true, true);
            Clear_Client_Ctrl();
            FillDropDownList();
            ddlErps.Focus();
        }
        protected void btnSaveERP_Click(object sender, EventArgs e)
        {
            if (IsValidErpTypeControls())
            {
                Save_ERP();
            }

        }

        private void Erps_Control_Clear()
        {
            txtErpType.Text = "";
            txtContactInfo.Text = "";
        }

        private void Save_ERP()
        {
           

            if (!IsExist("Select * from JMC_ClientType where Erps='" + txtErpType.Text.Trim() + "'"))
            {
                try
                {
                    CONN.ExecuteNonQuery("Insert Into JMC_ClientType  ([ERPs], [ContactInfo]) Values('" + txtErpType.Text.Trim() + "','" + txtContactInfo.Text.Trim() + "')");
                    RegisterStartupScript("MSG", "<script>alert ('ERP Type added successfully!')</script>;");
                    Erps_Control_Clear();
                    btnSaveERP.Enabled = false;
                }
                catch(Exception e)
                {
                    RegisterStartupScript("MSG", "<script>alert ('Error:'"+ Convert.ToString(e.Message)+"')</script>;");
                }
            }
            else
            {
                RegisterStartupScript("MSG", "<script>alert ('Erp Type Already Exist, Please provide different')</script>;");
                ErpType_ControlEnable(true, true, true);
                txtErpType.Focus();
            }

        }

        protected void FillDropDownList()
        {
            DataTable dtErps = new DataTable();
            dtErps = BL.returnDataTable("Select Erps from JMC_ClientType Order by Pkey");
            foreach (DataRow drErps in dtErps.Rows)
            {
                ddlErps.Items.Add(Convert.ToString(drErps[0]));
            }
        }


        protected void btnSaveClient_Click(object sender, EventArgs e)
        {
            if (IsValidClientControls())
            {
                Save_Client();
            }
        }
        private bool IsValidClientControls()
        {
            if (ddlErps.Text.Trim() == "")
            {
                ClientCtrlValidatedMsg("ERP Name");
                ErpClient_ControlEnable(true, true, true, true, true, true);
                ddlErps.Focus();
                return false;
            }
            else if (txtClientName.Text.Trim() == "")
            {
                ClientCtrlValidatedMsg("Client Name");
                ErpClient_ControlEnable(true, true, true, true, true, true);
                txtClientName.Focus();
                return false;
            }
            else if (txtClientInfo.Text.Trim() == "")
            {
                ClientCtrlValidatedMsg("Client info.");
                ErpClient_ControlEnable(true, true, true, true, true, true);
                txtClientInfo.Focus();
                return false;
            }
            else if (txtClientContact.Text.Trim() == "")
            {
                ClientCtrlValidatedMsg("Client contact info.");
                ErpClient_ControlEnable(true, true, true, true, true, true);
                txtClientContact.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void ClientCtrlValidatedMsg(string strMessage)
        {
            RegisterStartupScript("MSG", "<script>alert ('Please provide " + strMessage + "')</script>;");
        }
        private void Save_Client()
        {
            string[] strMethods = GetMethodsInArray();

            if (!IsExist("Select * from JMC_CLIENTS where ClientName ='" + txtClientName.Text.Trim() + "'"))
            {
                try
                {
                    int iFkeyErpType = GetFkey("Select Pkey from JMC_ClientType where ERPS = '" + ddlErps.Text.Trim() + "'");
                    CONN.ExecuteNonQuery("Insert Into JMC_Clients  ([Fkey_ClientType] ,[ClientName], [ClientInfo], [ContactInfo],[Isactive]) Values ('" + iFkeyErpType + "','" + txtClientName.Text.Trim() + "','" + txtClientInfo.Text.Trim() + "','" + txtClientContact.Text.Trim() + "','" + "Y" + "')");
                    int iFkeyClient = GetFkey("Select Pkey from JMC_Clients where ClientName ='" + txtClientName.Text.Trim() + "'");
                    for (int iLoop = 0; iLoop < strMethods.Length; iLoop++)
                    {
                        CONN.ExecuteNonQuery("Insert Into JMC_Methods  ([Fkey_Client],[Job_Name],[Integration_ID],[Description]) Values (" + iFkeyClient + ",'" + strMethods[iLoop] + "'," + "1" + ",'" + strMethods[iLoop] + "')");
                    }
                    RegisterStartupScript("MSG", "<script>alert ('Client information created successfully!!')</script>;");
                    btnSaveClient.Enabled = false;
                    Clear_Client_Ctrl();
                }
                catch(Exception e)
                {
                    RegisterStartupScript("MSG", "<script>alert ('Error:'" + Convert.ToString(e.Message) + "')</script>;");
                }
            }
            else
            {
                ErpClient_ControlEnable(true, true, true, true, true, true);
                RegisterStartupScript("MSG", "<script>alert ('Client Name Already Exists, Please provide different')</script>;");
                txtClientName.Focus();
            }
        }

        private int GetFkey(string strQuery)
        {
            DataTable dtFkeyClientType = new DataTable();
            int iPkeyClientType=0;
            dtFkeyClientType = BL.returnDataTable(strQuery);

            if (dtFkeyClientType.Rows.Count > 0)
            {
                foreach (DataRow drPkey in dtFkeyClientType.Rows)
                {
                    iPkeyClientType = Convert.ToInt16(drPkey[0]);
                }
            }

            return iPkeyClientType;
        }
        

        private string[] GetMethodsInArray()
        {
            string[] strArrMethods;
            string strMethods;
            if (rbWithoutJobCost.Checked)
            {
                strMethods = "ImportInvoices,ExportBudgets,ExportActuals,ExportVendorCombos,ExportInvoicePayments";
            }
            else
            {
                strMethods = "ImportInvoices,ExportBudgets,ExportActuals,ExportVendorCombos,ExportInvoicePayments,ExportJobTypes,ExportJobCodes,ExportPhaseCodes,ExportCostCodes";
            }
            strArrMethods = strMethods.Split(',');
            return strArrMethods;
        }

       
}

}
