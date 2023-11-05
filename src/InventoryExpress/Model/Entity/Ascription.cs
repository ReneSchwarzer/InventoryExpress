namespace InventoryExpress.Model.Entity
{
    /// <summary>
    /// A ascription can be made to an inventory in order to expand it (e.g. memory expansion)
    /// without the attribution itself being kept in the inventory.
    /// </summary>
    public class Ascription : ItemTag
    {
        ///// <summary>
        ///// Returns or sets the parent inventory.
        ///// </summary>
        //public Inventory Parent { get; set; }

        ///// <summary>
        ///// Returns or sets the cost value.
        ///// </summary>
        //public decimal CostValue { get; set; }

        ///// <summary>
        ///// Returns or sets the template.
        ///// </summary>
        //public Template Template { get; set; }

        ///// <summary>
        ///// Returns or sets the attributes.
        ///// </summary>
        //public List<AttributeTextValue> Attributes { get; set; }

        ///// <summary>
        ///// Returns or sets the purchase date.
        ///// </summary>
        //public DateTimeOffset? PurchaseDate { get; set; }

        ///// <summary>
        ///// Returns or sets the derecognition date.
        ///// </summary>
        //public DateTimeOffset? DerecognitionDate { get; set; }

        ///// <summary>
        ///// Returns or sets the manufacturer.
        ///// </summary>
        //public Manufacturer Manufacturer { get; set; }

        ///// <summary>
        ///// Returns or sets the state.
        ///// </summary>
        //public Condition State { get; set; }

        ///// <summary>
        ///// Returns or sets the supplier.
        ///// </summary>
        //public Supplier Supplier { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Ascription()
            : base()
        {
            //Attributes = new List<AttributeTextValue>();
            //PurchaseDate = DateTime.Today;
        }
    }
}
