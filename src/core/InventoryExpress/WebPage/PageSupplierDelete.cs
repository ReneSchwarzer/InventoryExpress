using InventoryExpress.Model;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;
using static WebExpress.Internationalization.InternationalizationManager;

namespace InventoryExpress.WebPage
{
    [WebExID("SupplierDelete")]
    [WebExTitle("inventoryexpress:inventoryexpress.supplier.delete.label")]
    [WebExSegment("del", "inventoryexpress:inventoryexpress.supplier.delete.display")]
    [WebExContextPath("/")]
    [WebExParent("SupplierEdit")]
    [WebExModule("inventoryexpress")]
    [WebExContext("general")]
    [WebExContext("supplierdelete")]
    public sealed class PageSupplierDelete : PageWebApp, IPageSupplier
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularConfirmDelete Form { get; } = new ControlFormularConfirmDelete("supplier")
        {
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSupplierDelete()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.RedirectUri = ResourceContext.ContextPath.Append("suppliers");
            Form.InitializeFormular += OnInitializeFormular;
            Form.Confirm += OnConfirmFormular;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void OnInitializeFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular bestätigt urde.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Die Eventargumente/param>
        private void OnConfirmFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("SupplierID")?.Value;
            var supplier = ViewModel.GetSupplier(guid);

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.DeleteSupplier(guid);

                transaction.Commit();
            }

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    I18N(Culture, "inventoryexpress:inventoryexpress.supplier.notification.delete"),
                    new ControlText()
                    {
                        Text = supplier.Name,
                        TextColor = new PropertyColorText(TypeColorText.Danger),
                        Format = TypeFormatText.Span
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
