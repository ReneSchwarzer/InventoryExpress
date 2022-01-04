using InventoryExpress.Model;
using System.Linq;
using WebExpress.WebAttribute;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebComponent
{
    [Section(Section.Metadata)]
    [Module("inventoryexpress")]
    [Context("inventorydetails")]
    public sealed class ComponentHeadlineInventoryMetadata : ComponentControlText
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentHeadlineInventoryMetadata()
        {
            //Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
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
                var id = context.Request.GetParameter("InventoryID")?.Value;
                var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid.Equals(id)).FirstOrDefault();

                Text = string.Format(I18N(context.Culture, "inventoryexpress:inventoryexpress.inventory.metadata.created"), inventory.Created.ToString("d", context.Culture));

                if (inventory.Created != inventory.Updated)
                {
                    Text += " ";
                    Text += string.Format(I18N(context.Culture, "inventoryexpress:inventoryexpress.inventory.metadata.lastchange"), inventory.Updated.ToString("d", context.Culture));
                }
            }

            return base.Render(context);
        }
    }
}
