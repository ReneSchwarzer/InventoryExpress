using InventoryExpress.WebResource;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Components;

namespace InventoryExpress.WebControl
{
    [Section(Section.AppNavigationPrimary)]
    [Application("InventoryExpress")]
    public sealed class ControlAppNavigationCostCenter : ControlNavigationItemLink, IComponent
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
            Text = context.I18N("inventoryexpress.costcenters.label");
            Uri = context.Page.Uri.Root.Append("costcenters");
            Active = context.Page is IPageCostCenter ? TypeActive.Active : TypeActive.None;
            Icon = new PropertyIcon(TypeIcon.ShoppingBag);

            return base.Render(context);
        }

    }
}
