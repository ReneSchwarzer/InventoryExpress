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
        /// Ermittelt die Sachkonten-URL
        /// </summary>
        /// <param name="guid">Die ID des Sachkontos</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetLedgerAccountUri(string guid)
        {
            return $"{RootUri}/ledgeraccounts/{guid}";
        }

        /// <summary>
        /// Liefert alle Sachkonten
        /// </summary>
        /// <param name="wql">Die Filter- und Sortieroptinen</param>
        /// <returns>Eine Aufzählung, welche die Sachkonten beinhaltet</returns>
        public static IEnumerable<WebItemEntityLedgerAccount> GetLedgerAccounts(WqlStatement wql)
        {
            lock (DbContext)
            {
                var ledgerAccounts = DbContext.LedgerAccounts.Select(x => new WebItemEntityLedgerAccount(x));

                return wql.Apply(ledgerAccounts.AsQueryable()).ToList();
            }
        }

        /// <summary>
        /// Liefert ein Sachkonto
        /// </summary>
        /// <param name="id">Die ID des Sachkontos</param>
        /// <returns>Das Sachkonto oder null</returns>
        public static WebItemEntityLedgerAccount GetLedgerAccount(string id)
        {
            lock (DbContext)
            {
                var ledgerAccount = DbContext.LedgerAccounts.Where(x => x.Guid == id).Select(x => new WebItemEntityLedgerAccount(x)).FirstOrDefault();

                return ledgerAccount;
            }
        }

        /// <summary>
        /// Liefert das Sachkonto, welche dem Inventargegenstand zugeordnet ist
        /// </summary>
        /// <param name="inventory">Der Inventargegenstand</param>
        /// <returns>Das Sachkonto oder null</returns>
        public static WebItemEntityLedgerAccount GetLedgerAccount(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var condition = from i in DbContext.Inventories
                                join l in DbContext.LedgerAccounts on i.LedgerAccountId equals l.Id
                                where i.Guid == inventory.Id
                                select new WebItemEntityLedgerAccount(l);

                return condition.FirstOrDefault();
            }
        }

        /// <summary>
        /// Fügt ein Sachkonto hinzu oder aktuallisiert diesen
        /// </summary>
        /// <param name="location">Das Sachkonto</param>
        public static void AddOrUpdateLedgerAccount(WebItemEntityLedgerAccount location)
        {
            lock (DbContext)
            {
                var availableEntity = DbContext.LedgerAccounts.Where(x => x.Guid == location.Id).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new LedgerAccount()
                    {
                        Guid = location.Id,
                        Name = location.Name,
                        Description = location.Description,
                        Tag = location.Tag,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Media = new Media()
                        {
                            Guid = location.Media?.Id,
                            Name = location.Media?.Name ?? "",
                            Description = location.Media?.Description,
                            Tag = location.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        }
                    };

                    DbContext.LedgerAccounts.Add(entity);
                    DbContext.SaveChanges();
                }
                else
                {
                    // Update
                    var availableMedia = location.Media != null ? DbContext.Media.Where(x => x.Guid == location.Media.Id).FirstOrDefault() : null;

                    availableEntity.Name = location.Name;
                    availableEntity.Description = location.Description;
                    availableEntity.Tag = location.Tag;
                    availableEntity.Updated = DateTime.Now;

                    if (availableMedia == null)
                    {
                        var media = new Media()
                        {
                            Guid = location.Media?.Id,
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
        /// Löscht ein Sachkonto
        /// </summary>
        /// <param name="id">Die ID des Sachkontos</param>
        public static void DeleteLedgerAccount(string id)
        {
            lock (DbContext)
            {
                var entity = DbContext.LedgerAccounts.Where(x => x.Guid == id).FirstOrDefault();
                var entityMedia = DbContext.Media.Where(x => x.Id == entity.MediaId).FirstOrDefault();

                if (entityMedia != null)
                {
                    DeleteMedia(entityMedia.Guid);
                }

                if (entity != null)
                {
                    DbContext.LedgerAccounts.Remove(entity);
                    DbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Prüft ob das Sachkonto in Verwendung ist
        /// </summary>
        /// <param name="ledgerAccount">Das Sachkonto</param>
        /// <returns>True wenn in Verwendung, false sonst</returns>
        public static bool GetLedgerAccountInUse(WebItemEntityLedgerAccount ledgerAccount)
        {
            lock (DbContext)
            {
                var used = from i in DbContext.Inventories
                           join l in DbContext.LedgerAccounts on i.LedgerAccountId equals l.Id
                           where l.Guid == ledgerAccount.Id
                           select l;

                return used.Any();
            }
        }
    }
}