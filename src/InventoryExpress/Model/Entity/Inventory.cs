using System;
using System.Collections.Generic;

namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// Inventar
    /// </summary>
    public class Inventory : ItemTag
    {
        /// <summary>
        /// Der Anschaffungswert
        /// </summary>
        public decimal CostValue { get; set; }

        /// <summary>
        /// Die Id der Vorlage
        /// </summary>
        public int? TemplateId { get; set; }

        /// <summary>
        /// Das Template
        /// </summary>
        public Template Template { get; set; }

        /// <summary>
        /// Die Zuschreibungen
        /// </summary>
        public List<Ascription> Ascriptions { get; set; }

        /// <summary>
        /// Das Konto ist ein Favorit
        /// </summary>
        //public bool Like { get; set; }

        /// <summary>
        /// Das Anschaffungsdatum
        /// </summary>
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// Das Abgangsdatum
        /// </summary>
        public DateTime? DerecognitionDate { get; set; }

        /// <summary>
        /// Die Id des Standortes
        /// </summary>
        public int? LocationId { get; set; }

        /// <summary>
        /// Der Standort
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Die Id der Kostenstelle
        /// </summary>
        public int? CostCenterId { get; set; }

        /// <summary>
        /// Die Kostenstelle
        /// </summary>
        public CostCenter CostCenter { get; set; }

        /// <summary>
        /// Die Id des Herstellers
        /// </summary>
        public int? ManufacturerId { get; set; }

        /// <summary>
        /// Der Hersteller
        /// </summary>
        public Manufacturer Manufacturer { get; set; }

        /// <summary>
        /// Die Id des Zustandes
        /// </summary>
        public int? ConditionId { get; set; }

        /// <summary>
        /// Der Zustand
        /// </summary>
        public Condition Condition { get; set; }

        /// <summary>
        /// Die Id des Lieferanten
        /// </summary>
        public int? SupplierId { get; set; }

        /// <summary>
        /// Der Lieferant
        /// </summary>
        public Supplier Supplier { get; set; }

        /// <summary>
        /// Die Id des Sachkontos
        /// </summary>
        public int? LedgerAccountId { get; set; }

        /// <summary>
        /// Das Sachkonto
        /// </summary>
        public LedgerAccount LedgerAccount { get; set; }

        /// <summary>
        /// Die Id übergeordneten Inventargegenstandes
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Der übergeordnete Inventargegenstand
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
