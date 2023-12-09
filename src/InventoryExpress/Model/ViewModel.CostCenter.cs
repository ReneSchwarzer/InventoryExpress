using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using InventoryExpress.Parameter;
using InventoryExpress.WebPage;
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
        /// Determines the cost center uri.
        /// </summary>
        /// <param name="guid">The id of the cost center.</param>
        /// <returns>The uri or null.</returns>
        public static string GetCostCenterUri(string guid)
        {
            return ComponentManager.SitemapManager.GetUri<PageCostCenterEdit>(new ParameterCostCenterId(guid));
        }

        /// <summary>
        /// Returns all cost centers.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>An enumeration that includes the cost centers.</returns>
        public static IEnumerable<WebItemEntityCostCenter> GetCostCenters(string wql = "")
        {
            var wqlStatement = ComponentManager.GetComponent<IndexManager>()
                    .ExecuteWql<WebItemEntityCostCenter>(wql);

            return GetCostCenters(wqlStatement);
        }

        /// <summary>
        /// Returns all cost centers.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>An enumeration that includes the cost centers.</returns>
        public static IEnumerable<WebItemEntityCostCenter> GetCostCenters(IWqlStatement<WebItemEntityCostCenter> wql)
        {
            lock (DbContext)
            {
                var costCenters = DbContext.CostCenters.Select(x => new WebItemEntityCostCenter(x));

                return wql.Apply(costCenters.AsQueryable());
            }
        }

        /// <summary>
        /// Returns a cost center.
        /// </summary>
        /// <param name="id">The id of the cost center.</param>
        /// <returns>The cost center or null.</returns>
        public static WebItemEntityCostCenter GetCostCenter(string id)
        {
            lock (DbContext)
            {
                var location = DbContext.CostCenters.Where(x => x.Guid == id).Select(x => new WebItemEntityCostCenter(x)).FirstOrDefault();

                return location;
            }
        }

        /// <summary>
        /// Returns the cost center that is assigned to the inventory item.
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <returns>The cost center or null.</returns>
        public static WebItemEntityCostCenter GetCostCenter(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var costCenter = from i in DbContext.Inventories
                                 join c in DbContext.CostCenters on i.CostCenterId equals c.Id
                                 where i.Guid == inventory.Guid
                                 select new WebItemEntityCostCenter(c);

                return costCenter.FirstOrDefault();
            }
        }

        /// <summary>
        /// Adds or updates a cost center.
        /// </summary>
        /// <param name="costCenter">The cost center.</param>
        public static void AddOrUpdateCostCenter(WebItemEntityCostCenter costCenter)
        {
            lock (DbContext)
            {
                var availableEntity = DbContext.CostCenters.Where(x => x.Guid == costCenter.Guid).FirstOrDefault();

                if (availableEntity == null)
                {
                    // create new
                    var entity = new CostCenter()
                    {
                        Guid = costCenter.Guid,
                        Name = costCenter.Name,
                        Description = costCenter.Description,
                        Tag = costCenter.Tag,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Media = new Media()
                        {
                            Guid = costCenter.Media?.Guid,
                            Name = costCenter.Media?.Name ?? "",
                            Description = costCenter.Media?.Description,
                            Tag = costCenter.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        }
                    };

                    DbContext.CostCenters.Add(entity);
                    DbContext.SaveChanges();
                }
                else
                {
                    // update
                    var availableMedia = costCenter.Media != null ? DbContext.Media.Where(x => x.Guid == costCenter.Media.Guid).FirstOrDefault() : null;

                    availableEntity.Name = costCenter.Name;
                    availableEntity.Description = costCenter.Description;
                    availableEntity.Tag = costCenter.Tag;
                    availableEntity.Updated = DateTime.Now;

                    if (availableMedia == null)
                    {
                        var media = new Media()
                        {
                            Guid = costCenter.Media?.Guid,
                            Name = costCenter.Media?.Name,
                            Description = costCenter.Media?.Description,
                            Tag = costCenter.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        };

                        DbContext.Media.Add(media);
                    }
                    else if (!string.IsNullOrWhiteSpace(costCenter.Media.Name))
                    {
                        availableMedia.Name = costCenter.Media?.Name;
                        availableMedia.Description = costCenter.Media?.Description;
                        availableMedia.Tag = costCenter.Media?.Tag;
                        availableMedia.Updated = DateTime.Now;
                    }

                    DbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Deletes a cost center.
        /// </summary>
        /// <param name="id">The id of the cost center.</param>
        public static void DeleteCostCenter(string id)
        {
            lock (DbContext)
            {
                var entity = DbContext.CostCenters.Where(x => x.Guid == id).FirstOrDefault();
                var entityMedia = DbContext.Media.Where(x => x.Id == entity.MediaId).FirstOrDefault();

                if (entityMedia != null)
                {
                    DeleteMedia(entityMedia.Guid);
                }

                if (entity != null)
                {
                    DbContext.CostCenters.Remove(entity);
                    DbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Checks whether the cost center is in use.
        /// </summary>
        /// <param name="costCenter">The cost center.</param>
        /// <returns>True when in use, false otherwise.</returns>
        public static bool GetCostCenterInUse(WebItemEntityCostCenter costCenter)
        {
            lock (DbContext)
            {
                var used = from i in DbContext.Inventories
                           join c in DbContext.CostCenters on i.CostCenterId equals c.Id
                           where c.Guid == costCenter.Guid
                           select c;

                return used.Any();
            }
        }
    }
}