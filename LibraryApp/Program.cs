using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClassLibrary;
using DatabasesClassLibrary;

namespace LibraryApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UsersDatabase users = new UsersDatabase();
            RolesDatabase roles = new RolesDatabase();
            bool exit = false;
            do
            {
                bool loggedin = false;
                Printer.mainMenu();
                string input = Console.ReadLine().Trim();
                if (input == "g")
                {
                    users.currentUser = users.Users.First();
                    loggedin = true;
                }
                else if (input == "r")
                {
                    bool register = false;
                    do
                    {
                        //code to add new user
                        Console.WriteLine("Please enter your Username: ");
                        string username = Console.ReadLine().Trim();
                        if (users.isExistingUsername(username))
                        {
                            //Console.ForegroundColor = ConsoleColor.Red;
                            //Console.WriteLine("Error! A user already has that username");
                            //Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine("Please enter your Password: ");
                            string password = Console.ReadLine().Trim();
                            int id = users.createNewUserId();
                            UserDTO newUser = new UserDTO(id, username, password);
                            users.addUser(newUser);
                            users.currentUser = newUser;
                            Console.WriteLine("Congrats! You have registered!");
                            loggedin = true;
                            register = true;
                        }
                    } while (!register);
                    

                }
                else if (input == "l")
                {
                    bool foundUser = false;
                    do
                    {
                        //code to log in
                        Console.WriteLine("Please enter your username: ");
                        string username = Console.ReadLine().Trim();
                        Console.WriteLine("please enter your password");
                        string password = Console.ReadLine().Trim();

                        UserDTO currentUser = users.findUser(username, password);
                        if (currentUser != null) //if we could find the user
                        {
                            users.currentUser = currentUser;
                            foundUser = true;
                            loggedin = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error! Could not find that User with that Username and Password. Please try logging in again.");
                            Console.ResetColor();
                        }

                    } while (!foundUser);
                    
                    
                }
                else if (input == "e")
                {
                    exit = true;
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
                        Printer.loggedInMenu();
                        string loggedInInput = Console.ReadLine().Trim();
                        //print profile
                        if (loggedInInput == "pp")
                        {
                            AllPrinter.printProfile(users.currentUser, roles);
                        }
                        //edit profile
                        else if (loggedInInput == "e")
                        {
                            if (users.currentUser.UserId == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Error! You cannot edit the profile of a Guest");
                                Console.ResetColor();
                            }
                            else
                            {
                                users.editUser(users.currentUser.UserId, roles);
                            }
                        }
                        //create new role
                        else if (loggedInInput == "cr")
                        {
                            roles.createNewRole(roles.createNewRoleId());
                            AllPrinter.printAllRoles(roles);
                           
                        }
                        //Edit role
                        else if (loggedInInput == "er")
                        {
                            bool okRoleID = false;
                            int inputedRoleId = -1;
                            do //for error handleing
                            {
                                okRoleID = false;
                                Console.WriteLine("Please enter the id of the role you wish to edit. Type 'list' if you wish to see all the roles.");
                                string roleEditing = Console.ReadLine().Trim().ToLower();
                                if (roleEditing == "list")
                                {
                                    AllPrinter.printAllRoles(roles);
                                }
                                else
                                {
                                    try
                                    {
                                        inputedRoleId = Convert.ToInt32(roleEditing); //got choice now we can come out of this do while
                                        RoleDTO foundRole = roles.findRole(inputedRoleId);
                                        if (inputedRoleId <= -1 || foundRole == null)
                                        {
                                            throw new Exception();
                                        }
                                        else
                                        {
                                            okRoleID=true;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        System.Console.WriteLine("ERROR: Did not enter a role id that exists!");
                                        Console.ResetColor();
                                        okRoleID = false;
                                    }
                                }
                            } while (!okRoleID);
                            roles.editRole(inputedRoleId);

                        }
                        //print roles
                        else if (loggedInInput == "pr")
                        {
                            AllPrinter.printAllRoles(roles);
                        }
                        //print users
                        else if (loggedInInput == "pu")
                        {
                            AllPrinter.printAllUsers(users);
                        }
                        else if (loggedInInput == "o")
                        {
                            loggedin = false;
                        }
                        else 
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR! You have not entered one of the options.");
                            Console.ResetColor();
                            
                        }

                    } while (loggedin);
                }
            }while (!exit);
            Console.WriteLine("You have successfully exited! Thanks and come back soon!");
           
            Console.ReadKey();
        }
    }
}
