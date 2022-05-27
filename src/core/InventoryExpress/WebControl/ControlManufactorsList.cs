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
        /// Konstruktor
        /// </summary>
        public ControlManufactorsList()
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

            foreach (var manufacturer in ViewModel.GetManufacturers(new WqlStatement()))
            {
                var card = new ControlCardManufacturer(manufacturer);

                Content.Add(card);
            }

            return base.Render(context);
        }
    }
}
