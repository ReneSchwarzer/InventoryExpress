using InventoryExpress.WebControl;
using InventoryExpress.WebSession;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;
using WebExpress.WebScope;

namespace InventoryExpress.WebPage
{
    [Title("inventoryexpress:inventoryexpress.manufacturers.label")]
    [Segment("manufacturers", "inventoryexpress:inventoryexpress.manufacturers.label")]
    [ContextPath("/")]
    [Module<Module>]
    public sealed class PageManufacturers : PageWebApp, IPageManufacturer, IScope
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageManufacturers()
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
                context.VisualTree.Content.Primary.Add(new ControlManufactorsList());
            }
            else
            {
                // Tabelarische Ansicht
                context.VisualTree.Content.Primary.Add(new ControlManufactorsTable());
            }
        }
    }
}
