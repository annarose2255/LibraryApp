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

    }
}
