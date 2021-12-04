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
                            Printer.printProfile(users.currentUser);
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
                                users.editUser(users.currentUser.UserId);
                            }
                        }
                        //create new role
                        else if (loggedInInput == "cr")
                        {
                           
                        }
                        //print roles
                        else if (loggedInInput == "pr")
                        {
                            //AllPrinter.printAllRoles();
                        }
                        //print users
                        else if (loggedInInput == "pu")
                        {
                            Console.WriteLine("----------------------------");
                            AllPrinter.printAllUsers(users);
                            Console.WriteLine("----------------------------");
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
