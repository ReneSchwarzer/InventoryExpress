using WebExpress.Html;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlInventoriesTable : ControlApiTable
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlInventoriesTable()
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            RestUri = context.Uri.Root.Append("api/v1/inventories");

            return base.Render(context);
        }
    }
}
