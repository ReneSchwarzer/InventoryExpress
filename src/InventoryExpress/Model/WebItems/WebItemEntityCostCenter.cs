using InventoryExpress.Model.Entity;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// The cost center.
    /// </summary>
    public class WebItemEntityCostCenter : WebItemEntityBaseTag
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntityCostCenter()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="costCenter">The database object of the cost center.</param>
        public WebItemEntityCostCenter(CostCenter costCenter)
            : base(costCenter)
        {
            Uri = ViewModel.GetCostCenterUri(costCenter.Guid);
        }
    }
}
