using InventoryExpress.Model;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebMessage;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [WebExSection(Section.SidebarHeader)]
    [WebExModule(typeof(Module))]
    [WebExContext("ledgeraccountedit")]
    public sealed class FragmentSidebarMediaLedgerAccount : FragmentSidebarMedia
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentSidebarMediaLedgerAccount()
        {
            Form.Header = "inventoryexpress:inventoryexpress.ledgeraccount.media.label";
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">The page where the component is active.</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Upload-Ereignis ausgelöst wurde
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Das Eventargument</param>
        protected override void OnUpload(object sender, FormularUploadEventArgs e)
        {
            var file = e.Context.Request.GetParameter(Form.File.Name) as ParameterFile;
            var guid = e.Context.Request.GetParameter("LedgerAccountId")?.Value;
            var ledgerAccount = ViewModel.GetLedgerAccount(guid);

            if (file != null)
            {
                using var transaction = ViewModel.BeginTransaction();

                ViewModel.AddOrUpdateMedia(ledgerAccount, file);

                transaction.Commit();
            }

            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(e.Context.Culture, "inventoryexpress:inventoryexpress.media.notification.edit"),
                    new ControlLink()
                    {
                        Text = ledgerAccount.Name,
                        Uri = ViewModel.GetLedgerAccountUri(ledgerAccount.Id)
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: ViewModel.GetMediaUri(ledgerAccount.Media.Id),
                durability: 10000
            );
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is represented.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var guid = context.Request.GetParameter("LedgerAccountId")?.Value;
            var ledgerAccount = ViewModel.GetLedgerAccount(guid);

            Image.Uri = ledgerAccount?.Image;

            return base.Render(context);
        }
    }
}
