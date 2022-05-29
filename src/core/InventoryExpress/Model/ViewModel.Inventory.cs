using InventoryExpress.Model.WebItems;
using System.Collections.Generic;
using System.IO;
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
            lock (DbContext)
            {
                var inventorys = DbContext.Inventories.Select(x => new WebItemEntityInventory(x));

                return wql.Apply(inventorys.AsQueryable()).ToList();
            }
        }
        
        /// <summary>
        /// Zählt die Inventargegenstände
        /// </summary>
        /// <param name="wql">Die Filteroptinen</param>
        /// <returns>Die Anzahl der Inventargegenstände, welche der Suchanfrage entspricht</returns>
        public static long CountInventories(WqlStatement wql)
        {
            lock (DbContext)
            {
                var inventorys = DbContext.Inventories;

                return wql.Apply(inventorys.AsQueryable()).LongCount();
            }
        }
        
        /// <summary>
        /// Ermittelt die Investitionskosten der Inventargegenstände
        /// </summary>
        /// <param name="wql">Die Filteroptinen</param>
        /// <returns>Die Investitionskosten der Inventargegenstände, welche der Suchanfrage entsprichen</returns>
        public static float GetInventoriesCapitalCosts(WqlStatement wql)
        {
            lock (DbContext)
            {
                var inventorys = DbContext.Inventories;

                return wql.Apply(inventorys.AsQueryable()).Sum(x => (float)x.CostValue);
            }
        }

        /// <summary>
        /// Liefert ein Inventargegenstand
        /// </summary>
        /// <param name="giud">Die InventarID</param>
        /// <returns>Der Inventargegenstände oder null</returns>
        public static WebItemEntityInventory GetInventory(string guid)
        {
            lock (DbContext)
            {
                var inventory = DbContext.Inventories.Where(x => x.Guid == guid).Select(x => new WebItemEntityInventory(x)).FirstOrDefault();

                return inventory;
            }
        }

        /// <summary>
        /// Liefert ein übergeordnetes Inventargegenstand
        /// </summary>
        /// <param name="inventory">Das übergeordnete Inventargegenstand</param>
        /// <returns>Der Inventargegenstände oder null</returns>
        public static WebItemEntityInventory GetInventoryParent(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var entity = from i in DbContext.Inventories
                             join p in DbContext.Inventories on i.ParentId equals p.Id
                             where i.Guid == inventory.ID
                             select new WebItemEntityInventory(p);

                return entity.FirstOrDefault();
            }
        }

        /// <summary>
        /// Löscht ein Hersteller
        /// </summary>
        /// <param name="id">Die ID des Inventargegenstandes</param>
        public static void DeleteInventory(string id)
        {
            lock (DbContext)
            {
                var entity = DbContext.Inventories.Where(x => x.Guid == id).FirstOrDefault();
                var entityMedia = DbContext.Media.Where(x => x.Id == entity.MediaId).FirstOrDefault();
                var entityComments = DbContext.InventoryComments.Where(x => x.InventoryId == entity.Id);
                var entityJournal = DbContext.InventoryJournals.Where(x => x.InventoryId == entity.Id);

                if (entityMedia != null)
                {
                    DeleteMedia(entityMedia.Guid);
                }

                if (entity != null)
                {
                    DbContext.Inventories.Remove(entity);
                    DbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Prüft ob der Inventargegenstand in Verwendung ist
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand</param>
        /// <returns>True wenn in Verwendung, false sonst</returns>
        public static bool GetInventoryInUse(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var used = from i in DbContext.Inventories 
                           join p in DbContext.Inventories on i.Id equals p.ParentId
                           where i.Guid == inventory.ID
                           select i;

                return used.Any();
            }
        }
    }
}