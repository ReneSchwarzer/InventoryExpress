using InventoryExpress.Model;
using System.Linq;
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
    [Context("media")]
    public sealed class ControlMoreMediaDelete : ControlDropdownItemLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlMoreMediaDelete()
        {
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

            lock (ViewModel.Instance.Database)
            {
                var guid = context.Page.GetParamValue("MediaID");
                var media = ViewModel.Instance.Media.Where(x => x.Guid == guid).FirstOrDefault();

                TextColor = media != null ? new PropertyColorText(TypeColorText.Danger) : new PropertyColorText(TypeColorText.Muted);
                Active = media != null ? TypeActive.None : TypeActive.Disabled;
            }
            
            OnClick = $"$('#del_media_modal').modal('show');";
            
            return base.Render(context);
        }
    }
}
