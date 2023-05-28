using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Wql;
using WebExpress.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlManufactorsList : ControlPanelGrid
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ControlManufactorsList()
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

            foreach (var manufacturer in ViewModel.GetManufacturers(new WqlStatement()))
            {
                var card = new ControlCardManufacturer(manufacturer);

                Content.Add(card);
            }

            return base.Render(context);
        }
    }
}
