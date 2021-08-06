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
    [Section(Section.HeadlineSecondary)]
    [Application("InventoryExpress")]
    [Context("inventorydetails")]
    public sealed class ControlHeadlineInventoryEdit : ControlButtonLink, IComponent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlHeadlineInventoryEdit()
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
            Uri = context.Page.Uri.Append("edit");
            Text = "Bearbeiten";
            Icon = new PropertyIcon(TypeIcon.Edit);
            BackgroundColor = new PropertyColorButton(TypeColorButton.Primary);

            return base.Render(context);
        }
    }
}
