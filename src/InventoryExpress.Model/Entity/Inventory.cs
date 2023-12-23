using System;
using System.Collections.Generic;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// The inventory item.
    /// </summary>
    internal class Inventory : ItemTag
    {
        /// <summary>
        /// Returns or set the cost value.
        /// </summary>
        public decimal CostValue { get; set; }

        /// <summary>
        /// Returns or set the id of the template.
        /// </summary>
        public int? TemplateId { get; set; }

        /// <summary>
        /// Returns or set the template.
        /// </summary>
        public Template Template { get; set; }

        /// <summary>
        /// Returns or set the ascriptions.
        /// </summary>
        public List<Ascription> Ascriptions { get; set; }

        /// <summary>
        /// Das Konto ist ein Favorit
        /// </summary>
        //public bool Like { get; set; }

        /// <summary>
        /// Returns or set the purchase date.
        /// </summary>
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// Returns or set the derecognition date.
        /// </summary>
        public DateTime? DerecognitionDate { get; set; }

        /// <summary>
        /// Returns or set the id of the location.
        /// </summary>
        public int? LocationId { get; set; }

        /// <summary>
        /// Returns or set the location.
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Returns or set the id of the cost center.
        /// </summary>
        public int? CostCenterId { get; set; }

        /// <summary>
        /// Returns or set the cost center.
        /// </summary>
        public CostCenter CostCenter { get; set; }

        /// <summary>
        /// Returns or sets the id of the manufacturer.
        /// </summary>
        public int? ManufacturerId { get; set; }

        /// <summary>
        /// Returns or set the manufacturer.
        /// </summary>
        public Manufacturer Manufacturer { get; set; }

        /// <summary>
        /// Returns or set the id of the state.
        /// </summary>
        public int? ConditionId { get; set; }

        /// <summary>
        /// Returns or set the state.
        /// </summary>
        public Condition Condition { get; set; }

        /// <summary>
        /// Returns or sets the id of the supplier.
        /// </summary>
        public int? SupplierId { get; set; }

        /// <summary>
        /// Returns or set the supplier.
        /// </summary>
        public Supplier Supplier { get; set; }

        /// <summary>
        /// Returns or set the id of the ledger account.
        /// </summary>
        public int? LedgerAccountId { get; set; }

        /// <summary>
        /// Returns or set the ledger account.
        /// </summary>
        public LedgerAccount LedgerAccount { get; set; }

        /// <summary>
        /// Returns or sets the id parenr inventory.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Returns or set the parent.
        /// </summary>
        public Inventory Parent { get; set; }

        public virtual ICollection<InventoryAttribute> InventoryAttributes { get; set; }
        public virtual ICollection<InventoryAttachment> InventoryMedia { get; set; }
        public virtual ICollection<InventoryComment> InventoryComments { get; set; }
        public virtual ICollection<InventoryJournal> InventoryJournals { get; set; }
        public virtual ICollection<InventoryTag> InventoryTag { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Inventory()
            : base()
        {
            PurchaseDate = DateTime.Today;
        }
    }
}
