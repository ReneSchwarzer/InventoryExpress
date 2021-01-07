using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.UI.WebControl;
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
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var grid = new ControlPanelGrid() { Fluid = TypePanelContainer.Fluid };
            var list = null as ICollection<LedgerAccount>;

            lock (ViewModel.Instance.Database)
            {
                list = ViewModel.Instance.LedgerAccounts.OrderBy(x => x.Name).ToList();
            }

            foreach (var gLAccount in list)
            {
                var card = new ControlCardLedgerAccount()
                {
                    LedgerAccount = gLAccount
                };

                grid.Content.Add(card);
            }

            Content.Primary.Add(grid);
        }
    }
}
