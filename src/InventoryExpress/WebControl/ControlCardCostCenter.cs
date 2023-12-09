using InventoryExpress.Model.WebItems;
using WebExpress.WebCore.WebHtml;
using WebExpress.WebCore.WebPage;
using WebExpress.WebUI.WebControl;

namespace InventoryExpress.WebControl
{
    public class ControlCardCostCenter : ControlPanelCard
    {
        /// <summary>
        /// Liefert oder setzt die Kostenstelle
        /// </summary>
        private WebItemEntityCostCenter CostCenter { get; set; }

        /// <summary>
        /// Liefert das Bild
        /// </summary>
        private ControlPanelMedia Media { get; } = new ControlPanelMedia()
        {
            ImageWidth = 100
        };

        /// <summary>
        /// Liefert den Link
        /// </summary>
        private ControlLink MediaLink { get; } = new ControlLink()
        {
            TextColor = new PropertyColorText(TypeColorText.Dark)
        };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="costCenter">Die Kostenstelle</param>
        public ControlCardCostCenter(WebItemEntityCostCenter costCenter)
        {
            CostCenter = costCenter;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            BackgroundColor = new PropertyColorBackground(TypeColorBackground.Light);

            MediaLink.Text = CostCenter.Name;
            Media.Title = MediaLink;

            Media.Content.Add(new ControlText()
            {
                Text = CostCenter.Description,
                Format = TypeFormatText.Paragraph
            });

            Content.Add(Media);
            Styles.Add("width: fit-content;");
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            MediaLink.Uri = CostCenter.Uri;
            Media.Image = CostCenter.Image;

            return base.Render(context);
        }
    }
}
