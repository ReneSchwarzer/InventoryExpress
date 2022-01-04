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
    [Context("inventorydetails")]
    public sealed class ComponentHeadlineInventoryEdit : ComponentControlButtonLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentHeadlineInventoryEdit()
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
            Uri = context.Uri.Append("edit");
            Text = I18N(context.Culture, "inventoryexpress:inventoryexpress.edit.label");
            Icon = new PropertyIcon(TypeIcon.Edit);
            BackgroundColor = new PropertyColorButton(TypeColorButton.Primary);

            return base.Render(context);
        }
    }
}
