using InventoryExpress.WebPage;
using InventoryExpress.WebPageSetting;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [WebExSection(Section.HeadlinePrologue)]
    [WebExModule<Module>]
    [WebExScope<PageInventoryAttachments>]
    [WebExScope<PageInventoryEdit>]
    [WebExScope<PageCostCenterEdit>]
    [WebExScope<PageLedgerAccountEdit>]
    [WebExScope<PageLocationEdit>]
    [WebExScope<PageManufacturerEdit>]
    [WebExScope<PageSupplierEdit>]
    [WebExScope<PageSettingTemplateEdit>]
    [WebExScope<PageSettingTemplateAdd>]
    [WebExScope<PageInventoryJournal>]
    public sealed class FragmentHeadlineBack : FragmentControlButtonLink
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentHeadlineBack()
        {
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            Icon = new PropertyIcon(TypeIcon.ArrowLeft);
            Outline = true;
            BackgroundColor = new PropertyColorButton(TypeColorButton.Secondary);
            Text = "inventoryexpress:inventoryexpress.inventory.attachment.back";
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
            Uri = context.Uri.Take(-1);

            return base.Render(context);
        }
    }
}
