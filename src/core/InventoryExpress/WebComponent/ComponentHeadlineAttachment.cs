using InventoryExpress.Model;
using System.Linq;
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
    [Section(Section.HeadlinePreferences)]
    [Module("inventoryexpress")]
    [Context("inventorydetails")]
    public sealed class ComponentHeadlineAttachment : ComponentControlLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentHeadlineAttachment()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            Icon = new PropertyIcon(TypeIcon.PaperClip);
            Size = new PropertySizeText(TypeSizeText.Small);
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
            var inventory = ViewModel.GetInventory(guid);
            var count = ViewModel.GetInventoryAttachments(inventory).Count();

            Title = $"{InternationalizationManager.I18N(context.Culture, "inventoryexpress:inventoryexpress.inventory.attachment.function")} ({count})";
            Styles.Add(count == 0 ? "display: none;" : string.Empty);
            Uri = context.Uri.Append("attachments");

            return base.Render(context);
        }
    }
}
