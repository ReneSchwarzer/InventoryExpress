using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebAttribute;
using WebExpress.WebPage;
using WebExpress.WebPlugin;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebComponent
{
    [Section(Section.FooterPrimary)]
    [Module("inventoryexpress")]
    public sealed class ComponentFooterVersion : ComponentControlText
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
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IComponentContext context, IPage page)
        {
            base.Initialization(context, page);

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
