using InventoryExpress.Pages;
using WebExpress.Html;
using WebExpress.Plugins;
using WebExpress.UI.Controls;

namespace InventoryExpress.Controls
{
    [PluginComponent("app.quickcreate.primary")]
    public class ControlQuickCreateInventory : ControlSplitButtonItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlQuickCreateInventory()
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
            Text = context.I18N("inventoryexpress.inventories.label", "Inventory");
            Uri = context.Page.Uri.Root.Append("add");
            Active = context.Page is IPageInventory ? TypeActive.Active : TypeActive.None;
            Icon = new PropertyIcon(TypeIcon.Plus);

            return base.Render(context);
        }

    }
}
