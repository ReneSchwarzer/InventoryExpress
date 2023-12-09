using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebScope;
using WebExpress.WebCore.Internationalization;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebComponent;
using WebExpress.WebCore.WebResource;
using WebExpress.WebUI.WebControl;

namespace InventoryExpress.WebPage
{
    [Title("inventoryexpress:inventoryexpress.ledgeraccount.add.label")]
    [Segment("add", "inventoryexpress:inventoryexpress.ledgeraccount.add.label")]
    [ContextPath("/")]
    [Parent<PageLedgerAccounts>]
    [Module<Module>]
    [Scope<ScopeGeneral>]
    public sealed class PageLedgerAccountAdd : PageWebApp, IPageLedgerAccount
    {
        /// <summary>
        /// Returns the form.
        /// </summary>
        private ControlFormularLedgerAccount Form { get; } = new ControlFormularLedgerAccount("ledgeraccount")
        {
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public PageLedgerAccountAdd()
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
            Form.RedirectUri = ApplicationContext.ContextPath.Append("ledgeraccounts");
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
            // create and save a new ledger account
            var ledgeraccount = new WebItemEntityLedgerAccount()
            {
                Name = Form.LedgerAccountName.Value,
                Description = Form.Description.Value,
                Tag = Form.Tag.Value
            };

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.AddOrUpdateLedgerAccount(ledgeraccount);

                transaction.Commit();
            }

            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.ledgeraccount.notification.add"),
                    new ControlLink()
                    {
                        Text = ledgeraccount.Name,
                        Uri = ViewModel.GetLedgerAccountUri(ledgeraccount.Guid)
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: ledgeraccount.Image,
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