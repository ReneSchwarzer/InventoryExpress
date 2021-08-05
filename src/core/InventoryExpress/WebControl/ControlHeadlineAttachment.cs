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
    [Section(Section.HeadlineSecondary)]
    [Application("InventoryExpress")]
    [Context("inventorydetails")]
    public sealed class ControlHeadlineAttachment : ControlButtonLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlHeadlineAttachment()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            Icon = new PropertyIcon(TypeIcon.PaperClip);
            Outline = true;
            BackgroundColor = new PropertyColorButton(TypeColorButton.Secondary);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var guid = context.Page.GetParamValue("InventoryID");
            var count = (from i in ViewModel.Instance.Inventories
                         join a in ViewModel.Instance.InventoryAttachment
                         on i.Id equals a.InventoryId
                         where i.Guid == guid
                         select a).Count();

            Text = context.Page.I18N("inventoryexpress.inventory.attachment.function") + $" ({ count })";
            Uri = context.Uri.Append("attachments");

            return base.Render(context);
        }
    }
}
