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
    [Section(Section.AppSettingsPrimary)]
    [Module("inventoryexpress")]
    public sealed class ComponentSettingsTemplates : ComponentControlDropdownItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentSettingsTemplates()
            : base()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IComponentContext context, IPage page)
        {
            base.Initialization(context, page);

            Text = "inventoryexpress:inventoryexpress.templates.label";
            Uri = new UriResource(context.Module.ContextPath, "setting/templates");
            Icon = new PropertyIcon(TypeIcon.Clone);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Active = context.Page is IPageTemplate ? TypeActive.Active : TypeActive.None;

            return base.Render(context);
        }

    }
}
