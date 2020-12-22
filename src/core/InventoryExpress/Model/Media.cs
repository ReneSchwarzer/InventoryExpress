﻿using System.ComponentModel.DataAnnotations;
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
