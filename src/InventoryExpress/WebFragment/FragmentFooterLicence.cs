using WebExpress.WebHtml;
using WebExpress.Internationalization;
using WebExpress.WebUI.WebAttribute;
using WebExpress.WebUI.WebControl;
using WebExpress.WebUI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [Section(Section.FooterPrimary)]
    [Module<Module>]
    public sealed class FragmentFooterLicence : FragmentControlPanel
    {
        /// <summary>
        /// The license.
        /// </summary>
        private ControlLink LicenceLink { get; } = new ControlLink()
        {
            TextColor = new PropertyColorText(TypeColorText.Muted),
            Size = new PropertySizeText(TypeSizeText.Small)
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentFooterLicence()
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

            Classes.Add("text-center");

            Content.Add(LicenceLink);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            LicenceLink.Text = InternationalizationManager.I18N(context.Culture, "inventoryexpress:inventoryexpress.footer.licence.label");
            LicenceLink.Uri = InternationalizationManager.I18N(context.Culture, "inventoryexpress:inventoryexpress.footer.licence.uri");

            return base.Render(context);
        }
    }
}
