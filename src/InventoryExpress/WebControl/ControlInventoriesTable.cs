using WebExpress.WebHtml;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlInventoriesTable : ControlApiTable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ControlInventoriesTable()
        {
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            RestUri = context.Uri.ModuleRoot.Append("api/v1/inventories");

            return base.Render(context);
        }
    }
}
