using InventoryExpress.Model;
using InventoryExpress.Parameter;
using InventoryExpress.WebResource;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [WebExTitle("inventoryexpress:inventoryexpress.media.delete.label")]
    [WebExSegment("del", "inventoryexpress:inventoryexpress.media.delete.display")]
    [WebExContextPath("/")]
    [WebExParent<ResourceMedia>]
    [WebExModule<Module>]
    public sealed class PageMediaDelete : PageWebAppFormularConfirm<ControlFormularConfirmDelete>, IPageInventory
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageMediaDelete()
            : base("media")
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            SetRedirectUri(ComponentManager.SitemapManager.GetUri<PageInventories>());
        }

        /// <summary>
        /// Invoked when the form is initialized.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        protected override void OnInitializeFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterMediaId>()?.Value;
            var media = ViewModel.GetInventory(guid);

            SetDescription(InternationalizationManager.I18N
            (
                "inventoryexpress:inventoryexpress.media.delete.description",
                media?.Name
            ));
        }

        /// <summary>
        /// Triggered when the form is confirmed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        protected override void OnConfirmFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterMediaId>()?.Value;
            var media = ViewModel.GetMedia(guid);

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.DeleteMedia(media?.Id);

                transaction.Commit();
            }

            AddNotification
            (
                e.Context,
                "inventoryexpress:inventoryexpress.media.notification.delete",
                media.Name,
                new PropertyColorText(TypeColorText.Danger),
                media.Image
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
