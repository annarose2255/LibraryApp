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


        public static void printAllUsers(HashSet<UserDTO> listOfUsers)
        {
            for (int numUser = 0; numUser < listOfUsers.Count; numUser++)
            {
                Console.WriteLine(String.Format("User {0}: {1}", numUser + 1, listOfUsers.ElementAt(numUser).UserName));
            }

        }
        public static void printAllUserProfiles(HashSet<UserDTO> listOfUsers)
        {
            for (int numUser = 0; numUser < listOfUsers.Count; numUser++)
            {
                Console.WriteLine(String.Format("User {0}:", numUser + 1 ));
                Printer.printProfile(listOfUsers.ElementAt(numUser));
            }

        }

        //NEED TO EDIT WHEN HAVE ROLES LIST 
        public static void printAllRoles(HashSet<RoleDTO> rolesList)
        {
            foreach (RoleDTO role in rolesList)
            {
                Printer.printRole(role);
            }
        }
    }
}
