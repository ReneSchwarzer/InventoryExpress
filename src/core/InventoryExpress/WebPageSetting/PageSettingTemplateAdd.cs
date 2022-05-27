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
using WebExpress.WebApp.Wql;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace InventoryExpress.WebPageSetting
{
    [ID("SettingTemplateAdd")]
    [Title("inventoryexpress:inventoryexpress.template.add.label")]
    [Segment("add", "inventoryexpress:inventoryexpress.template.add.label")]
    [Path("/Setting/SettingTemplate")]
    [SettingHide()]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("inventoryexpress")]
    [Context("general")]
    [Context("templateadd")]
    public sealed class PageSettingTemplateAdd : PageWebAppSetting, IPageTemplate
    {
        /// <summary>
        /// Liefert das Formular
        /// </summary>
        private ControlFormularTemplate Form { get; } = new ControlFormularTemplate("template")
        {
            Edit = false
        };

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingTemplateAdd()
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
            Form.BackUri = e.Context.Uri.Take(-1);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular gefüllt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente</param>
        private void FillFormular(object sender, FormularEventArgs e)
        {
            foreach (var v in ViewModel.GetAttributes(new WqlStatement()))
            {
                Form.Attributes.Options.Add(new ControlFormularItemInputSelectionItem()
                {
                    ID = v.ID,
                    Label = v.Name
                });
            }

            Form.Attributes.Value = string.Empty;
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Formular verarbeitet werden soll.
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Die Eventargumente/param>
        private void ProcessFormular(object sender, FormularEventArgs e)
        {
            var file = e.Context.Request.GetParameter(Form.Image.Name) as ParameterFile;

            // Neue Vorlage erstellen und speichern
            var template = new WebItemEntityTemplate()
            {
                Name = Form.TemplateName.Value,
                Description = Form.Description.Value,
                Tag = Form.Tag.Value,
                Media = new WebItemEntityMedia()
                {
                    Name = file?.Value
                }
            };

            ViewModel.AddOrUpdateTemplate(template);
            ViewModel.Instance.SaveChanges();

            if (file != null)
            {
                ViewModel.AddOrUpdateMedia(template.Media, file?.Data);
                ViewModel.Instance.SaveChanges();
            }

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.template.notification.add"),
                    new ControlLink()
                    {
                        Text = template.Name,
                        Uri = new UriRelative(ViewModel.GetTemplateUri(template.ID))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(template.Image),
                durability: 10000
            );

            Form.RedirectUri = Form.RedirectUri.Append(template.ID);
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
