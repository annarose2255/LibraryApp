using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CommonClassLibrary;

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

    }
}
