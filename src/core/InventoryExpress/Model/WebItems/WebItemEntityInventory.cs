using InventoryExpress.Model.Entity;
using System;
using System.Collections.Generic;

namespace InventoryExpress.Model.WebItems
{
    public class WebItemEntityInventory : WebItemEntityBaseTag
    {
        /// <summary>
        /// Liefert den Zustand
        /// </summary>
        public WebItemEntityCondition Condition { get; set; }

        /// <summary>
        /// Liefert die Kostenstelle
        /// </summary>
        public WebItemEntityCostCenter CostCenter { get; set; }

        /// <summary>
        /// Liefert das Sachkonto
        /// </summary>
        public WebItemEntityLedgerAccount LedgerAccount { get; set; }

        /// <summary>
        /// Liefert den Standort
        /// </summary>
        public WebItemEntityLocation Location { get; set; }

        /// <summary>
        /// Liefert den Hersteller
        /// </summary>
        public WebItemEntityManufacturer Manufacturer { get; set; }

        /// <summary>
        /// Liefert den Lieferanten
        /// </summary>
        public WebItemEntitySupplier Supplier { get; set; }

        /// <summary>
        /// Liefert die Vorlage
        /// </summary>
        public WebItemEntityTemplate Template { get; set; }

        /// <summary>
        /// Liefert das übergeordnete Inventargegenstand
        /// </summary>
        public WebItemEntityInventory Parent { get; set; }

        /// <summary>
        /// Liefert die Attribute
        /// </summary>
        public IEnumerable<WebItemEntityInventoryAttribute> Attributes { get; set; }

        /// <summary>
        /// Der Anschaffungswert
        /// </summary>
        public decimal CostValue { get; set; }

        /// <summary>
        /// Das Anschaffungsdatum
        /// </summary>
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// Das Abgangsdatum
        /// </summary>
        public DateTime? DerecognitionDate { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntityInventory()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="inventory">Das Datenbankobjektes des Inventargegenstandes</param>
        public WebItemEntityInventory(Inventory inventory)
            : base(inventory)
        {
            Uri = ViewModel.GetInventoryUri(inventory.Guid);

            Condition = ViewModel.GetCondition(this);
            CostCenter = ViewModel.GetCostCenter(this);
            LedgerAccount = ViewModel.GetLedgerAccount(this);
            Location = ViewModel.GetLocation(this);
            Manufacturer = ViewModel.GetManufacturer(this);
            Supplier = ViewModel.GetSupplier(this);
            Template = ViewModel.GetTemplate(this);
            Parent = ViewModel.GetInventoryParent(this);
            Attributes = ViewModel.GetAttributes(this);

            CostValue = inventory.CostValue;
            PurchaseDate = inventory.PurchaseDate;
            DerecognitionDate = inventory.DerecognitionDate;
        }
    }
}
