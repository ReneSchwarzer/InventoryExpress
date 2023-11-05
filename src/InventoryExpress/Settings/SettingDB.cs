using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace InventoryExpress.Settings
{
    [XmlType("db")]
    public class SettingDB
    {
        [XmlAttribute(AttributeName = "version", DataType = "int")]
        public int Version { get; set; }

        [XmlElement(ElementName = "port", DataType = "int")]
        public int Port { get; set; }

        [XmlElement(ElementName = "host")]
        public string Host { get; set; }

        [XmlElement(ElementName = "servicename")]
        public string ServiceName { get; set; }

        [XmlElement(ElementName = "user")]
        public string User { get; set; }

        [XmlElement(ElementName = "password")]
        public string Password { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public SettingDB()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xml">The xml node.</param> 
        public SettingDB(XElement xml)
        {
            Port = -1;

            if (xml.Attribute("version") != null)
            {
                Version = Convert.ToInt32(xml.Attribute("version").Value);
            }

            if (xml.Element("port") != null)
            {
                Port = Convert.ToInt32(xml.Element("port").Value);
            }

            if (xml.Element("host") != null)
            {
                Host = xml.Element("host").Value;
            }

            if (xml.Element("servicename") != null)
            {
                ServiceName = xml.Element("servicename").Value;
            }

            if (xml.Element("user") != null)
            {
                User = xml.Element("user").Value;
            }

            if (xml.Element("password") != null)
            {
                Password = xml.Element("password").Value;
            }
        }
    }
}
