using InventoryExpress.Parameter;
using InventoryExpress.WebPage;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebComponent;
using WebExpress.WebCore.WebHtml;
using WebExpress.WebCore.WebPage;
using WebExpress.WebUI.WebAttribute;
using WebExpress.WebUI.WebControl;
using WebExpress.WebUI.WebFragment;

namespace InventoryExpress.WebFragment
{
    [Section(Section.HeadlineSecondary)]
    [Module<Module>]
    [Scope<PageInventoryDetails>]
    public sealed class FragmentHeadlineInventoryEdit : FragmentControlButtonLink
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentHeadlineInventoryEdit()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            Text = "inventoryexpress:inventoryexpress.edit.label";
            Icon = new PropertyIcon(TypeIcon.Edit);
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
            var guid = context.Request.GetParameter<ParameterInventoryId>();
            Uri = ComponentManager.SitemapManager.GetUri<PageInventoryEdit>(guid);

            return base.Render(context);
        }
    }
}
