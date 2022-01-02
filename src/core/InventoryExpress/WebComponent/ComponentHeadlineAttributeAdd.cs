using InventoryExpress.WebControl;
using WebExpress.WebAttribute;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebComponent
{
    [Section(Section.HeadlineSecondary)]
    [Module("inventoryexpress")]
    [Context("attribute")]
    public sealed class ComponentHeadlineAttributeAdd : ComponentControlButtonLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentHeadlineAttributeAdd()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IComponentContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Text = I18N(context.Culture, "inventoryexpress:inventoryexpress.attribute.add.label");
            Icon = new PropertyIcon(TypeIcon.Plus);
            BackgroundColor = new PropertyColorButton(TypeColorButton.Primary);

            Modal = new ControlModalFormularAttributeEdit();

            return base.Render(context);
        }
    }
}
