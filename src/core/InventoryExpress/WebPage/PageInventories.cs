using InventoryExpress.Session;
using InventoryExpress.WebControl;
using WebExpress.Attribute;
using WebExpress.WebApp.WebPage;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("Home")]
    [Title("inventoryexpress:inventoryexpress.inventories.label")]
    [Segment("", "inventoryexpress:inventoryexpress.inventories.label")]
    [Path("")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("inventories")]
    public sealed class PageInventories : PageWebApp, IPageInventory
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventories()
        {

        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var property = context.Request.Session.GetOrCreateProperty<SessionPropertyToggleStatus>();

            if (property.ViewList)
            {
                // Listenansicht
                context.VisualTree.Content.Primary.Add(new ControlListInventories());
            }
            else
            {
                // Tabelarische Ansicht
                context.VisualTree.Content.Primary.Add(new ControlTableInventories());
            }
        }
    }
}
