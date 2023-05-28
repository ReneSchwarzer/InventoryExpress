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
        /// <param name="Guid">Die Id des Standortes</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetLocationUri(string Guid)
        {
            return $"{RootUri}/locations/{Guid}";
        }

        /// <summary>
        /// Liefert alle Standorte
        /// </summary>
        /// <param name="wql">Die Filter- und Sortieroptinen</param>
        /// <returns>Eine Aufzählung, welche die Standorte beinhaltet</returns>
        public static IEnumerable<WebItemEntityLocation> GetLocations(WqlStatement wql)
        {
            lock (DbContext)
            {
                var locations = DbContext.Locations.Select(x => new WebItemEntityLocation(x));

                return wql.Apply(locations).ToList();
            }
        }

        /// <summary>
        /// Liefert ein Standort
        /// </summary>
        /// <param name="id">Die Id des Standortes</param>
        /// <returns>Der Standort oder null</returns>
        public static WebItemEntityLocation GetLocation(string id)
        {
            lock (DbContext)
            {
                var location = DbContext.Locations.Where(x => x.Guid == id).Select(x => new WebItemEntityLocation(x)).FirstOrDefault();

                return location;
            }
        }

        /// <summary>
        /// Liefert ein Standort, welche dem Inventargegenstand zugeordnet ist
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <returns>Der Standort oder null</returns>
        public static WebItemEntityLocation GetLocation(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var location = from i in DbContext.Inventories
                               join l in DbContext.Locations on i.LocationId equals l.Id
                               where i.Guid == inventory.Id
                               select new WebItemEntityLocation(l);

                return location.FirstOrDefault();
            }
        }

        /// <summary>
        /// Fügt ein Standort hinzu oder aktuallisiert diesen
        /// </summary>
        /// <param name="condition">The location.</param>
        public static void AddOrUpdateLocation(WebItemEntityLocation condition)
        {
            lock (DbContext)
            {
                var availableEntity = DbContext.Locations.Where(x => x.Guid == condition.Id).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Location()
                    {
                        Guid = condition.Id,
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
                            Guid = condition.Media?.Id,
                            Name = condition.Media?.Name ?? "",
                            Description = condition.Media?.Description,
                            Tag = condition.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        }
                    };

                    DbContext.Locations.Add(entity);
                    DbContext.SaveChanges();
                }
                else
                {
                    // Update
                    var availableMedia = condition.Media != null ? DbContext.Media.Where(x => x.Guid == condition.Media.Id).FirstOrDefault() : null;

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
        /// Löscht ein Standort
        /// </summary>
        /// <param name="id">Die Id des Standortes</param>
        public static void DeleteLocation(string id)
        {
            lock (DbContext)
            {
                var entity = DbContext.Locations.Where(x => x.Guid == id).FirstOrDefault();
                var entityMedia = DbContext.Media.Where(x => x.Id == entity.MediaId).FirstOrDefault();

                if (entityMedia != null)
                {
                    DeleteMedia(entityMedia.Guid);
                }

                if (entity != null)
                {
                    DbContext.Locations.Remove(entity);
                    DbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Prüft ob der Standort in Verwendung ist
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>True wenn in Verwendung, false sonst</returns>
        public static bool GetLocationInUse(WebItemEntityLocation location)
        {
            lock (DbContext)
            {
                var used = from i in DbContext.Inventories
                           join l in DbContext.Locations on i.LocationId equals l.Id
                           where l.Guid == location.Id
                           select l;

                return used.Any();
            }
        }
    }
}