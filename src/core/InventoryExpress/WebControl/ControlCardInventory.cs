using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlCardInventory : ControlPanelCard
    {
        /// <summary>
        /// Liefert oder setzt das Inventarelement
        /// </summary>
        private Inventory Inventory { get; set; }

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
        public ControlCardInventory(Inventory inventory)
        {


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
                var image = ViewModel.Instance.Media.Where(x => x.Id == Inventory.MediaId).Select(x => context.Uri.Root.Append("media/" + x.Guid)).FirstOrDefault();
                var manufacturer = ViewModel.Instance.Manufacturers.Where(x => x.Id == Inventory.ManufacturerId).FirstOrDefault();
                var location = ViewModel.Instance.Locations.Where(x => x.Id == Inventory.LocationId).FirstOrDefault();
                var supplier = ViewModel.Instance.Suppliers.Where(x => x.Id == Inventory.SupplierId).FirstOrDefault();
                var ledgerAccount = ViewModel.Instance.LedgerAccounts.Where(x => x.Id == Inventory.LedgerAccountId).FirstOrDefault();
                var costCenter = ViewModel.Instance.CostCenters.Where(x => x.Id == Inventory.CostCenterId).FirstOrDefault();
                var condition = ViewModel.Instance.Conditions.Where(x => x.Id == Inventory.ConditionId)?.FirstOrDefault();
                var template = ViewModel.Instance.Templates.Where(x => x.Id == Inventory.TemplateId)?.FirstOrDefault();

                Media.Image = image ?? context.Uri.Root.Append("/assets/img/inventoryexpress.svg");
                MediaLink.Uri = context.Uri.Root.Append(Inventory.Guid);

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
