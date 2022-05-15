using InventoryExpress.Model.Entity;

namespace InventoryExpress.Model.WebItems
{
    /// <summary>
    /// Sachkonto
    /// </summary>
    public class WebItemEntityLedgerAccount : WebItemEntityBaseTag
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebItemEntityLedgerAccount()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="ledgerAccount">Das Datenbankobjektes des Sachkontos</param>
        public WebItemEntityLedgerAccount(LedgerAccount ledgerAccount)
            : base(ledgerAccount)
        {
            Uri = ViewModel.GetLedgerAccountUri(ledgerAccount.Guid);
        }
    }
}
