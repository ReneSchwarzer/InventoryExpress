using InventoryExpress.WebControl;
using InventoryExpress.WebSession;
using WebExpress.WebApp.WebPage;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebResource;
using WebExpress.WebCore.WebScope;

namespace InventoryExpress.WebPage
{
    [Title("inventoryexpress:inventoryexpress.inventories.label")]
    [Segment(null, "inventoryexpress:inventoryexpress.inventories.label")]
    [ContextPath(null)]
    [Module<Module>()]
    public sealed class PageInventories : PageWebApp, IPageInventory, IScope
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageInventories()
        {

        }

        /// <summary>
        /// Initialization
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
