using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Sachkonto
    /// </summary>
    public class GLAccount : Item
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public GLAccount()
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
        protected GLAccount(XElement xml)
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
        /// Erstellt eine neue Instanz eines Sachkontos 
        /// aus der gegebenen XML-Datei
        /// </summary>
        /// <param name="file">Die XML-Repräsentation in Dateiform</param>
        /// <returns>Das Sachkonto</returns>
        public static GLAccount Factory(string file)
        {
            using (var content = File.OpenRead(file))
            {
                var doc = XDocument.Load(content);
                var root = doc.Descendants("glaccount");

                return new GLAccount(root.FirstOrDefault());
            }
        }
    }
}
