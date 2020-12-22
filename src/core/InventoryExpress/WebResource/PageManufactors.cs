using InventoryExpress.WebControl;
using InventoryExpress.Model;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.Attribute;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebResource
{
    [ID("Manufactor")]
    [Title("inventoryexpress.manufactors.label")]
    [Segment("manufactors", "inventoryexpress.manufactors.label")]
    [Path("/")]
    [Module("InventoryExpress")]
    [Context("general")]
    public sealed class PageManufactors : PageTemplateWebApp, IPageManufactor
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageManufactors()
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

            foreach (var manufactor in ViewModel.Instance.Manufacturers)
            {
                var card = new ControlCardManufactor()
                {
                    Manufactur = manufactor
                };

                grid.Content.Add(card);
            }

            Content.Content.Add(grid);
        }
    }
}
