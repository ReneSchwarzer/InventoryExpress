using InventoryExpress.Model.WebItems;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebUri;
using WebExpress.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlCardTemplate : ControlPanelCard
    {
        /// <summary>
        /// Liefert oder setzt die Vorlage
        /// </summary>
        public WebItemEntityTemplate Template { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlCardTemplate(string id = null)
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
            Styles.Add("width: fit-content;");
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
                Image = new UriRelative(Template.Image),
                ImageWidth = 100,
                Title = new ControlLink()
                {
                    Text = Template.Name,
                    Uri = new UriRelative(Template.Uri),
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
