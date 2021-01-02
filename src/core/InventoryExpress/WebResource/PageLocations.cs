using InventoryExpress.WebControl;
using InventoryExpress.Model;
using WebExpress.UI.WebControl;
using WebExpress.Attribute;
using WebExpress.WebApp.WebResource;
using System.Linq;

namespace InventoryExpress.WebResource
{
    [ID("Location")]
    [Title("inventoryexpress.locations.label")]
    [Segment("locations", "inventoryexpress.locations.label")]
    [Path("/")]
    [Module("InventoryExpress")]
    [Context("general")]
    public sealed class PageLocations : PageTemplateWebApp, IPageLocation
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageLocations()
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

            foreach (var location in ViewModel.Instance.Locations.OrderBy(x => x.Name))
            {
                var card = new ControlCardLocation()
                {
                    Location = location
                };

                grid.Content.Add(card);
            }

            Content.Primary.Add(grid);
        }
    }
}
