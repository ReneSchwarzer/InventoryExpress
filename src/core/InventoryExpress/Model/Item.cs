using System;
using System.Linq;
using System.Xml.Linq;

namespace InventoryExpress.Model
{
    public class Item
    {
        /// <summary>
        /// Die Versionsnummer
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Die ID (Die ID dient auch als Initialisierungsvektor für die Verschlüsselung)
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Der Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Die Beschreibung
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// Tag
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Zeitstempel
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Bild
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Item()
        {
            Version = "1.0";
            ID = Guid.NewGuid().ToString();
            Timestamp = DateTime.Now;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="xml">
        ///   Die XML-Struktur, aus dem das 
        ///   Objekt erstellt werden soll
        /// </param>
        protected Item(XElement xml)
            : this()
        {
            Version = (from x in xml.Attributes("version")
                       select x.Value.Trim()).FirstOrDefault();

            ID = (from x in xml.Elements("id")
                  select x.Value.Trim()).FirstOrDefault();

            Name = (from x in xml.Elements("name")
                    select x.Value.Trim()).FirstOrDefault();

            Memo = (from x in xml.Elements("memo")
                    select x.Value.Trim()).FirstOrDefault();

            var datetime = (from x in xml.Elements("timestamp")
                            select x.Value.Trim()).FirstOrDefault();

            Timestamp = !string.IsNullOrWhiteSpace(datetime) ?
                            Convert.ToDateTime(datetime) :
                            DateTime.Now;

            Image = (from x in xml.Elements("image")
                     select x.Value).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(ID))
            {
                ID = Guid.NewGuid().ToString();
            }
        }

        /// <summary>
        /// Wandelt das Objekt in XML um
        /// </summary>
        /// <param name="xml"></param>
        protected virtual void ToXML(XElement xml)
        {
            if (!string.IsNullOrWhiteSpace(Version))
            {
                xml.Add(new XAttribute("version", Version));
            }

            xml.Add(new XElement("id", !string.IsNullOrWhiteSpace(ID) ? ID : Guid.NewGuid().ToString()));

            if (!string.IsNullOrWhiteSpace(Name))
            {
                xml.Add(new XElement("name", Name));
            }

            if (!string.IsNullOrWhiteSpace(Memo))
            {
                xml.Add(new XElement("memo", Memo));
            }

            if (!string.IsNullOrWhiteSpace(Tag))
            {
                xml.Add(new XElement("tag", Tag));
            }

            if (!string.IsNullOrWhiteSpace(Image))
            {
                xml.Add(new XElement("image", Image));
            }

            xml.Add(new XElement("timestamp", Timestamp));
        }

    }
}
