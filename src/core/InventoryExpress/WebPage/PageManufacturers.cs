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
    [ID("Manufacturer")]
    [Title("inventoryexpress:inventoryexpress.manufacturers.label")]
    [Segment("manufacturers", "inventoryexpress:inventoryexpress.manufacturers.label")]
    [Path("/")]
    [Module("inventoryexpress")]
    [Context("general")]
    public sealed class PageManufacturers : PageWebApp, IPageManufacturer
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

            var visualTree = context.VisualTree;

            var grid = new ControlPanelGrid() { Fluid = TypePanelContainer.Fluid };
            var list = null as ICollection<Manufacturer>;

            lock (ViewModel.Instance.Database)
            {
                list = ViewModel.Instance.Manufacturers.OrderBy(x => x.Name).ToList();
            }

            foreach (var manufacturer in list)
            {
                var card = new ControlCardManufacturer()
                {
                    Manufactur = manufacturer
                };

                grid.Content.Add(card);
            }

            visualTree.Content.Primary.Add(grid);
        }
    }
}
