using InventoryExpress.Model.WebItems;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.Wql;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Ermittelt die Inventar-URL
        /// </summary>
        /// <param name="guid">Die InventarID</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetInventoryUri(string guid)
        {
            return $"{RootUri}/{guid}";
        }

        /// <summary>
        /// Liefert alle Inventargegenstände
        /// </summary>
        /// <param name="wql">Die Filter- und Sortieroptinen</param>
        /// <returns>Eine Aufzählung, welche die Inventargegenstände beinhaltet</returns>
        public static IEnumerable<WebItemEntityInventory> GetInventories(WqlStatement wql)
        {
            lock (Instance.Database)
            {
                var inventorys = Instance.Inventories.Select(x => new WebItemEntityInventory(x));

                return wql.Apply(inventorys.AsQueryable());
            }
        }

        /// <summary>
        /// Liefert einen Lieferanten
        /// </summary>
        /// <param name="id">Die interne InventarID</param>
        /// <returns>Der Inventargegenstände oder null</returns>
        public static WebItemEntityInventory GetInventory(int? id)
        {
            lock (Instance.Database)
            {
                var inventory = Instance.Inventories.Where(x => x.Id == id).Select(x => new WebItemEntityInventory(x)).FirstOrDefault();

                return inventory;
            }
        }
    }
}