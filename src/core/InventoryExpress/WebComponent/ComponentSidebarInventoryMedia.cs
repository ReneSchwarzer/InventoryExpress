using InventoryExpress.Model;
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
    [Section(Section.SidebarHeader)]
    [Module("inventoryexpress")]
    [Context("inventorydetails")]
    [Context("attachment")]
    [Context("journal")]
    [Context("inventoryedit")]
    public sealed class ComponentSidebarInventoryMedia : ComponentControlLink
    {
        /// <summary>
        /// Das Bild
        /// </summary>
        private ControlImage Image { get; } = new ControlImage()
        {
            Width = 180,
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two)
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentSidebarInventoryMedia()
        {
            Content.Add(Image);
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
            var guid = context.Request.GetParameter("InventoryID")?.Value;
            var inventory = ViewModel.GetInventory(guid);

            Modal = new PropertyModal(TypeModal.Formular, TypeModalSize.Large);
            Uri = context.Uri.Root.Append(guid).Append("media");
            Image.Uri = new UriRelative(inventory.Media?.Uri);

            return base.Render(context);
        }
    }
}
