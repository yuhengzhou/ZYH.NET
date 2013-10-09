using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Web.Services;

namespace Tester_ZYH.NET_V.ProtType
{
    public partial class FindWebMethods : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MethodInfo[] methodInfos = Page.GetType().BaseType.GetMethods(BindingFlags.Public |
                                                                 BindingFlags.Static);
            foreach (MethodInfo method in methodInfos)
            {
                foreach (Attribute attribute in method.GetCustomAttributes(true))
                {
                    if (attribute is WebMethodAttribute)
                    {
                        var MethodName = method.Name;
                        var Parameters = method.GetParameters();
                        foreach (var p in Parameters)
                        {
                            var ParameterName = p.Name;
                        }
                    }
                }
            }
        }

        [WebMethod]
        public static Person A(Person person, string Job)
        {
            return person;
        }
        [WebMethod]
        public static Person B()
        {
            return new Person() { Name = "zyh", Sex = "Male", Salary = 10000 };
        }
        [WebMethod]
        public static string C(string Name)
        {
            return Name;
        }

        [WebMethod]
        public static Person Add_1000_Salary(Person person)
        {
            person.Salary += 1000;
            return person;
        }
    }

    public class Person
    {
        public string Name { get; set; }

        public string Sex { get; set; }

        public int Salary { get; set; }
    }
}