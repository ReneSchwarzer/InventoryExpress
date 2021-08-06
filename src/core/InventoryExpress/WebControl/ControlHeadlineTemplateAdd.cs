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
    [Context("template")]
    public sealed class ControlHeadlineTemplateAdd : ControlButtonLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlHeadlineTemplateAdd()
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
            Text = context.Page.I18N("inventoryexpress.add.label");
            Icon = new PropertyIcon(TypeIcon.Plus);
            BackgroundColor = new PropertyColorButton(TypeColorButton.Primary);
            Uri = context.Uri.Append("add");

            return base.Render(context);
        }
    }
}
