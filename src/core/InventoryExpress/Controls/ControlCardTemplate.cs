using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.Pages;
using WebExpress.UI.Controls;

namespace InventoryExpress.Controls
{
    public class ControlCardTemplate : ControlPanelCard
    {
        /// <summary>
        /// Liefert oder setzt die Vorlage
        /// </summary>
        public Template Template { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlCardTemplate(IPage page, string id = null)
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
                //Image = new UriRelative(string.IsNullOrWhiteSpace(Manufactur.Image) ? "/Assets/img/Logo.png" : "/data/" + Manufactur.Image),
                ImageWidth = 100,
                ImageHeight = 100,
                Title = new ControlLink(Page)
                {
                    Text = Template.Name,
                    Uri = Page.Uri.Append(Template.ID.ToString()),
                    TextColor = new PropertyColorText(TypeColorText.Dark)
                }
            };

            media.Content.Add(new ControlText(Page)
            {
                Text = Template.Discription,
                Format = TypeFormatText.Paragraph
            });

            Content.Add(media);

            return base.ToHtml();
        }
    }
}
