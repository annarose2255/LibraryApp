using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClassLibrary;
using DatabasesClassLibrary;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace LibraryApp
{
    internal class Program
    {
        /// <summary>
        /// Method that creates a role into the Roles database. returns the role id of the created role.
        /// </summary>
        /// <param name="r">The roles database to connect to</param>
        /// <param name="error">The error database to connect to</param>
        /// <returns>The role id of the created role. </returns>
        public static int createRole(dboRoleCommands r, dboErrorLoggingCommands error)
        {
            bool goodname = false;
            string name;
            do //repeat input for good name
            {
                Console.WriteLine("Please enter the name of the role: ");
                name = Console.ReadLine().Trim();
                try
                {
                    List<object[]> rolenameSelect = r.selectRoleNameByNameInDb(name);
                    if (rolenameSelect.Count > 0) //we got role names in the db equal to input, we can't create new role = throw exception
                    {
                        throw new Exception("RoleName already exists in the System");
                    }
                    goodname = true;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR from database");
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                    error.createLogException(ex);
                    goodname = false;
                }

            } while (!goodname);

            Console.WriteLine("Enter a description of the role if you wish: ");
            string description = Console.ReadLine().Trim();


            RoleDTO newRole = new RoleDTO(0, name, description);
            try
            {
                r.createRoleIntoDb(newRole);
                //int newRoleId = (int) r.selectAllRolesInDb().Last()[0];
                //Console.WriteLine(newRole.RoleID);
                return newRole.RoleID;


            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR from database");
                Console.WriteLine(ex.Message);
                //Console.WriteLine(ex.StackTrace);
                Console.ResetColor();
                error.createLogException(ex);
            }
            return -1;
        }
        /// <summary>
        /// Method to edit the user by the given user id.
        /// </summary>
        /// <param name="id">Id of the user to edit</param>
        /// <param name="u"> Class to call the SPs for accessing the users DB</param>
        /// <param name="error">Class to call the SPs for accessing the error DB</param>
        /// <param name="r">Class to call the SPs for accessing the roles DB</param>
        /// <returns>The id of the user edited. Returns -1 if the user was deleted.</returns>
        public static int editUser(int id, dboUsersCommands u, dboErrorLoggingCommands error, dboRoleCommands r)
        {

            //List<object[]> userExist = selectUserByIDInDb(id);
            //if (user != null)
            //{
            int choice = -1;
            string edit = "yes";
            do //for repeat asking edits
            {
                
                bool notMadeGoodChoice = false;
                do //for error handleing
                {
                    notMadeGoodChoice = false;
                    Console.WriteLine("What would you like to edit? please enter number of field. " +
                "\n 0: First Name, 1: Last name, 2: Username, 3: Password, 4: Role, 5: Delete Profile");
                    string strChoice = Console.ReadLine().Trim();
                    try
                    {
                        choice = Convert.ToInt32(strChoice); //got choice now we can come out of this do while
                        if (choice >= 6 || choice <= -1)
                        {
                            throw new Exception("Did not enter one of the given choices");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("ERROR: Did not enter one of the given choices!");
                        error.createLogException(ex);
                        Console.ResetColor();
                        notMadeGoodChoice = true;
                    }

                } while (notMadeGoodChoice);
                switch (choice)
                {
                    case 0:
                        //NEED TRY CATCH??
                        Console.WriteLine("Please enter new First Name: ");
                        string newFirstName = Console.ReadLine().Trim();
                        u.updateUserFirstNameInDb(id, newFirstName);
                        //print changed profile
                        List<object[]> Profile = u.selectUserAndRoleNameByIDInDb(id);
                        AllPrinter.printAllUsersProfilesInDb(Profile);
                        break;

                    case 1:
                        Console.WriteLine("Please enter new Last Name: ");
                        string newLastName = Console.ReadLine().Trim();
                        u.updateUserLastNameInDb(id, newLastName);
                        //print changed profile
                        List<object[]> Profile2 = u.selectUserAndRoleNameByIDInDb(id);
                        AllPrinter.printAllUsersProfilesInDb(Profile2);
                        break;
                    case 2:
                        bool goodUsername = false;
                        do
                        {
                            //goodUsername = false;
                            Console.WriteLine("Please enter new Username: ");
                            string newUserName = Console.ReadLine().Trim();
                            try
                            {
                                u.updateUserUsernameInDb(id, newUserName);
                                goodUsername = true;
                                //print changed profile
                                List<object[]> Profile3 = u.selectUserAndRoleNameByIDInDb(id);
                                AllPrinter.printAllUsersProfilesInDb(Profile3);
                            }
                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("ERROR from database");
                                if (ex.Message.Contains("Violation of UNIQUE KEY constraint 'UQ__Users__C9F28456E0F51A72'"))
                                {
                                    //ex.Message = "The entered username already exists in ";
                                    Console.WriteLine("The username '" + newUserName + "' already exists in the system.");
                                }
                                else
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                //  Console.WriteLine(ex.Source);
                                Console.ResetColor();
                                error.createLogException(ex);
                                goodUsername = false;
                            }



                        } while (!goodUsername);
                        break;

                    case 3:
                        Console.WriteLine("Please enter new Password: ");
                        string newPassword = Console.ReadLine().Trim();
                        u.updateUserPasswordInDb(id, newPassword);
                        //print changed profile
                        List<object[]> Profile4 = u.selectUserAndRoleNameByIDInDb(id);
                        AllPrinter.printAllUsersProfilesInDb(Profile4);
                        break;
                    case 4: //role
                        do
                        { //for error handling 
                            notMadeGoodChoice = false;
                            Console.WriteLine("Do you wish to make your role one of the currently existing ones? Or create a new one? " +
                                              "\n Please enter 0 to make your role one of the currently existing ones, or 1 to create a new role");

                            try
                            {
                                string strRoleChoice = Console.ReadLine().Trim();
                                int roleChoice = Convert.ToInt32(strRoleChoice);

                                if (roleChoice < 0 || roleChoice > 1) //check that its one of the choices
                                {
                                    throw new Exception("Did Not enter a given choice for editing user profile's role");
                                }
                                switch (roleChoice)
                                {
                                    case 0:
                                        bool goodId = false;
                                        int roleID = -1;
                                        do //loop for retrying role id
                                        {
                                            Console.WriteLine("Please enter the role's id or 'list' to list all the possible roles: ");
                                            string strNewRoleID = Console.ReadLine().Trim();
                                            if (strNewRoleID == "list")
                                            {
                                                List<object[]> rows = r.selectAllRolesInDb();
                                                AllPrinter.printAllRolesInDb(rows);
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    roleID = Convert.ToInt32(strNewRoleID);

                                                    u.updateUserRoleIDInDb(id, roleID);
                                                    goodId = true;
                                                    List<object[]> Profile5 = u.selectUserAndRoleNameByIDInDb(id);
                                                    AllPrinter.printAllUsersProfilesInDb(Profile5);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    //Console.WriteLine("ERROR from database");

                                                    //WRONG!!!
                                                    if (ex.Message.Contains("statement conflicted with the FOREIGN KEY constraint "))
                                                    {
                                                        Console.WriteLine("The Role ID '" + roleID + "' does not exist in the system.");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine(ex.Message);
                                                    }
                                                    //  Console.WriteLine(ex.Source);
                                                    Console.ResetColor();
                                                    error.createLogException(ex);
                                                    goodId = false;
                                                }
                                            }
                                        } while (!goodId);
                                        break;
                                    case 1:
                                        try
                                        {
                                            int newRolesId;
                                            do //repeat creating a new role while a new role was not created (i.e. method returned -1)
                                            {
                                                newRolesId = createRole(r, error);
                                            } while (newRolesId == -1);

                                            u.updateUserRoleIDInDb(id, newRolesId);
                                            //print changed profile
                                            List<object[]> Profile5 = u.selectUserAndRoleNameByIDInDb(id);
                                            AllPrinter.printAllUsersProfilesInDb(Profile5);
                                            return id;

                                        }
                                        catch (Exception ex)
                                        {
                                            //???
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("ERROR! Please try again");
                                            Console.WriteLine(ex.Message);
                                            Console.ResetColor();
                                            error.createLogException(ex);
                                            Console.ResetColor();
                                            return id; //??
                                        }

                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("ERROR: Did not enter one of the given choices!");
                                Console.ResetColor();
                                notMadeGoodChoice = true; //repeat the loop
                            }
                        } while (notMadeGoodChoice);
                        break;
                    case 5: //delete user
                        try
                        {
                            u.deleteUserByIDInDb(id);
                            Console.WriteLine("You have succesfully deleted your profile.");
                            //change id to change current id and exit login 
                            return -1; //end editing cuz profile and user are gone.
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR!");
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            error.createLogException(ex);
                            Console.ResetColor();
                        }
                        
                        break;
                }
                Console.WriteLine("Would you like to edit another field? if so, type 'yes'");
                edit = Console.ReadLine().Trim().ToLower();
            } while (edit == "yes");
            return id;
        }

        public static void editRole(int roleId, dboErrorLoggingCommands error, dboRoleCommands r, dboUsersCommands u)
        {

            string edit = "edit";
            do //for repeat asking edits
            {
                bool okRoleID = false;
                string roleEditing = "";
                int roleDeleteId = -1;
                //int roleID = -1;
                do //for error handleing of finding role
                {
                    okRoleID = false;
                    Console.WriteLine("Please enter the id of the role you wish to edit. Type 'list' if you wish to see all the roles. Type 'delete' if you wish to delete a role.");
                    roleEditing = Console.ReadLine().Trim().ToLower();
                    if (roleEditing == "list")
                    {
                        List<object[]> rows = r.selectAllRolesInDb();
                        AllPrinter.printAllRolesInDb(rows);
                        //Console.WriteLine();

                    }
                    else if (roleEditing == "delete")
                    {
                        Console.WriteLine("Please enter the id of the role you wish to delete.");
                        string roleDelete = Console.ReadLine().Trim().ToLower();
                        try
                        {
                            roleDeleteId = Convert.ToInt32(roleDelete);

                            //check for role existing
                            List<object[]> roleFound = r.selectRoleByIDInDb(roleDeleteId);
                            if (roleFound.Count > 0)
                            {
                                okRoleID = true;
                            }
                            else
                            {
                                throw new Exception("Role was not found");
                            }


                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            //Console.WriteLine("ERROR from database");
                            Console.WriteLine(ex.Message);
                            //  Console.WriteLine(ex.Source);
                            Console.ResetColor();
                            error.createLogException(ex);
                            okRoleID = false;

                        }
                        //Console.WriteLine();

                    }
                    else
                    {
                        try //see if role id is good 
                        {
                            roleId = Convert.ToInt32(roleEditing);

                            List<object[]> roleFound = r.selectRoleByIDInDb(roleId);
                            if (roleFound.Count > 0)
                            {
                                okRoleID = true;
                            }
                            else
                            {
                                throw new Exception("Role was not found");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            //Console.WriteLine("ERROR from database");
                            Console.WriteLine(ex.Message);
                            //  Console.WriteLine(ex.Source);
                            Console.ResetColor();
                            error.createLogException(ex);
                            okRoleID = false;
                        }

                    }
                } while (!okRoleID);

                bool roleEdited = false;

                if (roleEditing == "delete")
                {
                    try
                    {
                        List<object[]> usersWithRoleID = u.selectUserAndRoleIDByRoleIDInDb(roleDeleteId);
                        foreach (object[] user in usersWithRoleID)
                        {
                            u.updateUserRoleIDInDb((int)user[0], -1); //might need to change the -1 or the update method
                        }
                        r.deleteRoleByIDInDb(roleDeleteId);
                        List<object[]> rows = r.selectAllRolesInDb();
                        AllPrinter.printAllRolesInDb(rows);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        //Console.WriteLine("ERROR from database");
                        Console.WriteLine(ex.Message);
                        //  Console.WriteLine(ex.Source);
                        Console.ResetColor();
                        error.createLogException(ex);
                    }
                }
                else
                {
                    do //update role
                    {
                        string newName = "";
                        try
                        {
                            Console.WriteLine("Please enter the new name of the role: ");
                            newName = Console.ReadLine().Trim();
                            Console.WriteLine("Please enter the new role description: ");
                            string newDesc = Console.ReadLine().Trim();
                            r.updateRoleInDb(roleId, newName, newDesc);

                            List<object[]> rows = r.selectRoleByIDInDb(roleId);
                            AllPrinter.printAllRolesInDb(rows);
                            roleEdited = true;
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR!");
                            if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                            {
                                //ex.Message = "The entered username already exists in ";
                                Console.WriteLine("The role name '" + newName + "' already exists in the system.");
                            }
                            else
                            {
                                Console.WriteLine(ex.Message);
                            }
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                            error.createLogException(ex);
                            Console.ResetColor();
                            roleEdited = false;
                        }

                    } while (!roleEdited);
                }


                Console.WriteLine("If you wish to keep editing or deleting, please type 'edit'.");
                edit = Console.ReadLine().Trim().ToLower();

            } while (edit == "edit");

        }

        static void Main(string[] args)
        {
            UsersDatabase users = new UsersDatabase();
            RolesDatabase roles = new RolesDatabase();
            bool exit = false;
            SqlConnection con = new SqlConnection();

            dboUsersCommands u = new dboUsersCommands();
            dboRoleCommands r = new dboRoleCommands();
            dboErrorLoggingCommands errorLogging = new dboErrorLoggingCommands();
            int currentUserID = 0;
            int permissionID = 2; //default to member
           
            //PLEASE NOTE THAT THE PREMISSIONS OF THE USERS ARE DEFINED IN THE PERMISSION TABLE, AND THUS PREMISSIONS ARE CODED VIA PERMISSION ID   
            
            do
            {
                permissionID = 2; //default to member
                bool loggedin = false;
                Printer.mainMenu();
                string input = Console.ReadLine().Trim();
                if (input == "g") //guest
                {
                    currentUserID = 0;
                    permissionID = 1; //guest permission
                    loggedin = true;
                }
                else if (input == "r") //register
                {
                    bool register = false;
                    do
                    {
                        Console.WriteLine("Please enter your Username: ");
                        string username = Console.ReadLine().Trim();
                        Console.WriteLine("Please enter your Password: ");
                        string password = Console.ReadLine().Trim();
                        UserDTO newUser = new UserDTO(0, username, password);


                        try //see if error from trying to enter name shows up
                        {
                            u.createUserIntoDb(newUser);
                            currentUserID = newUser.UserId;
                            //Console.WriteLine(currentUserID);
                            Console.WriteLine("Congrats! You have registered!"); //if we got here then we have no errors
                            loggedin = true;
                            register = true;
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR from database");
                            if (ex.Message.Contains("Violation of UNIQUE KEY constraint 'UQ__Users__C9F28456E0F51A72'"))
                            {
                                //ex.Message = "The entered username already exists in ";
                                Console.WriteLine("The username '" + username + "' already exists in the system.");
                            }
                            else
                            {
                                Console.WriteLine(ex.Message);
                            }
                            //  Console.WriteLine(ex.Source);
                            Console.ResetColor();
                            errorLogging.createLogException(ex);
                        }

                    } while (!register);


                }
                else if (input == "l") //login
                {
                    bool foundUser = false;
                    do
                    {
                        //code to log in
                        Console.WriteLine("Please enter your username: ");
                        string username = Console.ReadLine().Trim();
                        Console.WriteLine("please enter your password");
                        string password = Console.ReadLine().Trim();

                        try
                        {
                            List<object[]> usernameSelect = u.selectUserNameByUsernameInDb(username);
                            if (usernameSelect.Count == 0) //that username isn't there
                            {
                                throw new Exception("Username does not exist in the System");
                            }
                            List<object[]> select = u.selectUserByUsernameAndPasswordInDb(username, password);
                            if (select.Count == 0) //password not match username entry
                            {
                                throw new Exception("Username and Password Dont Match");
                            }
                            currentUserID = (int)select.ElementAt(0)[0]; //should return user id for this current user
                            //Console.WriteLine(currentUserID);
                            loggedin = true;
                            foundUser = true;
                        }
                        catch (Exception ex)
                        {

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR from database");
                            Console.WriteLine(ex.Message);
                            //Console.WriteLine(ex.StackTrace);
                            Console.ResetColor();
                            errorLogging.createLogException(ex);
                        }

                    } while (!foundUser);


                }
                else if (input == "e")
                {
                    exit = true;
                }
                else if (input == "pu")
                {
                    List<object[]> rows = u.selectAllUsersInDb();
                    AllPrinter.printAllUsersInDb(rows);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR! You have not entered one of the options.");
                    Console.ResetColor();
                }

                //logged in options 
                if (loggedin)
                {
                    Console.Write("You have successfully logged in! ");
                    do
                    {
                        //log in menu
                        List<object[]> user = u.selectUserAndPermissionIDByUserIDInDb(currentUserID);
                        if (user.ElementAt(0)[6].GetType().ToString() != "System.DBNull") //if not null
                        {
                            if (user.ElementAt(0)[6].ToString() != "") //if not empty
                            {
                                permissionID = (int)user.ElementAt(0)[6];
                                //Console.WriteLine("permission id: " + permissionID);
                            }
                        }

                        Printer.loggedInMenu();
                        string loggedInInput = Console.ReadLine().Trim();
                        //print profile
                        if (loggedInInput == "pp")
                        {
                            List<object[]> Profile = u.selectUserAndRoleNameByIDInDb(currentUserID);
                            AllPrinter.printAllUsersProfilesInDb(Profile);
                        }
                        //edit profile
                        else if (loggedInInput == "e")
                        {
                  
                            if (permissionID == 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Error! You cannot edit the profile of a Guest");
                                Console.ResetColor();
                            }
                            else
                            {
                                currentUserID = editUser(currentUserID, u, errorLogging, r);
                                //users.editUser(users.currentUser.UserId, roles);
                            }

                            if (currentUserID == -1)
                            {
                                Console.WriteLine("You have logged out!");
                                loggedin = false;
                            }
                        }
                        //create new role
                        else if (loggedInInput == "cr")
                        {
                            if (permissionID == 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Error! You cannot create or edit roles while a Guest");
                                Console.ResetColor();
                            }
                            else
                            {
                                int newRoleId;
                                do
                                {
                                    newRoleId = createRole(r, errorLogging);
                                } while (newRoleId == -1);

                                List<object[]> rows = r.selectAllRolesInDb();
                                AllPrinter.printAllRolesInDb(rows);
                            }
                        }
                        //Edit role
                        else if (loggedInInput == "er")
                        {
                            if (permissionID == 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Error! You cannot create or edit roles while a Guest");
                                Console.ResetColor();
                            }
                            else
                            {
                                editRole(-1, errorLogging, r, u);
                            }
                            
                        }
                        //print roles
                        else if (loggedInInput == "pr")
                        {
                            List<object[]> rows = r.selectAllRolesInDb();
                            AllPrinter.printAllRolesInDb(rows);
                            //AllPrinter.printAllRoles(roles);
                        }
                        //print users
                        else if (loggedInInput == "pu")
                        {
                            List<object[]> rows = u.selectAllUsersInDb();
                            AllPrinter.printAllUsersInDb(rows);
                        }
                        else if (loggedInInput == "pup")
                        {
                            if (permissionID == 0) //admin permission
                            {
                                List<object[]> rows = u.selectAllUsersInDb();
                                AllPrinter.printAllUsersProfilesInDb(rows);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Error! Only Admins can see all user profiles");
                                Console.ResetColor();
                            }
                        }
                        else if (loggedInInput == "o")
                        {
                            Console.WriteLine("You have successfully Logged Out!");
                            loggedin = false;
                        }
                        else
                        {
                          
                            //MAYBE NEED FIX
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR! You have not entered one of the options.");
                            Console.ResetColor();

                        }

                    } while (loggedin);
                    
                }
            } while (!exit);
            Console.WriteLine("You have successfully exited! Thanks and come back soon!");
           
            Console.ReadKey();
        }
    }
}
