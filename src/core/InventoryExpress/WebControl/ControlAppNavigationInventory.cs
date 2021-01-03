using InventoryExpress.WebResource;
using WebExpress.Html;
using WebExpress.WebResource;
using WebExpress.UI.WebControl;
using WebExpress.Internationalization;
using WebExpress.UI.Attribute;
using WebExpress.WebApp.Components;
using WebExpress.Attribute;
using WebExpress.UI.Component;

namespace InventoryExpress.WebControl
{
    [Section(Section.AppNavigationPreferences)]
    [Application("InventoryExpress")]
    public sealed class ControlAppNavigationInventory : ControlNavigationItemLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlAppNavigationInventory()
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
            Text = context.I18N("inventoryexpress.inventories.label");
            Uri = context.Page.Uri.Root;
            Active = context.Page is IPageInventory ? TypeActive.Active : TypeActive.None;
            Icon = new PropertyIcon(TypeIcon.LayerGroup);

            return base.Render(context);
        }

    }
}
