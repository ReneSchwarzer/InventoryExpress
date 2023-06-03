using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using InventoryExpress.Parameter;
using InventoryExpress.WebPage;
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
        /// Ermittelt die Kostenstelle-URL
        /// </summary>
        /// <param name="guid">Returns or sets the id. der Kostenstelle</param>
        /// <returns>The uri or null.</returns>
        public static string GetCostCenterUri(string guid)
        {
            return ComponentManager.SitemapManager.GetUri<PageCostCenterEdit>(new ParameterCostCenterId(guid));
        }

        /// <summary>
        /// Liefert alle Kostenstellen
        /// </summary>
        /// <param name="wql">Die Filter- und Sortieroptinen</param>
        /// <returns>Eine Aufzählung, welche die Kostenstelle beinhaltet</returns>
        public static IEnumerable<WebItemEntityCostCenter> GetCostCenters(WqlStatement wql)
        {
            lock (DbContext)
            {
                var costCenters = DbContext.CostCenters.Select(x => new WebItemEntityCostCenter(x));

                return wql.Apply(costCenters.AsQueryable()).ToList();
            }
        }

        /// <summary>
        /// Liefert ein Kostenstelle
        /// </summary>
        /// <param name="id">Returns or sets the id. der Kostenstelle</param>
        /// <returns>Die Kostenstelle oder null</returns>
        public static WebItemEntityCostCenter GetCostCenter(string id)
        {
            lock (DbContext)
            {
                var location = DbContext.CostCenters.Where(x => x.Guid == id).Select(x => new WebItemEntityCostCenter(x)).FirstOrDefault();

                return location;
            }
        }

        /// <summary>
        /// Liefert die Kostenstelle, welche dem Inventargegenstand zugeordnet ist
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <returns>Die Kostenstelle oder null</returns>
        public static WebItemEntityCostCenter GetCostCenter(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var costCenter = from i in DbContext.Inventories
                                 join c in DbContext.CostCenters on i.CostCenterId equals c.Id
                                 where i.Guid == inventory.Id
                                 select new WebItemEntityCostCenter(c);

                return costCenter.FirstOrDefault();
            }
        }

        /// <summary>
        /// Fügt eine Kostenstelle hinzu oder aktuallisiert diesen
        /// </summary>
        /// <param name="costCenter">Die Kostenstelle</param>
        public static void AddOrUpdateCostCenter(WebItemEntityCostCenter costCenter)
        {
            lock (DbContext)
            {
                var availableEntity = DbContext.CostCenters.Where(x => x.Guid == costCenter.Id).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new CostCenter()
                    {
                        Guid = costCenter.Id,
                        Name = costCenter.Name,
                        Description = costCenter.Description,
                        Tag = costCenter.Tag,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Media = new Media()
                        {
                            Guid = costCenter.Media?.Id,
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
                    // Update
                    var availableMedia = costCenter.Media != null ? DbContext.Media.Where(x => x.Guid == costCenter.Media.Id).FirstOrDefault() : null;

                    availableEntity.Name = costCenter.Name;
                    availableEntity.Description = costCenter.Description;
                    availableEntity.Tag = costCenter.Tag;
                    availableEntity.Updated = DateTime.Now;

                    if (availableMedia == null)
                    {
                        var media = new Media()
                        {
                            Guid = costCenter.Media?.Id,
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
        /// Löscht eine Kostenstelle
        /// </summary>
        /// <param name="id">Returns or sets the id. der Kostenstelle</param>
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
        /// Prüft ob die Kostenstelle in Verwendung ist
        /// </summary>
        /// <param name="costCenter">Die Kostenstelle</param>
        /// <returns>True wenn in Verwendung, false sonst</returns>
        public static bool GetCostCenterInUse(WebItemEntityCostCenter costCenter)
        {
            lock (DbContext)
            {
                var used = from i in DbContext.Inventories
                           join c in DbContext.CostCenters on i.CostCenterId equals c.Id
                           where c.Guid == costCenter.Id
                           select c;

                return used.Any();
            }
        }
    }
}