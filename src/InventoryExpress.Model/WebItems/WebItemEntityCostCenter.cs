using InventoryExpress.Model.Entity;
using System;

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
        internal WebItemEntityCostCenter(CostCenter costCenter)
            : base(costCenter)
        {
            Uri = ViewModel.GetUri(this).Append(costCenter.Guid);
        }
    }
}
