using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.Pages;
using WebExpress.UI.Controls;

namespace InventoryExpress.Controls
{
    public class ControlInventoryCard : ControlPanelCard
    {
        /// <summary>
        /// Liefert oder setzt das Inventarelement
        /// </summary>
        public Inventory Inventory { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlInventoryCard(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two);
            BackgroundColor = new PropertyColorBackground(TypeColorBackground.Light);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var media = new ControlPanelMedia(Page)
            {
                Image = new UriRelative(string.IsNullOrWhiteSpace(Inventory.Image) ? "/Assets/img/Logo.png" : "/data/" + Inventory.Image),
                ImageWidth = 100,
                ImageHeight = 100,
                Title = new ControlLink(Page)
                {
                    Text = Inventory.Name,
                    Uri = Page.Uri.Root.Append(Inventory.ID),
                    TextColor = new PropertyColorText(TypeColorText.Primary)
                }
            };

            media.Content.Add(new ControlLink(Page)
            {
                Text = Inventory?.Template?.Name,
                //Url = "/" + Inventory.ID,
                TextColor = new PropertyColorText(TypeColorText.Dark)
            });

            var flex = new ControlFlexbox(Page)
            {
                Direction = TypesFlexboxDirection.Horizontal
            };

            if (Inventory.Manufacturer != null && Inventory.Manufacturer is var manufacturer)
            {
                flex.Items.Add(new ControlAttribute(Page)
                {
                    Icon = new PropertyIcon(TypeIcon.Industry),
                    Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
                    TextColor = new PropertyColorText(TypeColorText.Secondary),
                    Name = manufacturer.Name
                });
            }

            if (Inventory.Location != null && Inventory.Location is var location)
            {
                flex.Items.Add(new ControlAttribute(Page)
                {
                    Icon = new PropertyIcon(TypeIcon.Map),
                    Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
                    TextColor = new PropertyColorText(TypeColorText.Secondary),
                    Name = location.Name
                });
            }

            if (Inventory.Supplier != null && Inventory.Supplier is var supplier)
            {
                flex.Items.Add(new ControlAttribute(Page)
                {
                    Icon = new PropertyIcon(TypeIcon.Truck),
                    Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
                    TextColor = new PropertyColorText(TypeColorText.Secondary),
                    Name = supplier.Name
                });
            }

            if (Inventory.GLAccount != null && Inventory.GLAccount is var glaAccount)
            {
                flex.Items.Add(new ControlAttribute(Page)
                {
                    Icon = new PropertyIcon(TypeIcon.At),
                    Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
                    TextColor = new PropertyColorText(TypeColorText.Secondary),
                    Name = glaAccount.Name
                });
            }

            if (Inventory.State != null && Inventory.State is var state)
            {
                flex.Items.Add(new ControlAttribute(Page)
                {
                    Icon = new PropertyIcon(TypeIcon.Star),
                    Name = state.Name,
                    Padding = new PropertySpacingPadding(PropertySpacing.Space.Two),
                    TextColor = new PropertyColorText(TypeColorText.Secondary)
                });
            }

            media.Content.Add(flex);

            Content.Add(media);

            return base.ToHtml();
        }
    }
}
