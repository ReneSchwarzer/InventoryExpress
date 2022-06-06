using InventoryExpress.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InventoryExpress.Model.WebItems
{
    public class WebItemEntityInventory : WebItemEntityBaseTag
    {
        /// <summary>
        /// Liefert den Zustand
        /// </summary>
        [JsonPropertyName("condition")]
        public WebItemEntityCondition Condition { get; set; }

        /// <summary>
        /// Liefert die Kostenstelle
        /// </summary>
        [JsonPropertyName("costcenter")]
        public WebItemEntityCostCenter CostCenter { get; set; }

        /// <summary>
        /// Liefert das Sachkonto
        /// </summary>
        [JsonPropertyName("ledgeraccount")]
        public WebItemEntityLedgerAccount LedgerAccount { get; set; }

        /// <summary>
        /// Liefert den Standort
        /// </summary>
        [JsonPropertyName("location")]
        public WebItemEntityLocation Location { get; set; }

        /// <summary>
        /// Liefert den Hersteller
        /// </summary>
        [JsonPropertyName("manufacturer")]
        public WebItemEntityManufacturer Manufacturer { get; set; }

        /// <summary>
        /// Liefert den Lieferanten
        /// </summary>
        [JsonPropertyName("supplier")]
        public WebItemEntitySupplier Supplier { get; set; }

        /// <summary>
        /// Liefert die Vorlage
        /// </summary>
        [JsonPropertyName("Template")]
        public WebItemEntityTemplate Template { get; set; }

        /// <summary>
        /// Liefert das übergeordnete Inventargegenstand
        /// </summary>
        [JsonPropertyName("parent")]
        public WebItemEntityInventory Parent { get; set; }

        /// <summary>
        /// Liefert die Attribute
        /// </summary>
        [JsonPropertyName("attributes")]
        public IEnumerable<WebItemEntityInventoryAttribute> Attributes { get; set; }

        /// <summary>
        /// Der Anschaffungswert
        /// </summary>
        [JsonPropertyName("costvalue")]
        public decimal CostValue { get; set; }

        /// <summary>
        /// Das Anschaffungsdatum
        /// </summary>
        [JsonPropertyName("purchasedate")]
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// Das Abgangsdatum
        /// </summary>
        [JsonPropertyName("derecognitiondate")]
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
            Attributes = ViewModel.GetInventoryAttributes(this);

            CostValue = inventory.CostValue;
            PurchaseDate = inventory.PurchaseDate;
            DerecognitionDate = inventory.DerecognitionDate;
        }
    }
}
