using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.Wql;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Liefert alle Schlagwörter
        /// </summary>
        /// <param name="wql">Die Filter- und Sortieroptinen</param>
        /// <returns>Eine Aufzählung, welche die Schlagwörter beinhaltet</returns>
        public static IEnumerable<WebItemEntityTag> GetTags(WqlStatement wql)
        {
            lock (DbContext)
            {
                var locations = DbContext.Tags.Select(x => new WebItemEntityTag(x));

                return wql.Apply(locations).ToList();
            }
        }

        /// <summary>
        /// Liefert ein Standort
        /// </summary>
        /// <param name="id">Die ID des Standortes</param>
        /// <returns>Der Standort oder null</returns>
        public static WebItemEntityTag GetTag(string id)
        {
            lock (DbContext)
            {
                var location = DbContext.Tags.Where(x => x.Label == id).Select(x => new WebItemEntityTag(x)).FirstOrDefault();

                return location;
            }
        }

        /// <summary>
        /// Fügt ein Schlagwort hinzu oder aktuallisiert dieses
        /// </summary>
        /// <param name="tag">Das Schlagwort</param>
        public static void AddOrUpdateTag(WebItemEntityTag tag)
        {
            lock (DbContext)
            {
                var availableEntity = DbContext.Tags.Where(x => x.Label == tag.Id).FirstOrDefault();

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
        /// Löscht ein Schlagwort
        /// </summary>
        /// <param name="id">Die ID des Schlagwortes</param>
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
        /// Ermittelt alle Schlagwörter zu einem Inventargegenstand
        /// </summary>
        /// <param name="guid">Die ID des Inventargegenstandes</param>
        /// <returns>Eine Aufzählung mit den Schlagwörtern</returns>
        public static IEnumerable<WebItemEntityTag> GetInventoryTags(string guid)
        {
            lock (DbContext)
            {
                var inventoryEntity = DbContext.Inventories.Where(x => x.Guid == guid).FirstOrDefault();

                if (inventoryEntity != null)
                {
                    var split = inventoryEntity.Tag.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    var tags = DbContext.Tags.Where(x => split.Contains(x.Label))
                        .Select(x => new WebItemEntityTag(x));

                    return tags.ToList();
                }

                return new List<WebItemEntityTag>();
            }
        }
    }
}