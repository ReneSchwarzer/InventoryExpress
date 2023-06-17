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
    [WebExTitle("inventoryexpress:inventoryexpress.attribute.delete.label")]
    [WebExSegmentGuid<ParameterAttributeId>("inventoryexpress:inventoryexpress.attribute.delete.display")]
    [WebExContextPath("/del")]
    [WebExParent<PageSettingAttributes>]
    [WebExSettingHide()]
    [WebExSettingContext("webexpress.webapp:setting.tab.general.label")]
    [WebExModule<Module>]
    public sealed class PageSettingAttributeDelete : PageWebAppSetting, IPageAttribute
    {
        /// <summary>
        /// Returns the form
        /// </summary>
        private ControlFormularConfirmDelete Form { get; } = new ControlFormularConfirmDelete("attribute")
        {
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public PageSettingAttributeDelete()
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
            Form.RedirectUri = ResourceContext.ContextPath.Append("setting/attributes");
        }

        /// <summary>
        /// Invoked when the form is initialized.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Triggered when the form is confirmed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        private void OnConfirmFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter<ParameterAttributeId>()?.Value;
            var attribute = ViewModel.GetAttribute(guid);

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.DeleteAttribute(guid);

                transaction.Commit();
            }

            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.attribute.notification.delete"),
                    new ControlText()
                    {
                        Text = attribute.Name,
                        TextColor = new PropertyColorText(TypeColorText.Danger),
                        Format = TypeFormatText.Span
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: attribute.Image,
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
