using InventoryExpress.Model;
using InventoryExpress.Parameter;
using WebExpress.Internationalization;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [Title("inventoryexpress:inventoryexpress.template.delete.label")]
    [Segment("del")]
    [ContextPath("/")]
    [Parent<PageSettingTemplates>]
    [WebExSettingHide()]
    [WebExSettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module<Module>]
    public sealed class PageSettingTemplateDelete : PageWebAppSetting, IPageTemplate
    {
        /// <summary>
        /// Returns the form
        /// </summary>
        private ControlFormularConfirmDelete Form { get; } = new ControlFormularConfirmDelete("template")
        {
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public PageSettingTemplateDelete()
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
            Form.Confirm += OnConfirmFormular;
            Form.RedirectUri = ResourceContext.ContextPath.Append("setting/templates");
        }

        /// <summary>
        /// Invoked when the form is initialized.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterTemplateId>()?.Value;
            var template = ViewModel.GetTemplate(guid);

            Form.Content.Text = string.Format(InternationalizationManager.I18N(e.Context, "inventoryexpress:inventoryexpress.template.delete.description"), template?.Name);
        }

        /// <summary>
        /// Triggered when the form is confirmed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        private void OnConfirmFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterTemplateId>()?.Value;
            var template = ViewModel.GetTemplate(guid);

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.DeleteTemplate(guid);

                transaction.Commit();
            }

            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.template.notification.delete"),
                    new ControlText()
                    {
                        Text = template.Name,
                        TextColor = new PropertyColorText(TypeColorText.Danger),
                        Format = TypeFormatText.Span
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: template.Image,
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
