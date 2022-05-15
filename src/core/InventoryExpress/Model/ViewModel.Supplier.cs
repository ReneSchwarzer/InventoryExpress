using InventoryExpress.Model.WebItems;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.Wql;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Ermittelt die Lieferanten-URL
        /// </summary>
        /// <param name="guid">Die LieferantenID</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetSupplierUri(string guid)
        {
            return $"{ RootUri }/suppliers/{ guid }";
        }

        /// <summary>
        /// Liefert alle Lieferanten
        /// </summary>
        /// <param name="wql">Die Filter- und Sortieroptinen</param>
        /// <returns>Eine Aufzählung, welche die Hersteller beinhaltet</returns>
        public IEnumerable<WebItemEntitySupplier> GetSuppliers(WqlStatement wql)
        {
            lock (Database)
            {
                var suppliers = Suppliers.Select(x => new WebItemEntitySupplier(x));

                return wql.Apply(suppliers.AsQueryable());
            }
        }

        /// <summary>
        /// Liefert einen Lieferanten
        /// </summary>
        /// <param name="id">Die interne LieferantenID</param>
        /// <returns>Der Lieferant oder null</returns>
        public WebItemEntitySupplier GetSupplier(int? id)
        {
            lock (Database)
            {
                var supplier = Suppliers.Where(x => x.Id == id).Select(x => new WebItemEntitySupplier(x)).FirstOrDefault();

                return supplier;
            }
        }

        /// <summary>
        /// Liefert den Lieferanten, welche dem Inventargegenstand zugeordnet ist
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand</param>
        /// <returns>Der Lieferant oder null</returns>
        public static WebItemEntitySupplier GetSupplier(WebItemEntityInventory inventory)
        {
            lock (Instance.Database)
            {
                var supplier = from i in Instance.Inventories
                               join s in Instance.Suppliers on i.ConditionId equals s.Id
                               where i.Guid == inventory.ID
                               select new WebItemEntitySupplier(s);

                return supplier.FirstOrDefault();
            }
        }
    }
}