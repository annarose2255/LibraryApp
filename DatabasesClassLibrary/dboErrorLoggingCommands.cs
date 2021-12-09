using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using CommonClassLibrary;

namespace DatabasesClassLibrary
{
    public class dboErrorLoggingCommands
    {

        public string _conn { get; set; } = "";


        public dboErrorLoggingCommands()
        {
            _conn = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();

        }
        public dboErrorLoggingCommands(string conn)
        {
            _conn = conn;
            // _conn = @"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryApp;Persist Security Info=True;";
        }

        public int createLogException(Exception e)
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("CreateLogException", conn))
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;

                    SqlParameter _paramStackTrace = _sqlCommand.CreateParameter();
                    _paramStackTrace.DbType = DbType.String; //set type
                    _paramStackTrace.ParameterName = "@parmStackTrace"; //set name
                    _paramStackTrace.Value = e.StackTrace; //set value 
                    _sqlCommand.Parameters.Add(_paramStackTrace);

                    SqlParameter _paramMessage = _sqlCommand.CreateParameter();
                    _paramMessage.DbType = DbType.String; //set type
                    _paramMessage.ParameterName = "@parmMessage"; //set name
                    _paramMessage.Value = e.Message; //set value 
                    _sqlCommand.Parameters.Add(_paramMessage);

                    SqlParameter _paramSource = _sqlCommand.CreateParameter();
                    _paramSource.DbType = DbType.String; //set type
                    _paramSource.ParameterName = "@parmSource"; //set name
                    _paramSource.Value = e.Source; //set value 
                    _sqlCommand.Parameters.Add(_paramSource);

                    //???
                    //SqlParameter _paramURL = _sqlCommand.CreateParameter();
                    //_paramURL.DbType = DbType.String; //set type
                    //_paramURL.ParameterName = "@parmURL"; //set name
                    //_paramURL.Value = e.HelpLink; //set value 
                    //_sqlCommand.Parameters.Add(_paramURL);

                    SqlParameter _paramLogDate = _sqlCommand.CreateParameter();
                    _paramLogDate.DbType = DbType.DateTime; //set type
                    _paramLogDate.ParameterName = "@parmLogdate"; //set name
                    _paramLogDate.Value = DateTime.Now; //set value 
                    _sqlCommand.Parameters.Add(_paramLogDate);


                    SqlParameter _paramErrorIDReturn = _sqlCommand.CreateParameter();
                    _paramErrorIDReturn.DbType = DbType.Int32; //set type
                    _paramErrorIDReturn.ParameterName = "@parmOutExceptionLoggingID"; //set name
                    //_paramErrorID.Value = u.RoleID; //set value 
                    var pk = _sqlCommand.Parameters.Add(_paramErrorIDReturn);
                    _paramErrorIDReturn.Direction = ParameterDirection.Output; //make it output 



                    conn.Open();
                    _sqlCommand.ExecuteNonQuery(); //call sp
                    var result = _paramErrorIDReturn.Value; //has auto incremented value returned 
                    //u.UserId = (int)result;
                    conn.Close();
                    return (int)result;
                }
            }
        }

        public List<object[]> selectAllLogErrors()
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("SelectAllLogException", conn))
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;



                    conn.Open();
                    DataTable dt = new DataTable();
                    SqlDataReader reader = _sqlCommand.ExecuteReader();
                    List<object[]> rows = new List<object[]>();
                    while (reader.Read())//read through the db
                    {
                        object[] values = new object[6];
                        reader.GetValues(values); //get the values of the row
                        rows.Add(values); //add the row's values to the list

                    }
                    reader.Close();

                    conn.Close();
                    reader.Close();
                    return rows;

                }
            }
        }


    }
}
