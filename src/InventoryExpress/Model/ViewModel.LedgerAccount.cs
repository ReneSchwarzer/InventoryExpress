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
        /// Returns the ledger account uri.
        /// </summary>
        /// <param name="guid">The id of the ledger account.</param>
        /// <returns>The uri or null.</returns>
        public static string GetLedgerAccountUri(string guid)
        {
            return ComponentManager.SitemapManager.GetUri<PageLedgerAccountEdit>(new ParameterLedgerAccountId(guid));
        }

        /// <summary>
        /// Returns all ledger accounts.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>An enumeration that includes the ledger accounts.</returns>
        public static IEnumerable<WebItemEntityLedgerAccount> GetLedgerAccounts(string wql = "")
        {
            var wqlStatement = ComponentManager.GetComponent<IndexManager>()
                    .ExecuteWql<WebItemEntityLedgerAccount>(wql);

            return GetLedgerAccounts(wqlStatement);
        }

        /// <summary>
        /// Returns all ledger accounts.
        /// </summary>
        /// <param name="wql">The filtering and sorting options.</param>
        /// <returns>An enumeration that includes the ledger accounts.</returns>
        public static IEnumerable<WebItemEntityLedgerAccount> GetLedgerAccounts(IWqlStatement<WebItemEntityLedgerAccount> wql)
        {
            lock (DbContext)
            {
                var ledgerAccounts = DbContext.LedgerAccounts.Select(x => new WebItemEntityLedgerAccount(x));

                return wql.Apply(ledgerAccounts.AsQueryable());
            }
        }

        /// <summary>
        /// Returns a ledger account.
        /// </summary>
        /// <param name="id">The id of the ledger account.</param>
        /// <returns>The ledger account oder null.</returns>
        public static WebItemEntityLedgerAccount GetLedgerAccount(string id)
        {
            lock (DbContext)
            {
                var ledgerAccount = DbContext.LedgerAccounts.Where(x => x.Guid == id).Select(x => new WebItemEntityLedgerAccount(x)).FirstOrDefault();

                return ledgerAccount;
            }
        }

        /// <summary>
        /// Returns the ledger account that is assigned to the inventory item.
        /// </summary>
        /// <param name="inventory">The inventory item.</param>
        /// <returns>The ledger account oder null.</returns>
        public static WebItemEntityLedgerAccount GetLedgerAccount(WebItemEntityInventory inventory)
        {
            lock (DbContext)
            {
                var condition = from i in DbContext.Inventories
                                join l in DbContext.LedgerAccounts on i.LedgerAccountId equals l.Id
                                where i.Guid == inventory.Guid
                                select new WebItemEntityLedgerAccount(l);

                return condition.FirstOrDefault();
            }
        }

        /// <summary>
        /// Adds or updates a ledger account.
        /// </summary>
        /// <param name="location">The ledger account.</param>
        public static void AddOrUpdateLedgerAccount(WebItemEntityLedgerAccount location)
        {
            lock (DbContext)
            {
                var availableEntity = DbContext.LedgerAccounts.Where(x => x.Guid == location.Guid).FirstOrDefault();

                if (availableEntity == null)
                {
                    // create new
                    var entity = new LedgerAccount()
                    {
                        Guid = location.Guid,
                        Name = location.Name,
                        Description = location.Description,
                        Tag = location.Tag,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Media = new Media()
                        {
                            Guid = location.Media?.Guid,
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
                    // update
                    var availableMedia = location.Media != null ? DbContext.Media.Where(x => x.Guid == location.Media.Guid).FirstOrDefault() : null;

                    availableEntity.Name = location.Name;
                    availableEntity.Description = location.Description;
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
        /// Deletes a ledger account.
        /// </summary>
        /// <param name="id">The id of the ledger account.</param>
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
        /// Checks whether the ledger account is in use.
        /// </summary>
        /// <param name="ledgerAccount">The ledger account.</param>
        /// <returns>True when in use, false otherwise.</returns>
        public static bool GetLedgerAccountInUse(WebItemEntityLedgerAccount ledgerAccount)
        {
            lock (DbContext)
            {
                var used = from i in DbContext.Inventories
                           join l in DbContext.LedgerAccounts on i.LedgerAccountId equals l.Id
                           where l.Guid == ledgerAccount.Guid
                           select l;

                return used.Any();
            }
        }
    }
}