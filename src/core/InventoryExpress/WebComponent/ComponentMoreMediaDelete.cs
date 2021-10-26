using InventoryExpress.Model;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.Attribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebPage;

namespace InventoryExpress.WebComponent
{
    [Section(Section.MoreSecondary)]
    [Module("inventoryexpress")]
    [Context("media")]
    public sealed class ComponentMoreMediaDelete : ControlDropdownItemLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentMoreMediaDelete()
        {
            Uri = new UriFragment();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public void Initialization(IComponentContext context)
        {
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

            lock (ViewModel.Instance.Database)
            {
                var guid = context.Request.GetParameter("MediaID")?.Value;
                var media = ViewModel.Instance.Media.Where(x => x.Guid == guid).FirstOrDefault();

                TextColor = media != null ? new PropertyColorText(TypeColorText.Danger) : new PropertyColorText(TypeColorText.Muted);
                Active = media != null ? TypeActive.None : TypeActive.Disabled;
            }

            OnClick = $"$('#modal_del_media').modal('show');";

            return base.Render(context);
        }
    }
}
