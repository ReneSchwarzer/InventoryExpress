using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebApp.Wql;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [WebExSection(Section.PropertyPrimary)]
    [WebExModule<Module>]
    [WebExContext("inventories")]
    public sealed class FragmentPropertyInventoriesDetails : FragmentControlList
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
        /// Constructor
        /// </summary>
        public FragmentPropertyInventoriesDetails()
        {
            Layout = TypeLayoutList.Flush;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);

            Add(new ControlListItem(CountAttribute));
            Add(new ControlListItem(CurrencyAttribute));
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">The page where the component is active.</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
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
