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
    [Title("inventoryexpress:inventoryexpress.location.delete.label")]
    [Segment("del", "inventoryexpress:inventoryexpress.location.delete.display")]
    [ContextPath("/")]
    [Parent<PageLocations>]
    [Module<Module>]
    public sealed class PageLocationDelete : PageWebAppFormularConfirm<ControlFormularConfirmDelete>, IPageLocation
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageLocationDelete()
            : base("location")
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            SetDescription(ComponentManager.SitemapManager.GetUri<PageLocations>(context));
        }

        /// <summary>
        /// Invoked when the form is initialized.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        protected override void OnInitializeFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterLocationId>()?.Value;
            var location = ViewModel.GetLocation(guid);

            SetDescription(InternationalizationManager.I18N
            (
                "inventoryexpress:inventoryexpress.location.delete.description",
                location?.Name
            ));
        }

        /// <summary>
        /// Triggered when the form is confirmed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        protected override void OnConfirmFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterLocationId>()?.Value;
            var location = ViewModel.GetLocation(guid);

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.DeleteLocation(guid);

                transaction.Commit();
            }

            AddNotification
            (
                e.Context,
                "inventoryexpress:inventoryexpress.location.notification.delete",
                location.Name,
                new PropertyColorText(TypeColorText.Danger),
                location.Image
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
