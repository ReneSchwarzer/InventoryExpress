using InventoryExpress.Model;
using InventoryExpress.Parameter;
using WebExpress.Internationalization;
using WebExpress.WebUI.WebControl;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [Title("inventoryexpress:inventoryexpress.ledgeraccount.delete.label")]
    [Segment("del", "inventoryexpress:inventoryexpress.ledgeraccount.delete.display")]
    [ContextPath("/")]
    [Parent<PageLedgerAccountEdit>]
    [Module<Module>]
    public sealed class PageLedgerAccountDelete : PageWebAppFormularConfirm<ControlFormularConfirmDelete>, IPageLedgerAccount
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageLedgerAccountDelete()
            : base("ledgeraccount")
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            SetDescription(ComponentManager.SitemapManager.GetUri<PageLedgerAccounts>(context));
        }

        /// <summary>
        /// Invoked when the form is initialized.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        protected override void OnInitializeFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterLedgerAccountId>()?.Value;
            var ledgeraccount = ViewModel.GetLedgerAccount(guid);

            SetDescription(InternationalizationManager.I18N
            (
                "inventoryexpress:inventoryexpress.ledgeraccount.delete.description",
                ledgeraccount?.Name
            ));
        }

        /// <summary>
        /// Triggered when the form is confirmed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        protected override void OnConfirmFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterLedgerAccountId>()?.Value;
            var ledgeraccount = ViewModel.GetLedgerAccount(guid);

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.DeleteLedgerAccount(guid);

                transaction.Commit();
            }

            AddNotification
            (
                e.Context,
                "inventoryexpress:inventoryexpress.ledgeraccount.notification.delete",
                ledgeraccount.Name,
                new PropertyColorText(TypeColorText.Danger),
                ledgeraccount.Image
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
