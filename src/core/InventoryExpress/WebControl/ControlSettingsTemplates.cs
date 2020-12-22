using WebExpress.Html;
using WebExpress.WebResource;
using WebExpress.UI.WebControl;
using WebExpress.Attribute;
using WebExpress.UI.Attribute;
using WebExpress.WebApp.Components;
using WebExpress.UI.Component;
using WebExpress.Internationalization;
using InventoryExpress.WebResource;

namespace InventoryExpress.WebControl
{
    [Section(Section.AppSettingsPrimary)]
    [Application("InventoryExpress")]
    public sealed class ControlSettingsTemplates : ControlDropdownItemLink, IComponent
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
            Text = context.I18N("inventoryexpress.templates.label");
            Uri = context.Page.Uri.Root.Append("templates");
            Active = context.Page is IPageTemplate ? TypeActive.Active : TypeActive.None;
            //Icon = new PropertyIcon(TypeIcon.Book);

            return base.Render(context);
        }

    }
}
