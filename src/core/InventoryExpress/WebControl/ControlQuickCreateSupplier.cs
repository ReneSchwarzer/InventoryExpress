using InventoryExpress.WebResource;
using WebExpress.Html;
using WebExpress.WebResource;
using WebExpress.UI.WebControl;
using WebExpress.Internationalization;
using WebExpress.UI.Attribute;
using WebExpress.Attribute;
using WebExpress.WebApp.Components;
using WebExpress.UI.Component;

namespace InventoryExpress.WebControl
{
    [Section(Section.AppQuickcreateSecondary)]
    [Application("InventoryExpress")]
    public sealed class ControlQuickCreateSupplier : ControlSplitButtonItemLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlQuickCreateSupplier()
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
            Text = context.I18N("inventoryexpress.supplier.label");
            Uri = context.Page.Uri.Root.Append("suppliers/add");
            Active = context.Page is IPageSupplier ? TypeActive.Active : TypeActive.None;
            Icon = new PropertyIcon(TypeIcon.Truck);

            return base.Render(context);
        }

    }
}
