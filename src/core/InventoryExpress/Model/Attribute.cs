using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace InventoryExpress.Model
{
    public class Attribute : Item
    {
        /// <summary>
        /// Gibt an oder legt den Standardwert fest
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Attribute()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="xml">
        ///   Die XML-Struktur, aus dem das 
        ///   Objekt erstellt werden soll
        /// </param>
        protected Attribute(XElement xml)
            : base(xml)
        {
            DefaultValue = (from x in xml.Elements("default")
                            select x.Value.Trim()).FirstOrDefault();
        }

        /// <summary>
        /// Wandelt das Objekt in XML um
        /// </summary>
        /// <param name="xml"></param>
        protected override void ToXML(XElement xml)
        {
            base.ToXML(xml);
        }

        /// <summary>
        /// Erstellt eine neue Instanz eines Atributes 
        /// aus der gegebenen XML-Datei
        /// </summary>
        /// <param name="file">Die XML-Repräsentation in Dateiform</param>
        /// <returns>Das Atribute</returns>
        public static Attribute Factory(string file)
        {
            using (var content = File.OpenRead(file))
            {
                var doc = XDocument.Load(content);
                var root = doc.Descendants("attribute");

                return new Attribute(root.FirstOrDefault());
            }
        }
    }
}
