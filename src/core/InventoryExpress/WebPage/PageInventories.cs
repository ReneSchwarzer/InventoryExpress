using InventoryExpress.WebControl;
using InventoryExpress.WebSession;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [Id("Home")]
    [Title("inventoryexpress:inventoryexpress.inventories.label")]
    [Segment("", "inventoryexpress:inventoryexpress.inventories.label")]
    [ContextPath("")]
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
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var property = context.Request.Session.GetOrCreateProperty<SessionPropertyToggleStatus>();

            if (property.ViewList)
            {
                // Listenansicht
                context.VisualTree.Content.Primary.Add(new ControlInventoriesList());
            }
            else
            {
                // Tabelarische Ansicht
                context.VisualTree.Content.Primary.Add(new ControlInventoriesTable());
            }
        }
    }
}
