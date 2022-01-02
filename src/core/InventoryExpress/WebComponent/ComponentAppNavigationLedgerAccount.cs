using InventoryExpress.WebPage;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebComponent
{
    [Section(Section.AppNavigationPrimary)]
    [Module("inventoryexpress")]
    public sealed class ComponentAppNavigationLedgerAccount : ComponentControlNavigationItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentAppNavigationLedgerAccount()
            : base()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IComponentContext context)
        {
            base.Initialization(context);

            Text = "inventoryexpress:inventoryexpress.ledgeraccounts.label";
            Uri = new UriResource(context.Module.ContextPath, "ledgeraccounts");
            Icon = new PropertyIcon(TypeIcon.At);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Active = context.Page is IPageLedgerAccount ? TypeActive.Active : TypeActive.None;

            return base.Render(context);
        }

    }
}
