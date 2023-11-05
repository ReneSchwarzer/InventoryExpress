using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System.Linq;
using WebExpress.WebUI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebScope;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [Title("inventoryexpress:inventoryexpress.locations.label")]
    [Segment("locations", "inventoryexpress:inventoryexpress.locations.label")]
    [ContextPath("/")]
    [Module<Module>]
    [Scope<ScopeGeneral>]
    public sealed class PageLocations : PageWebApp, IPageLocation
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageLocations()
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

            var grid = new ControlPanelGrid() { Fluid = TypePanelContainer.Fluid };
            var list = ViewModel.GetLocations().OrderBy(x => x.Name);

            foreach (var location in list)
            {
                var card = new ControlCardLocation()
                {
                    Location = location
                };

                grid.Content.Add(card);
            }

            context.VisualTree.Content.Primary.Add(grid);
        }
    }
}
