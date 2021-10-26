using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Plugin;
using WebExpress.UI.Attribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebComponent
{
    [Section(Section.FooterPrimary)]
    [Module("inventoryexpress")]
    public sealed class ComponentFooterVersion : ControlText, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentFooterVersion()
            : base()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public void Initialization(IComponentContext context)
        {
            TextColor = new PropertyColorText(TypeColorText.Muted);
            Format = TypeFormatText.Center;
            Size = new PropertySizeText(TypeSizeText.Small);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var webexpress = PluginManager.Plugins.Where(x => x.PluginID == "webexpress.ui").FirstOrDefault();
            var inventoryExpress = PluginManager.Plugins.Where(x => x.Assembly == GetType().Assembly).FirstOrDefault();

            Text = string.Format
            (
                I18N(context.Culture, "inventoryexpress:inventoryexpress.footer.version.label"),
                inventoryExpress?.PluginName,
                inventoryExpress?.Version,
                webexpress?.PluginName,
                webexpress?.Version
            );

            return base.Render(context);
        }
    }
}
