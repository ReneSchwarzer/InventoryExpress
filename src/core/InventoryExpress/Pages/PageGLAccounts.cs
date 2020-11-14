using InventoryExpress.Controls;
using InventoryExpress.Model;
using WebExpress.UI.Controls;

namespace InventoryExpress.Pages
{
    public class PageGLAccounts : PageBase, IPageGLAccount
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageGLAccounts()
            : base("Sachkonten")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();

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

            var grid = new ControlPanelGrid() { Fluid =  TypePanelContainer.Fluid };
            int i = 0;

            foreach (var gLAccount in ViewModel.Instance.GLAccounts)
            {
                var card = new ControlCardGLAccount()
                {
                    GLAccount = gLAccount
                };

                grid.Content.Add(card);
            }

            Content.Content.Add(grid);
        }
    }
}
