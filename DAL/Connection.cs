using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using System.Security;

namespace NexusJobs
{
    public class Connection
    {
        string strValues = ConfigurationManager.ConnectionStrings["JellyBeansConString"].ConnectionString;

        SqlConnection con;
        SqlCommand com = null;
        SqlDataAdapter sqlDA = null;
        public void openConnection()
        {            
            try
            {                
                con = new SqlConnection(strValues);
                con.Open();                
            }
            catch (Exception exp)
            {                
                throw exp;
            }            
        }

        public DataTable getDatatable(string Query)
        {
             openConnection();

            DataSet dsResult = new DataSet();
            DataTable dtResult = new DataTable();
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    SqlDataAdapter dad = new SqlDataAdapter(Query, con);
                    dad.Fill(dtResult);
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
               
            finally
            {
                closeConnection();
            }
            return dtResult;
        }

        public void closeConnection()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Dispose();            
        }

        public void FillCombo(DropDownList cmbObject, DataTable dtObject, string strValueColumn, string strTextColumn)
        {
            cmbObject.DataSource = dtObject;    
            cmbObject.DataTextField = strTextColumn;
            cmbObject.DataValueField = strValueColumn;
            cmbObject.DataBind();
            if (cmbObject.Items.Count != 0)
            {
                cmbObject.SelectedIndex = 0;
            }

        }

        public void AddClientType(string Query,DataTable dt,string ClientType)
        {
            try
            {
                openConnection();
                SqlDataAdapter ad1 = new SqlDataAdapter(Query,con);               
                SqlCommandBuilder bu = new SqlCommandBuilder(ad1);
                
                    DataRow dtNewRow = dt.NewRow();
                    dtNewRow["ERPs"] = ClientType;
                    dtNewRow["Contactinfo"] = "";
                    dt.Rows.Add(dtNewRow);
                    ad1.Update(dt);

                    ad1.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
               
                closeConnection();
            }
        }

        public int ExecuteNonQuery(String Query)
        {
            int intSuccess = 0;
            try
            {
                openConnection();
                SqlCommand sqlcom = new SqlCommand(Query, con);
                //sqlcom.ExecuteNonQuery();
                intSuccess = sqlcom.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                closeConnection();
            }
            return intSuccess;
        }

        #region GetClient&MethodKeyValues

        public int[] GetKeyValues(string ClientName, string JobName)
        {
            // IDictionary<string, int> iValue = new Dictionary<string, int>(); 
            int[] intA = new int[2];
            string[] arrStr = new string[2];
            try
            {
                openConnection();
                //string strInsert = "TableKeyValues_SP "+ ClientName+", "+JobName+","+Key1+" output ,"+Key2+" output";
                if (con.State == ConnectionState.Open)
                {
                    com = new SqlCommand("TableKeyValues_SP", con);
                    com.CommandType = System.Data.CommandType.StoredProcedure;

                    //input paramaters
                    com.Parameters.Add(new SqlParameter("@JobName", SqlDbType.NVarChar));
                    com.Parameters["@JobName"].Value = JobName;

                    com.Parameters.Add(new SqlParameter("@ClientName", ClientName));
                    com.Parameters["@ClientName"].Value = ClientName;

                    //Output Parameters
                    SqlParameter sqlparam = com.Parameters.Add(new SqlParameter("@JobKey", System.Data.SqlDbType.Int));
                    sqlparam.Direction = System.Data.ParameterDirection.Output;
                    sqlparam = com.Parameters.Add(new SqlParameter("@Client", System.Data.SqlDbType.Int));
                    sqlparam.Direction = System.Data.ParameterDirection.Output;
                    com.ExecuteNonQuery();

                    // set value to the int array..
                    intA[0] = Convert.ToInt32(com.Parameters["@Client"].Value.ToString());
                    intA[1] = Convert.ToInt32(com.Parameters["@JobKey"].Value.ToString());
                }
                return intA;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                closeConnection();
                com.Dispose();
            }

        }

        #endregion

        #region AddToLogTable

