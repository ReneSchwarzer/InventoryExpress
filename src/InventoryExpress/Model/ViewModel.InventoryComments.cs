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
        /// Returns all comments on an inventory item.
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <returns>A enumaration with the comments.</returns>
        public static IEnumerable<WebItemEntityComment> GetInventoryComments(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var comments = from i in DbContext.Inventories
                               join c in DbContext.InventoryComments on i.Id equals c.InventoryId
                               where i.Guid == inventory.Guid
                               select new WebItemEntityComment(c);

                return comments.ToList();
            }
        }

        /// <summary>
        /// Adds a comment.
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <param name="comment">The commentary.</param>
        public static void AddInventoryComment(WebItemEntityInventory inventory, WebItemEntityComment comment)
        {
            lock (DbContext)
            {
                var inventoryEntity = DbContext.Inventories.Where(x => x.Guid == inventory.Guid).FirstOrDefault();
                var commentEntity = new InventoryComment()
                {
                    InventoryId = inventoryEntity.Id,
                    Guid = comment.Guid,
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