using System;
using System.Collections.Generic;

namespace InventoryExpress.Model
{
    /// <summary>
    /// Zuschreibung
    /// 
    /// Eine Zuschreibung kann auf ein Inventar erfolgen, um diesen zu Erweitern (z.B. Speichererweiterung)
    /// ohne das die Zuschreibung selbst ím Inventar eigeständig geführt wird
    /// </summary>
    public class Ascription : Item
    {
        /// <summary>
        /// Liefert oder setzt das übergeordnete Inventar
        /// </summary>
        public Inventory Parent { get; set; }

        /// <summary>
        /// Der Anschaffungswert
        /// </summary>
        public decimal CostValue { get; set; }

        /// <summary>
        /// Das Template
        /// </summary>
        public Template Template { get; set; }

        /// <summary>
        /// Die Attribute des Kontos
        /// </summary>
        public List<AttributeTextValue> Attributes { get; set; }

        /// <summary>
        /// Das Anschaffungsdatum
        /// </summary>
        public DateTimeOffset? PurchaseDate { get; set; }

        /// <summary>
        /// Das Abgangsdatum
        /// </summary>
        public DateTimeOffset? DerecognitionDate { get; set; }

        /// <summary>
        /// Der Hersteller
        /// </summary>
        public Manufacturer Manufacturer { get; set; }

        /// <summary>
        /// Der Zustand
        /// </summary>
        public Condition State { get; set; }

        /// <summary>
        /// Der Lieferant
        /// </summary>
        public Supplier Supplier { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Ascription()
            : base()
        {
            Attributes = new List<AttributeTextValue>();
            PurchaseDate = DateTime.Today;
        }


    }
}
