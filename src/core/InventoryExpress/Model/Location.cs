using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Standort
    /// </summary>
    [Table("LOCATION")]
    public class Location : Item
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
        /// Das Gebäude
        /// </summary>
        [StringLength(64), Column("BUILDING")]
        public string Building { get; set; }

        /// <summary>
        /// Der Raum innerhalb des Gebäudes
        /// </summary>
        [StringLength(64), Column("ROOM")]
        public string Room { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Location()
            : base()
        {
        }
    }
}
