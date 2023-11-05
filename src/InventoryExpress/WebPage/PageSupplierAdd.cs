using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using WebExpress.Internationalization;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebResource;
using WebExpress.WebUI.WebControl;

namespace InventoryExpress.WebPage
{
    [Title("inventoryexpress:inventoryexpress.supplier.add.label")]
    [Segment("add", "inventoryexpress:inventoryexpress.supplier.add.label")]
    [ContextPath("/")]
    [Parent<PageSuppliers>]
    [Module<Module>]
    public sealed class PageSupplierAdd : PageWebApp, IPageSupplier
    {
        /// <summary>
        /// Returns the form.
        /// </summary>
        private ControlFormularSupplier Form { get; } = new ControlFormularSupplier("supplier")
        {
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public PageSupplierAdd()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.InitializeFormular += InitializeFormular;
            Form.FillFormular += FillFormular;
            Form.ProcessFormular += ProcessFormular;
            Form.RedirectUri = ApplicationContext.ContextPath.Append("suppliers");
        }

        /// <summary>
        /// Invoked when the form is initialized.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Invoked when the form is about to be populated.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Fired when the form is about to be processed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            // create and save a new supplier.
            var supplier = new WebItemEntitySupplier()
            {
                Name = Form.SupplierName.Value,
                Description = Form.Description.Value,
                Address = Form.Address.Value,
                Zip = Form.Zip.Value,
                Place = Form.Place.Value,
                Tag = Form.Tag.Value
            };

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.AddOrUpdateSupplier(supplier);

                transaction.Commit();
            }

            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.supplier.notification.add"),
                    new ControlLink()
                    {
                        Text = supplier.Name,
                        Uri = ViewModel.GetSupplierUri(supplier.Guid)
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: supplier.Image,
                durability: 10000
            );
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
