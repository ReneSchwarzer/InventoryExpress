using InventoryExpress.WebPage;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.UI.Attribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebPage;

namespace InventoryExpress.WebComponent
{
    [Section(Section.AppQuickcreateSecondary)]
    [Module("inventoryexpress")]
    public sealed class ComponentQuickCreateLedgerAccount : ControlSplitButtonItemLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentQuickCreateLedgerAccount()
            : base()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public void Initialization(IComponentContext context)
        {
            Text = "inventoryexpress:inventoryexpress.ledgeraccount.label";
            Uri = new UriResource(context.Module.ContextPath, "ledgeraccounts/add");
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
