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
            List<object[]> d =  c.selectAllRolesInDb();  
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

    }
}
