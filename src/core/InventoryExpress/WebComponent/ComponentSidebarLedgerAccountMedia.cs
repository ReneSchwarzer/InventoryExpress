using InventoryExpress.Model;
using System.Linq;
using WebExpress.WebAttribute;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebPage;

namespace InventoryExpress.WebComponent
{
    [Section(Section.SidebarHeader)]
    [Module("inventoryexpress")]
    [Context("ledgeraccountedit")]
    public sealed class ComponentSidebarLedgerAccountMedia : ComponentControlLink
    {
        /// <summary>
        /// Das Bild
        /// </summary>
        private ControlImage Image { get; } = new ControlImage()
        {
            Width = 180,
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two)
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentSidebarLedgerAccountMedia()
        {
            Content.Add(Image);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IComponentContext context, IPage page)
        {
            base.Initialization(context, page);
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
                var guid = context.Request.GetParameter("LedgerAccountID")?.Value;
                var ledgerAccount = ViewModel.Instance.LedgerAccounts.Where(x => x.Guid == guid).FirstOrDefault();
                var media = ViewModel.Instance.Media.Where(x => x.Id == (ledgerAccount != null ? ledgerAccount.MediaId : null)).FirstOrDefault();
                var image = media != null ? context.Uri.Root.Append("media").Append(media.Guid) : null;

                Uri = context.Uri.Append("media");

                Image.Uri = image == null ? context.Uri.Root.Append("/assets/img/inventoryexpress.svg") : image;
            }

            return base.Render(context);
        }
    }
}
