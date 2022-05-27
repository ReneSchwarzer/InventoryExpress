using InventoryExpress.Model;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
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
            lock (ViewModel.Instance.Database)
            {
                var inventories = ViewModel.Instance.Inventories.ToList();
                var currency = ViewModel.Instance.Settings.FirstOrDefault()?.Currency;

                CountAttribute.Value = inventories.Count().ToString();
                CurrencyAttribute.Value = $"{inventories.Sum(x => x.CostValue).ToString(context.Culture)} {(string.IsNullOrWhiteSpace(currency) ? "€" : currency)}";
            }

            return base.Render(context);
        }
    }
}
