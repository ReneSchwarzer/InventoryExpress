using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
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

            //Media.Content.Add(new ControlText()
            //{
            //    Text = Inventory.Description,
            //    Format = TypeFormatText.Paragraph
            //});

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
            lock (ViewModel.Instance.Database)
            {
                var image = new UriRelative(Inventory.Image);
                var manufacturer = Inventory.Manufacturer;
                var location = Inventory.Location;
                var supplier = Inventory.Supplier;
                var ledgerAccount = Inventory.LedgerAccount;
                var costCenter = Inventory.CostCenter;
                var condition = Inventory.Condition;
                var template = Inventory.Template;

                Media.Image = image;
                MediaLink.Uri = new UriRelative(Inventory.Uri);

                Template.Text = template?.Name;
                //Template.Uri = ;

                Manufacturer.Enable = manufacturer != null;
                Manufacturer.Name = manufacturer?.Name;

                Location.Enable = location != null;
                Location.Name = location?.Name;

                Supplier.Enable = supplier != null;
                Supplier.Name = supplier?.Name;

                LedgerAccount.Enable = ledgerAccount != null;
                LedgerAccount.Name = ledgerAccount?.Name;

                CostCenter.Enable = costCenter != null;
                CostCenter.Name = costCenter?.Name;

                Condition.Enable = condition != null;
                Condition.Name = condition?.Name;
            }

            return base.Render(context);
        }
    }
}
