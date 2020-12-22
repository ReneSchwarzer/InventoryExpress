using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.UI.WebControl;

namespace InventoryExpress.WebControl
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
                //Image = new UriRelative(string.IsNullOrWhiteSpace(Manufactur.Image) ? "/Assets/img/Logo.png" : "/data/" + Manufactur.Image),
                ImageWidth = 100,
                ImageHeight = 100,
                Title = new ControlLink()
                {
                    Text = Template.Name,
                    Uri = context.Page.Uri.Append(Template.Guid),
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
