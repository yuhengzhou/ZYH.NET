using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tester_ZYH.NET_V.Demo
{
    [Serializable]
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PersonSex Sex { get; set; }
        public int Age { get; set; }
        public string Comment { get; set; }
        public DateTime Birthday { get; set; }
    }

    [Serializable]
    public class PersonCollection : List<Person>
    {
        public Person this[string Name]
        {
            get { return this.Where(x => x.Name == Name).FirstOrDefault(); }
        }
        public Person this[Guid Id]
        {
            get { return this.Where(x => x.Id == Id).FirstOrDefault(); }
        }


        public static PersonCollection Load(System.Web.UI.Page p)
        {
            PersonCollection ps;
            var DataFile = p.Server.MapPath("~/Demo/DemoData.bin");
            var b = System.IO.File.Exists(DataFile);
            if (b)
            {
                ps = ZYH.WebControl_V.SerializableObjectBase.CreateFromBinaryFile<PersonCollection>(DataFile);
            }
            else
            {
                ps = new PersonCollection();
            }
            return ps;
        }

        public new void Add(Person P)
        {
            if (P.Id == Guid.Empty)
            { P.Id = Guid.NewGuid(); base.Add(P); }
            else
            {
                var o = this[P.Id]; o.Name = P.Name; o.Sex = P.Sex; o.Comment = P.Comment; o.Age = P.Age; o.Birthday = P.Birthday;
            }
           
        }

        public void Save(System.Web.UI.Page p)
        {
            var DataFile = p.Server.MapPath("~/Demo/DemoData.bin");
            ZYH.WebControl_V.SerializableObjectBase.ToBinaryFile(this, DataFile);
        }
    }

    public enum PersonSex
    {
        Male = 1,
        Female = 2
    }
}