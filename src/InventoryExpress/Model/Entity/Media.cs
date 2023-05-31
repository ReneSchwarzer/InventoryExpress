﻿using System;
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
        /// The timestamp of the last change.
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

        public virtual ICollection<Ascription> Ascriptions { get; set; }
        public virtual ICollection<Attribute> Attributes { get; set; }
        public virtual ICollection<Condition> Conditions { get; set; }
        public virtual ICollection<CostCenter> CostCenters { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<InventoryAttachment> InventoryAttachment { get; set; }
        public virtual ICollection<LedgerAccount> LedgerAccounts { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Manufacturer> Manufacturers { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
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
