using InventoryExpress.WebPage;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebCore.Internationalization;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebHtml;
using WebExpress.WebCore.WebPage;
using WebExpress.WebUI.WebAttribute;
using WebExpress.WebUI.WebControl;
using WebExpress.WebUI.WebFragment;

namespace InventoryExpress.WebFragment
{
    [Section(Section.MorePrimary)]
    [Module<Module>]
    [Scope<PageInventoryDetails>]
    public sealed class FragmentMoreJournal : FragmentControlDropdownItemLink
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentMoreJournal()
        {
            Icon = new PropertyIcon(TypeIcon.Book);
            TextColor = new PropertyColorText(TypeColorText.Secondary);
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
            Text = InternationalizationManager.I18N(context.Culture, "inventoryexpress:inventoryexpress.inventory.journal.function");
            Uri = context.Uri.Append("journal");

            return base.Render(context);
        }
    }
}
