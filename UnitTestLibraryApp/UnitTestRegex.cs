using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using CommonClassLibrary;
using DatabasesClassLibrary;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace UnitTestLibraryApp
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTestRegex
    {


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestPhoneRegrex1()
        {
            RegrexClass c = new RegrexClass();
            string test = "999-543-2383";
            MatchCollection matches = c.PhoneNumber.Matches(test);
            Console.WriteLine("{0} matches found in:\n   {1}",
                  matches.Count,
                  test);
            Assert.AreEqual(1, matches.Count);

        }

        [TestMethod]
        public void TestPhoneRegrex2()
        {
            RegrexClass c = new RegrexClass();
            string test = "999-543-23833";
            MatchCollection matches = c.PhoneNumber.Matches(test);
            Console.WriteLine("{0} matches found in:\n   {1}",
                  matches.Count,
                  test);
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
            Assert.AreEqual(0, matches.Count);

        }
        [TestMethod]
        public void TestPhoneRegrex3()
        {
            RegrexClass c = new RegrexClass();
            string test = "999-5433-2383";
            MatchCollection matches = c.PhoneNumber.Matches(test);
            Console.WriteLine("{0} matches found in:\n   {1}",
                  matches.Count,
                  test);
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
            Assert.AreEqual(0, matches.Count);

        }
        [TestMethod]
        public void TestPhoneRegrex4()
        {
            RegrexClass c = new RegrexClass();
            string test = "9399-543-2383";
            MatchCollection matches = c.PhoneNumber.Matches(test);
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
            Console.WriteLine("{0} matches found in:\n   {1}",
                  matches.Count,
                  test);
            Assert.AreEqual(0, matches.Count);

        }
        [TestMethod]
        public void TestPhoneRegrex5()
        {
            RegrexClass c = new RegrexClass();
            string test = "a939-543-2383";
            MatchCollection matches = c.PhoneNumber.Matches(test);
            Console.WriteLine("{0} matches found in:\n   {1}",
                  matches.Count,
                  test);
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
            Assert.AreEqual(0, matches.Count);

        }
        [TestMethod]
        public void TestEmailRegrex1()
        {
            RegrexClass c = new RegrexClass();
            string test = "hiya@hotmail.com";
            MatchCollection matches = c.Email.Matches(test);
            Console.WriteLine("{0} matches found in:\n   {1}",
                  matches.Count,
                  test);
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
            Assert.AreEqual(1, matches.Count);

        }
        [TestMethod]
        public void TestEmailRegrex2()
        {
            RegrexClass c = new RegrexClass();
            string test = "hiya2134234@hotmail.com";
            MatchCollection matches = c.Email.Matches(test);
            Console.WriteLine("{0} matches found in:\n   {1}",
                  matches.Count,
                  test);
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
            Assert.AreEqual(1, matches.Count);

        }
        [TestMethod]
        public void TestEmailRegrex3()
        {
            RegrexClass c = new RegrexClass();
            string test = "1234@hotmail.com";
            MatchCollection matches = c.Email.Matches(test);
            Console.WriteLine("{0} matches found in:\n   {1}",
                  matches.Count,
                  test);
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
            Assert.AreEqual(1, matches.Count);

        }
        [TestMethod]
        public void TestEmailRegrex4()
        {
            RegrexClass c = new RegrexClass();
            string test = " @hotmail.com";
            MatchCollection matches = c.Email.Matches(test);
            Console.WriteLine("{0} matches found in:\n   {1}",
                  matches.Count,
                  test);
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
            Assert.AreEqual(0, matches.Count);

        }
        [TestMethod]
        public void TestEmailRegrex5()
        {
            RegrexClass c = new RegrexClass();
            string test = "hiya@hotmail234.com";
            MatchCollection matches = c.Email.Matches(test);
            Console.WriteLine("{0} matches found in:\n   {1}",
                  matches.Count,
                  test);
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
            Assert.AreEqual(0, matches.Count);

        }
        [TestMethod]
        public void TestEmailRegrex6()
        {
            RegrexClass c = new RegrexClass();
            string test = "hiya@hotmail.com23";
            MatchCollection matches = c.Email.Matches(test);
            Console.WriteLine("{0} matches found in:\n   {1}",
                  matches.Count,
                  test);
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
            Assert.AreEqual(0, matches.Count);

        }
        [TestMethod]
        public void TestEmailRegrex7()
        {
            RegrexClass c = new RegrexClass();
            string test = "hiya@hotmail!.com";
            MatchCollection matches = c.Email.Matches(test);
            Console.WriteLine("{0} matches found in:\n   {1}",
                  matches.Count,
                  test);
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
            Assert.AreEqual(0, matches.Count);

        }
        [TestMethod]
        public void TestEmailRegrex8()
        {
            RegrexClass c = new RegrexClass();
            string test = "hi_ya@hotmail.com";
            MatchCollection matches = c.Email.Matches(test);
            Console.WriteLine("{0} matches found in:\n   {1}",
                  matches.Count,
                  test);
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
            Assert.AreEqual(1, matches.Count);

        }
    }
}
