using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;

public partial class Prottype_JsonObject : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public static string JsSerialize<T>(T obj)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string Json = serializer.Serialize(obj);
        return Json;
    }

    public static T JsDeserialize<T>(string json)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        T o = serializer.Deserialize<T>(json);
        return o;
    }

    public static string Serialize<T>(T obj)
    {
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());

        MemoryStream ms = new MemoryStream();
        serializer.WriteObject(ms, obj);
        string retVal = Encoding.UTF8.GetString(ms.ToArray()); 
        return retVal;
    }

    public static T Deserialize<T>(string json)
    {
        T obj = Activator.CreateInstance<T>();
        MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
        obj = (T)serializer.ReadObject(ms);
        ms.Close();
        return obj;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        JsonObject t = new JsonObject();
        t.ID = "c1";
        t.Text = "mmx";
        t.Expire = DateTime.Now.AddHours(-1);
        t.IsSomething = true;
        t.Cost = 5.1m;
        t.Scripts.Add(new Script("sss", true, ScriptTypes.Include));
        t.Scripts.Add(new Script("56", false, ScriptTypes.Startup));


        string j = Serialize<JsonObject>(t);
        //string j = Serialize<HttpRequest>(this.Request);

        JsonObject t2 = Deserialize<JsonObject>(j);
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        JsonObject t = new JsonObject();
        t.ID = "c1";
        t.Text = "mmx";
        t.Expire = DateTime.Now.AddHours(-1);
        t.IsSomething = true;
        t.Cost = 5.1m;
        t.Scripts.Add(new Script("sss", true, ScriptTypes.Include));
        t.Scripts.Add(new Script("56", false, ScriptTypes.Startup));


        string j = JsSerialize<JsonObject>(t);

        JsonObject t2 = JsDeserialize<JsonObject>(j);

    }
}

[Serializable()]
public class JsonObject
{
    public string ID { get; set; }
    public string Text { get; set; }
    public DateTime Expire { get; set; }
    public bool IsSomething { get; set; }
    public decimal Cost { get; set; }
    private ScriptCollection _Scripts = new ScriptCollection();
    public ScriptCollection Scripts { get { return _Scripts; } }
}

public enum ScriptTypes
{
    Include = 1,
    Block = 2,
    Startup = 3
}

[Serializable()]
public class Script
{
    public Script() { }
    public Script(string key, bool isValid, ScriptTypes type)
    {
        Key = key;
        IsValid = isValid;
        Type = type;
    }
    public string Key { get; set; }
    public bool IsValid { get; set; }
    public ScriptTypes Type { get; set; }
}

[Serializable()]
public class ScriptCollection : List<Script>
{
    public Script this[string Key]
    {
        get
        {
            var q = this.Where(x => x.Key == Key);
            return q.FirstOrDefault();
        }
    }
}

