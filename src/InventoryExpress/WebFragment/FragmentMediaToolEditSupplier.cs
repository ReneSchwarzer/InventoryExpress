using InventoryExpress.Model;
using InventoryExpress.Parameter;
using InventoryExpress.WebPage;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebMessage;
using WebExpress.WebPage;

namespace InventoryExpress.WebFragment
{
    [WebExSection("mediatool.primary")]
    [Module<Module>]
    [Scope<PageSupplierEdit>]
    public sealed class FragmentMediaToolEditSupplier : FragmentMediaToolEdit
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentMediaToolEditSupplier()
            : base("6662858D-8014-4AE9-9E35-8FCDD39B525D")
        {
            Form.Header = "inventoryexpress:inventoryexpress.supplier.media.label";
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
        /// Fired when the upload event was triggered.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        protected override void OnUpload(object sender, FormularUploadEventArgs e)
        {
            var file = e.Context.Request.GetParameter(Form.File.Name) as ParameterFile;
            var guid = e.Context.Request.GetParameter<ParameterSupplierId>()?.Value;
            var supplier = ViewModel.GetSupplier(guid);

            if (file != null)
            {
                using var transaction = ViewModel.BeginTransaction();

                ViewModel.AddOrUpdateMedia(supplier, file);

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
                        Text = supplier.Name,
                        Uri = ViewModel.GetSupplierUri(supplier.Id)
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: ViewModel.GetMediaUri(supplier.Media.Id),
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
            var guid = context.Request.GetParameter<ParameterSupplierId>()?.Value;
            var supplier = ViewModel.GetSupplier(guid);

            Form.RedirectUri = supplier?.Uri;

            return base.Render(context);
        }
    }
}
