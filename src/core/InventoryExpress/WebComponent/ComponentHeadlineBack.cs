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
    [Section(Section.HeadlinePrologue)]
    [Module("inventoryexpress")]
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
    public sealed class ComponentHeadlineBack : ComponentControlButtonLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentHeadlineBack()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            Icon = new PropertyIcon(TypeIcon.ArrowLeft);
            Outline = true;
            BackgroundColor = new PropertyColorButton(TypeColorButton.Secondary);
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
            Text = I18N(context.Culture, "inventoryexpress:inventoryexpress.inventory.attachment.back");
            Uri = context.Uri.Take(-1);

            return base.Render(context);
        }
    }
}
