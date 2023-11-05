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
        /// Returns the supplier uri.
        /// </summary>
        /// <param name="guid">The id of the supplier.</param>
        /// <returns>The uri or null.</returns>
        public static string GetSupplierUri(string guid)
        {
            return ComponentManager.SitemapManager.GetUri<PageSupplierEdit>(new ParameterSupplierId(guid));
        }

        /// <summary>
        /// Returns all suppliers.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>An enumeration that includes the suppliers.</returns>
        public static IEnumerable<WebItemEntitySupplier> GetSuppliers(string wql = "")
        {
            var wqlStatement = ComponentManager.GetComponent<IndexManager>()
                    .ExecuteWql<WebItemEntitySupplier>(wql);

            return GetSuppliers(wqlStatement);
        }

        /// <summary>
        /// Returns all suppliers.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>An enumeration that includes the suppliers.</returns>
        public static IEnumerable<WebItemEntitySupplier> GetSuppliers(IWqlStatement<WebItemEntitySupplier> wql)
        {
            lock (DbContext)
            {
                var suppliers = DbContext.Suppliers.Select(x => new WebItemEntitySupplier(x));

                return wql.Apply(suppliers.AsQueryable());
            }
        }

        /// <summary>
        /// Returns a supplier.
        /// </summary>
        /// <param name="id">The id of the supplier.</param>
        /// <returns>The supplier or null.</returns>
        public static WebItemEntitySupplier GetSupplier(string id)
        {
            lock (DbContext)
            {
                var supplier = DbContext.Suppliers.Where(x => x.Guid == id).Select(x => new WebItemEntitySupplier(x)).FirstOrDefault();

                return supplier;
            }
        }

        /// <summary>
        /// Delivers to the vendor that is assigned to the inventory item.
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <returns>The supplier or null.</returns>
        public static WebItemEntitySupplier GetSupplier(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var supplier = from i in DbContext.Inventories
                               join s in DbContext.Suppliers on i.SupplierId equals s.Id
                               where i.Guid == inventory.Guid
                               select new WebItemEntitySupplier(s);

                return supplier.FirstOrDefault();
            }
        }

        /// <summary>
        /// Adds or updates a supplier.
        /// </summary>
        /// <param name="supplier">The supplier.</param>
        public static void AddOrUpdateSupplier(WebItemEntitySupplier supplier)
        {
            lock (DbContext)
            {
                var availableEntity = DbContext.Suppliers.Where(x => x.Guid == supplier.Guid).FirstOrDefault();

                if (availableEntity == null)
                {
                    // create new
                    var entity = new Supplier()
                    {
                        Guid = supplier.Guid,
                        Name = supplier.Name,
                        Description = supplier.Description,
                        Address = supplier.Address,
                        Zip = supplier.Zip,
                        Place = supplier.Place,
                        Tag = supplier.Tag,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Media = new Media()
                        {
                            Guid = supplier.Media?.Guid,
                            Name = supplier.Media?.Name ?? "",
                            Description = supplier.Media?.Description,
                            Tag = supplier.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        }
                    };

                    DbContext.Suppliers.Add(entity);
                    DbContext.SaveChanges();
                }
                else
                {
                    // update
                    var availableMedia = supplier.Media != null ? DbContext.Media.Where(x => x.Guid == supplier.Media.Guid).FirstOrDefault() : null;

                    availableEntity.Name = supplier.Name;
                    availableEntity.Description = supplier.Description;
                    availableEntity.Address = supplier.Address;
                    availableEntity.Zip = supplier.Zip;
                    availableEntity.Place = supplier.Place;
                    availableEntity.Tag = supplier.Tag;
                    availableEntity.Updated = DateTime.Now;

                    if (availableMedia == null)
                    {
                        var media = new Media()
                        {
                            Guid = supplier.Media?.Guid,
                            Name = supplier.Media?.Name,
                            Description = supplier.Media?.Description,
                            Tag = supplier.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        };

                        DbContext.Media.Add(media);
                    }
                    else if (!string.IsNullOrWhiteSpace(supplier.Media.Name))
                    {
                        availableMedia.Name = supplier.Media?.Name;
                        availableMedia.Description = supplier.Media?.Description;
                        availableMedia.Tag = supplier.Media?.Tag;
                        availableMedia.Updated = DateTime.Now;
                    }

                    DbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Deletes a supplier.
        /// </summary>
        /// <param name="id">The id of the supplier.</param>
        public static void DeleteSupplier(string id)
        {
            lock (DbContext)
            {
                var entity = DbContext.Suppliers.Where(x => x.Guid == id).FirstOrDefault();
                var entityMedia = DbContext.Media.Where(x => x.Id == entity.MediaId).FirstOrDefault();

                if (entityMedia != null)
                {
                    DeleteMedia(entityMedia.Guid);
                }

                if (entity != null)
                {
                    DbContext.Suppliers.Remove(entity);
                    DbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Checks if the supplier is in use.
        /// </summary>
        /// <param name="supplier">The supplier.</param>
        /// <returns>True when in use, false otherwise.</returns>
        public static bool GetSupplierInUse(WebItemEntitySupplier supplier)
        {
            lock (DbContext)
            {
                var used = from i in DbContext.Inventories
                           join s in DbContext.Suppliers on i.SupplierId equals s.Id
                           where s.Guid == supplier.Guid
                           select s;

                return used.Any();
            }
        }
    }
}