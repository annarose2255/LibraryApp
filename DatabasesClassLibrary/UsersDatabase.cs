using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClassLibrary;

namespace DatabasesClassLibrary
{
    public class UsersDatabase
    {
        #region Fields
        /// <summary>
        /// Hashset of all the users in the database
        /// </summary>
        public HashSet<UserDTO> Users;

        /// <summary>
        /// The current logged in user
        /// </summary>
        public UserDTO currentUser;

        public HashSet<int> RolesIds { get; set; }


        #endregion

        #region Constructor
        //constructor
        public UsersDatabase()
        {
            Users = new HashSet<UserDTO>();
            Users.Add(new UserDTO(0, "Guest", "Guest")); //add a guest user since the database should have a guest user
            RolesIds = new HashSet<int>();
        }
        #endregion


        #region Methods
        /// <summary>
        /// Method to add a user to the database.
        /// </summary>
        /// <param name="user">The user to add</param>
        /// <returns>Returns true if the user was added. Returns false otherwise</returns>
        public bool addUser(UserDTO user)
        {
            if (Users.Add(user)) //check that it added it
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public bool removeUser(UserDTO user)
        {
            if (Users.Remove(user)) //check that it added it
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool removeUserByID(int id)
        {
            UserDTO user = getUser(id);
            if (user != null)
            {
                return Users.Remove(user);

            }
            else
            {
                return false;
            }
            
        }
        //add, remove (delete), editmethod for fields, get user 

        public UserDTO getUser(int id)
        {
            foreach (UserDTO user in Users)
            {
                if (user.UserId == id)
                {
                    return user;
                }
            }
            return null;
        }
        public bool editUser(int id, RolesDatabase roles)
        {
            UserDTO user = getUser(id);
            if (user != null)
            {
                int choice = -1;
                string edit = "yes";
                do //for repeat asking edits
                {
                    bool notMadeGoodChoice = false;
                    do //for error handleing
                    {
                        notMadeGoodChoice = false;
                        Console.WriteLine("What would you like to edit? please enter number of field. " +
                    "\n 0: First Name, 1: Last name, 2: Username, 3: Password, 4: Role");
                        string strChoice = Console.ReadLine().Trim();
                        try
                        {
                            choice = Convert.ToInt32(strChoice); //got choice now we can come out of this do while
                            if (choice >= 5 || choice == -1)
                            {
                                throw new Exception();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            System.Console.WriteLine("ERROR: Did not enter one of the given choices!");
                            Console.ResetColor();
                            notMadeGoodChoice = true;
                        }
                        
                    } while (notMadeGoodChoice);
                    switch (choice)
                    {
                        case 0:
                            Console.WriteLine("Please enter new First Name: ");
                            string newFirstName = Console.ReadLine().Trim();
                            editUserFirstName(id, newFirstName);
                            AllPrinter.printProfile(user, roles);

                            break;
                        case 1:
                            Console.WriteLine("Please enter new Last Name: ");
                            string newLastName = Console.ReadLine().Trim();
                            editUserLastName(id, newLastName);
                            AllPrinter.printProfile(user, roles);
                            //user.LastName = newLastName;
                            break;
                        case 2:
                            bool goodUsername = false;
                            do
                            {
                                //goodUsername = false;
                                Console.WriteLine("Please enter new Username: ");
                                string newUserName = Console.ReadLine().Trim();
                                if (editUserUserName(id, newUserName))
                                {
                                    AllPrinter.printProfile(user, roles);
                                    goodUsername = true;
                                }
                                else
                                {
                                   goodUsername = false;
                                }
                            } while (!goodUsername);
                            break;

                        case 3:
                            Console.WriteLine("Please enter new Password: ");
                            string newPassword = Console.ReadLine().Trim();
                            editUserPassword(id, newPassword);
                            AllPrinter.printProfile(user, roles);
                            break;
                        case 4: //role
                            do { //for error handling 
                            notMadeGoodChoice = false;
                            Console.WriteLine("Do you wish to make your role one of the currently existing ones? Or create a new one? " +
                                              "\n Please enter 0 to make your role one of the currently existing ones, or 1 to create a new role");

                            try
                            {
                                string strRoleChoice = Console.ReadLine().Trim();
                                int roleChoice = Convert.ToInt32(strRoleChoice);
                                //!!Printer.printAllRoles(); -- will add this method when roles database is made!!
                                if (roleChoice < 0 || roleChoice > 1) //check that its one of the choices
                                {
                                    throw new Exception();
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
                                                    AllPrinter.printAllRoles(roles);
                                                }
                                                else
                                                {
                                                    try
                                                    {
                                                        roleID = Convert.ToInt32(strNewRoleID);
                                                        bool roleIDExists = roles.roleIDExists(roleID);
                                                        if (!roleIDExists)
                                                        {
                                                            goodId = false;
                                                        }
                                                        else
                                                        {
                                                            goodId = true;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                        System.Console.WriteLine("ERROR: Did not enter a role id that exists!");
                                                        Console.ResetColor();
                                                    }
                                                }
                                            } while (!goodId);
                                            editUserRoleExistingRole(id, roleID);
                                            AllPrinter.printProfile(user, roles);
                                            break;
                                    case 1:
                                            int newId = roles.createNewRoleId();
                                            roles.createNewRole(newId);
                                            editUserRoleExistingRole(id, newId);
                                            AllPrinter.printProfile(user, roles);
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
                            } while(notMadeGoodChoice);
                            break; //???
                    }
                Console.WriteLine("Would you like to edit another field? if so, type 'yes'");
                 edit = Console.ReadLine().Trim().ToLower();
                } while (edit == "yes");
                return true;
                
            }
            else
            {
                return false; 
            }
        }
        

        /// <summary>
        /// method to edit the given user's id First name.
        /// </summary>
        /// <param name="id">Id of the user to edit</param>
        /// <param name="newFirstName"> New first name</param>
        /// <returns>true if the name was edited. False if the user's id does not exist</returns>
        public bool editUserFirstName(int id, string newFirstName)
        {
            UserDTO user = getUser(id);
            if (user == null)
            {
                return false;
            }
            else
            {
                user.FirstName = newFirstName;
                
                return true;
            }
        }
        /// <summary>
        /// method to edit the given user's id Last name.
        /// </summary>
        /// <param name="id">id of user toe dit</param>
        /// <param name="newLastName">new last name</param>
        /// <returns>true if the name was edited. False if the user's id does not exist</returns>
        public bool editUserLastName(int id, string newLastName)
        {
            UserDTO user = getUser(id);
            if (user == null)
            {
                return false;
            }
            else
            {
                user.LastName = newLastName;
                return true;
            }
        }
        /// <summary>
        /// Method to edit the given user's username.
        /// </summary>
        /// <param name="id">id of user</param>
        /// <param name="newUsername">new username</param>
        /// <returns>true if the name was edited. False if the user's id does not exist</returns>
        public bool editUserUserName(int id, string newUsername)
        {
            UserDTO user = getUser(id);
            if (user == null)
            {
                return false;
            }
            else
            {
                if (isExistingUsername(newUsername))
                {
                    return false;
                }
                else
                {
                    user.UserName = newUsername;
                    return true;
                }
                
            }
        }
        /// <summary>
        /// Method to edit given user's password
        /// </summary>
        /// <param name="id">id of user</param>
        /// <param name="newPassword">new password</param>
        /// <returns>true if the name was edited. False if the user's id does not exist</returns>
        public bool editUserPassword(int id, string newPassword)
        {
            UserDTO user = getUser(id);
            if (user == null)
            {
                return false;
            }
            else
            {
                user.Password = newPassword;
                return true;
            }
        }
        /// <summary>
        /// Method to edit the roleID of user
        /// </summary>
        /// <param name="userId">id of user</param>
        /// <param name="newRoleId">role id to place</param>
        /// <returns>true if the roleID was edited. False if the user's id does not exist</returns>
        public bool editUserRoleExistingRole(int userId, int newRoleId)
        {
            UserDTO user = getUser(userId);
            if (user == null)
            {
                return false;
            }
            else
            {
                user.RoleID = newRoleId;
                return true;
            }
        }



        //POSSIBLE MAY NOT USE 
        /// <summary>
        /// Method to create a new role to add its id to the user 
        /// </summary>
        /// <param name="userId">id of user</param>
        /// <returns>true if the roleID was edited. False if the user's id does not exist</returns>
        public bool editUserRoleNewRole(int userId)
        {
            UserDTO user = getUser(userId);
            if (user == null)
            {
                return false;
            }
            else
            {
                RoleDTO r = Creator.makeNewRole();
                user.RoleID = r.RoleID;
                return true;
            }
        }
        /// <summary>
        /// Method to find a user based on username and password
        /// </summary>
        /// <param name="username">possible username of the user</param>
        /// <param name="password">possible password of the user</param>
        /// <returns>the found User. If no user is found, returns null.</returns>
        public UserDTO findUser(string username, string password)
        {
            foreach (UserDTO possibleUser in Users)
            {
                if (possibleUser.UserName == username && possibleUser.Password == password)
                {
                    return possibleUser;
                }
            }
            return null;
        }
        /// <summary>
        /// Method that returns true if there is a user with that username 
        /// </summary>
        /// <param name="username">The username to search for</param>
        /// <returns>True if the username is in the database. False if the username is not in the database</returns>
        public bool isExistingUsername(string username)
        {
            bool found = false;
            foreach (UserDTO user in Users)
            {
                if (user.UserName == username)
                {
                    found = true;
                }

            }
            if (found)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR! Username already exists.");
                Console.ResetColor();
            }
            return found;
        }
        /// <summary>
        /// Method to create a new user id for the database. 
        /// </summary>
        /// <returns>An int that will be a new valid id</returns>
        public int createNewUserId()
        {
            return Users.Last().UserId+1; //get the last id in the database and add 1;
        }
        #endregion
    }
}
