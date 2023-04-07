using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.QR.WebFragment
{
    [Section(Section.PropertyPreferences)]
    [Module("InventoryExpress")]
    [Context("inventorydetails")]
    public sealed class ControlPropertyInventoriesQR : FragmentControlImage
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlPropertyInventoriesQR()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IFragmentContext context, IPage page)
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
            var id = context.Request.GetParameter("InventoryID")?.Value;
            Uri = context.Uri.Root.Append("qr").Append(id);
            Width = 200;

            return base.Render(context);
        }
    }
}
