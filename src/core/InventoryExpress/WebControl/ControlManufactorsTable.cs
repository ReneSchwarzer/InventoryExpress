using WebExpress.Html;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlManufactorsTable : ControlApiTable
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlManufactorsTable()
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            RestUri = context.Uri.ModuleRoot.Append("api/v1/manufacturers");

            return base.Render(context);
        }
    }
}
