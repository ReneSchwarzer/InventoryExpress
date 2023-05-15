using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebFragment;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [WebExSection(Section.MorePreferences)]
    [WebExModule(typeof(Module))]
    [WebExContext("inventorydetails")]
    public sealed class FragmentMoreAttachment : FragmentControlDropdownItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public FragmentMoreAttachment()
        {
            Icon = new PropertyIcon(TypeIcon.PaperClip);
            TextColor = new PropertyColorText(TypeColorText.Secondary);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var guid = context.Request.GetParameter("InventoryID")?.Value;
            var count = ViewModel.CountInventoryAttachments(guid);

            Text = InternationalizationManager.I18N(context.Culture, "inventoryexpress:inventoryexpress.inventory.attachment.function") + $" ({count})";
            Uri = context.Uri.Append("attachments");

            return base.Render(context);
        }
    }
}
