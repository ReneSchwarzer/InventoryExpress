using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Ermittelt alle Kommentare zu einem Inventargegenstand
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand</param>
        /// <returns>Eine Aufzählung mit den Kommentaren</returns>
        public static IEnumerable<WebItemEntityComment> GetInventoryComments(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var comments = from i in DbContext.Inventories
                               join c in DbContext.InventoryComments on i.Id equals c.InventoryId
                               where i.Guid == inventory.Id
                               select new WebItemEntityComment(c);

                return comments.ToList();
            }
        }

        /// <summary>
        /// Fügt ein Kommentar hinzu
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand</param>
        /// <param name="comment">Der Kommentar</param>
        public static void AddInventoryComment(WebItemEntityInventory inventory, WebItemEntityComment comment)
        {
            lock (DbContext)
            {
                var inventoryEntity = DbContext.Inventories.Where(x => x.Guid == inventory.Id).FirstOrDefault();
                var commentEntity = new InventoryComment()
                {
                    InventoryId = inventoryEntity.Id,
                    Guid = comment.Id,
                    Comment = comment.Comment,
                    Created = DateTime.Now,
                    Updated = DateTime.Now
                };

                DbContext.InventoryComments.Add(commentEntity);
                DbContext.SaveChanges();
            }
        }
    }
}