using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace Tester_ZYH.NET_V
{
    public partial class DirectCallPageBehind : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string ReturnStringServerMethod(string str)
        {
            return "Hello " + str;
        }

        [WebMethod]
        public static Person Add_1000_Salary(Person person)
        {
            person.Salary += 1000;
            return person;
        }
        [WebMethod]
        public static IDictionary<string, Person> GetOnePerson()
        {
            Dictionary<string, Person> result = new Dictionary<string, Person>();
            Person person = new Person();
            person.Name = "Rose Zhao";
            person.Sex = "Female";
            person.Salary = 2000;
            result[person.Name] = person;
            return result;
        }


    }

    public class Person
    {
        public string Name { get; set; }

        public string Sex { get; set; }

        public int Salary { get; set; }
    }
}