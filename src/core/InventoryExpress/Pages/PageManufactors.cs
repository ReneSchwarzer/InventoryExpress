using InventoryExpress.Controls;
using InventoryExpress.Model;
using WebExpress.Internationalization;
using WebExpress.UI.Controls;

namespace InventoryExpress.Pages
{
    public class PageManufactors : PageBase, IPageManufactor
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageManufactors()
            : base("inventoryexpress.manufactors.label")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();

            Toolbar.Add(new ControlToolBarItemButton()
            {
                Icon = new PropertyIcon(TypeIcon.Plus),
                Text = this.I18N("inventoryexpress.add.label"),
                Title = this.I18N("inventoryexpress.add.discription"),
                Uri = Uri.Append("add"),
                Size = new PropertySizeText(TypeSizeText.Small)
            },
            new ControlToolBarItemButton()
            {
                Icon = new PropertyIcon(TypeIcon.Print),
                Uri = Uri.Append("print"),
                Title = this.I18N("inventoryexpress.print.discription"),
                Size = new PropertySizeText(TypeSizeText.Small)
            });
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var grid = new ControlPanelGrid() { Fluid = TypePanelContainer.Fluid };
            int i = 0;

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
