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
        /// Ermittelt die Kostenstelle-URL
        /// </summary>
        /// <param name="guid">Die ID der Kostenstelle</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetCostCenterUri(string guid)
        {
            return $"{RootUri}/costcenters/{guid}";
        }

        /// <summary>
        /// Liefert alle Kostenstellen
        /// </summary>
        /// <param name="wql">Die Filter- und Sortieroptinen</param>
        /// <returns>Eine Aufzählung, welche die Kostenstelle beinhaltet</returns>
        public static IEnumerable<WebItemEntityCostCenter> GetCostCenters(WqlStatement wql)
        {
            lock (Instance.Database)
            {
                var costCenters = Instance.CostCenters.Select(x => new WebItemEntityCostCenter(x));

                return wql.Apply(costCenters.AsQueryable());
            }
        }

        /// <summary>
        /// Liefert ein Kostenstelle
        /// </summary>
        /// <param name="id">Die ID der Kostenstelle</param>
        /// <returns>Die Kostenstelle oder null</returns>
        public static WebItemEntityCostCenter GetCostCenter(string id)
        {
            lock (Instance.Database)
            {
                var location = Instance.CostCenters.Where(x => x.Guid == id).Select(x => new WebItemEntityCostCenter(x)).FirstOrDefault();

                return location;
            }
        }

        /// <summary>
        /// Liefert die Kostenstelle, welche dem Inventargegenstand zugeordnet ist
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand</param>
        /// <returns>Die Kostenstelle oder null</returns>
        public static WebItemEntityCostCenter GetCostCenter(WebItemEntityInventory inventory)
        {
            lock (Instance.Database)
            {
                var costCenter = from i in Instance.Inventories
                                 join c in Instance.CostCenters on i.ConditionId equals c.Id
                                 where i.Guid == inventory.ID
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
            lock (Instance.Database)
            {
                var availableEntity = Instance.CostCenters.Where(x => x.Guid == costCenter.ID).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new CostCenter()
                    {
                        Guid = costCenter.ID,
                        Name = costCenter.Name,
                        Description = costCenter.Description,
                        Tag = costCenter.Tag,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Media = new Media()
                        {
                            Guid = costCenter.Media?.ID,
                            Name = costCenter.Media?.Name ?? "",
                            Description = costCenter.Media?.Description,
                            Tag = costCenter.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        }
                    };

                    Instance.CostCenters.Add(entity);
                }
                else
                {
                    // Update
                    var availableMedia = costCenter.Media != null ? Instance.Media.Where(x => x.Guid == costCenter.Media.ID).FirstOrDefault() : null;

                    availableEntity.Name = costCenter.Name;
                    availableEntity.Description = costCenter.Description;
                    availableEntity.Tag = costCenter.Tag;
                    availableEntity.Updated = DateTime.Now;

                    if (availableMedia == null)
                    {
                        var media = new Media()
                        {
                            Guid = costCenter.Media?.ID,
                            Name = costCenter.Media?.Name,
                            Description = costCenter.Media?.Description,
                            Tag = costCenter.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        };

                        Instance.Media.Add(media);
                    }
                    else if (!string.IsNullOrWhiteSpace(costCenter.Media.Name))
                    {
                        availableMedia.Name = costCenter.Media?.Name;
                        availableMedia.Description = costCenter.Media?.Description;
                        availableMedia.Tag = costCenter.Media?.Tag;
                        availableMedia.Updated = DateTime.Now;
                    }
                }
            }
        }

        /// <summary>
        /// Löscht eine Kostenstelle
        /// </summary>
        /// <param name="id">Die ID der Kostenstelle</param>
        public static void DeleteCostCenter(string id)
        {
            lock (Instance.Database)
            {
                var entity = Instance.CostCenters.Where(x => x.Guid == id).FirstOrDefault();
                var entityMedia = Instance.Media.Where(x => x.Id == entity.MediaId).FirstOrDefault();

                if (entityMedia != null)
                {
                    DeleteMedia(entityMedia.Guid);
                }

                if (entity != null)
                {
                    Instance.CostCenters.Remove(entity);
                }
            }
        }
    }
}