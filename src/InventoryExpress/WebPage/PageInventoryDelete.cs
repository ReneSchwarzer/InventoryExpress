using InventoryExpress.Model;
using InventoryExpress.Parameter;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebCore.Internationalization;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebComponent;
using WebExpress.WebCore.WebResource;
using WebExpress.WebUI.WebControl;

namespace InventoryExpress.WebPage
{
    [Title("inventoryexpress:inventoryexpress.inventory.delete.label")]
    [Segment("del", "inventoryexpress:inventoryexpress.inventory.delete.display")]
    [ContextPath("/")]
    [Parent<PageInventoryDetails>]
    [Module<Module>]
    public sealed class PageInventoryDelete : PageWebAppFormularConfirm<ControlFormularConfirmDelete>, IPageInventory
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageInventoryDelete()
            : base("inventory")
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            SetRedirectUri(ComponentManager.SitemapManager.GetUri<PageInventories>(context));
        }

        /// <summary>
        /// Invoked when the form is initialized.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        protected override void OnInitializeFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterInventoryId>()?.Value;
            var inventory = ViewModel.GetInventory(guid);

            SetDescription(InternationalizationManager.I18N
            (
                "inventoryexpress:inventoryexpress.inventory.delete.description",
                inventory?.Name
            ));
        }

        /// <summary>
        /// Triggered when the form is confirmed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        protected override void OnConfirmFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterInventoryId>()?.Value;
            var inventory = ViewModel.GetInventory(guid);

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.DeleteInventory(inventory);

                transaction.Commit();
            }

            AddNotification
            (
                e.Context,
                "inventoryexpress:inventoryexpress.inventory.notification.delete",
                inventory.Name,
                new PropertyColorText(TypeColorText.Danger),
                inventory.Image
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
