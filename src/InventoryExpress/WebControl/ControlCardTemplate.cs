using InventoryExpress.Model.WebItems;
using WebExpress.WebCore.WebHtml;
using WebExpress.WebCore.WebPage;
using WebExpress.WebUI.WebControl;

namespace InventoryExpress.WebControl
{
    public class ControlCardTemplate : ControlPanelCard
    {
        /// <summary>
        /// Liefert oder setzt die Vorlage
        /// </summary>
        public WebItemEntityTemplate Template { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlCardTemplate(string id = null)
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
                Image = Template.Image,
                ImageWidth = 100,
                Title = new ControlLink()
                {
                    Text = Template.Name,
                    Uri = Template.Uri,
                    TextColor = new PropertyColorText(TypeColorText.Dark)
                }
            };

            media.Content.Add(new ControlText()
            {
                Text = Template.Description,
                Format = TypeFormatText.Paragraph
            });

            Content.Add(media);

            return base.Render(context);
        }
    }
}
