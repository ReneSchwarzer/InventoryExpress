using InventoryExpress.Pages;
using WebExpress.Html;
using WebExpress.UI.Controls;
using WebExpress.UI.Plugin;

namespace InventoryExpress.Controls
{
    public class ControlQuickCreateInventory : ControlLink, IPluginComponentQuickCreatePrimary
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
            TextColor = new PropertyColorText(TypeColorText.Light);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Text = "Inventar";
            Uri = context.Page.Uri.Root.Append("add");
            Active = context.Page is IPageInventory ? TypeActive.Active : TypeActive.None;
            Icon = new PropertyIcon(TypeIcon.Plus);

            return base.Render(context);
        }

    }
}
