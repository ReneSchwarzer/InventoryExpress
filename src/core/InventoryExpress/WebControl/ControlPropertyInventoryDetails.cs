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
                Value = inventory?.Manufacturer?.Name
            }));

            Add(new ControlListItem(new ControlAttribute()
            {
                Name = "Standort:",
                Icon = new PropertyIcon(TypeIcon.Map),
                Value = inventory?.Location?.Name
            }));

            Add(new ControlListItem(new ControlAttribute()
            {
                Name = "Lieferant:",
                Icon = new PropertyIcon(TypeIcon.Truck),
                Value = inventory?.Supplier?.Name
            }));

            Add(new ControlListItem(new ControlAttribute()
            {
                Name = "Sachkonto:",
                Icon = new PropertyIcon(TypeIcon.At),
                Value = inventory?.LedgerAccount?.Name
            }));

            Add(new ControlListItem(new ControlAttribute()
            {
                Name = "Kostenstelle:",
                Icon = new PropertyIcon(TypeIcon.ShoppingBag),
                Value = inventory?.CostCenter?.Name
            }));

            Add(new ControlListItem(new ControlAttribute()
            {
                Name = "Zustand:",
                Icon = new PropertyIcon(TypeIcon.Star),
                Value = inventory?.Condition?.Name
            }));

            foreach (var v in inventory?.Attributes)
            {
                Add(new ControlListItem(new ControlAttribute()
                {
                    Name = v.Name + ":",
                    Icon = new PropertyIcon(TypeIcon.MapMarker),
                    Value = v.Value
                }));
            }

            Add(new ControlListItem(new ControlAttribute()
            {
                Name = "Anschaffungskosten:",
                Icon = new PropertyIcon(TypeIcon.EuroSign),
                Value = inventory?.CostValue.ToString() + " €"
            }));

            Add(new ControlListItem(new ControlAttribute()
            {
                Name = "Anschaffungsdatum:",
                Icon = new PropertyIcon(TypeIcon.CalendarPlus),
                Value = inventory?.PurchaseDate != null ? inventory?.PurchaseDate.Value.ToString("d", context.Culture) : string.Empty
            }));

            if (inventory.DerecognitionDate.HasValue)
            {
                Add(new ControlListItem(new ControlAttribute()
                {
                    Name = "Abgangsdatum:",
                    Icon = new PropertyIcon(TypeIcon.CalendarMinus),
                    Value = inventory?.DerecognitionDate != null ? inventory?.DerecognitionDate.Value.ToString("d", context.Culture) : string.Empty
                }));
            }

            return base.Render(context);
        }
    }
}
