using InventoryExpress.Model;
using InventoryExpress.WebControl;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [WebExTitle("inventoryexpress:inventoryexpress.setting.label")]
    [WebExSegment("general", "inventoryexpress:inventoryexpress.setting.label")]
    [WebExContextPath("/setting")]
    [WebExSettingSection(WebExSettingSection.Preferences)]
    [WebExSettingIcon(TypeIcon.Wrench)]
    [WebExSettingGroup("inventoryexpress:inventoryexpress.setting.general.label")]
    [WebExSettingContext("webexpress.webapp:setting.tab.general.label")]
    [WebExModule(typeof(Module))]
    [WebExContext("admin")]
    public sealed class PageSettingGeneral : PageWebAppSetting
    {
        /// <summary>
        /// Das Einstellungsformular
        /// </summary>
        private ControlFormularSettings Form { get; } = new ControlFormularSettings("settings")
        {
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public PageSettingGeneral()
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
            var setting = ViewModel.GetSettings();

            Form.Currency.Value = setting?.Currency;
        }

        /// <summary>
        /// Fired when the form is about to be processed.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument./param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            using var transaction = ViewModel.BeginTransaction();

            var setting = ViewModel.GetSettings();

            // Einstellungen ändern und speichern
            setting.Currency = Form.Currency.Value;

            ViewModel.AddOrUpdateSettings(setting);

            transaction.Commit();
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            Form.RedirectUri = context.Uri;

            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
