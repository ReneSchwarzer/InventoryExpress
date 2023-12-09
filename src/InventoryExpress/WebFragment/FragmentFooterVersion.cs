using System.Linq;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebCore.Internationalization;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebComponent;
using WebExpress.WebCore.WebHtml;
using WebExpress.WebCore.WebPage;
using WebExpress.WebUI.WebAttribute;
using WebExpress.WebUI.WebControl;
using WebExpress.WebUI.WebFragment;

namespace InventoryExpress.WebFragment
{
    [Section(Section.FooterPrimary)]
    [Module<Module>]
    public sealed class FragmentFooterVersion : FragmentControlText
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentFooterVersion()
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

            TextColor = new PropertyColorText(TypeColorText.Muted);
            Format = TypeFormatText.Center;
            Size = new PropertySizeText(TypeSizeText.Small);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var webexpress = ComponentManager.PluginManager.Plugins.Where(x => x.PluginId == "webexpress.ui").FirstOrDefault();
            var inventoryExpress = ComponentManager.PluginManager.Plugins.Where(x => x.Assembly == GetType().Assembly).FirstOrDefault();

            Text = string.Format
            (
                InternationalizationManager.I18N(context.Culture, "inventoryexpress:inventoryexpress.footer.version.label"),
                inventoryExpress?.PluginName,
                inventoryExpress?.Version,
                webexpress?.PluginName,
                webexpress?.Version
            );

            return base.Render(context);
        }
    }
}
