using InventoryExpress.Model;
using InventoryExpress.Parameter;
using WebExpress.Internationalization;
using WebExpress.WebUI.WebControl;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebResource;
using WebExpress.WebScope;

namespace InventoryExpress.WebPage
{
    [Title("inventoryexpress:inventoryexpress.manufacturer.delete.label")]
    [Segment("del", "inventoryexpress:inventoryexpress.manufacturer.delete.display")]
    [ContextPath("/")]
    [Parent<PageManufacturers>]
    [Module<Module>]
    public sealed class PageManufacturerDelete : PageWebAppFormularConfirm<ControlFormularConfirmDelete>, IPageManufacturer, IScope
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageManufacturerDelete()
            : base("manufacturer")
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            SetDescription(ComponentManager.SitemapManager.GetUri<PageManufacturers>(context));
        }

        /// <summary>
        /// Invoked when the form is initialized.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        protected override void OnInitializeFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterManufacturerId>()?.Value;
            var manufacturer = ViewModel.GetManufacturer(guid);

            SetDescription(InternationalizationManager.I18N
            (
                "inventoryexpress:inventoryexpress.manufacturer.delete.description",
                manufacturer?.Name
            ));
        }

        /// <summary>
        /// Triggered when the form is confirmed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        protected override void OnConfirmFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterManufacturerId>()?.Value;
            var manufacturer = ViewModel.GetManufacturer(guid);

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.DeleteManufacturer(guid);

                transaction.Commit();
            }

            AddNotification
            (
                e.Context,
                "inventoryexpress:inventoryexpress.manufacturer.notification.delete",
                manufacturer.Name,
                new PropertyColorText(TypeColorText.Danger),
                manufacturer.Image
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
