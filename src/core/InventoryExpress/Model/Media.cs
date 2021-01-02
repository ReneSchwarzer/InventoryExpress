using System;
using System.Collections.Generic;
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
        public int Id { get; set; }

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

        public virtual ICollection<Ascription> Ascriptions { get; set; }
        public virtual ICollection<Attribute> Attributes { get; set; }
        public virtual ICollection<Condition> Conditions { get; set; }
        public virtual ICollection<CostCenter> CostCenters { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<InventoryMedia> InventoryMedia { get; set; }
        public virtual ICollection<LedgerAccount> LedgerAccounts { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Manufacturer> Manufacturers { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<Template> Templates { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Media()
            : base()
        {
            Ascriptions = new HashSet<Ascription>();
            Attributes = new HashSet<Attribute>();
            Conditions = new HashSet<Condition>();
            CostCenters = new HashSet<CostCenter>();
            Inventories = new HashSet<Inventory>();
            InventoryMedia = new HashSet<InventoryMedia>();
            LedgerAccounts = new HashSet<LedgerAccount>();
            Locations = new HashSet<Location>();
            Manufacturers = new HashSet<Manufacturer>();
            Suppliers = new HashSet<Supplier>();
            Templates = new HashSet<Template>();
        }
    }
}
