using InventoryExpress.Model;
using InventoryExpress.Parameter;
using InventoryExpress.WebControl;
using System.Linq;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebCore.Internationalization;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebComponent;
using WebExpress.WebCore.WebResource;
using WebExpress.WebCore.WebScope;
using WebExpress.WebUI.WebControl;

namespace InventoryExpress.WebPage
{
    [Title("inventoryexpress:inventoryexpress.inventory.edit.label")]
    [Segment("edit", "inventoryexpress:inventoryexpress.inventory.edit.display")]
    [ContextPath("/")]
    [Parent<PageInventoryDetails>]
    [Module<Module>]
    //[Cache]
    public sealed class PageInventoryEdit : PageWebApp, IPageInventory, IScope
    {
        /// <summary>
        /// Returns the formular.
        /// </summary>
        private readonly ControlFormularInventory Form = new("inventory")
        {
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public PageInventoryEdit()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.InitializeFormular += OnInitializeFormular;
            Form.FillFormular += OnFillFormular;
            Form.ProcessFormular += OnProcessFormular;
        }

        /// <summary>
        /// Initializes the form.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void OnInitializeFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterInventoryId>()?.Value;

            Form.RedirectUri = ComponentManager.SitemapManager.GetUri<PageInventoryDetails>(new ParameterInventoryId(guid));
        }

        /// <summary>
        /// Called when the form is to be filled initially.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void OnFillFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterInventoryId>()?.Value;
            var inventory = ViewModel.GetInventory(guid);

            Form.Fill(inventory, e.Context.Culture);
        }

        /// <summary>
        /// Dispatched when the form inputs are to be processed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void OnProcessFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterInventoryId>()?.Value;
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
                        Uri = ViewModel.GetInventoryUri(inventory.Guid)
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

            // create attributes in the form
            context.VisualTree.Content.Primary.Add(Form);
            context.Uri.Display = context.Request.GetParameter<ParameterInventoryId>()?.Value.Split('-').LastOrDefault();
        }
    }
}
