using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;


namespace NexusJobs
{
    public class BLmodel
    {
        Connection dbcon = new Connection();
        public DataTable returnDataTable(string Query)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = dbcon.getDatatable(Query);
            return dt;
        }

        public void FillCombo(DropDownList cmbObject, DataTable dtObject, string strValueColumn, string strTextColumn)
        {
            dbcon.FillCombo(cmbObject, dtObject, strValueColumn, strTextColumn);
        }

        public void UpdateUserMethodId(string query)
        {
            dbcon.UpdateUserLogDetails(query);
        }

        public string[] SendUserEmail(string Username)
        {
            string[] arrVal = dbcon.GetUserCredentials(Username);
            return arrVal;
        }

        public void updateTable(string Query)
        {
            dbcon.ExecuteNonQuery(Query);
        }
        public DataTable returnFilteredData(string strValue, string strColumn, DataTable dtInput)
        {
            DataTable dtReturn = dtInput.Clone();
            string strFilter = string.Empty;
            if (strValue.Trim() != string.Empty && strColumn.Trim() != string.Empty)
            {
                strFilter = "[" + strColumn.Trim() + "] LIKE'%" + strValue.Trim() + "%'";
            }
            DataRow[] drReturn = dtInput.Select(strFilter);
            if (drReturn.Length > 0)
            {
                foreach (DataRow dr in drReturn)
                {
                    dtReturn.ImportRow(dr);
                }
            }
            return dtReturn;
        }

        public DataTable returnSingleRow(string strValue, string strColumn, DataTable dtInput)
        {
            DataTable dtReturn = dtInput.Clone();
            string strFilter = string.Empty;
            if (strValue.Trim() != string.Empty && strColumn.Trim() != string.Empty)
            {
                strFilter = "[" + strColumn.Trim() + "] =" + strValue.Trim();
            }
            DataRow[] drReturn = dtInput.Select(strFilter);
            if (drReturn.Length > 0)
            {
                foreach (DataRow dr in drReturn)
                {
                    dtReturn.ImportRow(dr);
                }

            }

            return dtReturn;

        }

        public string GetDashboardMethodIds(string UserName)
        {
            string strVal = dbcon.GetMethodIDs(UserName);

            return strVal;
        }

        public string[] GetUserAuthentication(string UserName, string Password)
        {
            string[] arrStr = new string[9];

            arrStr = dbcon.GetAuthentication(UserName, Password);

            return arrStr;
        }

        public string GetUserMethodIDs(string UserName)
        {
            string StrMethodIds = null;
            StrMethodIds = dbcon.GetMethodIDs(UserName);
            return StrMethodIds;
        }







    }



}