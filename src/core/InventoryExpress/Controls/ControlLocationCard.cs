using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.Pages;
using WebExpress.UI.Controls;

namespace InventoryExpress.Controls
{
    public class ControlLocationCard : ControlPanelCard
    {
        /// <summary>
        /// Liefert oder setzt den Standort
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlLocationCard(IPage page, string id = null)
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
                Image = new UriRelative(string.IsNullOrWhiteSpace(Location.Image) ? "/Assets/img/Logo.png" : "/data/" + Location.Image),
                ImageWidth = 100,
                ImageHeight = 100,
                Title = new ControlLink(Page)
                {
                    Text = Location.Name,
                    Uri = Page.Uri.Append(Location.ID),
                    TextColor = new PropertyColorText(TypeColorText.Dark)
                }
            };

            media.Content.Add(new ControlText(Page)
            {
                Text = Location.Memo,
                Format = TypeFormatText.Paragraph
            });

            Content.Add(media);

            return base.ToHtml();
        }
    }
}
