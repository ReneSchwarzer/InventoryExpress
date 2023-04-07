using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebUri;
using WebExpress.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlCardInventory : ControlPanelCard
    {
        /// <summary>
        /// Liefert oder setzt das Inventarelement
        /// </summary>
        private WebItemEntityInventory Inventory { get; set; }

        /// <summary>
        /// Liefert das Bild
        /// </summary>
        private ControlPanelMedia Media { get; } = new ControlPanelMedia()
        {
            ImageWidth = 100
        };

        /// <summary>
        /// Liefert den Link
        /// </summary>
        private ControlLink MediaLink { get; } = new ControlLink()
        {
            TextColor = new PropertyColorText(TypeColorText.Dark)
        };

        /// <summary>
        /// Liefert das Herstellerattribut
        /// </summary>
        private ControlLink Template { get; } = new ControlLink()
        {
            TextColor = new PropertyColorText(TypeColorText.Dark)
        };

        /// <summary>
        /// Liefert das Herstellerattribut
        /// </summary>
        private ControlAttribute Manufacturer { get; } = new ControlAttribute()
        {
            Icon = new PropertyIcon(TypeIcon.Industry),
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
            TextColor = new PropertyColorText(TypeColorText.Secondary)
        };

        /// <summary>
        /// Liefert das Standortattribut
        /// </summary>
        private ControlAttribute Location { get; } = new ControlAttribute()
        {
            Icon = new PropertyIcon(TypeIcon.Map),
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
            TextColor = new PropertyColorText(TypeColorText.Secondary)
        };

        /// <summary>
        /// Liefert das Lieferantenattribut
        /// </summary>
        private ControlAttribute Supplier { get; } = new ControlAttribute()
        {
            Icon = new PropertyIcon(TypeIcon.Truck),
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
            TextColor = new PropertyColorText(TypeColorText.Secondary)
        };

        /// <summary>
        /// Liefert das Sachkontoattribut
        /// </summary>
        private ControlAttribute LedgerAccount { get; } = new ControlAttribute()
        {
            Icon = new PropertyIcon(TypeIcon.At),
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
            TextColor = new PropertyColorText(TypeColorText.Secondary)
        };

        /// <summary>
        /// Liefert das Kostenstellenattribut
        /// </summary>
        private ControlAttribute CostCenter { get; } = new ControlAttribute()
        {
            Icon = new PropertyIcon(TypeIcon.ShoppingBag),
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
            TextColor = new PropertyColorText(TypeColorText.Secondary)
        };

        /// <summary>
        /// Liefert das Zustandsattribut
        /// </summary>
        private ControlAttribute Condition { get; } = new ControlAttribute()
        {
            Icon = new PropertyIcon(TypeIcon.Star),
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
            TextColor = new PropertyColorText(TypeColorText.Secondary)
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="inventory">Der Invntargegenstand</param>
        public ControlCardInventory(WebItemEntityInventory inventory)
        {
            Styles.Add("width: fit-content;");

            Inventory = inventory;

            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            BackgroundColor = new PropertyColorBackground(TypeColorBackground.Light);

            MediaLink.Text = Inventory.Name;
            Media.Title = MediaLink;

            Media.Content.Add(Template);

            var flex = new ControlPanelFlexbox
            (
                Manufacturer,
                Location,
                Supplier,
                LedgerAccount,
                CostCenter,
                Condition
            )
            {
                Direction = TypeDirection.Horizontal
            };

            Media.Image = new UriRelative(Inventory.Media?.Uri);
            MediaLink.Uri = new UriRelative(Inventory.Uri);

            Template.Text = Inventory.Template?.Name;
            Template.Uri = new UriRelative(Inventory.Template?.Uri);

            Manufacturer.Enable = Inventory.Manufacturer != null;
            Manufacturer.Name = Inventory.Manufacturer?.Name;

            Location.Enable = Inventory.Location != null;
            Location.Name = Inventory.Location?.Name;

            Supplier.Enable = Inventory.Supplier != null;
            Supplier.Name = Inventory.Supplier?.Name;

            LedgerAccount.Enable = Inventory.LedgerAccount != null;
            LedgerAccount.Name = Inventory.LedgerAccount?.Name;

            CostCenter.Enable = Inventory.CostCenter != null;
            CostCenter.Name = Inventory.CostCenter?.Name;

            Condition.Enable = Inventory.Condition != null;
            Condition.Name = Inventory.Condition?.Name;

            Media.Content.Add(flex);
            Content.Add(Media);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            return base.Render(context);
        }
    }
}
