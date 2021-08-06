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
    [Section(Section.AppQuickcreatePrimary)]
    [Application("InventoryExpress")]
    public sealed class ControlQuickCreateInventory : ControlSplitButtonItemLink, IComponent
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
            Text = context.I18N("inventoryexpress.inventory.label");
            Uri = context.Page.Uri.Root.Append("add");
            Active = context.Page is IPageInventory ? TypeActive.Active : TypeActive.None;
            Icon = new PropertyIcon(TypeIcon.Plus);

            return base.Render(context);
        }

    }
}
