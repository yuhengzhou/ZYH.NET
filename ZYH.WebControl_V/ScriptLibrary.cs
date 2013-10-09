using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZYH.WebControl_V
{
    public class ScriptResource
    {
        public ScriptResource() { }
        public ScriptResource(string name, string version, string url, ScriptResourceLoadFrom loadFrom) { Name = name; Version = version; URL = url; LoadFrom = loadFrom; }

        public string Name { get; set; }
        public string Version { get; set; }
        public string URL { get; set; }
        public ScriptResourceLoadFrom LoadFrom { get; set; }
    }

    public class ScriptResourceCollection : List<ScriptResource>
    {
        public ScriptResource this[string name, string version]
        {
            get { return this.Where(x => x.Name == name && x.Version == version).FirstOrDefault(); }
            set
            {
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(version))
                {
                    ScriptResource sl = this.Where(x => x.Name == name && x.Version == version).FirstOrDefault();
                    if (sl == null) { base.Add(value); } else { sl = value; }
                }
            }
        }

        public new void Add(ScriptResource scriptLibrary)
        {
            this[scriptLibrary.Name, scriptLibrary.Version] = scriptLibrary;
        }
    }

    public class CssStyle
    {
        public CssStyle() { }
        public CssStyle(string name, string url, ScriptResourceLoadFrom loadFrom) { Name = name; URL = url; LoadFrom = loadFrom; }

        public string Name { get; set; }
        public string URL { get; set; }
        public ScriptResourceLoadFrom LoadFrom { get; set; }
    }

    public class CssStylesCollection : List<CssStyle>
    {
        public CssStyle this[string name]
        { get { return this.Where(x => x.Name == name).FirstOrDefault(); } }
    }
}

