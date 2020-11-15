using InventoryExpress.Pages;
using WebExpress.Html;
using WebExpress.UI.Controls;
using WebExpress.UI.Plugin;

namespace InventoryExpress.Controls
{
    public class ControlQuickCreateSupplier : ControlDropdownItemLink, IPluginComponentQuickCreateSecondary
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
            Text = context.I18N("inventoryexpress.suppliers.labelxxx", "Suppliers");
            Uri = context.Page.Uri.Root.Append("suppliers/add");
            Active = context.Page is IPageSupplier ? TypeActive.Active : TypeActive.None;
            Icon = new PropertyIcon(TypeIcon.Truck);

            return base.Render(context);
        }

    }
}
