﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CommonClassLibrary;
using DatabasesClassLibrary;

namespace UnitTestLibraryApp
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPrintProfile_NoName_NoRole()
        {
            //arrange
            UserDTO user = new UserDTO(1, "HiyaBoys21", "1234Password");
            user.UserId = 1;
            //act
            Printer.printProfile(user);
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
            int expected = 2;
            AllPrinter.printAllUserProfiles(t);

            //act
            t.addUser(user);
            t.editUserUserName(1, "HiyaGirls61");
            //t.editUserUsername(1);

            AllPrinter.printAllUserProfiles(t);

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


    }
}
