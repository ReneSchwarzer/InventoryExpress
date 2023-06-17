using InventoryExpress.Model;
using InventoryExpress.Parameter;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [WebExTitle("inventoryexpress:inventoryexpress.costcenter.delete.label")]
    [WebExSegment("del", "inventoryexpress:inventoryexpress.costcenter.delete.display")]
    [WebExContextPath("/")]
    [WebExParent<PageCostCenters>]
    [WebExModule<Module>]
    public sealed class PageCostCenterDelete : PageWebAppFormularConfirm<ControlFormularConfirmDelete>, IPageCostCenter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageCostCenterDelete()
            : base("costcenter")
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            SetDescription(ComponentManager.SitemapManager.GetUri<PageCostCenters>(context));
        }

        /// <summary>
        /// Invoked when the form is initialized.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        protected override void OnInitializeFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterCostCenterId>()?.Value;
            var costcenter = ViewModel.GetCostCenter(guid);

            SetDescription(InternationalizationManager.I18N
            (
                "inventoryexpress:inventoryexpress.costcenter.delete.description",
                costcenter?.Name
            ));
        }

        /// <summary>
        /// Triggered when the form is confirmed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        protected override void OnConfirmFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterCostCenterId>()?.Value;
            var costcenter = ViewModel.GetCostCenter(guid);

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.DeleteCostCenter(guid);

                transaction.Commit();
            }

            AddNotification
            (
                e.Context,
                "inventoryexpress:inventoryexpress.costcenter.notification.delete",
                costcenter.Name,
                new PropertyColorText(TypeColorText.Danger),
                costcenter.Image
            );
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);
        }
    }
}
