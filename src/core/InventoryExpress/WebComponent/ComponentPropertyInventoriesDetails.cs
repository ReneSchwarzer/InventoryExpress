using InventoryExpress.Model;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebApp.Wql;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebComponent
{
    [Section(Section.PropertyPrimary)]
    [Module("inventoryexpress")]
    [Context("inventories")]
    public sealed class ComponentPropertyInventoriesDetails : ComponentControlList
    {
        /// <summary>
        /// Die Anzahl der Inventargegenstände
        /// </summary>
        private ControlAttribute CountAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Name = "inventoryexpress:inventoryexpress.inventory.details.count.label"
        };

        /// <summary>
        /// Die Währung, indem die Inventargegentände monetarisiert werden
        /// </summary>
        private ControlAttribute CurrencyAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.EuroSign),
            Name = "inventoryexpress:inventoryexpress.inventory.details.totalacquisitioncosts.label"
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentPropertyInventoriesDetails()
        {
            Layout = TypeLayoutList.Flush;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);

            Add(new ControlListItem(CountAttribute));
            Add(new ControlListItem(CurrencyAttribute));
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IComponentContext context, IPage page)
        {
            base.Initialization(context, page);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var count = ViewModel.CountInventories(new WqlStatement());
            var capitalCosts = ViewModel.GetInventoriesCapitalCosts(new WqlStatement());
            var currency = ViewModel.GetSettings()?.Currency;

            CountAttribute.Value = count.ToString();
            CurrencyAttribute.Value = $"{capitalCosts.ToString(context.Culture)} {(string.IsNullOrWhiteSpace(currency) ? "€" : currency)}";

            return base.Render(context);
        }
    }
}
