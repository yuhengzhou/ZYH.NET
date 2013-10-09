using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace Tester_ZYH.NET_V.Test_V
{
    public partial class PageMethod_Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(true)]
        public static Person A(Person person, int AddSalary)
        {
            //throw new Exception("hhh");

            System.Threading.Thread.Sleep(2000);
            person.Salary += AddSalary;
            return person;
        }
    }

    public class Person
    {
        public String Name;
        public bool Sex;
        public int Salary;
    }
}