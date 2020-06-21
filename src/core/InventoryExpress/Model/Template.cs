using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Vorlage
    /// </summary>
    public class Template : Item
    {
        /// <summary>
        /// Die URL des Providers
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Die Attribute der Vorlage
        /// </summary>
        public List<Attribute> Attributes { get; set; }

        /// <summary>
        /// Die ungenutzten Attribute der Vorlage
        /// </summary>
        public List<Attribute> UnusedAttributes { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Template()
            : base()
        {
            Attributes = new List<Attribute>();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="xml">
        ///   Die XML-Struktur, aus dem das 
        ///   Objekt erstellt werden soll
        /// </param>
        protected Template(XElement xml)
            : base(xml)
        {
            Attributes = new List<Attribute>();

            Url = (from x in xml.Elements("url")
                   select x.Value.Trim()).FirstOrDefault();

            var attributes = new List<Attribute>();

            foreach (var v in xml.Elements("attribute"))
            {
                var name = v.Attribute("name") != null ? v.Attribute("name").Value : null;

                if (!string.IsNullOrWhiteSpace(name))
                {
                    attributes.AddRange(from x in Model.ViewModel.Instance.Attributes
                                        where x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                                        select x);
                }
            }

            Attributes = attributes.Distinct().ToList();
        }

        /// <summary>
        /// Wandelt das Objekt in XML um
        /// </summary>
        /// <param name="xml"></param>
        protected override void ToXML(XElement xml)
        {
            base.ToXML(xml);

            xml.Add(new XElement("url", Url));

            foreach (var v in Attributes)
            {
                xml.Add(new XElement("attribute", new XAttribute("name", v.Name)));
            }
        }

        /// <summary>
        /// Erstellt eine neue Instanz einer Vorlage 
        /// aus der gegebenen XML-Datei
        /// </summary>
        /// <param name="file">Die XML-Repräsentation in Dateiform</param>
        /// <returns>Die Vorlage</returns>
        public static Template Factory(string file)
        {
            using (var content = File.OpenRead(file))
            {
                var doc = XDocument.Load(content);
                var root = doc.Descendants("template");

                return new Template(root.FirstOrDefault());
            }
        }
    }
}
