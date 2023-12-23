using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebIndex.Wql;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Returns all tags.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>A enumaration that includes the tags.</returns>
        public static IEnumerable<WebItemEntityTag> GetTags(IWqlStatement<WebItemEntityTag> wql)
        {
            lock (DbContext)
            {
                var tags = DbContext.Tags.Select(x => new WebItemEntityTag(x));

                return wql.Apply(tags).ToList();
            }
        }

        /// <summary>
        /// Returns a tag.
        /// </summary>
        /// <param name="id">The id of the tag.</param>
        /// <returns>The tag or null.</returns>
        public static WebItemEntityTag GetTag(string id)
        {
            lock (DbContext)
            {
                var location = DbContext.Tags.Where(x => x.Label == id).Select(x => new WebItemEntityTag(x)).FirstOrDefault();

                return location;
            }
        }

        /// <summary>
        /// Adds or updates a tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public static void AddOrUpdateTag(WebItemEntityTag tag)
        {
            lock (DbContext)
            {
                var availableEntity = DbContext.Tags.Where(x => x.Label == tag.Guid).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Tag()
                    {
                        Label = tag.Label
                    };

                    DbContext.Tags.Add(entity);
                }
                else
                {
                    // Update
                    availableEntity.Label = tag.Label;
                }

                DbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a tag.
        /// </summary>
        /// <param name="id">The id of the tag.</param>
        public static void DeleteTag(string id)
        {
            lock (DbContext)
            {
                var entity = DbContext.Tags.Where(x => x.Label == id).FirstOrDefault();

                if (entity != null)
                {
                    DbContext.Tags.Remove(entity);
                    DbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Identifies all tags related to an inventory item.
        /// </summary>
        /// <param name="guid">The guid of the inventory item.</param>
        /// <returns>A enumaration that includes the tags.</returns>
        public static IEnumerable<WebItemEntityTag> GetInventoryTags(string guid)
        {
            lock (DbContext)
            {
                var inventoryEntity = DbContext.Inventories.Where(x => x.Guid == guid).FirstOrDefault();

                if (inventoryEntity != null)
                {
                    var split = inventoryEntity.Tag?.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    if (split != null)
                    {
                        var tags = DbContext.Tags.Where(x => split.Contains(x.Label))
                            .Select(x => new WebItemEntityTag(x));

                        return tags.ToList();
                    }
                }

                return new List<WebItemEntityTag>();
            }
        }
    }
}