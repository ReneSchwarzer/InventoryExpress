using InventoryExpress.Model;
using System.Linq;
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
    [Context("media")]
    public sealed class ComponentMoreMediaDelete : ComponentControlDropdownItemLink
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
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IComponentContext context, IPage page)
        {
            base.Initialization(context, page);
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

            OnClick = new PropertyOnClick($"$('#modal_del_media').modal('show');");

            return base.Render(context);
        }
    }
}
