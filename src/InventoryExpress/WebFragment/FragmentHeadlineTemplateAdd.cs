using WebExpress.WebApp.WebFragment;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebHtml;
using WebExpress.WebCore.WebPage;
using WebExpress.WebCore.WebScope;
using WebExpress.WebUI.WebAttribute;
using WebExpress.WebUI.WebControl;
using WebExpress.WebUI.WebFragment;

namespace InventoryExpress.WebFragment
{
    [Section(Section.HeadlineSecondary)]
    [Module<Module>]
    [Scope<IScope>]
    public sealed class FragmentHeadlineTemplateAdd : FragmentControlButtonLink
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentHeadlineTemplateAdd()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            Text = "inventoryexpress:inventoryexpress.add.label";
            Icon = new PropertyIcon(TypeIcon.Plus);
            BackgroundColor = new PropertyColorButton(TypeColorButton.Primary);
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">The page where the component is active.</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Uri = context.Uri.Append("add");

            return base.Render(context);
        }
    }
}
