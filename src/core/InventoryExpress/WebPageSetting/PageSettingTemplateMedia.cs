using InventoryExpress.Model;
using InventoryExpress.Model.Entity;
using InventoryExpress.Model.WebItems;
using InventoryExpress.WebControl;
using System;
using System.IO;
using System.Linq;
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
    [ID("SettingTemplateMedia")]
    [Title("inventoryexpress:inventoryexpress.template.media.label")]
    [Segment("media", "inventoryexpress:inventoryexpress.template.media.display")]
    [Path("/Setting/SettingTemplate/SettingTemplateEdit")]
    [Module("inventoryexpress")]
    [SettingHide()]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Context("general")]
    [Context("media")]
    [Context("mediaedit")]
    public sealed class PageSettingTemplateMedia : PageWebAppSetting
    {
        /// <summary>
        /// Formular
        /// </summary>
        private ControlFormularMedia Form { get; } = new ControlFormularMedia("media");

        /// <summary>
        /// Liefert oder setzt die Vorlage
        /// </summary>
        private WebItemEntityTemplate Template { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageSettingTemplateMedia()
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
            Form.Tag.Value = Template.Media?.Tag;
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

            if (file != null)
            {
                ViewModel.AddOrUpdateMedia(Template.Media, file?.Data);
            }

            transaction.Commit();

            NotificationManager.CreateNotification
            (
                request: e.Context.Request,
                message: string.Format
                (
                    InternationalizationManager.I18N(Culture, "inventoryexpress:inventoryexpress.media.notification.edit"),
                    new ControlLink()
                    {
                        Text = Template.Name,
                        Uri = new UriRelative(ViewModel.GetTemplateUri(Template.ID))
                    }.Render(e.Context).ToString().Trim()
                ),
                icon: new UriRelative(Template.Image),
                durability: 10000
            );
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var guid = context.Request.GetParameter("TemplateID")?.Value;
            Template = ViewModel.GetTemplate(guid);

            context.VisualTree.Content.Preferences.Add(new ControlImage()
            {
                Uri = Template.Media != null ? new UriRelative(Template.Media?.Uri) : context.Uri.Root.Append("/assets/img/inventoryexpress.svg"),
                Width = 400,
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.Three)
            });

            context.VisualTree.Content.Primary.Add(Form);
        }
    }
}
