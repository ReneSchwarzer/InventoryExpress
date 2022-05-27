using InventoryExpress.Model;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebAttribute;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

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
            lock (ViewModel.Instance.Database)
            {
                var guid = context.Request.GetParameter("InventoryID")?.Value;
                var count = (from i in ViewModel.Instance.Inventories
                             join a in ViewModel.Instance.InventoryAttachments
                             on i.Id equals a.InventoryId
                             where i.Guid == guid
                             select a).Count();

                Title = I18N(context.Culture, "inventoryexpress:inventoryexpress.inventory.attachment.function") + $" ({count})";
                Styles.Add(count == 0 ? "display: none;" : string.Empty);
                Uri = context.Uri.Append("attachments");
            }

            return base.Render(context);
        }
    }
}
