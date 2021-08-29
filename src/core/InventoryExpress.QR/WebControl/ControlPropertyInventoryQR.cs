using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Components;

namespace InventoryExpress.QR.WebControl
{
    [Section(Section.PropertyPreferences)]
    [Application("InventoryExpress")]
    [Context("inventorydetails")]
    public sealed class ControlPropertyInventoriesQR : ControlImage, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlPropertyInventoriesQR()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var id = context.Page.GetParamValue("InventoryID");
            Uri = context.Uri.Root.Append("qr").Append(id);
            Width = 200;

            return base.Render(context);
        }
    }
}
