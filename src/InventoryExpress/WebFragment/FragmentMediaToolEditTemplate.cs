using InventoryExpress.Model;
using InventoryExpress.Parameter;
using InventoryExpress.WebPageSetting;
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
    [Scope<PageSettingTemplateEdit>]
    public sealed class FragmentMediaToolEditTemplate : FragmentMediaToolEdit
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentMediaToolEditTemplate()
            : base("E2C47436-A4C7-47D5-A17A-913DD1ED8B8F")
        {
            Form.Header = "inventoryexpress:inventoryexpress.template.media.label";
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
            var guid = e.Context.Request.GetParameter<ParameterTemplateId>()?.Value;
            var template = ViewModel.GetTemplate(guid);

            if (file != null)
            {
                using var transaction = ViewModel.BeginTransaction();

                ViewModel.AddOrUpdateMedia(template, file);

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
                        Text = template.Name,
                        Uri = ViewModel.GetTemplateUri(template.Id)
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: ViewModel.GetMediaUri(template.Media.Id),
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
            var guid = context.Request.GetParameter<ParameterTemplateId>()?.Value;
            var template = ViewModel.GetTemplate(guid);

            Form.RedirectUri = template?.Uri;

            return base.Render(context);
        }
    }
}
