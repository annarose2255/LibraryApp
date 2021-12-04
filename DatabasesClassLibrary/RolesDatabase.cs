using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClassLibrary;

namespace DatabasesClassLibrary
{
    public class RolesDatabase
    {
        public HashSet<RoleDTO> Roles;

        //constructor 
        public RolesDatabase()
        {
            Roles = new HashSet<RoleDTO>();
            RoleDTO guest = new RoleDTO(0, "Guest", "Guest Account");
            Roles.Add(guest);
        }

        /// <summary>
        /// Method to add a role to the database.
        /// </summary>
        /// <param name="role">The role to add</param>
        /// <returns>Returns true if the role was added. Returns false otherwise</returns>
        public bool addRole(RoleDTO role)
        {
            if (Roles.Add(role))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Method to create a new Role id
        /// </summary>
        /// <returns>Int of a new possible role id</returns>
        public int createNewRoleId()
        {
            if (Roles.Count == 0)
            {
                return 0;
            }
            else
            {
                return Roles.Last().RoleID + 1; //add one to the last role id
            }
        }
        /// <summary>
        /// Method to create a new role.
        /// </summary>
        /// <returns>Returns true if the role was created sucessfully. Otherwise return false</returns>
        public bool createNewRole()
        {
            try
            {
                int id = createNewRoleId();
                bool goodname = false;
                string name;
                do //repeat input for good name
                {
                    Console.WriteLine("Please enter the name of the role: ");
                    name = Console.ReadLine().Trim();
                    if (roleNameExists(name))
                    {
                        goodname = false;
                    }
                    else
                    {
                        goodname = true;
                    }
                } while (!goodname);
                Console.WriteLine("Enter a description of the role if you wish: ");
                string description = Console.ReadLine().Trim();

                RoleDTO role = new RoleDTO(id, name, description);
                if (!addRole(role))
                {
                    throw new Exception();
                }
                return true;

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR! Something went wrong. Please try again");
                Console.ResetColor();
                return false;
            }
        }

        /// <summary>
        /// Method that returns true if the role name exists (and outprints an error message)
        /// </summary>
        /// <param name="roleName">The name of the role to search for</param>
        /// <returns>True if the there is a role with that name. Otherwise return false</returns>
        public bool roleNameExists(string roleName)
        {
            bool found = false;
            foreach(RoleDTO role in Roles)
            {
                if (role.RoleName == roleName)
                {

                    found = true;
                }
            }
            if (found)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR! That Role Name already exists.");
                Console.ResetColor();
            }
            return found;
        }

    }
}
