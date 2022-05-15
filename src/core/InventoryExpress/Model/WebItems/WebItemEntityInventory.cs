using InventoryExpress.Model.Entity;

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
        /// Konstruktor
        /// </summary>
        public WebItemEntityInventory()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="costCenter">Das Datenbankobjektes der Kosenstelle</param>
        public WebItemEntityInventory(Inventory costCenter)
            : base(costCenter)
        {
            Uri = ViewModel.GetInventoryUri(costCenter.Guid);

            Condition = ViewModel.GetCondition(this);
            CostCenter = ViewModel.GetCostCenter(this);
            LedgerAccount = ViewModel.GetLedgerAccount(this);
            Location = ViewModel.GetLocation(this);
            Manufacturer = ViewModel.GetManufacturer(this);
            Supplier = ViewModel.GetSupplier(this);
            Template = ViewModel.GetTemplate(this);
        }
    }
}
