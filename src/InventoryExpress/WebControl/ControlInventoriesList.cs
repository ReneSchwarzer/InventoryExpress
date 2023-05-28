using InventoryExpress.Model;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Wql;
using WebExpress.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlInventoriesList : ControlPanelGrid
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ControlInventoriesList()
        {
            Fluid = TypePanelContainer.Fluid;
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Content.Clear();

            foreach (var inventory in ViewModel.GetInventories(new WqlStatement()).OrderBy(x => x.Name))
            {
                var card = new ControlCardInventory(inventory);

                Content.Add(card);
            }

            return base.Render(context);
        }
    }
}
