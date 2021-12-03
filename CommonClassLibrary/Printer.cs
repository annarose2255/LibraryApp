using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClassLibrary
{
    internal class Printer
    {
        /// <summary>
        /// Method to print the user's (<paramref name="user"/>) profile. Will list the user's first and last name, username, password, and role
        /// </summary>
        /// <param name="user"> The User to print</param>
        public void printProfile(UserDTO user)
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
            if (user.RoleID != null)
            {
                Console.WriteLine("Role: " ); //need to add function to get role then call the print role method
            }
            else
            {
                Console.WriteLine("Role: Not Set/Added");
            }
            Console.WriteLine("----------------------------");
        }

        //NEED TO WRITE WHEN HAVE ROLES LIST 
        public void printAllRoles(List<RoleDTO> rolesList) 
        {
            foreach (RoleDTO role in rolesList)
            {
                printRole(role);
            }
        } 

        /// <summary>
        /// Method to print a given role's (<paramref name="role"/>) name and description
        /// </summary>
        /// <param name="role">The role to print</param>
        public void printRole(RoleDTO role)
        {
            Console.WriteLine("Role Name: "+role.RoleName);
            if (role.RoleDescription != null)
            {
                Console.WriteLine("Role Description: "+role.RoleDescription);
            }
            else
            {
                Console.WriteLine("Role Description: Not Defined");
            }
        }
    }
}
