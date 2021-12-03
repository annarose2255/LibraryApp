using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClassLibrary;

namespace DatabasesClassLibrary
{
    public class UsersDatabase
    {
        //public List<UserDTO> Users;
        public HashSet<UserDTO> Users;


        //constructor
        public UsersDatabase()
        {
            Users = new HashSet<UserDTO>();
            Users.Add(new UserDTO(1, "Guest", "Guest")); //add a guest user since the database should have a guest user
        }

        public bool addUser(UserDTO user)
        {
            if (Users.Add(user)) //check that it added it
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        //add, remove (delete), editmethod for fields, get user 

        public UserDTO getUser(int id)
        {
            foreach (UserDTO user in Users)
            {
                if (user.UserId == id)
                {
                    return user;
                }
            }
            return null;
        }
        public bool editUser(int id)
        {
            UserDTO user = getUser(id);
            if (user != null)
            {
                int choice = -1;
                string edit = "yes";
                do //for repeat asking edits
                {
                    bool notMadeGoodChoice = false;
                    do //for error handleing
                    {
                        Console.WriteLine("What would you like to edit? please enter number of field. " +
                    "\n 0: First Name, 1: Last name, 2: Username, 3: Password, 4: Role");
                        string strChoice = Console.ReadLine().Trim();
                        try
                        {
                            choice = Convert.ToInt32(strChoice); //got choice now we can come out of this do while
                            if (choice >= 5 || choice == -1)
                            {
                                throw new Exception();
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine("ERROR: Did not enter one of the given choices!");
                            notMadeGoodChoice = true;
                        }
                    } while (notMadeGoodChoice);
                    switch (choice)
                    {
                        case 0:
                            Console.WriteLine("Please enter new First Name: ");
                            string newFirstName = Console.ReadLine().Trim();
                            user.FirstName = newFirstName;
                            break;
                        case 1:
                            Console.WriteLine("Please enter new Last Name: ");
                            string newLastName = Console.ReadLine().Trim();
                            user.LastName = newLastName;
                            break;
                        case 2:
                            Console.WriteLine("Please enter new Username: ");
                            string newUserName = Console.ReadLine().Trim();
                            user.FirstName = newUserName;
                            break;
                        case 3:
                            Console.WriteLine("Please enter new Password: ");
                            string newPassword = Console.ReadLine().Trim();
                            user.FirstName = newPassword;
                            break;
                        case 4: //role
                            do { //for error handling 
                            notMadeGoodChoice = false;
                            Console.WriteLine("Do you wish to make your role one of the currently existing ones? Or create a new one? " +
                                              "\n Please enter 0 to make your role one of the currently existing ones, or 1 to create a new role");

                            try
                            {
                                string strRoleChoice = Console.ReadLine().Trim();
                                int roleChoice = Convert.ToInt32(strRoleChoice);
                                //!!Printer.printAllRoles(); -- will add this method when roles database is made!!
                                if (roleChoice != 0 || roleChoice != 2) //check that its one of the choices
                                {
                                    throw new Exception();
                                }
                                switch (roleChoice)
                                {
                                    case 0:
                                        Console.WriteLine("Please enter the role's id: ");
                                        string newRoleID = Console.ReadLine().Trim();
                                        int roleID = Convert.ToInt32(newRoleID);
                                        user.RoleID = roleID;
                                        break;
                                    case 1:
                                        //!!!!make creation class/method!!!!!!
                                        RoleDTO roleDTO = new RoleDTO();
                                        user.RoleID = roleDTO.RoleID;
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Console.WriteLine("ERROR: Did not enter one of the given choices!");
                                notMadeGoodChoice = true; //repeat the loop
                            }
                            } while(notMadeGoodChoice);
                            break;
                    }
                Console.WriteLine("Would you like to edit another field? if so, type 'edit'");
                 edit = Console.ReadLine().Trim().ToLower();
                } while (edit == "true");
                return true;
                
            }
            else
            {
                return false; 
            }
           
        }
    }
}
