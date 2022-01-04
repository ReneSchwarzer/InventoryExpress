using InventoryExpress.Model;
using System.Linq;
using WebExpress.WebAttribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebPage;

namespace InventoryExpress.WebComponent
{
    [Section(Section.MorePrimary)]
    [Module("inventoryexpress")]
    [Context("inventorydetails")]
    public sealed class ComponentMoreJournal : ComponentControlDropdownItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentMoreJournal()
        {
            Icon = new PropertyIcon(TypeIcon.Book);
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

                Text = InternationalizationManager.I18N(context.Culture, "inventoryexpress:inventoryexpress.inventory.journal.function");
                Uri = context.Uri.Append("journal");
            }

            return base.Render(context);
        }
    }
}
