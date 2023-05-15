using InventoryExpress.WebPage;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;
using WebExpress.WebUri;

namespace InventoryExpress.WebFragment
{
    [WebExSection(Section.AppNavigationPrimary)]
    [WebExModule(typeof(Module))]
    [WebExCache]
    public sealed class FragmentAppNavigationLedgerAccount : FragmentControlNavigationItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public FragmentAppNavigationLedgerAccount()
            : base()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);

            Text = "inventoryexpress:inventoryexpress.ledgeraccounts.label";
            Uri = UriResource.Combine(context.ApplicationContext.ContextPath, "ledgeraccounts");
            Icon = new PropertyIcon(TypeIcon.At);
            Active = page is IPageLedgerAccount ? TypeActive.Active : TypeActive.None;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            return base.Render(context);
        }
    }
}
