using InventoryExpress.Model.WebItems;
using WebExpress.WebHtml;
using WebExpress.WebUI.WebControl;
using WebExpress.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlCardLocation : ControlPanelCard
    {
        /// <summary>
        /// Liefert oder setzt den Standort
        /// </summary>
        public WebItemEntityLocation Location { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlCardLocation(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            BackgroundColor = new PropertyColorBackground(TypeColorBackground.Light);
            Styles.Add("width: fit-content;");
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
                Image = Location.Image,
                ImageWidth = 100,
                Title = new ControlLink()
                {
                    Text = Location.Name,
                    Uri = Location.Uri,
                    TextColor = new PropertyColorText(TypeColorText.Dark)
                }
            };

            media.Content.Add(new ControlText()
            {
                Text = Location.Description,
                Format = TypeFormatText.Paragraph
            });

            Content.Add(media);

            return base.Render(context);
        }
    }
}
