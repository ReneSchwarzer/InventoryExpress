using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebScope;
using WebExpress.WebApp.Wql;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [Title("inventoryexpress:inventoryexpress.ledgeraccounts.label")]
    [Segment("ledgeraccounts", "inventoryexpress:inventoryexpress.ledgeraccounts.label")]
    [ContextPath("/")]
    [Module<Module>]
    [Scope<ScopeGeneral>]
    public sealed class PageLedgerAccounts : PageWebApp, IPageLedgerAccount
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageLedgerAccounts()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var visualTree = context.VisualTree;

            var grid = new ControlPanelGrid() { Fluid = TypePanelContainer.Fluid };
            var list = ViewModel.GetLedgerAccounts(new WqlStatement()).OrderBy(x => x.Name);

            foreach (var ledgerAccount in list)
            {
                var card = new ControlCardLedgerAccount()
                {
                    LedgerAccount = ledgerAccount
                };

                grid.Content.Add(card);
            }

            visualTree.Content.Primary.Add(grid);
        }
    }
}
