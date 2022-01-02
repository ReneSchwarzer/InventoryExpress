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
        public override void Initialization(IComponentContext context)
        {
            base.Initialization(context);
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

                Text = I18N(context.Culture, "inventoryexpress:inventoryexpress.inventory.attachment.function") + $" ({ count })";
                Uri = context.Uri.Append("attachments");
            }

            return base.Render(context);
        }
    }
}
