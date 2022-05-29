using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebComponent
{
    [Section(Section.MorePreferences)]
    [Module("inventoryexpress")]
    [Context("inventorydetails")]
    public sealed class ComponentMoreAttachment : ComponentControlDropdownItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentMoreAttachment()
        {
            Icon = new PropertyIcon(TypeIcon.PaperClip);
            TextColor = new PropertyColorText(TypeColorText.Secondary);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IComponentContext context, IPage page)
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
