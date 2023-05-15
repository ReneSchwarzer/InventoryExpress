using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.Wql;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [WebExTitle("inventoryexpress:inventoryexpress.suppliers.label")]
    [WebExSegment("suppliers", "inventoryexpress:inventoryexpress.suppliers.label")]
    [WebExContextPath("/")]
    [WebExModule(typeof(Module))]
    [WebExContext("general")]
    public sealed class PageSuppliers : PageWebApp, IPageSupplier
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSuppliers()
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

            var visualTree = context.VisualTree;

            var grid = new ControlPanelGrid() { Fluid = TypePanelContainer.Fluid };
            var list = ViewModel.GetSuppliers(new WqlStatement()).OrderBy(x => x.Name);

            foreach (var supplier in list)
            {
                var card = new ControlCardSupplier()
                {
                    Supplier = supplier
                };

                grid.Content.Add(card);
            }

            visualTree.Content.Primary.Add(grid);
        }
    }
}
