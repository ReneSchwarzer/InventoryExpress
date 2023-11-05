using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using InventoryExpress.Parameter;
using InventoryExpress.WebPage;
using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApp.WebIndex;
using WebExpress.WebComponent;
using WebExpress.WebIndex.Wql;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Returns the manufacturer's uri.
        /// </summary>
        /// <param name="guid">The id of the manufacturer.</param>
        /// <returns>The uri or null.</returns>
        public static string GetManufacturerUri(string guid)
        {
            return ComponentManager.SitemapManager.GetUri<PageManufacturerEdit>(new ParameterManufacturerId(guid));
        }

        /// <summary>
        /// Returns all manufacturers.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>An enumeration that includes the manufacturers.</returns>
        public static IEnumerable<WebItemEntityManufacturer> GetManufacturers(string wql = "")
        {
            var wqlStatement = ComponentManager.GetComponent<IndexManager>()
                    .ExecuteWql<WebItemEntityManufacturer>(wql);

            return GetManufacturers(wqlStatement);
        }

        /// <summary>
        /// Returns all manufacturers.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>An enumeration that includes the manufacturers.</returns>
        public static IEnumerable<WebItemEntityManufacturer> GetManufacturers(IWqlStatement<WebItemEntityManufacturer> wql)
        {
            lock (DbContext)
            {
                var manufacturers = DbContext.Manufacturers.Select(x => new WebItemEntityManufacturer(x));

                return wql.Apply(manufacturers.AsQueryable());
            }
        }

        /// <summary>
        /// Returns a manufacturer.
        /// </summary>
        /// <param name="id">Returns or sets the id. des Herstellers</param>
        /// <returns>The manufacturer or null.</returns>
        public static WebItemEntityManufacturer GetManufacturer(string id)
        {
            lock (DbContext)
            {
                var manufacturer = DbContext.Manufacturers.Where(x => x.Guid == id).Select(x => new WebItemEntityManufacturer(x)).FirstOrDefault();

                return manufacturer;
            }
        }

        /// <summary>
        /// Returns the manufacturer that is assigned to the inventory item.
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <returns>The manufacturer or null.</returns>
        public static WebItemEntityManufacturer GetManufacturer(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var manufacturer = from i in DbContext.Inventories
                                   join m in DbContext.Manufacturers on i.ManufacturerId equals m.Id
                                   where i.Guid == inventory.Guid
                                   select new WebItemEntityManufacturer(m);

                return manufacturer.FirstOrDefault();
            }
        }

        /// <summary>
        /// A manufacturer adds or updates it.
        /// </summary>
        /// <param name="manufacturer">The manufacturer.</param>
        public static void AddOrUpdateManufacturer(WebItemEntityManufacturer manufacturer)
        {
            lock (DbContext)
            {
                var availableEntity = DbContext.Manufacturers.Where(x => x.Guid == manufacturer.Guid).FirstOrDefault();

                if (availableEntity == null)
                {
                    // create new
                    var entity = new Manufacturer()
                    {
                        Guid = manufacturer.Guid,
                        Name = manufacturer.Name,
                        Description = manufacturer.Description,
                        Address = manufacturer.Address,
                        Zip = manufacturer.Zip,
                        Place = manufacturer.Place,
                        Tag = manufacturer.Tag,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Media = new Media()
                        {
                            Guid = manufacturer.Media?.Guid,
                            Name = manufacturer.Media?.Name ?? "",
                            Description = manufacturer.Media?.Description,
                            Tag = manufacturer.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        }
                    };

                    DbContext.Manufacturers.Add(entity);
                    DbContext.SaveChanges();
                }
                else
                {
                    // update
                    var availableMedia = manufacturer.Media != null ? DbContext.Media.Where(x => x.Guid == manufacturer.Media.Guid).FirstOrDefault() : null;

                    availableEntity.Name = manufacturer.Name;
                    availableEntity.Description = manufacturer.Description;
                    availableEntity.Address = manufacturer.Address;
                    availableEntity.Zip = manufacturer.Zip;
                    availableEntity.Place = manufacturer.Place;
                    availableEntity.Tag = manufacturer.Tag;
                    availableEntity.Updated = DateTime.Now;

                    if (availableMedia == null)
                    {
                        var media = new Media()
                        {
                            Guid = manufacturer.Media?.Guid,
                            Name = manufacturer.Media?.Name,
                            Description = manufacturer.Media?.Description,
                            Tag = manufacturer.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        };

                        DbContext.Media.Add(media);
                    }
                    else if (!string.IsNullOrWhiteSpace(manufacturer.Media.Name))
                    {
                        availableMedia.Name = manufacturer.Media?.Name;
                        availableMedia.Description = manufacturer.Media?.Description;
                        availableMedia.Tag = manufacturer.Media?.Tag;
                        availableMedia.Updated = DateTime.Now;
                    }

                    DbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Deletes a manufacturer.
        /// </summary>
        /// <param name="id">The id of the manufacturer.</param>
        public static void DeleteManufacturer(string id)
        {
            lock (DbContext)
            {
                var entity = DbContext.Manufacturers.Where(x => x.Guid == id).FirstOrDefault();
                var entityMedia = DbContext.Media.Where(x => x.Id == entity.MediaId).FirstOrDefault();

                if (entityMedia != null)
                {
                    DeleteMedia(entityMedia.Guid);
                }

                if (entity != null)
                {
                    DbContext.Manufacturers.Remove(entity);
                    DbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Checks if the manufacturer is in use.
        /// </summary>
        /// <param name="manufacturer">The manufacturer.</param>
        /// <returns>True when in use, false otherwise.</returns>
        public static bool GetManufacturerInUse(WebItemEntityManufacturer manufacturer)
        {
            lock (DbContext)
            {
                var used = from i in DbContext.Inventories
                           join m in DbContext.Manufacturers on i.ManufacturerId equals m.Id
                           where m.Guid == manufacturer.Guid
                           select m;

                return used.Any();
            }
        }
    }
}