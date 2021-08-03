using InventoryExpress.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Components;

namespace InventoryExpress.WebControl
{
    [Section(Section.PropertyPrimary)]
    [Application("InventoryExpress")]
    [Context("inventorydetails")]
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
            lock (ViewModel.Instance.Database)
            {
                var id = context.Page.GetParamValue("InventoryID");
                var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid.Equals(id)).FirstOrDefault();
                var currency = ViewModel.Instance.Settings.FirstOrDefault()?.Currency;

                Add(new ControlListItem
                (
                    new ControlAttribute()
                    {
                        Name = context.I18N("inventoryexpress.inventory.inventorynumber.label"),
                        Icon = new PropertyIcon(TypeIcon.Key),
                        TextColor = new PropertyColorText(TypeColorText.Secondary)
                    },
                    new ControlLink()
                    {
                        Text = id,
                        Uri = context.Uri.Root.Append(id),
                    }
                ));

                Add(new ControlListItem(new ControlAttribute()
                {
                    Name = context.I18N("inventoryexpress.inventory.manufacturers.label"),
                    Icon = new PropertyIcon(TypeIcon.Industry),
                    Value = ViewModel.Instance.Manufacturers.Where(x => x.Id == inventory.ManufacturerId).FirstOrDefault()?.Name,
                    TextColor = new PropertyColorText(TypeColorText.Secondary)
                }));

                Add(new ControlListItem(new ControlAttribute()
                {
                    Name = context.I18N("inventoryexpress.inventory.location.label"),
                    Icon = new PropertyIcon(TypeIcon.Map),
                    Value = ViewModel.Instance.Locations.Where(x => x.Id == inventory.LocationId).FirstOrDefault()?.Name,
                    TextColor = new PropertyColorText(TypeColorText.Secondary)
                }));

                Add(new ControlListItem(new ControlAttribute()
                {
                    Name = context.I18N("inventoryexpress.inventory.supplier.label"),
                    Icon = new PropertyIcon(TypeIcon.Truck),
                    Value = ViewModel.Instance.Suppliers.Where(x => x.Id == inventory.SupplierId).FirstOrDefault()?.Name,
                    TextColor = new PropertyColorText(TypeColorText.Secondary)
                }));

                Add(new ControlListItem(new ControlAttribute()
                {
                    Name = context.I18N("inventoryexpress.inventory.ledgeraccount.label"),
                    Icon = new PropertyIcon(TypeIcon.At),
                    Value = ViewModel.Instance.LedgerAccounts.Where(x => x.Id == inventory.LedgerAccountId).FirstOrDefault()?.Name,
                    TextColor = new PropertyColorText(TypeColorText.Secondary)
                }));

                Add(new ControlListItem(new ControlAttribute()
                {
                    Name = context.I18N("inventoryexpress.inventory.costcenter.label"),
                    Icon = new PropertyIcon(TypeIcon.ShoppingBag),
                    Value = ViewModel.Instance.CostCenters.Where(x => x.Id == inventory.CostCenterId).FirstOrDefault()?.Name,
                    TextColor = new PropertyColorText(TypeColorText.Secondary)
                }));

                Add(new ControlListItem(new ControlAttribute()
                {
                    Name = context.I18N("inventoryexpress.inventory.condition.label"),
                    Icon = new PropertyIcon(TypeIcon.Star),
                    Value = ViewModel.Instance.Conditions.Where(x => x.Id == inventory.ConditionId)?.FirstOrDefault()?.Name,
                    TextColor = new PropertyColorText(TypeColorText.Secondary)
                }));

                var inventoryAttributes = ViewModel.Instance.InventoryAttributes.Where(x => x.InventoryId == inventory.Id).ToList();

                foreach (var v in inventoryAttributes)
                {
                    var att = ViewModel.Instance.Attributes.Where(x => x.Id == v.AttributeId).FirstOrDefault();

                    Add(new ControlListItem(new ControlAttribute()
                    {
                        Name = att?.Name + ":",
                        Icon = new PropertyIcon(TypeIcon.Cube),
                        Value = v.Value,
                        TextColor = new PropertyColorText(TypeColorText.Secondary)
                    }));
                }

                Add(new ControlListItem(new ControlAttribute()
                {
                    Name = context.I18N("inventoryexpress.inventory.costvalue.label"),
                    Icon = new PropertyIcon(TypeIcon.EuroSign),
                    Value = $"{inventory?.CostValue.ToString(context.Culture)} { (string.IsNullOrWhiteSpace(currency) ? "€" : currency) }",
                    TextColor = new PropertyColorText(TypeColorText.Secondary)
                }));

                Add(new ControlListItem(new ControlAttribute()
                {
                    Name = context.I18N("inventoryexpress.inventory.purchasedate.label"),
                    Icon = new PropertyIcon(TypeIcon.CalendarPlus),
                    Value = inventory?.PurchaseDate != null ? inventory?.PurchaseDate.Value.ToString("d", context.Culture) : string.Empty,
                    TextColor = new PropertyColorText(TypeColorText.Secondary)
                }));

                if (inventory.DerecognitionDate.HasValue)
                {
                    Add(new ControlListItem(new ControlAttribute()
                    {
                        Name = context.I18N("inventoryexpress.inventory.derecognitiondate.label"),
                        Icon = new PropertyIcon(TypeIcon.CalendarMinus),
                        Value = inventory?.DerecognitionDate != null ? inventory?.DerecognitionDate.Value.ToString("d", context.Culture) : string.Empty,
                        TextColor = new PropertyColorText(TypeColorText.Secondary)
                    }));
                }

                return base.Render(context);
            }
        }
    }
}
