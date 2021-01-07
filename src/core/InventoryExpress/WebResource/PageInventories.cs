using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebResource
{
    [ID("Home")]
    [Title("inventoryexpress.inventories.label")]
    [Segment("", "inventoryexpress.inventories.label")]
    [Path("")]
    [Module("InventoryExpress")]
    [Context("general")]
    [Context("inventories")]
    public sealed class PageInventories : PageTemplateWebApp, IPageInventory
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventories()
        {
            Favicons.Add(new Favicon("/assets/img/Favicon.png", TypeFavicon.PNG));
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
            var list = null as ICollection<Inventory>;

            lock (ViewModel.Instance.Database)
            {
                list = ViewModel.Instance.Inventories.OrderBy(x => x.Name).ToList();
            }

            foreach (var inventory in list)
            {
                var card = new ControlCardInventory()
                {
                    Inventory = inventory
                };

                grid.Content.Add(card);
            }

            Content.Primary.Add(grid);
        }
    }
}
