using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.WebControl;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("LedgerAccount")]
    [Title("inventoryexpress:inventoryexpress.ledgeraccounts.label")]
    [Segment("ledgeraccounts", "inventoryexpress:inventoryexpress.ledgeraccounts.label")]
    [Path("/")]
    [Module("inventoryexpress")]
    [Context("general")]
    public sealed class PageLedgerAccounts : PageWebApp, IPageLedgerAccount
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
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var visualTree = context.VisualTree;

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

            visualTree.Content.Primary.Add(grid);
        }
    }
}
