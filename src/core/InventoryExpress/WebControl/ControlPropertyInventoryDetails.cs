using InventoryExpress.Model;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Components;

namespace InventoryExpress.WebControl
{
    [Section(Section.PropertyPrimary)]
    [Application("InventoryExpress")]
    [Context("details")]
    public sealed class ControlPropertyInventoryDetails : ControlList, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlPropertyInventoryDetails()
        {
            Layout = TypeLayoutList.Flush;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var id = context.Page.GetParamValue("InventoryID");
            var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid.Equals(id)).FirstOrDefault();

            Add(new ControlListItem(new ControlAttribute()
            {
                Name = "Hersteller:",
                Icon = new PropertyIcon(TypeIcon.Industry),
                Value = inventory?.Manufacturer?.Name,
                TextColor = new PropertyColorText(TypeColorText.Secondary)
            }));

            Add(new ControlListItem(new ControlAttribute()
            {
                Name = "Standort:",
                Icon = new PropertyIcon(TypeIcon.Map),
                Value = inventory?.Location?.Name,
                TextColor = new PropertyColorText(TypeColorText.Secondary)
            }));

            Add(new ControlListItem(new ControlAttribute()
            {
                Name = "Lieferant:",
                Icon = new PropertyIcon(TypeIcon.Truck),
                Value = inventory?.Supplier?.Name,
                TextColor = new PropertyColorText(TypeColorText.Secondary)
            }));

            Add(new ControlListItem(new ControlAttribute()
            {
                Name = "Sachkonto:",
                Icon = new PropertyIcon(TypeIcon.At),
                Value = inventory?.LedgerAccount?.Name,
                TextColor = new PropertyColorText(TypeColorText.Secondary)
            }));

            Add(new ControlListItem(new ControlAttribute()
            {
                Name = "Kostenstelle:",
                Icon = new PropertyIcon(TypeIcon.ShoppingBag),
                Value = inventory?.CostCenter?.Name,
                TextColor = new PropertyColorText(TypeColorText.Secondary)
            }));

            Add(new ControlListItem(new ControlAttribute()
            {
                Name = "Zustand:",
                Icon = new PropertyIcon(TypeIcon.Star),
                Value = inventory?.Condition?.Name,
                TextColor = new PropertyColorText(TypeColorText.Secondary)
            }));

            foreach (var v in inventory?.InventoryAttributes)
            {
                Add(new ControlListItem(new ControlAttribute()
                {
                    Name = "???:",
                    Icon = new PropertyIcon(TypeIcon.MapMarker),
                    Value = v.Value,
                    TextColor = new PropertyColorText(TypeColorText.Secondary)
                }));
            }

            Add(new ControlListItem(new ControlAttribute()
            {
                Name = "Anschaffungskosten:",
                Icon = new PropertyIcon(TypeIcon.EuroSign),
                Value = inventory?.CostValue.ToString(context.Culture) + " €",
                TextColor = new PropertyColorText(TypeColorText.Secondary)
            }));

            Add(new ControlListItem(new ControlAttribute()
            {
                Name = "Anschaffungsdatum:",
                Icon = new PropertyIcon(TypeIcon.CalendarPlus),
                Value = inventory?.PurchaseDate != null ? inventory?.PurchaseDate.Value.ToString("d", context.Culture) : string.Empty,
                TextColor = new PropertyColorText(TypeColorText.Secondary)
            }));

            if (inventory.DerecognitionDate.HasValue)
            {
                Add(new ControlListItem(new ControlAttribute()
                {
                    Name = "Abgangsdatum:",
                    Icon = new PropertyIcon(TypeIcon.CalendarMinus),
                    Value = inventory?.DerecognitionDate != null ? inventory?.DerecognitionDate.Value.ToString("d", context.Culture) : string.Empty,
                    TextColor = new PropertyColorText(TypeColorText.Secondary)
                }));
            }

            return base.Render(context);
        }
    }
}
