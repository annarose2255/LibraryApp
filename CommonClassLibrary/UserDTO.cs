using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClassLibrary
{
    public class UserDTO
    {
        /// <summary>
        /// The ID of the user. This is the Primary key and should be unique (required)
        /// </summary>
        public int UserId { get; set; } //primary key
        /// <summary>
        /// First name of user
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of user
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Username of user - part of login 
        /// </summary>
        public string UserName { get; set; } //user name of user 
        /// <summary>
        /// Password for user. Part of login
        /// </summary>
        public string Password { get; set; } //password for user
        /// <summary>
        /// RoleID. Forgien key to RoleDTO. -1 means no set role
        /// </summary>
        public int RoleID { get; set; }

        //constuctors
        public UserDTO()
        {
            UserId = 0;
            UserName = "";
            Password = "";
            RoleID = -1;
        }
        public UserDTO(int userid, string username, string password)
        {
            UserId =userid;
            UserName =username;
            Password =password;
            RoleID = -1;
        }
        public UserDTO(int userid, string username, string password, string firstname, string lastname)
        {
            UserId = userid;
            UserName = username;
            Password = password;
            FirstName = firstname;
            LastName = lastname;
            RoleID = -1;
        }

    }
}
