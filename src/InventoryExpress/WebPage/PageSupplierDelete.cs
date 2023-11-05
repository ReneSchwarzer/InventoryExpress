using InventoryExpress.Model;
using InventoryExpress.Parameter;
using WebExpress.Internationalization;
using WebExpress.WebUI.WebControl;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [Title("inventoryexpress:inventoryexpress.supplier.delete.label")]
    [Segment("del", "inventoryexpress:inventoryexpress.supplier.delete.display")]
    [ContextPath("/")]
    [Parent<PageSuppliers>]
    [Module<Module>]
    public sealed class PageSupplierDelete : PageWebAppFormularConfirm<ControlFormularConfirmDelete>, IPageSupplier
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageSupplierDelete()
            : base("supplier")
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            SetDescription(ComponentManager.SitemapManager.GetUri<PageSuppliers>(context));
        }

        /// <summary>
        /// Invoked when the form is initialized.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        protected override void OnInitializeFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterSupplierId>()?.Value;
            var supplier = ViewModel.GetSupplier(guid);

            SetDescription(InternationalizationManager.I18N
            (
                "inventoryexpress:inventoryexpress.supplier.delete.description",
                supplier?.Name
            ));
        }

        /// <summary>
        /// Triggered when the form is confirmed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        protected override void OnConfirmFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterSupplierId>()?.Value;
            var supplier = ViewModel.GetSupplier(guid);

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.DeleteSupplier(guid);

                transaction.Commit();
            }

            AddNotification
            (
                e.Context,
                "inventoryexpress:inventoryexpress.supplier.notification.delete",
                supplier.Name,
                new PropertyColorText(TypeColorText.Danger),
                supplier.Image
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
