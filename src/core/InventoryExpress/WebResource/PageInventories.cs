using InventoryExpress.WebControl;
using InventoryExpress.Model;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;
using WebExpress.Attribute;
using WebExpress.Html;

namespace InventoryExpress.WebResource
{
    [ID("Home")]
    [Title("inventoryexpress.inventories.label")]
    [Segment("", "inventoryexpress.inventories.label")]
    [Path("")]
    [Module("InventoryExpress")]
    [Context("general")]
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

            //ToolBar.Add(new ControlToolBarItemButton()
            //{
            //    Icon = new PropertyIcon(TypeIcon.Plus),
            //    Text = "Hinzufügen",
            //    Title = "Neu",
            //    Uri = Uri.Root.Append("add"),
            //    TextColor = new PropertyColorText(TypeColorText.White)
            //},
            //new ControlToolBarItemButton()
            //{
            //    Icon = new PropertyIcon(TypeIcon.Print),
            //    Uri = Uri.Root.Append("print"),
            //    Title = "Drucken",
            //    Size = new PropertySizeText(TypeSizeText.Default),
            //    TextColor = new PropertyColorText(TypeColorText.White)
            //},
            //new ControlToolBarItemButton()
            //{
            //    Icon = new PropertyIcon(TypeIcon.Download),
            //    Uri = Uri.Root.Append("export"),
            //    Title = "Exportieren",
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

            foreach (var inventory in ViewModel.Instance.Inventories)
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
