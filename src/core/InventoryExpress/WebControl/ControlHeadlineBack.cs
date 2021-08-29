using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.Components;

namespace InventoryExpress.WebControl
{
    [Section(Section.HeadlinePrologue)]
    [Application("InventoryExpress")]
    [Context("attachment")]
    [Context("inventoryedit")]
    [Context("costcenteredit")]
    [Context("ledgeraccountedit")]
    [Context("locationedit")]
    [Context("manufactureredit")]
    [Context("supplieredit")]
    [Context("templateedit")]
    [Context("templateadd")]
    [Context("mediaedit")]
    [Context("journal")]
    public sealed class ControlHeadlineBack : ControlButtonLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlHeadlineBack()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            Icon = new PropertyIcon(TypeIcon.ArrowLeft);
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
            Text = context.Page.I18N("inventoryexpress.inventory.attachment.back");
            Uri = context.Uri.Take(-1);

            return base.Render(context);
        }
    }
}
