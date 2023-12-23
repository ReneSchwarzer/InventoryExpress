using InventoryExpress.Model.Entity;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// The ledger account.
    /// </summary>
    public class WebItemEntityLedgerAccount : WebItemEntityBaseTag
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WebItemEntityLedgerAccount()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ledgerAccount">The database object of the ledger account.</param>
        internal WebItemEntityLedgerAccount(LedgerAccount ledgerAccount)
            : base(ledgerAccount)
        {
        }
    }
}
