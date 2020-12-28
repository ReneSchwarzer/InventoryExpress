using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Mediendateien
    /// </summary>
    [Table("MEDIA")]
    public class Media 
    {
        /// <summary>
        /// Die ID
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        /// <summary>
        /// Liefert oder setzt den Namen
        /// </summary>
        [StringLength(64), Required, Column("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// Liefert oder setzt den Namen
        /// </summary>
        [Column("DATA")]
        public byte[] Data { get; set; }

        /// <summary>
        /// Die Postleitzahl
        /// </summary>
        [StringLength(256), Column("TAG")]
        public string Tag { get; set; }

        /// <summary>
        /// Der Zeitstempel der Erstellung
        /// </summary>
        [Column("CREATED")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Der Zeitstempel der letzten Änderung
        /// </summary>
        [Column("UPDATED")]
        public DateTime Updated { get; set; }

        /// <summary>
        /// Die GUID
        /// </summary>
        [Column("GUID")]
        public string Guid { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Media()
            : base()
        {

        }
    }
}
