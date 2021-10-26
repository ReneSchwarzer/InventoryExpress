using InventoryExpress.Model;
using System.Linq;
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
        private CostCenter CostCenter { get; set; }

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
        public ControlCardCostCenter(CostCenter costCenter)
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
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            lock (ViewModel.Instance.Database)
            {
                var image = ViewModel.Instance.Media.Where(x => x.Id == CostCenter.MediaId).Select(x => context.Request.Uri.Root.Append("media/" + x.Guid)).FirstOrDefault();

                MediaLink.Uri = context.Request.Uri.Append(CostCenter.Guid);
                Media.Image = image == null ? context.Request.Uri.Root.Append("/assets/img/inventoryexpress.svg") : image;
            }

            return base.Render(context);
        }
    }
}
