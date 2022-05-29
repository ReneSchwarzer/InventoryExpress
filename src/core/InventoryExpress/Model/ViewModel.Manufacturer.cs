﻿using InventoryExpress.Model.Entity;
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
        /// Ermittelt die Hersteller-URL
        /// </summary>
        /// <param name="guid">Die ID des Herstellers</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetManufacturerUri(string guid)
        {
            return $"{RootUri}/manufacturers/{guid}";
        }

        /// <summary>
        /// Liefert alle Hersteller
        /// </summary>
        /// <param name="wql">Die Filter- und Sortieroptinen</param>
        /// <returns>Eine Aufzählung, welche die Hersteller beinhaltet</returns>
        public static IEnumerable<WebItemEntityManufacturer> GetManufacturers(WqlStatement wql)
        {
            lock (DbContext)
            {
                var manufacturers = DbContext.Manufacturers.Select(x => new WebItemEntityManufacturer(x));

                return wql.Apply(manufacturers).ToList();
            }
        }

        /// <summary>
        /// Liefert ein Hersteller
        /// </summary>
        /// <param name="id">Die ID des Herstellers</param>
        /// <returns>Der Hersteller oder null</returns>
        public static WebItemEntityManufacturer GetManufacturer(string id)
        {
            lock (DbContext)
            {
                var manufacturer = DbContext.Manufacturers.Where(x => x.Guid == id).Select(x => new WebItemEntityManufacturer(x)).FirstOrDefault();

                return manufacturer;
            }
        }

        /// <summary>
        /// Liefert den Hersteller, welche dem Inventargegenstand zugeordnet ist
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand</param>
        /// <returns>Der Hersteller oder null</returns>
        public static WebItemEntityManufacturer GetManufacturer(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var manufacturer = from i in DbContext.Inventories
                                   join m in DbContext.Manufacturers on i.ManufacturerId equals m.Id
                                   where i.Guid == inventory.ID
                                   select new WebItemEntityManufacturer(m);

                return manufacturer.FirstOrDefault();
            }
        }

        /// <summary>
        /// Fügt ein Hersteller hinzu oder aktuallisiert diesen
        /// </summary>
        /// <param name="manufacturer">Der Hersteller</param>
        public static void AddOrUpdateManufacturer(WebItemEntityManufacturer manufacturer)
        {
            lock (DbContext)
            {
                var availableEntity = DbContext.Manufacturers.Where(x => x.Guid == manufacturer.ID).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Manufacturer()
                    {
                        Guid = manufacturer.ID,
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
                            Guid = manufacturer.Media?.ID,
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
                    // Update
                    var availableMedia = manufacturer.Media != null ? DbContext.Media.Where(x => x.Guid == manufacturer.Media.ID).FirstOrDefault() : null;

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
                            Guid = manufacturer.Media?.ID,
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
        /// Löscht ein Hersteller
        /// </summary>
        /// <param name="id">Die ID des Herstellers</param>
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
        /// Prüft ob der HErsteller in Verwendung ist
        /// </summary>
        /// <param name="manufacturer">Der Hersteller</param>
        /// <returns>True wenn in Verwendung, false sonst</returns>
        public static bool GetManufacturerInUse(WebItemEntityManufacturer manufacturer)
        {
            lock (DbContext)
            {
                var used = from i in DbContext.Inventories
                           join m in DbContext.Manufacturers on i.ManufacturerId equals m.Id
                           where m.Guid == manufacturer.ID
                           select m;

                return used.Any();
            }
        }
    }
}