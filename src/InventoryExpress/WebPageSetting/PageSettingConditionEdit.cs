using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.Parameter;
using InventoryExpress.WebControl;
using System;
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
    [Title("inventoryexpress:inventoryexpress.condition.edit.label")]
    [SegmentGuid<ParameterConditionId>("inventoryexpress:inventoryexpress.condition.edit.display")]
    [Parent<PageSettingConditions>]
    [ContextPath("/edit")]
    [SettingHide()]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module<Module>]
    public sealed class PageSettingConditionEdit : PageWebAppSetting, IPageCondition
    {
        /// <summary>
        /// Returns the form.
        /// </summary>
        private ControlFormularCondition Form { get; } = new ControlFormularCondition("condition")
        {
            Edit = true
        };

        /// <summary>
        /// Returns or sets the state.
        /// </summary>
        private WebItemEntityCondition Condition { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PageSettingConditionEdit()
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
            Form.ConditionName.Value = Condition?.Name;
            Form.Description.Value = Condition?.Description;
        }

        /// <summary>
        /// Fired when the form is about to be processed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            // change and save state
            Condition.Name = Form.ConditionName.Value;
            Condition.Description = Form.Description.Value;
            Condition.Updated = DateTime.Now;

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.AddOrUpdateCondition(Condition);

                transaction.Commit();
            }

            ComponentManager.GetComponent<NotificationManager>()?.AddNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.condition.notification.edit"),
                    new ControlLink()
                    {
                        Text = Condition.Name,
                        Uri = ViewModel.GetConditionUri(Condition.Guid)
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: Condition.Image,
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

            var guid = context.Request.GetParameter<ParameterConditionId>()?.Value;
            Condition = ViewModel.GetCondition(guid);

            context.Uri.Display = Condition.Name;
            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
