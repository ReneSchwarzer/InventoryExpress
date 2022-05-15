using InventoryExpress.Model.Entity;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Kostenstelle
    /// </summary>
    public class WebItemEntityCostCenter : WebItemEntityBaseTag
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntityCostCenter()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="costCenter">Das Datenbankobjektes der Kosenstelle</param>
        public WebItemEntityCostCenter(CostCenter costCenter)
            : base(costCenter)
        {
            Uri = ViewModel.GetCostCenterUri(costCenter.Guid);
        }
    }
}
