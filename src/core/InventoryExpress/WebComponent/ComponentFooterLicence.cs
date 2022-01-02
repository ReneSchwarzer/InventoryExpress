using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebAttribute;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebComponent
{
    [Section(Section.FooterPrimary)]
    [Module("inventoryexpress")]
    public sealed class ComponentFooterLicence : ComponentControlPanel
    { 
        /// <summary>
        /// Die Lizenz
        /// </summary>
        private ControlLink LicenceLink { get; } = new ControlLink()
        {
            TextColor = new PropertyColorText(TypeColorText.Muted),
            Size = new PropertySizeText(TypeSizeText.Small)
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentFooterLicence()
            : base()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IComponentContext context)
        {
            base.Initialization(context);

            Classes.Add("text-center");

            Content.Add(LicenceLink);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            LicenceLink.Text = I18N(context.Culture, "inventoryexpress:inventoryexpress.footer.licence.label");
            LicenceLink.Uri = new UriAbsolute(I18N(context.Culture, "inventoryexpress:inventoryexpress.footer.licence.uri"));

            return base.Render(context);
        }
    }
}
