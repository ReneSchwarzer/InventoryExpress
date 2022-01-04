using InventoryExpress.WebControl;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebAttribute;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebComponent
{
    [Section(Section.HeadlineSecondary)]
    [Module("inventoryexpress")]
    [Context("condition")]
    public sealed class ComponentHeadlineConditionAdd : ComponentControlButtonLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentHeadlineConditionAdd()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
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
            Text = I18N(context.Culture, "inventoryexpress:inventoryexpress.condition.add.label");
            Icon = new PropertyIcon(TypeIcon.Plus);
            BackgroundColor = new PropertyColorButton(TypeColorButton.Primary);

            Modal = new ControlModalFormularConditionEdit();

            return base.Render(context);
        }
    }
}
