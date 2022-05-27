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
        /// Ermittelt die Standort-URL
        /// </summary>
        /// <param name="guid">Die ID des Standortes</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetLocationUri(string guid)
        {
            return $"{RootUri}/locations/{guid}";
        }

        /// <summary>
        /// Liefert alle Standorte
        /// </summary>
        /// <param name="wql">Die Filter- und Sortieroptinen</param>
        /// <returns>Eine Aufzählung, welche die Standorte beinhaltet</returns>
        public static IEnumerable<WebItemEntityLocation> GetLocations(WqlStatement wql)
        {
            lock (Instance.Database)
            {
                var locations = Instance.Locations.Select(x => new WebItemEntityLocation(x));

                return wql.Apply(locations);
            }
        }

        /// <summary>
        /// Liefert ein Standort
        /// </summary>
        /// <param name="id">Die ID des Standortes</param>
        /// <returns>Der Standort oder null</returns>
        public static WebItemEntityLocation GetLocation(string id)
        {
            lock (Instance.Database)
            {
                var location = Instance.Locations.Where(x => x.Guid == id).Select(x => new WebItemEntityLocation(x)).FirstOrDefault();

                return location;
            }
        }

        /// <summary>
        /// Liefert ein Standort, welche dem Inventargegenstand zugeordnet ist
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand</param>
        /// <returns>Der Standort oder null</returns>
        public static WebItemEntityLocation GetLocation(WebItemEntityInventory inventory)
        {
            lock (Instance.Database)
            {
                var location = from i in Instance.Inventories
                               join l in Instance.Locations on i.ConditionId equals l.Id
                               where i.Guid == inventory.ID
                               select new WebItemEntityLocation(l);

                return location.FirstOrDefault();
            }
        }

        /// <summary>
        /// Fügt ein Standort hinzu oder aktuallisiert diesen
        /// </summary>
        /// <param name="condition">Der Standort</param>
        public static void AddOrUpdateLocation(WebItemEntityLocation condition)
        {
            lock (Instance.Database)
            {
                var availableEntity = Instance.Locations.Where(x => x.Guid == condition.ID).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Location()
                    {
                        Guid = condition.ID,
                        Name = condition.Name,
                        Description = condition.Description,
                        Address = condition.Address,
                        Zip = condition.Zip,
                        Place = condition.Place,
                        Tag = condition.Tag,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Media = new Media()
                        {
                            Guid = condition.Media?.ID,
                            Name = condition.Media?.Name ?? "",
                            Description = condition.Media?.Description,
                            Tag = condition.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        }
                    };

                    Instance.Locations.Add(entity);
                }
                else
                {
                    // Update
                    var availableMedia = condition.Media != null ? Instance.Media.Where(x => x.Guid == condition.Media.ID).FirstOrDefault() : null;

                    availableEntity.Name = condition.Name;
                    availableEntity.Description = condition.Description;
                    availableEntity.Address = condition.Address;
                    availableEntity.Zip = condition.Zip;
                    availableEntity.Place = condition.Place;
                    availableEntity.Tag = condition.Tag;
                    availableEntity.Updated = DateTime.Now;

                    if (availableMedia == null)
                    {
                        var media = new Media()
                        {
                            Guid = condition.Media?.ID,
                            Name = condition.Media?.Name,
                            Description = condition.Media?.Description,
                            Tag = condition.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        };

                        Instance.Media.Add(media);
                    }
                    else if (!string.IsNullOrWhiteSpace(condition.Media.Name))
                    {
                        availableMedia.Name = condition.Media?.Name;
                        availableMedia.Description = condition.Media?.Description;
                        availableMedia.Tag = condition.Media?.Tag;
                        availableMedia.Updated = DateTime.Now;
                    }
                }
            }
        }

        /// <summary>
        /// Löscht ein Standort
        /// </summary>
        /// <param name="id">Die ID des Standortes</param>
        public static void DeleteLocation(string id)
        {
            lock (Instance.Database)
            {
                var entity = Instance.Locations.Where(x => x.Guid == id).FirstOrDefault();
                var entityMedia = Instance.Media.Where(x => x.Id == entity.MediaId).FirstOrDefault();

                if (entityMedia != null)
                {
                    DeleteMedia(entityMedia.Guid);
                }

                if (entity != null)
                {
                    Instance.Locations.Remove(entity);
                }
            }
        }
    }
}