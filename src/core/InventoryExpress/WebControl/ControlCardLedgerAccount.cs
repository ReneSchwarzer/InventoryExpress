using InventoryExpress.Model.WebItems;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebUri;
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
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlCardLedgerAccount(string id = null)
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
                Image = new UriRelative(LedgerAccount.Image),
                ImageWidth = 100,
                Title = new ControlLink()
                {
                    Text = LedgerAccount.Name,
                    Uri = new UriRelative(LedgerAccount.Uri),
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
