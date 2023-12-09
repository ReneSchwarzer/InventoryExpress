using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebCore.Internationalization;
using WebExpress.WebCore.WebAttribute;
using WebExpress.WebCore.WebComponent;
using WebExpress.WebCore.WebResource;
using WebExpress.WebUI.WebAttribute;
using WebExpress.WebUI.WebControl;

namespace InventoryExpress.WebPageSetting
{
    [Title("inventoryexpress:inventoryexpress.condition.add.label")]
    [Segment("add", "inventoryexpress:inventoryexpress.condition.add.label")]
    [Parent<PageSettingConditions>]
    [ContextPath("/")]
    [SettingHide()]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module<Module>]
    public sealed class PageSettingConditionAdd : PageWebAppSetting, IPageCondition
    {
        /// <summary>
        /// Returns the form.
        /// </summary>
        private ControlFormularCondition Form { get; } = new ControlFormularCondition("condition")
        {
            Edit = false
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public PageSettingConditionAdd()
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
            Form.FillFormular += FillFormular;
            Form.ProcessFormular += ProcessFormular;
        }

        /// <summary>
        /// Invoked when the form is initialized.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
        }

        /// <summary>
        /// Invoked when the form is about to be populated.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Fired when the form is about to be processed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            // create and save a new state
            var condition = new WebItemEntityCondition()
            {
                Name = Form.ConditionName.Value,
                Description = Form.Description.Value
            };

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.AddOrUpdateCondition(condition);

                transaction.Commit();
            }

            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.condition.notification.add"),
                    new ControlLink()
                    {
                        Text = condition.Name,
                        Uri = ViewModel.GetConditionUri(condition.Guid)
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: condition.Image,
                durability: 10000
            );

            //Form.RedirectUri = Form.RedirectUri.Append(condition.Id);
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
