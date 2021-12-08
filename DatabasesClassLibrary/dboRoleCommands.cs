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
    public class dboRoleCommands
    {
        public string _conn { get; set; } = "";


        public dboRoleCommands()
        {
            _conn = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();
            // _conn = @"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryApp;Persist Security Info=True;";
            //_conn = @"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryApp;Integrated Security=True";
        }
        public dboRoleCommands(string conn)
        {
            _conn = conn;
            // _conn = @"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryApp;Persist Security Info=True;";
        }

        #region Role Insert
        public int createRoleIntoDb(RoleDTO r)
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("InsertRole", conn))
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;

                    SqlParameter _paramRoleName = _sqlCommand.CreateParameter();
                    _paramRoleName.DbType = DbType.String; //set type
                    _paramRoleName.ParameterName = "@RoleName"; //set name
                    _paramRoleName.Value = r.RoleName; //set value 
                    _sqlCommand.Parameters.Add(_paramRoleName);

                    SqlParameter _paramRoleDesc = _sqlCommand.CreateParameter();
                    _paramRoleDesc.DbType = DbType.String; //set type
                    _paramRoleDesc.ParameterName = "@RoleDescription"; //set name
                    _paramRoleDesc.Value = r.RoleDescription; //set value 
                    _sqlCommand.Parameters.Add(_paramRoleDesc);


                    SqlParameter _paramRoleIDReturn = _sqlCommand.CreateParameter();
                    _paramRoleIDReturn.DbType = DbType.Int32; //set type
                    _paramRoleIDReturn.ParameterName = "@ParamOutRoleID"; //set name
                    var pk = _sqlCommand.Parameters.Add(_paramRoleIDReturn);
                    _paramRoleIDReturn.Direction = ParameterDirection.Output; //make it output 

                    conn.Open();
                    _sqlCommand.ExecuteNonQuery(); //call sp
                    var result = _paramRoleIDReturn.Value; //has auto incremented value returned 
                    r.RoleID = (int)result;
                    conn.Close();
                    return (int)result;
                }
            }
        }
        #endregion

        #region Role Select
        public List<object[]> selectAllRolesInDb()
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("SelectAllRoles", conn))
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;



                    conn.Open();
                    DataTable dt = new DataTable();
                    SqlDataReader reader = _sqlCommand.ExecuteReader();
                    List<object[]> rows = new List<object[]>();
                    while (reader.Read())//read through the db
                    {
                        object[] values = new object[3];
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
        public List<object[]> selectRoleByIDInDb(int roleID)
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("SelectRoleByID", conn))
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;

                    SqlParameter _paramRoleID = _sqlCommand.CreateParameter();
                    _paramRoleID.DbType = DbType.Int32; //set type
                    _paramRoleID.ParameterName = "@RoleID"; //set name
                    _paramRoleID.Value = roleID; //set value 
                    _sqlCommand.Parameters.Add(_paramRoleID);


                    conn.Open();
                    DataTable dt = new DataTable();
                    SqlDataReader reader = _sqlCommand.ExecuteReader();
                    List<object[]> rows = new List<object[]>();
                    while (reader.Read())//read through the db
                    {
                        object[] values = new object[3];
                        reader.GetValues(values); //get the values of the row
                        rows.Add(values); //add the row's values to the list

                    }
                    reader.Close();

                    conn.Close();
                    return rows;

                }
            }
        }
        #endregion


        #region Role Delete
        public void deleteRoleByIDInDb(int roleId)
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("DeleteRoleByID", conn))
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;

                    SqlParameter _paramRoleID = _sqlCommand.CreateParameter();
                    _paramRoleID.DbType = DbType.String; //set type
                    _paramRoleID.ParameterName = "@RoleID"; //set name
                    _paramRoleID.Value = roleId; //set value 
                    _sqlCommand.Parameters.Add(_paramRoleID);

                    conn.Open();

                    _sqlCommand.ExecuteNonQuery(); //call sp

                    conn.Close();
                  

                }
            }
        }
        #endregion

        #region Role Update
        public void updateRoleInDb(int roleID, string roleName, string roleDescription)
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("UpdateRole", conn))
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;

                    SqlParameter _paramRoleID = _sqlCommand.CreateParameter();
                    _paramRoleID.DbType = DbType.Int32; //set type
                    _paramRoleID.ParameterName = "@RoleID"; //set name
                    _paramRoleID.Value = roleID; //set value 
                    _sqlCommand.Parameters.Add(_paramRoleID);

                    SqlParameter _paramRoleName = _sqlCommand.CreateParameter();
                    _paramRoleName.DbType = DbType.String; //set type
                    _paramRoleName.ParameterName = "@RoleName"; //set name
                    _paramRoleName.Value = roleName; //set value 
                    _sqlCommand.Parameters.Add(_paramRoleName);

                    SqlParameter _paramRoleDesc = _sqlCommand.CreateParameter();
                    _paramRoleDesc.DbType = DbType.String; //set type
                    _paramRoleDesc.ParameterName = "@RoleDescription"; //set name
                    _paramRoleDesc.Value = roleDescription; //set value 
                    _sqlCommand.Parameters.Add(_paramRoleDesc);


                   

                    conn.Open();
                    _sqlCommand.ExecuteNonQuery(); //call sp
                    conn.Close();
                    
                }
            }
        }
        #endregion
    }
}
