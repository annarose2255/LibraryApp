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
        /// Method to print the user's (<paramref name="user"/>) profile. Will list the user's first and last name, username, password, and role
        /// </summary>
        /// <param name="user"> The User to print</param>
        public static void printProfile(UserDTO user)
        {
            Console.WriteLine("----------------------------");
            if (user.FirstName != null || user.LastName != null) //if they have entered either a first or last name
            {
                if (user.FirstName != null && user.LastName != null)
                {
                    Console.WriteLine("Name: " + user.FirstName, user.LastName);
                }
                else if (user.FirstName != null)
                {
                    Console.WriteLine("Name: " + user.FirstName);
                }
                else
                {
                    Console.WriteLine("Name: " + user.LastName);
                }
            }
            else
            {
                Console.WriteLine("Name: Unknown");
            }
            Console.WriteLine("Username: "+user.UserName);
            Console.WriteLine("Password: " + user.Password);
            if (user.RoleID == -1) //no set role
            {
                Console.WriteLine("Role: Not Set/Added");
            }
            else
            {
                Console.WriteLine("Role: "); //need to add function to get role then call the print role method
            }
            Console.WriteLine("----------------------------");
        }

        /// <summary>
        /// Method to print a given role's (<paramref name="role"/>) name and description
        /// </summary>
        /// <param name="role">The role to print</param>
        public static void printRole(RoleDTO role)
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("Role Name: "+role.RoleName);
            if (role.RoleDescription != null)
            {
                Console.WriteLine("Role Description: "+role.RoleDescription);
            }
            else
            {
                Console.WriteLine("Role Description: Not Defined");
            }
            Console.WriteLine("Role ID: " + role.RoleID);
            Console.WriteLine("----------------------------");
        }

        public static void mainMenu()
        {
            Console.WriteLine("Welcome! Please enter one of the following: ");
            //guest
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("\tg");
            Console.ResetColor();
            Console.WriteLine(" to login as a guest");
            //register
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("\tr");
            Console.ResetColor();
            Console.WriteLine(" to register as a new user");
            //login
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\tl");
            Console.ResetColor();
            Console.WriteLine(" to login as an existing user");
            //logout
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("\to");
            Console.ResetColor();
            Console.WriteLine(" to logout");
            //print profile
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\tpp");
            Console.ResetColor();
            Console.WriteLine(" to print profile");
            //print roles
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("\tpr");
            Console.ResetColor();
            Console.WriteLine(" to print all existing roles");
            //print users
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("\tpu");
            Console.ResetColor();
            Console.WriteLine(" to print all existing Users");



        }
    }
}
