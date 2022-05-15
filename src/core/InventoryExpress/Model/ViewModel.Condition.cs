using InventoryExpress.Model.WebItems;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.Wql;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Ermittelt die Zustands-URL
        /// </summary>
        /// <param name="guid">Die ZustandsID</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetConditionUri(string guid)
        {
            return $"{ RootUri }/setting/conditions/{ guid }";
        }

        /// <summary>
        /// Ermittelt die Zustands-URL
        /// </summary>
        /// <param name="grade">Der Zustand</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetConditionIamgeUri(int grade)
        {
            return $"{ RootUri }/assets/img/condition_{ grade }.svg";
        }

        /// <summary>
        /// Liefert alle Zustände
        /// </summary>
        /// <param name="wql">Die Filter- und Sortieroptinen</param>
        /// <returns>Eine Aufzählung, welche die Zustände beinhaltet</returns>
        public static IEnumerable<WebItemEntityCondition> GetConditions(WqlStatement wql)
        {
            lock (Instance.Database)
            {
                var conditions = Instance.Conditions.Select(x => new WebItemEntityCondition(x));

                return wql.Apply(conditions.AsQueryable());
            }
        }

        /// <summary>
        /// Liefert ein Zustand
        /// </summary>
        /// <param name="id">Die interne ZustandsID</param>
        /// <returns>Der Zustands oder null</returns>
        public static WebItemEntityCondition GetCondition(int? id)
        {
            lock (Instance.Database)
            {
                var condition = Instance.Conditions.Where(x => x.Id == id).Select(x => new WebItemEntityCondition(x)).FirstOrDefault();

                return condition;
            }
        }

        /// <summary>
        /// Liefert den Zustand, welche dem Inventargegenstand zugeordnet ist
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand</param>
        /// <returns>Der Zustand oder null</returns>
        public static WebItemEntityCondition GetCondition(WebItemEntityInventory inventory)
        {
            lock (Instance.Database)
            {
                var condition = from i in Instance.Inventories
                                join c in Instance.Conditions on i.ConditionId equals c.Id
                                where i.Guid == inventory.ID
                                select new WebItemEntityCondition(c);

                return condition.FirstOrDefault();
            }
        }
    }
}