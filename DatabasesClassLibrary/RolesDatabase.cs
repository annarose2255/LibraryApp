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
        /// Method to create a new role given an id
        /// </summary>
        /// <param name="id">Id of thr role to create</param>
        /// <returns>True if the role was created. Otherwise return false</returns>
        public bool createNewRole(int id)
        {
            try
            {
                //int id = createNewRoleId();
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
        /// <summary>
        /// Method that returns the role from the ID given
        /// </summary>
        /// <param name="id">Id of the role to find</param>
        /// <returns>The role if it was found. Otherwise returns null</returns>
        public  RoleDTO findRole(int id)
        {
            foreach (RoleDTO role in Roles)
            {
                if (role.RoleID == id)
                {
                    return role;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR! Could not find a role by that id.");
            Console.ResetColor();
            return null;
        }
        /// <summary>
        /// Method that returns true if the role id exists
        /// </summary>
        /// <param name="id">Id of the role to find</param>
        /// <returns>true if the role id is in the database. Otherwise returns false</returns>
        public  bool roleIDExists(int id)
        {
            foreach (RoleDTO role in Roles)
            {
                if (role.RoleID == id)
                {
                    return true;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR! Could not find a role by that id.");
            Console.ResetColor();
            return false;
        }

        /// <summary>
        /// Method to edit a role if given its id
        /// </summary>
        /// <param name="roleId">Id of the role to edit</param>
        public void editRole(int roleId)
        {
            RoleDTO role = findRole(roleId);
            string edit = "edit";
            int choice = -1;
            do //for repeat asking edits
            {
                
                bool notMadeGoodChoice = false;
                do //for error handleing
                {
                    
                    notMadeGoodChoice = false;
                    Console.WriteLine("What would you like to edit? please enter number of field. " +
                   "\n 0: Role Name, 1: Role Description"); ;
                    string strChoice = Console.ReadLine().Trim();
                    try
                    {
                        choice = Convert.ToInt32(strChoice); //got choice now we can come out of this do while
                        if (choice >= 2 || choice < 0)
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("ERROR: Did not enter one of the given choices!");
                        Console.ResetColor();
                        notMadeGoodChoice = true;
                    }
                }while (notMadeGoodChoice);
                switch (choice) //do the actual stuff
                {
                    case 0:
                        bool roleNameEdited = false;
                        do
                        {
                            Console.WriteLine("Please enter the new name of the role: ");
                            string newName = Console.ReadLine().Trim();
                            roleNameEdited = editRoleName(role, newName);
                        }while (!roleNameEdited);
                        break;
                    case 1:
                        Console.WriteLine("Please enter the new role description: ");
                        string newDesc = Console.ReadLine().Trim();
                        editRoleDescription(role, newDesc);
                        break;
                }

                Console.WriteLine("If you wish to keep editing, please type 'edit'.");
                edit = Console.ReadLine().Trim().ToLower();

            } while (edit == "edit");
            
        }

        /// <summary>
        /// Method to edit a given role's name.
        /// </summary>
        /// <param name="role">The role to edit</param>
        /// <param name="newName">The name to change to</param>
        /// <returns>True if the role exists and was changed. Returns False otherwise. </returns>
        public bool editRoleName(RoleDTO role, string newName)
        {
            if (roleNameExists(newName))
            {
                return false;
            }
            else
            {
                role.RoleName = newName;
                Printer.printRole(role);
                return true;
            }
        }

        public bool editRoleDescription(RoleDTO role, string newDesc)
        {
            if (role != null)
            {

                role.RoleDescription = newDesc;
                Printer.printRole(role);
                return true;
            }
            else
            {
                return false;
            }
        }
        





    }
}
