using InventoryExpress.Pages;
using WebExpress.Html;
using WebExpress.Plugins;
using WebExpress.UI.Controls;

namespace InventoryExpress.Controls
{
    [PluginComponent("app.navigation.primary")]
    public class ControlAppNavigationCostCenter : ControlNavigationItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlAppNavigationCostCenter()
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
            Text = context.I18N("inventoryexpress.costcenters.label", "Cost centers");
            Uri = context.Page.Uri.Root.Append("costcenters");
            Active = context.Page is IPageCostCenter ? TypeActive.Active : TypeActive.None;
            Icon = new PropertyIcon(TypeIcon.ShoppingBag);

            return base.Render(context);
        }

    }
}
