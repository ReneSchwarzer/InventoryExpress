using InventoryExpress.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InventoryExpress.Model.WebItems
{
    public class WebItemEntityInventory : WebItemEntityBaseTag
    {
        /// <summary>
        /// Returns or sets the state.
        /// </summary>
        [JsonPropertyName("condition")]
        public WebItemEntityCondition Condition { get; set; }

        /// <summary>
        /// Returns or sets the cost center.
        /// </summary>
        [JsonPropertyName("costcenter")]
        public WebItemEntityCostCenter CostCenter { get; set; }

        /// <summary>
        /// Returns or sets the ledger account.
        /// </summary>
        [JsonPropertyName("ledgeraccount")]
        public WebItemEntityLedgerAccount LedgerAccount { get; set; }

        /// <summary>
        /// Returns or sets the location.
        /// </summary>
        [JsonPropertyName("location")]
        public WebItemEntityLocation Location { get; set; }

        /// <summary>
        /// Returns or sets the manufacturer.
        /// </summary>
        [JsonPropertyName("manufacturer")]
        public WebItemEntityManufacturer Manufacturer { get; set; }

        /// <summary>
        /// Returns or sets the supplier.
        /// </summary>
        [JsonPropertyName("supplier")]
        public WebItemEntitySupplier Supplier { get; set; }

        /// <summary>
        /// Returns or sets the template.
        /// </summary>
        [JsonPropertyName("template")]
        public WebItemEntityTemplate Template { get; set; }

        /// <summary>
        /// Returns or sets the parent inventory item.
        /// </summary>
        [JsonPropertyName("parent")]
        public WebItemEntityInventory Parent { get; set; }

        /// <summary>
        /// Returns or sets the attributes.
        /// </summary>
        [JsonPropertyName("attributes")]
        public IEnumerable<WebItemEntityInventoryAttribute> Attributes { get; set; }

        /// <summary>
        /// Returns or sets the cost value.
        /// </summary>
        [JsonPropertyName("costvalue")]
        public decimal CostValue { get; set; }

        /// <summary>
        /// Returns or sets the purchase date.
        /// </summary>
        [JsonPropertyName("purchasedate")]
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// Returns or sets the derecognition date.
        /// </summary>
        [JsonPropertyName("derecognitiondate")]
        public DateTime? DerecognitionDate { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntityInventory()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="inventory">The database object of the inventory.</param>
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
