using InventoryExpress.Model;
using InventoryExpress.WebControl;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [Id("SettingGeneral")]
    [Title("inventoryexpress:inventoryexpress.setting.label")]
    [Segment("general", "inventoryexpress:inventoryexpress.setting.label")]
    [ContextPath("/Setting")]
    [SettingSection(SettingSection.Preferences)]
    [SettingIcon(TypeIcon.Wrench)]
    [SettingGroup("inventoryexpress:inventoryexpress.setting.general.label")]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("inventoryexpress")]
    [Context("admin")]
    public sealed class PageSettingGeneral : PageWebAppSetting
    {
        /// <summary>
        /// Das Einstellungsformular
        /// </summary>
        private ControlFormularSettings Form { get; } = new ControlFormularSettings("settings")
        {
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingGeneral()
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
            Form.FillFormular += FillFormular;
            Form.ProcessFormular += ProcessFormular;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular initialisiert wird
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular gefüllt werden soll
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">The event argument.</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
            var setting = ViewModel.GetSettings();

            Form.Currency.Value = setting?.Currency;
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">The trigger of the event.</param>
        /// <param name="e">Die Eventargumente/param>
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
