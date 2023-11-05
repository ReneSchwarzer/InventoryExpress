using InventoryExpress.Model.WebItems;
using WebExpress.WebHtml;
using WebExpress.WebUI.WebControl;
using WebExpress.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlCardManufacturer : ControlPanelCard
    {
        /// <summary>
        /// Liefert oder setzt den Hersteller
        /// </summary>
        public WebItemEntityManufacturer Manufactur { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manufacture">The manufacturer.</param>
        public ControlCardManufacturer(WebItemEntityManufacturer manufacture)
            : base()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            BackgroundColor = new PropertyColorBackground(TypeColorBackground.Light);
            Styles.Add("width: fit-content;");

            Manufactur = manufacture;
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var media = new ControlPanelMedia()
            {
                Image = Manufactur.Image,
                ImageWidth = 100,
                Title = new ControlLink()
                {
                    Text = Manufactur.Name,
                    Uri = Manufactur.Uri,
                    TextColor = new PropertyColorText(TypeColorText.Dark)
                }
            };

            media.Content.Add(new ControlText()
            {
                Text = Manufactur.Description,
                Format = TypeFormatText.Paragraph
            });

            Content.Add(media);

            return base.Render(context);
        }
    }
}
