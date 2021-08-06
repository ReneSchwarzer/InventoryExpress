using InventoryExpress.WebControl;
using InventoryExpress.Model;
using WebExpress.UI.WebControl;
using WebExpress.Attribute;
using WebExpress.WebApp.WebResource;
using System.Linq;
using System.Collections.Generic;

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
            var list = null as ICollection<Location>;

            lock (ViewModel.Instance.Database)
            {
                list = ViewModel.Instance.Locations.OrderBy(x => x.Name).ToList();
            }

            foreach (var location in list)
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
