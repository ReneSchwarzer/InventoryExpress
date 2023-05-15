using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [WebExTitle("inventoryexpress:inventoryexpress.ledgeraccount.edit.label")]
    [WebExSegmentGuid("LedgerAccountID", "inventoryexpress:inventoryexpress.ledgeraccount.edit.display")]
    [WebExContextPath("/")]
    [WebExParent(typeof(PageLedgerAccounts))]
    [WebExModule(typeof(Module))]
    [WebExContext("general")]
    [WebExContext("ledgeraccountedit")]
    public sealed class PageLedgerAccountEdit : PageWebApp, IPageLedgerAccount
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
        public PageLedgerAccountEdit()
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
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular gefüllt werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("LedgerAccountID")?.Value;
            var ledgerAccount = ViewModel.GetLedgerAccount(guid);

            Form.LedgerAccountName.Value = ledgerAccount?.Name;
            Form.Description.Value = ledgerAccount?.Description;
            Form.Tag.Value = ledgerAccount?.Tag;
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("LedgerAccountID")?.Value;
            var ledgerAccount = ViewModel.GetLedgerAccount(guid);

            // Sachkonto ändern und speichern
            ledgerAccount.Name = Form.LedgerAccountName.Value;
            ledgerAccount.Description = Form.Description.Value;
            ledgerAccount.Tag = Form.Tag.Value;
            ledgerAccount.Updated = DateTime.Now;

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.AddOrUpdateLedgerAccount(ledgerAccount);

                transaction.Commit();
            }

            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.ledgeraccount.notification.edit"),
                    new ControlLink()
                    {
                        Text = ledgerAccount.Name,
                        Uri = ViewModel.GetSupplierUri(ledgerAccount.Id)
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: ledgerAccount.Image,
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

            var guid = context.Request.GetParameter("LedgerAccountID")?.Value;
            var ledgerAccount = ViewModel.GetLedgerAccount(guid);

            Uri.Display = ledgerAccount.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
