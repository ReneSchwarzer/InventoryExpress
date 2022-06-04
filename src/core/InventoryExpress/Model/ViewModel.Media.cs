using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using System;
using System.IO;
using System.Linq;
using WebExpress.Message;

namespace InventoryExpress.Model
{
    public partial class ViewModel
    {
        /// <summary>
        /// Ermittelt die Media-URL
        /// </summary>
        /// <param name="guid">Die ID des Dokumentes</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetMediaUri(string guid)
        {
            return $"{RootUri}/media/{guid}";
        }

        /// <summary>
        /// Ermittelt die Media-URL
        /// </summary>
        /// <param name="id">Die ID des Dokumentes</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetMediaUri(int? id)
        {
            if (!id.HasValue) { return ApplicationIcon; }

            lock (DbContext)
            {
                var media = DbContext.Media.Where(x => x.Id == id).Select(x => GetMediaUri(x.Guid)).FirstOrDefault();

                return media ?? ApplicationIcon;
            }
        }

        /// <summary>
        /// Ermittelt die Media-URL
        /// </summary>
        /// <param name="id">Die ID des Dokumentes</param>
        /// <returns>Die Uri oder null</returns>
        public static WebItemEntityMedia GetMedia(int? id)
        {
            if (!id.HasValue) { return new WebItemEntityMedia() { Uri = ViewModel.ApplicationIcon }; }

            lock (DbContext)
            {
                var media = DbContext.Media.Where(x => x.Id == id).Select(x => new WebItemEntityMedia(x)).FirstOrDefault();

                return media ?? new WebItemEntityMedia();
            }
        }

        /// <summary>
        /// Ermittelt die Media-URL
        /// </summary>
        /// <param name="guid">Die ID des Dokumentes</param>
        /// <returns>Die Uri oder null</returns>
        public static WebItemEntityMedia GetMedia(string guid)
        {
            lock (DbContext)
            {
                var media = DbContext.Media.Where(x => x.Guid == guid).Select(x => new WebItemEntityMedia(x)).FirstOrDefault();

                return media ?? new WebItemEntityMedia();
            }
        }

        /// <summary>
        /// Fügt Medien hinzu oder aktuslisiert diese
        /// </summary>
        /// <param name="media">Die Medien</param>
        /// <param name="file">Die Datei oder null</param>
        public static void AddOrUpdateMedia(WebItemEntityMedia media, ParameterFile file)
        {
            var root = Path.Combine(Context.DataPath, "media");

            lock (DbContext)
            {
                var availableEntity = DbContext.Media.Where(x => x.Guid == media.Id).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Media()
                    {
                        Guid = media.Id,
                        Name = media.Name,
                        Description = media.Description,
                        Tag = media.Tag,
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    };

                    DbContext.Media.Add(entity);
                    DbContext.SaveChanges();
                }
                else
                {
                    // Update
                    availableEntity.Name = media.Name;
                    availableEntity.Description = media.Description;
                    availableEntity.Tag = media.Tag;
                    availableEntity.Updated = DateTime.Now;

                    if (File.Exists(Path.Combine(root, availableEntity.Guid)))
                    {
                        File.Delete(Path.Combine(root, availableEntity.Guid));
                    }

                    DbContext.SaveChanges();
                }

                File.WriteAllBytes(Path.Combine(root, media.Id), file.Data);
            }
        }

