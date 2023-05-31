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
    [WebExTitle("inventoryexpress:inventoryexpress.ledgeraccount.delete.label")]
    [WebExSegment("del", "inventoryexpress:inventoryexpress.ledgeraccount.delete.display")]
    [WebExContextPath("/")]
    [WebExParent(typeof(PageLedgerAccountEdit))]
    [WebExModule<Module>]
    [WebExContext("general")]
    [WebExContext("ledgeraccountdelete")]
    public sealed class PageLedgerAccountDelete : PageWebApp, IPageLedgerAccount
    {
        /// <summary>
        /// Returns the form
        /// </summary>
        private ControlFormularConfirmDelete Form { get; } = new ControlFormularConfirmDelete("ledgeraccount")
        {
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public PageLedgerAccountDelete()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.RedirectUri = ComponentManager.SitemapManager.GetUri<PageLedgerAccounts>(context);
            Form.InitializeFormular += OnInitializeFormular;
            Form.Confirm += OnConfirmFormular;
        }

        /// <summary>
        /// Invoked when the form is initialized.
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
        /// <param name="e">The event argument./param>
        private void OnConfirmFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("LedgerAccountId")?.Value;
            var ledgeraccount = ViewModel.GetLedgerAccount(guid);

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.DeleteLedgerAccount(guid);

                transaction.Commit();
            }

            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.ledgeraccount.notification.delete"),
                    new ControlText()
                    {
                        Text = ledgeraccount.Name,
                        TextColor = new PropertyColorText(TypeColorText.Danger),
                        Format = TypeFormatText.Span
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
