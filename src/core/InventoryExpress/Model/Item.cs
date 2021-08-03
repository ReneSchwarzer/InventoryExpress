using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    public class Item
    {
        /// <summary>
        /// Die ID
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Liefert oder setzt den Namen
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Der Zeitstempel der Erstellung
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Der Zeitstempel der letzten Änderung
        /// </summary>
        public DateTime Updated { get; set; }

        /// <summary>
        /// Die GUID
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Das Bild
        /// </summary>
          public int? MediaId { get; set; }

        /// <summary>
        /// Das Bild
        /// </summary>
        public virtual Media Media { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Item()
        {
        }

        /// <summary>
        /// Umwandlung in String
        /// </summary>
        /// <returns>Das als String umgewandelte Objekt</returns>
        public override string ToString()
        {
            return string.Format("{0} - {1}", Id, Name);
        }
    }
}
