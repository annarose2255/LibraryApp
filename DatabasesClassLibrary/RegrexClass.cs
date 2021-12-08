using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DatabasesClassLibrary
{
    public class RegrexClass
    {
        public Regex PhoneNumber;
        public Regex Email;

        public RegrexClass()
        {
            PhoneNumber = new Regex(@"^\d{3}-\d{3}-\d{4}$");
            Email = new Regex(@"^\w+@[A-z]{4,}\.com$");
        }
    }
}
