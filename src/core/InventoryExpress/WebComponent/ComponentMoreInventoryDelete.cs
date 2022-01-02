using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebComponent
{
    [Section(Section.MoreSecondary)]
    [Module("inventoryexpress")]
    [Context("inventorydetails")]
    public sealed class ComponentMoreInventoryDelete : ComponentControlDropdownItemLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentMoreInventoryDelete()
           : base("del")
        {
            TextColor = new PropertyColorText(TypeColorText.Danger);
            Uri = new UriFragment();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IComponentContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Text = InternationalizationManager.I18N(context.Culture, "inventoryexpress:inventoryexpress.delete.label");
            Icon = new PropertyIcon(TypeIcon.Trash);

            OnClick = $"$('#modal_del_inventory').modal('show');";

            return base.Render(context);
        }
    }
}
