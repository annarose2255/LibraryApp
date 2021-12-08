using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using CommonClassLibrary;

namespace DatabasesClassLibrary
{
    public class dboErrorLoggingCommands
    {

        public string _conn { get; set; } = "";


        public dboErrorLoggingCommands()
        {
            _conn = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();

        }
        public dboErrorLoggingCommands(string conn)
        {
            _conn = conn;
            // _conn = @"Data Source=ANNA-DESKTOP;Initial Catalog=LibraryApp;Persist Security Info=True;";
        }



    }
}
