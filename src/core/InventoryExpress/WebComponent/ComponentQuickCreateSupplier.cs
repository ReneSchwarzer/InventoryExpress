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
    [Section(Section.AppQuickcreateSecondary)]
    [Module("inventoryexpress")]
    public sealed class ComponentQuickCreateSupplier : ComponentControlSplitButtonItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentQuickCreateSupplier()
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

            Text = "inventoryexpress:inventoryexpress.supplier.label";
            Uri = new UriResource(context.Module.ContextPath, "suppliers/add");
            Icon = new PropertyIcon(TypeIcon.Truck);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Active = context.Page is IPageSupplier ? TypeActive.Active : TypeActive.None;

            return base.Render(context);
        }

    }
}
