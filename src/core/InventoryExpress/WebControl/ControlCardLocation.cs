using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlCardLocation : ControlPanelCard
    {
        /// <summary>
        /// Liefert oder setzt den Standort
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlCardLocation(string id = null)
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
                var image = ViewModel.Instance.Media.Where(x => x.Id == Location.MediaId).Select(x => context.Uri.Root.Append("media/" + x.Guid)).FirstOrDefault();

                var media = new ControlPanelMedia()
                {
                    Image = image ?? context.Uri.Root.Append("/assets/img/inventoryexpress.svg"),
                    ImageWidth = 100,
                    Title = new ControlLink()
                    {
                        Text = Location.Name,
                        Uri = context.Uri.Append(Location.Guid),
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
}
