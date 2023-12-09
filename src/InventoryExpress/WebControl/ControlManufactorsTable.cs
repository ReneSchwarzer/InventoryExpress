using WebExpress.WebApp.WebApiControl;
using WebExpress.WebCore.WebHtml;
using WebExpress.WebCore.WebPage;

namespace InventoryExpress.WebControl
{
    public class ControlManufactorsTable : ControlApiTable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ControlManufactorsTable()
        {
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            RestUri = context.Uri.ModuleRoot.Append("api/v1/manufacturers");

            return base.Render(context);
        }
    }
}
