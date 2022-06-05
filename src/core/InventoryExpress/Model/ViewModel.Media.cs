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
        /// <param name="costCenter">Die Kostenstelle</param>
        /// <param name="file">Die Datei oder null</param>
        public static void AddOrUpdateMedia(WebItemEntityCostCenter costCenter, ParameterFile file)
        {
            var root = Path.Combine(Context.DataPath, "media");
            var guid = Guid.NewGuid().ToString();
            var filename = file?.Value;
            var journalParameter = new WebItemEntityJournalParameter()
            {
                Name = "inventoryexpress:inventoryexpress.costcenter.media.label",
                OldValue = "🖳",
                NewValue = filename,
            };

            lock (DbContext)
            {
                var costCenterEntity = DbContext.CostCenters.Where(x => x.Guid == costCenter.Id).FirstOrDefault();
                var availableEntity = DbContext.Media.Where(x => x.Id == costCenterEntity.MediaId).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Media()
                    {
                        Guid = guid,
                        Name = filename,
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    };

                    DbContext.Media.Add(entity);
                    DbContext.SaveChanges();

                    costCenterEntity.MediaId = entity.Id;

                    DbContext.SaveChanges();
                }
                else
                {
                    if (File.Exists(Path.Combine(root, availableEntity.Guid)))
                    {
                        File.Delete(Path.Combine(root, availableEntity.Guid));
                    }

                    costCenter.Media.Name = filename;
                    costCenter.Media.Id = guid;

                    // Update
                    availableEntity.Name = filename;
                    availableEntity.Guid = guid;
                    availableEntity.Updated = DateTime.Now;

                    DbContext.SaveChanges();
                }
            }

            if (costCenter.Media != null)
            {
                File.WriteAllBytes(Path.Combine(root, guid), file.Data);
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
            var guid = Guid.NewGuid().ToString();
            var filename = file?.Value;
            var journalParameter = new WebItemEntityJournalParameter()
            {
                Name = "inventoryexpress:inventoryexpress.inventory.media.label",
                OldValue = "🖳",
                NewValue = filename,
            };

            lock (DbContext)
            {
                var inventoryEntity = DbContext.Inventories.Where(x => x.Guid == inventory.Id).FirstOrDefault();
                var availableEntity = DbContext.Media.Where(x => x.Id == inventoryEntity.MediaId).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Media()
                    {
                        Guid = guid,
                        Name = filename,
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    };

                    DbContext.Media.Add(entity);
                    DbContext.SaveChanges();

                    inventoryEntity.MediaId = entity.Id;

                    DbContext.SaveChanges();

                    var journal = new WebItemEntityJournal()
                    {
                        Action = "inventoryexpress:inventoryexpress.journal.action.inventory.media.add",
                        Parameters = new[] { journalParameter }
                    };

                    AddInventoryJournal(inventory, journal);
                }
                else
                {
                    if (File.Exists(Path.Combine(root, availableEntity.Guid)))
                    {
                        File.Delete(Path.Combine(root, availableEntity.Guid));
                    }

                    inventory.Media.Name = filename;
                    inventory.Media.Id = guid;

                    // Update
                    availableEntity.Name = filename;
                    availableEntity.Guid = guid;
                    availableEntity.Updated = DateTime.Now;

                    DbContext.SaveChanges();

                    var journal = new WebItemEntityJournal()
                    {
                        Action = "inventoryexpress:inventoryexpress.journal.action.inventory.media.edit",
                        Parameters = new[] { journalParameter }
                    };

                    AddInventoryJournal(inventory, journal);
                }
            }

            if (inventory.Media != null)
            {
                File.WriteAllBytes(Path.Combine(root, guid), file.Data);
            }
        }

        /// <summary>
        /// Fügt Medien hinzu oder aktuslisiert diese
        /// </summary>
        /// <param name="ledgerAccount">Das Sachkonto</param>
        /// <param name="file">Die Datei oder null</param>
        public static void AddOrUpdateMedia(WebItemEntityLedgerAccount ledgerAccount, ParameterFile file)
        {
            var root = Path.Combine(Context.DataPath, "media");
            var guid = Guid.NewGuid().ToString();
            var filename = file?.Value;
            var journalParameter = new WebItemEntityJournalParameter()
            {
                Name = "inventoryexpress:inventoryexpress.ledgeraccount.media.label",
                OldValue = "🖳",
                NewValue = filename,
            };

            lock (DbContext)
            {
                var ledgerAccountEntity = DbContext.LedgerAccounts.Where(x => x.Guid == ledgerAccount.Id).FirstOrDefault();
                var availableEntity = DbContext.Media.Where(x => x.Id == ledgerAccountEntity.MediaId).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Media()
                    {
                        Guid = guid,
                        Name = filename,
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    };

                    DbContext.Media.Add(entity);
                    DbContext.SaveChanges();

                    ledgerAccountEntity.MediaId = entity.Id;

                    DbContext.SaveChanges();
                }
                else
                {
                    if (File.Exists(Path.Combine(root, availableEntity.Guid)))
                    {
                        File.Delete(Path.Combine(root, availableEntity.Guid));
                    }

                    ledgerAccount.Media.Name = filename;
                    ledgerAccount.Media.Id = guid;

                    // Update
                    availableEntity.Name = filename;
                    availableEntity.Guid = guid;
                    availableEntity.Updated = DateTime.Now;

                    DbContext.SaveChanges();
                }
            }

            if (ledgerAccount.Media != null)
            {
                File.WriteAllBytes(Path.Combine(root, guid), file.Data);
            }
        }

        /// <summary>
        /// Fügt Medien hinzu oder aktuslisiert diese
        /// </summary>
        /// <param name="location">Der Standort</param>
        /// <param name="file">Die Datei oder null</param>
        public static void AddOrUpdateMedia(WebItemEntityLocation location, ParameterFile file)
        {
            var root = Path.Combine(Context.DataPath, "media");
            var guid = Guid.NewGuid().ToString();
            var filename = file?.Value;
            var journalParameter = new WebItemEntityJournalParameter()
            {
                Name = "inventoryexpress:inventoryexpress.location.media.label",
                OldValue = "🖳",
                NewValue = filename,
            };

            lock (DbContext)
            {
                var locationEntity = DbContext.Locations.Where(x => x.Guid == location.Id).FirstOrDefault();
                var availableEntity = DbContext.Media.Where(x => x.Id == locationEntity.MediaId).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Media()
                    {
                        Guid = guid,
                        Name = filename,
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    };

                    DbContext.Media.Add(entity);
                    DbContext.SaveChanges();

                    locationEntity.MediaId = entity.Id;

                    DbContext.SaveChanges();
                }
                else
                {
                    if (File.Exists(Path.Combine(root, availableEntity.Guid)))
                    {
                        File.Delete(Path.Combine(root, availableEntity.Guid));
                    }

                    location.Media.Name = filename;
                    location.Media.Id = guid;

                    // Update
                    availableEntity.Name = filename;
                    availableEntity.Guid = guid;
                    availableEntity.Updated = DateTime.Now;

                    DbContext.SaveChanges();
                }
            }

            if (location.Media != null)
            {
                File.WriteAllBytes(Path.Combine(root, guid), file.Data);
            }
        }
        
        /// <summary>
        /// Fügt Medien hinzu oder aktuslisiert diese
        /// </summary>
        /// <param name="manufacturer">Der Hersteller</param>
        /// <param name="file">Die Datei oder null</param>
        public static void AddOrUpdateMedia(WebItemEntityManufacturer manufacturer, ParameterFile file)
        {
            var root = Path.Combine(Context.DataPath, "media");
            var guid = Guid.NewGuid().ToString();
            var filename = file?.Value;
            var journalParameter = new WebItemEntityJournalParameter()
            {
                Name = "inventoryexpress:inventoryexpress.manufacturer.media.label",
                OldValue = "🖳",
                NewValue = filename,
            };

            lock (DbContext)
            {
                var manufacturerEntity = DbContext.Manufacturers.Where(x => x.Guid == manufacturer.Id).FirstOrDefault();
                var availableEntity = DbContext.Media.Where(x => x.Id == manufacturerEntity.MediaId).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Media()
                    {
                        Guid = guid,
                        Name = filename,
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    };

                    DbContext.Media.Add(entity);
                    DbContext.SaveChanges();

                    manufacturerEntity.MediaId = entity.Id;

                    DbContext.SaveChanges();
                }
                else
                {
                    if (File.Exists(Path.Combine(root, availableEntity.Guid)))
                    {
                        File.Delete(Path.Combine(root, availableEntity.Guid));
                    }

                    manufacturer.Media.Name = filename;
                    manufacturer.Media.Id = guid;

                    // Update
                    availableEntity.Name = filename;
                    availableEntity.Guid = guid;
                    availableEntity.Updated = DateTime.Now;

                    DbContext.SaveChanges();
                }
            }

            if (manufacturer.Media != null)
            {
                File.WriteAllBytes(Path.Combine(root, guid), file.Data);
            }
        }

        /// <summary>
        /// Fügt Medien hinzu oder aktuslisiert diese
        /// </summary>
        /// <param name="supplier">Der Lieferant</param>
        /// <param name="file">Die Datei oder null</param>
        public static void AddOrUpdateMedia(WebItemEntitySupplier supplier, ParameterFile file)
        {
            var root = Path.Combine(Context.DataPath, "media");
            var guid = Guid.NewGuid().ToString();
            var filename = file?.Value;
            var journalParameter = new WebItemEntityJournalParameter()
            {
                Name = "inventoryexpress:inventoryexpress.supplier.media.label",
                OldValue = "🖳",
                NewValue = filename,
            };

            lock (DbContext)
            {
                var supplierEntity = DbContext.Suppliers.Where(x => x.Guid == supplier.Id).FirstOrDefault();
                var availableEntity = DbContext.Media.Where(x => x.Id == supplierEntity.MediaId).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Media()
                    {
                        Guid = guid,
                        Name = filename,
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    };

                    DbContext.Media.Add(entity);
                    DbContext.SaveChanges();

                    supplierEntity.MediaId = entity.Id;

                    DbContext.SaveChanges();
                }
                else
                {
                    if (File.Exists(Path.Combine(root, availableEntity.Guid)))
                    {
                        File.Delete(Path.Combine(root, availableEntity.Guid));
                    }

                    supplier.Media.Name = filename;
                    supplier.Media.Id = guid;

                    // Update
                    availableEntity.Name = filename;
                    availableEntity.Guid = guid;
                    availableEntity.Updated = DateTime.Now;

                    DbContext.SaveChanges();
                }
            }

            if (supplier.Media != null)
            {
                File.WriteAllBytes(Path.Combine(root, guid), file.Data);
            }
        }

        /// <summary>
        /// Fügt Medien hinzu oder aktuslisiert diese
        /// </summary>
        /// <param name="template">Die Vorlage</param>
        /// <param name="file">Die Datei oder null</param>
        public static void AddOrUpdateMedia(WebItemEntityTemplate template, ParameterFile file)
        {
            var root = Path.Combine(Context.DataPath, "media");
            var guid = Guid.NewGuid().ToString();
            var filename = file?.Value;
            var journalParameter = new WebItemEntityJournalParameter()
            {
                Name = "inventoryexpress:inventoryexpress.template.media.label",
                OldValue = "🖳",
                NewValue = filename,
            };

            lock (DbContext)
            {
                var templateEntity = DbContext.Templates.Where(x => x.Guid == template.Id).FirstOrDefault();
                var availableEntity = DbContext.Media.Where(x => x.Id == templateEntity.MediaId).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new Media()
                    {
                        Guid = guid,
                        Name = filename,
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    };

                    DbContext.Media.Add(entity);
                    DbContext.SaveChanges();

                    templateEntity.MediaId = entity.Id;

                    DbContext.SaveChanges();
                }
                else
                {
                    if (File.Exists(Path.Combine(root, availableEntity.Guid)))
                    {
                        File.Delete(Path.Combine(root, availableEntity.Guid));
                    }

                    template.Media.Name = filename;
                    template.Media.Id = guid;

                    // Update
                    availableEntity.Name = filename;
                    availableEntity.Guid = guid;
                    availableEntity.Updated = DateTime.Now;

                    DbContext.SaveChanges();
                }
            }

            if (template.Media != null)
            {
                File.WriteAllBytes(Path.Combine(root, guid), file.Data);
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