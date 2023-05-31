using InventoryExpress.WebPage;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [WebExSection(Section.AppNavigationPrimary)]
    [WebExModule<Module>]
    [WebExCache]
    public sealed class FragmentAppNavigationCostCenter : FragmentControlNavigationItemLink
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentAppNavigationCostCenter()
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

            Text = "inventoryexpress:inventoryexpress.costcenters.label";
            Uri = ComponentManager.SitemapManager.GetUri<PageCostCenters>();
            Icon = new PropertyIcon(TypeIcon.ShoppingBag);
            Active = page is IPageCostCenter ? TypeActive.Active : TypeActive.None;
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
