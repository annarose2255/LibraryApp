using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClassLibrary
{
    public class RoleDTO
    {
        /// <summary>
        /// The role's ID. This is the Primary key - should be unqiue (required)
        /// </summary>
        public int RoleID { get; set; } //primary key

        /// <summary>
        /// Name of the role, Required. must be unique
        /// </summary>
        public string RoleName { get; set; } = ""; //guest, admin, ...

        /// <summary>
        /// Description of the role. Not required.
        /// </summary>
        public string RoleDescription { get; set; }  //description, optional


        //constructors
        public RoleDTO()
        {
            RoleID = 0;
            RoleName = "Default";
        }

        public RoleDTO(int roleid, string name)
        {
            RoleID = roleid;
            RoleName = name;
        }
        public RoleDTO(int roleid, string name, string desc)
        {
            RoleID = roleid;
            RoleName = name;
            RoleDescription = desc;
        }
    }
}
