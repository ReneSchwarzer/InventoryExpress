using InventoryExpress.Model;
using InventoryExpress.Parameter;
using InventoryExpress.WebPageSetting;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebHtml;
using WebExpress.WebCore.WebPage;
using WebExpress.WebUI.WebAttribute;
using WebExpress.WebUI.WebFragment;

namespace InventoryExpress.WebFragment
{
    [Section(Section.SidebarHeader)]
    [Module<Module>]
    [Scope<PageSettingTemplateEdit>]
    public sealed class FragmentSidebarMediaTemplate : FragmentSidebarMedia
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentSidebarMediaTemplate()
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
            var guid = context.Request.GetParameter<ParameterTemplateId>();
            var inventory = ViewModel.GetTemplate(guid?.Value);

            Image.Uri = inventory.Image;

            return base.Render(context);
        }
    }
}
