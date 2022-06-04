using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

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
            Text = "inventoryexpress:inventoryexpress.inventory.attachment.back";
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
            Uri = context.Uri.Take(-1);

            return base.Render(context);
        }
    }
}
