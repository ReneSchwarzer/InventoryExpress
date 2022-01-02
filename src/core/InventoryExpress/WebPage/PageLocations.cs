using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.WebControl;
using System.Collections.Generic;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("Location")]
    [Title("inventoryexpress:inventoryexpress.locations.label")]
    [Segment("locations", "inventoryexpress:inventoryexpress.locations.label")]
    [Path("/")]
    [Module("inventoryexpress")]
    [Context("general")]
    public sealed class PageLocations : PageWebApp, IPageLocation
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
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

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

            context.VisualTree.Content.Primary.Add(grid);
        }
    }
}
