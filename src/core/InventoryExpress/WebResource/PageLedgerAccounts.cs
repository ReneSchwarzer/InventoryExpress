using InventoryExpress.WebControl;
using InventoryExpress.Model;
using WebExpress.UI.WebControl;
using WebExpress.Attribute;
using WebExpress.WebApp.WebResource;

namespace InventoryExpress.WebResource
{
    [ID("LedgerAccount")]
    [Title("inventoryexpress.ledgeraccounts.label")]
    [Segment("ledgeraccounts", "inventoryexpress.ledgeraccounts.label")]
    [Path("/")]
    [Module("InventoryExpress")]
    [Context("general")]
    public sealed class PageLedgerAccounts : PageTemplateWebApp, IPageLedgerAccount
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageLedgerAccounts()
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

            foreach (var gLAccount in ViewModel.Instance.LedgerAccounts)
            {
                var card = new ControlCardLedgerAccount()
                {
                    GLAccount = gLAccount
                };

                grid.Content.Add(card);
            }

            Content.Content.Add(grid);
        }
    }
}
