using InventoryExpress.Model;
using InventoryExpress.Parameter;
using InventoryExpress.WebPage;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebComponent;
using WebExpress.WebCore.WebHtml;
using WebExpress.WebCore.WebPage;
using WebExpress.WebCore.WebUri;
using WebExpress.WebUI.WebAttribute;
using WebExpress.WebUI.WebControl;
using WebExpress.WebUI.WebFragment;

namespace InventoryExpress.WebFragment
{
    [Section(Section.MoreSecondary)]
    [Module<Module>]
    [Scope<PageManufacturerEdit>]
    public sealed class FragmentMoreManufacturerDelete : FragmentControlDropdownItemLink
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentMoreManufacturerDelete()
        {
            TextColor = new PropertyColorText(TypeColorText.Danger);
            Uri = new UriFragment();
            Text = "inventoryexpress:inventoryexpress.delete.label";
            Icon = new PropertyIcon(TypeIcon.Trash);
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">The page where the component is active.</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var guid = context.Request.GetParameter<ParameterManufacturerId>();
            var manufacturer = ViewModel.GetManufacturer(guid?.Value);
            var inUse = ViewModel.GetManufacturerInUse(manufacturer);

            Active = inUse ? TypeActive.Disabled : TypeActive.None;
            TextColor = inUse ? new PropertyColorText(TypeColorText.Muted) : TextColor;

            Uri = ComponentManager.SitemapManager.GetUri<PageManufacturerDelete>(guid);
            Modal = new PropertyModal(TypeModal.Formular, TypeModalSize.Default)
            {
                RedirectUri = ComponentManager.SitemapManager.GetUri<PageManufacturers>()
            };

            return base.Render(context);
        }
    }
}
