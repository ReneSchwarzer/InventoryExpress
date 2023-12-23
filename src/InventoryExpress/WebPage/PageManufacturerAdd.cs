using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebCore.Internationalization;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebComponent;
using WebExpress.WebCore.WebResource;
using WebExpress.WebUI.WebControl;

namespace InventoryExpress.WebPage
{
    [Title("inventoryexpress:inventoryexpress.manufacturer.add.label")]
    [Segment("add", "inventoryexpress:inventoryexpress.manufacturer.add.label")]
    [ContextPath("/")]
    [Parent<PageManufacturers>]
    [Module<Module>]
    public sealed class PageManufacturerAdd : PageWebApp, IPageManufacturer
    {
        /// <summary>
        /// Returns the form.
        /// </summary>
        private ControlFormularManufacturer Form { get; } = new ControlFormularManufacturer("manufacturer")
        {
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public PageManufacturerAdd()
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
            Form.RedirectUri = ApplicationContext.ContextPath.Append("manufacturers");
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
            // creating and saving a new manufacturer object
            var manufacturer = new WebItemEntityManufacturer()
            {
                Name = Form.ManufacturerName.Value,
                Description = Form.Description.Value,
                Address = Form.Address.Value,
                Zip = Form.Zip.Value,
                Place = Form.Place.Value,
                Tag = Form.Tag.Value
            };

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.AddOrUpdateManufacturer(manufacturer);

                transaction.Commit();
            }

            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.manufacturer.notification.add"),
                    new ControlLink()
                    {
                        Text = manufacturer.Name,
                        Uri = manufacturer.Uri
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: manufacturer.Image,
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
