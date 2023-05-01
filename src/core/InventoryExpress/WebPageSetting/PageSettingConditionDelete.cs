using InventoryExpress.Model;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebControl;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [WebExID("SettingConditionDelete")]
    [WebExTitle("inventoryexpress:inventoryexpress.condition.delete.label")]
    [WebExSegmentGuid("ConditionID", "inventoryexpress:inventoryexpress.condition.delete.display")]
    [WebExContextPath("/Setting/SettingCondition/del")]
    [SettingHide()]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [WebExModule("inventoryexpress")]
    [WebExContext("general")]
    [WebExContext("conditiondelete")]
    public sealed class PageSettingConditionDelete : PageWebAppSetting, IPageCondition
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularConfirmDelete Form { get; } = new ControlFormularConfirmDelete("condition")
        {
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingConditionDelete()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Form.InitializeFormular += InitializeFormular;
            Form.Confirm += OnConfirmFormular;
            Form.RedirectUri = ResourceContext.ContextPath.Append("setting/conditions");
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("ConditionID")?.Value;
            var condition = ViewModel.GetCondition(guid);

            Form.Content.Text = string.Format(InternationalizationManager.I18N(e.Context, "inventoryexpress:inventoryexpress.condition.delete.description"), condition?.Name);
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular bestätigt urde.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Die Eventargumente/param>
        private void OnConfirmFormular(object sender, FormularEventArgs e)
        {
            var guid = e.Context.Request.GetParameter("ConditionID")?.Value;
            var condition = ViewModel.GetCondition(guid);

            using (var transaction = ViewModel.BeginTransaction())
            {
                ViewModel.DeleteCondition(guid);

                transaction.Commit();
            }

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.condition.notification.delete"),
                    new ControlText()
                    {
                        Text = condition.Name,
                        TextColor = new PropertyColorText(TypeColorText.Danger),
                        Format = TypeFormatText.Span
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: condition.Image,
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
