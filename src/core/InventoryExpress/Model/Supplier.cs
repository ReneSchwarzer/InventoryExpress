using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Lieferant
    /// </summary>
    [Table("SUPPLIER")]
    public class Supplier 
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        /// <summary>
        /// Liefert oder setzt den Namen
        /// </summary>
        [StringLength(64), Required, Column("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// Die Adresse
        /// </summary>
        [Column("ADDRESS")]
        public string Address { get; set; }

        /// <summary>
        /// Die Postleitzahl
        /// </summary>
        [StringLength(10), Column("ZIP")]
        public string Zip { get; set; }

        /// <summary>
        /// Der Ort
        /// </summary>
        [StringLength(64), Column("PLACE")]
        public string Place { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        [Column("DISCRIPTION")]
        public string Discription { get; set; }

        /// <summary>
        /// Der Zeitstempel der Erstellung
        /// </summary>
        [Column("TIMESTAMP")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Supplier()
            : base()
        {
        }

        /// <summary>
        /// Umwandlung in String
        /// </summary>
        /// <returns>Das als String umgewandelte Objekt</returns>
        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}
