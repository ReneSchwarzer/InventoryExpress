using InventoryExpress.Model;
using InventoryExpress.WebControl;
using System;
using System.IO;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("LedgerAccountEdit")]
    [Title("inventoryexpress:inventoryexpress.ledgeraccount.edit.label")]
    [SegmentGuid("LedgerAccountID", "inventoryexpress:inventoryexpress.ledgeraccount.edit.display")]
    [Path("/LedgerAccount")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("ledgeraccountedit")]
    public sealed class PageLedgerAccountEdit : PageWebApp, IPageLedgerAccount
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularLedgerAccount Form { get; } = new ControlFormularLedgerAccount("ledgeraccount")
        {
            Edit = true
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
        /// <param name="context">Der Kontext</param>
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
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular gefüllt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
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
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("LedgerAccountID")?.Value;
            var ledgerAccount = ViewModel.GetLedgerAccount(guid);

            var file = e.Context.Request.GetParameter(Form.Image.Name) as ParameterFile;

            using var transaction = ViewModel.BeginTransaction();

            // Sachkonto ändern und speichern
            ledgerAccount.Name = Form.LedgerAccountName.Value;
            ledgerAccount.Description = Form.Description.Value;
            ledgerAccount.Tag = Form.Tag.Value;
            ledgerAccount.Updated = DateTime.Now;

            ViewModel.AddOrUpdateLedgerAccount(ledgerAccount);

            if (file != null)
            {
                ViewModel.AddOrUpdateMedia(ledgerAccount.Media, file);
            }

            transaction.Commit();

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.ledgeraccount.notification.edit"),
                    new ControlLink()
                    {
                        Text = ledgerAccount.Name,
                        Uri = new UriRelative(ViewModel.GetSupplierUri(ledgerAccount.Id))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(ledgerAccount.Image),
                durability: 10000
            );
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var guid = context.Request.GetParameter("LedgerAccountID")?.Value;
            var ledgerAccount = ViewModel.GetLedgerAccount(guid);

            context.Request.Uri.Display = ledgerAccount.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
