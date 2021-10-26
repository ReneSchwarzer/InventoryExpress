using InventoryExpress.WebPage;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.UI.Attribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebPage;

namespace InventoryExpress.WebComponent
{
    [Section(Section.AppNavigationPreferences)]
    [Module("inventoryexpress")]
    public sealed class ComponentAppNavigationInventory : ControlNavigationItemLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentAppNavigationInventory()
            : base()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public void Initialization(IComponentContext context)
        {
            Text = "inventoryexpress:inventoryexpress.inventories.label";
            Uri = context.Module.ContextPath;
            Icon = new PropertyIcon(TypeIcon.LayerGroup);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Active = context.Page is IPageInventory ? TypeActive.Active : TypeActive.None;

            return base.Render(context);
        }

    }
}
