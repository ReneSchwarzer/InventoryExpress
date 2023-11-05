using InventoryExpress.WebPageSetting;
using WebExpress.WebHtml;
using WebExpress.WebUI.WebAttribute;
using WebExpress.WebUI.WebControl;
using WebExpress.WebUI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WeFragment
{
    [Section(Section.HeadlineSecondary)]
    [Module<Module>]
    [Scope<IPageCondition>]
    public sealed class FragmentHeadlineConditionAdd : FragmentControlButtonLink
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentHeadlineConditionAdd()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            Text = "inventoryexpress:inventoryexpress.condition.add.label";
            Icon = new PropertyIcon(TypeIcon.Plus);
            BackgroundColor = new PropertyColorButton(TypeColorButton.Primary);
            Modal = new PropertyModal(TypeModal.Formular, TypeModalSize.Large);
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
            Uri = context.ApplicationContext.ContextPath.Append("setting/conditions/add/");

            return base.Render(context);
        }
    }
}
