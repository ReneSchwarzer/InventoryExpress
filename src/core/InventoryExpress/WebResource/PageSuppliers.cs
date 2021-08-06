using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebResource
{
    [ID("Supplier")]
    [Title("inventoryexpress.suppliers.label")]
    [Segment("suppliers", "inventoryexpress.suppliers.label")]
    [Path("/")]
    [Module("InventoryExpress")]
    [Context("general")]
    public sealed class PageSuppliers : PageTemplateWebApp, IPageSupplier
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
            var list = null as ICollection<Supplier>;

            lock (ViewModel.Instance.Database)
            {
                list = ViewModel.Instance.Suppliers.OrderBy(x => x.Name).ToList();
            }

            foreach (var supplier in list)
            {
                var card = new ControlCardSupplier()
                {
                    Supplier = supplier
                };

                grid.Content.Add(card);
            }

            Content.Primary.Add(grid);
        }
    }
}
