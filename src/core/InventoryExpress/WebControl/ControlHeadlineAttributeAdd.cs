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
    [Context("attribute")]
    public sealed class ControlHeadlineAttributeAdd : ControlButtonLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlHeadlineAttributeAdd()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Text = context.Page.I18N("inventoryexpress.attribute.add.label");
            Icon = new PropertyIcon(TypeIcon.Plus);
            BackgroundColor = new PropertyColorButton(TypeColorButton.Primary);

            Modal = new ControlModalFormularAttributeEdit();

            return base.Render(context);
        }
    }
}
