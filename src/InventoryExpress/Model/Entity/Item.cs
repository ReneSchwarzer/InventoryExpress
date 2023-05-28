using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model.Entity
{
    public class Item
    {
        /// <summary>
        /// Die Id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Liefert oder setzt den Namen
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Returns or sets the description.
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
        /// Die Guid
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
        /// Constructor
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
