using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Standort
    /// </summary>
    public class Location : Item
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
        /// Das Gebäude
        /// </summary>
        public string Building { get; set; }

        /// <summary>
        /// Der Raum innerhalb des Gebäudes
        /// </summary>
        public string Room { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Location()
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
        protected Location(XElement xml)
            : base(xml)
        {
            Address = (from x in xml.Elements("address")
                       select x.Value.Trim()).FirstOrDefault();

            Zip = (from x in xml.Elements("zip")
                   select x.Value.Trim()).FirstOrDefault();

            Place = (from x in xml.Elements("place")
                     select x.Value.Trim()).FirstOrDefault();

            Building = (from x in xml.Elements("building")
                        select x.Value.Trim()).FirstOrDefault();

            Room = (from x in xml.Elements("room")
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
            xml.Add(new XElement("building", Building));
            xml.Add(new XElement("room", Room));
        }

        /// <summary>
        /// Erstellt eine neue Instanz eines Standortes 
        /// aus der gegebenen XML-Datei
        /// </summary>
        /// <param name="file">Die XML-Repräsentation in Dateiform</param>
        /// <returns>Der Standort</returns>
        public static Location Factory(string file)
        {
            using (var content = File.OpenRead(file))
            {
                var doc = XDocument.Load(content);
                var root = doc.Descendants("location");

                return new Location(root.FirstOrDefault());
            }
        }
    }
}
