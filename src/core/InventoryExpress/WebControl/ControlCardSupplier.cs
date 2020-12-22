using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.UI.WebControl;

namespace InventoryExpress.WebControl
{
    public class ControlCardSupplier : ControlPanelCard
    {
        /// <summary>
        /// Liefert oder setzt den Lieferanten
        /// </summary>
        public Supplier Supplier { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlCardSupplier(string id = null)
            : base(id)
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
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var media = new ControlPanelMedia()
            {
                //Image = new UriRelative(string.IsNullOrWhiteSpace(Supplier.Image) ? "/Assets/img/Logo.png" : "/data/" + Supplier.Image),
                ImageWidth = 100,
                ImageHeight = 100,
                Title = new ControlLink()
                {
                    Text = Supplier.Name,
                    Uri = context.Page.Uri.Append(Supplier.Guid),
                    TextColor = new PropertyColorText(TypeColorText.Dark)
                }
            };

            media.Content.Add(new ControlText()
            {
                Text = Supplier.Description,
                Format = TypeFormatText.Paragraph
            });

            Content.Add(media);

            return base.Render(context);
        }
    }
}
