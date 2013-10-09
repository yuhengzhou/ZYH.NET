using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using System.Xml;

namespace ZYH.WebControl_V
{
    [Serializable()]
    public abstract class SerializableObjectBase
    {
        #region XML
        public static T CreateFromXML<T>(string XML)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlReader reader = XmlReader.Create(new StringReader(XML));
            return (T)serializer.Deserialize(reader);
        }

        public virtual string ToXML()
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            XmlWriterSettings writerSettings = new XmlWriterSettings();
            writerSettings.OmitXmlDeclaration = true;
            StringWriter stringWriter = new StringWriter();
            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, writerSettings))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                serializer.Serialize(xmlWriter, this, ns);
            }
            return stringWriter.ToString();
        }

        public static string ToXML<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlWriterSettings writerSettings = new XmlWriterSettings();
            writerSettings.OmitXmlDeclaration = true;
            StringWriter stringWriter = new StringWriter();
            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, writerSettings))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                serializer.Serialize(xmlWriter, obj, ns);
            }
            return stringWriter.ToString();
        }
        #endregion

        #region Json
        public static T CreateFromJson<T>(string Json)
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(Json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }

        public virtual string ToJson()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(this.GetType());

            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, this);
            string retVal = Encoding.UTF8.GetString(ms.ToArray());
            return retVal;
        }

        public static string ToJson(object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());

            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = Encoding.UTF8.GetString(ms.ToArray());
            return retVal;
        }

        public static T CreateFromJson_Js<T>(string Json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            T o = serializer.Deserialize<T>(Json);
            return o;
        }

        public virtual string ToJson_Js()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string Json = serializer.Serialize(this);
            return Json;
        }

        public static string ToJson_Js(object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string Json = serializer.Serialize(obj);
            return Json;
        }
        #endregion

        #region Binary
        public static byte[] ToBinary<T>(T obj)
        {
            if (obj == null) return null;
            MemoryStream ms = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;
            byte[] bytes = new byte[ms.Length];
            ms.Read(bytes, 0, bytes.Length);
            ms.Close();
            return bytes;
        }

        public static T CreateFromBinary<T>(byte[] Binary)
        {
            MemoryStream ms = new MemoryStream(Binary);
            ms.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            T obj = (T)formatter.Deserialize(ms);
            ms.Close();
            return obj;

        }

        public static void ToBinaryFile(object obj, string FileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, obj);
            stream.Close();
        }

        public static T CreateFromBinaryFile<T>(string FileName)
        {
            Stream stream = File.Open(FileName, FileMode.Open, FileAccess.Read);
            BinaryFormatter formatter = new BinaryFormatter();
            T o = (T)formatter.Deserialize(stream);
            stream.Close();
            return o;
        }

        #endregion
    }
}
