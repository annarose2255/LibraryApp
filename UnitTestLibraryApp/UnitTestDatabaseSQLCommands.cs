using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CommonClassLibrary;
using DatabasesClassLibrary;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace UnitTestLibraryApp
{
    [TestClass]
    public class UnitTestDatabaseSQLCommands
    {
        [TestMethod]
        public void TestInsertRoleFullDboCommand()
        {
            dboRoleCommands c = new dboRoleCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();
            RoleDTO roleDTO = new RoleDTO(1, "admin", "admin of the system");
            int num = c.createRoleIntoDb(roleDTO);
            Assert.AreEqual(0, num);
        }
        [TestMethod]
        public void TestInsertRoleNullDescDboCommand()
        {
            dboRoleCommands c = new dboRoleCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();
            RoleDTO roleDTO = new RoleDTO(1, "admin2", null);
            int num = c.createRoleIntoDb(roleDTO);
            Assert.AreEqual(1, num);
        }
        [TestMethod]
        public void TestSelectAllRolesDboCommand()
        {
            dboRoleCommands c = new dboRoleCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();
            // RoleDTO roleDTO = new RoleDTO(1, "admin2", null);
            //int num = c.createRoleIntoDb(roleDTO);
            // Assert.AreEqual(1, num);
            List<object[]> d = c.selectAllRolesInDb();
            Assert.IsNotNull(d);
        }
        [TestMethod]
        public void TestSelectRoleByIDDboCommand()
        {
            dboRoleCommands c = new dboRoleCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();
            // RoleDTO roleDTO = new RoleDTO(1, "admin2", null);
            //int num = c.createRoleIntoDb(roleDTO);
            // Assert.AreEqual(1, num);
            // c.updateRoleInDb(1, "SuperAdmin", "Best admin of all");
            List<object[]> d = c.selectRoleByIDInDb(1);
            AllPrinter.printAllRolesInDb(d);
            Assert.IsNotNull(d);
        }
        [TestMethod]
        public void TestPrintAllRolesDboCommand()
        {
            dboRoleCommands c = new dboRoleCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();
            // RoleDTO roleDTO = new RoleDTO(1, "admin2", null);
            //int num = c.createRoleIntoDb(roleDTO);
            // Assert.AreEqual(1, num);
            List<object[]> d = c.selectAllRolesInDb();
            AllPrinter.printAllRolesInDb(d);
            Assert.IsNotNull(d);
        }
        [TestMethod]
        public void TestDeleteRoleByIdDboCommand()
        {
            dboRoleCommands c = new dboRoleCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();
            // RoleDTO roleDTO = new RoleDTO(1, "admin2", null);
            //int num = c.createRoleIntoDb(roleDTO);
            // Assert.AreEqual(1, num);
            c.deleteRoleByIDInDb(0);
            List<object[]> d = c.selectAllRolesInDb();
            AllPrinter.printAllRolesInDb(d);
            Assert.IsNotNull(d);
        }

        [TestMethod]
        public void TestUpdateRoleDboCommand()
        {
            dboRoleCommands c = new dboRoleCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();
            // RoleDTO roleDTO = new RoleDTO(1, "admin2", null);
            //int num = c.createRoleIntoDb(roleDTO);
            // Assert.AreEqual(1, num);
            c.updateRoleInDb(1, "SuperAdmin", "Best admin of all");
            List<object[]> d = c.selectAllRolesInDb();
            AllPrinter.printAllRolesInDb(d);
            Assert.IsNotNull(d);
        }

        [TestMethod]
        public void TestInsertUserFullDboCommand()
        {
            dboUsersCommands c = new dboUsersCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();
            UserDTO user = new UserDTO(0, "hiya", "pass", "girl", "yay");
            user.RoleID = 1;
            int num = c.createUserIntoDb(user);
            Assert.AreEqual(0, num);
        }
        [TestMethod]
        public void TestSelectAllUsersDboCommand()
        {
            dboUsersCommands c = new dboUsersCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();

            List<object[]> d = c.selectAllUsersInDb();
            AllPrinter.printAllUsersProfilesInDb(d);
            //int num = c.createUserIntoDb(user);
            Assert.IsNotNull(d);
        }

        [TestMethod]
        public void TestSelectUserByIDDboCommand()
        {
            dboUsersCommands c = new dboUsersCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();

            List<object[]> d = c.selectUserByIDInDb(0);
            AllPrinter.printAllUsersProfilesInDb(d);
            //int num = c.createUserIntoDb(user);
            Assert.IsNotNull(d);
        }


        [TestMethod]
        public void TestSelectUserAndRoleNameByIDDboCommand()
        {
            dboUsersCommands c = new dboUsersCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();

            List<object[]> d = c.selectUserAndRoleNameByIDInDb(0);
            AllPrinter.printAllUsersProfilesInDb(d);
            //int num = c.createUserIntoDb(user);
            Assert.IsNotNull(d);
        }
        [TestMethod]
        public void TestPrintAllUsersinDbDboCommand()
        {
            dboUsersCommands c = new dboUsersCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();
            UserDTO user = new UserDTO(0, "girlsrkool", "pass", "girl", "yay");
            //user.RoleID = 1;
            int num = c.createUserIntoDb(user);
            List<object[]> d = c.selectAllUsersInDb();
            AllPrinter.printAllUsersInDb(d);
            //int num = c.createUserIntoDb(user);
            Assert.IsNotNull(d);
        }

        [TestMethod]
        public void TestSelectAllUsersProfilesinDbDboCommand()
        {
            dboUsersCommands c = new dboUsersCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();

            List<object[]> d = c.selectAllUsersAndRoleNameInDb();
            AllPrinter.printAllUsersProfilesInDb(d);
            //int num = c.createUserIntoDb(user);
            Assert.IsNotNull(d);
        }

        [TestMethod]
        public void TestPrintAllUsersProfilesinDbDboCommand()
        {
            dboUsersCommands c = new dboUsersCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();

            List<object[]> d = c.selectAllUsersAndRoleNameInDb();
            AllPrinter.printAllUsersProfilesInDb(d);
            //int num = c.createUserIntoDb(user);
            Assert.IsNotNull(d);
        }
        [TestMethod]
        public void TestUpdateUserUserNameinDbDboCommand()
        {
            dboUsersCommands c = new dboUsersCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();

            c.updateUserUsernameInDb(0, "why");
            List<object[]> d = c.selectAllUsersAndRoleNameInDb();
            AllPrinter.printAllUsersProfilesInDb(d);
            //int num = c.createUserIntoDb(user);
            Assert.IsNotNull(d);
        }
        [TestMethod]
        public void TestUpdateUserPasswordinDbDboCommand()
        {
            dboUsersCommands c = new dboUsersCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();

            c.updateUserPasswordInDb(0, "boyesSuck");
            List<object[]> d = c.selectAllUsersAndRoleNameInDb();
            AllPrinter.printAllUsersProfilesInDb(d);
            //int num = c.createUserIntoDb(user);
            Assert.IsNotNull(d);
        }
        [TestMethod]
        public void TestUpdateUserFirstNameinDbDboCommand()
        {
            dboUsersCommands c = new dboUsersCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();

            c.updateUserPasswordInDb(0, "re");
            List<object[]> d = c.selectAllUsersAndRoleNameInDb();
            AllPrinter.printAllUsersProfilesInDb(d);
            //int num = c.createUserIntoDb(user);
            Assert.IsNotNull(d);
        }

        [TestMethod]
        public void TestUpdateUserLastNameinDbDboCommand()
        {
            dboUsersCommands c = new dboUsersCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();

            c.updateUserLastNameInDb(0, "Yeet");
            List<object[]> d = c.selectAllUsersAndRoleNameInDb();
            AllPrinter.printAllUsersProfilesInDb(d);
            //int num = c.createUserIntoDb(user);
            Assert.IsNotNull(d);
        }

        [TestMethod]
        public void TestUpdateUserRoleIDinDbDboCommand()
        {
            dboUsersCommands c = new dboUsersCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();

            c.updateUserRoleIDInDb(0, 2);
            List<object[]> d = c.selectAllUsersAndRoleNameInDb();
            AllPrinter.printAllUsersProfilesInDb(d);
            //int num = c.createUserIntoDb(user);
            Assert.IsNotNull(d);
        }


        [TestMethod]
        public void TestUpdateUserRoleIDNullinDbDboCommand()
        {
            dboUsersCommands c = new dboUsersCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();

            c.updateUserRoleIDInDb(0, -1);
            List<object[]> d = c.selectAllUsersAndRoleNameInDb();
            AllPrinter.printAllUsersProfilesInDb(d);
            //int num = c.createUserIntoDb(user);
            Assert.IsNotNull(d);
        }

        [TestMethod]
        public void TestDeleteUserByIDinDbDboCommand()
        {
            dboUsersCommands c = new dboUsersCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();

            c.deleteUserByIDInDb(4);
            List<object[]> d = c.selectAllUsersAndRoleNameInDb();
            AllPrinter.printAllUsersProfilesInDb(d);
            //int num = c.createUserIntoDb(user);
            Assert.IsNotNull(d);
        }

        [TestMethod]
        public void TestInsertLogException()
        {
            //dboUsersCommands c = new dboUsersCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();
            dboErrorLoggingCommands c = new dboErrorLoggingCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            try
            {
                throw new Exception();
            }
            catch (Exception ex)
            {
                c.createLogException(ex);
            }

            List<object[]> d = c.selectAllLogErrors();
            AllPrinter.printAllLoggedErrors(d);
            //int num = c.createUserIntoDb(user);
            Assert.IsNotNull(d);
        }

        [TestMethod]
        public void TestPrintLogException()
        {
            //dboUsersCommands c = new dboUsersCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");
            //dboRoleCommands c = new dboRoleCommands();
            dboErrorLoggingCommands c = new dboErrorLoggingCommands(@"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryAppTest;Integrated Security=True");

            List<object[]> d = c.selectAllLogErrors();
            AllPrinter.printAllLoggedErrors(d);
            //int num = c.createUserIntoDb(user);
            Assert.IsNotNull(d);
        }
    }
}
