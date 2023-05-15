using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [WebExTitle("inventoryexpress:inventoryexpress.inventory.edit.label")]
    [WebExSegment("edit", "inventoryexpress:inventoryexpress.inventory.edit.display")]
    [WebExContextPath("/")]
    [WebExParent(typeof(PageInventoryDetails))]
    [WebExModule(typeof(Module))]
    [WebExContext("general")]
    [WebExContext("inventoryedit")]
    //[Cache]
    public sealed class PageInventoryEdit : PageWebApp, IPageInventory
    {
        /// <summary>
        /// Formular
        /// </summary>
        private readonly ControlFormularInventory Form = new("inventory")
        {
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageInventoryEdit()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.RedirectUri = Uri.Take(-1);
            Form.InitializeFormular += OnInitializeFormular;
            Form.FillFormular += OnFillFormular;
            Form.ProcessFormular += OnProcessFormular;
        }

        /// <summary>
        /// Initialisiert das Formular
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Das Eventargument</param>
        private void OnInitializeFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initial befüllt werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Das Eventargument</param>
        /// <exception cref="NotImplementedException"></exception>
        private void OnFillFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("InventoryID")?.Value;
            var inventory = ViewModel.GetInventory(guid);

            Form.Fill(inventory, e.Context.Culture);
        }

        /// <summary>
        /// Wird ausgelöst, wenn die Formulareingaben verarbeitet werden sollen
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Das Eventargument</param>
        private void OnProcessFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("InventoryID")?.Value;
            var inventory = ViewModel.GetInventory(guid);
            Form.Apply(inventory, e.Context.Culture);

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.AddOrUpdateInventory(inventory);

                transaction.Commit();
            }

            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.inventory.notification.edit"),
                    new ControlLink()
                    {
                        Text = inventory.Name,
                        Uri = ViewModel.GetInventoryUri(inventory.Id)
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

            // Attribute im Formular erstellen
            context.VisualTree.Content.Primary.Add(Form);
            Uri.Display = context.Request.GetParameter("InventoryID")?.Value.Split('-').LastOrDefault();
        }
    }
}
