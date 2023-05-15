using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebFragment;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [WebExSection(Section.HeadlineSecondary)]
    [WebExModule(typeof(Module))]
    [WebExContext("template")]
    public sealed class FragmentHeadlineTemplateAdd : FragmentControlButtonLink
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public FragmentHeadlineTemplateAdd()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            Text = "inventoryexpress:inventoryexpress.add.label";
            Icon = new PropertyIcon(TypeIcon.Plus);
            BackgroundColor = new PropertyColorButton(TypeColorButton.Primary);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IFragmentContext context, IPage page)
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
            Uri = context.Uri.Append("add");

            return base.Render(context);
        }
    }
}
