using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClassLibrary;

namespace DatabasesClassLibrary
{
    public  class AllPrinter
    {
        public static void printAllRolesInDb(List<object[]> rows)
        {
           foreach (object[] values in rows)
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("Role ID: " + values[0]);
                Console.WriteLine("Role Name: " + values[1]);
                //Console.WriteLine(values[2].GetType().ToString());                 
                if (values[2].GetType().ToString() != "System.DBNull")
                {
                    Console.WriteLine("Role Description: " + values[2]);
                }
                else
                {
                    Console.WriteLine("Role Description: Not Defined");
                }
                Console.WriteLine("----------------------------");

            }
            

            //Console.WriteLine("----------------------------");
            //Console.WriteLine("Role ID: " + reader.GetInt32(0));
            ////Console.WriteLine();
            //Console.WriteLine("Role Name: "+reader.GetString(1));
            //if (reader.GetString(2) != null)
            //{
            //    Console.WriteLine("Role Description: " + reader.GetString(2));
            //}
            //else
            //{
            //    Console.WriteLine("Role Description: Not Defined");
            //}
            //Console.WriteLine("----------------------------");

            //Console.WriteLine(reader.GetString(2));

        }


        public static void printAllUsers(UsersDatabase t)
        {
            Console.WriteLine("----------------------------");
            for (int numUser = 0; numUser < t.Users.Count; numUser++)
            {
                Console.WriteLine(String.Format("User {0}: {1}", numUser + 1, t.Users.ElementAt(numUser).UserName));
            }
            Console.WriteLine("----------------------------");

        }
        public static void printAllUserProfiles(UsersDatabase t, RolesDatabase r)
        {
            Console.WriteLine("----------------------------");
            for (int numUser = 0; numUser < t.Users.Count; numUser++)
            {
                Console.WriteLine(String.Format("User {0}:", numUser + 1 ));
                printProfile(t.Users.ElementAt(numUser), r);
            }
            Console.WriteLine("----------------------------");

        }

        
        public static void printAllRoles(RolesDatabase t)
        {
            foreach (RoleDTO role in t.Roles)
            {
                Printer.printRole(role);
            }
        }
        /// <summary>
        /// Method to print the user's (<paramref name="user"/>) profile. Will list the user's first and last name, username, password, and role
        /// </summary>
        /// <param name="user"> The User to print</param>
        public static void printProfile(UserDTO user, RolesDatabase r)
        {
            Console.WriteLine("----------------------------");
            if (user.FirstName != null || user.LastName != null) //if they have entered either a first or last name
            {
                if (user.FirstName != null && user.LastName != null)
                {
                    Console.WriteLine("Name: " + user.FirstName + " " + user.LastName);
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
            Console.WriteLine("Username: " + user.UserName);
            Console.WriteLine("Password: " + user.Password);
            if (user.RoleID == -1) //no set role
            {
                Console.WriteLine("Role: Not Set/Added");
            }
            else
            {
                RoleDTO role = r.findRole(user.RoleID);
                Console.WriteLine("Role: "+role.RoleName); //need to add function to get role then call the print role method
                //Printer.printRole(role);
            }
            Console.WriteLine("----------------------------");
        }

    }
}
