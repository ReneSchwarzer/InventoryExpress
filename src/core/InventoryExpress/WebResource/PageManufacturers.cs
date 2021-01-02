using InventoryExpress.WebControl;
using InventoryExpress.Model;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.Attribute;
using WebExpress.WebApp.WebResource;
using System.Linq;

namespace InventoryExpress.WebResource
{
    [ID("Manufacturer")]
    [Title("inventoryexpress.manufacturers.label")]
    [Segment("manufacturers", "inventoryexpress.manufacturers.label")]
    [Path("/")]
    [Module("InventoryExpress")]
    [Context("general")]
    public sealed class PageManufacturers : PageTemplateWebApp, IPageManufacturer
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageManufacturers()
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

            foreach (var manufacturer in ViewModel.Instance.Manufacturers.OrderBy(x => x.Name))
            {
                var card = new ControlCardManufacturer()
                {
                    Manufactur = manufacturer
                };

                grid.Content.Add(card);
            }

            Content.Primary.Add(grid);
        }
    }
}
