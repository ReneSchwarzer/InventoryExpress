using InventoryExpress.Model;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using System.IO;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [ID("SettingConditionAdd")]
    [Title("inventoryexpress:inventoryexpress.condition.add.label")]
    [Segment("add", "inventoryexpress:inventoryexpress.condition.add.label")]
    [Path("/Setting/SettingCondition")]
    [SettingHide()]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("conditionadd")]
    public sealed class PageSettingConditionAdd : PageWebAppSetting, IPageCondition
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularCondition Form { get; } = new ControlFormularCondition("condition")
        {
            Edit = false
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingConditionAdd()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
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
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void InitializeFormular(object sender, FormularEventArgs e)
        {
            Form.RedirectUri = e.Context.Uri.Take(-1);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular gefüllt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            var file = e.Context.Request.GetParameter(Form.Image.Name) as ParameterFile;
            using var transaction = ViewModel.BeginTransaction();

            // Neuen Zustand erstellen und speichern
            var condition = new WebItemEntityCondition()
            {
                Name = Form.ConditionName.Value,
                Description = Form.Description.Value,
                Media = new WebItemEntityMedia()
                {
                    Name = file?.Value
                }
            };

            ViewModel.AddOrUpdateCondition(condition);

            if (file != null)
            {
                ViewModel.AddOrUpdateMedia(condition.Media, file?.Data);
            }

            transaction.Commit();

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.condition.notification.add"),
                    new ControlLink()
                    {
                        Text = condition.Name,
                        Uri = new UriRelative(ViewModel.GetConditionUri(condition.ID))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(condition.Image),
                durability: 10000
            );

            Form.RedirectUri = Form.RedirectUri.Append(condition.ID);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
