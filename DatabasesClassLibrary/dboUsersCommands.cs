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
    public class dboUsersCommands
    {
        public string _conn { get; set; } = "";


        public dboUsersCommands()
        {
            _conn = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();

        }
        public dboUsersCommands(string conn)
        {
            _conn = conn;
            // _conn = @"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryApp;Persist Security Info=True;";
        }
        #region User Insert
        public int createUserIntoDb(UserDTO u)
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("InsertUser", conn))
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;

                    SqlParameter _paramUserName = _sqlCommand.CreateParameter();
                    _paramUserName.DbType = DbType.String; //set type
                    _paramUserName.ParameterName = "@UserName"; //set name
                    _paramUserName.Value = u.UserName; //set value 
                    _sqlCommand.Parameters.Add(_paramUserName);

                    SqlParameter _paramPassword = _sqlCommand.CreateParameter();
                    _paramPassword.DbType = DbType.String; //set type
                    _paramPassword.ParameterName = "@Password"; //set name
                    _paramPassword.Value = u.Password; //set value 
                    _sqlCommand.Parameters.Add(_paramPassword);

                    SqlParameter _paramFirstName = _sqlCommand.CreateParameter();
                    _paramFirstName.DbType = DbType.String; //set type
                    _paramFirstName.ParameterName = "@FirstName"; //set name
                    _paramFirstName.Value = u.FirstName; //set value 
                    _sqlCommand.Parameters.Add(_paramFirstName);

                    SqlParameter _paramLastName = _sqlCommand.CreateParameter();
                    _paramLastName.DbType = DbType.String; //set type
                    _paramLastName.ParameterName = "@LastName"; //set name
                    _paramLastName.Value = u.LastName; //set value 
                    _sqlCommand.Parameters.Add(_paramLastName);

                    SqlParameter _paramRoleID = _sqlCommand.CreateParameter();
                    _paramRoleID.DbType = DbType.Int32; //set type
                    _paramRoleID.ParameterName = "@RoleID"; //set name
                    if (u.RoleID > -1)
                    {
                        _paramRoleID.Value = u.RoleID; //set value 
                    }
                    _sqlCommand.Parameters.Add(_paramRoleID);

                    SqlParameter _paramUserIDReturn = _sqlCommand.CreateParameter();
                    _paramUserIDReturn.DbType = DbType.Int32; //set type
                    _paramUserIDReturn.ParameterName = "@ParamOutUserID"; //set name
                    var pk = _sqlCommand.Parameters.Add(_paramUserIDReturn);
                    _paramUserIDReturn.Direction = ParameterDirection.Output; //make it output 

                    conn.Open();
                    _sqlCommand.ExecuteNonQuery(); //call sp
                    var result = _paramUserIDReturn.Value; //has auto incremented value returned 
                    u.UserId = (int)result;
                    conn.Close();
                    return (int)result;
                }
            }
        }
        #endregion

        #region User Select
        public List<object[]> selectAllUsersInDb()
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("SelectAllUsers", conn))
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

        public List<object[]> selectUserByIDInDb(int id)
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("SelectUserByID", conn))
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;

                    SqlParameter _paramUserID = _sqlCommand.CreateParameter();
                    _paramUserID.DbType = DbType.String; //set type
                    _paramUserID.ParameterName = "@UserID"; //set name
                    _paramUserID.Value = id; //set value 
                    _sqlCommand.Parameters.Add(_paramUserID);

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

        public List<object[]> selectUserAndRoleNameByIDInDb(int id)
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("SelectUserandRoleNameByID", conn))
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;

                    SqlParameter _paramUserID = _sqlCommand.CreateParameter();
                    _paramUserID.DbType = DbType.String; //set type
                    _paramUserID.ParameterName = "@UserID"; //set name
                    _paramUserID.Value = id; //set value 
                    _sqlCommand.Parameters.Add(_paramUserID);

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

        public List<object[]> selectAllUsersAndRoleNameInDb()
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("SelectAllUserandRoleName", conn))
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


        #region User Update
        //public void updateUserByIDInDb(int userid, string username, string password, string firstname, string lastname, int roleid)
        //{

        //    using (SqlConnection conn = new SqlConnection(_conn))
        //    {
        //        using (SqlCommand _sqlCommand = new SqlCommand("UpdateUserByID", conn))
        //        {
        //            _sqlCommand.CommandType = CommandType.StoredProcedure;
        //            _sqlCommand.CommandTimeout = 30;

        //            SqlParameter _paramUserId = _sqlCommand.CreateParameter();
        //            _paramUserId.DbType = DbType.String; //set type
        //            _paramUserId.ParameterName = "@UserID"; //set name
        //            _paramUserId.Value = userid; //set value 
        //            _sqlCommand.Parameters.Add(_paramUserId);

        //            SqlParameter _paramUserName = _sqlCommand.CreateParameter();
        //            _paramUserName.DbType = DbType.String; //set type
        //            _paramUserName.ParameterName = "@UserName"; //set name
        //            if (username != "") //if there is a value to change
        //            {
        //                _paramUserName.Value = username; //set value 
        //            }
        //            _sqlCommand.Parameters.Add(_paramUserName);

        //            SqlParameter _paramPassword = _sqlCommand.CreateParameter();
        //            _paramPassword.DbType = DbType.String; //set type
        //            _paramPassword.ParameterName = "@Password"; //set name
        //            if (password != "") //if there is a value to change
        //            {
        //                _paramPassword.Value = password; //set value 
        //            }
        //            _sqlCommand.Parameters.Add(_paramPassword);

        //            SqlParameter _paramFirstName = _sqlCommand.CreateParameter();
        //            _paramFirstName.DbType = DbType.String; //set type
        //            _paramFirstName.ParameterName = "@FirstName"; //set name
        //            if (firstname != "") //if there is a value to change
        //            {
        //                _paramFirstName.Value = firstname; //set value 
        //            }
        //            _sqlCommand.Parameters.Add(_paramFirstName);

        //            SqlParameter _paramLastName = _sqlCommand.CreateParameter();
        //            _paramLastName.DbType = DbType.String; //set type
        //            _paramLastName.ParameterName = "@LastName"; //set name
        //            if (lastname != "") //if there is a value to change
        //            {
        //                _paramLastName.Value = lastname; //set value 
        //            }
        //            _sqlCommand.Parameters.Add(_paramLastName);

        //            SqlParameter _paramRoleID = _sqlCommand.CreateParameter();
        //            _paramRoleID.DbType = DbType.Int32; //set type
        //            _paramRoleID.ParameterName = "@RoleID"; //set name
        //            if (roleid > -1)
        //            {
        //                _paramRoleID.Value = roleid; //set value 
        //            }
        //            _sqlCommand.Parameters.Add(_paramRoleID);

        //            conn.Open();
        //            _sqlCommand.ExecuteNonQuery(); //call sp
        //            conn.Close();
        //        }
        //    }
        //}

        public void updateUserUsernameInDb(int userid, string username)
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("UpdateUserUsername", conn))
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;

                    SqlParameter _paramUserId = _sqlCommand.CreateParameter();
                    _paramUserId.DbType = DbType.String; //set type
                    _paramUserId.ParameterName = "@UserID"; //set name
                    _paramUserId.Value = userid; //set value 
                    _sqlCommand.Parameters.Add(_paramUserId);

                    SqlParameter _paramUserName = _sqlCommand.CreateParameter();
                    _paramUserName.DbType = DbType.String; //set type
                    _paramUserName.ParameterName = "@UserName"; //set name
                    _paramUserName.Value = username; //set value 
                    _sqlCommand.Parameters.Add(_paramUserName);

                    conn.Open();
                    _sqlCommand.ExecuteNonQuery(); //call sp
                    conn.Close();
                }
            }
        }

        public void updateUserPasswordInDb(int userid, string password)
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("UpdateUserPassword", conn))
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;

                    SqlParameter _paramUserId = _sqlCommand.CreateParameter();
                    _paramUserId.DbType = DbType.String; //set type
                    _paramUserId.ParameterName = "@UserID"; //set name
                    _paramUserId.Value = userid; //set value 
                    _sqlCommand.Parameters.Add(_paramUserId);


                    SqlParameter _paramPassword = _sqlCommand.CreateParameter();
                    _paramPassword.DbType = DbType.String; //set type
                    _paramPassword.ParameterName = "@Password"; //set name
                    _paramPassword.Value = password; //set value 
                    _sqlCommand.Parameters.Add(_paramPassword);

                    conn.Open();
                    _sqlCommand.ExecuteNonQuery(); //call sp
                    conn.Close();
                }
            }
        }

        public void updateUserFirstNameInDb(int userid, string firstname)
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("UpdateUserFirstName", conn))
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;

                    SqlParameter _paramUserId = _sqlCommand.CreateParameter();
                    _paramUserId.DbType = DbType.String; //set type
                    _paramUserId.ParameterName = "@UserID"; //set name
                    _paramUserId.Value = userid; //set value 
                    _sqlCommand.Parameters.Add(_paramUserId);

                    SqlParameter _paramFirstName = _sqlCommand.CreateParameter();
                    _paramFirstName.DbType = DbType.String; //set type
                    _paramFirstName.ParameterName = "@FirstName"; //set name
                    if (firstname != "") //if there is a value to change
                    {
                        _paramFirstName.Value = firstname; //set value 
                    }
                    _sqlCommand.Parameters.Add(_paramFirstName);


                    conn.Open();
                    _sqlCommand.ExecuteNonQuery(); //call sp
                    conn.Close();
                }
            }
        }

        public void updateUserLastNameInDb(int userid, string lastname)
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("UpdateUserLastName", conn))
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;

                    SqlParameter _paramUserId = _sqlCommand.CreateParameter();
                    _paramUserId.DbType = DbType.String; //set type
                    _paramUserId.ParameterName = "@UserID"; //set name
                    _paramUserId.Value = userid; //set value 
                    _sqlCommand.Parameters.Add(_paramUserId);

                    SqlParameter _paramLastName = _sqlCommand.CreateParameter();
                    _paramLastName.DbType = DbType.String; //set type
                    _paramLastName.ParameterName = "@LastName"; //set name
                    if (lastname != "") //if there is a value to change
                    {
                        _paramLastName.Value = lastname; //set value 
                    }
                    _sqlCommand.Parameters.Add(_paramLastName);

                    conn.Open();
                    _sqlCommand.ExecuteNonQuery(); //call sp
                    conn.Close();
                }
            }
        }

        public void updateUserRoleIDInDb(int userid, int roleid)
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("UpdateUserRoleID", conn))
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;

                    SqlParameter _paramUserId = _sqlCommand.CreateParameter();
                    _paramUserId.DbType = DbType.String; //set type
                    _paramUserId.ParameterName = "@UserID"; //set name
                    _paramUserId.Value = userid; //set value 
                    _sqlCommand.Parameters.Add(_paramUserId);

                    SqlParameter _paramRoleID = _sqlCommand.CreateParameter();
                    _paramRoleID.DbType = DbType.Int32; //set type
                    _paramRoleID.ParameterName = "@RoleID"; //set name
                    if (roleid > -1)
                    {
                        _paramRoleID.Value = roleid; //set value 
                    }
                    _sqlCommand.Parameters.Add(_paramRoleID);

                    conn.Open();
                    _sqlCommand.ExecuteNonQuery(); //call sp
                    conn.Close();
                }
            }
        }
        #endregion


        #region User Delete
        public void deleteUserByIDInDb(int userid)
        {

            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand _sqlCommand = new SqlCommand("DeleteUserByID", conn))
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;


                    SqlParameter _paramUserId = _sqlCommand.CreateParameter();
                    _paramUserId.DbType = DbType.String; //set type
                    _paramUserId.ParameterName = "@UserID"; //set name
                    _paramUserId.Value = userid; //set value 
                    _sqlCommand.Parameters.Add(_paramUserId);

                    conn.Open();
                    _sqlCommand.ExecuteNonQuery(); //call sp
                    conn.Close();

                }
            }
        }
        #endregion
    }
}
/*

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
*/
