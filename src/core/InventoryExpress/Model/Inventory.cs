using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Inventar
    /// </summary>
    [Table("INVENTORY")]
    public class Inventory : ItemTag
    {
        /// <summary>
        /// Der Anschaffungswert
        /// </summary>
        [Column("COSTVALUE")]
        public decimal CostValue { get; set; }

        /// <summary>
        /// Die ID der Vorlage
        /// </summary>
        [Column("TEMPLATEID")]
        public int? TemplateId { get; set; }

        /// <summary>
        /// Das Template
        /// </summary>
        public virtual Template Template { get; set; }

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
        [Column("PURCHASEDATE")]
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// Das Abgangsdatum
        /// </summary>
        [Column("DERECOGNITIONDATE")]
        public DateTime? DerecognitionDate { get; set; }

        /// <summary>
        /// Die ID des Standortes
        /// </summary>
        [Column("LOCATIONID")]
        public int? LocationId { get; set; }

        /// <summary>
        /// Der Standort
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Die ID der Kostenstelle
        /// </summary>
        [Column("COSTCENTERID")]
        public int? CostCenterId { get; set; }

        /// <summary>
        /// Die Kostenstelle
        /// </summary>
        public virtual CostCenter CostCenter { get; set; }

        /// <summary>
        /// Die ID des Herstellers
        /// </summary>
        [Column("MANUFACTURERID")]
        public int? ManufacturerId { get; set; }

        /// <summary>
        /// Der Hersteller
        /// </summary>
        public virtual Manufacturer Manufacturer { get; set; }

        /// <summary>
        /// Die ID des Zustandes
        /// </summary>
        [Column("CONDITIONID")]
        public int? ConditionId { get; set; }

        /// <summary>
        /// Der Zustand
        /// </summary>
        public virtual Condition Condition { get; set; }

        /// <summary>
        /// Die ID des Lieferanten
        /// </summary>
        [Column("SUPPLIERID")]
        public int? SupplierId { get; set; }

        /// <summary>
        /// Der Lieferant
        /// </summary>
        public virtual Supplier Supplier { get; set; }

        /// <summary>
        /// Die ID des Sachkontos
        /// </summary>
        [Column("LEDGERACCOUNTID")]
        public int? LedgerAccountId { get; set; }

        /// <summary>
        /// Das Sachkonto
        /// </summary>
        public virtual LedgerAccount LedgerAccount { get; set; }

        public virtual ICollection<InventoryAttribute> InventoryAttributes { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Inventory()
            : base()
        {
            InventoryAttributes = new HashSet<InventoryAttribute>();

            PurchaseDate = DateTime.Today;
        }
    }
}
