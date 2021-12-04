using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClassLibrary;

namespace DatabasesClassLibrary
{
    public  class AllPrinter
    {


        public static void printAllUsers(UsersDatabase t)
        {
            for (int numUser = 0; numUser < t.Users.Count; numUser++)
            {
                Console.WriteLine(String.Format("User {0}: {1}", numUser + 1, t.Users.ElementAt(numUser).UserName));
            }

        }
        public static void printAllUserProfiles(UsersDatabase t)
        {
            Console.WriteLine("----------------------------");
            for (int numUser = 0; numUser < t.Users.Count; numUser++)
            {
                Console.WriteLine(String.Format("User {0}:", numUser + 1 ));
                Printer.printProfile(t.Users.ElementAt(numUser));
            }
            Console.WriteLine("----------------------------");

        }

        //NEED TO EDIT WHEN HAVE ROLES LIST 
        public static void printAllRoles(RolesDatabase t)
        {
            foreach (RoleDTO role in t.Roles)
            {
                Printer.printRole(role);
            }
        }
    }
}
