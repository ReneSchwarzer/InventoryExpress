using InventoryExpress.Model;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Components;

namespace InventoryExpress.WebControl
{
    [Section(Section.SidebarPreferences)]
    [Application("InventoryExpress")]
    [Context("inventorydetails")]
    public sealed class ControlSidebarInventoryMedia : ControlLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlSidebarInventoryMedia()
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var guid = context.Page.GetParamValue("InventoryID");
            var inventory = ViewModel.Instance.Inventories.Where(x => x.Guid == guid).FirstOrDefault();
            var media = ViewModel.Instance.Media.Where(x => x.Id == (inventory != null ? inventory.MediaId : null)).FirstOrDefault();
            var image = media != null ? context.Uri.Root.Append("media").Append(media.Guid) : null;

            Uri = context.Uri.Append("media");
            
            Content.Add(new ControlImage()
            {
                Uri = image == null ? context.Uri.Root.Append("/assets/img/inventoryexpress.svg") : image,
                Width = 180,
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two)
            });

            return base.Render(context);
        }
    }
}
