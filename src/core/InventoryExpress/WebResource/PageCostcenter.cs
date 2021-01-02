using InventoryExpress.WebControl;
using InventoryExpress.Model;
using WebExpress.UI.WebControl;
using WebExpress.Attribute;
using WebExpress.WebApp.WebResource;
using System.Linq;

namespace InventoryExpress.WebResource
{
    [ID("CostCenter")]
    [Title("inventoryexpress.costcenters.label")]
    [Segment("costcenters", "inventoryexpress.costcenters.label")]
    [Path("/")]
    [Module("InventoryExpress")]
    [Context("general")]
    public sealed class PageCostCenter : PageTemplateWebApp, IPageCostCenter
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
        public override void Initialization()
        {
            base.Initialization();
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var grid = new ControlPanelGrid() { Fluid = TypePanelContainer.Fluid };

            foreach (var costcenter in ViewModel.Instance.CostCenters.OrderBy(x => x.Name))
            {
                var card = new ControlCardCostCenter()
                {
                    CostCenter = costcenter
                };

                grid.Content.Add(card);
            }

            Content.Primary.Add(grid);
        }
    }
}
