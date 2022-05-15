using InventoryExpress.Model.WebItems;
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
        /// <param name="guid">Die SachkontenID</param>
        /// <returns>Die Uri oder null</returns>
        public static string GetLedgerAccountUri(string guid)
        {
            return $"{ RootUri }/ledgeraccounts/{ guid }";
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
        /// Liefert einen Lieferanten
        /// </summary>
        /// <param name="id">Die interne SachkontenID</param>
        /// <returns>Das Sachkonten oder null</returns>
        public static WebItemEntityLedgerAccount GetLedgerAccount(int? id)
        {
            lock (Instance.Database)
            {
                var ledgerAccount = Instance.LedgerAccounts.Where(x => x.Id == id).Select(x => new WebItemEntityLedgerAccount(x)).FirstOrDefault();

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
    }
}