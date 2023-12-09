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
    [Scope<PageLocationEdit>]
    public sealed class FragmentMoreLocationDelete : FragmentControlDropdownItemLink
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentMoreLocationDelete()
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
            var guid = context.Request.GetParameter<ParameterLocationId>();
            var location = ViewModel.GetLocation(guid?.Value);
            var inUse = ViewModel.GetLocationInUse(location);

            Active = inUse ? TypeActive.Disabled : TypeActive.None;
            TextColor = inUse ? new PropertyColorText(TypeColorText.Muted) : TextColor;

            Uri = ComponentManager.SitemapManager.GetUri<PageLocationDelete>(guid);
            Modal = new PropertyModal(TypeModal.Formular, TypeModalSize.Default)
            {
                RedirectUri = ComponentManager.SitemapManager.GetUri<PageLocations>()
            };

            return base.Render(context);
        }
    }
}
