using InventoryExpress.Model;
using System.Linq;
using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Components;

namespace InventoryExpress.WebControl
{
    [Section(Section.HeadlinePreferences)]
    [Application("InventoryExpress")]
    [Context("inventorydetails")]
    public sealed class ControlHeadlineAttachment : ControlLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlHeadlineAttachment()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            Icon = new PropertyIcon(TypeIcon.PaperClip);
            Size = new PropertySizeText(TypeSizeText.Small);
            TextColor = new PropertyColorText(TypeColorText.Secondary);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            lock (ViewModel.Instance.Database)
            {
                var guid = context.Page.GetParamValue("InventoryID");
                var count = (from i in ViewModel.Instance.Inventories
                             join a in ViewModel.Instance.InventoryAttachment
                             on i.Id equals a.InventoryId
                             where i.Guid == guid
                             select a).Count();

                Title = context.Page.I18N("inventoryexpress.inventory.attachment.function") + $" ({ count })";
                Styles.Add(count == 0 ? "display: none;" : string.Empty);
                Uri = context.Uri.Append("attachments");
            }

            return base.Render(context);
        }
    }
}
