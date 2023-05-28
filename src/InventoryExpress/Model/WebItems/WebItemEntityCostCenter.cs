using InventoryExpress.Model.Entity;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Kostenstelle
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
        /// <param name="costCenter">Das Datenbankobjektes der Kosenstelle</param>
        public WebItemEntityCostCenter(CostCenter costCenter)
            : base(costCenter)
        {
            Uri = ViewModel.GetCostCenterUri(costCenter.Guid);
        }
    }
}
