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
        /// Konstruktor
        /// </summary>
        public ControlInventoriesList()
        {
            Fluid = TypePanelContainer.Fluid;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
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
