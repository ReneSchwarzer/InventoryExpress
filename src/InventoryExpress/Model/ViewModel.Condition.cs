using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using InventoryExpress.Parameter;
using InventoryExpress.WebPageSetting;
using InventoryExpress.WebResource;
using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.Wql;
using WebExpress.WebComponent;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Returns the state uri.
        /// </summary>
        /// <returns>The uri or null.</returns>
        public static string GetConditionsUri()
        {
            return ComponentManager.SitemapManager.GetUri<PageSettingConditions>();
        }

        /// <summary>
        /// Returns the state uri.
        /// </summary>
        /// <param name="guid">Returns or sets the id. des Zustandes</param>
        /// <returns>The uri or null.</returns>
        public static string GetConditionUri(string guid)
        {
            return ComponentManager.SitemapManager.GetUri<PageSettingConditionEdit>(new ParameterConditionId(guid));
        }

        /// <summary>
        /// Returns the state uri.
        /// </summary>
        /// <param name="guid">Returns or sets the id. des Zustandes</param>
        /// <returns>The uri or null.</returns>
        public static string GetConditionAddUri(string guid)
        {
            return ComponentManager.SitemapManager.GetUri<PageSettingConditionAdd>();
        }

        /// <summary>
        /// Returns the state uri.
        /// </summary>
        /// <param name="grade">Der Zustand</param>
        /// <returns>The uri or null.</returns>
        public static string GetConditionIamgeUri(int grade)
        {
            return ComponentManager.SitemapManager.GetUri<ResourceAsset>().Append($"/img/condition_{grade}.svg");
        }

        /// <summary>
        /// Liefert alle Zustände
        /// </summary>
        /// <param name="wql">Die Filter- und Sortieroptinen</param>
        /// <returns>Eine Aufzählung, welche die Zustände beinhaltet</returns>
        public static IEnumerable<WebItemEntityCondition> GetConditions(WqlStatement wql)
        {
            lock (DbContext)
            {
                var conditions = DbContext.Conditions.Select(x => new WebItemEntityCondition(x));

                return wql.Apply(conditions.AsQueryable()).ToList();
            }
        }

        /// <summary>
        /// Liefert ein Zustand
        /// </summary>
        /// <param name="id">Returns or sets the id. des Zustandes</param>
        /// <returns>Der Zustand oder null</returns>
        public static WebItemEntityCondition GetCondition(string id)
        {
            lock (DbContext)
            {
                var condition = DbContext.Conditions.Where(x => x.Guid == id).Select(x => new WebItemEntityCondition(x)).FirstOrDefault();

                return condition;
            }
        }

        /// <summary>
        /// Liefert den Zustand, welche dem Inventargegenstand zugeordnet ist
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <returns>Der Zustand oder null</returns>
        public static WebItemEntityCondition GetCondition(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var condition = from i in DbContext.Inventories
                                join c in DbContext.Conditions on i.ConditionId equals c.Id
                                where i.Guid == inventory.Id
                                select new WebItemEntityCondition(c);

                return condition.FirstOrDefault();
            }
        }

        /// <summary>
        /// Fügt ein Zustand hinzu oder aktuallisiert diesen
        /// </summary>
        /// <param name="condition">Der Zustand</param>
        public static void AddOrUpdateCondition(WebItemEntityCondition condition)
        {
            lock (DbContext)
            {
                var availableEntity = DbContext.Conditions.Where(x => x.Guid == condition.Id).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Condition()
                    {
                        Guid = condition.Id,
                        Name = condition.Name,
                        Description = condition.Description,
                        Grade = condition.Grade,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Media = new Media()
                        {
                            Guid = condition.Media?.Id,
                            Name = condition.Media?.Name ?? "",
                            Description = condition.Media?.Description,
                            Tag = condition.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        }
                    };

                    DbContext.Conditions.Add(entity);
                    DbContext.SaveChanges();
                }
                else
                {
                    // Update
                    var availableMedia = condition.Media != null ? DbContext.Media.Where(x => x.Guid == condition.Media.Id).FirstOrDefault() : null;

                    availableEntity.Name = condition.Name;
                    availableEntity.Description = condition.Description;
                    availableEntity.Grade = condition.Grade;
                    availableEntity.Updated = DateTime.Now;

                    if (availableMedia == null)
                    {
                        var media = new Media()
                        {
                            Guid = condition.Media?.Id,
                            Name = condition.Media?.Name,
                            Description = condition.Media?.Description,
                            Tag = condition.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        };

                        DbContext.Media.Add(media);
                    }
                    else if (!string.IsNullOrWhiteSpace(condition.Media.Name))
                    {
                        availableMedia.Name = condition.Media?.Name;
                        availableMedia.Description = condition.Media?.Description;
                        availableMedia.Tag = condition.Media?.Tag;
                        availableMedia.Updated = DateTime.Now;
                    }

                    DbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Löscht ein Zustand
        /// </summary>
        /// <param name="id">Returns or sets the id. des Zustandes</param>
        public static void DeleteCondition(string id)
        {
            lock (DbContext)
            {
                var entity = DbContext.Conditions.Where(x => x.Guid == id).FirstOrDefault();
                var entityMedia = DbContext.Media.Where(x => x.Id == entity.MediaId).FirstOrDefault();

                if (entityMedia != null)
                {
                    DeleteMedia(entityMedia.Guid);
                }

                if (entity != null)
                {
                    DbContext.Conditions.Remove(entity);
                    DbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Prüft ob der Zustand in Verwendung ist
        /// </summary>
        /// <param name="condition">Der Zustand</param>
        /// <returns>True wenn in Verwendung, false sonst</returns>
        public static bool GetConditionInUse(WebItemEntityCondition condition)
        {
            lock (DbContext)
            {
                var used = from i in DbContext.Inventories
                           join c in DbContext.Conditions on i.ConditionId equals c.Id
                           where c.Guid == condition.Id
                           select c;

                return used.Any();
            }
        }
    }
}