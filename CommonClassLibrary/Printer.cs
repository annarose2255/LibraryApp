using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClassLibrary
{
    public class Printer
    {


        /// <summary>
        /// Method to print a given role's (<paramref name="role"/>) name and description
        /// </summary>
        /// <param name="role">The role to print</param>
        public static void printRole(RoleDTO role)
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("Role ID: " + role.RoleID);
            Console.WriteLine("Role Name: "+role.RoleName);
            if (role.RoleDescription != null && role.RoleDescription != "")
            {
                Console.WriteLine("Role Description: "+role.RoleDescription);
            }
            else
            {
                Console.WriteLine("Role Description: Not Defined");
            }
            Console.WriteLine("----------------------------");
        }

        /// <summary>
        /// Method to print the main menu of the program
        /// </summary>
        public static void mainMenu()
        {
            Console.WriteLine("Welcome! Please enter one of the following: ");
            //guest
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("\tg");
            Console.ResetColor();
            Console.WriteLine(" to login as a guest");
            //register
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\tr");
            Console.ResetColor();
            Console.WriteLine(" to register as a new user");
            //login
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\tl");
            Console.ResetColor();
            Console.WriteLine(" to login as an existing user");

            //Exit
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("\te");
            Console.ResetColor();
            Console.WriteLine(" to exit the program");


        }
        /// <summary>
        /// Method to print the login menu
        /// </summary>
        public static void loggedInMenu()
        {
            Console.WriteLine("Please enter one of the following: ");

            //print profile
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\tpp");
            Console.ResetColor();
            Console.WriteLine(" to print profile");

            //Edit profile
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\te");
            Console.ResetColor();
            Console.WriteLine(" to edit your user settings");

            //create new role
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("\tcr");
            Console.ResetColor();
            Console.WriteLine(" to create a new role");

            //Edit role
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("\ter");
            Console.ResetColor();
            Console.WriteLine(" to edit a role");

            //print roles
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("\tpr");
            Console.ResetColor();
            Console.WriteLine(" to print all existing roles");
            //print users
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("\tpu");
            Console.ResetColor();
            Console.WriteLine(" to print all existing Users");

            //logout
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("\to");
            Console.ResetColor();
            Console.WriteLine(" to logout");
        }
    }
}
