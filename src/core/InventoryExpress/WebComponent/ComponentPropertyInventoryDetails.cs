using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebComponent
{
    [Section(Section.PropertyPrimary)]
    [Module("inventoryexpress")]
    [Context("inventorydetails")]
    public sealed class ComponentPropertyInventoryDetails : ComponentControlList
    {
        /// <summary>
        /// Die Inventarnummer
        /// </summary>
        private ControlAttribute InventoryNumberAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.Key),
            Name = "inventoryexpress:inventoryexpress.inventory.inventorynumber.label"
        };

        /// <summary>
        /// Die Link aud das Inventar
        /// </summary>
        private ControlLink InventoryNumberLink { get; } = new ControlLink();

        /// <summary>
        /// Der Hersteller
        /// </summary>
        private ControlAttribute ManufacturerAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.Industry),
            Name = "inventoryexpress:inventoryexpress.inventory.manufacturers.label"
        };

        /// <summary>
        /// Der Standort
        /// </summary>
        private ControlAttribute LocationAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.Map),
            Name = "inventoryexpress:inventoryexpress.inventory.location.label"
        };

        /// <summary>
        /// Der Lieferant
        /// </summary>
        private ControlAttribute SupplierAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.Truck),
            Name = "inventoryexpress:inventoryexpress.inventory.supplier.label"
        };

        /// <summary>
        /// Das Sachkonto
        /// </summary>
        private ControlAttribute LedgeraccountAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.At),
            Name = "inventoryexpress:inventoryexpress.inventory.ledgeraccount.label"
        };

        /// <summary>
        /// Die Kostenstelle
        /// </summary>
        private ControlAttribute CostcenterAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.ShoppingBag),
            Name = "inventoryexpress:inventoryexpress.inventory.costcenter.label"
        };

        /// <summary>
        /// Der Zustand
        /// </summary>
        private ControlAttribute ConditionAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.Star),
            Name = "inventoryexpress:inventoryexpress.inventory.condition.label"
        };

        /// <summary>
        /// Die Attribute
        /// </summary>
        private ControlListItem AttributesListItem { get; } = new ControlListItem();

        /// <summary>
        /// Der Anschaffungswert
        /// </summary>
        private ControlAttribute CostValueAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.EuroSign),
            Name = "inventoryexpress:inventoryexpress.inventory.costvalue.label"
        };

        /// <summary>
        /// Das Anschaffungsdatum
        /// </summary>
        private ControlAttribute PurchaseDateAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.CalendarPlus),
            Name = "inventoryexpress:inventoryexpress.inventory.purchasedate.label"
        };

        /// <summary>
        /// Das Abgangsdatum
        /// </summary>
        private ControlListItem DrecognitionDateListItem { get; } = new ControlListItem();

        /// <summary>
        /// Das Abgangsdatum
        /// </summary>
        private ControlAttribute DrecognitionDateAttribute { get; } = new ControlAttribute()
        {
            TextColor = new PropertyColorText(TypeColorText.Secondary),
            Icon = new PropertyIcon(TypeIcon.CalendarMinus),
            Name = "inventoryexpress:inventoryexpress.inventory.derecognitiondate.label"
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentPropertyInventoryDetails()
        {
            Layout = TypeLayoutList.Flush;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);

            Add(new ControlListItem(InventoryNumberAttribute, InventoryNumberLink));
            Add(new ControlListItem(ManufacturerAttribute));
            Add(new ControlListItem(LocationAttribute));
            Add(new ControlListItem(SupplierAttribute));
            Add(new ControlListItem(LedgeraccountAttribute));
            Add(new ControlListItem(CostcenterAttribute));
            Add(new ControlListItem(ConditionAttribute));
            Add(AttributesListItem);
            Add(new ControlListItem(CostValueAttribute));
            Add(new ControlListItem(PurchaseDateAttribute));
            Add(DrecognitionDateListItem);

            DrecognitionDateListItem.Content.Add(DrecognitionDateAttribute);
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
            var guid = context.Request.GetParameter("InventoryID")?.Value;
            var inventory = ViewModel.GetInventory(guid);
            var currency = ViewModel.GetSettings()?.Currency;

            InventoryNumberLink.Text = guid;
            InventoryNumberLink.Uri = new UriRelative(inventory.Uri);

            ManufacturerAttribute.Value = inventory.Manufacturer?.Name;
            LocationAttribute.Value = inventory.Location?.Name;
            SupplierAttribute.Value = inventory.Supplier?.Name;
            LedgeraccountAttribute.Value = inventory.LedgerAccount?.Name;
            CostcenterAttribute.Value = inventory.CostCenter?.Name;
            ConditionAttribute.Value = inventory.Condition?.Name;
            CostValueAttribute.Value = $"{inventory?.CostValue.ToString(context.Culture)} {(string.IsNullOrWhiteSpace(currency) ? "€" : currency)}";
            PurchaseDateAttribute.Value = inventory?.PurchaseDate != null ? inventory?.PurchaseDate.Value.ToString("d", context.Culture) : string.Empty;

            DrecognitionDateListItem.Enable = inventory.DerecognitionDate.HasValue;
            DrecognitionDateAttribute.Value = inventory?.DerecognitionDate != null ? inventory?.DerecognitionDate.Value.ToString("d", context.Culture) : string.Empty;

            AttributesListItem.Content.Clear();
            AttributesListItem.Enable = false;

            foreach (var attribute in inventory.Attributes)
            {
                AttributesListItem.Content.Add(new ControlAttribute()
                {
                    Name = attribute?.Name + ":",
                    Icon = new PropertyIcon(TypeIcon.Cube),
                    Value = attribute.Value,
                    TextColor = new PropertyColorText(TypeColorText.Secondary)
                });

                AttributesListItem.Enable = true;
            }

            return base.Render(context);
        }
    }
}
