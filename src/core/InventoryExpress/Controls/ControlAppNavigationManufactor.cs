using InventoryExpress.Pages;
using WebExpress.Html;
using WebExpress.Plugins;
using WebExpress.UI.Controls;

namespace InventoryExpress.Controls
{
    [PluginComponent("app.navigation.primary")]
    public class ControlAppNavigationManufactor : ControlNavigationItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlAppNavigationManufactor()
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
            Text = context.I18N("inventoryexpress.manufactors.label", "Manufactors");
            Uri = context.Page.Uri.Root.Append("manufactors");
            Active = context.Page is IPageManufactor ? TypeActive.Active : TypeActive.None;
            Icon = new PropertyIcon(TypeIcon.Industry);

            return base.Render(context);
        }

    }
}
