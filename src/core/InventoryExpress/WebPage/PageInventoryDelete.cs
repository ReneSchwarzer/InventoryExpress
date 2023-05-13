using InventoryExpress.Model;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [WebExID("InventoryDelete")]
    [WebExTitle("inventoryexpress:inventoryexpress.inventory.delete.label")]
    [WebExSegment("del", "inventoryexpress:inventoryexpress.inventory.delete.display")]
    [WebExContextPath("/")]
    [WebExParent("InventoryDetails")]
    [WebExModule("inventoryexpress")]
    [WebExContext("general")]
    [WebExContext("Inventorydelete")]
    public sealed class PageInventoryDelete : PageWebApp, IPageInventory
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularConfirmDelete Form { get; } = new ControlFormularConfirmDelete("inventory")
        {
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryDelete()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.RedirectUri = ResourceContext.ContextPath;
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
            var guid = e.Context.Request.GetParameter("InventoryID")?.Value;
            var inventory = ViewModel.GetInventory(guid);
            Form.Content.Text = string.Format(InternationalizationManager.I18N("inventoryexpress:inventoryexpress.inventory.delete.description"), inventory?.Name);
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular bestätigt urde.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Die Eventargumente/param>
        private void OnConfirmFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("InventoryID")?.Value;
            var inventory = ViewModel.GetInventory(guid);

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.DeleteInventory(inventory);

                transaction.Commit();
            }

            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.inventory.notification.delete"),
                    new ControlText()
                    {
                        Text = inventory.Name,
                        TextColor = new PropertyColorText(TypeColorText.Danger),
                        Format = TypeFormatText.Span
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: inventory.Image,
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
