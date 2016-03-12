using System;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// คลาสจัดการเกี่ยวกับฐานข้อมูลทั้งหมด เช่น Insert Update Query หรือ ตัวจัดการเกี่ยวกับคำสั่ง SQL
/// </summary>
public class clsSQL
{
    #region GlobalVariable
    private DBType dbType = DBType.SQLServer;
    private string cs = "cs";
    #endregion
    public clsSQL()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
    }
    public clsSQL(DBType dbType, string connectionString)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        this.dbType = dbType;
        this.cs = connectionString;
    }
    public enum DBType
    {
        SQLServer,MySQL,ODBC
    }
    public string getConnectionString(string cs)
    {
        #region Variable
        var result = "";
        #endregion
        #region Procedure
        if (cs.ToLower().Contains("connectionstring"))
        {
            #region IsConnectionString
            result = ConnectionStringToAppSetting(cs);
            #endregion
        }
        else if (cs.Contains("="))
        {
            #region IsAppSettingValue
            result = cs;
            #endregion
        }
        else
        {
            #region IsAppSettingName
            result = System.Configuration.ConfigurationManager.AppSettings[cs];
            #endregion
        }
        #endregion
        return result;
    }
    /// <summary>
    /// คืนค่าจากคำสั่ง SQLQuery เป็น DataTable
    /// </summary>
    /// <param name="strSQL"></param>
    /// <returns></returns>
    public DataTable Bind(string strSQL)
    {
        #region Variable
        var csSQL = getConnectionString(cs);
        var dt = new DataTable();
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                using (var myConn_SQL = new SqlConnection(csSQL))
                using (var myDa_SQL = new SqlDataAdapter(QueryFilterByDatabaseType(strSQL), myConn_SQL))
                {
                    myDa_SQL.Fill(dt);
                    myConn_SQL.Dispose();
                    myDa_SQL.Dispose();
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        return dt;
                    }
                    else
                    {
                        dt.Dispose();
                        return null;
                    }
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                using (var myConn_ODBC = new OdbcConnection(csSQL))
                using (var myDa_ODBC = new OdbcDataAdapter(QueryFilterByDatabaseType(strSQL), myConn_ODBC))
                {
                    myDa_ODBC.Fill(dt);
                    myConn_ODBC.Dispose();
                    myDa_ODBC.Dispose();
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        return dt;
                    }
                    else
                    {
                        dt.Dispose();
                        return null;
                    }
                }
                #endregion
            }
            else if (dbType == DBType.MySQL)
            {
                #region MySQL
                using(var myConn_MySQL = new MySql.Data.MySqlClient.MySqlConnection(csSQL))
                using (var myDa_MySQL = new MySql.Data.MySqlClient.MySqlDataAdapter(QueryFilterByDatabaseType(strSQL), myConn_MySQL))
                {
                    myDa_MySQL.Fill(dt);
                    myConn_MySQL.Dispose();
                    myDa_MySQL.Dispose();
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        return dt;
                    }
                    else
                    {
                        dt.Dispose();
                        return null;
                    }
                }
                #endregion
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
        #endregion
    }
    /// <summary>
    /// Execute คำสั่ง SQL แล้วเก็บค่าที่ได้ใส่ DataTable
    /// </summary>
    /// <param name="strSQL"></param>
    /// <param name="outMessage"></param>
    /// <returns></returns>
    public DataTable Bind(string strSQL,out string outMessage,int timeOut=30)
    {
        #region Variable
        var csSQL = getConnectionString(cs);
        var dt = new DataTable();
        outMessage = "";
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                try
                {
                    using (var myConn_SQL = new SqlConnection(csSQL))
                    using (var myDa_SQL = new SqlDataAdapter(QueryFilterByDatabaseType(strSQL), myConn_SQL))
                    {
                        myDa_SQL.SelectCommand.CommandTimeout = timeOut;
                        myDa_SQL.Fill(dt);
                        myConn_SQL.Dispose();
                        myDa_SQL.Dispose();
                        if (dt.Rows.Count > 0 && dt != null)
                        {
                            return dt;
                        }
                        else
                        {
                            dt.Dispose();
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    return null;
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                try
                {
                    using (var myConn_ODBC = new OdbcConnection(csSQL))
                    using (var myDa_ODBC = new OdbcDataAdapter(QueryFilterByDatabaseType(strSQL), myConn_ODBC))
                    {
                        myDa_ODBC.SelectCommand.CommandTimeout = timeOut;
                        myDa_ODBC.Fill(dt);
                        myConn_ODBC.Dispose();
                        myDa_ODBC.Dispose();
                        if (dt.Rows.Count > 0 && dt != null)
                        {
                            return dt;
                        }
                        else
                        {
                            dt.Dispose();
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    return null;
                }
                #endregion
            }
            else if (dbType == DBType.MySQL)
            {
                #region MySQL
                try
                {
                    using (var myConn_MySQL = new MySql.Data.MySqlClient.MySqlConnection(csSQL))
                    using (var myDa_MySQL = new MySql.Data.MySqlClient.MySqlDataAdapter(QueryFilterByDatabaseType(strSQL), myConn_MySQL))
                    {
                        myDa_MySQL.SelectCommand.CommandTimeout = timeOut;
                        myDa_MySQL.Fill(dt);
                        myConn_MySQL.Dispose();
                        myDa_MySQL.Dispose();
                        if (dt.Rows.Count > 0 && dt != null)
                        {
                            return dt;
                        }
                        else
                        {
                            dt.Dispose();
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    return null;
                }
                #endregion
            }
            else
            {
                outMessage = "Not found DBType.";
                return null;
            }
        }
        else
        {
            outMessage = "Not found AppSettingName.";
            return null;
        }
        #endregion
    }
    /// <summary>
    /// คืนค่าเป็น DataTable พร้อมรองรับการใช้ Parameter
    /// </summary>
    /// <param name="strSQL"></param>
    /// <param name="arrParameter"></param>
    /// <returns></returns>
    /// <example>
    /// clsSQL.Bind("SELECT * FROM Table WHERE UID=?UID",new string[,]{{"?UID",1}});
    /// </example>
    public DataTable Bind(string strSQL, string[,] arrParameter)
    {
        #region Variable
        var csSQL = getConnectionString(cs);
        var dt = new DataTable();
        var i = 0;
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                using (var myConn_SQL = new SqlConnection(csSQL))
                using (var myDa_SQL = new SqlDataAdapter(QueryFilterByDatabaseType(strSQL), myConn_SQL))
                {
                    for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                    {
                        myDa_SQL.SelectCommand.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                    }
                    myDa_SQL.Fill(dt);
                    myConn_SQL.Dispose();
                    myDa_SQL.Dispose();
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        return dt;
                    }
                    else
                    {
                        dt.Dispose();
                        return null;
                    }
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                using (var myConn_ODBC = new OdbcConnection(csSQL))
                using (var myDa_ODBC = new OdbcDataAdapter(QueryFilterByDatabaseType(strSQL), myConn_ODBC))
                {
                    for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                    {
                        myDa_ODBC.SelectCommand.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                    }
                    myDa_ODBC.Fill(dt);
                    myConn_ODBC.Dispose();
                    myDa_ODBC.Dispose();
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        return dt;
                    }
                    else
                    {
                        dt.Dispose();
                        return null;
                    }
                }
                #endregion
            }
            else if (dbType == DBType.MySQL)
            {
                #region MySQL
                using (var myConn_MySQL = new MySql.Data.MySqlClient.MySqlConnection(csSQL))
                using (var myDa_MySQL = new MySql.Data.MySqlClient.MySqlDataAdapter(QueryFilterByDatabaseType(strSQL), myConn_MySQL))
                {
                    for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                    {
                        myDa_MySQL.SelectCommand.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                    }
                    myDa_MySQL.Fill(dt);
                    myConn_MySQL.Dispose();
                    myDa_MySQL.Dispose();
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        return dt;
                    }
                    else
                    {
                        dt.Dispose();
                        return null;
                    }
                }
                #endregion
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
        #endregion
    }
    public DataTable Bind(string strSQL, string[,] arrParameter, out string outMessage)
    {
        #region Variable
        var csSQL = getConnectionString(cs);
        var dt = new DataTable();
        var i = 0;
        outMessage = "";
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                try
                {
                    using (var myConn_SQL = new SqlConnection(csSQL))
                    using (var myDa_SQL = new SqlDataAdapter(QueryFilterByDatabaseType(strSQL), myConn_SQL))
                    {
                        for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                        {
                            myDa_SQL.SelectCommand.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                        }
                        myDa_SQL.Fill(dt);
                        myConn_SQL.Dispose();
                        myDa_SQL.Dispose();
                        if (dt.Rows.Count > 0 && dt != null)
                        {
                            return dt;
                        }
                        else
                        {
                            dt.Dispose();
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    return null;
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                try
                {
                    using (var myConn_ODBC = new OdbcConnection(csSQL))
                    using (var myDa_ODBC = new OdbcDataAdapter(QueryFilterByDatabaseType(strSQL), myConn_ODBC))
                    {
                        for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                        {
                            myDa_ODBC.SelectCommand.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                        }
                        myDa_ODBC.Fill(dt);
                        myConn_ODBC.Dispose();
                        myDa_ODBC.Dispose();
                        if (dt.Rows.Count > 0 && dt != null)
                        {
                            return dt;
                        }
                        else
                        {
                            dt.Dispose();
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    return null;
                }
                #endregion
            }
            else if (dbType == DBType.MySQL)
            {
                #region MySQL
                try
                {
                    using (var myConn_MySQL = new MySql.Data.MySqlClient.MySqlConnection(csSQL))
                    using (var myDa_MySQL = new MySql.Data.MySqlClient.MySqlDataAdapter(QueryFilterByDatabaseType(strSQL), myConn_MySQL))
                    {
                        for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                        {
                            myDa_MySQL.SelectCommand.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                        }
                        myDa_MySQL.Fill(dt);
                        myConn_MySQL.Dispose();
                        myDa_MySQL.Dispose();
                        if (dt.Rows.Count > 0 && dt != null)
                        {
                            return dt;
                        }
                        else
                        {
                            dt.Dispose();
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    return null;
                }
                #endregion
            }
            else
            {
                outMessage = "Not found DBType.";
                return null;
            }
        }
        else
        {
            outMessage = "Not found AppSettingName.";
            return null;
        }
        #endregion
    }
    public string Return(string strSQL)
    {
        #region Variable
        var csSQL = getConnectionString(cs); ;
        var strReturn = "";
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                using (var myConn_SQL = new SqlConnection(csSQL))
                using (var myCmd_SQL = new SqlCommand(QueryFilterByDatabaseType(strSQL), myConn_SQL))
                {
                    try
                    {
                        myConn_SQL.Open();
                        strReturn = myCmd_SQL.ExecuteScalar().ToString();
                        myConn_SQL.Close();
                        myCmd_SQL.Dispose();
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        myCmd_SQL.Dispose();
                        myConn_SQL.Close();
                    }
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                using (var myConn_ODBC = new OdbcConnection(csSQL))
                using (var myCmd_ODBC = new OdbcCommand(QueryFilterByDatabaseType(strSQL), myConn_ODBC))
                {
                    try
                    {
                        myConn_ODBC.Open();
                        strReturn = myCmd_ODBC.ExecuteScalar().ToString();
                        myConn_ODBC.Close();
                        myCmd_ODBC.Dispose();
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        myCmd_ODBC.Dispose();
                        myConn_ODBC.Close();
                    }
                }
                #endregion
            }
            else if (dbType == DBType.MySQL)
            {
                #region MySQL
                using (var myConn_MySQL = new MySql.Data.MySqlClient.MySqlConnection(csSQL))
                using (var myCmd_MySQL = new MySql.Data.MySqlClient.MySqlCommand(QueryFilterByDatabaseType(strSQL), myConn_MySQL))
                {
                    try
                    {
                        myConn_MySQL.Open();
                        strReturn = myCmd_MySQL.ExecuteScalar().ToString();
                        myConn_MySQL.Close();
                        myCmd_MySQL.Dispose();
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        myCmd_MySQL.Dispose();
                        myConn_MySQL.Close();
                    }
                }
                #endregion
            }
            else
            {
                strReturn = "";
            }
        }
        return strReturn;
        #endregion
    }
    /// <summary>
    /// Execute คำสั่ง SQL ที่คืนค่าเป็นค่าเดียว
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="outMessage">ข้อความ กรณีเกิดข้อผิดพลาด</param>
    /// <returns>ข้อมูล</returns>
    /// <example>
    /// string outMessage;
    /// clsSQL.Return("SELECT MAX(id) FROM member",clsSQL.DBType.MySQL,"cs",out outMessage);
    /// </example>
    public string Return(string strSQL,out string outMessage)
    {
        #region Variable
        var csSQL = getConnectionString(cs);
        var strReturn = "";
        outMessage = "";
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                using (var myConn_SQL = new SqlConnection(csSQL))
                using (var myCmd_SQL = new SqlCommand(QueryFilterByDatabaseType(strSQL), myConn_SQL))
                {
                    try
                    {
                        myConn_SQL.Open();
                        strReturn = myCmd_SQL.ExecuteScalar().ToString();
                        myConn_SQL.Close();
                        myCmd_SQL.Dispose();
                    }
                    catch (Exception ex)
                    {
                        outMessage = ex.Message;
                    }
                    finally
                    {
                        myCmd_SQL.Dispose();
                        myConn_SQL.Close();
                    }
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                using (var myConn_ODBC = new OdbcConnection(csSQL))
                using (var myCmd_ODBC = new OdbcCommand(QueryFilterByDatabaseType(strSQL), myConn_ODBC))
                {
                    try
                    {
                        myConn_ODBC.Open();
                        strReturn = myCmd_ODBC.ExecuteScalar().ToString();
                        myConn_ODBC.Close();
                        myCmd_ODBC.Dispose();
                    }
                    catch (Exception ex)
                    {
                        outMessage = ex.Message;
                    }
                    finally
                    {
                        myCmd_ODBC.Dispose();
                        myConn_ODBC.Close();
                    }
                }
                #endregion
            }
            else if (dbType == DBType.MySQL)
            {
                #region MySQL
                using (var myConn_MySQL = new MySql.Data.MySqlClient.MySqlConnection(csSQL))
                using (var myCmd_MySQL = new MySql.Data.MySqlClient.MySqlCommand(QueryFilterByDatabaseType(strSQL), myConn_MySQL))
                {
                    try
                    {
                        myConn_MySQL.Open();
                        strReturn = myCmd_MySQL.ExecuteScalar().ToString();
                        myConn_MySQL.Close();
                        myCmd_MySQL.Dispose();
                    }
                    catch (Exception ex)
                    {
                        outMessage = ex.Message;
                    }
                    finally
                    {
                        myCmd_MySQL.Dispose();
                        myConn_MySQL.Close();
                    }
                }
                #endregion
            }
            else
            {
                strReturn = "";
                outMessage = "Not found DBType.";
            }
        }
        return strReturn;
        #endregion
    }
    /// <summary>
    /// Execute คำสั่ง SQL ที่คืนค่าเป็นค่าเดียว โดยสามารถใช้ SQL Parameter ได้
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="arrParameter">SQL Parameter (new string[,] { { "?ID", txtTest.Text } })</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <returns>ข้อมูล</returns>
    /// <example>
    /// strSQL.Append("SELECT email FROM member WHERE id=?ID");
    /// lblMessage.Text = clsSQL.Return(strSQL.ToString(), new string[,] { { "?ID", txtTest.Text } }, clsSQL.DBType.MySQL, "cs");
    /// </example>
    public string Return(string strSQL, string[,] arrParameter)
    {
        #region Variable
        var csSQL = getConnectionString(cs);
        var strReturn = "";
        var i = 0;
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                using (var myConn_SQL = new SqlConnection(csSQL))
                using (var myCmd_SQL = new SqlCommand(QueryFilterByDatabaseType(strSQL), myConn_SQL))
                {
                    for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                    {
                        myCmd_SQL.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                    }
                    try
                    {
                        myConn_SQL.Open();
                        strReturn = myCmd_SQL.ExecuteScalar().ToString();
                        myConn_SQL.Close();
                        myCmd_SQL.Dispose();
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        myCmd_SQL.Dispose();
                        myConn_SQL.Close();
                    }
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                using (var myConn_ODBC = new OdbcConnection(csSQL))
                using (var myCmd_ODBC = new OdbcCommand(QueryFilterByDatabaseType(strSQL), myConn_ODBC))
                {
                    for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                    {
                        myCmd_ODBC.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                    }
                    try
                    {
                        myConn_ODBC.Open();
                        strReturn = myCmd_ODBC.ExecuteScalar().ToString();
                        myConn_ODBC.Close();
                        myCmd_ODBC.Dispose();
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        myCmd_ODBC.Dispose();
                        myConn_ODBC.Close();
                    }
                }
                #endregion
            }
            else if (dbType == DBType.MySQL)
            {
                #region MySQL
                using (var myConn_MySQL = new MySql.Data.MySqlClient.MySqlConnection(csSQL))
                using (var myCmd_MySQL = new MySql.Data.MySqlClient.MySqlCommand(QueryFilterByDatabaseType(strSQL), myConn_MySQL))
                {
                    for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                    {
                        myCmd_MySQL.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                    }
                    try
                    {
                        myConn_MySQL.Open();
                        strReturn = myCmd_MySQL.ExecuteScalar().ToString();
                        myConn_MySQL.Close();
                        myCmd_MySQL.Dispose();
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        myCmd_MySQL.Dispose();
                        myConn_MySQL.Close();
                    }
                }
                #endregion
            }
            else
            {
                strReturn = "";
            }
        }
        return strReturn;
        #endregion
    }
    /// <summary>
    /// Execute คำสั่ง SQL ที่คืนค่าเป็นค่าเดียว โดยสามารถใช้ SQL Parameter ได้
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="arrParameter">SQL Parameter (new string[,] { { "?ID", txtTest.Text } })</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="outMessage">ข้อความ กรณีเกิดข้อผิดพลาด</param>
    /// <returns>ข้อมูล</returns>
    /// <example>
    /// string outMessage;
    /// strSQL.Append("SELECT email FROM member WHERE id=?ID");
    /// lblMessage.Text = clsSQL.Return(strSQL.ToString(), new string[,] { { "?ID", txtTest.Text } }, clsSQL.DBType.MySQL, "cs",out outMessage);
    /// </example>
    public string Return(string strSQL, string[,] arrParameter, out string outMessage)
    {
        #region Variable
        var csSQL = getConnectionString(cs);
        var strReturn = "";
        var i = 0;
        outMessage = "";
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                using (var myConn_SQL = new SqlConnection(csSQL))
                using (var myCmd_SQL = new SqlCommand(QueryFilterByDatabaseType(strSQL), myConn_SQL))
                {
                    for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                    {
                        myCmd_SQL.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                    }
                    try
                    {
                        myConn_SQL.Open();
                        strReturn = myCmd_SQL.ExecuteScalar().ToString();
                        myConn_SQL.Close();
                        myCmd_SQL.Dispose();
                    }
                    catch (Exception ex)
                    {
                        outMessage = ex.Message;
                    }
                    finally
                    {
                        myCmd_SQL.Dispose();
                        myConn_SQL.Close();
                    }
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                using (var myConn_ODBC = new OdbcConnection(csSQL))
                using (var myCmd_ODBC = new OdbcCommand(QueryFilterByDatabaseType(strSQL), myConn_ODBC))
                {
                    for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                    {
                        myCmd_ODBC.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                    }
                    try
                    {
                        myConn_ODBC.Open();
                        strReturn = myCmd_ODBC.ExecuteScalar().ToString();
                        myConn_ODBC.Close();
                        myCmd_ODBC.Dispose();
                    }
                    catch (Exception ex)
                    {
                        outMessage = ex.Message;
                    }
                    finally
                    {
                        myCmd_ODBC.Dispose();
                        myConn_ODBC.Close();
                    }
                }
                #endregion
            }
            else if (dbType == DBType.MySQL)
            {
                #region MySQL
                using (var myConn_MySQL = new MySql.Data.MySqlClient.MySqlConnection(csSQL))
                using (var myCmd_MySQL = new MySql.Data.MySqlClient.MySqlCommand(QueryFilterByDatabaseType(strSQL), myConn_MySQL))
                {
                    for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                    {
                        myCmd_MySQL.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                    }
                    try
                    {
                        myConn_MySQL.Open();
                        strReturn = myCmd_MySQL.ExecuteScalar().ToString();
                        myConn_MySQL.Close();
                        myCmd_MySQL.Dispose();
                    }
                    catch (Exception ex)
                    {
                        outMessage = ex.Message;
                    }
                    finally
                    {
                        myCmd_MySQL.Dispose();
                        myConn_MySQL.Close();
                    }
                }
                #endregion
            }
            else
            {
                outMessage = "Not found DBType.";
                strReturn = "";
            }
        }
        return strReturn;
        #endregion
    }
    /// <summary>
    /// Execute คำสั่ง SQL ที่ใช้ในการบันทึกข้อมูล
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <returns>True=รันสำเร็จ , False=รันไม่สำเร็จ</returns>
    /// <example>
    /// clsSQL.Execute("DELETE FROM member WHERE id=1",clsSQL.DBType.MySQL,"cs");
    /// </example>
    public bool Execute(string strSQL)
    {
        #region Variable
        var csSQL = getConnectionString(cs);
        var result = false;
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                using (var myConn_SQL = new SqlConnection(csSQL))
                using (var myCmd_SQL = new SqlCommand(QueryFilterByDatabaseType(strSQL), myConn_SQL))
                {
                    try
                    {
                        myConn_SQL.Open();
                        myCmd_SQL.ExecuteNonQuery();
                        myConn_SQL.Close();
                        myCmd_SQL.Dispose();
                        result = true;
                    }
                    catch (Exception)
                    {
                        result = false;
                    }
                    finally
                    {
                        myCmd_SQL.Dispose();
                        myConn_SQL.Close();
                    }
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                using (var myConn_ODBC = new OdbcConnection(csSQL))
                using (var myCmd_ODBC = new OdbcCommand(QueryFilterByDatabaseType(strSQL), myConn_ODBC))
                {
                    try
                    {
                        myConn_ODBC.Open();
                        myCmd_ODBC.ExecuteNonQuery();
                        myConn_ODBC.Close();
                        myCmd_ODBC.Dispose();
                        result = true;
                    }
                    catch (Exception)
                    {
                        result = false;
                    }
                    finally
                    {
                        myCmd_ODBC.Dispose();
                        myConn_ODBC.Close();
                    }
                }
                #endregion
            }
            else if (dbType == DBType.MySQL)
            {
                #region MySQL
                using (var myConn_MySQL = new MySql.Data.MySqlClient.MySqlConnection(csSQL))
                using (var myCmd_MySQL = new MySql.Data.MySqlClient.MySqlCommand(QueryFilterByDatabaseType(strSQL), myConn_MySQL))
                {
                    try
                    {
                        myConn_MySQL.Open();
                        myCmd_MySQL.ExecuteNonQuery();
                        myConn_MySQL.Close();
                        myCmd_MySQL.Dispose();
                        result = true;
                    }
                    catch (Exception)
                    {
                        result = false;
                    }
                    finally
                    {
                        myCmd_MySQL.Dispose();
                        myConn_MySQL.Close();
                    }
                }
                #endregion
            }
            else
            {
                result = false;
            }
        }
        else
        {
            result = false;
        }
        return result;
        #endregion
    }
    /// <summary>
    /// Execute คำสั่ง SQL ที่ใช้ในการบันทึกข้อมูล
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="outMessage">ข้อความ กรณีเกิดข้อผิดพลาด</param>
    /// <returns>True=รันสำเร็จ , False=รันไม่สำเร็จ</returns>
    /// <example>
    /// string outMessage;
    /// clsSQL.Execute("DELETE FROM member WHERE id=1",clsSQL.DBType.MySQL,"cs",out outMessage);
    /// </example>
    public bool Execute(string strSQL, out string outMessage)
    {
        #region Variable
        var csSQL = getConnectionString(cs);
        var result = false;
        outMessage = "";
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                using (var myConn_SQL = new SqlConnection(csSQL))
                using (var myCmd_SQL = new SqlCommand(QueryFilterByDatabaseType(strSQL), myConn_SQL))
                {
                    try
                    {
                        myConn_SQL.Open();
                        myCmd_SQL.ExecuteNonQuery();
                        myConn_SQL.Close();
                        myCmd_SQL.Dispose();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        outMessage = ex.Message;
                        result = false;
                    }
                    finally
                    {
                        myCmd_SQL.Dispose();
                        myConn_SQL.Close();
                    }
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                using (var myConn_ODBC = new OdbcConnection(csSQL))
                using (var myCmd_ODBC = new OdbcCommand(QueryFilterByDatabaseType(strSQL), myConn_ODBC))
                {
                    try
                    {
                        myConn_ODBC.Open();
                        myCmd_ODBC.ExecuteNonQuery();
                        myConn_ODBC.Close();
                        myCmd_ODBC.Dispose();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        outMessage = ex.Message;
                        result = false;
                    }
                    finally
                    {
                        myCmd_ODBC.Dispose();
                        myConn_ODBC.Close();
                    }
                }
                #endregion
            }
            else if (dbType == DBType.MySQL)
            {
                #region MySQL
                using (var myConn_MySQL = new MySql.Data.MySqlClient.MySqlConnection(csSQL))
                using (var myCmd_MySQL = new MySql.Data.MySqlClient.MySqlCommand(QueryFilterByDatabaseType(strSQL), myConn_MySQL))
                {
                    try
                    {
                        myConn_MySQL.Open();
                        myCmd_MySQL.ExecuteNonQuery();
                        myConn_MySQL.Close();
                        myCmd_MySQL.Dispose();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        outMessage = ex.Message;
                        result = false;
                    }
                    finally
                    {
                        myCmd_MySQL.Dispose();
                        myConn_MySQL.Close();
                    }
                }
                #endregion
            }
            else
            {
                outMessage = "Not found DBType.";
                result = false;
            }
        }
        else
        {
            outMessage = "Not found AppSettingName.";
            result = false;
        }
        return result;
        #endregion
    }
    public bool Execute(string strSQL, out string outMessage,out int outRowEffective)
    {
        #region Variable
        var csSQL = getConnectionString(cs);
        var result = false;
        outMessage = "";
        outRowEffective = 0;
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                using (var myConn_SQL = new SqlConnection(csSQL))
                using (var myCmd_SQL = new SqlCommand(QueryFilterByDatabaseType(strSQL), myConn_SQL))
                {
                    try
                    {
                        myConn_SQL.Open();
                        outRowEffective=myCmd_SQL.ExecuteNonQuery();
                        myConn_SQL.Close();
                        myCmd_SQL.Dispose();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        outMessage = ex.Message;
                        result = false;
                    }
                    finally
                    {
                        myCmd_SQL.Dispose();
                        myConn_SQL.Close();
                    }
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                using (var myConn_ODBC = new OdbcConnection(csSQL))
                using (var myCmd_ODBC = new OdbcCommand(QueryFilterByDatabaseType(strSQL), myConn_ODBC))
                {
                    try
                    {
                        myConn_ODBC.Open();
                        outRowEffective=myCmd_ODBC.ExecuteNonQuery();
                        myConn_ODBC.Close();
                        myCmd_ODBC.Dispose();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        outMessage = ex.Message;
                        result = false;
                    }
                    finally
                    {
                        myCmd_ODBC.Dispose();
                        myConn_ODBC.Close();
                    }
                }
                #endregion
            }
            else if (dbType == DBType.MySQL)
            {
                #region MySQL
                using (var myConn_MySQL = new MySql.Data.MySqlClient.MySqlConnection(csSQL))
                using (var myCmd_MySQL = new MySql.Data.MySqlClient.MySqlCommand(QueryFilterByDatabaseType(strSQL), myConn_MySQL))
                {
                    try
                    {
                        myConn_MySQL.Open();
                        outRowEffective=myCmd_MySQL.ExecuteNonQuery();
                        myConn_MySQL.Close();
                        myCmd_MySQL.Dispose();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        outMessage = ex.Message;
                        result = false;
                    }
                    finally
                    {
                        myCmd_MySQL.Dispose();
                        myConn_MySQL.Close();
                    }
                }
                #endregion
            }
            else
            {
                outMessage = "Not found DBType.";
                result = false;
            }
        }
        else
        {
            outMessage = "Not found AppSettingName.";
            result = false;
        }
        return result;
        #endregion
    }
    /// <summary>
    /// Execute คำสั่ง SQL ที่ใช้ในการบันทึกข้อมูล โดยสามารถระบุ SQL Parameter ได้
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="arrParameter">SQL Parameter (new string[,] { { "?ID", txtTest.Text } })</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <returns>True=รันสำเร็จ , False=รันไม่สำเร็จ</returns>
    /// <example>
    /// clsSQL.Execute("UPDATE webboard_type SET type_name=?NAME WHERE type_id=?ID", new string[,] { { "?ID", txtTest.Text }, { "?NAME", "ใช้ Array 2 มิติ" } }, clsSQL.DBType.MySQL, "cs");
    /// </example>
    public bool Execute(string strSQL, string[,] arrParameter)
    {
        #region Variable
        var csSQL = getConnectionString(cs);
        var result = false;
        var i = 0;
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL) && arrParameter.Rank == 2)
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                using (var myConn_SQL = new SqlConnection(csSQL))
                using (var myCmd_SQL = new SqlCommand(QueryFilterByDatabaseType(strSQL), myConn_SQL))
                {
                    for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                    {
                        myCmd_SQL.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                    }

                    try
                    {
                        myConn_SQL.Open();
                        myCmd_SQL.ExecuteNonQuery();
                        myConn_SQL.Close();
                        myCmd_SQL.Dispose();
                        result = true;
                    }
                    catch (Exception)
                    {
                        result = false;
                    }
                    finally
                    {
                        myCmd_SQL.Dispose();
                        myConn_SQL.Close();
                    }
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                using (var myConn_ODBC = new OdbcConnection(csSQL))
                using (var myCmd_ODBC = new OdbcCommand(QueryFilterByDatabaseType(strSQL), myConn_ODBC))
                {
                    for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                    {
                        myCmd_ODBC.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                    }

                    try
                    {
                        myConn_ODBC.Open();
                        myCmd_ODBC.ExecuteNonQuery();
                        myConn_ODBC.Close();
                        myCmd_ODBC.Dispose();
                        result = true;
                    }
                    catch (Exception)
                    {
                        result = false;
                    }
                    finally
                    {
                        myCmd_ODBC.Dispose();
                        myConn_ODBC.Close();
                    }
                }
                #endregion
            }
            else if (dbType == DBType.MySQL)
            {
                #region MySQL
                using (var myConn_MySQL = new MySql.Data.MySqlClient.MySqlConnection(csSQL))
                using (var myCmd_MySQL = new MySql.Data.MySqlClient.MySqlCommand(QueryFilterByDatabaseType(strSQL), myConn_MySQL))
                {
                    for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                    {
                        myCmd_MySQL.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                    }

                    try
                    {
                        myConn_MySQL.Open();
                        myCmd_MySQL.ExecuteNonQuery();
                        myConn_MySQL.Close();
                        myCmd_MySQL.Dispose();
                        result = true;
                    }
                    catch (Exception)
                    {
                        result = false;
                    }
                    finally
                    {
                        myCmd_MySQL.Dispose();
                        myConn_MySQL.Close();
                    }
                }
                #endregion
            }
            else
            {
                result = false;
            }
        }
        else
        {
            result = false;
        }
        return result;
        #endregion
    }
    /// <summary>
    /// Execute คำสั่ง SQL ที่ใช้ในการบันทึกข้อมูล โดยสามารถระบุ SQL Parameter ได้
    /// </summary>
    /// <param name="strSQL">SQL Query</param>
    /// <param name="arrParameter">SQL Parameter (new string[,] { { "?ID", txtTest.Text } })</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="outMessage">ข้อความ เมื่อเกิดข้อผิดพลาด</param>
    /// <returns>True=รันสำเร็จ , False=รันไม่สำเร็จ</returns>
    /// <example>
    /// string outMessage;
    /// clsSQL.Execute("UPDATE webboard_type SET type_name=?NAME WHERE type_id=?ID", new string[,] { { "?ID", txtTest.Text }, { "?NAME", "ใช้ Array 2 มิติ" } }, clsSQL.DBType.MySQL, "cs",out outMessage);
    /// </example>
    public bool Execute(string strSQL, string[,] arrParameter, out string outMessage)
    {
        #region Variable
        var csSQL = getConnectionString(cs);
        var result = false;
        var i = 0;
        outMessage = "";
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(csSQL) && arrParameter.Rank == 2)
        {
            if (dbType == DBType.SQLServer)
            {
                #region SQLServer
                var myConn_SQL = new SqlConnection(csSQL);
                var myCmd_SQL = new SqlCommand(QueryFilterByDatabaseType(strSQL), myConn_SQL);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myCmd_SQL.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }

                try
                {
                    myConn_SQL.Open();
                    myCmd_SQL.ExecuteNonQuery();
                    myConn_SQL.Close();
                    myCmd_SQL.Dispose();
                    result = true;
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    result = false;
                }
                finally
                {
                    myCmd_SQL.Dispose();
                    myConn_SQL.Close();
                }
                #endregion
            }
            else if (dbType == DBType.ODBC)
            {
                #region ODBC
                var myConn_ODBC = new OdbcConnection(csSQL);
                var myCmd_ODBC = new OdbcCommand(QueryFilterByDatabaseType(strSQL), myConn_ODBC);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myCmd_ODBC.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }

                try
                {
                    myConn_ODBC.Open();
                    myCmd_ODBC.ExecuteNonQuery();
                    myConn_ODBC.Close();
                    myCmd_ODBC.Dispose();
                    result = true;
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    result = false;
                }
                finally
                {
                    myCmd_ODBC.Dispose();
                    myConn_ODBC.Close();
                }
                #endregion
            }
            else if (dbType == DBType.MySQL)
            {
                #region MySQL
                var myConn_MySQL = new MySql.Data.MySqlClient.MySqlConnection(csSQL);
                var myCmd_MySQL = new MySql.Data.MySqlClient.MySqlCommand(QueryFilterByDatabaseType(strSQL), myConn_MySQL);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myCmd_MySQL.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }

                try
                {
                    myConn_MySQL.Open();
                    myCmd_MySQL.ExecuteNonQuery();
                    myConn_MySQL.Close();
                    myCmd_MySQL.Dispose();
                    result = true;
                }
                catch (Exception ex)
                {
                    outMessage = ex.Message;
                    result = false;
                }
                finally
                {
                    myCmd_MySQL.Dispose();
                    myConn_MySQL.Close();
                }
                #endregion
            }
            else
            {
                outMessage = "Not found DBType.";
                result = false;
            }
        }
        else
        {
            outMessage = "Not found AppSettingName.";
            result = false;
        }
        return result;
        #endregion
    }
    /// <summary>
    /// Insert ข้อมูลลงฐานข้อมูล โดยส่งค่าฟิลด์และข้อมูลมาเป็นลิส และ ใช้ SQL Parameter ได้
    /// </summary>
    /// <param name="strTable">ชื่อ Table</param>
    /// <param name="arrValue">ลิสชื่อฟิลด์และข้อมูล (new string[,] { { "region_id", "?ID" } })</param>
    /// <param name="arrParameter">SQL Parameter (new string[,]{{"?ID","3"}})</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="outSQL">คืน SQL Query ที่โปรแกรมสร้างให้</param>
    /// <param name="AutoExcute">true=Execute คำสั่ง , false=ไม่ต้อง Execute คำสั่ง</param>
    /// <returns>True=รันสำเร็จ , False=ไม่สำเร็จ</returns>
    /// <example>
    /// clsSQL clsSQL = new clsSQL();string outSQL;
    /// clsSQL.Insert(
    ///     "PROVINCE",
    ///     new string[,] { { "region_id", "?ID" }, { "province_id", "79" }, { "province_name", "'ทดสอบ 2'" },{"province_sort","99"} }, 
    ///     new string[,]{{"?ID","3"}},
    ///     "MySQL","cs",out outSQL
    /// );
    /// clsSQL.Insert(
    ///     "PROVINCE",
    ///     new string[,] { { "region_id", "?ID" }, { "province_id", "79" }, { "province_name", "'ทดสอบ 2'" },{"province_sort","99"} }, 
    ///     new string[,]{{"?ID","3"}},
    ///     clsSQL.DBType.MySQL,"cs",out outSQL
    /// );
    /// </example>
    public bool Insert(string strTable, string[,] arrValue, string[,] arrParameter, out string outSQL, bool AutoExcute = true)
    {
        #region Variable
        var result = false;
        var strSQL = new StringBuilder();
        var i = 0;
        outSQL = "";
        #endregion
        #region Procedure
        if (arrValue.Rank == 2)
        {
            #region QueryBuilder
            strSQL.Append("INSERT INTO ");
            strSQL.Append(strTable);
            strSQL.Append("(");
            #region FieldBuilder
            for (i = 0; i < arrValue.Length / arrValue.Rank; i++)
            {
                strSQL.Append(arrValue[i, 0]);
                if (i < (arrValue.Length / arrValue.Rank) - 1)
                {
                    strSQL.Append(",");
                }
            }
            #endregion
            strSQL.Append(")VALUES(");
            #region ValueBuilder
            for (i = 0; i < arrValue.Length / arrValue.Rank; i++)
            {
                strSQL.Append(arrValue[i, 1]);
                if (i < (arrValue.Length / arrValue.Rank) - 1)
                {
                    strSQL.Append(",");
                }
            }
            #endregion
            strSQL.Append(");");
            #endregion
            outSQL = strSQL.ToString();
            #region Excute
            if (AutoExcute)
            {
                result = Execute(strSQL.ToString(), arrParameter);
            }
            else
            {
                result = true;
            }
            #endregion
        }
        return result;
        #endregion
    }
    /// Insert ข้อมูลลงฐานข้อมูล โดยส่งค่าฟิลด์และข้อมูลมาเป็นลิส และ ใช้ SQL Parameter ได้
    /// </summary>
    /// <param name="strTable">ชื่อ Table</param>
    /// <param name="arrValue">ลิสชื่อฟิลด์และข้อมูล (new string[,] { { "region_id", "?ID" } })</param>
    /// <param name="arrParameter">SQL Parameter (new string[,]{{"?ID","3"}})</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="outSQL">คืน SQL Query ที่โปรแกรมสร้างให้</param>
    /// <param name="outMessage">ข้อความ กรณีเกิดข้อผิดพลาด</param>
    /// <param name="AutoExcute">true=Execute คำสั่ง , false=ไม่ต้อง Execute คำสั่ง</param>
    /// <returns>True=รันสำเร็จ , False=ไม่สำเร็จ</returns>
    /// <example>
    /// clsSQL clsSQL = new clsSQL();string outSQL;string outMessage;
    /// clsSQL.Insert(
    ///     "PROVINCE",
    ///     new string[,] { { "region_id", "?ID" }, { "province_id", "79" }, { "province_name", "'ทดสอบ 2'" },{"province_sort","99"} }, 
    ///     new string[,]{{"?ID","3"}},
    ///     "MySQL","cs",out outSQL
    /// );
    /// clsSQL.Insert(
    ///     "PROVINCE",
    ///     new string[,] { { "region_id", "?ID" }, { "province_id", "79" }, { "province_name", "'ทดสอบ 2'" },{"province_sort","99"} }, 
    ///     new string[,]{{"?ID","3"}},
    ///     clsSQL.DBType.MySQL,"cs",out outSQL,out outMessage
    /// );
    /// </example>
    public bool Insert(string strTable, string[,] arrValue, string[,] arrParameter, out string outSQL,out string outMessage, bool AutoExcute = true)
    {
        #region Variable
        var result = false;
        var strSQL = new StringBuilder();
        var i = 0;
        outSQL = "";
        outMessage = "";
        #endregion
        #region Procedure
        if (arrValue.Rank == 2)
        {
            #region QueryBuilder
            strSQL.Append("INSERT INTO ");
            strSQL.Append(strTable);
            strSQL.Append("(");
            #region FieldBuilder
            for (i = 0; i < arrValue.Length / arrValue.Rank; i++)
            {
                strSQL.Append(arrValue[i, 0]);
                if (i < (arrValue.Length / arrValue.Rank) - 1)
                {
                    strSQL.Append(",");
                }
            }
            #endregion
            strSQL.Append(")VALUES(");
            #region ValueBuilder
            for (i = 0; i < arrValue.Length / arrValue.Rank; i++)
            {
                strSQL.Append(arrValue[i, 1]);
                if (i < (arrValue.Length / arrValue.Rank) - 1)
                {
                    strSQL.Append(",");
                }
            }
            #endregion
            strSQL.Append(");");
            #endregion
            outSQL = strSQL.ToString();
            #region Excute
            if (AutoExcute)
            {
                result = Execute(strSQL.ToString(), arrParameter, out outMessage);
            }
            else
            {
                result = true;
            }
            #endregion
        }
        return result;
        #endregion
    }
    /// <summary>
    /// Update ข้อมูลในฐานข้อมูล โดยส่งลิสของฟิลด์และข้อมูล พร้อมระบุ SQL Parameter ได้
    /// </summary>
    /// <param name="strTable">ชื่อ Table</param>
    /// <param name="arrValue">ลิสของชื่อฟิลด์และข้อมูล</param>
    /// <param name="arrParameter">SQL Parameter</param>
    /// <param name="strWhere">เงื่อนไข WHERE (province_id=?ID)</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="outSQL">SQL Query ที่โปรแกรมสร้างขึ้น</param>
    /// <param name="AutoExcute">true=Execute คำสั่ง , false=ไม่ต้อง Execute คำสั่ง</param>
    /// <returns>True=รันสำเร็จ , False=ไม่สำเร็จ</returns>
    /// <example>
    /// clsSQL clsSQL = new clsSQL();
    /// string outSQL;
    /// clsSQL.Update(
    ///     "PROVINCE",
    ///     new string[,] {{ "province_name", "'ทดสอบ 2'" }, { "province_sort", "1" } },
    ///     new string[,] { {"?ID","78"} },
    ///     "province_id=?ID",
    ///     clsSQL.DBType.MySQL,"cs",out outSQL
    /// );
    /// clsSQL.Update(
    ///     "PROVINCE",
    ///     new string[,] {{ "province_name", "'ทดสอบ 2'" }, { "province_sort", "1" } },
    ///     new string[,] { {} },
    ///     "province_id=78",clsSQL.DBType.MySQL,"cs",out outSQL
    /// );
    /// </example>
    public bool Update(string strTable, string[,] arrValue, string[,] arrParameter, string strWhere, out string outSQL, bool AutoExcute = true)
    {
        #region Variable
        var result = false;
        var strSQL = new StringBuilder();
        var i = 0;
        outSQL = "";
        #endregion
        #region Procedure
        if (arrValue.Rank == 2)
        {
            #region QueryBuilder
            strSQL.Append("UPDATE ");
            strSQL.Append(strTable);
            strSQL.Append(" SET ");
            #region FieldBuilder
            for (i = 0; i < arrValue.Length / arrValue.Rank; i++)
            {
                strSQL.Append(arrValue[i, 0]);
                strSQL.Append("=");
                strSQL.Append(arrValue[i, 1]);
                if (i < (arrValue.Length / arrValue.Rank) - 1)
                {
                    strSQL.Append(",");
                }
            }
            #endregion
            strSQL.Append(" WHERE ");
            strSQL.Append(strWhere);
            strSQL.Append(";");
            #endregion
            outSQL = strSQL.ToString();
            #region Excute
            if (AutoExcute)
            {
                result = Execute(strSQL.ToString(), arrParameter);
            }
            else
            {
                result = true;
            }
            #endregion
        }
        return result;
        #endregion
    }
    /// <summary>
    /// Update ข้อมูลในฐานข้อมูล โดยส่งลิสของฟิลด์และข้อมูล พร้อมระบุ SQL Parameter ได้
    /// </summary>
    /// <param name="strTable">ชื่อ Table</param>
    /// <param name="arrValue">ลิสของชื่อฟิลด์และข้อมูล</param>
    /// <param name="arrParameter">SQL Parameter</param>
    /// <param name="strWhere">เงื่อนไข WHERE (province_id=?ID)</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="outSQL">SQL Query ที่โปรแกรมสร้างขึ้น</param>
    /// <param name="outMessage">ข้อความ กรณีเกิดข้อผิดพลาด</param>
    /// <param name="AutoExcute">true=Execute คำสั่ง , false=ไม่ต้อง Execute คำสั่ง</param>
    /// <returns>True=รันสำเร็จ , False=ไม่สำเร็จ</returns>
    /// <example>
    /// clsSQL clsSQL = new clsSQL();
    /// string outSQL;string outMessage;
    /// clsSQL.Update(
    ///     "PROVINCE",
    ///     new string[,] {{ "province_name", "'ทดสอบ 2'" }, { "province_sort", "1" } },
    ///     new string[,] { {"?ID","78"} },
    ///     "province_id=?ID",
    ///     clsSQL.DBType.MySQL,"cs",out outSQL,out outMessage
    /// );
    /// clsSQL.Update(
    ///     "PROVINCE",
    ///     new string[,] {{ "province_name", "'ทดสอบ 2'" }, { "province_sort", "1" } },
    ///     new string[,] { {} },
    ///     "province_id=78",clsSQL.DBType.MySQL,"cs",out outSQL,out outMessage
    /// );
    /// </example>
    public bool Update(string strTable, string[,] arrValue, string[,] arrParameter, string strWhere, out string outSQL,out string outMessage, bool AutoExcute = true)
    {
        #region Variable
        var result = false;
        var strSQL = new StringBuilder();
        var i = 0;
        outSQL = "";
        outMessage = "";
        #endregion
        #region Procedure
        if (arrValue.Rank == 2)
        {
            #region QueryBuilder
            strSQL.Append("UPDATE ");
            strSQL.Append(strTable);
            strSQL.Append(" SET ");
            #region FieldBuilder
            for (i = 0; i < arrValue.Length / arrValue.Rank; i++)
            {
                strSQL.Append(arrValue[i, 0]);
                strSQL.Append("=");
                strSQL.Append(arrValue[i, 1]);
                if (i < (arrValue.Length / arrValue.Rank) - 1)
                {
                    strSQL.Append(",");
                }
            }
            #endregion
            strSQL.Append(" WHERE ");
            strSQL.Append(strWhere);
            strSQL.Append(";");
            #endregion
            outSQL = strSQL.ToString();
            #region Excute
            if (AutoExcute)
            {
                result = Execute(strSQL.ToString(), arrParameter, out outMessage);
            }
            else
            {
                result = true;
            }
            #endregion
        }
        return result;
        #endregion
    }
    /// <summary>
    /// ใช้สำหรับ Insert OR Update โดยเป็นการส่งค่าครั้งเดียวให้ SQL Server นำไปวิเคราะห์เอง
    /// </summary>
    /// <param name="strTable">ชื่อตาราง</param>
    /// <param name="arrValue">อาร์เรย์ 2 มิติ ที่ระบุชื่อ Field และ Value</param>
    /// <param name="arrPrimaryKey">ชื่อ Field ที่ใช้ค้นหาค่าว่ามีหรือปล่าว ก่อนจะ Update หรือ Insert</param>
    /// <param name="arrExceptionFieldForUpdate">ชื่อ Field ที่ไม่นำมากำหนดค่าในการ Update (กรณีมีข้อมูลแล้ว)</param>
    /// <param name="arrParameter">SQL Parameter เช่น SQL=@UID , MySQL=?UID</param>
    /// <param name="dbType">ประเภทฐานข้อมูล</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ Connection ในไฟล์ AppSetting</param>
    /// <param name="outSQL">SQL Query ที่ได้</param>
    /// <param name="AutoExcute">true=Execute คำสั่ง , false=ไม่ต้อง Execute คำสั่ง</param>
    /// <returns>true=เสร็จสมบูรณ์ , false=เกิดข้อผิดพลาด</returns>
    /// <example>
    ///     clsSQL clsSQL = new clsSQL();string outSQL;
    ///     clsSQL.Upsert("Doctor", 
    ///         new string[,] { 
    ///         { "UID", "@UID" },
    ///         {"Photo","'Test.jpg'" },
    ///         {"PNameTH","'นาย'" },
    ///         {"FNameTH","'นิธิ Update'" },
    ///         {"CWhen","GETDATE()" },
    ///         {"MWhen","GETDATE()" },
    ///         {"Active","'1'" }
    ///         }, 
    ///         new string[] { "UID" },
    ///         new string[] { "CWhen" },
    ///         new string[,] { { "@UID","1" } }, 
    ///         clsSQL.DBType.SQLServer, 
    ///         "cs", 
    ///         out outSQL,
    ///         false
    ///     );
    /// </example>
    public bool Upsert(string strTable, string[,] arrValue, string[] arrPrimaryKey, string[] arrExceptionFieldForUpdate, string[,] arrParameter, out string outSQL, bool AutoExcute = true)
    {
        #region Variable
        var clsData = new clsData();
        var result = false;
        var strSQL = new StringBuilder();
        var strPrimaryKey = new StringBuilder();
        var strCondition = new StringBuilder();
        var strField = new StringBuilder();
        var strValue = new StringBuilder();
        var i=0; var j=0;
        outSQL = "";
        #endregion
        #region Procedure
        if (arrValue.Rank == 2)
        {
            #region SQL Query
            strSQL.Append("MERGE " + strTable + " AS T ");
            strSQL.Append("USING");
            strSQL.Append("(");
            strSQL.Append("SELECT ");

            #region PrimaryKey & Condition
            for (i = 0; i < arrPrimaryKey.Length; i++)
            {
                for (j = 0; j < arrValue.Length / arrValue.Rank; j++)
                {
                    if (arrPrimaryKey[i] == arrValue[j, 0])
                    {
                        if (strPrimaryKey.Length > 0)
                        {
                            strPrimaryKey.Append(",");
                            strCondition.Append(" AND ");
                        }

                        strPrimaryKey.Append(arrValue[j, 1] + " AS " + arrValue[j, 0]);
                        strCondition.Append("T." + arrValue[j, 0] + "=V." + arrValue[j, 0]);
                        break;
                    }
                }
            }
            #endregion

            strSQL.Append(strPrimaryKey.ToString()); strPrimaryKey.Length = 0; strPrimaryKey.Capacity = 0;
            strSQL.Append(") As V ");
            strSQL.Append("ON ");
            strSQL.Append(strCondition.ToString()); strCondition.Length = 0; strCondition.Capacity = 0;
            strSQL.Append(" WHEN MATCHED THEN ");
            strSQL.Append("UPDATE SET ");
            #region Update
            for (i = 0; i < arrValue.Length / arrValue.Rank; i++)
            {
                #region Field & Value Store
                strField.Append(arrValue[i, 0]);
                strValue.Append(arrValue[i, 1]);
                if (i < (arrValue.Length / arrValue.Rank) - 1)
                {
                    strField.Append(",");
                    strValue.Append(",");
                }
                #endregion

                if (
                    !clsData.Search(new List<string>(arrExceptionFieldForUpdate), arrValue[i, 0]) &&
                    !clsData.Search(new List<string>(arrPrimaryKey), arrValue[i, 0]))
                {
                    strSQL.Append("T." + arrValue[i, 0] + "=" + arrValue[i, 1]);
                    if (i < (arrValue.Length / arrValue.Rank) - 1)
                    {
                        strSQL.Append(",");
                    }
                }
            }
            #endregion
            strSQL.Append(" WHEN NOT MATCHED THEN ");
            #region Insert
            strSQL.Append("INSERT(");
            strSQL.Append(strField.ToString());
            strSQL.Append(")VALUES(");
            strSQL.Append(strValue.ToString());
            strSQL.Append(");");
            #endregion
            #endregion
            outSQL = strSQL.ToString();
            #region Excute
            if (AutoExcute)
            {
                result = Execute(strSQL.ToString(), arrParameter);
            }
            else
            {
                result = true;
            }
            #endregion
        }

        return result;
        #endregion
    }
    /// <summary>
    /// ใช้สำหรับ Insert OR Update โดยเป็นการส่งค่าครั้งเดียวให้ SQL Server นำไปวิเคราะห์เอง
    /// </summary>
    /// <param name="strTable">ชื่อตาราง</param>
    /// <param name="arrValue">อาร์เรย์ 2 มิติ ที่ระบุชื่อ Field และ Value</param>
    /// <param name="arrPrimaryKey">ชื่อ Field ที่ใช้ค้นหาค่าว่ามีหรือปล่าว ก่อนจะ Update หรือ Insert</param>
    /// <param name="arrExceptionFieldForUpdate">ชื่อ Field ที่ไม่นำมากำหนดค่าในการ Update (กรณีมีข้อมูลแล้ว)</param>
    /// <param name="arrParameter">SQL Parameter เช่น SQL=@UID , MySQL=?UID</param>
    /// <param name="dbType">ประเภทฐานข้อมูล</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ Connection ในไฟล์ AppSetting</param>
    /// <param name="outSQL">SQL Query ที่ได้</param>
    /// <param name="outMessage">ข้อความ เมื่อเกิดข้อผิดพลาด</param>
    /// <param name="AutoExcute">true=Execute คำสั่ง , false=ไม่ต้อง Execute คำสั่ง</param>
    /// <returns>true=เสร็จสมบูรณ์ , false=เกิดข้อผิดพลาด</returns>
    /// <example>
    ///     clsSQL clsSQL = new clsSQL();string outSQL;string outMessage;
    ///     clsSQL.Upsert("Doctor", 
    ///         new string[,] { 
    ///         { "UID", "@UID" },
    ///         {"Photo","'Test.jpg'" },
    ///         {"PNameTH","'นาย'" },
    ///         {"FNameTH","'นิธิ Update'" },
    ///         {"CWhen","GETDATE()" },
    ///         {"MWhen","GETDATE()" },
    ///         {"Active","'1'" }
    ///         }, 
    ///         new string[] { "UID" },
    ///         new string[] { "CWhen" },
    ///         new string[,] { { "@UID","1" } }, 
    ///         clsSQL.DBType.SQLServer, 
    ///         "cs", 
    ///         out outSQL,out outMessage,
    ///         false
    ///     );
    /// </example>
    public bool Upsert(string strTable, string[,] arrValue, string[] arrPrimaryKey, string[] arrExceptionFieldForUpdate, string[,] arrParameter, out string outSQL,out string outMessage, bool AutoExcute = true)
    {
        #region Variable
        var clsData = new clsData();
        var result = false;
        var strSQL = new StringBuilder();
        var strPrimaryKey = new StringBuilder();
        var strCondition = new StringBuilder();
        var strField = new StringBuilder();
        var strValue = new StringBuilder();
        var i = 0; var j = 0;
        outSQL = "";
        outMessage = "";
        #endregion
        #region Procedure
        if (arrValue.Rank == 2)
        {
            #region SQL Query
            strSQL.Append("MERGE " + strTable + " AS T ");
            strSQL.Append("USING");
            strSQL.Append("(");
            strSQL.Append("SELECT ");

            #region PrimaryKey & Condition
            for (i = 0; i < arrPrimaryKey.Length; i++)
            {
                for (j = 0; j < arrValue.Length / arrValue.Rank; j++)
                {
                    if (arrPrimaryKey[i] == arrValue[j, 0])
                    {
                        if (strPrimaryKey.Length > 0)
                        {
                            strPrimaryKey.Append(",");
                            strCondition.Append(" AND ");
                        }

                        strPrimaryKey.Append(arrValue[j, 1] + " AS " + arrValue[j, 0]);
                        strCondition.Append("T." + arrValue[j, 0] + "=V." + arrValue[j, 0]);
                        break;
                    }
                }
            }
            #endregion

            strSQL.Append(strPrimaryKey.ToString()); strPrimaryKey.Length = 0; strPrimaryKey.Capacity = 0;
            strSQL.Append(") As V ");
            strSQL.Append("ON ");
            strSQL.Append(strCondition.ToString()); strCondition.Length = 0; strCondition.Capacity = 0;
            strSQL.Append(" WHEN MATCHED THEN ");
            strSQL.Append("UPDATE SET ");
            #region Update
            for (i = 0; i < arrValue.Length / arrValue.Rank; i++)
            {
                #region Field & Value Store
                strField.Append(arrValue[i, 0]);
                strValue.Append(arrValue[i, 1]);
                if (i < (arrValue.Length / arrValue.Rank) - 1)
                {
                    strField.Append(",");
                    strValue.Append(",");
                }
                #endregion

                if (
                    !clsData.Search(new List<string>(arrExceptionFieldForUpdate), arrValue[i, 0]) &&
                    !clsData.Search(new List<string>(arrPrimaryKey), arrValue[i, 0]))
                {
                    strSQL.Append("T." + arrValue[i, 0] + "=" + arrValue[i, 1]);
                    if (i < (arrValue.Length / arrValue.Rank) - 1)
                    {
                        strSQL.Append(",");
                    }
                }
            }
            #endregion
            strSQL.Append(" WHEN NOT MATCHED THEN ");
            #region Insert
            strSQL.Append("INSERT(");
            strSQL.Append(strField.ToString());
            strSQL.Append(")VALUES(");
            strSQL.Append(strValue.ToString());
            strSQL.Append(");");
            #endregion
            #endregion
            outSQL = strSQL.ToString();
            #region Excute
            if (AutoExcute)
            {
                result = Execute(strSQL.ToString(), arrParameter,out outMessage);
            }
            else
            {
                result = true;
            }
            #endregion
        }

        return result;
        #endregion
    }
    /// <summary>
    /// ลบข้อมูลในฐานข้อมูล
    /// </summary>
    /// <param name="strTable">ชื่อ Table</param>
    /// <param name="strWhere">เงื่อนไข เช่น UID=1</param>
    /// <param name="dbType">ประเภทของฐานข้อมูล</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="outSQL">SQLQuery</param>
    /// <param name="AutoExcute">true=ให้ Exceute เลย , false=ไม่ต้อง Excute</param>
    /// <returns>true=สำเร็จ , false=ผิดพลาด</returns>
    public bool Delete(string strTable, string strWhere, out string outSQL, bool AutoExcute = true)
    {
        #region Variable
        var result = false;
        var strSql = new StringBuilder();
        outSQL = "";
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(strTable) && !string.IsNullOrEmpty(cs))
        {
            #region QueryBuilder
            strSql.Append("DELETE ");
            strSql.Append("FROM ");
            strSql.Append(strTable);
            strSql.Append(" WHERE ");
            strSql.Append(strWhere);
            strSql.Append(";");
            #endregion
            outSQL = strSql.ToString();
            if (AutoExcute)
            {
                if (Execute(strSql.ToString()))
                {
                    result = true;
                }
            }
            else
            {
                result = true;
            }
        }
        else
        {
            result = false;
        }
        strSql.Length = 0; strSql.Capacity = 0;
        return result;
        #endregion
    }
    /// <summary>
    /// ลบข้อมูลในฐานข้อมูล
    /// </summary>
    /// <param name="strTable">ชื่อ Table</param>
    /// <param name="strWhere">เงื่อนไข เช่น UID=1</param>
    /// <param name="dbType">ประเภทของฐานข้อมูล</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="outSQL">SQLQuery</param>
    /// <param name="outMessage">ข้อความ กรณีเกิดข้อผิดพลาด</param>
    /// <param name="AutoExcute">true=ให้ Exceute เลย , false=ไม่ต้อง Excute</param>
    /// <returns>true=สำเร็จ , false=ผิดพลาด</returns>
    public bool Delete(string strTable, string strWhere, out string outSQL,out string outMessage, bool AutoExcute = true)
    {
        #region Variable
        var result = false;
        var strSql = new StringBuilder();
        outSQL = "";
        outMessage = "";
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(strTable) && !string.IsNullOrEmpty(cs))
        {
            #region QueryBuilder
            strSql.Append("DELETE ");
            strSql.Append("FROM ");
            strSql.Append(strTable);
            strSql.Append(" WHERE ");
            strSql.Append(strWhere);
            strSql.Append(";");
            #endregion
            outSQL = strSql.ToString();
            if (AutoExcute)
            {
                if (Execute(strSql.ToString(), out outMessage))
                {
                    result = true;
                }
            }
            else
            {
                result = true;
            }
        }
        else
        {
            result = false;
        }
        strSql.Length = 0; strSql.Capacity = 0;
        return result;
        #endregion
    }
    /// <summary>
    /// สร้างประโยค WHERE จาก CheckBoxList
    /// </summary>
    /// <param name="cbControl">ชื่อคอนโทรล CheckBoxList</param>
    /// <param name="strField">ชื่อฟิลด์ที่ใช้สร้าง SQL Query</param>
    /// <param name="strJoin">ตัวเชื่อมประโยค SQL Query (OR,AND)</param>
    /// <param name="booQuote">True=ใช้สัญลักษณ์ ' ครอบข้อมูล , False=ไม่ใช้ ' ครอบ</param>
    /// <param name="booBracket">True=ใช้ () ครอบ SQL Query ที่ได้ , False=ไม่ใช้ () ครอบ</param>
    /// <returns>คืนค่า SQL Where เฉพาะส่วนที่ตรวจสอบกับ CheckBoxList</returns>
    /// <example>
    /// clsSQL.QueryBuilderWhere(cbGender,"sex","OR",true,true);
    /// </example>
    public string QueryBuilderWhere(CheckBoxList cbControl, string strField, string strJoin, bool booQuote, bool booBracket)
    {
        StringBuilder strSQL = new StringBuilder();
        string strReturn = "";
        int i;
        string strQuote = (booQuote == true ? "'" : "");
        strJoin = " " + strJoin + " ";

        strSQL.Length = 0; strSQL.Capacity = 0;
        for (i = 0; i <= cbControl.Items.Count - 1; i++)
        {
            if (cbControl.Items[i].Selected == true)
            {
                if (cbControl.Items[i].Value == "all")
                {
                    strSQL.Length = 0; strSQL.Capacity = 0;
                    break;
                }
                strSQL.Append(strField + "=");
                strSQL.Append(strQuote);
                strSQL.Append(cbControl.Items[i].Value);
                strSQL.Append(strQuote);
                strSQL.Append(strJoin);
            }
        }
        if (!string.IsNullOrEmpty(strSQL.ToString()))
        {
            if (strSQL.ToString().Length > strJoin.Length)
            {
                strReturn = (booBracket == true ? "(" : "") +
                    strSQL.ToString().Substring(0, strSQL.ToString().Length - strJoin.Length) +
                    (booBracket == true ? ")" : "");
            }
        }
        strSQL.Length = 0; strSQL.Capacity = 0; strSQL = null;
        return strReturn;
    }
    /// <summary>
    /// Execute คำสั่ง SQL กับไฟล์ Excel
    /// </summary>
    /// <param name="strSQL">SQL Query (Insert into [ACCOUNT$](id,usern,pwd,type)VALUES('999999','username','password','admin'))</param>
    /// <param name="pathShortXLSX">ไฟล์พาร์ธ (Upload/ACCOUNT.xlsx)</param>
    /// <returns>True=รันสำเร็จ , False=รันไม่สำเร็จ</returns>
    /// <example>
    /// ExecuteExcel("Insert into [ACCOUNT$](id,usern,pwd,type)VALUES('999999','username','password','admin')", "Upload/ACCOUNT.xlsx");
    /// </example>
    public bool ExecuteExcel(string strSQL, string pathShortXLSX)
    {
        bool rtnValue = false;
        FileInfo objInfo = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(pathShortXLSX));

        if (objInfo.Exists)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                System.Web.HttpContext.Current.Server.MapPath(pathShortXLSX) +
                ";Extended Properties='Excel 12.0 Xml;HDR=YES'";
            OleDbConnection myConn = new OleDbConnection(cs);
            try
            {
                OleDbCommand myCmd = new OleDbCommand(strSQL, myConn);
                myConn.Open();
                myCmd.ExecuteNonQuery();
                myConn.Close();
                rtnValue = true;
            }
            catch (Exception ex)
            {

            }
        }

        return rtnValue;
    }
    /// <summary>
    /// Execute คำสั่ง SQL กับไฟล์ Excel โดยใช้วิธีคืนค่า
    /// </summary>
    /// <param name="strSQL">SQL Query (SELECT COUNT(id) FROM [ACCOUNT$])</param>
    /// <param name="pathShortXLSX">ไฟล์พาร์ธ (Upload/ACCOUNT.xlsx)</param>
    /// <returns>คืนค่าที่ได้จาก SQL Query</returns>
    /// <example>
    /// ReturnExcel("SELECT COUNT(id) FROM [ACCOUNT$]","Upload/ACCOUNT.xlsx");
    /// </example>
    public string ReturnExcel(string strSQL, string pathShortXLSX)
    {
        string rtnValue = "";
        FileInfo objInfo = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(pathShortXLSX));

        if (objInfo.Exists)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                System.Web.HttpContext.Current.Server.MapPath(pathShortXLSX) +
                ";Extended Properties='Excel 12.0 Xml;HDR=YES'";
            OleDbConnection myConn = new OleDbConnection(cs);
            try
            {
                OleDbCommand myCmd = new OleDbCommand(strSQL, myConn);
                myConn.Open();
                rtnValue = myCmd.ExecuteScalar().ToString();
                myConn.Close();
            }
            catch (Exception ex)
            {

            }
        }

        return rtnValue;
    }
    /// <summary>
    /// สร้าง DataTable จากไฟล์ Excel ที่เราระบุ โดยสร้างจาก SQL Query ที่เราระบุเอง
    /// </summary>
    /// <param name="strSQL">SQL Query (SELECT * FROM [ACCOUNT$])</param>
    /// <param name="pathShortXLSX">ไฟล์พาร์ธ (Upload/ACCOUNT.xlsx)</param>
    /// <returns>DataTable ที่ได้ตาม SQL Query</returns>
    /// <example>
    /// BindExcel("SELECT * FROM [ACCOUNT$]", "Upload/ACCOUNT.xlsx");
    /// </example>
    public DataTable BindExcel(string strSQL, string pathShortXLSX)
    {
        DataTable dt = new DataTable();
        FileInfo objInfo = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(pathShortXLSX));

        if (objInfo.Exists == false)
        {
            //clsJS clsJS = new clsJS();
            //clsJS.Alert("ไม่พบไฟล์ที่คุณระบุ");
            return dt;
        }

        string cs = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            System.Web.HttpContext.Current.Server.MapPath(pathShortXLSX) +
            ";Extended Properties='Excel 12.0 Xml;HDR=YES'";

        OleDbConnection myConn = new OleDbConnection(cs);

        try
        {
            OleDbDataAdapter myDa = new OleDbDataAdapter(strSQL, myConn);
            myDa.Fill(dt);
        }
        catch (Exception ex)
        {
            // เกิดข้อผิดพลาด เช่น ไม่มี Column ที่เลือก , ชื่อ Sheet ผิด , Statement ผิด
        }

        return dt;
    }
    /// <summary>
    /// ใช้สร้าง DataTable จากการ Query ข้อมูลในไฟล์ Excel โดยการระบุแยก เช่น ชื่อชีต ชื่อคอลัมที่ต้องการ และ เงื่อนไข
    /// </summary>
    /// <param name="strSheetName">ชื่อ Sheet ในไฟล์ Excel</param>
    /// <param name="strColumnName">ชื่อฟิลด์ที่ต้องการใน DataTable (id,usern,pwd)</param>
    /// <param name="strWhere">WHERE Query (usern like '%002%')</param>
    /// <param name="pathShortXLSX">ไฟล์พาร์ธ (Upload/ACCOUNT.xlsx)</param>
    /// <returns>DataTable ที่ได้ตาม SQL Query</returns>
    /// <example>
    /// BindExcel("ACCOUNT", "id,usern,pwd", "usern like '%002%'", "Upload/ACCOUNT.xlsx");
    /// </example>
    public DataTable BindExcel(string strSheetName, string strColumnName, string strWhere, string pathShortXLSX)
    {
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();

        FileInfo objInfo = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(pathShortXLSX));
        if (objInfo.Exists == false)
        {
            //clsJS clsJS = new clsJS();
            //clsJS.Alert("ไม่พบไฟล์ที่คุณระบุ");
            return dt;
        }

        string cs = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            System.Web.HttpContext.Current.Server.MapPath(pathShortXLSX) +
            ";Extended Properties='Excel 12.0 Xml;HDR=YES'";

        strSQL.Append("SELECT ");
        strSQL.Append((!string.IsNullOrEmpty(strColumnName) ? strColumnName : "*"));
        strSQL.Append(" FROM ");
        strSQL.Append((!string.IsNullOrEmpty(strSheetName) ? "[" + strSheetName + "$]" : "[Sheet1$]"));
        strSQL.Append((!string.IsNullOrEmpty(strWhere) ? " WHERE " + strWhere : ""));

        OleDbConnection myConn = new OleDbConnection(cs);

        try
        {
            OleDbDataAdapter myDa = new OleDbDataAdapter(strSQL.ToString(), myConn);
            myDa.Fill(dt);
        }
        catch (Exception ex)
        {
            // เกิดข้อผิดพลาด เช่น ไม่มี Column ที่เลือก , ชื่อ Sheet ผิด , Statement ผิด
        }

        return dt;
    }
    /// <summary>
    /// หา UID ใหม่ในฐานข้อมูล จากเงื่อนไขที่เรากำหนด
    /// </summary>
    /// <param name="id_column_name">ชื่อฟิลด์ที่ต้องการ (UID)</param>
    /// <param name="fromTable">ชื่อ Table</param>
    /// <param name="whereStr">เงื่อนไขพิเศษ อาจเว้นว่างไว้ก็ได้ (member_active='1')</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <returns>คืนค่า UID ใหม่</returns>
    /// <example>
    /// clsSQL.GetNewID("member_id","MEMBER","member_active='1'",clsSQL.DBType.MySQL,"cs");
    /// </example>
    public int GetNewID(string id_column_name, string fromTable, string whereStr, DBType dbType, string cs)
    {
        StringBuilder strSQL = new StringBuilder();
        int id = 0;
        string functionName;

        if (dbType == DBType.SQLServer)
        {
            functionName = "ISNULL";
        }
        else if (dbType == DBType.MySQL)
        {
            functionName = "IFNULL";
        }
        else
        {
            functionName = "IFNULL";
        }

        strSQL.Append("SELECT ");
        strSQL.Append(functionName + "(MAX(" + id_column_name + "),0)+1 ");
        strSQL.Append("FROM ");
        strSQL.Append(fromTable + " ");
        if (!string.IsNullOrEmpty(whereStr))
        {
            strSQL.Append("WHERE ");
            strSQL.Append(whereStr);
        }

        clsSQL clsSQL = new clsSQL();

        id = int.Parse(clsSQL.Return(strSQL.ToString()));

        return id;
    }
    /// <summary>
    /// หารหัส UID ใหม่จากฟิลด์ประเภท AutoIncrement
    /// </summary>
    /// <param name="tbName"></param>
    /// <param name="dbType"></param>
    /// <param name="appSettingName"></param>
    /// <returns></returns>
    public int GetNewIDAutoIncrement(string tbName)
    {
        #region Variable
        var clsSQL = new clsSQL(dbType, cs);
        var strSQL = "";
        var id = 0;
        #endregion
        #region Procedure
        if (dbType == DBType.SQLServer)
        {
            strSQL = "SELECT ISNULL(IDENT_CURRENT('" + tbName + "'),0)+1;";
        }
        else if (dbType == DBType.MySQL)
        {
            strSQL = @"SELECT AUTO_INCREMENT 
                            FROM information_schema.tables 
                            WHERE table_name='" + tbName + @"' 
                            AND table_schema=DATABASE()";
        }
        else
        {
            return 0;
        }

        try
        {
            id = int.Parse(clsSQL.Return(strSQL.ToString()));
        }
        catch (Exception) { }
        #endregion
        return id;
    }
    /// <summary>
    /// เช็คข้อมูลก่อนบันทึกลงฐานข้อมูล เป็นว่างจะคืนเป็น null หากไม่ว่าง สามารถระบุว่าจะให้ใส่ ' ด้วยหรือไม่
    /// </summary>
    /// <param name="Value">ค่าที่ต้องการให้ตรวจสอบ</param>
    /// <param name="IsVarchar">True=ใส่เครื่องหมาย ' ครอบข้อมูล กรณีข้อมูลไม่ว่าง , False=ไม่ใส่ ' กรณีข้อมูลไม่ว่าง</param>
    /// <returns>ข้อมูลหลังการตรวจสอบและปรับค่า</returns>
    /// <example>
    /// clsSQL.GetNull("",true);        return null
    /// clsSQL.GetNull("ทดสอบ",true);   return 'ทดสอบ'
    /// </example>
    public string GetNull(string Value, bool IsVarchar = true)
    {
        string rtnValue = "";

        if (!string.IsNullOrEmpty(Value))
        {
            if (IsVarchar)
            {
                rtnValue = "'" + Value + "'";
            }
            else
            {
                rtnValue = Value;
            }
        }
        else
        {
            rtnValue = "null";
        }

        return rtnValue;
    }
    /// <summary>
    /// กรองอักขระที่อาจก่อให้เกิดความผิดพลาด เมื่อใช้ร่วมกับ SQL Query
    /// </summary>
    /// <param name="strInput">คำที่ต้องการตรวจสอบ</param>
    /// <returns>คำที่ผ่านการตรวจอักขระพิเศษแล้ว</returns>
    /// <example>
    /// clsDefault.CodeFilter("สวัสดีครับ&nbsp;เราจะมาจัดการโค้ดต่างๆกันนะ เช่น 'โค้ดแบบนี้'");
    /// Output : สวัสดีครับ เราจะมาจัดการโค้ดต่างๆกันนะ เช่น ''โค้ดแบบนี้''
    /// </example>
    public string CodeFilter(string strInput)
    {
        if (string.IsNullOrEmpty(strInput)) { return ""; }

        string strOutput;

        strOutput = strInput.Trim();
        if (!string.IsNullOrEmpty(strOutput))
        {
            strOutput = strOutput.Replace("&nbsp;", " ");
            strOutput = strOutput.Replace("''", "'"); // กันไว้ กรณีที่เรา Replace เป็น '' มาแล้วรอบนึง
            strOutput = strOutput.Replace("'", "''");
        }

        return strOutput;
    }
    /// <summary>
    /// ตรวจสอบสถานะของฐานข้อมูลที่ต้องการว่าใช้งานได้หรือไม่
    /// </summary>
    /// <param name="dbType">ประเภทฐานข้อมูล (clsSQL.DBType.SQLServer)</param>
    /// <param name="appSettingConnectionName">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ web.config</param>
    /// <returns>True=ใช้งานได้ , False=ไม่สามารถใช้งานได้</returns>
    /// <example>
    /// clsSQL.IsConnected(clsSQL.DBType.SQLServer, "csCalendar"));
    /// clsSQL.IsConnected(clsSQL.DBType.MySQL, "cs")
    /// </example>
    public bool IsConnected(DBType dbType, string cs)
    {
        #region Variable
        bool result = false;
        string csSQL = getConnectionString(cs);
        #endregion
        #region Procedure
        if (string.IsNullOrEmpty(csSQL)) return false;

        switch (dbType)
        {
            case DBType.SQLServer:
                #region SQLServer
                using (var myConn = new SqlConnection(csSQL))
                {
                    try
                    {
                        myConn.Open();
                        result = true;
                    }
                    catch (SqlException)
                    {
                        result = false;
                    }
                    catch (Exception)
                    {
                        result = false;
                    }
                }
                #endregion
                break;
            case DBType.MySQL:
                #region MySQL
                using (var myConn = new MySql.Data.MySqlClient.MySqlConnection(csSQL))
                {
                    try
                    {
                        myConn.Open();
                        result = true;
                    }
                    catch (SqlException)
                    {
                        result = false;
                    }
                    catch (Exception)
                    {
                        result = false;
                    }
                }
                #endregion
                break;
            case DBType.ODBC:
                #region ODBC
                using (var myConn = new OdbcConnection(csSQL))
                {
                    try
                    {
                        myConn.Open();
                        result = true;
                    }
                    catch (SqlException)
                    {
                        result = false;
                    }
                    catch (Exception)
                    {
                        result = false;
                    }
                }
                #endregion
                break;
            default:
                break;
        }
        #endregion
        return result;
    }
    public bool IsConnected()
    {
        #region Variable
        bool result = false;
        string csSQL = getConnectionString(cs);
        #endregion
        #region Procedure
        if (string.IsNullOrEmpty(csSQL)) return false;

        switch (dbType)
        {
            case DBType.SQLServer:
                #region SQLServer
                using (var myConn = new SqlConnection(csSQL))
                {
                    try
                    {
                        myConn.Open();
                        result = true;
                    }
                    catch (SqlException)
                    {
                        result = false;
                    }
                    catch (Exception)
                    {
                        result = false;
                    }
                }
                #endregion
                break;
            case DBType.MySQL:
                #region MySQL
                using (var myConn = new MySql.Data.MySqlClient.MySqlConnection(csSQL))
                {
                    try
                    {
                        myConn.Open();
                        result = true;
                    }
                    catch (SqlException)
                    {
                        result = false;
                    }
                    catch (Exception)
                    {
                        result = false;
                    }
                }
                #endregion
                break;
            case DBType.ODBC:
                #region ODBC
                using (var myConn = new OdbcConnection(csSQL))
                {
                    try
                    {
                        myConn.Open();
                        result = true;
                    }
                    catch (SqlException)
                    {
                        result = false;
                    }
                    catch (Exception)
                    {
                        result = false;
                    }
                }
                #endregion
                break;
            default:
                break;
        }
        #endregion
        return result;
    }
    /// <summary>
    /// เปลี่ยนคำสั่ง SELECT ธรรมดา ให้กลายเป็นการ SELECT แบบกำหนดขนาดหน้า และ หน้าที่ต้องการได้
    /// </summary>
    /// <param name="strSQL">SELECT Query ปกติ (ตัด ORDER BY ... ออก)</param>
    /// <param name="messageError">ตัวแปรสำหรับรับค่า กรณีเกิดข้อผิดพลาด</param>
    /// <param name="orderBy">ชื่อฟิลด์ที่ต้องการให้เรียงลำดับ</param>
    /// <param name="pageSize">ขนาดหน้า</param>
    /// <param name="pageSelected">หน้าที่ต้องการข้อมูล</param>
    /// <returns>SELECT Query ที่มีการจัดหน้าแล้ว</returns>
    /// <example>
    /// clsSQL clsSQL = new clsSQL();
    /// string messageError = "";
    /// Response.Write(
    ///     clsSQL.SelectByPager(
    ///         "SELECT UID,Name,Detail,CWhen FROM MedicalCenter;",
    ///         out messageError,
    ///         "CWhen",
    ///         5,
    ///         2
    ///     )
    ///  );
    ///  #### OutPut ####
    ///  DECLARE @PageNum AS INT;DECLARE @PageSize AS INT;
    ///  SET @PageNum = 2;SET @PageSize = 5;
    ///  WITH DataPager AS(SELECT ROW_NUMBER() OVER(ORDER BY CWhen) AS RowNum, UID,Name,Detail,CWhen 
    ///  FROM MedicalCenter)
    ///  SELECT * FROM DataPager WHERE RowNum BETWEEN (@PageNum - 1) * @PageSize + 1 AND @PageNum * @PageSize;
    /// </example>
    public string SelectByPager(string strSQL, out string messageError, string orderBy = "Sort,CWhen DESC", int pageSize = 10, int pageSelected = 1)
    {
        #region Variable
        var result = "";
        var sqlSelect = "";
        var strSQLResult = new StringBuilder();
        messageError = "";
        #endregion
        #region Procedure
        if (!string.IsNullOrEmpty(strSQL))
        {
            #region Cleansing
            strSQL = strSQL.Trim();
            strSQL = strSQL.Replace("Select", "SELECT").Replace("select", "SELECT");
            #endregion
            if (strSQL.StartsWith("SELECT"))
            {
                #region RemoveLastSemicolon
                if (strSQL.EndsWith(";"))
                {
                    strSQL = System.Text.RegularExpressions.Regex.Replace(strSQL, ";$", "");
                }
                #endregion
                #region AddRowNumber
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("SELECT");
                sqlSelect = regex.Replace(strSQL, "SELECT ROW_NUMBER() OVER(ORDER BY " + orderBy + ") AS RowNum,", 1);
                #endregion
                #region SQLQuery
                strSQLResult.Append("DECLARE @PageNum AS INT;");
                strSQLResult.Append("DECLARE @PageSize AS INT;");
                strSQLResult.Append("SET @PageNum = "+pageSelected.ToString()+";");
                strSQLResult.Append("SET @PageSize = "+pageSize.ToString()+";");
                strSQLResult.Append("WITH DataPager AS");
                strSQLResult.Append("(");
                strSQLResult.Append(sqlSelect);
                strSQLResult.Append(")");
                strSQLResult.Append("SELECT ");
                strSQLResult.Append("* ");
                strSQLResult.Append("FROM ");
                strSQLResult.Append("DataPager ");
                strSQLResult.Append("WHERE ");
                strSQLResult.Append("RowNum BETWEEN (@PageNum - 1) * @PageSize + 1 AND @PageNum * @PageSize;");
                #endregion
                result = strSQLResult.ToString();
            }
            else
            {
                messageError += "StartsWith('SELECT') = False ";
            }
        }
        #endregion
        return result;
    }
    public string ConnectionStringToAppSetting(string connectionStringName)
    {
        #region Variable
        var result = "";
        var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        string[] csCal;
        #endregion
        #region ConnectionBuilder
        csCal = System.Text.RegularExpressions.Regex.Split(connectionString, "data source=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = "server=" + csCal[1].Split(';')[0] + ";";

        csCal = System.Text.RegularExpressions.Regex.Split(connectionString, "user id=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result += "uid=" + csCal[1].Split(';')[0] + ";";

        csCal = System.Text.RegularExpressions.Regex.Split(connectionString, "password=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result += "pwd=" + csCal[1].Split(';')[0] + ";";

        csCal = System.Text.RegularExpressions.Regex.Split(connectionString, "initial catalog=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result += "database=" + csCal[1].Split(';')[0] + ";";
        #endregion
        return result;
    }
    /// <summary>
    /// กรอง Query ตามประเภทฐานข้อมูลต่างๆ เช่น MySQL ห้ามมี [] แต่ SQLServer มีได้ หรือ GETDATE() และ NOW()
    /// </summary>
    /// <param name="Query"></param>
    /// <returns></returns>
    private string QueryFilterByDatabaseType(string Query)
    {
        #region Variable
        var result = Query;
        #endregion
        #region Procedure
        switch (dbType)
        {
            case DBType.SQLServer:
                break;
            case DBType.MySQL:
                result = result.Replace("[", "");
                result = result.Replace("]", "");
                result = result.Replace("GETDATE()", "NOW()");
                break;
            case DBType.ODBC:
                break;
            default:
                break;
        }
        #endregion
        return result;
    }
}

public static class clsSQLExtension
{
    /// <summary>
    /// กรองอักขระพิเศษที่ปนมากับ SQL Query Statement
    /// </summary>
    /// <param name="sqlQuery">SQL Query</param>
    /// <returns>SQL Query ที่ถูกกรองอักขระพิเศษออกแล้ว</returns>
    /// <example>
    /// "INSERT INTO Test(Detail)VALUES('" + txtDetail.Text.SQLQueryFilter() + "');";
    /// </example>
    public static string SQLQueryFilter(this string sqlQuery)
    {
        #region Variable
        string sqlQueryFiltered;
        string[,] wordReplaces =
            new string[,]{
                {"&nbsp;"," "},
                {"''","'"},
                {"'","''"}};
        #endregion
        #region Error Checker
        if (string.IsNullOrEmpty(sqlQuery.Trim())) { return ""; }
        #endregion
        #region SQL Query Filter
        sqlQueryFiltered = sqlQuery.Trim();

        for (int i = 0; i < wordReplaces.Length / wordReplaces.Rank; i++)
        {
            sqlQueryFiltered = sqlQueryFiltered.Replace(wordReplaces[i, 0], wordReplaces[i, 1]);
        }
        #endregion

        return sqlQueryFiltered;
    }
    /// <summary>
    /// ตรวจสอบสถานะการเชื่อมต่อฐานข้อมูล
    /// </summary>
    /// <param name="connection"></param>
    /// <returns></returns>
    /// <example>
    /// SqlConnection myConn = new SqlConnection("server=10.121.10.7;uid=kpi;pwd=kpi;database=BRHDB;");
    /// if (myConn.IsConnected())
    /// {
    ///     Response.Write("ฐานข้อมูลใช้ได้นะ");
    /// }
    /// else
    /// {
    ///     Response.Write("ต่อฐานข้อมูลไม่ได้เลย");
    /// }
    /// </example>
    public static bool IsConnected(this SqlConnection connection)
    {
        try
        {
            connection.Open();
            connection.Close();
        }
        catch (SqlException)
        {
            return false;
        }

        return true;
    }
    /// <summary>
    /// ตรวจสอบสถานะการเชื่อมต่อฐานข้อมูล
    /// </summary>
    /// <param name="connection"></param>
    /// <returns></returns>
    /// <example>
    /// MySqlConnection myConn = new MySqlConnection("server=10.121.10.7;uid=kpi;pwd=kpi;database=BRHDB;");
    /// if (myConn.IsConnected())
    /// {
    ///     Response.Write("ฐานข้อมูลใช้ได้นะ");
    /// }
    /// else
    /// {
    ///     Response.Write("ต่อฐานข้อมูลไม่ได้เลย");
    /// }
    /// </example>
    public static bool IsConnected(this MySql.Data.MySqlClient.MySqlConnection connection)
    {
        try
        {
            connection.Open();
            connection.Close();
        }
        catch (SqlException)
        {
            return false;
        }

        return true;
    }
    /// <summary>
    /// ตรวจสอบสถานะการเชื่อมต่อฐานข้อมูล
    /// </summary>
    /// <param name="connection"></param>
    /// <returns></returns>
    /// <example>
    /// MySqlConnection myConn = new MySqlConnection("server=10.121.10.7;uid=kpi;pwd=kpi;database=BRHDB;");
    /// if (myConn.IsConnected())
    /// {
    ///     Response.Write("ฐานข้อมูลใช้ได้นะ");
    /// }
    /// else
    /// {
    ///     Response.Write("ต่อฐานข้อมูลไม่ได้เลย");
    /// }
    /// </example>
    public static bool IsConnected(this OdbcConnection connection)
    {
        try
        {
            connection.Open();
            connection.Close();
        }
        catch (SqlException)
        {
            return false;
        }

        return true;
    }
}