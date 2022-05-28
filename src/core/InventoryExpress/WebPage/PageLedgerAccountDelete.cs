using InventoryExpress.Model;
using System.IO;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPage
{
    [ID("LedgerAccountDelete")]
    [Title("inventoryexpress:inventoryexpress.ledgeraccount.delete.label")]
    [Segment("del", "inventoryexpress:inventoryexpress.ledgeraccount.delete.display")]
    [Path("/LedgerAccount/LedgerAccountEdit")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("ledgeraccountdelete")]
    public sealed class PageLedgerAccountDelete : PageWebApp, IPageLedgerAccount
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularConfirmDelete Form { get; } = new ControlFormularConfirmDelete("ledgeraccount")
        {
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageLedgerAccountDelete()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.RedirectUri = context.ContextPath.Append("ledgeraccounts");
            Form.InitializeFormular += OnInitializeFormular;
            Form.Confirm += OnConfirmFormular;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void OnInitializeFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular bestätigt urde.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void OnConfirmFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("LedgerAccountID")?.Value;
            var ledgeraccount = ViewModel.GetLedgerAccount(guid);

            ViewModel.DeleteLedgerAccount(guid);
            ViewModel.Instance.SaveChanges();

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.ledgeraccount.notification.delete"),
                    new ControlLink()
                    {
                        Text = ledgeraccount.Name,
                        Uri = new UriRelative(ViewModel.GetLedgerAccountUri(ledgeraccount.ID))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(ledgeraccount.Image),
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

            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
