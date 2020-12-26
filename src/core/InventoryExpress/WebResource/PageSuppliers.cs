using InventoryExpress.WebControl;
using InventoryExpress.Model;
using WebExpress.UI.WebControl;
using WebExpress.Attribute;
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

            //ToolBar.Add(new ControlToolBarItemButton()
            //{
            //    Icon = new PropertyIcon(TypeIcon.Plus),
            //    Text = "Hinzufügen",
            //    Title = "Neu",
            //    Uri = Uri.Append("add"),
            //    TextColor = new PropertyColorText(TypeColorText.White)
            //},
            //new ControlToolBarItemButton()
            //{
            //    Icon = new PropertyIcon(TypeIcon.Print),
            //    Uri = Uri.Append("print"),
            //    Title = "Drucken",
            //    Size = new PropertySizeText(TypeSizeText.Default),
            //    TextColor = new PropertyColorText(TypeColorText.White)
            //});
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var grid = new ControlPanelGrid() { Fluid = TypePanelContainer.Fluid };

            foreach (var supplier in ViewModel.Instance.Suppliers)
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
