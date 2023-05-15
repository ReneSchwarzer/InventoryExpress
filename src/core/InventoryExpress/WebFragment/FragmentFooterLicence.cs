using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [WebExSection(Section.FooterPrimary)]
    [WebExModule(typeof(Module))]
    public sealed class FragmentFooterLicence : FragmentControlPanel
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
        public FragmentFooterLicence()
            : base()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);

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
            LicenceLink.Text = InternationalizationManager.I18N(context.Culture, "inventoryexpress:inventoryexpress.footer.licence.label");
            LicenceLink.Uri = InternationalizationManager.I18N(context.Culture, "inventoryexpress:inventoryexpress.footer.licence.uri");

            return base.Render(context);
        }
    }
}
