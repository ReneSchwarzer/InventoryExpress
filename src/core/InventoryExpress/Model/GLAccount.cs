using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Sachkonto
    /// </summary>
    [Table("GLACCOUNT")]
    public class GLAccount
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
        public GLAccount()
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
