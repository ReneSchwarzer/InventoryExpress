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
        /// Ermittelt die Lieferanten-URL
        /// </summary>
        /// <param name="guid">Die ID des Lieferanten</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetSupplierUri(string guid)
        {
            return $"{RootUri}/suppliers/{guid}";
        }

        /// <summary>
        /// Liefert alle Lieferanten
        /// </summary>
        /// <param name="wql">Die Filter- und Sortieroptinen</param>
        /// <returns>Eine Aufzählung, welche die Lieferanten beinhaltet</returns>
        public static IEnumerable<WebItemEntitySupplier> GetSuppliers(WqlStatement wql)
        {
            lock (DbContext)
            {
                var suppliers = DbContext.Suppliers.Select(x => new WebItemEntitySupplier(x));

                return wql.Apply(suppliers.AsQueryable()).ToList();
            }
        }

        /// <summary>
        /// Liefert ein Lieferant
        /// </summary>
        /// <param name="id">Die ID des Lieferanten</param>
        /// <returns>Der Lieferant oder null</returns>
        public static WebItemEntitySupplier GetSupplier(string id)
        {
            lock (DbContext)
            {
                var supplier = DbContext.Suppliers.Where(x => x.Guid == id).Select(x => new WebItemEntitySupplier(x)).FirstOrDefault();

                return supplier;
            }
        }

        /// <summary>
        /// Liefert den Lieferanten, welche dem Inventargegenstand zugeordnet ist
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand</param>
        /// <returns>Der Lieferant oder null</returns>
        public static WebItemEntitySupplier GetSupplier(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var supplier = from i in DbContext.Inventories
                               join s in DbContext.Suppliers on i.SupplierId equals s.Id
                               where i.Guid == inventory.ID
                               select new WebItemEntitySupplier(s);

                return supplier.FirstOrDefault();
            }
        }

        /// <summary>
        /// Fügt ein Lieferant hinzu oder aktuallisiert diesen
        /// </summary>
        /// <param name="supplier">Der Lieferant</param>
        public static void AddOrUpdateSupplier(WebItemEntitySupplier supplier)
        {
            lock (DbContext)
            {
                var availableEntity = DbContext.Manufacturers.Where(x => x.Guid == supplier.ID).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Supplier()
                    {
                        Guid = supplier.ID,
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
                            Guid = supplier.Media?.ID,
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
                    // Update
                    var availableMedia = supplier.Media != null ? DbContext.Media.Where(x => x.Guid == supplier.Media.ID).FirstOrDefault() : null;

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
                            Guid = supplier.Media?.ID,
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
        /// Löscht ein Lieferant
        /// </summary>
        /// <param name="id">Die ID des Lieferanten</param>
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
        /// Prüft ob der Lieferant in Verwendung ist
        /// </summary>
        /// <param name="supplier">Der Lieferant</param>
        /// <returns>True wenn in Verwendung, false sonst</returns>
        public static bool GetSupplierInUse(WebItemEntitySupplier supplier)
        {
            lock (DbContext)
            {
                var used = from i in DbContext.Inventories
                           join s in DbContext.Suppliers on i.SupplierId equals s.Id
                           where s.Guid == supplier.ID
                           select s;

                return used.Any();
            }
        }
    }
}