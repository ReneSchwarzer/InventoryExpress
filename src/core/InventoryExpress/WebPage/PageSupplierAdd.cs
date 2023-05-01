using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [WebExID("SupplierAdd")]
    [WebExTitle("inventoryexpress:inventoryexpress.supplier.add.label")]
    [WebExSegment("add", "inventoryexpress:inventoryexpress.supplier.add.label")]
    [WebExContextPath("/")]
    [WebExParent("Supplier")]
    [WebExModule("inventoryexpress")]
    [WebExContext("general")]
    public sealed class PageSupplierAdd : PageWebApp, IPageSupplier
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularSupplier Form { get; } = new ControlFormularSupplier("supplier")
        {
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSupplierAdd()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.InitializeFormular += InitializeFormular;
            Form.FillFormular += FillFormular;
            Form.ProcessFormular += ProcessFormular;
            Form.RedirectUri = ResourceContext.ContextPath.Append("suppliers");
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular gefüllt werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            // Neuen Liferanten erstellen und speichern
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

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.supplier.notification.add"),
                    new ControlLink()
                    {
                        Text = supplier.Name,
                        Uri = ViewModel.GetSupplierUri(supplier.Id)
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
