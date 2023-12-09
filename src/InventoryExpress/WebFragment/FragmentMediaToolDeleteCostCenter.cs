using InventoryExpress.Model;
using InventoryExpress.Parameter;
using InventoryExpress.WebPage;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebComponent;
using WebExpress.WebCore.WebHtml;
using WebExpress.WebCore.WebPage;
using WebExpress.WebUI.WebAttribute;
using WebExpress.WebUI.WebControl;
using WebExpress.WebUI.WebFragment;

namespace InventoryExpress.WebFragment
{
    [Section("mediatool.secondary")]
    [Module<Module>]
    [Scope<PageCostCenterEdit>]
    public sealed class FragmentMediaToolDeleteCostCenter : FragmentMediaToolDelete
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentMediaToolDeleteCostCenter()
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
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var guid = context.Request.GetParameter<ParameterCostCenterId>();
            var costCenter = ViewModel.GetCostCenter(guid?.Value);
            var disabled = costCenter.Media?.Image == null;

            Active = disabled ? TypeActive.Disabled : TypeActive.None;
            TextColor = disabled ? new PropertyColorText(TypeColorText.Muted) : TextColor;

            Uri = ComponentManager.SitemapManager.GetUri<PageMediaDelete>(new ParameterMediaId(costCenter.Media?.Guid));
            Modal = new PropertyModal(TypeModal.Formular, TypeModalSize.Default)
            {
                RedirectUri = ComponentManager.SitemapManager.GetUri<PageCostCenterEdit>(guid)
            };

            return base.Render(context);
        }
    }
}