        /// <summary>
        /// Fügt Medien hinzu oder aktuslisiert diese
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand</param>
        /// <param name="file">Die Datei oder null</param>
        public static void AddOrUpdateMedia(WebItemEntityInventory inventory, ParameterFile file)
        {
            var root = Path.Combine(Context.DataPath, "media");

            lock (DbContext)
            {
                var inventoryEntity = DbContext.Inventories.Where(x => x.Guid == inventory.Id).FirstOrDefault();
                var availableEntity = DbContext.Media.Where(x => x.Id == inventoryEntity.MediaId).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Media()
                    {
                        Guid = inventory.Id,
                        Name = inventory.Name,
                        Description = inventory.Description,
                        Tag = inventory.Tag,
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    };

                    DbContext.Media.Add(entity);
                    DbContext.SaveChanges();

                    inventoryEntity.MediaId = entity.Id;
                    
                    DbContext.SaveChanges();
                    
                    var journal = new WebItemEntityJournal()
                    {
                        Action = "inventoryexpress:inventoryexpress.journal.action.inventory.media.add"
                    };

                    AddInventoryJournal(inventory, journal);
                }
                else
                {
                    // Update
                    availableEntity.Name = inventory.Media?.Name;
                    availableEntity.Description = inventory.Media?.Description;
                    availableEntity.Tag = inventory.Media?.Tag;
                    availableEntity.Updated = DateTime.Now;
                    
                    DbContext.SaveChanges();

                    if (File.Exists(Path.Combine(root, availableEntity.Guid)))
                    {
                        File.Delete(Path.Combine(root, availableEntity.Guid));
                    }

                    var journal = new WebItemEntityJournal()
                    {
                        Action = "inventoryexpress:inventoryexpress.journal.action.inventory.media.edit"
                    };

                    AddInventoryJournal(inventory, journal);
                }

                File.WriteAllBytes(Path.Combine(root, inventory.Id), file.Data);
            }
        }

        /// <summary>
        /// Löscht die Medien
        /// </summary>
        /// <param name="id">Die ID des Dokuementes</param>
        public static void DeleteMedia(string id)
        {
            var root = Path.Combine(Context.DataPath, "media");

            lock (DbContext)
            {
                var entity = DbContext.Media.Where(x => x.Guid == id).FirstOrDefault();

                if (entity != null)
                {
                    DbContext.Media.Remove(entity);
                    DbContext.SaveChanges();

                    if (File.Exists(Path.Combine(root, entity.Guid)))
                    {
                        File.Delete(Path.Combine(root, entity.Guid));
                    }
                }
            }
        }

        /// <summary>
        /// Prüft ob das Dokument in Verwendung ist
        /// </summary>
        /// <param name="media">Das Dokument</param>
        /// <returns>True wenn in Verwendung, false sonst</returns>
        public static bool GetMediaInUse(WebItemEntityMedia media)
        {
            lock (DbContext)
            {
                var usedInventories = from x in DbContext.Inventories
                                      join m in DbContext.Media on x.MediaId equals m.Id
                                      where m.Guid == media.Id
                                      select m;

                var usedSuppliers = from x in DbContext.Suppliers
                                    join m in DbContext.Media on x.MediaId equals m.Id
                                    where m.Guid == media.Id
                                    select m;

                var usedManufacturers = from x in DbContext.Manufacturers
                                        join m in DbContext.Media on x.MediaId equals m.Id
                                        where m.Guid == media.Id
                                        select m;

                var usedCostCenters = from x in DbContext.CostCenters
                                      join m in DbContext.Media on x.MediaId equals m.Id
                                      where m.Guid == media.Id
                                      select m;

                var usedLedgerAccounts = from x in DbContext.LedgerAccounts
                                         join m in DbContext.Media on x.MediaId equals m.Id
                                         where m.Guid == media.Id
                                         select m;

                var usedLocations = from x in DbContext.Locations
                                    join m in DbContext.Media on x.MediaId equals m.Id
                                    where m.Guid == media.Id
                                    select m;

                var usedTemplates = from x in DbContext.Templates
                                    join m in DbContext.Media on x.MediaId equals m.Id
                                    where m.Guid == media.Id
                                    select m;

                return usedInventories.Any() ||
                        usedSuppliers.Any() ||
                        usedManufacturers.Any() ||
                        usedCostCenters.Any() ||
                        usedLedgerAccounts.Any() ||
                        usedLocations.Any() ||
                        usedTemplates.Any();
            }
        }
    }
}