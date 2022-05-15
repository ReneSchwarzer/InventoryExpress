using InventoryExpress.Model.WebItems;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.Wql;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Ermittelt die Standort-URL
        /// </summary>
        /// <param name="guid">Die StandortID</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetLocationUri(string guid)
        {
            return $"{ RootUri }/locations/{ guid }";
        }

        /// <summary>
        /// Liefert alle Standorte
        /// </summary>
        /// <param name="wql">Die Filter- und Sortieroptinen</param>
        /// <returns>Eine Aufzählung, welche die Standorte beinhaltet</returns>
        public static IEnumerable<WebItemEntityLocation> GetLocations(WqlStatement wql)
        {
            lock (Instance.Database)
            {
                var locations = Instance.Locations.Select(x => new WebItemEntityLocation(x));

                return wql.Apply(locations);
            }
        }

        /// <summary>
        /// Liefert ein Standort
        /// </summary>
        /// <param name="id">Die interne StandortID</param>
        /// <returns>Der Standort oder null</returns>
        public static WebItemEntityLocation GetLocation(int? id)
        {
            lock (Instance.Database)
            {
                var location = Instance.Locations.Where(x => x.Id == id).Select(x => new WebItemEntityLocation(x)).FirstOrDefault();

                return location;
            }
        }

        /// <summary>
        /// Liefert ein Standort, welche dem Inventargegenstand zugeordnet ist
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand</param>
        /// <returns>Der Standort oder null</returns>
        public static WebItemEntityLocation GetLocation(WebItemEntityInventory inventory)
        {
            lock (Instance.Database)
            {
                var location = from i in Instance.Inventories
                                 join l in Instance.Locations on i.ConditionId equals l.Id
                                 where i.Guid == inventory.ID
                                 select new WebItemEntityLocation(l);

                return location.FirstOrDefault();
            }
        }
    }
}