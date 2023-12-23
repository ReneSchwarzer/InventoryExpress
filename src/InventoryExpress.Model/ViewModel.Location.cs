using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
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
        ///// <summary>
        ///// Returns the location URL.
        ///// </summary>
        ///// <param name="guid">The guidid of the location.</param>
        ///// <returns>The uri or null.</returns>
        //public static string GetLocationUri(string guid)
        //{
        //    return ComponentManager.SitemapManager.GetUri<PageLocationEdit>(new ParameterLocationId(guid));
        //}

        /// <summary>
        /// Returns all locations.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>An enumeration that includes the locations.</returns>
        public static IEnumerable<WebItemEntityLocation> GetLocations(string wql = "")
        {
            var wqlStatement = ComponentManager.GetComponent<IndexManager>()
                    .ExecuteWql<WebItemEntityLocation>(wql);

            return GetLocations(wqlStatement);
        }

        /// <summary>
        /// Returns all locations.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>An enumeration that includes the conditions.</returns>
        public static IEnumerable<WebItemEntityLocation> GetLocations(IWqlStatement<WebItemEntityLocation> wql)
        {
            lock (DbContext)
            {
                var locations = DbContext.Locations.Select(x => new WebItemEntityLocation(x));

                return wql.Apply(locations.AsQueryable());
            }
        }

        /// <summary>
        /// Returns a location.
        /// </summary>
        /// <param name="id">The id of the location.</param>
        /// <returns>The location or null.</returns>
        public static WebItemEntityLocation GetLocation(string id)
        {
            lock (DbContext)
            {
                var location = DbContext.Locations.Where(x => x.Guid == id).Select(x => new WebItemEntityLocation(x)).FirstOrDefault();

                return location;
            }
        }

        /// <summary>
        /// Returns a location that is assigned to the inventory item.
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <returns>The location or null.</returns>
        public static WebItemEntityLocation GetLocation(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var location = from i in DbContext.Inventories
                               join l in DbContext.Locations on i.LocationId equals l.Id
                               where i.Guid == inventory.Guid
                               select new WebItemEntityLocation(l);

                return location.FirstOrDefault();
            }
        }

        /// <summary>
        /// Adds or updates a location.
        /// </summary>
        /// <param name="location">The location.</param>
        public static void AddOrUpdateLocation(WebItemEntityLocation location)
        {
            lock (DbContext)
            {
                var availableEntity = DbContext.Locations.Where(x => x.Guid == location.Guid).FirstOrDefault();

                if (availableEntity == null)
                {
                    // create new
                    var entity = new Location()
                    {
                        Id = location.Id,
                        Guid = location.Guid,
                        Name = location.Name,
                        Description = location.Description,
                        Address = location.Address,
                        Zip = location.Zip,
                        Place = location.Place,
                        Tag = location.Tag,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Media = new Media()
                        {
                            Id = location.Media?.Id ?? -1,
                            Guid = location.Media?.Guid,
                            Name = location.Media?.Name ?? "",
                            Description = location.Media?.Description,
                            Tag = location.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        }
                    };

                    DbContext.Locations.Add(entity);
                    DbContext.SaveChanges();
                }
                else
                {
                    // update
                    var availableMedia = location.Media != null ? DbContext.Media.Where(x => x.Guid == location.Media.Guid).FirstOrDefault() : null;

                    availableEntity.Name = location.Name;
                    availableEntity.Description = location.Description;
                    availableEntity.Address = location.Address;
                    availableEntity.Zip = location.Zip;
                    availableEntity.Place = location.Place;
                    availableEntity.Tag = location.Tag;
                    availableEntity.Updated = DateTime.Now;

                    if (availableMedia == null)
                    {
                        var media = new Media()
                        {
                            Guid = location.Media?.Guid,
                            Name = location.Media?.Name,
                            Description = location.Media?.Description,
                            Tag = location.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        };

                        DbContext.Media.Add(media);
                    }
                    else if (!string.IsNullOrWhiteSpace(location.Media.Name))
                    {
                        availableMedia.Name = location.Media?.Name;
                        availableMedia.Description = location.Media?.Description;
                        availableMedia.Tag = location.Media?.Tag;
                        availableMedia.Updated = DateTime.Now;
                    }

                    DbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Deletes a location.
        /// </summary>
        /// <param name="id">The id of the location.</param>
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
        /// Checks if the site is in use.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>True when in use, false otherwise.</returns>
        public static bool GetLocationInUse(WebItemEntityLocation location)
        {
            lock (DbContext)
            {
                var used = from i in DbContext.Inventories
                           join l in DbContext.Locations on i.LocationId equals l.Id
                           where l.Guid == location.Guid
                           select l;

                return used.Any();
            }
        }
    }
}