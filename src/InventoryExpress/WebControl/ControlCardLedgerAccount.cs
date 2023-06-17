using InventoryExpress.Model.WebItems;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlCardLedgerAccount : ControlPanelCard
    {
        /// <summary>
        /// Liefert oder setzt das Sachkonto
        /// </summary>
        public WebItemEntityLedgerAccount LedgerAccount { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlCardLedgerAccount(string id = null)
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
                Image = LedgerAccount.Image,
                ImageWidth = 100,
                Title = new ControlLink()
                {
                    Text = LedgerAccount.Name,
                    Uri = LedgerAccount.Uri,
                    TextColor = new PropertyColorText(TypeColorText.Dark)
                }
            };

            media.Content.Add(new ControlText()
            {
                Text = LedgerAccount.Description,
                Format = TypeFormatText.Paragraph
            });

            Content.Add(media);

            return base.Render(context);
        }
    }
}
