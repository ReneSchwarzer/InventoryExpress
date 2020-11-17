using InventoryExpress.Pages;
using WebExpress.Html;
using WebExpress.Plugins;
using WebExpress.UI.Controls;

namespace InventoryExpress.Controls
{
    [PluginComponent("app.settings.primary")]
    public class ControlSettingsTemplates : ControlDropdownItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlSettingsTemplates()
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
            Text = context.I18N("inventoryexpress.templates.label", "Templates");
            Uri = context.Page.Uri.Root.Append("templates");
            Active = context.Page is IPageTemplate ? TypeActive.Active : TypeActive.None;
            //Icon = new PropertyIcon(TypeIcon.Book);

            return base.Render(context);
        }

    }
}
