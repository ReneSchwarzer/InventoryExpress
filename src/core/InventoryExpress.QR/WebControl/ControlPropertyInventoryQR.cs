using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.QR.WebControl
{
    [Section(Section.PropertyPreferences)]
    [Application("InventoryExpress")]
    [Context("inventorydetails")]
    public sealed class ControlPropertyInventoriesQR : ComponentControlImage
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
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IComponentContext context)
        {
            base.Initialization(context);
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
