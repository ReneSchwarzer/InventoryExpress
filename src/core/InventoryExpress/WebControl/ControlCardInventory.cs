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

            var media = new ControlPanelMedia()
            {
                Image = image == null ? context.Page.Uri.Root.Append("/assets/img/inventoryexpress.svg") : image,
                ImageWidth = 100,
                ImageHeight = 100,
                Title = new ControlLink()
                {
                    Text = Inventory.Name,
                    Uri = context.Page.Uri.Root.Append(Inventory.Guid),
                    TextColor = new PropertyColorText(TypeColorText.Primary)
                }
            };

            media.Content.Add(new ControlLink()
            {
                Text = Inventory?.Template?.Name,
                //Url = "/" + Inventory.ID,
                TextColor = new PropertyColorText(TypeColorText.Dark)
            });

            var flex = new ControlPanelFlexbox()
            {
                Direction = TypeDirection.Horizontal
            };

            if (Inventory.Manufacturer != null && Inventory.Manufacturer is var manufacturer)
            {
                flex.Content.Add(new ControlAttribute()
                {
                    Icon = new PropertyIcon(TypeIcon.Industry),
                    Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
                    TextColor = new PropertyColorText(TypeColorText.Secondary),
                    Name = manufacturer.Name
                });
            }

            if (Inventory.Location != null && Inventory.Location is var location)
            {
                flex.Content.Add(new ControlAttribute()
                {
                    Icon = new PropertyIcon(TypeIcon.Map),
                    Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
                    TextColor = new PropertyColorText(TypeColorText.Secondary),
                    Name = location.Name
                });
            }

            if (Inventory.Supplier != null && Inventory.Supplier is var supplier)
            {
                flex.Content.Add(new ControlAttribute()
                {
                    Icon = new PropertyIcon(TypeIcon.Truck),
                    Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
                    TextColor = new PropertyColorText(TypeColorText.Secondary),
                    Name = supplier.Name
                });
            }

            if (Inventory.LedgerAccount != null && Inventory.LedgerAccount is var ledgerAccount)
            {
                flex.Content.Add(new ControlAttribute()
                {
                    Icon = new PropertyIcon(TypeIcon.At),
                    Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
                    TextColor = new PropertyColorText(TypeColorText.Secondary),
                    Name = ledgerAccount.Name
                });
            }

            if (Inventory.Condition != null && Inventory.Condition is var state)
            {
                flex.Content.Add(new ControlAttribute()
                {
                    Icon = new PropertyIcon(TypeIcon.Star),
                    Name = state.Name,
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
