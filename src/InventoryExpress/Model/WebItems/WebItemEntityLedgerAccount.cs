using InventoryExpress.Model.Entity;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Sachkonto
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
        /// <param name="ledgerAccount">Das Datenbankobjektes des Sachkontos</param>
        public WebItemEntityLedgerAccount(LedgerAccount ledgerAccount)
            : base(ledgerAccount)
        {
            Uri = ViewModel.GetLedgerAccountUri(ledgerAccount.Guid);
        }
    }
}
