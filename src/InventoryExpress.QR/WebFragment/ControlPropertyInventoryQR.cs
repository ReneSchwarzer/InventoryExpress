using InventoryExpress.Parameter;
using InventoryExpress.WebPage;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebHtml;
using WebExpress.WebCore.WebPage;
using WebExpress.WebUI.WebAttribute;
using WebExpress.WebUI.WebControl;
using WebExpress.WebUI.WebFragment;

namespace InventoryExpress.QR.WebFragment
{
    [Section(Section.PropertyPreferences)]
    [Module<Module>]
    [Scope<PageInventoryDetails>]
    public sealed class ControlPropertyInventoriesQR : FragmentControlImage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ControlPropertyInventoriesQR()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
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
            var id = context.Request.GetParameter<ParameterInventoryId>()?.Value;
            Uri = context.Uri.ModuleRoot.Append("qr").Append(id);
            Width = 200;

            return base.Render(context);
        }
    }
}
