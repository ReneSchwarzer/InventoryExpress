using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Zustand
    /// </summary>
    public class State : Item
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public State()
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
        public State(XElement xml)
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
        /// Erstellt eine neue Instanz eines Zustandes 
        /// aus der gegebenen XML-Datei
        /// </summary>
        /// <param name="file">Die XML-Repräsentation in Dateiform</param>
        /// <returns>Der Zustand</returns>
        public static State Factory(string file)
        {
            using (var content = File.OpenRead(file))
            {
                var doc = XDocument.Load(content);
                var root = doc.Descendants("state");

                return new State(root.FirstOrDefault());
            }
        }
    }
}
