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
    public class dboPermissionsCommands
    {
        public string _conn { get; set; } = "";


        public dboPermissionsCommands()
        {
            _conn = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();

        }
        public dboPermissionsCommands(string conn)
        {
            _conn = conn;
        }

        #region Insert
        public int createPermissionIntoDb(string name, string description)
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("InsertPermission", conn))
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;

                    SqlParameter _paramPermissionName = _sqlCommand.CreateParameter();
                    _paramPermissionName.DbType = DbType.String; //set type
                    _paramPermissionName.ParameterName = "@PermissionName"; //set name
                    _paramPermissionName.Value = name; //set value 
                    _sqlCommand.Parameters.Add(_paramPermissionName);

                    SqlParameter _paramPermDesc = _sqlCommand.CreateParameter();
                    _paramPermDesc.DbType = DbType.String; //set type
                    _paramPermDesc.ParameterName = "@PermissionDescription"; //set name
                    _paramPermDesc.Value = description; //set value 
                    _sqlCommand.Parameters.Add(_paramPermDesc);


                    SqlParameter _paramPermIDReturn = _sqlCommand.CreateParameter();
                    _paramPermIDReturn.DbType = DbType.Int32; //set type
                    _paramPermIDReturn.ParameterName = "@ParamOutPermID"; //set name
                    var pk = _sqlCommand.Parameters.Add(_paramPermIDReturn);
                    _paramPermIDReturn.Direction = ParameterDirection.Output; //make it output 

                    conn.Open();
                    _sqlCommand.ExecuteNonQuery(); //call sp
                    var result = _paramPermIDReturn.Value; //has auto incremented value returned 
                    //r.RoleID = (int)result;
                    conn.Close();
                    return (int)result;
                }
            }
        }
        #endregion

        #region Select
        public List<object[]> selectAllPermissionsInDb()
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("SelectALLPermissions", conn))
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

        #endregion

        #region Update
        public void updatePermissonInDb(int permID, string permName, string permDescription, int roleID)
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("UpdatePermission", conn))
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;

                    SqlParameter _paramPermID = _sqlCommand.CreateParameter();
                    _paramPermID.DbType = DbType.Int32; //set type
                    _paramPermID.ParameterName = "@PermissionID"; //set name
                    _paramPermID.Value = permID; //set value 
                    _sqlCommand.Parameters.Add(_paramPermID);

                    SqlParameter _paramPermName = _sqlCommand.CreateParameter();
                    _paramPermName.DbType = DbType.String; //set type
                    _paramPermName.ParameterName = "@PermissionName"; //set name
                    _paramPermName.Value = permName; //set value 
                    _sqlCommand.Parameters.Add(_paramPermName);

                    SqlParameter _paramPermDesc = _sqlCommand.CreateParameter();
                    _paramPermDesc.DbType = DbType.String; //set type
                    _paramPermDesc.ParameterName = "@PermissionDescription"; //set name
                    _paramPermDesc.Value = permDescription; //set value 
                    _sqlCommand.Parameters.Add(_paramPermDesc);

                    SqlParameter _paramRoleID = _sqlCommand.CreateParameter();
                    _paramRoleID.DbType = DbType.Int32; //set type
                    _paramRoleID.ParameterName = "@RoleID"; //set name
                    _paramRoleID.Value = roleID; //set value 
                    _sqlCommand.Parameters.Add(_paramRoleID);


                    conn.Open();
                    _sqlCommand.ExecuteNonQuery(); //call sp
                    conn.Close();

                }
            }
        }
        #endregion

        #region Delete
        public void deletePermissionByNameInDb(string name)
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("DeletePermissionByName", conn))
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;


                    SqlParameter _paramPermName = _sqlCommand.CreateParameter();
                    _paramPermName.DbType = DbType.String; //set type
                    _paramPermName.ParameterName = "@PermissionName"; //set name
                    _paramPermName.Value = name; //set value 
                    _sqlCommand.Parameters.Add(_paramPermName);

                    conn.Open();
                    _sqlCommand.ExecuteNonQuery(); //call sp
                    conn.Close();

                }
            }
        }
        #endregion

    }
}
