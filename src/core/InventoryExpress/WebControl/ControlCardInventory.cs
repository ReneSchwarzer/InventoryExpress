using InventoryExpress.Model;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebControl;

namespace InventoryExpress.WebControl
{
    public class ControlCardInventory : ControlPanelCard
    {
        /// <summary>
        /// Liefert oder setzt das Inventarelement
        /// </summary>
        public Inventory Inventory { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlCardInventory(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            BackgroundColor = new PropertyColorBackground(TypeColorBackground.Light);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var image = ViewModel.Instance.Media.Where(x => x.Id == Inventory.MediaId).Select(x => context.Page.Uri.Root.Append("media/" + x.Guid)).FirstOrDefault();
            var manufacturer = ViewModel.Instance.Manufacturers.Where(x => x.Id == Inventory.ManufacturerId).FirstOrDefault();
            var location = ViewModel.Instance.Locations.Where(x => x.Id == Inventory.LocationId).FirstOrDefault();
            var supplier = ViewModel.Instance.Suppliers.Where(x => x.Id == Inventory.SupplierId).FirstOrDefault();
            var ledgerAccount = ViewModel.Instance.LedgerAccounts.Where(x => x.Id == Inventory.LedgerAccountId).FirstOrDefault();
            var costCenter = ViewModel.Instance.CostCenters.Where(x => x.Id == Inventory.CostCenterId).FirstOrDefault();
            var condition = ViewModel.Instance.Conditions.Where(x => x.Id == Inventory.ConditionId)?.FirstOrDefault();
            var template = ViewModel.Instance.Templates.Where(x => x.Id == Inventory.TemplateId)?.FirstOrDefault();

            var media = new ControlPanelMedia()
            {
                Image = image == null ? context.Page.Uri.Root.Append("/assets/img/inventoryexpress.svg") : image,
                ImageWidth = 100,
                Title = new ControlLink()
                {
                    Text = Inventory.Name,
                    Uri = context.Page.Uri.Root.Append(Inventory.Guid),
                    TextColor = new PropertyColorText(TypeColorText.Primary)
                }
            };

            media.Content.Add(new ControlLink()
            {
                Text = template?.Name,
                //Url = "/" + Inventory.ID,
                TextColor = new PropertyColorText(TypeColorText.Dark)
            });

            var flex = new ControlPanelFlexbox()
            {
                Direction = TypeDirection.Horizontal
            };

            if (manufacturer != null)
            {
                flex.Content.Add(new ControlAttribute()
                {
                    Icon = new PropertyIcon(TypeIcon.Industry),
                    Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
                    TextColor = new PropertyColorText(TypeColorText.Secondary),
                    Name = manufacturer.Name
                });
            }

            if (location != null)
            {
                flex.Content.Add(new ControlAttribute()
                {
                    Icon = new PropertyIcon(TypeIcon.Map),
                    Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
                    TextColor = new PropertyColorText(TypeColorText.Secondary),
                    Name = location.Name
                });
            }

            if (supplier != null)
            {
                flex.Content.Add(new ControlAttribute()
                {
                    Icon = new PropertyIcon(TypeIcon.Truck),
                    Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
                    TextColor = new PropertyColorText(TypeColorText.Secondary),
                    Name = supplier.Name
                });
            }

            if (ledgerAccount != null)
            {
                flex.Content.Add(new ControlAttribute()
                {
                    Icon = new PropertyIcon(TypeIcon.At),
                    Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
                    TextColor = new PropertyColorText(TypeColorText.Secondary),
                    Name = ledgerAccount.Name
                });
            }
            
            if (costCenter != null)
            {
                flex.Content.Add(new ControlAttribute()
                {
                    Icon = new PropertyIcon(TypeIcon.ShoppingBag),
                    Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
                    TextColor = new PropertyColorText(TypeColorText.Secondary),
                    Name = costCenter.Name
                });
            }

            if (condition != null)
            {
                flex.Content.Add(new ControlAttribute()
                {
                    Icon = new PropertyIcon(TypeIcon.Star),
                    Name = condition.Name,
                    Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
                    TextColor = new PropertyColorText(TypeColorText.Secondary)
                });
            }

            media.Content.Add(flex);

            Content.Add(media);

            return base.Render(context);
        }
    }
}
