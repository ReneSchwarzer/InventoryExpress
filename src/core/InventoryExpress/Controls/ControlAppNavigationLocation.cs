using InventoryExpress.Pages;
using WebExpress.Html;
using WebExpress.Plugins;
using WebExpress.UI.Controls;

namespace InventoryExpress.Controls
{
    [PluginComponent("app.navigation.primary")]
    public class ControlAppNavigationLocation : ControlNavigationItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlAppNavigationLocation()
            : base()
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Text = context.I18N("inventoryexpress.locations.label", "Locations");
            Uri = context.Page.Uri.Root.Append("locations");
            Active = context.Page is IPageLocation ? TypeActive.Active : TypeActive.None;
            Icon = new PropertyIcon(TypeIcon.Map);

            return base.Render(context);
        }

    }
}
