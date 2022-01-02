using InventoryExpress.WebPage;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebComponent
{
    [Section(Section.AppNavigationPrimary)]
    [Module("inventoryexpress")]
    public sealed class ComponentAppNavigationLocation : ComponentControlNavigationItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentAppNavigationLocation()
            : base()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IComponentContext context)
        {
            base.Initialization(context);

            Text = "inventoryexpress:inventoryexpress.locations.label";
            Uri = new UriResource(context.Module.ContextPath, "locations");
            Icon = new PropertyIcon(TypeIcon.Map);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Active = context.Page is IPageLocation ? TypeActive.Active : TypeActive.None;

            return base.Render(context);
        }

    }
}
