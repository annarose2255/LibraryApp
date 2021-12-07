using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CommonClassLibrary;
using DatabasesClassLibrary;

namespace UnitTestLibraryApp
{
    [TestClass]
    public class UnitTestLibraryApp
    {
        [TestMethod]
        public void TestPrintProfile_NoName_NoRole()
        {
            //arrange
            UserDTO user = new UserDTO(1, "HiyaBoys21", "1234Password");
            RolesDatabase r = new RolesDatabase();
            user.UserId = 1;
            //act
            AllPrinter.printProfile(user, r);
            //Assert
        }
        [TestMethod]
        public void TestAdd1UserToDatabase()
        {
            //arrange
            UserDTO user = new UserDTO(1, "HiyaBoys21", "1234Password");
            user.UserId = 1;
            UsersDatabase t = new UsersDatabase();
            int expected = 2;
            //act
            t.addUser(user);
            int result = t.Users.Count;

            //Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestPrintAllUsers()
        {
            //arrange
            UserDTO user = new UserDTO(1, "HiyaBoys21", "1234Password");
            //user.UserId = 1;
            UsersDatabase t = new UsersDatabase();
            t.addUser(user);

            //act
            AllPrinter.printAllUsers(t); 
            //Assert
        }
        [TestMethod]
        public void Testremove1UserByIDFromDatabase()
        {
            //arrange
            UserDTO user = new UserDTO(1, "HiyaBoys21", "1234Password");
            user.UserId = 1;
            UsersDatabase t = new UsersDatabase();
            int expected = 1;

            //act
            t.addUser(user);
            t.removeUserByID(1);

            int result = t.Users.Count;

            //Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestEditUserFirstNameFromDatabase()
        {
            //arrange
            UserDTO user = new UserDTO(1, "HiyaBoys21", "1234Password");
            //user.UserId = 1;
            UsersDatabase t = new UsersDatabase();
            RolesDatabase r = new RolesDatabase();
            int expected = 2;
            AllPrinter.printAllUserProfiles(t, r);

            //act
            t.addUser(user);
            t.editUserUserName(1, "HiyaGirls61");
            //t.editUserUsername(1);

            AllPrinter.printAllUserProfiles(t, r);

            int result = t.Users.Count;

            //Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestGetNewUserID()
        {
            //arrange
            UserDTO user = new UserDTO(1, "HiyaBoys21", "1234Password");
            UserDTO user2 = new UserDTO(2, "Hiyagirls21", "1234Password");
            UsersDatabase t = new UsersDatabase();
            int expected = 3;


            //act
            t.addUser(user);
            t.addUser(user2);
            //AllPrinter.printAllUserProfiles(t);

            int result = t.createNewUserId();

            //Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestCreateRoleDatabase()
        {

            RolesDatabase t = new RolesDatabase();
            int expected = 1;


            int result = t.Roles.Count;

            //Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestAddRoleDatabase()
        {
            RoleDTO role = new RoleDTO(1, "new");
            RolesDatabase t = new RolesDatabase();
            int expected = 2;

            t.addRole(role);
            int result = t.Roles.Count;

            //Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestGetNewRoleID()
        {
            
            RolesDatabase t = new RolesDatabase();
            int expected = 1;

            int result = t.createNewRoleId();

            //Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestPrintAllRoles()
        {
            RoleDTO role = new RoleDTO(1, "new");
            RolesDatabase t = new RolesDatabase();
            int expected = 2;

            t.addRole(role);
            int result = t.Roles.Count;
            AllPrinter.printAllRoles(t);

            //Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestRoleNameExistsTrue()
        {
            RolesDatabase t = new RolesDatabase();
            bool expected = true;

            bool result = t.roleNameExists("Guest");

            //Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestRoleNameExistsFalse()
        {
            RolesDatabase t = new RolesDatabase();
            bool expected = false;

            bool result = t.roleNameExists("hi");

            //Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestEditRoleName()
        {
            RoleDTO role = new RoleDTO(1, "new");
            RolesDatabase t = new RolesDatabase();
            string expected = "edited";
            Printer.printRole(role);

            t.addRole(role);
            t.editRoleName(role, "edited");
            string result = t.findRole(1).RoleName;
            

            //Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestEditRoleDescription()
        {
            RoleDTO role = new RoleDTO(1, "new", "old");
            RolesDatabase t = new RolesDatabase();
            string expected = "edited";
            

            t.addRole(role);
            t.editRoleDescription(role, "edited");
            string result = t.findRole(1).RoleDescription;
            Printer.printRole(role);

            //Assert
            Assert.AreEqual(expected, result);
        }



    }
}
