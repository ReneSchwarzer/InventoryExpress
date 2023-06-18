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
    [Scope<PageLocationEdit>]
    public sealed class FragmentMediaToolEditLocation : FragmentMediaToolEdit
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentMediaToolEditLocation()
            : base("21DBB366-1089-4023-829E-5869E6B5E4BB")
        {
            Form.Header = "inventoryexpress:inventoryexpress.location.media.label";
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
            var guid = e.Context.Request.GetParameter<ParameterLocationId>()?.Value;
            var location = ViewModel.GetLocation(guid);

            if (file != null)
            {
                using var transaction = ViewModel.BeginTransaction();

                ViewModel.AddOrUpdateMedia(location, file);

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
                        Text = location.Name,
                        Uri = ViewModel.GetLocationUri(location.Id)
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: ViewModel.GetMediaUri(location.Media.Id),
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
            var guid = context.Request.GetParameter<ParameterLocationId>()?.Value;
            var location = ViewModel.GetLocation(guid);

            Form.RedirectUri = location?.Uri;

            return base.Render(context);
        }
    }
}
