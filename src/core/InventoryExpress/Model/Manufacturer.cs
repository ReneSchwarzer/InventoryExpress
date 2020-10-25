using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Hersteller
    /// </summary>
    [Table("MANUFACTURER")]
    public class Manufacturer : Item
    {
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
        /// Konstruktor
        /// </summary>
        public Manufacturer()
            : base()
        {
        }
    }
}
