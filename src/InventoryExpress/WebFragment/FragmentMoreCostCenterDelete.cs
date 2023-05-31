using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [WebExSection(Section.MoreSecondary)]
    [WebExModule(typeof(Module))]
    [WebExContext("costcenteredit")]
    public sealed class FragmentMoreCostCenterDelete : FragmentControlDropdownItemLink
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentMoreCostCenterDelete()
        {
            TextColor = new PropertyColorText(TypeColorText.Danger);
            Text = "inventoryexpress:inventoryexpress.delete.label";
            Icon = new PropertyIcon(TypeIcon.Trash);
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
            var guid = context.Request.GetParameter("CostCenterId")?.Value;
            var costCenter = ViewModel.GetCostCenter(guid);
            var inUse = ViewModel.GetCostCenterInUse(costCenter);

            Active = inUse ? TypeActive.Disabled : TypeActive.None;
            TextColor = inUse ? new PropertyColorText(TypeColorText.Muted) : TextColor;

            Uri = context.Uri.Append("del");
            Modal = new PropertyModal(TypeModal.Formular, TypeModalSize.Default) { RedirectUri = context.ApplicationContext.ContextPath.Append("costcenters") };

            return base.Render(context);
        }
    }
}
