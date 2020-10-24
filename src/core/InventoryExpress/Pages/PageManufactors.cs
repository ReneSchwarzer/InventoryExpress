using InventoryExpress.Controls;
using WebExpress.UI.Controls;

namespace InventoryExpress.Pages
{
    public class PageManufactors : PageBase, IManufactor
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageManufactors()
            : base("Hersteller")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();

            ToolBar.Add(new ControlToolBarItemButton(this)
            {
                Icon = new PropertyIcon(TypeIcon.Plus),
                Text = "Hinzufügen",
                Title = "Neu",
                Uri = Uri.Append("add"),
                TextColor = new PropertyColorText(TypeColorText.White)
            },
            new ControlToolBarItemButton(this)
            {
                Icon = new PropertyIcon(TypeIcon.Print),
                Uri = Uri.Append("print"),
                Title = "Drucken",
                Size = new PropertySizeText(TypeSizeText.Default),
                TextColor = new PropertyColorText(TypeColorText.White)
            });
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var grid = new ControlGrid(this) { Fluid = false };
            int i = 0;

            foreach (var manufactor in DB.DB.Instance.Manufacturers)
            {
                var card = new ControlManufactorsCard(this)
                {
                    Manufactur = manufactor
                };

                grid.Add(i++, card);
            }

            Main.Content.Add(grid);
        }
    }
}
