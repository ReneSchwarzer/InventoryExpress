using InventoryExpress.Pages;
using WebExpress.Html;
using WebExpress.Plugins;
using WebExpress.UI.Controls;

namespace InventoryExpress.Controls
{
    [PluginComponent("app.navigation.primary")]
    public class ControlAppNavigationLedgerAccount : ControlNavigationItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlAppNavigationLedgerAccount()
            : base()
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Text = context.I18N("inventoryexpress.ledgeraccounts.label", "Ledger accounts");
            Uri = context.Page.Uri.Root.Append("ledgeraccounts");
            Active = context.Page is IPageLedgerAccount ? TypeActive.Active : TypeActive.None;
            Icon = new PropertyIcon(TypeIcon.At);

            return base.Render(context);
        }

    }
}
