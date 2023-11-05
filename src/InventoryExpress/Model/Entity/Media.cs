using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// Mediendateien
    /// </summary>
    [Table("MEDIA")]
    public class Media
    {
        /// <summary>
        /// Returns or sets the id.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Returns or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Returns or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Returns or sets the timestamp of the creation.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Returns or sets the timestamp of the last change.
        /// </summary>
        public DateTime Updated { get; set; }

        /// <summary>
        /// Returns or sets the guid.
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Returns or sets the tags.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Returns or sets the related ascription items.
        /// </summary>
        public virtual ICollection<Ascription> Ascriptions { get; set; }

        /// <summary>
        /// Returns or sets the related attribute items.
        /// </summary>
        public virtual ICollection<Attribute> Attributes { get; set; }

        /// <summary>
        /// Returns or sets the related state items.
        /// </summary>
        public virtual ICollection<Condition> Conditions { get; set; }

        /// <summary>
        /// Returns or sets the related cost center items.
        /// </summary>
        public virtual ICollection<CostCenter> CostCenters { get; set; }

        /// <summary>
        /// Returns or sets the related inventory items.
        /// </summary>
        public virtual ICollection<Inventory> Inventories { get; set; }

        /// <summary>
        /// Returns or sets the related inventory attatchment items.
        /// </summary>
        public virtual ICollection<InventoryAttachment> InventoryAttachment { get; set; }

        /// <summary>
        /// Returns or sets the related ledger account items.
        /// </summary>
        public virtual ICollection<LedgerAccount> LedgerAccounts { get; set; }

        /// <summary>
        /// Returns or sets the related location items.
        /// </summary>
        public virtual ICollection<Location> Locations { get; set; }

        /// <summary>
        /// Returns or sets the related manufacturer items.
        /// </summary>
        public virtual ICollection<Manufacturer> Manufacturers { get; set; }

        /// <summary>
        /// Returns or sets the related suppliers items.
        /// </summary>
        public virtual ICollection<Supplier> Suppliers { get; set; }

        /// <summary>
        /// Returns or sets the related template items.
        /// </summary>
        public virtual ICollection<Template> Templates { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Media()
            : base()
        {
            Ascriptions = new HashSet<Ascription>();
            Attributes = new HashSet<Attribute>();
            Conditions = new HashSet<Condition>();
            CostCenters = new HashSet<CostCenter>();
            Inventories = new HashSet<Inventory>();
            InventoryAttachment = new HashSet<InventoryAttachment>();
            LedgerAccounts = new HashSet<LedgerAccount>();
            Locations = new HashSet<Location>();
            Manufacturers = new HashSet<Manufacturer>();
            Suppliers = new HashSet<Supplier>();
            Templates = new HashSet<Template>();
        }
    }
}
