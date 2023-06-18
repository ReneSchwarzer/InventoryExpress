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
    public sealed class ControlQuickCreateManufacturer : FragmentControlSplitButtonItemLink
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ControlQuickCreateManufacturer()
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

            Text = "inventoryexpress:inventoryexpress.manufacturer.label";
            Uri = ComponentManager.SitemapManager.GetUri<PageManufacturerAdd>();
            Icon = new PropertyIcon(TypeIcon.Industry);
            Modal = new PropertyModal(TypeModal.Formular, TypeModalSize.Large);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Active = context.Page is IPageManufacturer ? TypeActive.Active : TypeActive.None;

            return base.Render(context);
        }
    }
}
