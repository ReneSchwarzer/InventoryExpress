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
    [WebExSection(Section.AppQuickcreateSecondary)]
    [Module<Module>]
    public sealed class FragmentQuickCreateSupplier : FragmentControlSplitButtonItemLink
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentQuickCreateSupplier()
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

            Text = "inventoryexpress:inventoryexpress.supplier.label";
            Uri = ComponentManager.SitemapManager.GetUri<PageSupplierAdd>();
            Icon = new PropertyIcon(TypeIcon.Truck);
            Modal = new PropertyModal(TypeModal.Formular, TypeModalSize.Large);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Active = context.Page is IPageSupplier ? TypeActive.Active : TypeActive.None;

            return base.Render(context);
        }

    }
}
