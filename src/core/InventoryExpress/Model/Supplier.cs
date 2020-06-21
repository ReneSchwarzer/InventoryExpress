using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Lieferant
    /// </summary>
    public class Supplier : Item
    {
        /// <summary>
        /// Die Adresse
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Die Postleitzahl
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// Der Ort
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Supplier()
            : base()
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="xml">
        ///   Die XML-Struktur, aus dem das 
        ///   Objekt erstellt werden soll
        /// </param>
        protected Supplier(XElement xml)
            : base(xml)
        {
            Address = (from x in xml.Elements("address")
                        select x.Value.Trim()).FirstOrDefault();

            Zip = (from x in xml.Elements("zip")
                    select x.Value.Trim()).FirstOrDefault();

            Place = (from x in xml.Elements("place")
                      select x.Value.Trim()).FirstOrDefault();
        }

        /// <summary>
        /// Wandelt das Objekt in XML um
        /// </summary>
        /// <param name="xml"></param>
        protected override void ToXML(XElement xml)
        {
            base.ToXML(xml);

            xml.Add(new XElement("address", Address));
            xml.Add(new XElement("zip", Zip));
            xml.Add(new XElement("place", Place));
        }

        /// <summary>
        /// Erstellt eine neue Instanz eines Lieferanten 
        /// aus der gegebenen XML-Datei
        /// </summary>
        /// <param name="file">Die XML-Repräsentation in Dateiform</param>
        /// <returns>Der Lieferant</returns>
        public static Supplier Factory(string file)
        {
            using (var content = File.OpenRead(file))
            {
                var doc = XDocument.Load(content);
                var root = doc.Descendants("supplier");

                return new Supplier(root.FirstOrDefault());
            }
        }
    }
}
