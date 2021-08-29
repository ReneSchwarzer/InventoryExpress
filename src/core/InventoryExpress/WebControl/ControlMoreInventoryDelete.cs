using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.Components;

namespace InventoryExpress.WebControl
{
    [Section(Section.MoreSecondary)]
    [Application("InventoryExpress")]
    [Context("inventorydetails")]
    public sealed class ControlMoreInventoryDelete : ControlDropdownItemLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlMoreInventoryDelete()
           : base("del")
        {
            TextColor = new PropertyColorText(TypeColorText.Danger);
            Uri = new UriFragment();
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Text = context.Page.I18N("inventoryexpress.delete.label");
            Icon = new PropertyIcon(TypeIcon.Trash);

            OnClick = $"$('#modal_del_inventory').modal('show');";

            return base.Render(context);
        }
    }
}
