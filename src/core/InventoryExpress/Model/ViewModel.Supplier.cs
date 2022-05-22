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
        /// <param name="guid">Die LieferantenID</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetSupplierUri(string guid)
        {
            return $"{ RootUri }/suppliers/{ guid }";
        }

        /// <summary>
        /// Liefert alle Lieferanten
        /// </summary>
        /// <param name="wql">Die Filter- und Sortieroptinen</param>
        /// <returns>Eine Aufzählung, welche die Hersteller beinhaltet</returns>
        public static IEnumerable<WebItemEntitySupplier> GetSuppliers(WqlStatement wql)
        {
            lock (Instance.Database)
            {
                var suppliers = Instance.Suppliers.Select(x => new WebItemEntitySupplier(x));

                return wql.Apply(suppliers.AsQueryable());
            }
        }

        /// <summary>
        /// Liefert ein Lieferant
        /// </summary>
        /// <param name="id">Die LieferantenID</param>
        /// <returns>Der Lieferant oder null</returns>
        public static WebItemEntitySupplier GetSupplier(string id)
        {
            lock (Instance.Database)
            {
                var supplier = Instance.Suppliers.Where(x => x.Guid == id).Select(x => new WebItemEntitySupplier(x)).FirstOrDefault();

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
            lock (Instance.Database)
            {
                var supplier = from i in Instance.Inventories
                               join s in Instance.Suppliers on i.ConditionId equals s.Id
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
            lock (Instance.Database)
            {
                var availableEntity = Instance.Manufacturers.Where(x => x.Guid == supplier.ID).FirstOrDefault();

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

                    Instance.Suppliers.Add(entity);
                }
                else
                {
                    // Update
                    var availableMedia = supplier.Media != null ? Instance.Media.Where(x => x.Guid == supplier.Media.ID).FirstOrDefault() : null;

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

                        Instance.Media.Add(media);
                    }
                    else if (!string.IsNullOrWhiteSpace(supplier.Media.Name))
                    {
                        availableMedia.Name = supplier.Media?.Name;
                        availableMedia.Description = supplier.Media?.Description;
                        availableMedia.Tag = supplier.Media?.Tag;
                        availableMedia.Updated = DateTime.Now;
                    }
                }
            }
        }

        /// <summary>
        /// Löscht ein Lieferant
        /// </summary>
        /// <param name="id">Die Lieferanten ID</param>
        public static void DeleteSupplier(string id)
        {
            lock (Instance.Database)
            {
                var entity = Instance.Suppliers.Where(x => x.Guid == id).FirstOrDefault();
                var entityMedia = Instance.Media.Where(x => x.Id == entity.MediaId).FirstOrDefault();

                if (entityMedia != null)
                {
                    DeleteMedia(entityMedia.Guid);
                }

                if (entity != null)
                {
                    Instance.Suppliers.Remove(entity);
                }
            }
        }
    }
}