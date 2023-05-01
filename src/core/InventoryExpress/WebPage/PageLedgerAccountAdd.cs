using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [WebExID("LedgerAccountAdd")]
    [WebExTitle("inventoryexpress:inventoryexpress.ledgeraccount.add.label")]
    [WebExSegment("add", "inventoryexpress:inventoryexpress.ledgeraccount.add.label")]
    [WebExContextPath("/")]
    [WebExParent("LedgerAccount")]
    [WebExModule("inventoryexpress")]
    [WebExContext("general")]
    public sealed class PageLedgerAccountAdd : PageWebApp, IPageLedgerAccount
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularLedgerAccount Form { get; } = new ControlFormularLedgerAccount("ledgeraccount")
        {
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageLedgerAccountAdd()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.InitializeFormular += InitializeFormular;
            Form.FillFormular += FillFormular;
            Form.ProcessFormular += ProcessFormular;
            Form.RedirectUri = ResourceContext.ContextPath.Append("ledgeraccounts");
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular gefüllt werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            // Neues Sachkonto erstellen und speichern
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

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.ledgeraccount.notification.add"),
                    new ControlLink()
                    {
                        Text = ledgeraccount.Name,
                        Uri = ViewModel.GetLedgerAccountUri(ledgeraccount.Id)
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