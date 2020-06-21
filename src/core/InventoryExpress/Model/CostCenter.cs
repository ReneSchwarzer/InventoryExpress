using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Kostenstelle
    /// </summary>
    public class CostCenter : Item
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public CostCenter()
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
        protected CostCenter(XElement xml)
            : base(xml)
        {
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
        /// Erstellt eine neue Instanz einer Kostenstelle 
        /// aus der gegebenen XML-Datei
        /// </summary>
        /// <param name="file">Die XML-Repräsentation in Dateiform</param>
        /// <returns>Die Kostenstelle</returns>
        public static CostCenter Factory(string file)
        {
            using (var content = File.OpenRead(file))
            {
                var doc = XDocument.Load(content);
                var root = doc.Descendants("costcenter");

                return new CostCenter(root.FirstOrDefault());
            }
        }
    }
}
