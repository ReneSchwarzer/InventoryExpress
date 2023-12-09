using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using InventoryExpress.Parameter;
using InventoryExpress.WebPageSetting;
using InventoryExpress.WebResource;
using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.WebIndex;
using WebExpress.WebCore.WebComponent;
using WebExpress.WebIndex.Wql;

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
        /// <param name="guid">The id of the state.</param>
        /// <returns>The uri or null.</returns>
        public static string GetConditionUri(string guid)
        {
            return ComponentManager.SitemapManager.GetUri<PageSettingConditionEdit>(new ParameterConditionId(guid));
        }

        /// <summary>
        /// Returns the state uri.
        /// </summary>
        /// <param name="guid">The id of the state.</param>
        /// <returns>The uri or null.</returns>
        public static string GetConditionAddUri(string guid)
        {
            return ComponentManager.SitemapManager.GetUri<PageSettingConditionAdd>();
        }

        /// <summary>
        /// Returns the state uri.
        /// </summary>
        /// <param name="grade">The state.</param>
        /// <returns>The uri or null.</returns>
        public static string GetConditionIamgeUri(int grade)
        {
            return ComponentManager.SitemapManager.GetUri<ResourceAsset>().Append($"/img/condition_{grade}.svg");
        }

        /// <summary>
        /// Returns all conditions.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>An enumeration that includes the conditions.</returns>
        public static IEnumerable<WebItemEntityCondition> GetConditions(string wql = "")
        {
            var wqlStatement = ComponentManager.GetComponent<IndexManager>()
                    .ExecuteWql<WebItemEntityCondition>(wql);

            return GetConditions(wqlStatement);
        }

        /// <summary>
        /// Returns all conditions.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>An enumeration that includes the conditions.</returns>
        public static IEnumerable<WebItemEntityCondition> GetConditions(IWqlStatement<WebItemEntityCondition> wql)
        {
            lock (DbContext)
            {
                var conditions = DbContext.Conditions;

                return conditions.Select(x => new WebItemEntityCondition(x));
            }
        }

        /// <summary>
        /// Returns a state.
        /// </summary>
        /// <param name="id">The id of the state.</param>
        /// <returns>The state or null.</returns>
        public static WebItemEntityCondition GetCondition(string id)
        {
            lock (DbContext)
            {
                var condition = DbContext.Conditions.Where(x => x.Guid == id).Select(x => new WebItemEntityCondition(x)).FirstOrDefault();

                return condition;
            }
        }

        /// <summary>
        /// Returns the state that is assigned to the inventory item.
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <returns>The state or null.</returns>
        public static WebItemEntityCondition GetCondition(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var condition = from i in DbContext.Inventories
                                join c in DbContext.Conditions on i.ConditionId equals c.Id
                                where i.Guid == inventory.Guid
                                select new WebItemEntityCondition(c);

                return condition.FirstOrDefault();
            }
        }

        /// <summary>
        /// Adds or updates a state.
        /// </summary>
        /// <param name="condition">The state.</param>
        public static void AddOrUpdateCondition(WebItemEntityCondition condition)
        {
            lock (DbContext)
            {
                var availableEntity = DbContext.Conditions.Where(x => x.Guid == condition.Guid).FirstOrDefault();

                if (availableEntity == null)
                {
                    // create new
                    var entity = new Condition()
                    {
                        Guid = condition.Guid,
                        Name = condition.Name,
                        Description = condition.Description,
                        Grade = condition.Grade,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Media = new Media()
                        {
                            Guid = condition.Media?.Guid,
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
                    // update
                    var availableMedia = condition.Media != null ? DbContext.Media.Where(x => x.Guid == condition.Media.Guid).FirstOrDefault() : null;

                    availableEntity.Name = condition.Name;
                    availableEntity.Description = condition.Description;
                    availableEntity.Grade = condition.Grade;
                    availableEntity.Updated = DateTime.Now;

                    if (availableMedia == null)
                    {
                        var media = new Media()
                        {
                            Guid = condition.Media?.Guid,
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
        /// Deletes a state.
        /// </summary>
        /// <param name="id">The id of the state.</param>
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
        /// Checks if the state is in use.
        /// </summary>
        /// <param name="condition">The state.</param>
        /// <returns>True when in use, false otherwise.</returns>
        public static bool GetConditionInUse(WebItemEntityCondition condition)
        {
            lock (DbContext)
            {
                var used = from i in DbContext.Inventories
                           join c in DbContext.Conditions on i.ConditionId equals c.Id
                           where c.Guid == condition.Guid
                           select c;

                return used.Any();
            }
        }
    }
}