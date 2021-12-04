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
                    //code to add new user
                    loggedin = true;

                }
                else if (input == "l")
                {
                    //code to log in
                    loggedin= true;
                }
                else if (input == "e")
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("ERROR! You have not entered one of the options.");
                    break;
                }

                //logged in options 
                if (loggedin)
                {
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
                                Console.WriteLine("Error! You cannot edit the profile of a Guest");
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
                            AllPrinter.printAllUsers(users);
                        }
                        else if (loggedInInput == "o")
                        {
                            loggedin = false;
                        }
                        else 
                        {
                            Console.WriteLine("ERROR! You have not entered one of the options.");
                        }

                    } while (loggedin);
                }
            }while (!exit);
            Console.WriteLine("You have successfully exited! Thanks and come back soon!");
           
            Console.ReadKey();
        }
    }
}