        public void insertValues(string ClientName, string jobName, string lastRunDate, string NextrunDate, string Status, string Comment)
        {
            try
            {


                int[] arrayValue = GetKeyValues(ClientName, jobName);

                openConnection();
                com = new SqlCommand("SP_INSERT_LOGDETAILS", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@CLIENTKEY", arrayValue[0]);
                com.Parameters.AddWithValue("@JOBKEY", arrayValue[1]);
                com.Parameters.AddWithValue("@Status", Status);
                com.Parameters.AddWithValue("@LASTRUNDATE", lastRunDate);
                com.Parameters.AddWithValue("@NEXTRUNDATE", NextrunDate);
                com.Parameters.AddWithValue("@COMMENT", Comment);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                closeConnection();
                com.Dispose();
            }
        }

        #endregion

        public string[] testProcedure(string ClientName, string JobName)
        {

            try
            {

                openConnection();
                //string strInsert = "TableKeyValues_SP "+ ClientName+", "+JobName+","+Key1+" output ,"+Key2+" output";

                com = new SqlCommand("TestProcedure", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                //input paramaters
                com.Parameters.Add(new SqlParameter("@JobName", JobName));
                com.Parameters.Add(new SqlParameter("@ClientName", ClientName));
                SqlDataReader sqlRead = com.ExecuteReader();

                // sqlRead = com.ExecuteReader();
                string[] arrStr = new string[2];

                int i = 0;
                while (sqlRead.Read())
                {
                    arrStr[0] = sqlRead[0].ToString();
                    arrStr[1] = sqlRead[1].ToString();
                }

                sqlRead.Close();

                return arrStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                closeConnection();
                com.Dispose();
            }

        }

        public string[] GetAuthentication(string UserName, string Password)
        {
            // IDictionary<string, int> iValue = new Dictionary<string, int>(); 

            string[] arrStr = new string[9];
            try
            {
                openConnection();
                //string strInsert = "TableKeyValues_SP "+ ClientName+", "+JobName+","+Key1+" output ,"+Key2+" output";
                if (con.State == ConnectionState.Open)
                {
                    com = new SqlCommand("SP_UserAuthenticaion", con);
                    com.CommandType = System.Data.CommandType.StoredProcedure;

                    //input paramaters
                    com.Parameters.Add(new SqlParameter("@UserName", UserName));
                    com.Parameters["@UserName"].Value = UserName;

                    com.Parameters.Add(new SqlParameter("@Password", Password));
                    com.Parameters["@Password"].Value = Password;

                    //Output Parameters
                    SqlParameter sqlparam = com.Parameters.Add(new SqlParameter("@UserNames", System.Data.SqlDbType.VarChar,30));
                    sqlparam.Direction = System.Data.ParameterDirection.Output;

                    sqlparam = com.Parameters.Add(new SqlParameter("@Passwords", System.Data.SqlDbType.VarChar,30));
                    sqlparam.Direction = System.Data.ParameterDirection.Output;

                    sqlparam = com.Parameters.Add(new SqlParameter("@FIRST_NAME", System.Data.SqlDbType.VarChar,30));
                    sqlparam.Direction = System.Data.ParameterDirection.Output;

                    sqlparam = com.Parameters.Add(new SqlParameter("@LAST_NAMES", System.Data.SqlDbType.VarChar, 30));
                    sqlparam.Direction = System.Data.ParameterDirection.Output;

                    sqlparam = com.Parameters.Add(new SqlParameter("@PRIORITY_METHODS", System.Data.SqlDbType.VarChar,1000));
                    sqlparam.Direction = System.Data.ParameterDirection.Output;

                    sqlparam = com.Parameters.Add(new SqlParameter("@NO_OF_RECORDS_PER_GRID", System.Data.SqlDbType.Int));
                    sqlparam.Direction = System.Data.ParameterDirection.Output;

                    sqlparam = com.Parameters.Add(new SqlParameter("@NO_OF_LOG_DAYS", System.Data.SqlDbType.Int));
                    sqlparam.Direction = System.Data.ParameterDirection.Output;

                    sqlparam = com.Parameters.Add(new SqlParameter("@ACCESS_RIGHTS", System.Data.SqlDbType.VarChar,10));
                    sqlparam.Direction = System.Data.ParameterDirection.Output;

                    sqlparam = com.Parameters.Add(new SqlParameter("@EMAIL", System.Data.SqlDbType.VarChar,100));
                    sqlparam.Direction = System.Data.ParameterDirection.Output;

                    com.ExecuteNonQuery ();

                    // set value to the string array..
                    arrStr[0] = com.Parameters["@UserNames"].Value.ToString();
                    arrStr[1] = com.Parameters["@Passwords"].Value.ToString();
                    arrStr[2] = com.Parameters["@FIRST_NAME"].Value.ToString();
                    arrStr[3] = com.Parameters["@LAST_NAMES"].Value.ToString();
                    arrStr[4] = com.Parameters["@PRIORITY_METHODS"].Value.ToString();
                    arrStr[5] = com.Parameters["@NO_OF_RECORDS_PER_GRID"].Value.ToString();
                    arrStr[6] = com.Parameters["@NO_OF_LOG_DAYS"].Value.ToString();
                    arrStr[7] = com.Parameters["@ACCESS_RIGHTS"].Value.ToString();
                    arrStr[8] = com.Parameters["@EMAIL"].Value.ToString();

                }
                return arrStr;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                closeConnection();
                com.Dispose();
            }

        }

        public void UpdateUserLogDetails(string query)
        {
            try
            {
                openConnection();
                com = new SqlCommand(query, con);
                com.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                closeConnection();
                com.Dispose();
            }
        }

        public string GetMethodIDs(string UserName)
        {
            string StrMethodIds = null;
            
            try
            {
                openConnection();
                //string strInsert = "TableKeyValues_SP "+ ClientName+", "+JobName+","+Key1+" output ,"+Key2+" output";
                if (con.State == ConnectionState.Open)
                {
                    com = new SqlCommand("GetUserMethods_SP", con);
                    com.CommandType = System.Data.CommandType.StoredProcedure;

                    //input paramaters
                    com.Parameters.Add(new SqlParameter("@UserName", UserName));
                    com.Parameters["@UserName"].Value = UserName;

                    
                    //Output Parameters
                    SqlParameter sqlparam = com.Parameters.Add(new SqlParameter("@MethodIds", System.Data.SqlDbType.VarChar, 100));
                    sqlparam.Direction = System.Data.ParameterDirection.Output;

                    com.ExecuteNonQuery();

                    // set value to the string array..
                    StrMethodIds = com.Parameters["@MethodIds"].Value.ToString();

                    
                }
                return StrMethodIds;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                closeConnection();
                com.Dispose();
            }
        }        

        public string[] GetUserCredentials(string Username)
        {
            string[] arrCredential = new string[2];
            
            try
            {
                openConnection();
                //string strInsert = "TableKeyValues_SP "+ ClientName+", "+JobName+","+Key1+" output ,"+Key2+" output";
                if (con.State == ConnectionState.Open)
                {
                    com = new SqlCommand("GetUserCredential_SP", con);
                    com.CommandType = System.Data.CommandType.StoredProcedure;

                    //input paramaters
                    com.Parameters.Add(new SqlParameter("@UserName", Username));
                    com.Parameters["@UserName"].Value = Username;


                    //Output Parameters
                    SqlParameter sqlparam = com.Parameters.Add(new SqlParameter("@Credential", System.Data.SqlDbType.VarChar, 100));
                    sqlparam.Direction = System.Data.ParameterDirection.Output;

                    sqlparam = com.Parameters.Add(new SqlParameter("@Email", System.Data.SqlDbType.VarChar, 100));
                    sqlparam.Direction = System.Data.ParameterDirection.Output;
                                       

                    com.ExecuteNonQuery();

                    // set value to the string array..
                    arrCredential[0] = com.Parameters["@Credential"].Value.ToString();
                    arrCredential[1] = com.Parameters["@Email"].Value.ToString();

                }
                return arrCredential;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                closeConnection();
                com.Dispose();
            }
        }

    }
}