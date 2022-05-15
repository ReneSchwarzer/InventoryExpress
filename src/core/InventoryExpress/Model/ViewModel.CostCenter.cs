using InventoryExpress.Model.WebItems;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.Wql;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Ermittelt die Kostenstelle-URL
        /// </summary>
        /// <param name="guid">Die KostenstellenID</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetCostCenterUri(string guid)
        {
            return $"{ RootUri }/costcenters/{ guid }";
        }

        /// <summary>
        /// Liefert alle Kostenstellen
        /// </summary>
        /// <param name="wql">Die Filter- und Sortieroptinen</param>
        /// <returns>Eine Aufzählung, welche die Kostenstelle beinhaltet</returns>
        public IEnumerable<WebItemEntityCostCenter> GetCostCenters(WqlStatement wql)
        {
            lock (Database)
            {
                var costCenters = CostCenters.Select(x => new WebItemEntityCostCenter(x));

                return wql.Apply(costCenters.AsQueryable());
            }
        }

        /// <summary>
        /// Liefert eine Kostenstelle
        /// </summary>
        /// <param name="id">Die interne KostenstellenID</param>
        /// <returns>Die Kostenstelle oder null</returns>
        public WebItemEntityCostCenter GetCostCenter(int? id)
        {
            lock (Database)
            {
                var costCenter = CostCenters.Where(x => x.Id == id).Select(x => new WebItemEntityCostCenter(x)).FirstOrDefault();

                return costCenter;
            }
        }

        /// <summary>
        /// Liefert die Kostenstelle, welche dem Inventargegenstand zugeordnet ist
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand</param>
        /// <returns>Die Kostenstelle oder null</returns>
        public static WebItemEntityCostCenter GetCostCenter(WebItemEntityInventory inventory)
        {
            lock (Instance.Database)
            {
                var costCenter = from i in Instance.Inventories
                                 join c in Instance.CostCenters on i.ConditionId equals c.Id
                                 where i.Guid == inventory.ID
                                 select new WebItemEntityCostCenter(c);

                return costCenter.FirstOrDefault();
            }
        }
    }
}