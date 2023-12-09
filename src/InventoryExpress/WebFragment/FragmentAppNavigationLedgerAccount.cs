using InventoryExpress.WebPage;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebComponent;
using WebExpress.WebCore.WebHtml;
using WebExpress.WebCore.WebPage;
using WebExpress.WebUI.WebAttribute;
using WebExpress.WebUI.WebControl;
using WebExpress.WebUI.WebFragment;

namespace InventoryExpress.WebFragment
{
    [Section(Section.AppNavigationPrimary)]
    [Module<Module>]
    [Cache]
    public sealed class FragmentAppNavigationLedgerAccount : FragmentControlNavigationItemLink
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentAppNavigationLedgerAccount()
            : base()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">The page where the component is active.</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);

            Text = "inventoryexpress:inventoryexpress.ledgeraccounts.label";
            Uri = ComponentManager.SitemapManager.GetUri<PageLedgerAccounts>();
            Icon = new PropertyIcon(TypeIcon.At);
            Active = page is IPageLedgerAccount ? TypeActive.Active : TypeActive.None;
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            return base.Render(context);
        }
    }
}
