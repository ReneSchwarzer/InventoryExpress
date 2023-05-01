using InventoryExpress.Model.WebItems;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

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
        /// Konstruktor
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
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            MediaLink.Uri = CostCenter.Uri;
            Media.Image = CostCenter.Image;

            return base.Render(context);
        }
    }
}
