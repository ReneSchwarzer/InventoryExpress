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
    [Id("CostCenter")]
    [Title("inventoryexpress:inventoryexpress.costcenters.label")]
    [Segment("costcenters", "inventoryexpress:inventoryexpress.costcenters.label")]
    [ContextPath("/")]
    [Module("inventoryexpress")]
    [Context("general")]
    public sealed class PageCostCenter : PageWebApp, IPageCostCenter
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageCostCenter()
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
            var list = ViewModel.GetCostCenters(new WqlStatement()).OrderBy(x => x.Name);

            foreach (var costcenter in list)
            {
                var card = new ControlCardCostCenter(costcenter);

                grid.Content.Add(card);
            }

            visualTree.Content.Primary.Add(grid);
        }
    }
}
