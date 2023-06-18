using InventoryExpress.Model;
using InventoryExpress.Parameter;
using InventoryExpress.WebPage;
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
    [WebExSection(Section.MorePreferences)]
    [Module<Module>]
    [Scope<PageInventoryDetails>]
    public sealed class FragmentMoreAttachment : FragmentControlDropdownItemLink
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentMoreAttachment()
        {
            Icon = new PropertyIcon(TypeIcon.PaperClip);
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
            var guid = context.Request.GetParameter<ParameterInventoryId>()?.Value;
            var count = ViewModel.CountInventoryAttachments(guid);

            Text = InternationalizationManager.I18N(context.Culture, "inventoryexpress:inventoryexpress.inventory.attachment.function") + $" ({count})";
            Uri = context.Uri.Append("attachments");

            return base.Render(context);
        }
    }
}
