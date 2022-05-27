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
            lock (Instance.Database)
            {
                var ledgerAccounts = Instance.LedgerAccounts.Select(x => new WebItemEntityLedgerAccount(x));

                return wql.Apply(ledgerAccounts.AsQueryable());
            }
        }

        /// <summary>
        /// Liefert ein Sachkonto
        /// </summary>
        /// <param name="id">Die ID des Sachkontos</param>
        /// <returns>Das Sachkonto oder null</returns>
        public static WebItemEntityLedgerAccount GetLedgerAccount(string id)
        {
            lock (Instance.Database)
            {
                var ledgerAccount = Instance.LedgerAccounts.Where(x => x.Guid == id).Select(x => new WebItemEntityLedgerAccount(x)).FirstOrDefault();

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
            lock (Instance.Database)
            {
                var condition = from i in Instance.Inventories
                                join l in Instance.LedgerAccounts on i.ConditionId equals l.Id
                                where i.Guid == inventory.ID
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
            lock (Instance.Database)
            {
                var availableEntity = Instance.Locations.Where(x => x.Guid == location.ID).FirstOrDefault();

                if (availableEntity == null)
                {
                    // Neu erstellen
                    var entity = new LedgerAccount()
                    {
                        Guid = location.ID,
                        Name = location.Name,
                        Description = location.Description,
                        Tag = location.Tag,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Media = new Media()
                        {
                            Guid = location.Media?.ID,
                            Name = location.Media?.Name ?? "",
                            Description = location.Media?.Description,
                            Tag = location.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        }
                    };

                    Instance.LedgerAccounts.Add(entity);
                }
                else
                {
                    // Update
                    var availableMedia = location.Media != null ? Instance.Media.Where(x => x.Guid == location.Media.ID).FirstOrDefault() : null;

                    availableEntity.Name = location.Name;
                    availableEntity.Description = location.Description;
                    availableEntity.Tag = location.Tag;
                    availableEntity.Updated = DateTime.Now;

                    if (availableMedia == null)
                    {
                        var media = new Media()
                        {
                            Guid = location.Media?.ID,
                            Name = location.Media?.Name,
                            Description = location.Media?.Description,
                            Tag = location.Media?.Tag,
                            Created = DateTime.Now,
                            Updated = DateTime.Now
                        };

                        Instance.Media.Add(media);
                    }
                    else if (!string.IsNullOrWhiteSpace(location.Media.Name))
                    {
                        availableMedia.Name = location.Media?.Name;
                        availableMedia.Description = location.Media?.Description;
                        availableMedia.Tag = location.Media?.Tag;
                        availableMedia.Updated = DateTime.Now;
                    }
                }
            }
        }

        /// <summary>
        /// Löscht ein Sachkonto
        /// </summary>
        /// <param name="id">Die ID des Sachkontos</param>
        public static void DeleteLedgerAccount(string id)
        {
            lock (Instance.Database)
            {
                var entity = Instance.LedgerAccounts.Where(x => x.Guid == id).FirstOrDefault();
                var entityMedia = Instance.Media.Where(x => x.Id == entity.MediaId).FirstOrDefault();

                if (entityMedia != null)
                {
                    DeleteMedia(entityMedia.Guid);
                }

                if (entity != null)
                {
                    Instance.LedgerAccounts.Remove(entity);
                }
            }
        }
    }
}